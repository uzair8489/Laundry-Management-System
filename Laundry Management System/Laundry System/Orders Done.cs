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


namespace Laundry_System
{
    public partial class Orders_Done : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        //Cashier_Menu fp;
        public string suser;
        public string auser;
        public Orders_Done()
        {
            InitializeComponent();
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            dateTimePicker3.Value = DateTime.Now;
            dateTimePicker4.Value = DateTime.Now;
            LoadRecord();
            loadCashier();
            LoadNotPickedRecord();
            //fp = fm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRecord()
        {
            //dataGridView1.Rows.Clear();
            double _total = 0;
            
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (comboBox1.Text == "All Cashiers")
            { cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Picked' and S_Date between '" + dateTimePicker1.Value + "'and '" + dateTimePicker2.Value + "'"; }
            else
            {
                cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,C.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Picked' and S_Date between '" + dateTimePicker1.Value + "'and '" + dateTimePicker2.Value + "' and Cashier = '" + comboBox1.Text + "'";
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                _total+= double.Parse(dr["Total"].ToString());
            }
            dr.Close();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            label2.Text = _total.ToString("#,##0.00");
        }
        public void LoadNotPickedRecord()
        {
            //dataGridView1.Rows.Clear();
            double _total = 0;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (comboBox2.Text == "All Cashiers")
            { cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Not Picked' and S_Date between '" + dateTimePicker4.Value + "'and '" + dateTimePicker3.Value + "'"; }
            else
            {
                cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,C.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Not Picked' and S_Date between '" + dateTimePicker4.Value + "'and '" + dateTimePicker3.Value + "' and Cashier = '" + comboBox2.Text + "'";
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                _total += double.Parse(dr["Total"].ToString());
            }
            dr.Close();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
            label5.Text = _total.ToString("#,##0.00");
        }

        private void Orders_Done_Load(object sender, EventArgs e)
        {
            
            
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void loadCashier()
        {
            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            comboBox1.Items.Add("All Cashiers");
            comboBox2.Items.Add("All Cashiers");
            con.Open();
            cmd = new SqlCommand("select *from Users where Role like 'Cashier'", con);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr["Username"].ToString());
                comboBox2.Items.Add(dr["Username"].ToString());
            }
            dr.Close();
            con.Close();
        }
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                metroTextBox1.Enabled = true;
            }
            LoadRecord();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colname=="Cancel")
            {
                Cancel_Details f = new Cancel_Details(this);
                f.textID.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.texttrans.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.textcltID.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.textclttype.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.textprice.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.textqty.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.textdisc.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
               
                f.textcancel.Text = suser;
                //f.textcancel.Text = auser;
                f.ShowDialog();
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Done_Orders_Report d = new Done_Orders_Report(this);
            d.LoadReport();
            d.ShowDialog();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            LoadNotPickedRecord();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            LoadNotPickedRecord();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text != "")
            {
                metroTextBox2.Enabled = true;

            }
            LoadNotPickedRecord();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView2.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView2.Columns[e.ColumnIndex].Name;
            if (colname == "Cancel")
            {
                Cancel_Details f = new Cancel_Details(this);
                f.textID.Text = dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString();
                f.texttrans.Text = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.textcltID.Text = dataGridView2.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.textclttype.Text = dataGridView2.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.textprice.Text = dataGridView2.Rows[e.RowIndex].Cells[8].Value.ToString();
                f.textqty.Text = dataGridView2.Rows[e.RowIndex].Cells[9].Value.ToString();
                f.txtservice.Text = dataGridView2.Rows[e.RowIndex].Cells[10].Value.ToString();
                f.txtcharges.Text = dataGridView2.Rows[e.RowIndex].Cells[11].Value.ToString();
                f.textdisc.Text = dataGridView2.Rows[e.RowIndex].Cells[12].Value.ToString();
                f.texttotal.Text = dataGridView2.Rows[e.RowIndex].Cells[13].Value.ToString();
                f.texttotal.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                f.textoreturn.Text = dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                f.textcancel.Text = suser;
                //f.textcancel.Text = auser;
                f.ShowDialog();
            }
            if(colname=="Picked")
            {
                if(MessageBox.Show("IS THIS ORDER PICKED?CLICK YES","Update",MessageBoxButtons.YesNo)==DialogResult.Yes)
                {
                    int prodid = Convert.ToInt32(dataGridView2.Rows[e.RowIndex].Cells[2].Value);
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Cart set Picked='Picked' where ID='" + prodid + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    LoadNotPickedRecord();
                    LoadRecord();
                    MessageBox.Show("Record Updated Successfully");
                }
            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            double _total = 0;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (comboBox1.Text == "All Cashiers")
            { cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Picked' and S_Date between '" + dateTimePicker1.Value + "'and '" + dateTimePicker2.Value + "'and Transaction_Number like '"+metroTextBox1.Text+"%'"; }
            else
            {
                cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,C.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Picked' and S_Date between '" + dateTimePicker1.Value + "'and '" + dateTimePicker2.Value + "' and Cashier = '" + comboBox1.Text + "'and Transaction_Number like '" + metroTextBox1.Text + "%'";
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                _total += double.Parse(dr["Total"].ToString());
            }
            dr.Close();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
            label2.Text = _total.ToString("#,##0.00");
        }

        private void metroTextBox2_TextChanged(object sender, EventArgs e)
        {
            //dataGridView1.Rows.Clear();
            double _total = 0;

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            if (comboBox2.Text == "All Cashiers")
            { cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Not Picked' and S_Date between '" + dateTimePicker4.Value + "'and '" + dateTimePicker3.Value + "'and Transaction_Number Like '"+metroTextBox2.Text+"%'"; }
            else
            {
                cmd.CommandText = "select c.ID,c.Transaction_Number,c.Customer_Name,c.Customer_Contact,c.Cloth_ID,cl.Cloth_Type,c.Price,c.Quantity,C.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and c.Picked like 'Not Picked' and S_Date between '" + dateTimePicker4.Value + "'and '" + dateTimePicker3.Value + "' and Cashier = '" + comboBox2.Text + "'and Transaction_Number Like '" + metroTextBox2.Text + "%'";
            }
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                _total += double.Parse(dr["Total"].ToString());
            }
            dr.Close();
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
            label5.Text = _total.ToString("#,##0.00");
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView2.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void Orders_Done_Resize(object sender, EventArgs e)
        {
            int intx = Screen.PrimaryScreen.Bounds.Width;
            int inty = Screen.PrimaryScreen.Bounds.Height;
            this.Width = intx;
            this.Height = inty - 40;
            this.Top = 0;
            this.Left = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Done_Orders_Report d = new Done_Orders_Report(this);
            d.LoadNotPicked();
            d.ShowDialog();
        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
