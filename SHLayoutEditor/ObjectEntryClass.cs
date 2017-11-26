using System;
namespace SHLayoutEditor
{
	public class ObjectEntryClass : ListClass
	{
		private string ObjectName;
		private Byte ObjectID;

		public ObjectEntryClass() : base() { }
		public ObjectEntryClass(string ListName, Byte ListID, string ObjectNameX, Byte ObjectIDX) : base ()
		{
			ObjectName = ObjectNameX;
			ListNameX = ListName;
			ListIDX = ListID;
			ObjectID = ObjectIDX;
		}

		public ObjectEntryClass(string ObjectNameX, Byte ObjectIDX) : base()
		{
			ObjectName = ObjectNameX;
			ObjectID = ObjectIDX;
		}

		public string ObjectNameX { get { return this.ObjectName; } set { this.ObjectName = value; } }
		public Byte ObjectIDX { get { return this.ObjectID; } set { this.ObjectID = value; } }
	}
}
