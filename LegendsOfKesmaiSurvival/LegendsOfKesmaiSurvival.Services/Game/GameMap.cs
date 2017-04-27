using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using LegendsOfKesmaiSurvival.Services.Business.Maps;
using System.Drawing;

namespace LegendsOfKesmaiSurvival.Services.Game
{
    /// <summary>
    /// Represents the current state of a Map in an active game.
    /// </summary>
    public class GameMap
    {
        #region Declarations
        private Map _map;
        #endregion

        #region Properties
        /// <summary>
        /// Holds the list of game tiles on the map by location.  No two tiles may have the same location.
        /// The key of the hashtable is the tile's location.
        /// </summary>
        public Hashtable GameTiles { get; set; }

        /// <summary>
        /// Gets the size of the map.
        /// </summary>
        public Size Size
        {
            //TODO:Can the size of the map change during gameplay?  If so, this does not need to reference the original Map.
            get { return _map.Size; }
        }
        #endregion

        #region Constructors
        public GameMap(Map map)
        {
            _map = map;
            LoadMap();
        }
        #endregion

        #region Private Methods
        private void LoadMap()
        {
            //Convert the map into a game map
            this.GameTiles = new Hashtable();

            foreach (MapTile mapTile in _map.Tiles)
            {
                GameTile gameTile = new GameTile(this, mapTile.Location);
                foreach (string wallImage in mapTile.WallImages)
                {
                    //TODO:Refactor - For now, walls are only on the south and east side
                    //The name of the image determines where it is - horz = south, vert = east
                    if(wallImage.Contains("Horz"))
                    {
                        GameTileWall gameWall = new GameTileWall(gameTile);
                        gameWall.ImageName = wallImage;
                        gameTile.Walls.Add(GameTileWall.WallTilePositions.South, gameWall);
                    }
                    else if(wallImage.Contains("Vert"))
                    {
                        GameTileWall gameWall = new GameTileWall(gameTile);
                        gameWall.ImageName = wallImage;
                        gameTile.Walls.Add(GameTileWall.WallTilePositions.East, gameWall);
                    }
                }

                foreach (string objectImage in mapTile.Objects)
                {
                    GameTileObject gameTileObject = new GameTileObject();
                    gameTileObject.Passable = true;
                    gameTileObject.ImageName = objectImage;
                }

                foreach (string unpassableObjectImage in mapTile.UnpassableObjects)
                {
                    GameTileObject gameTileObject = new GameTileObject();
                    gameTileObject.Passable = false;
                    gameTileObject.ImageName = unpassableObjectImage;
                }

                this.GameTiles.Add(mapTile.Location, gameTile);
            }
        }
        #endregion

        #region Public Methids
        public GameTile GetTileFromLocation(int x, int y)
        {
            return (GameTile)this.GameTiles[new Point(x, y)];
        }
        #endregion
    }
}
