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
    public partial class NewAccountDialog : Form
    {
        public NewAccountDialog()
        {
            InitializeComponent();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (!ValidateControls()) return;

            if (!Utility.Global.AuthenticationClientInitialized)
            {
                Utility.Global.InitializeAuthenticationClient();
            }

            string exception = "";
            if (!Utility.Global.AuthenicationClient.CreateNewUser(out exception, txtUserName.Text, txtNickName.Text, txtPassword.Text))
            {
                MessageBox.Show(this, exception, "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            MessageBox.Show(this, "User account created.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private bool ValidateControls()
        {
            if (txtUserName.Text.Trim() == "")
            {
                MessageBox.Show(this, "Please enter a user name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserName.Focus();
                return false;
            }

            if (txtNickName.Text.Trim() == "")
            {
                MessageBox.Show(this, "Please enter a nick name.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNickName.Focus();
                return false;
            }

            if (txtPassword.Text.Trim() == "")
            {
                MessageBox.Show(this, "Please enter a password.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return false;
            }

            //passwords must match
            if (txtConfirmPassword.Text != txtPassword.Text)
            {
                MessageBox.Show(this, "Passwords do not match.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return false;
            }

            return true;
        }
    }
}
