using System;
using System.Collections.Generic;
using System.IO;
using Android.App;
using Android.Views;
using Android.Widget;
using NotesCore;
using Android.Graphics;
namespace app2
{
	public class CameraAdapter:BaseAdapter<DataModelCamera>
	{
		List<DataModelCamera> _photos;
		Activity _activity;

		public CameraAdapter(Activity activity , List<DataModelCamera> photo ):base()
		{
			_activity = activity;
			_photos = photo;
		}

		public override DataModelCamera this[int position]
		{
			get
			{
				return _photos[position];
			}
		}
		public override int Count
		{
			get
			{
				return _photos.Count;
			}
		}
		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			ViewHolder holder;
			var view = convertView;
			if (view == null)
			{
				view = _activity.LayoutInflater.Inflate(Resource.Layout.CameraCustomRow, parent, false);
				holder = new ViewHolder();
				holder.Image = view.FindViewById<ImageView>(Resource.Id.customImageView);
				holder.TitleCam = view.FindViewById<TextView>(Resource.Id.cameraTitle);
				holder.CamDesc = view.FindViewById<TextView>(Resource.Id.cameraDesc);
				view.Tag = holder;
			}
			else
			{
				holder = view.Tag as ViewHolder;
			}

			holder.TitleCam.Text = _photos[position].CameraTitle;
			holder.CamDesc.Text = _photos[position].CameraDesc;
			var ms = new MemoryStream(_photos[position].CameraPath);
			holder.Image.SetImageBitmap(BitmapFactory.DecodeStream(ms));
			
			return view;
		}

		public void refresh(List<DataModelCamera> list)
		{
			_photos.Clear();
			foreach (DataModelCamera c in list)
			{
				_photos.Add(c);
			}

			base.NotifyDataSetChanged();  
		}

		class ViewHolder:Java.Lang.Object
		{
			public ImageView Image { get; set; }
			public TextView TitleCam { get; set; }
			public TextView CamDesc { get; set; }
		}

	}
}
