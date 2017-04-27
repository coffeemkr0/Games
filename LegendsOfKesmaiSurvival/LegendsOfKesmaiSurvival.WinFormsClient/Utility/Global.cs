using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.WinFormsClient.Utility
{
    public static class Global
    {
        #region Properties
        /// <summary>
        /// Gets or sets a key that the client gets from the server so that the server can validate that the client has been authenticated.
        /// </summary>
        public static Guid ServerKey { get; set; }

        /// <summary>
        /// Gets or sets the configuration for the client
        /// </summary>
        public static Config Config { get; set; }

        public static bool AuthenticationClientInitialized { get; set; }

        public static Authentication.AuthenticationServiceClient AuthenicationClient { get; set; }

        public static Lobby.LobbyServiceClient LobbyClient { get; set; }

        public static ServerCommunication.LobbyCallbackHandler LobbyCallbackHandler { get; set; }

        public static MatchMaking.MatchMakingServiceClient MatchMakingClient { get; set; }

        public static Chat.ChatServiceClient ChatClient { get; set; }

        public static ServerCommunication.ChatCallbackHandler ChatCallbackHandler { get; set; }

        public static ServerCommunication.MatchMakingCallbackHandler MatchMakingCallbackHandler { get; set; }

        public static Zombies.GameplayClient GamePlayClient { get; set; }

        public static ServerCommunication.GameplayCallbackHandler GameplayCallbackHandler { get; set; }
        #endregion

        public static void InitializeAuthenticationClient()
        {
            NetTcpBinding binding = new NetTcpBinding(System.ServiceModel.SecurityMode.None, true);
            binding.SendTimeout = TimeSpan.FromSeconds(5);
            EndpointAddress address = new EndpointAddress(new Uri(Config.ServerAddress));

            AuthenicationClient = new Authentication.AuthenticationServiceClient(binding, address);

            AuthenticationClientInitialized = true;

            //NetTcpBinding binding = new NetTcpBinding(System.ServiceModel.SecurityMode.None, true);
            //binding.SendTimeout = TimeSpan.FromSeconds(5);
            //EndpointAddress address = new EndpointAddress(new Uri(Global.Config.ServerAddress));
            //LegendsServiceCallBackHandler = new LegendsServiceCallbackHandler();
            //InstanceContext instanceContext = new InstanceContext(LegendsServiceCallBackHandler);
            //Global.LegendsServiceClient = new LegendsService.LegendsServiceClient(instanceContext, binding, address);
            //Global.LegendsServiceClient.ChannelFactory.Endpoint.Behaviors.Add(new Legends.Core.Exceptions.ExceptionMarshallingBehavior());
        }

        public static void InitializeGameplayClient(Uri gamePlayUri)
        {
            NetTcpBinding binding = new NetTcpBinding(System.ServiceModel.SecurityMode.None, true);
            binding.SendTimeout = TimeSpan.FromSeconds(5);
            EndpointAddress address = new EndpointAddress(gamePlayUri);

            GameplayCallbackHandler = new ServerCommunication.GameplayCallbackHandler();
            InstanceContext instanceContext = new InstanceContext(GameplayCallbackHandler);
            GamePlayClient = new Zombies.GameplayClient(instanceContext, binding, address);
        }

        public static void InitializeClients()
        {
            Core.GameStateInformation.ServiceInformation[] servicesInformation = AuthenicationClient.GetServiceInformation();

            foreach (Core.GameStateInformation.ServiceInformation serviceInformation in servicesInformation)
            {
                NetTcpBinding binding = new NetTcpBinding(System.ServiceModel.SecurityMode.None, true);
                binding.SendTimeout = TimeSpan.FromSeconds(5);
                EndpointAddress address = new EndpointAddress(serviceInformation.ServiceUri);

                switch (serviceInformation.ServiceType)
                {
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.ServiceInformation.ServiceTypes.Lobby:
                        LobbyCallbackHandler = new ServerCommunication.LobbyCallbackHandler();
                        InstanceContext lobbyInstanceContext = new InstanceContext(LobbyCallbackHandler);
                        LobbyClient = new Lobby.LobbyServiceClient(lobbyInstanceContext, binding, address);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.ServiceInformation.ServiceTypes.MatchMaking:
                        MatchMakingCallbackHandler = new ServerCommunication.MatchMakingCallbackHandler();
                        InstanceContext matchMakingInstanceContext = new InstanceContext(MatchMakingCallbackHandler);
                        MatchMakingClient = new MatchMaking.MatchMakingServiceClient(matchMakingInstanceContext, binding, address);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.ServiceInformation.ServiceTypes.Chat:
                        ChatCallbackHandler = new ServerCommunication.ChatCallbackHandler();
                        InstanceContext chatInstanceContext = new InstanceContext(ChatCallbackHandler);
                        ChatClient = new Chat.ChatServiceClient(chatInstanceContext, binding, address);
                        break;
                }
            }
        }
    }
}
