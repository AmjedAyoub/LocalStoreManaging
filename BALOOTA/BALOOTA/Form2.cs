using System;
using System.Windows.Forms;
using System.Threading;
using System.Data.Sql;
using System.Data.SqlClient;
// chash memory  bedal e3ml run between DB and RAM
using System.Data.Odbc;
using System.Configuration;

namespace BALOOTA
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }


        private string src = Program.xsrc;
        int t = 1;
        public string userid = "";
        public string username = "";
        public Panel pnl = new Panel();

        private void ادخالالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myinvoucein.TopLevel = false;
            Program.myinvoucein.AutoScroll = true;
            Program.myinvoucein.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myinvoucein);
            Program.myinvoucein.Show();
            Program.myinvoucein.Invoucein_Load(sender,e);
            linkLabel1.Visible = false;
        }

        private void تعديلالفواتيرToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.mypurchase.TopLevel = false;
            Program.mypurchase.AutoScroll = true;
            Program.mypurchase.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mypurchase);
            Program.mypurchase.Purches_Load(sender, e);
            Program.mypurchase.Show();
            linkLabel1.Visible = false;

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.mymain.TopLevel = false;
            Program.mymain.AutoScroll = true;
            Program.mymain.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mymain);
            Program.mymain.start();
            Program.mymain.Show();
            linkLabel1.Visible = true;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            panel1.Visible = true;
            panel2.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (t == 1) { label6.BackColor = System.Drawing.Color.Red; t++; }
            else if (t == 2) { label6.BackColor = System.Drawing.Color.Yellow; t++; }
            else if (t == 3) { label6.BackColor = System.Drawing.Color.Orange; t=1; }
        }

        private void تعديلعملياتالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.mysales.TopLevel = false;
            Program.mysales.AutoScroll = true;
            Program.mysales.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mysales);
            Program.mysales.Show();
            Program.mysales.S();
            linkLabel1.Visible = false;
        }

        private void عرضالفواتيروالاصنافToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myviewpur.TopLevel = false;
            Program.myviewpur.AutoScroll = true;
            Program.myviewpur.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myviewpur);
            Program.myviewpur.Show();
            Program.myviewpur.Viewpur_Load(sender,e);
            linkLabel1.Visible = false;
        }

        private void عرضعملياتالبيعToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myviewsale.TopLevel = false;
            Program.myviewsale.AutoScroll = true;
            Program.myviewsale.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myviewsale);
            Program.myviewsale.Viewsale_Load(sender, e);
            Program.myviewsale.Show();
            linkLabel1.Visible = false;
        }
        
        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            Program.myinventory.TopLevel = false;
            Program.myinventory.AutoScroll = true;
            Program.myinventory.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myinventory);
            Program.myinventory.Show();
            Program.myinventory.Inventory_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void ادخالالمصاريفToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myout.TopLevel = false;
            Program.myout.AutoScroll = true;
            Program.myout.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myout);
            Program.myout.Show();
            Program.myout.Out_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void تعديلمصاريفالموظفينToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myeditout.TopLevel = false;
            Program.myeditout.AutoScroll = true;
            Program.myeditout.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myeditout);
            Program.myeditout.Show();
            Program.myeditout.Editout_Load(sender,e);
            linkLabel1.Visible = false;
        }

        private void تعديلمصاريفالمحلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myeditstore.TopLevel = false;
            Program.myeditstore.AutoScroll = true;
            Program.myeditstore.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myeditstore);
            Program.myeditstore.Show();
            Program.myeditstore.Editstore_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void عرضToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myviewout.TopLevel = false;
            Program.myviewout.AutoScroll = true;
            Program.myviewout.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myviewout);
            Program.myviewout.Show();
            Program.myviewout.Viewout_Load(sender,e);
            linkLabel1.Visible = false;
        }

        private void اضافةToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myaddemp.TopLevel = false;
            Program.myaddemp.AutoScroll = true;
            Program.myaddemp.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myaddemp);
            Program.myaddemp.Show();
            Program.myaddemp.Addemp_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void تعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myeditemp.TopLevel = false;
            Program.myeditemp.AutoScroll = true;
            Program.myeditemp.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myeditemp);
            Program.myeditemp.Show();
            Program.myeditemp.Editemp_Load(sender,e);
            linkLabel1.Visible = false;
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            Program.mypassword.TopLevel = false;
            Program.mypassword.AutoScroll = true;
            Program.mypassword.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mypassword);
            Program.mypassword.Show();
            Program.mypassword.Password_Load(sender, e);
            Program.mypassword.user(label5.Text);
            linkLabel1.Visible = false;
        }
        
        private void خروجToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myref.TopLevel = false;
            Program.myref.AutoScroll = true;
            Program.myref.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myref);
            Program.myref.Show();
            Program.myref.Ref_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void خروجToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.myalert.TopLevel = false;
            Program.myalert.AutoScroll = true;
            Program.myalert.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myalert);
            Program.myalert.Show();
            linkLabel1.Visible = false;
        }
         
        private void خروجToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            if ((MessageBox.Show("هل انت متأكد من تسجيل الخروج؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                SqlConnection con6w = new SqlConnection(src);
                SqlCommand cmd6w = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con6w);
                cmd6w.Parameters.AddWithValue("@textBox1", label5.Text);
                cmd6w.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd6w.Parameters.AddWithValue("@textBox3", " لقد تم تسجيل خروج  ");
                cmd6w.Parameters.AddWithValue("@textBox4", "IN");
                con6w.Open();
                SqlDataReader dr6w = cmd6w.ExecuteReader();
                Program.myform1.Se(false, "");
                panel5.Controls.Clear();
                panel1.Visible = true;
                panel2.Visible = false;
                label4.Visible = false;
                label5.Visible = false;
                linkLabel1.Visible = false;
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
        }
        
        public void Emergency()
        {
            panel1.Visible = false;
            panel2.Visible = true;
            Program.mymain.TopLevel = false;
            Program.mymain.Visible = true;
            Program.mymain.AutoScroll = true;
            Program.mymain.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            this.Controls.Add(panel5);
            panel5.Controls.Add(Program.mymain);
            Program.mymain.Show();
        }

        public void button1_Click_1(object sender, EventArgs e)
        {
            string src = Program.xsrc; // path for DB
            SqlConnection con = new SqlConnection(src);
            bool blnfound = false; // the username and pass correct (ana b76 enoh false cuz bfred enoh feh eroor bl user or pass)
            con.Open();
                SqlCommand cmd = new SqlCommand("select * from Employee", con);
                SqlDataReader dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["Username"].ToString() == textBox1.Text && textBox1.Text != "")
                    {
                        if (dr["Password"].ToString() == textBox2.Text)
                        {
                            blnfound = true; // the username and pass corect
                            userid = dr["Id"].ToString();
                            username = dr["EmployeeName"].ToString();
                        }
                    }
            }
            SqlConnection conn3316 = new SqlConnection(src);
            conn3316.Open();
            SqlCommand cmd3316 = new SqlCommand("select * from F WHERE Id ='" + 1 + "'", conn3316);
            SqlDataReader dr8316 = cmd3316.ExecuteReader();
            string f = "";
            while (dr8316.Read())
            {
                f = dr8316["Name"].ToString();
            }
            if (blnfound == false) // the username and pass not corect
            {
                MessageBox.Show("خطأ في اسم المستخدم او كلمة السر \n الرجاء اعادة المحاولة ", "ادخال خاطئ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
            }
            else // the username and pass corect
            {
                if (f == "NO")
                {
                    Program.myF.Show();
                    Program.myF.F_Load(sender, e);
                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox2.Focus();
                }
                else if(f == "YES")
                {
                    dr.Close();
                    con.Close();
                    textBox1.Text = "";
                    textBox2.Text = "";
                    panel1.Visible = false;
                    panel2.Visible = true;
                    label5.Text = username;
                    label4.Visible = true;
                    label5.Visible = true;
                    SqlConnection con6w = new SqlConnection(src);
                    SqlCommand cmd6w = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con6w);
                    cmd6w.Parameters.AddWithValue("@textBox1", username);
                    cmd6w.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                    cmd6w.Parameters.AddWithValue("@textBox3", " لقد تم تسجيل دخول  ");
                    cmd6w.Parameters.AddWithValue("@textBox4", "IN");
                    con6w.Open();
                    SqlDataReader dr6w = cmd6w.ExecuteReader();
                    Program.myform1.Se(true, username);
                    Program.mymain.TopLevel = false;
                    Program.mymain.AutoScroll = true;
                    Program.mymain.Dock = DockStyle.Fill;
                    panel5.Controls.Clear();
                    panel5.Controls.Add(Program.mymain);
                    Program.mymain.start();
                    Program.mymain.l5();
                    Program.mymain.Show();
                    Program.mymain.usern = username;
                    linkLabel1.Visible = true;

                    string last = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)).ToShortDateString();
                    if (DateTime.Parse(DateTime.Now.ToShortDateString()) >= DateTime.Parse(last).AddDays(-5) && DateTime.Parse(DateTime.Now.ToShortDateString()) <= DateTime.Parse(last))
                    {
                        label6.Visible = true;
                        timer1.Start();
                    }
                    else
                    {
                        label6.Visible = false;
                    }
                }
            }
            
            
        }
        
        public void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // if press enter sha3'el  el log in
            {
                button1_Click_1((object)sender, (EventArgs)e);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Program.mysignin.Show();
            //Program.mysignin.Signin_Load(sender, e);
            //Program.mysignin.which("Sales", "Sales");
            Program.mysales.TopLevel = false;
            Program.mysales.AutoScroll = true;
            Program.mysales.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mysales);
            Program.mysales.Show();
            Program.mysales.S();
            linkLabel1.Visible = false;

        }

        public void MM(string page, object sender, EventArgs e)
        {
            if(page=="Sales")
            {
                Program.mysales.TopLevel = false;
                Program.mysales.AutoScroll = true;
                Program.mysales.Dock = DockStyle.Fill;
                this.panel5.Controls.Clear();
                this.panel5.Controls.Add(Program.mysales);
                Program.mysales.Show();
                Program.mysales.Sales_Load(sender, e);
                this.linkLabel1.Visible = false;
            }
        }

        private void ديرنالشركاتوالموزعينToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Program.mycoDebt.TopLevel = false;
            Program.mycoDebt.AutoScroll = true;
            Program.mycoDebt.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mycoDebt);
            Program.mycoDebt.Show();
            Program.mycoDebt.CoDebt_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void ديونالزبائنToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.mycusDebt.TopLevel = false;
            Program.mycusDebt.AutoScroll = true;
            Program.mycusDebt.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mycusDebt);
            Program.mycusDebt.Show();
            Program.mycusDebt.CusDebt_Load(sender, e);
            linkLabel1.Visible = false;

        }
        
        private void عرضالارباحToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myprofit.TopLevel = false;
            Program.myprofit.AutoScroll = true;
            Program.myprofit.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myprofit);
            Program.myprofit.Show();
            Program.myprofit.Profit_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void عرضالنسبةToolStripMenuItem_Click(object sender, EventArgs e)
        {// @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Samer
          /* @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@ Ayman 
            Program.myN.TopLevel = false;
            Program.myN.AutoScroll = true;
            Program.myN.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myN);
            Program.myN.Show();
            Program.myN.N_Load(sender, e);
            linkLabel1.Visible = false;
            //*/
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            Program.mySalary.TopLevel = false;
            Program.mySalary.AutoScroll = true;
            Program.mySalary.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mySalary);
            Program.mySalary.Show();
            Program.mySalary.Salary_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void إضافةتعديلToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myempDebt.TopLevel = false;
            Program.myempDebt.AutoScroll = true;
            Program.myempDebt.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myempDebt);
            Program.myempDebt.Show();
            Program.myempDebt.EmpDebt_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void إضافةتعديلToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Program.mystoreDebt.TopLevel = false;
            Program.mystoreDebt.AutoScroll = true;
            Program.mystoreDebt.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mystoreDebt);
            Program.mystoreDebt.Show();
            Program.mystoreDebt.StoreDebt_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void قبضToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.myempP.TopLevel = false;
            Program.myempP.AutoScroll = true;
            Program.myempP.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.myempP);
            Program.myempP.Show();
            Program.myempP.EmpP_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void دفعToolStripMenuItem_Click(object sender, EventArgs e)
        {

            Program.mystoreP.TopLevel = false;
            Program.mystoreP.AutoScroll = true;
            Program.mystoreP.Dock = DockStyle.Fill;
            panel5.Controls.Clear();
            panel5.Controls.Add(Program.mystoreP);
            Program.mystoreP.Show();
            Program.mystoreP.StoreP_Load(sender, e);
            linkLabel1.Visible = false;
        }

        private void label6_Click(object sender, EventArgs e)
        {
            timer1.Stop();
            label6.Visible = false;
        }
        
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter) // if press enter sha3'el  el log in
            {
                button1_Click_1((object)sender, (EventArgs)e);
            }
        }
        
    }
    
}
