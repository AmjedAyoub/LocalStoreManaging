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
    public partial class Purches : Form
    {
        public Purches()
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
        public string[] idarr = new string[1000000];
        private string src = Program.xsrc;
        string[] items;
        string[] items1;
        string[] items2;
        float dis = 0;
        float per = 0;

        public void Purches_Load(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 1)
            {
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
            }
            
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox12.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            comboBox3.SelectedText = "";
            dateTimePicker1.Checked = false;
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from Purchases", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            dataGridView1.Rows.Clear();
            while (dr39.Read())
            {
                iitem = dr39["CompanyName"].ToString();

                if (!comboBox1.Items.Contains(iitem))
                {
                    comboBox1.Items.Add(iitem);
                    comboBox3.Items.Add(iitem);
                }
            }
            dr39.Close();

            items = new string[comboBox3.Items.Count];
            comboBox3.Items.CopyTo(items, 0);
            items1 = new string[comboBox1.Items.Count];
            comboBox1.Items.CopyTo(items1, 0);

            if (comboBox2.Items.Count > 1)
            {
                comboBox2.Items.Clear();
            }
            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Inventory", conn3);
            string item = "";
            SqlDataReader dr3 = cmd3.ExecuteReader();
            comboBox2.Items.Add("");
            while (dr3.Read())
            {
                item = dr3["Item"].ToString();

                if (!comboBox2.Items.Contains(item))
                {
                    comboBox2.Items.Add(item);
                }
            }
            dr3.Close();
            items2 = new string[comboBox2.Items.Count];
            comboBox2.Items.CopyTo(items2, 0);

            empty = true;
            dataGridView2.Rows.Clear();
            SqlConnection conn393 = new SqlConnection(src);
            conn393.Open();
            SqlCommand cmd393 = new SqlCommand("select * from Items", conn393);
            string iitemtt = "";
            SqlDataReader dr393 = cmd393.ExecuteReader();
            while (dr393.Read())
            {
                iitemtt = dr393["ItemName"].ToString();

                if (!this.dataGridViewComboBoxColumn1.Items.Contains(iitemtt))
                {
                    this.dataGridViewComboBoxColumn1.Items.Add(iitemtt);
                }

            }
            dr393.Close();
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                dataGridView2.Rows.Add(null, null, null, null, null, null, null, null);
            }
            comboBox3.Focus();
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate3 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox2.Text = theDate3.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "" && comboBox2.Text == "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
            { MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا"); }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "SHP";
                string page = "SHP";
                Program.mysignin.which(ww, page);
            }
            
        }

        public void shp()
        {
            try
            {
                row11 = 0;
               
                if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from Purchases", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr.Read())
                    {
                        if (dr["CompanyName"].ToString() == comboBox3.Text && float.Parse(dr["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr["Id"].ToString(), dr["Date"].ToString(), dr["CompanyName"].ToString(), dr["InvoiceNo"].ToString(), dr["Amount"].ToString(), dr["Debt"].ToString(), dr["Paid"].ToString(), dr["RDebt"].ToString(), dr["Dis"].ToString(), dr["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["CompanyName"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text)

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd3 = new SqlCommand("select * from Purchases", con3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr3.Read())
                    {
                        if (dr3["CompanyName"].ToString() == comboBox3.Text && (dr3["InvoiceNo"].ToString() == textBox9.Text || dr3["Id"].ToString() == textBox9.Text) && float.Parse(dr3["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr3["Id"].ToString(), dr3["Date"].ToString(), dr3["CompanyName"].ToString(), dr3["InvoiceNo"].ToString(), dr3["Amount"].ToString(), dr3["Debt"].ToString(), dr3["Paid"].ToString(), dr3["RDebt"].ToString(), dr3["Dis"].ToString(), dr3["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con4 = new SqlConnection(src);
                    con4.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from Purchases", con4);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr4.Read())
                    {
                        if (dr4["CompanyName"].ToString() == comboBox3.Text && DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr4["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr4["Id"].ToString(), dr4["Date"].ToString(), dr4["CompanyName"].ToString(), dr4["InvoiceNo"].ToString(), dr4["Amount"].ToString(), dr4["Debt"].ToString(), dr4["Paid"].ToString(), dr4["RDebt"].ToString(), dr4["Dis"].ToString(), dr4["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con5 = new SqlConnection(src);
                    con5.Open();
                    SqlCommand cmd5 = new SqlCommand("select * from Purchases", con5);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr5.Read())
                    {
                        if (dr5["CompanyName"].ToString() == comboBox3.Text && DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr5["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr5["Id"].ToString(), dr5["Date"].ToString(), dr5["CompanyName"].ToString(), dr5["InvoiceNo"].ToString(), dr5["Amount"].ToString(), dr5["Debt"].ToString(), dr5["Paid"].ToString(), dr5["RDebt"].ToString(), dr5["Dis"].ToString(), dr5["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text == "" && textBox8.Text != "" && textBox9.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con6 = new SqlConnection(src);
                    con6.Open();
                    SqlCommand cmd6 = new SqlCommand("select * from Purchases", con6);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr6.Read())
                    {
                        if (dr6["CompanyName"].ToString() == comboBox3.Text && DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr6["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr6["Id"].ToString(), dr6["Date"].ToString(), dr6["CompanyName"].ToString(), dr6["InvoiceNo"].ToString(), dr6["Amount"].ToString(), dr6["Debt"].ToString(), dr6["Paid"].ToString(), dr6["RDebt"].ToString(), dr6["Dis"].ToString(), dr6["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con7 = new SqlConnection(src);
                    con7.Open();
                    SqlCommand cmd7 = new SqlCommand("select * from Purchases", con7);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr7.Read())
                    {
                        if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr7["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr7["Id"].ToString(), dr7["Date"].ToString(), dr7["CompanyName"].ToString(), dr7["InvoiceNo"].ToString(), dr7["Amount"].ToString(), dr7["Debt"].ToString(), dr7["Paid"].ToString(), dr7["RDebt"].ToString(), dr7["Dis"].ToString(), dr7["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con8 = new SqlConnection(src);
                    con8.Open();
                    SqlCommand cmd8 = new SqlCommand("select * from Purchases", con8);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr8.Read())
                    {
                        if (DateTime.Parse(dr8["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr8["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr8["Id"].ToString(), dr8["Date"].ToString(), dr8["CompanyName"].ToString(), dr8["InvoiceNo"].ToString(), dr8["Amount"].ToString(), dr8["Debt"].ToString(), dr8["Paid"].ToString(), dr8["RDebt"].ToString(), dr8["Dis"].ToString(), dr8["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con9 = new SqlConnection(src);
                    con9.Open();
                    SqlCommand cmd9 = new SqlCommand("select * from Purchases", con9);
                    SqlDataReader dr9 = cmd9.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr9.Read())
                    {
                        if (DateTime.Parse(dr9["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr9["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr9["Amount"].ToString()) > -1)

                        {
                            dataGridView1.Rows.Insert(row11, false, dr9["Id"].ToString(), dr9["Date"].ToString(), dr9["CompanyName"].ToString(), dr9["InvoiceNo"].ToString(), dr9["Amount"].ToString(), dr9["Debt"].ToString(), dr9["Paid"].ToString(), dr9["RDebt"].ToString(), dr9["Dis"].ToString(), dr9["Notes"].ToString());
                            row11++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from Purchases", con10);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr10.Read())
                    {
                        if (float.Parse(dr10["Amount"].ToString()) > -1)
                        {
                            dataGridView1.Rows.Insert(row11, false, dr10["Id"].ToString(), dr10["Date"].ToString(), dr10["CompanyName"].ToString(), dr10["InvoiceNo"].ToString(), dr10["Amount"].ToString(), dr10["Debt"].ToString(), dr10["Paid"].ToString(), dr10["RDebt"].ToString(), dr10["Dis"].ToString(), dr10["Notes"].ToString());

                            row11++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["CompanyName"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && (dr1["InvoiceNo"].ToString() == textBox9.Text || dr1["Id"].ToString() == textBox9.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["CompanyName"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["CompanyName"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["CompanyName"].ToString() == comboBox3.Text && dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && (dr1["InvoiceNo"].ToString() == textBox9.Text || dr1["Id"].ToString() == textBox9.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if ((dr1["InvoiceNo"].ToString() == textBox9.Text || dr1["Id"].ToString() == textBox9.Text))

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
                                    row11++;
                                }
                            }
                            con2.Close();
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && comboBox2.Text != "" && textBox8.Text == "" && textBox9.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Items", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    }
                    row11 = 0;
                    while (dr1.Read())
                    {
                        if (dr1["ItemName"].ToString() == comboBox2.Text)

                        {
                            SqlConnection con2 = new SqlConnection(src);
                            con2.Open();
                            SqlCommand cmd2 = new SqlCommand("select * from Purchases", con2);
                            SqlDataReader dr2 = cmd2.ExecuteReader();
                            while (dr2.Read())
                            {
                                if (dr2["Id"].ToString() == dr1["IdPurchase"].ToString() && float.Parse(dr2["Amount"].ToString()) > -1)

                                {
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["InvoiceNo"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Dis"].ToString(), dr2["Notes"].ToString());
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
            }
            catch { MessageBox.Show("الرجاء التاكد من معلومات البحث"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            if (select)
            {
                SqlConnection conn3 = new SqlConnection(src);
                conn3.Open();
                n = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                SqlCommand cmd3 = new SqlCommand("select * from Purchases WHERE Id ='"+ dataGridView1.Rows[rowindex1].Cells[1].Value.ToString() + "'", conn3);
                idpur = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                paid = dataGridView1.Rows[rowindex1].Cells[7].Value.ToString();
                SqlDataReader dr8 = cmd3.ExecuteReader();
                while (dr8.Read())
                {
                    id = dr8["Id"].ToString();
                }
                dr8.Close();
                comboBox1.SelectedItem= dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowindex1].Cells[5].Value.ToString();
                textBox11.Text = dataGridView1.Rows[rowindex1].Cells[10].Value.ToString();
                textBox5.Text = dataGridView1.Rows[rowindex1].Cells[7].Value.ToString();
                textBox4.Text = dataGridView1.Rows[rowindex1].Cells[8].Value.ToString();


                SqlConnection conn33 = new SqlConnection(src);
                conn33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from Items WHERE IdPurchase ='" + id + "'", conn33);
                SqlDataReader dr83 = cmd33.ExecuteReader();
                empty = true;
                dataGridView2.Rows.Clear();
                for (int ro = 0; ro < 15; ro++)
                {
                    dataGridView2.Rows.Add(null, null, null, null, null, null, null, null);
                }
                row1 = 0;
                Array.Clear(itemarr, 0, itemarr.Length);
                Array.Clear(idarr, 0, idarr.Length);
                while (dr83.Read())
                {
                    if (float.Parse(dr83["RQuantity"].ToString()) > -1)
                    {
                        dataGridView2.Rows.Insert(row1, "", dr83["Price"].ToString(), dr83["Quantity"].ToString(), dr83["RQuantity"].ToString(), dr83["MinQuantity"].ToString(), dr83["FullPrice"].ToString(), dr83["Notes"].ToString(), dr83["Quantity"].ToString());

                        this.dataGridView2.Rows[row1].HeaderCell.Value = (row1+1).ToString();
                        dataGridView2.Rows[row1].Cells[0].Value = dr83["ItemName"];
                        itemarr[row1] = dr83["ItemName"].ToString();
                        idarr[row1] = dr83["Id"].ToString();
                        row1++;
                    }
                }
                dr83.Close();
                textBox12.Text = dataGridView1.Rows[rowindex1].Cells[9].Value.ToString();
                textBox12_Leave(sender, e);

                empty = false;
                select = false;
            }
        }        

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView2.CurrentCellAddress.X == dataGridViewComboBoxColumn1.DisplayIndex)
            {
                cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cb.Sorted = true;
                    cb.DroppedDown = true;

                }

                empty = false;
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCellAddress.X == 0 && dataGridView1.CurrentRow.Cells[1].Value!=null)
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

        private void button4_Click(object sender, EventArgs e)
        {
            if(select)
            {
                Program.myrp.Show();
                Program.myrp.Rp(dataGridView1.Rows[rowindex1].Cells[1].Value.ToString());
            }
        }

        public void Setname(bool ok,string n,string iid, string rd,string rp)
        {
            if (ok)
            {
                name = n;
                rdept = rd;
                rpaid = rp;
                Rrp(iid);
            }
        }

        public void Rrp(string rid)
        {
            if ((MessageBox.Show("هل انت متأكد من حذف الفاتورة ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                float q = 0;
                float r = 0;
                string it = "";
                string dd = "";
                SqlConnection conn33 = new SqlConnection(src);
                conn33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from Items WHERE IdPurchase ='" + rid + "'", conn33);
                SqlDataReader dr83 = cmd33.ExecuteReader();
                
                while (dr83.Read())
                {
                    it = it + Environment.NewLine + "  الصنف  " + dr83["ItemName"].ToString() + "  السعر الفردي  " + dr83["Price"].ToString() + "  الكمية  " + dr83["Quantity"].ToString() + "  الحد الادنى  " + dr83["MinQuantity"].ToString() + "  السعر الكلي  " + dr83["FullPrice"].ToString()+ "  الملاحظات  " + dr83["Notes"].ToString();
                    r = r + (float.Parse(dr83["Price"].ToString()) * float.Parse(dr83["RQuantity"].ToString()));
                    q = float.Parse(dr83["RQuantity"].ToString());
                    dd = dr83["Id"].ToString();
                    string itid = "";
                    float itq = 0;
                    SqlConnection conn31 = new SqlConnection(src);
                    conn31.Open();
                    SqlCommand cmd31 = new SqlCommand("select * from Inventory WHERE Item = @Name", conn31);
                    cmd31.Parameters.AddWithValue("@Name", dr83["ItemName"].ToString());
                    SqlDataReader dr81 = cmd31.ExecuteReader();
                    while (dr81.Read())
                    {
                        itid = dr81["Id"].ToString();
                        itq= float.Parse(dr81["Quantity"].ToString());
                    }
                    dr81.Close();
                    float total = itq - q;
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Inventory] SET Quantity=@box1 WHERE Id = '" + itid + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", total);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();

                    SqlConnection conn5 = new SqlConnection(src);
                    SqlCommand cmdn5 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn5);
                    cmdn5.Parameters.AddWithValue("@box1", -1);
                    conn5.Open();
                    SqlDataReader d725 = cmdn5.ExecuteReader();
                    conn5.Close();

                }
                dr83.Close();
               /* SqlConnection cn1 = new SqlConnection(src);
                SqlCommand cmd1 = new SqlCommand("DELETE FROM [Items] WHERE IdPurchase = @Box1", cn1);
                cmd1.Parameters.AddWithValue("@Box1", rid);
                cn1.Open();*/
                SqlConnection conn331 = new SqlConnection(src);
                conn331.Open();
                SqlCommand cmd331 = new SqlCommand("select * from Purchases WHERE Id ='" + rid + "'", conn331);
                SqlDataReader dr831 = cmd331.ExecuteReader();
                string inv = "";
                while (dr831.Read())
                {
                    inv = " لقد تم حذف فاتورة مشتريات " + ">>" + "  رمز الحركة " + rid + "  اسم الشركة " + dr831["CompanyName"].ToString() + "   " + "  رقم الفاتورة " + dr831["InvoiceNo"].ToString() + Environment.NewLine + "  التاريخ " + dr831["Date"].ToString() + "   " + "  القيمة " + dr831["Amount"].ToString() + "   " + "  القيمة المدفوعة " + dr831["Paid"].ToString() + "   " + "   القيمة المتبقية " + dr831["RDebt"].ToString() + "  ملاحظات " + dr831["Notes"].ToString() + Environment.NewLine + "  الاصناف  ";
                }

                SqlConnection conn58 = new SqlConnection(src);
                SqlCommand cmdn58 = new SqlCommand("UPDATE [Purchases] SET Amount=@box1 WHERE Id = '" + rid + "'", conn58);
                cmdn58.Parameters.AddWithValue("@box1", -1);
                conn58.Open();
                SqlDataReader d7258 = cmdn58.ExecuteReader();
                conn58.Close();
                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [StoreDebt] WHERE idPurchase = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", rid);
                cn111.Open();
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", name);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", inv + it);
                cmd.Parameters.AddWithValue("@textBox4", "DEL");
                con.Open();
                //SqlDataReader dr1 = cmd1.ExecuteReader();
                SqlDataReader dr111 = cmd111.ExecuteReader();
                SqlDataReader dr2 = cmd.ExecuteReader();

                SqlConnection conne = new SqlConnection(src);
                conne.Open();
                SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                regid = Convert.ToInt32(cmod.ExecuteScalar());
                SqlConnection conn3316 = new SqlConnection(src);
                conn3316.Open();
                SqlCommand cmd3316 = new SqlCommand("select * from Register WHERE Id ='" + regid + "'", conn3316);
                SqlDataReader dr8316 = cmd3316.ExecuteReader();
                float reg = 0;
                float t = 0;
                while (dr8316.Read())
                {
                    reg = float.Parse(dr8316["Amount"].ToString());
                }
                t = reg + float.Parse(rpaid);
                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                cmd55.Parameters.AddWithValue("@textBox2", t);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();
                
                MessageBox.Show("لقد تمت حذف الفاتورة بنجاح");
                con.Close();
                if (comboBox1.Items.Count > 1)
                {
                    comboBox1.Items.Clear();
                    comboBox3.Items.Clear();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox12.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                SqlConnection conn399 = new SqlConnection(src);
                conn399.Open();
                SqlCommand cmd399 = new SqlCommand("select * from Purchases", conn399);
                string iitem9 = "";
                SqlDataReader dr399 = cmd399.ExecuteReader();
                comboBox3.Items.Add("");
                comboBox3.Items.Add("الكل");
                dataGridView1.Rows.Clear();
                while (dr399.Read())
                {
                    iitem9 = dr399["CompanyName"].ToString();

                    if (!comboBox1.Items.Contains(iitem9))
                    {
                        comboBox1.Items.Add(iitem9);
                        comboBox3.Items.Add(iitem9);
                    }
                }
                dr399.Close();


                if (comboBox2.Items.Count > 1)
                {
                    comboBox2.Items.Clear();
                }
                SqlConnection conn30 = new SqlConnection(src);
                conn30.Open();
                SqlCommand cmd30 = new SqlCommand("select * from Inventory", conn30);
                string item0 = "";
                SqlDataReader dr30 = cmd30.ExecuteReader();
                comboBox2.Items.Add("");
                while (dr30.Read())
                {
                    item0 = dr30["Item"].ToString();

                    if (!comboBox2.Items.Contains(item0))
                    {
                        comboBox2.Items.Add(item0);
                    }
                }
                dr30.Close();

                empty = true;
                dataGridView2.Rows.Clear();
                SqlConnection conn3930 = new SqlConnection(src);
                conn3930.Open();
                SqlCommand cmd3930 = new SqlCommand("select * from Items", conn3930);
                string iitemtt0 = "";
                SqlDataReader dr3930 = cmd3930.ExecuteReader();
                while (dr3930.Read())
                {
                    iitemtt0 = dr3930["ItemName"].ToString();

                    if (!this.dataGridViewComboBoxColumn1.Items.Contains(iitemtt0))
                    {
                        this.dataGridViewComboBoxColumn1.Items.Add(iitemtt0);
                    }

                }
                dr3930.Close();
                for (int ro = 0; ro < 15; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    dataGridView2.Rows.Add(null, null, null, null, null, null, null, null);
                }

            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "")
                {
                    textBox4.Text = (float.Parse(textBox3.Text) - float.Parse(textBox5.Text)).ToString();
                    if (float.Parse(textBox4.Text) < 0)
                    {
                        MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                        textBox5.Text = "0.0";
                    }
                }
                
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                textBox5.Text = "0.0";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            bool good = false;
            bool good2 = false;
            if (comboBox1.Text == "") { MessageBox.Show("الرجاء ادخال اسم الشركة"); }
            else if (textBox1.Text == "") { MessageBox.Show("الرجاء ادخال رقم الفاتورة"); }
            else if (textBox2.Text == "") { MessageBox.Show("الرجاء ادخال التاريخ"); }
            else if (dataGridView2.Rows.Count <= 1) { MessageBox.Show("الرجاء ادخال الاصناف الى الجدول"); }
            else
            {
                for(int i=0;i<dataGridView2.Rows.Count-1;i++)
                {
                    if (dataGridView2.Rows[i].Cells[7].Value != null)
                    {
                        try
                        {
                            if (((float.Parse(dataGridView2.Rows[i].Cells[7].Value.ToString())) - (float.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString()))) > (float.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString())))
                            {
                                MessageBox.Show("الرجاء التأكد من الكميات في الجدول");
                                good = false;
                                break;
                            }
                            else
                            {
                                good = true;
                            }
                        }
                        catch
                        { MessageBox.Show("الرجاء ادخال ارقام صحيحة الى الجدول");
                            good = false;
                        }
                    }
                }
            }
            try
            {
                if (float.Parse(textBox3.Text) >= 0 || float.Parse(textBox4.Text) >= 0 || float.Parse(textBox5.Text) >= 0)
                { good2 = true; }
                else
                {
                    good2 = false;
                    MessageBox.Show("لا يجوز ان تكون القيم اقل من صفر");
                }
            }
            catch { MessageBox.Show("لا يجوز ان تكون القيم اقل من صفر");
                good2 = false;
            }
            if (good && good2)
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "";
                string page = "DP";
                Program.mysignin.which(ww, page);

            }
        }

        public void edit(bool ok, string namew)
        {

            int regiid = 0;
            if (ok)
            {
                for (int s = 0; s < dataGridView2.Rows.Count - 1; s++)
                {

                    if (dataGridView2.Rows[s].Cells[7].Value != null)
                    {
                        string iid = "";
                        float itq = 0;
                        SqlConnection co1 = new SqlConnection(src);
                        co1.Open();
                        SqlCommand cm31 = new SqlCommand("select * from Inventory WHERE Item = @Name", co1);
                        cm31.Parameters.AddWithValue("@Name", itemarr[s]);//dataGridView2.Rows[s].Cells[0].Value.ToString()
                        SqlDataReader drw = cm31.ExecuteReader();
                        while (drw.Read())
                        {
                            iid = drw["Id"].ToString();
                            itq = float.Parse(drw["Quantity"].ToString());
                        }
                        drw.Close();
                        float total = itq - float.Parse(dataGridView2.Rows[s].Cells[3].Value.ToString());
                        SqlConnection co2 = new SqlConnection(src);
                        SqlCommand cmdn2 = new SqlCommand("UPDATE [Inventory] SET Quantity=@box1 WHERE Id = '" + iid + "'", co2);
                        cmdn2.Parameters.AddWithValue("@box1", total);
                        co2.Open();
                        SqlDataReader d555 = cmdn2.ExecuteReader();
                        co2.Close();

                    }
                }

                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                SqlConnection co233 = new SqlConnection(src);
                SqlCommand cmdn233 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE IdPurchase = '" + idpur + "'", co233);
                cmdn233.Parameters.AddWithValue("@box1", -1);
                co233.Open();
                SqlDataReader d55533 = cmdn233.ExecuteReader();

                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

                SqlConnection c1 = new SqlConnection(src);
                SqlCommand cm1 = new SqlCommand("DELETE FROM [StoreDebt] WHERE idPurchase = @Ird", c1);
                cm1.Parameters.AddWithValue("@Ird", idpur);
                c1.Open();
                SqlDataReader dr1 = cm1.ExecuteReader();

                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                SqlConnection conne2 = new SqlConnection(src);
                conne2.Open();
                SqlCommand cmod2 = new SqlCommand("select max(Id) from Register", conne2);
                regiid = Convert.ToInt32(cmod2.ExecuteScalar());
                SqlConnection co4 = new SqlConnection(src);
                co4.Open();
                SqlCommand cm4 = new SqlCommand("select * from Register WHERE Id ='" + regiid + "'", co4);
                SqlDataReader d4 = cm4.ExecuteReader();
                float reg4 = 0;
                float t4 = 0;
                while (d4.Read())
                {
                    reg4 = float.Parse(d4["Amount"].ToString());
                }
                t4 = reg4 + float.Parse(paid) - float.Parse(textBox5.Text);
                SqlConnection co5 = new SqlConnection(src);
                SqlCommand cm5 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", co5);
                cm5.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                cm5.Parameters.AddWithValue("@textBox2", t4);
                co5.Open();
                SqlDataReader d5 = cm5.ExecuteReader();
                string debt = "";
                SqlConnection co22 = new SqlConnection(src);
                SqlCommand cmdn22 = new SqlCommand("UPDATE [Purchases] SET CompanyName=@box1,InvoiceNo=@box2,Date=@box3,Amount=@box4,Debt=@box5,Paid=@box6,RDebt=@box7,Notes=@box8,Dis=@box9 WHERE Id = '" + idpur + "'", co22);
                cmdn22.Parameters.AddWithValue("@box1", comboBox1.Text);
                cmdn22.Parameters.AddWithValue("@box2", textBox1.Text);
                cmdn22.Parameters.AddWithValue("@box3", textBox2.Text);
                cmdn22.Parameters.AddWithValue("@box4", textBox3.Text);
                if (textBox4.Text == "0.0" || textBox4.Text == "0" || textBox4.Text == "" || textBox4.Text == null)
                {
                    cmdn22.Parameters.AddWithValue("@box5", "لا");
                    debt = "لا";
                }
                else
                {
                    cmdn22.Parameters.AddWithValue("@box5", "نعم");
                    debt = "نعم";
                }
                cmdn22.Parameters.AddWithValue("@box6", textBox5.Text);
                cmdn22.Parameters.AddWithValue("@box7", textBox4.Text);
                cmdn22.Parameters.AddWithValue("@box8", textBox11.Text);
                cmdn22.Parameters.AddWithValue("@box9", textBox12.Text);
                co22.Open();
                SqlDataReader d569 = cmdn22.ExecuteReader();
                co22.Close();

                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                if (debt == "نعم")
                {
                    SqlConnection con5f5 = new SqlConnection(src);
                    SqlCommand cmd5f5 = new SqlCommand("INSERT INTO [StoreDebt](Date,Name,InvNo,Amount,idPurchase)VALUES (@textBox1,@textBox2,@textBox3,@textBox4,@text)", con5f5);
                    cmd5f5.Parameters.AddWithValue("@textBox1", textBox2.Text);
                    cmd5f5.Parameters.AddWithValue("@textBox2", comboBox1.Text);
                    cmd5f5.Parameters.AddWithValue("@textBox3", textBox1.Text);
                    cmd5f5.Parameters.AddWithValue("@textBox4", textBox4.Text);
                    cmd5f5.Parameters.AddWithValue("@text", idpur);
                    con5f5.Open();
                    SqlDataReader drf155 = cmd5f5.ExecuteReader();
                }
                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                string sitem = "";
                for (int j = 0; j < dataGridView2.RowCount - 1; j++)
                {
                    if (dataGridView2.Rows[j].Cells[0].Value != null && dataGridView2.Rows[j].Cells[1].Value != null && dataGridView2.Rows[j].Cells[2].Value != null && dataGridView2.Rows[j].Cells[4].Value != null)
                    {
                        if(dataGridView2.Rows[j].Cells[0].Value.ToString() == itemarr[j])
                        {
                            string srce = Program.xsrc;
                            SqlConnection co2266 = new SqlConnection(srce);
                            SqlCommand cmdn2266 = new SqlCommand("UPDATE [Items] SET CompanyName=@box1,InvoiceNo=@box2,Date=@box3,Price=@box4,Quantity=@box5,FullPrice=@box6,RQuantity=@box7,Notes=@box8 WHERE Id = '" + idarr[j] + "'", co2266);
                            cmdn2266.Parameters.AddWithValue("@box1", comboBox1.Text);
                            cmdn2266.Parameters.AddWithValue("@box2", textBox1.Text);
                            cmdn2266.Parameters.AddWithValue("@box3", textBox2.Text);
                            cmdn2266.Parameters.AddWithValue("@box4", dataGridView2.Rows[j].Cells[1].Value);
                            cmdn2266.Parameters.AddWithValue("@box5", dataGridView2.Rows[j].Cells[2].Value);
                            cmdn2266.Parameters.AddWithValue("@box6", dataGridView2.Rows[j].Cells[5].Value);
                            float trr = 0;
                            if (float.Parse(dataGridView2.Rows[j].Cells[7].Value.ToString()) < float.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString()))
                            {
                                float tr = float.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString()) - float.Parse(dataGridView2.Rows[j].Cells[7].Value.ToString());
                                trr = float.Parse(dataGridView2.Rows[j].Cells[3].Value.ToString()) + tr;
                                cmdn2266.Parameters.AddWithValue("@box7", trr);
                            }
                            else if (float.Parse(dataGridView2.Rows[j].Cells[7].Value.ToString()) > float.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString()))
                            {
                                float tr = float.Parse(dataGridView2.Rows[j].Cells[7].Value.ToString()) - float.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString());
                                trr = float.Parse(dataGridView2.Rows[j].Cells[3].Value.ToString()) - tr;
                                cmdn2266.Parameters.AddWithValue("@box7", trr);
                            }
                            else if (float.Parse(dataGridView2.Rows[j].Cells[7].Value.ToString()) == float.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString()))
                            {
                                trr = float.Parse(dataGridView2.Rows[j].Cells[3].Value.ToString());
                                cmdn2266.Parameters.AddWithValue("@box7", trr);
                            }
                            cmdn2266.Parameters.AddWithValue("@box8", dataGridView2.Rows[j].Cells[6].Value);
                            co2266.Open();
                            SqlDataReader d56966 = cmdn2266.ExecuteReader();
                            co2266.Close();
                            sitem = sitem + Environment.NewLine + "  الصنف  " + dataGridView2.Rows[j].Cells[0].Value.ToString() + "  السعر الفردي  " + dataGridView2.Rows[j].Cells[1].Value.ToString() + "  الكمية  " + dataGridView2.Rows[j].Cells[2].Value.ToString() + "  الحد الادنى  " + dataGridView2.Rows[j].Cells[4].Value.ToString() + "  السعر الكلي  " + dataGridView2.Rows[j].Cells[5].Value.ToString() + "  الملاحظات  " + dataGridView2.Rows[j].Cells[6].Value.ToString();
                            string qq = "";
                            string ididq = "";
                            string src = Program.xsrc; // path for DB
                            SqlConnection con7q = new SqlConnection(src);
                            con7q.Open();
                            SqlCommand cmd7q = new SqlCommand("select * from Inventory", con7q);
                            SqlDataReader dr7q = cmd7q.ExecuteReader();
                            while (dr7q.Read())
                            {
                                if (dr7q["Item"].ToString() == dataGridView2.Rows[j].Cells[0].Value.ToString())
                                {
                                    qq = dr7q["Quantity"].ToString();
                                    ididq = dr7q["Id"].ToString();
                                }
                            }
                            con7q.Close();
                            SqlConnection conn7q = new SqlConnection(srce);
                            SqlCommand cmdn7q = new SqlCommand("UPDATE [Inventory] SET  Quantity = @box2, MinQ = @box3, Notes = @box4 WHERE Id = '" + ididq + "'", conn7q);
                            cmdn7q.Parameters.AddWithValue("@box2", float.Parse(qq) + trr);
                            cmdn7q.Parameters.AddWithValue("@box3", dataGridView2.Rows[j].Cells[4].Value);
                            cmdn7q.Parameters.AddWithValue("@box4", dataGridView2.Rows[j].Cells[6].Value);
                            conn7q.Open();
                            SqlDataReader dr72q = cmdn7q.ExecuteReader();
                            conn7q.Close();
                        }
                        else
                        {
                            string srcc = Program.xsrc;
                            SqlConnection con66qq = new SqlConnection(srcc);
                            SqlCommand cmd66qq = new SqlCommand("INSERT INTO [Items](IdPurchase,ItemName,CompanyName,InvoiceNo,Date,Price,Quantity,MinQuantity,FullPrice,RQuantity,Notes)VALUES (@invid,@data0,@comboBox1,@textBox1,@textBox2,@data1,@data2,@data3,@data4,@data5,@N)", con66qq);
                            cmd66qq.Parameters.AddWithValue("@invid", idpur);
                            cmd66qq.Parameters.AddWithValue("@data0", dataGridView2.Rows[j].Cells[0].Value);
                            cmd66qq.Parameters.AddWithValue("@comboBox1", comboBox1.Text);
                            cmd66qq.Parameters.AddWithValue("@textBox1", textBox1.Text);
                            cmd66qq.Parameters.AddWithValue("@textBox2", textBox2.Text);
                            cmd66qq.Parameters.AddWithValue("@data1", dataGridView2.Rows[j].Cells[1].Value);
                            cmd66qq.Parameters.AddWithValue("@data2", dataGridView2.Rows[j].Cells[2].Value);
                            cmd66qq.Parameters.AddWithValue("@data3", dataGridView2.Rows[j].Cells[4].Value);
                            cmd66qq.Parameters.AddWithValue("@data4", dataGridView2.Rows[j].Cells[5].Value);
                            cmd66qq.Parameters.AddWithValue("@data5", dataGridView2.Rows[j].Cells[2].Value);///@@@@@@@@@@@@@@@@@@@@@@@@@
                            if (dataGridView2.Rows[j].Cells[6].Value != null)
                            {
                                cmd66qq.Parameters.AddWithValue("@N", dataGridView2.Rows[j].Cells[6].Value);
                                sitem = sitem + Environment.NewLine + "  الصنف  " + dataGridView2.Rows[j].Cells[0].Value.ToString() + "  السعر الفردي  " + dataGridView2.Rows[j].Cells[1].Value.ToString() + "  الكمية  " + dataGridView2.Rows[j].Cells[2].Value.ToString() + "  الحد الادنى  " + dataGridView2.Rows[j].Cells[4].Value.ToString() + "  السعر الكلي  " + dataGridView2.Rows[j].Cells[5].Value.ToString() + "  الملاحظات  " + dataGridView2.Rows[j].Cells[6].Value.ToString();

                            }
                            else
                            {
                                cmd66qq.Parameters.AddWithValue("@N", "لا يوجد");
                                sitem = sitem + Environment.NewLine + "  الصنف  " + dataGridView2.Rows[j].Cells[0].Value.ToString() + "  السعر الفردي  " + dataGridView2.Rows[j].Cells[1].Value.ToString() + "  الكمية  " + dataGridView2.Rows[j].Cells[2].Value.ToString() + "  الحد الادنى  " + dataGridView2.Rows[j].Cells[4].Value.ToString() + "  السعر الكلي  " + dataGridView2.Rows[j].Cells[5].Value.ToString() + "  الملاحظات  " + "لا يوجد";
                            }
                            // cmd66q.Parameters.AddWithValue("@text11", dataGridView1.Rows[j].Cells[5].Value);
                            con66qq.Open();
                            SqlDataReader dr66qq = cmd66qq.ExecuteReader();
                            con66qq.Close();
                            string q66 = "";
                            string mq66 = "";
                            string idid66 = "";
                            string note66 = "";
                            string src = Program.xsrc; // path for DB
                            SqlConnection con766 = new SqlConnection(src);
                            bool blnfound766 = false; // the username and pass correct (ana b76 enoh false cuz bfred enoh feh eroor bl user or pass)
                            con766.Open();
                            SqlCommand cmd766 = new SqlCommand("select * from Inventory", con766);
                            SqlDataReader dr766 = cmd766.ExecuteReader();
                            while (dr766.Read())
                            {
                                if (dr766["Item"].ToString() == dataGridView2.Rows[j].Cells[0].Value.ToString())
                                {
                                    blnfound766 = true;
                                    q66 = dr766["Quantity"].ToString();
                                    mq66 = dr766["MinQ"].ToString();
                                    idid66 = dr766["Id"].ToString();
                                    note66 = dr766["Notes"].ToString();
                                }
                            }
                            con766.Close();
                            if (blnfound766)
                            {
                                SqlConnection conn766 = new SqlConnection(src);
                                SqlCommand cmdn766 = new SqlCommand("UPDATE [Inventory] SET  Quantity = @box2, MinQ = @box3, Notes = @N WHERE Id = '" + idid66 + "'", conn766);
                                cmdn766.Parameters.AddWithValue("@box2", (float.Parse(q66)) + (float.Parse(dataGridView2.Rows[j].Cells[2].Value.ToString())));
                                cmdn766.Parameters.AddWithValue("@box3", dataGridView2.Rows[j].Cells[4].Value);
                                if (dataGridView2.Rows[j].Cells[6].Value != null)
                                {
                                    cmdn766.Parameters.AddWithValue("@N", dataGridView2.Rows[j].Cells[6].Value);
                                }
                                else
                                {
                                    cmdn766.Parameters.AddWithValue("@N", note66);
                                }
                                conn766.Open();
                                SqlDataReader dr7266 = cmdn766.ExecuteReader();
                                conn766.Close();
                            }
                            else
                            {
                                SqlConnection con66766 = new SqlConnection(src);
                                SqlCommand cmd66766 = new SqlCommand("INSERT INTO [Inventory](Item,Quantity,MinQ,Notes)VALUES (@data0,@data2,@data3,@N)", con66766);
                                cmd66766.Parameters.AddWithValue("@data0", dataGridView2.Rows[j].Cells[0].Value);
                                cmd66766.Parameters.AddWithValue("@data2", dataGridView2.Rows[j].Cells[2].Value);
                                cmd66766.Parameters.AddWithValue("@data3", dataGridView2.Rows[j].Cells[4].Value);
                                if (dataGridView2.Rows[j].Cells[6].Value != null)
                                {
                                    cmd66766.Parameters.AddWithValue("@N", dataGridView2.Rows[j].Cells[6].Value);
                                }
                                else
                                {
                                    cmd66766.Parameters.AddWithValue("@N", "لا يوجد");
                                }
                                con66766.Open();
                                SqlDataReader dr66766 = cmd66766.ExecuteReader();
                                con66766.Close();
                            }
                        }

                    }
                }


                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


                SqlConnection con6w = new SqlConnection(src);
                SqlCommand cmd6w = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con6w);
                cmd6w.Parameters.AddWithValue("@textBox1", namew);
                cmd6w.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd6w.Parameters.AddWithValue("@textBox3", " لقد تم تعديل فاتورة مشتريات " + ">>" + "  رمز الحركة " + idpur + "  اسم الشركة " + comboBox1.Text + "   " + "  رقم الفاتورة " + textBox1.Text + Environment.NewLine + "  التاريخ " + textBox2.Text + "   " + "  القيمة " + textBox3.Text + "   " + "  القيمة المدفوعة " + textBox5.Text + "   " + "   القيمة المتبقية " + textBox4.Text + "  ملاحظات " + textBox11.Text + Environment.NewLine + "  الاصناف  " + sitem);
                cmd6w.Parameters.AddWithValue("@textBox4", "UP");
                con6w.Open();
                SqlDataReader dr6w = cmd6w.ExecuteReader();
                MessageBox.Show("لقد تم تعديل الفاتورة بنجاح ");
                if (comboBox1.Items.Count > 1)
                {
                    comboBox1.Items.Clear();
                    comboBox3.Items.Clear();
                }
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox12.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox8.Text = "";
                textBox9.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                SqlConnection conn399q = new SqlConnection(src);
                conn399q.Open();
                SqlCommand cmd399q = new SqlCommand("select * from Purchases", conn399q);
                string iitem9q = "";
                SqlDataReader dr399q = cmd399q.ExecuteReader();
                comboBox3.Items.Add("");
                comboBox3.Items.Add("الكل");
                dataGridView1.Rows.Clear();
                while (dr399q.Read())
                {
                    iitem9q = dr399q["CompanyName"].ToString();

                    if (!comboBox1.Items.Contains(iitem9q))
                    {
                        comboBox1.Items.Add(iitem9q);
                        comboBox3.Items.Add(iitem9q);
                    }
                }
                dr399q.Close();


                if (comboBox2.Items.Count > 1)
                {
                    comboBox2.Items.Clear();
                }
                SqlConnection conn30q = new SqlConnection(src);
                conn30q.Open();
                SqlCommand cmd30q = new SqlCommand("select * from Inventory", conn30q);
                string item0q = "";
                SqlDataReader dr30q = cmd30q.ExecuteReader();
                comboBox2.Items.Add("");
                while (dr30q.Read())
                {
                    item0q = dr30q["Item"].ToString();

                    if (!comboBox2.Items.Contains(item0q))
                    {
                        comboBox2.Items.Add(item0q);
                    }
                }
                dr30q.Close();

                empty = true;
                dataGridView2.Rows.Clear();
                SqlConnection conn3930q = new SqlConnection(src);
                conn3930q.Open();
                SqlCommand cmd3930q = new SqlCommand("select * from Items", conn3930q);
                string iitemtt0q = "";
                SqlDataReader dr3930q = cmd3930q.ExecuteReader();
                while (dr3930q.Read())
                {
                    iitemtt0q = dr3930q["ItemName"].ToString();

                    if (!this.dataGridViewComboBoxColumn1.Items.Contains(iitemtt0q))
                    {
                        this.dataGridViewComboBoxColumn1.Items.Add(iitemtt0q);
                    }

                }
                dr3930q.Close();
                for (int ro = 0; ro < 15; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                    dataGridView2.Rows.Add(null, null, null, null, null, null, null, null);
                }

            }
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        {
            int z = 0;
            string box = textBox12.Text;
            try
            {
                if (!empty)
                {
                    if (dataGridView2.Rows.Count > 1)
                    {
                        textBox12.Text = "";
                        textBox3.Text = "0.0";
                        for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                        {
                            if ((dataGridView2.CurrentRow.Index >= 0 && dataGridView2.Rows[i].Cells[1].Value != null && dataGridView2.Rows[i].Cells[2].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView2.Rows[i].Cells[1].Value.ToString());
                                float b = float.Parse(dataGridView2.Rows[i].Cells[2].Value.ToString());
                                float c = a * b;
                                dataGridView2.Rows[i].Cells[5].Value = c;

                                textBox3.Text = (float.Parse(textBox3.Text) + float.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString())).ToString();
                            }
                        }
                        textBox12.Text = box;
                        textBox12_Leave(sender, e);
                        if (textBox4.Text != "0.0" || textBox4.Text != "0")
                        { textBox4.Text = ((float.Parse(textBox3.Text)) - (float.Parse(textBox5.Text))).ToString(); }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView2.Rows[z].Cells[1].Value = 0.0;
                dataGridView2.Rows[z].Cells[2].Value = 0.0;
                dataGridView2.Rows[z].Cells[5].Value = 0.0;
                textBox12.Text = box;
                textBox12_Leave(sender, e);
            }
        }

        private void dataGridView2_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == dataGridViewComboBoxColumn1.DisplayIndex)
            {
                if (!this.dataGridViewComboBoxColumn1.Items.Contains(e.FormattedValue))
                {
                    this.dataGridViewComboBoxColumn1.Items.Add(e.FormattedValue);
                    dataGridView2.Rows[dataGridView2.CurrentCellAddress.Y].Cells[0].Value = e.FormattedValue;

                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Items.Count > 1)
            {
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox12.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            comboBox3.SelectedText = "";
            dateTimePicker1.Checked = false;
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            SqlConnection conn399qq = new SqlConnection(src);
            conn399qq.Open();
            SqlCommand cmd399qq = new SqlCommand("select * from Purchases", conn399qq);
            string iitem9qq = "";
            SqlDataReader dr399qq = cmd399qq.ExecuteReader();
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            dataGridView1.Rows.Clear();
            while (dr399qq.Read())
            {
                iitem9qq = dr399qq["CompanyName"].ToString();

                if (!comboBox1.Items.Contains(iitem9qq))
                {
                    comboBox1.Items.Add(iitem9qq);
                    comboBox3.Items.Add(iitem9qq);
                }
            }
            dr399qq.Close();


            if (comboBox2.Items.Count > 1)
            {
                comboBox2.Items.Clear();
            }
            SqlConnection conn30qq = new SqlConnection(src);
            conn30qq.Open();
            SqlCommand cmd30qq = new SqlCommand("select * from Inventory", conn30qq);
            string item0qq = "";
            SqlDataReader dr30qq = cmd30qq.ExecuteReader();
            comboBox2.Items.Add("");
            while (dr30qq.Read())
            {
                item0qq = dr30qq["Item"].ToString();

                if (!comboBox2.Items.Contains(item0qq))
                {
                    comboBox2.Items.Add(item0qq);
                }
            }
            dr30qq.Close();

            empty = true;
            dataGridView2.Rows.Clear();
            SqlConnection conn3930qq = new SqlConnection(src);
            conn3930qq.Open();
            SqlCommand cmd3930qq = new SqlCommand("select * from Items", conn3930qq);
            string iitemtt0qq = "";
            SqlDataReader dr3930qq = cmd3930qq.ExecuteReader();
            while (dr3930qq.Read())
            {
                iitemtt0qq = dr3930qq["ItemName"].ToString();

                if (!this.dataGridViewComboBoxColumn1.Items.Contains(iitemtt0qq))
                {
                    this.dataGridViewComboBoxColumn1.Items.Add(iitemtt0qq);
                }

            }
            dr3930qq.Close();
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null, null);
                dataGridView2.Rows.Add(null, null, null, null, null, null, null, null);
            }
            comboBox3.Focus();
        }
        
        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item = comboBox3.Text;
                string[] filteredItems = items.Where(x => x.Contains(item)).ToArray();
                comboBox3.Items.Clear();
                comboBox3.Items.Add(item);
                comboBox3.Items.AddRange(filteredItems);
                comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox3.DroppedDown = true;
                comboBox3.SelectionStart = item.Length;
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

        private void dataGridView2_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            if (dataGridView2.CurrentCellAddress.X == 0 && dataGridView2.CurrentRow.Cells[0].Value != null)
            {
                string itid = "";
                string itq = "";
                SqlConnection conn31 = new SqlConnection(src);
                conn31.Open();
                SqlCommand cmd31 = new SqlCommand("select * from Inventory WHERE Item = @Name", conn31);
                cmd31.Parameters.AddWithValue("@Name", dataGridView2.CurrentRow.Cells[0].Value.ToString());
                SqlDataReader dr81 = cmd31.ExecuteReader();
                while (dr81.Read())
                {
                    itid = dr81["MinQ"].ToString();
                    itq = dr81["Notes"].ToString();
                }
                dr81.Close();
                this.dataGridView2.CurrentRow.HeaderCell.Value = (dataGridView2.CurrentCellAddress.Y + 1).ToString();
                dataGridView2.CurrentRow.Cells[4].Value = itid;
                dataGridView2.CurrentRow.Cells[6].Value = itq;
            }

        }

        private void textBox12_Leave(object sender, EventArgs e)
        {

            dis = 0;
            per = 0;
            try
            {
                for (int r = 0; r < dataGridView2.RowCount - 1; r++)
                {
                    if (dataGridView2.Rows[r].Cells[5].Value != null)
                    { dis = dis + float.Parse(dataGridView2.Rows[r].Cells[5].Value.ToString()); }
                }
                if (textBox12.Text != "")
                {
                    if (float.Parse(textBox12.Text) == 0)
                    {
                        textBox3.Text = dis.ToString();
                        dis = 0;
                        per = 0;
                    }
                    else
                    {
                        textBox3.Text = (dis - float.Parse(textBox12.Text)).ToString();
                        //per = ((float.Parse(textBox12.Text) * 100) / dis) / 100;
                        if (float.Parse(textBox3.Text) < 0)
                        {
                            MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                            textBox3.Text = dis.ToString();
                            textBox12.Text = "0.0";
                            dis = 0;
                            per = 0;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                textBox12.Text = "0.0";
            }
        }
    }
}
