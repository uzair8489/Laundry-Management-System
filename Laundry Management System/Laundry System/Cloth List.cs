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
    public partial class Clothes_Details : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public Clothes_Details()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private void Clothes_Details_Load(object sender, EventArgs e)
        {
            loadData();
           
        }
        void loadData()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Clothes_Details";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }



        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
        }


        private void dataGridView1_RowPostPaint_1(object sender, DataGridViewRowPostPaintEventArgs e)
        {
        }

        private void Clothes_Details_Load_1(object sender, EventArgs e)
        {
            loadData();
            loadService();
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }
        public void loadService()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Services";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void metroTextBox1_TextChanged_1(object sender, EventArgs e)
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Clothes_Details where Cloth_Type like '" + metroTextBox1.Text + "%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            Edit_Cloth_Details a = new Edit_Cloth_Details(this);
            a.button5.Enabled = true;
            a.button7.Enabled = true;
            a.button6.Enabled = false;
            //a.textBox2.Enabled = true;
            a.textBox4.Enabled = true;
            a.Show();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();

        }

        private void dataGridView1_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("Are You Sure To Delete The Selected Data?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string prodid = Convert.ToString(dataGridView1.Rows[e.RowIndex].Cells[3].Value);
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Clothes_Details where Cloth_ID='" + prodid + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    loadData();
                }
            }
            else if (colname == "Edit")
            {



                Edit_Cloth_Details c = new Edit_Cloth_Details(this);
                //c.textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells["Cloth_ID"].Value.ToString();
                c.textBox2.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
                c.textBox4.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                c.textBox1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
                double price = double.Parse(dataGridView1.CurrentRow.Cells[6].Value.ToString());
                c.textBox3.Text = price.ToString();
                c.button7.Enabled = false;
                c.button6.Enabled = true;
                c.textBox2.Enabled = false;
                c.Show();


            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Service_Module a = new Service_Module(this);
            a.button5.Enabled = true;
            a.button7.Enabled = true;
            a.button6.Enabled = false;
            
            a.Show();
        }

        private void dataGridView2_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView2.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView2.Columns[e.ColumnIndex].Name;
            if (colname == "Del")
            {
                if (MessageBox.Show("Are You Sure To Delete The Selected Data?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    string prodid = dataGridView2.Rows[e.RowIndex].Cells[3].Value.ToString();
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Services where Service_ID='" + prodid + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    loadService();
                    MessageBox.Show("Record Deleted Successfully");
                }

            }

            if (colname == "Ed")
            {

                Service_Module cl = new Service_Module(this);

                cl.textBox2.Text = dataGridView2[3, e.RowIndex].Value.ToString();
                cl.textBox4.Text = dataGridView2[4, e.RowIndex].Value.ToString();
                cl.textBox3.Text = dataGridView2[5, e.RowIndex].Value.ToString();
                //cl.label3.Text = dataGridView2[3, e.RowIndex].Value.ToString();
                cl.button7.Enabled = false;
                cl.button6.Enabled = true;
                cl.Show();

            }
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView2  .Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (metroTextBox1.Text == string.Empty)
            {
                Inventory_Report r = new Inventory_Report();
                r.loadCloth("Select *from CLothes_Details", "List Of All Clothes");
                r.ShowDialog();
            }
            else
            {
                Inventory_Report r = new Inventory_Report();
                r.loadCloth("Select *from Clothes_Details where Cloth_Type like '" + metroTextBox1.Text + "%'", "List Of Clothes Name Starting With : '" + metroTextBox1.Text + "'");
                r.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (metroTextBox2.Text == string.Empty)
            {
                Inventory_Report r = new Inventory_Report();
                r.loadService("Select *from Services", "List Of All Services");
                r.ShowDialog();
            }
            else
            {
                Inventory_Report r = new Inventory_Report();
                r.loadService("Select *from Services where Service like '" + metroTextBox2.Text + "%'", "List Of Services Name Starting With : '" + metroTextBox2.Text + "'");
                r.ShowDialog();
            }
        }

        private void metroTextBox2_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Services where Service like '"+metroTextBox2.Text+"%'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }
    }



}
