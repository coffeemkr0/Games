using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace LegendsOfKesmaiSurvival.WinFormsClient
{
    static class Program
    {
        public const string CONFIG_FILE = "ClientConfiguration.xml";

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (!Authenticate())
            {
                Application.Exit();
                return;
            }

            Application.Run(new MainForm());
        }

        private static bool Authenticate()
        {
            LoadConfig();

            Dialogs.LoginDialog dlg = new Dialogs.LoginDialog();
            dlg.ShowDialog();
            if (dlg.DialogResult != DialogResult.OK)
            {
                return false;
            }

            return true;
        }

        private static void LoadConfig()
        {
            if (System.IO.File.Exists(CONFIG_FILE))
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(CONFIG_FILE);
                Utility.Global.Config = Utility.Config.LoadFromXml(sr.ReadToEnd());
                sr.Close();
            }
            else Utility.Global.Config = new Utility.Config();
        }
    }
}
