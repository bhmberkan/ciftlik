using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;

namespace çiftlik
{
    public partial class satılık : Form
    {
        public satılık()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = ciftlik.accdb");
        public void getir()
        {
            DataTable tablo = new DataTable();
            baglan.Open();
            OleDbDataAdapter al = new OleDbDataAdapter("SELECT KUPE_NO,AD,DOGUM_T FROM besi", baglan);
            al.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglan.Close();
        }
        public void getir2()
        {
            DataTable tablo = new DataTable();
            baglan.Open();
            OleDbDataAdapter al = new OleDbDataAdapter("SELECT KUPE_NO,AD,DOGUM_T,SATIM_T,SATIM_NEDENI,KILOSU,SATILDI FROM satım", baglan);
            al.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int kayitsayisi;
            kayitsayisi = dataGridView2.RowCount;
            MessageBox.Show(kayitsayisi.ToString());
        }

        private void satılık_Load(object sender, EventArgs e)
        {
            getir();
            getir2();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
            maskedTextBox1.Text = dataGridView1.Rows[getir].Cells[2].Value.ToString();
        }

        bool değişken;

        void varmı()
        {
            baglan.Open();
            OleDbCommand sec = new OleDbCommand("SELECT * FROM satım WHERE KUPE_NO=@P1", baglan);
            sec.Parameters.AddWithValue("@p1", textBox1.Text);
            OleDbDataReader okut = sec.ExecuteReader();

            if (okut.Read())
            {
                değişken = false;
            }
            else
                değişken = true;
            baglan.Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                varmı();
                if (değişken == true)
                {


                    OleDbCommand ekle = new OleDbCommand("INSERT into satım(KUPE_NO,AD,DOGUM_T,SATIM_T,SATIM_NEDENI,KILOSU,SATILDI) " +
                        "VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox1.Text + "','" + maskedTextBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + maskedTextBox3.Text + "' )", baglan);
                    baglan.Open();
                    ekle.ExecuteNonQuery();
                    baglan.Close();
                    getir();
                    getir2();

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                }
                else
                    MessageBox.Show("bu kayıt zaten var!.");
            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
                baglan.Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("silmek istediğinize emin misiniz.", "dikkat tüm verileriniz silinebilir.", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE * FROM satım WHERE KUPE_NO='" + textBox5.Text + "'", baglan);
                    baglan.Open();
                    sil.ExecuteNonQuery();
                    baglan.Close();
                    getir();
                    getir2();

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                }
                catch (Exception sorun)
                {
                    MessageBox.Show(sorun.Message);
                }

            }
            else
                MessageBox.Show("işlemi iptal ettiniz.");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand güncelle = new OleDbCommand("UPDATE satım set KUPE_NO='" + textBox1.Text + "',AD='" + textBox2.Text + "',DOGUM_T='"+maskedTextBox1.Text+"',SATM_T='"+maskedTextBox2.Text+"',SATIM_NEDENI='"+textBox3.Text+"',KILOSU='"+textBox4.Text+"',SATILDI='"+maskedTextBox3.Text+ "' WHERE  KUPE_NO='" + textBox5.Text + "'", baglan);
                baglan.Open();
                güncelle.ExecuteNonQuery();
                baglan.Close();
                getir();
                getir2();



            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
                baglan.Close();
            }

            try
            {
                OleDbCommand güncelle2 = new OleDbCommand("UPDATE besi set KUPE_NO='" + textBox1.Text + "',AD='" + textBox2.Text + "',DOGUM_T='"+maskedTextBox1.Text+"' WHERE KUPE_NO='" + textBox5.Text + "'", baglan);
                baglan.Open();
                güncelle2.ExecuteNonQuery();
                baglan.Close();
                getir();
                getir2();

                MessageBox.Show("güncelleme işlemi başarılı");

                for (int i = 0; i < this.Controls.Count; i++)
                {
                    if (Controls[i] is TextBox) Controls[i].Text = "";
                }
            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
                baglan.Close();
            }

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable tablo = new DataTable();
                baglan.Open();
                OleDbDataAdapter al = new OleDbDataAdapter("SELECT KUPE_NO,AD,DOGUM_T,SATIM_T,SATIM_NEDENI,KILOSU,SATILDI FROM satım WHERE KUPE_NO LIKE '" + textBox5.Text + "%'", baglan);
                al.Fill(tablo);
                dataGridView2.DataSource = tablo;
                baglan.Close();
            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
            }

            try
            {
                DataTable tablo = new DataTable();
                baglan.Open();
                OleDbDataAdapter al = new OleDbDataAdapter("SELECT KUPE_NO,AD,DOGUM_T FROM besi WHERE KUPE_NO LIKE '" + textBox5.Text + "%'", baglan);
                al.Fill(tablo);
                dataGridView1.DataSource = tablo;
                baglan.Close();
            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            besi besi = new besi();
            besi.Show();
            this.Hide();
        }
    } 
}

