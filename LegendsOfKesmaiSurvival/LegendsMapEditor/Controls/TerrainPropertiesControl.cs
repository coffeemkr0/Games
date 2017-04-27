using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.Controls
{
    public partial class TerrainPropertiesControl : UserControl
    {
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain Terrain
        {
            get { return terrainBindingSource.DataSource as LegendsOfKesmaiSurvival.Services.Business.Maps.Terrain; }
            set
            {
                cmbBackgroundImage.Items.Clear();
                List<string> items = new List<string>();
                items.AddRange(LegendsOfKesmaiSurvival.Content.ContentManager.GetTerrains().Keys);
                items.Sort(delegate(string x, string y)
                {
                    return x.CompareTo(y);
                });
                cmbBackgroundImage.Items.AddRange(items.ToArray());
                terrainBindingSource.DataSource = value; 
            }
        }

        public TerrainPropertiesControl()
        {
            InitializeComponent();
        }
    }
}
