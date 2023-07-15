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
    public partial class Vendors_List : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        public Vendors_List()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }
        public void LoadRecord()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Vendors";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            
        }



        private void metroTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Vendors where Vendor like '" + metroTextBox1.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "Edit")
            {
                Vendors f = new Vendors(this);
                f.label8.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                f.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                f.textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                f.textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                f.textBox6.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                f.textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                f.textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                f.button1.Enabled = false;
                f.button2.Enabled = true;
                f.ShowDialog();

            }
            else if (colname == "Delete")
            {
                if (MessageBox.Show("ARE YOU SURE TO DELETE THIS? CLICK YES", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("delete from Vendors where ID='" + dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadRecord();
                    MessageBox.Show("RECORD DELETED SUCCESSFULLY.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Vendors f = new Vendors(this);
            f.button1.Enabled = true;
            f.button2.Enabled = false;
            f.ShowDialog();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty)
            {
                Inventory_Report s = new Inventory_Report();
                s.LoadVendors("select *from Vendors","List Of ALL Vendors");
                s.ShowDialog();
            }
            else
            {
                Inventory_Report f = new Inventory_Report();
                f.LoadVendors("select *from Vendors where Vendor like '" + metroTextBox1.Text + "%'","List Of Vendors Name Starting With : '"+metroTextBox1.Text+"'");
                f.ShowDialog();
            }


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
