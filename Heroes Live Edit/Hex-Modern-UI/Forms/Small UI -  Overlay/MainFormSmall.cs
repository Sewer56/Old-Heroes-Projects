using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

using Binarysharp.MemoryManagement.Helpers;
using Binarysharp.MemoryManagement;
using SlimDX.DirectInput;
using System.Timers;
using System.Runtime.InteropServices;

namespace Hex_Modern_UI
{
    public partial class MainFormSmall : Form
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

        // Allow for dragging around of the form.
        private void MainFormSmall_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                WinAPIComponents.ReleaseCapture();
                WinAPIComponents.SendMessage(Handle, WinAPIComponents.WM_NCLBUTTONDOWN, WinAPIComponents.HT_CAPTION, 0);
                this.Invalidate();
                Program.GDIOverlay.Invalidate();
            }
        }

        private void Panel_TitleBar_MouseMove(object sender, MouseEventArgs e) { MainFormSmall_MouseMove(sender, e); }
        private void Panel_SideBar_MouseMove(object sender, MouseEventArgs e) { MainFormSmall_MouseMove(sender, e); }
        private void Toolstrip_Bottom_MouseMove(object sender, MouseEventArgs e) { MainFormSmall_MouseMove(sender, e); }
        private void TinyUI_TopLabel_PageTitle_MouseMove(object sender, MouseEventArgs e) { MainFormSmall_MouseMove(sender, e); }

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
        public ThemeMenuTinyUI ThemeMenu;
        public WelcomeScreenTinyUI WelcomeMenu;
        public OptionsScreenTinyUI OptionsMenu;
        public WarpUtilities WarpUtilitiesMenu;

        /// 
        /// Sets up swappable screens!
        /// 
        public void SetupSwappableScreens()
        {
            this.IsMdiContainer = true;

            ThemeMenu = new ThemeMenuTinyUI();
            ThemeMethods.DoThemeAssets(ThemeMenu);
            Program.OpenedForms.Add(ThemeMenu);
            ThemeMenu.MdiParent = this;

            WelcomeMenu = new WelcomeScreenTinyUI();
            ThemeMethods.DoThemeAssets(WelcomeMenu);
            Program.OpenedForms.Add(WelcomeMenu);
            WelcomeMenu.MdiParent = this;

            WarpUtilitiesMenu = new WarpUtilities();
            ThemeMethods.DoThemeAssets(WarpUtilitiesMenu);
            Program.OpenedForms.Add(WarpUtilitiesMenu);
            WarpUtilitiesMenu.MdiParent = this;

            // Warp Menu Sub Menu
            ThemeMethods.DoThemeAssets(WarpUtilitiesMenu.WarpInformationOverlay);
            Program.OpenedForms.Add(WarpUtilitiesMenu.WarpInformationOverlay);

            OptionsMenu = new OptionsScreenTinyUI();
            ThemeMethods.DoThemeAssets(OptionsMenu);
            Program.OpenedForms.Add(OptionsMenu);
            OptionsMenu.MdiParent = this;

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
                Control.MouseMove += MainFormSmall_MouseMove;
                WinAPIComponents.SetBevel(this, false);
            }
        }

        public MainFormSmall()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
            FormGraphics = this.CreateGraphics();
            InitializeSystem();
        }

        public void InitializeSystem()
        {
            Program.xSmallMainWindow = this;
            Toolstrip_Bottom.Renderer = new MyToolStrip(); // Do not draw an outline on toolstrips!
            this.Region = System.Drawing.Region.FromHrgn(WinAPIComponents.CreateRoundRectRgn(0, 0, this.Width, this.Height, 30, 30)); // Rounded edges!

            Program.xFonts.SetupMainWindow(this);            // Call SetupFonts in xFonts object.
            ButtonColour = Color.FromArgb(232, 234, 246);    // Set the button default colour to the background colour of the light control buttons.      

            /// Set local theme overrides!
            RedirectThemeValues();                           // Must preceede following.
            ThemeMethods.DoThemeAssets(this);                // Automatically theme assets!

            SetupSwappableScreens(); // If using Swappable forms!
            ThemeMethods.AutoLoadCurrentTheme(); // Automatically theme the world!
            OptionsMenu.LoadCurrentTheme(); // Load current theme!

            ShowWelcomeScreen(); // Show the welcome screen!
            DirectInputDevicesHook.PlayerControllers = DirectInputDevicesHook.GetJoySticks(); // Setup Gamepads!
            DirectInputDevicesHook.SetupGamePadTick(); // Setup Gamepad Ticks!

            SplashLoader.xSetPercentage(100);
            SplashLoader.UpdateStatusCircleComplete = true;
        }

        private void SideBtn_Themes_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Theming Menu";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = ThemeMenu;
            FinishSwappableFormSetup();
        }

        private void ShowWelcomeScreen()
        {
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = WelcomeMenu;
            FinishSwappableFormSetup();
        }

        private void SideBtn_Options_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Options Menu";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = OptionsMenu;
            FinishSwappableFormSetup();
        }

        private void SideBtn_Warping_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Warp Settings Menu";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = WarpUtilitiesMenu;
            FinishSwappableFormSetup();
        }

        public void ChangeActionBarLabelText(string Message)
        {
            lbl_ActionBar_BottomRight.Text = Message;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Application data processing | non UI code goes here
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public GameHook SonicHeroesHook = new GameHook();
        public DirectInputDevices DirectInputDevicesHook = new DirectInputDevices();

        public void VerifyStockSettings()
        {

        }

        public void SetupAutoHook()
        {

        }


    }
}
