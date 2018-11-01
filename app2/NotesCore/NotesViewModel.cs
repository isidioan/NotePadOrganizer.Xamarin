using System;
using System.Collections.Generic;
using System.Linq;
using Android.Util;
using SQLite;

namespace NotesCore
{
	public class NotesViewModel
	{
		public const string DB_NAME = "NotePadApp.db3";
		readonly string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

		public void CreateTable()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.CreateTable<DataModelNotes>();
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
			}
		}
		public void insert(DataModelNotes d)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.Insert(d);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
			}
		}
		public List<DataModelNotes> queryAll()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					return connection.Table<DataModelNotes>().ToList();
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
				return null;
			}
		}
		public void update(DataModelNotes d)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.Query<DataModelNotes>("UPDATE MyNotes set Title=?, Summary=?, Date=? Where _id=?", d.Title, d.Summary,d.Date, d.Id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);

			}
		}

		public void updateA(int id, String title, String summary, String date)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.Query<DataModelNotes>("UPDATE MyNotes set Title=?, Summary=?, Date=? Where _id=?",title, summary,date,id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);


			}
		}

		public void delete(int id)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.Delete<DataModelNotes>(id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);

			}
		}
	
	}
}
