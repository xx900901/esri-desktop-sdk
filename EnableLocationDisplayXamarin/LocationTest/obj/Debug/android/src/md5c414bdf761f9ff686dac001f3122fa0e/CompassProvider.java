package md5c414bdf761f9ff686dac001f3122fa0e;


public abstract class CompassProvider
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Esri.ArcGISRuntime.Location.CompassProvider, Esri.ArcGISRuntime", CompassProvider.class, __md_methods);
	}


	public CompassProvider ()
	{
		super ();
		if (getClass () == CompassProvider.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.Location.CompassProvider, Esri.ArcGISRuntime", "", this, new java.lang.Object[] {  });
	}

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
