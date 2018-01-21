using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace Bitirme_Projesi 
{
    [Activity(Label = "Özbey Hastanesi", MainLauncher = true, Icon = "@drawable/icon", ScreenOrientation = Android.Content.PM.ScreenOrientation.Portrait)]
    public class randevu_al : Activity 
    {
        public string secilmis_doktor, secilmis_bolum;
        DateTime secilmistarih;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.randevu_al);

            try {
                Button button = FindViewById<Button>(Resource.Id.rndval);
                WebReference.TestService bolumlistesi = new WebReference.TestService();
                string bolumu = bolumlistesi.BolumListesi();
                Spinner bolum_liste = FindViewById<Spinner>(Resource.Id.bolumliste);
                string[] bolum = bolumu.Split(',');
                var bolumAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, bolum);
                bolumAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                bolum_liste.Adapter = bolumAdapter;
                bolum_liste.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(spinner_ItemSelected);

                CalendarView tarihcalview = FindViewById<CalendarView>(Resource.Id.randevutarih);
                tarihcalview.DateChange += tarihcalviewCalendarOnDateChange;

                //saatlerin tanımlanması
                TextView bir = FindViewById<TextView>(Resource.Id.bir);
                TextView iki = FindViewById<TextView>(Resource.Id.iki);
                TextView uc = FindViewById<TextView>(Resource.Id.uc);
                TextView dort = FindViewById<TextView>(Resource.Id.dort);
                TextView bes = FindViewById<TextView>(Resource.Id.bes);
                TextView alti = FindViewById<TextView>(Resource.Id.alti);
                TextView yedi = FindViewById<TextView>(Resource.Id.yedi);
                TextView sekiz = FindViewById<TextView>(Resource.Id.sekiz);
                TextView dokuz = FindViewById<TextView>(Resource.Id.dokuz);
                TextView on = FindViewById<TextView>(Resource.Id.on);
                TextView onbir = FindViewById<TextView>(Resource.Id.onbir);
                TextView oniki = FindViewById<TextView>(Resource.Id.oniki);
                TextView onuc = FindViewById<TextView>(Resource.Id.onuc);
                TextView ondort = FindViewById<TextView>(Resource.Id.ondort);
                TextView onbes = FindViewById<TextView>(Resource.Id.onbes);
                TextView onalti = FindViewById<TextView>(Resource.Id.onalti);
                TextView onyedi = FindViewById<TextView>(Resource.Id.onyedi);
                TextView onsekiz = FindViewById<TextView>(Resource.Id.onsekiz);
                TextView ondokuz = FindViewById<TextView>(Resource.Id.ondokuz);
                TextView yirmi = FindViewById<TextView>(Resource.Id.yirmi);
                TextView yirmiiki = FindViewById<TextView>(Resource.Id.yirmiiki);
                TextView yirmiuc = FindViewById<TextView>(Resource.Id.yirmiuc);
                TextView yirmidort = FindViewById<TextView>(Resource.Id.yirmidort);
                TextView saat = FindViewById<TextView>(Resource.Id.saat);

                //seçilen saatlerin ayrı bir textview'e aktarılması
                bir.Click += delegate { saat.Text = string.Format("08:00"); };
                iki.Click += delegate { saat.Text = string.Format("08:20"); };
                uc.Click += delegate { saat.Text = string.Format("08:40"); };
                dort.Click += delegate { saat.Text = string.Format("09:00"); };
                bes.Click += delegate { saat.Text = string.Format("09:40"); };
                alti.Click += delegate { saat.Text = string.Format("10:00"); };
                yedi.Click += delegate { saat.Text = string.Format("10:20"); };
                sekiz.Click += delegate { saat.Text = string.Format("10:40"); };
                dokuz.Click += delegate { saat.Text = string.Format("11:00"); };
                on.Click += delegate { saat.Text = string.Format("11:20"); };
                onbir.Click += delegate { saat.Text = string.Format("11:40"); };
                oniki.Click += delegate { saat.Text = string.Format("13:00"); };
                onuc.Click += delegate { saat.Text = string.Format("13:20"); };
                ondort.Click += delegate { saat.Text = string.Format("13:40"); };
                onbes.Click += delegate { saat.Text = string.Format("14:00"); };
                onalti.Click += delegate { saat.Text = string.Format("14:20"); };
                onyedi.Click += delegate { saat.Text = string.Format("14:40"); };
                onsekiz.Click += delegate { saat.Text = string.Format("15:00"); };
                ondokuz.Click += delegate { saat.Text = string.Format("15:20"); };
                yirmi.Click += delegate { saat.Text = string.Format("15:40"); };
                yirmiiki.Click += delegate { saat.Text = string.Format("16:00"); };
                yirmiuc.Click += delegate { saat.Text = string.Format("16:20"); };
                yirmidort.Click += delegate { saat.Text = string.Format("16:40"); };

                Button randevual = FindViewById<Button>(Resource.Id.rndval);
                randevual.Click += RandevuAl2;
            }catch{
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }

        private void tarihcalviewCalendarOnDateChange(object sender, CalendarView.DateChangeEventArgs args)
        {
            secilmistarih = new DateTime(args.Year, args.Month+1, args.DayOfMonth);
        }

        void RandevuAl2(object sender, EventArgs e)
        {
            try {
                EditText tc = FindViewById<EditText>(Resource.Id.tc);
                EditText adsoyad = FindViewById<EditText>(Resource.Id.adsoyad);
                EditText sikayet = FindViewById<EditText>(Resource.Id.sikayet);
                string sec_tarih = secilmistarih.Date.ToShortDateString();
                TextView randevusaat = FindViewById<TextView>(Resource.Id.saat);
                   if (tc.Text.Length == 11)
                {
                    WebReference.TestService randevuekle = new WebReference.TestService();
                    string sonuc = randevuekle.RandevuAl(tc.Text, adsoyad.Text, secilmis_bolum.ToString(), secilmis_doktor.ToString(), sec_tarih.ToString(), randevusaat.Text, sikayet.Text);
                   if (sonuc != "")
                    {
                        Toast.MakeText(this, "Randevunuzu aldınız. Referans Kodunuz:" + sonuc, ToastLength.Long).Show();
                    }
                    else {
                        Toast.MakeText(this, "Lütfen bütün boş alanları doldurunuz.", ToastLength.Long).Show();
                    }
                }       
                else
                {
                    Toast.MakeText(this, "Lütfen 11 haneli Tc kimlik numaranızı eksiksiz doldurunuz.", ToastLength.Long).Show();
                }
            }catch(Exception ex){
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
           }
        }
        private void spinner_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            try {
                Spinner spinnersender = (Spinner)sender;
                Spinner bolum_liste = FindViewById<Spinner>(Resource.Id.bolumliste);
                WebReference.TestService doktorlistesi = new WebReference.TestService();
                secilmis_bolum = spinnersender.GetItemAtPosition(e.Position).ToString();
                string d_adi = doktorlistesi.DoktorListesi(spinnersender.GetItemAtPosition(e.Position).ToString());
                Spinner spinner = FindViewById<Spinner>(Resource.Id.doktorliste);
                string[] doktor = d_adi.Split(',');
                var doktorAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleSpinnerItem, doktor);
                doktorAdapter.SetDropDownViewResource(Android.Resource.Layout.SimpleSpinnerDropDownItem);
                spinner.Adapter = doktorAdapter;
                spinner.ItemSelected += new EventHandler<AdapterView.ItemSelectedEventArgs>(doktor_ItemSelected);
            }catch{
                Toast.MakeText(this, "Bir hata meydana geldi.", ToastLength.Long).Show();
            }
        }

        private void doktor_ItemSelected(object sender, AdapterView.ItemSelectedEventArgs e)
        {
            Spinner spinnersender = (Spinner)sender;
            secilmis_doktor = spinnersender.GetItemAtPosition(e.Position).ToString();            
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