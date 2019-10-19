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
    public partial class Sales : Form
    {
        public Sales()
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
        public string[] itemarr1 = new string[1000000];
        public string[] idarr = new string[1000000];
        public List<string> itemid = new List<string>();
        public List<string> sale = new List<string>();
        private string src = Program.xsrc;
        bool show = false;
        public string usern = "";
        public string[] profitarr = new string[1000000];
        int rowarr = 0;
        string it1 = "";
        bool enough = false;
        bool enough2 = false;
        float p = 0;
        float rd = 0;
        float profit = 0;
        string rdd = "";
        string sitem = "";
        string name3 = "";
        string inv = "";
        string[] items;
        string[] items1;
        string[] items2;
        float q = 0;
        float dis = 0;
        float per = 0;

        public void S()
        {
            if (comboBox1.Items.Count > 1)
            {
                comboBox1.Items.Clear();
                comboBox3.Items.Clear();
            }
            if (comboBox2.Items.Count > 1)
            {
                comboBox2.Items.Clear();
            }
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox13.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            textBox6.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            checkBox1.Checked = false;
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            comboBox3.SelectedText = "";
            dateTimePicker1.Checked = false;
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            show = false;
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from SoldItems", conn39);
            string iitem = "";
            string item = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            comboBox2.Items.Add("");
            comboBox1.Items.Add("");
            dataGridView1.Rows.Clear();
            while (dr39.Read())
            {
                iitem = dr39["Name"].ToString();
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
            empty = true;
            dataGridView1.Rows.Clear();
            dataGridView2.Rows.Clear();
            SqlConnection conn393 = new SqlConnection(src);
            conn393.Open();
            SqlCommand cmd393 = new SqlCommand("select * from Items", conn393);
            string iitemtt = "";
            string cname = "";
            SqlDataReader dr393 = cmd393.ExecuteReader();
            this.dataGridViewComboBoxColumn1.Items.Add("");
            this.Column2.Items.Add("");
            while (dr393.Read())
            {
                iitemtt = dr393["ItemName"].ToString();
                cname = dr393["CompanyName"].ToString();
                item = dr393["ItemName"].ToString();

                if (!this.dataGridViewComboBoxColumn1.Items.Contains(iitemtt))
                {
                    this.dataGridViewComboBoxColumn1.Items.Add(iitemtt);
                }
                if (!this.Column2.Items.Contains(cname))
                {
                    this.Column2.Items.Add(cname);
                }
                if (!comboBox2.Items.Contains(item))
                {
                    comboBox2.Items.Add(item);
                }

            }
            dr393.Close();
            items2 = new string[comboBox2.Items.Count];
            comboBox2.Items.CopyTo(items2, 0);
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null, null, null, null, null);
                dataGridView2.Rows.Add(null, null, null, null, null);
            }
            comboBox3.Focus();
        }

        public void Sales_Load(object sender, EventArgs e)
        {
            S();
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
            if (e.ColumnIndex == Column2.DisplayIndex)
            {
                if (!this.Column2.Items.Contains(e.FormattedValue))
                {
                    this.Column2.Items.Add(e.FormattedValue);
                    dataGridView2.Rows[dataGridView2.CurrentCellAddress.Y].Cells[1].Value = e.FormattedValue;

                }
            }
        }

        private void dataGridView2_CurrentCellChanged(object sender, EventArgs e)
        { int z = 0;
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
                            if ((dataGridView2.CurrentRow.Index >= 0 && dataGridView2.Rows[i].Cells[3].Value != null && dataGridView2.Rows[i].Cells[3].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView2.Rows[i].Cells[3].Value.ToString());
                                float b = float.Parse(dataGridView2.Rows[i].Cells[4].Value.ToString());
                                float c = a * b;
                                dataGridView2.Rows[i].Cells[5].Value = c;

                                textBox3.Text = (float.Parse(textBox3.Text) + float.Parse(dataGridView2.Rows[i].Cells[5].Value.ToString())).ToString();
                            }
                        }
                        textBox12.Text = box;
                        textBox12_Leave(sender,e);
                        if (textBox4.Text != "0.0" || textBox4.Text != "0")
                        { textBox4.Text = ((float.Parse(textBox3.Text)) - (float.Parse(textBox5.Text))).ToString(); }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView2.Rows[z].Cells[3].Value = 0.0;
                dataGridView2.Rows[z].Cells[4].Value = 0.0;
                dataGridView2.Rows[z].Cells[5].Value = 0.0;
                textBox12.Text = box;
                textBox12_Leave(sender, e);
            }
        }

        private void dataGridView2_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView2.CurrentCellAddress.X == Column2.DisplayIndex)
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
                string ww = "SHS";
                string page = "SHS";
                Program.mysignin.which(ww, page);
            }

        }

        public void shs()
        {

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
                            dataGridView1.Rows.Insert(row11, false, dr["Id"].ToString(), dr["Date"].ToString(), dr["CompanyName"].ToString(), dr["Amount"].ToString(), dr["Debt"].ToString(), dr["Paid"].ToString(), dr["RDebt"].ToString(), dr["Notes"].ToString(), dr["Profit"].ToString(), dr["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(),dr2["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr3["Id"].ToString(), dr3["Date"].ToString(), dr3["CompanyName"].ToString(), dr3["Amount"].ToString(), dr3["Debt"].ToString(), dr3["Paid"].ToString(), dr3["RDebt"].ToString(), dr3["Notes"].ToString(), dr3["Profit"].ToString(),dr3["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr4["Id"].ToString(), dr4["Date"].ToString(), dr4["CompanyName"].ToString(), dr4["Amount"].ToString(), dr4["Debt"].ToString(), dr4["Paid"].ToString(), dr4["RDebt"].ToString(), dr4["Notes"].ToString(), dr4["Profit"].ToString(),dr4["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr5["Id"].ToString(), dr5["Date"].ToString(), dr5["CompanyName"].ToString(), dr5["Amount"].ToString(), dr5["Debt"].ToString(), dr5["Paid"].ToString(), dr5["RDebt"].ToString(), dr5["Notes"].ToString(), dr5["Profit"].ToString(),dr5["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr6["Id"].ToString(), dr6["Date"].ToString(), dr6["CompanyName"].ToString(), dr6["Amount"].ToString(), dr6["Debt"].ToString(), dr6["Paid"].ToString(), dr6["RDebt"].ToString(), dr6["Notes"].ToString(), dr6["Profit"].ToString(),dr6["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr7["Id"].ToString(), dr7["Date"].ToString(), dr7["CompanyName"].ToString(), dr7["Amount"].ToString(), dr7["Debt"].ToString(), dr7["Paid"].ToString(), dr7["RDebt"].ToString(), dr7["Notes"].ToString(), dr7["Profit"].ToString(),dr7["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr8["Id"].ToString(), dr8["Date"].ToString(), dr8["CompanyName"].ToString(), dr8["Amount"].ToString(), dr8["Debt"].ToString(), dr8["Paid"].ToString(), dr8["RDebt"].ToString(), dr8["Notes"].ToString(), dr8["Profit"].ToString(),dr8["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr9["Id"].ToString(), dr9["Date"].ToString(), dr9["CompanyName"].ToString(), dr9["Amount"].ToString(), dr9["Debt"].ToString(), dr9["Paid"].ToString(), dr9["RDebt"].ToString(), dr9["Notes"].ToString(), dr9["Profit"].ToString(),dr9["Dis"].ToString());
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
                            dataGridView1.Rows.Insert(row11, false, dr10["Id"].ToString(), dr10["Date"].ToString(), dr10["CompanyName"].ToString(), dr10["Amount"].ToString(), dr10["Debt"].ToString(), dr10["Paid"].ToString(), dr10["RDebt"].ToString(), dr10["Notes"].ToString(), dr10["Profit"].ToString(),dr10["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(),dr2["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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

                            dataGridView1.Rows.Insert(row11, false, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["CompanyName"].ToString(), dr1["Amount"].ToString(), dr1["Debt"].ToString(), dr1["Paid"].ToString(), dr1["RDebt"].ToString(), dr1["Notes"].ToString(), dr1["Profit"].ToString(), dr1["Dis"].ToString());
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
                                    dataGridView1.Rows.Insert(row11, false, dr2["Id"].ToString(), dr2["Date"].ToString(), dr2["CompanyName"].ToString(), dr2["Amount"].ToString(), dr2["Debt"].ToString(), dr2["Paid"].ToString(), dr2["RDebt"].ToString(), dr2["Notes"].ToString(), dr2["Profit"].ToString(), dr2["Dis"].ToString());
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

        public void Delete(bool ok, string name1)
        {
            string it = "";
            if (ok)
            {
                string pp = "";
                if (!checkBox1.Checked)
                {
                    for (int k = 0; k < itemarr.Length - 1; k++)
                    {
                        if (itemarr[k] != null && itemarr[k] != "")
                        {
                            string itid = "";
                            float itq = 0;
                            SqlConnection conn31 = new SqlConnection(src);
                            conn31.Open();
                            SqlCommand cmd31 = new SqlCommand("select * from Inventory WHERE Item = @Name", conn31);
                            cmd31.Parameters.AddWithValue("@Name", itemarr[k]);
                            SqlDataReader dr81 = cmd31.ExecuteReader();
                            while (dr81.Read())
                            {
                                itid = dr81["Id"].ToString();
                                itq = float.Parse(dr81["Quantity"].ToString());
                            }
                            dr81.Close();
                            float total1 = itq + float.Parse(idarr[k]);
                            SqlConnection conn = new SqlConnection(src);
                            SqlCommand cmdn = new SqlCommand("UPDATE [Inventory] SET Quantity=@box1 WHERE Id = '" + itid + "'", conn);
                            cmdn.Parameters.AddWithValue("@box1", total1);
                            conn.Open();
                            SqlDataReader d72 = cmdn.ExecuteReader();
                            conn.Close();
                        }
                    }
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[7].Value != null)
                        {
                            pp = pp + dataGridView2.Rows[i].Cells[7].Value.ToString();
                        }
                    }
                    sale = pp.Split('@').ToList<string>();
                    for (int arr = 0; arr < sale.Count; arr++)
                    {
                        itemid = sale[arr].Split('>').ToList<string>();
                        for (int arr1 = 0; arr1 < itemid.Count; arr1 = +4)
                        {
                            if (itemid[arr1] != null && itemid[arr1] != "")
                            {

                                SqlConnection conn33162 = new SqlConnection(src);
                                conn33162.Open();
                                SqlCommand cmd33162 = new SqlCommand("select * from Items WHERE Id ='" + itemid[arr1] + "'", conn33162);
                                SqlDataReader dr83162 = cmd33162.ExecuteReader();
                                float reg = 0;
                                while (dr83162.Read())
                                {
                                    reg = float.Parse(dr83162["RQuantity"].ToString());
                                }
                                if (reg > -1)
                                {
                                  //  MessageBox.Show(itemid[arr1] + "  >>>  " + itemid[arr1 + 1] + "  >>>  " + itemid[arr1 + 2] + "  >>>  " + itemid[arr1 + 3]);
                                    SqlConnection co2 = new SqlConnection(src);
                                    SqlCommand cmdn2 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + int.Parse(itemid[arr1]) + "'", co2);
                                    cmdn2.Parameters.AddWithValue("@box1", int.Parse(itemid[arr1 + 3]) + reg);
                                    co2.Open();
                                    SqlDataReader d555 = cmdn2.ExecuteReader();
                                    co2.Close();


                                }
                                else
                                {
                                  //  MessageBox.Show(itemid[arr1] + "  >>>  " + itemid[arr1 + 1] + "  >>>  " + itemid[arr1 + 2] + "  >>>  " + itemid[arr1 + 3]);
                                    SqlConnection co2 = new SqlConnection(src);
                                    SqlCommand cmdn2 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + int.Parse(itemid[arr1]) + "'", co2);
                                    cmdn2.Parameters.AddWithValue("@box1", itemid[arr1 + 3]);
                                    co2.Open();
                                    SqlDataReader d555 = cmdn2.ExecuteReader();
                                    co2.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    SqlConnection con515 = new SqlConnection(src);
                    SqlCommand cmd515 = new SqlCommand("INSERT INTO [Destroy](Date,Amount)VALUES (@textBox1,@textBox2)", con515);
                    cmd515.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                    cmd515.Parameters.AddWithValue("@textBox2", float.Parse(textBox3.Text) - float.Parse(textBox13.Text));
                    con515.Open();
                    SqlDataReader dr1515 = cmd515.ExecuteReader();
                }
                float amount = 0;
                float total = 0;
                SqlConnection conne = new SqlConnection(src);
                conne.Open();
                SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                int regid1 = Convert.ToInt32(cmod.ExecuteScalar());
                SqlConnection conn3316 = new SqlConnection(src);
                conn3316.Open();
                SqlCommand cmd3316 = new SqlCommand("select * from Register WHERE Id ='" + regid1 + "'", conn3316);
                SqlDataReader dr8316 = cmd3316.ExecuteReader();
                while (dr8316.Read())
                {
                    amount = float.Parse(dr8316["Amount"].ToString());
                }
                total = amount - float.Parse(textBox6.Text);
                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                cmd55.Parameters.AddWithValue("@textBox2", total);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                SqlConnection conn331 = new SqlConnection(src);
                conn331.Open();
                SqlCommand cmd331 = new SqlCommand("select * from Sales WHERE Id ='" + textBox1.Text + "'", conn331);
                SqlDataReader dr831 = cmd331.ExecuteReader();
                string inv = "";
                while (dr831.Read())
                {
                    inv = " لقد تم حذف أو إرجاع فاتورة مبيعات " + ">>" + "  رمز الحركة " + textBox1.Text + "  الاسم " + dr831["CompanyName"].ToString() + "   " + "  رقم الفاتورة " + dr831["Id"].ToString() + Environment.NewLine + "  التاريخ " + dr831["Date"].ToString() + "   " + "  القيمة " + dr831["Amount"].ToString() + "   " + "  القيمة المدفوعة " + dr831["Paid"].ToString() + "   " + "   القيمة المتبقية " + dr831["RDebt"].ToString() + "  ملاحظات " + dr831["Notes"].ToString() + Environment.NewLine + "  الاصناف  ";
                }

                SqlConnection conn33 = new SqlConnection(src);
                conn33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from SoldItems WHERE IdSale ='" + textBox1.Text + "'", conn33);
                SqlDataReader dr83 = cmd33.ExecuteReader();

                while (dr83.Read())
                {
                    it = it + Environment.NewLine + "  الصنف  " + dr83["ItemName"].ToString() + "  السعر الفردي  " + dr83["Price"].ToString() + "  الكمية  " + dr83["Quantity"].ToString() + "  السعر الكلي  " + dr83["FullPrice"].ToString();
                }
                dr83.Close();

                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [SaleDebt] WHERE idSales = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", textBox1.Text);
                cn111.Open();
                SqlDataReader dr111 = cmd111.ExecuteReader();

                SqlConnection cn1 = new SqlConnection(src);
                SqlCommand cmd1 = new SqlCommand("DELETE FROM [SoldItems] WHERE IdSale = @Box1", cn1);
                cmd1.Parameters.AddWithValue("@Box1", textBox1.Text);
                cn1.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                SqlConnection cn12 = new SqlConnection(src);
                SqlCommand cmd12 = new SqlCommand("DELETE FROM [Sales] WHERE Id = @Box12", cn12);
                cmd12.Parameters.AddWithValue("@Box12", textBox1.Text);
                cn12.Open();
                SqlDataReader dr11 = cmd12.ExecuteReader();

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", name1);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", inv + it);
                if (!checkBox1.Checked)
                {
                    cmd.Parameters.AddWithValue("@textBox4", "DEL");
                }
                else
                {
                    cmd.Parameters.AddWithValue("@textBox4", "DELA");
                }
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();
                MessageBox.Show("لقد تم حذف أو إرجاع الفاتورة بنجاح ");
                S();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (show)
            {
                if ((MessageBox.Show("هل انت متأكد من حذف أو إرجاع الفاتورة ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    if (checkBox1.Checked)
                    {
                        if ((MessageBox.Show("هل انت متأكد ان اصناف الفاتورة كلها فاسدة ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            Program.mysignin.Show();
                            Program.mysignin.Signin_Load(sender, e);
                            string ww = "DSale";
                            string page = "DSale";
                            Program.mysignin.which(ww, page);
                        }
                    }
                    else
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "DSale";
                        string page = "DSale";
                        Program.mysignin.which(ww, page);
                    }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(select && dataGridView1.Rows[rowindex1].Cells[1].Value!=null)
            {
                SqlConnection conn3 = new SqlConnection(src);
                conn3.Open();
              //  n = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                SqlCommand cmd3 = new SqlCommand("select * from Sales WHERE Id ='" + dataGridView1.Rows[rowindex1].Cells[1].Value.ToString() + "'", conn3);
                idpur = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
               // paid = dataGridView1.Rows[rowindex1].Cells[7].Value.ToString();
                SqlDataReader dr8 = cmd3.ExecuteReader();
                while (dr8.Read())
                {
                    id = dr8["Id"].ToString();
                }
                dr8.Close();
                comboBox1.SelectedItem = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox3.Text = (float.Parse(dataGridView1.Rows[rowindex1].Cells[10].Value.ToString()) + float.Parse(dataGridView1.Rows[rowindex1].Cells[4].Value.ToString())).ToString();
                textBox7.Text = dataGridView1.Rows[rowindex1].Cells[8].Value.ToString();
                textBox5.Text = dataGridView1.Rows[rowindex1].Cells[6].Value.ToString();
                textBox4.Text = dataGridView1.Rows[rowindex1].Cells[7].Value.ToString();
                textBox6.Text = dataGridView1.Rows[rowindex1].Cells[6].Value.ToString();
                textBox11.Text = dataGridView1.Rows[rowindex1].Cells[7].Value.ToString();
                textBox13.Text= dataGridView1.Rows[rowindex1].Cells[9].Value.ToString();

                SqlConnection conn33 = new SqlConnection(src);
                conn33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from SoldItems WHERE IdSale ='" + id + "'", conn33);
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
                        dataGridView2.Rows.Insert(row1, dr83["Price"].ToString(), null, null, dr83["Price"].ToString(), dr83["Quantity"].ToString(), dr83["FullPrice"].ToString(), dr83["Profit"].ToString(), dr83["PP"].ToString());
                        this.dataGridView2.Rows[row1].HeaderCell.Value = (row1+1).ToString();
                        dataGridView2.Rows[row1].Cells[1].Value = dr83["ItemName"];
                        if (dr83["CompanyName"] != null && dr83["CompanyName"].ToString() != "")
                        {
                            dataGridView2.Rows[row1].Cells[2].Value = dr83["CompanyName"];
                        }
                        else
                       {
                            dataGridView2.Rows[row1].Cells[2].Value = null;
                       }
                        itemarr[row1] = dr83["ItemName"].ToString();
                        idarr[row1] = dr83["Quantity"].ToString();
                        row1++;
                }
                dr83.Close();
                textBox12.Text = "";
                textBox12.Text = dataGridView1.Rows[rowindex1].Cells[10].Value.ToString();
                textBox12_Leave(sender,e);
                empty = false;
                select = false;
                show = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            S();
        }

        public void S1(float q, string s)
        {
            int r = 0;
            for (int rr = 0; rr < dataGridView2.RowCount - 1; rr++)
            {
                if (dataGridView2.Rows[rr].Cells[1].Value != null && dataGridView2.Rows[rr].Cells[3].Value != null)
                { r++; }
            }
            float sp = q * float.Parse(s);
            float sd = float.Parse(textBox12.Text) / r * 100 / sp / 100;
            profit = sp - (sp * sd) - profit;
            profitarr[rowarr] = profit.ToString();
            itemarr1[rowarr] = it1;
            rowarr++;
            // MessageBox.Show("q   " + q + "     s    " + s + "     sp    " + sp + "     sd    " + sd + "     profit    " + profit + "     pppp    " + profit.ToString());
        }

        public void SaleList1(string item1, string quan1, string sprice)
        {
            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                string dd = dr3["Id"].ToString();
                if (item1 == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0 && float.Parse(dr3["RQuantity"].ToString()) >= float.Parse(quan1))
                {
                    q = q + float.Parse(quan1);
                    float qqq = float.Parse(dr3["RQuantity"].ToString()) - float.Parse(quan1);
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + quan1 + "@";
                    profit = profit + (float.Parse(quan1) * float.Parse(dr3["Price"].ToString()));
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", qqq);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();
                    S1(q, sprice);
                    break;
                }
                else if (item1 == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                {
                    q = q + float.Parse(dr3["RQuantity"].ToString());
                    float qq = float.Parse(quan1) - float.Parse(dr3["RQuantity"].ToString());
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + dr3["RQuantity"].ToString() + "@";
                    profit = profit + (float.Parse(dr3["RQuantity"].ToString()) * float.Parse(dr3["Price"].ToString()));
                    SqlConnection conn4 = new SqlConnection(src);
                    SqlCommand cmdn4 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn4);
                    cmdn4.Parameters.AddWithValue("@box1", "0");
                    conn4.Open();
                    SqlDataReader d724 = cmdn4.ExecuteReader();
                    conn4.Close();
                    SaleList1(item1, qq.ToString(), sprice);
                    break;
                }
            }
        }

        public void SaleList2(string item2, string company2, string quan2, string sprice2)
        {
            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                string dd = dr3["Id"].ToString();

                if (item2 == dr3["ItemName"].ToString() && company2 == dr3["CompanyName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0 && float.Parse(dr3["RQuantity"].ToString()) >= float.Parse(quan2))
                {
                    q = q + float.Parse(quan2);
                    float qqq = float.Parse(dr3["RQuantity"].ToString()) - float.Parse(quan2);
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + quan2 + "@";
                    profit = profit + (float.Parse(quan2) * float.Parse(dr3["Price"].ToString()));
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", qqq);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();
                    S1(q, sprice2);
                    break;
                }
                else if (item2 == dr3["ItemName"].ToString() && company2 == dr3["CompanyName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                {
                    q = q + float.Parse(dr3["RQuantity"].ToString());
                    float qq = float.Parse(quan2) - float.Parse(dr3["RQuantity"].ToString());
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + dr3["RQuantity"].ToString() + "@";
                    profit = profit + (float.Parse(dr3["RQuantity"].ToString()) * float.Parse(dr3["Price"].ToString()));
                    SqlConnection conn4 = new SqlConnection(src);
                    SqlCommand cmdn4 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn4);
                    cmdn4.Parameters.AddWithValue("@box1", "0");
                    conn4.Open();
                    SqlDataReader d724 = cmdn4.ExecuteReader();
                    conn4.Close();
                    SaleList2(item2, company2, qq.ToString(), sprice2);
                    break;
                }
            }
        }

        public void MakeSale()
        {
            if (float.Parse(textBox4.Text) > 0)
            {
                p = float.Parse(textBox5.Text);
                rd = float.Parse(textBox4.Text);
                rdd = "نعم";
            }
            else
            {
                p = float.Parse(textBox3.Text);
                rd = 0;
                rdd = "لا";
            }
            SqlConnection conn99 = new SqlConnection(src);
             SqlCommand cmdn99 = new SqlCommand("UPDATE [Sales] SET CompanyName=@box1, Date=@box2, Amount=@box3, Debt=@box4, Paid=@box5, RDebt=@box6, Notes=@box7, Profit=@box8, Dis=@box9 WHERE Id = '" + textBox1.Text + "'", conn99);
            cmdn99.Parameters.AddWithValue("@box1", comboBox1.Text);
            cmdn99.Parameters.AddWithValue("@box2", textBox2.Text);
            cmdn99.Parameters.AddWithValue("@box3", textBox3.Text);
            cmdn99.Parameters.AddWithValue("@box4", rdd);
            cmdn99.Parameters.AddWithValue("@box5", p);
            cmdn99.Parameters.AddWithValue("@box6", rd);
            cmdn99.Parameters.AddWithValue("@box7", textBox7.Text);
            float netp = 0;
            for (int b = 0; b < profitarr.Length; b++)
            {
                if (profitarr[b] != null)
                { netp = netp + float.Parse(profitarr[b]);
                }
            }
            cmdn99.Parameters.AddWithValue("@box8", netp);
            cmdn99.Parameters.AddWithValue("@box9", textBox12.Text);
            conn99.Open();
            SqlDataReader d7299 = cmdn99.ExecuteReader();
            conn99.Close();
            sitem = "";
            for (int jj = 0; jj < dataGridView2.RowCount - 1; jj++)
            {

                if (dataGridView2.Rows[jj].Cells[1].Value != null && dataGridView2.Rows[jj].Cells[1].Value.ToString() != "")
                {
                    float q23 = 0;
                    SqlConnection co55 = new SqlConnection(src);
                    SqlCommand cm55 = new SqlCommand("INSERT INTO [SoldItems](CompanyName,Date,ItemName,IdSale,Price,Quantity,FullPrice,Name,PP,Profit)VALUES (@Box1,@Box2,@Box3,@Box4,@Box5,@Box6,@Box7,@Box8,@Box9,@Box10)", co55);

                    if (dataGridView2.Rows[jj].Cells[2].Value != null && dataGridView2.Rows[jj].Cells[2].Value.ToString() != "")
                    { cm55.Parameters.AddWithValue("@Box1", dataGridView2.Rows[jj].Cells[2].Value); }
                    else
                    { cm55.Parameters.AddWithValue("@Box1", ""); }
                    cm55.Parameters.AddWithValue("@Box2", textBox2.Text);
                    cm55.Parameters.AddWithValue("@Box3", dataGridView2.Rows[jj].Cells[1].Value);
                    cm55.Parameters.AddWithValue("@Box4", int.Parse(textBox1.Text));
                    cm55.Parameters.AddWithValue("@Box5", dataGridView2.Rows[jj].Cells[3].Value);
                    cm55.Parameters.AddWithValue("@Box6", dataGridView2.Rows[jj].Cells[4].Value);
                    cm55.Parameters.AddWithValue("@Box7", dataGridView2.Rows[jj].Cells[5].Value);
                    cm55.Parameters.AddWithValue("@Box8", comboBox1.Text);
                    cm55.Parameters.AddWithValue("@Box9", itemarr1[jj]);
                    cm55.Parameters.AddWithValue("@Box10", float.Parse(profitarr[jj]));
                    co55.Open();
                    SqlDataReader d55 = cm55.ExecuteReader();
                    sitem = sitem + Environment.NewLine + "  الصنف  " + dataGridView2.Rows[jj].Cells[1].Value.ToString() + "  السعر الفردي  " + dataGridView2.Rows[jj].Cells[3].Value.ToString() + "  الكمية  " + dataGridView2.Rows[jj].Cells[4].Value.ToString() + "  السعر الكلي  " + dataGridView2.Rows[jj].Cells[5].Value.ToString();
                    

                    SqlConnection conn3 = new SqlConnection(src);
                    conn3.Open();
                    SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    string nit = "";
                    while (dr3.Read())
                    {

                        if (dataGridView1.Rows[jj].Cells[0].Value.ToString() == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                        {
                            q23 = q23 + float.Parse(dr3["RQuantity"].ToString());
                            nit = dr3["ItemName"].ToString();
                        }

                    }
                    string itid = "";
                    SqlConnection conn31 = new SqlConnection(src);
                    conn31.Open();
                    SqlCommand cmd31 = new SqlCommand("select * from Inventory WHERE Item = @Name", conn31);
                    cmd31.Parameters.AddWithValue("@Name", nit);
                    SqlDataReader dr81 = cmd31.ExecuteReader();
                    while (dr81.Read())
                    {
                        itid = dr81["Id"].ToString();
                    }
                    dr81.Close();
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Inventory] SET Quantity=@box1 WHERE Id = '" + itid + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", q23);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();
                }
            }
            if (rdd == "نعم")
            {

                SqlConnection co = new SqlConnection(src);
                SqlCommand cm = new SqlCommand("INSERT INTO [SaleDebt](Name,Date,Amount,idSales)VALUES (@Box1,@Box2,@Box3,@Box4)", co);
                cm.Parameters.AddWithValue("@Box1", comboBox1.Text);
                cm.Parameters.AddWithValue("@Box2", textBox2.Text);
                cm.Parameters.AddWithValue("@Box3", textBox4.Text);
                cm.Parameters.AddWithValue("@Box4", textBox1.Text);
                co.Open();
                SqlDataReader d = cm.ExecuteReader();
            }


            SqlConnection con = new SqlConnection(src);
            SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
            cmd.Parameters.AddWithValue("@textBox1", name3);
            cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
            cmd.Parameters.AddWithValue("@textBox3", inv + sitem);
            if (!checkBox1.Checked)
            {
                cmd.Parameters.AddWithValue("@textBox4", "UP");
            }
            else
            {
                cmd.Parameters.AddWithValue("@textBox4", "DELA");
            }
            con.Open();
            SqlDataReader dr2 = cmd.ExecuteReader();
            MessageBox.Show("لقد تم تعديل أو إرجاع بعض اصناف الفاتورة بنجاح ");
            S();
            per = 0;
            dis = 0;
        }
        
        public void Edit(bool ok1, string name2)
        {
            name3 = name2;
            string it = "";
            if (ok1)
            {
                string pp = "";
                if (!checkBox1.Checked)
                {
                    for (int k = 0; k < itemarr.Length - 1; k++)
                    {
                        if (itemarr[k] != null && itemarr[k] != "")
                        {
                            string itid = "";
                            float itq = 0;
                            SqlConnection conn31 = new SqlConnection(src);
                            conn31.Open();
                            SqlCommand cmd31 = new SqlCommand("select * from Inventory WHERE Item = @Name", conn31);
                            cmd31.Parameters.AddWithValue("@Name", itemarr[k]);
                            SqlDataReader dr81 = cmd31.ExecuteReader();
                            while (dr81.Read())
                            {
                                itid = dr81["Id"].ToString();
                                itq = float.Parse(dr81["Quantity"].ToString());
                            }
                            dr81.Close();
                            float total1 = itq + float.Parse(idarr[k]);
                            SqlConnection conn = new SqlConnection(src);
                            SqlCommand cmdn = new SqlCommand("UPDATE [Inventory] SET Quantity=@box1 WHERE Id = '" + itid + "'", conn);
                            cmdn.Parameters.AddWithValue("@box1", total1);

                            conn.Open();
                            SqlDataReader d72 = cmdn.ExecuteReader();
                            conn.Close();
                        }
                    }
                    for (int i = 0; i < dataGridView2.RowCount - 1; i++)
                    {
                        if (dataGridView2.Rows[i].Cells[7].Value != null)
                        {
                            pp = pp + dataGridView2.Rows[i].Cells[7].Value.ToString();
                        }
                    }
                    sale = pp.Split('@').ToList<string>();
                    for (int arr = 0; arr < sale.Count; arr++)
                    {
                        itemid = sale[arr].Split('>').ToList<string>();
                        for (int arr1 = 0; arr1 < itemid.Count; arr1 = +4)
                        {
                            if (itemid[arr1] != null && itemid[arr1] != "")
                            {
                                
                                SqlConnection conn33162 = new SqlConnection(src);
                                conn33162.Open();
                                SqlCommand cmd33162 = new SqlCommand("select * from Items WHERE Id ='" + itemid[arr1] + "'", conn33162);
                                SqlDataReader dr83162 = cmd33162.ExecuteReader();
                                float reg = 0;
                                while (dr83162.Read())
                                {
                                    reg = float.Parse(dr83162["RQuantity"].ToString());
                                }
                                if (reg > -1)
                                {
                                    SqlConnection co2 = new SqlConnection(src);
                                    SqlCommand cmdn2 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + int.Parse(itemid[arr1]) + "'", co2);
                                    cmdn2.Parameters.AddWithValue("@box1", int.Parse(itemid[arr1 + 3]) + reg);
                                    co2.Open();
                                    SqlDataReader d555 = cmdn2.ExecuteReader();
                                    co2.Close();


                                }
                                else
                                {
                                    SqlConnection co2 = new SqlConnection(src);
                                    SqlCommand cmdn2 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + int.Parse(itemid[arr1]) + "'", co2);
                                    cmdn2.Parameters.AddWithValue("@box1", itemid[arr1 + 3]);
                                    co2.Open();
                                    SqlDataReader d555 = cmdn2.ExecuteReader();
                                    co2.Close();
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int l = 0; l < itemarr.Length - 1; l++)
                    {
                        if (itemarr[l] != null)
                        {
                            if (itemarr[l] == dataGridView2.Rows[l].Cells[1].Value.ToString() && float.Parse(idarr[l]) > float.Parse(dataGridView2.Rows[l].Cells[4].Value.ToString()))
                            {
                                float count = float.Parse(idarr[l]) - float.Parse(dataGridView2.Rows[l].Cells[4].Value.ToString());
                                float pr = float.Parse(dataGridView2.Rows[l].Cells[6].Value.ToString()) / float.Parse(idarr[l]);
                                float amt = (count * float.Parse(dataGridView2.Rows[l].Cells[3].Value.ToString())) - (count * pr);
                                SqlConnection con5151 = new SqlConnection(src);
                                SqlCommand cmd5151 = new SqlCommand("INSERT INTO [Destroy](Date,Amount)VALUES (@textBox1,@textBox2)", con5151);
                                cmd5151.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                                cmd5151.Parameters.AddWithValue("@textBox2", amt);
                                con5151.Open();
                                SqlDataReader dr15151 = cmd5151.ExecuteReader();
                            }
                        }
                    }
                }
                float amount = 0;
                float total = 0;
                SqlConnection conne = new SqlConnection(src);
                conne.Open();
                SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                int regid1 = Convert.ToInt32(cmod.ExecuteScalar());
                SqlConnection conn3316 = new SqlConnection(src);
                conn3316.Open();
                SqlCommand cmd3316 = new SqlCommand("select * from Register WHERE Id ='" + regid1 + "'", conn3316);
                SqlDataReader dr8316 = cmd3316.ExecuteReader();
                while (dr8316.Read())
                {
                    amount = float.Parse(dr8316["Amount"].ToString());
                }
                total = amount - float.Parse(textBox6.Text)+ float.Parse(textBox5.Text);
                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                cmd55.Parameters.AddWithValue("@textBox2", total);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                inv = " لقد تم تعديل أو إرجاع بعض اصناف فاتورة مبيعات " + ">>" + "  رمز الحركة " + textBox1.Text + "  الاسم " + comboBox1.Text + "   " + "  رقم الفاتورة " + textBox1.Text + Environment.NewLine + "  التاريخ " + textBox2.Text + "   " + "  القيمة " + textBox3.Text + "   " + "  القيمة المدفوعة " + textBox5.Text + "   " + "   القيمة المتبقية " + textBox4.Text + "  ملاحظات " + textBox7.Text + Environment.NewLine + "  الاصناف  ";

                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [SaleDebt] WHERE idSales = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", textBox1.Text);
                cn111.Open();
                SqlDataReader dr111 = cmd111.ExecuteReader();

                SqlConnection cn1 = new SqlConnection(src);
                SqlCommand cmd1 = new SqlCommand("DELETE FROM [SoldItems] WHERE IdSale = @Box1", cn1);
                cmd1.Parameters.AddWithValue("@Box1", textBox1.Text);
                cn1.Open();
                SqlDataReader dr1 = cmd1.ExecuteReader();
                
                Array.Clear(itemarr1, 0, itemarr1.Length);
                Array.Clear(profitarr, 0, profitarr.Length);
                rowarr = 0;
                q = 0;
                profit = 0;
                it1 = "";
                for (int rr = 0; rr < dataGridView2.RowCount - 1; rr++)
                {
                    if (dataGridView2.Rows[rr].Cells[1].Value != null && dataGridView2.Rows[rr].Cells[1].Value.ToString() != "")
                    {
                        if (dataGridView2.Rows[rr].Cells[1].Value != null && dataGridView2.Rows[rr].Cells[1].Value.ToString() != "" && dataGridView2.Rows[rr].Cells[2].Value != null && dataGridView2.Rows[rr].Cells[2].Value.ToString() != "")
                        {

                            it1 = "";
                            profit = 0;q = 0;
                            SaleList2(dataGridView2.Rows[rr].Cells[1].Value.ToString(), dataGridView2.Rows[rr].Cells[2].Value.ToString(), dataGridView2.Rows[rr].Cells[4].Value.ToString(), dataGridView2.Rows[rr].Cells[3].Value.ToString());
                            
                        }
                        else
                        {
                            it1 = "";
                            profit = 0;q = 0;
                            SaleList1(dataGridView2.Rows[rr].Cells[1].Value.ToString(), dataGridView2.Rows[rr].Cells[4].Value.ToString(), dataGridView2.Rows[rr].Cells[3].Value.ToString());
                           
                        }

                    }

                }
                MakeSale();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (show)
            {
                enough = false;
                enough2 = false;
                p = 0;
                rd = 0;
                rdd = "";
                sitem = "";
                if (dataGridView2.Rows.Count <= 1 || dataGridView2.Rows[0].Cells[1].Value == null || dataGridView2.Rows[0].Cells[1].Value.ToString() == "") { MessageBox.Show("الرجاء ادخال الاصناف الى الجدول"); }
                else
                {
                    if (!checkBox1.Checked)
                    {
                        for (int rr = 0; rr < dataGridView2.RowCount - 1; rr++)
                        {
                            if (dataGridView2.Rows[rr].Cells[1].Value != null)
                            {
                                float q = 0;
                                if (dataGridView2.Rows[rr].Cells[1].Value != null && dataGridView2.Rows[rr].Cells[2].Value != null)
                                {
                                    SqlConnection conn3 = new SqlConnection(src);
                                    conn3.Open();
                                    SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
                                    SqlDataReader dr3 = cmd3.ExecuteReader();
                                    while (dr3.Read())
                                    {

                                        if (dataGridView2.Rows[rr].Cells[1].Value.ToString() == dr3["ItemName"].ToString() && dataGridView2.Rows[rr].Cells[2].Value.ToString() == dr3["CompanyName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > -1)
                                        {
                                            q = q + float.Parse(dr3["RQuantity"].ToString());
                                        }
                                    }
                                }
                                else if (dataGridView2.Rows[rr].Cells[1].Value != null)
                                {
                                    SqlConnection conn3 = new SqlConnection(src);
                                    conn3.Open();
                                    SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
                                    SqlDataReader dr3 = cmd3.ExecuteReader();
                                    while (dr3.Read())
                                    {

                                        if (dataGridView2.Rows[rr].Cells[1].Value.ToString() == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > -1)
                                        {
                                            q = q + float.Parse(dr3["RQuantity"].ToString());
                                        }
                                    }
                                }
                                if (q >= float.Parse(dataGridView2.Rows[rr].Cells[4].Value.ToString()))
                                { enough = true; }
                                else if (float.Parse(dataGridView2.Rows[rr].Cells[4].Value.ToString()) == float.Parse(idarr[rr]) && q==0)
                                { enough = true; }
                                else
                                {
                                    enough = false;
                                    MessageBox.Show("الكميات في المستودع غير كافية لاتمام عملية التعديل" + Environment.NewLine + Environment.NewLine + "                                       أو                   " + Environment.NewLine + Environment.NewLine + "             اسماء الشركات غير مطابقة للاصناف          ");
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        for(int l=0;l<itemarr.Length-1;l++)
                        {
                            if (itemarr[l] != null)
                            {
                                if (itemarr[l] != dataGridView2.Rows[l].Cells[1].Value.ToString() || float.Parse(idarr[l]) < float.Parse(dataGridView2.Rows[l].Cells[4].Value.ToString()))
                                {
                                    MessageBox.Show("لا يمكن تغيير معلومات الفاتورة او زيادة الكميات في حالة البضاعة الفاسدة" + Environment.NewLine + "إضغط مساعدة لمعرفة المزيد");
                                    enough = false;
                                    break;
                                }
                                else if (itemarr[l] == dataGridView2.Rows[l].Cells[1].Value.ToString() && float.Parse(idarr[l]) >= float.Parse(dataGridView2.Rows[l].Cells[4].Value.ToString()))
                                {
                                    enough = true;
                                }
                            }
                        }
                    }
                    try
                    {
                        if (float.Parse(textBox3.Text) >= 0 || float.Parse(textBox12.Text) >= 0 || float.Parse(textBox4.Text) >= 0 || float.Parse(textBox5.Text) >= 0)
                        { enough2 = true; }
                        else
                        {
                            enough2 = false;
                            MessageBox.Show("لا يجوز ان تكون القيم اقل من صفر");
                        }
                    }
                    catch
                    {
                        MessageBox.Show("لا يجوز ان تكون القيم اقل من صفر");
                        enough2 = false;
                    }
                    if (enough && enough2)
                    {
                        if ((MessageBox.Show("هل انت متأكد من عملية التعديل ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                        {
                            if (checkBox1.Checked)
                            {
                                if ((MessageBox.Show("هل انت متأكد ان بعض اصناف الفاتورة فاسدة ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                                {
                                    Program.mysignin.Show();
                                    Program.mysignin.Signin_Load(sender, e);
                                    string ww = "ESale";
                                    string page = "ESale";
                                    Program.mysignin.which(ww, page);
                                }
                            }
                            else
                            {
                                Program.mysignin.Show();
                                Program.mysignin.Signin_Load(sender, e);
                                string ww = "ESale";
                                string page = "ESale";
                                Program.mysignin.which(ww, page);
                            }
                        }
                    }
                }


            }
        }
       
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            MessageBox.Show("                   يمكن استخدام هذه العملية في حالتين"+Environment.NewLine+ Environment.NewLine + Environment.NewLine + Environment.NewLine + "أولا:  في حالة إرجاع الزبون البضاعة كاملة وكانت كلها فاسدة" +Environment.NewLine+"             وهنا يجب حذف القاتورة مع تفعيل هذه الحالة"+Environment.NewLine+"        على أن تبقى معلومات الفاتورة كما هي دون أي تغيير" +Environment.NewLine+ Environment.NewLine + Environment.NewLine + "         أما الحاله الثانيه:  فهي إرجاع الزبون لاصناف فاسدة" +Environment.NewLine+"            و في هذه الحلة يسمح بتقليل الكميات الفاسدة فقط"+Environment.NewLine+"                                من الكمية التي في الجدول"+Environment.NewLine+"                             و عدم تغيير أي معلومات أخرى"+Environment.NewLine+"                       ثم قم بتفيل هذه الحالة ثم إضغط تعديل");
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

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
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

        int i = 0; int c = 0; int dr = 0; bool end = false;
        private void button6_Click(object sender, EventArgs e)
        {
            if (show)
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
                        //printPreviewDialog1.ShowDialog();

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
                e.Graphics.DrawString(textBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                e.Graphics.DrawString(comboBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                e.Graphics.DrawString(textBox2.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                e.Graphics.DrawImage(newImage2, 50, 225);


                int ee = 300;
                for (int r = 0; r <= i; r++) //29 36
                {
                    if (dataGridView2.Rows[r].Cells[0].Value != null)
                    {
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[1].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                        e.Graphics.DrawString(dataGridView2.Rows[r].Cells[5].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                        ee += 20;
                    }
                }

                e.Graphics.DrawImage(newImage3, 50, 900);
                e.Graphics.DrawString(textBox12.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 915));
                e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 905));
                e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 927));
                e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 915));

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
                    e.Graphics.DrawString(textBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                    e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                    e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                    e.Graphics.DrawString(comboBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                    e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                    e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                    e.Graphics.DrawString(textBox2.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                    e.Graphics.DrawImage(newImage2, 50, 225);


                    int ee = 300;
                    for (int r = 0; r <= i; r++) //29 36
                    {
                        if (dataGridView2.Rows[r].Cells[0].Value != null)
                        {
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[1].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[5].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                            ee += 20;
                        }
                    }

                    e.Graphics.DrawString("1", new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                }
                if (c == 1)
                {
                    e.Graphics.DrawImage(newImage3, 50, 50);
                    e.Graphics.DrawString(textBox12.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 65));
                    e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 55));
                    e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 77));
                    e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 65));

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
                    e.Graphics.DrawString(textBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                    e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                    e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                    e.Graphics.DrawString(comboBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                    e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                    e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                    e.Graphics.DrawString(textBox2.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                    e.Graphics.DrawImage(newImage2, 50, 225);


                    int ee = 300;
                    for (int r = 0; r < 36; r++) //29 36
                    {
                        if (dataGridView2.Rows[r].Cells[0].Value != null)
                        {
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[1].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                            e.Graphics.DrawString(dataGridView2.Rows[r].Cells[5].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
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
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[1].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[5].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
                                ee2 += 20;
                                dr++;
                            }
                        }
                        ee2 += 20;
                        e.Graphics.DrawImage(newImage3, 50, ee2);
                        e.Graphics.DrawString(textBox12.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2 + 15));
                        e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, ee2 + 5));
                        e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, ee2 + 27));
                        e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, ee2 + 15));

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
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[1].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                    e.Graphics.DrawString(dataGridView2.Rows[r].Cells[5].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
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
                        e.Graphics.DrawString(textBox12.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 65));
                        e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 55));
                        e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 77));
                        e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 65));

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
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[1].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                e.Graphics.DrawString(dataGridView2.Rows[r].Cells[5].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
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

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

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
                        textBox3.Text = (dis- float.Parse(textBox12.Text)).ToString();
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
