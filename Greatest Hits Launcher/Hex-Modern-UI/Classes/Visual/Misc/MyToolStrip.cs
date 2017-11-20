//Src: Last year's HangmanX project.
using System.Windows.Forms;
// This is a fix for the toolstrip renderer which has been having a known bug, in order to fix the appearance of the overall loading screen.

namespace HeroesGHConfigTool
{
    public class MyToolStrip : ToolStripSystemRenderer                                                          // Inherit properties from the default toolstrip renderer.
    {
        public MyToolStrip() { }                                                                                // Constructor to allow the creation of an instance of an object of this class.
        protected override void OnRenderToolStripBorder(ToolStripRenderEventArgs e) { }                         // Override the strip border method from the original toolstrip renderer intended for drawing the border with empty code.
    }
}