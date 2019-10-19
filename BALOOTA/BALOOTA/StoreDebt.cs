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
    public partial class StoreDebt : Form
    {
        public StoreDebt()
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
        int u = 1;
        int uu = 1;

        public void StoreDebt_Load(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox3.Text = "";
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            comboBox3.SelectedText = "";
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            if (comboBox3.Items.Count > 1)
            { comboBox3.Items.Clear(); comboBox2.Items.Clear(); comboBox1.Items.Clear(); }
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            comboBox2.Items.Add("");
            comboBox1.Items.Add("");
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from SDebt", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            while (dr39.Read())
            {
                iitem = dr39["Name"].ToString();

                if (!comboBox3.Items.Contains(iitem))
                {
                    comboBox3.Items.Add(iitem);
                    comboBox2.Items.Add(iitem);
                    comboBox1.Items.Add(iitem);
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
            u = 1;
            uu = 1;
            timer1.Start();
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
                string ww = "Sdebt";
                string page = "Sdebt";
                Program.mysignin.which(ww, page);
            }
        }

        public void Sdebt()
        {
            row1 = 0;
            try
            {
                if (comboBox3.Text != "" && comboBox3.Text != "الكل")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from SDebt", conn);
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
                }
                else if (comboBox3.SelectedItem.ToString() == "الكل")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from SDebt", con10);
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
                }
                if (row1 > 0)
                {
                    float sum1 = 0;
                    for (int k1 = 0; k1 <= row1 - 1; k1++)
                    {
                        sum1 = sum1 + float.Parse(dataGridView1.Rows[k1].Cells[4].Value.ToString());
                    }
                    textBox4.Text = sum1.ToString();
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

        private void button8_Click(object sender, EventArgs e)
        {
            comboBox3.Text = "";
            comboBox3.SelectedText = "";
            textBox4.Text = "";
            empty = true;
            select = false;
            dataGridView1.Rows.Clear();

            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null);
            }
            comboBox3.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {

            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {
                if ((MessageBox.Show("هل انت متأكد من حذف الدين ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    string ww = "SDD";
                    string page = "SDD";
                    Program.mysignin.which(ww, page);
                }

            }
        }

        public void SDD(bool ok, string n)
        {
            if (ok)
            {
                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [SDebt] WHERE Id = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", dataGridView1.Rows[rowindex1].Cells[1].Value.ToString());
                cn111.Open();
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم حذف دين محل" + Environment.NewLine + "  رمز الحركة  " + dataGridView1.Rows[rowindex1].Cells[1].Value.ToString() + "  الاسم " + dataGridView1.Rows[rowindex1].Cells[3].Value.ToString() + "  التاريخ  " + dataGridView1.Rows[rowindex1].Cells[2].Value.ToString() + "  القيمة  " + dataGridView1.Rows[rowindex1].Cells[4].Value.ToString() + "  الملاحظات  " + dataGridView1.Rows[rowindex1].Cells[5].Value.ToString());
                cmd.Parameters.AddWithValue("@textBox4", "DEL");
                con.Open();
                SqlDataReader dr111 = cmd111.ExecuteReader();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تم حذف الدين بنجاح");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox1.SelectedText = "";
                comboBox2.SelectedText = "";
                comboBox3.SelectedText = "";
                dateTimePicker1.Checked = false;
                dateTimePicker2.Checked = false;
                empty = true;
                select = false;
                dataGridView1.Rows.Clear();

                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null);
                }
                comboBox3.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {

                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox5.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                comboBox2.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[5].Value.ToString();
                textBox11.Text = textBox3.Text;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                if (textBox12.Text != "")
                {
                    if (textBox13.Text != "")
                    {
                        if (float.Parse(textBox13.Text) >= 0)
                        {

                            Program.mysignin.Show();
                            Program.mysignin.Signin_Load(sender, e);
                            string ww = "SD";
                            string page = "SD";
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
                else
                {
                    MessageBox.Show("الرجاء إدخال التاريخ");
                }

            }
            else
            {
                MessageBox.Show("الرجاء إدخال الاسم");
            }
        }

        public void SD(bool ok, string n)
        {
            if (ok)
            {
                string src = Program.xsrc;
                SqlConnection con1 = new SqlConnection(src);
                SqlCommand cmd2 = new SqlCommand("INSERT INTO [SDebt](Name,Date,Amount,Notes)VALUES (@textBox1,@textBox2,@textBox4,@textBox11)", con1);
                cmd2.Parameters.AddWithValue("@textBox1", comboBox1.Text);
                cmd2.Parameters.AddWithValue("@textBox4", textBox13.Text);
                cmd2.Parameters.AddWithValue("@textBox2", textBox12.Text);
                cmd2.Parameters.AddWithValue("@textBox11", textBox14.Text);
                con1.Open();
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", " لقد تم اضافة دين محل  "  + Environment.NewLine + "  الاسم  " + comboBox1.Text +  "  التاريخ  " + textBox12.Text + "  القيمة  " + textBox13.Text + "  الملاحظات  " + textBox14.Text);
                cmd.Parameters.AddWithValue("@textBox4", "AD");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();
                SqlDataReader dr3 = cmd2.ExecuteReader();
                
                MessageBox.Show("لقد تمت اضافة دين محل بنجاح ");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox1.SelectedText = "";
                comboBox2.SelectedText = "";
                comboBox3.SelectedText = "";
                dateTimePicker1.Checked = false;
                dateTimePicker2.Checked = false;
                empty = true;
                select = false;
                dataGridView1.Rows.Clear();

                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null);
                }comboBox3.Focus();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            comboBox1.Text = "";
            comboBox1.SelectedText = "";
            dateTimePicker1.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            textBox1.Text = "";
            textBox11.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox5.Text = "";
            comboBox2.Text = "";
            comboBox2.SelectedText = "";
            dateTimePicker2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                if (textBox5.Text != "")
                {
                    if (textBox3.Text != "")
                    {
                        if (float.Parse(textBox3.Text) >= 0)
                        {
                            Program.mysignin.Show();
                            Program.mysignin.Signin_Load(sender, e);
                            string ww = "SDU";
                            string page = "SDU";
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
                else
                {
                    MessageBox.Show("الرجاء إدخال التاريخ");
                }

            }
            else
            {
                MessageBox.Show("الرجاء إدخال الاسم");
            }
        }

        public void SDU(bool ok, string n)
        {
            if (ok)
            {
                SqlConnection conn5 = new SqlConnection(src);
                SqlCommand cmdn5 = new SqlCommand("UPDATE [SDebt] SET Name=@box1,Date=@box2,Amount=@box3,Notes=@box4 WHERE Id = '" + textBox1.Text + "'", conn5);
                cmdn5.Parameters.AddWithValue("@box1", comboBox2.Text);
                cmdn5.Parameters.AddWithValue("@box2", textBox5.Text);
                cmdn5.Parameters.AddWithValue("@box3", textBox3.Text);
                cmdn5.Parameters.AddWithValue("@box4", textBox2.Text);
                conn5.Open();
                SqlDataReader d725 = cmdn5.ExecuteReader();
                conn5.Close();
                
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم تعديل دين محل" + Environment.NewLine + "  رمز الحركة  " + textBox1.Text + "  الاسم   " + comboBox2.Text + "  التاريخ  " + textBox5.Text + "  القيمة  " + textBox3.Text + "  الملاحظات  " + textBox2.Text);
                cmd.Parameters.AddWithValue("@textBox4", "UP");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تم التعديل بنجاح");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox11.Text = "";
                textBox12.Text = "";
                textBox13.Text = "";
                textBox14.Text = "";
                comboBox1.Text = "";
                comboBox2.Text = "";
                comboBox3.Text = "";
                comboBox1.SelectedText = "";
                comboBox2.SelectedText = "";
                comboBox3.SelectedText = "";
                dateTimePicker1.Checked = false;
                dateTimePicker2.Checked = false;
                empty = true;
                select = false;
                dataGridView1.Rows.Clear();

                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null);
                }comboBox3.Focus();
            }
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            string theDate1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox12.Text = theDate1.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {

            string theDate1 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            textBox5.Text = theDate1.ToString();
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

        private void timer1_Tick(object sender, EventArgs e)
        {
                if(u==1)
                {
                    label24.Visible = true;
                    label7.Visible = false;
                u++;
            }
            else if (u == 2)
            {
                u++;
            }
            else if (u == 3)
                {
                    label24.Visible = false;
                    label7.Visible = true;
                u++;
            }
            else if (u == 4)
            {
                u++;
            }
            else if (u == 5)
                {
                    label24.Visible = true;
                    label7.Visible = true;
                u++;
            }
            else if (u == 6)
            {
                u++;
            }
            else if (u == 7)
                {
                    label24.Visible = false;
                    label7.Visible = false;
                u++;
            }
            else if (u == 8)
                {
                    label24.Visible = true;
                    label7.Visible = true;
                u++;
            }
            else if (u == 9)
            {
                u++;
            }
            else if (u == 10)
                {
                    label24.Visible = false;
                    label7.Visible = false;
                    u = 1;
                    uu++;
                }
            if(uu==15)
            {
                label24.Visible = true;
                label7.Visible = true;
                timer1.Stop();
            }

        }

    }
}
