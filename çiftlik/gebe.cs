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
    public partial class gebe : Form
    {
        public gebe()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = ciftlik.accdb");
        public void getir()
        {
            DataTable tablo = new DataTable();
            baglan.Open();
            OleDbDataAdapter al = new OleDbDataAdapter("SELECT KUPE_NO,AD FROM besi", baglan);
            al.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglan.Close();

        }
        public void getir2()
        {
            DataTable tablo = new DataTable();
            baglan.Open();
            OleDbDataAdapter al = new OleDbDataAdapter("SELECT KUPE_NO,AD,TOHUM_TARIHI,T_ADET,T_TURU,GEBELIK_K,TAHMINI_T FROM gebe", baglan);
            al.Fill(tablo);
            dataGridView2.DataSource = tablo;
            baglan.Close();
        }
        bool değişken;
        
        void varmı()
        {
            baglan.Open();
            OleDbCommand sec = new OleDbCommand("SELECT * FROM gebe WHERE KUPE_NO=@P1",baglan);
            sec.Parameters.AddWithValue("@p1",textBox1.Text);
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
                    OleDbCommand ekle = new OleDbCommand("INSERT into gebe(KUPE_NO,AD,TOHUM_TARIHI,T_ADET,T_TURU,GEBELIK_K,TAHMINI_T) VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + maskedTextBox1.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + textBox6.Text + "')", baglan);
                    baglan.Open();
                    ekle.ExecuteNonQuery();
                    baglan.Close();
                    getir();
                    getir2();
                    MessageBox.Show("kayıt eklendi");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                }
                else
                    MessageBox.Show("bu kayıt zaten var.");

            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
                baglan.Close();
            }
        }

        private void gebe_Load(object sender, EventArgs e)
        {
            getir();
            getir2();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int getir = dataGridView1.SelectedCells[0].RowIndex;
            textBox1.Text = dataGridView1.Rows[getir].Cells[0].Value.ToString();
            textBox2.Text = dataGridView1.Rows[getir].Cells[1].Value.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("silmek istediğinize emin misiniz.", "dikkat tüm verileriniz silinebilir.", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE * FROM gebe WHERE KUPE_NO='" + textBox7.Text + "'", baglan);
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
                OleDbCommand güncelle = new OleDbCommand("UPDATE gebe set KUPE_NO='" + textBox1.Text + "',AD='" + textBox2.Text + "',TOHUM_TARIHI='"+maskedTextBox1.Text+"',T_ADET='"+textBox3.Text+"',T_TURU='"+textBox4.Text+"',GEBELIK_K='"+textBox5.Text+"',TAHMINI_T='"+textBox6.Text+"' WHERE KUPE_NO='"+textBox7.Text+"'", baglan);
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
                OleDbCommand güncelle2 = new OleDbCommand("UPDATE besi set KUPE_NO='"+textBox1.Text+"',AD='" + textBox2.Text + "' WHERE KUPE_NO='" + textBox7.Text + "'",baglan);
                baglan.Open();
                güncelle2.ExecuteNonQuery();
                baglan.Close();
                getir();
                getir2();

                MessageBox.Show("güncelleme işlemi yapıldı");

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

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataTable getir = new DataTable();
                baglan.Open();
                OleDbDataAdapter g = new OleDbDataAdapter("SELECT * FROM gebe WHERE KUPE_NO LIKE '" + textBox7.Text + "%'", baglan);
                g.Fill(getir);
                dataGridView2.DataSource = getir;
                baglan.Close();

                DataTable getir2 = new DataTable();
                baglan.Open();
                OleDbDataAdapter besi = new OleDbDataAdapter("SELECT KUPE_NO,AD FROM  besi WHERE KUPE_NO LIKE '" + textBox7.Text + "%'", baglan);
                besi.Fill(getir2);
                dataGridView1.DataSource = getir2;
                baglan.Close();
            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
            }
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int kayitsayisi;
            kayitsayisi = dataGridView2.RowCount;
            MessageBox.Show(kayitsayisi.ToString());
        }

        private void button5_Click(object sender, EventArgs e)
        {
            besi besi = new besi();
            besi.Show();
            this.Hide();
        }
    }
}
