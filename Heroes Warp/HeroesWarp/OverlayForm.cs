using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

namespace HeroesWarp
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
            WindowHeight = this.Height;
            WindowWidth = this.Width;
        }

        Graphics MyGraphics;
        Pen MyPen = new Pen(Color.Red);
        int WindowHeight;
        int WindowWidth;
        string HookedTextMessage = "Hooked By Sewer";

        private void OverlayForm_Paint(object sender, PaintEventArgs e)
        {
            MyGraphics = e.Graphics;
            DrawHookedText(MyGraphics);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            MyGraphics = e.Graphics;
            DrawHookedText(MyGraphics);
            base.OnPaint(e);
        }

        private void DrawHookedText(Graphics MyGraphics)
        {
            Font DrawFont = new Font("Consolas", 48);
            SolidBrush DrawBrush = new SolidBrush(Color.Black);

            float FontWidth = MyGraphics.MeasureString(HookedTextMessage, DrawFont).Width;
            float FontHeight = MyGraphics.MeasureString(HookedTextMessage, DrawFont).Height;
            float FontCornerPositionWidth = WindowWidth - FontWidth;
            float FontCornerPositionHeight = WindowHeight - FontHeight;

            /* Get Center of Form */
            float FontCenterPositionWidth = (WindowWidth - FontWidth) / 2;

            MyGraphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SingleBitPerPixel;

            MyGraphics.DrawString(HookedTextMessage, DrawFont, DrawBrush, FontCenterPositionWidth, FontCornerPositionHeight);
        }

        public string HookedTextMessageX
        {
            get { return HookedTextMessage;  }
            set { HookedTextMessage = value; }
        }

        private void OverlayForm_Resize(object sender, EventArgs e)
        {
            WindowWidth = this.Width;
            WindowHeight = this.Height;
        }
    }
}
