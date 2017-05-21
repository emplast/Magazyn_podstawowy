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
    public partial class Form4_mag : Form
    {

        #region Zmienne
        private string path;
        private string sql;
        private string zalogowany;
        private string magazyn_domyslny;
        List<string> list = new List<string>();
        
        #endregion



        
        public Form4_mag()
        {
            InitializeComponent();
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
        private void button2_Click(object sender, EventArgs e)
        {

        }
        private void button1_Click(object sender, EventArgs e)
        {
            
            
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_"+zalogowany+".sqlite");
            SQLiteConnection con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            try
            {
                con.Open();
                sql = "Delete from Magazyny";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                for(int i=0;i<dataGridView1.RowCount-1;i++)
                {
                    
                    sql = "Insert into Magazyny (Symbol,Opis,Typ) Values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                sql = "Delete from Magazyn";
                cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                sql = "Insert into Magazyn (Symbol, Opis,Typ) values('" + dataGridView1.CurrentRow.Cells[1].Value + "','" + dataGridView1.CurrentRow.Cells[2].Value + "','" + dataGridView1.CurrentRow.Cells[3].Value + "')";
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
            //path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            //con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            //try
            //{
            //    con.Open();
            //    sql = "Update Dane_Firmy_" + zalogowany + " set Magazyn_domyslny_magazyn='"+dataGridView1.CurrentRow.Cells[1].Value+"',Magazyn_domyslny_opis='"+dataGridView1.CurrentRow.Cells[2].Value+"',Magazyn_domyslny_typ='"+dataGridView1.CurrentRow.Cells[3].Value+"' where Lp=1";
            //    SQLiteCommand cmd = new SQLiteCommand(sql, con);
            //    cmd.ExecuteNonQuery();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());

            //}
            //finally
            //{
            //    con.Close();
            //}
            #region Zakładanie Magazynów

            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_"+zalogowany+".sqlite");
            con = new SQLiteConnection("Data Source='"+path+"';Version=3;");
            try
            {
                con.Open();
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    sql = "Create table if not exists Magazyn_" + dataGridView1.Rows[i].Cells[1].Value + "(Lp Integer primary key,Indeks varchar(20),Symbol_SWW varchar (20),Nazwa varchar(20),Jednostka_miary varchar(20),Typ varchar(20),Stawka_VAT varchar(20),Stan_magazynowy varchar (20),Mimimalny_stan varchar (20),Okres_zbywania varchar (20),Cena_I varchar (20),Cena_II varchar(20),Cena_III varchar (20),Cena_IV varchar (20),Cena_zakupu varchar (20),Marza varchar(20))";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
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

            #endregion



            Close();
        }

       

        private void Form4_mag_Load(object sender, EventArgs e)
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
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_"+zalogowany+".sqlite");
            con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            try
            {
                con.Open();
               
                sql = "Select * from Magazyny";
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
                dataGridView1.Columns[1].Width = 50;
                dataGridView1.Columns[2].Width = 100;
                
                dataGridView1.Columns.RemoveAt(3);
                DataGridViewComboBoxColumn combo = new DataGridViewComboBoxColumn();
                combo.HeaderText = "Typ";
                combo.Items.Add(Properties.Resources.Towary);
                combo.Items.Add(Properties.Resources.Uslugi);
                combo.Items.Add(Properties.Resources.Towary_Uslugi);
                dataGridView1.Columns.Add(combo);
                sql="Select Typ from magazyny";
                cmd= new SQLiteCommand(sql,con);
                SQLiteDataReader reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    list.Add(reader["Typ"].ToString());
                }
                reader.Close();
                for (int i = 0; i < dataGridView1.RowCount-1; i++)
                {
                    dataGridView1.Rows[i].Cells[3].Value = list[i];
                }
                sql = "Select * from Magazyn";
                cmd = new SQLiteCommand(sql,con);
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    magazyn_domyslny = reader["Symbol"].ToString();
                }
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    if (dataGridView1[1, i].Value.ToString() == magazyn_domyslny)
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
            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            try
            {
                con.Open();
                //sql = "Select Magazyn_domyslny_magazyn from Dane_Firmy_" + zalogowany + "";
                //SQLiteCommand cmd = new SQLiteCommand(sql, con);
                //SQLiteDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    magazyn_domyslny = reader["Magazyn_domyslny_magazyn"].ToString();

                //}
                //reader.Close();
                //sql = "Select * from Magazyn";
                //SQLiteCommand cmd = new SQLiteCommand();
                //SQLiteDataReader reader = cmd.ExecuteReader();
                //while (reader.Read())
                //{
                //    magazyn_domyslny = reader["Symbol"].ToString();
                //}
                //for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                //{
                //    if (dataGridView1[1, i].Value.ToString() == magazyn_domyslny)
                //    {
                       
                //        dataGridView1.CurrentCell = dataGridView1.Rows[i].Cells[0];
                        

                //    }

                //}

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

        private void button2_Click_1(object sender, EventArgs e)
        {
            Close();
        }

        

        

        private void timer1_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells.Count; i++)
            {
                dataGridView1[i, dataGridView1.CurrentRow.Index].Style.BackColor = Color.LightBlue;

            }
            
            timer1.Stop();
        }

        private void dataGridView1_RowEnter_1(object sender, DataGridViewCellEventArgs e)
        {
            timer1.Interval = 2;
            timer1.Start();
            
            
                        
        }

        private void dataGridView1_RowLeave_1(object sender, DataGridViewCellEventArgs e)
        {
            for (int i = 0; i < dataGridView1.Rows[e.RowIndex].Cells.Count; i++)
            {
                dataGridView1[i, e.RowIndex].Style.BackColor = Color.White;

            }
        }

        private void dataGridView1_RowStateChanged(object sender, DataGridViewRowStateChangedEventArgs e)
        {
           
        }

        private void dataGridView1_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            
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
                sql = "Delete from Magazyny";
                SQLiteCommand cmd = new SQLiteCommand(sql, con);
                cmd.ExecuteNonQuery();
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {

                    sql = "Insert into Magazyny (Symbol,Opis,Typ) Values('" + dataGridView1.Rows[i].Cells[1].Value + "','" + dataGridView1.Rows[i].Cells[2].Value + "','" + dataGridView1.Rows[i].Cells[3].Value + "')";
                    cmd = new SQLiteCommand(sql, con);
                    cmd.ExecuteNonQuery();
                }
                sql = "Insert into Magazyn (Symbol, Opis,Typ) values('" + dataGridView1.CurrentRow.Cells[1].Value + "','" + dataGridView1.CurrentRow.Cells[2].Value + "','" + dataGridView1.CurrentRow.Cells[3].Value + "')";
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
            //path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Archiwum.sqlite");
            //con = new SQLiteConnection("Data Source ='" + path + "';Version=3;");
            //try
            //{
            //    con.Open();
            //    sql = "Update Dane_Firmy_" + zalogowany + " set Magazyn_domyslny_magazyn='" + dataGridView1.CurrentRow.Cells[1].Value + "',Magazyn_domyslny_opis='" + dataGridView1.CurrentRow.Cells[2].Value + "',Magazyn_domyslny_typ='" + dataGridView1.CurrentRow.Cells[3].Value + "' where Lp=1";
            //    SQLiteCommand cmd = new SQLiteCommand(sql, con);
            //    cmd.ExecuteNonQuery();

            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.ToString());

            //}
            //finally
            //{
            //    con.Close();
            //}
            #region Zakładanie Magazynów

            path = System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "Archiwum\\Magazyn_" + zalogowany + ".sqlite");
            con = new SQLiteConnection("Data Source='" + path + "';Version=3;");
            try
            {
                con.Open();
                for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                {
                    sql = "Create table if not exists Magazyn_" + dataGridView1.Rows[i].Cells[1].Value + "(Lp Integer primary key,Indeks varchar(20),Symbol_SWW varchar (20),Nazwa varchar(20),Jednostka_miary varchar(20),Typ varchar(20),Stawka_VAT varchar(20),Stan_magazynowy varchar (20),Mimimalny_stan varchar (20),Okres_zbywania varchar (20),Cena_I varchar (20),Cena_II varchar(20),Cena_III varchar (20),Cena_IV varchar (20),Cena_zakupu varchar (20),Marza varchar(20))";
                    SQLiteCommand cmd = new SQLiteCommand(sql, con);
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

            #endregion



            Close();
        }
       
        
    }
}
