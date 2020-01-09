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
    public partial class Alert : Form
    {
        public Alert()
        {
            InitializeComponent();
        }

        int row11 = 0;
        int row1 = 0;
        public int rowindex1;
        private bool select = false;
        public bool empty = true;
        private string src = Program.xsrc;

        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox4.Text = "";
            textBox11.Text = "";
            comboBox2.Text = "";
            textBox2.Focus();
        }

        public void Alert_Load(object sender, EventArgs e)
        {

            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox3.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox11.Text = "";
            comboBox2.Text = "";
            textBox1.Text = "";
            dateTimePicker1.Checked = false;
            dateTimePicker3.Checked = false;
            empty = true;
            dataGridView1.Rows.Clear();

            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null);
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

        private void button6_Click(object sender, EventArgs e)
        {

            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            comboBox3.Text = "";
            dateTimePicker3.Checked = false;
            textBox6.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox2.Text=="" || textBox4.Text=="" || comboBox2.Text=="")
            {
                MessageBox.Show("الرجاء إدخال معلومات التحذير");
            }
            else
            {
                if ((MessageBox.Show("هل انت متأكد من اضافة التحذير ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    Program.mysignin.which("Al", "Al");
                }
            }
        }

        public void Al(bool ok, string name)
        {
            if(ok)
            {
                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Alert](Name,Date,Alert,Notes)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", textBox2.Text);
                cmd55.Parameters.AddWithValue("@textBox2", textBox4.Text);
                int day = int.Parse(comboBox2.Text);
                cmd55.Parameters.AddWithValue("@textBox3", DateTime.Parse(textBox4.Text).AddDays(-day).ToShortDateString());
                cmd55.Parameters.AddWithValue("@textBox4", textBox11.Text);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", name);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تمت اضافة تحذير شخصي"+Environment.NewLine+"  الاسم  "+ textBox2.Text + "  التاريخ  " + textBox4.Text + "  تحذير قبل  " + comboBox2.Text + "  الملاحظات  " + textBox11.Text);
                cmd.Parameters.AddWithValue("@textBox4", "AD");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تمت العملية بنجاح");
                textBox2.Text = "";
                textBox4.Text = "";
                textBox11.Text = "";
                comboBox2.Text = "";
                AS(true);
                dateTimePicker1.Checked = false;
                textBox2.Focus();

            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Program.mysignin.Show();
            Program.mysignin.Signin_Load(sender, e);
            Program.mysignin.which("AS", "AS");
        }

        public void AS(bool o)
        {
            if(o)
            {
                row1 = 0;
                empty = true;
                dataGridView1.Rows.Clear();

                for (int ro = 0; ro < 15; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null);
                }

                SqlConnection conn331 = new SqlConnection(src);
                conn331.Open();
                SqlCommand cmd331 = new SqlCommand("select * from Alert", conn331);
                SqlDataReader dr831 = cmd331.ExecuteReader();
                while (dr831.Read())
                {
                    dataGridView1.Rows.Insert(row1, false, dr831["Id"].ToString(), dr831["Name"].ToString(), dr831["Date"].ToString(), dr831["Alert"].ToString(), dr831["Notes"].ToString());
                    row1++;
                    empty = false;
                }
            }
        }        

        private void button4_Click(object sender, EventArgs e)
        {
            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value!=null)
            {
                if ((MessageBox.Show("هل انت متأكد من حذف التحذير ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    Program.mysignin.which("AlD", "AlD");
                }
            }
        }

        public void AlD(bool v, string n)
        {
            if(v)
            {
                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [Alert] WHERE Id = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", dataGridView1.Rows[rowindex1].Cells[1].Value.ToString());
                cn111.Open();
                SqlDataReader dr111 = cmd111.ExecuteReader();

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تمت حذف تحذير شخصي" + Environment.NewLine + "  الاسم  " + dataGridView1.Rows[rowindex1].Cells[2].Value.ToString() + "  التاريخ  " + dataGridView1.Rows[rowindex1].Cells[3].Value.ToString() + "  تحذير قبل  " + dataGridView1.Rows[rowindex1].Cells[4].Value.ToString() + "  الملاحظات  " + dataGridView1.Rows[rowindex1].Cells[5].Value.ToString());
                cmd.Parameters.AddWithValue("@textBox4", "DEL");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تمت العملية بنجاح");
                AS(true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {
                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox5.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowindex1].Cells[5].Value.ToString();
                comboBox3.Text = (DateTime.Parse(dataGridView1.Rows[rowindex1].Cells[3].Value.ToString()).Day - DateTime.Parse(dataGridView1.Rows[rowindex1].Cells[4].Value.ToString()).Day).ToString();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox6.Text == "" || textBox5.Text == "" || comboBox3.Text == "")
            {
                MessageBox.Show("الرجاء إدخال معلومات التحذير");
            }
            else
            {
                if ((MessageBox.Show("هل انت متأكد من تعديل التحذير ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    Program.mysignin.which("AE", "AE");
                }
            }
        }

        public void AE(bool b, string l)
        {
            if(b)
            {
                SqlConnection conn58 = new SqlConnection(src);
                SqlCommand cmdn58 = new SqlCommand("UPDATE [Alert] SET Name=@box, Date=@box1, Alert=@box2, Notes=@box3 WHERE Id = '" + textBox1.Text + "'", conn58);
                cmdn58.Parameters.AddWithValue("@box", textBox6.Text);
                cmdn58.Parameters.AddWithValue("@box1", textBox5.Text);
                int dayt = int.Parse(comboBox3.Text);
                cmdn58.Parameters.AddWithValue("@box2", DateTime.Parse(textBox5.Text).AddDays(-dayt).ToShortDateString());
                cmdn58.Parameters.AddWithValue("@box3", textBox3.Text);
                conn58.Open();
                SqlDataReader d7258 = cmdn58.ExecuteReader();
                conn58.Close();

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", l);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تمت تعديل تحذير شخصي" + Environment.NewLine + "  الاسم  " + textBox6.Text + "  التاريخ  " + textBox5.Text + "  تحذير قبل  " + comboBox3.Text + "  الملاحظات  " + textBox3.Text);
                cmd.Parameters.AddWithValue("@textBox4", "UP");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تمت العملية بنجاح");
                AS(true);
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                comboBox3.Text = "";
                dateTimePicker3.Checked = false;

            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox4.Text = theDate1.ToString();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            string theDate11 = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            textBox5.Text = theDate11.ToString();
        }
    }
}
