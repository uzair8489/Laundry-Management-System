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
    public partial class Customer_Details : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        string pass;

        public Customer_Details()
        {
            InitializeComponent();
            load();

        }

        

        public void load()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Customer_Details";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Customer_Module c = new Customer_Module(this);
            c.button7.Enabled = true;
            c.button6.Enabled = false;
            
            c.Show();
        }

        private void Customer_Details_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Customer_Details where Name like'" + metroTextBox1.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("Are You Sure To Delete The Selected Data?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Customer_Details where Customer_ID='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    load();
                    MessageBox.Show("Record Deleted Successfully");
                }
            }
            if (colname == "Edit")
            {

                Customer_Module cl = new Customer_Module(this);

                cl.textcid.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                cl.textcname.Text = dataGridView1[4, e.RowIndex].Value.ToString();
                cl.textcphone.Text = dataGridView1[5, e.RowIndex].Value.ToString();
                cl.textemail.Text = dataGridView1[6, e.RowIndex].Value.ToString();
                cl.textaddress.Text = dataGridView1[7, e.RowIndex].Value.ToString();
                cl.button7.Enabled = false;
                cl.button6.Enabled = true;
                cl.Show();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty)
            {
                Inventory_Report f = new Inventory_Report();
                f.loadCustomer("select *from Customer_Details","List Of ALL Regular Customers");
                f.ShowDialog();
            }
            else
            {
                Inventory_Report f = new Inventory_Report();
                f.loadCustomer("select *from Customer_Details where Name like '"+metroTextBox1.Text+"%'","List Of Regular Customers Name Starting With : '"+metroTextBox1.Text+"'");
                f.ShowDialog();
            }
        }
    }
}
