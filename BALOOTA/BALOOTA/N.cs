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
using System.Configuration;

namespace BALOOTA
{
    public partial class N : Form
    {
        public N()
        {
            InitializeComponent();
        }

        bool u1 = false;
        bool u2 = false;
        float reg1 = 0;
        private string src = Program.xsrc;

        public void N_Load(object sender, EventArgs e)
        {
            u1 = false;
            u2 = false;
            reg1 = 0;
            textBox1.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            textBox17.Text = "";
            textBox18.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox19.Text = "";
            textBox20.Text = "";
            textBox5.Text = "";
            textBox5.Text = "50";
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
            textBox26.Text = "";
            textBox27.Text = "";
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;end = false;p = false;
            dateTimePicker4.Focus();
        }
        
        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker4.Value.ToString("dd/MM/yyyy");
            textBox10.Text = theDate1.ToString();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            string theDate11 = dateTimePicker3.Value.ToString("dd/MM/yyyy");
            textBox8.Text = theDate11.ToString();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
                try
                {
                    if (textBox5.Text != "")
                    {
                        textBox12.Text = (100 - float.Parse(textBox5.Text)).ToString();
                        if(float.Parse(textBox12.Text)<0 || float.Parse(textBox12.Text) > 100)
                        {
                            MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                            textBox5.Text = "50";
                        }
                        else
                        {
                            u1 = false;
                            u2 = false;
                            textBox1.Text = "";
                            textBox10.Text = "";
                            textBox11.Text = "";
                            textBox13.Text = "";
                            textBox14.Text = "";
                            textBox15.Text = "";
                            textBox16.Text = "";
                            textBox17.Text = "";
                            textBox18.Text = "";
                            textBox2.Text = "";
                            textBox3.Text = "";
                            textBox4.Text = "";
                            textBox6.Text = "";
                            textBox7.Text = "";
                            textBox8.Text = "";
                            textBox9.Text = "";
                            textBox19.Text = "";
                            textBox20.Text = "";
                            textBox21.Text = "";
                            textBox22.Text = "";
                            textBox23.Text = "";
                            textBox24.Text = "";
                            textBox25.Text = "";
                            textBox26.Text = "";
                        textBox27.Text = "";
                        dateTimePicker3.Checked = false;
                            dateTimePicker4.Checked = false;
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                    textBox5.Text = "50";
                }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox10.Text == "" && textBox8.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "NS";
                string page = "NS";
                Program.mysignin.which(ww, page);
            }
        }

        public void NS()
        {
            end = true;
            float profit = 0;
            float aymanb = 0;
            float salemb = 0;
            float aymandebt = 0;
            float salemdebt = 0;
            float nayman = 0;
            float nsalem = 0;
            float allaymandebt = 0;
            float allsalemdebt = 0;
            float sout = 0;
            float eout = 0;
            float dis = 0;

            if (textBox8.Text != "" && textBox10.Text != "")
            {
                SqlConnection con9 = new SqlConnection(src);
                con9.Open();
                SqlCommand cmd9 = new SqlCommand("select * from Purchases", con9);
                SqlDataReader dr = cmd9.ExecuteReader();
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr["Amount"].ToString()) > 0)

                    {
                        dis = dis + float.Parse(dr["Dis"].ToString());
                    }
                }
                SqlConnection con1 = new SqlConnection(src);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Sales", con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr1["Amount"].ToString()) > 0)

                    {
                        profit = profit + float.Parse(dr1["Profit"].ToString());
                    }
                }
                SqlConnection con2 = new SqlConnection(src);
                con2.Open();
                SqlCommand cmd2 = new SqlCommand("select * from StoreOut", con2);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    if (DateTime.Parse(dr2["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr2["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr2["Amount"].ToString()) > 0)

                    {
                        sout = sout + float.Parse(dr2["Amount"].ToString());
                    }
                }

                SqlConnection con33 = new SqlConnection(src);
                con33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from EmpOut", con33);
                SqlDataReader dr33 = cmd33.ExecuteReader();
                while (dr33.Read())
                {
                    if (DateTime.Parse(dr33["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr33["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr33["Amount"].ToString()) > 0)

                    {
                        eout = eout + float.Parse(dr33["Amount"].ToString());
                    }
                }

                SqlConnection con3 = new SqlConnection(src);
                con3.Open();
                SqlCommand cmd3 = new SqlCommand("select * from EmpDebt", con3);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if (dr3["Name"].ToString()=="ايمن عويس" && DateTime.Parse(dr3["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr3["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr3["Amount"].ToString()) > 0)

                    {
                        aymandebt = aymandebt + float.Parse(dr3["Amount"].ToString());
                    }
                }
                SqlConnection con32 = new SqlConnection(src);
                con32.Open();
                SqlCommand cmd32 = new SqlCommand("select * from EmpDebt", con32);
                SqlDataReader dr32 = cmd32.ExecuteReader();
                while (dr32.Read())
                {
                    if (dr32["Name"].ToString() == "سالم عويسات" && DateTime.Parse(dr32["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr32["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr32["Amount"].ToString()) > 0)

                    {
                        salemdebt = salemdebt + float.Parse(dr32["Amount"].ToString());
                    }
                }
                SqlConnection con39 = new SqlConnection(src);
                con39.Open();
                SqlCommand cmd39 = new SqlCommand("select * from EmpDebt", con39);
                SqlDataReader dr39 = cmd39.ExecuteReader();
                while (dr39.Read())
                {
                    if (dr39["Name"].ToString() == "ايمن عويس"  && float.Parse(dr39["Amount"].ToString()) > 0)

                    {
                        allaymandebt = allaymandebt + float.Parse(dr39["Amount"].ToString());
                    }
                }
                SqlConnection con329 = new SqlConnection(src);
                con329.Open();
                SqlCommand cmd329 = new SqlCommand("select * from EmpDebt", con329);
                SqlDataReader dr329 = cmd329.ExecuteReader();
                while (dr329.Read())
                {
                    if (dr329["Name"].ToString() == "سالم عويسات" && float.Parse(dr329["Amount"].ToString()) > 0)

                    {
                        allsalemdebt = allsalemdebt + float.Parse(dr329["Amount"].ToString());
                    }
                }


                SqlConnection con66 = new SqlConnection(src);
                con66.Open();
                SqlCommand cmd66 = new SqlCommand("select * from Balance", con66);
                SqlDataReader dr66 = cmd66.ExecuteReader();
                while (dr66.Read())
                {
                    if (dr66["Name"].ToString() == "ايمن عويس")

                    {
                        nayman = float.Parse(dr66["Amount"].ToString());
                    }
                    if (dr66["Name"].ToString() == "سالم عويسات")

                    {
                        nsalem = float.Parse(dr66["Amount"].ToString());
                    }
                }

            }
            else if (textBox8.Text == "" && textBox10.Text != "")
            {

                SqlConnection con9 = new SqlConnection(src);
                con9.Open();
                SqlCommand cmd9 = new SqlCommand("select * from Purchases", con9);
                SqlDataReader dr = cmd9.ExecuteReader();
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr["Amount"].ToString()) > 0)

                    {
                        dis = dis + float.Parse(dr["Dis"].ToString());
                    }
                }
                SqlConnection con1 = new SqlConnection(src);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Sales", con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr1["Amount"].ToString()) > 0)

                    {
                        profit = profit + float.Parse(dr1["Profit"].ToString());
                    }
                }
                SqlConnection con2 = new SqlConnection(src);
                con2.Open();
                SqlCommand cmd2 = new SqlCommand("select * from StoreOut", con2);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    if (DateTime.Parse(dr2["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr2["Amount"].ToString()) > 0)

                    {
                        sout = sout + float.Parse(dr2["Amount"].ToString());
                    }
                }

                SqlConnection con33 = new SqlConnection(src);
                con33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from EmpOut", con33);
                SqlDataReader dr33 = cmd33.ExecuteReader();
                while (dr33.Read())
                {
                    if (DateTime.Parse(dr33["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr33["Amount"].ToString()) > 0)

                    {
                        eout = eout + float.Parse(dr33["Amount"].ToString());
                    }
                }

                SqlConnection con3 = new SqlConnection(src);
                con3.Open();
                SqlCommand cmd3 = new SqlCommand("select * from EmpDebt", con3);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if (dr3["Name"].ToString() == "ايمن عويس" && DateTime.Parse(dr3["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr3["Amount"].ToString()) > 0)

                    {
                        aymandebt = aymandebt + float.Parse(dr3["Amount"].ToString());
                    }
                }
                SqlConnection con32 = new SqlConnection(src);
                con32.Open();
                SqlCommand cmd32 = new SqlCommand("select * from EmpDebt", con32);
                SqlDataReader dr32 = cmd32.ExecuteReader();
                while (dr32.Read())
                {
                    if (dr32["Name"].ToString() == "سالم عويسات" && DateTime.Parse(dr32["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr32["Amount"].ToString()) > 0)

                    {
                        salemdebt = salemdebt + float.Parse(dr32["Amount"].ToString());
                    }
                }
                SqlConnection con39 = new SqlConnection(src);
                con39.Open();
                SqlCommand cmd39 = new SqlCommand("select * from EmpDebt", con39);
                SqlDataReader dr39 = cmd39.ExecuteReader();
                while (dr39.Read())
                {
                    if (dr39["Name"].ToString() == "ايمن عويس" && float.Parse(dr39["Amount"].ToString()) > 0)

                    {
                        allaymandebt = allaymandebt + float.Parse(dr39["Amount"].ToString());
                    }
                }
                SqlConnection con329 = new SqlConnection(src);
                con329.Open();
                SqlCommand cmd329 = new SqlCommand("select * from EmpDebt", con329);
                SqlDataReader dr329 = cmd329.ExecuteReader();
                while (dr329.Read())
                {
                    if (dr329["Name"].ToString() == "سالم عويسات" && float.Parse(dr329["Amount"].ToString()) > 0)

                    {
                        allsalemdebt = allsalemdebt + float.Parse(dr329["Amount"].ToString());
                    }
                }
                SqlConnection con66 = new SqlConnection(src);
                con66.Open();
                SqlCommand cmd66 = new SqlCommand("select * from Balance", con66);
                SqlDataReader dr66 = cmd66.ExecuteReader();
                while (dr66.Read())
                {
                    if (dr66["Name"].ToString() == "ايمن عويس")

                    {
                        nayman = float.Parse(dr66["Amount"].ToString());
                    }
                    if (dr66["Name"].ToString() == "سالم عويسات")

                    {
                        nsalem = float.Parse(dr66["Amount"].ToString());
                    }
                }

            }
            else if (textBox8.Text != "" && textBox10.Text == "")
            {

                SqlConnection con9 = new SqlConnection(src);
                con9.Open();
                SqlCommand cmd9 = new SqlCommand("select * from Purchases", con9);
                SqlDataReader dr = cmd9.ExecuteReader();
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr["Amount"].ToString()) > 0)

                    {
                        dis = dis + float.Parse(dr["Dis"].ToString());
                    }
                }
                SqlConnection con1 = new SqlConnection(src);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Sales", con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr1["Amount"].ToString()) > 0)

                    {
                        profit = profit + float.Parse(dr1["Profit"].ToString());
                    }
                }
                SqlConnection con2 = new SqlConnection(src);
                con2.Open();
                SqlCommand cmd2 = new SqlCommand("select * from StoreOut", con2);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    if (DateTime.Parse(dr2["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr2["Amount"].ToString()) > 0)

                    {
                        sout = sout + float.Parse(dr2["Amount"].ToString());
                    }
                }

                SqlConnection con33 = new SqlConnection(src);
                con33.Open();
                SqlCommand cmd33 = new SqlCommand("select * from EmpOut", con33);
                SqlDataReader dr33 = cmd33.ExecuteReader();
                while (dr33.Read())
                {
                    if ( DateTime.Parse(dr33["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr33["Amount"].ToString()) > 0)

                    {
                        eout = eout + float.Parse(dr33["Amount"].ToString());
                    }
                }

                SqlConnection con3 = new SqlConnection(src);
                con3.Open();
                SqlCommand cmd3 = new SqlCommand("select * from EmpDebt", con3);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if (dr3["Name"].ToString() == "ايمن عويس"  && DateTime.Parse(dr3["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr3["Amount"].ToString()) > 0)

                    {
                        aymandebt = aymandebt + float.Parse(dr3["Amount"].ToString());
                    }
                }
                SqlConnection con32 = new SqlConnection(src);
                con32.Open();
                SqlCommand cmd32 = new SqlCommand("select * from EmpDebt", con32);
                SqlDataReader dr32 = cmd32.ExecuteReader();
                while (dr32.Read())
                {
                    if (dr32["Name"].ToString() == "سالم عويسات" && DateTime.Parse(dr32["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr32["Amount"].ToString()) > 0)

                    {
                        salemdebt = salemdebt + float.Parse(dr32["Amount"].ToString());
                    }
                }
                SqlConnection con39 = new SqlConnection(src);
                con39.Open();
                SqlCommand cmd39 = new SqlCommand("select * from EmpDebt", con39);
                SqlDataReader dr39 = cmd39.ExecuteReader();
                while (dr39.Read())
                {
                    if (dr39["Name"].ToString() == "ايمن عويس" && float.Parse(dr39["Amount"].ToString()) > 0)

                    {
                        allaymandebt = allaymandebt + float.Parse(dr39["Amount"].ToString());
                    }
                }
                SqlConnection con329 = new SqlConnection(src);
                con329.Open();
                SqlCommand cmd329 = new SqlCommand("select * from EmpDebt", con329);
                SqlDataReader dr329 = cmd329.ExecuteReader();
                while (dr329.Read())
                {
                    if (dr329["Name"].ToString() == "سالم عويسات" && float.Parse(dr329["Amount"].ToString()) > 0)

                    {
                        allsalemdebt = allsalemdebt + float.Parse(dr329["Amount"].ToString());
                    }
                }
                SqlConnection con66 = new SqlConnection(src);
                con66.Open();
                SqlCommand cmd66 = new SqlCommand("select * from Balance", con66);
                SqlDataReader dr66 = cmd66.ExecuteReader();
                while (dr66.Read())
                {
                    if (dr66["Name"].ToString() == "ايمن عويس")

                    {
                        nayman = float.Parse(dr66["Amount"].ToString());
                    }
                    if (dr66["Name"].ToString() == "سالم عويسات")

                    {
                        nsalem = float.Parse(dr66["Amount"].ToString());
                    }
                }

            }

            textBox6.Text = profit.ToString();
            textBox19.Text = sout.ToString();
            textBox20.Text = eout.ToString();
            textBox27.Text = dis.ToString();
            textBox1.Text = allaymandebt.ToString();
            textBox2.Text = aymandebt.ToString();
            textBox13.Text = allsalemdebt.ToString();
            textBox18.Text = salemdebt.ToString();
            textBox3.Text = nayman.ToString();
            textBox4.Text = nsalem.ToString();
            textBox7.Text = ((float.Parse(textBox6.Text)+ float.Parse(textBox27.Text)) - (float.Parse(textBox19.Text) + float.Parse(textBox20.Text))).ToString();
            textBox9.Text = (float.Parse(textBox7.Text) * (float.Parse(textBox5.Text) / 100)).ToString();
            textBox14.Text = textBox9.Text;
            textBox11.Text = (float.Parse(textBox7.Text) * (float.Parse(textBox12.Text) / 100)).ToString();
            textBox17.Text = textBox11.Text;
            textBox15.Text = "0.0";
            textBox16.Text = "0.0";
        }
        
        private void button3_Click(object sender, EventArgs e)
        {
            Program.mysignin.Show();
            Program.mysignin.Signin_Load(sender, e);
            string ww = "NU";
            string page = "NU";
            Program.mysignin.which(ww, page);
        }

        public void NU()
        {

            SqlConnection con66 = new SqlConnection(src);
            con66.Open();
            SqlCommand cmd66 = new SqlCommand("select * from Balance", con66);
            SqlDataReader dr66 = cmd66.ExecuteReader();
            while (dr66.Read())
            {
                if (dr66["Name"].ToString() == "ايمن عويس")

                {
                    textBox21.Text = float.Parse(dr66["Amount"].ToString()).ToString();
                    textBox25.Text = dr66["Id"].ToString();
                }
                if (dr66["Name"].ToString() == "سالم عويسات")

                {
                    textBox22.Text = float.Parse(dr66["Amount"].ToString()).ToString();
                    textBox26.Text = dr66["Id"].ToString();
                }
            }
        }
        
        private void button6_Click(object sender, EventArgs e)
        {
            textBox21.Text = "";
            textBox22.Text = "";
            textBox23.Text = "";
            textBox24.Text = "";
            textBox25.Text = "";
            textBox26.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (textBox25.Text != "" && textBox26.Text != "")
            {
                if (textBox23.Text != "" && textBox24.Text != "")
                {
                    
                    if ((MessageBox.Show("هل انت متأكد من تعديل الرصيد ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
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
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "NUN";
                        string page = "NUN";
                        Program.mysignin.which(ww, page);
                    }
                }
                else
                {
                    MessageBox.Show("الرجاء ادخال القيم");
                }
            }
            else
            {
                MessageBox.Show("الرجاء عرض القيم اولا");
            }
        }

        public void NUN(bool ok, string n, object sender, EventArgs e)
        {
            try
            {
                if (ok)
                {
                    if (n == "ايمن عويس")
                    {
                        u1 = true;
                    }
                    if (n == "سالم عويسات")
                    {
                        u2 = true;
                    }
                    if (u1 && u2)
                    {
                        float AO = float.Parse(textBox21.Text) + float.Parse(textBox23.Text);
                        float SO = float.Parse(textBox22.Text) + float.Parse(textBox24.Text);
                        SqlConnection conn5 = new SqlConnection(src);
                        SqlCommand cmdn5 = new SqlCommand("UPDATE [Balance] SET Amount=@box3 WHERE Id = '" + textBox25.Text + "'", conn5);
                        cmdn5.Parameters.AddWithValue("@box3", AO);
                        conn5.Open();
                        SqlDataReader d725 = cmdn5.ExecuteReader();
                        conn5.Close();

                        SqlConnection conn55 = new SqlConnection(src);
                        SqlCommand cmdn55 = new SqlCommand("UPDATE [Balance] SET Amount=@box3 WHERE Id = '" + textBox26.Text + "'", conn55);
                        cmdn55.Parameters.AddWithValue("@box3", SO);
                        conn55.Open();
                        SqlDataReader d7255 = cmdn55.ExecuteReader();
                        conn55.Close();

                        SqlConnection con505 = new SqlConnection(src);
                        SqlCommand cmd505 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con505);
                        cmd505.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                        string t7 = (reg1 + float.Parse(textBox23.Text) + float.Parse(textBox24.Text)).ToString();
                        cmd505.Parameters.AddWithValue("@textBox2", t7);
                        con505.Open();
                        SqlDataReader dr1505 = cmd505.ExecuteReader();

                        SqlConnection con = new SqlConnection(src);
                        SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                        cmd.Parameters.AddWithValue("@textBox1", "ايمن عويس/سالم عويسات");
                        cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@textBox3", "لقد تم اضافة مبلغ الى الرصيد" + Environment.NewLine + "  التاريخ  " + DateTime.Now.ToShortDateString() + "  ايمن عويس  " + textBox23.Text + "  سالم عويسات  " + textBox24.Text);
                        cmd.Parameters.AddWithValue("@textBox4", "UP");
                        con.Open();
                        SqlDataReader dr2 = cmd.ExecuteReader();

                        MessageBox.Show("لقد تم تعديل الرصيد بنجاح");
                        u1 = false;
                        u2 = false;
                        reg1 = 0;
                        textBox1.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";
                        textBox13.Text = "";
                        textBox14.Text = "";
                        textBox15.Text = "";
                        textBox16.Text = "";
                        textBox17.Text = "";
                        textBox18.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                        textBox19.Text = "";
                        textBox20.Text = "";
                        textBox5.Text = "50";
                        textBox21.Text = "";
                        textBox22.Text = "";
                        textBox23.Text = "";
                        textBox24.Text = "";
                        textBox25.Text = "";
                        textBox26.Text = "";
                        textBox27.Text = "";
                    }
                    else
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "NUN";
                        string page = "NUN";
                        Program.mysignin.which(ww, page);
                    }
                }
            }
            catch
            {

                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                textBox23.Text = ""; textBox24.Text = "";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox25.Text != "" && textBox26.Text != "")
            {
                if (textBox23.Text != "" && textBox24.Text != "")
                {
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
                    if ((float.Parse(textBox23.Text) + float.Parse(textBox24.Text)) <= reg1)
                    {
                        if (float.Parse(textBox23.Text)<= float.Parse(textBox21.Text) && float.Parse(textBox24.Text) <= float.Parse(textBox22.Text))
                        {
                            if ((MessageBox.Show("هل انت متأكد من تعديل الرصيد ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                            {
                                Program.mysignin.Show();
                                Program.mysignin.Signin_Load(sender, e);
                                string ww = "NUNS";
                                string page = "NUNS";
                                Program.mysignin.which(ww, page);
                            }
                        }
                        else
                        {
                            MessageBox.Show("لا يمكن سحب قيمة اكبر من الرصيد");

                        }
                    }
                    else
                    {
                        MessageBox.Show("الصندوق لا يكفي لإتمام العملية");
                    }
                                       
                }
                else
                {
                    MessageBox.Show("الرجاء ادخال القيم");
                }
            }
            else
            {
                MessageBox.Show("الرجاء عرض القيم اولا");
            }

        }

        public void NUNS(bool ok, string n, object sender, EventArgs e)
        {
            try
            {
                if (ok)
                {
                    if (n == "ايمن عويس")
                    {
                        u1 = true;
                    }
                    if (n == "سالم عويسات")
                    {
                        u2 = true;
                    }
                    if (u1 && u2)
                    {
                        float AO = float.Parse(textBox21.Text) - float.Parse(textBox23.Text);
                        float SO = float.Parse(textBox22.Text) - float.Parse(textBox24.Text);
                        SqlConnection conn5 = new SqlConnection(src);
                        SqlCommand cmdn5 = new SqlCommand("UPDATE [Balance] SET Amount=@box3 WHERE Id = '" + textBox25.Text + "'", conn5);
                        cmdn5.Parameters.AddWithValue("@box3", AO);
                        conn5.Open();
                        SqlDataReader d725 = cmdn5.ExecuteReader();
                        conn5.Close();

                        SqlConnection conn55 = new SqlConnection(src);
                        SqlCommand cmdn55 = new SqlCommand("UPDATE [Balance] SET Amount=@box3 WHERE Id = '" + textBox26.Text + "'", conn55);
                        cmdn55.Parameters.AddWithValue("@box3", SO);
                        conn55.Open();
                        SqlDataReader d7255 = cmdn55.ExecuteReader();
                        conn55.Close();

                        SqlConnection con505 = new SqlConnection(src);
                        SqlCommand cmd505 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con505);
                        cmd505.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                        string t7 = (reg1 - float.Parse(textBox23.Text) + float.Parse(textBox24.Text)).ToString();
                        cmd505.Parameters.AddWithValue("@textBox2", t7);
                        con505.Open();
                        SqlDataReader dr1505 = cmd505.ExecuteReader();

                        SqlConnection con = new SqlConnection(src);
                        SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                        cmd.Parameters.AddWithValue("@textBox1", "ايمن عويس/سالم عويسات");
                        cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@textBox3", "لقد تم سحب مبلغ من الرصيد" + Environment.NewLine + "  التاريخ  " + DateTime.Now.ToShortDateString() + "  ايمن عويس  " + textBox23.Text + "  سالم عويسات  " + textBox24.Text);
                        cmd.Parameters.AddWithValue("@textBox4", "UP");
                        con.Open();
                        SqlDataReader dr2 = cmd.ExecuteReader();

                        MessageBox.Show("لقد تم تعديل الرصيد بنجاح");
                        u1 = false;
                        u2 = false;
                        reg1 = 0;
                        textBox1.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";
                        textBox13.Text = "";
                        textBox14.Text = "";
                        textBox15.Text = "";
                        textBox16.Text = "";
                        textBox17.Text = "";
                        textBox18.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                        textBox19.Text = "";
                        textBox20.Text = "";
                        textBox5.Text = "50";
                        textBox21.Text = "";
                        textBox22.Text = "";
                        textBox23.Text = "";
                        textBox24.Text = "";
                        textBox25.Text = "";
                        textBox26.Text = "";
                        textBox27.Text = "";
                    }
                    else
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "NUNS";
                        string page = "NUNS";
                        Program.mysignin.which(ww, page);
                    }
                }
            }
            catch
            {

                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                textBox23.Text = ""; textBox24.Text = "";
            }
        }

        private void textBox15_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text != "")
            {
                try
                {
                    if (textBox15.Text != "")
                    {
                        textBox14.Text = (float.Parse(textBox9.Text) - float.Parse(textBox15.Text)).ToString();
                        if (float.Parse(textBox9.Text) > 0)
                        {
                            if (float.Parse(textBox14.Text) < 0)
                            {
                                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                                textBox15.Text = "0.0";
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                    textBox15.Text = "0.0";
                }
            }
        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                try
                {
                    if (textBox16.Text != "")
                    {
                        textBox17.Text = (float.Parse(textBox11.Text) - float.Parse(textBox16.Text)).ToString();
                        if (float.Parse(textBox11.Text) > 0)
                        {
                            if (float.Parse(textBox17.Text) < 0)
                            {
                                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                                textBox16.Text = "0.0";
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                    textBox16.Text = "0.0";
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox11.Text == "" || textBox9.Text == "" || textBox17.Text == "" || textBox16.Text == "" || textBox15.Text == "" || textBox14.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء العملية  ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "NSSA";
                string page = "NSSA";
                Program.mysignin.which(ww, page);
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox15.Text = "0.0";
            textBox16.Text = "0.0";
        }

        public void NSSA(bool ok, string n, object sender, EventArgs e)
        {
            try
            {
                if (ok)
                {
                    if (n == "ايمن عويس")
                    {
                        u1 = true;
                    }
                    if (n == "سالم عويسات")
                    {
                        u2 = true;
                    }
                    string a = "";
                    string s = "";
                    if (u1 && u2)
                    {
                        p = true;
                           //DialogResult result = printDialog1.ShowDialog();
                           //if (result == DialogResult.OK)
                           //{
                               printDocument1.Print();
                         //  }
                       // printPreviewDialog1.ShowDialog();
                        SqlConnection con66 = new SqlConnection(src);
                        con66.Open();
                        SqlCommand cmd66 = new SqlCommand("select * from Balance", con66);
                        SqlDataReader dr66 = cmd66.ExecuteReader();
                        while (dr66.Read())
                        {
                            if (dr66["Name"].ToString() == "ايمن عويس")

                            {
                                a = dr66["Id"].ToString();
                            }
                            if (dr66["Name"].ToString() == "سالم عويسات")

                            {
                                s = dr66["Id"].ToString();
                            }
                        }
                        float AO = float.Parse(textBox14.Text) + float.Parse(textBox3.Text);
                        float SO = float.Parse(textBox4.Text) + float.Parse(textBox17.Text);
                        float rr= float.Parse(textBox15.Text) + float.Parse(textBox16.Text);
                        SqlConnection conn5 = new SqlConnection(src);
                        SqlCommand cmdn5 = new SqlCommand("UPDATE [Balance] SET Amount=@box3 WHERE Id = '" + a + "'", conn5);
                        cmdn5.Parameters.AddWithValue("@box3", AO);
                        conn5.Open();
                        SqlDataReader d725 = cmdn5.ExecuteReader();
                        conn5.Close();

                        SqlConnection conn55 = new SqlConnection(src);
                        SqlCommand cmdn55 = new SqlCommand("UPDATE [Balance] SET Amount=@box3 WHERE Id = '" + s + "'", conn55);
                        cmdn55.Parameters.AddWithValue("@box3", SO);
                        conn55.Open();
                        SqlDataReader d7255 = cmdn55.ExecuteReader();
                        conn55.Close();

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

                        SqlConnection con505 = new SqlConnection(src);
                        SqlCommand cmd505 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con505);
                        cmd505.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                        string t7 = (reg1 - rr).ToString();
                        cmd505.Parameters.AddWithValue("@textBox2", t7);
                        con505.Open();
                        SqlDataReader dr1505 = cmd505.ExecuteReader();

                        SqlConnection con = new SqlConnection(src);
                        SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                        cmd.Parameters.AddWithValue("@textBox1",n);
                        cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                        cmd.Parameters.AddWithValue("@textBox3", "لقد تم اجراء محاسبة" + Environment.NewLine + "  التاريخ  " + DateTime.Now.ToShortDateString() + "  ايمن عويس القيمة المسحوبة  " + textBox15.Text + "  الى الرصيد  " + textBox14.Text + "  سالم عويسات القيمة المسحوية  " + textBox16.Text + "  الى الرصيد  " + textBox17.Text);
                        cmd.Parameters.AddWithValue("@textBox4", "UP");
                        con.Open();
                        SqlDataReader dr2 = cmd.ExecuteReader();

                        MessageBox.Show("لقد تم اجراء المحاسبة بنجاح");
                        u1 = false;
                        u2 = false;
                        reg1 = 0;
                        textBox1.Text = "";
                        textBox10.Text = "";
                        textBox11.Text = "";
                        textBox13.Text = "";
                        textBox14.Text = "";
                        textBox15.Text = "";
                        textBox16.Text = "";
                        textBox17.Text = "";
                        textBox18.Text = "";
                        textBox2.Text = "";
                        textBox3.Text = "";
                        textBox4.Text = "";
                        textBox6.Text = "";
                        textBox7.Text = "";
                        textBox8.Text = "";
                        textBox9.Text = "";
                        textBox19.Text = "";
                        textBox20.Text = "";
                        textBox5.Text = "";
                        textBox5.Text = "50";
                        textBox21.Text = "";
                        textBox22.Text = "";
                        textBox23.Text = "";
                        textBox24.Text = "";
                        textBox25.Text = "";
                        textBox26.Text = "";
                        textBox27.Text = "";
                    }
                    else
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "NSSA";
                        string page = "NSSA";
                        Program.mysignin.which(ww, page);
                    }
                }
            }
            catch
            {

                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                textBox15.Text = ""; textBox16.Text = "";
            }
        }

        int i = 0; int c = 0; int dr = 0; bool end = false; bool p = false;

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            float t = float.Parse(textBox27.Text) + float.Parse(textBox6.Text);
            if (p)
            {
                Image newImage = Image.FromFile("C:\\Users\\Amjad\\source\\repos\\BALOOTA\\BALOOTA\\Resources\\BP4.jpg");
                e.Graphics.DrawString("بــــــلّــــــوطــــــــــــة لـــــــلــــــــــدهــــــــانــــــــات" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
                e.Graphics.DrawString("عجلون - شارع إربد - مقابل المقبرة المسيحية - 0777519277 - 0776947787" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(380, 70));
                e.Graphics.DrawImage(newImage, 30, 2);
                e.Graphics.DrawString("و مواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));


                e.Graphics.DrawString("دهــــــانـــــات و مـــــواد عـــــــزل - أدوات كــــهــــربــــائــــيــــة وصـــحـــيـــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 35));
                e.Graphics.DrawString("لـوازم نـجـاريــن وحـداديــن - خـردوات وبـراغــي - عــدد يــدويــة وهــنــدســيــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 50));
                e.Graphics.DrawString("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ", new Font("Microsoft Sans Serif", 20, FontStyle.Bold), Brushes.Navy, new PointF(0, 75));
                e.Graphics.DrawString("مـحـاسـبـة" + Environment.NewLine, new Font("Microsoft Sans Serif", 18, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));

                e.Graphics.DrawString(": التاريخ       من", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(610, 180));
                e.Graphics.DrawString(textBox10.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(500, 180));
                e.Graphics.DrawString(": إلى", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(300, 180));
                e.Graphics.DrawString(textBox8.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(190, 180));
                e.Graphics.DrawString(": مجمل الارباح ", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(700, 275));
                e.Graphics.DrawString(": الربح الصافي", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(700, 325));
                e.Graphics.DrawString(": مصاريف المحل", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(250, 275));
                e.Graphics.DrawString(": مصاريف الموظفين", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(250, 325));


                e.Graphics.DrawString("الـــديـــن", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(400, 375));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 405));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 405));
                e.Graphics.DrawString(": الدين العام", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 435));
                e.Graphics.DrawString(": الدين ضمن الفترة", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 465));
                e.Graphics.DrawString("الرصيد قبل المحاسبة", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(390, 515));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 545));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 545));
                e.Graphics.DrawString("الـنـسـبـة", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(400, 625));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 655));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 655));
                e.Graphics.DrawString(": النسبة", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 685));
                e.Graphics.DrawString(": المسحوب نقداً", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 715));
                e.Graphics.DrawString(": المتبقي الى الرصيد", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 745));
                e.Graphics.DrawString("الرصيد بعد المحاسبة", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(390, 795));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 825));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 825));

                
                e.Graphics.DrawString(t.ToString(), new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 275));
                e.Graphics.DrawString(textBox7.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 325));
                e.Graphics.DrawString(textBox19.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(90, 275));
                e.Graphics.DrawString(textBox20.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(90, 325));

                e.Graphics.DrawString(textBox1.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 435));
                e.Graphics.DrawString(textBox13.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(170, 435));
                e.Graphics.DrawString(textBox2.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 465));
                e.Graphics.DrawString(textBox18.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(170, 465));

                e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 575));
                e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(170, 575));

                e.Graphics.DrawString(textBox9.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 685));
                e.Graphics.DrawString(textBox11.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 685));
                e.Graphics.DrawString(textBox15.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 715));
                e.Graphics.DrawString(textBox16.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 715));
                e.Graphics.DrawString(textBox14.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 745));
                e.Graphics.DrawString(textBox17.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 745));

                e.Graphics.DrawString((float.Parse(textBox14.Text) + float.Parse(textBox3.Text)).ToString(), new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 855));
                e.Graphics.DrawString((float.Parse(textBox17.Text) + float.Parse(textBox4.Text)).ToString(), new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 855));
                end = false; p = false;
            }
            else
            {
                e.Graphics.DrawString("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ", new Font("Microsoft Sans Serif", 20, FontStyle.Bold), Brushes.Navy, new PointF(0, 75));
                e.Graphics.DrawString("مـحـاسـبـة" + Environment.NewLine, new Font("Microsoft Sans Serif", 18, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));

                e.Graphics.DrawString(": التاريخ       من", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(610, 180));
                e.Graphics.DrawString(textBox10.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(500, 180));
                e.Graphics.DrawString(": إلى", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(300, 180));
                e.Graphics.DrawString(textBox8.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(190, 180));
                e.Graphics.DrawString(": مجمل الارباح ", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(700, 275));
                e.Graphics.DrawString(": الربح الصافي", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(700, 325));
                e.Graphics.DrawString(": مصاريف المحل", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(250, 275));
                e.Graphics.DrawString(": مصاريف الموظفين", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(250, 325));


                e.Graphics.DrawString("الـــديـــن", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(400, 375));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 405));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 405));
                e.Graphics.DrawString(": الدين العام", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 435));
                e.Graphics.DrawString(": الدين ضمن الفترة", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 465));
                e.Graphics.DrawString("الرصيد قبل المحاسبة", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(390, 515));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 545));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 545));
                e.Graphics.DrawString("الـنـسـبـة", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(400, 625));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 655));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 655));
                e.Graphics.DrawString(": النسبة", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 685));
                e.Graphics.DrawString(": المسحوب نقداً", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 715));
                e.Graphics.DrawString(": المتبقي الى الرصيد", new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(650, 745));
                e.Graphics.DrawString("الرصيد بعد المحاسبة", new Font("Microsoft Sans Serif", 16, FontStyle.Bold), Brushes.Black, new PointF(390, 795));
                e.Graphics.DrawString("ايمن عويس", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 825));
                e.Graphics.DrawString("سالم عويسات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(150, 825));


                e.Graphics.DrawString(t.ToString(), new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 275));
                e.Graphics.DrawString(textBox7.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(500, 325));
                e.Graphics.DrawString(textBox19.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(90, 275));
                e.Graphics.DrawString(textBox20.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(90, 325));

                e.Graphics.DrawString(textBox1.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 435));
                e.Graphics.DrawString(textBox13.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(170, 435));
                e.Graphics.DrawString(textBox2.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 465));
                e.Graphics.DrawString(textBox18.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(170, 465));

                e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 575));
                e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(170, 575));

                e.Graphics.DrawString(textBox9.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 685));
                e.Graphics.DrawString(textBox11.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 685));
                e.Graphics.DrawString(textBox15.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 715));
                e.Graphics.DrawString(textBox16.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 715));
                e.Graphics.DrawString(textBox14.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 745));
                e.Graphics.DrawString(textBox17.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 745));

                e.Graphics.DrawString((float.Parse(textBox14.Text)+float.Parse(textBox3.Text)).ToString(), new Font("Microsoft Sans Serif", 14, FontStyle.Regular), Brushes.Black, new PointF(515, 855));
                e.Graphics.DrawString((float.Parse(textBox17.Text) + float.Parse(textBox4.Text)).ToString(), new Font("Microsoft Sans Serif", 12, FontStyle.Regular), Brushes.Black, new PointF(170, 855));
                end = false; p = false;


            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(end)
            {
                p = false;
                //   DialogResult result = printDialog1.ShowDialog();
                //   if (result == DialogResult.OK)
                //   {
                //       printDocument1.Print();
                //   }
                printPreviewDialog1.ShowDialog();

            }
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
