using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Data.Sql;
using System.Data.SqlClient;
// chash memory  bedal e3ml run between DB and RAM
using System.Data.Odbc;
using System.Configuration;

namespace BALOOTA
{
    static class Program
    {

        public static SqlConnection xsql; //connection
        public static string xsrc; // path of connection
        public static Form1 myform1;
        public static Form2 myform2;
        public static Addemp myaddemp;
        public static Alert myalert;
        public static Editemp myeditemp;
        public static Editout myeditout;
        public static Editstore myeditstore;
        public static Inventory myinventory;
        public static Invoucein myinvoucein;
        public static Main mymain;
        public static Out myout;
        public static Password mypassword;
        public static Profit myprofit;
        public static Purches mypurchase;
        public static Ref myref;
        public static Sales mysales;
        public static Signin mysignin;
        public static Viewout myviewout;
        public static Viewpur myviewpur;
        public static Viewsale myviewsale;
        public static RP myrp;
        public static Notes mynote;
        public static Forget myforget;
        public static CoDebt mycoDebt;
        public static CusDebt mycusDebt;
        public static EmpDebt myempDebt;
        public static StoreDebt mystoreDebt;
        public static N myN;
        public static Salary mySalary;
        public static StoreP mystoreP;
        public static EmpP myempP;
        public static F myF;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            xsql = new System.Data.SqlClient.SqlConnection(); //Data Source = (localdb)\v11.0; AttachDbFilename = C:\Users\Amjad\source\repos\BALOOTA\BALOOTA\Database1.mdf; Integrated Security = True
            xsql.ConnectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";//Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Amjad\source\repos\BALOOTA\BALOOTA\Database1.mdf;Integrated Security=True;User Instance=True//@" Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\oweis\Desktop\RRS\RRS\Database1.mdf;Integrated Security=True;User Instance=True";
            xsrc = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database1.mdf;Integrated Security=True";//Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Amjad\source\repos\BALOOTA\BALOOTA\Database1.mdf;Integrated Security=True;User Instance=True//@" Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\oweis\Desktop\RRS\RRS\Database1.mdf;Integrated Security=True;User Instance=True";
            myform1 = new Form1();
            myform2 = new Form2();
            myaddemp = new Addemp();
            myalert = new Alert();
            myviewout = new Viewout();
            myviewpur = new Viewpur();
            myviewsale = new Viewsale();
            mysales = new Sales();
            mysignin = new Signin();
            myref = new Ref();
            mypassword = new Password();
            myprofit = new Profit();
            mypurchase = new Purches();
            myeditemp = new Editemp();
            myeditout = new Editout();
            myeditstore = new Editstore();
            myinventory = new Inventory();
            myinvoucein = new Invoucein();
            mymain = new Main();
            myout = new Out();
            myrp = new RP();
            mynote = new Notes();
            myforget = new Forget();
            mycoDebt = new CoDebt();
            mycusDebt = new CusDebt();
            myempDebt = new EmpDebt();
            mystoreDebt = new StoreDebt();
            myN = new N();
            mySalary = new Salary();
            mystoreP = new StoreP();
            myempP = new EmpP();
            myF = new F();

            Application.Run(new Form1());
           
        }
    }
}
