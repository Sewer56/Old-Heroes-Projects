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
    public partial class TweaksScreen : Form
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

        public TweaksScreen()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }

        private void MainScreen_Shown(object sender, EventArgs e)
        {
            int Width = BitConverter.ToInt32(Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_Resolutions.Width1280);
            int Height = BitConverter.ToInt32(Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_Resolutions.Height1280);
            TinyUI_TxtBoxSmaller_ResolutionWidth.Text = Width + "x" + Height;
            TinyUI_ComboBoxSmall_AnisotropicFilter.SelectedIndex = Program.xSmallMainWindow.ConfigFile.AnisotropicFiltering;
            TinyUI_ComboBoxSmall_FogEmulation.SelectedIndex = Program.xSmallMainWindow.ConfigFile.FogEmulation;
            TinyUI_ComboBoxSmall_SoftShadows.SelectedIndex = Program.xSmallMainWindow.ConfigFile.SoftShadows;
            TinyUI_ComboBoxSmall_Fullscreen.SelectedIndex = Program.xSmallMainWindow.ConfigFile.FullScreen;
            TinyUI_ComboBoxSmall_ClippingSetting.SelectedIndex = Program.xSmallMainWindow.ConfigFile.ClipRange;
            TinyUI_ComboBoxSmall_FrameRate.SelectedIndex = Program.xSmallMainWindow.ConfigFile.FrameRate;
            TinyUI_ComboBoxSmall_SurroundSound.SelectedIndex = Program.xSmallMainWindow.ConfigFile.SurroundSound;
        }

        private void TinyUI_TxtBoxSmaller_ResolutionWidth_Leave(object sender, EventArgs e)
        {
            try
            {
                int Width = Convert.ToInt32(TinyUI_TxtBoxSmaller_ResolutionWidth.Text.Substring(0, TinyUI_TxtBoxSmaller_ResolutionWidth.Text.IndexOf("x")));
                int Height = Convert.ToInt32(TinyUI_TxtBoxSmaller_ResolutionWidth.Text.Substring(TinyUI_TxtBoxSmaller_ResolutionWidth.Text.IndexOf("x") + 1));
                Buffer.BlockCopy(BitConverter.GetBytes(Width), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_Resolutions.Width1280, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Width), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_Resolutions.WidthFullscreen1280, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Height), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_Resolutions.Height1280, 4);
                Buffer.BlockCopy(BitConverter.GetBytes(Height), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_Resolutions.HeightFullscreen1280, 4);
            }
            catch { MessageBox.Show("Invalid Resolution! It has been reset to 1920x1080."); TinyUI_TxtBoxSmaller_ResolutionWidth.Text = "1920x1080"; }
        }

        private void TweaksScreen_Leave(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.ConfigFile.AnisotropicFiltering = (byte)TinyUI_ComboBoxSmall_AnisotropicFilter.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.FogEmulation = (byte)TinyUI_ComboBoxSmall_FogEmulation.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.SoftShadows = (byte)TinyUI_ComboBoxSmall_SoftShadows.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.FullScreen = (byte)TinyUI_ComboBoxSmall_Fullscreen.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.ClipRange = (byte)TinyUI_ComboBoxSmall_ClippingSetting.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.FrameRate = (byte)TinyUI_ComboBoxSmall_FrameRate.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.SurroundSound = (byte)TinyUI_ComboBoxSmall_SurroundSound.SelectedIndex;
        }
    }
}
