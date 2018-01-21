using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Channels;
using System.ServiceModel.Web;
using System.ServiceModel.Description;
using System.Configuration;

namespace Hastane_WebServis
{
    class Program
    {
        static void Main(string[] args)
        {

            Uri uri = new Uri("http" + "://" + "192.168.1.16" + ":" + "80" + "/service.svc");
            using (var serviceHost = new ServiceHost(typeof(TestService), uri))
            {

                ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
                smb.HttpGetEnabled = true;

                smb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                serviceHost.Description.Behaviors.Add(smb);

                serviceHost.Open();
                Console.WriteLine("kapatmak için enter");
                Console.ReadLine();
                serviceHost.Close();
            }
        }

        [ServiceContract]
        public interface ITestService
        {
            [OperationContract]
            string DoktorGirisi(string k_adi, string sifre);
            [OperationContract]
            String DoktorAdiniGetir(string k_adi);
            [OperationContract]
            string HastaIsim(string doktor, string randevu_tarihi);
            [OperationContract]
            string EeczaneGirisi(int tc_kimlik, int kod);
            [OperationContract]
            string RandevuSilme(int tc_k, int kod_k);
            [OperationContract]
            string HastaBilgileri(int tc_k);
            [OperationContract]
            string BolumListesi();
            [OperationContract]
            string DoktorListesi(string bolumu);
            [OperationContract]
            string BolumYazdirma(string adisoyadi);
            [OperationContract]
            string SikayetYazdirma(string adisoyadi,string doktor);
            [OperationContract]
            string HastaTc(string adisoyadi);
            [OperationContract]
            string RandevuAl(string tc_kimlik, string adsoyad, string bolum, string doktor, string randevu_tarihi, string randevu_saati, string sikayet);
            [OperationContract]
            string IlacKaydi(int tc_kimlik, string adisoyadi, string doktoradi, string bolum, string sikayet, string tani, string k_ilaclar);
        }

        public class TestService : ITestService
        {
            public string DoktorGirisi(string k_adi, string sifre)
            {
                string doktoradi;
                Data.Connection.Open();
                doktoradi = Data.DoktorGirisi(k_adi, sifre, Data.Connection);
                Data.Connection.Close();
                return doktoradi;
            }

            public String DoktorAdiniGetir(string k_adi)
            {
                string doktoradi = "";
                Data.Connection.Open();
                doktoradi = Data.DoktorAdiniGetir(k_adi, Data.Connection);
                Data.Connection.Close();
                return doktoradi;
            }

            public String HastaIsim(string doktor, string randevu_tarihi)
            {
                string hastaadi = "";
                Data.Connection.Open();
                hastaadi = Data.HastaIsim(doktor, randevu_tarihi, Data.Connection);
                Data.Connection.Close();
                return hastaadi;
            }
            
            public String EeczaneGirisi(int tc_kimlik, int kod)
            {
                string ilacadi = "";
                Data.Connection.Open();
                ilacadi = Data.EeczaneGirisi(tc_kimlik, kod, Data.Connection);
                Data.Connection.Close();
                return ilacadi;
            }

            public String RandevuSilme(int tc_k, int kod_k)
            {
                string doktoradi = "";
                Data.Connection.Open();
                doktoradi = Data.RandevuSilme(tc_k, kod_k, Data.Connection);
                Data.Connection.Close();
                return "Sonuç : " + doktoradi;
            }

            public String HastaBilgileri(int tc_kimlik)
            {
                string hastabilgileri = "";
                Data.Connection.Open();
                hastabilgileri = Data.HastaBilgileri(tc_kimlik, Data.Connection);
                Data.Connection.Close();
                return hastabilgileri;
            }

            public String BolumListesi()
            {
                string bolum = "";
                Data.Connection.Open();
                bolum = Data.BolumListesi(Data.Connection);
                Data.Connection.Close();
                return bolum;
            }

            public String DoktorListesi(string bolumu)
            {
                string doktor = "";
                Data.Connection.Open();
                doktor = Data.DoktorListesi(bolumu, Data.Connection);
                Data.Connection.Close();
                return doktor;
            }

            public String BolumYazdirma(string adisoyadi)
            {
                string doktor = "";
                Data.Connection.Open();
                doktor = Data.BolumYazdirma(adisoyadi, Data.Connection);
                Data.Connection.Close();
                return doktor;
            }

            public String SikayetYazdirma(string adisoyadi,string doktor)
            {
                string sikayet = "";
                Data.Connection.Open();
                sikayet = Data.SikayetYazdirma(adisoyadi,doktor, Data.Connection);
                Data.Connection.Close();
                return sikayet;
            }

            public String HastaTc(string adisoyadi)
            {
                string hastatc = "";
                Data.Connection.Open();
                hastatc = Data.HastaTc(adisoyadi, Data.Connection);
                Data.Connection.Close();
                return hastatc;
            }

            public String RandevuAl(string tc_kimlik, string adsoyad, string bolum, string doktor, string randevu_tarihi, string randevu_saati, string sikayet)
            {
                string randevu = "";
                Data.Connection.Open();
                randevu = Data.RandevuAl(tc_kimlik, adsoyad, bolum, doktor, randevu_tarihi, randevu_saati, sikayet, Data.Connection);
                Data.Connection.Close();
                return randevu;
            }

            public String IlacKaydi(int tc_kimlik, string adisoyadi, string doktoradi, string bolum, string sikayet, string tani, string k_ilaclar)
            {
                string kayit = "";
                Data.Connection.Open();
                kayit = Data.IlacKaydi(tc_kimlik, adisoyadi, doktoradi, bolum, sikayet, tani, k_ilaclar, Data.Connection);
                Data.Connection.Close();
                return kayit;
            }
        }
    }
}
