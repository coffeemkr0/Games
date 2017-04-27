using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.CharacterActions
{
    /// <summary>
    /// Represents all of the actions that a character can perform in a hierarchial structure.
    /// 
    /// The actions are held in a tree structure so that we can have complex grouping for actions.  This enables us to have
    /// actions in broad categories like Spells, Attacks, or Movement while also group actions in those broad categories even more,
    /// such as having schools of magic inside the Spells group.
    /// </summary>
    [Serializable()]
    public class CharacterActionTree
    {
        #region Properties
        /// <summary>
        /// Gets or sets a time in miliseconds that can be applied to actions that have their IsAffectedByGlobalCooldown flag set to true.
        /// </summary>
        public int GlobalCooldown { get; set; }

        /// <summary>
        /// Gets or sets how many miliseconds are left before the global cool down expires.
        /// </summary>
        public int GlobalCooldownTimeRemaining { get; set; }

        /// <summary>
        /// Gets or sets how many miliseconds are left before the character has recovered from a previous action.
        /// </summary>
        public int RecoveryTimeRemaining { get; set; }

        private List<CharacterActionGroup> _actionGroups = new List<CharacterActionGroup>();
        /// <summary>
        /// Gets or sets a list of action groups for the tree.
        /// </summary>
        public List<CharacterActionGroup> ActionGroups
        {
            get { return _actionGroups; }
            set
            {
                _actionGroups = value;
            }
        }
        #endregion

        #region PrivateMethods
        private CharacterAction GetById(int actionId, CharacterActionGroup group)
        {
            CharacterAction characterAction =  group.Where(a => a.Id == actionId).SingleOrDefault<CharacterAction>();

            if (characterAction != null) return characterAction;

            foreach (CharacterActionGroup childGroup in group.Groups)
            {
                characterAction = GetById(actionId, group);
                if (characterAction != null) return characterAction;
            }

            return null;
        }
        #endregion

        #region Public Methods
        public CharacterAction GetActionById(int actionId)
        {
            foreach (CharacterActionGroup group in this.ActionGroups)
            {
                CharacterAction characterAction = GetById(actionId, group);
                if (characterAction != null) return characterAction;
            }

            return null;
        }
        #endregion
    }
}
