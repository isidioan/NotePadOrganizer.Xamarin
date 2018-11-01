
using System;
using System.IO;

using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Util;
using Android.Widget;
using NotesCore;

namespace app2
{
	[Activity(Label = "New Photo",Theme="@style/AppTheme" ,ParentActivity=(typeof(CameraMainActivity)))]
	public class CameraSaveActivity : AppCompatActivity
	{
		byte[] byteArray;
		EditText editTitle, editDesc;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.CameraSave);
			 editTitle = FindViewById<EditText>(Resource.Id.cameraEditTitle);
			 editDesc = FindViewById<EditText>(Resource.Id.cameraEditDesc);
			var imagePrev = FindViewById<ImageView>(Resource.Id.cameraPreview);
			var saveButton = FindViewById<Button>(Resource.Id.saveCameraButton);
			var intent = Intent;
			byteArray = intent.GetByteArrayExtra("byteImage");
			var stream = new MemoryStream(byteArray);
			imagePrev.SetImageBitmap(BitmapFactory.DecodeStream(stream));
			saveButton.Click += ButtonClick;
		}


		void ButtonClick(object sender, EventArgs e)
		{
			DataModelCamera cm = new DataModelCamera();
			cm.CameraTitle = editTitle.Text;
			cm.CameraDesc = editDesc.Text;
			cm.CameraPath = byteArray;
			CameraViewModel helper = new CameraViewModel();
			try {
                helper.insert(cm);
				Toast.MakeText(this,"Saved successfully", ToastLength.Short).Show();
				}
			catch (Exception ex)
			{
				Log.Info("Error saving", ex.Message);
            }
			Finish();
		}
	}
}
