using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    public partial class OverlayForm : Form
    {
        public OverlayForm()
        {
            InitializeComponent();
            WindowHeight = this.Height;
            WindowWidth = this.Width;
        }

        int WindowHeight;
        int WindowWidth;

        private void OverlayForm_Resize(object sender, EventArgs e)
        {
            WindowWidth = this.Width;
            WindowHeight = this.Height;
        }
    }
}
