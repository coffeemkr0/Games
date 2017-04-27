using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.ServiceModel.Discovery;
using System.Xml.Linq;
using System.Net;
using System.Net.Sockets;

namespace LegendsOfKesmaiSurvival.Services.ServiceHosting
{
    /// <summary>
    /// A class that can be used to host and manage WCF services that reside in the LegendsOfKesmaiSurvival.Services assembly.
    /// </summary>
    public class ServiceHostingManager
    {
        #region Properties
        private List<NamedServiceHost> _serviceHosts = new List<NamedServiceHost>();
        /// <summary>
        /// Gets or sets a list of <see cref="System.ServiceModel.ServiceHost">ServiceHost</see> that host a service for the server.
        /// </summary>
        public List<NamedServiceHost> ServiceHosts
        {
            get { return _serviceHosts; }
            set { _serviceHosts = value; }
        }
        #endregion

        #region Public Methods
        public static void HostSingletonService(object singletonInstance, Uri uri, Type endPointType)
        {
            ServiceHost host = new ServiceHost(singletonInstance, uri);

            //Uncomment this line to send all incoming and outoing messages to the console
            //host.Description.Behaviors.Add(new ConsoleMessageTracing());

            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None, true);
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.SendTimeout = TimeSpan.FromSeconds(10);

            //// ** DISCOVERY ** //
            //// make the service discoverable by adding the discovery behavior
            //host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());

            //// ** DISCOVERY ** //
            //// add the discovery endpoint that specifies where to publish the services
            //host.AddServiceEndpoint(new UdpDiscoveryEndpoint());

            ServiceEndpoint endpoint = host.AddServiceEndpoint(endPointType, binding, "");

            ////Add exception marshalling behavior to the host so that it can throw exceptions to clients
            //endpoint.Behaviors.Add(new LegendsOfKesmaiSurvival.Core.Exceptions.ExceptionMarshallingBehavior());

            host.Open();
        }

        public static void HostService(Uri uri, Type serviceType, Type endPointType)
        {
            ServiceHost host = new ServiceHost(serviceType, uri);

            //Uncomment this line to send all incoming and outoing messages to the console
            //host.Description.Behaviors.Add(new ConsoleMessageTracing());

            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None, true);
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.SendTimeout = TimeSpan.FromSeconds(10);

            //// ** DISCOVERY ** //
            //// make the service discoverable by adding the discovery behavior
            //host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());

            //// ** DISCOVERY ** //
            //// add the discovery endpoint that specifies where to publish the services
            //host.AddServiceEndpoint(new UdpDiscoveryEndpoint());


            ServiceEndpoint endpoint = host.AddServiceEndpoint(endPointType, binding, "");

            //Add exception marshalling behavior to the host so that it can throw exceptions to clients
            endpoint.Behaviors.Add(new LegendsOfKesmaiSurvival.Core.Exceptions.ExceptionMarshallingBehavior());

            host.Open();
        }

        public LegendsOfKesmaiSurvival.Core.GameStateInformation.ServerInstanceInformation HostZombiesInstance(Zombies.ZombiesService zombiesService)
        {
            LegendsOfKesmaiSurvival.Core.GameStateInformation.ServerInstanceInformation serverInstanceInformation = new Core.GameStateInformation.ServerInstanceInformation();
            serverInstanceInformation.ServerId = Guid.NewGuid();

            //TODO:Currently this will get any free tcp port on the server.  This works but it needs to be a known range so that port forwarding can
            //be setup for that known range.
            string baseAddress = "net.tcp://" + System.Environment.MachineName + ":" + GetFreeTcpPort().ToString() + "/LegendsOfKesmaiSurvival/Zombies";

            NamedServiceHost host = new NamedServiceHost(zombiesService, new Uri(baseAddress));
            host.Name = serverInstanceInformation.ServerId.ToString();

            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None, true);
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.SendTimeout = TimeSpan.FromSeconds(10);

            ServiceEndpoint gamePlayEndpoint = host.AddServiceEndpoint(typeof(Zombies.IGameplay), binding, "/Gameplay");
            serverInstanceInformation.GameplayUri = new Uri(baseAddress + "/Gameplay");

            host.ServerInstanceInformation = serverInstanceInformation;
            host.Open();

            this.ServiceHosts.Add(host);

            return serverInstanceInformation;
        }
        #endregion

        #region Private Methods
        private void InitializeNamedServiceHost(string name, string address, Type serviceType, Type endPointType)
        {
            NamedServiceHost host = new NamedServiceHost(serviceType, new Uri(address));
            host.Name = name;

            //Uncomment this line to send all incoming and outoing messages to the console
            //host.Description.Behaviors.Add(new ConsoleMessageTracing());

            NetTcpBinding binding = new NetTcpBinding(SecurityMode.None, true);
            binding.MaxBufferSize = int.MaxValue;
            binding.MaxReceivedMessageSize = int.MaxValue;
            binding.ReaderQuotas.MaxArrayLength = int.MaxValue;
            binding.SendTimeout = TimeSpan.FromSeconds(10);

            //// ** DISCOVERY ** //
            //// make the service discoverable by adding the discovery behavior
            //host.Description.Behaviors.Add(new ServiceDiscoveryBehavior());

            //// ** DISCOVERY ** //
            //// add the discovery endpoint that specifies where to publish the services
            //host.AddServiceEndpoint(new UdpDiscoveryEndpoint());


            ServiceEndpoint endpoint = host.AddServiceEndpoint(endPointType, binding, "");

            //Add exception marshalling behavior to the host so that it can throw exceptions to clients
            endpoint.Behaviors.Add(new LegendsOfKesmaiSurvival.Core.Exceptions.ExceptionMarshallingBehavior());

            host.Open();

            this.ServiceHosts.Add(host);
        }

        private int GetFreeTcpPort()
        {
            TcpListener listener = null;
            try
            {
                listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 0);
                listener.Start();
                int port = ((IPEndPoint)listener.LocalEndpoint).Port;
                return port;
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                listener.Stop();
            }
        }
        #endregion
    }
}
