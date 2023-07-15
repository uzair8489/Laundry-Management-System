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
    public partial class Category_List : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public Category_List()
        {
            InitializeComponent();
        }
        public void load()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Category";
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
            Category_Module c = new Category_Module(this);
            c.button1.Enabled = true;
            c.button2.Enabled = false;
            c.Show();
        }

        private void Category_List_Load(object sender, EventArgs e)
        {
            load();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname=="Delete")
            {
                if (MessageBox.Show("Are You Sure To Delete The Selected Data?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    int prodid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Category where ID='" + prodid + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    load();
                    MessageBox.Show("Record Deleted Successfully");
                }

            }

            if (colname=="Edit")
            {

                Category_Module cl = new Category_Module(this);

                cl.textBox1.Text = dataGridView1[4, e.RowIndex].Value.ToString();
                cl.label3.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                cl.button1.Enabled = false;
                cl.button2.Enabled = true;
                cl.Show();

            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Category where Category like '" + metroTextBox1.Text + "%'";
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

        private void button3_Click(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty)
            {
                Inventory_Report r = new Inventory_Report();
                r.loadCategory("Select *from Category", "List Of All Categories");
                r.ShowDialog();
            }
            else
            {
                Inventory_Report r = new Inventory_Report();
                r.loadCategory("Select *from Category where Category like '" + metroTextBox1.Text + "%'", "List Of Categories Name Starting With : '" + metroTextBox1.Text + "'");
                r.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
