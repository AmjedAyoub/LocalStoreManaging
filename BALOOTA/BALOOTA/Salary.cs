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
    public partial class Salary : Form
    {
        public Salary()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        int row1 = 0;
        public int rowindex1;
        private bool select = false;
        float a = 0;
        public bool empty = true;

        public void Salary_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox10.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            empty = true;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {


            Program.mysignin.Show();
            Program.mysignin.Signin_Load(sender, e);
            string ww = "SalaryS";
            string page = "SalaryS";
            Program.mysignin.which(ww, page);
        }

        public void SalaryS()
        {
            empty = true;
            dataGridView1.Rows.Clear();

            row1 = 0;
            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Employee", conn3);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                if (dr3["EmployeeName"].ToString() != "المبرمج")
                {
                    dataGridView1.Rows.Insert(row1, false, dr3["Date"].ToString(), dr3["EmployeeName"].ToString(), dr3["Salary"].ToString(), dr3["Notes"].ToString());
                    row1++;
                    empty = false;
                }
            }
            dr3.Close();
            this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Ascending);
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null);
            }
            for (int y = 0; y < row1; y++)
            {
                this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
            }
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCellAddress.X == 0 && dataGridView1.CurrentRow.Cells[1].Value != null)
            {
                if (!empty)
                {
                    if (row1 > 0 && dataGridView1.CurrentCell.Value.ToString() != "True")
                    {
                        select = true;
                        rowindex1 = dataGridView1.CurrentCell.RowIndex;
                        dataGridView1.Rows[rowindex1].Cells[0].Value = true;
                        for (int b = 0; b < row1; b++)
                        {
                            if (b != rowindex1)
                            { dataGridView1.Rows[b].Cells[0].Value = false; }
                        }

                    }
                    else
                    { select = false; }
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (select)
            {
                textBox10.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
                textBox8.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToShortDateString();
                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                SqlConnection conn301 = new SqlConnection(src);
                conn301.Open();
                SqlCommand cmd301 = new SqlCommand("select * from EmpOut", conn301);
                SqlDataReader dr301 = cmd301.ExecuteReader();
                float t = 0;
                while (dr301.Read())
                {
                    if (textBox1.Text == dr301["Name"].ToString() && DateTime.Parse(dr301["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr301["Date"].ToString()) <= DateTime.Parse(textBox8.Text))
                    {
                        t = t + float.Parse(dr301["Amount"].ToString());
                    }
                }
                dr301.Close();
                textBox3.Text = t.ToString();
                textBox5.Text = (float.Parse(textBox2.Text) - float.Parse(textBox3.Text)).ToString();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox10.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if ((MessageBox.Show("هل انت متأكد من دفع الراتب ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    reg();
                    if (float.Parse(textBox5.Text) <= a)
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "SalaryP";
                        string page = "SalaryP";
                        Program.mysignin.which(ww, page);
                    }
                    else
                    {

                        MessageBox.Show("الصندوق لا يكفي لاتمام العملية");
                    }
                }
            }
        }

        public void SalaryP(bool ok, string n)
        {
            SqlConnection con = new SqlConnection(src);
            SqlCommand cmd = new SqlCommand("INSERT INTO [EmpOut](Name,Date,Amount,Notes)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
            cmd.Parameters.AddWithValue("@textBox1", textBox1.Text);
            cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToShortDateString());
            cmd.Parameters.AddWithValue("@textBox3", textBox5.Text);
            if (textBox2.Text == textBox5.Text)
            {
                cmd.Parameters.AddWithValue("@textBox4", "الراتب");
            }
            else
            {

                cmd.Parameters.AddWithValue("@textBox4", "متبقي الراتب");
            }
            con.Open();
            SqlDataReader dr2 = cmd.ExecuteReader();
            SqlConnection con55 = new SqlConnection(src);
            SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
            cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
            float tt = a - float.Parse(textBox5.Text);
            cmd55.Parameters.AddWithValue("@textBox2", tt);
            con55.Open();
            SqlDataReader dr155 = cmd55.ExecuteReader();

            SqlConnection con1 = new SqlConnection(src);
            SqlCommand cmd1 = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con1);
            cmd1.Parameters.AddWithValue("@textBox1", n);
            cmd1.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
            cmd1.Parameters.AddWithValue("@textBox3", "لقد تم دفع راتب موظف" + Environment.NewLine + "  الموظف  " + textBox1.Text + "  التاريخ  " + DateTime.Now.ToShortDateString() + "  القيمة  " + textBox5.Text );
            cmd1.Parameters.AddWithValue("@textBox4", "AD");
            con1.Open();
            SqlDataReader dr21 = cmd1.ExecuteReader();
            
            MessageBox.Show("لقد تمت العملية بنجاح"+Environment.NewLine+ Environment.NewLine+"للتعديل الرجاء الذهاب الى > تعديل مصاريف الموظفين");

            textBox1.Text = "";
            textBox10.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            reg();
        }

        public void reg()
        {
            float amount = 0;
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
            a = amount;
        }

    }
}
