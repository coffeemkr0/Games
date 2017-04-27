using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.Characters.PlayerCharacters
{
    /// <summary>
    /// Represents a player character in the game.
    /// In other words, this is a character that the player controls during the game.
    /// </summary>
    public class PlayerCharacter : Character
    {
        public PlayerCharacter(Game.Game game)
            : base(game)
        {
            
        }
    }
}
