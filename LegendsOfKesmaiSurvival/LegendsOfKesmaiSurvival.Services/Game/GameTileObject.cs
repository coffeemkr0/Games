using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Game
{
    /// <summary>
    /// Represents an object on a game tile.
    /// </summary>
    public class GameTileObject
    {
        #region Properties
        /// <summary>
        /// Gets or sets the name of the image that the client should use to draw the object.
        /// </summary>
        public string ImageName { get; set; }

        /// <summary>
        /// Gets or sets whether the object can be moved over by a character.
        /// </summary>
        public bool Passable { get; set; }
        #endregion
    }
}
