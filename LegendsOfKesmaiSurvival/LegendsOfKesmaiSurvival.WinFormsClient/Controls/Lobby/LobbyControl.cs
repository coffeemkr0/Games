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
    public partial class LobbyControl : UserControl
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

        #region Properties
        public string ChatDisplayText
        {
            get { return lobbyChatDisplay1.txtChat.Text; }
            set { lobbyChatDisplay1.txtChat.Text = value; }
        }
        #endregion

        public LobbyControl()
        {
            InitializeComponent();
        }

        private void txtChat_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Utility.Global.ChatClient.Say(Utility.Global.ServerKey, txtChat.Text);
                txtChat.Text = "";
            }
        }

        private void LobbyControl_Load(object sender, EventArgs e)
        {
            txtChat.Focus();
        }

        private void lobbyControlPanel1_StartNewGame(object sender, EventArgs e)
        {
            OnStartNewGame();
        }

        private void lobbyControlPanel1_JoinGame(Guid serverId)
        {
            OnJoinGame(serverId);
        }
    }
}
