using System;
using Gtk;
namespace SHLayoutEditor
{
	public partial class MainWindowX2 : Gtk.Window
	{
		public PlatformID OSPlatformX;
		public MainWindowX2() : base(Gtk.WindowType.Toplevel)
		{
			this.Build();
			CheckOS();
		}

		public void CheckOS()
		{
            // Check for Windows, Linux or OSX
            OperatingSystem OS = System.Environment.OSVersion;
			OSPlatformX = OS.Platform;
			switch (OSPlatformX)
			{
				case PlatformID.Unix:
                    lbl_Windows.Text = "You're running on Linux or OSX, keep it up glorious brother. Would you like to use the builtin theme or your system theme?";
                    break;
				default:
                    break;
			}
        }

		// Keep Default appearance, I didn't rename method lel
		protected void OnBtnUseGTKClicked(object sender, EventArgs e)
		{
			ShowMainWindow();
		}

		public void ShowMainWindow() 
		{
			MainWindow MainWindowX = new MainWindow();
			MainWindowX.OSPlatform = OSPlatformX;
			MainWindowX.ShowAll();
			this.Destroy();
		}

		// Use built-in theme.
		protected void OnBtnUseBuiltInClicked(object sender, EventArgs e)
		{
			Gtk.Rc.AddDefaultFile(@"gtk-2.0/gtkrc"); 
			Gtk.Rc.Parse(@"gtk-2.0/gtkrc");
			ShowMainWindow();
		}
	}
}
