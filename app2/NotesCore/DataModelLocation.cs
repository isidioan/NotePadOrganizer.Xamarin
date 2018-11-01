using System;
using SQLite;
namespace NotesCore
{

	[Table("MyLocation")]
	public class DataModelLocation
	{
		[PrimaryKey, AutoIncrement,Column("_id")]
		public int Id { get; set; }
		[MaxLength(50), NotNull]
		public string Title { get; set;}
		[MaxLength(100)]
		public string Address { get; set; }
		public double Latitude { get; set; }
		public double Longitude { get; set; }
	}
}
