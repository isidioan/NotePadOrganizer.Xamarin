package md56e50694a3408ec27ef32796f24258eb7;


public class LocationSecondActivity_AddressResultReceiver
	extends android.os.ResultReceiver
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onReceiveResult:(ILandroid/os/Bundle;)V:GetOnReceiveResult_ILandroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("app2.LocationSecondActivity+AddressResultReceiver, app2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", LocationSecondActivity_AddressResultReceiver.class, __md_methods);
	}


	public LocationSecondActivity_AddressResultReceiver (android.os.Handler p0) throws java.lang.Throwable
	{
		super (p0);
		if (getClass () == LocationSecondActivity_AddressResultReceiver.class)
			mono.android.TypeManager.Activate ("app2.LocationSecondActivity+AddressResultReceiver, app2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "Android.OS.Handler, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=84e04ff9cfb79065", this, new java.lang.Object[] { p0 });
	}


	public void onReceiveResult (int p0, android.os.Bundle p1)
	{
		n_onReceiveResult (p0, p1);
	}

	private native void n_onReceiveResult (int p0, android.os.Bundle p1);

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
