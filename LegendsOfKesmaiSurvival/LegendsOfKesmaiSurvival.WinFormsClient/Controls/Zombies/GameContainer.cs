using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies
{
    public partial class GameContainer : UserControl
    {
        public GameContainer()
        {
            InitializeComponent();
        }

        public void UpdateGameState(Core.GameStateInformation.GameStateUpdate gameStateUpdate)
        {
            playerView1.GameStateUpdate = gameStateUpdate;
        }

        private void navigationControl1_Navigate(Core.GameStateInformation.Directions[] movements)
        {
            Utility.Global.GamePlayClient.Move(Utility.Global.ServerKey, movements);
        }
    }
}
