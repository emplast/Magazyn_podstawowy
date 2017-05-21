using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Program_Księgowy;
using System.Xml.XPath;
using System.Net;
using System.IO;
using System.Data.SQLite;
using Moja_Ksiegowosc;

namespace Magazyn_podstawowy
{
    public partial class Form1_mag : Form
    {

        #region Zmienne

        private string sql;
        private string path;
        private string zalogowany;


        #endregion

        public Form1_mag()
        {
            InitializeComponent();
        }
        private void aktualizacja_kursów()
        {
            List<string> files = new List<string>();

            WebClient wc = new WebClient();
            wc.DownloadFile("http://www.nbp.pl/Kursy/xml/dir.txt", "dir.txt");

            StreamReader sr = new StreamReader("dir.txt");
            string line = "";

            while (line != null)
            {
                line = sr.ReadLine();

                if (line != null)
                {
                    if (line.StartsWith("a"))
                    {
                        files.Add(line);
                    }
                }
            }

            sr.Close();

            try
            {
                XPathDocument document = new XPathDocument("http://www.nbp.pl/kursy/xml/" + files[files.Count - 1] + ".xml");
                XPathNavigator navigator = document.CreateNavigator();
                XPathNodeIterator iterator;
                iterator = navigator.Select("tabela_kursow");

                foreach (XPathNavigator nav in iterator)
                {
                    label18.Text = nav.SelectSingleNode("data_publikacji").Value;
                    //dataToolStripMenuItem.Text = "Data kursu: " + dataLabel.Text;
                }

                iterator = navigator.Select("tabela_kursow/pozycja");

                foreach (XPathNavigator nav in iterator)
                {

                    if (nav.SelectSingleNode("kod_waluty").Value == "BGN" || nav.SelectSingleNode("kod_waluty").Value == "CHF" || nav.SelectSingleNode("kod_waluty").Value == "CZK" || nav.SelectSingleNode("kod_waluty").Value == "DKK" || nav.SelectSingleNode("kod_waluty").Value == "EUR" || nav.SelectSingleNode("kod_waluty").Value == "GBP" || nav.SelectSingleNode("kod_waluty").Value == "HRK" || nav.SelectSingleNode("kod_waluty").Value == "HUF" || nav.SelectSingleNode("kod_waluty").Value == "RON" || nav.SelectSingleNode("kod_waluty").Value == "SEK" || nav.SelectSingleNode("kod_waluty").Value == "USD")
                    {
                        if (nav.SelectSingleNode("kod_waluty").Value == "BGN" && comboBox3.SelectedIndex == 0)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "CHF" && comboBox3.SelectedIndex == 1)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "CZK" && comboBox3.SelectedIndex == 2)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "DKK" && comboBox3.SelectedIndex == 3)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "EUR" && comboBox3.SelectedIndex == 4)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "GBP" && comboBox3.SelectedIndex == 5)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "HRK" && comboBox3.SelectedIndex == 6)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "HUF" && comboBox3.SelectedIndex == 7)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (comboBox3.SelectedIndex == 8)
                            textBox7.Text = "1,00";
                        if (nav.SelectSingleNode("kod_waluty").Value == "RON" && comboBox3.SelectedIndex == 9)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "SEK" && comboBox3.SelectedIndex == 10)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                        if (nav.SelectSingleNode("kod_waluty").Value == "USD" && comboBox3.SelectedIndex == 11)
                            textBox7.Text = nav.SelectSingleNode("kurs_sredni").Value;
                    }
                }
            }
            catch (XPathException ex)
            {
                MessageBox.Show("Błąd przy pobieraniu kursów walut" + ex.ToString());
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                Close();
            }
            else
            {


                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Delete from Pozycje_dokumentu";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Delete from Pozycja_dokumentu";
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
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex = 0;
            comboBox2.Enabled = false;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 8;
            textBox7.Text = "1,00";

            #region Ustawienia pól edycyjnych

            switch (comboBox1.SelectedItem.ToString())
            {
                case "BO":
                    textBox2.Text = "BO";
                    break;
                case "MM+":
                    textBox2.Text = "MM+";
                    break;
                case "MM-":
                    textBox2.Text = "MM-";
                    break;
                case "PW":
                    textBox2.Text = "PW";
                    break;
                case "PZ":
                    textBox2.Text = "PZ";
                    break;
                case "RR":
                    textBox2.Text = "RR";
                    break;
                case "RZ":
                    textBox2.Text = "RZ";
                    break;
                case "WZ":
                    textBox2.Text = "WZ";
                    break;


            }

            #endregion


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
                sql = "Select * from Pozycje_dokumentu";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;
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

        private void Form1_mag_Activated(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select * from Pozycje_dokumentu";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.AlternatingRowsDefaultCellStyle.BackColor = Color.LightGreen;
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


        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox2.SelectedIndex)
            {


                case 0:
                    groupBox2.Text = "Faktura";
                    label7.Text = "Numer Faktury";
                    label8.Text = "Data Faktury";
                    label9.Text = "Termin Faktury";
                    dateTimePicker3.Visible = true;
                    label9.Visible = true;
                    break;
                case 1:
                    groupBox2.Text = "Paragon";
                    label7.Text = "Numer Paragonu";
                    label8.Text = "Data Paragonu";
                    label9.Visible = false;
                    dateTimePicker3.Visible = false;
                    break;
                case 2:
                    groupBox2.Text = "Bez Faktury";
                    label7.Text = "Numer WZ";
                    label8.Text = "Data WZ";
                    label9.Visible = false;
                    dateTimePicker3.Visible = false;
                    break;

            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == 7)
            {
                comboBox2.Enabled = true;
            }
            else
            {
                comboBox2.Enabled = false;
                comboBox2.SelectedIndex = 0;
            }
            switch (comboBox1.SelectedItem.ToString())
            {
                case "BO":
                    textBox2.Text = "BO";
                    break;
                case "MM+":
                    textBox2.Text = "MM+";
                    break;
                case "MM-":
                    textBox2.Text = "MM-";
                    break;
                case "PW":
                    textBox2.Text = "PW";
                    break;
                case "PZ":
                    textBox2.Text = "PZ";
                    break;
                case "RR":
                    textBox2.Text = "RR";
                    break;
                case "RZ":
                    textBox2.Text = "RZ";
                    break;
                case "WZ":
                    textBox2.Text = "WZ";
                    break;


            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Form2_mag form2 = new Form2_mag();
            if (Application.OpenForms["Form2_mag"] == null)
            {
                form2.Show();
            }
            else
            {
                this.SendToBack();
                form2.BringToFront();
            }

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex)
            {
                case 0:
                    label13.Image = Properties.Resources.bg;
                    break;
                case 1:
                    label13.Image = Properties.Resources.ch;
                    break;
                case 2:
                    label13.Image = Properties.Resources.cz;
                    break;
                case 3:
                    label13.Image = Properties.Resources.dk;
                    break;
                case 4:
                    label13.Image = Properties.Resources.europeanunion;
                    break;
                case 5:
                    label13.Image = Properties.Resources.gb;
                    break;
                case 6:
                    label13.Image = Properties.Resources.hr;
                    break;
                case 7:
                    label13.Image = Properties.Resources.hu;
                    break;
                case 8:
                    label13.Image = Properties.Resources.pl;
                    textBox7.Text = "1,00";
                    break;
                case 9:
                    label13.Image = Properties.Resources.ro;
                    break;
                case 10:
                    label13.Image = Properties.Resources.se;
                    break;
                case 11:
                    label13.Image = Properties.Resources.us;
                    break;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            aktualizacja_kursów();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowEnter(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Interval = 2;
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells.Count; i++)
            {
                dataGridView1[i, dataGridView1.CurrentRow.Index].Style.BackColor = Color.LightBlue;

            }

            timer1.Stop();
        }

        private void dataGridView1_RowLeave(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows[e.RowIndex].Cells.Count; i++)
            {
                dataGridView1[i, e.RowIndex].Style.BackColor = Color.White;

            }

        }

        private void button6_Click(object sender, EventArgs e)
        {


            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Musisz stworzyć przynajmniej jedną pozycję dokumentu", "Informacja");
                return;
            }
            else
            {
                int i = 0;
                i = dataGridView1.CurrentRow.Index;
                dataGridView1.Rows.RemoveAt(i);



                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Delete from Pozycje_dokumentu";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    for (i = 0; i < dataGridView1.RowCount - 1; i++)
                    {
                        sql = "Insert into Pozycje_dokumentu (Magazyn,Indeks,Nazwa,Ilość,Jednostka_miary,Cena_sprzedaży,Rabat,Cena_z_rabatem,Wartość,Stawka_VAT,Cena_zakupu) values('" + dataGridView1[1, i].Value + "','" + dataGridView1[2, i].Value + "','" + dataGridView1[3, i].Value + "','" + dataGridView1[4, i].Value + "','" + dataGridView1[5, i].Value + "','" + dataGridView1[6, i].Value + "','" + dataGridView1[7, i].Value + "','" + dataGridView1[8, i].Value + "','" + dataGridView1[9, i].Value + "','" + dataGridView1[10, i].Value + "','" + dataGridView1[11, i].Value + "')";
                        cmd = new SQLiteCommand(sql, con);
                        cmd.ExecuteNonQuery();
                    }
                    sql = "Select * from Pozycje_dokumentu";
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
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.RowCount > 1)
            {
                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Delete from Edycja_dokumentu_magazynowego";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Edycja_dokumentu_magazynowego (Magazyn,Indeks,Nazwa,Ilość,Jednostka_miary, Cena_sprzedaży,Rabat,Cena_z_rabatem ,Wartość,Stawka_VAT,Cena_zakupu) values('" + dataGridView1.CurrentRow.Cells[1].Value + "','" + dataGridView1.CurrentRow.Cells[2].Value + "','" + dataGridView1.CurrentRow.Cells[3].Value + "','" + dataGridView1.CurrentRow.Cells[4].Value + "','" + dataGridView1.CurrentRow.Cells[5].Value + "','" + dataGridView1.CurrentRow.Cells[6].Value + "','" + dataGridView1.CurrentRow.Cells[7].Value + "','" + dataGridView1.CurrentRow.Cells[8].Value + "','" + dataGridView1.CurrentRow.Cells[9].Value + "','" + dataGridView1.CurrentRow.Cells[10].Value + "','" + dataGridView1.CurrentRow.Cells[11].Value + "')";
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

                Form8_mag form8 = new Form8_mag();
                form8.ShowDialog();
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Do edycji musi zostać stworzona jedna pozycja dokumentu", "Informacja");
                return;
            }
            else
            {


                path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
                SQLiteConnection con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
                try
                {
                    con.Open();
                    sql = "Delete from Edycja_dokumentu_magazynowego";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                    sql = "Insert into Edycja_dokumentu_magazynowego (Magazyn,Indeks,Nazwa,Ilość,Jednostka_miary, Cena_sprzedaży,Rabat,Cena_z_rabatem ,Wartość,Stawka_VAT,Cena_zakupu) values('" + dataGridView1.CurrentRow.Cells[1].Value + "','" + dataGridView1.CurrentRow.Cells[2].Value + "','" + dataGridView1.CurrentRow.Cells[3].Value + "','" + dataGridView1.CurrentRow.Cells[4].Value + "','" + dataGridView1.CurrentRow.Cells[5].Value + "','" + dataGridView1.CurrentRow.Cells[6].Value + "','" + dataGridView1.CurrentRow.Cells[7].Value + "','" + dataGridView1.CurrentRow.Cells[8].Value + "','" + dataGridView1.CurrentRow.Cells[9].Value + "','" + dataGridView1.CurrentRow.Cells[10].Value + "','" + dataGridView1.CurrentRow.Cells[11].Value + "')";
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

                Form8_mag form8 = new Form8_mag();
                form8.ShowDialog();
            }

        }


    }
}
