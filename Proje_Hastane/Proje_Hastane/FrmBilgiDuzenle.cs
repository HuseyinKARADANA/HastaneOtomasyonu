using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Proje_Hastane
{
    public partial class FrmBilgiDuzenle : Form
    {
        public FrmBilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TCno;
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void FrmBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            SqlCommand komut1 = new SqlCommand("SELECT * FROM Tbl_Hastalar WHERE HastaTC=@p1",bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", TCno);
            SqlDataReader dr=komut1.ExecuteReader();
            while(dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                mskTel.Text = dr[4].ToString();
                txtSifre.Text = dr[5].ToString();
                cbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut2 = new SqlCommand("UPDATE Tbl_Hastalar set HastaAd=@p1,HastaSoyad=@p2,HastaTelefon=@p3,HastaSifre=@p4,HastaCinsiyet=@p5 WHERE HastaTC=@p6", bgl.baglanti());
            komut2.Parameters.AddWithValue("@p1",txtAd.Text);
            komut2.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut2.Parameters.AddWithValue("@p3",mskTel.Text);
            komut2.Parameters.AddWithValue("@p4",txtSifre.Text);
            komut2.Parameters.AddWithValue("@p5",cbCinsiyet.Text);
            komut2.Parameters.AddWithValue("@p6",TCno);
            komut2.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Başarıyla Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
