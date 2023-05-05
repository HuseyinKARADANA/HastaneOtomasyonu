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
    public partial class FrmSekreterDetay : Form
    {
        public FrmSekreterDetay()
        {
            InitializeComponent();
        }
        public String TCno;
        
        sqlbaglantisi bgl=new sqlbaglantisi();

        private void FrmSekreterDetay_Load(object sender, EventArgs e)
        {
            lblTC.Text = TCno;
            
            //Ad Soyad Yazdırma
            SqlCommand komut= new SqlCommand("SELECT SekreterAdSoyad FROM Tbl_Sekreter WHERE SekreterTC=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", TCno);
            SqlDataReader dr1= komut.ExecuteReader();
            while (dr1.Read())
            {
                lblAdSoyad.Text = dr1[0].ToString();

            }
            bgl.baglanti().Close();

            //branşları DataGride  aktarma
            DataTable dt= new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            //Doktorları DataGride Aktarma
            DataTable dt2= new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("SELECT (DoktorAd+' '+DoktorSoyad) as Doktorlar,DoktorBrans FROM Tbl_Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;

            // branşı combobox a aktarma
            SqlCommand komut2 = new SqlCommand("SELECT BransAd FROM Tbl_Branslar",bgl.baglanti());
            SqlDataReader dr2=komut2.ExecuteReader();
            while (dr2.Read())
            {
                cbBrans.Items.Add(dr2[0].ToString());
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komutKaydet = new SqlCommand("Insert Into Tbl_Randevular (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) VALUES (@p1,@p2,@p3,@p4)", bgl.baglanti());
            komutKaydet.Parameters.AddWithValue("@p1",mskTarih.Text);
            komutKaydet.Parameters.AddWithValue("@p2",mskSaat.Text);
            komutKaydet.Parameters.AddWithValue("@p3",cbBrans.Text);
            komutKaydet.Parameters.AddWithValue("@p4",cbDoktor.Text);
            komutKaydet.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu.");

        }

        private void cbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            cbDoktor.Items.Clear();
            SqlCommand komut = new SqlCommand("SELECT DoktorAd,DoktorSoyad FROM Tbl_Doktorlar WHERE DoktorBrans=@p1", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", cbBrans.Text);
            SqlDataReader dr= komut.ExecuteReader();
            while(dr.Read())
            {
                cbDoktor.Items.Add(dr[0]+" " + dr[1]);
            }
            bgl.baglanti().Close();
        }

        private void btnDuyuruOlustur_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("INSERT INTO Tbl_Duyurular (Duyuru) VALUES (@p1)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", rchDuyuru.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Duyuru Oluşturuldu");
        }

        private void btnDoktorPanel_Click(object sender, EventArgs e)
        {
            FrmDoktorPaneli frm=new FrmDoktorPaneli();
            frm.Show();
        }

        private void btnBransPanel_Click(object sender, EventArgs e)
        {
            FrmBrans frm=new FrmBrans();
            frm.Show();
        }

        private void btnRandevuPanel_Click(object sender, EventArgs e)
        {
            FrmRandevuListesi frm=new FrmRandevuListesi();
            frm.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FrmDuyurular frm=new FrmDuyurular();
            frm.Show();
        }
    }
}
