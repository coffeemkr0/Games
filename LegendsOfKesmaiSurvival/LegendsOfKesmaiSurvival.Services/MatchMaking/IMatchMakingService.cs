using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.MatchMaking
{
    [ServiceContract
    (Namespace = "LegendsOfKesmaiSurvival.Services.MatchMaking",
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(IMatchMakingServiceCallback))]
    public interface IMatchMakingService
    {
        [OperationContract(IsInitiating = true, IsOneWay = false)]
        void Join(Guid key);

        [OperationContract(IsOneWay = false)]
        void Leave(Guid key);

        [OperationContract(IsOneWay = true)]
        void StartNewGame(Guid key);

        [OperationContract(IsOneWay = true)]
        void JoinGame(Guid key, Guid serverId);

        [OperationContract(IsOneWay = false)]
        List<Guid> GetActiveGameList(Guid key);
    }
}
