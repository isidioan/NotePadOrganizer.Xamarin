
using System;
using System.Collections.Generic;
using NotesCore;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace app2
{
	public class NoteListFragment : ListFragment
	{
		DataModelNotes note;
		NotesViewModel model;
		List<DataModelNotes> notes;
		NoteAdapter ad;

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			model = new NotesViewModel();
			model.CreateTable();
			notes = new List<DataModelNotes>();
			notes = model.queryAll();
			 ad = new NoteAdapter(this.Activity, notes);
			this.ListAdapter = ad;
			return base.OnCreateView(inflater, container, savedInstanceState);
		}

		public override void OnStart()
		{
			base.OnStart();
			RegisterForContextMenu(ListView);
		}

		public override void OnListItemClick(ListView l, View v, int position, long id)
		{
			base.OnListItemClick(l, v, position, id);
			proceedToView("detail", position);
		}

		public override void OnCreateContextMenu(IContextMenu menu, View v, IContextMenuContextMenuInfo menuInfo)
		{
			base.OnCreateContextMenu(menu, v, menuInfo);
			var inflater = Activity.MenuInflater;
			inflater.Inflate(Resource.Menu.contentx_menu, menu);
		}

		public override bool OnContextItemSelected(IMenuItem item)
		{

			var menuInfo = (AdapterView.AdapterContextMenuInfo)item.MenuInfo;
			var position = menuInfo.Position;
			switch (item.ItemId)
			{

				case Resource.Id.editOption:
					proceedToView("edit", position);
					return true;

				case Resource.Id.deleteOption:
					Android.Support.V7.App.AlertDialog.Builder alertDialog = new Android.Support.V7.App.AlertDialog.Builder(Activity);
					alertDialog.SetTitle("Are you sure?");
					alertDialog.SetPositiveButton("Yes", (sender, e) =>
					{
						note = notes[position];
						model.delete(note.Id);
						notes = model.queryAll();
						ad = new NoteAdapter(Activity, notes);
						this.ListAdapter = ad;
					});
					alertDialog.SetNegativeButton("No", (sender, e) => { });
					alertDialog.Create().Show();
					return true;
			}
			return base.OnContextItemSelected(item);
		}

		void proceedToView(String option, int position) 
		{
			note = notes[position];
			var intent = new Intent(this.Activity.ApplicationContext, typeof(NotesDetailActivity));
			intent.PutExtra(NotesMainActivity.NOTEID, note.Id);
			intent.PutExtra(NotesMainActivity.NOTETITLE, note.Title);
			intent.PutExtra(NotesMainActivity.NOTESUMMARY, note.Summary);
			intent.PutExtra(NotesMainActivity.NOTEDATE, note.Date);
			if (option.Equals("detail"))
			{
				intent.PutExtra(NotesMainActivity.FRAGMENTTOCHOOSE, "detail");
			}
			else if (option.Equals("edit"))
			{
				intent.PutExtra(NotesMainActivity.FRAGMENTTOCHOOSE, "edit");
			}
			StartActivity(intent);
		
		}


	}
}
