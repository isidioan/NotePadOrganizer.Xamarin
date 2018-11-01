
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Locations;
using Android.OS;
using Android.Text;
using Java.IO;
using Java.Lang;
using Java.Util;

namespace app2
{
	[Service(Label = "BackgroundThread")]

	public class BackgroundThread : IntentService
	{
		ResultReceiver resultReceiver;
		public static int SUCCESS = 100;
		public static int FAIL = 101;
		public BackgroundThread() : base("Reverse")
		{
		}

		protected override void OnHandleIntent(Intent intent)
		{
			var geo = new Geocoder(this, Java.Util.Locale.Default);
			resultReceiver = intent.GetParcelableExtra("receiver") as ResultReceiver;
			var location = intent.GetParcelableExtra("location") as Location;
			System.Collections.Generic.IList<Address> addresses = null;
			try
			{
				addresses = geo.GetFromLocation(location.Latitude, location.Longitude, 1);
			}
			catch (IOException e)
			{
				e.PrintStackTrace();
			}
			catch (IllegalArgumentException ex)
			{
				ex.PrintStackTrace();
			}
			if (addresses == null || addresses.Count == 0)
			{
				var bundle = new Bundle();
				bundle.PutString("AddressResult", "No Addresses");
				resultReceiver.Send(Result.Canceled, bundle);
			}
			else
			{
				var address = addresses[0];
				var printAdress = new ArrayList();
				for (int i = 0; i <= address.MaxAddressLineIndex; i++)
				{
					printAdress.Add(address.GetAddressLine(i));
				}
				var bundle = new Bundle();
				bundle.PutString("Address Result", TextUtils.Join(JavaSystem.GetProperty("line.separator"), printAdress));
				resultReceiver.Send(Result.Ok, bundle);



			}




		}
	}
}
