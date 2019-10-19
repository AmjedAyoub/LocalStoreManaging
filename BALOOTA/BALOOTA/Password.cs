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
    public partial class Password : Form
    {
        public Password()
        {
            InitializeComponent();
        }
        private string src = Program.xsrc;
        string name = "";

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton2.Checked)
            { panel2.Enabled = true; }
            else
            { panel2.Enabled = false;
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
               
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            { panel1.Enabled = true; }
            else
            {
                panel1.Enabled = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox7.Text = "";
            }
        }

        public void user(string u)
        {
            name = u;
        }

        public void Password_Load(object sender, EventArgs e)
        {

            panel2.Enabled = false;
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = ""; panel1.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {

            panel2.Enabled = false;
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = ""; panel1.Enabled = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox7.Text = "";
            radioButton1.Checked = false;
            radioButton2.Checked = false;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox3.Text != "" && textBox5.Text != "" &&  textBox6.Text != "")
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "Pass1";
                string page = "Pass1";
                Program.mysignin.which(ww, page);
            }
            else
            {
                MessageBox.Show("الرجاء إدخال المعلومات بشكل صحيح");
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }

        public void Pass1(bool ok1, string n1)
        {
            if(ok1 && name == n1)
            {
                SqlConnection conn331 = new SqlConnection(src);
                conn331.Open();
                SqlCommand cmd331 = new SqlCommand("select * from Employee", conn331);
                SqlDataReader dr831 = cmd331.ExecuteReader();
                string id = "";
                string username = "";
                string password = "";
                while (dr831.Read())
                {
                    if (n1 == dr831["EmployeeName"].ToString())
                    {
                        id = dr831["Id"].ToString();
                        username = dr831["Username"].ToString();
                        password = dr831["Password"].ToString();
                    }
                }

                if(username==textBox3.Text && password== textBox5.Text)
                {
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Employee] SET Username=@box1 WHERE Id = '" + id + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", textBox6.Text);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();
                    MessageBox.Show("لقد تمت العملية بنجاح");
                    panel2.Enabled = false;
                    textBox3.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = ""; panel1.Enabled = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
                else
                {
                    MessageBox.Show("خطأ في المعلومات");
                    textBox3.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = "";
                }
            }
            else
            {
                MessageBox.Show("لا يمكنك تغيير اسم المستخدم دون تسجيل الدخول الى حسابك الشخصي");
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }
        
        public void Pass2(bool ok2, string n2)
        {
            if (ok2 && name == n2)
            {
                SqlConnection conn3312 = new SqlConnection(src);
                conn3312.Open();
                SqlCommand cmd3312 = new SqlCommand("select * from Employee", conn3312);
                SqlDataReader dr8312 = cmd3312.ExecuteReader();
                string id2 = "";
                string username2 = "";
                string password2 = "";
                while (dr8312.Read())
                {
                    if (n2 == dr8312["EmployeeName"].ToString())
                    {
                        id2 = dr8312["Id"].ToString();
                        username2 = dr8312["Username"].ToString();
                        password2 = dr8312["Password"].ToString();
                    }
                }
                if (username2 == textBox4.Text && password2 == textBox2.Text)
                {
                    SqlConnection conn2 = new SqlConnection(src);
                    SqlCommand cmdn2 = new SqlCommand("UPDATE [Employee] SET Password=@box1 WHERE Id = '" + id2 + "'", conn2);
                    cmdn2.Parameters.AddWithValue("@box1", textBox7.Text);
                    conn2.Open();
                    SqlDataReader d722 = cmdn2.ExecuteReader();
                    conn2.Close();
                    MessageBox.Show("لقد تمت العملية بنجاح");
                    panel2.Enabled = false;
                    textBox3.Text = "";
                    textBox5.Text = "";
                    textBox6.Text = ""; panel1.Enabled = false;
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
                else
                {
                    MessageBox.Show("خطأ في المعلومات");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox4.Text = "";
                    textBox7.Text = "";
                }
            }
            else
            {
                MessageBox.Show("لا يمكنك تغيير كلمة السر دون تسجيل الدخول الى حسابك الشخصي");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox7.Text = "";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox7.Text != "")
            {
                if (textBox1.Text == textBox7.Text)
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    string ww = "Pass2";
                    string page = "Pass2";
                    Program.mysignin.which(ww, page);
                }
                else
                {
                    MessageBox.Show("كلمة السر الجديدة غير متطابقة");
                    textBox1.Text = "";
                    textBox7.Text = "";
                }
            }
            else
            {
                MessageBox.Show("الرجاء إدخال المعلومات بشكل صحيح");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox7.Text = "";
            }
        }

    }
}
