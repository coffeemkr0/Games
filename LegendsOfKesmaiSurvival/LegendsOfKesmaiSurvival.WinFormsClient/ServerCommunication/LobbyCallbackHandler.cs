using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.WinFormsClient.ServerCommunication
{
    public class LobbyCallbackHandler : Lobby.ILobbyServiceCallback
    {
        #region Events
        public delegate void PlayerJoinedEventHandler(string nickName);
        public event PlayerJoinedEventHandler PlayerJoined;
        protected virtual void OnPlayerJoined(string nickName)
        {
            PlayerJoinedEventHandler temp = PlayerJoined;
            if (temp != null)
            {
                temp(nickName);
            }
        }
        #endregion

        public void PlayerJoinedCallback(string nickName)
        {
            OnPlayerJoined(nickName);
        }
    }
}
