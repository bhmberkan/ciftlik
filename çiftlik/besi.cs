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
    public partial class besi : Form
    {
        public besi()
        {
            InitializeComponent();
        }
        OleDbConnection baglan = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = ciftlik.accdb");
      
        public void getir()
        {
            DataTable tablo = new DataTable();
            baglan.Open();
            OleDbDataAdapter al = new OleDbDataAdapter("SELECT KUPE_NO,AD,RENK,ANA_K_NO,BABA_K_NO,DOGUM_T,IRKI,CINSIYETI,GRUBU,ANNE_RENGI,ACIKLAMA FROM besi", baglan);
            al.Fill(tablo);
            dataGridView1.DataSource = tablo;
            baglan.Close();
        }
        bool değişken;

        void varmı()
        {
            baglan.Open();
            OleDbCommand sec = new OleDbCommand("SELECT * FROM besi WHERE KUPE_NO=@P1", baglan);
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

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                varmı();
                if (değişken == true)
                {
                    OleDbCommand ekle = new OleDbCommand("INSERT INTO besi(KUPE_NO,AD,RENK,ANA_K_NO,BABA_K_NO,DOGUM_T,IRKI,CINSIYETI,GRUBU,ANNE_RENGI,ACIKLAMA)" +
                        "VALUES('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + maskedTextBox1.Text + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + comboBox3.Text + "','" + textBox7.Text + "','" + richTextBox1.Text + "')", baglan);
                    baglan.Open();
                    ekle.ExecuteNonQuery();
                    baglan.Close();
                    getir();
                    MessageBox.Show("bilgiler eklendi.");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                    yoket();
                }
                else
                    MessageBox.Show("bu kayıt zaten var!.");
            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
            }
        }
        public void yoket()
        {
            maskedTextBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            richTextBox1.Text = "";
        }
        private void besi_Load(object sender, EventArgs e)
        {
            
            getir();
            comboBox1.Items.Add("angus");
            comboBox1.Items.Add("boz");
            comboBox1.Items.Add("erangus");
            comboBox1.Items.Add("charolais");
            comboBox1.Items.Add("dak");
            comboBox1.Items.Add("gak");
            comboBox1.Items.Add("holsterin");
            comboBox1.Items.Add("jersey");
            comboBox1.Items.Add("kırım sığırı");
            comboBox1.Items.Add("maraş sığırı");
            comboBox1.Items.Add("nantafon");
            comboBox1.Items.Add("simental");
            comboBox1.Items.Add("şarole");
            comboBox1.Items.Add("zavot sığırı");
            comboBox1.Items.Add("yerli kara");

            comboBox2.Items.Add("ERKEK");
            comboBox2.Items.Add("DİŞİ");

            comboBox3.Items.Add("besi");
            comboBox3.Items.Add("satılık");
            comboBox3.Items.Add("sütten kesilecek");
            comboBox3.Items.Add("gebe");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DialogResult cevap;
            cevap = MessageBox.Show("silmek istediğinize emin misiniz.", "dikkat tüm verileriniz silinebilir.", MessageBoxButtons.YesNo);
            if (cevap == DialogResult.Yes)
            {
                try
                {
                    OleDbCommand sil = new OleDbCommand("DELETE * FROM besi WHERE KUPE_NO='" + textBox8.Text + "'", baglan);
                    baglan.Open();
                    sil.ExecuteNonQuery();
                    baglan.Close();
                    getir();

                    textBox8.Text = "";

                }
                catch (Exception sorun)
                {
                    MessageBox.Show(sorun.Message);
                    baglan.Close();
                }
            }
            else
                MessageBox.Show("işlemi iptal ettiniz.");
        }

        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                OleDbCommand güncelle = new OleDbCommand("UPDATE besi SET KUPE_NO='"+textBox1.Text+"',AD='"+textBox2.Text+"',RENK='"+textBox3.Text+ "',ANA_K_NO='"+textBox4.Text+ "',BABA_K_NO='"+textBox5.Text+ "',DOGUM_T='"+maskedTextBox1.Text+ "',IRKI='"+comboBox1.Text+"',CINSIYETI='"+comboBox2.Text+"',GRUBU='"+comboBox3.Text+ "',ANNE_RENGI='"+textBox7.Text+"',ACIKLAMA='"+richTextBox1.Text+"' WHERE KUPE_NO='"+textBox8.Text+"'",baglan);
                baglan.Open();
                güncelle.ExecuteNonQuery();
                baglan.Close();
                getir();
                yoket();

                for(int i=0; i<this.Controls.Count; i++)
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

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            DataTable getir = new DataTable();
            baglan.Open();
            OleDbDataAdapter g = new OleDbDataAdapter("SELECT * FROM besi WHERE KUPE_NO LIKE '"+textBox8.Text+"%'",baglan);
            g.Fill(getir);
            dataGridView1.DataSource = getir;
            baglan.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int kayitsayisi;
            kayitsayisi = dataGridView1.RowCount;
            MessageBox.Show(kayitsayisi.ToString());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ASI ası = new ASI();
            ası.Show();
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            gebe gebe = new gebe();
            gebe.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            satılık satılık = new satılık();
            satılık.Show();
            this.Hide();
        }
    }
}
