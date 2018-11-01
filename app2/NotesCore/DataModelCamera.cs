using System;
using SQLite;
namespace NotesCore
{
	[Table("MyCamera")]
	public class DataModelCamera
	{
		public DataModelCamera()
		{
		}

		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id { get; set;}
		[NotNull, MaxLength(50)]
		public string CameraTitle { get; set;}
		[MaxLength(100)]
		public string CameraDesc { get; set; }
		public byte[] CameraPath { get; set; }
	}
}
