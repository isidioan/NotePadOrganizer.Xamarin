using System;
using System.Collections.Generic;
using Plugin.Geolocator;
using Xamarin.Forms.Maps;
namespace NotesCore
{
	public class Locator
	{
		double longitude { get; set; }
		double latitude { get; set; }
		Geocoder geocoder;
		String address {get; set;}
		public Locator()
		{
			geocoder = new Geocoder();
		}

		public async void  getPosition(){
			
			var locator = CrossGeolocator.Current;
			locator.DesiredAccuracy = 50;

			var position = await locator.GetPositionAsync(300000);
			longitude = position.Longitude;
			latitude = position.Longitude;
			var pos = new Position(latitude, longitude);
			var addresses = await geocoder.GetAddressesForPositionAsync(pos);
			foreach (var item in addresses)
			{
				address += item;
			}
		}
}
