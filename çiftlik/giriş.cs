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
    public partial class giriş : Form
    {
        public giriş()
        {
            InitializeComponent();
        }

        OleDbConnection baglan = new OleDbConnection("Provider = Microsoft.ACE.OLEDB.12.0;" + "Data Source = ciftlik.accdb");

        private void button1_Click(object sender, EventArgs e)
        {
            baglan.Open();
            OleDbCommand giriş = new OleDbCommand("SELECT * FROM KULLANICI WHERE KULLANICI_ADI=@P1 AND SIFRE=@P2",baglan);
            giriş.Parameters.AddWithValue("@p1",textBox1.Text);
            giriş.Parameters.AddWithValue("@p2",textBox2.Text);
            OleDbDataReader oku = giriş.ExecuteReader();

            if(oku.Read())
            {
                besi aç = new besi();
                aç.Show();
                Hide();
            }
        }
        bool değişken;
        void varmı()
        {
            baglan.Open();
            OleDbCommand sec = new OleDbCommand("SELECT * FROM KULLANICI WHERE KULLANICI_ADI=@P1", baglan);
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

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                varmı();
                if (değişken == true)
                {
                    OleDbCommand ekle = new OleDbCommand("INSERT INTO KULLANICI(KULLANICI_ADI,SIFRE) values('" + textBox1.Text + "','" + textBox2.Text + "')", baglan);
                    baglan.Open();
                    ekle.ExecuteNonQuery();
                    baglan.Close();

                    MessageBox.Show("kayıt başarılı");

                    for (int i = 0; i < this.Controls.Count; i++)
                    {
                        if (Controls[i] is TextBox) Controls[i].Text = "";
                    }
                }
                else
                    MessageBox.Show("Bu Kullanıcı zaten var");
            }
            catch (Exception sorun)
            {
                MessageBox.Show(sorun.Message);
            }
            
        }
    }
}
