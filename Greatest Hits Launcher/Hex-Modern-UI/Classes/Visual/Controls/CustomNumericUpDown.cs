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
    public class CustomNumericUpDown : NumericUpDown
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

        public CustomNumericUpDown() : base()
        {
            this.DoubleBuffered = true;
        }

        public void Arrows_BackColor()
        {
            this.Region = new Region(new Rectangle(2,0,this.Width - 21, this.Height));
            Controls[0].Visible = false;
        }

        private Brush DropButtonBrush = new SolidBrush(SystemColors.Control);

        private Color _ButtonColor = SystemColors.Control;

        private static int WM_PAINT = 0x000F;

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            if (m.Msg == WM_PAINT)
            {
                Graphics g = Graphics.FromHwnd(Handle);

                // Do the button first!
                //Rectangle ButtonControl = new Rectangle(this.Width - 16, 0, 16, this.Height);
                //g.FillRectangle(DropButtonBrush, ButtonControl);
                
                // Rectangles!
                Rectangle ControlBoundaries = new Rectangle(0, 0, Width, Height - 2);
 
                // Draw the border!
                ControlPaint.DrawBorder(g, ControlBoundaries, LeftBorderColour,
                    LeftWidth, LeftBorderStyle, TopBorderColour, TopWidth, TopBorderStyle, RightBorderColour,
                    RightWidth, RightBorderStyle, BottomBorderColour, BottomWidth, BottomBorderStyle);

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
