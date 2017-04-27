using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace LegendsOfKesmaiSurvival.Core.Maps
{
    /// <summary>
    /// Represents an object on a MapHex.
    /// </summary>
    public class MapObject
    {
        private int _id;
        /// <summary>
        /// Gets or sets a numeric ID used to identify the object
        /// </summary>
        public int ID
        {
            get { return _id; }
            set { _id = value; }
        }

        private string _name;
        /// <summary>
        /// Gets or sets a name of the object
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        private byte[] _imageData;
        [Browsable(false)]
        public byte[] ImageData
        {
            get { return _imageData; }
            set
            {
                if (_imageData == null)
                {
                    _imageData = value;
                }
                else
                {
                    if (!_imageData.Equals(value))
                    {
                        _imageData = value;
                        OnImageChanged();
                    }
                }

            }
        }

        [System.Xml.Serialization.XmlIgnore()]
        public Bitmap Image
        {
            get
            {
                if (this.ImageData == null) return null;
                System.IO.MemoryStream ms = new System.IO.MemoryStream(this.ImageData);
                Bitmap image = new Bitmap(ms);
                ms.Close();
                return image;
            }
            set
            {
                if (value == null)
                {
                    this.ImageData = null;
                    return;
                }
                System.IO.MemoryStream ms = new System.IO.MemoryStream();
                value.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                this.ImageData = ms.ToArray();
                ms.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnImageChanged()
        {
            if (PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Image"));
        }
    }

    public class MapObjectPalette
    {
        public List<MapObject> MapObjects { get; set; }

        public MapObjectPalette()
        {
            this.MapObjects = new List<MapObject>();
        }

        public string ToXml()
        {
            Type type = this.GetType();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);

            System.IO.StringWriter s = new System.IO.StringWriter();
            xs.Serialize(s, this);

            return s.ToString();
        }

        public static MapObjectPalette LoadFromXml(string xml)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(MapObjectPalette));

            System.IO.StringReader s = new System.IO.StringReader(xml);

            MapObjectPalette obj = (MapObjectPalette)xs.Deserialize(s);

            return obj;
        }
    }
}
