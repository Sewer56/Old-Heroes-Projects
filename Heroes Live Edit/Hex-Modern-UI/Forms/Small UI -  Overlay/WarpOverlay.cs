using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace Hex_Modern_UI
{
    public partial class WarpOverlay : Form
    {
        // Kill the flickering of dark control overdraws.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        public WarpOverlay()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }

        private void WarpOverlay_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WinAPIComponents.ReleaseCapture();
                WinAPIComponents.SendMessage(Handle, WinAPIComponents.WM_NCLBUTTONDOWN, WinAPIComponents.HT_CAPTION, 0);
                this.Invalidate();
                Program.GDIOverlay.Invalidate();
            }
        }

        private void WarpOverlay_Deactivate(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.WarpUtilitiesMenu.CurrentWarps[Program.xSmallMainWindow.WarpUtilitiesMenu.MenuWarpIndex].CharacterWarpName = TxtBoxSmall_WarpName.Text;
        }
    }
}
