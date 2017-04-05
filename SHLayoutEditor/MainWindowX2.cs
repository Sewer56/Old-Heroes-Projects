using System;
namespace SHLayoutEditor
{
	public partial class MainWindowX2 : Gtk.Window
	{
		public MainWindowX2() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
