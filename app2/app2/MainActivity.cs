using Android.App;
using Android.OS;
using System.Text;
using Android.Support.V7.App;
using Toolbar = Android.Support.V7.Widget.Toolbar;
using Android.Content.Res;
using System.IO;
using Android.Views;
using Android.Support.Constraints;
using Android.Widget;
using Android.Content;

namespace app2
{
	[Activity(MainLauncher = true, Theme = "@style/AppTheme.NoActionBar")]
	public class MainActivity : AppCompatActivity
	{
	    int Height { set; get;}
		int Width { set; get;}
		StringBuilder builder = new StringBuilder();
		TextView pop;

		protected override void OnCreate(Bundle savedInstanceState)
		{
			base.OnCreate(savedInstanceState);
			SetContentView(Resource.Layout.Main1);
			var toolbar = FindViewById<Toolbar>(Resource.Id.toolbar1);
			SetSupportActionBar(toolbar);
			SupportActionBar.Title = "Notepad Organizer";
			var metrics = Resources.DisplayMetrics;
			Height = metrics.HeightPixels;
			Width = metrics.WidthPixels;
			createText();
			var c1 = FindViewById<RelativeLayout>(Resource.Id.clayoutNotes);
			var c2 = FindViewById<RelativeLayout>(Resource.Id.clayoutTasks);
			var c3 = FindViewById<RelativeLayout>(Resource.Id.clayoutCamera);
			var c4 = FindViewById<RelativeLayout>(Resource.Id.clayoutLocation);
			c1.Click += (sender, e) =>
			{
				Intent intent = new Intent(this, typeof(NotesMainActivity));
				StartActivity(intent);
			};
			c2.Click += (sender, e)=>
			{
				Intent intent = new Intent(this, typeof(TaskMainActivity));
				StartActivity(intent);
			};
			c3.Click += (sender, e) =>
			  {
				  Intent intent = new Intent(this, typeof(CameraMainActivity));
				  StartActivity(intent);
			  };
			c4.Click += (IntentSender, e) =>
			  {
				  Intent intent = new Intent(this, typeof(LocationMainActivity));
				  StartActivity(intent);
			  };
		}
		public override bool OnCreateOptionsMenu(IMenu menu)
		{
			MenuInflater.Inflate(Resource.Menu.menu_top, menu);
			return base.OnCreateOptionsMenu(menu);
		}

		public override bool OnOptionsItemSelected(IMenuItem item)
		{
			int id = item.ItemId;
			if (id == Resource.Id.help_button)
			{
				var inflater = (LayoutInflater)GetSystemService(LayoutInflaterService);
				var view = inflater.Inflate(Resource.Layout.pop_up_window,(ConstraintLayout)FindViewById(Resource.Id.popupContainer));
				pop = view.FindViewById<TextView>(Resource.Id.popupBody);
				pop.Text = builder.ToString();
				var window = new PopupWindow(view, Width, Height, true);
				window.ShowAtLocation(view, GravityFlags.Center, 0, 0);
				var closeButton = view.FindViewById<ImageView>(Resource.Id.imageClose);
				closeButton.Click += (sender, e) =>
				{
					window.Dismiss();
				};
			}
			return base.OnOptionsItemSelected(item);
		}

		 void createText()
		{
			string line;
			AssetManager asset = Assets;
			using (StreamReader breader = new StreamReader(asset.Open("instructions1.txt")))
			{
				while ((line = breader.ReadLine()) != null)
				{
					builder.Append(line);
					builder.Append("\n");
				}
				breader.Close();
			}
		}
	}
}

