using Gtk;

namespace SHLayoutEditor
{
	class MainClass
	{
		public static void Main(string[] args)
        {
            Application.Init();
			MainWindowX2 ThemeSelection = new MainWindowX2();
			ThemeSelection.Show();
			Application.Run(); 
		}
    }
}
