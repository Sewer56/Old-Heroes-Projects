using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hex_Modern_UI
{

    // Sample navigation code. This is not used in the application!
    class NavigationCode
    {

        // For implementing swappable forms ||| Sample code

        /*
        /// ///////////////////////////////////////////////////////////////////////////////
        /// ID Based Menu Controls | You can also use swappable forms! See SwappableScreens
        /// ///////////////////////////////////////////////////////////////////////////////

        /// 
        /// Lists of controls for each menu type
        ///
        List<Control> SampleMenuControls1 = new List<Control>();
        List<Control> SampleMenuControls2 = new List<Control>();
        List<Control> SampleMenuControls3 = new List<Control>();
        List<Control> SampleMenu_SubmenuControls1 = new List<Control>();
        List<Control> SampleMenu_SubmenuControls2 = new List<Control>();

        // Iterates over each control in the menus and hides/shows them.
        private void HideMenu(List<Control> ControlArray) { foreach (Control myControl in ControlArray) { myControl.Hide(); } }
        private void ShowMenu(List<Control> ControlArray) { foreach (Control myControl in ControlArray) { myControl.Show(); } }

        // This populates the menu control lists, which declare which controls belong to which menu within my application.
        // They are dynamically allocated to menus based on parts of the controls' names.
        private void PopulateMenuControlLists()
        {
            foreach (Control myControlX in Controls)
            {
                if (myControlX.Name.EndsWith("_SampleMenu1") == true) { SampleMenuControls1.Add(myControlX); }
                if (myControlX.Name.EndsWith("_SampleMenu2") == true) { SampleMenuControls2.Add(myControlX); }
                if (myControlX.Name.EndsWith("_SampleMenu3") == true) { SampleMenuControls3.Add(myControlX); }
                if (myControlX.Name.EndsWith("_SampleMenu") == true)
                {
                    if (myControlX.Name.Contains("SubMenu1") == true) { SampleMenu_SubmenuControls1.Add(myControlX); }
                    else if (myControlX.Name.Contains("SubMenu2") == true) { SampleMenu_SubmenuControls2.Add(myControlX); }
                }
            }
        }

        InitializeSystem/Form Constructor -> PopulateMenuControlLists();  // If not using swappable forms!
        */



        // For implementing swappable forms ||| Sample code

        /*
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
        */
    }
}
