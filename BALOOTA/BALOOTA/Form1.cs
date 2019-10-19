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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        public static string username = "";
        public static bool signed=false;

        private void Form1_Load(object sender, EventArgs e)
        {
            signed = false;
            panel2.Enabled = false;
            panel2.Visible = false;
            timer1.Start();
            Form1 myfrm1 = new Form1();
            Form2 myfrm2 = new Form2();
            myfrm2.TopLevel = false;
            myfrm2.AutoScroll = true;
            myfrm2.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(myfrm2);
            myfrm2.Show();
            SqlConnection conne = new SqlConnection(src);
            conne.Open();
            SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
            int regid1 = Convert.ToInt32(cmod.ExecuteScalar());
            label6.Text = regid1.ToString();

            SqlConnection conne3 = new SqlConnection(src);
            conne3.Open();
            SqlCommand cmod3 = new SqlCommand("select min(Id) from Register", conne3);
            int regid3 = Convert.ToInt32(cmod3.ExecuteScalar());

            SqlConnection conne2 = new SqlConnection(src);
            conne2.Open();
            SqlCommand cmod2 = new SqlCommand("select max(Id) from Purchases", conne2);
            int regid2 = Convert.ToInt32(cmod2.ExecuteScalar());
            label7.Text = regid2.ToString();
            string date = "01/01/2019";

            for(int y=1;y<=51;y++)                //   @@@@@@@@@@@@@@@@@@@@@@@@@@@@    50 years guaranteed
            {
                        SqlConnection conn331 = new SqlConnection(src);
                        conn331.Open();
                        SqlCommand cmd331 = new SqlCommand("select * from Purchases", conn331);
                        SqlDataReader dr831 = cmd331.ExecuteReader();
                        while (dr831.Read())
                        {
                            if (dr831["Amount"].ToString() == "-1" && DateTime.Parse(DateTime.Now.AddYears(-y).ToShortDateString()) >= DateTime.Parse(DateTime.Parse(dr831["Date"].ToString()).ToShortDateString()))
                            {
                                string id = dr831["Id"].ToString();
                                SqlConnection cn1 = new SqlConnection(src);
                                SqlCommand cmd1 = new SqlCommand("DELETE FROM [Purchases] WHERE Id = @Irde", cn1);
                                cmd1.Parameters.AddWithValue("@Irde", id);
                                cn1.Open();
                                SqlDataReader dr1 = cmd1.ExecuteReader();

                                SqlConnection cn11 = new SqlConnection(src);
                                SqlCommand cmd11 = new SqlCommand("DELETE FROM [Items] WHERE IdPurchase = @Ir", cn11);
                                cmd11.Parameters.AddWithValue("@Ir", id);
                                cn11.Open();
                                SqlDataReader dr11 = cmd11.ExecuteReader();
                            }
                }
                SqlConnection conn1331 = new SqlConnection(src);
                conn1331.Open();
                SqlCommand cmd1331 = new SqlCommand("select * from Items", conn1331);
                SqlDataReader dr1831 = cmd1331.ExecuteReader();
                while (dr1831.Read())
                {
                    if (dr1831["RQuantity"].ToString() == "-1" && DateTime.Parse(DateTime.Now.AddYears(-y).ToShortDateString()) >= DateTime.Parse(DateTime.Parse(dr1831["Date"].ToString()).ToShortDateString()))
                    {
                        string id1 = dr1831["Id"].ToString();
                        SqlConnection cn11 = new SqlConnection(src);
                        SqlCommand cmd11 = new SqlCommand("DELETE FROM [Items] WHERE Id = @Irde", cn11);
                        cmd11.Parameters.AddWithValue("@Irde", id1);
                        cn11.Open();
                        SqlDataReader dr11 = cmd11.ExecuteReader();
                    }
                }
                for (int m = 1; m <= 12; m = m + 2)
                {
                    if(DateTime.Now.ToShortDateString() == DateTime.Parse(date).AddMonths(m).AddYears(y-1).ToShortDateString())
                    {
                        for (int i = regid3; i <= regid1 - 100; i++)
                        {
                            SqlConnection cn111 = new SqlConnection(src);
                            SqlCommand cmd111 = new SqlCommand("DELETE FROM [Register] WHERE Id = @Ird", cn111);
                            cmd111.Parameters.AddWithValue("@Ird", i);
                            cn111.Open();
                            SqlDataReader dr111 = cmd111.ExecuteReader();
                        }
                    }
                }
            }

        }

        public void Se(bool s, string g)
        {
            signed = s;
            username = g;
        }

        private void button1_Click(object sender, EventArgs e)
        {
          //  panel4.Visible = false;
           // panel5.Visible = false;
            Form1 myfrm1 = new Form1();
            Form2 myfrm2 = new Form2();
            myfrm2.TopLevel = false;
            myfrm2.AutoScroll = true;
            myfrm2.Dock = DockStyle.Fill;
            panel3.Controls.Clear();
            panel3.Controls.Add(myfrm2);
            myfrm2.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label2.Text = DateTime.Now.ToLongTimeString();
            label1.Text = DateTime.Now.ToShortDateString();
        }
        
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(panel2.Enabled)
            {
                panel2.Enabled = false;
                panel2.Visible = false;
                textBox2.Text = "";
                textBox1.Text = "";
            }
            else
            {
                panel2.Enabled = true;
                panel2.Visible = true;
            }
        }

        public void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if(textBox2.Text== label7.Text + "AA0318SSAd4c8pda6t3a47f" + label6.Text)
            {
                Program.myforget.Show();
                Program.myforget.Forget_Load(sender,e);
                panel2.Enabled = false;
                panel2.Visible = false;
                textBox2.Text = "";
                textBox1.Text = "";
            }
            else
            {
                panel2.Enabled = false;
                panel2.Visible = false;
                textBox2.Text = "";
                textBox1.Text = "";
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (signed)
            {
                if ((MessageBox.Show("هل انت متأكد من تسجيل الخروج و إغلاق البرنامج ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    e.Cancel = false;
                    SqlConnection con6w = new SqlConnection(src);
                    SqlCommand cmd6w = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con6w);
                    cmd6w.Parameters.AddWithValue("@textBox1", username);
                    cmd6w.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                    cmd6w.Parameters.AddWithValue("@textBox3", " لقد تم تسجيل خروج  ");
                    cmd6w.Parameters.AddWithValue("@textBox4", "IN");
                    con6w.Open();
                    SqlDataReader dr6w = cmd6w.ExecuteReader();
                }
                else
                {
                    e.Cancel = true;
                }
            }
            else
            {
                if ((MessageBox.Show("هل انت متأكد من إغلاق البرنامج ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }

            
        }
        
    }
}
