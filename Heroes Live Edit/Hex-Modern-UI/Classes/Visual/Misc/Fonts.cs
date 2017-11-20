using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;

namespace Hex_Modern_UI
{
    class Fonts
    {
        public Font RobotoRegular;                                                                                  // Create a font type object used for regular text.
        public Font RobotoCondensed;                                                                                // Create a font type object used for button text.
        public Font RobotoLabel;                                                                                    // Create a font collection to hold the label/bold text in menus.
        public Font RobotoRegularLarge;                                                                             // Create a font type object used for regular text.
        public Font RobotoCondensedLarge;                                                                           // Create a font type object used for button text.
        public Font RobotoLight;                                                                                    // Create a font type object used for the Roboto Light font.
        public Font RobotoLightLarge;                                                                               // Create a font type object used for the Roboto Light font (large)..
        public Font RobotoThin;                                                                                     // Create a font type object used for the Roboto Thin font

        public Font RobotoTopTitle;
        public Font RobotoTitleLabel;
        public Font RobotoTitleLabelSmall;

        public Font RobotoLargeTextbox;                                                                               // Create a font type object used for the Roboto Light font (large).
        public Font RobotoMidTextbox;

        public Font RobotoLargeButton;
        public Font RobotoSmallTextbox;
        public Font RobotoSmallTextboxLabel;

        public Font RobotoRegularText;


        // TinyUI
        public Font RobotoTopTitleTinyUI;
        public Font RobotoSpecialTextTinyUI;
        public Font RobotoNumericUpDownTinyUI;

        PrivateFontCollection Roboto = new PrivateFontCollection();                                                 // Create a font collection to hold the Roboto font.

                                                                                                                    // ---------------------------------------------------------------------------------
        public void SetupFonts()                                                                                    // Method used to load and set up all the fonts that will be used in the application
        {                                                                                                           // ---------------------------------------------------------------------------------
            try
            {
                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\RobotoCondensed-Regular.ttf");   // Load the Roboto Condensed font into the PrivateFontCollection.
                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\RobotoCondensed-Italic.ttf");    // Load the Roboto Condensed Italic font into the PrivateFontCollection.
                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\RobotoCondensed-Bold.ttf");      // Load the Roboto Condensed Bold font into the PrivateFontCollection.
                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\RobotoCondensed-BoldItalic.ttf");// Load the Roboto Condensed Bold Italic font into the PrivateFontCollection.

                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\Roboto-Regular.ttf");            // Load the Roboto font into the PrivateFontCollection.
                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\Roboto-Bold.ttf");               // Load the Roboto Bold font into the PrivateFontCollection.
                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\Roboto-Light.ttf");              // Load the Roboto light font into the PrivateFontCollection.
                Roboto.AddFontFile(AppDomain.CurrentDomain.BaseDirectory + @"Fonts\Roboto-Thin.ttf");               // Load the Roboto thin font into the PrivateFontCollection.

                // Regular font styles

                RobotoRegular = new Font(Roboto.Families[0], 12);                                                   // Set the Roboto font to the Roboto font family as present in the PrivateFontCollection.
                RobotoCondensed = new Font(Roboto.Families[1], 12);                                                 // Set the Roboto condensed font to the Roboto condensed font family as present in the PrivateFontCollection.
                RobotoLight = new Font(Roboto.Families[2], 12);                                                     // Set the Roboto condensed font to the Roboto condensed font family as present in the PrivateFontCollection.
                RobotoThin = new Font(Roboto.Families[3], 12);                                                      // Set the Roboto label font identically to the Roboto Condensed font but of size 8.5pt.

                // Regular Large Font Styles

                RobotoRegularLarge = new Font(Roboto.Families[0], 24);                                              // Set the Roboto font to the Roboto font family as present in the PrivateFontCollection.
                RobotoCondensedLarge = new Font(Roboto.Families[1], 24);                                            // Set the Roboto condensed font to the Roboto condensed font family as present in the PrivateFontCollection.
                RobotoLightLarge = new Font(Roboto.Families[2], 24);                                                // Set the Roboto light large font to the Roboto condensed font family as present in the PrivateFontCollection.

                // Custom Fonts  
                RobotoLargeTextbox = new Font(Roboto.Families[2], 20F);
                RobotoLargeButton = new Font(Roboto.Families[0], 27F);
                RobotoMidTextbox = new Font(Roboto.Families[0], 21.75F);                                              
                RobotoTopTitle = new Font(Roboto.Families[2], 48);
                RobotoTitleLabel = new Font(Roboto.Families[3], 44);
                RobotoTitleLabelSmall = new Font(Roboto.Families[3], 36);

                RobotoSmallTextboxLabel = new Font(Roboto.Families[2], 20.25F);                            
                RobotoSmallTextbox = new Font(Roboto.Families[2], 16F);                                                
                RobotoRegularText = new Font(Roboto.Families[2], 20.25F);

                // TinyUI
                RobotoTopTitleTinyUI = new Font(Roboto.Families[2], 20.25F);
                RobotoSpecialTextTinyUI = new Font(Roboto.Families[2], 15.75F);
                RobotoNumericUpDownTinyUI = new Font(Roboto.Families[1], 9.75F);
            }
            catch (Exception)
            {
                MessageBox.Show("The font files could not be found or loaded. This is bad, a very dire situation indeed.");
                // Inform the user that the font could not be found or loaded.
                RobotoRegular = new Font("Microsoft Sans Serif", 8);                                                 // Load Microsoft Sans Serif in place of the Roboto Regular font.
                RobotoCondensed = new Font("Microsoft Sans Serif", 11);                                               // Load Microsoft Sans Serif in place of the Roboto Condensed font.
                RobotoLabel = new Font("Microsoft Sans Serif", 8);                                                    // Load Microsoft Sans Serif in place of the Roboto Label font.
            }
        }

        /*
        public void SetupMainWindow(MainForm xMainWindow)
        {
            xMainWindow.lbl_ActionBar_BottomRight.Font = RobotoCondensed;                                                    // Set the font of the ActionBar/Toolstrip bottom right label to the Roboto Condensed font.
        }
        */

        public void SetupMainWindow(MainFormSmall xMainWindow)
        {
            xMainWindow.lbl_ActionBar_BottomRight.Font = RobotoCondensed;                                                    // Set the font of the ActionBar/Toolstrip bottom right label to the Roboto Condensed font.
        }
    }
}
