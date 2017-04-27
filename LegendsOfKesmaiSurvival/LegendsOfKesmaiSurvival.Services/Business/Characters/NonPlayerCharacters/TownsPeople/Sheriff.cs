using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.Characters.NonPlayerCharacters.TownsPeople
{
    public class Sheriff : NonPlayerCharacter
    {
        #region Constructors
        public Sheriff(Game.Game game)
            : base(game)
        {

        }
        #endregion

        protected override void BuildActionTree()
        {
            base.BuildActionTree();
        }
    }
}
