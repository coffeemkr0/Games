using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace LegendsOfKesmaiSurvival.Core.Maps
{
    #region Map
    /// <summary>
    /// Represents a map in the game.
    /// </summary>
    public class Map
    {
        #region Events
        /// <summary>
        /// Fires when the map changes.  This can be used by a map editor to repaint the map.
        /// </summary>
        public event EventHandler MapChanged;
        protected virtual void OnMapChanged()
        {
            EventHandler temp = MapChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the map
        /// </summary>
        public string Name { get; set; }

        private Size _hexSize = new Size(55, 55);
        /// <summary>
        /// Gets or sets the size of the hexes in the map in pixels.
        /// </summary>
        public Size HexSize
        {
            get { return _hexSize; }
            set
            {
                if (_hexSize != value)
                {
                    _hexSize = value;
                    OnMapChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Width of hexes in pixels.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public int HexWidth
        {
            get { return this.HexSize.Width; }
            set { this.HexSize = new Size(value, this.HexSize.Height); }
        }

        /// <summary>
        /// Gets or sets the Height of hexes in pixels.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public int HexHeight
        {
            get { return this.HexSize.Height; }
            set { this.HexSize = new Size(this.HexSize.Width, value); }
        }

        /// <summary>
        /// Gets or sets the size of the map in hexes.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Size Size
        {
            get { return Hexes.Size; }
            set
            {
                Hexes.Size = value;
                OnMapChanged();
            }
        }

        private MapHexCollection _hexes;
        /// <summary>
        /// Gets or sets the list of hexes contained in the map
        /// </summary>
        public MapHexCollection Hexes
        {
            get { return _hexes; }
            set
            {
                if (_hexes != null)
                {
                    _hexes.CollectionChanged -= new EventHandler(_hexes_CollectionChanged);
                }
                _hexes = value;
                if (_hexes != null)
                {
                    _hexes.CollectionChanged += new EventHandler(_hexes_CollectionChanged);
                }
                OnMapChanged();
            }
        }

        private TerrainPalette _terrainPalette = new TerrainPalette();
        /// <summary>
        /// Gets or sets the TerrainPalette for the Map's Hexes to use.
        /// </summary>
        public TerrainPalette TerrainPalette
        {
            get { return _terrainPalette; }
            set
            {
                if (_terrainPalette != value)
                {
                    _terrainPalette = value;
                }
            }
        }
        #endregion

        #region Constructors
        public Map()
        {
            _hexes = new MapHexCollection(this);
            _hexes.CollectionChanged += new EventHandler(_hexes_CollectionChanged);
        }

        /// <summary>
        /// Creates a new map.
        /// </summary>
        /// <returns>A Map object that has been initialized.  This should be used to create new maps instead of the default constructor.</returns>
        public static Map NewMap()
        {
            Map map = new Map();
            map.Hexes.Add(new MapHex(new Point(0, 0), 0));
            return map;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Renders the map to an image that shows all of the hexes on the map
        /// </summary>
        /// <returns></returns>
        public Bitmap RenderToImage()
        {
            Bitmap mapImage = new Bitmap((this.Hexes.Right + 1) * this.HexSize.Width, (this.Hexes.Bottom + 1) * this.HexSize.Height);
            using (Graphics g = Graphics.FromImage(mapImage))
            {
                //Sort the list of hexes by their location, in the order that they should be drawn
                this.Hexes.SortByLocation();

                System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();

                //Iterate over the map's hexes and draw them in their appropriate location
                foreach (MapHex hex in this.Hexes)
                {
                    //Get the terrain object for the hex
                    Terrain terrain = this.TerrainPalette.Get(hex.TerrainId);

                    //Get the terrain image.  If the terrain was not found, create an empty image the size of a hex
                    Bitmap terrainImage = terrain != null ? terrain.BackgroundImage : new Bitmap(this.HexSize.Width, this.HexSize.Height);

                    //Get the greater of the hex width and the terrain image width 
                    int width = this.HexSize.Width < terrainImage.Width ? terrainImage.Width : this.HexSize.Width;
                    //Get the greater of the hex height and the terrain image height
                    int height = this.HexSize.Height < terrainImage.Height ? terrainImage.Height : this.HexSize.Height;

                    //Calculate each corner of the destination rectangle based on the hex's location, size and image size
                    //Images that are bigger than the hex width need to be drawn so that their bottom right lines up with the
                    //bottom right of each hex
                    int x1 = this.HexSize.Width * hex.Location.X;
                    if (width > this.HexSize.Width)
                    {
                        x1 -= width - this.HexSize.Width;
                    }
                    int x2 = this.HexSize.Width * (hex.Location.X + 1);

                    int y1 = this.HexSize.Height * hex.Location.Y;
                    if (height > this.HexSize.Height)
                    {
                        y1 -= height - this.HexSize.Height;
                    }
                    int y2 = this.HexSize.Height * (hex.Location.Y + 1);

                    Rectangle destinationRectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);

                    g.DrawImage(terrainImage, destinationRectangle);

                    //Draw the hex's walls
                    foreach (string wallImage in hex.WallImages)
                    {
                        using (Bitmap wallImageBitmap = Content.ContentManager.GetImage(wallImage))
                        {
                            if (wallImage != null)
                            {
                                //We need to draw the wall so that its image has its bottom right corner in the bottom right corner of the hex
                                Point wallOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                wallOrigin.X -= wallImageBitmap.Width;
                                wallOrigin.Y -= wallImageBitmap.Height;

                                g.DrawImage(wallImageBitmap, wallOrigin);
                            }
                        }
                    }

                    //Draw the hex's unpassable objects
                    foreach (string unpassableObject in hex.UnpassableObjects)
                    {
                        using (Bitmap objectImage = Content.ContentManager.GetImage(unpassableObject))
                        {
                            if (objectImage != null)
                            {
                                //We need to draw the object so that its image has its bottom right corner in the bottom right corner of the hex
                                Point objectOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                objectOrigin.X -= objectImage.Width;
                                objectOrigin.Y -= objectImage.Height;

                                g.DrawImage(objectImage, objectOrigin);
                            }
                        }
                    }

                    //Draw the hex's normal objects
                    foreach (string normalObject in hex.Objects)
                    {
                        using (Bitmap objectImage = Content.ContentManager.GetImage(normalObject))
                        {
                            if (objectImage != null)
                            {
                                //We need to draw the object so that its image has its bottom right corner in the bottom right corner of the hex
                                Point objectOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                objectOrigin.X -= objectImage.Width;
                                objectOrigin.Y -= objectImage.Height;

                                g.DrawImage(objectImage, objectOrigin);
                            }
                        }
                    }

                    //Draw doors
                    foreach (string door in hex.DoorImages)
                    {
                        using (Bitmap doorImage = Content.ContentManager.GetImage(door))
                        {
                            if (doorImage != null)
                            {
                                //We need to draw the object so that its image has its bottom right corner in the bottom right corner of the hex
                                Point doorOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                doorOrigin.X -= doorImage.Width;
                                doorOrigin.Y -= doorImage.Height;

                                g.DrawImage(doorImage, doorOrigin);
                            }
                        }
                    }

                }

                stopWatch.Stop();
                Console.Out.WriteLine(stopWatch.Elapsed.Milliseconds.ToString());
            }

            return mapImage;
        }

        /// <summary>
        /// Serializes the map into an xml string.
        /// </summary>
        /// <returns>A string containing the xml of the serialization of the map.</returns>
        public void SaveToFile(string file)
        {
            System.IO.StreamWriter sw = new System.IO.StreamWriter(file, false);
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(this.GetType());
            xs.Serialize(sw, this);
            sw.Close();
        }

        public static Map LoadFromFile(string file)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(file);
            System.Xml.Serialization.XmlSerializer xs = new System.Xml.Serialization.XmlSerializer(typeof(Map));
            Map map = (Map)xs.Deserialize(sr);
            sr.Close();

            return map;
        }

        /// <summary>
        /// Gets a list of hexes around a point on the map.
        /// This method can be used to get the viewable hexes for a player based on their current location on the map.
        /// </summary>
        /// <param name="point">The location to get the surrounding hexes from.</param>
        /// <returns>A List of MapHexes</returns>
        public List<MapHex> GetHexesFromPoint(Point point)
        {
            List<MapHex> hexes = new List<MapHex>();

            //The default viewinig area for a player is 7X7 hexes.
            //We need to get the hex the player is on and 3 hexes above, below, left and right of the player.
            for (int x = point.X - 3; x <= point.X + 3; x++)
            {
                for (int y = point.Y - 3; y <= point.Y + 3; y++)
                {
                    hexes.Add(this.Hexes.GetFromLocation(x, y));
                }
            }

            return hexes;
        }
        #endregion

        #region Private Methods
        void _hexes_CollectionChanged(object sender, EventArgs e)
        {
            OnMapChanged();
        }
        #endregion
    }
    #endregion
}
