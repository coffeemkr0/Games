using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LegendsOfKesmaiSurvival.Services.Business.Maps;
using System.Threading;
using LegendsOfKesmaiSurvival.Services.Business.Characters.NonPlayerCharacters;
using LegendsOfKesmaiSurvival.Services.Business.Characters.PlayerCharacters;
using System.Drawing;
using LegendsOfKesmaiSurvival.Services.Business.Characters;
using LegendsOfKesmaiSurvival.Core.Pathfinding;
using LegendsOfKesmaiSurvival.Services.Game;


namespace LegendsOfKesmaiSurvival.Services.Zombies
{
    public class ZombiesSystem
    {
        public event EventHandler Updated;
        private void OnUpdated()
        {
            EventHandler temp = Updated;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public RecentPathContainer RecentlyUsedPaths { get; set; }

        public static bool Cancel { get; set; }

        private Random _random = new Random();

        private Game.Game _game;

        byte[,] _byteMap;

        public List<NonPlayerCharacter> Zombies { get; set; }

        public List<PlayerCharacter> Players { get; set; }

        public ZombiesSystem(Game.Game game)
        {
            _game = game;
            this.Zombies = new List<NonPlayerCharacter>();
            this.RecentlyUsedPaths = new RecentPathContainer();
            this.Players = new List<PlayerCharacter>();
            RefreshByteMap();
        }

        public void Start()
        {
            Cancel = false;
            Thread thread = new Thread(new ThreadStart(Run));
            thread.Start();
        }

        private void Run()
        {
            while (!Cancel)
            {
                if (this.Players.Count == 0) continue;

                DateTime now = DateTime.Now;

                //TODO:Create logic to pick a player to follow
                PlayerCharacter player = this.Players[0];

                //Iterate each zombie and make it move toward the first player based on its movement speed and the last time it moved
                foreach (NonPlayerCharacter zombie in this.Zombies)
                {
                    if (now - zombie.LastMoved >= new TimeSpan(0, 0, zombie.MovementSpeed))
                    {
                        zombie.LastMoved = now;
                        List<Point> pathPoints;
                        RecentlyUsedPath recentPath = this.RecentlyUsedPaths.GetRecentlyUsedPath(zombie.Location, player.Location);
                        if (recentPath != null)
                        {
                            pathPoints = recentPath.Path;
                        }
                        else
                        {
                            pathPoints = GetShortestPath(zombie.Location, player.Location);
                            recentPath = new RecentlyUsedPath();
                            recentPath.Start = zombie.Location;
                            recentPath.End = player.Location;
                            recentPath.Path = pathPoints;
                            this.RecentlyUsedPaths.Insert(0, recentPath);
                            if (this.RecentlyUsedPaths.Count > 5)
                            {
                                this.RecentlyUsedPaths.RemoveRange(5, 1);
                            }
                        }

                        if (pathPoints != null)
                        {
                            if (zombie.MovementDistance >= pathPoints.Count)
                            {
                                zombie.Location = pathPoints.Last();
                            }
                            else
                            {
                                zombie.Location = pathPoints[zombie.MovementDistance];
                            }

                            OnUpdated();
                        }
                    }
                }
            }
        }

        public void RefreshByteMap()
        {
            _byteMap = new byte[_game.GameMap.Size.Width, _game.GameMap.Size.Height];
            for (int y = 0; y < _game.GameMap.Size.Height; y++)
            {
                for (int x = 0; x < _game.GameMap.Size.Width; x++)
                {
                    //A byte value of 0 means the tile is blocked, 1 means passable and anything greater than one adds to the movement cost of crossing the tile (up to 100).
                    byte byteValue = 1;
                    GameTile gameTile = (GameTile)_game.GameMap.GetTileFromLocation(x, y);
                    if (!gameTile.IsPassable)
                    {
                        byteValue = 0;
                    }
                    _byteMap[x, y] = byteValue;
                }
            }
        }

        //TODO:This is allowing a path to go through two diagonal tiles that are unpassable
        public List<Point> GetShortestPath(Point start, Point end)
        {
            PathFinder pathFinder = new PathFinder(_byteMap);
            List<PathFinderNode> path;

            pathFinder.Formula = HeuristicFormula.Manhattan;
            pathFinder.Diagonals = true;
            pathFinder.HeavyDiagonals = false;
            pathFinder.HeuristicEstimate = 1;
            pathFinder.PunishChangeDirection = false;
            pathFinder.TieBreaker = false;
            pathFinder.SearchLimit = _game.GameMap.Size.Width * _game.GameMap.Size.Height;
            pathFinder.DebugProgress = false;

            pathFinder.DebugFoundPath = false;
            path = pathFinder.FindPath(start, end);

            if (path != null)
            {
                List<Point> points = new List<Point>();

                foreach (PathFinderNode pathNode in path)
                {
                    points.Add(new Point(pathNode.X, pathNode.Y));
                }

                return points;
            }

            return null;
        }
    }

    public class RecentlyUsedPath
    {
        public Point Start { get; set; }

        public Point End { get; set; }

        public List<Point> Path { get; set; }
    }

    public class RecentPathContainer : List<RecentlyUsedPath>
    {
        public RecentPathContainer()
        {
            
        }

        public RecentlyUsedPath GetRecentlyUsedPath(Point start, Point end)
        {
            RecentlyUsedPath recentlyUsedPath = this.Where(i => i.Start == start && i.End == end).FirstOrDefault();
            if (recentlyUsedPath != null) return recentlyUsedPath;
            return null;
        }
    }
}
