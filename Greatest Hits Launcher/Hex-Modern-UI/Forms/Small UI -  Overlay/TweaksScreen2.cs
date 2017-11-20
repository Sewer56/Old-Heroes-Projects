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
    public partial class TweaksScreen2 : Form
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

        public TweaksScreen2()
        {
            InitializeComponent();
            CenterToScreen();
            this.BringToFront();
        }

        private void MainScreen_Shown(object sender, EventArgs e)
        {
            TinyUI_ComboBoxSmall_Language.SelectedIndex = Program.xSmallMainWindow.ConfigFile.Language;
            TinyUI_ComboBoxSmall_BGM.SelectedIndex = Program.xSmallMainWindow.ConfigFile.BGMToggle;
            TinyUI_ComboBoxSmall_SEVoice.SelectedIndex = Program.xSmallMainWindow.ConfigFile.SFXToggle;
            TinyUI_TrackBar_BGM.Value = Program.xSmallMainWindow.ConfigFile.BGMVolume;
            TinyUI_TrackBar_SEVoice.Value = Program.xSmallMainWindow.ConfigFile.SFXVolume;

            switch ( BitConverter.ToInt32 (Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.WindowStyle) )
            {
                case (Int32)SonicHeroesVariables.GreatestHits_BorderStyles.Stock:
                    TinyUI_ComboBoxSmall_BorderStyle.SelectedIndex = 0;
                    break;
                case (Int32)SonicHeroesVariables.GreatestHits_BorderStyles.Borderless:
                    TinyUI_ComboBoxSmall_BorderStyle.SelectedIndex = 1;
                    break;
                case (Int32)SonicHeroesVariables.GreatestHits_BorderStyles.Resizable:
                    TinyUI_ComboBoxSmall_BorderStyle.SelectedIndex = 2;
                    break;
                case (Int32)SonicHeroesVariables.GreatestHits_BorderStyles.ResizableBorderless:
                    TinyUI_ComboBoxSmall_BorderStyle.SelectedIndex = 3;
                    break;
            }
        }

        private void TweaksScreen_Leave(object sender, EventArgs e)
        {
            Program.xSmallMainWindow.ConfigFile.Language = (byte)TinyUI_ComboBoxSmall_Language.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.BGMToggle = (byte)TinyUI_ComboBoxSmall_BGM.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.SFXToggle = (byte)TinyUI_ComboBoxSmall_SEVoice.SelectedIndex;
            Program.xSmallMainWindow.ConfigFile.BGMVolume = (byte)TinyUI_TrackBar_BGM.Value;
            Program.xSmallMainWindow.ConfigFile.SFXVolume = (byte)TinyUI_TrackBar_SEVoice.Value;

            switch (TinyUI_ComboBoxSmall_BorderStyle.SelectedIndex)
            {
                case 0:
                    Buffer.BlockCopy( BitConverter.GetBytes((Int32)SonicHeroesVariables.GreatestHits_BorderStyles.Stock), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.WindowStyle, 4);
                    break;
                case 1:
                    Buffer.BlockCopy(BitConverter.GetBytes((Int32)SonicHeroesVariables.GreatestHits_BorderStyles.Borderless), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.WindowStyle, 4);
                    break;
                case 2:
                    Buffer.BlockCopy(BitConverter.GetBytes((Int32)SonicHeroesVariables.GreatestHits_BorderStyles.Resizable), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.WindowStyle, 4);
                    break;
                case 3:
                    Buffer.BlockCopy(BitConverter.GetBytes((Int32)SonicHeroesVariables.GreatestHits_BorderStyles.ResizableBorderless), 0, Program.xSmallMainWindow.SonicHeroesExecutable, (int)SonicHeroesVariables.GreatestHits_ExecutableAddresses.WindowStyle, 4);
                    break;
            }
        }

        private void TinyUI_TrackBar_SEVoice_ValueChanged(object sender, EventArgs e)
        {
            TxtMini_TrackBarCounterSEVoice.Text = "SE/Voice Volume: " + Convert.ToString(TinyUI_TrackBar_SEVoice.Value);
        }

        private void TinyUI_TrackBar_BGM_ValueChanged(object sender, EventArgs e)
        {
            TxtMini_TrackBarCounterBGM.Text = "Background Music Volume: " + Convert.ToString(TinyUI_TrackBar_BGM.Value);
        }
    }
}
