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
    [Activity(Label = "Doktor Giriþi", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class Doktor_Girisi : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.doktor_girisi);

            Button giris = FindViewById<Button>(Resource.Id.dgiris);
            giris.Click += DoktorGirisi2;
        }

        void DoktorGirisi2(object sender, EventArgs e)
        {
            try {
                EditText k_adi = FindViewById<EditText>(Resource.Id.k_adi);
                TextView sifre = FindViewById<TextView>(Resource.Id.sifre);
                WebReference.TestService test1client = new WebReference.TestService();
                string sonuc = "";
                sonuc = test1client.DoktorGirisi(k_adi.Text, sifre.Text);
                if (sonuc == "BASARILI")
                {
                    var hastalistesi1 = new Intent(this, typeof(ilkgiris));
                    hastalistesi1.PutExtra("hastalistesi", k_adi.Text);
                    StartActivity(hastalistesi1);
                }
                else {
                    Toast.MakeText(this, "Kullanýcý adý veya þifreyi yanlýþ girdiniz.", ToastLength.Long).Show();
                }
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