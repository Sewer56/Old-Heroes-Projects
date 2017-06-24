using System;
using System.Windows.Forms;

namespace HeroesGHConfigTool
{
    public partial class ControllerScreen : Form
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

        public ControllerScreen()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }

        private void SetGenericButtonAction()
        {
            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.Null;
        }

        private void Btn_SaveExit_Click(object sender, EventArgs e)
        {
            Btn_StartPause.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_StartPause;
        }

        private void Btn_Jump_Click(object sender, EventArgs e)
        {
            Btn_Jump.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_Jump;
        }

        private void Btn_Action_Click(object sender, EventArgs e)
        {
            Btn_Action.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_Action;
        }

        private void Btn_TeamBlast_Click(object sender, EventArgs e)
        {
            Btn_TeamBlast.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_TeamBlast;
        }

        private void Btn_FormationL_Click(object sender, EventArgs e)
        {
            Btn_FormationL.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_FormationL;
        }

        private void Btn_FormationR_Click(object sender, EventArgs e)
        {
            Btn_FormationR.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_FormationR;
        }

        private void Btn_CameraL_Click(object sender, EventArgs e)
        {
            Btn_CameraL.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_CameraL;
        }

        private void Btn_CameraR_Click(object sender, EventArgs e)
        {
            Btn_CameraR.Text = "Press A Button";
            SetGenericButtonAction();
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_CameraR;
        }

        private void ControllerScreen_Leave(object sender, EventArgs e)
        {
            // Try-catch is used in the case user goes to another tab without setting a button on one of the controls.
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[0] = Convert.ToByte(Btn_StartPause.OwnerDrawText.Substring(Btn_StartPause.OwnerDrawText.IndexOf(" "))); } catch { }
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[1] = Convert.ToByte(Btn_Jump.OwnerDrawText.Substring(Btn_Jump.OwnerDrawText.IndexOf(" "))); } catch { }
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[2] = Convert.ToByte(Btn_Action.OwnerDrawText.Substring(Btn_Action.OwnerDrawText.IndexOf(" "))); } catch { }
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[3] = Convert.ToByte(Btn_FormationR.OwnerDrawText.Substring(Btn_FormationR.OwnerDrawText.IndexOf(" ")));} catch { }
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[4] = Convert.ToByte(Btn_FormationL.OwnerDrawText.Substring(Btn_FormationL.OwnerDrawText.IndexOf(" ")));} catch { }
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[5] = Convert.ToByte(Btn_TeamBlast.OwnerDrawText.Substring(Btn_TeamBlast.OwnerDrawText.IndexOf(" ")));} catch { }
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[6] = Convert.ToByte(Btn_CameraR.OwnerDrawText.Substring(Btn_CameraR.OwnerDrawText.IndexOf(" ")));} catch { }
            try { Program.xSmallMainWindow.ConfigFile.ControllerOne[7] = Convert.ToByte(Btn_CameraL.OwnerDrawText.Substring(Btn_CameraL.OwnerDrawText.IndexOf(" ")));} catch { }
        }

        private void ControllerScreen_Enter(object sender, EventArgs e)
        { 
            Btn_StartPause.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[0];
            Btn_Jump.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[1];
            Btn_Action.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[2];
            Btn_FormationR.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[3];
            Btn_FormationL.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[4];
            Btn_TeamBlast.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[5];
            Btn_CameraR.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[6];
            Btn_CameraL.Text = "Button " + Program.xSmallMainWindow.ConfigFile.ControllerOne[7];
        }
    }
}
