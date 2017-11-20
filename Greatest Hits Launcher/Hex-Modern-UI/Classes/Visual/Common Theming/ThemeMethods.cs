using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HeroesGHConfigTool
{
    class ThemeMethods
    {
        // Reference: https://msdn.microsoft.com/en-us/library/windows/desktop/ff468877(v=vs.85).aspx
        public const int WM_NCLBUTTONDOWN = 0xA1;                                           // Hex pointer/offset to parameter. Posted when the user presses the left mouse button while the cursor is within the nonclient area of a window.
        public const int HT_CAPTION = 0x2;                                                  // Hex pointer/offset to parameter. Position of mouse is caption in titlebar

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam); // Send message to Windows component user32.dll responsible for window management.
                                                                                            // Pointer to Window, message, 'unused' parameter and parameter specifying mouse pointer coordinates.
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();                                         // Release the captured mouse event.

        /*
            Import of the GDI drawing library from the Windows Operating system which will be used to draw the application with rounded rectangular sides.
            This however hampers portability for deploying this application onto other operating systems, however I think the usage of Windows.Forms does have a worse impact
            than this, if I recall for non-native platforms, Wine should have this library.
        */

        // Mask away the corners of the form using GDI for drawing.
        [DllImport("GDI32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // Represents the x-coordinate of upper-left corner of the control/form.
            int nTopRect, // Represents the y-coordinate of upper-left corner of the control/form.
            int nRightRect, // Represents the x-coordinate of the lower right corner of the control/form.
            int nBottomRect, // Represents the x-coordinate of the lower right of the control/form.
            int nWidthEllipse, // The height by which the form is meant to be cut in order to make an ellipse.
            int nHeightEllipse // The width by which the form is meant to be cut in order to make an ellipse.
        );

        /// -------------------------------------------------------
        /// Theme the application assets to match the desired theme
        /// -------------------------------------------------------
                                                                                                                    // ------------------------------------------------------------------------------------
        public static void DoThemeAssets(Form PassedInForm)                                                         // Method ran by the backgroundWorker to set new enter and leave events for the buttons
        {                                                                                                           // ------------------------------------------------------------------------------------
            foreach (Control myControl in PassedInForm.Controls)
            {
                TestAndApplyThemingPropertiesByObjectName(myControl, PassedInForm);
                TestAndApplyThemingPropertiesByObjectType(myControl, PassedInForm);

                if (myControl is Panel)                                                                        // If the specific control were to be a panel, we must find and deal with controls also inside the panel.
                {
                    foreach (Control myPanelControl in myControl.Controls)
                    {
                        TestAndApplyThemingPropertiesByObjectName(myPanelControl, PassedInForm);
                        TestAndApplyThemingPropertiesByObjectType(myPanelControl, PassedInForm);
                    }
                }
            }
            foreach (CustomNumericUpDown myControl in PassedInForm.Controls.OfType<CustomNumericUpDown>())
            {
                myControl.Arrows_BackColor();
            }
        }

        /// -----------------------------------------------------------------------------------------------------------
        /// Method which will deploy various object theming properties depending on the name of the control in question
        /// -----------------------------------------------------------------------------------------------------------

        public static void TestAndApplyThemingPropertiesByObjectName(Control myControl, Form PassedInForm)
        {
            if (myControl.Name.StartsWith("TxtLbl_") == true) { myControl.Font = Program.xFonts.RobotoSmallTextboxLabel; myControl.ForeColor = Program.BottomToolstrip.BackColor; }
            else if (myControl.Name.StartsWith("Txt_") == true) { myControl.Font = Program.xFonts.RobotoRegularText; myControl.ForeColor = Program.BottomToolstrip.BackColor; }
            else if (myControl.Name.StartsWith("TxtSpc_") == true) { myControl.Font = Program.xFonts.RobotoRegularText; }
            else if (myControl.Name.StartsWith("TinyUI_TxtSpc_") == true) { myControl.Font = Program.xFonts.RobotoSpecialTextTinyUI; }
            else if (myControl.Name.StartsWith("TinyUI_TxtSpc2") == true) { myControl.Font = Program.xFonts.RobotoSpecialTextTinyUI2; }
            else if (myControl.Name.StartsWith("TopLabel_") == true) { myControl.Font = Program.xFonts.RobotoTopTitle; }
            else if (myControl.Name.StartsWith("TinyUI_TopLabel_") == true) { myControl.Font = Program.xFonts.RobotoTopTitleTinyUI; }
            else if (myControl.Name.StartsWith("TitleLbl_") == true) { myControl.Font = Program.xFonts.RobotoTitleLabel; myControl.ForeColor = Program.BottomToolstrip.BackColor; }
            else if (myControl.Name.StartsWith("TitleLblSmall_") == true) { myControl.Font = Program.xFonts.RobotoTitleLabelSmall; myControl.ForeColor = Program.BottomToolstrip.BackColor; }
            else if (myControl.Name.StartsWith("TxtBoxLarge_") == true) { myControl.Font = Program.xFonts.RobotoLargeTextbox; }
            else if (myControl.Name.StartsWith("TxtBoxSmall_") == true) { myControl.Font = Program.xFonts.RobotoSmallTextbox; }
            else if (myControl.Name.StartsWith("DateTime_") == true) { myControl.Font = Program.xFonts.RobotoSmallTextbox; myControl.Region = new Region(new Rectangle(2, 2, myControl.Width - 4, myControl.Height - 6)); }
            else if (myControl.Name.StartsWith("ComboBoxSmall_") == true) { myControl.Font = Program.xFonts.RobotoSmallTextbox; }
            else if (myControl.Name.StartsWith("TinyUI_ComboBoxSmall_") == true) { myControl.Font = Program.xFonts.RobotoComboBoxSmallTinyUI; }
            else if (myControl.Name.StartsWith("TinyUI_TxtBoxSmall_") == true) { myControl.Font = Program.xFonts.RobotoTextBoxSmallTinyUI; }
            else if (myControl.Name.StartsWith("TinyUI_TxtBoxSmaller_") == true) { myControl.Font = Program.xFonts.RobotoTextBoxSmallerTinyUI; }
            else if (myControl.Name.StartsWith("LrgBtn_") == true) { myControl.Font = Program.xFonts.RobotoLargeTextbox; }
            else if (myControl.Name.StartsWith("LrgBtnRnd_") == true) { myControl.Font = Program.xFonts.RobotoLargeButton; myControl.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, myControl.Width, myControl.Height, 30, 30)); }
            else if (myControl.Name.StartsWith("BtnRnd_") == true) { myControl.Font = Program.xFonts.RobotoMidTextbox; myControl.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, myControl.Width, myControl.Height, 24, 24)); }
            else if (myControl.Name.StartsWith("TinyUI_Nud_") == true) { myControl.Font = Program.xFonts.RobotoNumericUpDownTinyUI; }
        }

        public static void TestAndApplyThemingPropertiesByObjectType(Control myControl, Form PassedInForm)
        {
            if (myControl is Button || myControl is CustomButton)
            {
                if (myControl.Name.StartsWith("Btn_") == true) { myControl.MouseEnter += SetButtonHighlightColour; myControl.MouseLeave += UnSetButtonHighlightColour; }
                else if (myControl.Name.StartsWith("TopBtn_") == true) { myControl.MouseEnter += SetButtonHighlightColourTopbar; myControl.MouseLeave += UnSetButtonHighlightColourTopbar; }
                else if (myControl.Name.StartsWith("SideBtn_") == true) { myControl.MouseEnter += SetButtonHighlightColourSidebar; myControl.MouseLeave += UnSetButtonHighlightColourSidebar; }

                else if (myControl.Name.StartsWith("ThemeAsset_") == true)
                {
                    myControl.MouseEnter += SetButtonBorderThemeAsset; myControl.MouseLeave += UnSetButtonBorderThemeAsset;

                    if (myControl.Name.Contains("TopBarColour") == true) { myControl.Click += SetTopBarTheme; }
                    if (myControl.Name.Contains("SidebarTheme") == true) { myControl.Click += SetSideBarTheme; }
                    if (myControl.Name.Contains("ThemeAccent") == true) { myControl.Click += SetThemeAccent; }

                }
            }
        }

        public static void SetSideBarTheme(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            Color TempColor = btn.BackColor;

            if (btn.Name.Contains("Customizable"))
            {
                ColorDialog ColorDialog = new ColorDialog();
                DialogResult ColorResult = ColorDialog.ShowDialog();
                if (ColorResult == DialogResult.OK) { btn.BackColor = ColorDialog.Color; }
            }

            Program.SideBarPanel.BackColor = btn.BackColor;

            foreach (Control myControlX in Program.SideBarPanel.Controls.OfType<Button>())
            {
                if (myControlX.Name.StartsWith("SideBtn_") == true)
                {
                    myControlX.BackColor = btn.BackColor;
                }
            }
            btn.BackColor = TempColor;
        }

        public static void SetTopBarTheme(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            Color TempColor = btn.BackColor;

            if (btn.Name.Contains("Customizable"))
            {
                ColorDialog ColorDialog = new ColorDialog();
                DialogResult ColorResult = ColorDialog.ShowDialog();
                if (ColorResult == DialogResult.OK) { btn.BackColor = ColorDialog.Color; }
            }

            Program.TopBarPanel.BackColor = btn.BackColor;
            btn.BackColor = TempColor;
            ResetTopBarButtonBG();
        }

        public static void SetThemeAccent(object sender, EventArgs e)
        {
            var btn = (Button)sender;
            Color TempColor = btn.BackColor;

            if (btn.Name.Contains("Customizable"))
            {
                ColorDialog ColorDialog = new ColorDialog();
                DialogResult ColorResult = ColorDialog.ShowDialog();
                if (ColorResult == DialogResult.OK) { btn.BackColor = ColorDialog.Color; }
            }

            Program.BottomToolstrip.BackColor = btn.BackColor;

            foreach (Form MyFormList in Program.OpenedForms)
            {
                foreach (Control myControlX in MyFormList.Controls.OfType<Label>())
                {
                    if (myControlX.Name.StartsWith("TitleLbl_") == true) { myControlX.ForeColor = btn.BackColor; }
                    if (myControlX.Name.StartsWith("TinyUI_TopLabel_") == true) { myControlX.ForeColor = btn.BackColor; }
                    if (myControlX.Name.StartsWith("TitleLblSmall_") == true) { myControlX.ForeColor = btn.BackColor; }
                    if (myControlX.Name.StartsWith("TxtLbl_") == true) { myControlX.ForeColor = btn.BackColor; }
                    if (myControlX.Name.StartsWith("Txt_") == true) { myControlX.ForeColor = btn.BackColor; }
                }
                foreach (Control myControlX in MyFormList.Controls.OfType<Button>())
                {
                    if (myControlX.Name.StartsWith("LrgBtnRnd_") == true) { myControlX.BackColor = btn.BackColor; }
                    if (myControlX.Name.StartsWith("BtnRnd_") == true) { myControlX.BackColor = btn.BackColor; }
                    if (myControlX.Name.StartsWith("Btn_") == true) { myControlX.BackColor = btn.BackColor; }
                }
            }

            btn.BackColor = TempColor;
        }

        public static void SetSideBarThemeManual(Color SideBarColor)
        {
            Program.SideBarPanel.BackColor = SideBarColor;
            foreach (Control myControlX in Program.SideBarPanel.Controls.OfType<Button>())
            {
                if (myControlX.Name.StartsWith("SideBtn_") == true) { myControlX.BackColor = SideBarColor; }
            }
        }

        public static void SetTopBarThemeManual(Color TopBarColor) { Program.TopBarPanel.BackColor = TopBarColor; ResetTopBarButtonBG(); }
        public static void SetThemeAccentManual(Color ThemeAccentColor)
        {
            Program.BottomToolstrip.BackColor = ThemeAccentColor;

            foreach (Form MyFormList in Program.OpenedForms)
            {
                foreach (Control myControlX in MyFormList.Controls.OfType<Label>())
                {
                    if (myControlX.Name.StartsWith("TitleLbl_") == true) { myControlX.ForeColor = ThemeAccentColor; }
                    if (myControlX.Name.StartsWith("TinyUI_TopLabel_") == true) { myControlX.ForeColor = ThemeAccentColor; }
                    if (myControlX.Name.StartsWith("TitleLblSmall_") == true) { myControlX.ForeColor = ThemeAccentColor; }
                    if (myControlX.Name.StartsWith("TxtLbl_") == true) { myControlX.ForeColor = ThemeAccentColor; }
                    if (myControlX.Name.StartsWith("Txt_") == true) { myControlX.ForeColor = ThemeAccentColor; }
                }
                foreach (Control myControlX in MyFormList.Controls.OfType<Button>())
                {
                    if (myControlX.Name.StartsWith("LrgBtnRnd_") == true) { myControlX.BackColor = ThemeAccentColor; }
                    if (myControlX.Name.StartsWith("BtnRnd_") == true) { myControlX.BackColor = ThemeAccentColor; }
                    if (myControlX.Name.StartsWith("Btn_") == true) { myControlX.BackColor = ThemeAccentColor; }
                }
            }
        }

        public static void ResetTopBarButtonBG()
        {
            foreach (Form MyFormList in Program.OpenedForms)
            {
                foreach (Panel myPanelX in MyFormList.Controls.OfType<Panel>())
                {
                    foreach (Control myControlX in myPanelX.Controls.OfType<Button>())
                    {
                        if (myControlX.Name.StartsWith("TopBtn_") == true)
                        {
                            UnSetButtonHighlightColourTopbar(myControlX, null);
                        }
                    }
                }
            }      
        }
           
                                                                                                                    // ------------------------------------------------------------------------------------------
        public static void SetButtonHighlightColour(object sender, EventArgs e)                                     // Code used to add to the mousehover event. It is used to adjust highlight colour of buttons
        {                                                                                                           // ------------------------------------------------------------------------------------------
            var btn = (Button)sender;                                                                               // Set the button variable to the sender of type button.
            btn.BackColor = GenerateHighlightLighterButton(Program.BottomToolstrip.BackColor);                      // Set the BackColor of the button to the background colour of the ActionBar.
            btn.ForeColor = Program.ToolstripThemeControlLabel.ForeColor;                                           // Set the ForeColor (text color) of the button to the text colour of the ActionBar.
            
        }                                                                                                           // Collect garbage from the last button colour.

                                                                                                                    // ------------------------------------------------------------------------------------------
        public static void SetButtonHighlightColourSidebar(object sender, EventArgs e) // Code used to add to the mousehover event. It is used to adjust highlight colour of buttons
        {                                                                                                           // ------------------------------------------------------------------------------------------
            var btn = (Button)sender;                                                                               // Set the button variable to the sender of type button.
            btn.BackColor = GenerateHighlightLighterButton(Program.SideBarPanel.BackColor);                                // Set the BackColor of the button to the background colour of the SideBar.
            btn.ForeColor = Program.ToolstripThemeControlLabel.ForeColor;                                                                // Set the ForeColor (text color) of the button to the foreground colour of the SideBar.
            
        }                                                                                                           // Collect garbage from the last button colour.

        public static void SetButtonHighlightColourTopbar(object sender, EventArgs e)// Code used to add to the mousehover event. It is used to adjust highlight colour of buttons
        {                                                                                                           // ------------------------------------------------------------------------------------------
            var btn = (Button)sender;                                                                               // Set the button variable to the sender of type button.
            btn.BackColor = GenerateHighlightLighterButton(Program.SideBarPanel.BackColor);                       // Set the BackColor of the button to the background colour of the SideBar.
            
        }                                                                                                           // Collect garbage from the last button colour.

                                                                                                                    // ------------------------------------------------------------------------------------------
        public static void SetButtonBorderThemeAsset(object sender, EventArgs e)                                    // Code used to add to the mousehover event. It is used to adjust highlight colour of buttons
        {                                                                                                           // ------------------------------------------------------------------------------------------
            var btn = (Button)sender;                                                                               // Set the button variable to the sender of type button.
            btn.FlatAppearance.BorderSize = 1;
            
        }                                                                                                           // Collect garbage from the last button colour.

                                                                                                                    // ------------------------------------------------------------------------------------------
        public static void UnSetButtonBorderThemeAsset(object sender, EventArgs e)                                  // Code used to add to the mousehover event. It is used to adjust highlight colour of buttons
        {                                                                                                           // ------------------------------------------------------------------------------------------
            var btn = (Button)sender;                                                                               // Set the button variable to the sender of type button.
            btn.FlatAppearance.BorderSize = 0;
            
        }                                                                                                           // Collect garbage from the last button colour.



        public static Color GenerateHighlightLighterButton(Color InputColor)
        {
            byte BlueChannel; BlueChannel = InputColor.B;
            byte RedChannel; RedChannel = InputColor.R;
            byte GreenChannel; GreenChannel = InputColor.G;

            // Note, percentage is from 0%, 0.00 to 100%, 1.00 - directly expressed as float.
            Color newColor = ControlPaint.Light(InputColor, 0.05F);

            // DEBUG
            // MessageBox.Show("Input Colour: " + Convert.ToString(InputColor) + "\nToolstrip Colour: " + Convert.ToString(Program.BottomToolstrip.BackColor) + "\nNew Colour: " + Convert.ToString(newColor));

            return newColor;
        }
                                                                                                                    // ------------------------------------------------------------------------------------------
        public static void UnSetButtonHighlightColour
        (object sender, EventArgs e)                                                                                // Code used to add to the mouseleave event. It is used to adjust highlight colour of buttons
        {                                                                                                           // ------------------------------------------------------------------------------------------
            var btn = (Button)sender;                                                                               // Set the button variable to the sender of type button.
            btn.BackColor = Program.BottomToolstrip.BackColor;                                                     // Set the BackColor of the button to the background colour of the ActionBar.
            btn.ForeColor = Program.ToolstripThemeControlLabel.ForeColor;                                                 // Set the ForeColor (text color) of the button to the text colour of the ActionBar.                                                                         
            
        }                                                                                                           // Collect garbage from the last button colour.

        public static void UnSetButtonHighlightColourSidebar(object sender, EventArgs e)                            
        {                                                                                                           
            var btn = (Button)sender;                                                                               
            btn.BackColor = Program.SideBarPanel.BackColor;                                                                
            btn.ForeColor = Program.ToolstripThemeControlLabel.ForeColor;                                                    
            
        }                                                                                                           

        public static void UnSetButtonHighlightColourTopbar(object sender, EventArgs e) 
        {                                                                                                           
            var btn = (Button)sender;                                                                              
            btn.BackColor = Program.TopBarPanel.BackColor;                                                               
        }


        public static void AutoLoadCurrentTheme()
        {
            if ( File.Exists(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ThemeConfig.txt") )
            {
                try
                {
                    LoadCurrentTheme();
                }
                catch
                {

                }
            }
        }

        public static void SaveCurrentTheme()
        {
            System.IO.StreamWriter TextFile = new System.IO.StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ThemeConfig.txt");

            TextFile.WriteLine("TitleBarColor: " + ColorTranslator.ToHtml(Program.TopBarPanel.BackColor));
            TextFile.WriteLine("SideBarColor: " + ColorTranslator.ToHtml(Program.SideBarPanel.BackColor));
            TextFile.WriteLine("AccentColor: " + ColorTranslator.ToHtml(Program.BottomToolstrip.BackColor));

            TextFile.Dispose();
        }

        public static void LoadCurrentTheme()
        {
            StreamReader TextFile = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\ThemeConfig.txt");
            string CurrentLine;
            int IndexOfColour;

            while ((CurrentLine = TextFile.ReadLine()) != null)
            {
                IndexOfColour = CurrentLine.IndexOf("#");

                if (CurrentLine.Contains("TitleBarColor: ")) { ThemeMethods.SetTopBarThemeManual(ColorTranslator.FromHtml(CurrentLine.Substring(IndexOfColour))); }
                else if (CurrentLine.Contains("SideBarColor: ")) { ThemeMethods.SetSideBarThemeManual(ColorTranslator.FromHtml(CurrentLine.Substring(IndexOfColour))); }
                else if (CurrentLine.Contains("AccentColor: ")) { ThemeMethods.SetThemeAccentManual(ColorTranslator.FromHtml(CurrentLine.Substring(IndexOfColour))); }
            }

            TextFile.Dispose();
        }
    }
}
