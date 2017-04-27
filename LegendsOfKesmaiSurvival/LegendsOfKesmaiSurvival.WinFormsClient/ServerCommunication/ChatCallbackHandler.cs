using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.WinFormsClient.ServerCommunication
{
    public class ChatCallbackHandler : Chat.IChatServiceCallback
    {
        #region Events
        public delegate void PlayerSaidEventHandler(string nickName, string text);
        public event PlayerSaidEventHandler PlayerSaid;
        protected virtual void OnPlayerSaid(string nickName, string text)
        {
            PlayerSaidEventHandler temp = PlayerSaid;
            if (temp != null)
            {
                temp(nickName, text);
            }
        }
        #endregion

        public void SayCallback(string nickName, string text)
        {
            OnPlayerSaid(nickName, text);
        }
    }
}
