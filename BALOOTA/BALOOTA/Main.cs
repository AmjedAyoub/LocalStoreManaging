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
using System.Drawing.Printing;

namespace BALOOTA
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        public bool empty = true;
        ComboBox cb;
        public string usern = "";
        public string[] itemarr = new string[1000000];
        public string[] profitarr = new string[1000000];
        public string[] cus = new string[1000000];
        int rowarr = 0;
        string it1 = "";
        bool enough = false;
        bool enough2 = false;
        float p = 0;
        float rd = 0;
        float profit = 0;
        string rdd = "";
        string sitem = "";
        string[] items1;
        string[] items2;
        string[] items3;
        string[] items4;
        float dis = 0;
        float per = 0;
        float q = 0;

        private void Main_Load(object sender, EventArgs e)
        {
        }

        public void l5()
        {
            SqlConnection conne = new SqlConnection(src);
            conne.Open();
            SqlCommand cmod = new SqlCommand("select max(Id) from Sales", conne);
            label5.Text = (Convert.ToInt32(cmod.ExecuteScalar()) + 1).ToString();
        }

        public void start()
        {
            if (comboBox2.Items.Count > 1)
            {
                comboBox2.Items.Clear();
            }
            if (comboBox1.Items.Count > 1)
            {
                comboBox1.Items.Clear();
            }
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from Sales", conn39);
            string iitem = "";
            SqlDataReader dr39 = cmd39.ExecuteReader();
            comboBox2.Items.Add("");
            while (dr39.Read())
            {
                iitem = dr39["CompanyName"].ToString();

                if (!comboBox1.Items.Contains(iitem))
                {
                    comboBox1.Items.Add(iitem);
                }
            }
            dr39.Close();
            items4 = new string[comboBox1.Items.Count];
            comboBox1.Items.CopyTo(items4, 0);
            empty = true;
            dataGridView1.Rows.Clear();
            SqlConnection conn393 = new SqlConnection(src);
            conn393.Open();
            SqlCommand cmd393 = new SqlCommand("select * from Items", conn393);
            string iitemtt = "";
            string cname = "";
            SqlDataReader dr393 = cmd393.ExecuteReader();
            this.Cname.Items.Add("");
            this.Column2.Items.Add("");
            while (dr393.Read())
            {
                iitemtt = dr393["ItemName"].ToString();
                cname = dr393["CompanyName"].ToString();

                if (!this.Cname.Items.Contains(iitemtt))
                {
                    this.Cname.Items.Add(iitemtt); comboBox2.Items.Add(iitemtt);
                }
                if (!this.Column2.Items.Contains(cname))
                {
                    this.Column2.Items.Add(cname);
                }

            }

            items1 = new string[Cname.Items.Count];
            Cname.Items.CopyTo(items1, 0);
            items2 = new string[Column2.Items.Count];
            Column2.Items.CopyTo(items2, 0);
            items3 = new string[Cname.Items.Count];
            Cname.Items.CopyTo(items3, 0);
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(null, null, null, null, null);
            }

            SqlConnection conn399 = new SqlConnection(src);
            conn399.Open();
            SqlCommand cmd399 = new SqlCommand("select * from Inventory", conn399);
            string alert = "";
            SqlDataReader dr399 = cmd399.ExecuteReader();
            while (dr399.Read())
            {
                if (float.Parse(dr399["Quantity"].ToString()) <= float.Parse(dr399["MinQ"].ToString()))
                {
                    alert = alert + (">>> الرجاء الانتباه  <<<" + Environment.NewLine + "لم يتبقى سوى   " + dr399["Quantity"] + "  من  " + dr399["Item"] + Environment.NewLine + "____________________________________" + Environment.NewLine + Environment.NewLine);

                }
            }
            dr399.Close();
            if (alert == "")
            { alert = "لا يوجد تحذيرات"; }
            textBox12.Text = alert;
            textBox2.Text = DateTime.Now.ToShortDateString();

            SqlConnection conn3911 = new SqlConnection(src);
            conn3911.Open();
            SqlCommand cmd3911 = new SqlCommand("select * from Alert", conn3911);
            string alertp = "";
            SqlDataReader dr3911 = cmd3911.ExecuteReader();
            while (dr3911.Read())
            {
                if (DateTime.Parse(dr3911["Date"].ToString()) >= DateTime.Parse(DateTime.Now.ToShortDateString()) && DateTime.Parse(dr3911["Alert"].ToString()) <= DateTime.Parse(DateTime.Now.ToShortDateString()))
                {
                    alertp = alertp + (">>>  تحذير  <<<" + Environment.NewLine + "اسم التحذير   " + dr3911["Name"] + Environment.NewLine + " التاريخ  " + dr3911["Date"] + Environment.NewLine + " الملاحظات  " + dr3911["Notes"] + Environment.NewLine + "____________________________________" + Environment.NewLine + Environment.NewLine);

                }
            }
            dr3911.Close();
            if (alertp == "")
            { alertp = "لا يوجد تحذيرات"; }
            textBox13.Text = alertp;

            textBox3.Text = "0.0";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox7.Text = "";
            textBox8.Text = "0.0";
            checkBox1.Checked = false;
            comboBox1.Focus();
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == Column2.DisplayIndex)
            {
                if (!this.Column2.Items.Contains(e.FormattedValue))
                {
                    this.Column2.Items.Add(e.FormattedValue);
                    dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[1].Value = e.FormattedValue;
                }
            }
            if (e.ColumnIndex == Cname.DisplayIndex)
            {
                if (!this.Cname.Items.Contains(e.FormattedValue))
                {
                    this.Cname.Items.Add(e.FormattedValue);
                    dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[0].Value = e.FormattedValue;

                }
            }
        }

        private void dataGridView1_CurrentCellChanged(object sender, EventArgs e)
        {
            int z = 0;
            string box = textBox8.Text;
            try
            {
                if (!empty)
                {
                    if (dataGridView1.Rows.Count > 1)
                    {
                        textBox8.Text = "";
                        textBox3.Text = "0.0";
                        for (int i = 0; i < dataGridView1.RowCount - 1; i++)
                        {
                            if (dataGridView1.Rows[i].Cells[0].Value != null)
                            {
                                this.dataGridView1.Rows[i].HeaderCell.Value = (i + 1).ToString();
                            }
                            if ((dataGridView1.CurrentRow.Index >= 0 && dataGridView1.Rows[i].Cells[3].Value != null && dataGridView1.Rows[i].Cells[2].Value != null))
                            {
                                z = i;
                                float a = float.Parse(dataGridView1.Rows[i].Cells[3].Value.ToString());
                                float b = float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                                float c = a * b;
                                dataGridView1.Rows[i].Cells[4].Value = c;
                                textBox3.Text = (float.Parse(textBox3.Text) + float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())).ToString();
                            }
                        }
                        textBox8.Text = box;
                        textBox8_Leave(sender,e);
                        if (checkBox1.Checked)
                        { textBox4.Text = ((float.Parse(textBox3.Text)) - (float.Parse(textBox5.Text))).ToString(); }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView1.Rows[z].Cells[2].Value = 0.0;
                dataGridView1.Rows[z].Cells[3].Value = 0.0;
                dataGridView1.Rows[z].Cells[4].Value = 0.0;
                textBox8.Text = box;
                textBox8_Leave(sender, e);
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                panel5.Enabled = true;
                textBox5.Text = "0.0";
                textBox4.Text = textBox3.Text;
            }
            else
            {
                panel5.Enabled = false;
                textBox4.Text = "0.0";
                textBox5.Text = "0.0";
            }
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (textBox5.Text != "")
                {
                    textBox4.Text = (float.Parse(textBox3.Text) - float.Parse(textBox5.Text)).ToString();
                    if (float.Parse(textBox4.Text) < 0)
                    {
                        MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                        textBox5.Text = "0.0";
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                textBox5.Text = "0.0";
            }
        }

        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if (this.dataGridView1.CurrentCellAddress.X == Column2.DisplayIndex)
            {
                cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cb.Sorted = true;
                    cb.DroppedDown = true;

                }

                empty = false;
            }
            if (this.dataGridView1.CurrentCellAddress.X == Cname.DisplayIndex)
            {
                cb = e.Control as ComboBox;
                if (cb != null)
                {
                    cb.DropDownStyle = ComboBoxStyle.DropDown;
                    cb.AutoCompleteMode = AutoCompleteMode.Suggest;
                    cb.Sorted = true;
                    cb.DroppedDown = true;

                }

                empty = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            comboBox1.Text = "";
            textBox7.Text = "";
            textBox3.Text = "0.0";
            textBox2.Text = DateTime.Now.ToShortDateString();
            checkBox1.Checked = false;
            empty = true;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(null, null, null, null, null);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enough = false;
            enough2 = false;
            p = 0;
            rd = 0;
            rdd = "";
            sitem = "";

            for (int rr = 0; rr < dataGridView1.RowCount - 1; rr++)
            {
                if (dataGridView1.Rows[rr].Cells[0].Value != null)
                {
                    float q = 0;
                    if (dataGridView1.Rows[rr].Cells[0].Value != null && dataGridView1.Rows[rr].Cells[1].Value != null)
                    {
                        SqlConnection conn3 = new SqlConnection(src);
                        conn3.Open();
                        SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        while (dr3.Read())
                        {

                            if (dataGridView1.Rows[rr].Cells[0].Value.ToString() == dr3["ItemName"].ToString() && dataGridView1.Rows[rr].Cells[1].Value.ToString() == dr3["CompanyName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                            {
                                q = q + float.Parse(dr3["RQuantity"].ToString());
                            }
                        }
                    }
                    else if (dataGridView1.Rows[rr].Cells[0].Value != null)
                    {
                        SqlConnection conn3 = new SqlConnection(src);
                        conn3.Open();
                        SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
                        SqlDataReader dr3 = cmd3.ExecuteReader();
                        while (dr3.Read())
                        {

                            if (dataGridView1.Rows[rr].Cells[0].Value.ToString() == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                            {
                                q = q + float.Parse(dr3["RQuantity"].ToString());
                            }
                        }
                    }
                    try
                    {
                        if (q >= float.Parse(dataGridView1.Rows[rr].Cells[3].Value.ToString()))
                        { enough = true; }
                        else
                        {
                            enough = false;
                            MessageBox.Show("الكميات في المستودع غير كافية لاتمام عملية البيع" + Environment.NewLine + Environment.NewLine + "                                       أو                   " + Environment.NewLine + Environment.NewLine + "             اسماء الشركات غير مطابقة للاصناف          ");
                            break;
                        }
                    }
                    catch { MessageBox.Show("الرجاء ادخال السعر و الكمية بشكل صحيح"); }
                   
                }

            }
            try
            {
                if (float.Parse(textBox3.Text) >= 0 || float.Parse(textBox8.Text) >= 0 || float.Parse(textBox4.Text) >= 0 || float.Parse(textBox5.Text) >= 0)
                { enough2 = true; }
                else
                {
                    enough2 = false;
                    MessageBox.Show("لا يجوز ان تكون القيم اقل من صفر");
                }
            }
            catch { MessageBox.Show("لا يجوز ان تكون القيم اقل من صفر");
                enough2 = false;
            }
            if (enough && enough2)
            {
                if ((MessageBox.Show("هل انت متأكد من عملية البيع ؟", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                {
                    Sale();
                }
            }
        }

        public void Sale()
        {
            Array.Clear(itemarr, 0, itemarr.Length);
            Array.Clear(profitarr, 0, profitarr.Length);
            rowarr = 0;
            profit = 0;
            q = 0;
            it1 = "";
            for (int rr = 0; rr < dataGridView1.RowCount - 1; rr++)
            {
                if (dataGridView1.Rows[rr].Cells[0].Value != null)
                {
                    if (dataGridView1.Rows[rr].Cells[0].Value != null && dataGridView1.Rows[rr].Cells[1].Value != null)
                    {

                        it1 = "";
                        profit = 0;
                        q = 0;
                        SaleList2(dataGridView1.Rows[rr].Cells[0].Value.ToString(), dataGridView1.Rows[rr].Cells[1].Value.ToString(), dataGridView1.Rows[rr].Cells[3].Value.ToString(), dataGridView1.Rows[rr].Cells[2].Value.ToString());

                    }
                    else
                    {
                        it1 = "";
                        profit = 0;
                        q = 0;
                        SaleList1(dataGridView1.Rows[rr].Cells[0].Value.ToString(), dataGridView1.Rows[rr].Cells[3].Value.ToString(), dataGridView1.Rows[rr].Cells[2].Value.ToString());
                    }

                }

            }
            MakeSale();
        }

        public void SaleList1(string item1, string quan1, string sprice)
        {
                        SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                string dd = dr3["Id"].ToString();
                if (item1 == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0 && float.Parse(dr3["RQuantity"].ToString()) >= float.Parse(quan1))
                {
                    q = q + float.Parse(quan1);
                    float qqq = float.Parse(dr3["RQuantity"].ToString()) - float.Parse(quan1);
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + quan1 + "@";
                    profit = profit + (float.Parse(quan1) * float.Parse(dr3["Price"].ToString()));
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", qqq);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();
                    S1(q, sprice);
                    break;
                }
                else if (item1 == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                {
                    q = q + float.Parse(dr3["RQuantity"].ToString());
                    float qq = float.Parse(quan1) - float.Parse(dr3["RQuantity"].ToString());
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + dr3["RQuantity"].ToString() + "@";
                    profit = profit + (float.Parse(dr3["RQuantity"].ToString()) * float.Parse(dr3["Price"].ToString()));
                    // ((float.Parse(dr3["RQuantity"].ToString()) * float.Parse(sprice)) - (float.Parse(dr3["RQuantity"].ToString()) * float.Parse(dr3["Price"].ToString())));
                    SqlConnection conn4 = new SqlConnection(src);
                    SqlCommand cmdn4 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn4);
                    cmdn4.Parameters.AddWithValue("@box1", "0");
                    conn4.Open();
                    SqlDataReader d724 = cmdn4.ExecuteReader();
                    conn4.Close();
                    SaleList1(item1, qq.ToString(), sprice);
                    break;
                }
            }
        }

        public void S1(float q, string s)
        {
            int r = 0;
            for (int rr = 0; rr < dataGridView1.RowCount - 1; rr++)
            {
                if (dataGridView1.Rows[rr].Cells[0].Value != null && dataGridView1.Rows[rr].Cells[2].Value != null)
                { r++; }
            }
            float sp = q * float.Parse(s);
            float sd = float.Parse(textBox8.Text) / r * 100 / sp / 100;
            profit = sp - (sp * sd) - profit;
            profitarr[rowarr] = profit.ToString();
            itemarr[rowarr] = it1;
            rowarr++;
           // MessageBox.Show("q   " + q + "     s    " + s + "     sp    " + sp + "     sd    " + sd + "     profit    " + profit + "     pppp    " + profit.ToString());
        }

        public void SaleList2(string item2, string company2, string quan2, string sprice2)
        {
            SqlConnection conn3 = new SqlConnection(src);
            conn3.Open();
            SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
            SqlDataReader dr3 = cmd3.ExecuteReader();
            while (dr3.Read())
            {
                string dd = dr3["Id"].ToString();

                if (item2 == dr3["ItemName"].ToString() && company2 == dr3["CompanyName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0 && float.Parse(dr3["RQuantity"].ToString()) >= float.Parse(quan2))
                {
                    q = q + float.Parse(quan2);
                    float qqq = float.Parse(dr3["RQuantity"].ToString()) - float.Parse(quan2);
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + quan2 + "@";
                    profit = profit + (float.Parse(quan2) * float.Parse(dr3["Price"].ToString()));
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", qqq);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();
                    S1(q, sprice2);
                    break;
                }
                else if (item2 == dr3["ItemName"].ToString() && company2 == dr3["CompanyName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                {
                    q = q + float.Parse(dr3["RQuantity"].ToString());
                    float qq = float.Parse(quan2) - float.Parse(dr3["RQuantity"].ToString());
                    it1 = it1 + dr3["Id"].ToString() + ">" + dr3["IdPurchase"].ToString() + ">" + dr3["Price"].ToString() + ">" + dr3["RQuantity"].ToString() + "@";
                    profit = profit + (float.Parse(dr3["RQuantity"].ToString()) * float.Parse(dr3["Price"].ToString()));
                    //((float.Parse(dr3["RQuantity"].ToString()) * float.Parse(sprice2)) - (float.Parse(dr3["RQuantity"].ToString()) * float.Parse(dr3["Price"].ToString())));
                    SqlConnection conn4 = new SqlConnection(src);
                    SqlCommand cmdn4 = new SqlCommand("UPDATE [Items] SET RQuantity=@box1 WHERE Id = '" + dd + "'", conn4);
                    cmdn4.Parameters.AddWithValue("@box1", "0");
                    conn4.Open();
                    SqlDataReader d724 = cmdn4.ExecuteReader();
                    conn4.Close();
                    SaleList2(item2, company2, qq.ToString(), sprice2);
                    break;
                }
            }
        }

        public void MakeSale()
        {
            if (checkBox1.Checked)
            {
                if (textBox4.Text != "0.0" || textBox4.Text != "0" || textBox4.Text != null)
                {
                    p = float.Parse(textBox5.Text);
                    rd = float.Parse(textBox4.Text);
                    rdd = "نعم";
                }
                else
                {
                    p = float.Parse(textBox3.Text);
                    rd = 0;
                    rdd = "لا";
                }
            }
            else
            {
                p = float.Parse(textBox3.Text);
                rd = 0;
                rdd = "لا";
            }

            SqlConnection co5 = new SqlConnection(src);
            SqlCommand cm5 = new SqlCommand("INSERT INTO [Sales](CompanyName,Date,Amount,Debt,Paid,RDebt,Notes,Profit,Dis)VALUES (@Box1,@Box2,@Box3,@Box4,@Box5,@Box6,@Box7,@Box8,@Box9)", co5);
            cm5.Parameters.AddWithValue("@Box1", comboBox1.Text);
            cm5.Parameters.AddWithValue("@Box2", textBox2.Text);
            cm5.Parameters.AddWithValue("@Box3", textBox3.Text);
            cm5.Parameters.AddWithValue("@Box4", rdd);
            cm5.Parameters.AddWithValue("@Box5", p);
            cm5.Parameters.AddWithValue("@Box6", rd);
            cm5.Parameters.AddWithValue("@Box7", textBox7.Text);
            float netp = 0;
            for (int b = 0; b < profitarr.Length; b++)
            {
                if (profitarr[b] != null)
                {
                    netp = netp + float.Parse(profitarr[b]);
                   // MessageBox.Show("profit    " + profitarr[b]+ "       netp    " + netp);
                }
            }
            cm5.Parameters.AddWithValue("@Box8", netp);
            cm5.Parameters.AddWithValue("@Box9", textBox8.Text);
            co5.Open();
            SqlDataReader d5 = cm5.ExecuteReader();
            SqlConnection conne33 = new SqlConnection(src);
            conne33.Open();
            SqlCommand cmod33 = new SqlCommand("select max(Id) from Sales", conne33);
            label5.Text = (Convert.ToInt32(cmod33.ExecuteScalar())).ToString();
            for (int jj = 0; jj < dataGridView1.RowCount - 1; jj++)
            {

                if (dataGridView1.Rows[jj].Cells[0].Value != null && dataGridView1.Rows[jj].Cells[0].Value.ToString() != "")
                {
                    float q23 = 0;
                    SqlConnection co55 = new SqlConnection(src);
                    SqlCommand cm55 = new SqlCommand("INSERT INTO [SoldItems](CompanyName,Date,ItemName,IdSale,Price,Quantity,FullPrice,Name,PP,Profit)VALUES (@Box1,@Box2,@Box3,@Box4,@Box5,@Box6,@Box7,@Box8,@Box9,@Box10)", co55);

                    if (dataGridView1.Rows[jj].Cells[1].Value != null && dataGridView1.Rows[jj].Cells[1].Value.ToString() != "")
                    { cm55.Parameters.AddWithValue("@Box1", dataGridView1.Rows[jj].Cells[1].Value); }
                    else
                    { cm55.Parameters.AddWithValue("@Box1", ""); }
                    cm55.Parameters.AddWithValue("@Box2", textBox2.Text);
                    cm55.Parameters.AddWithValue("@Box3", dataGridView1.Rows[jj].Cells[0].Value);
                    cm55.Parameters.AddWithValue("@Box4", int.Parse(label5.Text));
                    cm55.Parameters.AddWithValue("@Box5", dataGridView1.Rows[jj].Cells[2].Value);
                    cm55.Parameters.AddWithValue("@Box6", dataGridView1.Rows[jj].Cells[3].Value);
                    cm55.Parameters.AddWithValue("@Box7", dataGridView1.Rows[jj].Cells[4].Value);
                    cm55.Parameters.AddWithValue("@Box8", comboBox1.Text);
                    cm55.Parameters.AddWithValue("@Box9", itemarr[jj]);
                    cm55.Parameters.AddWithValue("@Box10", float.Parse(profitarr[jj]));
                   // MessageBox.Show("profit item   " + profitarr[jj]);
                    co55.Open();
                    SqlDataReader d55 = cm55.ExecuteReader();
                    sitem = sitem + Environment.NewLine + "  الصنف  " + dataGridView1.Rows[jj].Cells[0].Value.ToString() + "  السعر الفردي  " + dataGridView1.Rows[jj].Cells[2].Value.ToString() + "  الكمية  " + dataGridView1.Rows[jj].Cells[3].Value.ToString() + "  السعر الكلي  " + dataGridView1.Rows[jj].Cells[4].Value.ToString();


                    SqlConnection conn3 = new SqlConnection(src);
                    conn3.Open();
                    SqlCommand cmd3 = new SqlCommand("select * from Items", conn3);
                    SqlDataReader dr3 = cmd3.ExecuteReader();
                    string nit = "";
                    while (dr3.Read())
                    {

                        if (dataGridView1.Rows[jj].Cells[0].Value.ToString() == dr3["ItemName"].ToString() && float.Parse(dr3["RQuantity"].ToString()) > 0)
                        {
                            q23 = q23 + float.Parse(dr3["RQuantity"].ToString());
                            nit = dr3["ItemName"].ToString();
                        }

                    }
                    string itid = "";
                    SqlConnection conn31 = new SqlConnection(src);
                    conn31.Open();
                    SqlCommand cmd31 = new SqlCommand("select * from Inventory WHERE Item = @Name", conn31);
                    cmd31.Parameters.AddWithValue("@Name", nit);
                    SqlDataReader dr81 = cmd31.ExecuteReader();
                    while (dr81.Read())
                    {
                        itid = dr81["Id"].ToString();
                    }
                    dr81.Close();
                    SqlConnection conn = new SqlConnection(src);
                    SqlCommand cmdn = new SqlCommand("UPDATE [Inventory] SET Quantity=@box1 WHERE Id = '" + itid + "'", conn);
                    cmdn.Parameters.AddWithValue("@box1", q23);
                    conn.Open();
                    SqlDataReader d72 = cmdn.ExecuteReader();
                    conn.Close();
                }
            }
            if (rdd == "نعم")
            {

                SqlConnection co = new SqlConnection(src);
                SqlCommand cm = new SqlCommand("INSERT INTO [SaleDebt](Name,Date,Amount,idSales)VALUES (@Box1,@Box2,@Box3,@Box4)", co);
                cm.Parameters.AddWithValue("@Box1", comboBox1.Text);
                cm.Parameters.AddWithValue("@Box2", textBox2.Text);
                cm.Parameters.AddWithValue("@Box3", textBox3.Text);
                cm.Parameters.AddWithValue("@Box4", label5.Text);
                co.Open();
                SqlDataReader d = cm.ExecuteReader();
            }
            SqlConnection conne2 = new SqlConnection(src);
            conne2.Open();
            SqlCommand cmod2 = new SqlCommand("select max(Id) from Register", conne2);
            int regiid = Convert.ToInt32(cmod2.ExecuteScalar());
            SqlConnection co4 = new SqlConnection(src);
            co4.Open();
            SqlCommand cm4 = new SqlCommand("select * from Register WHERE Id ='" + regiid + "'", co4);
            SqlDataReader d4 = cm4.ExecuteReader();
            float reg4 = 0;
            float t4 = 0;
            while (d4.Read())
            {
                reg4 = float.Parse(d4["Amount"].ToString());
            }
            t4 = reg4 + p;
            SqlConnection co51 = new SqlConnection(src);
            SqlCommand cm51 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", co51);
            cm51.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
            cm51.Parameters.AddWithValue("@textBox2", t4);
            co51.Open();
            SqlDataReader d51 = cm51.ExecuteReader();



            SqlConnection con6w = new SqlConnection(src);
            SqlCommand cmd6w = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con6w);
            cmd6w.Parameters.AddWithValue("@textBox1", usern);
            cmd6w.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
            cmd6w.Parameters.AddWithValue("@textBox3", " لقد تمت عملية بيع  " + ">>" + "  الاسم " + comboBox1.Text + "   " + "  رقم الفاتورة " + label5.Text + Environment.NewLine + "  التاريخ " + textBox2.Text + "   " + "  القيمة " + textBox3.Text + "   " + "  القيمة المدفوعة " + textBox5.Text + "   " + "   القيمة المتبقية " + textBox4.Text + "  ملاحظات " + textBox7.Text + Environment.NewLine + "  الاصناف  " + sitem);
            cmd6w.Parameters.AddWithValue("@textBox4", "SL");
            con6w.Open();
            SqlDataReader dr6w = cmd6w.ExecuteReader();
            start();
            l5();
            per = 0;
            dis = 0;
            MessageBox.Show("لقد تمت عملية البيع بنجاح ");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string[] ar = new string[1000000];
            for (int w = 0; w < dataGridView1.RowCount - 1; w++)
            {
                if (dataGridView1.Rows[w].Cells[0].Value != null)
                {
                    ar[w] = dataGridView1.Rows[w].Cells[0].Value.ToString();
                }
            }
            Program.mynote.Show();
            Program.mynote.S(ar);
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item1 = comboBox1.Text;
                string[] filteredItems1 = items4.Where(x => x.Contains(item1)).ToArray();
                comboBox1.Items.Clear();
                comboBox1.Items.Add(item1);
                comboBox1.Items.AddRange(filteredItems1);
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox1.DroppedDown = true;
                comboBox1.SelectionStart = item1.Length;
                comboBox1.SelectionLength = 0;

                comboBox1.Cursor = Cursor.Current;
            }
        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item2 = comboBox2.Text;
                string[] filteredItems2 = items3.Where(x => x.Contains(item2)).ToArray();
                comboBox2.Items.Clear();
                comboBox2.Items.Add(item2);
                comboBox2.Items.AddRange(filteredItems2);
                comboBox2.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox2.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox2.DroppedDown = true;
                comboBox2.SelectionStart = item2.Length;
                comboBox2.SelectionLength = 0;

                comboBox2.Cursor = Cursor.Current;
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            
        }

        int i = 0; int c = 0; int dr = 0; bool end = false;
        private void button3_Click(object sender, EventArgs e)
        {
            end = false;
            i = 0;
            
            try
            {
                for (int f = 0; f < dataGridView1.RowCount - 1; f++)
                {
                    if (dataGridView1.Rows[f].Cells[0].Value != null)
                    { i++; }
                }
                if(i>0)
                {

                     DialogResult result = printDialog1.ShowDialog();
                     if (result == DialogResult.OK)
                     {
                         printDocument1.Print();
                     }
                    // DialogResult result = printPreviewDialog1.ShowDialog();
                     //if (result == DialogResult.OK)
                     //{
                      //   printDocument1.Print();
                     //}
                    
                }
            }catch
            {
                
            } 
            if (i > 28) { c = 0; dr = 0; }
            else { c = -1; }
            
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.HasMorePages = false;
            Image newImage2 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\C.PNG");
            Image newImage3 = Image.FromFile("C:\\Users\\samer\\Downloads\\Owes\\Owes\\Owes\\Resources\\C2.PNG");
            
            if (i <= 28 && i>0)
            {
                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@    Samer
                 ///*
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
                e.Graphics.DrawString("   فاتورة" + Environment.NewLine + "نقدي - ذمم", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));
                e.Graphics.DrawString("رقم الفاتورة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(760, 180));
                e.Graphics.DrawString(label5.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                e.Graphics.DrawString(comboBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                e.Graphics.DrawString(textBox2.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                e.Graphics.DrawImage(newImage2, 50, 225);


                int ee = 300;
                for (int r = 0; r <= i; r++) //29 36
                {
                    if (dataGridView1.Rows[r].Cells[0].Value != null)
                    {
                        e.Graphics.DrawString(dataGridView1.Rows[r].Cells[2].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                        e.Graphics.DrawString(dataGridView1.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                        e.Graphics.DrawString(dataGridView1.Rows[r].Cells[0].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                        e.Graphics.DrawString(dataGridView1.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                        ee += 20;
                    }
                }
                
                e.Graphics.DrawImage(newImage3, 50, 900);
                e.Graphics.DrawString(textBox8.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 915));
                e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 905));
                e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 927));
                e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 915));

                e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, 970));
                e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, 990));
                e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, 990));
                end = true;
            }
            else if (i > 28 && i <= 35)
            {
                if (c == 0)
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
                    e.Graphics.DrawString("   فاتورة" + Environment.NewLine + "نقدي - ذمم", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));
                    e.Graphics.DrawString("رقم الفاتورة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(760, 180));
                    e.Graphics.DrawString(label5.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                    e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                    e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                    e.Graphics.DrawString(comboBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                    e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                    e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                    e.Graphics.DrawString(textBox2.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                    e.Graphics.DrawImage(newImage2, 50, 225);


                    int ee = 300;
                    for (int r = 0; r <= i; r++) //29 36
                    {
                         if (dataGridView1.Rows[r].Cells[0].Value != null)
                         {
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[2].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[0].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                            ee += 20;
                        }
                    }

                    e.Graphics.DrawString("1", new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                }
                if (c == 1)
                {
                    e.Graphics.DrawImage(newImage3, 50, 50);
                    e.Graphics.DrawString(textBox8.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 65));
                    e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 55));
                    e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 77));
                    e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 65));

                    e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, 120));
                    e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, 140));
                    e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, 140));
                    e.Graphics.DrawString("2", new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                    end = true;
                }
                if (c == 0) { e.HasMorePages = true; c++; return; }
                else if (c == 1)
                {
                    e.HasMorePages = false;
                }
            }
            else if(i>35)
            {
                if (c == 0)
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
                    e.Graphics.DrawString("   فاتورة" + Environment.NewLine + "نقدي - ذمم", new Font("Microsoft Sans Serif", 14, FontStyle.Bold), Brushes.Navy, new PointF(400, 105));
                    e.Graphics.DrawString("رقم الفاتورة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(760, 180));
                    e.Graphics.DrawString(label5.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Red, new PointF(710, 180));
                    e.Graphics.DrawString("المطلوب من السيد / ة / السادة" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(500, 180));
                    e.Graphics.DrawString("_____________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(250, 180));
                    e.Graphics.DrawString(comboBox1.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Black, new PointF(270, 180));
                    e.Graphics.DrawString("المحترمـ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(190, 180));
                    e.Graphics.DrawString("التاريخ" + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(120, 180));
                    e.Graphics.DrawString(textBox2.Text + Environment.NewLine, new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(20, 180));
                    e.Graphics.DrawImage(newImage2, 50, 225);


                    int ee = 300;
                    for (int r = 0; r < 36; r++) //29 36
                    {
                         if (dataGridView1.Rows[r].Cells[0].Value != null)
                         {
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[2].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee));
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee));
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[0].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee));
                            e.Graphics.DrawString(dataGridView1.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee));
                            ee += 20;
                            dr++;
                        }
                    }

                    e.Graphics.DrawString("1", new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                   
                }
                if (c>0)
                {
                    if(i - dr < 38  && i>dr)
                    {
                        e.Graphics.DrawImage(newImage2, 50, 50);
                        int ee2 = 125;
                        int y = dr;
                        for (int r = y; r <= i; r++) //38 45
                        {
                             if (dataGridView1.Rows[r].Cells[0].Value != null)
                             {
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[2].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[0].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
                                ee2 += 20;
                                dr++;
                            }
                        }
                        ee2 += 20;
                        e.Graphics.DrawImage(newImage3, 50, ee2);
                        e.Graphics.DrawString(textBox8.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2 + 15));
                        e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, ee2 + 5));
                        e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, ee2 + 27));
                        e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, ee2 + 15));

                        e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, ee2 + 70));
                        e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, ee2 + 90));
                        e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, ee2 + 90));
                        e.Graphics.DrawString((c+1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                        end = true;
                    }
                    else if (i - dr >= 38 && i - dr < 45 )
                    {
                        if (dr <= i)
                        {
                            e.Graphics.DrawImage(newImage2, 50, 50);
                            int y = dr;
                            int ee2 = 125;
                            for (int r = y; r <= i; r++) //38 45
                            {
                                 if (dataGridView1.Rows[r].Cells[0].Value != null)
                                 {
                                    e.Graphics.DrawString(dataGridView1.Rows[r].Cells[2].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                    e.Graphics.DrawString(dataGridView1.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                    e.Graphics.DrawString(dataGridView1.Rows[r].Cells[0].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                    e.Graphics.DrawString(dataGridView1.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
                                    ee2 += 20; dr++;
                                }
                            }
                            e.Graphics.DrawString((c+1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));

                            if (dr > i)
                            { e.HasMorePages = true; c++; return; }
                        }

                    }
                    else if(dr>i && !end)
                    {
                        e.Graphics.DrawImage(newImage3, 50, 50);
                        e.Graphics.DrawString(textBox8.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, 65));
                        e.Graphics.DrawString(textBox5.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 55));
                        e.Graphics.DrawString(textBox4.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(310, 77));
                        e.Graphics.DrawString(textBox3.Text, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(560, 65));

                        e.Graphics.DrawString("إستلمت البضاعة بحالة جيدة و أتعهد بسداد قيمتها عند الطلب*", new Font("Microsoft Sans Serif", 10, FontStyle.Bold), Brushes.Navy, new PointF(480, 120));
                        e.Graphics.DrawString(": توقيع المستلم", new Font("Microsoft Sans Serif", 12, FontStyle.Bold), Brushes.Navy, new PointF(280, 140));
                        e.Graphics.DrawString("___________________________" + Environment.NewLine, new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Navy, new PointF(50, 140));
                        e.Graphics.DrawString((c + 1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                        end = true;
                    }
                    else if(i-dr>=45)
                    {
                        e.Graphics.DrawImage(newImage2, 50, 50);
                        int y = dr;
                        int ee2 = 125;
                        for (int r = y; r <= y+45; r++) //38 45
                        {
                             if (dataGridView1.Rows[r].Cells[0].Value != null)
                            {
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[2].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(90, ee2));
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[3].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(205, ee2));
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[0].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(420, ee2));
                                e.Graphics.DrawString(dataGridView1.Rows[r].Cells[4].Value.ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Regular), Brushes.Black, new PointF(705, ee2));
                                ee2 += 20; dr++;
                            }
                        }
                        e.Graphics.DrawString((c + 1).ToString(), new Font("Microsoft Sans Serif", 11, FontStyle.Bold), Brushes.Navy, new PointF(400, 1050));
                        if (dr > i)
                        { e.HasMorePages = true; c++; return; }
                    }

                }
                if (dr <= i && !end)
                {
                    if (dr == i) { dr++; }
                    c++; e.HasMorePages = true; return;
                }
            }

        }

        private void textBox8_Leave(object sender, EventArgs e)
        {

            dis = 0;
            per = 0;
            try
            {
                for (int r = 0; r < dataGridView1.RowCount - 1; r++)
                {
                    if (dataGridView1.Rows[r].Cells[4].Value != null)
                    { dis = dis + float.Parse(dataGridView1.Rows[r].Cells[4].Value.ToString()); }
                }
                if (textBox8.Text != "")
                {
                    if (float.Parse(textBox8.Text) == 0)
                    {
                        textBox3.Text = dis.ToString();
                        dis = 0;
                        per = 0;
                    }
                    else
                    {
                        textBox3.Text = (dis - float.Parse(textBox8.Text)).ToString();
                       // per = ((float.Parse(textBox8.Text) * 100) / dis) / 100;
                        if (float.Parse(textBox3.Text) < 0)
                        {
                            MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                            textBox3.Text = dis.ToString();
                            textBox8.Text = "0.0";
                            dis = 0;
                            per = 0;
                        }
                    }
                }
            }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة");
                textBox8.Text = "0.0";
            }
        }

        private void textBox8_Validated(object sender, EventArgs e)
        {
            
        }
    }

}