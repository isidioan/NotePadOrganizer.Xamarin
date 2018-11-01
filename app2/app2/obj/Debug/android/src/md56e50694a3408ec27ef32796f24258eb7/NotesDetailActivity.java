package md56e50694a3408ec27ef32796f24258eb7;


public class NotesDetailActivity
	extends android.support.v7.app.AppCompatActivity
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("app2.NotesDetailActivity, app2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", NotesDetailActivity.class, __md_methods);
	}


	public NotesDetailActivity () throws java.lang.Throwable
	{
		super ();
		if (getClass () == NotesDetailActivity.class)
			mono.android.TypeManager.Activate ("app2.NotesDetailActivity, app2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	private java.util.ArrayList refList;
	public void monodroidAddReference (java.lang.Object obj)
	{
		if (refList == null)
			refList = new java.util.ArrayList ();
		refList.add (obj);
	}

	public void monodroidClearReferences ()
	{
		if (refList != null)
			refList.clear ();
	}
}
