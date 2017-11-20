using Binarysharp.MemoryManagement;
using Binarysharp.MemoryManagement.Helpers;
using System;
using System.Drawing;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    public class GameHook
    {
        // Misc
        const string HEROES_WINDOW_NAME = "SONIC HEROES(TM)";
        public static GameHook.RECT HeroesWindowRectangle;
        public static IntPtr HeroesHandle;
        public static System.Timers.Timer GameHookCheckTimer;

        // Is the game hooked?
        public static System.Timers.Timer GameHookCheck = new System.Timers.Timer();

        // Game Process
        public static MemorySharp GameProcess;

        public struct RECT
        {
            public int LeftBorder, TopBorder, RightBorder, BottomBorder;
        }

        public void WriteWaitingForGameText() { Program.xSmallMainWindow.lbl_ActionBar_BottomRight.Text = "Waiting for game..."; }
        public void WriteGameIsHookedText() { Program.xSmallMainWindow.lbl_ActionBar_BottomRight.Text = "Game is hooked."; }

        public void SetWindowParent()
        {
            IntPtr FormHandleID = Program.xSmallMainWindow.Handle;
            IntPtr OverlayWindowHandleID = Program.GDIOverlay.Handle;
            WinAPIComponents.SetParentX(FormHandleID, OverlayWindowHandleID);

            IntPtr WarpMenuInformationHandleID = Program.xSmallMainWindow.WarpUtilitiesMenu.WarpInformationOverlay.Handle;
            WinAPIComponents.SetParentX(WarpMenuInformationHandleID, OverlayWindowHandleID);

            Program.xSmallMainWindow.TopMost = true;
        }

        public void DrawWindow()
        {
            Program.GDIOverlay.FormBorderStyle = FormBorderStyle.None;
            Program.GDIOverlay.BackColor = Color.FromArgb(00, 255, 00);
            Program.GDIOverlay.TransparencyKey = Color.FromArgb(00, 255, 00);
            Program.GDIOverlay.Show();

            int InitialStyle = WinAPIComponents.GetWindowLong(Program.GDIOverlay.Handle, -20); // Get the window style

            // WinAPIComponents.SetWindowLong(Program.GDIOverlay.Handle, -20, InitialStyle | 0x80000 | 0x20); // Set window properties | Window is now clickthrough!
            // This is now redundant!

            WinAPIComponents.GetWindowRect(HeroesHandle, out HeroesWindowRectangle); // Get window rectangle
            Program.GDIOverlay.TopMost = true; // Put to top.

            AdjustOverlayWindow();

            // Listen for sizechange change changes across all processes/threads on current desktop...
            IntPtr HeroesHook = WinAPIComponents.SetWinEventHook(WinAPIComponents.EVENT_OBJECT_LOCATIONCHANGE, WinAPIComponents.EVENT_OBJECT_LOCATIONCHANGE, IntPtr.Zero, WinAPIComponents.procDelegate, 0, 0, WinAPIComponents.WINEVENT_OUTOFCONTEXT);

            // Now put this window on that fake borderspace window!
            SetWindowParent();
            Program.OpenedForms.Add(Program.GDIOverlay);
            Program.xSmallMainWindow.Location = new Point(0, 0);
            Program.xSmallMainWindow.WarpUtilitiesMenu.WarpInformationOverlay.Location = new Point(0, 0);
            Program.xSmallMainWindow.OptionsMenu.TinyUI_Nud_OverlayOpacity.Enabled = true;
            Program.xSmallMainWindow.OptionsMenu.TinyUI_Nud_OverlayOpacity.Value = Program.xSmallMainWindow.OptionsMenu.TinyUI_Nud_Opacity.Value;
            Program.xSmallMainWindow.OptionsMenu.TinyUI_Nud_Opacity.Value = 1;
            Program.xSmallMainWindow.OptionsMenu.TinyUI_Nud_Opacity.Enabled = false;
        }

        public void AdjustOverlayWindow()
        {
            // Adjust Size Accordingly
            Program.GDIOverlay.Size = new Size(HeroesWindowRectangle.RightBorder - HeroesWindowRectangle.LeftBorder, HeroesWindowRectangle.BottomBorder - HeroesWindowRectangle.TopBorder);
            Program.GDIOverlay.Top = HeroesWindowRectangle.TopBorder;
            Program.GDIOverlay.Left = HeroesWindowRectangle.LeftBorder;
        }

        public void SetupHookCheckThread()
        {
            Thread GameHookCheck = new Thread(SetupHookCheckTimer);
            GameHookCheck.Start();
        }

        public void SetupHookCheckTimer()
        {
            GameHookCheckTimer = new System.Timers.Timer();
            GameHookCheckTimer.Interval = Convert.ToDouble(Program.xSmallMainWindow.OptionsMenu.TinyUI_Nud_AutoHookInterval.Value);
            GameHookCheckTimer.Elapsed += new ElapsedEventHandler(HookTheGame);
            GameHookCheckTimer.Enabled = true;
            GameHookCheckTimer.Start();
        }

        public void HookTheGame(object sender, EventArgs e)
        {
            if (GameProcess == null)
            {
                try
                {
                    // Game Process
                    GameProcess = new MemorySharp(ApplicationFinder.FromProcessName("Tsonic_win").First());
                    // If the first doesn't successfully happen this will never be hit
                    HeroesHandle = WinAPIComponents.FindWindow(null, HEROES_WINDOW_NAME);
                    Program.xSmallMainWindow.Invoke(new MethodInvoker(WriteGameIsHookedText));
                    Program.xSmallMainWindow.Invoke(new MethodInvoker(DrawWindow));                    
                }
                catch
                {
                    Program.xSmallMainWindow.Invoke(new MethodInvoker(WriteWaitingForGameText));
                }
            }
            else if (GameProcess.IsRunning == false)
            {
                try
                {
                    // Game Process
                    GameProcess = new MemorySharp(ApplicationFinder.FromProcessName("Tsonic_win").First());
                    // If the first doesn't successfully happen this will never be hit
                    HeroesHandle = WinAPIComponents.FindWindow(null, HEROES_WINDOW_NAME);
                    Program.xSmallMainWindow.Invoke(new MethodInvoker(WriteGameIsHookedText));
                    Program.xSmallMainWindow.Invoke(new MethodInvoker(DrawWindow));
                }
                catch
                {
                    Program.xSmallMainWindow.Invoke(new MethodInvoker(WriteWaitingForGameText));
                }
            }
        }
    }
}
