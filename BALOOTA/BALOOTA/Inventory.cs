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
    public partial class Inventory : Form
    {
        public Inventory()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        int row1 = 0;
        int row11 = 0;
        string[] items1;

        public void Inventory_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox5.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox3.Text = "";
            comboBox1.Text = "";
            textBox4.Text = "";
            textBox10.Text = "";
            textBox8.Text = "";
            comboBox1.SelectedText = "";
            comboBox3.SelectedText = "";
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            textBox5.ReadOnly = true;
            if (comboBox3.Items.Count > 1)
            { comboBox3.Items.Clear(); comboBox1.Items.Clear(); }
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from Inventory", conn39);
            string iitem = "";
            row1 = 0;
            SqlDataReader dr39 = cmd39.ExecuteReader();
            comboBox3.Items.Add("");
            comboBox1.Items.Add("");
            comboBox3.Items.Add("الكل");
            while (dr39.Read())
            {
                iitem = dr39["Item"].ToString();

                if (!comboBox3.Items.Contains(iitem))
                {
                    comboBox3.Items.Add(iitem);
                    comboBox1.Items.Add(iitem);
                }
            }
            dr39.Close();
            items1 = new string[comboBox3.Items.Count];
            comboBox3.Items.CopyTo(items1, 0);
            dataGridView2.Rows.Clear();
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView2.Rows.Add(null, null, null, null);
                dataGridView1.Rows.Add(null, null, null);
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
                string ww = "SI";
                string page = "SI";
                Program.mysignin.which(ww, page);
            }
        }
        
        public void SI()
        {

            row11 = 0;
            if (comboBox3.Text != null && comboBox3.Text != "الكل")
            {
                SqlConnection conn = new SqlConnection(src);
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from Inventory", conn);
                SqlDataReader dr = cmd.ExecuteReader();
                dataGridView2.Rows.Clear();
                for (int ro = 0; ro < 15; ro++)
                {
                    dataGridView2.Rows.Add(null, null, null, null);
                }
                while (dr.Read())
                {
                    if (dr["Item"].ToString() == comboBox3.Text)

                    {
                        dataGridView2.Rows.Insert(row11, dr["Item"].ToString(), dr["Quantity"].ToString(), dr["MinQ"].ToString(), dr["Notes"].ToString());
                        row11++;
                    }
                }
                dr.Close();
            }
            else
            {
                SqlConnection conn399 = new SqlConnection(src);
                conn399.Open();
                SqlCommand cmd399 = new SqlCommand("select * from Inventory", conn399);
                row11 = 0;
                SqlDataReader dr399 = cmd399.ExecuteReader();
                dataGridView2.Rows.Clear();
                for (int ro = 0; ro < 15; ro++)
                {
                    dataGridView2.Rows.Add(null, null, null, null);
                }
                while (dr399.Read())
                {
                    dataGridView2.Rows.Insert(row11, dr399["Item"].ToString(), dr399["Quantity"].ToString(), dr399["MinQ"].ToString(), dr399["Notes"].ToString());
                    row11++;

                }
                dr399.Close();
            }
            this.dataGridView2.Sort(this.dataGridView2.Columns[0], ListSortDirection.Descending);
            for (int y = 0; y < row11; y++)
            {
                this.dataGridView2.Rows[y].HeaderCell.Value = (y + 1).ToString();
            }


            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
            float RQ = 0;
            float P = 0;
            float total = 0;
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                if (float.Parse(dr3["RQuantity"].ToString()) > 0)
                {
                    RQ = float.Parse(dr3["RQuantity"].ToString());
                    P = float.Parse(dr3["Price"].ToString());
                    total = total + (RQ * P);
                }
            }
            dr3.Close();
            textBox5.Text = total + "  (د.أ)";

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
            textBox1.Text = amount.ToString() + "  (د.أ)";
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            comboBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text=="" || textBox2.Text=="" || textBox3.Text=="")
            {
                MessageBox.Show("الرجاء إدخال المعلومات بشكل صحيح");
            }
            else if (float.Parse(textBox3.Text) >= 0)
            {
                if ((MessageBox.Show("هل انت متأكد من هذه العملية ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    string ww = "F";
                    string page = "F";
                    Program.mysignin.which(ww, page);

                }
            }
            else
            {

                MessageBox.Show("لا يجوز ان تكون الكمية اقل من صفر");
            }
        }

        public void F(bool k, string a)
        {
            if (k)
            {
                SqlConnection conn33162 = new SqlConnection(src);
                conn33162.Open();
                SqlCommand cmd33162 = new SqlCommand("select * from Items", conn33162);
                SqlDataReader dr83162 = cmd33162.ExecuteReader();
                float reg = 0;
                int ii = 0;
                float t = 0;
                float pr = 0;
                bool qm = false;
                while (dr83162.Read())
                {
                    if (float.Parse(dr83162["RQuantity"].ToString()) > 0 && float.Parse(dr83162["RQuantity"].ToString()) >= float.Parse(textBox3.Text) && dr83162["Id"].ToString() == textBox2.Text && dr83162["ItemName"].ToString() == comboBox1.Text)
                    {
                        reg = float.Parse(dr83162["RQuantity"].ToString());
                        pr = float.Parse(dr83162["Price"].ToString());
                        ii = int.Parse(dr83162["Id"].ToString());
                        t = reg - float.Parse(textBox3.Text);
                        qm = true;
                    }
                }
                if (qm)
                {
                    SqlConnection co2 = new SqlConnection(src);
                    SqlCommand cmdn2 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + ii + "'", co2);
                    cmdn2.Parameters.AddWithValue("@box1", t);
                    co2.Open();
                    SqlDataReader d555 = cmdn2.ExecuteReader();
                    co2.Close();

                    SqlConnection conn62 = new SqlConnection(src);
                    conn62.Open();
                    SqlCommand cmd62 = new SqlCommand("select * from Inventory WHERE Item ='" + comboBox1.Text + "'", conn62);
                    SqlDataReader dr62 = cmd62.ExecuteReader();
                    float quan = 0;
                    int w = 0;
                    while (dr83162.Read())
                    {
                        quan = float.Parse(dr83162["Quantity"].ToString());
                        w = int.Parse(dr83162["Id"].ToString());
                    }

                    SqlConnection co22 = new SqlConnection(src);
                    SqlCommand cmdn22 = new SqlCommand("UPDATE [Inventory] SET Quantity=@box1 WHERE Id = '" + w + "'", co22);
                    cmdn22.Parameters.AddWithValue("@box1", quan - float.Parse(textBox3.Text));
                    co22.Open();
                    SqlDataReader d22 = cmdn22.ExecuteReader();
                    co22.Close();

                    SqlConnection con515 = new SqlConnection(src);
                    SqlCommand cmd515 = new SqlCommand("INSERT INTO [Destroy](Date,Amount)VALUES (@textBox1,@textBox2)", con515);
                    cmd515.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                    cmd515.Parameters.AddWithValue("@textBox2", float.Parse(textBox3.Text) * pr);
                    con515.Open();
                    SqlDataReader dr1515 = cmd515.ExecuteReader();

                    SqlConnection con = new SqlConnection(src);
                    SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                    cmd.Parameters.AddWithValue("@textBox1", a);
                    cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                    cmd.Parameters.AddWithValue("@textBox3", "لقد تم حذف بضاعة فاسدة من المستودع" + Environment.NewLine + "  الصنف  " + comboBox1.Text + "  رمز الحركه للصنف   " + textBox2.Text + "  الكمية    " + textBox3.Text);
                    cmd.Parameters.AddWithValue("@textBox4", "DELA");
                    con.Open();
                    SqlDataReader dr2 = cmd.ExecuteReader();
                    MessageBox.Show("لقد تمت العملية بنجاح ");

                    textBox2.Text = "";
                    textBox3.Text = "";
                    comboBox1.Text = "";

                    SqlConnection conn3991 = new SqlConnection(src);
                    conn3991.Open();
                    SqlCommand cmd3991 = new SqlCommand("select * from Inventory", conn3991);
                    row11 = 0;
                    SqlDataReader dr3991 = cmd3991.ExecuteReader();
                    dataGridView2.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView2.Rows.Add(null, null, null, null);
                    }
                    while (dr3991.Read())
                    {
                        dataGridView2.Rows.Insert(row11, dr3991["Item"].ToString(), dr3991["Quantity"].ToString(), dr3991["MinQ"].ToString(), dr3991["Notes"].ToString());
                        row11++;

                    }
                    dr3991.Close();
                    this.dataGridView2.Sort(this.dataGridView2.Columns[0], ListSortDirection.Descending);
                    for (int y = 0; y < row11; y++)
                    {
                        this.dataGridView2.Rows[y].HeaderCell.Value = (y + 1).ToString();
                    }


                    SqlConnection conn30 = new SqlConnection(src);
                    conn30.Open();
                    SqlCommand cmd30 = new SqlCommand("select * from Items", conn30);
                    float RQp = 0;
                    float Pp = 0;
                    float totalp = 0;
                    SqlDataReader dr30 = cmd30.ExecuteReader();
                    while (dr30.Read())
                    {
                        if (float.Parse(dr30["RQuantity"].ToString()) > 0)
                        {
                            RQp = float.Parse(dr30["RQuantity"].ToString());
                            Pp = float.Parse(dr30["Price"].ToString());
                            totalp = totalp + (RQp * Pp);
                        }
                    }
                    dr30.Close();
                    textBox5.Text = totalp + "  (د.أ)";


                }
                else
                {
                    MessageBox.Show("خطأ في المعلومات");
                }

            }
        }
        
        private void button4_Click(object sender, EventArgs e)
        {

            textBox4.Text = "";
            textBox4.Focus();
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

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "" && textBox8.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "SIR";
                string page = "SIR";
                Program.mysignin.which(ww, page);
            }
        }

        public void SIR()
        {
            if (textBox8.Text == "" && textBox10.Text != "")
            {
                SqlConnection con7 = new SqlConnection(src);
                con7.Open();
                SqlCommand cmd7 = new SqlCommand("select * from Register", con7);
                SqlDataReader dr = cmd7.ExecuteReader();
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null);
                }
                row1 = 0;
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                    {
                        dataGridView1.Rows.Insert(row1, row1, dr["Date"].ToString(), dr["Amount"].ToString());
                        row1++;
                    }
                }

            }
            else if (textBox8.Text != "" && textBox10.Text == "")
            {
                SqlConnection con8 = new SqlConnection(src);
                con8.Open();
                SqlCommand cmd8 = new SqlCommand("select * from Register", con8);
                SqlDataReader dr = cmd8.ExecuteReader();
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null);
                }
                row1 = 0;
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                    {
                        dataGridView1.Rows.Insert(row1, row1, dr["Date"].ToString(), dr["Amount"].ToString());
                        row1++;
                    }
                }

            }
            else if (textBox8.Text != "" && textBox10.Text != "")
            {
                SqlConnection con9 = new SqlConnection(src);
                con9.Open();
                SqlCommand cmd9 = new SqlCommand("select * from Register", con9);
                SqlDataReader dr = cmd9.ExecuteReader();
                dataGridView1.Rows.Clear();
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(null, null, null);
                }
                row1 = 0;
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                    {

                        dataGridView1.Rows.Insert(row1, row1, dr["Date"].ToString(), dr["Amount"].ToString());
                        row1++;
                    }

                }

            }
            this.dataGridView1.Sort(this.dataGridView1.Columns[0], ListSortDirection.Descending);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox4.Text!="" && float.Parse(textBox4.Text) >= 0)
            {
                if ((MessageBox.Show("هل انت متأكد من إدخال مبلغ الى الصندوق ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "Reg";
                        string page = "Reg";
                        Program.mysignin.which(ww, page);
                    
                }
            }
            else
            {

                MessageBox.Show("لا يجوز ان تكون القيمة اقل من صفر");
            }
        }

        public void Reg(bool ok, string name)
        {
            if (ok)
            {
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
                total = amount + float.Parse(textBox4.Text);
                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                cmd55.Parameters.AddWithValue("@textBox2", total);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();
                textBox1.Text = total.ToString() + "  (د.أ)";

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", name);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم اضافة مبلغ الى الصندوق" + Environment.NewLine + "  القيمة  " + textBox4.Text);
                cmd.Parameters.AddWithValue("@textBox4", "AD");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();
                MessageBox.Show("لقد تم إضافة المبلغ بنجاح ");
                textBox4.Text = "";
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
