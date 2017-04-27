using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.Zombies
{
    public class GameplayCommunicationsStore
    {
        /// <summary>
        /// Gets or sets the InstanceContext for the client
        /// </summary>
        public InstanceContext InstanceContext { get; set; }

        /// <summary>
        /// Gets or sets the ISurvivalInstanceServiceGameplayCallback reference for the client
        /// </summary>
        public IGameplayCallback Callback { get; set; }

        /// <summary>
        /// Gets or sets the IP address of the client
        /// </summary>
        public string IPAddress { get; set; }

        /// <summary>
        /// Gets or sets the UserAccount that the client is logged in with
        /// </summary>
        public Business.UserAccount UserAccount { get; set; }

        /// <summary>
        /// Gets or sets the Id of the player character that the player has control of
        /// </summary>
        public int PlayerCharacterId { get; set; }

        public GameplayCommunicationsStore(InstanceContext instanceContext, IGameplayCallback callback)
        {
            this.InstanceContext = instanceContext;
            this.Callback = callback;
        }
    }
}
