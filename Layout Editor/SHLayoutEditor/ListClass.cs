using System;
namespace SHLayoutEditor
{
	public class ListClass
	{
		private string ListName;
		private Byte ListID;

		public ListClass() {}
		public ListClass(string ListNameX, Byte ListIDX)
		{
			ListName = ListNameX;
			ListID = ListIDX;
		}

		public string ListNameX { get { return this.ListName; } set { this.ListName = value; } }
		public Byte ListIDX { get { return this.ListID; } set { this.ListID = value; } }
	}
}
