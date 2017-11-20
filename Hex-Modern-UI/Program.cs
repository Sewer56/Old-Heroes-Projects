using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        public static Fonts xFonts;
        //public static MainForm xMainWindow;
        public static MainFormSmall xSmallMainWindow;

        public static OverlayForm GDIOverlay; // Overlay me now!

        /// <summary>
        ///  Automatic redirect properties for application theming
        /// </summary>
        public static ToolStripLabel ToolstripThemeControlLabel;
        public static Panel SideBarPanel;
        public static Panel TopBarPanel;
        public static ToolStrip BottomToolstrip;

        public static List<Form> OpenedForms = new List<Form>();
        public static Form CurrentlyOpenedForm;

        [STAThread]
        static void Main()
        {
            xFonts = new Fonts(); // Whohoo!
            xFonts.SetupFonts(); // Get the fonts up and running!

            Application.SetCompatibleTextRenderingDefault(false);
            SplashLoader.ShowSplashScreen(); // Run the method to display the splash screen.

            /// Enable for BIGUI
            //xMainWindow = new MainForm(); // Loading occurs here in the background.       
            xSmallMainWindow = new MainFormSmall(); // Loading occurs here in the BG.
            GDIOverlay = new OverlayForm();

            Thread.Sleep(1000);
            // Sleep the thread for 1000ms, this MUST equal at least the animation speed of the circular progressbar(500ms), else you'd risk 
            // exception as a form closes and the animation library attempts to change the visual value for the circle.
            SplashLoader.CloseForm();
            Application.Run(xSmallMainWindow);
        }
    }
}
