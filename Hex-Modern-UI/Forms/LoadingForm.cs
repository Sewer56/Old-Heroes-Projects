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
    public partial class LoadingForm : Form
    {

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

        public LoadingForm()
        {
            InitializeComponent();
            CenterToScreen();
            Circle_Loading.Value = 0;
            Circle_Loading.Region = new Region(new Rectangle(2, 2, Circle_Loading.Width - 4, Circle_Loading.Height - 4));
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 30, 30));
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            Timer_FadingTimer.Start();
            StartBackgroundWork();
            FadeOutSplash();
        }

        private void Timer_FadingTimer_Tick(object sender, EventArgs e)
        {
            //Object is not fully invisible. Fade it in
            if (this.Opacity < 1.0)
            {
                this.Opacity += 0.025;
            }
            else { Timer_FadingTimer.Stop(); }

        }

        public void FadeOutSplash()
        {
            //Object is not fully invisible. Fade it in
            while (this.Opacity > 0.0)
            {
                this.Opacity -= 0.025;
                Thread.Sleep(10);
            }
            Thread.Sleep(300); // UX: Sleep just for the cause of the main form to not show too quickly post load.
        }

        /// ----------------------------------------------------------------------------------------------------
        /// Start the backgroundWorker's Background work || Method for the work the backgroundWorker carries out
        /// ----------------------------------------------------------------------------------------------------
        // ------------------------------------------------------------
        private void StartBackgroundWork()                                                                          // Method to start the background work for the backgroundWorker
        {

                                                                                                                    // ------------------------------------------------------------
            BGWorker_Loading.WorkerReportsProgress = true;                                                          // Tell the backgroundWorker to report its progress back. Used for loading bar completion percentage.
            BGWorker_Loading.RunWorkerAsync();                                                                      // Run the worker asynchronously to the rest of the program on a separate thread.
        }
                                                                                                                    // --------------------------------------------------------------------------
        private void BGWorker_Loading_DoWork(object sender, System.ComponentModel.DoWorkEventArgs e)                // Method for the backgroundWorker's event triggered used to execute its code
        {                                                                                                           // --------------------------------------------------------------------------
            while (true)
            {
                BGWorker_Loading.ReportProgress(SplashLoader.SetPercentage);
                if (SplashLoader.UpdateStatusCircleComplete == true) { break; }
                Thread.Sleep(100);
            }  // Report the progress as X% // Sleep 100ms

        }

        /// -----------------------------------
        /// Method for updating the progressbar
        /// -----------------------------------
        private void BGWorker_Loading_ProgressChanged(object sender, System.ComponentModel.ProgressChangedEventArgs e)      // Method ran the the change in the backgroundWorker progress
        {                                                                                                                   // ----------------------------------------------------------
            Circle_Loading.Text = Convert.ToString(e.ProgressPercentage) + "%";
            Circle_Loading.Value = e.ProgressPercentage;                                                               // Set the progressbar to the percentage completion of the backgroundWorker.
        }
    }
}
