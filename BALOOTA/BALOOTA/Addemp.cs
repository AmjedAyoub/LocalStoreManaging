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
    public partial class Addemp : Form
    {
        public Addemp()
        {
            InitializeComponent();
        }

        int row1 = 0;
        private string src = Program.xsrc;
        public bool m = false;
        public string ww = "";
        public string page = "";

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.Checked)
            { panel2.Enabled = true; }
            else
            { panel2.Enabled = false;
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox4.Text = theDate1.ToString();
        }

        public void found(bool ok, string name, string wh)
        {
            if (ok)
            {
                if (m)
                {

                    SqlConnection con = new SqlConnection(src);
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Employee](EmployeeName,Salary,Date,Username,Password,Notes)VALUES (@textBox1,@textBox2,@textBox4,@textBox3,@textBox5,@textBox11)", con);
                    cmd.Parameters.AddWithValue("@textBox1", textBox1.Text);
                    cmd.Parameters.AddWithValue("@textBox5", textBox5.Text);
                    cmd.Parameters.AddWithValue("@textBox4", textBox4.Text);
                    cmd.Parameters.AddWithValue("@textBox2", textBox2.Text);
                    cmd.Parameters.AddWithValue("@textBox3", textBox3.Text);
                    cmd.Parameters.AddWithValue("@textBox11", textBox11.Text);
                    con.Open();

                    SqlConnection con6 = new SqlConnection(src);
                    SqlCommand cmd6 = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con6);
                    cmd6.Parameters.AddWithValue("@textBox1", name);
                    cmd6.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                    cmd6.Parameters.AddWithValue("@textBox3", " لقد تم اضافة الموظف \"  السيد  " + textBox1.Text + "  الراتب  " + textBox2.Text);
                    cmd6.Parameters.AddWithValue("@textBox4", "AD");
                    con6.Open();
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    MessageBox.Show("لقد تمت اضافة الموطف بنجاح ");
                    dataGridView1.Rows.Clear();
                    int row1 = 0;
                    SqlConnection conn3 = new SqlConnection(src);
                    conn3.Open();
                    SqlCommand cmd3 = new SqlCommand("select * from Employee", conn3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    while (dr3.Read())
                    {
                        if (dr3["EmployeeName"].ToString() != "المبرمج")
                        {
                            dataGridView1.Rows.Insert(row1, dr3["Date"].ToString(), dr3["EmployeeName"].ToString(), dr3["Salary"].ToString(), dr3["Notes"].ToString());
                            row1++;
                        }
                    }
                    dr3.Close();
                    this.dataGridView1.Sort(this.dataGridView1.Columns[1], ListSortDirection.Ascending);
                    for (int ro = 0; ro < 25; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null);
                    }
                    for (int y = 0; y < row1; y++)
                    {
                        this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                    }
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox11.Clear();
                    textBox6.Clear();
                    textBox5.Clear();
                    con.Close();
                    textBox1.Focus();
                    dateTimePicker1.Checked = false;

                    //this.Hide();
                }
                 
                else
                {
                    string src = Program.xsrc;
                    SqlConnection con1 = new SqlConnection(src);
                    SqlCommand cmd2 = new SqlCommand("INSERT INTO [Employee](EmployeeName,Salary,Date,Notes)VALUES (@textBox1,@textBox2,@textBox4,@textBox11)", con1);
                    cmd2.Parameters.AddWithValue("@textBox1", textBox1.Text);
                    cmd2.Parameters.AddWithValue("@textBox4", textBox4.Text);
                    cmd2.Parameters.AddWithValue("@textBox2", textBox2.Text);
                    cmd2.Parameters.AddWithValue("@textBox11", textBox11.Text);
                    con1.Open();
                    SqlConnection con = new SqlConnection(src);
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                    cmd.Parameters.AddWithValue("@textBox1", name);
                    cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@textBox3", " لقد تم اضافة الموظف \"  السيد  " + textBox1.Text + "  الراتب  " + textBox2.Text);
                    cmd.Parameters.AddWithValue("@textBox4", "AD");
                    con.Open();
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    SqlDataReader dr3 = cmd2.ExecuteReader();
                    //   dr3.Close();
                    MessageBox.Show("لقد تمت اضافة الموطف بنجاح ");
                    dataGridView1.Rows.Clear();
                    int row1 = 0;
                    SqlConnection conn3 = new SqlConnection(src);
                    conn3.Open();
                    SqlCommand cmd3 = new SqlCommand("select * from Employee", conn3);
                    SqlDataReader dr4 = cmd3.ExecuteReader();
                    while (dr4.Read())
                    {
                        if (dr3["EmployeeName"].ToString() != "المبرمج")
                        {
                            dataGridView1.Rows.Insert(row1, dr4["Date"].ToString(), dr4["EmployeeName"].ToString(), dr4["Salary"].ToString(), dr4["Notes"].ToString());
                            row1++;
                        }
                    }
                    dr4.Close();
                    this.dataGridView1.Sort(this.dataGridView1.Columns[1], ListSortDirection.Ascending);
                    for (int ro = 0; ro < 25; ro++)
                    {
                        dataGridView1.Rows.Add(null, null, null, null);
                    }
                    for (int y = 0; y < row1; y++)
                    {
                        this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                    }
                    textBox1.Clear();
                    textBox2.Clear();
                    textBox3.Clear();
                    textBox4.Clear();
                    textBox11.Clear();
                    textBox6.Clear();
                    textBox5.Clear();
                    con1.Close();
                    textBox1.Focus();
                    dateTimePicker1.Checked = false;
                    //  this.Hide();
                }
            }
            }
        
        public void button1_Click(object sender, EventArgs e)
{
    if (textBox1.Text == "") { MessageBox.Show("الرجاء ادخال اسم الموطف"); }
    else if (textBox2.Text == "") { MessageBox.Show("الرجاء ادخال راتب الموطف"); }
    else if (float.Parse(textBox2.Text) < 0 ) { MessageBox.Show("لا يجوز ان يكون الراتب اقل من صفر"); }
    else if (textBox4.Text == "") { MessageBox.Show("الرجاء ادخال التاريخ"); }
    else if (checkBox1.Checked)
    {
        if (textBox3.Text == "") { MessageBox.Show("الرجاء ادخال اسم المستخدم"); }
        else if (textBox5.Text == textBox6.Text && textBox5.Text != "")
        { bool found = false; // true when user name used befor

                    SqlConnection connn = new SqlConnection(src);
                    connn.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from Employee", connn);
            SqlDataReader dr = cmd1.ExecuteReader();
            while (dr.Read())
            {
                if (dr["Username"].ToString() == textBox3.Text)
                {
                    found = true;
                }
            }
            if (found)
            {
                MessageBox.Show("الرجاء تغيير اسم المستخدم");
                textBox5.Text = "";
                textBox6.Text = "";
                textBox3.Text = "";
                dr.Close();
                connn.Close();
            }
                    else
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        ww = "ADDEMP";
                        page = "Addemp";
                        m = true;
                        Program.mysignin.which(ww, page);
                    }
                }
        else
        {
            MessageBox.Show("كلمة السر غير متطابقة");
            textBox5.Clear();
            textBox6.Clear();
        }
    }
    else
    {
        Program.mysignin.Show();
        Program.mysignin.Signin_Load(sender, e);
        ww = "ADDEMP";
        page = "Addemp";
                m = false;
        Program.mysignin.which(ww, page);
    }
}

        public void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox11.Clear();
            textBox6.Clear();
            textBox5.Clear();
            checkBox1.Checked = false;
            panel2.Enabled = false;
            dateTimePicker1.Checked = false;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(null, null, null, null);
            }
            //Program.myform2.button1_Click_1(sender, e);
            textBox1.Focus();
        }
        
        public void Addemp_Load(object sender, EventArgs e)
        {
            panel2.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox11.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            dateTimePicker1.Checked = false;
            m = false;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(null, null, null, null);
            }
            textBox1.Focus();
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            Program.mysignin.Show();
            Program.mysignin.Signin_Load(sender, e);
            string ww = "EmpAS";
            string page = "EmpAS";
            Program.mysignin.which(ww, page);
        }

        public void EmpAS(bool p)
        {
            if(p)
            {
                dataGridView1.Rows.Clear();

                row1 = 0;
                SqlConnection conn = new SqlConnection(src);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Employee", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["EmployeeName"].ToString() != "المبرمج")
                    {
                        dataGridView1.Rows.Insert(row1, dr["Date"].ToString(), dr["EmployeeName"].ToString(), dr["Salary"].ToString(), dr["Notes"].ToString());
                        row1++;
                    }
                }
                dr.Close();
                this.dataGridView1.Sort(this.dataGridView1.Columns[1], ListSortDirection.Ascending);
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null, null);
                }
                for (int y = 0; y < row1; y++)
                {
                    this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }
            }
        }

    }
}
