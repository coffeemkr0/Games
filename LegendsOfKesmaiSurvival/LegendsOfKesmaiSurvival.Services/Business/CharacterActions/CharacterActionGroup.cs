using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.CharacterActions
{
    /// <summary>
    /// Represents a group of character actions that a character can perform.
    /// This collection can be used to help create an "Action Tree" to group actions together.
    /// </summary>
    [Serializable()]
    public class CharacterActionGroup : BusinessBaseList<CharacterAction>
    {
        #region Properties
        /// <summary>
        /// Gets or sets the action tree that the group belongs to.  Every group must belong to a tree.
        /// </summary>
        public CharacterActionTree Tree { get; set; }

        /// <summary>
        /// Gets or sets a group that this group belongs to.  This value can be null if the group is not part of another group.
        /// </summary>
        public CharacterActionGroup Group { get; set; }

        /// <summary>
        /// Gets or sets the group level cooldown for ations within the group in miliseconds.
        /// 
        /// A group level cool down can make it so that all actions in the group must wait for the group cooldown
        /// to expire before any other action in the group can be performed.
        /// </summary>
        public long GroupCooldown { get; set; }

        /// <summary>
        /// Gets or sets how many miliseconds are left before the group cool down expires.
        /// </summary>
        public int GroupCooldownTimeRemaining { get; set; }

        private List<CharacterActionGroup> _groups = new List<CharacterActionGroup>();
        /// <summary>
        /// Gets or sets a list of child groups that belong to this group.
        /// 
        /// The child groups are what make it possible to build an action tree structure so that
        /// groups of groups can be created and so on.
        /// For example, fire spells and ice spells may be in their own group but both may belong to
        /// a group called spells.
        /// </summary>
        public List<CharacterActionGroup> Groups
        {
            get { return _groups; }
            set
            {
                _groups = value;
            }
        }
        #endregion

        #region Constructors
        /// <summary>
        /// We hide the default constructor so that we can't create groups that don't belong to a tree.
        /// </summary>
        private CharacterActionGroup()
        {

        }

        public CharacterActionGroup(CharacterActionTree tree, CharacterActionGroup group)
        {
            this.Tree = tree;
            this.Group = group;
        }
        #endregion
    }
}
