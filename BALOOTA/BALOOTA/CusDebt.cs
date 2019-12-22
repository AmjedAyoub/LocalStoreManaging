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
    public partial class CusDebt : Form
    {
        public CusDebt()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        int row1 = 0;
        public int rowindex1;
        public string name = "";
        public bool empty = true;
        private bool select = false;
        public float reg1 = 0;
        string[] items1;


        public void CusDebt_Load(object sender, EventArgs e)
        {

            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox9.Text = "";
            textBox7.Text = "";
            comboBox3.SelectedText = "";
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            if (comboBox3.Items.Count > 1)
            { comboBox3.Items.Clear(); }
            comboBox3.Items.Add("");
            comboBox3.Items.Add("الكل");
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from SaleDebt", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            while (dr39.Read())
            {
                iitem = dr39["Name"].ToString();

                if (!comboBox3.Items.Contains(iitem))
                {
                    comboBox3.Items.Add(iitem);
                }
            }
            dr39.Close();
            items1 = new string[comboBox3.Items.Count];
            comboBox3.Items.CopyTo(items1, 0);
            dataGridView1.Rows.Clear();

            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null);
            }
            comboBox3.Focus();
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
            if (comboBox3.Text == "" && textBox8.Text == "" && textBox10.Text == "")
            {
                MessageBox.Show("الرجاء ادخال المعلومات لاجراء عملية البحث ! \n شكرا");
            }
            else
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                string ww = "Cusdebt";
                string page = "Cusdebt";
                Program.mysignin.which(ww, page);
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                try
                {
                    if (textBox9.Text != "")
                    {
                        textBox1.Text = (float.Parse(textBox3.Text) - float.Parse(textBox9.Text)).ToString();
                        if (float.Parse(textBox1.Text) < 0)
                        {
                            MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                            textBox9.Text = "0.0";
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                    textBox9.Text = "0.0";
                }
            }
        }

        public void Cusdebt()
        {
            row1 = 0;
            try
            {
                if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text == "" && textBox10.Text == "")
                {
                    SqlConnection conn = new SqlConnection(src);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("select * from SaleDebt", conn);
                    SqlDataReader dr = cmd.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr.Read())
                    {
                        if (dr["Name"].ToString() == comboBox3.Text)

                        {
                            dataGridView1.Rows.Insert(row1, false, dr["Id"].ToString(), dr["Date"].ToString(), dr["Name"].ToString(), dr["idSales"].ToString(), dr["Amount"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con4 = new SqlConnection(src);
                    con4.Open();
                    SqlCommand cmd4 = new SqlCommand("select * from SaleDebt", con4);
                    SqlDataReader dr4 = cmd4.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr4.Read())
                    {
                        if (dr4["Name"].ToString() == comboBox3.Text && DateTime.Parse(dr4["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr4["Id"].ToString(), dr4["Date"].ToString(), dr4["Name"].ToString(), dr4["idSales"].ToString(), dr4["Amount"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con5 = new SqlConnection(src);
                    con5.Open();
                    SqlCommand cmd5 = new SqlCommand("select * from SaleDebt", con5);
                    SqlDataReader dr5 = cmd5.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr5.Read())
                    {
                        if (dr5["Name"].ToString() == comboBox3.Text && DateTime.Parse(dr5["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr5["Id"].ToString(), dr5["Date"].ToString(), dr5["Name"].ToString(), dr5["idSales"].ToString(), dr5["Amount"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.Text != "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con6 = new SqlConnection(src);
                    con6.Open();
                    SqlCommand cmd6 = new SqlCommand("select * from SaleDebt", con6);
                    SqlDataReader dr6 = cmd6.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr6.Read())
                    {
                        if (dr6["Name"].ToString() == comboBox3.Text && DateTime.Parse(dr6["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr6["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr6["Id"].ToString(), dr6["Date"].ToString(), dr6["Name"].ToString(), dr6["idSales"].ToString(), dr6["Amount"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con7 = new SqlConnection(src);
                    con7.Open();
                    SqlCommand cmd7 = new SqlCommand("select * from SaleDebt", con7);
                    SqlDataReader dr7 = cmd7.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr7.Read())
                    {
                        if (DateTime.Parse(dr7["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr7["Id"].ToString(), dr7["Date"].ToString(), dr7["Name"].ToString(), dr7["idSales"].ToString(), dr7["Amount"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con8 = new SqlConnection(src);
                    con8.Open();
                    SqlCommand cmd8 = new SqlCommand("select * from SaleDebt", con8);
                    SqlDataReader dr8 = cmd8.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr8.Read())
                    {
                        if (DateTime.Parse(dr8["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr8["Id"].ToString(), dr8["Date"].ToString(), dr8["Name"].ToString(), dr8["idSales"].ToString(), dr8["Amount"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con9 = new SqlConnection(src);
                    con9.Open();
                    SqlCommand cmd9 = new SqlCommand("select * from SaleDebt", con9);
                    SqlDataReader dr9 = cmd9.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr9.Read())
                    {
                        if (DateTime.Parse(dr9["Date"].ToString()) >= DateTime.Parse(textBox10.Text) && DateTime.Parse(dr9["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr9["Id"].ToString(), dr9["Date"].ToString(), dr9["Name"].ToString(), dr9["idSales"].ToString(), dr9["Amount"].ToString());
                            row1++;
                        }
                    }

                }
                else if (comboBox3.SelectedItem.ToString() == "الكل" && textBox8.Text == "" && textBox10.Text == "")
                {
                    SqlConnection con10 = new SqlConnection(src);
                    con10.Open();
                    SqlCommand cmd10 = new SqlCommand("select * from SaleDebt", con10);
                    SqlDataReader dr10 = cmd10.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr10.Read())
                    {
                        dataGridView1.Rows.Insert(row1, false, dr10["Id"].ToString(), dr10["Date"].ToString(), dr10["Name"].ToString(), dr10["idSales"].ToString(), dr10["Amount"].ToString());
                        row1++;

                    }

                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text == "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SaleDebt", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["idSales"].ToString(), dr1["Amount"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && textBox8.Text == "" && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SaleDebt", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["idSales"].ToString(), dr1["Amount"].ToString());
                            row1++;
                        }
                    }
                }
                else if (comboBox3.Text == "" && comboBox3.Text != "الكل" && textBox8.Text != "" && textBox10.Text != "")
                {
                    SqlConnection con1 = new SqlConnection(src);
                    con1.Open();
                    SqlCommand cmd1 = new SqlCommand("select * from SaleDebt", con1);
                    SqlDataReader dr1 = cmd1.ExecuteReader();
                    dataGridView1.Rows.Clear();
                    for (int ro = 0; ro < 15; ro++)
                    {
                        dataGridView1.Rows.Add(false, null, null, null, null, null);
                    }
                    row1 = 0;
                    while (dr1.Read())
                    {
                        if (DateTime.Parse(dr1["Date"].ToString()) <= DateTime.Parse(textBox8.Text) && DateTime.Parse(dr1["Date"].ToString()) >= DateTime.Parse(textBox10.Text))

                        {
                            dataGridView1.Rows.Insert(row1, false, dr1["Id"].ToString(), dr1["Date"].ToString(), dr1["Name"].ToString(), dr1["idSales"].ToString(), dr1["Amount"].ToString());
                            row1++;
                        }
                    }
                }
                if (row1 > 0)
                {
                    float sum1 = 0;
                    for (int k1 = 0; k1 <= row1 - 1; k1++)
                    {
                        sum1 = sum1 + float.Parse(dataGridView1.Rows[k1].Cells[5].Value.ToString());
                    }
                    textBox4.Text = sum1.ToString();
                    empty = false;
                }
                this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Descending);
                for (int y = 0; y < row1; y++)
                {
                    this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }

            }
            catch { MessageBox.Show("الرجاء التاكد من معلومات البحث"); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {

                textBox11.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox7.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox6.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox5.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();
                textBox3.Text = dataGridView1.Rows[rowindex1].Cells[5].Value.ToString();
                textBox1.Text = textBox3.Text;
                textBox9.Text = "0.0";
            }

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentCellAddress.X == 0 && dataGridView1.CurrentRow.Cells[1].Value != null)
            {
                if (!empty)
                {
                    if (row1 > 0 && dataGridView1.CurrentCell.Value.ToString() != "True")
                    {
                        select = true;
                        rowindex1 = dataGridView1.CurrentCell.RowIndex;
                        dataGridView1.Rows[rowindex1].Cells[0].Value = true;
                        for (int b = 0; b < row1; b++)
                        {
                            if (b != rowindex1)
                            { dataGridView1.Rows[b].Cells[0].Value = false; }
                        }

                    }
                    else
                    { select = false; }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            comboBox3.Text = "";
            textBox1.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            textBox5.Text = "";
            textBox8.Text = "";
            textBox7.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox3.Text = "";
            textBox6.Text = "";
            textBox9.Text = "";
            comboBox3.SelectedText = "";
            dateTimePicker3.Checked = false;
            dateTimePicker4.Checked = false;
            empty = true;
            select = false;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null, null, null);
            }
            comboBox3.Focus();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (select && dataGridView1.Rows[rowindex1].Cells[1].Value != null)
            {
                if (float.Parse(dataGridView1.Rows[rowindex1].Cells[5].Value.ToString()) <= 0)
                {
                    if ((MessageBox.Show("هل انت متأكد من حذف الدين ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "CusDD";
                        string page = "CusDD";
                        Program.mysignin.which(ww, page);
                    }
                }
                else { MessageBox.Show("لا يمكن حذف فاتورة قيمتها اكبر من صفر"); }
            }
        }

        public void CusDD(bool ok, string n)
        {
            if (ok)
            {
                SqlConnection cn111 = new SqlConnection(src);
                SqlCommand cmd111 = new SqlCommand("DELETE FROM [SaleDebt] WHERE Id = @Ird", cn111);
                cmd111.Parameters.AddWithValue("@Ird", dataGridView1.Rows[rowindex1].Cells[1].Value.ToString());
                cn111.Open();
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم حذف فاتورة مبيعات من الدين" + Environment.NewLine + "  رمز الحركة  " + dataGridView1.Rows[rowindex1].Cells[1].Value.ToString() + "  اسم الشركة  " + dataGridView1.Rows[rowindex1].Cells[3].Value.ToString() + "  التاريخ  " + dataGridView1.Rows[rowindex1].Cells[2].Value.ToString() + "  رقم فاتورة المبيعات  " + dataGridView1.Rows[rowindex1].Cells[4].Value.ToString() + "  القيمة  " + dataGridView1.Rows[rowindex1].Cells[5].Value.ToString());
                cmd.Parameters.AddWithValue("@textBox4", "DEL");
                con.Open();
                SqlDataReader dr111 = cmd111.ExecuteReader();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تم حذف الفاتورة بنجاح");
                comboBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox9.Text = "";
                comboBox3.SelectedText = "";
                dateTimePicker3.Checked = false;
                dateTimePicker4.Checked = false;
                empty = true;
                select = false;
                dataGridView1.Rows.Clear();

                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null, null);
                }comboBox3.Focus();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox11.Text != "")
            {
                if (textBox9.Text == "") { MessageBox.Show("الرجاء ادخال القيمة المدفوعة"); }
                else if(float.Parse(textBox9.Text) >= 0)
                {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        string ww = "CusDP";
                        string page = "CusDP";
                        Program.mysignin.which(ww, page);
                }
                else
                { MessageBox.Show("لا يجوز ان تكون القيمة اقل من صفر"); }
            }
        }

        public void CusDP(bool ok, string n)
        {
            if (ok)
            {
                SqlConnection conn5 = new SqlConnection(src);
                SqlCommand cmdn5 = new SqlCommand("UPDATE [SaleDebt] SET Amount=@box3 WHERE Id = '" + textBox11.Text + "'", conn5);
                cmdn5.Parameters.AddWithValue("@box3", textBox1.Text);
                conn5.Open();
                SqlDataReader d725 = cmdn5.ExecuteReader();
                conn5.Close();

                SqlConnection conn399 = new SqlConnection(src);
                conn399.Open();
                SqlCommand cmd399 = new SqlCommand("select * from Sales", conn399);
                float am = 0;
                string d = "";
                float p = 0;
                string rd = "";
                SqlDataReader dr399 = cmd399.ExecuteReader();
                while (dr399.Read())
                {
                    if (dr399["Id"].ToString() == textBox5.Text)
                    {
                        am = float.Parse(dr399["Amount"].ToString());
                        d = dr399["Debt"].ToString();
                        p = float.Parse(dr399["Paid"].ToString());
                        rd = dr399["RDebt"].ToString();
                    }
                }
                dr399.Close();

                SqlConnection conn58 = new SqlConnection(src);
                SqlCommand cmdn58 = new SqlCommand("UPDATE [Sales] SET Debt=@box1, Paid=@box2, RDebt=@box3 WHERE Id = '" + int.Parse(textBox5.Text) + "'", conn58);
                if (float.Parse(textBox1.Text) == 0)
                {
                    cmdn58.Parameters.AddWithValue("@box1", "لا");
                }
                else
                {
                    cmdn58.Parameters.AddWithValue("@box1", "نعم");
                }
                float a = p + float.Parse(textBox9.Text);
                cmdn58.Parameters.AddWithValue("@box2", a);
                float b = am - a;
                cmdn58.Parameters.AddWithValue("@box3", b);
                conn58.Open();
                SqlDataReader d7258 = cmdn58.ExecuteReader();
                conn58.Close();

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

                SqlConnection con55 = new SqlConnection(src);
                SqlCommand cmd55 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con55);
                cmd55.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                string t7 = (reg1 + float.Parse(textBox9.Text)).ToString();
                cmd55.Parameters.AddWithValue("@textBox2", t7);
                con55.Open();
                SqlDataReader dr155 = cmd55.ExecuteReader();

                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                cmd.Parameters.AddWithValue("@textBox1", n);
                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd.Parameters.AddWithValue("@textBox3", "لقد تم قبض مبلغ لفاتورة مبيعات من الدين" + Environment.NewLine + "  رمز الحركة  " + textBox7.Text + "  اسم الشركه  " + textBox2.Text + "  التاريخ  " + textBox6.Text + "  رقم فاتورة المبيعات  " + textBox5.Text + "  القيمة  " + textBox9.Text + "  المتبقي  " + textBox1.Text);
                cmd.Parameters.AddWithValue("@textBox4", "UP");
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();

                MessageBox.Show("لقد تم قبض المبلغ بنجاح");
                comboBox3.Text = "";
                textBox1.Text = "";
                textBox2.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox8.Text = "";
                textBox7.Text = "";
                textBox10.Text = "";
                textBox11.Text = "";
                textBox3.Text = "";
                textBox6.Text = "";
                textBox9.Text = "";
                comboBox3.SelectedText = "";
                dateTimePicker3.Checked = false;
                dateTimePicker4.Checked = false;
                empty = true;
                select = false;
                dataGridView1.Rows.Clear();

                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null, null, null);
                }
                comboBox3.Focus();
            }
        }

        private void comboBox3_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item = comboBox3.Text;
                string[] filteredItems = items1.Where(x => x.Contains(item)).ToArray();
                comboBox3.Items.Clear();
                comboBox3.Items.Add(item);
                comboBox3.Items.AddRange(filteredItems);
                comboBox3.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox3.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox3.DroppedDown = true;
                comboBox3.SelectionStart = item.Length;
                comboBox3.SelectionLength = 0;

                comboBox3.Cursor = Cursor.Current;
            }
        }
    }
}