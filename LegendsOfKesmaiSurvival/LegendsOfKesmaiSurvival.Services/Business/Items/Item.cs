using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.Items
{
    /// <summary>
    /// Represents an item in the game.  An item can be anything like a weapon, potion or usable object.
    /// </summary>
    public class Item : BusinessBase
    {
        /// <summary>
        /// Gets or sets the name of an image from the Content assembly to use as the item's image.
        /// </summary>
        public string ContentImageName { get; set; }
    }
}
