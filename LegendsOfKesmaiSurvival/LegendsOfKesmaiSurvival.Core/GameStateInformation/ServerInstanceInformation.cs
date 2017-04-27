using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Core.GameStateInformation
{
    public class ServerInstanceInformation
    {
        public Uri GameplayUri { get; set; }

        public Guid ServerId { get; set; }

        public override string ToString()
        {
            return string.Format("ServerId:{0} Uri:{1}", this.ServerId.ToString(), this.GameplayUri.ToString());
        }
    }
}
