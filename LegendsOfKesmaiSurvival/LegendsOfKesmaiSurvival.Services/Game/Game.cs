using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsOfKesmaiSurvival.Services.Business.Characters.PlayerCharacters;
using System.Threading;
using LegendsOfKesmaiSurvival.Services.Business.Maps;

namespace LegendsOfKesmaiSurvival.Services.Game
{
    /// <summary>
    /// Represents an instance of a game that is being played.
    /// This class holds all information about the game and has access to all all systems that control the game.
    /// </summary>
    public class Game
    {
        #region Events
        /// <summary>
        /// Event that gets raised when the game has ended.
        /// </summary>
        public event EventHandler GameOver;
        void OnGameOver()
        {
            EventHandler temp = GameOver;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Declarations
        private bool _endGame;
        private bool _pauseGame;
        private Level _currentLevel;
        private Map _map;
        #endregion

        #region Properties
        private List<PlayerCharacter> _players = new List<PlayerCharacter>();
        /// <summary>
        /// Gets the list of characters that represent the players playing the game
        /// </summary>
        public List<PlayerCharacter> Players
        {
            get { return _players; }
        }

        private GameMap _gameMap;
        /// <summary>
        /// Gets the map for the game.
        /// </summary>
        public GameMap GameMap
        {
            get { return _gameMap; }
        }
        #endregion

        #region Initialization
        public Game(Map map)
        {
            _gameMap = new GameMap(map);
        }
        #endregion

        #region Private Methods
        private void Update()
        {
            while (!_endGame)
            {
                //If the game is paused, don't do any updates
                if (_pauseGame) continue;

                //If there are no players left, the game is over
                if (this.Players.Count == 0)
                {
                    OnGameOver();
                    break;
                }


            }
        }

        private void LoadNextLevel()
        {

        }
        #endregion

        #region Public Methods
        public void AddPlayer(PlayerCharacter player)
        {
            _players.Add(player);
        }

        public void StartGame()
        {
            Thread updateThread = new Thread(new ThreadStart(Update));
            updateThread.Start();
        }

        public void EndGame()
        {
            _endGame = true;
        }

        public void PauseGame()
        {
            _pauseGame = true;
        }

        public void UnPauseGame()
        {
            _pauseGame = false;
        }
        #endregion
    }
}
