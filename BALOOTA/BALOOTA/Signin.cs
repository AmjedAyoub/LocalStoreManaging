using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Data.Sql;
using System.Data.SqlClient;
// chash memory  bedal e3ml run between DB and RAM
using System.Data.Odbc;
using System.Configuration;


namespace BALOOTA
{
    public partial class Signin : Form
    {
        public Signin()
        {
            InitializeComponent();
        }

        public string usern = "";
        public bool ok = false;
        public string proc = "";
        public string pag = "";
        string ii = "";
        string rd = "";
        string rp = "";

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // if press enter sha3'el  el log in
            {
                button1_Click((object)sender, (EventArgs)e);
            }
        }

        public void which(string who,string p)
        {
            proc = who;
            pag = p;
        }

        public void Ep(string w, string pp, string rdept,string rpaid)
        {
            ii = w;
            pag = pp;
            rd = rdept;
            rp = rpaid;
        }

        public void button1_Click(object sender, EventArgs e)
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
                        blnfound = true; // the username and pass corectusern = "";
                        usern = dr["EmployeeName"].ToString();
                    }
                }
            }
            if (blnfound == false) // the username and pass not corect
            {
                MessageBox.Show("خطأ في اسم المستخدم او كلمة السر \n الرجاء اعادة المحاولة ", "ادخال خاطئ", MessageBoxButtons.OK, MessageBoxIcon.Error);
                textBox1.Text = "";
                textBox2.Text = "";
                textBox1.Focus();
                ok = false;
            }
            else // the username and pass corect
            {
                dr.Close();
                con.Close();
                textBox1.Text = "";
                textBox2.Text = "";
                ok = true;
                this.Hide();
                if (pag == "Editemp")
                {
                    Program.myeditemp.found(ok, usern, proc);
                }
                else if(pag=="Addemp")
                {
                    Program.myaddemp.found(ok, usern, proc);
                }
                else if(pag== "INVIN")
                {
                    Program.myinvoucein.found(ok, usern, proc);
                }
                else if (pag == "EP")
                {
                    Program.mypurchase.Setname(ok, usern, ii,rd,rp);
                }
                else if(pag=="DP")
                {
                    Program.mypurchase.edit(ok, usern);
                }
                else if (pag == "DSale")
                {
                    Program.mysales.Delete(ok, usern);
                }
                else if (pag == "ESale")
                {
                    Program.mysales.Edit(ok, usern);
                }
                else if (pag == "Reg")
                {
                    Program.myinventory.Reg(ok, usern);
                }
                else if (pag == "F")
                {
                    Program.myinventory.F(ok, usern);
                }
                else if (pag == "Al")
                {
                    Program.myalert.Al(ok, usern);
                }
                else if (pag == "AS")
                {
                    Program.myalert.AS(ok);
                }
                else if (pag == "AE")
                {
                    Program.myalert.AE(ok, usern);
                }
                else if (pag == "AlD") 
                {
                    Program.myalert.AlD(ok, usern);
                } 
                else if (pag == "EmpAS")
                {
                    Program.myaddemp.EmpAS(ok);
                }
                else if (pag == "EmpES")
                {
                    Program.myeditemp.EmpES(ok);
                }
                else if (pag == "Pass1")
                {
                    Program.mypassword.Pass1(ok, usern);
                }
                else if (pag == "Pass2")
                {
                    Program.mypassword.Pass2(ok, usern);
                }
                else if (pag == "Ref1")
                {
                    Program.myref.Ref1(ok);
                }
                else if (pag == "Ref2")
                {
                    Program.myref.Ref2(ok, usern, sender, e);
                }
                else if (pag == "O")
                {
                    Program.myout.reg();
                }
                else if (pag == "OS")
                {
                    Program.myout.sho();
                }
                else if (pag == "OEmp")
                {
                    Program.myout.OEmp(ok,usern); 
                }
                else if (pag == "OStore")
                {
                    Program.myout.OStore(ok, usern);
                }
                else if (pag == "EO")
                {
                    Program.myeditout.EO();
                }
                else if (pag == "EOD")
                {
                    Program.myeditout.EOD(ok, usern);
                }
                else if (pag == "EOE")
                {
                    Program.myeditout.EOE(ok, usern);
                }
                else if (pag == "SO")
                {
                    Program.myeditstore.SO();
                }
                else if (pag == "SOD")
                {
                    Program.myeditstore.SOD(ok, usern);
                }
                else if (pag == "SOE")
                {
                    Program.myeditstore.SOE(ok, usern);
                }
                else if (pag == "EOV")
                {
                    Program.myviewout.EOV();
                }
                else if (pag == "SOV")
                {
                    Program.myviewout.SOV();
                }
                else if (pag == "Codebt")
                {
                    Program.mycoDebt.Codebt();
                }
                else if (pag == "CoDD")
                {
                    Program.mycoDebt.CoDD(ok, usern);
                }
                if(pag == "CoDP")
                {
                    Program.mycoDebt.CoDP(ok, usern);
                }
                else if (pag == "Cusdebt")
                {
                    Program.mycusDebt.Cusdebt();
                }
                else if (pag == "CusDD")
                {
                    Program.mycusDebt.CusDD(ok, usern);
                }
                if (pag == "CusDP")
                {
                    Program.mycusDebt.CusDP(ok, usern);
                }
                else if (pag == "Empdebt")
                {
                    Program.myempDebt.Empdebt();
                }
                else if (pag == "EmpDD")
                {
                    Program.myempDebt.EmpDD(ok, usern);
                }
                else if (pag == "EmpD")
                {
                    Program.myempDebt.EmpD(ok, usern);
                }
                else if (pag == "EmpDU")
                {
                    Program.myempDebt.EmpDU(ok, usern);
                }
                else if (pag == "Sdebt")
                {
                    Program.mystoreDebt.Sdebt();
                }
                else if (pag == "SDD")
                {
                    Program.mystoreDebt.SDD(ok, usern);
                }
                else if (pag == "SD")
                {
                    Program.mystoreDebt.SD(ok, usern);
                }
                else if (pag == "SDU")
                {
                    Program.mystoreDebt.SDU(ok, usern);
                }
                else if (pag == "Profit")
                {
                    Program.myprofit.Prof();
                }
                else if (pag == "SalaryS")
                {
                    Program.mySalary.SalaryS();
                }
                else if (pag == "SalaryP")
                {
                    Program.mySalary.SalaryP(ok, usern);
                }
                else if (pag == "StorePS")
                {
                    Program.mystoreP.StorePS();
                }
                else if (pag == "StorePP")
                {
                    Program.mystoreP.StorePP(ok, usern);
                }
                else if (pag == "EmpPS")
                {
                    Program.myempP.EmpPS();
                }
                else if (pag == "EmpPP")
                {
                    Program.myempP.EmpPP(ok, usern);
                }
                else if (pag == "NS")
                {
                    Program.myN.NS();
                }
                else if (pag == "NU")
                {
                    Program.myN.NU();
                }
                else if (pag == "NUN")
                {
                    Program.myN.NUN(ok, usern, sender, e);
                }
                else if (pag == "NUNS")
                {
                    Program.myN.NUNS(ok, usern, sender, e);
                }
                else if (pag == "NSSA")
                {
                    Program.myN.NSSA(ok, usern, sender, e);
                }
                else if (pag == "SHP")
                {
                    Program.mypurchase.shp();
                }
                else if (pag == "SH1")
                {
                    Program.myviewpur.sh1();
                }
                else if (pag == "SH2")
                {
                    Program.myviewpur.sh2();
                }
                else if (pag == "SHS")
                {
                    Program.mysales.shs();
                }
                else if (pag == "SHS1")
                {
                    Program.myviewsale.SHS1();
                }
                else if (pag == "SHS2")
                {
                    Program.myviewsale.SHS2();
                }
                else if (pag == "SI")
                {
                    Program.myinventory.SI();
                }
                else if (pag == "SIR")
                {
                    Program.myinventory.SIR();
                }
                else if (pag == "RefS")
                {
                    Program.myref.RefS();
                }
                else if (pag == "FD")
                {
                    Program.myF.Delete();
                }
                else if (pag == "FS")
                {
                    Program.myF.Sav();
                }
                else if (pag == "FI")
                {
                    Program.myF.In();
                }
            }
        }        

        public void Signin_Load(object sender, EventArgs e)
        {
            usern = "";
            ok = false;
            textBox1.Focus();
            ii = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            textBox2.Text = "";
            textBox1.Focus();
            usern = "";
            ok = false;
            this.Hide();
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) // if press enter sha3'el  el log in
            {
                button1_Click((object)sender, (EventArgs)e);
            }

        }
    }
}
