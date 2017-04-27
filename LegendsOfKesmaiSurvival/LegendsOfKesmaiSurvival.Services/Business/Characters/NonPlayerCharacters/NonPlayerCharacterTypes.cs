using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.Characters.NonPlayerCharacters
{
    //TODO:I wasn't really in love with the idea of having a giant enum for all of the NPC types, but for now, it will suffice.

    /// <summary>
    /// Represents all of the types of NPCs in the game.
    /// </summary>
    public enum NonPlayerCharacterTypes
    {
        /// <summary>
        /// Um, don't use this.  I needed to add it for the map editor's type converter to support a property grid.
        /// </summary>
        None,
        /// <summary>
        /// The sheriff.  Oh sheriff, poor poor sheriff.  Such evil plans are in store for you.
        /// </summary>
        Sheriff
    }
}
