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
    public partial class Form7_mag : Form
    {
        private string path;
        private string zalogowany;
        private string sql;
        private string domyslna_jednostka;
       
        public Form7_mag()
        {
            InitializeComponent();
        }

       
        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form7_mag_Load(object sender, EventArgs e)
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
            con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Select * from Jednostki_miary";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                SQLiteDataAdapter adapter = new SQLiteDataAdapter();
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                ds.Clear();
                adapter.SelectCommand = cmd;
                adapter.Fill(ds);
                cmd.ExecuteNonQuery();
                dataGridView1.DataSource = ds.Tables[0];
                dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
                dataGridView1.RowHeadersVisible = false;
                dataGridView1.Columns[0].Width = 30;


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());

            }
            finally
            {
                con.Close();
            }
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
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
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1[1, i].Value.ToString() == domyslna_jednostka)
                    {

                        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                        

                    }

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
        private void button1_Click(object sender, EventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Delete from Jednostki_miary";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    sql = "Insert into Jednostki_miary (Nazwa_jednostki) values ('" + dataGridView1.Rows[i].Cells[1].Value + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                sql = "Delete from Jednostka_miary";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql="Insert into Jednostka_miary (Nazwa_jednostki) values('"+dataGridView1.CurrentRow.Cells[1].Value+"')";
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

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            timer2.Interval = 2;
            timer2.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value = (dataGridView1.CurrentRow.Index + 1).ToString();
        }

        private void dataGridView1_CellContentDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Delete from Jednostki_miary";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    sql = "Insert into Jednostki_miary (Nazwa_jednostki) values ('" + dataGridView1.Rows[i].Cells[1].Value + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
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
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            con = new SQLiteConnection("Data Source = '" + path + "';Version=3");
            try
            {
                con.Open();
                sql = "Update Dane_Firmy_" + zalogowany + " set Magazyn_domyslna_jednostka='" + dataGridView1.CurrentRow.Cells[1].Value.ToString() + "'Where Lp=1";
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
    }
}
