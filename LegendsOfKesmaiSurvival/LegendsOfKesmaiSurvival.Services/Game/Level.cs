using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Game
{
    /// <summary>
    /// Represents the current level being played in a game that is being played.
    /// </summary>
    public class Level
    {
        /// <summary>
        /// Gets or sets the level number of the level, ie Wave 1, Wave 2 etc.
        /// </summary>
        public int LevelNumber { get; set; }
    }
}
