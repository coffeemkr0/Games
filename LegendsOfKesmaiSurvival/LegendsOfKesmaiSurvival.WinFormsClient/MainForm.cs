using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.ServiceModel;
using System.ServiceModel.Discovery;

namespace LegendsOfKesmaiSurvival.WinFormsClient
{
    public partial class MainForm : Form
    {
        #region Declarations
        private bool _testMode;
        #endregion

        public MainForm()
        {
            InitializeComponent();
        }

        public MainForm(bool testMode)
            : this()
        {
            _testMode = testMode;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!_testMode)
            {
                Utility.Global.LobbyCallbackHandler.PlayerJoined += new ServerCommunication.LobbyCallbackHandler.PlayerJoinedEventHandler(LobbyCallbackHandler_PlayerJoined);
                Utility.Global.ChatCallbackHandler.PlayerSaid += new ServerCommunication.ChatCallbackHandler.PlayerSaidEventHandler(ChatCallbackHandler_PlayerSaid);

                Utility.Global.LobbyClient.Join(Utility.Global.ServerKey);
                Utility.Global.ChatClient.Join(Utility.Global.ServerKey);
                Utility.Global.MatchMakingClient.Join(Utility.Global.ServerKey);

                Utility.Global.MatchMakingCallbackHandler.JoinedGame += new ServerCommunication.MatchMakingCallbackHandler.JoinedGameEventHandler(MatchMakingCallbackHandler_JoinedGame);
            }
        }

        void ChatCallbackHandler_PlayerSaid(string nickName, string text)
        {
            lobbyControl.ChatDisplayText += string.Format("{0}:{1}\r\n", nickName, text);
        }

        void LobbyCallbackHandler_PlayerJoined(string nickName)
        {
            lobbyControl.ChatDisplayText += string.Format("{0} has joined the lobby.\r\n", nickName);
        }

        private void lobbyControl_JoinGame(Guid serverId)
        {
            Utility.Global.MatchMakingClient.JoinGame(Utility.Global.ServerKey, serverId);
        }

        private void lobbyControl_StartNewGame(object sender, EventArgs e)
        {
            Utility.Global.MatchMakingClient.StartNewGame(Utility.Global.ServerKey);
        }

        void MatchMakingCallbackHandler_JoinedGame(Core.GameStateInformation.ServerInstanceInformation serverInstanceInformation)
        {
            Utility.Global.InitializeGameplayClient(serverInstanceInformation.GameplayUri);
            gameContainer1.BringToFront();
            Utility.Global.GameplayCallbackHandler.GameStateUpdated += new ServerCommunication.GameplayCallbackHandler.GameStateUpdatedEventHandler(GameplayCallbackHandler_GameStateUpdated);
            Utility.Global.GamePlayClient.Join(Utility.Global.ServerKey);
        }

        void GameplayCallbackHandler_GameStateUpdated(Core.GameStateInformation.GameStateUpdate gameStateUpdate)
        {
            gameContainer1.UpdateGameState(gameStateUpdate);
        }
    }
}
