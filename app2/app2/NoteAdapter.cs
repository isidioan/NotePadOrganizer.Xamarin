using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Views;
using Android.Widget;
using NotesCore;

namespace app2
{
	public class NoteAdapter:BaseAdapter<DataModelNotes>
	{
	//	Context _context;
		List<DataModelNotes> _notes;
		private readonly Activity _activity;
		public NoteAdapter(Activity activity, List<DataModelNotes> notes):base()
		{
			_activity = activity;

			_notes = notes;
		}

		public override int Count
		{
			get { return _notes.Count; }
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override DataModelNotes this[int position]
		{
			get { return _notes[position]; }
		}


		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			//DataModelNotes note = this[position];
			var view = convertView;
				if (view == null)
			{
				view = _activity.LayoutInflater.Inflate(Resource.Layout.custom_notes_row, null);
				}
			var title = view.FindViewById<TextView>(Resource.Id.noteTitle);
			var summary = view.FindViewById<TextView>(Resource.Id.noteSummary);
			var date = view.FindViewById<TextView>(Resource.Id.noteDate);
			title.Text = _notes[position].Title;
			summary.Text = _notes[position].Summary;
			date.Text = _notes[position].Date;
			return view;
		}
	}
}
