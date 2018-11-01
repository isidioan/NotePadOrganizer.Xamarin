
using System;
using Android.App;
using Android.OS;
using Android.Support.V7.App;


namespace app2
{
	[Activity(Label = "Note Details", Theme = "@style/AppTheme", ParentActivity=typeof(NotesMainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "app2.NotesMainActivity")]
	public class NotesDetailActivity : AppCompatActivity
	{
		public const String ADDNEWNOTE = "newNote";
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NoteDetail);
			addFragment();
		}

		void addFragment()
		{

			var intent = Intent;
			var fragment = intent.GetStringExtra(NotesMainActivity.FRAGMENTTOCHOOSE);
			var transaction = FragmentManager.BeginTransaction();

			if (fragment.Equals("detail"))
			{
				var detailFr = new NoteDetailFragment();
				transaction.Add(Resource.Id.noteDetailContainer, detailFr, "FRAGMENTDETAIL");
				this.Title = "Note Details";
			}

			else if (fragment.Equals("add"))
			{
				var addFr = new NoteEditFragment();
				var bundle = new Bundle();
				bundle.PutBoolean(ADDNEWNOTE, true);
				addFr.Arguments = bundle;
				transaction.Add(Resource.Id.noteDetailContainer, addFr, "FRAGMENTCREATE");
				this.Title = "New Note";
			}
			else
			{
				var editFr = new NoteEditFragment();
				transaction.Add(Resource.Id.noteDetailContainer, editFr, "FRAGMENTEDIT");
				this.Title = "Edit Note";
			}

			transaction.Commit();
		}

	}
}
