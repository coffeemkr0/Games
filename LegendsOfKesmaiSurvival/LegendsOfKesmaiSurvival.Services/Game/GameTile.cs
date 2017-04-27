using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;

namespace LegendsOfKesmaiSurvival.Services.Game
{
    /// <summary>
    /// Represents a tile in an active game.
    /// </summary>
    public class GameTile
    {
        #region Declarations
        private GameMap _gameMap;
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the location of the tile on its map.
        /// </summary>
        public Point Location { get; set; }

        /// <summary>
        /// Gets or sets the walls on the tile.  No two walls can share the same position on the tile.
        /// </summary>
        public Dictionary<GameTileWall.WallTilePositions, GameTileWall> Walls { get; set; }

        /// <summary>
        /// Gets or sets the list of objects on the tile.  These objects should live on the tile and never move.
        /// </summary>
        public List<GameTileObject> Objects { get; set; }

        /// <summary>
        /// Gets whether or not the tile can be moved over by a character.
        /// </summary>
        public bool IsPassable
        {
            get
            {
                if (this.Walls.Count > 0) return false;
                if (this.Objects.Where(i => i.Passable == false).Count() > 0) return false;
                return true;
            }
        }
        #endregion

        #region Constructors
        private GameTile()
        {
            this.Walls = new Dictionary<GameTileWall.WallTilePositions, GameTileWall>();
            this.Objects = new List<GameTileObject>();
        }

        public GameTile(GameMap gameMap, Point location)
            : this()
        {
            _gameMap = gameMap;
            this.Location = location;
        }
        #endregion

        #region Public Methods
        public bool IsWithinDistance(System.Drawing.Point location, int distance)
        {
            int xDistance = Math.Abs(location.X - this.Location.X);
            int yDistance = Math.Abs(location.Y - this.Location.Y);
            if (xDistance > distance) return false;
            if (yDistance > distance) return false;

            return true;
        }
        #endregion
    }
}
