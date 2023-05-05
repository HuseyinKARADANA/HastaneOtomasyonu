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
    public partial class FrmDoktorBilgiDuzenle : Form
    {
        public FrmDoktorBilgiDuzenle()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        public String TCno;
        private void FrmDoktorBilgiDuzenle_Load(object sender, EventArgs e)
        {
            mskTC.Text = TCno;
            SqlCommand komut = new SqlCommand("SELECT * FROM Tbl_Doktorlar WHERE DoktorTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TCno);
            SqlDataReader dr= komut.ExecuteReader();
            while (dr.Read())
            {
                txtAd.Text = dr[1].ToString();
                txtSoyad.Text = dr[2].ToString();
                cbBrans.Text = dr[3].ToString();
                txtSifre.Text = dr[5].ToString();
            }
            bgl.baglanti().Close(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Doktorlar set DoktorAd=@p1,DoktorSoyad=@p2,DoktorBrans=@p3,DoktorSifre=@p4 WHERE DoktorTC=@p5", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtAd.Text);
            komut.Parameters.AddWithValue("@p2",txtSoyad.Text);
            komut.Parameters.AddWithValue("@p3",cbBrans.Text);
            komut.Parameters.AddWithValue("@p4",txtSifre.Text);
            komut.Parameters.AddWithValue("@p5",mskTC.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgiler Güncellendi");
            MessageBox.Show("Bilgiler Güncellendi");
            
        }
    }
}
