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
    [Activity(Label = "Hasta Listesi", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class hasta_listesi : Activity
    {
        public TextView d_adi;
        public string p_doktoradi;
        public string[] hastalar;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hasta_listesi);
            try {
                string k_ad = Intent.GetStringExtra("hastalistesi");
                TextView liste1 = FindViewById<TextView>(Resource.Id.k_adi);
                liste1.Text = k_ad;

                string tarih = Intent.GetStringExtra("randevutarihi");
                TextView trh = FindViewById<TextView>(Resource.Id.tarih);
                trh.Text = tarih;

                WebReference.TestService doktor_adi = new WebReference.TestService();
                string doktoradi = doktor_adi.DoktorAdiniGetir(liste1.Text);
                TextView d_adi = FindViewById<TextView>(Resource.Id.d_adi);
                d_adi.Text = doktoradi;
                p_doktoradi = doktoradi;

                WebReference.TestService hastaisimleri = new WebReference.TestService();
                string tmp_doktorad = d_adi.Text;
                if (tmp_doktorad.IndexOf(":") > -1)
                {
                    int indSep = tmp_doktorad.IndexOf(':');
                    int lenDoktorAdi = tmp_doktorad.Length;
                    tmp_doktorad = tmp_doktorad.Substring(indSep + 2, lenDoktorAdi - (indSep + 2));
                }

                string hasta_adi = hastaisimleri.HastaIsim(tmp_doktorad, trh.Text);
                hastalar = hasta_adi.Split(',');
                ListView h_adi = FindViewById<ListView>(Resource.Id.h_adi);
                List<string> hasta = new List<string>();
                foreach (var hastaadi in hastalar)
                {
                    hasta.Add(hastaadi);
                }
                ArrayAdapter<String> adapter = new ArrayAdapter<String>(this, Android.Resource.Layout.SimpleSelectableListItem, hasta);
                h_adi.Adapter = adapter;
                h_adi.ItemClick += HastaAdiClick;
            }
            catch{
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }

        private void HastaAdiClick(object sender, AdapterView.ItemClickEventArgs e)
        {
            try {
                var hastaadi1 = new Intent(this, typeof(hasta));
                string hastaadi = hastalar[e.Position];
                hastaadi1.PutExtra("hastaadi", hastaadi.ToString());
                hastaadi1.PutExtra("doktoradi", p_doktoradi);
                StartActivity(hastaadi1);
            }catch{
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }
    }
}

