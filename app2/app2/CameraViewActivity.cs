
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using NotesCore;

namespace app2
{
	[Activity(Label = "Photo Display", Theme="@style/AppTheme")]
	public class CameraViewActivity : AppCompatActivity
	{
		int id;
	//	byte[] byteArray;
		DataModelCamera obj;
		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.CameraView);
			var intent = Intent;
			id = intent.Extras.GetInt("View");
			var backButton = FindViewById<Button>(Resource.Id.displayBack);
			var deleteButton = FindViewById<Button>(Resource.Id.displayDelete);
			CameraViewModel helper = new CameraViewModel();
			obj = helper.queryOne(id);
			var displayTitle = FindViewById<TextView>(Resource.Id.displayTitle);
			var displayDesc =  FindViewById<TextView>(Resource.Id.displayDesc);
			var displayImage = FindViewById<ImageView>(Resource.Id.displayImage);
			displayTitle.Text = obj.CameraTitle;
			displayDesc.Text = obj.CameraDesc;
			var display = WindowManager.DefaultDisplay;
			var size = new Point();
			display.GetSize(size);
			var width = size.X;
			var height = size.Y;
			var imageHelper = new ImageHelpers(this);
			displayImage.SetImageBitmap(imageHelper.decodeBitmapFromFile(obj.CameraPath, height, width));

			backButton.Click += (s, e) =>
			  {
				  Finish();
			  };
			deleteButton.Click += (s, e) =>
			  {
				  helper.delete(id);
				  Finish();
			  };
			}
	}
}
