using System;
namespace SHLayoutEditor
{
	public class MiscProperty
	{
		private UInt16 Activator;
		private UInt16 EntryID;
		private UInt32 FourByteContainer1;
		private UInt32 FourByteContainer2;
		private UInt32 FourByteContainer3;
		private UInt32 FourByteContainer4;
		private UInt32 FourByteContainer5;
		private UInt32 FourByteContainer6;
		private UInt32 FourByteContainer7;
		private UInt32 FourByteContainer8;

		public MiscProperty() { }

		public MiscProperty(UInt16 ActivatorX, UInt16 EntryIDX, UInt32 FourByteContainer1X, UInt32 FourByteContainer2X, UInt32 FourByteContainer3X, UInt32 FourByteContainer4X, UInt32 FourByteContainer5X, UInt32 FourByteContainer6X, UInt32 FourByteContainer7X, UInt32 FourByteContainer8X)
		{
			Activator = ActivatorX;
			EntryID = EntryIDX;
			FourByteContainer1 = FourByteContainer1X;
			FourByteContainer2 = FourByteContainer2X;
			FourByteContainer3 = FourByteContainer3X;
			FourByteContainer4 = FourByteContainer4X;
			FourByteContainer5 = FourByteContainer5X;
			FourByteContainer6 = FourByteContainer6X;
			FourByteContainer7 = FourByteContainer7X;
			FourByteContainer8 = FourByteContainer8X;
		}

		public UInt16 ActivatorX { get { return this.Activator; } set { this.Activator = value; } }
		public UInt16 EntryIDX { get { return this.EntryID; } set { this.EntryID = value; } }

		public UInt32 FourByteContainer1X { get { return this.FourByteContainer1; } set { this.FourByteContainer1 = value; } }
		public UInt32 FourByteContainer2X { get { return this.FourByteContainer2; } set { this.FourByteContainer2 = value; } }
		public UInt32 FourByteContainer3X { get { return this.FourByteContainer3; } set { this.FourByteContainer3 = value; } }
		public UInt32 FourByteContainer4X { get { return this.FourByteContainer4; } set { this.FourByteContainer4 = value; } }
		public UInt32 FourByteContainer5X { get { return this.FourByteContainer5; } set { this.FourByteContainer5 = value; } }
		public UInt32 FourByteContainer6X { get { return this.FourByteContainer6; } set { this.FourByteContainer6 = value; } }
		public UInt32 FourByteContainer7X { get { return this.FourByteContainer7; } set { this.FourByteContainer7 = value; } }
		public UInt32 FourByteContainer8X { get { return this.FourByteContainer8; } set { this.FourByteContainer8 = value; } }
	}
}
