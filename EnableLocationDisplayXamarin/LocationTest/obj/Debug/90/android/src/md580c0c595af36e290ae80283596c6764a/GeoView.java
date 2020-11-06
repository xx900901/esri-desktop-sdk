package md580c0c595af36e290ae80283596c6764a;


public abstract class GeoView
	extends android.view.ViewGroup
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onMeasure:(II)V:GetOnMeasure_IIHandler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"n_addView:(Landroid/view/View;)V:GetAddView_Landroid_view_View_Handler\n" +
			"n_addView:(Landroid/view/View;I)V:GetAddView_Landroid_view_View_IHandler\n" +
			"n_addView:(Landroid/view/View;ILandroid/view/ViewGroup$LayoutParams;)V:GetAddView_Landroid_view_View_ILandroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_addView:(Landroid/view/View;II)V:GetAddView_Landroid_view_View_IIHandler\n" +
			"n_addView:(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V:GetAddView_Landroid_view_View_Landroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_removeAllViews:()V:GetRemoveAllViewsHandler\n" +
			"n_removeView:(Landroid/view/View;)V:GetRemoveView_Landroid_view_View_Handler\n" +
			"n_removeViewAt:(I)V:GetRemoveViewAt_IHandler\n" +
			"n_removeViews:(II)V:GetRemoveViews_IIHandler\n" +
			"n_removeAllViewsInLayout:()V:GetRemoveAllViewsInLayoutHandler\n" +
			"n_removeViewInLayout:(Landroid/view/View;)V:GetRemoveViewInLayout_Landroid_view_View_Handler\n" +
			"n_removeViewsInLayout:(II)V:GetRemoveViewsInLayout_IIHandler\n" +
			"n_bringChildToFront:(Landroid/view/View;)V:GetBringChildToFront_Landroid_view_View_Handler\n" +
			"n_getChildAt:(I)Landroid/view/View;:GetGetChildAt_IHandler\n" +
			"n_indexOfChild:(Landroid/view/View;)I:GetIndexOfChild_Landroid_view_View_Handler\n" +
			"n_addChildrenForAccessibility:(Ljava/util/ArrayList;)V:GetAddChildrenForAccessibility_Ljava_util_ArrayList_Handler\n" +
			"n_requestChildFocus:(Landroid/view/View;Landroid/view/View;)V:GetRequestChildFocus_Landroid_view_View_Landroid_view_View_Handler\n" +
			"n_requestChildRectangleOnScreen:(Landroid/view/View;Landroid/graphics/Rect;Z)Z:GetRequestChildRectangleOnScreen_Landroid_view_View_Landroid_graphics_Rect_ZHandler\n" +
			"n_clearChildFocus:(Landroid/view/View;)V:GetClearChildFocus_Landroid_view_View_Handler\n" +
			"n_clearDisappearingChildren:()V:GetClearDisappearingChildrenHandler\n" +
			"n_addStatesFromChildren:()Z:GetAddStatesFromChildrenHandler\n" +
			"n_childDrawableStateChanged:(Landroid/view/View;)V:GetChildDrawableStateChanged_Landroid_view_View_Handler\n" +
			"n_getChildVisibleRect:(Landroid/view/View;Landroid/graphics/Rect;Landroid/graphics/Point;)Z:GetGetChildVisibleRect_Landroid_view_View_Landroid_graphics_Rect_Landroid_graphics_Point_Handler\n" +
			"n_setAddStatesFromChildren:(Z)V:GetSetAddStatesFromChildren_ZHandler\n" +
			"n_setClipChildren:(Z)V:GetSetClipChildren_ZHandler\n" +
			"n_shouldDelayChildPressedState:()Z:GetShouldDelayChildPressedStateHandler\n" +
			"n_showContextMenuForChild:(Landroid/view/View;)Z:GetShowContextMenuForChild_Landroid_view_View_Handler\n" +
			"n_startActionModeForChild:(Landroid/view/View;Landroid/view/ActionMode$Callback;)Landroid/view/ActionMode;:GetStartActionModeForChild_Landroid_view_View_Landroid_view_ActionMode_Callback_Handler\n" +
			"n_focusableViewAvailable:(Landroid/view/View;)V:GetFocusableViewAvailable_Landroid_view_View_Handler\n" +
			"n_requestTransparentRegion:(Landroid/view/View;)V:GetRequestTransparentRegion_Landroid_view_View_Handler\n" +
			"n_requestSendAccessibilityEvent:(Landroid/view/View;Landroid/view/accessibility/AccessibilityEvent;)Z:GetRequestSendAccessibilityEvent_Landroid_view_View_Landroid_view_accessibility_AccessibilityEvent_Handler\n" +
			"n_recomputeViewAttributes:(Landroid/view/View;)V:GetRecomputeViewAttributes_Landroid_view_View_Handler\n" +
			"n_onRequestSendAccessibilityEvent:(Landroid/view/View;Landroid/view/accessibility/AccessibilityEvent;)Z:GetOnRequestSendAccessibilityEvent_Landroid_view_View_Landroid_view_accessibility_AccessibilityEvent_Handler\n" +
			"n_endViewTransition:(Landroid/view/View;)V:GetEndViewTransition_Landroid_view_View_Handler\n" +
			"n_focusSearch:(Landroid/view/View;I)Landroid/view/View;:GetFocusSearch_Landroid_view_View_IHandler\n" +
			"n_invalidateChildInParent:([ILandroid/graphics/Rect;)Landroid/view/ViewParent;:GetInvalidateChildInParent_arrayILandroid_graphics_Rect_Handler\n" +
			"n_startViewTransition:(Landroid/view/View;)V:GetStartViewTransition_Landroid_view_View_Handler\n" +
			"n_updateViewLayout:(Landroid/view/View;Landroid/view/ViewGroup$LayoutParams;)V:GetUpdateViewLayout_Landroid_view_View_Landroid_view_ViewGroup_LayoutParams_Handler\n" +
			"n_getChildCount:()I:GetGetChildCountHandler\n" +
			"n_getFocusedChild:()Landroid/view/View;:GetGetFocusedChildHandler\n" +
			"";
		mono.android.Runtime.register ("Esri.ArcGISRuntime.UI.Controls.GeoView, Esri.ArcGISRuntime", GeoView.class, __md_methods);
	}


	public GeoView (android.content.Context p0)
	{
		super (p0);
		if (getClass () == GeoView.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Controls.GeoView, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
	}


	public GeoView (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == GeoView.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Controls.GeoView, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
	}


	public GeoView (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == GeoView.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Controls.GeoView, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
	}


	public GeoView (android.content.Context p0, android.util.AttributeSet p1, int p2, int p3)
	{
		super (p0, p1, p2, p3);
		if (getClass () == GeoView.class)
			mono.android.TypeManager.Activate ("Esri.ArcGISRuntime.UI.Controls.GeoView, Esri.ArcGISRuntime", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2, p3 });
	}


	public void onMeasure (int p0, int p1)
	{
		n_onMeasure (p0, p1);
	}

	private native void n_onMeasure (int p0, int p1);


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);


	public void addView (android.view.View p0)
	{
		n_addView (p0);
	}

	private native void n_addView (android.view.View p0);


	public void addView (android.view.View p0, int p1)
	{
		n_addView (p0, p1);
	}

	private native void n_addView (android.view.View p0, int p1);


	public void addView (android.view.View p0, int p1, android.view.ViewGroup.LayoutParams p2)
	{
		n_addView (p0, p1, p2);
	}

	private native void n_addView (android.view.View p0, int p1, android.view.ViewGroup.LayoutParams p2);


	public void addView (android.view.View p0, int p1, int p2)
	{
		n_addView (p0, p1, p2);
	}

	private native void n_addView (android.view.View p0, int p1, int p2);


	public void addView (android.view.View p0, android.view.ViewGroup.LayoutParams p1)
	{
		n_addView (p0, p1);
	}

	private native void n_addView (android.view.View p0, android.view.ViewGroup.LayoutParams p1);


	public void removeAllViews ()
	{
		n_removeAllViews ();
	}

	private native void n_removeAllViews ();


	public void removeView (android.view.View p0)
	{
		n_removeView (p0);
	}

	private native void n_removeView (android.view.View p0);


	public void removeViewAt (int p0)
	{
		n_removeViewAt (p0);
	}

	private native void n_removeViewAt (int p0);


	public void removeViews (int p0, int p1)
	{
		n_removeViews (p0, p1);
	}

	private native void n_removeViews (int p0, int p1);


	public void removeAllViewsInLayout ()
	{
		n_removeAllViewsInLayout ();
	}

	private native void n_removeAllViewsInLayout ();


	public void removeViewInLayout (android.view.View p0)
	{
		n_removeViewInLayout (p0);
	}

	private native void n_removeViewInLayout (android.view.View p0);


	public void removeViewsInLayout (int p0, int p1)
	{
		n_removeViewsInLayout (p0, p1);
	}

	private native void n_removeViewsInLayout (int p0, int p1);


	public void bringChildToFront (android.view.View p0)
	{
		n_bringChildToFront (p0);
	}

	private native void n_bringChildToFront (android.view.View p0);


	public android.view.View getChildAt (int p0)
	{
		return n_getChildAt (p0);
	}

	private native android.view.View n_getChildAt (int p0);


	public int indexOfChild (android.view.View p0)
	{
		return n_indexOfChild (p0);
	}

	private native int n_indexOfChild (android.view.View p0);


	public void addChildrenForAccessibility (java.util.ArrayList p0)
	{
		n_addChildrenForAccessibility (p0);
	}

	private native void n_addChildrenForAccessibility (java.util.ArrayList p0);


	public void requestChildFocus (android.view.View p0, android.view.View p1)
	{
		n_requestChildFocus (p0, p1);
	}

	private native void n_requestChildFocus (android.view.View p0, android.view.View p1);


	public boolean requestChildRectangleOnScreen (android.view.View p0, android.graphics.Rect p1, boolean p2)
	{
		return n_requestChildRectangleOnScreen (p0, p1, p2);
	}

	private native boolean n_requestChildRectangleOnScreen (android.view.View p0, android.graphics.Rect p1, boolean p2);


	public void clearChildFocus (android.view.View p0)
	{
		n_clearChildFocus (p0);
	}

	private native void n_clearChildFocus (android.view.View p0);


	public void clearDisappearingChildren ()
	{
		n_clearDisappearingChildren ();
	}

	private native void n_clearDisappearingChildren ();


	public boolean addStatesFromChildren ()
	{
		return n_addStatesFromChildren ();
	}

	private native boolean n_addStatesFromChildren ();


	public void childDrawableStateChanged (android.view.View p0)
	{
		n_childDrawableStateChanged (p0);
	}

	private native void n_childDrawableStateChanged (android.view.View p0);


	public boolean getChildVisibleRect (android.view.View p0, android.graphics.Rect p1, android.graphics.Point p2)
	{
		return n_getChildVisibleRect (p0, p1, p2);
	}

	private native boolean n_getChildVisibleRect (android.view.View p0, android.graphics.Rect p1, android.graphics.Point p2);


	public void setAddStatesFromChildren (boolean p0)
	{
		n_setAddStatesFromChildren (p0);
	}

	private native void n_setAddStatesFromChildren (boolean p0);


	public void setClipChildren (boolean p0)
	{
		n_setClipChildren (p0);
	}

	private native void n_setClipChildren (boolean p0);


	public boolean shouldDelayChildPressedState ()
	{
		return n_shouldDelayChildPressedState ();
	}

	private native boolean n_shouldDelayChildPressedState ();


	public boolean showContextMenuForChild (android.view.View p0)
	{
		return n_showContextMenuForChild (p0);
	}

	private native boolean n_showContextMenuForChild (android.view.View p0);


	public android.view.ActionMode startActionModeForChild (android.view.View p0, android.view.ActionMode.Callback p1)
	{
		return n_startActionModeForChild (p0, p1);
	}

	private native android.view.ActionMode n_startActionModeForChild (android.view.View p0, android.view.ActionMode.Callback p1);


	public void focusableViewAvailable (android.view.View p0)
	{
		n_focusableViewAvailable (p0);
	}

	private native void n_focusableViewAvailable (android.view.View p0);


	public void requestTransparentRegion (android.view.View p0)
	{
		n_requestTransparentRegion (p0);
	}

	private native void n_requestTransparentRegion (android.view.View p0);


	public boolean requestSendAccessibilityEvent (android.view.View p0, android.view.accessibility.AccessibilityEvent p1)
	{
		return n_requestSendAccessibilityEvent (p0, p1);
	}

	private native boolean n_requestSendAccessibilityEvent (android.view.View p0, android.view.accessibility.AccessibilityEvent p1);


	public void recomputeViewAttributes (android.view.View p0)
	{
		n_recomputeViewAttributes (p0);
	}

	private native void n_recomputeViewAttributes (android.view.View p0);


	public boolean onRequestSendAccessibilityEvent (android.view.View p0, android.view.accessibility.AccessibilityEvent p1)
	{
		return n_onRequestSendAccessibilityEvent (p0, p1);
	}

	private native boolean n_onRequestSendAccessibilityEvent (android.view.View p0, android.view.accessibility.AccessibilityEvent p1);


	public void endViewTransition (android.view.View p0)
	{
		n_endViewTransition (p0);
	}

	private native void n_endViewTransition (android.view.View p0);


	public android.view.View focusSearch (android.view.View p0, int p1)
	{
		return n_focusSearch (p0, p1);
	}

	private native android.view.View n_focusSearch (android.view.View p0, int p1);


	public android.view.ViewParent invalidateChildInParent (int[] p0, android.graphics.Rect p1)
	{
		return n_invalidateChildInParent (p0, p1);
	}

	private native android.view.ViewParent n_invalidateChildInParent (int[] p0, android.graphics.Rect p1);


	public void startViewTransition (android.view.View p0)
	{
		n_startViewTransition (p0);
	}

	private native void n_startViewTransition (android.view.View p0);


	public void updateViewLayout (android.view.View p0, android.view.ViewGroup.LayoutParams p1)
	{
		n_updateViewLayout (p0, p1);
	}

	private native void n_updateViewLayout (android.view.View p0, android.view.ViewGroup.LayoutParams p1);


	public int getChildCount ()
	{
		return n_getChildCount ();
	}

	private native int n_getChildCount ();


	public android.view.View getFocusedChild ()
	{
		return n_getFocusedChild ();
	}

	private native android.view.View n_getFocusedChild ();

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
