using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LegendsOfKesmaiSurvival.Core.GameStateInformation;

namespace LegendsOfKesmaiSurvival.WinFormsClient.Controls.Zombies
{
    public partial class PlayerView : UserControl
    {
        #region Declarations
        private string _characterAvatar;
        private string _otherPlayersAvatar;
        #endregion

        #region Properties
        private Core.GameStateInformation.GameStateUpdate _gameStateUpdate;
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Core.GameStateInformation.GameStateUpdate GameStateUpdate
        {
            get { return _gameStateUpdate; }
            set
            {
                _gameStateUpdate = value;
                this.Invalidate();
            }
        }
        #endregion

        #region Constructors
        public PlayerView()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
        }
        #endregion

        #region Overrides
        protected override void OnPaint(PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(_characterAvatar))
            {
                _characterAvatar = Content.ContentManager.GetRandomCharacterAvatar();
                _otherPlayersAvatar = Content.ContentManager.GetRandomCharacterAvatar();
            }

            if (this.GameStateUpdate == null)
            {
                base.OnPaint(e);
                return;
            }

            //The size of the tiles is based on the current size of the control.  Use the smallest dimension to calculate the tile size,
            //so that we end up with square tiles
            Size tileSize = new Size(this.Width > this.Height ? this.Height / 7 : this.Width / 7, this.Width > this.Height ? this.Height / 7 : this.Width / 7);

            //Get a rectangle that represents the viewable that the tiles will be drawn on
            Rectangle viewableArea = new Rectangle(0, 0, tileSize.Width * 7, tileSize.Height * 7);
            
            //Center the viewable area to the control's client area
            if (this.Width > this.Height)
            {
                viewableArea.Location = new Point((this.Width - this.Height) / 2, 0);
            }
            else
            {
                viewableArea.Location = new Point(0, (this.Height - this.Width) / 2);
            }

            //Designate a rectnagle that the player is standing on
            Core.GameStateInformation.GameTile playerTile = this.GameStateUpdate.GetPlayerTile();

            foreach (Core.GameStateInformation.GameTile viewableTile in this.GameStateUpdate.ViewableArea)
            {
                int y = viewableTile.Location.Y * tileSize.Height + viewableArea.Y;
                int x = viewableTile.Location.X * tileSize.Width + viewableArea.X;

                //Get a rectangle for the tile
                Rectangle tileRectangle = new Rectangle(x, y, tileSize.Width, tileSize.Height);

                //If the tile is not viewable, fill it in with a black rectangle
                //TODO:Kinda dumb to call it viewable if it isn't viewable.
                if (!this.GameStateUpdate.IsTileViewable(playerTile, viewableTile.Location))
                {
                    e.Graphics.FillRectangle(Brushes.Black, tileRectangle);
                    continue;
                }

                //Draw the tile's terrain
                if (!string.IsNullOrEmpty(viewableTile.BackgroundImageName))
                {
                    using (Bitmap tileBackgroundImage = Content.ContentManager.GetImage(viewableTile.BackgroundImageName))
                    {
                        e.Graphics.DrawImage(tileBackgroundImage, tileRectangle);
                    }
                }
                else
                {
                    e.Graphics.FillRectangle(Brushes.Black, tileRectangle);
                    continue;
                }

                //Draw the tile's walls
                foreach (string wallImageName in viewableTile.WallImageNames)
                {
                    using (Bitmap wallImage = Content.ContentManager.GetImage(wallImageName))
                    {
                        if (wallImage != null)
                        {
                            //TODO:Need to not depend on a known tile size
                            //The normal size of a tile is 55X55 pixels.  However, since the control size determines our destination rectangle, we need a sort of zoom factor to apply to the wall image
                            float zoomFactor = (float)tileSize.Width / 55.0F;//Since we know we have square tiles being drawn, we can just use the width.
                            RectangleF wallRectangle = new RectangleF(0.0F, 0.0F, (float)wallImage.Width * zoomFactor, (float)wallImage.Height * zoomFactor);

                            //We need to move the rectangle so that its bottom right corner is in the bottom right corner of the tile
                            PointF wallOrigin = new Point(tileRectangle.Right, tileRectangle.Bottom);

                            //Adjust the origin based on the image size
                            wallOrigin.X -= wallRectangle.Width;
                            wallOrigin.Y -= wallRectangle.Height;
                            wallRectangle.Location = wallOrigin;

                            e.Graphics.DrawImage(wallImage, wallRectangle);
                        }
                    }
                }

                //TODO:The code for walls and objects is re-usable, consider refactoring this to another method.
                //Draw the tile's objects
                foreach (string unpassableObject in viewableTile.UnpassableObjectImageNames)
                {
                    using (Bitmap objectImage = Content.ContentManager.GetImage(unpassableObject))
                    {
                        if (objectImage != null)
                        {
                            //TODO:Need to not depend on a known tile size
                            //The normal size of a tile is 55X55 pixels.  However, since the control size determines our destination rectangle, we need a sort of zoom factor to apply to the wall image
                            float zoomFactor = (float)tileSize.Width / 55.0F;//Since we know we have square tiles being drawn, we can just use the width.
                            RectangleF objectRectangle = new RectangleF(0.0F, 0.0F, (float)objectImage.Width * zoomFactor, (float)objectImage.Height * zoomFactor);

                            //We need to move the rectangle so that its bottom right corner is in the bottom right corner of the tile
                            PointF wallOrigin = new Point(tileRectangle.Right, tileRectangle.Bottom);

                            //Adjust the origin based on the image size
                            wallOrigin.X -= objectRectangle.Width;
                            wallOrigin.Y -= objectRectangle.Height;
                            objectRectangle.Location = wallOrigin;

                            e.Graphics.DrawImage(objectImage, objectRectangle);
                        }
                    }
                }

                foreach (string normalObject in viewableTile.ObjectNames)
                {
                    using (Bitmap objectImage = Content.ContentManager.GetImage(normalObject))
                    {
                        if (objectImage != null)
                        {
                            //TODO:Need to not depend on a known tile size
                            //The normal size of a tile is 55X55 pixels.  However, since the control size determines our destination rectangle, we need a sort of zoom factor to apply to the wall image
                            float zoomFactor = (float)tileSize.Width / 55.0F;//Since we know we have square tiles being drawn, we can just use the width.
                            RectangleF objectRectangle = new RectangleF(0.0F, 0.0F, (float)objectImage.Width * zoomFactor, (float)objectImage.Height * zoomFactor);

                            //We need to move the rectangle so that its bottom right corner is in the bottom right corner of the tile
                            PointF wallOrigin = new Point(tileRectangle.Right, tileRectangle.Bottom);

                            //Adjust the origin based on the image size
                            wallOrigin.X -= objectRectangle.Width;
                            wallOrigin.Y -= objectRectangle.Height;
                            objectRectangle.Location = wallOrigin;

                            e.Graphics.DrawImage(objectImage, objectRectangle);
                        }
                    }
                }

                //Draw the door objects
                foreach (string door in viewableTile.DoorNames)
                {
                    using (Bitmap doorImage = Content.ContentManager.GetImage(door))
                    {
                        if (doorImage != null)
                        {
                            //TODO:Need to not depend on a known tile size
                            //The normal size of a tile is 55X55 pixels.  However, since the control size determines our destination rectangle, we need a sort of zoom factor to apply to the wall image
                            float zoomFactor = (float)tileSize.Width / 55.0F;//Since we know we have square tiles being drawn, we can just use the width.
                            RectangleF objectRectangle = new RectangleF(0.0F, 0.0F, (float)doorImage.Width * zoomFactor, (float)doorImage.Height * zoomFactor);

                            //We need to move the rectangle so that its bottom right corner is in the bottom right corner of the tile
                            PointF objectOrigin = new Point(tileRectangle.Right, tileRectangle.Bottom);

                            //Adjust the origin based on the image size
                            objectOrigin.X -= objectRectangle.Width;
                            objectOrigin.Y -= objectRectangle.Height;
                            objectRectangle.Location = objectOrigin;

                            e.Graphics.DrawImage(doorImage, objectRectangle);
                        }
                    }
                }

                //Draw NPCs
                foreach (string npcImageName in viewableTile.NonPlayableCharacterImageNames)
                {
                    using (Bitmap npcImage = Content.ContentManager.GetImage(npcImageName))
                    {
                        if (npcImage != null)
                        {
                            //TODO:Need to not depend on a known tile size
                            //The normal size of a tile is 55X55 pixels.  However, since the control size determines our destination rectangle, we need a sort of zoom factor to apply to the wall image
                            float zoomFactor = (float)tileSize.Width / 55.0F;//Since we know we have square tiles being drawn, we can just use the width.
                            RectangleF objectRectangle = new RectangleF(0.0F, 0.0F, (float)npcImage.Width * zoomFactor, (float)npcImage.Height * zoomFactor);

                            //We need to move the rectangle so that its bottom right corner is in the bottom right corner of the tile
                            PointF objectOrigin = new Point(tileRectangle.Right, tileRectangle.Bottom);

                            //Adjust the origin based on the image size
                            objectOrigin.X -= objectRectangle.Width;
                            objectOrigin.Y -= objectRectangle.Height;
                            objectRectangle.Location = objectOrigin;

                            e.Graphics.DrawImage(npcImage, objectRectangle);
                        }
                    }
                }
            }

            //Draw the player - assuming that the player is always in the center of the viewable area for now
            Rectangle characterRectangle = new Rectangle(viewableArea.Location.X + tileSize.Width * 3, viewableArea.Location.Y + tileSize.Height * 3, tileSize.Width, tileSize.Height);
            using (Bitmap playerImage = Content.ContentManager.GetImage(_characterAvatar))
            {
                e.Graphics.DrawImage(playerImage, characterRectangle);
            }

            //Draw other players
            if (this.GameStateUpdate.OtherPlayers != null)
            {
                foreach (System.Drawing.Point otherPlayerPosition in this.GameStateUpdate.OtherPlayers.PlayerPositions)
                {
                    using (Bitmap playerImage = Content.ContentManager.GetImage(_otherPlayersAvatar))
                    {
                        characterRectangle.Location = new Point(characterRectangle.Location.X + otherPlayerPosition.X * tileSize.Width, characterRectangle.Y + otherPlayerPosition.Y * tileSize.Height);
                        e.Graphics.DrawImage(playerImage, characterRectangle);
                    }
                }
            }

            //Draw zombies
            //TODO:These are not drawing correctly when the control is resized.
            foreach (Core.GameStateInformation.NpcInformation npcInTheArea in this.GameStateUpdate.NpcsInTheArea)
            {
                using (Bitmap zombieImage = Content.ContentManager.GetImage(npcInTheArea.NpcAvatarName))
                {
                    //TODO:This is working but should not be.
                    characterRectangle.Location = new Point((npcInTheArea.NpcRelativeLocation.X) * tileSize.Width, (npcInTheArea.NpcRelativeLocation.Y + 1) * tileSize.Height);
                    e.Graphics.DrawImage(zombieImage, characterRectangle);
                }
            }
        }
        #endregion

        private void PlayerView_DoubleClick(object sender, EventArgs e)
        {

        }

        private void PlayerView_Click(object sender, EventArgs e)
        {
            const int tiles = 7;            
            //get click location
            Point location = MousePosition;
            //logic to get tile location
            int x = Convert.ToInt32(((float)MousePosition.X/Size.Width)*tiles);
            int y = Convert.ToInt32(((float)MousePosition.Y/Size.Height)*tiles);
            // if click was on local tile remove feet
            if (x == 3 && y == 3)
            {
                //remove feet;
                return;
            }
            //if tile is within 1 then show/remove feet on that tile
            if ((x - 3 < 1 || x - 3 > -1) && (y - 3 < 1 || y - 3 > -1))
            {

                return;
            }

            // if tile is outside of one then check path
            var moves = buildMovesList(x, y);
            //draw feet for the moves.
            int p;
        }

        private List<Core.GameStateInformation.Directions> buildMovesList(int x, int y)
        {
            List<GameTile> map = _gameStateUpdate.ViewableArea;//for checking if location is valid. 
            var moves = new List<Core.GameStateInformation.Directions>();
            while (x != 3 && y != 3)
            {

                if (x > 3 && y > 3)
                {
                    moves.Add(Core.GameStateInformation.Directions.SouthEast);
                    x--;
                    y--;
                }
                else if (x > 3 && y < 3)
                {
                    moves.Add(Core.GameStateInformation.Directions.NorthEast);
                    x--;
                    y++;
                }
                else if (y > 3)
                {
                    moves.Add(Core.GameStateInformation.Directions.SouthWest);
                    x++;
                    y--;
                }
                else
                {
                    moves.Add(Core.GameStateInformation.Directions.NorthWest);
                    x++;
                    y--;
                }
            }
            while (x > 3)
            {
                moves.Add(Core.GameStateInformation.Directions.East);
                x--;
            }
            while (x < 3)
            {
                moves.Add(Core.GameStateInformation.Directions.West);
                x++;
            }
            while (y < 3)
            {
                moves.Add(Core.GameStateInformation.Directions.North);
                y++;
            }
            while (y > 3)
            {
                moves.Add(Core.GameStateInformation.Directions.South);
                y--;
            }
            return moves;
        }
    
    }
}
