using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Threading.Tasks;


// This is a custom control based off the default 'button' control, in order to remove the present text padding within the button.
namespace Hex_Modern_UI
{
    class CustomButton : Button
    {
        private string ownerDrawText;
        public string OwnerDrawText
        {
            get { return ownerDrawText; }
            set { ownerDrawText = value; Invalidate(); }
        }

        protected override void OnPaint(PaintEventArgs e)   // Override the OnPaint method of the button.
        {
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            base.OnPaint(e);

            StringFormat stringFormat = new StringFormat(); // Create a new StringFormat object.
            stringFormat.Alignment = StringAlignment.Center;// Set the alignment of the string to Center.
            stringFormat.LineAlignment = StringAlignment.Center;// Set the vertical line alignment of the string to Center.
            if (String.IsNullOrEmpty(Text) && !String.IsNullOrEmpty(ownerDrawText))
            {
                e.Graphics.DrawString(ownerDrawText, Font, new SolidBrush(ForeColor), ClientRectangle, stringFormat);
            }
        }
    }
}
