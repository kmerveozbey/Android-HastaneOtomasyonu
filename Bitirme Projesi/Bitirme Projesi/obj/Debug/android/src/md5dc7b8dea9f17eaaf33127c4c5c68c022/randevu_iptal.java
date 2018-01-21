package md5dc7b8dea9f17eaaf33127c4c5c68c022;


public class randevu_iptal
	extends android.app.Activity
	implements
		mono.android.IGCUserPeer
{
	static final String __md_methods;
	static {
		__md_methods = 
			"n_onCreate:(Landroid/os/Bundle;)V:GetOnCreate_Landroid_os_Bundle_Handler\n" +
			"";
		mono.android.Runtime.register ("Bitirme_Projesi.randevu_iptal, Bitirme Projesi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", randevu_iptal.class, __md_methods);
	}


	public randevu_iptal () throws java.lang.Throwable
	{
		super ();
		if (getClass () == randevu_iptal.class)
			mono.android.TypeManager.Activate ("Bitirme_Projesi.randevu_iptal, Bitirme Projesi, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", "", this, new java.lang.Object[] {  });
	}


	public void onCreate (android.os.Bundle p0)
	{
		n_onCreate (p0);
	}

	private native void n_onCreate (android.os.Bundle p0);

	java.util.ArrayList refList;
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
