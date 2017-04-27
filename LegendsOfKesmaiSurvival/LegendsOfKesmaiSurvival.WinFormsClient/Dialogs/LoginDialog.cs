using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace LegendsOfKesmaiSurvival.WinFormsClient.Dialogs
{
    public partial class LoginDialog : Form
    {
        public LoginDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                if (!Utility.Global.AuthenticationClientInitialized)
                {
                    Utility.Global.InitializeAuthenticationClient();
                }

                Guid serverKey = Guid.Empty;
                if (!Utility.Global.AuthenicationClient.Login(out serverKey, txtUserName.Text, txtPassword.Text))
                {
                    MessageBox.Show("Invalid username or password.");
                    return;
                }

                Utility.Global.ServerKey = serverKey;

                Utility.Global.InitializeClients();
            }
            catch (Exception ex)
            {
                MessageBox.Show("There was a problem connecting to the server." + "\r\n\r\n" + ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            Dialogs.OptionsDialog dlg = new OptionsDialog();
            dlg.Config = Utility.Global.Config.Clone();
            dlg.ShowDialog(this);

            if (dlg.DialogResult == System.Windows.Forms.DialogResult.OK)
            {
                Utility.Global.Config = dlg.Config;
                SaveConfig();
            }
        }

        private void SaveConfig()
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(Program.CONFIG_FILE, false);
            sw.Write(Utility.Global.Config.ToXml());
            sw.Close();
        }

        private void lnlCreateNewUserAccount_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            NewAccountDialog dlg = new NewAccountDialog();
            dlg.ShowDialog(this);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            MainForm dlg = new MainForm(true);
            dlg.ShowDialog(this);
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
        }
    }
}
