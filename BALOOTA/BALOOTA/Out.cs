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
    public partial class Out : Form
    {
        public Out()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        string name = "";
        float amount2 = 0;
        string[] items1;
        string[] items2;


        private void button6_Click_1(object sender, EventArgs e)
        {
            Program.mysignin.Show();
            Program.mysignin.Signin_Load(sender, e);
            string ww = "O";
            string page = "O";
            Program.mysignin.which(ww, page);
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
            textBox11.Text = amount.ToString() + "  (د.أ)";
            textBox12.Text = amount.ToString();
        }
        
        public void sho()
        {
            reg();
            SqlConnection conn30 = new SqlConnection(src);
            conn30.Open();
            SqlCommand cmd30 = new SqlCommand("select * from Employee", conn30);
            SqlDataReader dr30 = cmd30.ExecuteReader();
            while (dr30.Read())
            {
                if (comboBox1.Text==dr30["EmployeeName"].ToString())
                {
                    textBox8.Text = dr30["Salary"].ToString();
                    name = dr30["EmployeeName"].ToString();
                }
            }
            dr30.Close();

            SqlConnection conn301 = new SqlConnection(src);
            conn301.Open();
            SqlCommand cmd301 = new SqlCommand("select * from EmpOut", conn301);
            SqlDataReader dr301 = cmd301.ExecuteReader();
            float t = 0;
            while (dr301.Read())
            {
                if (comboBox1.Text == dr301["Name"].ToString() && DateTime.Parse(dr301["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr301["Date"].ToString()) <= DateTime.Parse(textBox9.Text))
                {
                    t = t + float.Parse(dr301["Amount"].ToString());
                }
            }
            dr30.Close();

            textBox6.Text = t.ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(comboBox1.Text!="")
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "OS";
                string page = "OS";
                Program.mysignin.which(ww, page);
            }
            else
            {
                MessageBox.Show("الرجاء إختيار الموظف");
            }
        }

        public void Out_Load(object sender, EventArgs e)
        {
            textBox10.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).ToShortDateString();
            textBox9.Text = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToShortDateString();
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            comboBox1.SelectedText = "";
            comboBox2.SelectedText = "";
            dateTimePicker1.Checked = false;
            dateTimePicker2.Checked = false;
            name = "";
            if (comboBox1.Items.Count > 1)
            { comboBox1.Items.Clear(); }
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from Employee", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            while (dr39.Read())
            {
                iitem = dr39["EmployeeName"].ToString();

                if (!comboBox1.Items.Contains(iitem) && iitem != "المبرمج")
                {
                    comboBox1.Items.Add(iitem);
                }
            }
            dr39.Close();
            if (comboBox2.Items.Count > 1)
            { comboBox2.Items.Clear(); }
            SqlConnection conn392 = new SqlConnection(src);
            conn392.Open();
            SqlCommand cmd392 = new SqlCommand("select * from StoreOut", conn392);
            string iitem2 = "";
            SqlDataReader dr392 = cmd392.ExecuteReader();
            while (dr392.Read())
            {
                iitem2 = dr392["Name"].ToString();

                if (!comboBox2.Items.Contains(iitem2))
                {
                    comboBox2.Items.Add(iitem2);
                }
            }
            dr392.Close();
            items2 = new string[comboBox2.Items.Count];
            comboBox2.Items.CopyTo(items2, 0);
            items1 = new string[comboBox1.Items.Count];
            comboBox1.Items.CopyTo(items1, 0);

            comboBox1.Focus();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox2.Text = theDate1.ToString();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            string theDate2 = dateTimePicker2.Value.ToString("dd/MM/yyyy");
            textBox5.Text = theDate2.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox2.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            comboBox1.Text = "";
            name = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            comboBox2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox6.Text!="" && textBox8.Text != "")
            {
                if(textBox2.Text!="")
                {
                    if(DateTime.Parse(textBox2.Text)>= DateTime.Parse(textBox10.Text) && DateTime.Parse(textBox2.Text) <= DateTime.Parse(textBox9.Text))
                    {
                        if (textBox3.Text != "")
                        {
                            if (float.Parse(textBox3.Text) >= 0)
                            {
                                if ((float.Parse(textBox3.Text) + float.Parse(textBox6.Text)) <= float.Parse(textBox8.Text) && float.Parse(textBox3.Text) <= float.Parse(textBox12.Text))
                                {
                                    if ((MessageBox.Show("هل انت متأكد من إضافة مصروف ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                                    {
                                        Program.mysignin.Show();
                                        Program.mysignin.Signin_Load(sender, e);
                                        string ww = "OEmp";
                                        string page = "OEmp";
                                        Program.mysignin.which(ww, page);
                                    }
                                }
                                else
                                {
                                    MessageBox.Show("لا يمكن صرف قيمة اكبر من الراتب أو الصندوق");
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
                MessageBox.Show("الرجاء إختيار الموظف");
            }
        }

        public void OEmp(bool ok, string n)
        {
            if(ok)
            {
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [EmpOut](Name,Date,Amount,Notes)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", name);
                cmd.Parameters.AddWithValue("@textBox2", textBox2.Text);
                cmd.Parameters.AddWithValue("@textBox3", textBox3.Text);
                cmd.Parameters.AddWithValue("@textBox4", textBox7.Text);
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();
                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                float tt = float.Parse(textBox12.Text) - float.Parse(textBox3.Text);
                cmd55.Parameters.AddWithValue("@textBox2", tt);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                SqlConnection con1 = new SqlConnection(src);
                SqlCommand cmd1 = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con1);
                cmd1.Parameters.AddWithValue("@textBox1", n);
                cmd1.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd1.Parameters.AddWithValue("@textBox3", "لقد تم إضافة مصروف موظف"+Environment.NewLine+"  الموظف  "+name+ "  التاريخ  " + textBox2.Text + "  القيمة  " + textBox3.Text + "  الملاحظات  " + textBox7.Text);
                cmd1.Parameters.AddWithValue("@textBox4", "AD");
                con1.Open();
                SqlDataReader dr21 = cmd1.ExecuteReader();

                MessageBox.Show("لقد تمت العملية بنجاح");
                textBox2.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox7.Text = "";
                textBox8.Text = "";
                comboBox1.Text = "";
                name = "";
                reg();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                if (textBox5.Text != "")
                {
                    if (DateTime.Parse(textBox5.Text) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(textBox5.Text) <= DateTime.Parse(textBox9.Text))
                    {
                        if (textBox4.Text != "")
                        {
                            if (float.Parse(textBox4.Text) >= 0)
                            {
                                amount2 = 0;
                                SqlConnection conne2 = new SqlConnection(src);
                                conne2.Open();
                                SqlCommand cmod2 = new SqlCommand("select max(Id) from Register", conne2);
                                int regid12 = Convert.ToInt32(cmod2.ExecuteScalar());
                                SqlConnection conn33162 = new SqlConnection(src);
                                conn33162.Open();
                                SqlCommand cmd33162 = new SqlCommand("select * from Register WHERE Id ='" + regid12 + "'", conn33162);
                                SqlDataReader dr83162 = cmd33162.ExecuteReader();
                                while (dr83162.Read())
                                {
                                    amount2 = float.Parse(dr83162["Amount"].ToString());
                                }
                                if (float.Parse(textBox4.Text) <= amount2)
                                {
                                    if ((MessageBox.Show("هل انت متأكد من إضافة مصروف ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                                    {
                                        Program.mysignin.Show();
                                        Program.mysignin.Signin_Load(sender, e);
                                        string ww = "OStore";
                                        string page = "OStore";
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
                MessageBox.Show("الرجاء إدخال اسم المصروف");
            }
        }

        public void OStore(bool ok2, string n2)
        {
            if (ok2)
            {
                SqlConnection con8 = new SqlConnection(src);
                SqlCommand cmd8 = new SqlCommand("INSERT INTO [StoreOut](Name,Date,Amount,Notes)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con8);
                cmd8.Parameters.AddWithValue("@textBox1", comboBox2.Text);
                cmd8.Parameters.AddWithValue("@textBox2", textBox5.Text);
                cmd8.Parameters.AddWithValue("@textBox3", textBox4.Text);
                cmd8.Parameters.AddWithValue("@textBox4", textBox1.Text);
                con8.Open();
                SqlDataReader dr28 = cmd8.ExecuteReader();
                SqlConnection con558 = new SqlConnection(src);
                SqlCommand cmd558 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con558);
                cmd558.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                float tt8 = amount2 - float.Parse(textBox4.Text);
                cmd558.Parameters.AddWithValue("@textBox2", tt8);
                con558.Open();
                SqlDataReader dr1558 = cmd558.ExecuteReader();

                SqlConnection con13 = new SqlConnection(src);
                SqlCommand cmd13 = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con13);
                cmd13.Parameters.AddWithValue("@textBox1", n2);
                cmd13.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd13.Parameters.AddWithValue("@textBox3", "لقد تم إضافة مصروف محل" + Environment.NewLine + "  المصروف  " + comboBox2.Text + "  التاريخ  " + textBox5.Text + "  القيمة  " + textBox4.Text + "  الملاحظات  " + textBox1.Text);
                cmd13.Parameters.AddWithValue("@textBox4", "AD");
                con13.Open();
                SqlDataReader dr213 = cmd13.ExecuteReader();

                MessageBox.Show("لقد تمت العملية بنجاح");
                textBox5.Text = "";
                textBox4.Text = "";
                textBox1.Text = "";
                comboBox2.Text = "";
                reg();
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
    }
}
