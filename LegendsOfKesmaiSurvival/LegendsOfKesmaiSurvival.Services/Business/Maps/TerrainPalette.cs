using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;


namespace LegendsOfKesmaiSurvival.Services.Business.Maps
{
    /// <summary>
    /// A class used as a container for Terrains.  This class is used by a Map to store the Terrains
    /// instead of each individual tile having a terrain object.
    /// </summary>
    [Serializable()]
    public class TerrainPalette : List<Terrain>, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets a Terrain object from the palette.
        /// </summary>
        /// <param name="id">The Id of the terrain to get.</param>
        /// <returns>A Terrain object if the palette has a Terrain with the specified Id, otherwise null.</returns>
        public Terrain Get(int id)
        {
            return this.Find(delegate(Terrain search)
            {
                return search.ID == id;
            });
        }

        public string ToXml()
        {
            Type type = this.GetType();
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(type);

            System.IO.StringWriter s = new System.IO.StringWriter();
            xs.Serialize(s, this);

            return s.ToString();
        }

        public static TerrainPalette LoadFromXml(string xml)
        {
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(TerrainPalette));

            System.IO.StringReader s = new System.IO.StringReader(xml);

            TerrainPalette obj = (TerrainPalette)xs.Deserialize(s);

            return obj;
        }

        public TerrainPalette Clone()
        {
            BinaryFormatter binFormater = new BinaryFormatter();
            using (System.IO.MemoryStream memStream = new System.IO.MemoryStream())
            {
                binFormater.Serialize(memStream, this);
                //Reset the posotion to initial so that it can read the bytes
                memStream.Position = 0;
                return binFormater.Deserialize(memStream) as TerrainPalette;
            }
        }

        #region Public List Methods
        public new void Add(Terrain item)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
            base.Add(item);
        }

        public new void AddRange(IEnumerable<Terrain> collection)
        {
            foreach (Terrain item in collection)
            {
                item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
            }
            base.AddRange(collection);
        }

        public new void Insert(int index, Terrain item)
        {
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
            base.Insert(index, item);
        }

        public new void InsertRange(int index, IEnumerable<Terrain> collection)
        {
            foreach (Terrain item in collection)
            {
                item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
            }
            base.InsertRange(index, collection);
        }

        public new void Remove(Terrain item)
        {
            item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
            base.Remove(item);
        }

        public new void RemoveAt(int index)
        {
            if (base[index] != null)
            {
                base[index].PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
            }
            base.RemoveAt(index);
        }

        public new void RemoveAll(Predicate<Terrain> match)
        {
            foreach (Terrain item in this.FindAll(match))
            {
                item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
            }
            base.RemoveAll(match);
        }
        #endregion

        #region Private Methods
        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged("Items");
        }
        #endregion

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
