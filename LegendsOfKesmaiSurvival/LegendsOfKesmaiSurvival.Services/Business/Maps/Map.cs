using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace LegendsOfKesmaiSurvival.Services.Business.Maps
{
    #region Map
    /// <summary>
    /// A class that holds information about a map for a level in the game.  This object holds all information needed for the game to create
    /// a game state map.  The game should not use the map directly once a game has been started.
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

        private Size _tileSize = new Size(55, 55);
        /// <summary>
        /// Gets or sets the size of the tiles in the map in pixels.
        /// </summary>
        public Size TileSize
        {
            get { return _tileSize; }
            set
            {
                if (_tileSize != value)
                {
                    _tileSize = value;
                    OnMapChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the Width of tiles in pixels.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public int TileWidth
        {
            get { return this.TileSize.Width; }
            set { this.TileSize = new Size(value, this.TileSize.Height); }
        }

        /// <summary>
        /// Gets or sets the Height of tiles in pixels.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public int TileHeight
        {
            get { return this.TileSize.Height; }
            set { this.TileSize = new Size(this.TileSize.Width, value); }
        }

        /// <summary>
        /// Gets or sets the size of the map in tiles.
        /// </summary>
        [System.Xml.Serialization.XmlIgnore()]
        public Size Size
        {
            get { return Tiles.Size; }
            set
            {
                Tiles.Size = value;
                OnMapChanged();
            }
        }

        private MapTileCollection _tiles;
        /// <summary>
        /// Gets or sets the list of tiles contained in the map
        /// </summary>
        public MapTileCollection Tiles
        {
            get { return _tiles; }
            set
            {
                if (_tiles != null)
                {
                    _tiles.CollectionChanged -= new EventHandler(_tiles_CollectionChanged);
                }
                _tiles = value;
                if (_tiles != null)
                {
                    _tiles.CollectionChanged += new EventHandler(_tiles_CollectionChanged);
                }
                OnMapChanged();
            }
        }

        private TerrainPalette _terrainPalette = new TerrainPalette();
        /// <summary>
        /// Gets or sets the TerrainPalette for the Map's Tiles to use.
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
            _tiles = new MapTileCollection(this);
            _tiles.CollectionChanged += new EventHandler(_tiles_CollectionChanged);
        }

        /// <summary>
        /// Creates a new map.
        /// </summary>
        /// <returns>A Map object that has been initialized.  This should be used to create new maps instead of the default constructor.</returns>
        public static Map NewMap()
        {
            Map map = new Map();
            map.Tiles.Add(new MapTile(new Point(0, 0), 0));
            return map;
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Renders the map to an image that shows all of the tiles on the map
        /// </summary>
        /// <returns></returns>
        public Bitmap RenderToImage()
        {
            Bitmap mapImage = new Bitmap((this.Tiles.Right + 1) * this.TileSize.Width, (this.Tiles.Bottom + 1) * this.TileSize.Height);
            using (Graphics g = Graphics.FromImage(mapImage))
            {
                //Sort the list of tiles by their location, in the order that they should be drawn
                this.Tiles.SortByLocation();

                System.Diagnostics.Stopwatch stopWatch = new System.Diagnostics.Stopwatch();
                stopWatch.Start();

                //Iterate over the map's tiles and draw them in their appropriate location
                foreach (MapTile tile in this.Tiles)
                {
                    //Get the terrain object for the tile
                    Terrain terrain = this.TerrainPalette.Get(tile.TerrainId);

                    //Get the terrain image.  If the terrain was not found, create an empty image the size of a tile
                    Bitmap terrainImage = terrain != null ? terrain.BackgroundImage : new Bitmap(this.TileSize.Width, this.TileSize.Height);

                    //Get the greater of the tile width and the terrain image width 
                    int width = this.TileSize.Width < terrainImage.Width ? terrainImage.Width : this.TileSize.Width;
                    //Get the greater of the tile height and the terrain image height
                    int height = this.TileSize.Height < terrainImage.Height ? terrainImage.Height : this.TileSize.Height;

                    //Calculate each corner of the destination rectangle based on the tile's location, size and image size
                    //Images that are bigger than the tile width need to be drawn so that their bottom right lines up with the
                    //bottom right of each tile
                    int x1 = this.TileSize.Width * tile.Location.X;
                    if (width > this.TileSize.Width)
                    {
                        x1 -= width - this.TileSize.Width;
                    }
                    int x2 = this.TileSize.Width * (tile.Location.X + 1);

                    int y1 = this.TileSize.Height * tile.Location.Y;
                    if (height > this.TileSize.Height)
                    {
                        y1 -= height - this.TileSize.Height;
                    }
                    int y2 = this.TileSize.Height * (tile.Location.Y + 1);

                    Rectangle destinationRectangle = new Rectangle(x1, y1, x2 - x1, y2 - y1);

                    g.DrawImage(terrainImage, destinationRectangle);

                    //Draw the tile's walls
                    foreach (string wallImage in tile.WallImages)
                    {
                        using (Bitmap wallImageBitmap = Content.ContentManager.GetImage(wallImage))
                        {
                            if (wallImage != null)
                            {
                                //We need to draw the wall so that its image has its bottom right corner in the bottom right corner of the tile
                                Point wallOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                wallOrigin.X -= wallImageBitmap.Width;
                                wallOrigin.Y -= wallImageBitmap.Height;

                                g.DrawImage(wallImageBitmap, wallOrigin);
                            }
                        }
                    }

                    //Draw the tile's unpassable objects
                    foreach (string unpassableObject in tile.UnpassableObjects)
                    {
                        using (Bitmap objectImage = Content.ContentManager.GetImage(unpassableObject))
                        {
                            if (objectImage != null)
                            {
                                //We need to draw the object so that its image has its bottom right corner in the bottom right corner of the tile
                                Point objectOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                objectOrigin.X -= objectImage.Width;
                                objectOrigin.Y -= objectImage.Height;

                                g.DrawImage(objectImage, objectOrigin);
                            }
                        }
                    }

                    //Draw the tile's normal objects
                    foreach (string normalObject in tile.Objects)
                    {
                        using (Bitmap objectImage = Content.ContentManager.GetImage(normalObject))
                        {
                            if (objectImage != null)
                            {
                                //We need to draw the object so that its image has its bottom right corner in the bottom right corner of the tile
                                Point objectOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                objectOrigin.X -= objectImage.Width;
                                objectOrigin.Y -= objectImage.Height;

                                g.DrawImage(objectImage, objectOrigin);
                            }
                        }
                    }

                    //Draw doors
                    foreach (string door in tile.DoorImages)
                    {
                        using (Bitmap doorImage = Content.ContentManager.GetImage(door))
                        {
                            if (doorImage != null)
                            {
                                //We need to draw the object so that its image has its bottom right corner in the bottom right corner of the tile
                                Point doorOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                doorOrigin.X -= doorImage.Width;
                                doorOrigin.Y -= doorImage.Height;

                                g.DrawImage(doorImage, doorOrigin);
                            }
                        }
                    }

                    //Draw NPCs
                    foreach (MapNonPlayerCharacter npc in tile.NPCs)
                    {
                        //TODO:Need to handle NPC's image names better
                        string npcImageName = "NPC_" + npc.NonPlayerCharacterType.ToString();
                        using (Bitmap npcImage = Content.ContentManager.GetImage(npcImageName))
                        {
                            if (npcImage != null)
                            {
                                //We need to draw the objecvt so that its image has its bottom right corner in the bottom right corner of the tile
                                Point npcOrigin = new Point(destinationRectangle.Right, destinationRectangle.Bottom);

                                //Adjust the origin based on the image size
                                npcOrigin.X -= npcImage.Width;
                                npcOrigin.Y -= npcImage.Height;

                                g.DrawImage(npcImage, npcOrigin);
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
        /// Gets a list of tiles around a point on the map.
        /// This method can be used to get the viewable tiles for a player based on their current location on the map.
        /// </summary>
        /// <param name="point">The location to get the surrounding tiles from.</param>
        /// <returns>A List of MapTiles</returns>
        public List<MapTile> GetTilesFromPoint(Point point)
        {
            List<MapTile> tiles = new List<MapTile>();

            //The default viewinig area for a player is 7X7 tiles.
            //We need to get the tile the player is on and 3 tiles above, below, left and right of the player.
            for (int x = point.X - 3; x <= point.X + 3; x++)
            {
                for (int y = point.Y - 3; y <= point.Y + 3; y++)
                {
                    tiles.Add(this.Tiles.GetFromLocation(x, y));
                }
            }

            return tiles;
        }
        #endregion

        #region Private Methods
        void _tiles_CollectionChanged(object sender, EventArgs e)
        {
            OnMapChanged();
        }
        #endregion
    }
    #endregion
}
