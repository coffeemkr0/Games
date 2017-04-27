using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.WinFormsClient.ServerCommunication
{
    public class GameplayCallbackHandler : Zombies.IGameplayCallback
    {
        #region Events
        public delegate void ChatReceievedEventHandler(string text);
        public event ChatReceievedEventHandler ChatReceived;
        protected virtual void OnChatReceived(string text)
        {
            ChatReceievedEventHandler temp = ChatReceived;
            if (temp != null)
            {
                temp(text);
            }
        }

        public delegate void GameStateUpdatedEventHandler(Core.GameStateInformation.GameStateUpdate gameStateUpdate);
        public event GameStateUpdatedEventHandler GameStateUpdated;
        protected virtual void OnGameStateUpdated(Core.GameStateInformation.GameStateUpdate gameStateUpdate)
        {
            GameStateUpdatedEventHandler temp = GameStateUpdated;
            if (temp != null)
            {
                temp(gameStateUpdate);
            }
        }
        #endregion

        public void ChatReceivedCallback(string text)
        {
            OnChatReceived(text);
        }

        public void GameStateUpdatedCallback(Core.GameStateInformation.GameStateUpdate gameStateUpdate)
        {
            OnGameStateUpdated(gameStateUpdate);
        }
    }
}
