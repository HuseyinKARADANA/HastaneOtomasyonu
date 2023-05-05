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
    public partial class FrmSekreterGiris : Form
    {
        public FrmSekreterGiris()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        private void BnGirisYap_Click(object sender, EventArgs e)
        {
            SqlCommand komut1 = new SqlCommand("SELECT * FROM Tbl_Sekreter WHERE SekreterTC=@p1 and SekreterSifre=@p2", bgl.baglanti());
            komut1.Parameters.AddWithValue("@p1", mskTC.Text);
            komut1.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr=komut1.ExecuteReader();
            if(dr.Read())
            {
                FrmSekreterDetay frm=new FrmSekreterDetay();
                frm.TCno = mskTC.Text;
                frm.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı TC & Şifre");
            }
            bgl.baglanti().Close();

        }
    }
}
