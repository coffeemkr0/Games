using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Drawing;

namespace LegendsOfKesmaiSurvival.Core.GameStateInformation
{
    /// <summary>
    /// Represents a collection of game tiles that uses each tile's map location as a key.
    /// This collection can be used to represent the game state of a subsection of the game map, for example, the player's viewable area or the viewable area of an NPC.
    /// No two game tiles may share the same location.
    /// </summary>
    public class GameTileCollection : Hashtable
    {
        #region Declarations

        #endregion

        #region Properties

        #endregion

        #region Constructors

        #endregion

        #region Private Methods

        #endregion

        #region Public Methods
        public void Add(GameTile gameTile)
        {
            base.Add(gameTile.Location, gameTile);
        }

        public GameTile Get(Point location)
        {
            return (GameTile)base[location];
        }

        public bool IsTileVisible(Point start, Point end)
        {
            float xDelta = (end.X - start.X);
            float yDelta = (end.Y - start.Y);
            float unitX;
            float uintY;

            System.Drawing.Point checkPoint = start;

            bool previousPointBlockedLineOfSight = false;

            if (Math.Abs(xDelta) > Math.Abs(yDelta))
            {
                //TODO:What the heck is this?
                if (end.X == 30 && end.Y == 37)
                    end.X = 30;

                unitX = xDelta / Math.Abs(xDelta);
                uintY = yDelta / Math.Abs(xDelta);

                for (int x = 1; x <= Math.Abs(xDelta); x++)
                {
                    if (previousPointBlockedLineOfSight) return false;

                    checkPoint.X = start.X + (int)Math.Round(x * unitX, 0);
                    checkPoint.Y = start.Y + (int)Math.Round(x * uintY, 0);

                    GameTile gameTile = this.Get(checkPoint);

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

                    GameTile gameTile = this.Get(checkPoint);

                    if (gameTile.WallImageNames.Count > 0) previousPointBlockedLineOfSight = true;

                }
            }

            return true;
        }
        #endregion
    }
}
