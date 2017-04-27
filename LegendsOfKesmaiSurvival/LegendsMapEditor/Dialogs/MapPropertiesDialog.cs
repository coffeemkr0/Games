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
    public partial class MapPropertiesDialog : Form
    {
        #region Propertyes
        public LegendsOfKesmaiSurvival.Services.Business.Maps.Map Map
        {
            get { return mapBindingSource.DataSource as LegendsOfKesmaiSurvival.Services.Business.Maps.Map; }
            set
            {
                mapBindingSource.DataSource = value;
                numMapWidth.Value = value.Size.Width;
                numMapHeight.Value = value.Size.Height;
            }
        }
        #endregion

        #region Constructors
        public MapPropertiesDialog()
        {
            InitializeComponent();

            Utility.DataBindingUtilities.SuspendTwoWayBinding(this.BindingContext[mapBindingSource]);
        }
        #endregion

        #region Private Methods
        private void btnOk_Click(object sender, EventArgs e)
        {
            mapBindingSource.RaiseListChangedEvents = false;
            Utility.DataBindingUtilities.UpdateDataBoundObject(this.BindingContext[mapBindingSource]);
            mapBindingSource.RaiseListChangedEvents = true;

            this.Map.Size = new Size((int)numMapWidth.Value, (int)numMapHeight.Value);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
        #endregion

        private void btnEditTerainPaletteFile_Click(object sender, EventArgs e)
        {
            Dialogs.TerrainPaletteDialog dlg = new TerrainPaletteDialog();
            dlg.TerrainPalette = this.Map.TerrainPalette;
            if (dlg.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
            {
                this.Map.TerrainPalette = dlg.TerrainPalette;
            }
        }
    }
}
