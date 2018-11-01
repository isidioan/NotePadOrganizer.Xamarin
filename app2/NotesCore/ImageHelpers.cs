using System;
using System.IO;
using Android.App;
using Android.Graphics;
using Android.Util;
using Uri = Android.Net.Uri;
namespace NotesCore
{
	public class ImageHelpers
	{
		Activity _activity;
		public ImageHelpers(Activity activity)
		{
			_activity = activity;
		}
 
		public byte[] convertToByte(Bitmap bitmap)
		{
			using (MemoryStream ms = new MemoryStream())
			{
				bitmap.Compress(Bitmap.CompressFormat.Jpeg, 50, ms);
				return ms.ToArray();
			}
		}

		public Bitmap decodeUri(Uri image, int size)
		{
			try
			{
				var options = new BitmapFactory.Options();
				options.InJustDecodeBounds = true;
				BitmapFactory.DecodeStream(_activity.ContentResolver.OpenInputStream(image), null, options);
				var width = options.OutWidth;
				var height = options.OutHeight;
				var scale = 1;
				while (true)
				{
					if (width / 2 < size || height / 2 < size)
						break;
					width /= 2;
					height /= 2;
					scale *= 2;
				}
				var options2 = new BitmapFactory.Options();
				options2.InSampleSize = scale;
				return BitmapFactory.DecodeStream(_activity.ContentResolver.OpenInputStream(image), null, options2);
			}
			catch (Exception e)
			{
				Log.Info("Error",e.Message);
			}
			return null;
		}

		int calculateSampleSize(BitmapFactory.Options options, int rwidth, int rheight)
		{
			var width = options.OutWidth;
			var height = options.OutHeight;
			var sampleSize = 1;

			if (height > rheight || width > rwidth)
			{
				var halfHeight = height / 2;
				var halfWidth = width / 2;
				while ((halfHeight / sampleSize) > rheight && (halfWidth / sampleSize) > rwidth)
				{
					sampleSize *= 2;
				}
			}
			return sampleSize;

		}

		public Bitmap decodeBitmapFromFile(byte[] byteArray, int rheight, int rwidth)
		{
			var option = new BitmapFactory.Options();
			option.InJustDecodeBounds = true;
			BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length, option);
			option.InSampleSize = calculateSampleSize(option, rwidth, rheight);
			option.InJustDecodeBounds = false;
			return BitmapFactory.DecodeByteArray(byteArray, 0, byteArray.Length, option);
		}


	}
}
