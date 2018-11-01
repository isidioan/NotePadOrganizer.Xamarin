
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;

using Android.Views;
using Android.Widget;
using NotesCore;

namespace app2
{
	[Activity(Label = "Task List", Theme = "@style/AppTheme.NoActionBar",ParentActivity=typeof(MainActivity))]
	public class TaskMainActivity : AppCompatActivity
	{
		ListView listview;
		EditText textView;
		Android.Support.V7.App.AlertDialog alertWindow;
		List<DataModelToDo> tasks;
		TaskAdapter adapter;
		TodoViewModel helper;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.TaskMain);
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar3);
			SetSupportActionBar(toolbar);
			SupportActionBar.Title = "Task List";
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);
			 helper = new TodoViewModel();
			helper.CreateTable();
			tasks=helper.queryAll();
			DataModelToDo ab = new DataModelToDo();
			listview = FindViewById<ListView>(Resource.Id.listView);
			adapter = new TaskAdapter(this, tasks, helper);
			listview.Adapter=adapter;
			textView = new EditText(this);
			createAlertWindow();
			RegisterForContextMenu(listview);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.add_menu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var id = item.ItemId;
			if (id == Resource.Id.add_icon)
			{
				textView.Text = "";
				alertWindow.Show();
				return true;
			}
			return base.OnOptionsItemSelected(item);
		}

		void createAlertWindow()
		{
			Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
			alertDialog.SetTitle("Add a new Task");
			alertDialog.SetView(textView);
			alertDialog.SetPositiveButton("Add", (sender, e) =>
			{
				var text = textView.Text;
				DataModelToDo task = new DataModelToDo();
				task.TodoTitle = text;
				task.Checked = 0;
				helper.insert(task);
				tasks = helper.queryAll();
				adapter.refresh(tasks);
			});
			alertDialog.SetNegativeButton("Cancel", (sender, e) =>
			{
			});
			alertWindow = alertDialog.Create();
		}
		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);
			var menuInflater = MenuInflater;
			menuInflater.Inflate(Resource.Menu.contentx_menu, menu);
		}
		public override bool OnContextItemSelected(IMenuItem item)
		{
			var menuInfo = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
			var row = menuInfo.Position;
			if (item.ItemId == Resource.Id.deleteOption)
			{
				DataModelToDo t = tasks[row];
				Android.Support.V7.App.AlertDialog.Builder builder = new Android.Support.V7.App.AlertDialog.Builder(this);
				builder.SetTitle("Are you sure?");
				builder.SetPositiveButton("Yes", (sender, e) => { 
				helper.delete(t.Id);
				tasks = helper.queryAll();
			    adapter.refresh(tasks);
				});
				builder.SetNegativeButton("No", (sender, e) => { });
				builder.Create().Show();
			}
			else
			{
				Android.Support.V7.App.AlertDialog.Builder alertEditDialog = new Android.Support.V7.App.AlertDialog.Builder(this);
				alertEditDialog.SetTitle("Edit Task");
				var editText = new EditText(this);
				editText.Text = tasks[row].TodoTitle;
				alertEditDialog.SetView(editText);
				alertEditDialog.SetPositiveButton("Save", (sender, e) =>
				 {
					 var text = editText.Text;
					 helper.updateTitle(tasks[row].Id, text);
					tasks = helper.queryAll();
					adapter.refresh(tasks);
				 });
				alertEditDialog.SetNegativeButton("Cancel", (sender, e) => { });
				alertEditDialog.Create().Show();
			}



			return base.OnContextItemSelected(item);
		}
        [Java.Interop.Export("clickHandler")]
		public void clickHandler(View v)
		{
			tasks = helper.queryAll();
			adapter = new TaskAdapter(this, tasks, helper);
			listview.Adapter = adapter;
		}
	}
}
