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
    public partial class Editemp : Form
    {
        public Editemp()
        {
            InitializeComponent();
        }

        private string src = Program.xsrc;
        int row1 = 0;
        public int rowindex1;
        private bool select = false;
        string id = "";
        string user = "";
        string n = "";
        public bool u;
        public bool empty;
        public string ww = "";
        public string page = "";

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            { panel1.Enabled = true; }
            else
            {
                panel1.Enabled = false;
                textBox3.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
            }
        }
        
        public void Editemp_Load(object sender, EventArgs e)
        {           
            u = false;
            panel1.Enabled = false;
            checkBox1.Checked = false;
            textBox3.Text = "";
            textBox5.Text = "";
            textBox6.Text = "";
            textBox1.Text = "";
            textBox11.Text = "";
            textBox2.Text = "";
            textBox4.Text = "";
            dateTimePicker1.Checked = false;
            empty = true;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null);
            }
            textBox1.Focus();
        }
        
        public void button3_Click(object sender, EventArgs e)
        {            
            if (select)
            {
                SqlConnection conn3 = new SqlConnection(src);
                conn3.Open();
                n = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                SqlCommand cmd3 = new SqlCommand("select * from Employee WHERE EmployeeName = @Name", conn3);
                cmd3.Parameters.AddWithValue("@Name", n);
                SqlDataReader dr8 = cmd3.ExecuteReader();
                while (dr8.Read())
                {
                    id = dr8["Id"].ToString();
                    user = dr8["Username"].ToString();
                }
                dr8.Close();
                textBox1.Text = dataGridView1.Rows[rowindex1].Cells[2].Value.ToString();
                textBox2.Text = dataGridView1.Rows[rowindex1].Cells[3].Value.ToString();
                textBox4.Text = dataGridView1.Rows[rowindex1].Cells[1].Value.ToString();
                textBox11.Text = dataGridView1.Rows[rowindex1].Cells[4].Value.ToString();                
            }
        }

        public void found(bool ok, string name, string wh)
        {
            if (wh == "DELEMP")
            {
                if (ok)
                {
                    if ((MessageBox.Show("هل انت متأكد من حذف معلومات الموظف", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
                    {
                        for (int i = 0; i < row1; i++)
                        {
                            string t = dataGridView1.Rows[i].Cells[0].Value.ToString();
                            if (t == "True")
                            {
                                string q1 = Program.xsrc;
                                SqlConnection cn1 = new SqlConnection(q1);
                                SqlCommand cmd1 = new SqlCommand("DELETE FROM [Employee] WHERE EmployeeName = @Box1", cn1);
                                cmd1.Parameters.AddWithValue("@Box1", dataGridView1.Rows[i].Cells[2].Value.ToString());
                                cn1.Open();
                                SqlConnection con = new SqlConnection(src);
                                SqlCommand cmd = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                                cmd.Parameters.AddWithValue("@textBox1", name);
                                cmd.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                                cmd.Parameters.AddWithValue("@textBox3", " لقد تم حذف الموظف \"  السيد  " + dataGridView1.Rows[i].Cells[2].Value.ToString() + "  \" وشكرا");
                                cmd.Parameters.AddWithValue("@textBox4", "DEL");
                                con.Open();
                                SqlDataReader dr2 = cmd.ExecuteReader();
                                SqlDataReader dr1 = cmd1.ExecuteReader();
                                //cn1.Close(); MessageBox.Show("2");
                                // Editemp_Load(sender, e);
                                con.Close();
                            }
                        }

                        MessageBox.Show("لقد تمت حذف معلومات الموطف بنجاح ");
                        empty = true;
                        dataGridView1.Rows.Clear();
                        row1 = 0;
                        SqlConnection conn4 = new SqlConnection(src);
                        conn4.Open();
                        SqlCommand cmd34 = new SqlCommand("select * from Employee", conn4);
                        SqlDataReader dr34 = cmd34.ExecuteReader();
                        while (dr34.Read())
                        {
                            dataGridView1.Rows.Insert(row1, false, dr34["Date"].ToString(), dr34["EmployeeName"].ToString(), dr34["Salary"].ToString(), dr34["Notes"].ToString());
                            row1++;
                        }
                        dr34.Close();
                        this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Ascending);
                        for (int ro = 0; ro < 25; ro++)
                        {
                            dataGridView1.Rows.Add(false, null, null, null, null);
                        }
                        for (int y = 0; y < row1; y++)
                        {
                            this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                        }
                        empty = false;
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox11.Clear();
                        textBox6.Clear();
                        textBox5.Clear();
                        conn4.Close();
                        textBox1.Focus();
                        dateTimePicker1.Checked = false;
                    }
                }
            }
            else if (wh == "UPEMP")
            {
                if (ok)
                {
                    if (u)
                    {

                        SqlConnection conn = new SqlConnection(src);
                        SqlCommand cmdn = new SqlCommand("UPDATE [Employee] SET EmployeeName=@textbox1, Salary=@textbox2, Date=@textbox4, Username=@textbox3, Password=@textbox5, Notes=@textbox11 WHERE Id = '" + id + "'", conn);
                        cmdn.Parameters.AddWithValue("@textBox1", textBox1.Text);
                        cmdn.Parameters.AddWithValue("@textBox5", textBox5.Text);
                        cmdn.Parameters.AddWithValue("@textBox4", textBox4.Text);
                        cmdn.Parameters.AddWithValue("@textBox2", textBox2.Text);
                        cmdn.Parameters.AddWithValue("@textBox3", textBox3.Text);
                        cmdn.Parameters.AddWithValue("@textBox11", textBox11.Text);
                        conn.Open();
                        //SqlDataReader dr1 = cmdn.ExecuteReader();
                        if (MessageBox.Show("هل انت متأكد من تعديل معلومات الموظف", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)

                        {
                            SqlConnection con = new SqlConnection(src);
                            SqlCommand cmd8 = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                            cmd8.Parameters.AddWithValue("@textBox1", name);
                            cmd8.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                            cmd8.Parameters.AddWithValue("@textBox3", " لقد تم تعديل معلومات الموظف \"  السيد  " + n + "  \" وشكرا");
                            cmd8.Parameters.AddWithValue("@textBox4", "UP");
                            con.Open();
                            SqlDataReader dr2 = cmdn.ExecuteReader();
                            SqlDataReader dr8 = cmd8.ExecuteReader();
                            MessageBox.Show("لقد تمت تعديل معلومات الموظف بنجاح ");
                            empty = true;
                            dataGridView1.Rows.Clear();
                            int row1 = 0;
                            SqlConnection conn3 = new SqlConnection(src);
                            conn3.Open();
                            SqlCommand cmd3 = new SqlCommand("select * from Employee", conn3);
                            SqlDataReader dr3 = cmd3.ExecuteReader();
                            while (dr3.Read())
                            {
                                dataGridView1.Rows.Insert(row1, false, dr3["Date"].ToString(), dr3["EmployeeName"].ToString(), dr3["Salary"].ToString(), dr3["Notes"].ToString());
                                row1++;
                            }
                            dr3.Close();
                            this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Ascending);
                            for (int ro = 0; ro < 25; ro++)
                            {
                                dataGridView1.Rows.Add(false, null, null, null, null);
                            }
                            for (int y = 0; y < row1; y++)
                            {
                                this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                            }
                            textBox1.Clear();
                            textBox2.Clear();
                            textBox3.Clear();
                            textBox4.Clear();
                            textBox11.Clear();
                            textBox6.Clear();
                            textBox5.Clear();
                            conn3.Close();
                            textBox1.Focus();
                            dateTimePicker1.Checked = false;
                            empty = false;
                            //this.Hide();
                        }
                    }
                    else if (MessageBox.Show("هل انت متأكد من تعديل معلومات الموظف", "الرجاء التأكيد", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        SqlConnection ccon = new SqlConnection(src);
                        SqlCommand cmd = new SqlCommand("UPDATE [Employee] SET EmployeeName=@textbox1, Salary=@textbox2, Date=@textbox4, Notes=@textbox11 WHERE Id = '" + id + "'", ccon);
                        cmd.Parameters.AddWithValue("@textBox1", textBox1.Text);
                        cmd.Parameters.AddWithValue("@textBox4", textBox4.Text);
                        cmd.Parameters.AddWithValue("@textBox2", textBox2.Text);
                        cmd.Parameters.AddWithValue("@textBox11", textBox11.Text);
                        ccon.Open();
                        SqlConnection con = new SqlConnection(src);
                        SqlCommand cmd8 = new SqlCommand("INSERT INTO [Ref](Name,Date,Action,Kind)VALUES (@textBox1,@textBox2,@textBox3,@textBox4)", con);
                        cmd8.Parameters.AddWithValue("@textBox1", name);
                        cmd8.Parameters.AddWithValue("@textBox2", DateTime.Now.ToString());
                        cmd8.Parameters.AddWithValue("@textBox3", " لقد تم تعديل معلومات الموظف \"  السيد  " + textBox1.Text + "  الراتب  "+textBox2.Text);
                        cmd8.Parameters.AddWithValue("@textBox4", "UP");
                        con.Open();
                        SqlDataReader dr1 = cmd.ExecuteReader();
                        SqlDataReader dr8 = cmd8.ExecuteReader();
                        MessageBox.Show("لقد تمت تعديل معلومات الموظف بنجاح");
                        empty = true;
                        dataGridView1.Rows.Clear();
                        int row1 = 0;
                        SqlConnection conn33 = new SqlConnection(src);
                        conn33.Open();
                        SqlCommand cmd33 = new SqlCommand("select * from Employee", conn33);
                        SqlDataReader dr4 = cmd33.ExecuteReader();
                        while (dr4.Read())
                        {
                            dataGridView1.Rows.Insert(row1, false, dr4["Date"].ToString(), dr4["EmployeeName"].ToString(), dr4["Salary"].ToString(), dr4["Notes"].ToString());
                            row1++;
                        }
                        dr4.Close();
                        this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Ascending);
                        for (int ro = 0; ro < 25; ro++)
                        {
                            dataGridView1.Rows.Add(false, null, null, null, null);
                        }
                        for (int y = 0; y < row1; y++)
                        {
                            this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                        }
                        empty = false;
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        textBox11.Clear();
                        textBox6.Clear();
                        textBox5.Clear();
                        conn33.Close();
                        textBox1.Focus();
                        dateTimePicker1.Checked = false;
                    }
                }
            }
        }
          
        public void button4_Click(object sender, EventArgs e)
        {
            if (select)
            {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                ww = "DELEMP";
                page = "Editemp";
                Program.mysignin.which(ww, page);
            }
        }

        public void button2_Click(object sender, EventArgs e)
        {
    if (textBox1.Text == "") { MessageBox.Show("الرجاء ادخال اسم الموطف"); }
    else if (textBox2.Text == "") { MessageBox.Show("الرجاء ادخال راتب الموطف"); }
    else if (float.Parse(textBox2.Text) < 0) { MessageBox.Show("لا يجوز ان يكون الراتب اقل من صفر"); }
    else if (textBox4.Text == "") { MessageBox.Show("الرجاء ادخال التاريخ"); }
    else if (checkBox1.Checked)
    {
        if (user == "")
        {
            if (textBox3.Text == "") { MessageBox.Show("الرجاء ادخال اسم المستخدم"); }
            else if (textBox5.Text == textBox6.Text && textBox5.Text != "")
                    {
                        SqlConnection con1n = new SqlConnection(src);
                        con1n.Open();
                        bool found = false; // true when user name used befor 
                SqlCommand cmd1 = new SqlCommand("select * from Employee", con1n);
                SqlDataReader dr = cmd1.ExecuteReader();
                while (dr.Read())
                {
                    if (dr["Username"].ToString() == textBox3.Text)
                    {
                        found = true;
                    }
                }
                if (found)
                {
                    MessageBox.Show("الرجاء تغيير اسم المستخدم");
                    textBox5.Text = "";
                    textBox6.Text = "";
                    textBox3.Text = "";
                    dr.Close();
                    con1n.Close();
                }
                else
                {
                    Program.mysignin.Show();
                    Program.mysignin.Signin_Load(sender, e);
                    ww = "UPEMP";
                    page = "Editemp";
                            u = true;
                    Program.mysignin.which(ww, page);
                }
            }
            else
            {
                MessageBox.Show("كلمة السر غير متطابقة");
                textBox5.Clear();
                textBox6.Clear();
            }
        }
        else
        {
            MessageBox.Show("لا يمكن تغيير اسم المستخدم او كلمة السر من هنا");
            textBox5.Text = "";
            textBox6.Text = "";
            textBox3.Text = "";
        }
    }
            else {
                Program.mysignin.Show();
                Program.mysignin.Signin_Load(sender, e);
                ww = "UPEMP";
                page = "Editemp";
                u = false;
                Program.mysignin.which(ww, page);
            }
                
        }
        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            string theDate1 = dateTimePicker1.Value.ToString("dd/MM/yyyy");
            textBox4.Text = theDate1.ToString();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox11.Clear();
            textBox6.Clear();
            textBox5.Clear();
            checkBox1.Checked = false;
            empty = true;
            dataGridView1.Rows.Clear();
            for (int ro = 0; ro < 25; ro++)
            {
                dataGridView1.Rows.Add(false, null, null, null, null);
            }
            textBox1.Focus();
            dateTimePicker1.Checked = false;
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Program.mysignin.Show();
            Program.mysignin.Signin_Load(sender, e);
            string ww = "EmpES";
            string page = "EmpES";
            Program.mysignin.which(ww, page);
        }

        public void EmpES(bool j)
        {
            if(j)
            {
                empty = true;
                dataGridView1.Rows.Clear();
                
                row1 = 0;
                SqlConnection conn3 = new SqlConnection(src);
                conn3.Open();
                SqlCommand cmd3 = new SqlCommand("select * from Employee", conn3);
                SqlDataReader dr3 = cmd3.ExecuteReader();
                while (dr3.Read())
                {
                    if (dr3["EmployeeName"].ToString() != "المبرمج")
                    {
                        dataGridView1.Rows.Insert(row1, false, dr3["Date"].ToString(), dr3["EmployeeName"].ToString(), dr3["Salary"].ToString(), dr3["Notes"].ToString());
                        row1++;
                    }
                }
                dr3.Close();
                this.dataGridView1.Sort(this.dataGridView1.Columns[2], ListSortDirection.Ascending);
                for (int ro = 0; ro < 25; ro++)
                {
                    dataGridView1.Rows.Add(false, null, null, null, null);
                }
                for (int y = 0; y < row1; y++)
                {
                    this.dataGridView1.Rows[y].HeaderCell.Value = (y + 1).ToString();
                }
                empty = false;
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

    }
}
