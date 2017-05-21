using Devart.Data.SQLite;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Magazyn_podstawowy
{
    public partial class Form3_mag : Form
    {
        #region Zmienne
        private string sql;
        private string path;
        private string zalogowany;
        private string magazyn_domyslny;
        private string Indeks_bazy;
        private string[] Indeks_bazy_char;
        private int numer_ineksu;
        private string folder;
        private string katalog_1;
        private string katalog_2;
        private string nazwa;
        private string sprzedaz_1;
        private string sprzedaz_2;
        private string cena_1;
        private string cena_2;
        private string cena_3;
        private string cena_4;
        private string domyslna_jednostka;
        private string magazyn_opis;
        
        
            



        #endregion

        public Form3_mag()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
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
                    domyslna_jednostka = reader["Magazyn_domyslna_jednostka"].ToString();

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
                sql = "Delete from Jednostka_miary";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Insert into Jednostka_miary (Nazwa_jednostki) values('" + domyslna_jednostka + "')";
                cmd = new SQLiteCommand(sql, con);
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text == string.Empty)
            {
                
                MessageBox.Show("Wprowadż nazwę towaru", "Informacja");
                textBox4.BackColor = Color.Yellow;
                textBox4.Text = string.Empty;
                textBox4.Focus();
                return;
            }

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
                    domyslna_jednostka = reader["Magazyn_domyslna_jednostka"].ToString();

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
                sql = "Select * from Magazyn_kartoteki";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    if(reader["Nazwa"].ToString()==textBox4.Text)
                    {
                        MessageBox.Show("Podałeś tą samą nazwę artykułu", "Informacja");
                        textBox4.Text = string.Empty;
                        textBox4.BackColor = Color.Yellow;
                        textBox4.Focus();
                        return;
                    }
                }
                reader.Close();
                sql = "Insert into Magazyn_kartoteki(Symbol_magazynu,Indeks,Symbol_SWW ,Nazwa ,Jednostka_miary ,Typ ,Stawka_VAT ,Stan_magazynowy ,Minimalny_stan ,Okres_zbywania,Rezerwacja,Cena_I ,Cena_II ,Cena_III ,Cena_IV ,Cena_zakupu ,Marza,JPG ) values ('"+label27.Text+"','"+textBox2.Text+"','" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "','" + comboBox1.SelectedItem.ToString() + "','" + comboBox2.SelectedItem.ToString() + "','" + textBox6.Text + "','" + textBox7.Text + "','" + textBox8.Text + "','"+textBox15.Text+"','" + textBox9.Text + "','" + textBox10.Text + "','" + textBox11.Text + "','" + textBox12.Text + "','" + textBox13.Text + "','" + textBox14.Text + "','"+pictureBox1.ImageLocation+"') ";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Delete from Jednostka_miary";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Insert into Jednostka_miary (Nazwa_jednostki) values('" + domyslna_jednostka + "')";
                cmd = new SQLiteCommand(sql, con);
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

        private void button1_Click(object sender, EventArgs e)
        {
            Form4_mag form4 = new Form4_mag();
            form4.ShowDialog();
            
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
            Form7_mag form7 = new Form7_mag();
            form7.ShowDialog();
        }

        private void Form3_mag_Load(object sender, EventArgs e)
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
                    
                    sprzedaz_1 = reader["Magazyn_sprzedawaj_w_cenach_brutto"].ToString();
                    sprzedaz_2 = reader["Magazyn_ustal_cene_sprzedazy_1_wedlug_narzutu"].ToString();
                    cena_1 = reader["Magazyn_cena_sprzedazy_1"].ToString();
                    cena_2 = reader["Magazyn_cena_sprzedazy_2"].ToString();
                    cena_3 = reader["Magazyn_cena_sprzedazy_3"].ToString();
                    cena_4 = reader["Magazyn_cena_sprzedazy_4"].ToString();
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
                sql = "Select* from Magazyn";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    magazyn_domyslny = reader["Symbol"].ToString();
                    magazyn_opis = reader["Opis"].ToString();
                    textBox1.Text = magazyn_opis;
                    label27.Text = magazyn_domyslny;
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

        private void Form3_mag_Activated(object sender, EventArgs e)
        {
            
            Indeks_bazy = "0" + "/" + magazyn_domyslny;
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
                sql = "Select * from Magazyn_kartoteki where Symbol_magazynu='"+magazyn_domyslny+"'";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                  Indeks_bazy = reader["Indeks"].ToString();
                }
                reader.Close();
                sql = "Select * from Jednostka_miary";
                cmd = new SQLiteCommand(sql, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    textBox5.Text = reader["Nazwa_jednostki"].ToString();
                }
                reader.Close();
                sql = "Select* from Magazyn";
                cmd = new SQLiteCommand(sql, con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    magazyn_domyslny = reader["Symbol"].ToString();
                    magazyn_opis = reader["Opis"].ToString();
                    textBox1.Text = magazyn_opis;
                    label27.Text = magazyn_domyslny;
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
            
            Indeks_bazy_char = Indeks_bazy.Split('/');
            numer_ineksu = Convert.ToInt32(Indeks_bazy_char[0]);
            textBox2.Text = Convert.ToInt32(numer_ineksu + 1).ToString() + "/" + label27.Text;
           
           
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            textBox4.BackColor = Color.White;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

            

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            
            
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch !=48 && ch!= 49 && ch != 50 && ch!= 51 && ch != 52 && ch != 53 && ch!= 54 && ch!= 55 && ch != 56 && ch!= 57 && ch != 8 && ch != 44 && ch != 110 )//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled=true;
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
        private void textBox14_KeyPress(object sender, KeyPressEventArgs e)
        {
            char ch = e.KeyChar;
            if (ch != 48 && ch != 49 && ch != 50 && ch != 51 && ch != 52 && ch != 53 && ch != 54 && ch != 55 && ch != 56 && ch != 57 && ch != 8 && ch != 44 && ch != 110)//8 backspace ,46 delete 110 kropka numeryczna
            {
                e.Handled = true;
            }
        }

        
        private void button5_Click(object sender, EventArgs e)
        {
            folder = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Obrazy");
            if (!System.IO.Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            openFileDialog1.ShowDialog();
            nazwa = System.IO.Path.GetFileName(openFileDialog1.FileName);
            katalog_1 = System.IO.Path.Combine(System.IO.Path.GetDirectoryName(openFileDialog1.FileName), nazwa);
            katalog_2 = System.IO.Path.Combine(folder, nazwa);
            System.IO.File.Copy(katalog_1, katalog_2, true);
            pictureBox1.ImageLocation = folder + "\\" + nazwa;

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {
            if (sprzedaz_2 == "True")
            {
                if (textBox13.Text != "0" & textBox13.Text != string.Empty & textBox14.Text != "0" & textBox14.Text != string.Empty)
                {
                    textBox9.Text = ((Convert.ToDouble(textBox13.Text) * (Convert.ToDouble(textBox14.Text) / 100)) + Convert.ToDouble(textBox13.Text)).ToString();
                }
            }
            
        }

        private void textBox13_TextChanged(object sender, EventArgs e)
        {
            if (sprzedaz_1 == "True")
            {
                textBox9.Text = (Convert.ToDouble(textBox13.Text) * (Convert.ToDouble(cena_1) / 100) + Convert.ToDouble(textBox13.Text)).ToString();
                textBox10.Text = (Convert.ToDouble(textBox13.Text) * (Convert.ToDouble(cena_2) / 100) + Convert.ToDouble(textBox13.Text)).ToString();
                textBox11.Text = (Convert.ToDouble(textBox13.Text) * (Convert.ToDouble(cena_3) / 100) + Convert.ToDouble(textBox13.Text)).ToString();
                textBox12.Text = (Convert.ToDouble(textBox13.Text) * (Convert.ToDouble(cena_4) / 100) + Convert.ToDouble(textBox13.Text)).ToString();
            }
        }

       
    }
}
