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
    public partial class Search_Product_Stock_In : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Stock_In slist;
        public Search_Product_Stock_In(Stock_In flist)
        {
            InitializeComponent();
            slist = flist;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void load()
        {
            //dataGridView1.Rows.Clear();
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select Product_Code,Product_Description,Quantity from Products where Product_Description Like '" + metroTextBox1.Text + "%' order by Product_Description";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            if (e.ColumnIndex == 1)
            {
                if (slist.textBox1.Text == string.Empty)
                {
                    MessageBox.Show("PLEASE ENTER REFERENCE #", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    
                    return;
                }
               
                if (MessageBox.Show("Add THIS ITEM?", "Add Item", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    //string refno = textBox1.Text.ToString();
                    //string pcode = ;

                    //string stby = textBox2.Text.ToString();
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Stock_In(Reference_Number,Product_Code,Stock_In_Date,Stock_In_By,Vendor_ID) values('" + slist.textBox1.Text.ToString() + "','" + dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString() + "','" + slist.dateTimePicker1.Value + "','" + slist.textBox2.Text.ToString() + "','"+slist.label10.Text+"')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    slist.loadStockIn();
                   

                }

            }
        }

        private void Search_Product_Stock_In_Load(object sender, EventArgs e)
        {
                        
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        //private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    load();
        //}

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            load();
        }
    }
}
