namespace LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies
{
    partial class NavigationControl
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
            this.btnSE = new System.Windows.Forms.Button();
            this.btnSW = new System.Windows.Forms.Button();
            this.btnNE = new System.Windows.Forms.Button();
            this.btnNW = new System.Windows.Forms.Button();
            this.btnW = new System.Windows.Forms.Button();
            this.btnE = new System.Windows.Forms.Button();
            this.btnS = new System.Windows.Forms.Button();
            this.btnN = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnSE
            // 
            this.btnSE.Location = new System.Drawing.Point(87, 61);
            this.btnSE.Name = "btnSE";
            this.btnSE.Size = new System.Drawing.Size(36, 23);
            this.btnSE.TabIndex = 22;
            this.btnSE.Text = "SE";
            this.btnSE.UseVisualStyleBackColor = true;
            this.btnSE.Click += new System.EventHandler(this.btnSE_Click);
            // 
            // btnSW
            // 
            this.btnSW.Location = new System.Drawing.Point(3, 61);
            this.btnSW.Name = "btnSW";
            this.btnSW.Size = new System.Drawing.Size(36, 23);
            this.btnSW.TabIndex = 21;
            this.btnSW.Text = "SW";
            this.btnSW.UseVisualStyleBackColor = true;
            this.btnSW.Click += new System.EventHandler(this.btnSW_Click);
            // 
            // btnNE
            // 
            this.btnNE.Location = new System.Drawing.Point(87, 3);
            this.btnNE.Name = "btnNE";
            this.btnNE.Size = new System.Drawing.Size(36, 23);
            this.btnNE.TabIndex = 20;
            this.btnNE.Text = "NE";
            this.btnNE.UseVisualStyleBackColor = true;
            this.btnNE.Click += new System.EventHandler(this.btnNE_Click);
            // 
            // btnNW
            // 
            this.btnNW.Location = new System.Drawing.Point(3, 3);
            this.btnNW.Name = "btnNW";
            this.btnNW.Size = new System.Drawing.Size(36, 23);
            this.btnNW.TabIndex = 19;
            this.btnNW.Text = "NW";
            this.btnNW.UseVisualStyleBackColor = true;
            this.btnNW.Click += new System.EventHandler(this.btnNW_Click);
            // 
            // btnW
            // 
            this.btnW.Location = new System.Drawing.Point(3, 32);
            this.btnW.Name = "btnW";
            this.btnW.Size = new System.Drawing.Size(36, 23);
            this.btnW.TabIndex = 18;
            this.btnW.Text = "W";
            this.btnW.UseVisualStyleBackColor = true;
            this.btnW.Click += new System.EventHandler(this.btnW_Click);
            // 
            // btnE
            // 
            this.btnE.Location = new System.Drawing.Point(87, 32);
            this.btnE.Name = "btnE";
            this.btnE.Size = new System.Drawing.Size(36, 23);
            this.btnE.TabIndex = 17;
            this.btnE.Text = "E";
            this.btnE.UseVisualStyleBackColor = true;
            this.btnE.Click += new System.EventHandler(this.btnE_Click);
            // 
            // btnS
            // 
            this.btnS.Location = new System.Drawing.Point(45, 61);
            this.btnS.Name = "btnS";
            this.btnS.Size = new System.Drawing.Size(36, 23);
            this.btnS.TabIndex = 16;
            this.btnS.Text = "S";
            this.btnS.UseVisualStyleBackColor = true;
            this.btnS.Click += new System.EventHandler(this.btnS_Click);
            // 
            // btnN
            // 
            this.btnN.Location = new System.Drawing.Point(45, 3);
            this.btnN.Name = "btnN";
            this.btnN.Size = new System.Drawing.Size(36, 23);
            this.btnN.TabIndex = 15;
            this.btnN.Text = "N";
            this.btnN.UseVisualStyleBackColor = true;
            this.btnN.Click += new System.EventHandler(this.btnN_Click);
            // 
            // NavigationControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnSE);
            this.Controls.Add(this.btnSW);
            this.Controls.Add(this.btnNE);
            this.Controls.Add(this.btnNW);
            this.Controls.Add(this.btnW);
            this.Controls.Add(this.btnE);
            this.Controls.Add(this.btnS);
            this.Controls.Add(this.btnN);
            this.Name = "NavigationControl";
            this.Size = new System.Drawing.Size(135, 93);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSE;
        private System.Windows.Forms.Button btnSW;
        private System.Windows.Forms.Button btnNE;
        private System.Windows.Forms.Button btnNW;
        private System.Windows.Forms.Button btnW;
        private System.Windows.Forms.Button btnE;
        private System.Windows.Forms.Button btnS;
        private System.Windows.Forms.Button btnN;
    }
}
