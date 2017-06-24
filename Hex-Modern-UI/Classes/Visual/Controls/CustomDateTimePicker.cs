using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;


namespace HeroesGHConfigTool.Classes.Visual
{
    public partial class CustomDateTimePicker : System.Windows.Forms.DateTimePicker
    {
        private Color FontColour;
        private bool IsDateTimePickerDown;
        public enum BorderSides { Left, Right, Top, Bottom, TopBottom, All };
        public BorderSides SetBorderSide;
        Color BorderColor;
        int BorderSize;

        public CustomDateTimePicker() : base()
        {
            this.SetStyle(ControlStyles.UserPaint, true); // Allow the user to colour the DateTimePicker#
            this.MouseMove += CustomDateTimePicker_MouseMove;
            this.DropDown += CustomDateTimePicker_DropDown;
            this.CloseUp += CustomDateTimePicker_CloseUp;
        }

        private void CustomDateTimePicker_CloseUp(object sender, EventArgs e)
        {
            IsDateTimePickerDown = false;
        }

        private void CustomDateTimePicker_DropDown(object sender, EventArgs e)
        {
            IsDateTimePickerDown = true;
        }

        private void CustomDateTimePicker_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsDateTimePickerDown == false)
            {
                this.Select();
                SendKeys.Send("%{DOWN}");
            }
        }

        ///     Gets or sets the background color of the control, browsable for designer support.
        [Browsable(true)]
        public override Color BackColor
        {
            get { return base.BackColor; }
            set { base.BackColor = value; }
        }


        // Expose the internal font colour
        [Browsable(true)]
        public Color FontColourX
        {
            get { return FontColour; }
            set { FontColour = value; }
        }

        public int BorderSizeX
        {
            get { return BorderSize; }
            set { BorderSize = value; }
        }

        public Color BorderColour
        {
            get { return BorderColor; }
            set { BorderColor = value; }
        }

        public BorderSides BorderSidesX
        {
            get { return SetBorderSide; }
            set { SetBorderSide = value; }
        }

        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            Graphics g = this.CreateGraphics();
            // When the control is enabled the brush is set to Backcolor, 
            // otherwise to colour stored in BackgroundDisabledColour.

            Brush BackgroundBrush = new SolidBrush(this.BackColor);
            Brush TextBrush = new SolidBrush(this.FontColour);

            // Filling the background
            g.FillRectangle(BackgroundBrush, 0, 0, ClientRectangle.Width, ClientRectangle.Height);

            // Draw Border!
            Pen BorderLinePen = new Pen(BorderColor, BorderSize);
            
            Point TopLeft = new Point(0, 0);
            Point TopRight = new Point(this.Width, 0);
            Point BottomLeft = new Point(0, this.Height - 6);
            Point BottomRight = new Point(this.Width, this.Height - 6);

            switch (SetBorderSide)
            {
                case BorderSides.All:
                    g.DrawLine(BorderLinePen, TopLeft, TopRight);
                    g.DrawLine(BorderLinePen, TopRight, BottomRight);
                    g.DrawLine(BorderLinePen, BottomRight, BottomLeft);
                    g.DrawLine(BorderLinePen, BottomLeft, TopLeft);
                    break;
                case BorderSides.Left:
                    g.DrawLine(BorderLinePen, BottomLeft, TopLeft);
                    break;
                case BorderSides.Top:
                    g.DrawLine(BorderLinePen, TopLeft, TopRight);
                    break;
                case BorderSides.Right:
                    g.DrawLine(BorderLinePen, BottomRight, TopRight);
                    break;
                case BorderSides.Bottom:
                    g.DrawLine(BorderLinePen, BottomRight, BottomLeft);
                    break;
                case BorderSides.TopBottom:
                    g.DrawLine(BorderLinePen, TopLeft, TopRight);
                    g.DrawLine(BorderLinePen, BottomRight, BottomLeft);
                    break;
                default:
                    break;
            }

            // Drawing the datetime text
            g.DrawString(this.Text, this.Font, TextBrush, 0, 2);
            g.Dispose();
            BackgroundBrush.Dispose();
            TextBrush.Dispose();
            BorderLinePen.Dispose();
        }
    }
}
