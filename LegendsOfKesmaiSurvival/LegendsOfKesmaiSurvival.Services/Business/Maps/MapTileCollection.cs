using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace LegendsOfKesmaiSurvival.Services.Business.Maps
{
    /// <summary>
    /// Represents a collection of map tiles that does not allow two map tiles to occupy the same location in the collection.
    /// </summary>
    public class MapTileCollection : List<MapTile>
    {
        #region Events
        /// <summary>
        /// Fires when a tile changes or when the collection is modified.
        /// </summary>
        public event EventHandler CollectionChanged;
        protected virtual void OnCollectionChanged()
        {
            EventHandler temp = CollectionChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the width and height of the map collection in tiles.
        /// Increasing the size will add additional generic tiles to the collection so that they are added to the right and bottom sides of the map.
        /// Decreasing the size will remove tiles from the collection that fall outside of the new size.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Size Size
        {
            get
            {
                //Get the max X coordinate for the the width
                int maxX = -1;
                foreach (MapTile tile in this)
                {
                    if (tile.Location.X > maxX) maxX = tile.Location.X;
                }

                //Get the max Y coordinate for the height
                int maxY = -1;
                foreach (MapTile tile in this)
                {
                    if (tile.Location.Y > maxY) maxY = tile.Location.Y;
                }

                return new Size(maxX + 1, maxY + 1);
            }
            set
            {
                if (this.Size != value)
                {
                    ChangeSize(value);
                }
            }
        }

        /// <summary>
        /// Gets the x coordinate of the tiles on the right side of the map.
        /// </summary>
        public int Right
        {
            get
            {
                return this.Size.Width;
            }
        }

        /// <summary>
        /// Gets the y coordinate of the tiles on the bottom of a map.
        /// </summary>
        public int Bottom
        {
            get
            {
                return this.Size.Height;
            }
        }

        private Map _map;
        /// <summary>
        /// Gets the map that owns the tile collection.
        /// </summary>
        public Map Map
        {
            get { return _map; }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// Creates an instance of the MapTileCollection class.
        /// </summary>
        /// <param name="map">The map that the collection belongs to.</param>
        public MapTileCollection(Map map)
        {
            _map = map;
        }
        #endregion

        #region Public List Methods
        public new void Add(MapTile item)
        {
            //Do not allow items with the same location to exist in the collection
            if (this.Find(delegate(MapTile search)
            {
                return search.Location == item.Location;
            }) != null)
            {
                throw new InvalidOperationException(String.Format("A map tile with the location {0} already exists in the map.", item.Location.ToString()));
            }
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
            item.Map = _map;
            base.Add(item);
        }

        public new void AddRange(IEnumerable<MapTile> collection)
        {
            foreach (MapTile item in collection)
            {
                //Do not allow items with the same location to exist in the collection
                if (this.Find(delegate(MapTile search)
                {
                    return search.Location == item.Location;
                }) != null)
                {
                    throw new InvalidOperationException(String.Format("A map tile with the location {0} already exists in the map.", item.Location.ToString()));
                }
            }
            foreach (MapTile item in collection)
            {
                item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                item.Map = _map;
            }
            base.AddRange(collection);
        }

        public new void Insert(int index, MapTile item)
        {
            //Do not allow items with the same location to exist in the collection
            if (this.Find(delegate(MapTile search)
            {
                return search.Location == item.Location;
            }) != null)
            {
                throw new InvalidOperationException(String.Format("A map tile with the location {0} already exists in the map.", item.Location.ToString()));
            }
            item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
            item.Map = _map;
            base.Insert(index, item);
        }

        public new void InsertRange(int index, IEnumerable<MapTile> collection)
        {
            foreach (MapTile item in collection)
            {
                //Do not allow items with the same location to exist in the collection
                if (this.Find(delegate(MapTile search)
                {
                    return search.Location == item.Location;
                }) != null)
                {
                    throw new InvalidOperationException(String.Format("A map tile with the location {0} already exists in the map.", item.Location.ToString()));
                }
            }
            foreach (MapTile item in collection)
            {
                item.PropertyChanged += new PropertyChangedEventHandler(item_PropertyChanged);
                item.Map = _map;
            }
            base.InsertRange(index, collection);
        }

        public new void Remove(MapTile item)
        {
            item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
            item.Map = null;
            base.Remove(item);
        }

        public new void RemoveAt(int index)
        {
            if (base[index] != null)
            {
                base[index].PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                base[index].Map = null;
            }
            base.RemoveAt(index);
        }

        public new void RemoveAll(Predicate<MapTile> match)
        {
            foreach (MapTile item in this.FindAll(match))
            {
                item.PropertyChanged -= new PropertyChangedEventHandler(item_PropertyChanged);
                item.Map = null;
            }
            base.RemoveAll(match);
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Sorts the list of map tiles into the order that they should be drawn based on their location.
        /// </summary>
        public void SortByLocation()
        {
            this.Sort(delegate(MapTile x, MapTile y)
            {
                if (x.Location.Y == y.Location.Y)
                {
                    return x.Location.X - y.Location.X;
                }
                else
                {
                    return x.Location.Y - y.Location.Y;
                }
            });
        }

        /// <summary>
        /// Removes a tile at a specific location.
        /// </summary>
        /// <param name="x">The x coordinate of the tile to remove</param>
        /// <param name="y">The y corrdinate of the tile to remove</param>
        public void RemoveAtLocation(int x, int y)
        {
            MapTile match = this.Find(delegate(MapTile search)
            {
                return search.Location == new Point(x, y);
            });
            if (match != null) this.Remove(match);
        }

        public MapTile GetFromLocation(int x, int y)
        {
            return this.Find(delegate(MapTile search)
            {
                return search.Location == new Point(x, y);
            });
        }
        #endregion

        #region Private Methods
        void item_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCollectionChanged();
        }

        private void ChangeSize(Size newSize)
        {
            Size originalSize = this.Size;

            //If the width has increased, we add more columns to the map using the original height to determine how many tiles to add to each column
            if (originalSize.Width < newSize.Width)
            {
                int x = originalSize.Width;
                while (x < newSize.Width)
                {
                    for (int y = 0; y < originalSize.Height; y++)
                    {
                        this.Add(new MapTile(new Point(x, y), 0));
                    }
                    x += 1;
                }
            }

            //If the width has decreased, we remove columns from the map
            if (originalSize.Width > newSize.Width)
            {
                int x = originalSize.Width - 1;
                while (x > newSize.Width - 1)
                {
                    for (int y = 0; y < originalSize.Height; y++)
                    {
                        this.RemoveAtLocation(x, y);
                    }
                    x -= 1;
                }
            }

            //Now that the width has been changed, we need to update the original size to the new width
            originalSize.Width = newSize.Width;

            //If the height has increased, we add more rows to the map using the original Height to determine how many tiles to add to each row
            if (originalSize.Height < newSize.Height)
            {
                int y = originalSize.Height;
                while (y < newSize.Height)
                {
                    for (int x = 0; x < originalSize.Width; x++)
                    {
                        this.Add(new MapTile(new Point(x, y), 0));
                    }
                    y += 1;
                }
            }

            //If the Height has decreased, we remove columns from the map
            if (originalSize.Height > newSize.Height)
            {
                int y = originalSize.Height - 1;
                while (y > newSize.Height - 1)
                {
                    for (int x = 0; x < originalSize.Width; x++)
                    {
                        this.RemoveAtLocation(x, y);
                    }
                    y -= 1;
                }
            }
        }
        #endregion
    }
}
