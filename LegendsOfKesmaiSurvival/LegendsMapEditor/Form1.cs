using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LegendsOfKesmaiSurvival.Core;
using LegendsOfKesmaiSurvival.Services.Business.Maps;

namespace MapEditor
{
    public partial class Form1 : Form
    {
        private string _workingFileName = "";

        public Form1()
        {
            InitializeComponent();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            mapViewer1.Map = Map.NewMap();
            lblFile.Text = "Untitled.map";

            btnSave.Enabled = btnSaveAs.Enabled = btnMapProperties.Enabled = true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workingFileName == "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "Map files (*.map)|*.map|All Files (*.*)|*.*";
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _workingFileName = lblFile.Text = dlg.FileName;
                }
                else return;
            }
            mapViewer1.Map.SaveToFile(_workingFileName);

            mapViewer1.RedrawMap();
        }

        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Map files (*.map)|*.map|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                _workingFileName = lblFile.Text = dlg.FileName;
                mapViewer1.Map.SaveToFile(_workingFileName);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = "Map files (*.map)|*.map|All Files (*.*)|*.*";
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
                {
                    _workingFileName = lblFile.Text = dlg.FileName;
                    mapViewer1.Map = LegendsOfKesmaiSurvival.Services.Business.Maps.Map.LoadFromFile(_workingFileName);
                }
                btnSave.Enabled = btnSaveAs.Enabled = btnMapProperties.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnExtractLegacyTerrainPalette_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select the terrain image file.";
            fileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg|All Files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            using (Bitmap terrainPaletteImage = (Bitmap)Bitmap.FromFile(fileDialog.FileName))
            {
                int imageIndex = 1;
                for (int y = 0; y <= 4500; y += 100)
                {
                    for (int x = 0; x <= 1200; x += 100)
                    {
                        Bitmap terrainImage = new Bitmap(100, 100);
                        using (Graphics g = Graphics.FromImage(terrainImage))
                        {
                            Rectangle sourceRectangle = new Rectangle(x, y, 100, 100);
                            Rectangle destinationRectangle = new Rectangle(0, 0, 100, 100);
                            g.DrawImage(terrainPaletteImage, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                        }
                        terrainImage.Save(System.IO.Path.Combine(folderDialog.SelectedPath, imageIndex.ToString() + ".png"));
                        terrainImage.Dispose();
                        terrainImage = null;

                        imageIndex += 1;
                    }
                }
            }
           
            MessageBox.Show("Extraction complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnMapProperties_Click(object sender, EventArgs e)
        {
            Dialogs.MapPropertiesDialog dlg = new Dialogs.MapPropertiesDialog();
            dlg.Map = mapViewer1.Map;
            dlg.ShowDialog(this);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            mapViewer1.ZoomLevel = trackBar1.Value;
        }

        private void mapViewer1_SelectedTileChanged(object sender, EventArgs e)
        {
            propertyGrid1.SelectedObject = mapViewer1.SelectedTile;
            propertyGrid1.Enabled = mapViewer1.SelectedTile != null;
        }

        private void btnExtractArtFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Title = "Select the art file.";
            fileDialog.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg|All Files (*.*)|*.*";
            fileDialog.RestoreDirectory = true;
            if (fileDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            FolderBrowserDialog folderDialog = new FolderBrowserDialog();
            if (folderDialog.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            using (Bitmap artImage = (Bitmap)Bitmap.FromFile(fileDialog.FileName))
            {
                int width = 60;
                int height = 60;

                int imageIndex = 1;
                for (int y = 0; y <= 1035; y += height)
                {
                    for (int x = 0; x <= 990; x += width)
                    {
                        Bitmap extractedImage = new Bitmap(width, height);
                        using (Graphics g = Graphics.FromImage(extractedImage))
                        {
                            Rectangle sourceRectangle = new Rectangle(x, y, width, height);
                            Rectangle destinationRectangle = new Rectangle(0, 0, width, height);
                            g.DrawImage(artImage, destinationRectangle, sourceRectangle, GraphicsUnit.Pixel);
                        }

                        Color backColor = extractedImage.GetPixel(1, 1);
                        extractedImage.MakeTransparent(backColor);
                        extractedImage.Save(System.IO.Path.Combine(folderDialog.SelectedPath, imageIndex.ToString() + ".png"), System.Drawing.Imaging.ImageFormat.Png);
                        extractedImage.Dispose();
                        extractedImage = null;

                        imageIndex += 1;
                    }
                }
            }

            MessageBox.Show("Extraction complete.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
