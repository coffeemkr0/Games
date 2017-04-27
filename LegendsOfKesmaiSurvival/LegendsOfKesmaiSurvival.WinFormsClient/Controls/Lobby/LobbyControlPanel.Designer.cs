namespace LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby
{
    partial class LobbyControlPanel
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnStartNewGame = new System.Windows.Forms.Button();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lstGames = new System.Windows.Forms.ListBox();
            this.lblGames = new System.Windows.Forms.Label();
            this.btnJoinGame = new System.Windows.Forms.Button();
            this.btnRefreshGames = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnStartNewGame
            // 
            this.btnStartNewGame.Location = new System.Drawing.Point(3, 40);
            this.btnStartNewGame.Name = "btnStartNewGame";
            this.btnStartNewGame.Size = new System.Drawing.Size(105, 23);
            this.btnStartNewGame.TabIndex = 0;
            this.btnStartNewGame.Text = "Start New Game";
            this.btnStartNewGame.UseVisualStyleBackColor = true;
            this.btnStartNewGame.Click += new System.EventHandler(this.btnStartNewGame_Click);
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(265, 34);
            this.pnlTitleBar.TabIndex = 1;
            // 
            // lstGames
            // 
            this.lstGames.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstGames.FormattingEnabled = true;
            this.lstGames.Location = new System.Drawing.Point(3, 118);
            this.lstGames.Name = "lstGames";
            this.lstGames.Size = new System.Drawing.Size(259, 95);
            this.lstGames.TabIndex = 2;
            // 
            // lblGames
            // 
            this.lblGames.AutoSize = true;
            this.lblGames.Location = new System.Drawing.Point(3, 102);
            this.lblGames.Name = "lblGames";
            this.lblGames.Size = new System.Drawing.Size(103, 13);
            this.lblGames.TabIndex = 3;
            this.lblGames.Text = "Games being played";
            // 
            // btnJoinGame
            // 
            this.btnJoinGame.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnJoinGame.Location = new System.Drawing.Point(3, 219);
            this.btnJoinGame.Name = "btnJoinGame";
            this.btnJoinGame.Size = new System.Drawing.Size(105, 23);
            this.btnJoinGame.TabIndex = 4;
            this.btnJoinGame.Text = "Join Game";
            this.btnJoinGame.UseVisualStyleBackColor = true;
            this.btnJoinGame.Click += new System.EventHandler(this.btnJoinGame_Click);
            // 
            // btnRefreshGames
            // 
            this.btnRefreshGames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRefreshGames.Location = new System.Drawing.Point(187, 219);
            this.btnRefreshGames.Name = "btnRefreshGames";
            this.btnRefreshGames.Size = new System.Drawing.Size(75, 23);
            this.btnRefreshGames.TabIndex = 5;
            this.btnRefreshGames.Text = "Refresh";
            this.btnRefreshGames.UseVisualStyleBackColor = true;
            this.btnRefreshGames.Click += new System.EventHandler(this.btnRefreshGames_Click);
            // 
            // LobbyControlPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Controls.Add(this.btnRefreshGames);
            this.Controls.Add(this.btnJoinGame);
            this.Controls.Add(this.lblGames);
            this.Controls.Add(this.lstGames);
            this.Controls.Add(this.pnlTitleBar);
            this.Controls.Add(this.btnStartNewGame);
            this.DoubleBuffered = true;
            this.Name = "LobbyControlPanel";
            this.Size = new System.Drawing.Size(265, 300);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnStartNewGame;
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.ListBox lstGames;
        private System.Windows.Forms.Label lblGames;
        private System.Windows.Forms.Button btnJoinGame;
        private System.Windows.Forms.Button btnRefreshGames;
    }
}
