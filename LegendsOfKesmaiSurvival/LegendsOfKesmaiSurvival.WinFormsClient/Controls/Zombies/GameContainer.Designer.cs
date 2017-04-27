namespace LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies
{
    partial class GameContainer
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.playerControlPanel1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.PlayerControlPanel();
            this.characterStats1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.CharacterStats();
            this.navigationControl1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.NavigationControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chat1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.Chat();
            this.spellBar1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.SpellBar();
            this.playerView1 = new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.PlayerView();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.playerControlPanel1);
            this.panel1.Controls.Add(this.characterStats1);
            this.panel1.Controls.Add(this.navigationControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(403, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(304, 438);
            this.panel1.TabIndex = 5;
            // 
            // playerControlPanel1
            // 
            this.playerControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerControlPanel1.Location = new System.Drawing.Point(0, 90);
            this.playerControlPanel1.Name = "playerControlPanel1";
            this.playerControlPanel1.Size = new System.Drawing.Size(302, 223);
            this.playerControlPanel1.TabIndex = 3;
            // 
            // characterStats1
            // 
            this.characterStats1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.characterStats1.Location = new System.Drawing.Point(0, 313);
            this.characterStats1.Name = "characterStats1";
            this.characterStats1.Size = new System.Drawing.Size(302, 123);
            this.characterStats1.TabIndex = 4;
            // 
            // navigationControl1
            // 
            this.navigationControl1.Dock = System.Windows.Forms.DockStyle.Top;
            this.navigationControl1.Location = new System.Drawing.Point(0, 0);
            this.navigationControl1.Name = "navigationControl1";
            this.navigationControl1.Size = new System.Drawing.Size(302, 90);
            this.navigationControl1.TabIndex = 5;
            this.navigationControl1.Navigate += new LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies.NavigationControl.NavigateEventHandler(this.navigationControl1_Navigate);
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.chat1);
            this.panel2.Controls.Add(this.spellBar1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 438);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(707, 100);
            this.panel2.TabIndex = 6;
            // 
            // chat1
            // 
            this.chat1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chat1.Location = new System.Drawing.Point(0, 65);
            this.chat1.Name = "chat1";
            this.chat1.Size = new System.Drawing.Size(705, 33);
            this.chat1.TabIndex = 2;
            // 
            // spellBar1
            // 
            this.spellBar1.Dock = System.Windows.Forms.DockStyle.Top;
            this.spellBar1.Location = new System.Drawing.Point(0, 0);
            this.spellBar1.Name = "spellBar1";
            this.spellBar1.Size = new System.Drawing.Size(705, 65);
            this.spellBar1.TabIndex = 1;
            // 
            // playerView1
            // 
            this.playerView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.playerView1.Location = new System.Drawing.Point(0, 0);
            this.playerView1.Name = "playerView1";
            this.playerView1.Size = new System.Drawing.Size(403, 438);
            this.playerView1.TabIndex = 0;
            // 
            // GameContainer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.playerView1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "GameContainer";
            this.Size = new System.Drawing.Size(707, 538);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PlayerView playerView1;
        private SpellBar spellBar1;
        private Chat chat1;
        private PlayerControlPanel playerControlPanel1;
        private CharacterStats characterStats1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private NavigationControl navigationControl1;
    }
}
