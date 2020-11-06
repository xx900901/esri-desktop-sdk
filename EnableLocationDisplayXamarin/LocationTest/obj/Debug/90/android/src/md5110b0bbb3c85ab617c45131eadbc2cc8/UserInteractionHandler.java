package md5110b0bbb3c85ab617c45131eadbc2cc8;


public abstract class UserInteractionHandler
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Esri.ArcGISRuntime.UI.UserInteractionHandler, Esri.ArcGISRuntime", UserInteractionHandler.class, __md_methods);
	}


	public UserInteractionHandler ()
	{
		super ();
		if (getClass () == UserInteractionHandler.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.UserInteractionHandler, Esri.ArcGISRuntime", "", this, new java.lang.Object[] {  });
	}

	public UserInteractionHandler (md580c0c595af36e290ae80283596c6764a.GeoView p0)
	{
		super ();
		if (getClass () == UserInteractionHandler.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.UserInteractionHandler, Esri.ArcGISRuntime", "Esri.ArcGISRuntime.UI.Controls.GeoView, Esri.ArcGISRuntime", this, new java.lang.Object[] { p0 });
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
