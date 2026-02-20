using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;


namespace Kitaplik_Projesi
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

       

        OleDbConnection baglanti = new OleDbConnection(  @"Provider=Microsoft.ACE.OLEDB.16.0;Data Source=C:\Users\DELL\Desktop\Kitaplık.accdb;Persist Security Info=True");

        void listele()
        {
            OleDbDataAdapter da = new OleDbDataAdapter(
       "SELECT * FROM Kitaplar", baglanti);

            DataTable dt = new DataTable();
            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        string durum = "";// ilk başlangıçta böyle

        private void Form1_Load(object sender, EventArgs e)
        {

            listele();
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand kaydet = new OleDbCommand("INSERT INTO Kitaplar (KitapAd, Yazar, Tur, Sayfa, Durum) values ( @p1, @p2, @p3, @p4, @p5)", baglanti);
            kaydet.Parameters.AddWithValue("@p1", txtKitapAd.Text);
            kaydet.Parameters.AddWithValue("@p2", txtYazar.Text);
            kaydet.Parameters.AddWithValue("@P3", cmbTur.Text);
            kaydet.Parameters.AddWithValue("@p4", txtSayfa.Text);
            kaydet.Parameters.AddWithValue("@p5", durum);
            kaydet.ExecuteNonQuery();
            baglanti.Close();
            listele();
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            //buton ismi= Temiz
            durum = "1";
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //buton ismi = İkinci El
            durum = "0";
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;
            txtKitapID.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            txtKitapAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txtYazar.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            cmbTur.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txtSayfa.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            //DURUM
            if (dataGridView1.Rows[secilen].Cells[5].Value.ToString() == "True")
            {
                radioButton2.Checked = true;
            }
            else
            {
                radioButton1.Checked = true;
            }

            

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Delete From Kitaplar where KitapID=@p1",baglanti );
            komut.Parameters.AddWithValue("@P1", txtKitapID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Silindi!", "BİLGİ ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("update Kitaplar set KitapAd=@p1, Yazar=@p2, Tur=@p3, Sayfa=@p4, Durum= @p5 where KitapID=@p6 ", baglanti);
            komut.Parameters.AddWithValue("@p1",txtKitapAd.Text);
            komut.Parameters.AddWithValue("@p2", txtYazar.Text);
            komut.Parameters.AddWithValue("@p3", cmbTur.Text);
            komut.Parameters.AddWithValue("@p4", txtSayfa.Text);
            if (radioButton1.Checked == true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            if (radioButton2.Checked == true)
            {
                komut.Parameters.AddWithValue("@p5", durum);
            }
            komut.Parameters.AddWithValue("@p6", txtKitapID.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Güncellendi!", "BİLGİ", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            listele();
        }

        private void btnBul_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * from Kitaplar where KitapAd=@p1  ", baglanti);
            komut.Parameters.AddWithValue("@p1", txtBul.Text);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void btnAra_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            OleDbCommand komut = new OleDbCommand("Select * From Kitaplar where KitapAd like '%"+txtBul.Text+"%'",baglanti);
            DataTable dt = new DataTable();
            OleDbDataAdapter da = new OleDbDataAdapter(komut);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            baglanti.Close();
        }

        private void btnListele_Click(object sender, EventArgs e)
        {
            listele();
        }
    }
}
