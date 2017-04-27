using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using LegendsOfKesmaiSurvival.Services.Business.Characters.NonPlayerCharacters;

namespace LegendsOfKesmaiSurvival.Services.Business.Maps
{
    /// <summary>
    /// Represents an NPC on a map.
    /// </summary>
    public class MapNonPlayerCharacter
    {
        public string Name { get; set; }

        public NonPlayerCharacterTypes NonPlayerCharacterType { get; set; }

        public MapNonPlayerCharacter()
        {

        }

        public MapNonPlayerCharacter(NonPlayerCharacterTypes type, string name)
        {
            this.NonPlayerCharacterType = type;
            this.Name = name;
        }
    }
}
