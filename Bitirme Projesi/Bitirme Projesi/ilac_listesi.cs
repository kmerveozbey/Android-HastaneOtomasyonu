using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace Bitirme_Projesi
{
    [Activity(Label = "Ýlaç Listesi", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class ilac_listesi : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ilac_listesi);

            try {
                string tckimlikno = Intent.GetStringExtra("tckimlikno");
                TextView ilac1 = FindViewById<TextView>(Resource.Id.tc);
                ilac1.Text = tckimlikno;

                WebReference.TestService test1client = new WebReference.TestService();
                string sonuc = test1client.HastaBilgileri(Convert.ToInt32(ilac1.Text), true);
                TextView liste = FindViewById<TextView>(Resource.Id.listele);
                liste.Text = sonuc;
            }catch{
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }

        public override bool OnPrepareOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu, menu);
            return base.OnPrepareOptionsMenu(menu);
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                case Resource.Id.randevu_al:
                    StartActivity(typeof(randevu_al));
                    return true;
                case Resource.Id.doktor_girisi:
                    StartActivity(typeof(Doktor_Girisi));
                    return true;
                case Resource.Id.randevu_iptal:
                    StartActivity(typeof(randevu_sil));
                    return true;
                case Resource.Id.eeczane:
                    StartActivity(typeof(eeczane));
                    return true;
            }
            return base.OnOptionsItemSelected(item);
        }
    }
}