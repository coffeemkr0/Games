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
    public partial class NavigationControl : UserControl
    {
        #region Events
        public delegate void NavigateEventHandler(Core.GameStateInformation.Directions[] movements);
        public event NavigateEventHandler Navigate;
        protected virtual void OnNavigate(List<Core.GameStateInformation.Directions> movements)
        {
            NavigateEventHandler temp = Navigate;
            if (temp != null)
            {
                temp(movements.ToArray());
            }
        }
        #endregion

        public NavigationControl()
        {
            InitializeComponent();
        }

        private void btnNW_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.NorthWest });
        }

        private void btnN_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.North });
        }

        private void btnNE_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.NorthEast });
        }

        private void btnE_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.East });
        }

        private void btnSE_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.SouthEast });
        }

        private void btnS_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.South });
        }

        private void btnSW_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.SouthWest });
        }

        private void btnW_Click(object sender, EventArgs e)
        {
            OnNavigate(new List<Core.GameStateInformation.Directions>() { Core.GameStateInformation.Directions.West });
        }
    }
}
