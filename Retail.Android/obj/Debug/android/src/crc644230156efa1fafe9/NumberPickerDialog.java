package crc644230156efa1fafe9;


public class NumberPickerDialog
	extends androidx.appcompat.app.AppCompatDialogFragment
	implements
		mono.android.IGCUserPeer
{
/** @hide */
	public static final String __md_methods;
	static {
		__md_methods = 
			"";
		mono.android.Runtime.register ("Retail.Droid.Renderers.NumberPickerDialog, Retail.Android", NumberPickerDialog.class, __md_methods);
	}


	public NumberPickerDialog ()
	{
		super ();
		if (getClass () == NumberPickerDialog.class) {
			mono.android.TypeManager.Activate ("Retail.Droid.Renderers.NumberPickerDialog, Retail.Android", "", this, new java.lang.Object[] {  });
		}
	}


	public NumberPickerDialog (int p0)
	{
		super (p0);
		if (getClass () == NumberPickerDialog.class) {
			mono.android.TypeManager.Activate ("Retail.Droid.Renderers.NumberPickerDialog, Retail.Android", "System.Int32, mscorlib", this, new java.lang.Object[] { p0 });
		}
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
