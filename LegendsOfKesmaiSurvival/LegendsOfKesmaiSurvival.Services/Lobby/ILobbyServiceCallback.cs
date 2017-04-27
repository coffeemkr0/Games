using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace LegendsOfKesmaiSurvival.Services.Lobby
{
    public interface ILobbyServiceCallback
    {
        [OperationContract(IsOneWay = true)]
        void PlayerJoinedCallback(string nickName);
    }
}
