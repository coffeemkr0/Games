using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Description;


namespace LegendsOfKesmaiSurvival.Services.ServiceHosting
{
    public class ConsoleMessageTracing : Attribute, IEndpointBehavior, IServiceBehavior
    {
        void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint endpoint,
            ClientRuntime clientRuntime)
        {
            clientRuntime.MessageInspectors.Add(new ConsoleMessageTracer());
        }
        void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint endpoint,
            EndpointDispatcher endpointDispatcher)
        {
            endpointDispatcher.DispatchRuntime.MessageInspectors.Add(
                new ConsoleMessageTracer());
        }

        void IServiceBehavior.ApplyDispatchBehavior(
            ServiceDescription desc, ServiceHostBase host)
        {
            foreach (
                ChannelDispatcher cDispatcher in host.ChannelDispatchers)
                foreach (EndpointDispatcher eDispatcher in
                    cDispatcher.Endpoints)
                    eDispatcher.DispatchRuntime.MessageInspectors.Add(
                        new ConsoleMessageTracer());
        }

        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {

        }

        public void Validate(ServiceEndpoint endpoint)
        {

        }

        public void AddBindingParameters(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {

        }

        public void Validate(ServiceDescription serviceDescription, ServiceHostBase serviceHostBase)
        {

        }
    }
}
