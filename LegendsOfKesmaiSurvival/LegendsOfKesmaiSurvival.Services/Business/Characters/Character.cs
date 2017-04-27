using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LegendsOfKesmaiSurvival.Services.Business.Characters
{
    /// <summary>
    /// Represents a character in the game.
    /// This can be player and non player characters.
    /// </summary>
    public class Character : BusinessBase
    {
        #region Declarations
        private static int _currentId = 0;
        #endregion

        #region Properties
        private Guid _characterKey = Guid.NewGuid();
        /// <summary>
        /// Gets a globally unique key for the character that can be helped to identify it anywhere in any list.
        /// </summary>
        public Guid CharacterKey
        {
            get { return _characterKey; }
        }

        /// <summary>
        /// Gets or sets the character's position on the map.
        /// </summary>
        public System.Drawing.Point Location { get; set; }

        /// <summary>
        /// Gets or sets the actions that are available to the character
        /// </summary>
        public CharacterActions.CharacterActionTree ActionTree { get; set; }

        /// <summary>
        /// Gets or sets a processor for the character's actions.
        /// </summary>
        public CharacterActions.CharacterActionProcessor CharacterActionProcessor { get; set; }

        /// <summary>
        /// Gets or sets a list of character action Ids that are sorted in a way that the actions at the top of the list should be performed first.
        /// </summary>
        public List<int> CharacterActionPriorityList { get; set; }

        /// <summary>
        /// Gets or sets a list of character factions that this character has with other characters.
        /// </summary>
        public List<Faction.CharacterFaction> CharacterFactions { get; set; }

        /// <summary>
        /// Gets or sets the NPC's maximum health in hit points.
        /// </summary>
        public int MaximumHealth { get; set; }

        /// <summary>
        /// Gets or sets the current health of the NPC in hit points.
        /// </summary>
        public int CurrentHealth { get; set; }

        /// <summary>
        /// Gets or sets NPC's maximum mana in mana points.
        /// </summary>
        public int MaximumMana { get; set; }

        /// <summary>
        /// Gets or sets the current mana that the NPC has available in mana points.
        /// </summary>
        public int CurrentMana { get; set; }

        /// <summary>
        /// Gets or sets the name of an image from the Content assembly to use as the character's image.
        /// </summary>
        public string ContentImageName { get; set; }

        public System.Timers.Timer ThinkTimer { get; set; }

        /// <summary>
        /// Gets or sets a reference to the game the character is currently in.
        /// </summary>
        public Game.Game Game { get; set; }

        /// <summary>
        /// Gets or sets how many tiles away the character can see.
        /// This does not include the tile the character is on.
        /// </summary>
        public int SightPerceptionDistance { get; set; }

        /// <summary>
        /// Gets or sets how many tiles away the character can smell.
        /// This does not include the tile the character is on.
        /// </summary>
        public int SmellPerceptionDistance { get; set; }

        /// <summary>
        /// Gets or sets how many tiles away the character can hear.
        /// This does not include the tile the character is on.
        /// </summary>
        public int HearingPerceptionDistance { get; set; }

        /// <summary>
        /// Gets or sets the number of tiles the character can move over during a move.
        /// </summary>
        public int MovementDistance { get; set; }

        /// <summary>
        /// Gets or sets the movement speed for the character affecting how often they can perform a move.
        /// //TODO:For now this just holds the number of seconds before the character can move again.  This needs to be changed so that a higher move speed means move more often.
        /// </summary>
        public int MovementSpeed { get; set; }
        #endregion

        #region Constructors
        /// <summary>
        /// A default constructor that is hidden so that we can ensure characters are created with the needed parameters.
        /// This can still be used by an xml serializer however.
        /// </summary>
        private Character()
        {
            this.Id = _currentId++;

            this.CharacterFactions = new List<Faction.CharacterFaction>();
            this.CharacterActionProcessor = new CharacterActions.CharacterActionProcessor(this);

            this.SightPerceptionDistance = 3;
            this.SmellPerceptionDistance = 3;
            this.HearingPerceptionDistance = 3;
        }

        /// <summary>
        /// Creates an instance of the Character class.  Every character needs a reference to the game they are in
        /// so that they can respond to changes in the game state.
        /// </summary>
        public Character(Game.Game game)
            : this()
        {
            this.Game = game;
        }
        #endregion

        #region Private Methods
        private bool CanISeeHearOrSmellATile(Game.GameTile tile)
        {
            return CanISeeATile(tile) || CanIHearATile(tile) || CanISmellATile(tile);
        }
        #endregion

        #region Virutal Methods
        protected virtual bool CanISeeATile(Game.GameTile tile)
        {
            //First check to make sure the tile is within the character's sight distance
            if (!tile.IsWithinDistance(this.Location, this.SightPerceptionDistance)) return false;

            //TODO:Check to see if anything is blocking line of sight.

            return true;
        }

        protected virtual bool CanISmellATile(Game.GameTile tile)
        {
            if (!tile.IsWithinDistance(this.Location, this.SmellPerceptionDistance)) return false;

            //TODO:Can something block smell?

            return true;
        }

        protected virtual bool CanIHearATile(Game.GameTile tile)
        {
            if (!tile.IsWithinDistance(this.Location, this.HearingPerceptionDistance)) return false;

            //TODO:Can something block sound?

            return true;
        }

        protected virtual void BuildActionTree()
        {
            this.ActionTree = new CharacterActions.CharacterActionTree();
            this.CharacterActionPriorityList = new List<int>();
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Gets the faction this cahracter has with another character.
        /// </summary>
        public Business.Characters.Faction.CharacterFaction GetFaction(Guid characterKey)
        {
            return this.CharacterFactions.Where(cf => cf.OtherCharacterKey == characterKey).SingleOrDefault<Business.Characters.Faction.CharacterFaction>();
        }
        #endregion
    }
}
