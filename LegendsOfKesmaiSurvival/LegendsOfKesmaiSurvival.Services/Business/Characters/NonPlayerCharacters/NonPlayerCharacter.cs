using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace LegendsOfKesmaiSurvival.Services.Business.Characters.NonPlayerCharacters
{
    //TODO:Should consider if this is approriate or approriately named.
    //I put this class in the Services library because it needs to only be accessible by the server.
    //Basically anything in the Business namespace is a class that only the server should know about.
    
    /// <summary>
    /// Reprsents a non player character in the game.
    /// This includes things like merchants, trainers, zombies and bosses.
    /// </summary>
    [Serializable()]
    public class NonPlayerCharacter : Character
    {
        #region Properties
        /// <summary>
        /// Gets or sets the type of the NPC.
        /// </summary>
        public NonPlayerCharacterTypes NonPlayerCharacterType { get; set; }

        public DateTime LastMoved { get; set; }
        #endregion

        #region Constructors
        public NonPlayerCharacter(Game.Game game)
            : base(game)
        {

        }
        #endregion

        #region Public Methods
        protected virtual void Spawn(System.Drawing.Point startingPoint)
        {

        }
        #endregion
    }
}
