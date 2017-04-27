using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MapEditor.Controls
{
    public partial class MapViewer : ScrollableControl
    {
        #region Events
        public event EventHandler SelectedTileChanged;
        protected virtual void OnSelectedTileChanged()
        {
            EventHandler temp = SelectedTileChanged;
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }
        #endregion

        #region Declarations
        //Holds an image of the map to be painted in the viewer so that we only have to re-render the map when it changes and not on resize or zoom.
        private Bitmap _mapImage = null;
        #endregion

        #region Properties
        private LegendsOfKesmaiSurvival.Services.Business.Maps.Map _map;
        public LegendsOfKesmaiSurvival.Services.Business.Maps.Map Map
        {
            get { return _map; }
            set
            {
                if (_map != null)
                {
                    _map.MapChanged -= new EventHandler(_map_MapChanged);
                }
                _map = value;
                _mapImage = _map == null ? null : _map.RenderToImage();
                if (_map != null)
                {
                    _map.MapChanged += new EventHandler(_map_MapChanged);
                }
                if (_mapImage != null)
                {
                    this.AutoScrollMinSize = new Size(GetScaledImageWidth(), GetScaledImageHeight());
                }
                else
                {
                    this.AutoScrollMinSize = new Size(0, 0);
                }
                this.Invalidate();
            }
        }

        private int _zoomLevel = 100;
        /// <summary>
        /// Gets or sets the zoom level of the control as a percentage.
        /// 100% shows the map as it would appear in game.
        /// Less than 100% zooms out to show more of the map.
        /// Greater than 100% zooms in to enlarge tile detail.
        /// </summary>
        public int ZoomLevel
        {
            get { return _zoomLevel; }
            set
            {
                if (_zoomLevel != value)
                {
                    _zoomLevel = value;
                    this.AutoScrollMinSize = new Size(GetScaledImageWidth(), GetScaledImageHeight());
                    this.Invalidate();
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize
        {
            get
            {
                return base.AutoScrollMinSize;
            }
            set
            {
                base.AutoScrollMinSize = value;
            }
        }

        private LegendsOfKesmaiSurvival.Services.Business.Maps.MapTile _selectedTile = null;
        public LegendsOfKesmaiSurvival.Services.Business.Maps.MapTile SelectedTile
        {
            get
            {
                return _selectedTile;
            }
            set
            {
                if (_selectedTile != value)
                {
                    _selectedTile = value;
                    this.Invalidate();
                    OnSelectedTileChanged();
                }
            }
        }
        #endregion

        #region Constructors
        public MapViewer()
        {
            InitializeComponent();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint |
                ControlStyles.UserPaint |
                ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.ResizeRedraw, true);
            this.UpdateStyles();
        }
        #endregion

        #region Private Methods
        void _map_MapChanged(object sender, EventArgs e)
        {
            _mapImage = _map.RenderToImage();
            this.Invalidate();
        }

        private int GetScaledImageHeight()
        {
            return _mapImage != null ? (int)(_mapImage.Size.Height * GetZoomFactor()) : 0;
        }

        private int GetScaledImageWidth()
        {
            return _mapImage != null ? (int)(_mapImage.Size.Width * GetZoomFactor()) : 0;
        }

        private double GetZoomFactor()
        {
             return (double)this.ZoomLevel / 100;
        }

        private Rectangle GetImageViewPort()
        {
            if (_mapImage != null)
            {
                return this.ClientRectangle;
            }
            else
                return Rectangle.Empty; 
        }

        private Rectangle GetSourceImageRegion()
        {
            int sourceLeft;
            int sourceTop;
            int sourceWidth;
            int sourceHeight;
            Rectangle viewPort;
            Rectangle region;

            if (_mapImage != null)
            {
                viewPort = this.GetImageViewPort();
                sourceLeft = (int)(-this.AutoScrollPosition.X / GetZoomFactor());
                sourceTop = (int)(-this.AutoScrollPosition.Y / GetZoomFactor());
                sourceWidth = (int)(viewPort.Width / GetZoomFactor());
                sourceHeight = (int)(viewPort.Height / GetZoomFactor());
                region = new Rectangle(sourceLeft, sourceTop, sourceWidth, sourceHeight);
            }
            else
                region = Rectangle.Empty; 
            
            return region;
        }

        private Rectangle GetSelectedTileRectangle()
        {
            Rectangle rectangle = Rectangle.Empty;

            if (_selectedTile != null)
            {

            }

            return rectangle;
        }

        private LegendsOfKesmaiSurvival.Services.Business.Maps.MapTile GetTileFromPoint(System.Drawing.Point point)
        {
            if (_map == null) return null;

            //Convert the point on the control to a point on the map's image taking into consideration the current zoom level and scroll position.
            int x = (int)(-(this.AutoScrollPosition.X - point.X) / GetZoomFactor());
            int y = (int)(-(this.AutoScrollPosition.Y - point.Y) / GetZoomFactor());

            //Search for a tile that has a rectangle that contains the point.
            LegendsOfKesmaiSurvival.Services.Business.Maps.MapTile tile = _map.Tiles.Find(delegate(LegendsOfKesmaiSurvival.Services.Business.Maps.MapTile search)
            {
                Rectangle tileRectangle = new Rectangle(new Point(search.Location.X * _map.TileWidth, search.Location.Y * _map.TileHeight), _map.TileSize);
                return tileRectangle.Contains(new Point(x, y));
            });

            return tile;
        }
        #endregion

        #region Override Methods
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_mapImage == null)
            {
                base.OnPaint(e);
                return;
            }

            try
            {
                //Draw the background
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                {
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
                }

                //Draw the map
                e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Default;
                e.Graphics.DrawImage(_mapImage, this.GetImageViewPort(), this.GetSourceImageRegion(), GraphicsUnit.Pixel);

                //Draw a rectangle around the selected tile
                if (_selectedTile != null)
                {
                    Rectangle tileRectangle = new Rectangle(new Point(_selectedTile.Location.X * _map.TileWidth, _selectedTile.Location.Y * _map.TileHeight), _map.TileSize);
                    int x = (int)(tileRectangle.X * GetZoomFactor()) + this.AutoScrollPosition.X;
                    int y = (int)(tileRectangle.Y * GetZoomFactor()) + this.AutoScrollPosition.Y;
                    int width = (int)(_map.TileWidth * GetZoomFactor());
                    int height = (int)(_map.TileHeight * GetZoomFactor());
                    Rectangle selectionRectangle = new Rectangle(x, y, width, height);
                    selectionRectangle.Inflate(3,3);

                    e.Graphics.DrawRectangle(new Pen(Color.Yellow, 3), selectionRectangle);
                }
            }
            catch (Exception ex)
            {
                e.Graphics.DrawString(ex.ToString(), this.Font, Brushes.Black, this.ClientRectangle);
            }
        }

        protected override void OnScroll(ScrollEventArgs se)
        {
            this.Invalidate();

            base.OnScroll(se);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            this.SelectedTile = GetTileFromPoint(e.Location);
        }
        #endregion

        #region Public Methods
        public void RedrawMap()
        {
            _map_MapChanged(this, EventArgs.Empty);
        }
        #endregion
    }
}
