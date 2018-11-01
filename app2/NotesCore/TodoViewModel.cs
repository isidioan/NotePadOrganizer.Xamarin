using System;
using Android.Util;
using SQLite;
using System.Collections.Generic;
using System.Linq;

namespace NotesCore
{
	public class TodoViewModel
	{
		public const string DB_NAME = "NotePadApp.db3";
		readonly string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


		public void CreateTable()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.CreateTable<DataModelToDo>();
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
			}
		}

		public void insert(DataModelToDo d)
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

		public List<DataModelToDo> queryAll()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					return connection.Table<DataModelToDo>().ToList();
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
				return null;
			}
		}

		public void updateCheck(int id, int status)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.Query<DataModelToDo>("UPDATE MyToDo set Checked=? Where _id=?", status, id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
			}
		}

		public void updateTitle(int id, string status)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.Query<DataModelToDo>("UPDATE MyToDo set TodoTitle=? Where _id=?", status, id);
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
					connection.Delete<DataModelToDo>(id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
			}
		}
	}
}
