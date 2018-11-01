using System;
using SQLite;
namespace NotesCore
{
	[Table("MyNotes")]
	public class DataModelNotes
	{
		public DataModelNotes()
		{
		}
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id {get; set;}

		[NotNull]
		public string Title {get; set;}

		[MaxLength(500)]
		public string Summary {get; set;}
		[MaxLength(10)]
		public string Date { get; set; }
	}
}
