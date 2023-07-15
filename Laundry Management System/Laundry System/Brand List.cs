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
    public partial class Brand_List : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public Brand_List()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Brand_Module b = new Brand_Module(this);
            b.button1.Enabled = true;
            b.button2.Enabled = false;
            b.Show();
        }

        public void load()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Brands";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void Brand_List_Load(object sender, EventArgs e)
        {
            load();
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
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
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Brands where ID='" + prodid + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    load();
                    MessageBox.Show("Record Deleted Successfully", "Deleted");
                }

            }

            else if (colname == "Edit")
            {

                Brand_Module bl = new Brand_Module(this);

                bl.textBox1.Text = dataGridView1[4, e.RowIndex].Value.ToString();
                bl.label3.Text = dataGridView1[3, e.RowIndex].Value.ToString();
                bl.button1.Enabled = false;
                bl.button2.Enabled = true;
                bl.Show();

            }
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Brands where Brand like '" + metroTextBox1.Text + "%'";
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
                r.loadBrands("Select *from Brands","List Of ALL Brands");
                r.ShowDialog();
            }
            else
            {
                Inventory_Report r = new Inventory_Report();
                r.loadBrands("Select *from Brands where Brand like '"+metroTextBox1.Text+"%'","List Of Brands Name Starting With : '"+metroTextBox1.Text+"'");
                r.ShowDialog();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
