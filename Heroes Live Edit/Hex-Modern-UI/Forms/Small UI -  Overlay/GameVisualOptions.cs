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
    public partial class GameVisualOptions : Form
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

        public GameVisualOptions()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }


    }
}
