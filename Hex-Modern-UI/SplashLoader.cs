using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;

namespace HeroesGHConfigTool
{
    class SplashLoader
    {
        private delegate void Delegate(); //Delecate for regular thread call
        private delegate void CloseDelegate(); //Delegate for cross thread call to close
        public static LoadingForm SplashLoad; //Declare the form which I will present as the loading screen.
        private static Thread LoadingScreenThread; //Declare the thread object to be used for the loading screen.
        public static int SetPercentage;
        public static string LoadText;
        public static bool UpdateStatusCircleComplete = false;

        public static void ShowSplashScreen()
        {
            if (SplashLoad != null) { return; } //Check whether an instance of SplashLoad already exists. Do nothing if it does.
            LoadingScreenThread = new Thread(new ThreadStart(SplashLoader.ShowForm));
            LoadingScreenThread.IsBackground = true;
            LoadingScreenThread.SetApartmentState(ApartmentState.STA);
            LoadingScreenThread.Start();   
        }

        static private void ShowForm()
        {
            SplashLoad = new LoadingForm(); //Create a new instance of the splash form.
            Application.Run(SplashLoad); //Run the splash form.
        }

        static public void xSetPercentage(int Percentage) { SetPercentage = Percentage; }
        static public void xSetMessage(string Message)
        {
            SplashLoad.Invoke( 
                new Delegate
                ( 
                    () =>
                    {
                        SplashLoad.SetLoadingText(Message);
                    }
                ) 
            );
        }

        static public void CloseForm()
        {
            while (true) { if (SplashLoad == null) { Thread.Sleep(200); } else { break; } } //If there is no instance of the splash, sleep for 500ms.
            
                                                                                            // ^^ This is a workaround in the case of a possible cause where the form may be prompted to close before it starts (this
                                                                                            // would happen if the main form were to load too quickly). This is looped in a while loop in the case of a single core processor being used
            SplashLoad.Invoke(new Delegate(SplashLoad.FadeOutSplash));
            SplashLoad.Invoke(new CloseDelegate(SplashLoader.CloseFormInternal));
        }

        static public void CloseFormInternal()
        {
            SplashLoad.Close(); SplashLoad.Dispose();
        }
    }
}
