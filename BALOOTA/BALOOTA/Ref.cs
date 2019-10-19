using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using System.Data.Sql;
using System.Data.SqlClient;
// chash memory  bedal e3ml run between DB and RAM
using System.Data.Odbc;
using System.Configuration;

namespace BALOOTA
{
    public partial class Ref : Form
    {
        public Ref()
        {
            InitializeComponent();
        }

        int row1 = 0;
        private string src = Program.xsrc;
        bool u1 = false;
        bool u2 = false;
                
        public void Ref_Load(object sender, EventArgs e)
        {
            textBox10.Text = "";
            textBox8.Text = "";
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add( null, null, null);
            }
            row1 = 0;
            dateTimePicker4.Focus();
        }

        private void dataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            e.CellStyle.WrapMode = DataGridViewTriState.True;
            
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox8.Text == "" && textBox10.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "RefS";
                string page = "RefS";
                Program.mysignin.which(ww, page);
            }
        }

        public void RefS()
        {

            if (textBox8.Text == "" && textBox10.Text != "")
            {
                SqlConnection con7 = new SqlConnection(src);
                con7.Open();
                SqlCommand cmd7 = new SqlCommand("select * from Ref", con7);
                SqlDataReader dr = cmd7.ExecuteReader();
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null);
                }
                row1 = 0;
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                    {
                        if (dr["Kind"].ToString() == "DEL")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Red);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "UP")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.ForestGreen);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "AD")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.MediumBlue);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "IN")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.HotPink);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "SL")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Goldenrod);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "DELA")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Turquoise);
                            row1++;
                        }
                    }
                }

            }
            else if (textBox8.Text != "" && textBox10.Text == "")
            {
                SqlConnection con8 = new SqlConnection(src);
                con8.Open();
                SqlCommand cmd8 = new SqlCommand("select * from Ref", con8);
                SqlDataReader dr = cmd8.ExecuteReader();
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null);
                }
                row1 = 0;
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text).AddDays(1))

                    {
                        if (dr["Kind"].ToString() == "DEL")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Red);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "UP")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.ForestGreen);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "AD")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.MediumBlue);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "IN")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.HotPink);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "SL")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Goldenrod);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "DELA")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Turquoise);
                            row1++;
                        }
                    }
                }

            }
            else if (textBox8.Text != "" && textBox10.Text != "")
            {
                SqlConnection con9 = new SqlConnection(src);
                con9.Open();
                SqlCommand cmd9 = new SqlCommand("select * from Ref", con9);
                SqlDataReader dr = cmd9.ExecuteReader();
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null);
                }
                row1 = 0;
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text).AddDays(1))

                    {

                        if (dr["Kind"].ToString() == "DEL")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Red);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "UP")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.ForestGreen);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "AD")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.MediumBlue);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "IN")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.HotPink);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "SL")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Goldenrod);
                            row1++;
                        }
                        else if (dr["Kind"].ToString() == "DELA")
                        {
                            dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["Name"].ToString(), dr["Action"].ToString());
                            dataGridView1.Rows[row1].DefaultCellStyle.ForeColor = (Color.Turquoise);
                            row1++;
                        }

                    }
                }

            }
            this.dataGridView1.Sort(this.dataGridView1.Columns[0], ListSortDirection.Descending);
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker4.Value.ToString("dd/MM/yyyy");
            textBox10.Text = theDate1.ToString();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            string theDate2 = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            textBox8.Text = theDate2.ToString();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox10.Text!="" && textBox8.Text != "")
            {
                /*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            Samer
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "Ref1";
                string page = "Ref1";
                Program.mysignin.which(ww, page);
               // */

               // /*@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@            Ayman
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    string ww = "Ref2";
                    string page = "Ref2";
                    Program.mysignin.which(ww, page);
                   // */

            }
            else
            {
                MessageBox.Show("الرجاء إدخال التاريخ (من, الى) لإتمام عملية الحذف");
            }
        }

        public void Ref1(bool ok)
        {
            if(ok)
            {
                SqlConnection con9 = new SqlConnection(src);
                con9.Open();
                SqlCommand cmd9 = new SqlCommand("select * from Ref", con9);
                SqlDataReader dr = cmd9.ExecuteReader();
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text).AddDays(1))
                    {
                        SqlConnection cn111 = new SqlConnection(src);
                        SqlCommand cmd111 = new SqlCommand("DELETE FROM [Ref] WHERE Id = @Ird", cn111);
                        cmd111.Parameters.AddWithValue("@Ird", dr["Id"].ToString());
                        cn111.Open();
                        SqlDataReader dr111 = cmd111.ExecuteReader();
                    }
                }
                MessageBox.Show("لقد تمت العملية بنجاح");
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null);
                }
            }
        }
        
        public void Ref2(bool ok, string nn, object sender, EventArgs e)
        {
            if(ok)
            {
                /*  // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Ayman
                if(nn=="ايمن عويس")
                {
                    u1 = true;
                }
                if (nn == "سالم عويسات")
                {
                    u2 = true;
                }
                //  */

                //    /*  // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Samer
                if (nn == "سامر عويس")
                {
                    u1 = true;
                }
                if (nn == "المبرمج")
                {
                    u2 = true;
                }
                //   */
                if (u1 && u2)
                {
                    SqlConnection con94 = new SqlConnection(src);
                    con94.Open();
                    SqlCommand cmd94 = new SqlCommand("select * from Ref", con94);
                    SqlDataReader dr4 = cmd94.ExecuteReader();
                    while (dr4.Read())
                    {
                        if (DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr4["Date"].ToString()) <= DateTime.Parse(textBox8.Text).AddDays(1))
                        {
                            SqlConnection cn1114 = new SqlConnection(src);
                            SqlCommand cmd1114 = new SqlCommand("DELETE FROM [Ref] WHERE Id = @Ird", cn1114);
                            cmd1114.Parameters.AddWithValue("@Ird", dr4["Id"].ToString());
                            cn1114.Open();
                            SqlDataReader dr1114 = cmd1114.ExecuteReader();
                        }
                    }
                    MessageBox.Show("لقد تمت العملية بنجاح");
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 25; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null);
                    }
                }
               else
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    string ww = "Ref2";
                    string page = "Ref2";
                    Program.mysignin.which(ww, page);
                }
            }
        }
        
    }
}
