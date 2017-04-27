using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.CharacterActions
{
    /// <summary>
    /// This class represents any action that a character can perform.
    /// Actions can be things like casting a spell, using an item, attacking with a weapon or moving.
    /// </summary>
    [Serializable()]
    public class CharacterAction : BusinessBase
    {
        #region Properties
        /// <summary>
        /// Gets or sets the group that the action belongs to.  All actions must belong to a group.
        /// </summary>
        public CharacterActionGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the type of the action.  This helps the server know how to process the action.
        /// </summary>
        public CharacterActionTypes ActionType { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether or not the action is affected by the global cool down.
        /// </summary>
        public bool IsAffectedByGlobalCooldown { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether or not the action restarts the global cool down.
        /// </summary>
        public bool ResetsGlobalCooldown { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether or not the action is affected by its group cool down.
        /// </summary>
        public bool IsAffectedByGroupCooldown { get; set; }

        /// <summary>
        /// Gets or sets a flag indicating whether ot not the action restarts its group cool down.
        /// </summary>
        public bool ResetsGroupCooldown { get; set; }

        /// <summary>
        /// Gets or sets the time in miliseconds it takes to prepare for the action.
        /// </summary>
        public int PreparationTime { get; set; }

        /// <summary>
        /// Gets or sets the time in miliseconds it takes to execute the action.
        /// </summary>
        public int ExecutionTime { get; set; }

        /// <summary>
        /// Gets or sets the time in miliseconds it takes for the action to be ready to execute again.
        /// </summary>
        public long Cooldown { get; set; }

        /// <summary>
        /// Gets or sets how many miliseconds are left before the cool down expires.
        /// </summary>
        public int CooldownTimeRemaining { get; set; }

        /// <summary>
        /// Gets or sets the time in miliseconds that it takes for the character to recover from the action before they can perform
        /// any other actions.
        /// </summary>
        public int RecoveryTime { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// We hide the default constructor so that we can't create actions that don't belong to a group.
        /// </summary>
        private CharacterAction()
        {

        }

        public CharacterAction(CharacterActionGroup group)
        {

        }
        #endregion
    }
}
