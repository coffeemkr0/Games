using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Core.GameStateInformation
{
    /// <summary>
    /// Represents information about a service that a client can connect to.
    /// </summary>
    public class ServiceInformation
    {
        public enum ServiceTypes
        {
            Lobby,
            MatchMaking,
            Chat
        }

        /// <summary>
        /// Gets or sets the Uri for the service.
        /// </summary>
        public Uri ServiceUri { get; set; }

        /// <summary>
        /// Gets or sets the type of service that the information represents.
        /// </summary>
        public ServiceTypes ServiceType { get; set; }

        public ServiceInformation()
        {

        }

        public ServiceInformation(ServiceTypes serviceType, Uri uri)
        {
            this.ServiceType = serviceType;
            this.ServiceUri = uri;
        }
    }
}
