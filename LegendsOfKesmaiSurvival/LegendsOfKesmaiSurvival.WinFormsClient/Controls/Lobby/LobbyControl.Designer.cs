namespace LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby
{
    partial class LobbyControl
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LobbyControl));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.lobbyChatDisplay1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby.LobbyChatDisplay();
            this.lobbyControlPanel1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby.LobbyControlPanel();
            this.txtChat = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.splitContainer1.Panel2.Controls.Add(this.txtChat);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(626, 427);
            this.splitContainer1.SplitterDistance = 401;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 99;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.lobbyChatDisplay1);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.lobbyControlPanel1);
            this.splitContainer2.Size = new System.Drawing.Size(626, 401);
            this.splitContainer2.SplitterDistance = 418;
            this.splitContainer2.SplitterWidth = 1;
            this.splitContainer2.TabIndex = 99;
            // 
            // lobbyChatDisplay1
            // 
            this.lobbyChatDisplay1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("lobbyChatDisplay1.BackgroundImage")));
            this.lobbyChatDisplay1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lobbyChatDisplay1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lobbyChatDisplay1.Location = new System.Drawing.Point(0, 0);
            this.lobbyChatDisplay1.Name = "lobbyChatDisplay1";
            this.lobbyChatDisplay1.Size = new System.Drawing.Size(418, 401);
            this.lobbyChatDisplay1.TabIndex = 99;
            this.lobbyChatDisplay1.TabStop = false;
            // 
            // lobbyControlPanel1
            // 
            this.lobbyControlPanel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.lobbyControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lobbyControlPanel1.Location = new System.Drawing.Point(0, 0);
            this.lobbyControlPanel1.Name = "lobbyControlPanel1";
            this.lobbyControlPanel1.Size = new System.Drawing.Size(207, 401);
            this.lobbyControlPanel1.TabIndex = 99;
            this.lobbyControlPanel1.TabStop = false;
            this.lobbyControlPanel1.StartNewGame += new System.EventHandler(this.lobbyControlPanel1_StartNewGame);
            this.lobbyControlPanel1.JoinGame += new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Lobby.LobbyControlPanel.JoinGameEventHandler(this.lobbyControlPanel1_JoinGame);
            // 
            // txtChat
            // 
            this.txtChat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChat.Location = new System.Drawing.Point(0, 0);
            this.txtChat.Name = "txtChat";
            this.txtChat.Size = new System.Drawing.Size(626, 20);
            this.txtChat.TabIndex = 1;
            this.txtChat.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtChat_KeyUp);
            // 
            // LobbyControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Name = "LobbyControl";
            this.Size = new System.Drawing.Size(626, 427);
            this.Load += new System.EventHandler(this.LobbyControl_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private LobbyChatDisplay lobbyChatDisplay1;
        private LobbyControlPanel lobbyControlPanel1;
        private System.Windows.Forms.TextBox txtChat;
    }
}
