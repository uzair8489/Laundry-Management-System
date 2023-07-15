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
    public partial class Stock_In : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Admin_Portal f;
        public Stock_In(Admin_Portal fm)
        {
            InitializeComponent();
            f = fm;
            LoadVendor();
            loadStockInHistory();
            
        }

        /*public void load()
        {
            dataGridView1.Rows.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Product_Code,Product_Description,Quantity from Products where Product_Description Like '%" + metroTextBox1.Text + "%' order by Product_Description";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }*/
        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Stock_In_Load(object sender, EventArgs e)
        {
            textBox2.Text = f.lbluser.Text;
            //load();
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        /*private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
           // dataGridView1.Rows[e.RowIndex].Cells[1].Value
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Product_Code,Product_Description,Quantity from Products where Product_Description Like '%" + metroTextBox1.Text + "%' order by Product_Description";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }*/

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }
        public void loadStockIn()
        {
            /// dataGridView2.Rows.Clear();
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from View_Stock_In where Reference_Number='"+textBox1.Text+"' and Status like 'Pending'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView2.CurrentCell.ColumnIndex.ToString());
            if (e.ColumnIndex == 1)
            {
                
                if (MessageBox.Show("REMOVE THIS ITEM??", "Remove", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //int prodid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Stock_In where ID='" + dataGridView2.Rows[e.RowIndex].Cells[2].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    loadStockIn();
                    MessageBox.Show("ITEM HAS BEEN REMOVED", "Removed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                
            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView2.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Search_Product_Stock_In s = new Search_Product_Stock_In(this);
            s.load();
            s.Show();
        }
        public void clear()
        {
            textBox1.Clear();
            //textBox2.Clear();
            dateTimePicker1.Value = DateTime.Now;
        }
        private void loadStockInHistory()
        {
            
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from View_Stock_In where cast(Stock_In_Date as date) between'" + dateTimePicker2.Value.ToShortDateString() + "' and '" + dateTimePicker3.Value.ToShortDateString() + "' and Status like 'Done'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            try
            {
                if(dataGridView2.Rows.Count>0)
                {
                    if (MessageBox.Show("ARE YOU SURE TO SAVE THIS RECORD", "Save Record", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        for (int i = 0; i < dataGridView2.Rows.Count; i++)
                        {
                            con.Open();

                            SqlCommand cmd = con.CreateCommand();
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "update Products set Quantity=Quantity +" + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + " where Product_Code='" + dataGridView2.Rows[i].Cells[4].Value.ToString() + "'";
                            cmd.ExecuteNonQuery();
                            con.Close();

                            con.Open();

                            SqlCommand cmd1 = con.CreateCommand();
                            cmd1.CommandType = CommandType.Text;
                            cmd1.CommandText = "update Stock_In set Quantity=Quantity+" + int.Parse(dataGridView2.Rows[i].Cells[6].Value.ToString()) + ", Status='Done' where ID='" + dataGridView2.Rows[i].Cells[2].Value.ToString() + "'";
                            cmd1.ExecuteNonQuery();
                            con.Close();
                            MessageBox.Show("RECORD SAVED SUCCESSFULLY", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        }
                        clear();
                        loadStockIn();
                        loadStockInHistory();
                    }
                }

            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            loadStockInHistory();
        }

        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
        public void LoadVendor()
        {
            comboBox1.Items.Clear();
            con.Open();
            cmd = new SqlCommand("Select *from Vendors", con);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                comboBox1.Items.Add(dr["Vendor"].ToString());
            }
            dr.Close();
            con.Close();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            cmd = new SqlCommand("select *from Vendors where Vendor like '" + comboBox1.Text + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                label10.Text = dr["ID"].ToString();
                textBox3.Text = dr["Person_Contact"].ToString();
                textBox4.Text = dr["Address"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Random rmd = new Random();
            textBox1.Clear();
            textBox1.Text += rmd.Next();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            loadStockInHistory();
           
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            loadStockInHistory();
        }
    }
}
