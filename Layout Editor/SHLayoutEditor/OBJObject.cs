using System;

namespace SHLayoutEditor
{
	public class OBJObject
	{
		private float ObjectXCoordinate;
		private float ObjectYCoordinate;
		private float ObjectZCoordinate;
		private float ObjectXRotation;
		private float ObjectYRotation;
		private float ObjectZRotation;

		private UInt16 IgnoredValue1;
		private Byte ObjectTeam1;
		private Byte MysteryByte1;
		private UInt32 FourBytesOfMystery1;

		private UInt16 IgnoredValue2;
		private Byte ObjectTeam2;
		private Byte MysteryByte2;
		private UInt32 FourBytesOfMystery2;

		private Byte ObjectTypeID;
		private Byte LinkingID;
		private Byte LoadDistanceMultiplier;
		private Byte ObjectList;

		private UInt16 PropertyID;

		public OBJObject(){ }
		public OBJObject(float ObjectX, float ObjectY, float ObjectZ, float RotX, float RotY, float RotZ, UInt16 IgnVal1, Byte ObjTeam1, Byte MysteryByte1X, UInt32 BytesOfMystery, UInt16 IgnVal2,
		                 Byte ObjTeam2, Byte MysteryByte2X, UInt32 BytesOfMystery2, Byte ObjTypID, Byte LinkID, Byte LoadDistX, Byte ObjectListX, UInt16 PropertyIDX)
		{
			ObjectXCoordinate = ObjectX;
			ObjectYCoordinate = ObjectY;
			ObjectZCoordinate = ObjectZ;
			ObjectXRotation = RotX;
			ObjectYRotation = RotY;
			ObjectZRotation = RotZ;

			IgnoredValue1 = IgnVal1;
			ObjectTeam1 = ObjTeam1;
			MysteryByte1 = MysteryByte1X;
			FourBytesOfMystery1 = BytesOfMystery;

			IgnoredValue2 = IgnVal2;
			ObjectTeam2 = ObjTeam2;
			MysteryByte2 = MysteryByte2X;
			FourBytesOfMystery2 = BytesOfMystery2;

			ObjectTypeID = ObjTypID;
			LinkingID = LinkID;
			LoadDistanceMultiplier = LoadDistX;
			ObjectList = ObjectListX;

			PropertyID = PropertyIDX;
		}

		public float ObjectXCoordinateX { get { return this.ObjectXCoordinate; } set { this.ObjectXCoordinate = value; } }
		public float ObjectYCoordinateX { get { return this.ObjectYCoordinate; } set { this.ObjectYCoordinate = value; } }
		public float ObjectZCoordinateX { get { return this.ObjectZCoordinate; } set { this.ObjectZCoordinate = value; } }
		public float ObjectXRotationX { get { return this.ObjectXRotation; } set { this.ObjectXRotation = value; } }
		public float ObjectYRotationX { get { return this.ObjectYRotation; } set { this.ObjectYRotation = value; } }
		public float ObjectZRotationX { get { return this.ObjectZRotation; } set { this.ObjectZRotation = value; } }

		public UInt16 IgnoredValue1X { get { return this.IgnoredValue1; } set { this.IgnoredValue1 = value; } }
		public Byte ObjectTeam1X { get { return this.ObjectTeam1; } set { this.ObjectTeam1 = value; } }
		public Byte MysteryByte1X { get { return this.MysteryByte1; } set { this.MysteryByte1 = value; } }
		public UInt32 FourBytesOfMystery1X { get { return this.FourBytesOfMystery1; } set { this.FourBytesOfMystery1 = value; } }

		public UInt16 IgnoredValue2X { get { return this.IgnoredValue2; } set { this.IgnoredValue2 = value; } }
		public Byte ObjectTeam2X { get { return this.ObjectTeam2; } set { this.ObjectTeam2 = value; } }
		public Byte MysteryByte2X { get { return this.MysteryByte2; } set { this.MysteryByte2 = value; } }
		public UInt32 FourBytesOfMystery2X { get { return this.FourBytesOfMystery2; } set { this.FourBytesOfMystery2 = value; } }

		public Byte ObjectTypeIDX { get { return this.ObjectTypeID; } set { this.ObjectTypeID = value; } }
		public Byte LinkingIDX { get { return this.LinkingID; } set { this.LinkingID = value; } }
		public Byte LoadDistanceMultiplierX { get { return this.LoadDistanceMultiplier; } set { this.LoadDistanceMultiplier = value; } }
		public Byte ObjectListX { get { return this.ObjectList; } set { this.ObjectList = value; } }

		public UInt16 PropertyIDX { get { return this.PropertyID; } set { this.PropertyID = value; } }
	}
}
