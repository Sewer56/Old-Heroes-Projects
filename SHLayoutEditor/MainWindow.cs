using System;
using Gtk;
using System.Collections.Generic;
using System.IO;
using SHLayoutEditor;
using System.Diagnostics;

public partial class MainWindow : Window
{
    string CurrentFile;
	public PlatformID OSPlatform;
    static List<OBJObject> StageObjects = new List<OBJObject>();           // Objects read from files
    static List<MiscProperty> ObjectProperties = new List<MiscProperty>(); // Object properties/misc read from files

	// Used for select by features.
	static List<ListClass> ObjectLists = new List<ListClass>();            // Object lists
    static List<ObjectEntryClass> ObjectIDs = new List<ObjectEntryClass>();// Global Object Name/ID List
    static List<ObjectEntryClass> ListObjects = new List<ObjectEntryClass>(); // Stores objects in list

	static string ComboBoxInvoker; // Used in a few places to ensure unnecessary code is not ran.
	static int ComboBoxObjectLastItem = 1; // Last selected item on top combobox.
	static int ComboBoxLastPropertyItem = 1; // Last selected property item.
	static int PropertyDataType = 1;

	public MainWindow() : base(WindowType.Toplevel)
	{
		Build();
		SetupUIElements();
        PopulateLists();
		PopulateComboboxes();
	}

	public void SetupUIElements()
	{
		CmbBox_Item.HasTooltip = true;
		CmbBox_Item.TooltipText = "Select the item to modify!";
	}

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
	{
		Application.Quit();
		a.RetVal = true;
	}

    public void ConvertBinToTXT(string CurrentFile)
    {
        Process ProcessX;
        switch (OSPlatform)
        {
            case PlatformID.Unix:
                try
                {
                    ProcessX = Process.Start
                    (
                        new ProcessStartInfo
                        (
                            AppDomain.CurrentDomain.BaseDirectory + @"/Tools/Linux/HeroesOBJ",
                            @"--extract -i " + CurrentFile + " -o " + CurrentFile + ".txt"
                        )
                        { UseShellExecute = false }
                    );
                    ProcessX.WaitForExit();
                }
                catch { }
                break;
            default:
                try
                {
                    ProcessX = Process.Start
                    (
                        new ProcessStartInfo
                        (
                            AppDomain.CurrentDomain.BaseDirectory + @"\Tools\Windows\HeroesOBJ.exe",
                            @"--extract -i " + CurrentFile + " -o " + CurrentFile + ".txt"
                        )
                        { UseShellExecute = false }
                    );
                    ProcessX.WaitForExit();
                }
                catch { }
                break;
        }
    }

	public void ConvertTXTToBin(string CurrentFile)
	{ 
	    Process ProcessX;
		switch (OSPlatform)
		{
			case PlatformID.Unix:
				ProcessX = Process.Start
				(
					new ProcessStartInfo
					(
						AppDomain.CurrentDomain.BaseDirectory + @"/Tools/Linux/HeroesOBJ",
							@"--compile -i " + CurrentFile + " -o " + CurrentFile.Substring(0, CurrentFile.Length - 4)
					)
					{ UseShellExecute = false }
				);
				ProcessX.WaitForExit();
				break;
			default:
				ProcessX = Process.Start
				(
					new ProcessStartInfo
					(
						AppDomain.CurrentDomain.BaseDirectory + @"\Tools\Windows\HeroesOBJ.exe",
						@"--compile -i " + CurrentFile + " -o " + CurrentFile.Substring(0, CurrentFile.Length - 4)
					)
					{ UseShellExecute = false }
				);
				ProcessX.WaitForExit();
				break;
		}
	}

	protected void OnBtnOpenFileClicked(object sender, EventArgs e)
	{
		Gtk.FileChooserDialog OBJFileChooser = new Gtk.FileChooserDialog("Choose the Heroes OBJ File to Open", this, FileChooserAction.Open, "Cancel", ResponseType.Cancel, "Open", ResponseType.Accept);

		var result = OBJFileChooser.Run();

        if (result == (int)ResponseType.Accept)
        {
            // Converts from .bin to .txt if necessary
            CurrentFile = OBJFileChooser.Filename;

            /// If .bin, decompile
            int ExtensionIndex = CurrentFile.LastIndexOf(@".", StringComparison.OrdinalIgnoreCase);
            if (CurrentFile.Substring(ExtensionIndex + 1) == "bin")
            {
                ConvertBinToTXT(CurrentFile);
                CurrentFile = OBJFileChooser.Filename + ".txt";
            }
            result = (int)ResponseType.Accept;

            string CurrentFile2 = CurrentFile;
            StreamReader OBJFile = new StreamReader(CurrentFile);
            int Index = CurrentFile.LastIndexOf(@"/", StringComparison.OrdinalIgnoreCase); // File path's last index of forward slash.

            /// Resolves if back or forward slashes in file path.
            /// 
            if (Index != -1)
            {
                CurrentFile = CurrentFile.Substring(Index + 1);
            }
            else
            {
                CurrentFile = CurrentFile.Substring(CurrentFile.LastIndexOf(@"\", StringComparison.OrdinalIgnoreCase) + 1);
            }
            lbl_Statusbar3.Text = "Current File | " + CurrentFile;
            ////
            // Read OBJ Layout Text File Here
            ////
            ReadObjectFile(OBJFile);

            CurrentFile = CurrentFile2; 
            OBJFile.Close();
            OBJFile.Dispose();
        }

		OBJFileChooser.Destroy();
		PopulateObjects(); // Populates the objects dropdown
		PopulateMisc(); // Populates the misc dropdown
	}

	void PopulateObjects() // Populates the objects dropdown
	{
		ComboBoxInvoker = "null";
		string ObjectNameString;
		ClearComboBox(CmbBox_Item); // Clear any existing items.
		for (int x = 0; x < StageObjects.Count; x++)
		{
            ObjectNameString = "|" + x.ToString("D4") + "| " + "(" + StageObjects[x].ObjectXCoordinateX + "," + " " + StageObjects[x].ObjectYCoordinateX + "," + " " + StageObjects[x].ObjectZCoordinateX + ") " + GetObjectName(StageObjects[x].ObjectTypeIDX, StageObjects[x].ObjectListX);
			CmbBox_Item.AppendText(ObjectNameString);
		}
		ComboBoxInvoker = "donotsave";
		CmbBox_Item.Active = 0;
		ComboBoxInvoker = "default";
	}

	void PopulateMisc() // Populates the misc dropdown
	{
		ComboBoxInvoker = "null";
		string ObjectNameString;
		ClearComboBox(CmbBox_Property); // Clear any existing items.
		for (int x = 0; x < ObjectProperties.Count; x++)
		{
			var ObjectIndex = StageObjects.FindIndex(item => item.PropertyIDX == x);

			if (ObjectIndex != -1) { ObjectNameString = "Property: " + x + " | " + GetObjectName(StageObjects[ObjectIndex].ObjectTypeIDX, StageObjects[ObjectIndex].ObjectListX); }
			else { ObjectNameString = "Property: " + x + " | " + "Unused"; }

			CmbBox_Property.AppendText(ObjectNameString);
		}
		ComboBoxInvoker = "combobox";
		CmbBox_Property.Active = 0;
		ComboBoxInvoker = "default";
	}

	protected void OnCmbBoxPropertyChanged(object sender, EventArgs e)
	{
        
		if (ComboBoxInvoker != "donotsave") { SaveLastProperty(); } // Saves the values in the last itemboxes to the array.
		if (ComboBoxInvoker == "combobox") { ComboBoxLastPropertyItem = (int)SpinBtn_PropertyID.Value; } // This path is taken if user swtiches current object.
		else { ComboBoxLastPropertyItem = CmbBox_Property.Active; } // This lets the user adjust from manual control.

		// Revert back to floats.
		PropertyDataType = 0;
		Btn_ViewAsFloats.Sensitive = false;
		Btn_ViewAsIntegers.Sensitive = true;

		try
		{
			SpinBtn_MiscActivator.Value = ObjectProperties[ComboBoxLastPropertyItem].ActivatorX;
			SpinBtn_MiscEntryID.Value = ObjectProperties[ComboBoxLastPropertyItem].EntryIDX;

			SpinBtn_ObjectProperty1.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer1X), 0);
			SpinBtn_ObjectProperty2.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer2X), 0);
			SpinBtn_ObjectProperty3.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer3X), 0);
			SpinBtn_ObjectProperty4.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer4X), 0);
			SpinBtn_ObjectProperty5.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer5X), 0);
			SpinBtn_ObjectProperty6.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer6X), 0);
			SpinBtn_ObjectProperty7.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer7X), 0);
			SpinBtn_ObjectProperty8.Value = BitConverter.ToSingle(BitConverter.GetBytes(ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer8X), 0);
		}
		catch
		{

		}
        
	}

	protected void OnBtnViewAsFloatsReleased(object sender, EventArgs e)
	{
		PropertyDataType = 0;
		Btn_ViewAsFloats.Sensitive = false;
		Btn_ViewAsIntegers.Sensitive = true;
		int TmpValue1 = (int)SpinBtn_ObjectProperty1.Value;
		int TmpValue2 = (int)SpinBtn_ObjectProperty2.Value;
		int TmpValue3 = (int)SpinBtn_ObjectProperty3.Value;
		int TmpValue4 = (int)SpinBtn_ObjectProperty4.Value;
		int TmpValue5 = (int)SpinBtn_ObjectProperty5.Value;
		int TmpValue6 = (int)SpinBtn_ObjectProperty6.Value;
		int TmpValue7 = (int)SpinBtn_ObjectProperty7.Value;
		int TmpValue8 = (int)SpinBtn_ObjectProperty8.Value;
		SpinBtn_ObjectProperty1.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue1), 0);
		SpinBtn_ObjectProperty2.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue2), 0);
		SpinBtn_ObjectProperty3.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue3), 0);
		SpinBtn_ObjectProperty4.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue4), 0);
		SpinBtn_ObjectProperty5.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue5), 0);
		SpinBtn_ObjectProperty6.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue6), 0);
		SpinBtn_ObjectProperty7.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue7), 0);
		SpinBtn_ObjectProperty8.Value = BitConverter.ToSingle(BitConverter.GetBytes(TmpValue8), 0);
	}

	protected void OnBtnViewAsIntegersReleased(object sender, EventArgs e)
	{
		PropertyDataType = 1;
		Btn_ViewAsFloats.Sensitive = true;
		Btn_ViewAsIntegers.Sensitive = false;
		float TmpValue1 = (float)SpinBtn_ObjectProperty1.Value;
		float TmpValue2 = (float)SpinBtn_ObjectProperty2.Value;
		float TmpValue3 = (float)SpinBtn_ObjectProperty3.Value;
		float TmpValue4 = (float)SpinBtn_ObjectProperty4.Value;
		float TmpValue5 = (float)SpinBtn_ObjectProperty5.Value;
		float TmpValue6 = (float)SpinBtn_ObjectProperty6.Value;
		float TmpValue7 = (float)SpinBtn_ObjectProperty7.Value;
		float TmpValue8 = (float)SpinBtn_ObjectProperty8.Value;
		SpinBtn_ObjectProperty1.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue1), 0);
		SpinBtn_ObjectProperty2.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue2), 0);
		SpinBtn_ObjectProperty3.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue3), 0);
		SpinBtn_ObjectProperty4.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue4), 0);
		SpinBtn_ObjectProperty5.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue5), 0);
		SpinBtn_ObjectProperty6.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue6), 0);
		SpinBtn_ObjectProperty7.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue7), 0);
		SpinBtn_ObjectProperty8.Value = BitConverter.ToUInt32(BitConverter.GetBytes(TmpValue8), 0);
	}

	string GetObjectName(int ObjectID, int ObjectList)
	{
		for (int x = 0; x < ObjectIDs.Count; x++)
		{
			if (ObjectIDs[x].ObjectIDX == ObjectID && ObjectIDs[x].ListIDX == ObjectList)

			{ return ObjectIDs[x].ObjectNameX; }
		}
		return "No Object | You shouldn't see this";
	}

    protected void OnBtnSaveFileClicked(object sender, EventArgs e)
    {
		SaveLastItem();
		SaveLastProperty();
        WriteObjectFile();
    }

    private void WriteObjectFile()
    {
        using (FileStream OBJStream = new FileStream(CurrentFile, FileMode.Create))
        {
            using (StreamWriter OBJFile = new StreamWriter(OBJStream))
            {
                OBJFile.AutoFlush = true;
                int x = 1;
                foreach (OBJObject HeroesObject in StageObjects)
                {
                    OBJFile.Write("[Object " + x + "]" + "\n");
                    OBJFile.Write("Object X Coordinate: " + HeroesObject.ObjectXCoordinateX + "\n");
                    OBJFile.Write("Object Y Coordinate: " + HeroesObject.ObjectYCoordinateX + "\n");
                    OBJFile.Write("Object Z Coordinate: " + HeroesObject.ObjectZCoordinateX + "\n");

                    OBJFile.Write("Object X Rotation Degrees: " + HeroesObject.ObjectXRotationX + "\n");
                    OBJFile.Write("Object Y Rotation Degrees: " + HeroesObject.ObjectYRotationX + "\n");
                    OBJFile.Write("Object Z Rotation Degrees: " + HeroesObject.ObjectZRotationX + "\n");

                    OBJFile.Write("----------------------\n");
                    OBJFile.Write("Unknown Ignored Value: " + HeroesObject.IgnoredValue1X + "\n");
                    OBJFile.Write("Object's Associated Team: " + "0x" + HeroesObject.ObjectTeam1X.ToString("X2") + "\n");
                    OBJFile.Write("Mystery Object Byte: " + "0x" + HeroesObject.MysteryByte1X.ToString("X2") + "\n");
                    OBJFile.Write("Four Bytes of Mystery: " + "0x" + HeroesObject.FourBytesOfMystery1X.ToString("X8") + "\n");
                    OBJFile.Write("----------------------\n");
                    OBJFile.Write("Unknown Ignored Value #2: " + HeroesObject.IgnoredValue2X + "\n");
                    OBJFile.Write("Object's Associated Team #2: " + "0x" + HeroesObject.ObjectTeam2X.ToString("X2") + "\n");
                    OBJFile.Write("Mystery Object Byte #2: " + "0x" + HeroesObject.MysteryByte2X.ToString("X2") + "\n");
                    OBJFile.Write("Four Bytes of Mystery #2: " + "0x" + HeroesObject.FourBytesOfMystery2X.ToString("X8") + "\n");
                    OBJFile.Write("----------------------\n");

                    OBJFile.Write("Object Grouping/List: " + "0x" + HeroesObject.ObjectListX.ToString("X2") + "\n");
                    OBJFile.Write("Object Type/ID: " + "0x" + HeroesObject.ObjectTypeIDX.ToString("X2") + "\n");
                    OBJFile.Write("Object Linking ID: " + "0x" + HeroesObject.LinkingIDX.ToString("X2") + "\n");
                    OBJFile.Write("Object Load Distance Multiplier: " + "0x" + HeroesObject.LoadDistanceMultiplierX.ToString("X2") + "\n");
                    OBJFile.Write("Object Property Section ID: " + HeroesObject.PropertyIDX + "\n");

                    OBJFile.Write("\n"); // Write new object
                    x += 1; // Add to x
                }
            }
        }

        using (FileStream OBJStream = new FileStream(CurrentFile, FileMode.Append))
        {
            StreamWriter OBJFile = new StreamWriter(OBJStream);
			OBJFile.AutoFlush = true;
            int x = 1;
            foreach (MiscProperty HeroesProperty in ObjectProperties)
            {
                OBJFile.Write("[Misc " + x + "]" + "\n");
                OBJFile.Write("Activator: " + HeroesProperty.ActivatorX + "\n");
                OBJFile.Write("Entry ID: " + HeroesProperty.EntryIDX + "\n");

				OBJFile.Write("Four Byte Container #1: " + "0x" + HeroesProperty.FourByteContainer1X.ToString("X8") + "\n");
				OBJFile.Write("Four Byte Container #2: " + "0x" + HeroesProperty.FourByteContainer2X.ToString("X8") + "\n");
				OBJFile.Write("Four Byte Container #3: " + "0x" + HeroesProperty.FourByteContainer3X.ToString("X8") + "\n");
				OBJFile.Write("Four Byte Container #4: " + "0x" + HeroesProperty.FourByteContainer4X.ToString("X8") + "\n");
				OBJFile.Write("Four Byte Container #5: " + "0x" + HeroesProperty.FourByteContainer5X.ToString("X8") + "\n");
				OBJFile.Write("Four Byte Container #6: " + "0x" + HeroesProperty.FourByteContainer6X.ToString("X8") + "\n");
				OBJFile.Write("Four Byte Container #7: " + "0x" + HeroesProperty.FourByteContainer7X.ToString("X8") + "\n");
				OBJFile.Write("Four Byte Container #8: " + "0x" + HeroesProperty.FourByteContainer8X.ToString("X8") + "\n");

                OBJFile.Write("\n"); // Write new object
                x += 1; // Add to x
            }
        }

		ConvertTXTToBin(CurrentFile);
        File.Delete(CurrentFile);
        // Complete!
        Gtk.MessageDialog Complete = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Info, ButtonsType.Ok, "Save Complete!");
        Complete.Show();
        Complete.Run();
        Complete.Destroy();
    }

	private void ReadObjectFile(StreamReader FStream)
	{
        ClearObjectsList();
        ClearPropertyList();
		string CurrentLine; // Current line being read.
		int MiscFlag = 0; // 0 if main section, 1 if misc section.
		OBJObject TemporaryObject = new OBJObject();
		MiscProperty TemporaryProperty = new MiscProperty();
		int LastIndex;

		/// Main Section Entries
		while (MiscFlag == 0)
		{
			CurrentLine = FStream.ReadLine(); // Get the new line from the file.

            LastIndex = CurrentLine.LastIndexOf(" ", StringComparison.OrdinalIgnoreCase); // Get location of last space.
			LastIndex += 1; // Rmove the space preceeding the last index.

			// If the current line is empty, i.e. between objects, add the content of the current object and clear the object for assignment from the next object.
			if (CurrentLine == "")
			{
				AddObjectToList(TemporaryObject);
				ClearSETObject(TemporaryObject);
			}
			else if (CurrentLine.Contains("Object X Coordinate"))
			{
				TemporaryObject.ObjectXCoordinateX = (float)Convert.ToDouble(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Object Y Coordinate"))
			{
				TemporaryObject.ObjectYCoordinateX = (float)Convert.ToDouble(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Object Z Coordinate"))
			{
				TemporaryObject.ObjectZCoordinateX = (float)Convert.ToDouble(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Object X Rotation Degrees"))
			{
				TemporaryObject.ObjectXRotationX = (float)Convert.ToDouble(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Object Y Rotation Degrees"))
			{
				TemporaryObject.ObjectYRotationX = (float)Convert.ToDouble(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Object Z Rotation Degrees"))
			{
				TemporaryObject.ObjectZRotationX = (float)Convert.ToDouble(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Unknown Ignored Value:"))
			{
				TemporaryObject.IgnoredValue1X = Convert.ToUInt16(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Unknown Ignored Value #2:"))
			{
				TemporaryObject.IgnoredValue2X = Convert.ToUInt16(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Object's Associated Team:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.ObjectTeam1X = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Object's Associated Team #2:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.ObjectTeam2X = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Mystery Object Byte:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.MysteryByte1X = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Mystery Object Byte #2:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.MysteryByte2X = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Bytes of Mystery:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.FourBytesOfMystery1X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Bytes of Mystery #2:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.FourBytesOfMystery2X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Object Grouping/List:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.ObjectListX = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Object Type/ID:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.ObjectTypeIDX = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Object Linking ID:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.LinkingIDX = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Object Load Distance Multiplier:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryObject.LoadDistanceMultiplierX = Byte.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Object Property Section ID:"))
			{
				TemporaryObject.PropertyIDX = Convert.ToUInt16(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine == "[Misc 1]") { MiscFlag = 1; }
		}

		/// Misc Section Entries
		while ((CurrentLine = FStream.ReadLine()) != null)
		{
			LastIndex = CurrentLine.LastIndexOf(" ", StringComparison.OrdinalIgnoreCase); // Get location of last space.
			LastIndex += 1; // Rmove the space preceeding the last index.

			// If the current line is empty, i.e. between objects, add the content of the current object and clear the object for assignment from the next object.
			if (CurrentLine == "")
			{
				AddPropertyToList(TemporaryProperty);
				ClearProperty(TemporaryProperty);
			}
			else if (CurrentLine.Contains("Activator:"))
			{
				TemporaryProperty.ActivatorX = Convert.ToUInt16(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Entry ID:"))
			{
				TemporaryProperty.EntryIDX = Convert.ToUInt16(CurrentLine.Substring(LastIndex));
			}
			else if (CurrentLine.Contains("Four Byte Container #1:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer1X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Byte Container #2:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer2X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Byte Container #3:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer3X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Byte Container #4:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer4X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Byte Container #5:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer5X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Byte Container #6:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer6X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Byte Container #7:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer7X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
			else if (CurrentLine.Contains("Four Byte Container #8:"))
			{
				LastIndex += 2; // Cut 0x
				TemporaryProperty.FourByteContainer8X = UInt32.Parse(CurrentLine.Substring(LastIndex), System.Globalization.NumberStyles.HexNumber);
			}
		}
	}

	void CopySETObject(OBJObject Source, OBJObject Destination)
	{
		Source.FourBytesOfMystery1X = Destination.FourBytesOfMystery1X;
		Source.FourBytesOfMystery2X = Destination.FourBytesOfMystery2X;
		Source.IgnoredValue1X = Destination.IgnoredValue1X;
		Source.IgnoredValue2X = Destination.IgnoredValue2X;
		Source.LinkingIDX = Destination.LinkingIDX;
		Source.LoadDistanceMultiplierX = Destination.LoadDistanceMultiplierX;
		Source.MysteryByte1X = Destination.MysteryByte1X;
		Source.MysteryByte2X = Destination.MysteryByte2X;
		Source.ObjectListX = Destination.ObjectListX;
		Source.ObjectTeam1X = Destination.ObjectTeam1X;
		Source.ObjectTeam2X = Destination.ObjectTeam2X;
		Source.ObjectTypeIDX = Destination.ObjectTypeIDX;
		Source.ObjectXCoordinateX = Destination.ObjectXCoordinateX;
		Source.ObjectYCoordinateX = Destination.ObjectYCoordinateX;
		Source.ObjectZCoordinateX = Destination.ObjectZCoordinateX;
		Source.ObjectXRotationX = Destination.ObjectXRotationX;
		Source.ObjectYRotationX = Destination.ObjectYRotationX;
		Source.ObjectZRotationX = Destination.ObjectZRotationX;
		Source.PropertyIDX = Destination.PropertyIDX;
	}

	void ClearSETObject(OBJObject Source)
	{
		Source.FourBytesOfMystery1X = 0;
		Source.FourBytesOfMystery2X = 0;
		Source.IgnoredValue1X = 0;
		Source.IgnoredValue2X = 0;
		Source.LinkingIDX = 0;
		Source.LoadDistanceMultiplierX = 0;
		Source.MysteryByte1X = 0;
		Source.MysteryByte2X = 0;
		Source.ObjectListX = 0;
		Source.ObjectTeam1X = 0;
		Source.ObjectTeam2X = 0;
		Source.ObjectTypeIDX = 0;
		Source.ObjectXCoordinateX = 0;
		Source.ObjectYCoordinateX = 0;
		Source.ObjectZCoordinateX = 0;
		Source.ObjectXRotationX = 0;
		Source.ObjectYRotationX = 0;
		Source.ObjectZRotationX = 0;
		Source.PropertyIDX = 0;
	}

	void ClearProperty(MiscProperty Source)
	{
		Source.ActivatorX = 0;
		Source.EntryIDX = 0;
		Source.FourByteContainer1X = 0;
		Source.FourByteContainer2X = 0;
		Source.FourByteContainer3X = 0;
		Source.FourByteContainer4X = 0;
		Source.FourByteContainer5X = 0;
		Source.FourByteContainer6X = 0;
		Source.FourByteContainer7X = 0;
		Source.FourByteContainer8X = 0;
	}

    private void ClearObjectsList()
    {
        StageObjects.Clear();
    }

    private void ClearPropertyList()
    {
        ObjectProperties.Clear();
    }

	private void AddObjectToList(OBJObject Source)
	{
		StageObjects.Add(new OBJObject(Source.ObjectXCoordinateX, Source.ObjectYCoordinateX, Source.ObjectZCoordinateX, Source.ObjectXRotationX,
											 Source.ObjectYRotationX, Source.ObjectZRotationX, Source.IgnoredValue1X, Source.ObjectTeam1X, Source.MysteryByte1X,
											 Source.FourBytesOfMystery1X, Source.IgnoredValue2X, Source.ObjectTeam2X, Source.MysteryByte2X, Source.FourBytesOfMystery2X
											 , Source.ObjectTypeIDX, Source.LinkingIDX, Source.LoadDistanceMultiplierX, Source.ObjectListX, Source.PropertyIDX));
	}

	private void AddPropertyToList(MiscProperty Source)
	{
		ObjectProperties.Add(new MiscProperty(Source.ActivatorX, Source.EntryIDX, Source.FourByteContainer1X, Source.FourByteContainer2X, Source.FourByteContainer3X,
											 Source.FourByteContainer4X, Source.FourByteContainer5X, Source.FourByteContainer6X, Source.FourByteContainer7X, Source.FourByteContainer8X));
	}

	private void PopulateLists()
	{
		string CurrentPath = AppDomain.CurrentDomain.BaseDirectory; // Application Path

		/// First of all, object lists.
		Byte ListID; // ID Of List Being Generated
		string ListName; // List Name
		StreamReader OBJLists = new StreamReader(CurrentPath + "ListOfLists.ini");

		string CurrentLine; // Current line being read.
		int WantedIndex;

		/// Main Section Entries
		while ((CurrentLine = OBJLists.ReadLine()) != null)
		{
			WantedIndex = CurrentLine.IndexOf("[", StringComparison.OrdinalIgnoreCase); // Get location 
			WantedIndex += 1; // Remove the opening bracket.

			ListID = Byte.Parse(CurrentLine.Substring(WantedIndex, 2), System.Globalization.NumberStyles.HexNumber); // Get the ID

			WantedIndex = CurrentLine.IndexOf("] ", StringComparison.OrdinalIgnoreCase); // Get location of name.
			WantedIndex += 2; // Get to the name.

			ListName = CurrentLine.Substring(WantedIndex);

			ObjectLists.Add(new ListClass(ListName, ListID));
		}
		OBJLists.Close();

		///// DONE
		/// 

		/// Now for the Main Lists
		/// Some variables shared with code above.
		StreamReader ObjectsX = new StreamReader(CurrentPath + "ObjectList.ini");
		Byte ObjectID; // ID Of List Being Generated
		string ObjectName; // Object name

		/// Main Section Entries
		while ((CurrentLine = ObjectsX.ReadLine()) != null)
		{
			WantedIndex = CurrentLine.IndexOf("[", StringComparison.OrdinalIgnoreCase); // Get location 
			WantedIndex += 1; // Remove the opening bracket.

			ListID = Byte.Parse(CurrentLine.Substring(WantedIndex, 2), System.Globalization.NumberStyles.HexNumber); // Get the ID

			var ListItem = ObjectLists.Find(item => item.ListIDX == ListID);
			ListName = ListItem.ListNameX;

			ObjectID = Byte.Parse(CurrentLine.Substring(6, 2), System.Globalization.NumberStyles.HexNumber); // Get the ID
			ObjectName = CurrentLine.Substring(10);

			ObjectIDs.Add(new ObjectEntryClass(ListName, ListID, ObjectName, ObjectID));
		}
		ObjectsX.Close();
	}

	private void PopulateComboboxes()
	{
		for (int x = 0; x < ObjectLists.Count; x++)
		{
			CmbBox_SelectByType.AppendText(ObjectLists[x].ListNameX);
		}
		for (int x = 0; x < ObjectIDs.Count; x++)
		{
			CmbBox_SelectByName.AppendText(ObjectIDs[x].ObjectNameX);
		}
	}


	/// WARNING, WARNING, WARNING!!!
	/// Poor coding here, but bear with me, I only started with GTK, I do not know the whole liststore business myself yet.
	/// 


	protected void OnCmbBoxSelectByTypeChanged(object sender, EventArgs e)
	{
		ClearComboBox(CmbBox_SelectByID);

		try
		{
			var ListItem = ObjectLists.Find(item => item.ListNameX == CmbBox_SelectByType.ActiveText); // Gets ListID from ComboBox Selection
			ListObjects = ObjectIDs.FindAll(item => item.ListIDX == ListItem.ListIDX); // Gets all objects with matching ListID
			for (int x = 0; x < ListObjects.Count; x++)
			{
				CmbBox_SelectByID.AppendText(ListObjects[x].ObjectNameX);   // Appends all options for objects.
			}
		}
		catch (Exception Meme)
		{
			Gtk.MessageDialog DialogX = new Gtk.MessageDialog(this, 0, Gtk.MessageType.Error, ButtonsType.Ok, "Exception: " + Meme);
			DialogX.Run();
			DialogX.Destroy();
		}
	}

	public void ClearComboBox(Gtk.ComboBox ComboBoxX)
	{
		for (int x = 0; x < 4096; x++)
		{
			try { ComboBoxX.RemoveText(0); } // Try clearing current entry.
			catch (Exception E) { x = 4096; } // End the loop if exception.
		}
	}

	protected void OnCmbBoxSelectByIDChanged(object sender, EventArgs e)
	{
		ComboBoxInvoker = "null";

		// Finds the Index for the Object ID which matches both list and object ID.

		try
		{
			int ObjectIDIndex = ObjectIDs.FindIndex(Item => Item.ListIDX == ListObjects[CmbBox_SelectByID.Active].ListIDX && Item.ObjectIDX == ListObjects[CmbBox_SelectByID.Active].ObjectIDX);
			CmbBox_SelectByName.Active = ObjectIDIndex; // Makes Name Selection box equal to index, i.e. selects matching
			SpinBtn_ObjectID.Value = ObjectIDs[ObjectIDIndex].ObjectIDX;
			SpinBtn_ObjectList.Value = ObjectIDs[ObjectIDIndex].ListIDX;
		}
		catch
		{

		}
		ComboBoxInvoker = "default";
	}


	protected void OnCmbBoxSelectByNameChanged(object sender, EventArgs e)
	{
		if (ComboBoxInvoker != "null")
		{
			int ActiveObject = CmbBox_SelectByName.Active;
			string ActiveList = ObjectIDs[ActiveObject].ListNameX;
			string ActiveObjectName = ObjectIDs[ActiveObject].ObjectNameX;

			for (int x = 0; x < ObjectLists.Count; x++)
			{
				if (ObjectLists[x].ListNameX == ActiveList)
				{ CmbBox_SelectByType.Active = x; }
			}


			for (int x = 0; x < CmbBox_SelectByID.Model.IterNChildren(); x++)
			{
				CmbBox_SelectByID.Active = x;
				if (CmbBox_SelectByID.ActiveText == ActiveObjectName) { x = CmbBox_SelectByID.Model.IterNChildren(); }
			}
		}
	}


	protected void OnCmbBoxItemChanged(object sender, EventArgs e) // Sets values for each field once item changes.
	{
        
		if (ComboBoxInvoker != "null")
		{
			if (ComboBoxInvoker != "donotsave") 
			{ 
				SaveLastItem(); // Saves the values in the last itemboxes to the array.
				string ObjectNameString = "|" + ComboBoxObjectLastItem.ToString("D4") + "| " + "(" + StageObjects[ComboBoxObjectLastItem].ObjectXCoordinateX + "," + " " + StageObjects[ComboBoxObjectLastItem].ObjectYCoordinateX + "," + " " + StageObjects[ComboBoxObjectLastItem].ObjectZCoordinateX + ") " + GetObjectName(StageObjects[ComboBoxObjectLastItem].ObjectTypeIDX, StageObjects[ComboBoxObjectLastItem].ObjectListX);
				CmbBox_Item.RemoveText(ComboBoxObjectLastItem);
				CmbBox_Item.InsertText(ComboBoxObjectLastItem, ObjectNameString);
			} 
			ComboBoxObjectLastItem = CmbBox_Item.Active; // MUST BE PRESENT WHEN SWITCHING ITEMS, USED FOR SAVING, DO NOT FORGET.
			try
			{
				lbl_ObjectNumberLabel.Text = "Object Number: " + ComboBoxObjectLastItem + " / " + (CmbBox_Item.Model.IterNChildren() - 1);

				SpinBtn_PositionX.Value = StageObjects[ComboBoxObjectLastItem].ObjectXCoordinateX;
				SpinBtn_PositionY.Value = StageObjects[ComboBoxObjectLastItem].ObjectYCoordinateX;
				SpinBtn_PositionZ.Value = StageObjects[ComboBoxObjectLastItem].ObjectZCoordinateX;
				SpinBtn_RotationX.Value = StageObjects[ComboBoxObjectLastItem].ObjectXRotationX;
				SpinBtn_RotationY.Value = StageObjects[ComboBoxObjectLastItem].ObjectYRotationX;
				SpinBtn_RotationZ.Value = StageObjects[ComboBoxObjectLastItem].ObjectZRotationX;
				SpinBtn_ObjectList.Value = StageObjects[ComboBoxObjectLastItem].ObjectListX;
				SpinBtn_ObjectID.Value = StageObjects[ComboBoxObjectLastItem].ObjectTypeIDX;
				SpinBtn_LinkingID.Value = StageObjects[ComboBoxObjectLastItem].LinkingIDX;
				SpinBtn_LoadDistanceMultiplier.Value = StageObjects[ComboBoxObjectLastItem].LoadDistanceMultiplierX;
				SpinBtn_PropertyID.Value = StageObjects[ComboBoxObjectLastItem].PropertyIDX;

				ComboBoxInvoker = "null";
				ObjectTeamResolver(CmbBox_ObjectTeam1, StageObjects[ComboBoxObjectLastItem].ObjectTeam1X);
				ObjectTeamResolver(CmbBox_ObjectTeam2, StageObjects[ComboBoxObjectLastItem].ObjectTeam2X);
				ComboBoxInvoker = "default";

				SpinBtn_IgnoredValue1.Value = StageObjects[ComboBoxObjectLastItem].IgnoredValue1X;
				SpinBtn_IgnoredValue2.Value = StageObjects[ComboBoxObjectLastItem].IgnoredValue2X;

				SpinBtn_MysteryByte.Value = StageObjects[ComboBoxObjectLastItem].MysteryByte1X;
				SpinBtn_MysteryByte2.Value = StageObjects[ComboBoxObjectLastItem].MysteryByte2X;

				SpinBtn_MysteryFourBytes1.Value = StageObjects[ComboBoxObjectLastItem].FourBytesOfMystery1X;
				SpinBtn_MysteryFourBytes2.Value = StageObjects[ComboBoxObjectLastItem].FourBytesOfMystery2X;

				ComboBoxItemResolver(StageObjects[ComboBoxObjectLastItem].ObjectListX, StageObjects[ComboBoxObjectLastItem].ObjectTypeIDX);
			}
			catch
			{

			}
			int ObjectsIndex = ObjectProperties.FindIndex(item => item.EntryIDX == (int)SpinBtn_PropertyID.Value);
			ComboBoxInvoker = "combobox";
			CmbBox_Property.Active = ObjectsIndex;
			ComboBoxInvoker = "default";
		}
        
	}

	private void SaveLastItem()
	{

		try
		{
			StageObjects[ComboBoxObjectLastItem].ObjectXCoordinateX = (float)SpinBtn_PositionX.Value;
			StageObjects[ComboBoxObjectLastItem].ObjectYCoordinateX = (float)SpinBtn_PositionY.Value;
			StageObjects[ComboBoxObjectLastItem].ObjectZCoordinateX = (float)SpinBtn_PositionZ.Value;
			StageObjects[ComboBoxObjectLastItem].ObjectXRotationX = (float)SpinBtn_RotationX.Value;
			StageObjects[ComboBoxObjectLastItem].ObjectYRotationX = (float)SpinBtn_RotationY.Value;
			StageObjects[ComboBoxObjectLastItem].ObjectZRotationX = (float)SpinBtn_RotationZ.Value;


			StageObjects[ComboBoxObjectLastItem].ObjectListX = (byte)SpinBtn_ObjectList.Value;
			StageObjects[ComboBoxObjectLastItem].ObjectTypeIDX = (byte)SpinBtn_ObjectID.Value;
			StageObjects[ComboBoxObjectLastItem].LinkingIDX = (byte)SpinBtn_LinkingID.Value;
			StageObjects[ComboBoxObjectLastItem].LoadDistanceMultiplierX = (byte)SpinBtn_LoadDistanceMultiplier.Value;
			StageObjects[ComboBoxObjectLastItem].PropertyIDX = (ushort)SpinBtn_PropertyID.Value;

			StageObjects[ComboBoxObjectLastItem].ObjectTeam1X = ObjectTeamReverseResolver(CmbBox_ObjectTeam1);
			StageObjects[ComboBoxObjectLastItem].ObjectTeam2X = ObjectTeamReverseResolver(CmbBox_ObjectTeam2);

			StageObjects[ComboBoxObjectLastItem].IgnoredValue1X = (ushort)SpinBtn_IgnoredValue1.Value;
			StageObjects[ComboBoxObjectLastItem].IgnoredValue2X = (ushort)SpinBtn_IgnoredValue2.Value;
			StageObjects[ComboBoxObjectLastItem].MysteryByte1X = (byte)SpinBtn_MysteryByte.Value;
			StageObjects[ComboBoxObjectLastItem].MysteryByte2X = (byte)SpinBtn_MysteryByte2.Value;


			StageObjects[ComboBoxObjectLastItem].FourBytesOfMystery1X = (uint)SpinBtn_MysteryFourBytes1.Value;
			StageObjects[ComboBoxObjectLastItem].FourBytesOfMystery2X = (uint)SpinBtn_MysteryFourBytes2.Value;


		}
		catch
		{

		}
	}

	private void SaveLastProperty()
	{
		if (PropertyDataType == 0) { OnBtnViewAsIntegersReleased(null, null); }
		try
		{
			ObjectProperties[ComboBoxLastPropertyItem].ActivatorX = (ushort)SpinBtn_MiscActivator.Value;
			ObjectProperties[ComboBoxLastPropertyItem].EntryIDX = (ushort)SpinBtn_MiscEntryID.Value;

			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer1X = (UInt32)SpinBtn_ObjectProperty1.Value;
			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer2X = (UInt32)SpinBtn_ObjectProperty2.Value;
			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer3X = (UInt32)SpinBtn_ObjectProperty3.Value;
			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer4X = (UInt32)SpinBtn_ObjectProperty4.Value;
			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer5X = (UInt32)SpinBtn_ObjectProperty5.Value;
			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer6X = (UInt32)SpinBtn_ObjectProperty6.Value;
			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer7X = (UInt32)SpinBtn_ObjectProperty7.Value;
			ObjectProperties[ComboBoxLastPropertyItem].FourByteContainer8X = (UInt32)SpinBtn_ObjectProperty8.Value;
		}
		catch
		{

		}
	}

	private void ObjectTeamResolver(ComboBox TeamComboBox, Byte TeamNumber)
	{
		if (ComboBoxInvoker == "null")
		{
			switch (TeamNumber)
			{
				case 0:
					TeamComboBox.Active = 0;
					break;
				case 32:
					TeamComboBox.Active = 1;
					break;
				case 64:
					TeamComboBox.Active = 2;
					break;
				case 96:
					TeamComboBox.Active = 3;
					break;
				case 128:
					TeamComboBox.Active = 4;
					break;
				case 160:
					TeamComboBox.Active = 5;
					break;
			}
		}
	}

	private Byte ObjectTeamReverseResolver(ComboBox TeamComboBox)
	{
		switch (TeamComboBox.Active)
		{
			case 0:
				return 0;
			case 1:
				return 32;
			case 2:
				return 64;
			case 3:
				return 96;
			case 4:
				return 128;
			case 5:
				return 160;
			default:
				return 0;
		}
	}

	private void ComboBoxItemResolver(Byte ObjectList, Byte ObjectID)
	{
		// Gets index of loaded object in array equivalent to combobox
		int CurrentObjectIndex = ObjectIDs.FindIndex(ObjectX => (ObjectX.ListIDX == ObjectList) && (ObjectX.ObjectIDX == ObjectID));
		// Takes you to that entry
		CmbBox_SelectByName.Active = CurrentObjectIndex;
	}

	protected void OnBtnClearAllClicked(object sender, EventArgs e)
	{
		// Complete!
		Gtk.Dialog ClearAll = new Dialog("Tactical Nuke, Incoming!", this, DialogFlags.DestroyWithParent, "Proceed with the test!", ResponseType.Ok, "Aw hell no!", ResponseType.Cancel);
		Gtk.Label myLabel = new Label("Warning:\n" +
		                              "Immense Nuclear Reactions are about to take place in the form of the 'Indian Nuclear Test'.\n" +
		                              "All of the objects & properties present are about to become disintegrated to absolute dust, are you sure you want to continue?");
		ClearAll.BorderWidth = 15;
		ClearAll.VBox.PackStart(myLabel, true, true, 0);
		ClearAll.ShowAll();

		var result = ClearAll.Run();
		if (result == (int)ResponseType.Ok)
		{
			NukeAllTheObjects();
			Gtk.MessageDialog MessageDialog = new MessageDialog(this, DialogFlags.DestroyWithParent, MessageType.Warning, ButtonsType.Ok, "It is done!");
			MessageDialog.Show();
			MessageDialog.Run();
			MessageDialog.Destroy();
		}
		ClearAll.Destroy();
	}

	private void NukeObject(OBJObject StageObject)
	{ 
		StageObject.ObjectXCoordinateX = 0;
		StageObject.ObjectYCoordinateX = 0;
		StageObject.ObjectZCoordinateX = 0;
		StageObject.ObjectXRotationX = 0;
		StageObject.ObjectYRotationX = 0;
		StageObject.ObjectZRotationX = 0;
		StageObject.IgnoredValue1X = 0;
		StageObject.ObjectTeam1X = 0;
		StageObject.MysteryByte1X = 0;
		StageObject.FourBytesOfMystery1X = 0;
		StageObject.IgnoredValue2X = 0;
		StageObject.ObjectTeam2X = 0;
		StageObject.MysteryByte2X = 0;
		StageObject.FourBytesOfMystery2X = 0;
		StageObject.ObjectTypeIDX = 0;
		StageObject.LinkingIDX = 0;
		StageObject.LoadDistanceMultiplierX = 0;
		StageObject.ObjectListX = 0;
		StageObject.PropertyIDX = 0;
	}


	private void NukeProperty(MiscProperty MiscProperty)
	{
		MiscProperty.ActivatorX = 0;
		MiscProperty.EntryIDX = 0;
		MiscProperty.FourByteContainer1X = 0;
		MiscProperty.FourByteContainer2X = 0;
		MiscProperty.FourByteContainer3X = 0;
		MiscProperty.FourByteContainer4X = 0;
		MiscProperty.FourByteContainer5X = 0;
		MiscProperty.FourByteContainer6X = 0;
		MiscProperty.FourByteContainer7X = 0;
		MiscProperty.FourByteContainer8X = 0;
	}

	private void NukeAllTheObjects()
	{
		foreach (OBJObject StageObject in StageObjects)
		{
			NukeObject(StageObject);	
		}

		foreach (MiscProperty MiscProperty in ObjectProperties)
		{
			NukeProperty(MiscProperty);
		}

		PopulateObjects();
		PopulateMisc();
	}

	protected void OnBtnRemoveObjectClicked(object sender, EventArgs e)
	{
         NukeObjectScreen(); // Just zeroes all GUI
    }

    private void NukeObjectScreen()
    {
        CmbBox_SelectByName.Active = 0;
        SpinBtn_PositionX.Value = 0;
        SpinBtn_PositionY.Value = 0;
        SpinBtn_PositionZ.Value = 0;
        SpinBtn_RotationX.Value = 0;
        SpinBtn_RotationY.Value = 0;
        SpinBtn_RotationZ.Value = 0;
        SpinBtn_ObjectID.Value = 0;
        SpinBtn_ObjectList.Value = 0;
        SpinBtn_LinkingID.Value = 0;
        SpinBtn_LoadDistanceMultiplier.Value = 0;
        SpinBtn_PropertyID.Value = 0;
        SpinBtn_IgnoredValue1.Value = 0;
        SpinBtn_IgnoredValue2.Value = 0;
        CmbBox_ObjectTeam1.Active = 0;
        CmbBox_ObjectTeam2.Active = 0;
        SpinBtn_MysteryByte.Value = 0;
        SpinBtn_MysteryByte2.Value = 0;
        SpinBtn_MysteryFourBytes1.Value = 0;
        SpinBtn_MysteryFourBytes2.Value = 0;
    }

	protected void OnBtnCloneObjectClicked(object sender, EventArgs e)
	{
		CloneEntry CloneEntryWindow = new CloneEntry(this);
		CloneEntryWindow.Show();
		CloneEntryWindow.FillInComboBox(CmbBox_Item);
	}

	public Gtk.ComboBox GetComboBoxItemControl()
	{
		return CmbBox_Item;
	}

	public string ComboBoxInvokerX { get { return ComboBoxInvoker; } set { ComboBoxInvoker = value; } }
	public GUIObjectScreen GetObjectScreen()
	{
		GUIObjectScreen TempScreen = new GUIObjectScreen();
		TempScreen.PositionX = SpinBtn_PositionX.Value;
		TempScreen.PositionY = SpinBtn_PositionY.Value;
		TempScreen.PositionZ = SpinBtn_PositionZ.Value;
		TempScreen.RotationX = SpinBtn_RotationX.Value;
		TempScreen.RotationY = SpinBtn_RotationY.Value;
		TempScreen.RotationZ = SpinBtn_RotationZ.Value;
		TempScreen.ObjectID = SpinBtn_ObjectID.Value;
		TempScreen.ObjectList = SpinBtn_ObjectList.Value;
		TempScreen.LinkingID = SpinBtn_LinkingID.Value;
		TempScreen.LoadDistanceMultiplier = SpinBtn_LoadDistanceMultiplier.Value;
		TempScreen.PropertyID = SpinBtn_PropertyID.Value;
		TempScreen.IgnoredValue1 = SpinBtn_IgnoredValue1.Value;
		TempScreen.IgnoredValue2 = SpinBtn_IgnoredValue2.Value;
		TempScreen.ObjectTeam1 = CmbBox_ObjectTeam1.Active;
		TempScreen.ObjectTeam2 = CmbBox_ObjectTeam2.Active;
		TempScreen.MysteryByte = SpinBtn_MysteryByte.Value;
		TempScreen.MysteryByte2 = SpinBtn_MysteryByte2.Value;
		TempScreen.MysteryFourBytes1 = SpinBtn_MysteryFourBytes1.Value;
		TempScreen.MysteryFourBytes2 = SpinBtn_MysteryFourBytes2.Value;
		return TempScreen;
	}
	public void SetObjectScreen(double PositionX, double PositionY, double PositionZ, double RotationX, double RotationY, double RotationZ, double ObjectID, double ObjectList, double LinkingID, double LoadDistanceMultiplier, double PropertyID, double IgnoredValue1, double IgnoredValue2, int ObjectTeam1, 
	                            int ObjectTeam2, double MysteryByte, double MysteryByte2, double MysteryFourBytes1, double MysteryFourBytes2)
	{
		SpinBtn_PositionX.Value = PositionX;
		SpinBtn_PositionY.Value = PositionY;
		SpinBtn_PositionZ.Value = PositionZ;
		SpinBtn_RotationX.Value = RotationX;
		SpinBtn_RotationY.Value = RotationY;
		SpinBtn_RotationZ.Value = RotationZ;
		SpinBtn_ObjectID.Value = ObjectID;
		SpinBtn_ObjectList.Value = ObjectList;
		SpinBtn_LinkingID.Value = LinkingID;
		SpinBtn_LoadDistanceMultiplier.Value = LoadDistanceMultiplier;
		SpinBtn_PropertyID.Value = PropertyID;
		SpinBtn_IgnoredValue1.Value = IgnoredValue1;
		SpinBtn_IgnoredValue2.Value = IgnoredValue2;
		CmbBox_ObjectTeam1.Active = ObjectTeam1;
		CmbBox_ObjectTeam2.Active = ObjectTeam2;
		SpinBtn_MysteryByte.Value = MysteryByte;
		SpinBtn_MysteryByte2.Value = MysteryByte2;
		SpinBtn_MysteryFourBytes1.Value = MysteryFourBytes1;
		SpinBtn_MysteryFourBytes2.Value = MysteryFourBytes2;
	}

    public int ComboBoxObjectLastItemX { get { return ComboBoxObjectLastItem; } set { ComboBoxObjectLastItem = value; } }
}


/*
 * float ObjectX, float ObjectY, float ObjectZ, float RotX, float RotY, float RotZ, UInt16 IgnVal1, Byte ObjTeam1, Byte MysteryByte1X, UInt32 BytesOfMystery, UInt16 IgnVal2,
		                 Byte ObjTeam2, Byte MysteryByte2X, UInt32 BytesOfMystery2, Byte ObjTypID, Byte LinkID, Byte LoadDistX, Byte ObjectListX, UInt16 PropertyIDX
*/