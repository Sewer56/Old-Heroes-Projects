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
    public partial class WarpUtilities : Form
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

        public WarpUtilities()
        {
            InitializeComponent();
            this.BringToFront();
        }

        private void Btn_SelectLast_Click(object sender, EventArgs e)
        {
            Btn_SelectLast.Text = "Press A Button";
            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.SelectLastTeleport;
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_SelectLast;
        }

        private void Btn_SelectNext_Click(object sender, EventArgs e)
        {
            Btn_SelectNext.Text = "Press A Button";
            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.SelectNextTeleport;
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_SelectNext;
        }

        private void Btn_WarpToDestination_Click(object sender, EventArgs e)
        {
            Btn_WarpToDestination.Text = "Press A Button";
            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.WarpToDestination;
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_WarpToDestination;
        }

        private void Btn_SaveStatus_Click(object sender, EventArgs e)
        {
            Btn_SaveStatus.Text = "Press A Button";
            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.SaveWarpDestination;
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_SaveStatus;
        }

        private void Btn_ShowWarpOverlay_Click(object sender, EventArgs e)
        {
            ToggleWarpOverlay();
        }

        private void Btn_ToggleWarpOverlay_Click(object sender, EventArgs e)
        {
            Btn_ToggleWarpOverlay.Text = "Press A Button";
            Program.xSmallMainWindow.DirectInputDevicesHook.ControllerInputActionID = (int)DirectInputDevices.ControllerInputAction.IsAssigningButton;
            Program.xSmallMainWindow.DirectInputDevicesHook.ButtonActionAssignment = (int)DirectInputDevices.ControllerInputBindingsName.ToggleWarpOverlay;
            Program.xSmallMainWindow.DirectInputDevicesHook.EditModeButton = Btn_ToggleWarpOverlay;
        }

        /// Variables necessary for functions
        public SonicHeroesVariables.CharacterWarpInformation[] CurrentWarps = new SonicHeroesVariables.CharacterWarpInformation[Int16.MaxValue]; // All Warp Points
        public int MenuWarpIndex = 0; // Menu Warp Index
        public int MenuWarpCount = 0; // Menu Warp Count
        public WarpOverlay WarpInformationOverlay = new WarpOverlay(); // Warp info overlay

        /// <summary>
        /// Functions to perform on game.
        /// </summary>
        /// 
        public void ToggleWarpOverlay()
        {
            if (WarpInformationOverlay.Visible) { WarpInformationOverlay.Hide(); } else { WarpInformationOverlay.Show(); }
        }

        public void SelectNextTeleport()
        {
            if (GameHook.GameProcess != null)
            {
                if (GameHook.GameProcess.IsRunning == true)
                {
                    int Index = MenuWarpIndex + 1;
                    if (Index < MenuWarpCount)
                    {
                        MenuWarpIndex = Index;
                        WarpInformationOverlay.TxtBoxSmall_WarpName.Text = CurrentWarps[MenuWarpIndex].CharacterWarpName;
                    }
                }
            }
        }

        public void SelectLastTeleport()
        {
            if (GameHook.GameProcess != null)
            {
                if (GameHook.GameProcess.IsRunning == true)
                {
                    int Index = MenuWarpIndex - 1;
                    if (Index < MenuWarpCount && Index >= 0)
                    {
                        MenuWarpIndex = Index;
                        WarpInformationOverlay.TxtBoxSmall_WarpName.Text = CurrentWarps[MenuWarpIndex].CharacterWarpName;
                    }
                }
            }
        }

        public void TeleportToDestination()
        {
            if (GameHook.GameProcess != null)
            {
                if (GameHook.GameProcess.IsRunning == true)
                {
                    int PointerAddress = GameHook.GameProcess.Read<int>(SonicHeroesVariables.Characters.CurrentPlayerControlledCharacter_Pointer, false);
                    byte IsGameInAStage = GameHook.GameProcess.Read<byte>(SonicHeroesVariables.GameControl.AmICurrentlyInAStage, false);
                    if ( (PointerAddress != 0) && (IsGameInAStage == 1))
                    {
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XVelocity), CurrentWarps[MenuWarpIndex].XVelocity, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YVelocity), CurrentWarps[MenuWarpIndex].YVelocity, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.ZVelocity), CurrentWarps[MenuWarpIndex].ZVelocity, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XPosition), CurrentWarps[MenuWarpIndex].XPosition, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YPosition), CurrentWarps[MenuWarpIndex].YPosition, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.ZPosition), CurrentWarps[MenuWarpIndex].ZPosition, false);
                       GameHook.GameProcess.Write<ushort>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XRotation), CurrentWarps[MenuWarpIndex].XRotation, false);
                       GameHook.GameProcess.Write<ushort>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YRotation), CurrentWarps[MenuWarpIndex].YRotation, false);
                       GameHook.GameProcess.Write<ushort>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.ZRotation), CurrentWarps[MenuWarpIndex].ZRotation, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XCharacterThickness), CurrentWarps[MenuWarpIndex].XCharacterThickness, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YCharacterThickness), CurrentWarps[MenuWarpIndex].YCharacterThickness, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.TeamMatesFollowingSomething), CurrentWarps[MenuWarpIndex].TeamMatesFollowingSomething, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.TeamMatesFollowingSomething2), CurrentWarps[MenuWarpIndex].TeamMatesFollowingSomething2, false);
                       GameHook.GameProcess.Write<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.TeamMatesFollowingSomething3), CurrentWarps[MenuWarpIndex].TeamMatesFollowingSomething3, false);
                    }
                }
            }
        }
        public void SaveStatus()
        {
            if (GameHook.GameProcess != null)
            {
                if (GameHook.GameProcess.IsRunning == true)
                {
                    int PointerAddress = GameHook.GameProcess.Read<int>(SonicHeroesVariables.Characters.CurrentPlayerControlledCharacter_Pointer, false);
                    byte IsGameInAStage = GameHook.GameProcess.Read<byte>(SonicHeroesVariables.GameControl.AmICurrentlyInAStage, false);
                    if ((PointerAddress != 0) && (IsGameInAStage == 1))
                    {
                        SonicHeroesVariables.CharacterWarpInformation TempCharacterWarpX;
                        TempCharacterWarpX.XVelocity = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XVelocity), false);
                        TempCharacterWarpX.YVelocity = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YVelocity), false); ;
                        TempCharacterWarpX.ZVelocity = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.ZVelocity), false); ;
                        TempCharacterWarpX.XPosition = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XPosition), false); ;
                        TempCharacterWarpX.YPosition = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YPosition), false); ;
                        TempCharacterWarpX.ZPosition = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.ZPosition), false); ;
                        TempCharacterWarpX.XRotation = GameHook.GameProcess.Read<ushort>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XRotation), false); ;
                        TempCharacterWarpX.YRotation = GameHook.GameProcess.Read<ushort>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YRotation), false); ;
                        TempCharacterWarpX.ZRotation = GameHook.GameProcess.Read<ushort>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.ZRotation), false); ;
                        TempCharacterWarpX.XCharacterThickness = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.XCharacterThickness), false); ;
                        TempCharacterWarpX.YCharacterThickness = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.YCharacterThickness), false); ;
                        TempCharacterWarpX.TeamMatesFollowingSomething = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.TeamMatesFollowingSomething), false);
                        TempCharacterWarpX.TeamMatesFollowingSomething2 = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.TeamMatesFollowingSomething2), false);
                        TempCharacterWarpX.TeamMatesFollowingSomething3 = GameHook.GameProcess.Read<float>((IntPtr)(PointerAddress + (int)SonicHeroesVariables.CharacterPointerOffsets.TeamMatesFollowingSomething3), false);

                        TempCharacterWarpX.CharacterWarpName = "Warp " + MenuWarpCount;
                        WarpInformationOverlay.TxtBoxSmall_WarpName.Text = TempCharacterWarpX.CharacterWarpName;

                        CurrentWarps[MenuWarpCount] = TempCharacterWarpX;

                        MenuWarpIndex = MenuWarpCount;
                        MenuWarpCount += 1;
                    }
                }
            }
        }
    }
}
