using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Globalization;

namespace Laundry_System
{
    public partial class Cashier_Menu : Form
    {
        string id;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        DBConnection dbc = new DBConnection();
        Security sc;
        string price;
        int qty;
        string clt;

        
        public Cashier_Menu(Security st)
        {
            InitializeComponent();
            label3.Text = DateTime.Now.ToLongDateString();
            this.KeyPreview = true;
            con = new SqlConnection(dbc.myconnection());
            sc = st;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }
        public void GetTransNo()
        {
            try
            {
                int count;
                string transno;
                string sdate = DateTime.Now.ToString("yyyyMMdd");
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 1 Transaction_Number from Cart where Transaction_Number like '" + sdate + "%' order by ID desc";
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    transno = dr[0].ToString();
                    count = int.Parse(transno.Substring(8, 4));
                    label2.Text = sdate + (count + 1);
                }
                else
                {
                    transno = sdate + "1001";
                    label2.Text = transno;
                }
                dr.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>0)
            {
                return;
            }
            GetTransNo();
            metroTextBox1.Enabled = true;
            metroTextBox1.Focus();
        }

        private void Cashier_Menu_Load(object sender, EventArgs e)
        {
            //FormBorderStyle = FormBorderStyle.None;
            //WindowState = FormWindowState.Maximized;
            timer1.Start();
            
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
        public void loadcart()
        {
            try
            {

                Boolean Hasrecord = false;
                decimal total = 0;
                
                decimal discount = 0;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select c.ID,c.Cloth_ID, cl.Cloth_type,c.Price,c.Quantity,c.Service,  c.Charges,c.Discount,c.Total from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where Transaction_Number like '" + label2.Text + "' and Status like 'Pending'";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {

                    //dataGridView1.Rows.Add("[ + ]", "[ - ]");
                    total += decimal.Parse(dr["Total"].ToString());
                    discount += decimal.Parse(dr["Discount"].ToString());
                    Hasrecord = true;
                    //dataGridView1.Rows.Add(i,dr["Cloth_Type"].ToString(), dr["Price"].ToString(), dr["Quantity"].ToString(), dr["Discount"].ToString(), Double.Parse(dr["Total"].ToString()).ToString("#,##0.00"));

                }
                dr.Close();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
                //NumberFormatInfo nfi = new CultureInfo("en-US", false).NumberFormat;
                //CultureInfo us = new CultureInfo("en-US");
                //MessageBox.Show(total.ToString());

                label15.Text = total.ToString("#,##0.00");
                label8.Text = discount.ToString("#,##0.00");
                
                getcarttotal();
                if(Hasrecord==true)
                {
                    button4.Enabled = true;
                    button3.Enabled = true;
                    button5.Enabled = true;
                    button9.Enabled = true;

                }
                else
                {
                    button4.Enabled = false;
                    button3.Enabled = false;
                    button5.Enabled = false;
                    button9.Enabled = false;
                }
                //MessageBox.Show(label7.Text);
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void metroTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            
        }
        private void AddToCart(string _Clothid, double _price, int _qty)
        {
            string id = "";
            bool found = false;
            int cart_qty;
            
            con.Open();
            SqlCommand cmd1 = con.CreateCommand();
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "select *from Cart where Transaction_Number='" + label2.Text + "'and Cloth_ID='" + _Clothid + "'";
            SqlDataReader dr;
            dr = cmd1.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                found = true;
                id = dr["ID"].ToString();
                cart_qty = int.Parse(dr["Quantity"].ToString());
                
            }

            else
            {
                found = false;
            }
            dr.Close();
            cmd1.ExecuteNonQuery();
            con.Close();


            if (found == true)
            {

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Cart set Quantity=(Quantity + " + _qty + ") where ID='" + id + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                metroTextBox1.SelectionStart=0;
                metroTextBox1.SelectionLength = metroTextBox1.Text.Length;


                loadcart();
                //this.Dispose();
            }
            else
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Cart(Transaction_Number,Cloth_ID,Price,Quantity,S_Date,Cashier)values('" + label2.Text + "','" + _Clothid + "','" + _price + "','" + _qty + "','" + DateTime.Now + "','" + lbluser.Text + "')";
                cmd.ExecuteNonQuery();
                con.Close();
                metroTextBox1.SelectionStart = 0;   
                metroTextBox1.SelectionLength = metroTextBox1.Text.Length;


                loadcart();
                //this.Dispose();
            }
        }
        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            //WindowState = FormWindowState.Maximized;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label2.Text == "0000000000000")
            {
                return;
            }
            LookUp l = new LookUp(this);
            l.load();
            l.ShowDialog();
        }
        public void getcarttotal()
        {
            
            double discount = Double.Parse(label8.Text);
            double sales = Double.Parse(label15.Text);
            double vat = sales*dbc.getvat();
            double vatable = sales-vat;
            label5.Text = sales.ToString("#,##0.00"); 
            label10.Text = vat.ToString("#,##0.00");
            label12.Text = vatable.ToString("#,##0.00");  
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Discount d = new Discount(this);
            d.label5.Text = id;
            d.textBox1.Text = price;
            d.Show();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //checkBox1.Enabled = true;
            //checkBox2.Enabled = true;
            //checkBox3.Enabled = true;
           
            //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("REMOVE THIS ITEM?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //int prodid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Cart where ID='" + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    loadcart();
                    MessageBox.Show("ITEM REMOVED SUCCESSFULLY.");
                }
                
            }
            else if (colname == "colsub")
            {
                int i = 0;
                con.Open();
                cmd = new SqlCommand("Select sum(Quantity) as Quantity from Cart where Cloth_ID like '" + dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() + "' and Transaction_Number like'" + label2.Text + "'", con);
                i = int.Parse(cmd.ExecuteScalar().ToString());
                con.Close();

                //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
                if (i > 1)
                {
                    con.Open();
                    cmd = new SqlCommand("update Cart set Quantity=Quantity - " + int.Parse(metroTextBox2.Text) + "where Transaction_Number like '" + label2.Text + "'and Cloth_ID like '" + dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadcart();
                }
                 else
                {
                    MessageBox.Show("REMAINING ITEM ON CART IS " + i + " !", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                

            }
            else if (colname == "coladd")
            {
                int i = 0;
                
                    con.Open();
                    cmd = new SqlCommand("update Cart set Quantity=Quantity + " + int.Parse(metroTextBox2.Text) + "where Transaction_Number like '" + label2.Text + "'and Cloth_ID like '" + dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadcart();
                
               
            }
            else if(colname == "S_Reset")
            {
                con.Open();
                cmd = new SqlCommand("update Cart set Service ='' ,Charges='0.00' where ID like '" + dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString() + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                loadcart();
            }
        }

       

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            int i = dataGridView1.CurrentRow.Index;
            id = dataGridView1[5, i].Value.ToString();
            price = dataGridView1[13, i].Value.ToString();
            clt= dataGridView1[6, i].Value.ToString();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Settle s = new Settle(this);
            s.textBox1.Text = label5.Text;

            s.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Orders_Done o = new Orders_Done();
            //o.dateTimePicker1.Enabled = false;
            //o.dateTimePicker2.Enabled = false;
            o.suser = lbluser.Text;
            o.comboBox1.Enabled = false;
            o.comboBox2.Enabled = false;
            o.comboBox1.Text = lbluser.Text;
            o.comboBox2.Text = lbluser.Text;
            o.ShowDialog();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if(dataGridView1.Rows.Count>0)
            {
                MessageBox.Show("UNABLE TO LOGOUT.PLEASE CANCEL THE TRANSACTION.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(MessageBox.Show("LOGOUT FROM APPLICATION?","Logout",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                this.Hide();
                Security s = new Security();
                s.ShowDialog();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Change_Password f = new Change_Password(this);
            f.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("REMOVE ALL ITEMS FROM CART?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
            {
                con.Open();
                cmd = new SqlCommand("delete from Cart where Transaction_Number like '" + label2.Text + "'", con);
                cmd.ExecuteNonQuery();
                con.Close();
                loadcart();
                MessageBox.Show("ALL ITEMS REMOVED FROM CART", "Cleared Cart", MessageBoxButtons.OK, MessageBoxIcon.Information);
               
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                if (metroTextBox1.Text == string.Empty)
                {
                    return;
                }
                else
                {
                    string _Cltcode;
                    double _price;
                    int _qty;

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select *from Clothes_Details where Barcode Like '" + metroTextBox1.Text + "'";
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        //qty = int.Parse(dr["Quantity"].ToString());
                        //Quantity q = new Quantity(this);
                        //q.ClothDetails(dr["Cloth_ID"].ToString(), Double.Parse(dr["Price"].ToString()), label2.Text);
                        _Cltcode = dr["Cloth_ID"].ToString();
                        _price = Double.Parse(dr["Price"].ToString());
                        _qty = int.Parse(metroTextBox2.Text);
                        dr.Close();
                        con.Close();

                        AddToCart(_Cltcode, _price, _qty);
                        //q.Show();

                    }
                    else
                    {
                        dr.Close();
                        con.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void panel1_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label9_Click(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void lblname_Click(object sender, EventArgs e)
        {

        }

        private void lbluser_Click(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint_1(object sender, PaintEventArgs e)
        {

        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void metroTextBox2_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void button21_Click(object sender, EventArgs e)
        {

        }

        private void button22_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void Cashier_Menu_Resize(object sender, EventArgs e)
        {
            int intx = Screen.PrimaryScreen.Bounds.Width;
            int inty = Screen.PrimaryScreen.Bounds.Height;
            this.Width = intx;
            this.Height = inty - 40;
            this.Top = 0;
            this.Left = 0;
        }

        private void timer1_Tick_1(object sender, EventArgs e)
        {
            DateTime datetime = DateTime.Now;
            this.label7.Text = datetime.ToString("hh:mm:ss tt");
            this.label16.Text = datetime.ToLongDateString();
        }
       

       
       

        

        private void button9_Click_1(object sender, EventArgs e)
        {
            Services sr = new Services(this);
            sr.label2.Text = id;
            sr.Show();

        }

    }
}
