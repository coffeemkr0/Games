using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.MatchMaking
{
    public interface IMatchMakingServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void JoinedGameCallback(LegendsOfKesmaiSurvival.Core.GameStateInformation.ServerInstanceInformation serverInstanceInformation);
    }
}
