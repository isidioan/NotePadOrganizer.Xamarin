using System;
using System.Collections.Generic;
using System.Linq;
using Android.Util;
using SQLite;

namespace NotesCore
{
	public class LocationViewModel
	{
		public const string DB_NAME = "NotePadApp.db3";
		readonly string folder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);

		public void CreateTable()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.CreateTable<DataModelLocation>();
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
					connection.Delete<DataModelLocation>(id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
			}
		}
		public List<DataModelLocation> queryAll()
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					return connection.Table<DataModelLocation>().ToList();
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
				return null;

			}
		}

		public void insert(DataModelLocation d)
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
		public DataModelLocation queryOne(int id)
		{
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					return connection.Find<DataModelLocation>(id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);
				return null;

			}
		}

		public void update(int id , string title , string address , double lat , double longit){
			try
			{
				using (var connection = new SQLiteConnection(System.IO.Path.Combine(folder, DB_NAME)))
				{
					connection.Query<DataModelLocation>("UPDATE MyLocation SET Title = ?,Address=?,Latitude=?,Longitude=? Where _id=?",
					                                    title,address,lat,longit,id);
				}
			}
			catch (SQLiteException ex)
			{
				Log.Info("SQLite Error:", ex.Message);

			}
		}
			
	}
}
