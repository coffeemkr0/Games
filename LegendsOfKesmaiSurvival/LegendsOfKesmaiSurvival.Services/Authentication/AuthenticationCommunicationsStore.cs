using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.Authentication
{
    public class AuthenticationCommunicationsStore
    {
        /// <summary>
        /// Gets or sets the InstanceContext for the client
        /// </summary>
        public InstanceContext InstanceContext { get; set; }

        /// <summary>
        /// Gets or sets the IAuthenticationServiceCallback reference for the client
        /// </summary>
        public IAuthenticationServiceCallback Callback { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the client
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the UserAccount that the client is logged in with
        /// </summary>
        public Business.UserAccount UserAccount { get; set; }

        public AuthenticationCommunicationsStore(InstanceContext instanceContext, IAuthenticationServiceCallback callback)
        {
            this.InstanceContext = instanceContext;
            this.Callback = callback;
        }
    }
}
