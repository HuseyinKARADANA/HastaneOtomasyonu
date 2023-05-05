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
    public partial class FrmDoktorGiris : Form
    {
        public FrmDoktorGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void FrmDoktorGiris_Load(object sender, EventArgs e)
        {
            
        }

        private void BnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("Select * FROM Tbl_Doktorlar WHERE DoktorTC=@p1 and DoktorSifre=@p2", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", mskTC.Text);
            komut.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = komut.ExecuteReader();
            if (dr.Read())
            {
                FrmDoktorDetay frm = new FrmDoktorDetay();
                frm.TC = mskTC.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı Adı & Şifre");
            }
            bgl.baglanti().Close();
        }
    }
}
