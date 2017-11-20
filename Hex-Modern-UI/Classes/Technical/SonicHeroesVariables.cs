using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeroesGHConfigTool
{
    /// <summary>
    /// All of these pointers are NOT relative to the main module. Thus you may need to apply a 0x400000 base offset.
    /// </summary>
    public static class SonicHeroesVariables
    {
        public enum GreatestHits_ExecutableAddresses
        {
            SuperHardDefaultTeam = 0x502DA,
            TutorialStageDefaultTeam = 0x503EA,
            TornadoJumpToggle = 0x31CBE2,
            FieldOfView = 0x3869B4,
            WindowStyle = 0x46D88,
        }

        public enum GreatestHits_BorderStyles
        {
            /// <summary>
            /// Too lazy to do proper flags.
            /// </summary>
            Stock = 13107200, // 0x0000C800
            Borderless = -2147483648, // 0x00000080
            Resizable = 12845056, // 0x0000C400
            ResizableBorderless = 262144, // 0x00000400
        }

        public enum GreatestHits_Resolutions
        {
            Width1280 = 0x3C9290,
            Height1280 = 0x3C9294,

            // Fullscreen Res:

            WidthFullscreen1280 = 0x3C92E0,
            HeightFullscreen1280 = 0x3C92E4,
        }
    }
}
