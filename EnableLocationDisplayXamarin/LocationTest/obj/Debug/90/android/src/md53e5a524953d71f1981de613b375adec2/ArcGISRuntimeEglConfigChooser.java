package md53e5a524953d71f1981de613b375adec2;


public class ArcGISRuntimeEglConfigChooser
	extends java.lang.Object
	implements
		mono.android.IGCUserPeer,
		android.opengl.GLSurfaceView.EGLConfigChooser
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_chooseConfig:(Ljavax/microedition/khronos/egl/EGL10;Ljavax/microedition/khronos/egl/EGLDisplay;)Ljavax/microedition/khronos/egl/EGLConfig;:GetChooseConfig_Ljavax_microedition_khronos_egl_EGL10_Ljavax_microedition_khronos_egl_EGLDisplay_Handler:Android.Opengl.GLSurfaceView/IEGLConfigChooserInvoker, Mono.Android, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null\n" +
			"";
		mono.android.Runtime.register ("Esri.ArcGISRuntime.Internal.ArcGISRuntimeEglConfigChooser, Esri.ArcGISRuntime", ArcGISRuntimeEglConfigChooser.class, __md_methods);
	}


	public ArcGISRuntimeEglConfigChooser ()
	{
		super ();
		if (getClass () == ArcGISRuntimeEglConfigChooser.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.Internal.ArcGISRuntimeEglConfigChooser, Esri.ArcGISRuntime", "", this, new java.lang.Object[] {  });
	}


	public javax.microedition.khronos.egl.EGLConfig chooseConfig (javax.microedition.khronos.egl.EGL10 p0, javax.microedition.khronos.egl.EGLDisplay p1)
	{
		return n_chooseConfig (p0, p1);
	}

	private native javax.microedition.khronos.egl.EGLConfig n_chooseConfig (javax.microedition.khronos.egl.EGL10 p0, javax.microedition.khronos.egl.EGLDisplay p1);

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
