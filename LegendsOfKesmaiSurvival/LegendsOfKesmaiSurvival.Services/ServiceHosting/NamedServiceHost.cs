using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.ServiceHosting
{
    /// <summary>
    /// Extends the <see cref="System.ServiceModel.ServiceHost">ServiceHost</see> class by adding a Name property to it.
    /// </summary>
    public class NamedServiceHost : System.ServiceModel.ServiceHost
    {
        /// <summary>
        /// Gets or sets the Name of the service host
        /// </summary>
        public string Name { get; set; }

        public Core.GameStateInformation.ServerInstanceInformation ServerInstanceInformation { get; set; }
        
        public NamedServiceHost(Type serviceType, params Uri[] baseAddress)
            : base(serviceType, baseAddress)
        {

        }

        public NamedServiceHost(object singletonInstance, params Uri[] baseAddress)
            : base(singletonInstance, baseAddress)
        {

        }
    }
}
