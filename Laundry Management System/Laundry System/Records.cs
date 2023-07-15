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
using System.Windows.Forms.DataVisualization.Charting;

namespace Laundry_System
{
    public partial class Records : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        public Records()
        {
            InitializeComponent();
        }
        public void LoadRecord()
        {
            
            //dataGridView1.Rows.Clear();
            if (comboBox1.Text == "SORT BY QTY")
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandText = "select top 10 Cloth_ID,Cloth_Type,isnull(sum(Quantity),0) as Quantity,isnull(sum(Total),0) as Total  from vwOrdersDone where S_Date between '" + dateTimePicker1.Value.ToString() + "'and '" + dateTimePicker2.Value.ToString() + "' and Status like 'Done' group by Cloth_ID,Cloth_Type order by Quantity DESC";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            else if(comboBox1.Text=="SORT BY TOTAL AMOUNT")
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 10 Cloth_ID,Cloth_Type,isnull(sum(Quantity),0) as Quantity,isnull(sum(Total),0) as Total  from vwOrdersDone where S_Date between '" + dateTimePicker1.Value.ToString() + "'and '" + dateTimePicker2.Value.ToString() + "' and Status like 'Done' group by Cloth_ID,Cloth_Type order by Total DESC";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            //con.Close();
            //else
            //{
            //    SqlCommand cmd = con.CreateCommand();
            //    cmd.CommandType = CommandType.Text;
            //    cmd.CommandText = "select top 10 Cloth_ID,Cloth_Type,isnull(sum(Quantity),0) as Quantity,isnull(sum(Total),0) as Total  from vwOrdersDone where S_Date between '" + dateTimePicker1.Value.ToString() + "'and '" + dateTimePicker2.Value.ToString() + "' and Status like 'Done' group by Cloth_ID,Cloth_Type";
            //    cmd.ExecuteNonQuery();
            //    DataTable dt = new DataTable();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);
            //    da.Fill(dt);
            //    dataGridView1.DataSource = dt;
            //    con.Close();
            //}
            
        }

        public void CancelledOrders()
        {
            con.Open();
            //dataGridView1.Rows.Clear();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from vwCancelledOrders where S_Date between '" + dateTimePicker6.Value.ToString() + "' and '" + dateTimePicker5.Value.ToString() + "'";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView5.DataSource = dt;
            con.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            LoadRecord();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();

        }

        private void Records_Load(object sender, EventArgs e)
        {

        }
        public void TotalOrders()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select c.Cloth_ID,Cloth_Type,sum(c.Quantity) as Total_Quantity,sum(c.Discount) as Total_Discount,sum(c.Total) as Total from Cart as c inner join Clothes_Details as clt on c.Cloth_ID=clt.Cloth_ID where Status like 'Done' and S_Date between '" + dateTimePicker4.Value.ToString() + "' and '" + dateTimePicker3.Value.ToString() + "' group by c.Cloth_ID,clt.Cloth_Type  ";
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {
                    //double.Parse(dr["Price"].ToString()).ToString("#,##0.00");
                    double.Parse(dr["Total"].ToString()).ToString("#,##0.00");
                }
                dr.Close();
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView2.DataSource = dt;
                con.Close();

                string x;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select isnull(sum(Total),0) from Cart where Status like 'Done' and S_Date between '" + dateTimePicker4.Value.ToString() + "' and '" + dateTimePicker3.Value.ToString() + "'";
                label4.Text = double.Parse(cmd1.ExecuteScalar().ToString()).ToString("#,##0.00");
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ERROR");
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            
        }

        private void dataGridView2_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView2.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadInventry()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select p.Product_Code,p.Barcode,p.Product_Description, b.Brand,c.Category,p.Price,p.Reorder,p.Quantity from Products as p inner join Brands as b on b.ID=p.Brand_ID inner join Category as c on c.ID=p.Category_ID ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView4.DataSource = dt;
            con.Close();
        }

        public void loadStockInHistory()
        {

            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from View_Stock_In where cast(Stock_In_Date as date) between'" + dateTimePicker8.Value.ToShortDateString() + "' and '" + dateTimePicker7.Value.ToShortDateString() + "' and Status like 'Done'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView6.DataSource = dt;
            con.Close();
        }

        public void loadCriticalItems()
        {
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select *from vwCriticalItems";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView3.DataSource = dt;
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "ERROR");
            }
        }

        private void dataGridView3_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView3.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView4_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView4.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Inventory_Report f = new Inventory_Report();
            f.LoadReport("select p.Product_Code,p.Barcode,p.Product_Description, b.Brand,c.Category,p.Price,p.Reorder,p.Quantity from Products as p inner join Brands as b on b.ID=p.Brand_ID inner join Category as c on c.ID=p.Category_ID");
            f.ShowDialog();
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            CancelledOrders();
        }

        private void dataGridView5_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView5.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            loadStockInHistory();
        }

        private void dataGridView5_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            this.dataGridView5.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView6_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView6.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Store s = new Store();
            Inventory_Report f = new Inventory_Report();
            if (comboBox1.Text == "SORT BY QTY")
            {
                f.MostOrdered("select top 10 Cloth_ID,Cloth_Type,isnull(sum(Quantity),0) as Quantity,isnull(sum(Total),0) as Total from vwOrdersDone where S_Date between '" + dateTimePicker1.Value.ToString() + "'and '" + dateTimePicker2.Value.ToString() + "' and Status like 'Done' group by Cloth_ID,Cloth_Type order by Quantity DESC", "From : " + dateTimePicker1.Value.ToShortDateString() + "To : " + dateTimePicker2.Value.ToShortDateString(),"Most Ordered Clothes Order By Qty");


            }
            else if (comboBox1.Text == "SORT BY TOTAL AMOUNT")
            {
                f.MostOrdered("select top 10 Cloth_ID,Cloth_Type,isnull(sum(Quantity),0) as Quantity,isnull(sum(Total),0) as Total from vwOrdersDone where S_Date between '" + dateTimePicker1.Value.ToString() + "'and '" + dateTimePicker2.Value.ToString() + "' and Status like 'Done' group by Cloth_ID,Cloth_Type order by Total DESC", "From : " + dateTimePicker1.Value.ToShortDateString() + " To : " + dateTimePicker2.Value.ToShortDateString(), "Most Ordered Clothes Order By Total Amount");
            }
            f.ShowDialog();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Inventory_Report f = new Inventory_Report();
            f.TotalOrders("select c.Cloth_ID, Cloth_Type, sum(c.Quantity) as Total_Quantity, sum(c.Discount) as Total_Discount, sum(c.Total) as Total from Cart as c inner join Clothes_Details as clt on c.Cloth_ID = clt.Cloth_ID where Status like 'Done' and S_Date between '"+dateTimePicker4.Value.ToString()+"' and '"+dateTimePicker3.Value.ToString()+ "' group by c.Cloth_ID, clt.Cloth_Type", "From : " + dateTimePicker4.Value.ToShortDateString() + " To :" + dateTimePicker3.Value.ToShortDateString());
            f.ShowDialog();
        }

        private void dateTimePicker4_ValueChanged(object sender, EventArgs e)
        {
            TotalOrders();
        }

        private void dateTimePicker3_ValueChanged(object sender, EventArgs e)
        {
            TotalOrders();
        }

        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {

            if(comboBox1.Text==string.Empty)
            {
                MessageBox.Show("PLEASE SELECT FROM DROPDOWN LIST", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadRecord();
            LoadMostOrderedChart();
        }

        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == string.Empty)
            {
                MessageBox.Show("PLEASE SELECT FROM DROPDOWN LIST", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            LoadRecord();
            LoadMostOrderedChart();
        }
        public void LoadMostOrderedChart()
        {
            SqlDataAdapter da = new SqlDataAdapter();
            con.Open();
            if (comboBox1.Text == "SORT BY QTY")
            {
                da = new SqlDataAdapter("select top 10 Cloth_Type,isnull(sum(Quantity),0) as Quantity from vwOrdersDone where S_Date between '" + dateTimePicker1.Value.ToString() + "'and '" + dateTimePicker2.Value.ToString() + "' and Status like 'Done' group by Cloth_Type order by Quantity DESC", con);
                
                
            }
            else if (comboBox1.Text == "SORT BY TOTAL AMOUNT")
            {

                 da = new SqlDataAdapter("select top 10 Cloth_Type,isnull(sum(Total),0) as Total  from vwOrdersDone where S_Date between '" + dateTimePicker1.Value.ToString() + "'and '" + dateTimePicker2.Value.ToString() + "' and Status like 'Done' group by Cloth_Type order by Total DESC", con);
                
            }
            DataSet ds = new DataSet();
            da.Fill(ds, "MOST ORDERED");
            chart1.DataSource = ds.Tables["MOST ORDERED"];
            Series series1 = chart1.Series[0];
            series1.ChartType = SeriesChartType.Doughnut;
            series1.Name = "MOST ORDERED";
            var chart = chart1;
            chart.Series[0].XValueMember = "Cloth_Type";
            //double dd = double.Parse(chart.Series[series1.Name].YValueMembers);
            if (comboBox1.Text == "SORT BY QTY")
            {
                chart.Series[0].YValueMembers = "Quantity";
            }
            if (comboBox1.Text == "SORT BY TOTAL AMOUNT")
            {
                chart.Series[0].YValueMembers = "Total";
            }

            chart.Series[0].IsValueShownAsLabel = true;
            if (comboBox1.Text == "SORT BY TOTAL AMOUNT")
            {
                chart.Series[0].LabelFormat ="{#,##0.00}";
            }
            if (comboBox1.Text == "SORT BY QTY")
            {
                chart.Series[0].LabelFormat = "{#,##0}";
            }
            con.Close();

        }

        private void dateTimePicker6_ValueChanged(object sender, EventArgs e)
        {
            CancelledOrders();
        }

        private void dateTimePicker5_ValueChanged(object sender, EventArgs e)
        {
            CancelledOrders();
        }

        private void dateTimePicker8_ValueChanged(object sender, EventArgs e)
        {
            loadStockInHistory();
        }

        private void dateTimePicker7_ValueChanged(object sender, EventArgs e)
        {
            loadStockInHistory();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            LoadRecord();
            LoadMostOrderedChart();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void linkLabel7_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Chart f = new Chart();
            f.label1.Text = "TOTAL ORDERS [" + dateTimePicker4.Value.ToShortDateString() + " - " + dateTimePicker4.Value.ToShortDateString() + "]";
            f.LoadChartOrdered("select Cloth_Type,sum(c.Total) as Total from Cart as c inner join Clothes_Details as clt on c.Cloth_ID = clt.Cloth_ID where Status like 'Done' and S_Date between '" + dateTimePicker4.Value.ToString() + "' and '" + dateTimePicker3.Value.ToString() + "' group by clt.Cloth_Type");
            f.ShowDialog();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Inventory_Report f = new Inventory_Report();
            string param = "Date Covered: " + dateTimePicker8.Value.ToShortDateString() + " - " + dateTimePicker7.Value.ToShortDateString();
            f.LoadStockInReport("select * from View_Stock_In where cast(Stock_In_Date as date) between'" + dateTimePicker8.Value.ToShortDateString() + "' and '" + dateTimePicker7.Value.ToShortDateString() + "' and Status like 'Done'",param);
            f.ShowDialog();
        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Inventory_Report f = new Inventory_Report();
            string param = "Date Covered: "+ dateTimePicker6.Value.ToString() + "' - '" + dateTimePicker5.Value.ToString();
            f.LoadCancelledReport("select *from vwCancelledOrders where S_Date between '" + dateTimePicker6.Value.ToString() + "' and '" + dateTimePicker5.Value.ToString() + "'", param);
            f.ShowDialog();
        }

       

        private void metroTextBox1_TextChanged_1(object sender, EventArgs e)
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select p.Product_Code,p.Barcode,p.Product_Description, b.Brand,c.Category,p.Price,p.Reorder,p.Quantity from Products as p inner join Brands as b on b.ID=p.Brand_ID inner join Category as c on c.ID=p.Category_ID";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView4.DataSource = dt;
            con.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel7_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
