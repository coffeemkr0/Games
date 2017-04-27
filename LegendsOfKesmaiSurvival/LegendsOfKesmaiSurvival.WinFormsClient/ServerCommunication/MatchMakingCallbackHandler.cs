using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.WinFormsClient.ServerCommunication
{
    public class MatchMakingCallbackHandler : MatchMaking.IMatchMakingServiceCallback
    {
        #region Events
        public delegate void JoinedGameEventHandler(Core.GameStateInformation.ServerInstanceInformation serverInstanceInformation);
        public event JoinedGameEventHandler JoinedGame;
        protected virtual void OnJoinedGame(Core.GameStateInformation.ServerInstanceInformation serverInstanceInformation)
        {
            JoinedGameEventHandler temp = JoinedGame;
            if (temp != null)
            {
                temp(serverInstanceInformation);
            }
        }
        #endregion

        public void JoinedGameCallback(Core.GameStateInformation.ServerInstanceInformation serverInstanceInformation)
        {
            OnJoinedGame(serverInstanceInformation);
        }
    }
}
