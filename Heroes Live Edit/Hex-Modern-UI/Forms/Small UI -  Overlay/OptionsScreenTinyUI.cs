using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;
using System.Threading;

namespace Hex_Modern_UI
{
    public partial class OptionsScreenTinyUI : Form
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

        public OptionsScreenTinyUI()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }

        private void Btn_Hook_Click(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.SonicHeroesHook.HookTheGame(null, null);
        }

        private void TinyUI_Nud_Opacity_ValueChanged(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.Opacity = Convert.ToDouble(this.TinyUI_Nud_Opacity.Value);
        }

        private void TinyUI_Nud_OverlayOpacity_ValueChanged(object sender, EventArgs e)
        {
            Program.GDIOverlay.Opacity = Convert.ToDouble(this.TinyUI_Nud_OverlayOpacity.Value);
        }

        public void SaveCurrentSettings()
        {
            System.IO.StreamWriter TextFile = new System.IO.StreamWriter(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\OptionsConfig.txt");

            if (Program.xSmallMainWindow.OptionsMenu.TinyUI_Nud_Opacity.Enabled)
            { TextFile.WriteLine("Opacity: " + TinyUI_Nud_Opacity.Value); }
            else { TextFile.WriteLine("Opacity: " + TinyUI_Nud_OverlayOpacity.Value);  }

            if (Program.xSmallMainWindow.OptionsMenu.Btn_AutoHook.OwnerDrawText == "Enable Autohook")
            { TextFile.WriteLine("AutohookEnabled: N"); }
            else { TextFile.WriteLine("AutohookEnabled: "); }

            TextFile.WriteLine("AutohookDelay: " + TinyUI_Nud_AutoHookInterval.Value);

            TextFile.Dispose();
        }

        public void LoadCurrentTheme()
        {
            StreamReader TextFile = new StreamReader(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\OptionsConfig.txt");
            string CurrentLine;
            int IndexOfAnswer;

            while ((CurrentLine = TextFile.ReadLine()) != null)
            {
                IndexOfAnswer = CurrentLine.LastIndexOf(" ");
                IndexOfAnswer += 1;

                if (CurrentLine.Contains("Opacity: "))
                {
                    string CurrentOpacity = CurrentLine.Substring(IndexOfAnswer);
                    TinyUI_Nud_Opacity.Value = Convert.ToDecimal(CurrentOpacity);
                }

                if (CurrentLine.Contains("AutohookEnabled: "))
                {
                    string Result = CurrentLine.Substring(IndexOfAnswer);
                    if (Result == "Y") { Btn_AutoHook_Click(null,null); }
                }

                if (CurrentLine.Contains("AutohookDelay: "))
                {
                    string Result = CurrentLine.Substring(IndexOfAnswer);
                    TinyUI_Nud_AutoHookInterval.Value = Convert.ToDecimal(Result);
                }
            }

            TextFile.Dispose();
        }

        private void Btn_Save_Click(object sender, EventArgs e)
        {
            SaveCurrentSettings();

            Thread StatusBarTempMessage = new Thread
            (
                () => DisplayTemporaryStatusBarMessage("Settings Saved! Auto load on next startup!")
            );
            StatusBarTempMessage.Start();
        }

        private void DisplayTemporaryStatusBarMessage(string Message)
        {
            string OriginalText = Program.xSmallMainWindow.lbl_ActionBar_BottomRight.Text;
            Program.xSmallMainWindow.Invoke( new MethodInvoker( () => Program.xSmallMainWindow.ChangeActionBarLabelText(Message) ) );
            Thread.Sleep(4000);
            Program.xSmallMainWindow.Invoke(new MethodInvoker( () => Program.xSmallMainWindow.ChangeActionBarLabelText(OriginalText) ) );
        }

        private void EnableAutohook()
        {
            Program.xSmallMainWindow.SonicHeroesHook.SetupHookCheckThread();
        }

        private void Btn_AutoHook_Click(object sender, EventArgs e)
        {
            if (Btn_AutoHook.OwnerDrawText == "Enable Autohook")
            {
                Btn_AutoHook.OwnerDrawText = "Disable Autohook";
                EnableAutohook();
            }
            else if (Btn_AutoHook.OwnerDrawText == "Disable Autohook")
            {
                GameHook.GameHookCheckTimer.Stop();
                GameHook.GameHookCheckTimer.Enabled = false;
                GameHook.GameHookCheckTimer.Dispose();
                Btn_AutoHook.OwnerDrawText = "Enable Autohook";
            }
        }

        private void OptionsScreenTinyUI_Load(object sender, EventArgs e)
        {
            ToolTip ToolTip1 = new ToolTip(); ToolTip1.SetToolTip(TinyUI_TxtSpc_Opacity, "The opacity of this application, 0.00-1.00 when it is not attached to the game.");
            ToolTip ToolTip2 = new ToolTip(); ToolTip2.SetToolTip(TinyUI_TxtSpc_OverlayOpacity, "Opacity of the application when it is attached to the game. When attached this automatically takes the value of the previous Window opacity.");
            ToolTip ToolTip3 = new ToolTip(); ToolTip3.SetToolTip(TinyUI_TxtSpc_HookDelay, "Time in milliseconds between each attempt of trying to automatically attach to Sonic Heroes.");
            ToolTip ToolTip4 = new ToolTip(); ToolTip4.SetToolTip(Btn_Hook, "Manually attach and become an overlay ontop of Sonic Heroes, Windowed & Borderless Fullscreen supported.");
        }

        private void Btn_DInputTest_Click(object sender, EventArgs e)
        {
            Btn_ToggleVisible.Text = "Press A Button";
            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.ToggleFormVisibility;
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_ToggleVisible;
        }

        public void ToggleVisibilityHotkey()
        {
            if (Program.xSmallMainWindow.Visible) { Program.xSmallMainWindow.Visible = false; } else { Program.xSmallMainWindow.Visible = true; } 
        }

        private void Btn_HotkeyTest_Click(object sender, EventArgs e)
        {
            Btn_HotkeyTest.Text = "Press A Button";

            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.DisplayTestMessage;
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_HotkeyTest;
        }

        public void TestMessage()
        {
            MessageBox.Show("If you see this then hotkey binding works correctly. If it keeps re-appearing until you release a button, please tell me.");
        }


    }
}
