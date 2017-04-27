using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace LegendsOfKesmaiSurvival.Core.Maps
{
    /// <summary>
    /// Represents a hex in a map of the game.
    /// </summary>
    public class MapHex : INotifyPropertyChanged
    {
        #region Events
        /// <summary>
        /// Gets raised when a property of the map hex changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler temp = PropertyChanged;
            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
        #endregion

        #region Properties
        private Point _location = Point.Empty;
        /// <summary>
        /// Gets or sets the location of the map hex within it's map.
        /// The X coordinate is the 0 based column index of the hex.
        /// The Y coordinate is the 0 based row index of the hex.
        /// This property is not related to the Hex's actual pixel location.
        /// </summary>
        public Point Location
        {
            get { return _location; }
            set
            {
                if (_location != value)
                {
                    _location = value;
                    OnPropertyChanged("Location");
                }
            }
        }

        private int _terrainId;
        /// <summary>
        /// Gets or sets the numeric ID of the terrain for the hex.  
        /// The Id should correspond to a terrain object that exists in the Map's terrain palette.
        /// </summary>
        public int TerrainId
        {
            get { return _terrainId; }
            set
            {
                if (_terrainId != value)
                {
                    _terrainId = value;
                    OnPropertyChanged("TerrainID");
                }
            }
        }

        /// <summary>
        /// Gets the Terrain object for the hex from the Map's TerrainPalette using the MapHex's TerrainId
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Terrain Terrain
        {
            get
            {
                return this.Map.TerrainPalette.Get(this.TerrainId);
            }
        }

        //TODO:Need to add "Posts".  Walls that form corners at the top left of the building have to have a filler "post" to bridge the corner gap.

        private List<string> _wallImages = new List<string>();
        /// <summary>
        /// Gets or sets the names of wall images for the hex.  If the list is not empty it means the hex acts as a wall.
        /// </summary>
        public List<string> WallImages
        {
            get { return _wallImages; }
            set 
            {
                if (_wallImages != value)
                {
                    _wallImages = value;
                    OnPropertyChanged("WallImages");
                }
            }
        }

        /// <summary>
        /// Helper property for the map editor to make it easier to add walls to hexes
        /// </summary>
        [TypeConverter(typeof(WallImageNameTypeConverter))]
        public string WallImage1
        {
            get { return this.WallImages.Count > 0 ? this.WallImages[0] : null; }
            set
            {
                if (this.WallImages.Count == 0)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.WallImages.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.WallImages.RemoveAt(0);
                    }
                    else
                    {
                        this.WallImages[0] = value;
                    }
                }

                OnPropertyChanged("WallImages");
            }
        }

        /// <summary>
        /// Helper property for the map editor to make it easier to add walls to hexes
        /// </summary>
        [TypeConverter(typeof(WallImageNameTypeConverter))]
        public string WallImage2
        {
            get { return this.WallImages.Count > 1 ? this.WallImages[1] : null; }
            set
            {
                if (this.WallImages.Count < 2)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.WallImages.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.WallImages.RemoveAt(1);
                    }
                    else
                    {
                        this.WallImages[1] = value;
                    }
                }

                OnPropertyChanged("WallImages");
            }
        }

        private List<string> _doorImages = new List<string>();
        /// <summary>
        /// Gets or sets the names of door images for the hex.
        /// </summary>
        public List<string> DoorImages
        {
            get { return _doorImages; }
            set
            {
                if (_doorImages != value)
                {
                    _doorImages = value;
                    OnPropertyChanged("DoorImages");
                }
            }
        }

        /// <summary>
        /// Helper property for the map editor to make it easier to add a door to hexes
        /// </summary>
        [TypeConverter(typeof(DoorImageNameTypeConverter))]
        public string DoorImage1
        {
            get { return this.DoorImages.Count > 0 ? this.DoorImages[0] : null; }
            set
            {
                if (this.DoorImages.Count == 0)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.DoorImages.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.DoorImages.RemoveAt(0);
                    }
                    else
                    {
                        this.DoorImages[0] = value;
                    }
                }

                OnPropertyChanged("DoorImages");
            }
        }

        /// <summary>
        /// Helper property for the map editor to make it easier to add a door to hexes
        /// </summary>
        [TypeConverter(typeof(DoorImageNameTypeConverter))]
        public string DoorImage2
        {
            get { return this.DoorImages.Count > 1 ? this.DoorImages[1] : null; }
            set
            {
                if (this.DoorImages.Count < 2)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.DoorImages.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.DoorImages.RemoveAt(1);
                    }
                    else
                    {
                        this.DoorImages[1] = value;
                    }
                }

                OnPropertyChanged("DoorImages");
            }
        }

        /// <summary>
        /// Helper property for the map editor to make it easier to add a door to hexes
        /// </summary>
        [TypeConverter(typeof(DoorImageNameTypeConverter))]
        public string DoorImage3
        {
            get { return this.DoorImages.Count > 2 ? this.DoorImages[2] : null; }
            set
            {
                if (this.DoorImages.Count < 3)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.DoorImages.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.DoorImages.RemoveAt(2);
                    }
                    else
                    {
                        this.DoorImages[2] = value;
                    }
                }

                OnPropertyChanged("DoorImages");
            }
        }

        private List<string> _unpassableObjects = new List<string>();
        /// <summary>
        /// Gets or sets the names of objects for the hex that are make the hex act as a wall.  If the list is not empty it means the hex acts as a wall.
        /// </summary>
        public List<string> UnpassableObjects
        {
            get { return _unpassableObjects; }
            set
            {
                if (_unpassableObjects != value)
                {
                    _unpassableObjects = value;
                    OnPropertyChanged("UnpassableObjects");
                }
            }
        }

        /// <summary>
        /// Helper property for the map editor
        /// </summary>
        [TypeConverter(typeof(InsurmountableObjectTypeConverter))]
        public string UnpassableObject1
        {
            get { return this.UnpassableObjects.Count > 0 ? this.UnpassableObjects[0] : null; }
            set
            {
                if (this.UnpassableObjects.Count == 0)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.UnpassableObjects.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.UnpassableObjects.RemoveAt(0);
                    }
                    else
                    {
                        this.UnpassableObjects[0] = value;
                    }
                }

                OnPropertyChanged("UnpassableObjects");
            }
        }

        /// <summary>
        /// Helper property for the map editor
        /// </summary>
        [TypeConverter(typeof(InsurmountableObjectTypeConverter))]
        public string UnpassableObject2
        {
            get { return this.UnpassableObjects.Count > 1 ? this.UnpassableObjects[1] : null; }
            set
            {
                if (this.UnpassableObjects.Count < 2)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.UnpassableObjects.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.UnpassableObjects.RemoveAt(1);
                    }
                    else
                    {
                        this.UnpassableObjects[1] = value;
                    }
                }

                OnPropertyChanged("UnpassableObjects");
            }
        }

        private List<string> _objects = new List<string>();
        /// <summary>
        /// Gets or sets the names of objects that are on the hex.
        /// </summary>
        public List<string> Objects
        {
            get { return _objects; }
            set
            {
                if (_objects != value)
                {
                    _objects = value;
                    OnPropertyChanged("Objects");
                }
            }
        }

        /// <summary>
        /// Helper property for the map editor
        /// </summary>
        [TypeConverter(typeof(ObjectTypeConverter))]
        public string Object1
        {
            get { return this.Objects.Count > 0 ? this.Objects[0] : null; }
            set
            {
                if (this.Objects.Count == 0)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.Objects.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.Objects.RemoveAt(0);
                    }
                    else
                    {
                        this.Objects[0] = value;
                    }
                }

                OnPropertyChanged("Objects");
            }
        }

        /// <summary>
        /// Helper property for the map editor
        /// </summary>
        [TypeConverter(typeof(ObjectTypeConverter))]
        public string Object2
        {
            get { return this.Objects.Count > 1 ? this.Objects[1] : null; }
            set
            {
                if (this.Objects.Count < 2)
                {
                    if (!string.IsNullOrEmpty(value))
                    {
                        this.Objects.Add(value);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(value))
                    {
                        this.Objects.RemoveAt(1);
                    }
                    else
                    {
                        this.Objects[1] = value;
                    }
                }

                OnPropertyChanged("Objects");
            }
        }

        private Map _map;
        /// <summary>
        /// Gets or sets the Map object that the hex belongs to.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Map Map
        {
            get { return _map; }
            set
            {
                _map = value;
            }
        }
        #endregion

        #region Constructors
        public MapHex()
        {

        }

        public MapHex(Point location, int terrainID)
        {
            this.Location = location;
            this.TerrainId = terrainID;
        }
        #endregion
    }
}
