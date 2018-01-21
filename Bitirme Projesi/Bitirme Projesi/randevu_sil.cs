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
    [Activity(Label = "Randevu Ýptal", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class randevu_sil : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.randevu_sil);

            Button rsil = FindViewById<Button>(Resource.Id.riptal);
            rsil.Click += RandevuSil2;
        }

        void RandevuSil2(object sender, EventArgs e)
        {
            try {
                EditText tc_kimlik = FindViewById<EditText>(Resource.Id.tc);
                TextView kod = FindViewById<TextView>(Resource.Id.kod);
                WebReference.TestService test1client = new WebReference.TestService();
                string sonuc = "";
                sonuc = test1client.RandevuSilme(Convert.ToInt32(tc_kimlik.Text), true, Convert.ToInt32(kod.Text), true);
                if (sonuc !="")
                {
                    Toast.MakeText(this, "Randevunuz Silinmiþtir", ToastLength.Long).Show();
                }
                else {
                    Toast.MakeText(this, "Lütfen bütün boþ alanlarý doldurunuz.", ToastLength.Long).Show();
                }
            }
            catch {
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