using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Text;
using LegendsOfKesmaiSurvival.Core.Exceptions;
using LegendsOfKesmaiSurvival.Services.Business.Characters;
using LegendsOfKesmaiSurvival.Services.Business.Characters.NonPlayerCharacters;
using LegendsOfKesmaiSurvival.Services.Business.Characters.PlayerCharacters;
using System.Drawing;

namespace LegendsOfKesmaiSurvival.Services.Zombies
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public class ZombiesService : IGameplay
    {
        #region Declarations
        public static Dictionary<Guid, GameplayCommunicationsStore> ConnectedClients = new Dictionary<Guid, GameplayCommunicationsStore>();
        private LegendsOfKesmaiSurvival.Services.Business.Maps.Map _map;
        private List<Character> _players = new List<Character>();
        Game.Game _game;
        
        private Random _random = new Random();
        ZombiesSystem _zombiesSystem;
        #endregion

        #region IGameplayCallback Methods
        public void Join(Guid key)
        {
            //Get the call back for the game play client
            IGameplayCallback callback = OperationContext.Current.GetCallbackChannel<IGameplayCallback>();

            //Create a CommunicationsStore to hold information about the connected client
            GameplayCommunicationsStore client = new GameplayCommunicationsStore(OperationContext.Current.InstanceContext, callback);

            //TODO:Need to handle the client not being authenticated
            client.UserAccount = Authentication.AuthenticationService.GetUserAccount(key);

            if (!OperationContext.Current.IncomingMessageProperties.ContainsKey(RemoteEndpointMessageProperty.Name))
            {
                client.IPAddress = "127.0.0.1";
            }
            else
            {
                client.IPAddress = ((RemoteEndpointMessageProperty)OperationContext.Current.IncomingMessageProperties[RemoteEndpointMessageProperty.Name]).Address;
            }

            //Add the client to our static list.  We need to lock the list for synchronization safety.
            lock (ConnectedClients)
            {
                ConnectedClients.Add(key, client);
            }

            PlayerCharacter playerCharacter = new PlayerCharacter(_game);
            _players.Add(playerCharacter);
            playerCharacter.Location = new System.Drawing.Point(13, 10);
            client.PlayerCharacterId = playerCharacter.Id;
            _zombiesSystem.Players.Add(playerCharacter);

            Console.WriteLine("{0} joined the game.", client.UserAccount.Nickname);

            UpdateGameState(key);
        }

        public void SendChat(Guid key, string text)
        {
            //TODO:Add the nickname of the user that the chat came from
            foreach (Guid connectedKeys in ConnectedClients.Keys)
            {
                ConnectedClients[connectedKeys].Callback.ChatReceivedCallback(text);
            }
        }

        public void Move(Guid key, List<Core.GameStateInformation.Directions> movements)
        {
            if (!ConnectedClients.ContainsKey(key)) return;

            GameplayCommunicationsStore connectedClient = ConnectedClients[key];
            Character playerCharacter = _players.Where(i => i.Id == connectedClient.PlayerCharacterId).First();

            //TODO:Implement movement restrictions here
            //TODO:This is allowing the player to move diagonally through two hexes that are unpassable.
            foreach (Core.GameStateInformation.Directions movementDirection in movements)
            {
                Point newLocation = playerCharacter.Location;
                switch (movementDirection)
                {
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.North:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X, playerCharacter.Location.Y - 1);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.South:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X, playerCharacter.Location.Y + 1);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.East:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X + 1, playerCharacter.Location.Y);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.West:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X - 1, playerCharacter.Location.Y);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.NorthEast:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X + 1, playerCharacter.Location.Y - 1);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.NorthWest:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X - 1, playerCharacter.Location.Y - 1);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.SouthEast:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X + 1, playerCharacter.Location.Y + 1);
                        break;
                    case LegendsOfKesmaiSurvival.Core.GameStateInformation.Directions.SouthWest:
                        newLocation = new System.Drawing.Point(playerCharacter.Location.X - 1, playerCharacter.Location.Y + 1);
                        break;
                }

                //Get the tile that the player is trying to move to
                Game.GameTile newTile = _game.GameMap.GetTileFromLocation(newLocation.X, newLocation.Y);

                //Do not let the player move to a tile that is not accessible
                if (newTile.IsPassable)
                {
                    playerCharacter.Location = newLocation;
                }
            }

            //TODO:Only need to update the game states of clients that would be affected by the player moving in or into the viewable area
            foreach (Guid clientKey in ConnectedClients.Keys)
            {
                UpdateGameState(clientKey);
            }
        }
        #endregion

        #region Public Methods
        public void StartGame(string mapFile)
        {
            Console.WriteLine("Starting game.");

            //TODO: We do not need to hold references to game objects in the service.
            _map = Business.Maps.Map.LoadFromFile(mapFile);

            _game = new Game.Game(_map);

            _zombiesSystem = new ZombiesSystem(_game);
            _zombiesSystem.Updated += new EventHandler(_zombiesSystem_Updated);

            //Create some random zombies
            for (int i = 0; i < 7; i++)
            {
                NonPlayerCharacter zombie = new NonPlayerCharacter(_game);
                zombie.Location = new System.Drawing.Point(_random.Next(_map.Size.Width - 1), _random.Next(_map.Size.Height - 1));
                zombie.MovementDistance = 2;
                zombie.MovementSpeed = _random.Next(2) + 1;
                _zombiesSystem.Zombies.Add(zombie);
            }

            _zombiesSystem.Start();

            Console.WriteLine("Map loaded, zombies started.");
        }

        void _zombiesSystem_Updated(object sender, EventArgs e)
        {
            foreach (Guid key in ConnectedClients.Keys)
            {
                UpdateGameState(key);
            }
        }
        #endregion

        #region Private Methods
        private void UpdateGameState(Guid key)
        {
            GameplayCommunicationsStore client = ConnectedClients[key];

            Core.GameStateInformation.GameStateUpdate gameStateUpdate = new Core.GameStateInformation.GameStateUpdate();

            Character playerCharacter = _players.Where(i => i.Id == client.PlayerCharacterId).First();

            gameStateUpdate.ViewableArea = GetViewableArea(playerCharacter.Location, gameStateUpdate);
            gameStateUpdate.OtherPlayers = GetOtherViewablePlayersPositions(key, playerCharacter.Location);
            gameStateUpdate.NpcsInTheArea = GetNpcsInTheArea(key, playerCharacter.Location);

            client.Callback.GameStateUpdatedCallback(gameStateUpdate);
        }

        //TODO:Worst method name to go with the worst class name ever?
        private Core.GameStateInformation.OtherViewablePlayerInformation GetOtherViewablePlayersPositions(Guid playerKey, System.Drawing.Point playerPosition)
        {
            //TODO:This all needs to be based on the current player's position and not rely on a 7X7 viewable area

            List<System.Drawing.Point> otherPlayerPositions = new List<System.Drawing.Point>();

            foreach (Character otherPlayerCharcters in _players.Where(i=>i.Id != ConnectedClients[playerKey].PlayerCharacterId))
            {
                //Only add viewable players (assuming a 7x7 viewable area)
                if (otherPlayerCharcters.Location.X < playerPosition.X - 3 || otherPlayerCharcters.Location.Y < playerPosition.Y - 3 ||
                    otherPlayerCharcters.Location.X > playerPosition.X + 3 || otherPlayerCharcters.Location.Y > playerPosition.Y + 3)
                {
                    continue;
                }

                //Get the other player's position in relation to the current player's position
                System.Drawing.Point otherPlayerPosition = new System.Drawing.Point(otherPlayerCharcters.Location.X, otherPlayerCharcters.Location.Y);
                otherPlayerPosition.X = otherPlayerPosition.X - playerPosition.X;
                otherPlayerPosition.Y = otherPlayerPosition.Y - playerPosition.Y;

                otherPlayerPositions.Add(otherPlayerPosition);
            }

            return otherPlayerPositions.Count == 0 ? null : new Core.GameStateInformation.OtherViewablePlayerInformation(otherPlayerPositions);
        }

        private List<Core.GameStateInformation.NpcInformation> GetNpcsInTheArea(Guid playerKey, System.Drawing.Point playerPosition)
        {
            //TODO:This all needs to be based on the current player's position and not rely on a 7X7 viewable area

            List<Core.GameStateInformation.NpcInformation> zombiesInTheArea = new List<Core.GameStateInformation.NpcInformation>();

            List<System.Drawing.Point> positions = new List<System.Drawing.Point>();

            foreach (NonPlayerCharacter zombie in _zombiesSystem.Zombies)
            {
                //Only add viewable zombies (assuming a 7x7 viewable area)
                if (zombie.Location.X < playerPosition.X - 3 || zombie.Location.Y < playerPosition.Y - 3 ||
                    zombie.Location.X > playerPosition.X + 3 || zombie.Location.Y > playerPosition.Y + 3)
                {
                    continue;
                }

                //Get the zombie position in relation to the current player's position, default to the player's tile
                System.Drawing.Point zombiePosition = new System.Drawing.Point(3, 3);
                if (zombie.Location.Y < playerPosition.Y)
                {
                    zombiePosition.Y -= playerPosition.Y - zombie.Location.Y;
                }
                else if (zombie.Location.Y > playerPosition.Y)
                {
                    zombiePosition.Y += zombie.Location.Y - playerPosition.Y;
                }

                if (zombie.Location.X < playerPosition.X)
                {
                    zombiePosition.X -= playerPosition.X - zombie.Location.X;
                }
                else if (zombie.Location.X > playerPosition.X)
                {
                    zombiePosition.X += zombie.Location.X - playerPosition.X;
                }

                Core.GameStateInformation.NpcInformation npcInformation = new Core.GameStateInformation.NpcInformation();
                npcInformation.NpcRelativeLocation = zombiePosition;
                npcInformation.NpcAvatarName = "NPC_Sheriff";
                zombiesInTheArea.Add(npcInformation);
            }

            return zombiesInTheArea;
        }

        //TODO:Move this to a business object instead of having it in the service.
        private List<Core.GameStateInformation.GameTile> GetViewableArea(System.Drawing.Point playerPosition, Core.GameStateInformation.GameStateUpdate gameStateUpdate)
        {
            //We need to get a list of tiles that are currently viewable to the player based on their current position in the map
            List<Core.GameStateInformation.GameTile> tiles = new List<Core.GameStateInformation.GameTile>();

            int relativeY = 0;
            //TODO:Currently assuming that the player view is 7X7
            for (int y = playerPosition.Y - 3; y <= playerPosition.Y + 3; y++)
            {
                int relativeX = 0;
                for (int x = playerPosition.X - 3; x <= playerPosition.X + 3; x++)
                {
                    Business.Maps.MapTile mapTile = _map.Tiles.GetFromLocation(x, y);
                    Core.GameStateInformation.GameTile gameTile = new Core.GameStateInformation.GameTile();
                    gameTile.Location = new System.Drawing.Point(relativeX, relativeY);

                    if (mapTile != null)
                    {
                        gameTile.BackgroundImageName = mapTile.Terrain.BackgroundImageName;
                        gameTile.WallImageNames = mapTile.WallImages;
                        gameTile.UnpassableObjectImageNames = mapTile.UnpassableObjects;
                        gameTile.ObjectNames = mapTile.Objects;
                        gameTile.DoorNames = mapTile.DoorImages;

                        foreach (Business.Maps.MapNonPlayerCharacter npc in mapTile.NPCs)
                        {
                            //TODO:Need to handle the npc image name
                            string imageName = "NPC_" + npc.NonPlayerCharacterType.ToString();
                            gameTile.NonPlayableCharacterImageNames.Add(imageName);
                        }
                    }

                    tiles.Add(gameTile);

                    relativeX++;
                }
                relativeY++;
            }

            return tiles;
        }
        #endregion
    }
}
