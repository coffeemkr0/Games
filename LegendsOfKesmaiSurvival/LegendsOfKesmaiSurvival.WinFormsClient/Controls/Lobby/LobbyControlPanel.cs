using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby
{
    public partial class LobbyControlPanel : UserControl
    {
        #region Events
        public event EventHandler StartNewGame;
        protected virtual void OnStartNewGame()
        {
            EventHandler temp = StartNewGame;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public delegate void JoinGameEventHandler(Guid serverId);
        public event JoinGameEventHandler JoinGame;
        protected virtual void OnJoinGame(Guid serverId)
        {
            JoinGameEventHandler temp = JoinGame;
            if (temp != null)
            {
                temp(serverId);
            }
        }
        #endregion


        public LobbyControlPanel()
        {
            InitializeComponent();
        }

        private void btnStartNewGame_Click(object sender, EventArgs e)
        {
            OnStartNewGame();
        }

        private void btnRefreshGames_Click(object sender, EventArgs e)
        {
            lstGames.Items.Clear();
            //TODO:Need to get more information about games being played other than the server id
            foreach (Guid serverId in Utility.Global.MatchMakingClient.GetActiveGameList(Utility.Global.ServerKey))
            {
                lstGames.Items.Add(serverId.ToString());
            }
        }

        private void btnJoinGame_Click(object sender, EventArgs e)
        {
            if (lstGames.SelectedItem != null)
            {
                OnJoinGame(new Guid(lstGames.SelectedItem.ToString()));
            }
        }
    }
}
