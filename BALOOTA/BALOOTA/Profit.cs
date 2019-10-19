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
    public partial class Profit : Form
    {
        public Profit()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;

        public void Profit_Load(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;end = false;
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

        private void button5_Click(object sender, EventArgs e)
        {
            if(textBox10.Text=="" && textBox8.Text=="")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "Profit";
                string page = "Profit";
                Program.mysignin.which(ww, page);
            }
        }

        public void Prof()
        {
            end = true;
            float purchase = 0;
            float sale = 0;
            float profit = 0;
            float sout = 0;
            float eout = 0;
            float stdebt = 0;
            float sadebt = 0;
            float sdebt = 0;
            float edebt = 0;
            float purstart = 0;
            float salestart = 0;
            float profitstart = 0;
            float destroy = 0;
            float dis = 0;

            if (textBox8.Text != "" && textBox10.Text != "")
            {
                SqlConnection con9 = new SqlConnection(src);
                con9.Open();
                SqlCommand cmd9 = new SqlCommand("select * from Purchases", con9);
                SqlDataReader dr = cmd9.ExecuteReader();
                while (dr.Read())
                {
                    if (DateTime.Parse(dr["Date"].ToString()) < DateTime.Parse(textBox10.Text) && float.Parse(dr["Amount"].ToString()) > 0)
                        {

                        purstart = purstart + float.Parse(dr["Amount"].ToString());
                    }
                      if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr["Amount"].ToString()) > 0 )

                    {
                        purchase=purchase+float.Parse(dr["Amount"].ToString());
                        dis=dis+ float.Parse(dr["Dis"].ToString());
                    }
                }

                SqlConnection con1 = new SqlConnection(src);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Sales", con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (DateTime.Parse(dr1["Date"].ToString()) < DateTime.Parse(textBox10.Text) && float.Parse(dr1["Amount"].ToString()) > 0)
                    {
                        salestart = salestart + float.Parse(dr1["Amount"].ToString());
                        profitstart = profitstart + float.Parse(dr1["Profit"].ToString());
                    }
                    if (DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr1["Amount"].ToString()) > 0)

                    {
                       
                        sale = sale + float.Parse(dr1["Amount"].ToString());
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

                SqlConnection con3 = new SqlConnection(src);
                con3.Open();
                SqlCommand cmd3 = new SqlCommand("select * from EmpOut", con3);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if (DateTime.Parse(dr3["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr3["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr3["Amount"].ToString()) > 0)

                    {
                        eout = eout + float.Parse(dr3["Amount"].ToString());
                    }
                }

                SqlConnection con4 = new SqlConnection(src);
                con4.Open();
                SqlCommand cmd4 = new SqlCommand("select * from StoreDebt", con4);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                while (dr4.Read())
                {
                    if (DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr4["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr4["Amount"].ToString()) > 0)

                    {
                        stdebt = stdebt + float.Parse(dr4["Amount"].ToString());
                    }
                }


                SqlConnection con5 = new SqlConnection(src);
                con5.Open();
                SqlCommand cmd5 = new SqlCommand("select * from SaleDebt", con5);
                SqlDataReader dr5 = cmd5.ExecuteReader();
                while (dr5.Read())
                {
                    if (DateTime.Parse(dr5["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr5["Amount"].ToString()) > 0)

                    {
                        sadebt = sadebt + float.Parse(dr5["Amount"].ToString());
                    }
                }

                SqlConnection con6 = new SqlConnection(src);
                con6.Open();
                SqlCommand cmd6 = new SqlCommand("select * from SDebt", con6);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                while (dr6.Read())
                {
                    if (DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr6["Amount"].ToString()) > 0)

                    {
                        sdebt = sdebt + float.Parse(dr6["Amount"].ToString());
                    }
                }

                SqlConnection con7 = new SqlConnection(src);
                con7.Open();
                SqlCommand cmd7 = new SqlCommand("select * from EmpDebt", con7);
                SqlDataReader dr7 = cmd7.ExecuteReader();
                while (dr7.Read())
                {
                    if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr7["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr7["Amount"].ToString()) > 0)

                    {
                        edebt = edebt + float.Parse(dr7["Amount"].ToString());
                    }
                }

                SqlConnection con71 = new SqlConnection(src);
                con71.Open();
                SqlCommand cmd71 = new SqlCommand("select * from Destroy", con71);
                SqlDataReader dr71 = cmd71.ExecuteReader();
                while (dr71.Read())
                {
                    if (DateTime.Parse(dr71["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr71["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr71["Amount"].ToString()) > 0)

                    {
                        destroy = destroy + float.Parse(dr71["Amount"].ToString());
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
                    if (DateTime.Parse(dr["Date"].ToString()) < DateTime.Parse(textBox10.Text) && float.Parse(dr["Amount"].ToString()) > 0)
                    {

                        purstart = purstart + float.Parse(dr["Amount"].ToString());
                    }
                    if (DateTime.Parse(dr["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr["Amount"].ToString()) > 0)

                    {
                        purchase = purchase + float.Parse(dr["Amount"].ToString());
                        dis = dis + float.Parse(dr["Dis"].ToString());
                    }
                }

                SqlConnection con1 = new SqlConnection(src);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Sales", con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if (DateTime.Parse(dr1["Date"].ToString()) < DateTime.Parse(textBox10.Text) && float.Parse(dr1["Amount"].ToString()) > 0)
                    {
                        salestart = salestart + float.Parse(dr1["Amount"].ToString());
                        profitstart = profitstart + float.Parse(dr1["Profit"].ToString());
                    }
                    if (DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr1["Amount"].ToString()) > 0)

                    {
                        sale = sale + float.Parse(dr1["Amount"].ToString());
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

                SqlConnection con3 = new SqlConnection(src);
                con3.Open();
                SqlCommand cmd3 = new SqlCommand("select * from EmpOut", con3);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if (DateTime.Parse(dr3["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr3["Amount"].ToString()) > 0)

                    {
                        eout = eout + float.Parse(dr3["Amount"].ToString());
                    }
                }

                SqlConnection con4 = new SqlConnection(src);
                con4.Open();
                SqlCommand cmd4 = new SqlCommand("select * from StoreDebt", con4);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                while (dr4.Read())
                {
                    if (DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr4["Amount"].ToString()) > 0)

                    {
                        stdebt = stdebt + float.Parse(dr4["Amount"].ToString());
                    }
                }


                SqlConnection con5 = new SqlConnection(src);
                con5.Open();
                SqlCommand cmd5 = new SqlCommand("select * from SaleDebt", con5);
                SqlDataReader dr5 = cmd5.ExecuteReader();
                while (dr5.Read())
                {
                    if (DateTime.Parse(dr5["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr5["Amount"].ToString()) > 0)

                    {
                        sadebt = sadebt + float.Parse(dr5["Amount"].ToString());
                    }
                }

                SqlConnection con6 = new SqlConnection(src);
                con6.Open();
                SqlCommand cmd6 = new SqlCommand("select * from SDebt", con6);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                while (dr6.Read())
                {
                    if (DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr6["Amount"].ToString()) > 0)

                    {
                        sdebt = sdebt + float.Parse(dr6["Amount"].ToString());
                    }
                }

                SqlConnection con7 = new SqlConnection(src);
                con7.Open();
                SqlCommand cmd7 = new SqlCommand("select * from EmpDebt", con7);
                SqlDataReader dr7 = cmd7.ExecuteReader();
                while (dr7.Read())
                {
                    if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr7["Amount"].ToString()) > 0)

                    {
                        edebt = edebt + float.Parse(dr7["Amount"].ToString());
                    }
                }
                SqlConnection con71 = new SqlConnection(src);
                con71.Open();
                SqlCommand cmd71 = new SqlCommand("select * from Destroy", con71);
                SqlDataReader dr71 = cmd71.ExecuteReader();
                while (dr71.Read())
                {
                    if (DateTime.Parse(dr71["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && float.Parse(dr71["Amount"].ToString()) > 0)

                    {
                        destroy = destroy + float.Parse(dr71["Amount"].ToString());
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
                        purchase = purchase + float.Parse(dr["Amount"].ToString());
                        dis = dis + float.Parse(dr["Dis"].ToString());
                    }
                }

                SqlConnection con1 = new SqlConnection(src);
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("select * from Sales", con1);
                SqlDataReader dr1 = cmd1.ExecuteReader();
                while (dr1.Read())
                {
                    if ( DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr1["Amount"].ToString()) > 0)

                    {
                        sale = sale + float.Parse(dr1["Amount"].ToString());
                        profit = profit + float.Parse(dr1["Profit"].ToString());
                    }
                }

                SqlConnection con2 = new SqlConnection(src);
                con2.Open();
                SqlCommand cmd2 = new SqlCommand("select * from StoreOut", con2);
                SqlDataReader dr2 = cmd2.ExecuteReader();
                while (dr2.Read())
                {
                    if ( DateTime.Parse(dr2["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr2["Amount"].ToString()) > 0)

                    {
                        sout = sout + float.Parse(dr2["Amount"].ToString());
                    }
                }

                SqlConnection con3 = new SqlConnection(src);
                con3.Open();
                SqlCommand cmd3 = new SqlCommand("select * from EmpOut", con3);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if ( DateTime.Parse(dr3["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr3["Amount"].ToString()) > 0)

                    {
                        eout = eout + float.Parse(dr3["Amount"].ToString());
                    }
                }

                SqlConnection con4 = new SqlConnection(src);
                con4.Open();
                SqlCommand cmd4 = new SqlCommand("select * from StoreDebt", con4);
                SqlDataReader dr4 = cmd4.ExecuteReader();
                while (dr4.Read())
                {
                    if (DateTime.Parse(dr4["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr4["Amount"].ToString()) > 0)

                    {
                        stdebt = stdebt + float.Parse(dr4["Amount"].ToString());
                    }
                }


                SqlConnection con5 = new SqlConnection(src);
                con5.Open();
                SqlCommand cmd5 = new SqlCommand("select * from SaleDebt", con5);
                SqlDataReader dr5 = cmd5.ExecuteReader();
                while (dr5.Read())
                {
                    if (DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr5["Amount"].ToString()) > 0)

                    {
                        sadebt = sadebt + float.Parse(dr5["Amount"].ToString());
                    }
                }

                SqlConnection con6 = new SqlConnection(src);
                con6.Open();
                SqlCommand cmd6 = new SqlCommand("select * from SDebt", con6);
                SqlDataReader dr6 = cmd6.ExecuteReader();
                while (dr6.Read())
                {
                    if ( DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr6["Amount"].ToString()) > 0)

                    {
                        sdebt = sdebt + float.Parse(dr6["Amount"].ToString());
                    }
                }

                SqlConnection con7 = new SqlConnection(src);
                con7.Open();
                SqlCommand cmd7 = new SqlCommand("select * from EmpDebt", con7);
                SqlDataReader dr7 = cmd7.ExecuteReader();
                while (dr7.Read())
                {
                    if ( DateTime.Parse(dr7["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr7["Amount"].ToString()) > 0)

                    {
                        edebt = edebt + float.Parse(dr7["Amount"].ToString());
                    }
                }

                SqlConnection con71 = new SqlConnection(src);
                con71.Open();
                SqlCommand cmd71 = new SqlCommand("select * from Destroy", con71);
                SqlDataReader dr71 = cmd71.ExecuteReader();
                while (dr71.Read())
                {
                    if (DateTime.Parse(dr71["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && float.Parse(dr71["Amount"].ToString()) > 0)

                    {
                        destroy = destroy + float.Parse(dr71["Amount"].ToString());
                    }
                }


            }

            textBox3.Text = purchase.ToString();
            textBox1.Text = sale.ToString();
            textBox6.Text = profit.ToString();
            textBox2.Text = sout.ToString();
            textBox14.Text = eout.ToString();
            textBox13.Text = stdebt.ToString();
            textBox12.Text = sadebt.ToString();
            textBox11.Text = edebt.ToString();
            textBox9.Text = sdebt.ToString();
            textBox4.Text = (purstart - (salestart - profitstart)).ToString();
            textBox5.Text = (purstart+purchase - ((salestart+sale) - (profitstart+profit))).ToString();
            textBox15.Text = destroy.ToString();
            textBox16.Text = dis.ToString();
            textBox7.Text = ((float.Parse(textBox6.Text) + float.Parse(textBox16.Text)) - (float.Parse(textBox14.Text) + float.Parse(textBox2.Text))).ToString();
            string d = "01/01/2019";
            for(int c=0; c<=51; c++)
            {
                SqlConnection con90 = new SqlConnection(src);
                con90.Open();
                SqlCommand cmd90 = new SqlCommand("select * from Purchases", con90);
                SqlDataReader dr0 = cmd90.ExecuteReader();
                float tot = 0;
                while (dr0.Read())
                {
                    if (DateTime.Parse(dr0["Date"].ToString()) < DateTime.Parse(d).AddMonths(c+1) && DateTime.Parse(dr0["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c) && float.Parse(dr0["Amount"].ToString()) > 0)
                    {
                        tot = tot + float.Parse(dr0["Amount"].ToString());
                    }
                }
                this.chart2.Series["المشتريات"].Points.AddXY(DateTime.Parse(d).AddMonths(c).Month + "/" + DateTime.Parse(d).AddMonths(c).Year, tot);

                SqlConnection con901 = new SqlConnection(src);
                con901.Open();
                SqlCommand cmd901 = new SqlCommand("select * from Sales", con901);
                SqlDataReader dr01 = cmd901.ExecuteReader();
                float tot1 = 0;
                float tot2 = 0;
                while (dr01.Read())
                {
                    if (DateTime.Parse(dr01["Date"].ToString()) < DateTime.Parse(d).AddMonths(c + 1) && DateTime.Parse(dr01["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c))
                    {
                        tot1 = tot1 + float.Parse(dr01["Amount"].ToString());
                        tot2 = tot2 + float.Parse(dr01["Profit"].ToString());
                    }
                }
                this.chart2.Series["المبيعات"].Points.AddXY(DateTime.Parse(d).AddMonths(c).Month + "/" + DateTime.Parse(d).AddMonths(c).Year, tot1);
                this.chart2.Series["الأرباح"].Points.AddXY(DateTime.Parse(d).AddMonths(c).Month + "/" + DateTime.Parse(d).AddMonths(c).Year, tot2);


                SqlConnection con20 = new SqlConnection(src);
                con20.Open();
                SqlCommand cmd20 = new SqlCommand("select * from StoreOut", con20);
                SqlDataReader dr20 = cmd20.ExecuteReader();
                float tot3 = 0;
                while (dr20.Read())
                {
                    if (DateTime.Parse(dr20["Date"].ToString()) < DateTime.Parse(d).AddMonths(c + 1) && DateTime.Parse(dr20["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c))
                    {
                        tot3 = tot3 + float.Parse(dr20["Amount"].ToString());
                    }
                }

                SqlConnection con30 = new SqlConnection(src);
                con30.Open();
                SqlCommand cmd30 = new SqlCommand("select * from EmpOut", con30);
                SqlDataReader dr30 = cmd30.ExecuteReader();
                while (dr30.Read())
                {
                    if (DateTime.Parse(dr30["Date"].ToString()) < DateTime.Parse(d).AddMonths(c + 1) && DateTime.Parse(dr30["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c))
                    {
                        tot3 = tot3 + float.Parse(dr30["Amount"].ToString());
                    }
                }
                this.chart1.Series["المصاريف"].Points.AddXY(DateTime.Parse(d).AddMonths(c).Month + "/" + DateTime.Parse(d).AddMonths(c).Year, tot3);

                SqlConnection con40 = new SqlConnection(src);
                con40.Open();
                SqlCommand cmd40 = new SqlCommand("select * from StoreDebt", con40);
                SqlDataReader dr40 = cmd40.ExecuteReader();
                float tot4 = 0;
                while (dr40.Read())
                {
                    if (DateTime.Parse(dr40["Date"].ToString()) < DateTime.Parse(d).AddMonths(c + 1) && DateTime.Parse(dr40["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c))
                    {
                        tot4 = tot4 + float.Parse(dr40["Amount"].ToString());
                    }
                }


                SqlConnection con50 = new SqlConnection(src);
                con50.Open();
                SqlCommand cmd50 = new SqlCommand("select * from SaleDebt", con50);
                SqlDataReader dr50 = cmd50.ExecuteReader();
                float tot5 = 0;
                while (dr50.Read())
                {
                    if (DateTime.Parse(dr50["Date"].ToString()) < DateTime.Parse(d).AddMonths(c + 1) && DateTime.Parse(dr50["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c))
                    {
                        tot5 = tot5 + float.Parse(dr50["Amount"].ToString());
                    }
                }
                this.chart1.Series["ديون الزبائن"].Points.AddXY(DateTime.Parse(d).AddMonths(c).Month + "/" + DateTime.Parse(d).AddMonths(c).Year, tot5);

                SqlConnection con60 = new SqlConnection(src);
                con60.Open();
                SqlCommand cmd60 = new SqlCommand("select * from SDebt", con60);
                SqlDataReader dr60 = cmd60.ExecuteReader();
                while (dr60.Read())
                {
                    if (DateTime.Parse(dr60["Date"].ToString()) < DateTime.Parse(d).AddMonths(c + 1) && DateTime.Parse(dr60["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c))
                    {
                        tot4 = tot4 + float.Parse(dr60["Amount"].ToString());
                    }
                }
                this.chart1.Series["ديون المحل و الشركات"].Points.AddXY(DateTime.Parse(d).AddMonths(c).Month + "/" + DateTime.Parse(d).AddMonths(c).Year, tot4);

                SqlConnection con70 = new SqlConnection(src);
                con70.Open();
                SqlCommand cmd70 = new SqlCommand("select * from EmpDebt", con70);
                SqlDataReader dr70 = cmd70.ExecuteReader();
                float tot6 = 0;
                while (dr70.Read())
                {
                    if (DateTime.Parse(dr70["Date"].ToString()) < DateTime.Parse(d).AddMonths(c + 1) && DateTime.Parse(dr70["Date"].ToString()) >= DateTime.Parse(d).AddMonths(c))
                    {
                        tot6 = tot6 + float.Parse(dr70["Amount"].ToString());
                    }
                }
                this.chart1.Series["ديون الموظفين"].Points.AddXY(DateTime.Parse(d).AddMonths(c).Month + "/" + DateTime.Parse(d).AddMonths(c).Year, tot6);

                if (DateTime.Parse(d).AddMonths(c) > DateTime.Parse(DateTime.Now.ToShortDateString()))
                { break; }
            }

        }

        int i = 0; int c = 0; int dr = 0; bool end = false;
        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {
            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Samer
          // /*
            Image newImage4 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\S1.jpg");
            e.Graphics.DrawString("مـــــؤســــــــــــســـــــــة عـــــــــــــــويـــــــــــــــس" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
            e.Graphics.DrawString("مــــرج الـــحـــمـــام - شـــارع ام عـــبـــهـــرة - 0778982259" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(460, 70));
            e.Graphics.DrawImage(newImage4, 30, 2);
            e.Graphics.DrawString("لمواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
            //  */
            // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Ayman
             /*
          Image newImage = Image.FromFile("C:\\Users\\Amjad\\source\\repos\\BALOOTA\\BALOOTA\\Resources\\BP4.jpg");
          e.Graphics.DrawString("بــــــلّــــــوطــــــــــــة لـــــــلــــــــــدهــــــــانــــــــات" + Environment.NewLine, new Font("Microsoft Himalaya", 18, FontStyle.Bold), Brushes.Navy, new PointF(340, 15));
          e.Graphics.DrawString("عجلون - شارع إربد - مقابل المقبرة المسيحية - 0777519277 - 0776947787" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(380, 70));
          e.Graphics.DrawImage(newImage, 30, 2);
          e.Graphics.DrawString("و مواد البناء" + Environment.NewLine, new Font("Microsoft Uighur", 24, FontStyle.Bold), Brushes.Navy, new PointF(280, 30));
          //  */

            e.Graphics.DrawString("دهــــــانـــــات و مـــــواد عـــــــزل - أدوات كــــهــــربــــائــــيــــة وصـــحـــيـــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 35));
            e.Graphics.DrawString("لـوازم نـجـاريــن وحـداديــن - خـردوات وبـراغــي - عــدد يــدويــة وهــنــدســيــة" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(425, 50));
            e.Graphics.DrawString("ــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــــ", new Font("Microsoft Sans Serif", 20, FontStyle.Bold), Brushes.Navy, new PointF(0, 75));
            e.Graphics.DrawString("الأربـــاح" + Environment.NewLine, new Font("Microsoft Sans Serif", 18, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));

            e.Graphics.DrawString(": التاريخ       من", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(610, 180));
            e.Graphics.DrawString(textBox10.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(500, 180));
            e.Graphics.DrawString(": إلى", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(300, 180));
            e.Graphics.DrawString(textBox8.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(190, 180));
            e.Graphics.DrawString(": مجمل الارباح ", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 225));
            e.Graphics.DrawString(": الربح الصافي", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 275));
            e.Graphics.DrawString(": بضاعة بداية المدة", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 325));
            e.Graphics.DrawString(": بضاعة نهاية المدة", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 375));
            e.Graphics.DrawString(": المشتريات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(600, 425));
            e.Graphics.DrawString(": المبيعيات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(600, 475));
            e.Graphics.DrawString(": البضاعة الفاسدة", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 525));
            e.Graphics.DrawString(": مصاريف المحل", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(595, 575));
            e.Graphics.DrawString(": مصاريف الموظفين", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 625));
            e.Graphics.DrawString(": ديون الشركات", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 675));
            e.Graphics.DrawString(": ديون المحل", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 725));
            e.Graphics.DrawString(": ديون الموظفين", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(590, 775));
            e.Graphics.DrawString(": ديون الزبائن", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(590, 825));
            float t = float.Parse(textBox16.Text) + float.Parse(textBox6.Text);
            e.Graphics.DrawString(t.ToString(), new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 225));
            e.Graphics.DrawString(textBox7.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 275));
            e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 325));
            e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 375));
            e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(300, 425));
            e.Graphics.DrawString(textBox1.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(300, 475));
            e.Graphics.DrawString(textBox15.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 525));
            e.Graphics.DrawString(textBox2.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(295, 575));
            e.Graphics.DrawString(textBox14.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 625));
            e.Graphics.DrawString(textBox13.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 675));
            e.Graphics.DrawString(textBox9.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 725));
            e.Graphics.DrawString(textBox11.Text, new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Black, new PointF(290, 775));
            e.Graphics.DrawString(textBox12.Text, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Black, new PointF(290, 825));
            end = false;

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if(end)
            {
                           DialogResult result = printDialog1.ShowDialog();
                           if (result == DialogResult.OK)
                           {
                               printDocument1.Print();
                           }
                        // printPreviewDialog1.ShowDialog();
                
            }

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
