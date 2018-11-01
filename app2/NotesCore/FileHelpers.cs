using System;



using Environment = Android.OS.Environment;
using Java.IO;
using Java.Util;
using Java.Text;

namespace NotesCore
{
	public class FileHelpers
	{
		public string CurrentPhotoPath{get; set;}
		public File createFile()
		{
			var date = DateTime.Now.ToString("yy_mm_dd_hh_mm_ss");
			var imageName = date + "_.jpg";
           
			File storageDir = Environment.GetExternalStoragePublicDirectory(Environment.DirectoryPictures);
			   
			File imageFile = new File(storageDir,imageName);
			CurrentPhotoPath = "file:" + imageFile.AbsolutePath;
			return imageFile;
		}
	}
}
