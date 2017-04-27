using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.CharacterActions
{
    //TODO:Pretty dumb to have a processor that belongs to every character and then also have a corresponding action tree for the same character.
    //Either needs to be all in the action tree or this class needs a new purpose to help the server optimize action processing.

    /// <summary>
    /// This class is used to process character actions.
    /// 
    /// The class helps the game server by encapsulating all of the logic needed for characters to execute actions and then manage
    /// cool downs and interuptions in the character's action tree.
    /// </summary>
    public class CharacterActionProcessor
    {
        #region Events
        /// <summary>
        /// Handles characters executing actions.
        /// </summary>
        /// <param name="action"></param>
        public delegate void ActionExecutedEventHandler(Characters.Character character, CharacterAction action);
        /// <summary>
        /// Gets raised whenever the processor executes a character action.
        /// </summary>
        public event ActionExecutedEventHandler ActionExecuted;
        protected virtual void OnActionExecuted(CharacterAction action)
        {
            ActionExecutedEventHandler temp = ActionExecuted;
            if (temp != null)
            {
                temp(this.Character, action);
            }
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the character action tree for the processor.
        /// </summary>
        public CharacterActionTree CharacterActionTree { get; set; }

        /// <summary>
        /// Gets or sets a reference to the character that owns the action processor.
        /// </summary>
        public Characters.Character Character { get; set; }
        #endregion

        #region Constructors
        private CharacterActionProcessor()
        {

        }

        public CharacterActionProcessor(Characters.Character character)
        {
            this.Character = character;
        }
        #endregion

        #region Public Methods
        public bool CanExecuteAction(int actionId)
        {
            if (CharacterActionTree.RecoveryTimeRemaining > 0) return false;

            CharacterAction action = CharacterActionTree.GetActionById(actionId);

            if (action.IsAffectedByGlobalCooldown)
            {
                if (CharacterActionTree.GlobalCooldownTimeRemaining > 0)
                {
                    return false;
                }
            }

            if (action.IsAffectedByGroupCooldown)
            {
                if (action.Group.GroupCooldownTimeRemaining > 0)
                {
                    return false;
                }
            }

            if (action.CooldownTimeRemaining > 0) return false;

            return true;
        }

        public void ExecuteAction(int actionId)
        {
            //Start cooldowns

            OnActionExecuted(this.CharacterActionTree.GetActionById(actionId));
        }
        #endregion
    }
}
