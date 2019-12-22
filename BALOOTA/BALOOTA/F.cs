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
    public partial class F : Form
    {
        public F()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        public bool empty = true;
        public bool empty2 = true;
        public bool empty3 = true;
        public bool empty4 = true;
        public bool empty5 = true;
        int row1 = 0;
        int row2 = 0;
        int row3 = 0;
        int row4 = 0;
        int row5 = 0;

        public void F_Load(object sender, EventArgs e)
        {
            dateTimePicker1.Checked = false;

            SqlConnection con90 = new SqlConnection(src);
            con90.Open();
            SqlCommand cmd90 = new SqlCommand("select * from SaveF", con90);
            SqlDataReader dr0 = cmd90.ExecuteReader();
            empty = true;
            empty2 = true;
            empty3 = true;
            empty4 = true;
            empty5 = true;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            row1 = 0;
            row2 = 0;
            row3 = 0;
            row4 = 0;
            row5 = 0;
            for (int r=0; r<15;r++)
            {
                dataGridView1.Rows.Add(null, null, null, null, null, null);
                dataGridView2.Rows.Add(null, null);
                dataGridView3.Rows.Add(null, null, null);
                dataGridView4.Rows.Add(null, null, null);
                dataGridView5.Rows.Add(null, null);
            }
            while (dr0.Read())
            {
                if (dr0["Marker"].ToString() == "Date")
                {
                    textBox2.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Note")
                {
                    textBox11.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Total1")
                {
                    textBox3.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Total2")
                {
                    textBox4.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Total3")
                {
                    textBox7.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Total4")
                {
                    textBox9.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Total5")
                {
                    textBox8.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Reg")
                {
                    textBox1.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Ayman")
                {
                    textBox5.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "Salem")
                {
                    textBox6.Text = dr0["Name"].ToString();
                }
                else if (dr0["Marker"].ToString() == "D1")
                {
                    dataGridView1.Rows[row1].Cells[0].Value = dr0["Name"].ToString();
                    dataGridView1.Rows[row1].Cells[1].Value = dr0["Price"].ToString();
                    dataGridView1.Rows[row1].Cells[2].Value = dr0["Quan"].ToString();
                    dataGridView1.Rows[row1].Cells[3].Value = dr0["Min"].ToString();
                    dataGridView1.Rows[row1].Cells[4].Value = dr0["FullP"].ToString();
                    dataGridView1.Rows[row1].Cells[5].Value = dr0["Note"].ToString();
                    this.dataGridView1.Rows[row1].HeaderCell.Value = (row1 + 1).ToString();
                    row1++;
                }
                else if (dr0["Marker"].ToString() == "D2")
                {
                    dataGridView2.Rows[row2].Cells[0].Value = dr0["Name"].ToString();
                    dataGridView2.Rows[row2].Cells[1].Value = dr0["Price"].ToString();
                    this.dataGridView2.Rows[row2].HeaderCell.Value = (row2 + 1).ToString();
                    row2++;
                }
                else if (dr0["Marker"].ToString() == "D3")
                {
                    dataGridView3.Rows[row3].Cells[0].Value = dr0["Name"].ToString();
                    dataGridView3.Rows[row3].Cells[1].Value = dr0["Price"].ToString();
                    dataGridView3.Rows[row3].Cells[2].Value = dr0["Note"].ToString();
                    this.dataGridView3.Rows[row3].HeaderCell.Value = (row3 + 1).ToString();
                    row3++;
                }
                else if (dr0["Marker"].ToString() == "D4")
                {
                    dataGridView4.Rows[row4].Cells[0].Value = dr0["Name"].ToString();
                    dataGridView4.Rows[row4].Cells[1].Value = dr0["Price"].ToString();
                    dataGridView4.Rows[row4].Cells[2].Value = dr0["Note"].ToString();
                    this.dataGridView4.Rows[row4].HeaderCell.Value = (row4 + 1).ToString();
                    row4++;
                }
                else if (dr0["Marker"].ToString() == "D5")
                {
                    dataGridView5.Rows[row5].Cells[0].Value = dr0["Name"].ToString();
                    dataGridView5.Rows[row5].Cells[1].Value = dr0["Price"].ToString();
                    this.dataGridView5.Rows[row5].HeaderCell.Value = (row5 + 1).ToString();
                    row5++;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل انت متأكد من حفظ المعلومات ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "FS";
                string page = "FS";
                Program.mysignin.which(ww, page);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="")
            { MessageBox.Show("الرجاء ادخال التاريخ"); }
            else if ((MessageBox.Show("هل انت متأكد من ادخال المعلومات ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "FI";
                string page = "FI";
                Program.mysignin.which(ww, page);
            }

        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل انت متأكد من حذف كل المعلومات ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "FD";
                string page = "FD";
                Program.mysignin.which(ww, page);
            }
        }

        public void Delete()
        {
            SqlConnection conne = new SqlConnection(src);
            conne.Open();
            SqlCommand cmod = new SqlCommand("select max(Id) from SaveF", conne);
            int regid1 = Convert.ToInt32(cmod.ExecuteScalar());

            for (int i = 1; i <= regid1; i++)
            {
                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [SaveF] WHERE Id = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", i);
                cn111.Open();
                SqlDataReader dr111 = cmd111.ExecuteReader();
            }
            textBox2.Text = "";
            textBox11.Text = "";
            textBox1.Text = "0.0";
            textBox3.Text = "0.0";
            textBox4.Text = "0.0";
            textBox5.Text = "0.0";
            textBox6.Text = "0.0";
            textBox7.Text = "0.0";
            textBox8.Text = "0.0";
            textBox9.Text = "0.0";
            empty = true;
            empty2 = true;
            empty3 = true;
            empty4 = true;
            empty5 = true;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            dataGridView3.Rows.Clear();
            dataGridView4.Rows.Clear();
            dataGridView5.Rows.Clear();
            for (int r = 0; r < 15; r++)
            {
                dataGridView1.Rows.Add(null, null, null, null, null, null);
                dataGridView2.Rows.Add(null, null);
                dataGridView3.Rows.Add(null, null, null);
                dataGridView4.Rows.Add(null, null, null);
                dataGridView5.Rows.Add(null, null);
            }
            dateTimePicker1.Checked = false;

        }

        public void Sav()
        {
            Delete();
            for (int q = 1; q <= 10; q++)
            {
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [SaveF](Marker,Name)VALUES (@textBox1,@textBox2)", con);
                if(q==1)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Date");
                    cmd.Parameters.AddWithValue("@textBox2", textBox2.Text);
                }
                else if (q == 2)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Note");
                    cmd.Parameters.AddWithValue("@textBox2", textBox11.Text);
                }
                else if (q == 3)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Total1");
                    cmd.Parameters.AddWithValue("@textBox2", textBox3.Text);
                }
                else if (q == 4)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Total2");
                    cmd.Parameters.AddWithValue("@textBox2", textBox4.Text);
                }
                else if (q == 5)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Reg");
                    cmd.Parameters.AddWithValue("@textBox2", textBox1.Text);
                }
                else if (q == 6)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Ayman");
                    cmd.Parameters.AddWithValue("@textBox2", textBox5.Text);
                }
                else if (q == 7)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Salem");
                    cmd.Parameters.AddWithValue("@textBox2", textBox6.Text);
                }
                else if (q == 8)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Total3");
                    cmd.Parameters.AddWithValue("@textBox2", textBox7.Text);
                }
                else if (q == 9)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Total4");
                    cmd.Parameters.AddWithValue("@textBox2", textBox9.Text);
                }
                else if (q == 10)
                {
                    cmd.Parameters.AddWithValue("@textBox1", "Total5");
                    cmd.Parameters.AddWithValue("@textBox2", textBox8.Text);
                }

                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();
            }
            for(int d1=0;d1<dataGridView1.RowCount-1;d1++)
            {
                if (dataGridView1.Rows[d1].Cells[0].Value != null && dataGridView1.Rows[d1].Cells[1].Value != null && dataGridView1.Rows[d1].Cells[2].Value != null && dataGridView1.Rows[d1].Cells[3].Value != null && dataGridView1.Rows[d1].Cells[4].Value != null)
                {
                    SqlConnection con1 = new SqlConnection(src);
                    SqlCommand cmd1 = new SqlCommand("INSERT INTO [SaveF](Marker,Name,Price,Quan,Min,FullP,Note)VALUES (@textBox1,@textBox2,@textBox3,@textBox4,@textBox5,@textBox6,@textBox7)", con1);

                    cmd1.Parameters.AddWithValue("@textBox1", "D1");
                    cmd1.Parameters.AddWithValue("@textBox2", dataGridView1.Rows[d1].Cells[0].Value.ToString());
                    cmd1.Parameters.AddWithValue("@textBox3", dataGridView1.Rows[d1].Cells[1].Value.ToString());
                    cmd1.Parameters.AddWithValue("@textBox4", dataGridView1.Rows[d1].Cells[2].Value.ToString());
                    cmd1.Parameters.AddWithValue("@textBox5", dataGridView1.Rows[d1].Cells[3].Value.ToString());
                    cmd1.Parameters.AddWithValue("@textBox6", dataGridView1.Rows[d1].Cells[4].Value.ToString());
                    if (dataGridView1.Rows[d1].Cells[5].Value != null)
                    {
                        cmd1.Parameters.AddWithValue("@textBox7", dataGridView1.Rows[d1].Cells[5].Value.ToString());
                    }
                    else
                    {
                        cmd1.Parameters.AddWithValue("@textBox7", "لا يوجد");
                    }
                    con1.Open();
                    SqlDataReader dr21 = cmd1.ExecuteReader();
                }
            }
            for (int d2 = 0; d2 < dataGridView2.RowCount - 1; d2++)
            {
                if (dataGridView2.Rows[d2].Cells[0].Value != null && dataGridView2.Rows[d2].Cells[1].Value != null)
                {
                    SqlConnection con2 = new SqlConnection(src);
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [SaveF](Marker,Name,Price)VALUES (@textBox1,@textBox2,@textBox3)", con2);

                    cmd2.Parameters.AddWithValue("@textBox1", "D2");
                    cmd2.Parameters.AddWithValue("@textBox2", dataGridView2.Rows[d2].Cells[0].Value.ToString());
                    cmd2.Parameters.AddWithValue("@textBox3", dataGridView2.Rows[d2].Cells[1].Value.ToString());
                    con2.Open();
                    SqlDataReader dr22 = cmd2.ExecuteReader();
                }
            }
            for (int d3 = 0; d3 < dataGridView3.RowCount - 1; d3++)
            {
                if (dataGridView3.Rows[d3].Cells[0].Value != null && dataGridView3.Rows[d3].Cells[1].Value != null)
                {
                    SqlConnection con3 = new SqlConnection(src);
                    SqlCommand cmd3 = new SqlCommand("INSERT INTO [SaveF](Marker,Name,Price,Note)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con3);

                    cmd3.Parameters.AddWithValue("@textBox1", "D3");
                    cmd3.Parameters.AddWithValue("@textBox2", dataGridView3.Rows[d3].Cells[0].Value.ToString());
                    cmd3.Parameters.AddWithValue("@textBox3", dataGridView3.Rows[d3].Cells[1].Value.ToString());
                        if (dataGridView3.Rows[d3].Cells[2].Value != null)
                        {
                            cmd3.Parameters.AddWithValue("@textBox4", dataGridView3.Rows[d3].Cells[2].Value.ToString());
                    }
                    else
                    {
                        cmd3.Parameters.AddWithValue("@textBox4", "لا يوجد");
                    }
                    con3.Open();
                    SqlDataReader dr23 = cmd3.ExecuteReader();
                }
            }
            for (int d4 = 0; d4 < dataGridView4.RowCount - 1; d4++)
            {
                if (dataGridView4.Rows[d4].Cells[0].Value != null && dataGridView4.Rows[d4].Cells[1].Value != null)
                {
                    SqlConnection con34 = new SqlConnection(src);
                    SqlCommand cmd34 = new SqlCommand("INSERT INTO [SaveF](Marker,Name,Price,Note)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con34);

                    cmd34.Parameters.AddWithValue("@textBox1", "D4");
                    cmd34.Parameters.AddWithValue("@textBox2", dataGridView4.Rows[d4].Cells[0].Value.ToString());
                    cmd34.Parameters.AddWithValue("@textBox3", dataGridView4.Rows[d4].Cells[1].Value.ToString());
                    if (dataGridView4.Rows[d4].Cells[2].Value != null)
                    {
                        cmd34.Parameters.AddWithValue("@textBox4", dataGridView4.Rows[d4].Cells[2].Value.ToString());
                    }
                    else
                    {
                        cmd34.Parameters.AddWithValue("@textBox4", "لا يوجد");
                    }
                    con34.Open();
                    SqlDataReader dr234 = cmd34.ExecuteReader();
                }
            }
            for (int d5 = 0; d5 < dataGridView5.RowCount - 1; d5++)
            {
                if (dataGridView5.Rows[d5].Cells[0].Value != null && dataGridView5.Rows[d5].Cells[1].Value != null)
                {
                    SqlConnection con35 = new SqlConnection(src);
                    SqlCommand cmd35 = new SqlCommand("INSERT INTO [SaveF](Marker,Name,Price)VALUES (@textBox1,@textBox2,@textBox3)", con35);

                    cmd35.Parameters.AddWithValue("@textBox1", "D5");
                    cmd35.Parameters.AddWithValue("@textBox2", dataGridView5.Rows[d5].Cells[0].Value.ToString());
                    cmd35.Parameters.AddWithValue("@textBox3", dataGridView5.Rows[d5].Cells[1].Value.ToString());
                    con35.Open();
                    SqlDataReader dr235 = cmd35.ExecuteReader();
                }
            }
            MessageBox.Show("لقد تم الحفظ بنجاح");
        }

        public void In()
        {
            SqlConnection con = new SqlConnection(src);
            SqlCommand cmd = new SqlCommand("INSERT INTO [Purchases](CompanyName,InvoiceNo,Date,Amount,Debt,Paid,RDebt,Notes)VALUES (@comboBox1,@textBox1,@textBox2,@textBox3,@text,@textBox5,@textBox4,@textBox11)", con);
            cmd.Parameters.AddWithValue("@textBox1", "0");
            cmd.Parameters.AddWithValue("@comboBox1", "راس المال");
            cmd.Parameters.AddWithValue("@textBox5", textBox3.Text);
            cmd.Parameters.AddWithValue("@textBox4", "0");
            cmd.Parameters.AddWithValue("@textBox2", textBox2.Text);
            cmd.Parameters.AddWithValue("@textBox3", textBox3.Text);
            cmd.Parameters.AddWithValue("@text", "لا");
            cmd.Parameters.AddWithValue("@textBox11", textBox11.Text);
            con.Open();
            SqlDataReader dr2 = cmd.ExecuteReader();

            SqlConnection conne4 = new SqlConnection(src);
            conne4.Open();
            SqlCommand cmod4 = new SqlCommand("select max(Id) from Purchases", conne4);
            int vid = Convert.ToInt32(cmod4.ExecuteScalar());

                        for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                        {
                            if (dataGridView1.Rows[j].Cells[0].Value != null && dataGridView1.Rows[j].Cells[1].Value != null && dataGridView1.Rows[j].Cells[2].Value != null && dataGridView1.Rows[j].Cells[3].Value != null && dataGridView1.Rows[j].Cells[4].Value != null)
                            {
                                string srcc = Program.xsrc;
                                SqlConnection con66q = new SqlConnection(srcc);
                                SqlCommand cmd66q = new SqlCommand("INSERT INTO [Items](IdPurchase,ItemName,CompanyName,InvoiceNo,Date,Price,Quantity,MinQuantity,FullPrice,RQuantity,Notes)VALUES (@invid,@data0,@comboBox1,@textBox1,@textBox2,@data1,@data2,@data3,@data4,@data5,@N)", con66q);
                                cmd66q.Parameters.AddWithValue("@invid", vid);
                                cmd66q.Parameters.AddWithValue("@data0", dataGridView1.Rows[j].Cells[0].Value);
                                cmd66q.Parameters.AddWithValue("@comboBox1", "راس المال");
                                cmd66q.Parameters.AddWithValue("@textBox1", "0");
                                cmd66q.Parameters.AddWithValue("@textBox2", textBox2.Text);
                                cmd66q.Parameters.AddWithValue("@data1", dataGridView1.Rows[j].Cells[1].Value);
                                cmd66q.Parameters.AddWithValue("@data2", dataGridView1.Rows[j].Cells[2].Value);
                                cmd66q.Parameters.AddWithValue("@data3", dataGridView1.Rows[j].Cells[3].Value);
                                cmd66q.Parameters.AddWithValue("@data4", dataGridView1.Rows[j].Cells[4].Value);
                                cmd66q.Parameters.AddWithValue("@data5", dataGridView1.Rows[j].Cells[2].Value);
                                if (dataGridView1.Rows[j].Cells[5].Value != null)
                                {
                                    cmd66q.Parameters.AddWithValue("@N", dataGridView1.Rows[j].Cells[5].Value);

                                }
                                else
                                {
                                    cmd66q.Parameters.AddWithValue("@N", "لا يوجد");
                                }
                                con66q.Open();
                                SqlDataReader dr66q = cmd66q.ExecuteReader();
                                con66q.Close();


                                SqlConnection con667 = new SqlConnection(src);
                                SqlCommand cmd667 = new SqlCommand("INSERT INTO [Inventory](Item,Quantity,MinQ,Notes)VALUES (@data0,@data2,@data3,@N)", con667);
                                cmd667.Parameters.AddWithValue("@data0", dataGridView1.Rows[j].Cells[0].Value);
                                cmd667.Parameters.AddWithValue("@data2", dataGridView1.Rows[j].Cells[2].Value);
                                cmd667.Parameters.AddWithValue("@data3", dataGridView1.Rows[j].Cells[3].Value);
                                if (dataGridView1.Rows[j].Cells[5].Value != null)
                                {
                                    cmd667.Parameters.AddWithValue("@N", dataGridView1.Rows[j].Cells[5].Value);
                                }
                                else
                                {
                                    cmd667.Parameters.AddWithValue("@N", "لا يوجد");
                                }
                                con667.Open();
                                SqlDataReader dr667 = cmd667.ExecuteReader();
                                con667.Close();

                            }
                        }
                        for(int s=0; s<dataGridView2.RowCount-1; s++)
                        {
                            if (dataGridView2.Rows[s].Cells[0].Value != null && dataGridView2.Rows[s].Cells[1].Value != null)
                            {
                                SqlConnection con55 = new SqlConnection(src);
                                SqlCommand cmd55 = new SqlCommand("INSERT INTO [StoreDebt](Date,Name,InvNo,Amount,idPurchase)VALUES (@textBox1,@textBox2,@textBox3,@textBox4,@text)", con55);
                                cmd55.Parameters.AddWithValue("@textBox1", textBox2.Text);
                                cmd55.Parameters.AddWithValue("@textBox2", dataGridView2.Rows[s].Cells[0].Value.ToString());
                                cmd55.Parameters.AddWithValue("@textBox3", "0");
                                cmd55.Parameters.AddWithValue("@textBox4", dataGridView2.Rows[s].Cells[1].Value.ToString());
                                cmd55.Parameters.AddWithValue("@text", -1);
                                con55.Open();
                                SqlDataReader dr155 = cmd55.ExecuteReader();
                            }
                        }
                        for(int z=0; z<dataGridView3.RowCount-1; z++)
                        {
                            if (dataGridView3.Rows[z].Cells[0].Value != null && dataGridView3.Rows[z].Cells[1].Value != null)
                            {
                                SqlConnection con55z = new SqlConnection(src);
                                SqlCommand cmd55z = new SqlCommand("INSERT INTO [SDebt](Date,Name,Amount,Notes)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con55z);
                                cmd55z.Parameters.AddWithValue("@textBox1", textBox2.Text);
                                cmd55z.Parameters.AddWithValue("@textBox2", dataGridView3.Rows[z].Cells[0].Value.ToString());
                                cmd55z.Parameters.AddWithValue("@textBox3", dataGridView3.Rows[z].Cells[1].Value.ToString());
                                if (dataGridView3.Rows[z].Cells[2].Value != null)
                                {
                                    cmd55z.Parameters.AddWithValue("@textBox4", dataGridView3.Rows[z].Cells[2].Value.ToString());

                                }
                                else
                                {
                                    cmd55z.Parameters.AddWithValue("@textBox4", "لا يوجد");
                                }
                                con55z.Open();
                                SqlDataReader dr155z = cmd55z.ExecuteReader();
                            }
                        }
            for (int z = 0; z < dataGridView4.RowCount - 1; z++)
            {
                if (dataGridView4.Rows[z].Cells[0].Value != null && dataGridView4.Rows[z].Cells[1].Value != null)
                {
                    SqlConnection con55z4 = new SqlConnection(src);
                    SqlCommand cmd55z4 = new SqlCommand("INSERT INTO [EmpDebt](Date,Name,Amount,Notes)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con55z4);
                    cmd55z4.Parameters.AddWithValue("@textBox1", textBox2.Text);
                    cmd55z4.Parameters.AddWithValue("@textBox2", dataGridView4.Rows[z].Cells[0].Value.ToString());
                    cmd55z4.Parameters.AddWithValue("@textBox3", dataGridView4.Rows[z].Cells[1].Value.ToString());
                    if (dataGridView4.Rows[z].Cells[2].Value != null)
                    {
                        cmd55z4.Parameters.AddWithValue("@textBox4", dataGridView4.Rows[z].Cells[2].Value.ToString());

                    }
                    else
                    {
                        cmd55z4.Parameters.AddWithValue("@textBox4", "لا يوجد");
                    }
                    con55z4.Open();
                    SqlDataReader dr155z4 = cmd55z4.ExecuteReader();
                }
            }
            for (int z = 0; z < dataGridView5.RowCount - 1; z++)
            {
                if (dataGridView5.Rows[z].Cells[0].Value != null && dataGridView5.Rows[z].Cells[1].Value != null)
                {
                    SqlConnection con55z45 = new SqlConnection(src);
                    SqlCommand cmd55z45 = new SqlCommand("INSERT INTO [SaleDebt](Date,Name,Amount,idSales)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con55z45);
                    cmd55z45.Parameters.AddWithValue("@textBox1", textBox2.Text);
                    cmd55z45.Parameters.AddWithValue("@textBox2", dataGridView5.Rows[z].Cells[0].Value.ToString());
                    cmd55z45.Parameters.AddWithValue("@textBox3", dataGridView5.Rows[z].Cells[1].Value.ToString());
                        cmd55z45.Parameters.AddWithValue("@textBox4", -1);
                    con55z45.Open();
                    SqlDataReader dr155z45 = cmd55z45.ExecuteReader();
                }
            }

            SqlConnection con555 = new SqlConnection(src);
                        SqlCommand cmd555 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con555);
                        cmd555.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                        cmd555.Parameters.AddWithValue("@textBox2", textBox1.Text);
                        con555.Open();
                        SqlDataReader dr1555 = cmd555.ExecuteReader();


                        SqlConnection con555q = new SqlConnection(src);
                        SqlCommand cmd555q = new SqlCommand("INSERT INTO [Balance](Name,Amount)VALUES (@textBox1,@textBox2)", con555q);
                        cmd555q.Parameters.AddWithValue("@textBox1", "ايمن عويس");
                        cmd555q.Parameters.AddWithValue("@textBox2", textBox5.Text);
                        con555q.Open();
                        SqlDataReader dr1555q = cmd555q.ExecuteReader();
                        SqlConnection con555q2 = new SqlConnection(src);
                        SqlCommand cmd555q2 = new SqlCommand("INSERT INTO [Balance](Name,Amount)VALUES (@textBox1,@textBox2)", con555q2);
                        cmd555q2.Parameters.AddWithValue("@textBox1", "سالم عويسات");
                        cmd555q2.Parameters.AddWithValue("@textBox2", textBox6.Text);
                        con555q2.Open();
                        SqlDataReader dr1555q2 = cmd555q2.ExecuteReader();

                        SqlConnection conn7 = new SqlConnection(src);
                        SqlCommand cmdn7 = new SqlCommand("UPDATE [F] SET  Name = @box2 WHERE Id = '" + 1 + "'", conn7);
                        cmdn7.Parameters.AddWithValue("@box2", "YES");
                        conn7.Open();
                        SqlDataReader dr72 = cmdn7.ExecuteReader();
                        conn7.Close();

                        MessageBox.Show("لقد تم حفظ المعلومات بنجاح"+Environment.NewLine+ Environment.NewLine +"الان سوف يتم بدء البرنامج" + Environment.NewLine +"مع احر الأمنيات للجميع بالتوفيق");
                        Delete();
                        this.Hide();
                        
        }

        private void F_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل انت متأكد من الخروج ؟"+Environment.NewLine+ Environment.NewLine + "الرجاء حفظ المعلومات قبل الخروج", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                this.Hide();
                textBox2.Text = "";
                textBox11.Text = "";
                textBox1.Text = "0.0";
                textBox3.Text = "0.0";
                textBox4.Text = "0.0";
                textBox5.Text = "0.0";
                textBox6.Text = "0.0";
                textBox7.Text = "0.0";
                textBox8.Text = "0.0";
                textBox9.Text = "0.0";
                empty = true;
                empty2 = true;
                empty3 = true;
                empty4 = true;
                empty5 = true;
                dataGridView1.Rows.Clear();
                dataGridView2.Rows.Clear();
                dataGridView3.Rows.Clear();
                dataGridView4.Rows.Clear();
                dataGridView5.Rows.Clear();
                for (int r = 0; r < 15; r++)
                {
                    dataGridView1.Rows.Add(null, null, null, null, null, null);
                    dataGridView2.Rows.Add(null, null);
                    dataGridView3.Rows.Add(null, null, null);
                    dataGridView4.Rows.Add(null, null, null);
                    dataGridView5.Rows.Add(null, null);
                }
                dateTimePicker1.Checked = false;

            }
        }
        
        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {

            int z = 0;
            try
            {
                if (!empty)
                {

                    if (dataGridView1.Rows.Count > 1)
                    {
                        textBox3.Text = "0.0";
                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {

                            if ((dataGridView1.CurrentRow.Index >= 0 && dataGridView1.Rows[i].Cells[1].Value != null && dataGridView1.Rows[i].Cells[2].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                                float b = float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                                float c = a * b;
                                dataGridView1.Rows[i].Cells[4].Value = c;

                                textBox3.Text = (float.Parse(textBox3.Text) + float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())).ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView1.Rows[z].Cells[1].Value = 0.0;
                dataGridView1.Rows[z].Cells[2].Value = 0.0;
                dataGridView1.Rows[z].Cells[4].Value = 0.0;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCellAddress.X == 0 && dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                this.dataGridView1.CurrentRow.HeaderCell.Value = (dataGridView1.CurrentCellAddress.Y + 1).ToString();
                empty = false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل انت متأكد من حذف كل المعلومات ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "FD";
                string page = "FD";
                Program.mysignin.which(ww, page);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل انت متأكد من حفظ المعلومات ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "FS";
                string page = "FS";
                Program.mysignin.which(ww, page);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == "")
            { MessageBox.Show("الرجاء ادخال التاريخ"); }
            else
              if ((MessageBox.Show("هل انت متأكد من ادخال المعلومات ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "FI";
                string page = "FI";
                Program.mysignin.which(ww, page);
            }
        }

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentCellAddress.X == 0 && dataGridView2.CurrentRow.Cells[0].Value != null)
            {
                this.dataGridView2.CurrentRow.HeaderCell.Value = (dataGridView2.CurrentCellAddress.Y + 1).ToString();
                empty2 = false;
            }

        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {

            int z = 0;
            try
            {
                if (!empty2)
                {

                    if (dataGridView2.Rows.Count > 1)
                    {
                        textBox4.Text = "0.0";
                        for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                        {

                            if ((dataGridView2.CurrentRow.Index >= 0 && dataGridView2.Rows[i].Cells[0].Value != null && dataGridView2.Rows[i].Cells[1].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());

                                textBox4.Text = (float.Parse(textBox4.Text) + float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString())).ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView2.Rows[z].Cells[1].Value = 0.0;
            }
        }

        private void dataGridView3_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView3.CurrentCellAddress.X == 0 && dataGridView3.CurrentRow.Cells[0].Value != null)
            {
                this.dataGridView3.CurrentRow.HeaderCell.Value = (dataGridView3.CurrentCellAddress.Y + 1).ToString();
                empty3 = false;
            }
        }

        private void dataGridView3_CurrentCellChanged(object sender, EventArgs e)
        {
            int z = 0;
            try
            {
                if (!empty3)
                {

                    if (dataGridView3.Rows.Count > 1)
                    {
                        textBox7.Text = "0.0";
                        for (int i = 0; i < dataGridView3.RowCount - 1; i++)
                        {

                            if ((dataGridView3.CurrentRow.Index >= 0 && dataGridView3.Rows[i].Cells[0].Value != null && dataGridView3.Rows[i].Cells[1].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString());

                                textBox7.Text = (float.Parse(textBox7.Text) + float.Parse(dataGridView3.Rows[i].Cells[1].Value.ToString())).ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView3.Rows[z].Cells[1].Value = 0.0;
            }

        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox2.Text = theDate1.ToString();
        }

        private void dataGridView4_CurrentCellChanged(object sender, EventArgs e)
        {

            int z = 0;
            try
            {
                if (!empty4)
                {

                    if (dataGridView4.Rows.Count > 1)
                    {
                        textBox9.Text = "0.0";
                        for (int i = 0; i < dataGridView4.RowCount - 1; i++)
                        {

                            if ((dataGridView4.CurrentRow.Index >= 0 && dataGridView4.Rows[i].Cells[0].Value != null && dataGridView4.Rows[i].Cells[1].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView4.Rows[i].Cells[1].Value.ToString());

                                textBox9.Text = (float.Parse(textBox9.Text) + float.Parse(dataGridView4.Rows[i].Cells[1].Value.ToString())).ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView4.Rows[z].Cells[1].Value = 0.0;
            }
        }

        private void dataGridView4_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4.CurrentCellAddress.X == 0 && dataGridView4.CurrentRow.Cells[0].Value != null)
            {
                this.dataGridView4.CurrentRow.HeaderCell.Value = (dataGridView4.CurrentCellAddress.Y + 1).ToString();
                empty4 = false;
            }

        }

        private void dataGridView5_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView5.CurrentCellAddress.X == 0 && dataGridView5.CurrentRow.Cells[0].Value != null)
            {
                this.dataGridView5.CurrentRow.HeaderCell.Value = (dataGridView5.CurrentCellAddress.Y + 1).ToString();
                empty5 = false;
            }

        }

        private void dataGridView5_CurrentCellChanged(object sender, EventArgs e)
        {

            int z = 0;
            try
            {
                if (!empty5)
                {

                    if (dataGridView5.Rows.Count > 1)
                    {
                        textBox8.Text = "0.0";
                        for (int i = 0; i < dataGridView5.RowCount - 1; i++)
                        {

                            if ((dataGridView5.CurrentRow.Index >= 0 && dataGridView5.Rows[i].Cells[0].Value != null && dataGridView5.Rows[i].Cells[1].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView5.Rows[i].Cells[1].Value.ToString());

                                textBox8.Text = (float.Parse(textBox8.Text) + float.Parse(dataGridView5.Rows[i].Cells[1].Value.ToString())).ToString();
                            }
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView5.Rows[z].Cells[1].Value = 0.0;
            }
        }
    }
}
