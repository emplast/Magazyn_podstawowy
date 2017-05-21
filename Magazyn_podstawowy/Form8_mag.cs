using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazyn_podstawowy
{
    public partial class Form8_mag : Form
    {
        private string path;
        private string sql;
        private string zalogowany;
        public Form8_mag()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form8_mag_Load(object sender, EventArgs e)
        {
            textBox7.Focus();
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source ='" + path + "';Version=3");
            try
            {
                con.Open();
                sql = "Select * from Zalogowany";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    zalogowany = reader["Nazwa"].ToString();
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select * from Edycja_dokumentu_magazynowego";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["Magazyn"].ToString();
                    textBox2.Text = reader["Indeks"].ToString();
                    textBox3.Text = reader["Nazwa"].ToString();
                    textBox14.Text = reader["Ilość"].ToString();
                    textBox4.Text = reader["Jednostka_miary"].ToString();
                    textBox8.Text = reader["Cena_sprzedaży"].ToString();
                    textBox10.Text = reader["Rabat"].ToString();
                    textBox11.Text = reader["Cena_z_rabatem"].ToString();
                    textBox5.Text = reader["Wartość"].ToString();
                    textBox6.Text = reader["Stawka_VAT"].ToString();
                    textBox7.Text = reader["Cena_zakupu"].ToString();
                }
                reader.Close();
                sql = "Select * from Magazyn_kartoteki where Nazwa='" + textBox3.Text + "' and Indeks='" + textBox2.Text + "'";
                cmd = new SQLiteCommand(sql, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    pictureBox1.ImageLocation = reader["JPG"].ToString();
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Update Pozycje_dokumentu set Magazyn='" + textBox1.Text + "',Indeks='" + textBox2.Text + "',Nazwa='" + textBox3.Text + "',Ilość='" + textBox14.Text + "',Jednostka_miary='" + textBox4.Text + "',Cena_sprzedaży='" + textBox8.Text + "',Rabat='" + textBox10.Text + "',Cena_z_rabatem='" + textBox11.Text + "',Wartość='" + textBox5.Text + "',Stawka_VAT='" + textBox6.Text + "',Cena_zakupu='" + textBox7.Text + "' where Nazwa='" + textBox3.Text + "'";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                con.Close();
            }
            Close();

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            timer1.Interval = 5;
            timer1.Start();
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            timer1.Interval = 5;
            timer1.Start();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            //textBox5.Text = (Convert.ToDouble(textBox11.Text) * Convert.ToDouble(textBox14.Text)).ToString();
        }

        private void textBox11_TextChanged(object sender, EventArgs e)
        {
            // textBox11.Text = ((Convert.ToDouble(textBox8.Text) - (Convert.ToDouble(textBox8.Text) * (Convert.ToDouble(textBox10.Text) / 100)))).ToString();
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            timer1.Interval = 5;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (textBox10.Text != "0" & textBox10.Text != string.Empty & textBox8.Text != string.Empty & textBox8.Text != "0")
            {
                textBox11.Text = ((Convert.ToDouble(textBox8.Text) - (Convert.ToDouble(textBox8.Text) * (Convert.ToDouble(textBox10.Text) / 100)))).ToString();
            }
            else
            {
                textBox11.Text = textBox8.Text;

            }
            if (textBox14.Text != string.Empty & textBox14.Text != "0" & textBox8.Text != string.Empty & textBox8.Text != "0")
            {
                textBox5.Text = (Convert.ToDouble(textBox11.Text) * Convert.ToDouble(textBox14.Text)).ToString();

            }
            else
            {
                textBox5.Text = "0";
            }
        }





    }
}
