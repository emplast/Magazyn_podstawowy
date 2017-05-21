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
    public partial class Form6_mag : Form
    {
        private string path;
        private string sql;
        private string zalogowany;
        private string magazyn_domyslny;
        private double wartosc;
        public Form6_mag()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            textBox7.Focus();
            
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
                sql = "Select * from Pozycja_dokumentu";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox1.Text = reader["Magazyn"].ToString();
                    textBox2.Text = reader["Indeks"].ToString();
                    textBox3.Text = reader["Nazwa"].ToString();
                    textBox4.Text = reader["Jednostka_miary"].ToString();
                    textBox5.Text = reader["Typ"].ToString();
                    textBox6.Text = reader["Stawka_VAT"].ToString();
                    textBox7.Text = reader["Cena_zakupu"].ToString();
                    textBox9.Text = reader["Marża"].ToString();
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
            if (textBox8.Text == "0" || textBox8.Text == string.Empty)
            {
                MessageBox.Show("Podaj cenę sprzedaży", "Informacja");
                textBox8.BackColor = Color.Yellow;
                textBox8.Focus();
                return;
            }
            if (textBox14.Text == "0" || textBox14.Text == string.Empty)
            {
                MessageBox.Show("Podaj ilość asortymentu", "Informacja");
                textBox14.BackColor = Color.Yellow;
                textBox14.Focus();
                return;
            }
            wartosc = Convert.ToDouble(textBox11.Text) * Convert.ToDouble(textBox14.Text);
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Insert into Pozycje_dokumentu (Magazyn,Indeks,Nazwa,Ilość,Jednostka_miary,Cena_sprzedaży,Rabat,Cena_z_rabatem,Wartość,Stawka_VAT,Cena_zakupu) values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox14.Text + "','" + textBox4.Text + "','" + textBox8.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + wartosc.ToString() + "','" + textBox6.Text + "','" + textBox7.Text + "')";
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
            this.Visible = false;
            this.Close();
 
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

        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }
        
        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }

        }
       
        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text != "0" & textBox9.Text != "0" & textBox9.Text != string.Empty)
                textBox8.Text = ((Convert.ToDouble(textBox7.Text) * Convert.ToDouble(textBox9.Text) / 100) + Convert.ToDouble(textBox7.Text)).ToString();
            
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            textBox11.Text = textBox8.Text;
            textBox8.BackColor = Color.Empty;
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text != "0" & textBox8.Text != string.Empty & textBox10.Text != "0" & textBox10.Text != string.Empty)
            {
                textBox11.Text = (Convert.ToDouble(textBox8.Text) - (Convert.ToDouble(textBox8.Text) * (Convert.ToDouble(textBox10.Text) / 100))).ToString();
            }

            else
            {
                textBox11.Text = textBox8.Text;
               
            }
               
        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            textBox14.BackColor = Color.Empty;
        }

       
    }
}
