
using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace app2
{
	public class NoteDetailFragment : Fragment
	{
		 string title;
		 string summary;
		string date;

		public NoteDetailFragment()
			{
				// Required empty public constructo  
		}

		public override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);

			// Create your fragment here
		}

		public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
		{
			
			var view = inflater.Inflate(Resource.Layout.NoteDetailFragment, container, false);
			var intent = this.Activity.Intent;
			title = intent.GetStringExtra(NotesMainActivity.NOTETITLE);
			summary = intent.GetStringExtra(NotesMainActivity.NOTESUMMARY);
			date = intent.GetStringExtra(NotesMainActivity.NOTEDATE);
			var titleView = view.FindViewById<TextView>(Resource.Id.detailTitle);
			var summaryView = view.FindViewById<TextView>(Resource.Id.detailSummary);
			var dateView = view.FindViewById<TextView>(Resource.Id.detailDate);
			titleView.Text = title;
			summaryView.Text = summary;
			dateView.Text = date;
			return view;
		}
	}
}
