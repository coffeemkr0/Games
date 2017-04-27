using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace LegendsOfKesmaiSurvival.Core.GameStateInformation
{
    //TODO:This whole file probably isn't the best way to pass game updates, especially considering the bandwidth it takes.
    //Need to consider compressing these updates into something the client can translate and moving it out of the Core assembly.

    //TODO:Move the tile logic in this to a GameTileCollection class or something to encapsulate it instead of having it in the GameStateUpdate class.
    //TODO:May need to adjust this logic because some NPCs may be able to overcome unpassable objects or see through objects.
    public class GameStateUpdate
    {
        #region Properties
        public List<GameTile> ViewableArea { get; set; }

        public OtherViewablePlayerInformation OtherPlayers { get; set; }

        public List<NpcInformation> NpcsInTheArea { get; set; }
        #endregion

        #region Private Methods
        private GameTile GetGameTile(int x, int y)
        {
            return this.ViewableArea.Where(ph => ph.Location.X == x && ph.Location.Y == y).SingleOrDefault<GameTile>();
        }

        /// <summary>
        /// Checks to see if a tile in unpassable.
        /// </summary>
        /// <param name="gameTile">The game tile to check.</param>
        /// <returns>true if the tile has anything that makes it unpassable by a player or NPC, otherwise false.</returns>
        private bool IsTileUnpassable(GameTile gameTile)
        {
            //Currently walls and unpassable objects make a tile unpassable.
            if (gameTile.WallImageNames.Count > 0 || gameTile.UnpassableObjectImageNames.Count > 0) return true;

            return false;
        }

        /// <summary>
        /// Gets a list of tiles from the viewable area that are adjacent to another tile.
        /// </summary>
        /// <param name="gameTile">The game tile to check from.</param>
        /// <returns>A list of any tiles that are adjacent to the origin.</returns>
        private List<GameTile> GetAdjacentTiles(GameTile gameTile)
        {
            List<GameTile> adjacentTiles = new List<GameTile>();

            if (this.ViewableArea != null)
            {
                foreach (GameTile viewableTile in this.ViewableArea)
                {
                    if (viewableTile.Location == gameTile.Location) continue;

                    if (viewableTile.Location.X >= gameTile.Location.X - 1 || viewableTile.Location.X < -gameTile.Location.X + 1)
                    {
                        if (viewableTile.Location.Y >= gameTile.Location.Y - 1 || viewableTile.Location.Y < -gameTile.Location.Y + 1)
                        {
                            adjacentTiles.Add(viewableTile);
                        }
                    }
                }
            }

            return adjacentTiles;
        }

        /// <summary>
        /// Gets the game tile that is adjacent to another game tile in a specific direction.
        /// </summary>
        /// <param name="gameTile">The game tile to check from.</param>
        /// <param name="direction">The direction to check in.</param>
        /// <returns>The game tile that is adjacent in the given direction if there is one, otherwise null.</returns>
        private GameTile GetAdjacentTile(GameTile gameTile, Directions direction)
        {
            List<GameTile> adjacentGameTiles = GetAdjacentTiles(gameTile);

            foreach (GameTile adjacentTile in adjacentGameTiles)
            {
                switch (direction)
                {
                    case Directions.North:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X, gameTile.Location.Y - 1))
                        {
                            return adjacentTile;
                        }
                        break;
                    case Directions.South:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X, gameTile.Location.Y + 1))
                        {
                            return adjacentTile;
                        }
                        break;
                    case Directions.East:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X + 1, gameTile.Location.Y))
                        {
                            return adjacentTile;
                        }
                        break;
                    case Directions.West:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X - 1, gameTile.Location.Y))
                        {
                            return adjacentTile;
                        }
                        break;
                    case Directions.NorthEast:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X + 1, gameTile.Location.Y - 1))
                        {
                            return adjacentTile;
                        }
                        break;
                    case Directions.NorthWest:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X - 1, gameTile.Location.Y - 1))
                        {
                            return adjacentTile;
                        }
                        break;
                    case Directions.SouthEast:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X + 1, gameTile.Location.Y + 1))
                        {
                            return adjacentTile;
                        }
                        break;
                    case Directions.SouthWest:
                        if (adjacentTile.Location == new System.Drawing.Point(gameTile.Location.X - 1, gameTile.Location.Y + 1))
                        {
                            return adjacentTile;
                        }
                        break;
                }
            }

            return null;
        }

        /// <summary>
        /// Checks to see if a point is adjacent to a tile.
        /// </summary>
        /// <param name="gameTile">The tile to use as the origin to check from.</param>
        /// <param name="targetPoint">The relational point to check to see if it is adjacent to the origin.</param>
        /// <returns>true if the point is adjacent to the tile, otherwise false.</returns>
        private bool IsTileAdjacent(GameTile gameTile, System.Drawing.Point targetPoint)
        {
            if (this.ViewableArea != null)
            {
                foreach (GameTile viewableTile in this.ViewableArea)
                {
                    if (viewableTile.Location == gameTile.Location) return false;

                    if (viewableTile.Location.X >= gameTile.Location.X - 1 && viewableTile.Location.X < -gameTile.Location.X + 1)
                    {
                        if (viewableTile.Location.Y >= gameTile.Location.Y - 1 && viewableTile.Location.Y < -gameTile.Location.Y + 1)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        bool PathClear(System.Drawing.Point start, System.Drawing.Point end)
        {
            float xDelta = (end.X - start.X);
            float yDelta = (end.Y - start.Y);
            float unitX;
            float uintY;

            System.Drawing.Point checkPoint = start;

            bool previousPointBlockedLineOfSight = false;

            if (Math.Abs(xDelta) > Math.Abs(yDelta))
            {

                if (end.X == 30 && end.Y == 37)
                    end.X = 30;

                unitX = xDelta / Math.Abs(xDelta);
                uintY = yDelta / Math.Abs(xDelta);


                for (int x = 1; x <= Math.Abs(xDelta); x++)
                {
                    if (previousPointBlockedLineOfSight) return false;

                    checkPoint.X = start.X + (int)Math.Round(x * unitX, 0);
                    checkPoint.Y = start.Y + (int)Math.Round(x * uintY, 0);

                    GameTile gameTile = GetGameTile(checkPoint.X, checkPoint.Y);

                    if (gameTile.WallImageNames.Count > 0) previousPointBlockedLineOfSight = true;
                }
            }
            else
            {
                unitX = xDelta / Math.Abs(yDelta);
                uintY = yDelta / Math.Abs(yDelta);

                for (int x = 1; x <= Math.Abs(yDelta); x++)
                {
                    if (previousPointBlockedLineOfSight) return false;

                    checkPoint.X = start.X + (int)Math.Round(x * unitX, 0);
                    checkPoint.Y = start.Y + (int)Math.Round(x * uintY, 0);

                    GameTile gameTile = GetGameTile(checkPoint.X, checkPoint.Y);

                    if (gameTile.WallImageNames.Count > 0) previousPointBlockedLineOfSight = true;

                }
            }

            return true;
        }

        private static IEnumerable<System.Drawing.Point> GetPointsOnLine(int x0, int y0, int x1, int y1)
        {
            //This is an implementation of http://en.wikipedia.org/wiki/Bresenham%27s_line_algorithm

            bool steep = Math.Abs(y1 - y0) > Math.Abs(x1 - x0);
            if (steep)
            {
                int t;
                t = x0; // swap x0 and y0
                x0 = y0;
                y0 = t;
                t = x1; // swap x1 and y1
                x1 = y1;
                y1 = t;
            }
            if (x0 > x1)
            {
                int t;
                t = x0; // swap x0 and x1
                x0 = x1;
                x1 = t;
                t = y0; // swap y0 and y1
                y0 = y1;
                y1 = t;
            }
            int dx = x1 - x0;
            int dy = Math.Abs(y1 - y0);
            int error = dx / 2;
            int ystep = (y0 < y1) ? 1 : -1;
            int y = y0;
            for (int x = x0; x <= x1; x++)
            {
                yield return new System.Drawing.Point((steep ? y : x), (steep ? x : y));
                error = error - dy;
                if (error < 0)
                {
                    y += ystep;
                    error += dx;
                }
            }
            yield break;
        }
        #endregion

        #region Public Methods
        public GameTile GetPlayerTile()
        {
            //Assuming the player is in the center of a zero based 7X7 viewable area.  This includes a dependency that when the player
            //is on the edge of the map that there are still tiles returned for the ones that don't actually exist.
            return ViewableArea.Where(gameTile => gameTile.Location.X == 3 && gameTile.Location.Y == 3).SingleOrDefault<GameTile>();
        }

        /// <summary>
        /// Checks to see if a tile is viewable from another tile
        /// </summary>
        /// <param name="origin">The game tile to check from.</param>
        /// <param name="targetPoint">The point to check to see if it is visible from the origin.</param>
        /// <returns>true if a tile exists at the target point and no tiles block line of sight in between the origin and target, otherwise false.</returns>
        public bool IsTileViewable(GameTile origin, System.Drawing.Point targetPoint)
        {
            //Try to get the tile at the target point
            GameTile targetTile = this.ViewableArea.Where(gh => gh.Location == targetPoint).SingleOrDefault<GameTile>();

            //If the target point is off the map or not within the viewable area, it is not viewable.
            if (targetTile == null) return false;

            return PathClear(origin.Location, targetPoint);
        }

        /// <summary>
        /// Checks to see if there is a tile that is unpassable in a direction from another tile.
        /// </summary>
        /// <param name="gameTile">The tile to use as the origin.</param>
        /// <param name="direction">The direction to check for the adjacent tile.</param>
        /// <returns>true if the adjacent tile is unpassable, otherwise false.</returns>
        public bool IsAdjacentTileUnpassable(GameTile gameTile, Directions direction)
        {
            GameTile adjacentGameTile = GetAdjacentTile(gameTile, direction);

            if (adjacentGameTile == null) return true;

            //TODO:Add support for closed doors.
            if (adjacentGameTile.WallImageNames.Count > 0 || adjacentGameTile.UnpassableObjectImageNames.Count > 0) return true;

            return false;
        }

        /// <summary>
        /// Checks to see if there is a tile that blocks line of sight in a drection from another tile.
        /// </summary>
        /// <param name="gameTile">The tile to use as the origin.</param>
        /// <param name="direction">The direction to check for the adjacent tile.</param>
        /// <returns>true if the adjacent tile blocks line of sight, otherwise false.</returns>
        public bool DoesAdjacentTileBlockSight(GameTile gameTile, Directions direction)
        {
            GameTile adjacentGameTile = GetAdjacentTile(gameTile, direction);

            if (adjacentGameTile == null) return true;

            //TODO:Add support for closed doors.
            //Currently only walls block line of sight.
            if (adjacentGameTile.WallImageNames.Count > 0) return true;

            return false;
        }
        #endregion
    }

    //TODO:Worst class name ever?
    /// <summary>
    /// Represents information about other players that are viewable to the player.
    /// </summary>
    public class OtherViewablePlayerInformation
    {
        //TODO:Add the other player's avatar Id so that the client will know what to draw

        public List<System.Drawing.Point> PlayerPositions { get; set; }

        public OtherViewablePlayerInformation()
        {

        }

        public OtherViewablePlayerInformation(List<System.Drawing.Point> playerPositions)
        {
            this.PlayerPositions = playerPositions;
        }
    }

    public class NpcInformation
    {
        public Point NpcRelativeLocation { get; set; }

        public string NpcAvatarName { get; set; }
    }
}
