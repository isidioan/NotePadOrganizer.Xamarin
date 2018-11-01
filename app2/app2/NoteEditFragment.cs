
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using NotesCore;

namespace app2
{
	public class NoteEditFragment : Fragment
	{
		 String title, summary;
		 EditText editTextTitle, editTextSummary;
		 Button saveEditButton;
         bool createNote = false;
		 int noteId;
		NotesViewModel helper;
		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			
			var view = inflater.Inflate(Resource.Layout.NoteEditFragment, container, false);
			var bundle = Arguments;
			if (bundle != null)
			{
				createNote = bundle.GetBoolean(NotesDetailActivity.ADDNEWNOTE, false);
			}
			editTextTitle = view.FindViewById<EditText>(Resource.Id.editNoteTitle);
			editTextSummary = view.FindViewById<EditText>(Resource.Id.editNoteSummary);
			saveEditButton = view.FindViewById<Button>(Resource.Id.editSaveButton);

			var intent = Activity.Intent;
			title = intent.Extras.GetString(NotesMainActivity.NOTETITLE, "");
			summary = intent.Extras.GetString(NotesMainActivity.NOTESUMMARY, "");
			noteId = intent.Extras.GetInt(NotesMainActivity.NOTEID, 0);
			editTextTitle.Text = title;
			editTextSummary.Text = summary;
			helper = new NotesViewModel();
			saveEditButton.Click += (sender, e) =>
			  {

				  title = editTextTitle.Text;
				  summary = editTextSummary.Text;
				  var date = DateTime.Now.ToString("dd/MM/yy"); 
				if (createNote)
				  {
					  DataModelNotes note = new DataModelNotes();
					  note.Title = title;
					  note.Summary = summary;
					  note.Date = date;
					  helper.insert(note);
				  }
				  else
				  {
					helper.updateA(noteId, title, summary,date);
				  }
				  var intent1 = new Intent(Activity, typeof(NotesMainActivity));
				  intent1.AddFlags(ActivityFlags.ClearTop);
				  StartActivity(intent1);
			  };
			return view;
		}
	}
}
