using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;

namespace LegendsOfKesmaiSurvival.Services.Chat
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ChatService : IChatService
    {
        #region Declarations
        public static Dictionary<Guid, ChatCommunicationsStore> ConnectedClients = new Dictionary<Guid, ChatCommunicationsStore>();
        #endregion

        public void Join(Guid key)
        {
            //TODO:Need to send a callback to the client to let it know that it connected successfully or make this a one way call
            try
            {
                //Make sure the client has been authenticated
                if (!Utility.Global.IsClientAuthenticated(key)) return;

                //Get the call back for the client
                IChatServiceCallback callback = OperationContext.Current.GetCallbackChannel<IChatServiceCallback>();

                //Create a CommunicationsStore to hold information about the connected client
                ChatCommunicationsStore client = new ChatCommunicationsStore(OperationContext.Current.InstanceContext, callback);

                //TODO:Need to handle the client not being authenticated
                client.UserAccount = Authentication.AuthenticationService.GetUserAccount(key);

                if (!OperationContext.Current.IncomingMessageProperties.ContainsKey(RemoteEndpointMessageProperty.Name))
                {
                    //This is for dev connections.
                    client.IPAddress = "127.0.0.1";
                }
                else
                {
                    client.IPAddress = ((RemoteEndpointMessageProperty)OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
                }

                //TODO:May need to lock the list here for thread safety
                ConnectedClients.Add(key, client);

                Console.WriteLine("{0} has joined the chat service.", client.UserAccount.Nickname);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Leave()
        {
            throw new NotImplementedException();
        }

        public void Say(Guid key, string text)
        {
            //Make sure the client has been authenticated
            if (!Utility.Global.IsClientAuthenticated(key)) return;

            //Broadcast the chat to all players in the lobby, including the one that sent it
            foreach (KeyValuePair<Guid, ChatCommunicationsStore> connectedClient in ConnectedClients)
            {
                connectedClient.Value.Callback.SayCallback(Authentication.AuthenticationService.GetUserAccount(key).Nickname, text);
            }
        }

        public void Whisper(Guid key, string toNickName, string text)
        {
            
        }
    }
}
