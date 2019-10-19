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
    public partial class Invoucein : Form
    {
        public Invoucein()
        {
            InitializeComponent();
        }
        public bool empty = true;
        public string ww = "";
        public string page = "";
        public string d = "";
        public int invid;
        public string stItem = "";
        int regid = 0;
        public string[] itemarr = new string[1000000];
        ComboBox cb;
        private string src = Program.xsrc;
        string[] items;
        string[] items1;
        string[] items2;
        DataGridViewEditingControlShowingEventArgs ee;
        float dis = 0;
        float per = 0;

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            { panel5.Enabled = true;
                textBox5.Text = "0.00";
                textBox4.Text = textBox3.Text;
            }
            else
            {
                panel5.Enabled = false;
                textBox4.Text = "0.0";
                textBox5.Text = "0.0";
            }
        }

        public void found(bool ok, string name, string wh)
        {
            if (ok)
            {
                SqlConnection con = new SqlConnection(src);
                SqlCommand cmd = new SqlCommand("INSERT INTO [Purchases](CompanyName,InvoiceNo,Date,Amount,Debt,Paid,RDebt,Notes,Dis)VALUES (@comboBox1,@textBox1,@textBox2,@textBox3,@text,@textBox5,@textBox4,@textBox11,@textBox12)", con);
                cmd.Parameters.AddWithValue("@textBox1", textBox1.Text);
                cmd.Parameters.AddWithValue("@comboBox1", comboBox1.Text);
                cmd.Parameters.AddWithValue("@textBox5", textBox5.Text);
                cmd.Parameters.AddWithValue("@textBox4", textBox4.Text);
                cmd.Parameters.AddWithValue("@textBox2", textBox2.Text);
                cmd.Parameters.AddWithValue("@textBox3", textBox3.Text);
                cmd.Parameters.AddWithValue("@text", d);
                cmd.Parameters.AddWithValue("@textBox11", textBox11.Text);
                cmd.Parameters.AddWithValue("@textBox12", textBox8.Text);
                con.Open();
                SqlDataReader dr2 = cmd.ExecuteReader();
                


                SqlConnection conne = new SqlConnection(src);
                conne.Open();
                SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                regid = Convert.ToInt32(cmod.ExecuteScalar());
                SqlConnection conn3316 = new SqlConnection(src);
                conn3316.Open();
                SqlCommand cmd3316 = new SqlCommand("select * from Register WHERE Id ='" + regid + "'", conn3316);
                SqlDataReader dr8316 = cmd3316.ExecuteReader();
                float reg = 0;
                float t = 0;
                while (dr8316.Read())
                {
                    reg = float.Parse(dr8316["Amount"].ToString());
                }
                t = reg - float.Parse(textBox5.Text);
                SqlConnection con555 = new SqlConnection(src);
                SqlCommand cmd555 = new SqlCommand("INSERT INTO [Register](Date,Amount)VALUES (@textBox1,@textBox2)", con555);
                cmd555.Parameters.AddWithValue("@textBox1", DateTime.Now.ToShortDateString());
                cmd555.Parameters.AddWithValue("@textBox2", t);
                con555.Open();
                SqlDataReader dr1555 = cmd555.ExecuteReader();









                SqlConnection conne4 = new SqlConnection(src);
                conne4.Open();
                SqlCommand cmod4 = new SqlCommand("select max(Id) from Purchases", conne4);
                invid = Convert.ToInt32(cmod4.ExecuteScalar());
                stItem = "";
                if (d == "نعم")
                {
                    SqlConnection con55 = new SqlConnection(src);
                    SqlCommand cmd55 = new SqlCommand("INSERT INTO [StoreDebt](Date,Name,InvNo,Amount,idPurchase)VALUES (@textBox1,@textBox2,@textBox3,@textBox4,@text)", con55);
                    cmd55.Parameters.AddWithValue("@textBox1", textBox2.Text);
                    cmd55.Parameters.AddWithValue("@textBox2", comboBox1.Text);
                    cmd55.Parameters.AddWithValue("@textBox3", textBox1.Text);
                    cmd55.Parameters.AddWithValue("@textBox4", textBox4.Text);
                    cmd55.Parameters.AddWithValue("@text", invid);
                    con55.Open();
                    SqlDataReader dr155 = cmd55.ExecuteReader();
                }
                // @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                for (int j = 0; j < dataGridView1.RowCount - 1; j++)
                {
                    if (dataGridView1.Rows[j].Cells[0].Value != null && dataGridView1.Rows[j].Cells[1].Value != null && dataGridView1.Rows[j].Cells[2].Value != null && dataGridView1.Rows[j].Cells[3].Value != null && dataGridView1.Rows[j].Cells[4].Value != null)
                    {
                        string srcc = Program.xsrc;
                        SqlConnection con66q = new SqlConnection(srcc);
                        SqlCommand cmd66q = new SqlCommand("INSERT INTO [Items](IdPurchase,ItemName,CompanyName,InvoiceNo,Date,Price,Quantity,MinQuantity,FullPrice,RQuantity,Notes)VALUES (@invid,@data0,@comboBox1,@textBox1,@textBox2,@data1,@data2,@data3,@data4,@data5,@N)", con66q);
                        cmd66q.Parameters.AddWithValue("@invid", invid);
                        cmd66q.Parameters.AddWithValue("@data0", dataGridView1.Rows[j].Cells[0].Value);
                        cmd66q.Parameters.AddWithValue("@comboBox1", comboBox1.Text);
                        cmd66q.Parameters.AddWithValue("@textBox1", textBox1.Text);
                        cmd66q.Parameters.AddWithValue("@textBox2", textBox2.Text);
                        cmd66q.Parameters.AddWithValue("@data1", dataGridView1.Rows[j].Cells[1].Value);
                        cmd66q.Parameters.AddWithValue("@data2", dataGridView1.Rows[j].Cells[2].Value);
                        cmd66q.Parameters.AddWithValue("@data3", dataGridView1.Rows[j].Cells[3].Value);
                        cmd66q.Parameters.AddWithValue("@data4", dataGridView1.Rows[j].Cells[4].Value);
                        cmd66q.Parameters.AddWithValue("@data5", dataGridView1.Rows[j].Cells[2].Value);
                        if (dataGridView1.Rows[j].Cells[5].Value != null)
                        {
                            cmd66q.Parameters.AddWithValue("@N", dataGridView1.Rows[j].Cells[5].Value);
                            stItem = stItem + Environment.NewLine + "  الصنف  " + dataGridView1.Rows[j].Cells[0].Value.ToString() + "  السعر الفردي  " + dataGridView1.Rows[j].Cells[1].Value.ToString() + "  الكمية  " + dataGridView1.Rows[j].Cells[2].Value.ToString() + "  الحد الادنى  " + dataGridView1.Rows[j].Cells[3].Value.ToString() + "  السعر الكلي  " + dataGridView1.Rows[j].Cells[4].Value.ToString() + "  الملاحظات  " + dataGridView1.Rows[j].Cells[5].Value.ToString();

                        }
                        else
                        {
                            cmd66q.Parameters.AddWithValue("@N", "لا يوجد");
                            stItem = stItem + Environment.NewLine + "  الصنف  " + dataGridView1.Rows[j].Cells[0].Value.ToString() + "  السعر الفردي  " + dataGridView1.Rows[j].Cells[1].Value.ToString() + "  الكمية  " + dataGridView1.Rows[j].Cells[2].Value.ToString() + "  الحد الادنى  " + dataGridView1.Rows[j].Cells[3].Value.ToString() + "  السعر الكلي  " + dataGridView1.Rows[j].Cells[4].Value.ToString() + "  الملاحظات  " + "لا يوجد";
                        }
                        // cmd66q.Parameters.AddWithValue("@text11", dataGridView1.Rows[j].Cells[5].Value);
                        con66q.Open();
                        SqlDataReader dr66q = cmd66q.ExecuteReader();
                        con66q.Close();




                        string q = "";
                        string mq = "";
                        string idid = "";
                        string note = "";
                        string src = Program.xsrc; // path for DB
                        SqlConnection con7 = new SqlConnection(src);
                        bool blnfound7 = false; // the username and pass correct (ana b76 enoh false cuz bfred enoh feh eroor bl user or pass)
                        con7.Open();
                        SqlCommand cmd7 = new SqlCommand("select * from Inventory", con7);
                        SqlDataReader dr7 = cmd7.ExecuteReader();
                        while (dr7.Read())
                        {
                            if (dr7["Item"].ToString() == dataGridView1.Rows[j].Cells[0].Value.ToString())
                            {
                                blnfound7 = true;
                                q = dr7["Quantity"].ToString();
                                mq = dr7["MinQ"].ToString();
                                idid = dr7["Id"].ToString();
                                note= dr7["Notes"].ToString();
                            }
                        }
                        con7.Close();
                        if (blnfound7)
                        {
                            SqlConnection conn7 = new SqlConnection(src);
                            SqlCommand cmdn7 = new SqlCommand("UPDATE [Inventory] SET  Quantity = @box2, MinQ = @box3, Notes = @N WHERE Id = '" + idid + "'", conn7);
                            cmdn7.Parameters.AddWithValue("@box2", (float.Parse(q)) + (float.Parse(dataGridView1.Rows[j].Cells[2].Value.ToString())));
                            cmdn7.Parameters.AddWithValue("@box3", dataGridView1.Rows[j].Cells[3].Value);
                            if (dataGridView1.Rows[j].Cells[5].Value != null)
                            {
                                cmdn7.Parameters.AddWithValue("@N", dataGridView1.Rows[j].Cells[5].Value);
                            }
                            else
                            {
                                cmdn7.Parameters.AddWithValue("@N", note);
                            }
                                conn7.Open();
                            SqlDataReader dr72 = cmdn7.ExecuteReader();
                            conn7.Close();
                        }
                        else
                        {
                            SqlConnection con667 = new SqlConnection(src);
                            SqlCommand cmd667 = new SqlCommand("INSERT INTO [Inventory](Item,Quantity,MinQ,Notes)VALUES (@data0,@data2,@data3,@N)", con667);
                            cmd667.Parameters.AddWithValue("@data0", dataGridView1.Rows[j].Cells[0].Value);
                            cmd667.Parameters.AddWithValue("@data2", dataGridView1.Rows[j].Cells[2].Value);
                            cmd667.Parameters.AddWithValue("@data3", dataGridView1.Rows[j].Cells[3].Value);
                            if (dataGridView1.Rows[j].Cells[5].Value != null)
                            {
                                cmd667.Parameters.AddWithValue("@N", dataGridView1.Rows[j].Cells[5].Value);
                            }
                            else
                            {
                                cmd667.Parameters.AddWithValue("@N", "لا يوجد");
                            }
                            con667.Open();
                            SqlDataReader dr667 = cmd667.ExecuteReader();
                            con667.Close();
                        }

                    }
                }
                
                //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                SqlConnection con6 = new SqlConnection(src);
                SqlCommand cmd6 = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con6);
                cmd6.Parameters.AddWithValue("@textBox1", name);
                cmd6.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                cmd6.Parameters.AddWithValue("@textBox3", " لقد تم اضافة فاتورة مشتريات " + ">>" + "  رمز الحركة " + invid + "  اسم الشركة " + comboBox1.Text + "   " + "  رقم الفاتورة " + textBox1.Text + Environment.NewLine + "  التاريخ " + textBox2.Text + "   " + "  القيمة " + textBox3.Text + "   " + "  القيمة المدفوعة " + textBox5.Text + "   " + "   القيمة المتبقية " + textBox4.Text + "  ملاحظات " + textBox11.Text + Environment.NewLine + "  الاصناف  " + stItem);
                cmd6.Parameters.AddWithValue("@textBox4", "AD");
                con6.Open();
                SqlDataReader dr6 = cmd6.ExecuteReader();
                MessageBox.Show("لقد تمت اضافة الفاتورة بنجاح ");
                textBox1.Text = "";
                comboBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "0.0";
                textBox8.Text = "0.0";
                textBox4.Text = "0.0";
                textBox5.Text = "0.0";
                textBox11.Text = "";
                checkBox1.Checked = false;
                startin();
            }
        }

        public void button1_Click(object sender, EventArgs e)
        {
            if (float.Parse(textBox3.Text) < 0 || float.Parse(textBox4.Text) < 0 || float.Parse(textBox5.Text) < 0)
            {
                MessageBox.Show("لا يجوز ان تكون القيم اقل من صفر");
            }
            else if (comboBox1.Text == "") { MessageBox.Show("الرجاء ادخال اسم الشركة"); }
            else if (textBox1.Text == "") { MessageBox.Show("الرجاء ادخال رقم الفاتورة"); }
            else if (textBox2.Text == "") { MessageBox.Show("الرجاء ادخال التاريخ"); }
            else if (dataGridView1.Rows.Count<=1) { MessageBox.Show("الرجاء ادخال الاصناف الى الجدول"); }
            else if (checkBox1.Checked)
            {
                if (textBox5.Text == "") { MessageBox.Show("الرجاء ادخال القيمة المدفوعة"); }
                else
                {
                    SqlConnection conne = new SqlConnection(src);
                    conne.Open();
                    SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                    int x= Convert.ToInt32(cmod.ExecuteScalar());
                    SqlConnection conn3316 = new SqlConnection(src);
                    conn3316.Open();
                    SqlCommand cmd3316 = new SqlCommand("select * from Register WHERE Id ='" + x + "'", conn3316);
                    SqlDataReader dr8316 = cmd3316.ExecuteReader();
                    float reg1 = 0;
                    while (dr8316.Read())
                    {
                        reg1 = float.Parse(dr8316["Amount"].ToString());
                    }
                    if (float.Parse(textBox5.Text) <= reg1)
                    {
                        Program.mysignin.Show();
                        Program.mysignin.Signin_Load(sender, e);
                        ww = "ADDINV";
                        page = "INVIN";
                        d = "نعم";
                        Program.mysignin.which(ww, page);
                    }
                    else
                    {
                        MessageBox.Show("الصندوق لا يكفي لإتمام العملية");
                    }
                }

            }
            else
            {
                SqlConnection conne = new SqlConnection(src);
                conne.Open();
                SqlCommand cmod = new SqlCommand("select max(Id) from Register", conne);
                int x = Convert.ToInt32(cmod.ExecuteScalar());
                SqlConnection conn3316 = new SqlConnection(src);
                conn3316.Open();
                SqlCommand cmd3316 = new SqlCommand("select * from Register WHERE Id ='" + x + "'", conn3316);
                SqlDataReader dr8316 = cmd3316.ExecuteReader();
                float reg1 = 0;
                while (dr8316.Read())
                {
                    reg1 = float.Parse(dr8316["Amount"].ToString());
                }
                if (float.Parse(textBox3.Text) <= reg1)
                {
                    Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                ww = "ADDINV";
                page = "INVIN";
                d = "لا";
                Program.mysignin.which(ww, page);
                textBox5.Text = textBox3.Text;
                textBox4.Text = "0.0";
                }
                else
                {
                    MessageBox.Show("الصندوق لا يكفي لإتمام العملية");
                }
            }
        }
        
        public void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            ee = e;
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

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.ColumnIndex == Cname.DisplayIndex)
            {
                if (!this.Cname.Items.Contains(e.FormattedValue))
                {
                    this.Cname.Items.Add(e.FormattedValue);
                    dataGridView1.Rows[dataGridView1.CurrentCellAddress.Y].Cells[0].Value = e.FormattedValue;


                }



            }
        }

        public void startin()
        {
            textBox1.Text = "";
            comboBox1.Text = "";
            comboBox2.Text = "";
            textBox2.Text = "";
            textBox3.Text = "0.0";
            textBox4.Text = "0.0";
            textBox5.Text = "0.0";
            textBox8.Text = "0.0";
            textBox11.Text = "";
            checkBox1.Checked = false;

            //DataTable dt = new DataTable();
            int x = 0;
            if (comboBox1.Items.Count > 1)
            { comboBox1.Items.Clear(); }
            SqlConnection conn39 = new SqlConnection(src);
            conn39.Open();
            SqlCommand cmd39 = new SqlCommand("select * from Purchases", conn39);
            string iitem = "";
            comboBox1.Items.Add("");
            SqlDataReader dr39 = cmd39.ExecuteReader();
            while (dr39.Read())
            {
                iitem = dr39["CompanyName"].ToString();
              if (!comboBox1.Items.Contains(iitem))
                {
                     comboBox1.Items.Add(iitem);

                    itemarr[x] = iitem;

                    x++;
                }
            }
            dr39.Close();
            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@

            //comboBox1.TextChanged += new EventHandler(comboBox1_TextChanged);
            items = new string[comboBox1.Items.Count];
            comboBox1.Items.CopyTo(items, 0);

            //@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@


         //   SqlDataAdapter sda = new SqlDataAdapter();
         //   sda.Fill(dt);
         //   comboBox1.ValueMember = "Id";
         //   comboBox1.DisplayMember = "CompanyName";
         //   comboBox1.DataSource = dt;
            empty = true;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 15; ro++)
            {
                dataGridView1.Rows.Add(null, null, null, null, null, null);
            }

            SqlConnection conn393 = new SqlConnection(src);
            conn393.Open();
            SqlCommand cmd393 = new SqlCommand("select * from Items", conn393);
            string iitemtt = "";
            SqlDataReader dr393 = cmd393.ExecuteReader();
            int y = 0;
            while (dr393.Read())
            {
                iitemtt = dr393["ItemName"].ToString();

                if (!this.Cname.Items.Contains(iitemtt))
                {
                    this.Cname.Items.Add(iitemtt);
                    comboBox2.Items.Add(iitemtt);
                }

            }
            dr39.Close();
            items1 = new string[Cname.Items.Count];
            Cname.Items.CopyTo(items1, 0);
            items2 = new string[comboBox2.Items.Count];
            comboBox2.Items.CopyTo(items2, 0);
            comboBox1.Focus();
            dateTimePicker1.Checked=false;
        }

        public void Invoucein_Load(object sender, EventArgs e)
        {
            startin();
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
                       
                        if ((dataGridView1.CurrentRow.Index >= 0 && dataGridView1.Rows[i].Cells[1].Value != null && dataGridView1.Rows[i].Cells[2].Value != null))
                        {
                                z = i;
                            float a = float.Parse(dataGridView1.Rows[i].Cells[1].Value.ToString());
                            float b = float.Parse(dataGridView1.Rows[i].Cells[2].Value.ToString());
                            float c = a * b;
                            dataGridView1.Rows[i].Cells[4].Value = c;
                            textBox3.Text = (float.Parse(textBox3.Text) + float.Parse(dataGridView1.Rows[i].Cells[4].Value.ToString())).ToString();
                        }
                        }
                        textBox8.Text = box;
                        textBox8_Leave(sender, e);
                        if (checkBox1.Checked)
                        { textBox4.Text = ((float.Parse(textBox3.Text)) - (float.Parse(textBox5.Text))).ToString(); }
                    }
            }
        }
            catch
            {
                MessageBox.Show("الرجاء ادخال ارقام صحيحة في الجدول");
                dataGridView1.Rows[z].Cells[1].Value = 0.0;
                dataGridView1.Rows[z].Cells[2].Value = 0.0;
                dataGridView1.Rows[z].Cells[4].Value = 0.0;
                textBox8.Text = box;
                textBox8_Leave(sender, e);
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

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox2.Text = theDate1.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            startin();
        }
        
        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
              if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
              {
                  string item = comboBox1.Text;
                  string[] filteredItems = items.Where(x => x.Contains(item)).ToArray();
                  comboBox1.Items.Clear();
                  comboBox1.Items.Add(item);
                  comboBox1.Items.AddRange(filteredItems);
                comboBox1.DropDownStyle = ComboBoxStyle.DropDown;
                comboBox1.AutoCompleteMode = AutoCompleteMode.Suggest;
                comboBox1.DroppedDown = true;
                comboBox1.SelectionStart = item.Length;
                  comboBox1.SelectionLength = 0;

                comboBox1.Cursor = Cursor.Current;
            }
        }
                
        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCellAddress.X == 0 && dataGridView1.CurrentRow.Cells[0].Value != null)
            {
                string itid = "";
                string itq = "";
                SqlConnection conn31 = new SqlConnection(src);
                conn31.Open();
                SqlCommand cmd31 = new SqlCommand("select * from Inventory WHERE Item = @Name", conn31);
                cmd31.Parameters.AddWithValue("@Name", dataGridView1.CurrentRow.Cells[0].Value.ToString());
                SqlDataReader dr81 = cmd31.ExecuteReader();
                while (dr81.Read())
                {
                    itid = dr81["MinQ"].ToString();
                    itq = dr81["Notes"].ToString();
                }
                dr81.Close();
                this.dataGridView1.CurrentRow.HeaderCell.Value = (dataGridView1.CurrentCellAddress.Y + 1).ToString();
                dataGridView1.CurrentRow.Cells[3].Value = itid;
                dataGridView1.CurrentRow.Cells[5].Value = itq;
            }

        }

        private void comboBox2_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Down && e.KeyCode != Keys.Up && e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Back && e.KeyCode != Keys.Enter && e.KeyCode != Keys.Delete)
            {
                string item2 = comboBox2.Text;
                string[] filteredItems2 = items2.Where(x => x.Contains(item2)).ToArray();
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
    }
}

