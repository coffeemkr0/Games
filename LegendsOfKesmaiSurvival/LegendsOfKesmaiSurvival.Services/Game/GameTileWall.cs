using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Game
{
    /// <summary>
    /// Represents a wall in an active game.
    /// </summary>
    public class GameTileWall
    {
        #region Enums
        /// <summary>
        /// An enum that holds all of the valid positions for a wall on a game tile.
        /// </summary>
        public enum WallTilePositions
        {
            North,
            South,
            East,
            West
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the name of the image that the client should use to draw the wall on the tile
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets the position of the wall on its tile.
        /// </summary>
        public WallTilePositions TilePosition { get; set; }

        private GameTile _gameTile;
        /// <summary>
        /// Gets the Game Tile that the wall belongs to.
        /// </summary>
        public GameTile GameTile
        {
            //TODO:Will a wall ever move to another tile?  If so, it does not need to reference a tile.  Same with wall position.
            get { return _gameTile; }
        }
        #endregion

        #region Constructors
        public GameTileWall(GameTile gameTile)
        {
            _gameTile = gameTile;
        }
        #endregion
    }
}
