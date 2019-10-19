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
    public partial class Forget : Form
    {
        public Forget()
        {
            InitializeComponent();
        }
        private string src = Program.xsrc;


        public void Forget_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox4.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            this.Hide();
            textBox1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && textBox2.Text != "" && textBox4.Text != "" && textBox3.Text != "")
            {
                if (textBox1.Text == textBox2.Text)
                {
                    SqlConnection conn3312 = new SqlConnection(src);
                    conn3312.Open();
                    SqlCommand cmd3312 = new SqlCommand("select * from Employee", conn3312);
                    SqlDataReader dr8312 = cmd3312.ExecuteReader();
                    string id2 = "";
                    bool a = false;
                    while (dr8312.Read())
                    {
                        if (textBox4.Text == dr8312["EmployeeName"].ToString())
                        {
                            id2 = dr8312["Id"].ToString();
                            a = true;
                        }
                    }
                    if (a)
                    {
                        SqlConnection conn2 = new SqlConnection(src);
                        SqlCommand cmdn2 = new SqlCommand("UPDATE [Employee] SET Password=@box1, Username=@box2 WHERE Id = '" + id2 + "'", conn2);
                        cmdn2.Parameters.AddWithValue("@box1", textBox2.Text);
                        cmdn2.Parameters.AddWithValue("@box2", textBox3.Text);
                        conn2.Open();
                        SqlDataReader d722 = cmdn2.ExecuteReader();
                        conn2.Close();
                        MessageBox.Show("لقد تمت العملية بنجاح");
                        textBox3.Text = "";
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        this.Hide();
                        textBox4.Focus();
                    }
                    else
                    {
                        MessageBox.Show("خطأ في المعلومات الاسم غير موجود");
                        textBox1.Text = "";
                        textBox2.Text = "";
                        textBox4.Text = "";
                        textBox3.Text = "";
                        textBox4.Focus();

                    }
                }
                else
                {
                    MessageBox.Show("كلمة السر غير متطابقة");
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox2.Focus();
                }
            }
            else
            {
                MessageBox.Show("الرجاء إدخال المعلومات بشكل صحيح");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox3.Text = "";
                textBox4.Focus();
            }
        }

    }
}
