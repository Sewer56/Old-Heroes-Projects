using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace HeroesGHConfigTool
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

        Color ButtonColour;                                                                                         // Create a variable storing the default button colour.
        Graphics FormGraphics;                                                                                      // Lets draw a few things :)
        public ThemeMenuTinyUI ThemeMenu;
        public AboutScreen AboutMenu;
        public MainScreen MainMenu;
        public ControllerScreen ControllerScreenOne;
        public ControllerScreenTwo ControllerScreenTwo;
        public TweaksScreen TweaksScreen;
        public TweaksScreen2 TweaksIIScreen;

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

            MainMenu = new MainScreen();
            ThemeMethods.DoThemeAssets(MainMenu);
            Program.OpenedForms.Add(MainMenu);
            MainMenu.MdiParent = this;

            ControllerScreenOne = new ControllerScreen();
            ThemeMethods.DoThemeAssets(ControllerScreenOne);
            Program.OpenedForms.Add(ControllerScreenOne);
            ControllerScreenOne.MdiParent = this;

            TweaksScreen = new TweaksScreen();
            ThemeMethods.DoThemeAssets(TweaksScreen);
            Program.OpenedForms.Add(TweaksScreen);
            TweaksScreen.MdiParent = this;

            TweaksIIScreen = new TweaksScreen2();
            ThemeMethods.DoThemeAssets(TweaksIIScreen);
            Program.OpenedForms.Add(TweaksIIScreen);
            TweaksIIScreen.MdiParent = this;

            ControllerScreenTwo = new ControllerScreenTwo();
            ThemeMethods.DoThemeAssets(ControllerScreenTwo);
            Program.OpenedForms.Add(ControllerScreenTwo);
            ControllerScreenTwo.MdiParent = this;

            AboutMenu = new AboutScreen();
            ThemeMethods.DoThemeAssets(AboutMenu);
            Program.OpenedForms.Add(AboutMenu);
            AboutMenu.MdiParent = this;

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
            SplashLoader.xSetPercentage(10);
            SplashLoader.xSetMessage("Loading 1/6...");

            Program.xSmallMainWindow = this;
            SplashLoader.xSetPercentage(15);
            Toolstrip_Bottom.Renderer = new MyToolStrip(); // Do not draw an outline on toolstrips!
            SplashLoader.xSetPercentage(20);
            this.Region = System.Drawing.Region.FromHrgn(WinAPIComponents.CreateRoundRectRgn(0, 0, this.Width, this.Height, 30, 30)); // Rounded edges!

            SplashLoader.xSetPercentage(30);
            SplashLoader.xSetMessage("Loading 2/6...");

            Program.xFonts.SetupMainWindow(this);            // Call SetupFonts in xFonts object.
            ButtonColour = Color.FromArgb(232, 234, 246);    // Set the button default colour to the background colour of the light control buttons.      

            SplashLoader.xSetPercentage(40);
            SplashLoader.xSetMessage("Loading 3/6...");

            /// Set local theme overrides!
            RedirectThemeValues();                           // Must preceede following.
            ThemeMethods.DoThemeAssets(this);                // Automatically theme assets!

            SplashLoader.xSetPercentage(50);
            SplashLoader.xSetMessage("Loading 4/6...");

            SetupSwappableScreens(); // If using Swappable forms!
            ThemeMethods.AutoLoadCurrentTheme(); // Automatically theme the world!

            //ShowWelcomeScreen(); // Show the welcome screen!
            ShowOptionsMenu(); // Show the default options menu.

            DirectInputDevicesHook.PlayerControllers = DirectInputDevicesHook.GetJoySticks(); // Setup Gamepads!
            DirectInputDevicesHook.SetupGamePadTick(); // Setup Gamepad Ticks!

            SplashLoader.xSetPercentage(70);
            SplashLoader.xSetMessage("Loading 5/6...");
            LoadHeroesSettings();

            SplashLoader.xSetMessage("Loading 6/6...");
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

        private void SideBtn_Tweaks_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Options Menu";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = TweaksScreen;
            FinishSwappableFormSetup();
        }

        private void SideBtn_TweaksII_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Options #2";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = TweaksIIScreen;
            FinishSwappableFormSetup();
        }

        private void SideBtn_ControllerOne_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Controller #1 Buttons!";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = ControllerScreenOne;
            FinishSwappableFormSetup();
        }

        private void SideBtn_ControllerTwo_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Controller #2 Buttons!";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = ControllerScreenTwo;
            FinishSwappableFormSetup();
        }

        private void SideBtn_About_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "About Launcher";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = AboutMenu;
            FinishSwappableFormSetup();
        }


        private void SideBtn_MainScreen_Click(object sender, EventArgs e)
        {
            TinyUI_TopLabel_PageTitle.Text = "Main Menu";
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = MainMenu;
            FinishSwappableFormSetup();
        }

        private void ShowWelcomeScreen()
        {
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = AboutMenu;
            FinishSwappableFormSetup();
        }

        private void ShowOptionsMenu()
        {
            SetupNewSwappableForm();
            Program.CurrentlyOpenedForm = MainMenu;
            FinishSwappableFormSetup();
        }

        public void ChangeActionBarLabelText(string Message)
        {
            lbl_ActionBar_BottomRight.Text = Message;
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        // Application data processing | non UI code goes here
        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

        public byte[] SonicHeroesExecutable;
        public SonicHeroesConfigFile ConfigFile;
        public DirectInputDevices DirectInputDevicesHook = new DirectInputDevices();

        public struct SonicHeroesConfigFile
        {
            public byte FrameRate;
            public byte FreeCamera;
            public byte FogEmulation;
            public byte ClipRange;
            public byte AnisotropicFiltering;
            public byte[] ControllerOne;
            public byte[] ControllerTwo;
            public byte[] MouseControls;
            public byte Screensize;
            public byte FullScreen;
            public byte Language;
            public byte SurroundSound;
            public byte SFXVolume;
            public byte SFXToggle;
            public byte BGMVolume;
            public byte BGMToggle;
            public byte SoftShadows;
            public byte MouseControlType;
        }

        private void LoadHeroesSettings()
        {
            // Initialize Executable array of correct size!
            SonicHeroesExecutable = new byte[ new FileInfo("Tsonic_win.exe").Length ];
            SonicHeroesExecutable = File.ReadAllBytes("Tsonic_win.exe");
            LoadHeroesConfig();
            lbl_ActionBar_BottomRight.Text = "Executable Loaded!";
        }

        private void LoadHeroesConfig()
        {
            string ConfigFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SEGA\SONICHEROES\sonic_h.ini";
            string CurrentLine;
            System.IO.StreamReader INIConfigFile = new System.IO.StreamReader(ConfigFilePath);

            // Initialize size of struct arrays;
            ConfigFile.ControllerOne = new byte[8];
            ConfigFile.ControllerTwo = new byte[8];
            ConfigFile.MouseControls = new byte[8];

            while ((CurrentLine = INIConfigFile.ReadLine()) != null)
            {
                if (CurrentLine.StartsWith("Frame_Rate")) { ConfigFile.FrameRate = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Free_Camera")) { ConfigFile.FreeCamera = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Fog")) { ConfigFile.FogEmulation = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Clip_Range")) { ConfigFile.ClipRange = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Anisotoropic")) { ConfigFile.AnisotropicFiltering = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Pad_Assign1")) { ReadStringByteArray(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1), ConfigFile.ControllerOne); }
                else if (CurrentLine.StartsWith("Pad_Assign2")) { ReadStringByteArray(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1), ConfigFile.ControllerTwo); }
                else if (CurrentLine.StartsWith("Mouse_Assign")) { ReadStringByteArray(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1), ConfigFile.MouseControls); }
                else if (CurrentLine.StartsWith("Screen_Size_Selection")) { ConfigFile.Screensize = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Screen_Full")) { ConfigFile.FullScreen = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Language")) { ConfigFile.Language = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("3D_Sound")) { ConfigFile.SurroundSound = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("SE_Volume")) { ConfigFile.SFXVolume = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("SE_On")) { ConfigFile.SFXToggle = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("BGM_Volume")) { ConfigFile.BGMVolume = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("BGM_On")) { ConfigFile.BGMToggle = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Cheap_Shadow")) { ConfigFile.SoftShadows = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
                else if (CurrentLine.StartsWith("Mouse_Control_Type")) { ConfigFile.MouseControlType = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            }

            INIConfigFile.Dispose();
        }

        private void ReadStringByteArray(string OriginalString, byte[] ByteArray)
        {
            string[] StringArray = OriginalString.Split(' ');

            for (int x = 0; x < StringArray.Length; x++)
            {
                ByteArray[x] = byte.Parse(StringArray[x]);
            }
        }

        public void SaveHeroesConfig()
        {
            string ConfigFilePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\SEGA\SONICHEROES\sonic_h.ini";
            System.IO.StreamWriter INIConfigFile = new System.IO.StreamWriter(ConfigFilePath);

            string PadControls1 = ""; foreach (byte PadButton in ConfigFile.ControllerOne) { PadControls1 = PadControls1 + " " + PadButton; }
            string PadControls2 = ""; foreach (byte PadButton in ConfigFile.ControllerTwo) { PadControls2 = PadControls2 + " " + PadButton; }
            string MouseControls = ""; foreach (byte MouseButton in ConfigFile.MouseControls) { MouseControls = MouseControls + " " + MouseButton; }
            INIConfigFile.WriteLine("Frame_Rate " + ConfigFile.FrameRate);
            INIConfigFile.WriteLine("Free_Camera " + ConfigFile.FreeCamera);
            INIConfigFile.WriteLine("Fog " + ConfigFile.FogEmulation);
            INIConfigFile.WriteLine("Clip_Range " + ConfigFile.ClipRange);
            INIConfigFile.WriteLine("Anisotoropic " + ConfigFile.AnisotropicFiltering);
            INIConfigFile.WriteLine("Pad_Assign1" + PadControls1);
            INIConfigFile.WriteLine("Pad_Assign2" + PadControls2);
            INIConfigFile.WriteLine("Mouse_Assign" + MouseControls);
            INIConfigFile.WriteLine("Screen_Size_Selection 3");
            INIConfigFile.WriteLine("Screen_Full " + ConfigFile.FullScreen);
            INIConfigFile.WriteLine("Language " + ConfigFile.Language);
            INIConfigFile.WriteLine("3D_Sound " + ConfigFile.SurroundSound);
            INIConfigFile.WriteLine("SE_Volume " + ConfigFile.SFXVolume);
            INIConfigFile.WriteLine("SE_On " + ConfigFile.SFXToggle);
            INIConfigFile.WriteLine("BGM_Volume " + ConfigFile.BGMVolume);
            INIConfigFile.WriteLine("BGM_On " + ConfigFile.BGMToggle);
            INIConfigFile.WriteLine("Cheap_Shadow " + ConfigFile.SoftShadows);
            INIConfigFile.WriteLine("Mouse_Control_Type " + ConfigFile.MouseControlType);

            /*
            if (CurrentLine.StartsWith("Frame_Rate")) { ConfigFile.FrameRate = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Free_Camera")) { ConfigFile.FreeCamera = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Fog")) { ConfigFile.FogEmulation = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Clip_Range")) { ConfigFile.ClipRange = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Anisotoropic")) { ConfigFile.AnisotropicFiltering = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Pad_Assign1")) { ReadStringByteArray(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1), ConfigFile.ControllerOne); }
            else if (CurrentLine.StartsWith("Pad_Assign2")) { ReadStringByteArray(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1), ConfigFile.ControllerTwo); }
            else if (CurrentLine.StartsWith("Mouse_Assign")) { ReadStringByteArray(CurrentLine.Substring(CurrentLine.IndexOf(" ") + 1), ConfigFile.MouseControls); }
            else if (CurrentLine.StartsWith("Screen_Size_Selection")) { ConfigFile.Screensize = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Screen_Full")) { ConfigFile.FullScreen = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Language")) { ConfigFile.Language = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("3D_Sound")) { ConfigFile.SurroundSound = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("SE_Volume")) { ConfigFile.SFXVolume = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("SE_On")) { ConfigFile.SFXToggle = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("BGM_Volume")) { ConfigFile.BGMVolume = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("BGM_On")) { ConfigFile.BGMToggle = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Cheap_Shadow")) { ConfigFile.SoftShadows = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            else if (CurrentLine.StartsWith("Mouse_Control_Type")) { ConfigFile.MouseControlType = byte.Parse(CurrentLine.Substring(CurrentLine.LastIndexOf(" ") + 1)); }
            */
            INIConfigFile.Dispose();
        }
    }
}
