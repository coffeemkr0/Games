namespace MapEditor.Dialogs
{
    partial class MapPropertiesDialog
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label nameLabel;
            System.Windows.Forms.Label nameLabel1;
            System.Windows.Forms.Label heightLabel;
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label3;
            System.Windows.Forms.Label label4;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label5;
            System.Windows.Forms.Label label6;
            System.Windows.Forms.Label label7;
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MapPropertiesDialog));
            this.txtName = new System.Windows.Forms.TextBox();
            this.mapBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.numTileHeight = new System.Windows.Forms.NumericUpDown();
            this.numTileWidth = new System.Windows.Forms.NumericUpDown();
            this.numMapWidth = new System.Windows.Forms.NumericUpDown();
            this.numMapHeight = new System.Windows.Forms.NumericUpDown();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnEditTerainPaletteFile = new System.Windows.Forms.Button();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnEditWallPaletteFile = new System.Windows.Forms.Button();
            this.btnEditMapObjectPaletteFile = new System.Windows.Forms.Button();
            nameLabel = new System.Windows.Forms.Label();
            nameLabel1 = new System.Windows.Forms.Label();
            heightLabel = new System.Windows.Forms.Label();
            label1 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            label7 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.mapBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapHeight)).BeginInit();
            this.SuspendLayout();
            // 
            // nameLabel
            // 
            nameLabel.AutoSize = true;
            nameLabel.Location = new System.Drawing.Point(58, 15);
            nameLabel.Name = "nameLabel";
            nameLabel.Size = new System.Drawing.Size(35, 13);
            nameLabel.TabIndex = 2;
            nameLabel.Text = "Name";
            // 
            // nameLabel1
            // 
            nameLabel1.AutoSize = true;
            nameLabel1.Location = new System.Drawing.Point(17, 41);
            nameLabel1.Name = "nameLabel1";
            nameLabel1.Size = new System.Drawing.Size(76, 13);
            nameLabel1.TabIndex = 5;
            nameLabel1.Text = "Terrain Palette";
            // 
            // heightLabel
            // 
            heightLabel.AutoSize = true;
            heightLabel.Location = new System.Drawing.Point(278, 155);
            heightLabel.Name = "heightLabel";
            heightLabel.Size = new System.Drawing.Size(60, 13);
            heightLabel.TabIndex = 6;
            heightLabel.Text = "Tile Height";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(278, 129);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(57, 13);
            label1.TabIndex = 10;
            label1.Text = "Tile Width";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(91, 129);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(59, 13);
            label3.TabIndex = 12;
            label3.Text = "Map Width";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new System.Drawing.Point(219, 129);
            label4.Name = "label4";
            label4.Size = new System.Drawing.Size(35, 13);
            label4.TabIndex = 16;
            label4.Text = "tiles";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(219, 155);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(35, 13);
            label2.TabIndex = 19;
            label2.Text = "tiles";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new System.Drawing.Point(88, 155);
            label5.Name = "label5";
            label5.Size = new System.Drawing.Size(62, 13);
            label5.TabIndex = 17;
            label5.Text = "Map Height";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new System.Drawing.Point(29, 67);
            label6.Name = "label6";
            label6.Size = new System.Drawing.Size(64, 13);
            label6.TabIndex = 103;
            label6.Text = "Wall Palette";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new System.Drawing.Point(19, 93);
            label7.Name = "label7";
            label7.Size = new System.Drawing.Size(74, 13);
            label7.TabIndex = 107;
            label7.Text = "Object Palette";
            // 
            // txtName
            // 
            this.txtName.DataBindings.Add(new System.Windows.Forms.Binding("Text", this.mapBindingSource, "Name", true));
            this.txtName.Location = new System.Drawing.Point(99, 12);
            this.txtName.Name = "txtName";
            this.txtName.Size = new System.Drawing.Size(307, 20);
            this.txtName.TabIndex = 1;
            // 
            // mapBindingSource
            // 
            this.mapBindingSource.DataSource = typeof(LegendsOfKesmaiSurvival.Services.Business.Maps.Map);
            // 
            // numTileHeight
            // 
            this.numTileHeight.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.mapBindingSource, "TileHeight", true));
            this.numTileHeight.Location = new System.Drawing.Point(344, 153);
            this.numTileHeight.Name = "numTileHeight";
            this.numTileHeight.Size = new System.Drawing.Size(57, 20);
            this.numTileHeight.TabIndex = 6;
            // 
            // numTileWidth
            // 
            this.numTileWidth.DataBindings.Add(new System.Windows.Forms.Binding("Value", this.mapBindingSource, "TileWidth", true));
            this.numTileWidth.Location = new System.Drawing.Point(344, 127);
            this.numTileWidth.Name = "numTileWidth";
            this.numTileWidth.Size = new System.Drawing.Size(57, 20);
            this.numTileWidth.TabIndex = 8;
            // 
            // numMapWidth
            // 
            this.numMapWidth.Location = new System.Drawing.Point(156, 127);
            this.numMapWidth.Name = "numMapWidth";
            this.numMapWidth.Size = new System.Drawing.Size(57, 20);
            this.numMapWidth.TabIndex = 5;
            // 
            // numMapHeight
            // 
            this.numMapHeight.Location = new System.Drawing.Point(156, 153);
            this.numMapHeight.Name = "numMapHeight";
            this.numMapHeight.Size = new System.Drawing.Size(57, 20);
            this.numMapHeight.TabIndex = 7;
            // 
            // btnOk
            // 
            this.btnOk.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOk.Location = new System.Drawing.Point(328, 194);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 98;
            this.btnOk.Text = "Ok";
            this.btnOk.UseVisualStyleBackColor = true;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(409, 194);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 99;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnEditTerainPaletteFile
            // 
            this.btnEditTerainPaletteFile.Image = ((System.Drawing.Image)(resources.GetObject("btnEditTerainPaletteFile.Image")));
            this.btnEditTerainPaletteFile.Location = new System.Drawing.Point(99, 36);
            this.btnEditTerainPaletteFile.Name = "btnEditTerainPaletteFile";
            this.btnEditTerainPaletteFile.Size = new System.Drawing.Size(32, 23);
            this.btnEditTerainPaletteFile.TabIndex = 4;
            this.btnEditTerainPaletteFile.Text = "...";
            this.toolTip.SetToolTip(this.btnEditTerainPaletteFile, "Open in Terrain Palette Editor");
            this.btnEditTerainPaletteFile.UseVisualStyleBackColor = true;
            this.btnEditTerainPaletteFile.Click += new System.EventHandler(this.btnEditTerainPaletteFile_Click);
            // 
            // btnEditWallPaletteFile
            // 
            this.btnEditWallPaletteFile.Image = ((System.Drawing.Image)(resources.GetObject("btnEditWallPaletteFile.Image")));
            this.btnEditWallPaletteFile.Location = new System.Drawing.Point(99, 62);
            this.btnEditWallPaletteFile.Name = "btnEditWallPaletteFile";
            this.btnEditWallPaletteFile.Size = new System.Drawing.Size(32, 23);
            this.btnEditWallPaletteFile.TabIndex = 102;
            this.btnEditWallPaletteFile.Text = "...";
            this.toolTip.SetToolTip(this.btnEditWallPaletteFile, "Open in Terrain Palette Editor");
            this.btnEditWallPaletteFile.UseVisualStyleBackColor = true;
            // 
            // btnEditMapObjectPaletteFile
            // 
            this.btnEditMapObjectPaletteFile.Image = ((System.Drawing.Image)(resources.GetObject("btnEditMapObjectPaletteFile.Image")));
            this.btnEditMapObjectPaletteFile.Location = new System.Drawing.Point(99, 88);
            this.btnEditMapObjectPaletteFile.Name = "btnEditMapObjectPaletteFile";
            this.btnEditMapObjectPaletteFile.Size = new System.Drawing.Size(32, 23);
            this.btnEditMapObjectPaletteFile.TabIndex = 106;
            this.btnEditMapObjectPaletteFile.Text = "...";
            this.toolTip.SetToolTip(this.btnEditMapObjectPaletteFile, "Open in Terrain Palette Editor");
            this.btnEditMapObjectPaletteFile.UseVisualStyleBackColor = true;
            // 
            // MapPropertiesDialog
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(496, 229);
            this.Controls.Add(this.btnEditMapObjectPaletteFile);
            this.Controls.Add(label7);
            this.Controls.Add(this.btnEditWallPaletteFile);
            this.Controls.Add(label6);
            this.Controls.Add(this.btnEditTerainPaletteFile);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(label2);
            this.Controls.Add(this.numMapHeight);
            this.Controls.Add(label5);
            this.Controls.Add(label4);
            this.Controls.Add(this.numMapWidth);
            this.Controls.Add(label3);
            this.Controls.Add(this.numTileWidth);
            this.Controls.Add(label1);
            this.Controls.Add(this.numTileHeight);
            this.Controls.Add(heightLabel);
            this.Controls.Add(nameLabel1);
            this.Controls.Add(nameLabel);
            this.Controls.Add(this.txtName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MapPropertiesDialog";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Map Properties";
            ((System.ComponentModel.ISupportInitialize)(this.mapBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTileWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numMapHeight)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.BindingSource mapBindingSource;
        private System.Windows.Forms.TextBox txtName;
        private System.Windows.Forms.NumericUpDown numTileHeight;
        private System.Windows.Forms.NumericUpDown numTileWidth;
        private System.Windows.Forms.NumericUpDown numMapWidth;
        private System.Windows.Forms.NumericUpDown numMapHeight;
        private System.Windows.Forms.Button btnOk;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnEditTerainPaletteFile;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.Button btnEditWallPaletteFile;
        private System.Windows.Forms.Button btnEditMapObjectPaletteFile;
    }
}