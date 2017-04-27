using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace LegendsOfKesmaiSurvival.Core.Maps
{
    /// <summary>
    /// This class represents a Terrain on a Map.  Maps should not own Terrains directly, they should have a TerrainPalette instead
    /// and each Hex on the Map should have a TerrainId that corresponds to a specific Terrain in the TerrainPalette.
    /// This allows making broad changes to common Terrains without having to edit each Hex individually.
    /// </summary>
    [Serializable()]
    public class Terrain : INotifyPropertyChanged
    {
        private int _id;
        /// <summary>
        /// Gets or sets a numeric ID used to identify the terrain
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name = "";
        /// <summary>
        /// Gets or sets a name fo the terrain
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private string _backgroundImageName;
        /// <summary>
        /// Gets or sets the Name of the image object from Legends.Content for the background image of the terrain.
        /// </summary>
        public string BackgroundImageName
        {
            get { return _backgroundImageName; }
            set
            {
                if (_backgroundImageName != value)
                {
                    _backgroundImageName = value;
                    OnPropertyChanged("BackgroundImageName");
                }
            }
        }

        /// <summary>
        /// Gets the background image for the terrain from Legends.Content
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Bitmap BackgroundImage
        {
            get
            {
                return Content.ContentManager.ResourceManager.GetObject(this.BackgroundImageName) as Bitmap;
            }
        }

        [field: NonSerialized()]
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
