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
    public partial class Form9_mag : Form
    {
        private string path;
        private string sql;
        private string zalogowany;
        private string magazyn_domyslny;
        private int i;
        public Form9_mag()
        {
            InitializeComponent();
        }

        private void Form9_mag_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
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
                sql = "Select * from Dane_Firmy_" + zalogowany + "";
                cmd = new SQLiteCommand(sql, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    magazyn_domyslny = reader["Magazyn_domyslny_magazyn"].ToString();
                    textBox5.Text = reader["Magazyn_domyslna_jednostka"].ToString();
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
                sql = "Select * from Edycja_kartoteki";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["Symbol_magazynu"].ToString();
                    label27.Text = reader["Symbol_magazynu"].ToString();
                    textBox2.Text = reader["Indeks"].ToString();
                    textBox3.Text = reader["Symbol_SWW"].ToString();
                    textBox4.Text = reader["Nazwa"].ToString();
                    textBox5.Text = reader["Jednostka_miary"].ToString();
                    i = comboBox1.FindStringExact(reader["Typ"].ToString());
                    comboBox1.SelectedIndex = i;
                    i = comboBox2.FindStringExact(reader["Stawka_VAT"].ToString());
                    comboBox2.SelectedIndex = i;
                    textBox6.Text = reader["Stan_magazynowy"].ToString();
                    textBox7.Text = reader["Minimalny_stan"].ToString();
                    textBox8.Text = reader["Okres_zbywania"].ToString();
                    textBox15.Text = reader["Rezerwacja"].ToString();
                    textBox9.Text = reader["Cena_I"].ToString();
                    textBox10.Text = reader["Cena_II"].ToString();
                    textBox11.Text = reader["Cena_III"].ToString();
                    textBox12.Text = reader["Cena_IV"].ToString();
                    textBox13.Text = reader["Cena_zakupu"].ToString();
                    textBox14.Text = reader["Marza"].ToString();
                    pictureBox1.ImageLocation = reader["JPG"].ToString();
                }

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

        private void Form9_mag_Activated(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select * from Dane_Firmy_" + zalogowany + "";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    magazyn_domyslny = reader["Magazyn_domyslny_magazyn"].ToString();
                    textBox5.Text = reader["Magazyn_domyslna_jednostka"].ToString();
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

        private void button2_Click(object sender, EventArgs e)
        {

            Form7_mag form7 = new Form7_mag();
            form7.ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
            pictureBox1.ImageLocation = openFileDialog1.FileName;
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox8_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox9_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox11_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox13_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }




    }
}
