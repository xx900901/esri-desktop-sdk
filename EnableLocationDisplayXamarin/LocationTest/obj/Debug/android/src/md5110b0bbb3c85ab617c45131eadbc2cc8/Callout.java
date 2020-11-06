package md5110b0bbb3c85ab617c45131eadbc2cc8;


public class Callout
	extends android.widget.FrameLayout
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onAttachedToWindow:()V:GetOnAttachedToWindowHandler\n" +
			"n_onDetachedFromWindow:()V:GetOnDetachedFromWindowHandler\n" +
			"";
		mono.android.Runtime.register ("Esri.ArcGISRuntime.UI.Callout, Esri.ArcGISRuntime", Callout.class, __md_methods);
	}


	public Callout (android.content.Context p0)
	{
		super (p0);
		if (getClass () == Callout.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Callout, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public Callout (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == Callout.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Callout, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public Callout (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == Callout.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Callout, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public Callout (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == Callout.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Callout, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void onAttachedToWindow ()
	{
		n_onAttachedToWindow ();
	}

	private native void n_onAttachedToWindow ();


	public void onDetachedFromWindow ()
	{
		n_onDetachedFromWindow ();
	}

	private native void n_onDetachedFromWindow ();

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
