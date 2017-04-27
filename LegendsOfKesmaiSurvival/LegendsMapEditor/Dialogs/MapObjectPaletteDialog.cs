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
    public partial class MapObjectPaletteDialog : Form
    {
        #region Declarations
        private string _workingFileName = "";
        private LegendsOfKesmaiSurvival.Services.Business.Maps.MapObjectPalette _mapObjectPalette;
        #endregion

        #region Properties
        /// <summary>
        /// Gets the last file that was selected whent he dialog closes.  Returns empty string if a file was never opened or saved.
        /// </summary>
        public string SelectedFile
        {
            get
            {
                if (_workingFileName == "Untitled.mopal") return "";
                return _workingFileName;
            }
        }
        #endregion

        #region Initialization
        public MapObjectPaletteDialog()
        {
            InitializeComponent();
        }

        public MapObjectPaletteDialog(string _mapObjectPaletteFile)
        {
            InitializeComponent();

            OpenMapObjectPaletteFile(_mapObjectPaletteFile);
        }
        #endregion

        #region Private Methods
        void MapObject_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Image")
            {
                LegendsOfKesmaiSurvival.Services.Business.Maps.MapObject MapObject = (LegendsOfKesmaiSurvival.Services.Business.Maps.MapObject)sender;
                imageList1.Images.RemoveByKey(MapObject.ID.ToString());
                imageList1.Images.Add(MapObject.ID.ToString(), MapObject.Image);
            }
        }

        private void bindableListView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (bindableListView1.SelectedItems.Count > 0)
            {
                propertyGrid1.SelectedObject = _mapObjectPalette.MapObjects[bindableListView1.SelectedItems[0].Index];
                btnRemove.Enabled = propertyGrid1.Enabled = true;
            }
            else
            {
                propertyGrid1.SelectedObject = null;
                btnRemove.Enabled = propertyGrid1.Enabled = false;
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (_workingFileName == "")
            {
                SaveFileDialog dlg = new SaveFileDialog();
                dlg.Filter = "MapObject Palettes (*.mopal)|*.mopal|All Files (*.*)|*.*";
                dlg.RestoreDirectory = true;
                if (dlg.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
                _workingFileName = lblFile.Text = dlg.FileName;
            }
            System.IO.StreamWriter sw = new System.IO.StreamWriter(_workingFileName, false);
            sw.Write(_mapObjectPalette.ToXml());
            sw.Close();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "MapObject Palettes (*.mopal)|*.mopal|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog(this) != System.Windows.Forms.DialogResult.OK) return;
            OpenMapObjectPaletteFile(dlg.FileName);
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            _mapObjectPalette = new LegendsOfKesmaiSurvival.Services.Business.Maps.MapObjectPalette();
            lblFile.Text = "Untitled.mopal";
            mapObjectBindingSource.DataSource = _mapObjectPalette.MapObjects;
            UpdateMapObjectImgeList();
            btnSave.Enabled = btnAdd.Enabled = bindableListView1.Enabled = propertyGrid1.Enabled = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "Select the MapObject image file.";
            dlg.Filter = "Image Files (*.bmp;*.png;*.jpg)|*.bmp;*.png;*.jpg|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;
            if (dlg.ShowDialog() != System.Windows.Forms.DialogResult.OK) return;

            LegendsOfKesmaiSurvival.Services.Business.Maps.MapObject MapObject = new LegendsOfKesmaiSurvival.Services.Business.Maps.MapObject();
            MapObject.ID = _mapObjectPalette.MapObjects.Count;
            MapObject.Name = System.IO.Path.GetFileNameWithoutExtension(dlg.FileName);
            MapObject.Image = (Bitmap)Bitmap.FromFile(dlg.FileName);

            MapObject.PropertyChanged+=new PropertyChangedEventHandler(MapObject_PropertyChanged);

            _mapObjectPalette.MapObjects.Add(MapObject);

            imageList1.Images.Add(MapObject.ID.ToString(), MapObject.Image);
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            LegendsOfKesmaiSurvival.Services.Business.Maps.MapObject MapObject = _mapObjectPalette.MapObjects[bindableListView1.SelectedItems[0].Index];
            imageList1.Images.RemoveByKey(MapObject.ID.ToString());
            _mapObjectPalette.MapObjects.Remove(MapObject);
            mapObjectBindingSource.ResetBindings(false);
        }

        private void UpdateMapObjectImgeList()
        {
            imageList1.Images.Clear();
            foreach(LegendsOfKesmaiSurvival.Services.Business.Maps.MapObject MapObject in _mapObjectPalette.MapObjects)
            {
                imageList1.Images.Add(MapObject.ID.ToString(), MapObject.Image);
            }
        }

        private void OpenMapObjectPaletteFile(string mapObjectPaletteFile)
        {
            if (!System.IO.File.Exists(mapObjectPaletteFile))
            {
                MessageBox.Show("File not found.\r\n\r\n" + mapObjectPaletteFile, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                _workingFileName = lblFile.Text = mapObjectPaletteFile;
                System.IO.StreamReader sr = new System.IO.StreamReader(_workingFileName);
                _mapObjectPalette = LegendsOfKesmaiSurvival.Services.Business.Maps.MapObjectPalette.LoadFromXml(sr.ReadToEnd());
                sr.Close();

                foreach (LegendsOfKesmaiSurvival.Services.Business.Maps.MapObject MapObject in _mapObjectPalette.MapObjects)
                {
                    MapObject.PropertyChanged += new PropertyChangedEventHandler(MapObject_PropertyChanged);
                }

                mapObjectBindingSource.DataSource = _mapObjectPalette.MapObjects;
                UpdateMapObjectImgeList();
                btnSave.Enabled = btnAdd.Enabled = bindableListView1.Enabled = propertyGrid1.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Could not open the MapObject palette file at " + mapObjectPaletteFile + "\r\n\r\n" + ex.ToString(), "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);               
            }
        }
        #endregion
    }
}
