using System;
using System.Collections.Generic;
using Android.App;
using Android.Views;
using Android.Widget;
using NotesCore;

namespace app2
{
	public class TaskAdapter:BaseAdapter<DataModelToDo>,CompoundButton.IOnCheckedChangeListener
	{
		readonly Activity _activity;
		List<DataModelToDo> _tasks;
		TodoViewModel _helper;
		public TaskAdapter(Activity activity,List<DataModelToDo> tasks,TodoViewModel helper):base()
		{
			_activity = activity;
			_tasks = tasks;
			_helper = helper;
		}

		public override int Count
		{
			get
			{
				return _tasks.Count;
			}
		}
		public override DataModelToDo this[int position]
		{
			get
			{
				return _tasks[position];
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
				holder = new ViewHolder();
				view = _activity.LayoutInflater.Inflate(Resource.Layout.RowTask, parent, false);
				holder.Checkholder = view.FindViewById<CheckBox>(Resource.Id.toDoCheckBox);
				holder.Textholder = view.FindViewById<TextView>(Resource.Id.toDoText);
				view.Tag = holder;
			}
			else
			{
				holder = view.Tag as ViewHolder;
			}
			CheckBox cb = holder.Checkholder;
			cb.Tag = position;
			holder.Textholder.Text = _tasks[position].TodoTitle;
			cb.SetOnCheckedChangeListener(this);
			bool status = (_tasks[position].Checked == 1 ? true : false);
			if (status)
			{
				cb.Checked=status;
				holder.Textholder.PaintFlags=(holder.Textholder.PaintFlags | Android.Graphics.PaintFlags.StrikeThruText);
			}
			else
			{
				cb.Checked = status;
				holder.Textholder.PaintFlags=(holder.Textholder.PaintFlags & (~ Android.Graphics.PaintFlags.StrikeThruText));
			}

			return view;
		}

		public void OnCheckedChanged(CompoundButton buttonView, bool isChecked)
		{
			int pos = (int)buttonView.Tag ;
			int rowId = _tasks[pos].Id;
			if (isChecked)
			{
				_helper.updateCheck(rowId, 1);
			}
			else
			{
				_helper.updateCheck(rowId, 0);
			}
		}

		public void refresh(List<DataModelToDo> list)
		{
			_tasks.Clear();
			foreach (DataModelToDo t in list)
			{
				_tasks.Add(t);
			}
			base.NotifyDataSetChanged();  
		}

		private class ViewHolder : Java.Lang.Object
	{
	public TextView Textholder { get; set; }
	public CheckBox Checkholder { get; set; }
	}
		}

}
