using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    public partial class MainForm : Form
    {
        // Kill the flickering of dark control overdraws.
        protected override CreateParams CreateParams
        {
            get
            { CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;  // Turn on WS_EX_COMPOSITED
                return cp;
            }
        }

        /// This allows for the window to be moved without having a real border!
        private void Panel_TitleBar_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WinAPIComponents.ReleaseCapture();
                WinAPIComponents.SendMessage(Handle, WinAPIComponents.WM_NCLBUTTONDOWN, WinAPIComponents.HT_CAPTION, 0);
            }
        }

        // Top panel stuff.
        private void TopBtn_Close_Click(object sender, EventArgs e) { Application.Exit(); }
        private void TopBtn_Minimize_Click(object sender, EventArgs e) { Application.Exit(); }
        private void TopLabel_PageTitle_MouseDown(object sender, MouseEventArgs e) { Panel_TitleBar_MouseMove(sender, e); }

        ///
        /// Set theming to this form!
        ///
        public void RedirectThemeValues()
        {
            Program.ToolstripThemeControlLabel = lbl_ActionBar_BottomRight;
            Program.SideBarPanel = Panel_SideBar;
            Program.TopBarPanel = Panel_TitleBar;
            Program.BottomToolstrip = Toolstrip_Bottom;
            Program.OpenedForms.Add(this);
        }

        Colours xColours = new Colours();                                                                           // Create an object to store the colours.
        Color ButtonColour;                                                                                         // Create a variable storing the default button colour.
        Graphics FormGraphics;                                                                                      // Lets draw a few things :)
        ControlsSampleMenu ControlsSampleMenu; /// Must declare swappable screen and update SetupSwappableScreens() for new screens.
        ThemeSampleMenu ThemeSampleMenu; /// Must declare swappable screen and update SetupSwappableScreens() for new screens.

        /// 
        /// Sets up swappable screens!
        /// 
        public void SetupSwappableScreens()
        {
            this.IsMdiContainer = true;

            ControlsSampleMenu = new ControlsSampleMenu();
            ThemeMethods.DoThemeAssets(ControlsSampleMenu);
            Program.OpenedForms.Add(ControlsSampleMenu);
            ControlsSampleMenu.MdiParent = this;

            ThemeSampleMenu = new ThemeSampleMenu();
            ThemeMethods.DoThemeAssets(ThemeSampleMenu);
            Program.OpenedForms.Add(ThemeSampleMenu);
            ThemeSampleMenu.MdiParent = this;

            ThemeMDIClients();
        }

        private void SetupNewSwappableForm() // Setup for new form to be displayed.
        {
            try { Program.CurrentlyOpenedForm.Hide(); } catch { }
            // Will always throw on first running.
        }

        private void FinishSwappableFormSetup()
        {
            Program.CurrentlyOpenedForm.Visible = true;
            Program.CurrentlyOpenedForm.Location = new Point(0, 0);
            Program.CurrentlyOpenedForm.Dock = DockStyle.Fill;
        }

        private void ThemeMDIClients()
        {
            foreach (MdiClient Control in this.Controls.OfType<MdiClient>())
            {
                // Set the BackColor of the MdiClient control.
                Control.BackColor = this.BackColor;
                WinAPIComponents.SetBevel(this, false);
            }
        }

        // Define some styling elements!
        public MainForm() {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
            FormGraphics = this.CreateGraphics();
            InitializeSystem();
        }

        public void InitializeSystem()
        {
            Toolstrip_Bottom.Renderer = new MyToolStrip(); // Do not draw an outline on toolstrips!
            this.Region = System.Drawing.Region.FromHrgn(WinAPIComponents.CreateRoundRectRgn(0, 0, this.Width, this.Height, 30, 30)); // Rounded edges!

            Program.xFonts.SetupMainWindow(this);            // Call SetupFonts in xFonts object.
            ButtonColour = Color.FromArgb(232, 234, 246);    // Set the button default colour to the background colour of the light control buttons.      

            /// Set local theme overrides!
            RedirectThemeValues();                           // Must preceede following.
            ThemeMethods.DoThemeAssets(this);                // Automatically theme assets!

            SetupSwappableScreens(); // If using Swappable forms!
            ThemeMethods.AutoLoadCurrentTheme(); // Automatically theme the world!

            SplashLoader.xSetPercentage(100);
            SplashLoader.UpdateStatusCircleComplete = true;
        }

        private void SideBtn_Welcome_Click(object sender, EventArgs e)
        {
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = ControlsSampleMenu;
            FinishSwappableFormSetup();
        }

        private void SideBtn_ThemeMenu_Click(object sender, EventArgs e)
        {
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = ThemeSampleMenu;
            FinishSwappableFormSetup();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Application code goes here
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    }
}
