using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Core.GameStateInformation
{
    //TODO:How does a tile get information about its surrounding tiles so that it can give information about walls on tiles next to it?


    /// <summary>
    /// Represents a tile in the game.  This class is used to pass tile information between the server and client during game state updates.
    /// This is different from the MapTile in that the MapTile is only used by the server to to load map information.
    /// </summary>
    public class GameTile
    {
        /// <summary>
        /// Gets or sets the location of the tile in relation to the other tiles that were passed with the game state update.
        /// For example, the tile that is viewable to the player in the top left corner will have a location of 0,0 while
        /// the tile that is viewable to the player in the bottom right corner will have a location of 7,7 if the viewable area is 7X7.
        /// </summary>
        public System.Drawing.Point Location { get; set; }

        /// <summary>
        /// Gets or sets the name of the image to use from the content manager for the tiles background image.
        /// </summary>
        public string BackgroundImageName { get; set; }

        /// <summary>
        /// Gets or sets a list of images to use as walls for the tile.
        /// </summary>
        public List<string> WallImageNames { get; set; }

        /// <summary>
        /// Gets or sets a list of imags to use as door objects for the tile.
        /// </summary>
        public List<string> DoorNames { get; set; }

        /// <summary>
        /// Gets or sets a list of images to use as unpassable objects for the tile.
        /// </summary>
        public List<string> UnpassableObjectImageNames { get; set; }

        /// <summary>
        /// Gets or sets a list of images to use as non player characters.
        /// </summary>
        public List<string> NonPlayableCharacterImageNames { get; set; }

        /// <summary>
        /// Gets or sets a list of imags to use as normal objects for the tile.
        /// </summary>
        public List<string> ObjectNames { get; set; }

        public GameTile()
        {
            this.WallImageNames = new List<string>();
            this.UnpassableObjectImageNames = new List<string>();
            this.ObjectNames = new List<string>();
            this.DoorNames = new List<string>();
            this.NonPlayableCharacterImageNames = new List<string>();
        }
    }
}
