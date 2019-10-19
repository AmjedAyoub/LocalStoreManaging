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
    public partial class RP : Form
    {
        public RP()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        string rid = "";

        private void RP_Load(object sender, EventArgs e)
        {

        }

        public void Rp (string id)
        {
            rid = id;
            int row1 = 0;
            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Purchases WHERE Id ='" + id + "'", conn3);
            SqlDataReader dr8 = cmd3.ExecuteReader();

            float rr = 0;
            while (dr8.Read())
            {
                textBox7.Text = dr8["CompanyName"].ToString();
                textBox1.Text = dr8["InvoiceNo"].ToString();
                textBox2.Text = dr8["Date"].ToString();
                textBox3.Text = dr8["Amount"].ToString();
                textBox11.Text = dr8["Notes"].ToString();
                rr = float.Parse(dr8["RDebt"].ToString());
            }
            dr8.Close();
            float r = 0;
            SqlConnection conn33 = new SqlConnection(src);
            conn33.Open();
            SqlCommand cmd33 = new SqlCommand("select * from Items WHERE IdPurchase ='" + id + "'", conn33);
            SqlDataReader dr83 = cmd33.ExecuteReader();
            dataGridView2.Rows.Clear();
            row1 = 0;

            while (dr83.Read())
            {
                dataGridView2.Rows.Insert(row1, dr83["ItemName"].ToString(), dr83["Price"].ToString(), dr83["Quantity"].ToString(), dr83["RQuantity"].ToString(), dr83["MinQuantity"].ToString(), dr83["FullPrice"].ToString(), dr83["Notes"].ToString());
                r = r + (float.Parse(dr83["Price"].ToString()) * float.Parse(dr83["RQuantity"].ToString()));
                row1++;
            }
            dr83.Close();

            textBox6.Text = r.ToString();
            textBox5.Text = (r-rr).ToString();
            textBox4.Text = rr.ToString();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox11.Text = "";
            textBox5.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
            dataGridView2.Rows.Clear();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox7.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox11.Text = "";
            textBox6.Text = "";
            dataGridView2.Rows.Clear();
            this.Hide();
            Program.mysignin.Show();
            Program.mysignin.Signin_Load(sender, e);
            Program.mysignin.Ep(rid,"EP",textBox4.Text,textBox5.Text);
            textBox5.Text = "";
            textBox4.Text = "";
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

    }
}
