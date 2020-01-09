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
    public partial class Editstore : Form
    {
        public Editstore()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        int row1 = 0;
        public int rowindex1;
        private bool select = false;
        public string name = "";
        public bool empty = true;
        string[] items;
        string[] items1;
        
        public void Editstore_Load(object sender, EventArgs e)
        {

            textBox10.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
            textBox8.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToShortDateString();
            textBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox11.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            comboBox3.Text = "";
            comboBox2.Text = "";
            comboBox3.SelectedText = "";
            comboBox2.SelectedText = "";
            dateTimePicker2.Checked = false;
            if (comboBox3.Items.Count > 1)
            { comboBox3.Items.Clear(); comboBox2.Items.Clear(); }
            comboBox3.Items.Add(""); comboBox2.Items.Add("");
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from StoreOut", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            while (dr39.Read())
            {
                iitem = dr39["Name"].ToString();

                if (!comboBox3.Items.Contains(iitem))
                {
                    comboBox3.Items.Add(iitem); comboBox2.Items.Add(iitem);
                }
            }
            dr39.Close();
            items = new string[comboBox3.Items.Count];
            comboBox3.Items.CopyTo(items, 0);
            items1 = new string[comboBox2.Items.Count];
            comboBox2.Items.CopyTo(items1, 0);
            select = false;
            empty = true;
            dataGridView1.Rows.Clear();

            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null);
            }
            comboBox3.Focus();
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
            if (comboBox3.Text != "")
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "SO";
                string page = "SO";
                Program.mysignin.which(ww, page);
            }
            else
            {
                MessageBox.Show("الرجاء إختيار المصروف");
            }
        }

        public void SO()
        {
            SqlConnection conn392 = new SqlConnection(src);
            conn392.Open();
            SqlCommand cmd392 = new SqlCommand("select * from StoreOut", conn392);
            SqlDataReader dr392 = cmd392.ExecuteReader();
            empty = true;
            dataGridView1.Rows.Clear();

            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null);
            }
            row1 = 0;
            while (dr392.Read())
            {
                if (dr392["Name"].ToString() == comboBox3.Text && DateTime.Parse(dr392["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr392["Date"].ToString()) <= DateTime.Parse(textBox8.Text))
                {
                    dataGridView1.Rows.Insert(row1, false, dr392["Id"].ToString(), dr392["Date"].ToString(), dr392["Name"].ToString(), dr392["Amount"].ToString(), dr392["Notes"].ToString());
                    this.dataGridView1.Rows[row1].HeaderCell.Value = (row1 + 1).ToString();
                    row1++;
                }
            }
            this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Descending);
            dr392.Close();
            empty = false;
            select = false;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {
                if ((MessageBox.Show("هل انت متأكد من حذف المصروف ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    string ww = "SOD";
                    string page = "SOD";
                    Program.mysignin.which(ww, page);
                }
            }
        }

        public void SOD(bool ok, string n)
        {
            if (ok)
            {
                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [StoreOut] WHERE Id = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", dataGridView1.Rows[rowindex1].Cells[1].Value.ToString());
                cn111.Open();
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم حذف مصروف محل" + Environment.NewLine + "  المصروف  " + dataGridView1.Rows[rowindex1].Cells[3].Value.ToString() + "  التاريخ  " + dataGridView1.Rows[rowindex1].Cells[2].Value.ToString() + "  القيمة  " + dataGridView1.Rows[rowindex1].Cells[4].Value.ToString() + "  الملاحظات  " + dataGridView1.Rows[rowindex1].Cells[5].Value.ToString());
                cmd.Parameters.AddWithValue("@textBox4", "DEL");
                con.Open();
                SqlDataReader dr111 = cmd111.ExecuteReader();
                SqlDataReader dr2 = cmd.ExecuteReader();

                SqlConnection conne = new SqlConnection(src);
                conne.Open();
                SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                int regid = Convert.ToInt32(cmod.ExecuteScalar());
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
                t = reg + float.Parse(dataGridView1.Rows[rowindex1].Cells[4].Value.ToString());
                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                cmd55.Parameters.AddWithValue("@textBox2", t);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                MessageBox.Show("لقد تم حذف المصروف بنجاح");

                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox11.Text = "";
                comboBox3.Text = "";
                comboBox2.Text = "";
                empty = true; select = false; comboBox3.SelectedText = "";
                comboBox2.SelectedText = "";
                dateTimePicker2.Checked = false;
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null);
                }
                comboBox3.Focus();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {

                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox5.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();
                textBox6.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();
                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[5].Value.ToString();
               
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
                textBox11.Text = amount.ToString() + "  (د.أ)";
                textBox3.Text = amount.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox11.Text = "";
            comboBox3.Text = "";
            comboBox2.Text = "";
            empty = true; select = false; comboBox3.SelectedText = "";
            comboBox2.SelectedText = "";
            dateTimePicker2.Checked = false;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null);
            }
            comboBox3.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                if (textBox5.Text != "")
                {
                    if (DateTime.Parse(textBox5.Text) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(textBox5.Text) <= DateTime.Parse(textBox8.Text))
                    {
                        if (textBox4.Text != "")
                        {
                            if (float.Parse(textBox4.Text) >= 0)
                            {
                                if (float.Parse(textBox4.Text) <= float.Parse(textBox3.Text) - float.Parse(textBox6.Text))
                                {
                                    if ((MessageBox.Show("هل انت متأكد من تعديل المصروف ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                                    {
                                        Program.mysignin.Show();
                                        Program.mysignin.Signin_Load(sender, e);
                                        string ww = "SOE";
                                        string page = "SOE";
                                        Program.mysignin.which(ww, page);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لا يمكن صرف قيمة اكبر من الصندوق");
                                }
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
                    else
                    {
                        MessageBox.Show("الرجاء إدخال تاريخ ضمن الشهر الحالي");
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء إدخال التاريخ");
                }
            }
            else
            {
                MessageBox.Show("الرجاء إختيار المصروف");
            }
        }

        public void SOE(bool ok1, string n1)
        {
            if (ok1)
            {

                SqlConnection conn5 = new SqlConnection(src);
                SqlCommand cmdn5 = new SqlCommand("UPDATE [StoreOut] SET Name=@box1,Date=@box2,Amount=@box3,Notes=@box4 WHERE Id = '" + textBox2.Text + "'", conn5);
                cmdn5.Parameters.AddWithValue("@box1", comboBox2.Text);
                cmdn5.Parameters.AddWithValue("@box2", textBox5.Text);
                cmdn5.Parameters.AddWithValue("@box3", textBox4.Text);
                cmdn5.Parameters.AddWithValue("@box4", textBox1.Text);
                conn5.Open();
                SqlDataReader d725 = cmdn5.ExecuteReader();
                conn5.Close();

                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                string t = (float.Parse(textBox3.Text) + float.Parse(textBox6.Text) - float.Parse(textBox4.Text)).ToString();
                cmd55.Parameters.AddWithValue("@textBox2", t);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n1);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم تعديل مصروف محل" + Environment.NewLine + "  المصروف  " + comboBox2.Text + "  التاريخ  " + textBox5.Text + "  القيمة  " + textBox4.Text + "  الملاحظات  " + textBox1.Text);
                cmd.Parameters.AddWithValue("@textBox4", "UP");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تم تعديل المصروف بنجاح");


                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                textBox11.Text = "";
                comboBox3.Text = "";
                comboBox2.Text = "";
                empty = true; select = false; comboBox3.SelectedText = "";
                comboBox2.SelectedText = "";
                dateTimePicker2.Checked = false;
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null);
                }
                comboBox3.Focus();
            }
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            textBox5.Text = theDate1.ToString();
        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item2 = comboBox2.Text;
                string[] filteredItems2 = items1.Where(x => x.Contains(item2)).ToArray();
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
    }
    }
