using System;
namespace SHLayoutEditor
{
	public partial class CloneEntry : Gtk.Dialog
	{
		MainWindow PassedInWindow;
		Gtk.ComboBox PassedInComboBox;
		public CloneEntry(MainWindow PassedInWindowX)
		{
			this.Build();
			PassedInWindow = PassedInWindowX;
		}

		protected void OnButtonCancelClicked(object sender, EventArgs e)
		{
			this.Destroy();
		}

		protected void OnButtonOkClicked(object sender, EventArgs e)
		{
			CloneToPosition();
			this.Destroy();
		}

		public void FillInComboBox(Gtk.ComboBox NameCloneComboBox) 
		{
			ClearComboBox(CmbBox_Item);
			Gtk.TreeIter TreeIteration;
			string SelectedText;

			for (int x = 0; x < NameCloneComboBox.Model.IterNChildren(); x++)
			{
				NameCloneComboBox.Model.IterNthChild(out TreeIteration, x);
				SelectedText = (string)NameCloneComboBox.Model.GetValue(TreeIteration, 0);
				CmbBox_Item.AppendText(SelectedText);
			}
		}

		private void CloneToPosition()
		{
			PassedInComboBox = PassedInWindow.GetComboBoxItemControl();

			// Get the current Data here
			GUIObjectScreen GUIScreen = new GUIObjectScreen();
			GUIScreen = PassedInWindow.GetObjectScreen();

			PassedInWindow.ComboBoxInvokerX = "null";
			PassedInComboBox.Active = CmbBox_Item.Active; // Set active in child window to active in clone window.
            PassedInWindow.ComboBoxObjectLastItemX = CmbBox_Item.Active; // MUST BE HERE SUCH THAT NEW ENTRY SAVES.
            PassedInWindow.SetObjectScreen(GUIScreen.PositionX, GUIScreen.PositionY, GUIScreen.PositionZ, GUIScreen.RotationX, GUIScreen.RotationY, GUIScreen.RotationZ, GUIScreen.ObjectID, GUIScreen.ObjectList, GUIScreen.LinkingID, GUIScreen.LoadDistanceMultiplier, GUIScreen.PropertyID,
			                               GUIScreen.IgnoredValue1, GUIScreen.IgnoredValue2, GUIScreen.ObjectTeam1, GUIScreen.ObjectTeam2, GUIScreen.MysteryByte, GUIScreen.MysteryByte2, GUIScreen.MysteryFourBytes1, GUIScreen.MysteryFourBytes2);
			PassedInWindow.ComboBoxInvokerX = "default";
		}

		public void ClearComboBox(Gtk.ComboBox ComboBoxX)
		{
			for (int x = 0; x < 4096; x++)
			{
				try { ComboBoxX.RemoveText(0); } // Try clearing current entry.
				catch (Exception E) { x = 4096; } // End the loop if exception.
			}
		}
	}

	public class GUIObjectScreen
	{
		public double PositionX;
		public double PositionY;
		public double PositionZ;
		public double RotationX;
		public double RotationY;
		public double RotationZ;
		public double ObjectID;
		public double ObjectList;
		public double LinkingID;
		public double LoadDistanceMultiplier;
		public double PropertyID;
		public double IgnoredValue1;
		public double IgnoredValue2;
		public int ObjectTeam1;
		public int ObjectTeam2;
		public double MysteryByte;
		public double MysteryByte2;
		public double MysteryFourBytes1;
		public double MysteryFourBytes2;
	}
}
