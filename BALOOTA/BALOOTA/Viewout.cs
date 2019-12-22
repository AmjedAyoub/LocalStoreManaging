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
    public partial class Viewout : Form
    {
        public Viewout()
        {
            InitializeComponent();
        }
        private string src = Program.xsrc;
        int row1 = 0;
        public int rowindex1;
        string[] items;
        string[] items1;
        
        public void Viewout_Load(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            comboBox4.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            comboBox3.SelectedText = "";
            comboBox4.SelectedText = "";
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            if (comboBox4.Items.Count > 1)
            { comboBox4.Items.Clear();}
            comboBox4.Items.Add("");
            comboBox4.Items.Add("الكل");
            SqlConnection conn394 = new SqlConnection(src);
            conn394.Open();
            SqlCommand cmd394 = new SqlCommand("select * from StoreOut", conn394);
            string iitem4 = "";
            SqlDataReader dr394 = cmd394.ExecuteReader();
            while (dr394.Read())
            {
                iitem4 = dr394["Name"].ToString();

                if (!comboBox4.Items.Contains(iitem4))
                {
                    comboBox4.Items.Add(iitem4); 
                }
            }
            dr394.Close();
            if (comboBox3.Items.Count > 1)
            { comboBox3.Items.Clear(); }
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from Employee", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            while (dr39.Read())
            {
                iitem = dr39["EmployeeName"].ToString();

                if (!comboBox3.Items.Contains(iitem) && iitem !="المبرمج")
                {
                    comboBox3.Items.Add(iitem);
                }
            }
            dr39.Close();
            items = new string[comboBox3.Items.Count];
            comboBox3.Items.CopyTo(items, 0);
            items1 = new string[comboBox4.Items.Count];
            comboBox4.Items.CopyTo(items1, 0);
            dataGridView1.Rows.Clear();

            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(null, null, null, null, null);
            }
            dataGridView2.Rows.Clear();

            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView2.Rows.Add(null, null, null, null, null);
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

            string theDate11 = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            textBox8.Text = theDate11.ToString();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            string theDate21 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox1.Text = theDate21.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            string theDate13 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            textBox2.Text = theDate13.ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox3.Text == "" && textBox8.Text == "" && textBox10.Text == "")
            { MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا"); 
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "EOV";
                string page = "EOV";
                Program.mysignin.which(ww, page);
            }
        }

        public void EOV()
        {
            row1 = 0;
            try
            {
                if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text == "" && textBox10.Text == "")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from EmpOut", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr.Read())
                    {
                        if (dr["Name"].ToString() == comboBox3.Text)

                        {
                            dataGridView1.Rows.Insert(row1, dr["Id"].ToString(), dr["Date"].ToString(), dr["Name"].ToString(), dr["Amount"].ToString(), dr["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con4 = new SqlConnection(src);
                    con4.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from EmpOut", con4);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr4.Read())
                    {
                        if (dr4["Name"].ToString() == comboBox3.Text && DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            dataGridView1.Rows.Insert(row1, dr4["Id"].ToString(), dr4["Date"].ToString(), dr4["Name"].ToString(), dr4["Amount"].ToString(), dr4["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con5 = new SqlConnection(src);
                    con5.Open();
                    SqlCommand cmd5 = new SqlCommand("select * from EmpOut", con5);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr5.Read())
                    {
                        if (dr5["Name"].ToString() == comboBox3.Text && DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, dr5["Id"].ToString(), dr5["Date"].ToString(), dr5["Name"].ToString(), dr5["Amount"].ToString(), dr5["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con6 = new SqlConnection(src);
                    con6.Open();
                    SqlCommand cmd6 = new SqlCommand("select * from EmpOut", con6);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr6.Read())
                    {
                        if (dr6["Name"].ToString() == comboBox3.Text && DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox8.Text) )

                        {
                            dataGridView1.Rows.Insert(row1, dr6["Id"].ToString(), dr6["Date"].ToString(), dr6["Name"].ToString(), dr6["Amount"].ToString(), dr6["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con7 = new SqlConnection(src);
                    con7.Open();
                    SqlCommand cmd7 = new SqlCommand("select * from EmpOut", con7);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr7.Read())
                    {
                        if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox10.Text) )

                        {
                            dataGridView1.Rows.Insert(row1, dr7["Id"].ToString(), dr7["Date"].ToString(), dr7["Name"].ToString(), dr7["Amount"].ToString(), dr7["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con8 = new SqlConnection(src);
                    con8.Open();
                    SqlCommand cmd8 = new SqlCommand("select * from EmpOut", con8);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr8.Read())
                    {
                        if (DateTime.Parse(dr8["Date"].ToString()) <= DateTime.Parse(textBox8.Text) )

                        {
                            dataGridView1.Rows.Insert(row1, dr8["Id"].ToString(), dr8["Date"].ToString(), dr8["Name"].ToString(), dr8["Amount"].ToString(), dr8["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con9 = new SqlConnection(src);
                    con9.Open();
                    SqlCommand cmd9 = new SqlCommand("select * from EmpOut", con9);
                    SqlDataReader dr9 = cmd9.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr9.Read())
                    {
                        if (DateTime.Parse(dr9["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr9["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, dr9["Id"].ToString(), dr9["Date"].ToString(), dr9["Name"].ToString(), dr9["Amount"].ToString(), dr9["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from EmpOut", con10);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr10.Read())
                    {
                        dataGridView1.Rows.Insert(row1, dr10["Id"].ToString(), dr10["Date"].ToString(), dr10["Name"].ToString(), dr10["Amount"].ToString(), dr10["Notes"].ToString());
                        row1++;
                        
                    }

                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from EmpOut", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["Amount"].ToString(), dr1["Notes"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && textBox8.Text == ""  && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from EmpOut", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if ( DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text) )

                        {
                            dataGridView1.Rows.Insert(row1, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["Amount"].ToString(), dr1["Notes"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from EmpOut", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text) )

                        {
                            dataGridView1.Rows.Insert(row1, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["Amount"].ToString(), dr1["Notes"].ToString());
                            row1++;
                        }
                    }
                }
                if (row1 > 0)
                {
                    float sum1 = 0;
                    for (int k1 = 0; k1 <= row1 - 1; k1++)
                    {
                        sum1 = sum1 + float.Parse(dataGridView1.Rows[k1].Cells[3].Value.ToString());
                    }
                    textBox4.Text = sum1.ToString();
                }
                this.dataGridView1.Sort(this.dataGridView1.Columns[1], ListSortDirection.Descending);
                for (int y = 0; y < row1; y++)
                {
                    this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }
            }
            catch { MessageBox.Show("الرجاء التاكد من معلومات البحث"); }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox4.Text == "" && textBox2.Text == "" && textBox1.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "SOV";
                string page = "SOV";
                Program.mysignin.which(ww, page);
            }
        }

        public void SOV()
        {
            row1 = 0;
            try
            {
                if (comboBox4.Text != "" && comboBox4.Text != "الكل" && textBox2.Text == "" && textBox1.Text == "")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from StoreOut", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr.Read())
                    {
                        if (dr["Name"].ToString() == comboBox4.Text)

                        {
                            dataGridView2.Rows.Insert(row1, dr["Id"].ToString(), dr["Name"].ToString(), dr["Date"].ToString(), dr["Amount"].ToString(), dr["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && textBox2.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con4 = new SqlConnection(src);
                    con4.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from StoreOut", con4);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr4.Read())
                    {
                        if (dr4["Name"].ToString() == comboBox4.Text && DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr4["Id"].ToString(), dr4["Name"].ToString(), dr4["Date"].ToString(), dr4["Amount"].ToString(), dr4["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && textBox2.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con5 = new SqlConnection(src);
                    con5.Open();
                    SqlCommand cmd5 = new SqlCommand("select * from StoreOut", con5);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr5.Read())
                    {
                        if (dr5["Name"].ToString() == comboBox4.Text && DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr5["Id"].ToString(), dr5["Name"].ToString(), dr5["Date"].ToString(), dr5["Amount"].ToString(), dr5["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.Text != "" && comboBox4.Text != "الكل" && textBox2.Text != "" && textBox1.Text != "")
                {
                    SqlConnection con6 = new SqlConnection(src);
                    con6.Open();
                    SqlCommand cmd6 = new SqlCommand("select * from StoreOut", con6);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr6.Read())
                    {
                        if (dr6["Name"].ToString() == comboBox4.Text && DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox1.Text) && DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr6["Id"].ToString(), dr6["Name"].ToString(), dr6["Date"].ToString(), dr6["Amount"].ToString(), dr6["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con7 = new SqlConnection(src);
                    con7.Open();
                    SqlCommand cmd7 = new SqlCommand("select * from StoreOut", con7);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr7.Read())
                    {
                        if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr7["Id"].ToString(), dr7["Name"].ToString(), dr7["Date"].ToString(), dr7["Amount"].ToString(), dr7["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con8 = new SqlConnection(src);
                    con8.Open();
                    SqlCommand cmd8 = new SqlCommand("select * from StoreOut", con8);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr8.Read())
                    {
                        if (DateTime.Parse(dr8["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr8["Id"].ToString(), dr8["Name"].ToString(), dr8["Date"].ToString(), dr8["Amount"].ToString(), dr8["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text != "" && textBox1.Text != "")
                {
                    SqlConnection con9 = new SqlConnection(src);
                    con9.Open();
                    SqlCommand cmd9 = new SqlCommand("select * from StoreOut", con9);
                    SqlDataReader dr9 = cmd9.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr9.Read())
                    {
                        if (DateTime.Parse(dr9["Date"].ToString()) >= DateTime.Parse(textBox1.Text) && DateTime.Parse(dr9["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr9["Id"].ToString(), dr9["Name"].ToString(), dr9["Date"].ToString(), dr9["Amount"].ToString(), dr9["Notes"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox4.SelectedItem.ToString() == "الكل" && textBox2.Text == "" && textBox1.Text == "")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from StoreOut", con10);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr10.Read())
                    {
                        dataGridView2.Rows.Insert(row1, dr10["Id"].ToString(), dr10["Name"].ToString(), dr10["Date"].ToString(), dr10["Amount"].ToString(), dr10["Notes"].ToString());
                        row1++;

                    }

                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && textBox2.Text != "" && textBox1.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from StoreOut", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox2.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["Name"].ToString(), dr1["Date"].ToString(), dr1["Amount"].ToString(), dr1["Notes"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && textBox2.Text == "" && textBox1.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from StoreOut", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["Name"].ToString(), dr1["Date"].ToString(), dr1["Amount"].ToString(), dr1["Notes"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox4.Text == "" && comboBox4.Text != "الكل" && textBox2.Text != "" && textBox1.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from StoreOut", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox2.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox1.Text))

                        {
                            dataGridView2.Rows.Insert(row1, dr1["Id"].ToString(), dr1["Name"].ToString(), dr1["Date"].ToString(), dr1["Amount"].ToString(), dr1["Notes"].ToString());
                            row1++;
                        }
                    }
                }
                if (row1 > 0)
                {
                    float sum12 = 0;
                    for (int k12 = 0; k12 <= row1 - 1; k12++)
                    {
                        sum12 = sum12 + float.Parse(dataGridView2.Rows[k12].Cells[3].Value.ToString());
                    }
                    textBox5.Text = sum12.ToString();
                }
                this.dataGridView2.Sort(this.dataGridView2.Columns[2], ListSortDirection.Descending);
                for (int y = 0; y < row1; y++)
                {
                    this.dataGridView2.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }
            }
            catch { MessageBox.Show("الرجاء التاكد من معلومات البحث"); }
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

        private void comboBox4_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item1 = comboBox4.Text;
                string[] filteredItems1 = items1.Where(x => x.Contains(item1)).ToArray();
                comboBox4.Items.Clear();
                comboBox4.Items.Add(item1);
                comboBox4.Items.AddRange(filteredItems1);
                comboBox4.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox4.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox4.DroppedDown = true;
                comboBox4.SelectionStart = item1.Length;
                comboBox4.SelectionLength = 0;

                comboBox4.Cursor = Cursor.Current;
            }
        }
    }
}
