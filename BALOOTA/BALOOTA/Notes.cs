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
    public partial class Notes : Form
    {
        public Notes()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;

        private void Notes_Load(object sender, EventArgs e)
        {

        }

        public void S(string[] arr)
        {
            dataGridView1.Rows.Clear();
            for (int w = 0; w < arr.Length - 1; w++)
            {
                if (arr[w] != null)
                {
                    SqlConnection conn3 = new SqlConnection(src);
                    conn3.Open();
                    SqlCommand cmd3 = new SqlCommand("select * from Inventory", conn3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    while (dr3.Read())
                    {

                        if (arr[w] == dr3["Item"].ToString())
                        {
                            dataGridView1.Rows.Insert(w, dr3["Item"].ToString(), dr3["Notes"].ToString());
                        }
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            dataGridView1.Rows.Clear();
        }
    }
}
