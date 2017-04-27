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
    public partial class LobbyChatDisplay : UserControl
    {
        public LobbyChatDisplay()
        {
            InitializeComponent();
        }

        private void txtChat_TextChanged(object sender, EventArgs e)
        {
            //We need to scroll to the bottom when new text is received.
            txtChat.SelectionStart = txtChat.Text.Length; //Set the current caret position to the end
            txtChat.ScrollToCaret(); //Scroll it automatically
        }
    }
}
