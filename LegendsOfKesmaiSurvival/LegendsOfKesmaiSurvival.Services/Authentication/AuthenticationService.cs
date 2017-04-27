using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Channels;

namespace LegendsOfKesmaiSurvival.Services.Authentication
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class AuthenticationService : IAuthenticationService
    {
        #region Declarations
        /// <summary>
        /// Holds a list of user accounts and a reference to their instance context and call back channel so that we can perform operations on connected clients
        /// </summary>
        public static Dictionary<Guid, AuthenticationCommunicationsStore> AuthenticatedClients = new Dictionary<Guid, AuthenticationCommunicationsStore>();
        private int _clientId = 0;
        ServiceHosting.ServiceHostingManager _serviceHostingManager = new ServiceHosting.ServiceHostingManager();
        #endregion

        #region Properties
        public Uri MatchMakingUri { get; set; }

        public Uri LobbyUri { get; set; }

        public Uri ChatUri { get; set; }
        #endregion

        #region IAuthenticationService Methods
        public bool CreateNewUser(string userName, string nickName, string password, out string exception)
        {
            exception = "";

            try
            { 
                //Check to see if a user with the same user name or nickname already exists.
                if (Utility.Global.GetUserDataAccessor().UserNameExists(userName))
                {
                    exception = "An account with that user name already exists.";
                    return false;
                }
                if (Utility.Global.GetUserDataAccessor().NickNameExists(nickName))
                {
                    exception = "An account with that nick name already exists.";
                    return false;
                }

                Utility.Global.GetUserDataAccessor().CreateUserAccount(userName, nickName, password);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                exception = "Server error.";
                return false;
            }

            return true;
        }

        public bool Login(string userName, string password, out Guid key)
        {
            key = Guid.Empty;

            //TODO: Need to change this so that it sends back Invalid username or password if the credentials are invalid and some other message for errors
            try
            {
                Business.UserAccount userAccount = null;
                if (!Utility.Global.GetUserDataAccessor().ValidateUserCredentials(userName, password, out userAccount))
                {
                    return false;
                }

                //If the user is already logged in, we need to disconnect the existing connection and remove him from the list of connected clients
                RemoveUser(userAccount.Id);

                //Get the call back for the client
                IAuthenticationServiceCallback callback = OperationContext.Current.GetCallbackChannel<IAuthenticationServiceCallback>();

                //Create a CommunicationsStore to hold information about the connected client
                AuthenticationCommunicationsStore client = new AuthenticationCommunicationsStore(OperationContext.Current.InstanceContext, callback);
                client.UserAccount = userAccount;
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
                key = Guid.NewGuid();
                AuthenticatedClients.Add(key, client);

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return false;
            }
        }

        public List<Core.GameStateInformation.ServiceInformation> GetServiceInformation()
        {
            IsClientAuthenticated();//Throws an exception if the client is not found for some reason.

            List<Core.GameStateInformation.ServiceInformation> serviceInformation = new List<Core.GameStateInformation.ServiceInformation>();
            serviceInformation.Add(new Core.GameStateInformation.ServiceInformation(Core.GameStateInformation.ServiceInformation.ServiceTypes.MatchMaking, this.MatchMakingUri));
            serviceInformation.Add(new Core.GameStateInformation.ServiceInformation(Core.GameStateInformation.ServiceInformation.ServiceTypes.Lobby, this.LobbyUri));
            serviceInformation.Add(new Core.GameStateInformation.ServiceInformation(Core.GameStateInformation.ServiceInformation.ServiceTypes.Chat, this.ChatUri));

            return serviceInformation;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Initializes the authentication service and starts all other services.
        /// </summary>
        public void Initialize()
        {
            //TODO:Need to initialize a watchdog when the service is hosted to remove disconnected clients from all services.

            //TODO:Need to consider putting these in their own process instead of sharing the Authenication Service's process.
            Services.MatchMaking.MatchMakingService matchMakingService = new MatchMaking.MatchMakingService();
            Services.ServiceHosting.ServiceHostingManager.HostSingletonService(matchMakingService, this.MatchMakingUri, typeof(Services.MatchMaking.IMatchMakingService));
            Console.WriteLine("Match Making initialized and hosted at {0}", this.MatchMakingUri);

            Services.Lobby.LobbyService lobbyService = new Lobby.LobbyService();
            Services.ServiceHosting.ServiceHostingManager.HostSingletonService(lobbyService, this.LobbyUri, typeof(Services.Lobby.ILobbyService));
            Console.WriteLine("Lobby initialized and hosted at {0}", this.LobbyUri);

            Services.Chat.ChatService chatService = new Chat.ChatService();
            Services.ServiceHosting.ServiceHostingManager.HostSingletonService(chatService, this.ChatUri, typeof(Services.Chat.IChatService));
            Console.WriteLine("Chat initialized and hosted at {0}", this.ChatUri);
        }

        /// <summary>
        /// Gets a user account from the list of authenticated clients.
        /// </summary>
        /// <param name="key">The key from the client that was assigned to it by the server.</param>
        /// <returns>the UserAccount with the specified key if it exists, otherwise null.</returns>
        public static Business.UserAccount GetUserAccount(Guid key)
        {
            if (!AuthenticatedClients.ContainsKey(key)) return null;

            return AuthenticatedClients[key].UserAccount;
        }
        #endregion

        #region Private Methods
        private void RemoveUser(int userId)
        {
            //TODO:May need to lock the list here for thread safety.
            foreach (KeyValuePair<Guid, AuthenticationCommunicationsStore> client in AuthenticatedClients)
            {
                if (client.Value.UserAccount.Id == userId)
                {
                    //TODO:Remove the user from all other serives.
                    AuthenticatedClients[client.Key].InstanceContext.IncomingChannels.FirstOrDefault().Close();
                    AuthenticatedClients.Remove(client.Key);
                }
            }
        }

        private bool IsClientAuthenticated()
        {
            return GetConnectedUserId() > 0;
        }

        private int GetConnectedUserId()
        {
            InstanceContext isntanceContext = OperationContext.Current.InstanceContext;

            //Iterate over the connected clients and get the client that has the same instance context
            foreach (KeyValuePair<Guid, AuthenticationCommunicationsStore> client in AuthenticatedClients)
            {
                if (client.Value.InstanceContext == isntanceContext)
                {
                    return client.Value.UserAccount.Id;
                }
            }

            throw new Exception("Could not find the client in the list of connected clients.");
        }
        #endregion
    }
}
