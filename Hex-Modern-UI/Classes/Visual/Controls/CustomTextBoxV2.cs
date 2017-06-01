using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    public partial class CustomTextBoxV2 : UserControl
    {
        TextBox InternalTextBox = new TextBox();

        public enum BorderSides { Left, Right, Top, Bottom, TopBottom, All };

        public BorderSides SetBorderSide;

        int BorderSize;
        char PasswordChar;

        Color BorderColor;

        public CustomTextBoxV2()
        {
            InitializeComponent();

            InternalTextBox.Multiline = false;
            InternalTextBox.Font = this.Font;
            InternalTextBox.BackColor = this.BackColor;
            InternalTextBox.ForeColor = this.ForeColor;
            InternalTextBox.Height = this.Height;
            InternalTextBox.Width = this.Width;
            InternalTextBox.TextAlign = this.TextAlign;
            InternalTextBox.Dock = DockStyle.Fill;
            InternalTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            InternalTextBox.Location = new Point(BorderSize, BorderSize);
            InternalTextBox.PasswordChar = this.PasswordCharX;

            this.Controls.Add(InternalTextBox);
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

        public bool ReadOnlyX
        {
            get { return InternalTextBox.ReadOnly; }
            set { InternalTextBox.ReadOnly = value; }
        }

        public int BorderSizeX
        {
            get { return BorderSize; }
            set { BorderSize = value; }
        }

        public event KeyPressEventHandler KeyPress
        {
            add { InternalTextBox.KeyPress += value; }
            remove { InternalTextBox.KeyPress -= value;  }
        }

        public event KeyEventHandler KeyUp
        {
            add { InternalTextBox.KeyUp += value; }
            remove { InternalTextBox.KeyUp -= value; }
        }

        public event MouseEventHandler MouseDown
        {
            add { InternalTextBox.MouseDown += value; }
            remove { InternalTextBox.MouseDown -= value; }
        }

        public HorizontalAlignment TextAlign
        {
            get { return InternalTextBox.TextAlign; }
            set { InternalTextBox.TextAlign = value; }
        }

        [Browsable(true)]
        public char PasswordCharX
        {
            get { return PasswordChar; }
            set { PasswordChar = value; InternalTextBox.PasswordChar = value; }
        }

        public override string Text
        {
            get { return InternalTextBox.Text; }
            set { InternalTextBox.Text = value; }
        }


        public Color BackColorX
        {
            get { return InternalTextBox.BackColor; }
            set { InternalTextBox.BackColor = value;  }
        }

        public Color ForeColorX
        {
            get { return InternalTextBox.ForeColor; }
            set { InternalTextBox.ForeColor = value; }
        }

        public Font FontX
        {
            get { return InternalTextBox.Font; }
            set { InternalTextBox.Font = value; }
        }

        public override Font Font
        {
            get { return InternalTextBox.Font; }
            set { InternalTextBox.Font = value; }
        }

        public bool AutoSizeX
        {
            get { return InternalTextBox.AutoSize; }
            set { InternalTextBox.AutoSize = value; }
        }

        public string TextX
        {
            get { return InternalTextBox.Text; }
            set { InternalTextBox.Text = value; }
        }

        public void CustomTextBoxV2_Paint(object sender, PaintEventArgs e)
        {
            Pen BorderLinePen = new Pen(BorderColor, BorderSize);

            Point TopLeft = new Point(0, 0);
            Point TopRight = new Point(0 + this.Width, 0);
            Point BottomLeft = new Point(0, 0 + this.Height);
            Point BottomRight = new Point(0 + this.Width, 0 + this.Height);

            switch (SetBorderSide)
            {
                case BorderSides.All:
                    e.Graphics.DrawLine(BorderLinePen, TopLeft, TopRight);
                    e.Graphics.DrawLine(BorderLinePen, TopRight, BottomRight);
                    e.Graphics.DrawLine(BorderLinePen, BottomRight, BottomLeft);
                    e.Graphics.DrawLine(BorderLinePen, BottomLeft, TopLeft);
                    break;
                case BorderSides.Left:
                    e.Graphics.DrawLine(BorderLinePen, BottomLeft, TopLeft);
                    break;
                case BorderSides.Top:
                    e.Graphics.DrawLine(BorderLinePen, TopLeft, TopRight);
                    break;
                case BorderSides.Right:
                    e.Graphics.DrawLine(BorderLinePen, BottomRight, TopRight);
                    break;
                case BorderSides.Bottom:
                    e.Graphics.DrawLine(BorderLinePen, BottomRight, BottomLeft);
                    break;
                case BorderSides.TopBottom:
                    e.Graphics.DrawLine(BorderLinePen, TopLeft, TopRight);
                    e.Graphics.DrawLine(BorderLinePen, BottomRight, BottomLeft);
                    break;
                default:
                    break;
            }
        }
    }
}
