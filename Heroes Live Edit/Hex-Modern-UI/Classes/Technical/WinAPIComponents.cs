using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    static class WinAPIComponents
    {
        /*
            Import of the GDI drawing library from the Windows Operating system which will be used to draw the application with rounded rectangular sides.
            This however hampers portability for deploying this application onto other operating systems, however I think the usage of Windows.Forms does have a worse impact
            than this, if I recall for non-native platforms, Wine should have this library.
        */

        // Mask away the corners of the form using GDI for drawing.
        [DllImport("GDI32.dll", EntryPoint = "CreateRoundRectRgn")]
        public static extern IntPtr CreateRoundRectRgn
        (
            int nLeftRect, // Represents the x-coordinate of upper-left corner of the control/form.
            int nTopRect, // Represents the y-coordinate of upper-left corner of the control/form.
            int nRightRect, // Represents the x-coordinate of the lower right corner of the control/form.
            int nBottomRect, // Represents the x-coordinate of the lower right of the control/form.
            int nWidthEllipse, // The height by which the form is meant to be cut in order to make an ellipse.
            int nHeightEllipse // The width by which the form is meant to be cut in order to make an ellipse.
        );

        // Reference: https://msdn.microsoft.com/en-us/library/windows/desktop/ff468877(v=vs.85).aspx
        public const int WM_NCLBUTTONDOWN = 0xA1;                                           // Hex pointer/offset to parameter. Posted when the user presses the left mouse button while the cursor is within the nonclient area of a window.
        public const int HT_CAPTION = 0x2;                                                  // Hex pointer/offset to parameter. Position of mouse is caption in titlebar.

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam); // Send message to Windows component user32.dll responsible for window management.
                                                                                            // Pointer to Window, message, 'unused' parameter and parameter specifying mouse pointer coordinates.
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();                                         // Release the captured mouse event.

        [DllImport("user32.dll")]
        public static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool GetWindowRect(IntPtr hwnd, out GameHook.RECT lpRect);

        [DllImport("user32.dll")]
        public static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        public delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);
        public static WinEventDelegate procDelegate = new WinEventDelegate(WinEventProc);

        static void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            // filter out non-HWND namechanges... (eg. items within a listbox)
            if (idObject != 0 || idChild != 0)
            {
                return;
            }

            // Adjust Size Accordingly
            WinAPIComponents.GetWindowRect(GameHook.HeroesHandle, out GameHook.HeroesWindowRectangle); // Get rectangle

            int WindowWidth = GameHook.HeroesWindowRectangle.RightBorder - GameHook.HeroesWindowRectangle.LeftBorder;
            int WindowHeight = GameHook.HeroesWindowRectangle.BottomBorder - GameHook.HeroesWindowRectangle.TopBorder;

            Program.GDIOverlay.Size = new Size(WindowWidth, WindowHeight);
            Program.GDIOverlay.Top = GameHook.HeroesWindowRectangle.TopBorder;
            Program.GDIOverlay.Left = GameHook.HeroesWindowRectangle.LeftBorder;
        }

        public const uint EVENT_OBJECT_LOCATIONCHANGE = 0x800B;
        public const uint WINEVENT_OUTOFCONTEXT = 0;

        private const int GWL_EXSTYLE = -20;
        private const int WS_EX_CLIENTEDGE = 0x200;
        private const uint SWP_NOSIZE = 0x0001;
        private const uint SWP_NOMOVE = 0x0002;
        private const uint SWP_NOZORDER = 0x0004;
        private const uint SWP_NOREDRAW = 0x0008;
        private const uint SWP_NOACTIVATE = 0x0010;
        private const uint SWP_FRAMECHANGED = 0x0020;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_NOCOPYBITS = 0x0100;
        private const uint SWP_NOOWNERZORDER = 0x0200;
        private const uint SWP_NOSENDCHANGING = 0x0400;

        public static void SetParentX(IntPtr FormHandleID, IntPtr SonicHeroesHandleID)
        {
            SetParent(FormHandleID, SonicHeroesHandleID);
        }

        public static bool SetBevel(Form form, bool ShowBevel)
        {
            foreach (Control c in form.Controls)
            {
                MdiClient client = c as MdiClient;
                if (client != null)
                {
                    int windowLong = GetWindowLong(c.Handle, GWL_EXSTYLE);

                    if (ShowBevel)
                    {
                        windowLong |= WS_EX_CLIENTEDGE;
                    }
                    else
                    {
                        windowLong &= ~WS_EX_CLIENTEDGE;
                    }

                    SetWindowLong(c.Handle, GWL_EXSTYLE, windowLong);

                    // Update the non-client area.
                    SetWindowPos(client.Handle, IntPtr.Zero, 0, 0, 0, 0,
                        SWP_NOACTIVATE | SWP_NOMOVE | SWP_NOSIZE | SWP_NOZORDER |
                        SWP_NOOWNERZORDER | SWP_FRAMECHANGED);

                    return true;
                }
            }
            return false;
        }
    }
}
