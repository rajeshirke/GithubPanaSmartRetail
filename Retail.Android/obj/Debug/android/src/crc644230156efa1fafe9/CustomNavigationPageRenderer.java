package crc644230156efa1fafe9;


public class CustomNavigationPageRenderer
	extends crc64720bb2db43a66fe9.NavigationPageRenderer
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"n_onViewAdded:(Landroid/view/View;)V:GetOnViewAdded_Landroid_view_View_Handler\n" +
			"n_onLayout:(ZIIII)V:GetOnLayout_ZIIIIHandler\n" +
			"";
		mono.android.Runtime.register ("Retail.Droid.Renderers.CustomNavigationPageRenderer, Retail.Android", CustomNavigationPageRenderer.class, __md_methods);
	}


	public CustomNavigationPageRenderer (android.content.Context p0)
	{
		super (p0);
		if (getClass () == CustomNavigationPageRenderer.class) {
			mono.android.TypeManager.Activate ("Retail.Droid.Renderers.CustomNavigationPageRenderer, Retail.Android", "Android.Content.Context, Mono.Android", this, new java.lang.Object[] { p0 });
		}
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1)
	{
		super (p0, p1);
		if (getClass () == CustomNavigationPageRenderer.class) {
			mono.android.TypeManager.Activate ("Retail.Droid.Renderers.CustomNavigationPageRenderer, Retail.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android", this, new java.lang.Object[] { p0, p1 });
		}
	}


	public CustomNavigationPageRenderer (android.content.Context p0, android.util.AttributeSet p1, int p2)
	{
		super (p0, p1, p2);
		if (getClass () == CustomNavigationPageRenderer.class) {
			mono.android.TypeManager.Activate ("Retail.Droid.Renderers.CustomNavigationPageRenderer, Retail.Android", "Android.Content.Context, Mono.Android:Android.Util.IAttributeSet, Mono.Android:System.Int32, mscorlib", this, new java.lang.Object[] { p0, p1, p2 });
		}
	}


	public void onViewAdded (android.view.View p0)
	{
		n_onViewAdded (p0);
	}

	private native void n_onViewAdded (android.view.View p0);


	public void onLayout (boolean p0, int p1, int p2, int p3, int p4)
	{
		n_onLayout (p0, p1, p2, p3, p4);
	}

	private native void n_onLayout (boolean p0, int p1, int p2, int p3, int p4);

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
