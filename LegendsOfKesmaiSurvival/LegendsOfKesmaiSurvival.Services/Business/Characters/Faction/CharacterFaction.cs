using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.Characters.Faction
{
    /// <summary>
    /// Represents a character's faction with another character.
    /// This can be used to help with aggro, pets, charming etc.
    /// This class is intended to be used "owned" by characters in order to help them when they need to "think" about
    /// who to attack or protect or ignore etc.
    /// </summary>
    public class CharacterFaction
    {
        /// <summary>
        /// An enum that holds the different types of relationships one character can have towards another.
        /// </summary>
        public enum RelationshipTypes
        {
            /// <summary>
            /// The character hates the other character.
            /// </summary>
            Hate,
            /// <summary>
            /// The character is neutral toward the other character
            /// </summary>
            Neutral,
            /// <summary>
            /// The character loves the other character
            /// </summary>
            Love
        }

        /// <summary>
        /// Gets or sets the type of relationship the character has with the other.
        /// </summary>
        public RelationshipTypes RelationshipType { get; set; }

        /// <summary>
        /// Gets or sets a numeric value that represents the level of a character's relationship with another.
        /// </summary>
        public int FactionLevel { get; set; }

        /// <summary>
        /// Gets or sets the key of the other character that the faction is associated with.
        /// </summary>
        public Guid OtherCharacterKey { get; set; }
    }
}
