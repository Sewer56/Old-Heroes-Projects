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

namespace Hex_Modern_UI
{
    public partial class ThemeSampleMenu : Form
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

        public ThemeSampleMenu()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            ThemeMethods.SaveCurrentTheme();
            MessageBox.Show("Save successful!");
        }

        private void Btn_Load_Click(object sender, EventArgs e)
        {
            ThemeMethods.LoadCurrentTheme();
            MessageBox.Show("Load successful!");
        }
    }
}
