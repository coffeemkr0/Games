using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.Zombies
{
    public interface IGameplayCallback
    {
        [OperationContract(IsOneWay = true)]
        void ChatReceivedCallback(string text);

        [OperationContract(IsOneWay = true)]
        void GameStateUpdatedCallback(Core.GameStateInformation.GameStateUpdate gameStateUpdate);
    }
}
