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
    public partial class FrmBrans : Form
    {
        public FrmBrans()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl=new sqlbaglantisi();
        //bu metot ile brans datagridview objesini metotla dolduruyoruz
        public void dataTableFill()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Tbl_Branslar", bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;
        }
        private void FrmBrans_Load(object sender, EventArgs e)
        {
            dataTableFill();
        }

        private void btnEkle_Click(object sender, EventArgs e)
        {
            SqlCommand komut= new SqlCommand("INSERT INTO Tbl_Branslar (BransAd) VALUES (@p1)",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtbrans.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            dataTableFill();
            MessageBox.Show("Branş Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtid.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtbrans.Text= dataGridView1.Rows[secilen].Cells[1].Value.ToString();
        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("DELETE FROM Tbl_Branslar WHERE Bransid=@p1",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", txtid.Text);
            komut.ExecuteNonQuery();
            bgl.baglanti().Close();
            dataTableFill();
            MessageBox.Show("Branş Silindi");

        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("UPDATE Tbl_Branslar SET BransAd=@p1 WHERE Bransid=@p2",bgl.baglanti());
            komut.Parameters.AddWithValue("@p1",txtbrans.Text);
            komut.Parameters.AddWithValue("@p2", txtid.Text);
            komut.ExecuteNonQuery();
            dataTableFill();
            MessageBox.Show("Branş Güncellendi");
        }
    }
}
