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
    public partial class EmpP : Form
    {
        public EmpP()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        int row1 = 0;
        public int rowindex1;
        private bool select = false;
        public string name = "";
        public bool empty = true;
        public float reg1 = 0;
        string[] items1;

        public void EmpP_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            comboBox3.Text = "";
            comboBox3.SelectedText = "";
            if (comboBox3.Items.Count > 1)
            { comboBox3.Items.Clear(); }
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from EmpDebt", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            while (dr39.Read())
            {
                iitem = dr39["Name"].ToString();

                if (!comboBox3.Items.Contains(iitem))
                {
                    comboBox3.Items.Add(iitem);
                }
            }
            dr39.Close();

            items1 = new string[comboBox3.Items.Count];
            comboBox3.Items.CopyTo(items1, 0);
            empty = true;
            select = false;
            dataGridView1.Rows.Clear();

            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null);
            }
            comboBox3.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (comboBox3.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "EmpPS";
                string page = "EmpPS";
                Program.mysignin.which(ww, page);
            }
        }

        public void EmpPS()
        {
            row1 = 0;
            try
            {
                if (comboBox3.Text != "" && comboBox3.Text != "الكل")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from EmpDebt", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr.Read())
                    {
                        if (dr["Name"].ToString() == comboBox3.Text)

                        {
                            dataGridView1.Rows.Insert(row1, false, dr["Id"].ToString(), dr["Date"].ToString(), dr["Name"].ToString(), dr["Amount"].ToString(), dr["Notes"].ToString());
                            row1++;
                        }
                    }
                    empty = false;
                }
                else if (comboBox3.SelectedItem.ToString() == "الكل")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from EmpDebt", con10);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr10.Read())
                    {
                        dataGridView1.Rows.Insert(row1, false, dr10["Id"].ToString(), dr10["Date"].ToString(), dr10["Name"].ToString(), dr10["Amount"].ToString(), dr10["Notes"].ToString());
                        row1++;

                    }
                    empty = false;
                }
                this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Descending);
                for (int y = 0; y < row1; y++)
                {
                    this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }
            }
            catch { MessageBox.Show("الرجاء التاكد من معلومات البحث"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {
                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox4.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox6.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[5].Value.ToString();
                textBox5.Text = textBox6.Text;
                textBox7.Text = "0.0";
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

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

            if (textBox1.Text != "")
            {
                try
                {
                    if (textBox7.Text != "")
                    {
                        textBox5.Text = (float.Parse(textBox6.Text) - float.Parse(textBox7.Text)).ToString();
                        if (float.Parse(textBox5.Text) < 0)
                        {
                            MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                            textBox7.Text = "0.0";
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                    textBox7.Text = "0.0";
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                if (textBox7.Text != "")
                {
                    if (float.Parse(textBox7.Text) >= 0)
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "EmpPP";
                        string page = "EmpPP";
                        Program.mysignin.which(ww, page);
                    }
                    else
                    {

                        MessageBox.Show("لا يجوز ان تكون القيمة اقل من صفر");
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء إدخال القيمة");
                }
            }
        }

        public void EmpPP(bool ok, string n)
        {
            if (ok)
            {
                SqlConnection conn5 = new SqlConnection(src);
                SqlCommand cmdn5 = new SqlCommand("UPDATE [EmpDebt] SET Name=@box1,Date=@box2,Amount=@box3,Notes=@box4 WHERE Id = '" + textBox1.Text + "'", conn5);
                cmdn5.Parameters.AddWithValue("@box1", textBox4.Text);
                cmdn5.Parameters.AddWithValue("@box2", textBox3.Text);
                cmdn5.Parameters.AddWithValue("@box3", textBox5.Text);
                cmdn5.Parameters.AddWithValue("@box4", textBox2.Text);
                conn5.Open();
                SqlDataReader d725 = cmdn5.ExecuteReader();
                conn5.Close();

                SqlConnection conne = new SqlConnection(src);
                conne.Open();
                SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                int x = Convert.ToInt32(cmod.ExecuteScalar());
                SqlConnection conn3316 = new SqlConnection(src);
                conn3316.Open();
                SqlCommand cmd3316 = new SqlCommand("select * from Register WHERE Id ='" + x + "'", conn3316);
                SqlDataReader dr8316 = cmd3316.ExecuteReader();

                while (dr8316.Read())
                {
                    reg1 = float.Parse(dr8316["Amount"].ToString());
                }

                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                string t7 = (reg1 + float.Parse(textBox7.Text)).ToString();
                cmd55.Parameters.AddWithValue("@textBox2", t7);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم قبض دين موظف" + Environment.NewLine + "  رمز الحركة  " + textBox1.Text + "  الاسم   " + textBox4.Text + "  التاريخ  " + textBox3.Text + "  القيمة  " + textBox7.Text);
                cmd.Parameters.AddWithValue("@textBox4", "UP");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تم القبض بنجاح");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                comboBox3.Text = "";
                comboBox3.SelectedText = "";
                empty = true;
                select = false;
                dataGridView1.Rows.Clear();

                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null);
                }comboBox3.Focus();
            }
        }

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item = comboBox3.Text;
                string[] filteredItems = items1.Where(x => x.Contains(item)).ToArray();
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
    }
}
