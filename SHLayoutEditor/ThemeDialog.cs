using System;
namespace SHLayoutEditor
{
	public partial class ThemeDialog : Gtk.Window
	{
		public ThemeDialog() :
				base(Gtk.WindowType.Toplevel)
		{
			this.Build();
		}
	}
}
