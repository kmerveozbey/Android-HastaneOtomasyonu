using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Sql;
using System.Data.SqlClient;

namespace Hastane_WebServis
{
    class Data
    {
        public static SqlConnection Connection = new SqlConnection("Server=PC;Database=hastane;uid=sa;pwd=ab12AB34;");

        public static string DoktorGirisi(string k_adi,string sifre, SqlConnection Conn)
        {
            string doktoradi="";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from doktor where k_adi = @k_adi and sifre=@sifre", Conn);
            sqlcmd.Parameters.AddWithValue("@k_adi", k_adi);
            sqlcmd.Parameters.AddWithValue("@sifre", sifre);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                doktoradi=sqlrdr["adisoyadi"].ToString();
            }
            if (doktoradi != "")
            {
                return "BASARILI";
            }
            else
            {
                return "BASARISIZ";
            }
        }

        public static string DoktorAdiniGetir(string k_adi, SqlConnection Conn)
        {
            string doktoradi = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from doktor where k_adi = @k_adi", Conn);
            sqlcmd.Parameters.AddWithValue("@k_adi", k_adi);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                doktoradi = sqlrdr["adisoyadi"].ToString();
            }
            return doktoradi;
        }

        public static string HastaIsim(string doktor,string randevu_tarihi, SqlConnection Conn)
        {
            string hastaadi = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from randevu where doktor = @doktor and randevu_tarihi=@randevu_tarihi", Conn);
            sqlcmd.Parameters.AddWithValue("@doktor", doktor);
            sqlcmd.Parameters.AddWithValue("@randevu_tarihi", randevu_tarihi);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                if (hastaadi == "")
                {
                    hastaadi = sqlrdr["adisoyadi"].ToString();
                }
                else
                {
                    hastaadi += "," + sqlrdr["adisoyadi"].ToString();
                }
                
            }
            return hastaadi;
        }
        
        public static string EeczaneGirisi(int tc_kimlik, int kod, SqlConnection Conn)
        {
            string ilaclistesi = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from eeczane where tc_kimlik = @tc_kimlik and ID=@ID", Conn);
            sqlcmd.Parameters.AddWithValue("@tc_kimlik", tc_kimlik);
            sqlcmd.Parameters.AddWithValue("@ID", kod);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                ilaclistesi = sqlrdr["k_ilaclar"].ToString();
            }
            if (ilaclistesi != "")
            {
                return "BASARILI";
            }
            else
            {
                return "BASARISIZ";

            }
        }

        public static string RandevuSilme(int tc_k, int kod_k, SqlConnection Conn)
        {
            SqlCommand sqlcmd = new SqlCommand("Delete from randevu where tc_kimlik = @tc and ID=@id", Conn);
            sqlcmd.Parameters.AddWithValue("@tc", tc_k);
            sqlcmd.Parameters.AddWithValue("@id", kod_k);
            sqlcmd.ExecuteNonQuery();
            return "Basarili";
        }

        public static string HastaBilgileri(int tc_kimlik, SqlConnection Conn)
        {
            string hastabilgileri = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from eeczane where tc_kimlik = @tc_kimlik ", Conn);
            sqlcmd.Parameters.AddWithValue("@tc_kimlik", tc_kimlik);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                hastabilgileri += "\n" +"Tc Kimlik No: "+sqlrdr ["tc_kimlik"]+ "\n" + "Adı-Soyadı: "+sqlrdr ["adisoyadi"]+ "\n" +"Doktor Adı: "+ sqlrdr["doktor_adi"] + "\n" + "Bölüm: " + sqlrdr["bolumu"]+ "\n" + "Şikayetiniz: " + sqlrdr["sikayet"]+ "\n" + "Tanı: " + sqlrdr["tani"]+ "\n" + "Yazılan İlaçlar: " + sqlrdr["k_ilaclar"].ToString();
            }
            return hastabilgileri;
        }

        public static string BolumListesi(SqlConnection Conn)
        {
            string bolum = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from doktor", Conn);

            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                bolum += "," + sqlrdr["bolumu"].ToString();
            }
            return bolum;
        }

        public static string DoktorListesi(string bolumu, SqlConnection Conn)
        {
            string doktor = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from doktor where bolumu=@bolumu", Conn);
            sqlcmd.Parameters.AddWithValue("@bolumu", bolumu);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                doktor += "," + sqlrdr["adisoyadi"].ToString();
            }
            return doktor;
        }

        public static string BolumYazdirma(string adisoyadi, SqlConnection Conn)
        {
            string doktor = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from doktor where adisoyadi=@adisoyadi", Conn);
            sqlcmd.Parameters.AddWithValue("@adisoyadi", adisoyadi);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                doktor =sqlrdr["bolumu"].ToString();
            }
            return doktor;
        }

        public static string SikayetYazdirma(string adisoyadi,string doktor, SqlConnection Conn)
        {
            string sikayet = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from randevu where adisoyadi=@adisoyadi and doktor=@doktor", Conn);
            sqlcmd.Parameters.AddWithValue("@adisoyadi", adisoyadi);
            sqlcmd.Parameters.AddWithValue("@doktor", doktor);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                sikayet = sqlrdr["sikayet"].ToString();
            }
            return sikayet;
        }

        public static string HastaTc(string adisoyadi, SqlConnection Conn)
        {
            string hastatc = "";
            SqlCommand sqlcmd = new SqlCommand("SELECT * from randevu where adisoyadi = @adisoyadi", Conn);
            sqlcmd.Parameters.AddWithValue("@adisoyadi", adisoyadi);
            SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            while (sqlrdr.Read())
            {
                hastatc = sqlrdr["tc_kimlik"].ToString();
            }
            return hastatc;
        }

        public static string RandevuAl(string tc_kimlik,string adsoyad,string bolum, string doktor, string randevu_tarihi, string randevu_saati, string sikayet , SqlConnection Conn)
        {
            SqlCommand sqlcmd = new SqlCommand("insert  into  randevu(tc_kimlik,adisoyadi,bolum,doktor,randevu_tarihi,randevu_saati,sikayet) OUTPUT INSERTED.ID values(@tc_kimlik, @adisoyadi, @bolum, @doktor,@randevu_tarihi, @randevu_saati, @sikayet)", Conn);
            sqlcmd.Parameters.AddWithValue("@tc_kimlik", tc_kimlik);
            sqlcmd.Parameters.AddWithValue("@adisoyadi", adsoyad);
            sqlcmd.Parameters.AddWithValue("@bolum", bolum);
            sqlcmd.Parameters.AddWithValue("@doktor", doktor);
            sqlcmd.Parameters.AddWithValue("@randevu_tarihi", randevu_tarihi);
            sqlcmd.Parameters.AddWithValue("@randevu_saati", randevu_saati);
            sqlcmd.Parameters.AddWithValue("@sikayet", sikayet);
            string id = sqlcmd.ExecuteScalar().ToString();
            return id;
        }

        public static string IlacKaydi(int tc_kimlik, string adisoyadi, string doktoradi, string bolum, string sikayet,string tani,string k_ilaclar, SqlConnection Conn)
        {
            SqlCommand sqlcmd = new SqlCommand("insert into eeczane(tc_kimlik,adisoyadi,doktor_adi,bolumu,sikayet,tani,k_ilaclar) OUTPUT INSERTED.ID values(@tc_kimlik, @adisoyadi, @doktor_adi, @bolumu, @sikayet, @tani,@k_ilaclar)", Conn);
            sqlcmd.Parameters.AddWithValue("@tc_kimlik", tc_kimlik);
            sqlcmd.Parameters.AddWithValue("@adisoyadi", adisoyadi);
            sqlcmd.Parameters.AddWithValue("@doktor_adi", doktoradi);
            sqlcmd.Parameters.AddWithValue("@bolumu", bolum);
            sqlcmd.Parameters.AddWithValue("@sikayet", sikayet);
            sqlcmd.Parameters.AddWithValue("@tani", tani);
            sqlcmd.Parameters.AddWithValue("@k_ilaclar", k_ilaclar);
            string id = sqlcmd.ExecuteScalar().ToString();
            return id;
            //SqlDataReader sqlrdr = sqlcmd.ExecuteReader();
            //return sqlrdr["ID"].ToString(); 

        }
     }
}
