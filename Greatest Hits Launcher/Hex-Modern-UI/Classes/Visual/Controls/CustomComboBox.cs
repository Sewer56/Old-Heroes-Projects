using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;

namespace HeroesGHConfigTool
{
    class CustomComboBox : ComboBox
    {
        private Color LeftBorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
        private Color TopBorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
        private Color RightBorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));
        private Color BottomBorderColour = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(100)))), ((int)(((byte)(100)))));

        private ButtonBorderStyle LeftBorderStyle; 
        private ButtonBorderStyle RightBorderStyle; 
        private ButtonBorderStyle TopBorderStyle; 
        private ButtonBorderStyle BottomBorderStyle;

        private int LeftWidth;
        private int RightWidth;
        private int TopWidth; 
        private int BottomWidth;

        public CustomComboBox() : base()
        {
            this.DoubleBuffered = true;
        }

        private Brush ArrowBrush = new SolidBrush(SystemColors.ControlText);
        private Brush DropButtonBrush = new SolidBrush(SystemColors.Control);
        private Brush BackgroundBrush;

        private Color _ButtonColor = SystemColors.Control;
        private Color _ArrowColor = SystemColors.Control;

        private static int WM_PAINT = 0x000F;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);

                // Do the button first!
                Rectangle ButtonControl = new Rectangle(this.Width - 20, 0, 20, this.Height);
                g.FillRectangle(DropButtonBrush, ButtonControl);

                //Create the path for the border removal for builtin border.
                GraphicsPath BGPath = new GraphicsPath();
                PointF TopLeftBG = new PointF(0,0);
                PointF TopRightBG = new PointF(this.Width , 0);
                PointF BottomLeftBG = new PointF(0, this.Height);
                PointF BottomRightBG = new PointF(this.Width, this.Height);
                
                BackgroundBrush = new SolidBrush(this.BackColor);
                Pen BackgroundPen = new Pen(BackgroundBrush, 2);

                // Fill in the points!
                BGPath.AddLine(TopLeftBG, TopRightBG);
                BGPath.AddLine(TopRightBG, BottomRightBG);
                BGPath.AddLine(BottomRightBG, BottomLeftBG);
                BGPath.AddLine(BottomLeftBG, TopLeftBG);

                // Draw the BG
                g.DrawPath(BackgroundPen,BGPath);
                
                // Rectangles!
                Rectangle ControlBoundaries = new Rectangle(0, 0, Width, Height);

                //Create the path for the arrow
                GraphicsPath pth = new GraphicsPath();
                PointF TopLeft = new PointF(this.Width - 13, (this.Height - 5) / 2);
                PointF TopRight = new PointF(this.Width - 6, (this.Height - 5) / 2);
                PointF Bottom = new PointF(this.Width - 9, (this.Height + 2) / 2);
    
                // Fill in the points!
                pth.AddLine(TopLeft, TopRight);
                pth.AddLine(TopRight, Bottom);

                // Draw the border!
                ControlPaint.DrawBorder(g, ControlBoundaries, LeftBorderColour,
                    LeftWidth, LeftBorderStyle, TopBorderColour, TopWidth, TopBorderStyle, RightBorderColour,
                    RightWidth, RightBorderStyle, BottomBorderColour, BottomWidth, BottomBorderStyle);

                //Draw the arrow
                g.FillPath(ArrowBrush, pth);
                g.Dispose();
            }
        }

        public Color ButtonColor
        {
            get { return _ButtonColor; }
            set
            {
                _ButtonColor = value;
                DropButtonBrush = new SolidBrush(this.ButtonColor);
                this.Invalidate();
            }
        }

        public Color ArrowColor
        {
            get { return _ArrowColor; }
            set
            {
                _ArrowColor = value;
                ArrowBrush = new SolidBrush(this.ArrowColor);
                this.Invalidate();
            }
        }

        public Color LeftBorderColourX
        {
            get { return LeftBorderColour; }
            set
            {
                LeftBorderColour = value;
                Invalidate(); // causes control to be redrawn
            }
        }

        public Color RightBorderColourX
        {
            get { return RightBorderColour; }
            set
            {
                RightBorderColour = value;
                Invalidate(); // causes control to be redrawn
            }
        }

        public Color TopBorderColourX
        {
            get { return TopBorderColour; }
            set
            {
                TopBorderColour = value;
                Invalidate(); // causes control to be redrawn
            }
        }

        public Color BottomBorderColourX
        {
            get { return BottomBorderColour; }
            set
            {
                BottomBorderColour = value;
                Invalidate(); // causes control to be redrawn
            }
        }


        public ButtonBorderStyle LeftBorderStyleX
        {
            get { return LeftBorderStyle; }
            set
            {
                LeftBorderStyle = value;
                Invalidate();
            }
        }

        public ButtonBorderStyle TopBorderStyleX
        {
            get { return TopBorderStyle; }
            set
            {
                TopBorderStyle = value;
                Invalidate();
            }
        }

        public ButtonBorderStyle BottomBorderStyleX
        {
            get { return BottomBorderStyle; }
            set
            {
                BottomBorderStyle = value;
                Invalidate();
            }
        }

        public ButtonBorderStyle RightBorderStyleX
        {
            get { return RightBorderStyle; }
            set
            {
                RightBorderStyle = value;
                Invalidate();
            }
        }

        public int LeftBorderWidthX
        {
            get { return LeftWidth; }
            set
            {
                LeftWidth = value;
                Invalidate();
            }
        }

        public int RightBorderWidthX
        {
            get { return RightWidth; }
            set
            {
                RightWidth = value;
                Invalidate();
            }
        }

        public int TopBorderWidthX
        {
            get { return TopWidth; }
            set
            {
                TopWidth = value;
                Invalidate();
            }
        }

        public int BottomBorderWidthX
        {
            get { return BottomWidth; }
            set
            {
                BottomWidth = value;
                Invalidate();
            }
        }
    }
}
