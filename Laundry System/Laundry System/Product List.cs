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
    public partial class Product_List : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        public Product_List()
        {
            InitializeComponent();
            LoadDamaged();
            LoadExpired();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void load()
        {
            //MessageBox.Show(dataGridView1.Columns.IndexOf(dataGridView1.Columns["BRAND"]).ToString());
            int i = 0;
            //dataGridView1.Rows.Clear();
            con.Open();
            
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select p.Product_Code,p.Barcode,p.Product_Description,b.Brand,c.Category,p.Price,p.Reorder from Products as p inner join Brands as b on b.ID=p.Brand_ID inner join Category as c on c.ID=p.Category_ID ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            
            con.Close();

        }
        public void LoadExpired()
        {
           
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'EXPIRED'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                con.Close();
            
        }
        public void LoadDamaged()
        {

            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'DAMAGED'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            con.Close();

        }

        
        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        private void Product_List_Load(object sender, EventArgs e)
        {
            load();
        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            
            //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            if (e.ColumnIndex == 3)
            {
                if (MessageBox.Show("ARE YOU SURE TO DELETE THIS DATA?", "Delete", MessageBoxButtons.YesNo,MessageBoxIcon.Warning) == DialogResult.Yes)
                {
                    //int prodid = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["ID"].Value);
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Products where Product_Code='" + dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString() + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    
                    load();
                    MessageBox.Show("RECORD DELETED SUCCESSFULLY.","Deleted",MessageBoxButtons.OK,MessageBoxIcon.Information);
                }

            }

            if (e.ColumnIndex == 2)
            {

                Product_Module pl = new Product_Module(this);

                pl.textBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                pl.textBox4.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                pl.textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                pl.comboBox1.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                pl.comboBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                pl.textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                pl.textBox5.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                

                //pl.label3.Text = dataGridView1[2, e.RowIndex].Value.ToString();
                pl.button1.Enabled = false;
                pl.button2.Enabled = true;
                pl.loadbrand();
                pl.loadcategory();
                pl.Show();

            }
        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void metroTextBox1_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Product_Module p = new Product_Module(this);
            p.button1.Enabled = true;
            p.button2.Enabled = false;
            p.loadbrand();
            p.loadcategory();
            p.Show();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void metroTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            int i = 0;
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select p.Product_Code,p.Barcode,p.Product_Description,b.Brand,c.Category,p.Price,p.Reorder from Products as p inner join Brands as b on b.ID=p.Brand_ID inner join Category as c on c.ID=p.Category_ID where p.Product_Description like '" + metroTextBox1.Text + "%'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroTextBox2_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'EXPIRED' and Product_Description like '" + metroTextBox2.Text + "%'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt;
            con.Close();
        }

        private void metroTextBox3_TextChanged(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'DAMAGED' and Product_Description like '"+metroTextBox3.Text+"%'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView3.DataSource = dt;
            con.Close();
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView2.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView2.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if(metroTextBox2.Text=="")
            {
                Inventory_Report f = new Inventory_Report();
                f.loadExpired("select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'EXPIRED'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ","List Of All Expired Items");
                f.Show();
            }
            if(metroTextBox2.Text!=string.Empty)
            {
                Inventory_Report f = new Inventory_Report();
                f.loadExpired("select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'EXPIRED' and Product_Description Like '"+metroTextBox2.Text+"%'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ", "List Of Expired Items Product Name Starting With : '"+metroTextBox2.Text+"'");
                f.Show();
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (metroTextBox3.Text == "")
            {
                Inventory_Report f = new Inventory_Report();
                f.loadExpired("select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'DAMAGED'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ", "List Of All Damaged Items");
                f.Show();
            }
            if (metroTextBox3.Text != string.Empty)
            {
                Inventory_Report f = new Inventory_Report();
                f.loadExpired("select b.Product_Code,b.Product_Description,a.Price,isnull(sum(a.Quantity),0) as Quantity,isnull(sum(a.Total),0) as Total,Stock_In_Date  from Adjustment as a inner join Products as b on a.Product_Code=b.Product_Code where Remarks like 'DAMAGED' and Product_Description Like '" + metroTextBox3.Text + "%'   group by b.Product_Code,Product_Description,a.Price,a.Stock_In_Date ", "List Of Damaged Items Product Name Starting With : '" + metroTextBox3.Text + "'");
                f.Show();
            }
        }
    }
}
