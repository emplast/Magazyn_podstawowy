using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;

namespace Magazyn_podstawowy
{
    public partial class Form2_mag : Form
    {
        #region Zmienne


        private string sql;
        private string path;
        private string zalogowany;
        private string magazyn_domyslny;
        public DataGridViewRow pozycja;
        private string zdjęcie;
        private string typ;
        private string symbol_magazynu;
        private string indeks;
        private string symbol_sww;
        private string nazwa;
        private string jednostka_miary;
        private string stawka_vat;
        private string stan_magazynowy;
        private string minimalny_stan;
        private string okres_zbywania;
        private string rezerwacja;
        private string cena_I;
        private string cena_II;
        private string cena_III;
        private string cena_IV;
        private string cena_zakupu;
        private string marza;
        private string jpg;
        private string magazyn_opis;
        private string magazyn_typ;
        
        
        






        #endregion

        public Form2_mag()
        {
            InitializeComponent();
        }

        private void button8_Click(object sender, EventArgs e)
        {
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
                    magazyn_opis = reader["Magazyn_domyslny_opis"].ToString();
                    magazyn_typ = reader["Magazyn_domyslny_typ"].ToString();
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
                sql = "Delete from Magazyn";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Insert into Magazyn (Symbol,Opis,Typ) values('" + magazyn_domyslny + "','" + magazyn_opis + "','" + magazyn_typ + "')";
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

        private void button2_Click(object sender, EventArgs e)
        {
            Form3_mag form3 = new Form3_mag();
            //if (Application.OpenForms["Form3_mag"] == null)
            //{
            //    form3.Show();
            //}
            //else
            //{
            //    this.SendToBack();
            //    form3.BringToFront();
            //}
            form3.ShowDialog();
            
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Form4_mag form4 = new Form4_mag();
            form4.ShowDialog();
            
        }

        private void Form2_mag_Activated(object sender, EventArgs e)
        {
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
                sql = "Select* from Magazyn";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    magazyn_domyslny = reader["Symbol"].ToString();
                }
                reader.Close();
                sql = "Select * from Magazyn_kartoteki where Symbol_magazynu='" + magazyn_domyslny + "' ";
                cmd = new SQLiteCommand(sql, con);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;

                dataGridView1.Columns[0].Width = 85;
                dataGridView1.Columns[1].Width = 50;
                dataGridView1.Columns.RemoveAt(2);
                dataGridView1.Columns[2].Width = 350;
                dataGridView1.Columns[3].Width = 85;
                dataGridView1.Columns.RemoveAt(4);
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 100;
                dataGridView1.Columns.RemoveAt(7);
                dataGridView1.Columns[7].Width = 75;
                dataGridView1.Columns[8].Width = 100;
                dataGridView1.Columns.RemoveAt(9);
                dataGridView1.Columns.RemoveAt(9);
                dataGridView1.Columns.RemoveAt(9);
                dataGridView1.Columns[9].Width = 75;
                dataGridView1.Columns[10].Width = 75;
                dataGridView1.Columns.RemoveAt(11);

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

        }

        private void Form2_mag_Load(object sender, EventArgs e)
        {
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
                sql = "Select* from Magazyn";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    magazyn_domyslny = reader["Symbol"].ToString();
                }
                reader.Close();
                sql = "Select * from Magazyn_kartoteki where Symbol_magazynu='" + magazyn_domyslny + "' ";
                cmd = new SQLiteCommand(sql, con);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;

                dataGridView1.Columns[0].Width = 85;
                dataGridView1.Columns[1].Width = 50;
                dataGridView1.Columns.RemoveAt(2);
                dataGridView1.Columns[2].Width = 350;
                dataGridView1.Columns[3].Width = 85;
                dataGridView1.Columns.RemoveAt(4);
                dataGridView1.Columns[4].Width = 100;
                dataGridView1.Columns[5].Width = 100;
                dataGridView1.Columns[6].Width = 100;
                dataGridView1.Columns.RemoveAt(7);
                dataGridView1.Columns[7].Width=75;
                dataGridView1.Columns[8].Width = 100;
                dataGridView1.Columns.RemoveAt(9);
                dataGridView1.Columns.RemoveAt(9);
                dataGridView1.Columns.RemoveAt(9);
                dataGridView1.Columns[9].Width = 75;
                dataGridView1.Columns[10].Width=75;
                dataGridView1.Columns.RemoveAt(11);

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

            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells.Count; i++)
            {
                dataGridView1[i, dataGridView1.CurrentRow.Index].Style.BackColor = Color.LightBlue;

            }

            timer1.Stop();
        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Interval = 2;
            timer1.Start();
            
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows[e.RowIndex].Cells.Count; i++)
            {
                dataGridView1[i, e.RowIndex].Style.BackColor = Color.White;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                Form3_mag form3 = new Form3_mag();
                form3.ShowDialog();
            }
            else
            {


                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Select * from Magazyn_kartoteki where Symbol_magazynu='" + magazyn_domyslny + "' and Nazwa='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        zdjęcie = reader["JPG"].ToString();
                        typ = reader["Typ"].ToString();
                    }
                    reader.Close();
                    sql = "Delete from Pozycja_dokumentu";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Pozycja_dokumentu (Magazyn,Indeks,Nazwa,Jednostka_miary,Typ,Stawka_VAT,Cena_zakupu,Marża,JPG) values('" + dataGridView1.CurrentRow.Cells[0].Value + "','" + dataGridView1.CurrentRow.Cells[1].Value + "','" + dataGridView1.CurrentRow.Cells[2].Value + "','" + dataGridView1.CurrentRow.Cells[3].Value + "','" + typ + "','" + dataGridView1.CurrentRow.Cells[4].Value + "','" + dataGridView1.CurrentRow.Cells[9].Value + "','"+dataGridView1.CurrentRow.Cells[10].Value+"','" + zdjęcie + "')";
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


                this.Visible = false;
                Form6_mag form6 = new Form6_mag();
                form6.ShowDialog();
                this.Close();
            }

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                Form3_mag form3 = new Form3_mag();
                form3.ShowDialog();
            }
            else
            {


                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Select * from Magazyn_kartoteki where Symbol_magazynu='" + magazyn_domyslny + "' and Nazwa='" + dataGridView1.CurrentRow.Cells[2].Value.ToString() + "'";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        zdjęcie = reader["JPG"].ToString();
                        typ = reader["Typ"].ToString();
                    }
                    reader.Close();
                    sql = "Delete from Pozycja_dokumentu";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Pozycja_dokumentu (Magazyn,Indeks,Nazwa,Jednostka_miary,Typ,Stawka_VAT,Cena_zakupu,Marża,JPG) values('" + dataGridView1.CurrentRow.Cells[0].Value + "','" + dataGridView1.CurrentRow.Cells[1].Value + "','" + dataGridView1.CurrentRow.Cells[2].Value + "','" + dataGridView1.CurrentRow.Cells[3].Value + "','" + typ + "','" + dataGridView1.CurrentRow.Cells[4].Value + "','" + dataGridView1.CurrentRow.Cells[9].Value + "','" + dataGridView1.CurrentRow.Cells[10].Value + "','" + zdjęcie + "')";
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


                this.Visible = false;
                Form6_mag form6 = new Form6_mag();
                form6.ShowDialog();
                this.Close();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount==0)
            {
                MessageBox.Show("Musisz stworzyć co najmniej jedną pozycję zestawienia","Informacja");
                return;
            }
            else
            {
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Select * from Magazyn_kartoteki where Symbol_magazynu='" + dataGridView1.CurrentRow.Cells[0].Value + "' and Indeks='" + dataGridView1.CurrentRow.Cells[1].Value + "'";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        symbol_magazynu = reader["Symbol_magazynu"].ToString();
                        indeks = reader["Indeks"].ToString();
                        symbol_sww = reader["Symbol_SWW"].ToString();
                        nazwa = reader["Nazwa"].ToString();
                        jednostka_miary = reader["Jednostka_miary"].ToString();
                        typ = reader["Typ"].ToString();
                        stawka_vat = reader["Stawka_VAT"].ToString();
                        stan_magazynowy = reader["Stan_magazynowy"].ToString();
                        minimalny_stan = reader["Minimalny_stan"].ToString();
                        okres_zbywania = reader["Okres_zbywania"].ToString();
                        rezerwacja = reader["Rezerwacja"].ToString();
                        cena_I = reader["Cena_I"].ToString();
                        cena_II = reader["Cena_II"].ToString();
                        cena_III = reader["Cena_III"].ToString();
                        cena_IV = reader["Cena_IV"].ToString();
                        cena_zakupu = reader["Cena_zakupu"].ToString();
                        marza = reader["Marza"].ToString();
                        jpg = reader["JPG"].ToString();
                    }
                    reader.Close();
                    
                    sql = "Insert into Edycja_kartoteki (Symbol_magazynu,Indeks,Symbol_SWW,Nazwa,Jednostka_miary,Typ,Stawka_VAT,Stan_magazynowy,Minimalny_stan,Okres_zbywania,Rezerwacja,Cena_I,Cena_II,Cena_III,Cena_IV,Cena_zakupu,Marza,JPG) values('" + symbol_magazynu + "','" + indeks + "','" + symbol_sww + "','" + nazwa + "','" + jednostka_miary + "','" + typ + "','" + stawka_vat + "','" + stan_magazynowy + "','" + minimalny_stan + "','" + okres_zbywania + "','" + rezerwacja + "','" + cena_I + "','" + cena_II + "','" + cena_III + "','" + cena_IV + "','" + cena_zakupu + "','" + marza + "','" + jpg + "')";
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
                Form9_mag form9 = new Form9_mag();
                form9.ShowDialog();
            }
        }
    }
}
