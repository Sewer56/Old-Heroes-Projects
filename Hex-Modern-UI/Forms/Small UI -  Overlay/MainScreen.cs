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
using System.Diagnostics;

namespace HeroesGHConfigTool
{
    public partial class MainScreen : Form
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

        public MainScreen()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }

        private void TinyUI_ComboBoxSmall_Team_SelectedIndexChanged(object sender, EventArgs e)
        {
            TinyUI_ComboBoxSmall_SuperHardTeam.Select(0,0);
        }

        private void Btn_Exit_Click(object sender, EventArgs e) { Environment.Exit(0); }
        private void Btn_SaveExit_Click(object sender, EventArgs e) { File.WriteAllBytes("Tsonic_win.exe", Program.xSmallMainWindow.SonicHeroesExecutable); Program.xSmallMainWindow.SaveHeroesConfig(); Environment.Exit(0); }
        private void Btn_Launch_Click(object sender, EventArgs e) { File.WriteAllBytes("Tsonic_win.exe", Program.xSmallMainWindow.SonicHeroesExecutable); Program.xSmallMainWindow.SaveHeroesConfig(); Process.Start("Tsonic_win.exe"); Environment.Exit(0); }

        private void MainScreen_Shown(object sender, EventArgs e)
        {
            TinyUI_ComboBoxSmall_SuperHardTeam.SelectedIndex = Convert.ToByte(Program.xSmallMainWindow.SonicHeroesExecutable[(int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.SuperHardDefaultTeam]);
            TinyUI_ComboBoxSmall_TutorialTeam.SelectedIndex = Convert.ToByte(Program.xSmallMainWindow.SonicHeroesExecutable[(int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.TutorialStageDefaultTeam]);
            TinyUI_ComboBoxSmall_TornadoJump.SelectedIndex = Convert.ToByte(Program.xSmallMainWindow.SonicHeroesExecutable[(int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.TornadoJumpToggle]);
            TinyUI_TxtBoxSmall_FieldOfView.Text = Convert.ToString(BitConverter.ToSingle(Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.FieldOfView));
        }

        private void TinyUI_ComboBoxSmall_Team_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.SonicHeroesExecutable[(int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.SuperHardDefaultTeam] = (byte)TinyUI_ComboBoxSmall_SuperHardTeam.SelectedIndex;
        }

        private void TinyUI_ComboBoxSmall_Tutorial_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.SonicHeroesExecutable[(int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.TutorialStageDefaultTeam] = (byte)TinyUI_ComboBoxSmall_TutorialTeam.SelectedIndex;
        }

        private void TinyUI_ComboBoxSmall_TornadoJump_SelectionChangeCommitted(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.SonicHeroesExecutable[(int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.TornadoJumpToggle] = (byte)TinyUI_ComboBoxSmall_TornadoJump.SelectedIndex;
        }

        private void TinyUI_TxtBoxSmall_FieldOfView_Leave(object sender, EventArgs e)
        {
            try
            {
                Buffer.BlockCopy( BitConverter.GetBytes(Convert.ToSingle(TinyUI_TxtBoxSmall_FieldOfView.Text)), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.FieldOfView, 4);
            }
            catch (Exception Meme)
            {
                MessageBox.Show("Invalid Floating Point, the value has been reset to 1.");
                TinyUI_TxtBoxSmall_FieldOfView.Text = "1";
                TinyUI_TxtBoxSmall_FieldOfView.Select();
            }
        }
    }
}
