﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace NewuCommon
{
    public partial class ScrollingText : Control
    {
        private Timer timer;                             // Timer for text animation.
        private string text = "Text";                    // Scrolling text
        private float staticTextPos = 0;                 // The running x pos of the text
        private float yPos = 0;                          // The running y pos of the text
        private ScrollDirection scrollDirection = ScrollDirection.RightToLeft;                // The direction the text will scroll
        private ScrollDirection currentDirection = ScrollDirection.LeftToRight;                // Used for text bouncing
        private VerticleTextPosition verticleTextPosition = VerticleTextPosition.Center;    // Where will the text be vertically placed
        private int scrollPixelDistance = 2;             // How far the text scrolls per timer event
        private bool showBorder = true;                  // Show a border or not
        private bool stopScrollOnMouseOver = false;      // Flag to stop the scroll if the user mouses over the text
        private bool scrollOn = true;                    // Internal flag to stop / start the scrolling of the text
        private Brush foregroundBrush = null;            // Allow the user to set a custom Brush to the text Font
        private Brush backgroundBrush = null;            // Allow the user to set a custom Brush to the background
        private Color borderColor = Color.Black;         // Allow the user to set the color of the control border
        private RectangleF lastKnownRect;                // The last known position of the text

        private DateTime dt;
        private Color tempStringColor;

        public ScrollingText()
        {
            InitializeComponent();
            Version v = Environment.Version;

            if (v.Major < 2)
            {
                this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            }

            {
                this.SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            }

            this.SetStyle(ControlStyles.ResizeRedraw, true);

            //setup the timer object
            timer = new Timer();
            timer.Interval = 25;    //default timer interval
            timer.Enabled = true;
            timer.Tick += new EventHandler(Tick);
            dt = DateTime.Now;
            tempStringColor = this.ForeColor;
        }

        private void Tick(object sender, EventArgs e)
        {
            //update rectangle to include where to paint for new position
            //lastKnownRect.X -= 10;
            //lastKnownRect.Width += 20;
            lastKnownRect.Inflate(10, 5);

            //get the display rectangle
            RectangleF refreshRect = lastKnownRect;
            refreshRect.X = Math.Max(0, lastKnownRect.X);
            refreshRect.Width = Math.Min(lastKnownRect.Width + lastKnownRect.X, this.Width);
            refreshRect.Width = Math.Min(this.Width - lastKnownRect.X, refreshRect.Width);

            //create region based on updated rectangle
            //Region updateRegion = new Region(lastKnownRect);
            Region updateRegion = new Region(refreshRect);

            //repaint the control
            Invalidate(updateRegion);
            Update();
        }

        //Paint the ScrollingTextCtrl.
        protected override void OnPaint(PaintEventArgs pe)
        {
            //Paint the text to its new position
            DrawScrollingText(pe.Graphics);

            //pass on the graphics obj to the base Control class
            base.OnPaint(pe);
        }

        //Draw the scrolling text on the control
        public void DrawScrollingText(Graphics canvas)
        {
            canvas.SmoothingMode = SmoothingMode.HighQuality;
            canvas.PixelOffsetMode = PixelOffsetMode.HighQuality;

            //measure the size of the string for placement calculation
            SizeF stringSize = canvas.MeasureString(this.text, this.Font);

            //Calculate the begining x position of where to paint the text
            if (scrollOn)
            {
                CalcTextPosition(stringSize);
            }

            //Clear the control with user set BackColor
            if (backgroundBrush != null)
            {
                canvas.FillRectangle(backgroundBrush, 0, 0, this.ClientSize.Width, this.ClientSize.Height);
            }
            else
            {
                canvas.Clear(this.BackColor);
            }

            // Draw the border
            if (showBorder)
            {
                using (Pen borderPen = new Pen(borderColor))
                {
                    canvas.DrawRectangle(borderPen, 0, 0, this.ClientSize.Width - 1, this.ClientSize.Height - 1);
                }
            }

            if (IsSparkable)
            {
                int jiange = (int)(DateTime.Now - dt).TotalSeconds;

                if (jiange >= SparkleSeconds)
                {
                    dt = DateTime.Now;
                    if (tempStringColor != this.ForeColor)
                    {
                        tempStringColor = this.ForeColor;
                    }
                    else
                    {
                        tempStringColor = this.GlitterColor;
                    }
                }
                using (Brush tempForeBrush = new SolidBrush(tempStringColor))
                {
                    canvas.DrawString(this.text, this.Font, tempForeBrush, staticTextPos, yPos);
                }
            }
            else
            {
                // Draw the text string in the bitmap in memory
                if (foregroundBrush == null)
                {
                    using (Brush tempForeBrush = new SolidBrush(this.ForeColor))
                    {
                        canvas.DrawString(this.text, this.Font, tempForeBrush, staticTextPos, yPos);
                    }
                }
                else
                {
                    canvas.DrawString(this.text, this.Font, foregroundBrush, staticTextPos, yPos);
                }
            }

            lastKnownRect = new RectangleF(staticTextPos, yPos, stringSize.Width, stringSize.Height);
            EnableTextLink(lastKnownRect);
        }

        private void CalcTextPosition(SizeF stringSize)
        {
            switch (scrollDirection)
            {
                case ScrollDirection.RightToLeft:
                    if (staticTextPos < (-1 * (stringSize.Width)))
                        staticTextPos = this.ClientSize.Width - 1;
                    else
                        staticTextPos -= scrollPixelDistance;
                    break;

                case ScrollDirection.LeftToRight:
                    if (staticTextPos > this.ClientSize.Width)
                        staticTextPos = -1 * stringSize.Width;
                    else
                        staticTextPos += scrollPixelDistance;
                    break;

                case ScrollDirection.Bouncing:
                    if (currentDirection == ScrollDirection.RightToLeft)
                    {
                        if (staticTextPos < 0)
                            currentDirection = ScrollDirection.LeftToRight;
                        else
                            staticTextPos -= scrollPixelDistance;
                    }
                    else if (currentDirection == ScrollDirection.LeftToRight)
                    {
                        if (staticTextPos > this.ClientSize.Width - stringSize.Width)
                            currentDirection = ScrollDirection.RightToLeft;
                        else
                            staticTextPos += scrollPixelDistance;
                    }
                    break;
            }

            //Calculate the vertical position for the scrolling text
            switch (verticleTextPosition)
            {
                case VerticleTextPosition.Top:
                    yPos = 2;
                    break;

                case VerticleTextPosition.Center:
                    //yPos = (this.ClientSize.Height / 2) - (stringSize.Height / 2) + 3;
                    yPos = (this.ClientSize.Height / 2) - (stringSize.Height / 2) + YPosOffset;
                    break;

                case VerticleTextPosition.Botom:
                    yPos = this.ClientSize.Height - stringSize.Height;
                    break;
            }
        }

        #region Mouse over, text link logic

        private void EnableTextLink(RectangleF textRect)
        {
            Point curPt = this.PointToClient(Cursor.Position);

            //if (curPt.X > textRect.Left && curPt.X < textRect.Right
            //    && curPt.Y > textRect.Top && curPt.Y < textRect.Bottom)
            if (textRect.Contains(curPt))
            {
                //Stop the text of the user mouse's over the text
                if (stopScrollOnMouseOver)
                    scrollOn = false;

                this.Cursor = Cursors.Hand;
            }
            else
            {
                //Make sure the text is scrolling if user's mouse is not over the text
                scrollOn = true;

                this.Cursor = Cursors.Default;
            }
        }

        private void ScrollingText_Click(object sender, System.EventArgs e)
        {
            //Trigger the text clicked event if the user clicks while the mouse
            //is over the text.  This allows the text to act like a hyperlink
            if (this.Cursor == Cursors.Hand)
                OnTextClicked(this, new EventArgs());
        }

        public delegate void TextClickEventHandler(object sender, EventArgs args);

        public event TextClickEventHandler TextClicked;

        private void OnTextClicked(object sender, EventArgs args)
        {
            //Call the delegate
            if (TextClicked != null)
                TextClicked(sender, args);
        }

        #endregion Mouse over, text link logic

        #region Properties

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The timer interval that determines how often the control is repainted")
        ]
        public int TextScrollSpeed
        {
            set
            {
                timer.Interval = value;
            }
            get
            {
                return timer.Interval;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("How many pixels will the text be moved per Paint")
        ]
        public int TextScrollDistance
        {
            set
            {
                scrollPixelDistance = value;
            }
            get
            {
                return scrollPixelDistance;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The text that will scroll accros the control")
        ]
        public string ScrollText
        {
            set
            {
                text = value;
                this.Invalidate();
                this.Update();
            }
            get
            {
                return text;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("What direction the text will scroll: Left to Right, Right to Left, or Bouncing")
        ]
        public ScrollDirection ScrollDirection
        {
            set
            {
                scrollDirection = value;
            }
            get
            {
                return scrollDirection;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The verticle alignment of the text")
        ]
        public VerticleTextPosition VerticleTextPosition
        {
            set
            {
                verticleTextPosition = value;
            }
            get
            {
                return verticleTextPosition;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("Turns the border on or off")
        ]
        public bool ShowBorder
        {
            set
            {
                showBorder = value;
            }
            get
            {
                return showBorder;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("The color of the border")
        ]
        public Color BorderColor
        {
            set
            {
                borderColor = value;
            }
            get
            {
                return borderColor;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Scrolling Text"),
        Description("Determines if the text will stop scrolling if the user's mouse moves over the text")
        ]
        public bool StopScrollOnMouseOver
        {
            set
            {
                stopScrollOnMouseOver = value;
            }
            get
            {
                return stopScrollOnMouseOver;
            }
        }

        [
        Browsable(true),
        CategoryAttribute("Behavior"),
        Description("Indicates whether the control is enabled")
        ]
        public new bool Enabled
        {
            set
            {
                timer.Enabled = value;
                base.Enabled = value;
            }

            get
            {
                return base.Enabled;
            }
        }

        [
        Browsable(false)
        ]
        public Brush ForegroundBrush
        {
            set
            {
                foregroundBrush = value;
            }
            get
            {
                return foregroundBrush;
            }
        }

        [
        ReadOnly(true)
        ]
        public Brush BackgroundBrush
        {
            set
            {
                backgroundBrush = value;
            }
            get
            {
                return backgroundBrush;
            }
        }

        /// <summary>
        /// 滚动文字y轴坐标偏移量
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Scrolling Text"),
            Description("滚动文字y轴坐标偏移量,用来调整位置用的")
        ]
        public int YPosOffset { get; set; }

        /// <summary>
        /// 是否闪烁
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Scrolling Text"),
            Description("闪烁功能启用时替换色")
        ]
        public bool IsSparkable { get; set; }

        /// <summary>
        /// 闪烁颜色
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Scrolling Text"),
            Description("闪烁功能启用时替换色")
        ]
        public Color GlitterColor { get; set; }

        /// <summary>
        ///  闪烁间隔时间
        /// </summary>
        [
            Browsable(true),
            CategoryAttribute("Scrolling Text"),
            Description("闪烁间隔时间")
        ]
        public int SparkleSeconds { get; set; }

        #endregion Properties
    }

    public enum ScrollDirection
    {
        RightToLeft,
        LeftToRight,
        Bouncing
    }

    public enum VerticleTextPosition
    {
        Top,
        Center,
        Botom
    }
}