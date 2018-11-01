using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using NotesCore;

namespace app2
{
	public class Adapter : BaseAdapter<DataModelLocation>
	{
		List<DataModelLocation> locations;
		Activity _activity;
		public Adapter(Activity activity, List<DataModelLocation> loc) : base()
		{
			locations = loc;
			_activity = activity;
		}

		public override DataModelLocation this[int position]
		{
			get
			{
				return locations[position];
			}
		}

		public override int Count
		{
			get
			{
				return locations.Count;
			}
		}

		public override long GetItemId(int position)
		{
			return position;
		}

		public override View GetView(int position, View convertView, ViewGroup parent)
		{
			var view = convertView;
			if (view == null)
			{
				view = _activity.LayoutInflater.Inflate(Android.Resource.Layout.SimpleListItem2, parent, false);
			}
			TextView text = view.FindViewById<TextView>(Android.Resource.Id.Text1);
			TextView address = view.FindViewById<TextView>(Android.Resource.Id.Text2);

			text.Text = locations[position].Title;
			address.Text = locations[position].Address;

			return view;

		}

		public void refresh(List<DataModelLocation> list)
		{
			locations.Clear();
			foreach (DataModelLocation c in list)
			{
				locations.Add(c);
			}

			base.NotifyDataSetChanged();
				
	}
	}
}
