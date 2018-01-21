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
    [Activity(Label = "Tarih Seçimi")]
    public class ilkgiris : Activity
    {
        DateTime secilmistarih;
        TextView liste1;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.ilkgiris);
            try
            {
                string k_ad = Intent.GetStringExtra("hastalistesi");
                liste1 = FindViewById<TextView>(Resource.Id.k_adi);
                liste1.Text = k_ad;

                WebReference.TestService doktor_adi = new WebReference.TestService();
                string doktoradi = doktor_adi.DoktorAdiniGetir(liste1.Text);
                TextView d_adi = FindViewById<TextView>(Resource.Id.d_adi);
                d_adi.Text = doktoradi;

                CalendarView tarihcalview = FindViewById<CalendarView>(Resource.Id.listetarih);
                tarihcalview.DateChange += tarihcalviewCalendarOnDateChange;

                Button ileri = FindViewById<Button>(Resource.Id.ileri);
                ileri.Click += Ileri;
            }catch {
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }

        private void tarihcalviewCalendarOnDateChange(object sender, CalendarView.DateChangeEventArgs args)
        {
            secilmistarih = new DateTime(args.Year, args.Month+1, args.DayOfMonth);
        }

        void Ileri(object sender, EventArgs e)
        {
            try
            {
                string sec_tarih = secilmistarih.Date.ToShortDateString();
                var hastalistesi2 = new Intent(this, typeof(hasta_listesi));
                hastalistesi2.PutExtra("hastalistesi", liste1.Text);
                hastalistesi2.PutExtra("randevutarihi", sec_tarih.ToString());
                StartActivity(hastalistesi2);
            } catch {
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }
    }
}