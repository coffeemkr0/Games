namespace LegendsOfKesmaiSurvival.WinFormsClient
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lobbyControl = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby.LobbyControl();
            this.gameContainer1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.GameContainer();
            this.SuspendLayout();
            // 
            // lobbyControl
            // 
            this.lobbyControl.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lobbyControl.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lobbyControl.ChatDisplayText = "";
            this.lobbyControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lobbyControl.Location = new System.Drawing.Point(0, 0);
            this.lobbyControl.Name = "lobbyControl";
            this.lobbyControl.Size = new System.Drawing.Size(629, 528);
            this.lobbyControl.TabIndex = 1;
            this.lobbyControl.StartNewGame += new System.EventHandler(this.lobbyControl_StartNewGame);
            this.lobbyControl.JoinGame += new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby.LobbyControl.JoinGameEventHandler(this.lobbyControl_JoinGame);
            // 
            // gameContainer1
            // 
            this.gameContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gameContainer1.Location = new System.Drawing.Point(0, 0);
            this.gameContainer1.Name = "gameContainer1";
            this.gameContainer1.Size = new System.Drawing.Size(629, 528);
            this.gameContainer1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 528);
            this.Controls.Add(this.lobbyControl);
            this.Controls.Add(this.gameContainer1);
            this.DoubleBuffered = true;
            this.Name = "MainForm";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Controls.Lobby.LobbyControl lobbyControl;
        private Controls.Zombies.GameContainer gameContainer1;
    }
}

