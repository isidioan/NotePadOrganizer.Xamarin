
using System;
using System.Collections.Generic;
using Uri = Android.Net.Uri;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Common.Apis;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using NotesCore;

namespace app2
{
	[Activity(Label = "New Location",ParentActivity=typeof(LocationMainActivity),Theme="@style/AppTheme",ScreenOrientation=ScreenOrientation.Portrait)]
	public class LocationSecondActivity : AppCompatActivity,View.IOnClickListener, GoogleApiClient.IConnectionCallbacks,
	GoogleApiClient.IOnConnectionFailedListener, Android.Gms.Location.ILocationListener,IResultCallback
	{
        
		LocationViewModel helper;
		GoogleApiClient myClient;
		LocationRequest myLocationRequest;
		AddressResultReceiver resultReceiver;
		Location location;
		EditText editTitleLocation, addressText, latText, longText;
		int id;
		bool view,perm;
		static string address;
		DataModelLocation loc ;
		static string[] EXTERNAL_PERMISSIONS = {
			Manifest.Permission.AccessFineLocation, Manifest.Permission.AccessCoarseLocation, Manifest.Permission.Internet};
		const int EXTERNAL_CODE=1;
	    double longitude, latitude;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.LocationSecond);
			editTitleLocation = FindViewById<EditText>(Resource.Id.editTitleLocation);
			addressText = FindViewById<EditText>(Resource.Id.addressText);
			latText = FindViewById<EditText>(Resource.Id.latitudeText);
			longText=FindViewById<EditText>(Resource.Id.longitudeText);
			var saveLocationButton = FindViewById<Button>(Resource.Id.saveLocation);
			var deleteLocationButton = FindViewById<Button>(Resource.Id.deleteLocation);
			var findLocationButton = FindViewById<Button>(Resource.Id.findLocation);
			var navigateButton= FindViewById<Button>(Resource.Id.navigateLocation);
			saveLocationButton.SetOnClickListener(this);
			deleteLocationButton.SetOnClickListener(this);
			findLocationButton.SetOnClickListener(this);
			navigateButton.SetOnClickListener(this);
			helper = new LocationViewModel();
			var intent = Intent;
			if (view=intent.Extras.GetBoolean(LocationMainActivity.LocationView))
			{
				 id = intent.Extras.GetInt(LocationMainActivity.ItemSelected);
				 loc = helper.queryOne(id);
				address = loc.Address;
				latitude = loc.Latitude;
				longitude = loc.Longitude;
				editTitleLocation.Text = loc.Title;
				addressText.Text = loc.Address;
				latText.Text = System.Convert.ToString(loc.Latitude) ;
				longText.Text = System.Convert.ToString(loc.Longitude);
			}
			buildGoogleApi();
		}

		protected override void OnStart()
		{
			base.OnStart();
			myClient.Connect();
		}

		protected override void OnResume()
		{
			base.OnResume();
			if (myClient.IsConnected)
			{
				startLocationUpdates();
			}
		}

		protected override void OnPause()
		{
			base.OnPause();
			if(myClient.IsConnected)
       		 stopLocationUpdates();
		}
		protected override void OnStop()
		{
			base.OnStop();
			myClient.Disconnect();
		}

		void buildGoogleApi()
		{
			myClient = new GoogleApiClient.Builder(this)
										.AddConnectionCallbacks(this)
										.AddOnConnectionFailedListener(this)
										  .AddApi(LocationServices.API).Build();
		}

		void createRequest()
		{
			myLocationRequest = new LocationRequest();
			myLocationRequest.SetInterval(15000)
                        .SetFastestInterval(10000)
                        .SetSmallestDisplacement(5)
			                 .SetPriority(LocationRequest.PriorityHighAccuracy);
			var builder = new LocationSettingsRequest.Builder().AddLocationRequest(myLocationRequest);
			builder.SetAlwaysShow(true);
			var result = LocationServices.SettingsApi.CheckLocationSettings(myClient, builder.Build());
			result.SetResultCallback(this);
		}

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			switch (requestCode)
			{
				case EXTERNAL_CODE:
					if(grantResults[0]==Permission.Granted && grantResults[1] == Permission.Granted)
					{

						startLocationUpdates();
					}
					else{

						Toast.MakeText(this,"Some Permissions denied", ToastLength.Long).Show();
						ActivityCompat.RequestPermissions(this, EXTERNAL_PERMISSIONS, EXTERNAL_CODE);

					}
					break;
			}
		}

		void retrieveLocation()
		{
			try
			{
				location = LocationServices.FusedLocationApi.GetLastLocation(myClient);
			}
			catch (SecurityException ex)
			{
				Console.Write(ex.ToString());
			}
		}

		void startLocationUpdates()
		{
			if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessFineLocation) == (int)Permission.Granted &&
				ContextCompat.CheckSelfPermission(this, Manifest.Permission.AccessCoarseLocation) == (int)Permission.Granted)
			{
				LocationServices.FusedLocationApi.RequestLocationUpdates(myClient, myLocationRequest, this);
				perm = true;
			}
			else
			{
				ActivityCompat.RequestPermissions(this, EXTERNAL_PERMISSIONS, EXTERNAL_CODE);
			}
		}

		void stopLocationUpdates()
		{
			LocationServices.FusedLocationApi.RemoveLocationUpdates(myClient,this);
			 perm=false;
		}

		 void View.IOnClickListener.OnClick(View v)
		{

			switch (v.Id)
			{
				case Resource.Id.findLocation:
					retrieveLocation();
					resultReceiver =new AddressResultReceiver(this, null, latText, longText,addressText, location);

					if (location != null)
					{
						latitude = location.Latitude;
						longitude = location.Longitude;
						startIntentSetvice();
					}

					break;
				case Resource.Id.navigateLocation:
					var uri = "https://www.google.com/maps/dir/?api=1&destination=";
					Uri dir = Uri.Parse(uri + Uri.Encode(address));
					var intent = new Intent(Intent.ActionView, dir);
					StartActivity(intent);
					break;
				case Resource.Id.saveLocation:
					var titleLocation = editTitleLocation.Text;
					if (!view)
					{
						try
						{
							DataModelLocation dummy = new DataModelLocation();
							dummy.Title = titleLocation;
							dummy.Address = address;
							dummy.Latitude = latitude;
							dummy.Longitude = longitude;
							helper.insert(dummy);
							Toast.MakeText(this, "Location Saved", ToastLength.Short).Show();
						}
						catch (System.Exception e)
						{
							Console.Write(e.StackTrace);
						}
					}
					else
					{
						try
						{
							helper.update(id, titleLocation, address, latitude, longitude);
							Toast.MakeText(this, "Location Edited", ToastLength.Short).Show();
						}
						catch (System.Exception e)
						{
							Console.Write(e.StackTrace);
						}
					}
			   			Finish();
						break;
				case Resource.Id.deleteLocation:
					helper.delete(id); 
					Finish();
					break;
			}

		}

		public void OnConnected(Bundle connectionHint)
		{
			createRequest();
		}

		public void OnConnectionSuspended(int cause)
		{
			Toast.MakeText(this,"Google Api coonection suspended", ToastLength.Short).Show();
		}

		public void OnConnectionFailed(ConnectionResult result)
		{
			Toast.MakeText(this,"Google Api can not connect", ToastLength.Short).Show();
		}

		public void OnLocationChanged(Location location)
		{
			this.location = location;
			latitude = location.Latitude;
			longitude = location.Longitude;
		}

		public void OnResult(Java.Lang.Object arg)
		{
			var result = arg as LocationSettingsResult;
			switch (result.Status.StatusCode)
			{
				case CommonStatusCodes.Success:
					startLocationUpdates();
					break;
				case CommonStatusCodes.ResolutionRequired:
					try
					{
						result.Status.StartResolutionForResult(this, 1);
					}
					catch (IntentSender.SendIntentException)
					{
					}
					break;
				case LocationSettingsStatusCodes.SettingsChangeUnavailable:
					Toast.MakeText(this, "Settings Change Unvailable", ToastLength.Short).Show();
					break;
			}
		}

		private void startIntentSetvice()
		{

			Intent intent = new Intent(this, typeof(BackgroundThread));
       		 intent.PutExtra("receiver",resultReceiver);
       		 intent.PutExtra("location",location);
			StartService(intent);
    }

		class AddressResultReceiver : ResultReceiver
		{
			Location _location;
			Activity _a;
			EditText _t1, _t2, _t3;
			public AddressResultReceiver(Activity a ,Handler handler,EditText t1 ,EditText t2, EditText t3,Location location) : base(handler)
			{
				_a = a;
				_t1 = t1;
				_t2 = t2;
				_t3 = t3;
				_location = location;

			}

			public string add { get; set; }
			protected override void OnReceiveResult(int resultCode, Bundle resultData)
			{
				if (resultCode == (int)Result.Ok)
				{
					
					add = resultData.GetString("Address Result");
					address = add;
					_a.RunOnUiThread(() =>
					{
						_t1.Text = _location.Latitude.ToString();
						_t2.Text = _location.Longitude.ToString();
						_t3.Text = add;
					} );
				}

			}

		}

	}																																														
}
