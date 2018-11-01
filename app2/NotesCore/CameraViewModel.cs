using System;
using System.Collections.Generic;
using System.Linq;
using Android.Util;
using SQLite;

namespace NotesCore
{
	public class CameraViewModel
	{
		public const string DB_NAME = "NotePadApp.db3";
		readonly string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);


		public void CreateTable()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.CreateTable<DataModelCamera>();
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
					connection.Delete<DataModelCamera>(id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);

			}
		}

		public List<DataModelCamera> queryAll()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					return connection.Table<DataModelCamera>().ToList();
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
				return null;
			}
		}

		public void insert(DataModelCamera d)
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

		public DataModelCamera queryOne(int id)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					return connection.Find<DataModelCamera>(id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
				return null;
			}
		}

	}
}
