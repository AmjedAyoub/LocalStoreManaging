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

namespace BALOOTA
{
    public partial class Viewsale : Form
    {
        public Viewsale()
        {
            InitializeComponent();
        }
        int row11 = 0;
        int row1 = 0;
        public int rowindex1;
        public int rowindex2;
        private bool select = false;
        string id = "";
        string n = "";
        string idpur = "";
        string paid = "";
        ComboBox cb;
        public string name = "";
        public string rdept = "";
        public string rpaid = "";
        public int regid = 0;
        public bool empty = true;
        public string[] itemarr = new string[1000000];
        private string src = Program.xsrc;
        string[] items;
        string[] items1;
        string[] items2;
        string[] items3;
        
        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "" && comboBox2.Text == "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
            { MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا"); }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "SHS1";
                string page = "SHS1";
                Program.mysignin.which(ww, page);
            }
        }

        public void SHS1()
        {
            textBox6.Text = "";
            textBox7.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox5.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox5.Text = "";
            panel6.Visible = false;
            empty = true;
            select = false;
            dataGridView2.Rows.Clear();
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
            }
            row1 = 0;
            row11 = 0;
            try
            {
                if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Sales", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr.Read())
                    {
                        if (dr["CompanyName"].ToString() == comboBox3.Text && dr["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr["Id"].ToString(), dr["Date"].ToString(), dr["CompanyName"].ToString(), dr["Amount"].ToString(), dr["Debt"].ToString(), dr["Paid"].ToString(), dr["RDebt"].ToString(), dr["Profit"].ToString(), dr["Dis"].ToString(), dr["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text)

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text == "" && textBox9.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con3 = new SqlConnection(src);
                    con3.Open();
                    SqlCommand cmd3 = new SqlCommand("select * from Sales", con3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr3.Read())
                    {
                        if (dr3["CompanyName"].ToString() == comboBox3.Text && dr3["Id"].ToString() == textBox9.Text && dr3["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr3["Id"].ToString(), dr3["Date"].ToString(), dr3["CompanyName"].ToString(), dr3["Amount"].ToString(), dr3["Debt"].ToString(), dr3["Paid"].ToString(), dr3["RDebt"].ToString(), dr3["Profit"].ToString(), dr3["Dis"].ToString(), dr3["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con4 = new SqlConnection(src);
                    con4.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from Sales", con4);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr4.Read())
                    {
                        if (dr4["CompanyName"].ToString() == comboBox3.Text && DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && dr4["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr4["Id"].ToString(), dr4["Date"].ToString(), dr4["CompanyName"].ToString(), dr4["Amount"].ToString(), dr4["Debt"].ToString(), dr4["Paid"].ToString(), dr4["RDebt"].ToString(), dr4["Profit"].ToString(), dr4["Dis"].ToString(), dr4["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con5 = new SqlConnection(src);
                    con5.Open();
                    SqlCommand cmd5 = new SqlCommand("select * from Sales", con5);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr5.Read())
                    {
                        if (dr5["CompanyName"].ToString() == comboBox3.Text && DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && dr5["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr5["Id"].ToString(), dr5["Date"].ToString(), dr5["CompanyName"].ToString(), dr5["Amount"].ToString(), dr5["Debt"].ToString(), dr5["Paid"].ToString(), dr5["RDebt"].ToString(), dr5["Profit"].ToString(), dr5["Dis"].ToString(), dr5["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con6 = new SqlConnection(src);
                    con6.Open();
                    SqlCommand cmd6 = new SqlCommand("select * from Sales", con6);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr6.Read())
                    {
                        if (dr6["CompanyName"].ToString() == comboBox3.Text && DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && dr6["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr6["Id"].ToString(), dr6["Date"].ToString(), dr6["CompanyName"].ToString(), dr6["Amount"].ToString(), dr6["Debt"].ToString(), dr6["Paid"].ToString(), dr6["RDebt"].ToString(), dr6["Profit"].ToString(), dr6["Dis"].ToString(), dr6["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con7 = new SqlConnection(src);
                    con7.Open();
                    SqlCommand cmd7 = new SqlCommand("select * from Sales", con7);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr7.Read())
                    {
                        if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && dr7["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr7["Id"].ToString(), dr7["Date"].ToString(), dr7["CompanyName"].ToString(), dr7["Amount"].ToString(), dr7["Debt"].ToString(), dr7["Paid"].ToString(), dr7["RDebt"].ToString(), dr7["Profit"].ToString(), dr7["Dis"].ToString(), dr7["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con8 = new SqlConnection(src);
                    con8.Open();
                    SqlCommand cmd8 = new SqlCommand("select * from Sales", con8);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr8.Read())
                    {
                        if (DateTime.Parse(dr8["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && dr8["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr8["Id"].ToString(), dr8["Date"].ToString(), dr8["CompanyName"].ToString(), dr8["Amount"].ToString(), dr8["Debt"].ToString(), dr8["Paid"].ToString(), dr8["RDebt"].ToString(), dr8["Profit"].ToString(), dr8["Dis"].ToString(), dr8["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con9 = new SqlConnection(src);
                    con9.Open();
                    SqlCommand cmd9 = new SqlCommand("select * from Sales", con9);
                    SqlDataReader dr9 = cmd9.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr9.Read())
                    {
                        if (DateTime.Parse(dr9["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr9["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && dr9["Date"].ToString() != "01/01/1001")

                        {
                            dataGridView1.Rows.Insert(row11, false, dr9["Id"].ToString(), dr9["Date"].ToString(), dr9["CompanyName"].ToString(), dr9["Amount"].ToString(), dr9["Debt"].ToString(), dr9["Paid"].ToString(), dr9["RDebt"].ToString(), dr9["Profit"].ToString(), dr9["Dis"].ToString(), dr9["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from Sales", con10);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    row11 = 0;
                    while (dr10.Read())
                    {
                        if (dr10["Date"].ToString() != "01/01/1001")
                        {
                            dataGridView1.Rows.Insert(row11, false, dr10["Id"].ToString(), dr10["Date"].ToString(), dr10["CompanyName"].ToString(), dr10["Amount"].ToString(), dr10["Debt"].ToString(), dr10["Paid"].ToString(), dr10["RDebt"].ToString(), dr10["Profit"].ToString(), dr10["Dis"].ToString(), dr10["Notes"].ToString());
                            row11++;
                        }

                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && dr1["IdSale"].ToString() == textBox9.Text)

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Slaes", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sale", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && dr1["IdSale"].ToString() == textBox9.Text)

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text == "" && textBox9.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Sales", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Id"].ToString() == textBox9.Text && dr1["Date"].ToString() != "01/01/1001")

                        {

                            dataGridView1.Rows.Insert(row11, false, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["CompanyName"].ToString(), dr1["Amount"].ToString(), dr1["Debt"].ToString(), dr1["Paid"].ToString(), dr1["RDebt"].ToString(), dr1["Profit"].ToString(), dr1["Dis"].ToString(), dr1["Notes"].ToString());
                            row11++;
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text)

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Sales", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdSale"].ToString() && dr2["Date"].ToString() != "01/01/1001")

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Descending);
                for (int y = 0; y < row11; y++)
                {
                    this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }
                if (row11 > 0)
                {
                    float sum1 = 0; float sum2 = 0;
                    for (int k = 0; k <= row11 - 1; k++)
                    {
                        sum1 = sum1 + float.Parse(dataGridView1.Rows[k].Cells[4].Value.ToString());
                        if (dataGridView1.Rows[k].Cells[8].Value != null && dataGridView1.Rows[k].Cells[8].Value.ToString() != "")
                        { sum2 = sum2 + float.Parse(dataGridView1.Rows[k].Cells[8].Value.ToString()); }
                    }
                    textBox4.Text = sum1.ToString();
                    textBox16.Text = sum2.ToString();
                }
            }
            catch { MessageBox.Show("الرجاء التاكد من معلومات البحث"); }
        }

        public void Viewsale_Load(object sender, EventArgs e)
        {
            textBox6.Text = "";
            textBox18.Text = "";
             textBox7.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox5.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            panel6.Visible = false;
            if (comboBox3.Items.Count > 1)
            {
                comboBox3.Items.Clear();
                comboBox4.Items.Clear();
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox4.Text = "";
            comboBox4.SelectedText = "";
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            comboBox3.SelectedText = "";
            dateTimePicker1.Checked = false;
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            dateTimePicker2.Checked = false;
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from Sales", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            comboBox2.Items.Add("");
            comboBox4.Items.Add("");
            comboBox4.Items.Add("الكل");
            comboBox1.Items.Add("");
            select = false;
            empty = true;
            dataGridView2.Rows.Clear();

            dataGridView1.Rows.Clear();
            while (dr39.Read())
            {
                iitem = dr39["CompanyName"].ToString();

                if (!comboBox3.Items.Contains(iitem))
                {
                    comboBox3.Items.Add(iitem);
                    comboBox4.Items.Add(iitem);
                }
            }
            dr39.Close();

            items3 = new string[comboBox3.Items.Count];
            comboBox3.Items.CopyTo(items3, 0);
            items = new string[comboBox4.Items.Count];
            comboBox4.Items.CopyTo(items, 0);

            if (comboBox2.Items.Count > 1)
            {
                comboBox2.Items.Clear();
                comboBox1.Items.Clear();
            }
            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Inventory", conn3);
            string item = "";
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                item = dr3["Item"].ToString();

                if (!comboBox2.Items.Contains(item))
                {
                    comboBox2.Items.Add(item);
                    comboBox1.Items.Add(item);
                }
            }
            dr3.Close();

            items2 = new string[comboBox2.Items.Count];
            comboBox2.Items.CopyTo(items2, 0);
            items1 = new string[comboBox1.Items.Count];
            comboBox1.Items.CopyTo(items1, 0);
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
            }
            comboBox3.Focus();
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCellAddress.X == 0 && dataGridView1.CurrentRow.Cells[1].Value != null)
            {
                if (row11 > 0 && dataGridView1.CurrentCell.Value.ToString() != "True")
                {
                    select = true;
                    rowindex1 = dataGridView1.CurrentCell.RowIndex;
                    dataGridView1.Rows[rowindex1].Cells[0].Value = true;
                    for (int b = 0; b < row11; b++)
                    {
                        if (b != rowindex1)
                        { dataGridView1.Rows[b].Cells[0].Value = false; }
                    }
                }
                else
                { select = false; }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (select)
            {
                panel6.Visible = true;
                panel6.Show();
                SqlConnection conn3 = new SqlConnection(src);
                conn3.Open();
                n = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                SqlCommand cmd3 = new SqlCommand("select * from Sales WHERE Id ='" + dataGridView1.Rows[rowindex1].Cells[1].Value.ToString() + "'", conn3);
                idpur = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                paid = dataGridView1.Rows[rowindex1].Cells[6].Value.ToString();
                SqlDataReader dr8 = cmd3.ExecuteReader();
                while (dr8.Read())
                {
                    id = dr8["Id"].ToString();
                }
                dr8.Close();
                textBox15.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox14.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox13.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox11.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();
                textBox12.Text = dataGridView1.Rows[rowindex1].Cells[10].Value.ToString();
                textBox7.Text = dataGridView1.Rows[rowindex1].Cells[6].Value.ToString();
                textBox6.Text = dataGridView1.Rows[rowindex1].Cells[7].Value.ToString();
                textBox18.Text = dataGridView1.Rows[rowindex1].Cells[9].Value.ToString();

                SqlConnection conn33 = new SqlConnection(src);
                conn33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from SoldItems WHERE IdSale ='" + id + "'", conn33);
                SqlDataReader dr83 = cmd33.ExecuteReader();
                empty = true;
                dataGridView2.Rows.Clear();
                for (int ro = 0; ro < 15; ro++)
                {
                    dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                }
                row1 = 0;
                Array.Clear(itemarr, 0, itemarr.Length);
                while (dr83.Read())
                {
                        dataGridView2.Rows.Insert(row1, dr83["Id"], textBox14.Text, textBox13.Text, textBox15.Text, dr83["ItemName"], dr83["CompanyName"], dr83["Price"].ToString(), dr83["Quantity"].ToString(), dr83["FullPrice"].ToString(), dr83["Profit"].ToString());
                        row1++;
                   
                }
                dr83.Close();
                if (row1 > 0)
                {
                    float sum1 = 0; float sum2 = 0;
                    for (int k = 0; k <= row1 - 1; k++)
                    {
                        sum1 = sum1 + float.Parse(dataGridView2.Rows[k].Cells[7].Value.ToString());
                        if (dataGridView2.Rows[k].Cells[9].Value != null && dataGridView2.Rows[k].Cells[9].Value.ToString() != "")
                        { sum2 = sum2 + float.Parse(dataGridView2.Rows[k].Cells[9].Value.ToString()); }
                    }
                    textBox5.Text = sum1.ToString();
                    textBox17.Text = sum2.ToString();
                }

                empty = false;
                select = false;
                panel1.Focus();

            }
            else
            {

                textBox6.Text = "";
                textBox7.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                textBox15.Text = "";
                panel6.Visible = false;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text == "" && comboBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox1.Text == "")
            { MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا"); }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "SHS2";
                string page = "SHS2";
                Program.mysignin.which(ww, page);
            }
        }

        public void SHS2()
        {

            textBox6.Text = "";
            textBox7.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox18.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox17.Text = "";
            textBox5.Text = "";
            panel6.Visible = false;
            empty = true;
            select = false;
            row1 = 0;
            try
            {
                 if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox1.Text == "")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from SoldItems", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr.Read())
                    {
                        if (dr["Name"].ToString() == comboBox4.Text)

                        {
                            dataGridView2.Rows.Insert(row1, dr["Id"].ToString(), dr["IdSale"].ToString(), dr["Date"].ToString(), dr["Name"].ToString(), dr["ItemName"].ToString(), dr["CompanyName"].ToString(), dr["Price"].ToString(), dr["Quantity"].ToString(), dr["FullPrice"].ToString(), dr["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text == "" && textBox3.Text == "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox4.Text && dr1["ItemName"].ToString() == comboBox1.Text)

                        {

                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;

                        }
                    }
                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && textBox2.Text == "" && textBox3.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con3 = new SqlConnection(src);
                    con3.Open();
                    SqlCommand cmd3 = new SqlCommand("select * from SoldItems", con3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr3.Read())
                    {
                        if (dr3["Name"].ToString() == comboBox4.Text && dr3["IdSale"].ToString() == textBox3.Text)

                        {
                            dataGridView2.Rows.Insert(row1, dr3["Id"].ToString(), dr3["IdSale"].ToString(), dr3["Date"].ToString(), dr3["Name"].ToString(), dr3["ItemName"].ToString(), dr3["CompanyName"].ToString(), dr3["Price"].ToString(), dr3["Quantity"].ToString(), dr3["FullPrice"].ToString(), dr3["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text == "" && textBox2.Text == "" && textBox3.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con4 = new SqlConnection(src);
                    con4.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from SoldItems", con4);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr4.Read())
                    {
                        if (dr4["Name"].ToString() == comboBox4.Text && DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr4["Id"].ToString(), dr4["IdSale"].ToString(), dr4["Date"].ToString(), dr4["Name"].ToString(), dr4["ItemName"].ToString(), dr4["CompanyName"].ToString(), dr4["Price"].ToString(), dr4["Quantity"].ToString(), dr4["FullPrice"].ToString(), dr4["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text == "" && textBox2.Text != "" && textBox3.Text == "" && textBox1.Text == "")
                {
                    SqlConnection con5 = new SqlConnection(src);
                    con5.Open();
                    SqlCommand cmd5 = new SqlCommand("select * from SoldItems", con5);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr5.Read())
                    {
                        if (dr5["Name"].ToString() == comboBox4.Text && DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr5["Id"].ToString(), dr5["IdSale"].ToString(), dr5["Date"].ToString(), dr5["Name"].ToString(), dr5["ItemName"].ToString(), dr5["CompanyName"].ToString(), dr5["Price"].ToString(), dr5["Quantity"].ToString(), dr5["FullPrice"].ToString(), dr5["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text == "" && textBox2.Text != "" && textBox3.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con6 = new SqlConnection(src);
                    con6.Open();
                    SqlCommand cmd6 = new SqlCommand("select * from SoldItems", con6);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr6.Read())
                    {
                        if (dr6["Name"].ToString() == comboBox4.Text && DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox1.Text) && DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr6["Id"].ToString(), dr6["IdSale"].ToString(), dr6["Date"].ToString(), dr6["Name"].ToString(), dr6["ItemName"].ToString(), dr6["CompanyName"].ToString(), dr6["Price"].ToString(), dr6["Quantity"].ToString(), dr6["FullPrice"].ToString(), dr6["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con7 = new SqlConnection(src);
                    con7.Open();
                    SqlCommand cmd7 = new SqlCommand("select * from SoldItems", con7);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr7.Read())
                    {
                        if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr7["Id"].ToString(), dr7["IdSale"].ToString(), dr7["Date"].ToString(), dr7["Name"].ToString(), dr7["ItemName"].ToString(), dr7["CompanyName"].ToString(), dr7["Price"].ToString(), dr7["Quantity"].ToString(), dr7["FullPrice"].ToString(), dr7["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con8 = new SqlConnection(src);
                    con8.Open();
                    SqlCommand cmd8 = new SqlCommand("select * from SoldItems", con8);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr8.Read())
                    {
                        if (DateTime.Parse(dr8["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr8["Id"].ToString(), dr8["IdSale"].ToString(), dr8["Date"].ToString(), dr8["Name"].ToString(), dr8["ItemName"].ToString(), dr8["CompanyName"].ToString(), dr8["Price"].ToString(), dr8["Quantity"].ToString(), dr8["FullPrice"].ToString(), dr8["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text != "" && textBox1.Text != "")
                {
                    SqlConnection con9 = new SqlConnection(src);
                    con9.Open();
                    SqlCommand cmd9 = new SqlCommand("select * from SoldItems", con9);
                    SqlDataReader dr9 = cmd9.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr9.Read())
                    {
                        if (DateTime.Parse(dr9["Date"].ToString()) >= DateTime.Parse(textBox1.Text) && DateTime.Parse(dr9["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr9["Id"].ToString(), dr9["IdSale"].ToString(), dr9["Date"].ToString(), dr9["Name"].ToString(), dr9["ItemName"].ToString(), dr9["CompanyName"].ToString(), dr9["Price"].ToString(), dr9["Quantity"].ToString(), dr9["FullPrice"].ToString(), dr9["Profit"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text == "" && textBox1.Text == "")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from SoldItems", con10);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr10.Read())
                    {
                        dataGridView2.Rows.Insert(row1, dr10["Id"].ToString(), dr10["IdSale"].ToString(), dr10["Date"].ToString(), dr10["Name"].ToString(), dr10["ItemName"].ToString(), dr10["CompanyName"].ToString(), dr10["Price"].ToString(), dr10["Quantity"].ToString(), dr10["FullPrice"].ToString(), dr10["Profit"].ToString());
                        row1++;

                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text == "" && textBox3.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * Soldfrom Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox4.Text && dr1["ItemName"].ToString() == comboBox1.Text && dr1["IdSale"].ToString() == textBox3.Text)

                        {

                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text != "" && textBox3.Text == "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox4.Text && dr1["ItemName"].ToString() == comboBox1.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text == "" && textBox3.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox4.Text && dr1["ItemName"].ToString() == comboBox1.Text && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text != "" && textBox3.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["Name"].ToString() == comboBox4.Text && dr1["ItemName"].ToString() == comboBox1.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox2.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text == "" && textBox3.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox1.Text && dr1["IdSale"].ToString() == textBox3.Text)

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text != "" && textBox3.Text == "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox1.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text == "" && textBox3.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox1.Text && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {

                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text != "" && textBox3.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox1.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox2.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && comboBox1.Text == "" && textBox2.Text == "" && textBox3.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["IdSale"].ToString() == textBox3.Text)

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && comboBox1.Text != "" && textBox2.Text == "" && textBox3.Text == "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SoldItems", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox1.Text)

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["IdSale"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["ItemName"].ToString(), dr1["CompanyName"].ToString(), dr1["Price"].ToString(), dr1["Quantity"].ToString(), dr1["FullPrice"].ToString(), dr1["Profit"].ToString());
                            row1++;
                        }
                    }
                }
                if (row1 > 0)
                {
                    float sum1 = 0; float sum2 = 0;
                    for (int k1 = 0; k1 <= row1 - 1; k1++)
                    {
                        sum1 = sum1 + float.Parse(dataGridView2.Rows[k1].Cells[7].Value.ToString());
                        if (dataGridView2.Rows[k1].Cells[9].Value.ToString() != "" && dataGridView2.Rows[k1].Cells[9].Value != null)
                        { sum2 = sum2 + float.Parse(dataGridView2.Rows[k1].Cells[9].Value.ToString()); }
                    }
                    textBox5.Text = sum1.ToString();
                    textBox17.Text = sum2.ToString();
                }
                this.dataGridView2.Sort(this.dataGridView2.Columns[1], ListSortDirection.Descending);
                for (int y = 0; y < row1; y++)
                {
                    this.dataGridView2.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }
            }
            catch { MessageBox.Show("الرجاء التاكد من معلومات البحث"); }

        }

        private void dateTimePicker4_ValueChanged_1(object sender, EventArgs e)
        {
            string theDate2 = dateTimePicker4.Value.ToString("dd/MM/yyyy");
            textBox10.Text = theDate2.ToString();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            textBox8.Text = theDate1.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate3 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox1.Text = theDate3.ToString();

        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string theDate4 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            textBox2.Text = theDate4.ToString();

        }

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item3 = comboBox3.Text;
                string[] filteredItems3 = items3.Where(x => x.Contains(item3)).ToArray();
                comboBox3.Items.Clear();
                comboBox3.Items.Add(item3);
                comboBox3.Items.AddRange(filteredItems3);
                comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox3.DroppedDown = true;
                comboBox3.SelectionStart = item3.Length;
                comboBox3.SelectionLength = 0;

                comboBox3.Cursor = Cursor.Current;
            }
        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item2 = comboBox2.Text;
                string[] filteredItems2 = items2.Where(x => x.Contains(item2)).ToArray();
                comboBox2.Items.Clear();
                comboBox2.Items.Add(item2);
                comboBox2.Items.AddRange(filteredItems2);
                comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox2.DroppedDown = true;
                comboBox2.SelectionStart = item2.Length;
                comboBox2.SelectionLength = 0;

                comboBox2.Cursor = Cursor.Current;
            }
        }

        private void comboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item4 = comboBox4.Text;
                string[] filteredItems4 = items.Where(x => x.Contains(item4)).ToArray();
                comboBox4.Items.Clear();
                comboBox4.Items.Add(item4);
                comboBox4.Items.AddRange(filteredItems4);
                comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox4.DroppedDown = true;
                comboBox4.SelectionStart = item4.Length;
                comboBox4.SelectionLength = 0;

                comboBox4.Cursor = Cursor.Current;
            }
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item1 = comboBox1.Text;
                string[] filteredItems1 = items1.Where(x => x.Contains(item1)).ToArray();
                comboBox1.Items.Clear();
                comboBox1.Items.Add(item1);
                comboBox1.Items.AddRange(filteredItems1);
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox1.DroppedDown = true;
                comboBox1.SelectionStart = item1.Length;
                comboBox1.SelectionLength = 0;

                comboBox1.Cursor = Cursor.Current;
            }
        }

        int i = 0; int c = 0; int dr = 0; bool end = false;
        private void button3_Click(object sender, EventArgs e)
        {
            if (panel6.Visible)
            {

                end = false;
                i = 0;

                try
                {
                    for (int f = 0; f < dataGridView2.RowCount - 1; f++)
                    {
                        if (dataGridView2.Rows[f].Cells[0].Value != null)
                        { i++; }
                    }
                    if (i > 0)
                    {

                           DialogResult result = printDialog1.ShowDialog();
                           if (result == DialogResult.OK)
                           {
                               printDocument1.Print();
                           }
                       // printPreviewDialog1.ShowDialog();

                    }
                }
                catch
                {

                }
                if (i > 28) { c = 0; dr = 0; }
                else { c = -1; }
            }

        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            Image newImage2 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\C.PNG");
            Image newImage3 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\C2.PNG");

            if (i <= 28 && i > 0)
            {
                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Samer
                // /*
                Image newImage4 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\S1.jpg");
                e.Graphics.DrawString("مـــــؤســــــــــــســـــــــة عـــــــــــــــويـــــــــــــــس" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
                e.Graphics.DrawString("مــــرج الـــحـــمـــام - شـــارع ام عـــبـــهـــرة - 0778982259" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(460, 70));
                e.Graphics.DrawImage(newImage4, 30, 2);
                e.Graphics.DrawString("لمواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
                //  */
                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Ayman
                /*
              Image newImage = Image.FromFile("C:\\Users\\Amjad\\source\\repos\\BALOOTA\\BALOOTA\\Resources\\BP4.jpg");
              e.Graphics.DrawString("بــــــلّــــــوطــــــــــــة لـــــــلــــــــــدهــــــــانــــــــات" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
              e.Graphics.DrawString("عجلون - شارع إربد - مقابل المقبرة المسيحية - 0777519277 - 0776947787" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(380, 70));
              e.Graphics.DrawImage(newImage, 30, 2);
              e.Graphics.DrawString("و مواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
              //  */
                e.Graphics.DrawString("دهــــــانـــــات و مـــــواد عـــــــزل - أدوات كــــهــــربــــائــــيــــة وصـــحـــيـــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 35));
                e.Graphics.DrawString("لـوازم نـجـاريــن وحـداديــن - خـردوات وبـراغــي - عــدد يــدويــة وهــنــدســيــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 50));
                e.Graphics.DrawString("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ", new Font("Microsoft Sans Serif", 20, FontStyle.Bold), Brushes.Navy, new PointF(0, 75));
                e.Graphics.DrawString("   فاتورة" + Environment.NewLine + "نقدي - ذمم", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));
                e.Graphics.DrawString("رقم الفاتورة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(760, 180));
                e.Graphics.DrawString(textBox14.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                e.Graphics.DrawString(textBox15.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                e.Graphics.DrawString(textBox13.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                e.Graphics.DrawImage(newImage2, 50, 225);


                int ee = 300;
                for (int r = 0; r <= i; r++) //29 36
                {
                    if (dataGridView2.Rows[r].Cells[0].Value != null)
                    {
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[6].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[7].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[8].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                        ee += 20;
                    }
                }

                e.Graphics.DrawImage(newImage3, 50, 900);
                e.Graphics.DrawString(textBox18.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 915));
                e.Graphics.DrawString(textBox7.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 905));
                e.Graphics.DrawString(textBox6.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 927));
                e.Graphics.DrawString(textBox11.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 915));

                e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, 970));
                e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, 990));
                e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, 990));
                end = true;
            }
            else if (i > 28 && i <= 35)
            {
                if (c == 0)
                {
                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Samer
                    // /*
                    Image newImage4 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\S1.jpg");
                    e.Graphics.DrawString("مـــــؤســــــــــــســـــــــة عـــــــــــــــويـــــــــــــــس" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
                    e.Graphics.DrawString("مــــرج الـــحـــمـــام - شـــارع ام عـــبـــهـــرة - 0778982259" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(460, 70));
                    e.Graphics.DrawImage(newImage4, 30, 2);
                    e.Graphics.DrawString("لمواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
                    //  */
                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Ayman
                    /*
                  Image newImage = Image.FromFile("C:\\Users\\Amjad\\source\\repos\\BALOOTA\\BALOOTA\\Resources\\BP4.jpg");
                  e.Graphics.DrawString("بــــــلّــــــوطــــــــــــة لـــــــلــــــــــدهــــــــانــــــــات" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
                  e.Graphics.DrawString("عجلون - شارع إربد - مقابل المقبرة المسيحية - 0777519277 - 0776947787" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(380, 70));
                  e.Graphics.DrawImage(newImage, 30, 2);
                  e.Graphics.DrawString("و مواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
                  //  */
                    e.Graphics.DrawString("دهــــــانـــــات و مـــــواد عـــــــزل - أدوات كــــهــــربــــائــــيــــة وصـــحـــيـــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 35));
                    e.Graphics.DrawString("لـوازم نـجـاريــن وحـداديــن - خـردوات وبـراغــي - عــدد يــدويــة وهــنــدســيــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 50));
                    e.Graphics.DrawString("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ", new Font("Microsoft Sans Serif", 20, FontStyle.Bold), Brushes.Navy, new PointF(0, 75));
                    e.Graphics.DrawString("   فاتورة" + Environment.NewLine + "نقدي - ذمم", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));
                    e.Graphics.DrawString("رقم الفاتورة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(760, 180));
                    e.Graphics.DrawString(textBox14.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                    e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                    e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                    e.Graphics.DrawString(textBox15.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                    e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                    e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                    e.Graphics.DrawString(textBox13.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                    e.Graphics.DrawImage(newImage2, 50, 225);


                    int ee = 300;
                    for (int r = 0; r <= i; r++) //29 36
                    {
                        if (dataGridView2.Rows[r].Cells[0].Value != null)
                        {
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[6].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[7].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[8].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                            ee += 20;
                        }
                    }

                    e.Graphics.DrawString("1", new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                }
                if (c == 1)
                {
                    e.Graphics.DrawImage(newImage3, 50, 50);
                    e.Graphics.DrawString(textBox18.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 65));
                    e.Graphics.DrawString(textBox7.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 55));
                    e.Graphics.DrawString(textBox6.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 77));
                    e.Graphics.DrawString(textBox11.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 65));

                    e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, 120));
                    e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, 140));
                    e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, 140));
                    e.Graphics.DrawString("2", new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                    end = true;
                }
                if (c == 0) { e.HasMorePages = true; c++; return; }
                else if (c == 1)
                {
                    e.HasMorePages = false;
                }
            }
            else if (i > 35)
            {
                if (c == 0)
                {
                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Samer
                    // /*
                    Image newImage4 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\S1.jpg");
                    e.Graphics.DrawString("مـــــؤســــــــــــســـــــــة عـــــــــــــــويـــــــــــــــس" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
                    e.Graphics.DrawString("مــــرج الـــحـــمـــام - شـــارع ام عـــبـــهـــرة - 0778982259" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(460, 70));
                    e.Graphics.DrawImage(newImage4, 30, 2);
                    e.Graphics.DrawString("لمواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
                    //  */
                    // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Ayman
                    /*
                  Image newImage = Image.FromFile("C:\\Users\\Amjad\\source\\repos\\BALOOTA\\BALOOTA\\Resources\\BP4.jpg");
                  e.Graphics.DrawString("بــــــلّــــــوطــــــــــــة لـــــــلــــــــــدهــــــــانــــــــات" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
                  e.Graphics.DrawString("عجلون - شارع إربد - مقابل المقبرة المسيحية - 0777519277 - 0776947787" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(380, 70));
                  e.Graphics.DrawImage(newImage, 30, 2);
                  e.Graphics.DrawString("و مواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
                  //  */
                    e.Graphics.DrawString("دهــــــانـــــات و مـــــواد عـــــــزل - أدوات كــــهــــربــــائــــيــــة وصـــحـــيـــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 35));
                    e.Graphics.DrawString("لـوازم نـجـاريــن وحـداديــن - خـردوات وبـراغــي - عــدد يــدويــة وهــنــدســيــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 50));
                    e.Graphics.DrawString("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ", new Font("Microsoft Sans Serif", 20, FontStyle.Bold), Brushes.Navy, new PointF(0, 75));
                    e.Graphics.DrawString("   فاتورة" + Environment.NewLine + "نقدي - ذمم", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));
                    e.Graphics.DrawString("رقم الفاتورة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(760, 180));
                    e.Graphics.DrawString(textBox14.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                    e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                    e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                    e.Graphics.DrawString(textBox15.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                    e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                    e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                    e.Graphics.DrawString(textBox13.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                    e.Graphics.DrawImage(newImage2, 50, 225);


                    int ee = 300;
                    for (int r = 0; r < 36; r++) //29 36
                    {
                        if (dataGridView2.Rows[r].Cells[0].Value != null)
                        {
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[6].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[7].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[8].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                            ee += 20;
                            dr++;
                        }
                    }

                    e.Graphics.DrawString("1", new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));

                }
                if (c > 0)
                {
                    if (i - dr < 38 && i > dr)
                    {
                        e.Graphics.DrawImage(newImage2, 50, 50);
                        int ee2 = 125;
                        int y = dr;
                        for (int r = y; r <= i; r++) //38 45
                        {
                            if (dataGridView2.Rows[r].Cells[0].Value != null)
                            {
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[6].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[7].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[8].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
                                ee2 += 20;
                                dr++;
                            }
                        }
                        ee2 += 20;
                        e.Graphics.DrawImage(newImage3, 50, ee2);
                        e.Graphics.DrawString(textBox18.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2 + 15));
                        e.Graphics.DrawString(textBox7.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, ee2 + 5));
                        e.Graphics.DrawString(textBox6.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, ee2 + 27));
                        e.Graphics.DrawString(textBox11.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, ee2 + 15));

                        e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, ee2 + 70));
                        e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, ee2 + 90));
                        e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, ee2 + 90));
                        e.Graphics.DrawString((c + 1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                        end = true;
                    }
                    else if (i - dr >= 38 && i - dr < 45)
                    {
                        if (dr <= i)
                        {
                            e.Graphics.DrawImage(newImage2, 50, 50);
                            int y = dr;
                            int ee2 = 125;
                            for (int r = y; r <= i; r++) //38 45
                            {
                                if (dataGridView2.Rows[r].Cells[0].Value != null)
                                {
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[6].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[7].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[8].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
                                    ee2 += 20; dr++;
                                }
                            }
                            e.Graphics.DrawString((c + 1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));

                            if (dr > i)
                            { e.HasMorePages = true; c++; return; }
                        }

                    }
                    else if (dr > i && !end)
                    {
                        e.Graphics.DrawImage(newImage3, 50, 50);
                        e.Graphics.DrawString(textBox18.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 65));
                        e.Graphics.DrawString(textBox7.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 55));
                        e.Graphics.DrawString(textBox6.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 77));
                        e.Graphics.DrawString(textBox11.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 65));

                        e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, 120));
                        e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, 140));
                        e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, 140));
                        e.Graphics.DrawString((c + 1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                        end = true;
                    }
                    else if (i - dr >= 45)
                    {
                        e.Graphics.DrawImage(newImage2, 50, 50);
                        int y = dr;
                        int ee2 = 125;
                        for (int r = y; r <= y + 45; r++) //38 45
                        {
                            if (dataGridView2.Rows[r].Cells[0].Value != null)
                            {
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[6].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[7].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[8].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
                                ee2 += 20; dr++;
                            }
                        }
                        e.Graphics.DrawString((c + 1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                        if (dr > i)
                        { e.HasMorePages = true; c++; return; }
                    }

                }
                if (dr <= i && !end)
                {
                    if (dr == i) { dr++; }
                    c++; e.HasMorePages = true; return;
                }
            }


        }

    }
}
