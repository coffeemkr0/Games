using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Lobby
{
    [ServiceContract
    (Namespace = "LegendsOfKesmaiSurvival.Services.Lobby",
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(ILobbyServiceCallback))]
    public interface ILobbyService
    {
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void Join(Guid key);

        [OperationContract(IsOneWay = false)]
        void Leave();
    }
}
