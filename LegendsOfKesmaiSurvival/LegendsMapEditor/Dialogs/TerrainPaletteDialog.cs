using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.Dialogs
{
    public partial class TerrainPaletteDialog : Form
    {
        #region Properties
        private LegendsOfKesmaiSurvival.Services.Business.Maps.TerrainPalette _terrainPalette;
        public LegendsOfKesmaiSurvival.Services.Business.Maps.TerrainPalette TerrainPalette
        {
            get { return _terrainPalette; }
            set
            {
                _terrainPalette = value.Clone();
                foreach (LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain terrain in _terrainPalette)
                {
                    terrain.PropertyChanged += new PropertyChangedEventHandler(terrain_PropertyChanged);
                }
                terrainBindingSource.DataSource = _terrainPalette;
                UpdateTerrainImgeList();
            }
        }
        #endregion

        #region Initialization
        public TerrainPaletteDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Private Methods
        void terrain_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "BackgroundImageName")
            {
                LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain terrain = (LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain)sender;
                imageList1.Images.RemoveByKey(terrain.ID.ToString());
                imageList1.Images.Add(terrain.ID.ToString(), terrain.BackgroundImage);
            }
        }

        private void bindableListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bindableListView1.SelectedItems.Count > 0)
            {
                terrainPropertiesControl.Terrain = _terrainPalette[bindableListView1.SelectedItems[0].Index];
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain terrain = new LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain();
            terrain.ID = _terrainPalette.Count;
            terrain.BackgroundImageName = "Terrain_Gravel_Road";

            terrain.PropertyChanged+=new PropertyChangedEventHandler(terrain_PropertyChanged);

            _terrainPalette.Add(terrain);

            imageList1.Images.Add(terrain.ID.ToString(), terrain.BackgroundImage);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain terrain = _terrainPalette[bindableListView1.SelectedItems[0].Index];
            imageList1.Images.RemoveByKey(terrain.ID.ToString());
            _terrainPalette.Remove(terrain);
            terrainBindingSource.ResetBindings(false);
        }

        private void UpdateTerrainImgeList()
        {
            imageList1.Images.Clear();
            foreach(LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain terrain in _terrainPalette)
            {
                imageList1.Images.Add(terrain.ID.ToString(), terrain.BackgroundImage);
            }
        }
        #endregion

        private void btnExport_Click(object sender, EventArgs e)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.Filter = "Terrain Palettes (*.tpal)|*.tpal|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
                
            System.IO.StreamWriter sw = new System.IO.StreamWriter(dlg.FileName, false);
            sw.Write(_terrainPalette.ToXml());
            sw.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Terrain Palettes (*.tpal)|*.tpal|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
           
            try
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(dlg.FileName);
                _terrainPalette = LegendsOfKesmaiSurvival.Services.Business.Maps.TerrainPalette.LoadFromXml(sr.ReadToEnd());
                sr.Close();

                foreach (LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain terrain in _terrainPalette)
                {
                    terrain.PropertyChanged += new PropertyChangedEventHandler(terrain_PropertyChanged);
                }

                terrainBindingSource.DataSource = _terrainPalette;
                UpdateTerrainImgeList();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open the terrain palette file at " + dlg.FileName + "\r\n\r\n" + ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
