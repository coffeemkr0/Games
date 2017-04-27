using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;

namespace LegendsOfKesmaiSurvival.Services.Lobby
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class LobbyService : ILobbyService
    {
        #region Declarations
        public static Dictionary<Guid, LobbyCommunicationsStore> ConnectedClients = new Dictionary<Guid, LobbyCommunicationsStore>();
        #endregion

        public void Join(Guid key)
        {
            //TODO:Need to send a callback to the client to let it know that it connected successfully or make this a one way call
            try
            {
                //Make sure the client has been authenticated
                if (!Utility.Global.IsClientAuthenticated(key)) return;

                //Get the call back for the client
                ILobbyServiceCallback callback = OperationContext.Current.GetCallbackChannel<ILobbyServiceCallback>();

                //Create a CommunicationsStore to hold information about the connected client
                LobbyCommunicationsStore client = new LobbyCommunicationsStore(OperationContext.Current.InstanceContext, callback);

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

                Console.WriteLine("{0} has entered the lobby.", client.UserAccount.Nickname);

                //Broadcast a message to all players in the lobby that the player has joined (including the one that just joined).
                foreach (KeyValuePair<Guid, LobbyCommunicationsStore> connectedClient in ConnectedClients)
                {
                    connectedClient.Value.Callback.PlayerJoinedCallback(client.UserAccount.Nickname);
                }
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
    }
}
