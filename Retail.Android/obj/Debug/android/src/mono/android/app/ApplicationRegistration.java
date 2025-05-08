package mono.android.app;

public class ApplicationRegistration {

	public static void registerApplications ()
	{
				// Application and Instrumentation ACWs must be registered first.
		mono.android.Runtime.register ("Retail.Droid.MainApplication, Retail.Android, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", crc64ac5900f349db158f.MainApplication.class, crc64ac5900f349db158f.MainApplication.__md_methods);
		
	}
}
