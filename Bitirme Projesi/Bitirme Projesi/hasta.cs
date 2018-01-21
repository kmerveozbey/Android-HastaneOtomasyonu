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
    [Activity(Label = "hasta")]
    public class hasta : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.hasta);
            try {
                string h_ad = Intent.GetStringExtra("hastaadi");
                string d_ad = Intent.GetStringExtra("doktoradi");
                TextView liste1 = FindViewById<TextView>(Resource.Id.hasta);
                TextView h_adi = FindViewById<TextView>(Resource.Id.h_adi);
                liste1.Text = h_ad;
                h_adi.Text = h_ad;

                WebReference.TestService hasta_tc = new WebReference.TestService();
                string hastatc = hasta_tc.HastaTc(liste1.Text);
                TextView tc = FindViewById<TextView>(Resource.Id.tc);
                tc.Text = hastatc;

                TextView doktoradi = FindViewById<TextView>(Resource.Id.d_adi);
                doktoradi.Text = d_ad;

                WebReference.TestService bolumadi = new WebReference.TestService();
                string bolum = bolumadi.BolumYazdirma(doktoradi.Text);
                TextView bolumu = FindViewById<TextView>(Resource.Id.bolum);
                bolumu.Text = bolum;

                WebReference.TestService sikayet_y = new WebReference.TestService();
                string sikayet = sikayet_y.SikayetYazdirma(h_adi.Text, doktoradi.Text);
                TextView skyt = FindViewById<TextView>(Resource.Id.sikayet);
                skyt.Text = sikayet;

                Button ekle = FindViewById<Button>(Resource.Id.kayit);
                ekle.Click += HastaKayit2;
            }catch {
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }

        void HastaKayit2(object sender, EventArgs e)
        {
            try {
                TextView tc = FindViewById<TextView>(Resource.Id.tc);
                TextView adsoyad = FindViewById<TextView>(Resource.Id.h_adi);
                TextView bolum = FindViewById<TextView>(Resource.Id.bolum);
                TextView doktor = FindViewById<TextView>(Resource.Id.d_adi);
                EditText sikayet = FindViewById<EditText>(Resource.Id.sikayet);
                EditText tani = FindViewById<EditText>(Resource.Id.tani);
                EditText ilac = FindViewById<EditText>(Resource.Id.ilac);
                WebReference.TestService ilackaydi = new WebReference.TestService();
                string sonuc = ilackaydi.IlacKaydi(Convert.ToInt32(tc.Text), true, adsoyad.Text, doktor.Text, bolum.Text, sikayet.Text, tani.Text, ilac.Text);
                if (sonuc!="")
                {
                    Toast.MakeText(this, "Kaydedildi. Hastanýn Referans Kodu:" + sonuc, ToastLength.Long).Show();
                }
                else {
                    Toast.MakeText(this, "Lütfen bütün boþ alanlarý doldurunuz.", ToastLength.Long).Show();
                }
            }catch{
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }
   }
}