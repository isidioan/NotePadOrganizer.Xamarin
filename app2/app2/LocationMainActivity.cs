
using System;
using System.Collections.Generic;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Views;
using Android.Widget;
using NotesCore;

namespace app2
{
	[Activity(Label = "My Locations",ParentActivity=(typeof(MainActivity)),Theme="@style/AppTheme.NoActionBar",ScreenOrientation=Android.Content.PM.ScreenOrientation.Portrait)]
	public class LocationMainActivity : AppCompatActivity
	{
		public const string LocationView = "locationView";
		public const string ItemSelected = "item";
		LocationViewModel helper;
		List<DataModelLocation> locations;
		Adapter adapter;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.LocationMain);
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar4);
			SetSupportActionBar(toolbar);
			SupportActionBar.SetDisplayHomeAsUpEnabled(true);
			SupportActionBar.SetHomeButtonEnabled(true);
			helper = new LocationViewModel();
			helper.CreateTable();
			locations = helper.queryAll();
			var listview = FindViewById<ListView>(Resource.Id.locationContainer);
			adapter = new Adapter(this, locations);
			listview.Adapter = adapter;
			listview.ItemClick += ListClick;

			                                                 
		}
		protected override void OnResume()
		{
			base.OnResume();
			locations = helper.queryAll();
			adapter.refresh(locations);
		}

		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.add_menu, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			var id = item.ItemId;
			if(id==Resource.Id.add_icon){

				Intent intent = new Intent(this, typeof(LocationSecondActivity));
				intent.PutExtra(LocationMainActivity.LocationView,false);
				StartActivity(intent);
        }
			return base.OnOptionsItemSelected(item);
		}

		void ListClick(object sender, AdapterView.ItemClickEventArgs e)
		{
			var id = locations[e.Position].Id;
			var intent = new Intent(this, typeof(LocationSecondActivity));
			intent.PutExtra(ItemSelected, id);
			intent.PutExtra(LocationView, true);
			StartActivity(intent);
		}

	}


}
