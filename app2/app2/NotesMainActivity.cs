
using System;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Views;
using NotesCore;

namespace app2
{
	[Activity(Label = "Notes" , Theme = "@style/AppTheme.NoActionBar",ParentActivity=typeof(MainActivity))]
    [MetaData("android.support.PARENT_ACTIVITY", Value = "app2.MainActivity")]
	public class NotesMainActivity : AppCompatActivity
	{
		
		public const String NOTEID = "noteID";
		public const String NOTETITLE = "noteTitle";
		public const String NOTESUMMARY = "noteSummary";
		public const String FRAGMENTTOCHOOSE = "fragmentToChoose";
		public const String NOTEDATE = "noteDate";

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.NoteMain);
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar2);

			SetSupportActionBar(toolbar);
			SupportActionBar.Title = "Notes";
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);

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
				var intent = new Intent(this, typeof(NotesDetailActivity));
				intent.PutExtra(NotesMainActivity.FRAGMENTTOCHOOSE, "add");
				StartActivity(intent);
				return true;
			}
			return base.OnOptionsItemSelected(item);
		}
	}
}
