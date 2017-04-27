using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using LegendsOfKesmaiSurvival.Core.Exceptions;

namespace LegendsOfKesmaiSurvival.Services.MatchMaking
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class MatchMakingService : IMatchMakingService
    {
        #region Declarations
        /// <summary>
        /// Holds a list of user IDs and a reference to their instance context and call back channel so that we can perform operations on connected clients
        /// </summary>
        public static Dictionary<Guid, MatchMakingCommunicationsStore> ConnectedClients = new Dictionary<Guid, MatchMakingCommunicationsStore>();
        ServiceHosting.ServiceHostingManager _serviceHostingManager = new ServiceHosting.ServiceHostingManager();
        #endregion

        #region IMatchMakingService Methods
        public void Join(Guid key)
        {
            //TODO:Need to send a callback to the client to let it know that it connected successfully or make this a one way call
            try
            {
                //Make sure the client has been authenticated
                if (!Utility.Global.IsClientAuthenticated(key)) return;

                //Get the call back for the client
                IMatchMakingServiceCallback callback = OperationContext.Current.GetCallbackChannel<IMatchMakingServiceCallback>();

                //Create a CommunicationsStore to hold information about the connected client
                MatchMakingCommunicationsStore client = new MatchMakingCommunicationsStore(OperationContext.Current.InstanceContext, callback);

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

                Console.WriteLine("{0} has joined the match making service.", client.UserAccount.Nickname);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void Leave(Guid key)
        {
            throw new NotImplementedException();
        }

        public void StartNewGame(Guid key)
        {
            try
            {
                //Make sure the client has been authenticated
                if (!Utility.Global.IsClientAuthenticated(key)) return;

                //Host a new instance of the SurvivalInstanceService service.
                Zombies.ZombiesService zombiesService = new Zombies.ZombiesService();

                //Start the game by loading the test map
                zombiesService.StartGame(@"Maps\Test.map");

                LegendsOfKesmaiSurvival.Core.GameStateInformation.ServerInstanceInformation serverInstanceInformation = _serviceHostingManager.HostZombiesInstance(zombiesService);

                Console.WriteLine("New instance started:" + serverInstanceInformation.ToString());

                //Tell the client to join the new instance
                GetCallback().JoinedGameCallback(serverInstanceInformation);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        public void JoinGame(Guid key, Guid serverId)
        {
            //Make sure the client has been authenticated
            if (!Utility.Global.IsClientAuthenticated(key)) return;

            ServiceHosting.NamedServiceHost server = _serviceHostingManager.ServiceHosts.Where(s => s.ServerInstanceInformation.ServerId == serverId).SingleOrDefault<ServiceHosting.NamedServiceHost>();
            if (server != null)
            {
                GetCallback().JoinedGameCallback(server.ServerInstanceInformation);
            }
        }

        public List<Guid> GetActiveGameList(Guid key)
        {
            //Make sure the client has been authenticated
            if (!Utility.Global.IsClientAuthenticated(key)) return null;

            List<Guid> serverIds = new List<Guid>();

            foreach (ServiceHosting.NamedServiceHost server in _serviceHostingManager.ServiceHosts)
            {
                if (server.ServerInstanceInformation != null)
                {
                    serverIds.Add(server.ServerInstanceInformation.ServerId);
                }
            }

            return serverIds;
        }
        #endregion

        #region Private Methods
        private IMatchMakingServiceCallback GetCallback()
        {
            return OperationContext.Current.GetCallbackChannel<IMatchMakingServiceCallback>();
        }

        private ZombiesClient.AdministrationClient GetAdministationClient(Uri uri, out AdministrationCallbackHandler callbackHandler)
        {
            NetTcpBinding binding = new NetTcpBinding(System.ServiceModel.SecurityMode.None, true);
            binding.SendTimeout = TimeSpan.FromSeconds(10);
            EndpointAddress address = new EndpointAddress(uri);

            callbackHandler = new AdministrationCallbackHandler();

            InstanceContext instanceContext = new InstanceContext(callbackHandler);

            return new ZombiesClient.AdministrationClient(instanceContext, binding, address);
        }
        #endregion
    }
}
