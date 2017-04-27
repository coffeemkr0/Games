using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.ConsoleServer
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //TODO:Make port configurable

                //Create an instance of the AuthenicationService singleton and set the Uris for the other services for it to launch.
                Services.Authentication.AuthenticationService authenticationService = new Services.Authentication.AuthenticationService();

                string baseAddress = "net.tcp://" + System.Environment.MachineName + ":5200/LegendsOfKesmaiSurvival";
                authenticationService.MatchMakingUri = new Uri(baseAddress + "/MatchMaking");
                authenticationService.LobbyUri = new Uri(baseAddress + "/Lobby");
                authenticationService.ChatUri = new Uri(baseAddress + "/Chat");
                authenticationService.Initialize();

                //Host the authentication service
                Services.ServiceHosting.ServiceHostingManager.HostSingletonService(authenticationService, new Uri(baseAddress), typeof(Services.Authentication.IAuthenticationService));
                Console.WriteLine("Authentication service initialized and hosted at {0}", baseAddress);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            Console.ReadLine();
        }
    }
}
