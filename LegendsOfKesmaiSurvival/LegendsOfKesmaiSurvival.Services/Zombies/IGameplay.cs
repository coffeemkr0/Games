using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.Zombies
{
    [ServiceContract
    (Namespace = "LegendsOfKesmaiSurvival.Services.Zombies",
    SessionMode = SessionMode.Required,
    CallbackContract = typeof(IGameplayCallback))]
    public interface IGameplay
    {
        [OperationContract(IsInitiating = true, IsOneWay = true)]
        void Join(Guid key);

        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void SendChat(Guid key, string text);

        [OperationContract(IsInitiating = false, IsOneWay = true)]
        void Move(Guid key, List<Core.GameStateInformation.Directions> movements);
    }
}
