using System;
using SQLite;
namespace NotesCore
{
	[Table("MyToDo")]
	public class DataModelToDo
	{
		public DataModelToDo()
		{
		}
		[PrimaryKey, AutoIncrement, Column("_id")]
		public int Id {get; set;}
		[NotNull , MaxLength(50)]
		public string TodoTitle {get; set;}
		public int Checked {get; set;}
	}
}
