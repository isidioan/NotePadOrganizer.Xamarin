
using System;
using System.Collections.Generic;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Support.Compat;
using Android.Support.V4.App;
using Android.Support.V4.Content;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;
using Java.IO;
using NotesCore;
using Uri = Android.Net.Uri;
namespace app2
{
	[Activity(Label = "Camera Notes", Theme = "@style/AppTheme", ParentActivity = (typeof(MainActivity)),ScreenOrientation=ScreenOrientation.Portrait)]
	public class CameraMainActivity : AppCompatActivity, View.IOnClickListener, AdapterView.IOnItemClickListener
	{
		List<DataModelCamera> photo;
		CameraViewModel helper;
		CameraAdapter adapter;
		GridView gridView;
		ImageView imageButton;
		FileHelpers file;
		Android.App.AlertDialog alertOption;
		public const int GalleryCode = 1;
		public const int CameraCode = 0;
		static  string[] RPermissions = { Manifest.Permission.WriteExternalStorage,
			Manifest.Permission.ReadExternalStorage, Manifest.Permission.Camera};
		public const int PermCode = 3;
		bool perm;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.CameraMain);
			helper = new CameraViewModel();
			helper.CreateTable();
			gridView = FindViewById<GridView>(Resource.Id.gridviewPhoto);
			photo = helper.queryAll();
			adapter = new CameraAdapter(this, photo);
			gridView.Adapter = adapter;
			imageButton = FindViewById<ImageView>(Resource.Id.cameraOptionButton);
			imageButton.SetOnClickListener(this);
			gridView.OnItemClickListener = this;
			createOptionWindow();
			permissionsGrant();
		}

		protected override void OnResume()
		{
			base.OnResume();
			photo = helper.queryAll();
			adapter.refresh(photo);
		}


		void View.IOnClickListener.OnClick(View v)
		{
			if (v.Id == Resource.Id.cameraOptionButton)
			{
				alertOption.Show();
			}
		}

		void createOptionWindow()
		{
			var alertBuilder = new Android.App.AlertDialog.Builder(this);
			alertBuilder.SetTitle("Select an Option");
			alertBuilder.SetItems(Resource.Array.alert_dialog, (Object sender, DialogClickEventArgs e) =>
			{
				if (e.Which==0)
				{
					if (perm)
					{
						activateCamera();
					}
				}
				else
				{
					if (perm)
					{
						chooseFromGallery();
					}
				}
			});

			alertOption = alertBuilder.Create();
		}

		void chooseFromGallery()
		{
			Intent galleryIntent = new Intent(Intent.ActionPick);
			galleryIntent.SetType("image/*");
			StartActivityForResult(galleryIntent, GalleryCode);
		}

		void activateCamera()
		{
			file = new FileHelpers();
			Intent cameraIntent = new Intent(MediaStore.ActionImageCapture);
			File filePath = file.createFile();
			if (filePath != null)
			{
				if (Build.VERSION.SdkInt > BuildVersionCodes.M)
				{
					Uri photoUri = FileProvider.GetUriForFile(ApplicationContext,"com.isidioannou.app2.CameraMainActivity", filePath);
					cameraIntent.PutExtra(MediaStore.ExtraOutput, photoUri);
					cameraIntent.AddFlags(ActivityFlags.GrantWriteUriPermission);
					StartActivityForResult(cameraIntent, CameraCode);
				}
				else
				{

					Uri uri = Uri.FromFile(filePath);
					cameraIntent.PutExtra(MediaStore.ExtraOutput, uri);
					StartActivityForResult(cameraIntent, CameraCode);
				}

			}
		}

		protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
		{
			base.OnActivityResult(requestCode, resultCode, data);
			byte[] byteArray = null;
			var imageHelper = new ImageHelpers(this);
			if (requestCode == CameraCode && resultCode == Result.Ok)
			{
				
				var s = file.CurrentPhotoPath;
				Uri image = Uri.Parse(file.CurrentPhotoPath);
				Bitmap bitmap = imageHelper.decodeUri(image, 500);
				byteArray = imageHelper.convertToByte(bitmap);
			}
			else if (requestCode == GalleryCode && resultCode == Result.Ok && data != null)
			{
				Uri image = data.Data;
				Bitmap bitmap = imageHelper.decodeUri(image, 500);
				byteArray = imageHelper.convertToByte(bitmap);
			}
			if (byteArray != null)
			{
				Intent intent = new Intent(this, typeof(CameraSaveActivity));
				intent.PutExtra("byteImage", byteArray);
				StartActivity(intent);
			}
		}

		public void OnItemClick(AdapterView parent, View view, int position, long id)
		{
			var obj = photo[position];
			Intent intent = new Intent(this, typeof(CameraViewActivity));
			intent.PutExtra("View", obj.Id);
			StartActivity(intent);

		
		}

		void permissionsGrant()
		{
			var w = ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage);
			var r = ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage);
			var s = ContextCompat.CheckSelfPermission(this, Manifest.Permission.Camera);

			if (w != (int)Permission.Granted && r != (int)Permission.Granted && s!= (int)Permission.Granted)
			{
				ActivityCompat.RequestPermissions(this, RPermissions, PermCode);
			}
			else
			{
				perm = true;
			}
		}


		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
			switch (requestCode)
			{
				case PermCode:

					if(grantResults[0]==Permission.Granted && grantResults[1] == Permission.Granted && grantResults[2] == Permission.Granted)
					{

						perm = true;
					}
					else{

						Toast.MakeText(this,"Some Permissions denied", ToastLength.Long).Show();
						ActivityCompat.RequestPermissions(this, RPermissions, PermCode);
						perm = false;
					}

					break;
			}
		}
	}
}
