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
    public partial class Stock_Adjustment : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        int _qty;
        public Stock_Adjustment()
        {
            InitializeComponent();
            load();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void load()
        {
            comboBox2.Items.Clear();
            //MessageBox.Show(dataGridView1.Columns.IndexOf(dataGridView1.Columns["BRAND"]).ToString());
            int i = 0;
            //dataGridView1.Rows.Clear();
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select p.Product_Code,p.Barcode,p.Product_Description,b.Brand,c.Category,p.Price,p.Quantity from Products as p inner join Brands as b on b.ID=p.Brand_ID inner join Category as c on c.ID=p.Category_ID ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            con.Close();
            

        }
        private void Stock_Adjustment_Load(object sender, EventArgs e)
        {

        }

        private void metroTextBox1_Click(object sender, EventArgs e)
        {

           
               
           
        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select p.Product_Code,p.Product_Description,b.Brand,c.Category,p.Price,p.Quantity from Products as p inner join Brands as b on b.ID=p.Brand_ID inner join Category as c on c.ID=p.Category_ID where p.Product_Description like '" + metroTextBox1.Text + "%'";
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        public void ReferenceNo()
        {
            Random rnd = new Random();
            textBox1.Text = rnd.Next().ToString();
        }
        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if(colname=="Select")
            {
                textBox2.Text = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                textBox3.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString()+" "+dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                textBox7.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                _qty = int.Parse(dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString());
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox4.Text != string.Empty && textBox6.Text != string.Empty && comboBox1.Text!=string.Empty && comboBox2.Text!=string.Empty)
                {
                    if (int.Parse(textBox4.Text) > _qty)
                    {
                        MessageBox.Show("ADJUSTMENT QUANTITY SHOULD BE LESS THAN STOCK ON HAND QUANTITY.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (comboBox1.Text == "REMOVE FROM INVENTORY")
                    {
                        if (comboBox2.Text == "EXPIRED" || comboBox2.Text == "DAMAGE" || comboBox2.Text == "USED")
                        {
                            SqlStatement("update Products set Quantity=(Quantity- '" + int.Parse(textBox4.Text) + "') where Product_Code like '" + textBox2.Text + "'");
                            SqlStatement("insert into Stock_Adjustment(Reference_Number,Product_Code,Price,Quantity,Action,Remarks,Stock_In_Date,[User]) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + double.Parse(textBox7.Text) + "','" + int.Parse(textBox4.Text) + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + DateTime.Now.ToShortDateString() + "','" + textBox6.Text + "')");
                        }
                        else if(comboBox2.Text == "OTHERS")
                        {
                            SqlStatement("update Products set Quantity=(Quantity- '" + int.Parse(textBox4.Text) + "') where Product_Code like '" + textBox2.Text + "'");
                            SqlStatement("insert into Stock_Adjustment(Reference_Number,Product_Code,Price,Quantity,Action,Remarks,Stock_In_Date,[User]) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + double.Parse(textBox7.Text) + "','" + int.Parse(textBox4.Text) + "','" + comboBox1.Text + "','" + textBox8.Text + "','" + DateTime.Now.ToShortDateString() + "','" + textBox6.Text + "')");
                        }
                    }
                    else if (comboBox1.Text == "ADD TO INVENTORY")
                    {
                        //textBox8.Focus();
                        if (comboBox2.Text == "NEW ADDED")
                        {

                            SqlStatement("update Products set Quantity=(Quantity+ '" + int.Parse(textBox4.Text) + "') where Product_Code like '" + textBox2.Text + "'");
                            SqlStatement("insert into Stock_Adjustment(Reference_Number,Product_Code,Price,Quantity,Action,Remarks,Stock_In_Date,[User]) Values ('" + textBox1.Text + "','" + textBox2.Text + "','" + double.Parse(textBox7.Text) + "','" + int.Parse(textBox4.Text) + "','" + comboBox1.Text + "','" + comboBox2.Text + "','" + DateTime.Now.ToShortDateString() + "','" + textBox6.Text + "')");
                        }
                    }
                    

                    MessageBox.Show("STOCK ADJUSTED SUCCESSFULLY", "Adjusted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    load();
                    clear();
                    ReferenceNo();
                }
                else
                {
                    MessageBox.Show("SOME INFORMATION IS MISSING", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox7.Clear();
            textBox4.Clear();
            comboBox2.Text = "";
            comboBox2.Items.Clear();
            //textBox6.Clear();
            comboBox1.Text="";
        }
        public void SqlStatement(string _sql)
        {
            con.Open();
            cmd = new SqlCommand(_sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox2.Items.Clear();
            if (comboBox1.Text == "REMOVE FROM INVENTORY")
            {
                comboBox2.Text = "";
                
                comboBox2.Items.Add("EXPIRED");
                comboBox2.Items.Add("DAMAGED");
                comboBox2.Items.Add("USED");
                comboBox2.Items.Add("OTHERS");
                


            }
            else if (comboBox1.Text == "ADD TO INVENTORY")
            {
                comboBox2.Text = "";
                textBox8.Enabled = false;
                comboBox2.Items.Add("NEW ADDED");
            }
            else
            {
                comboBox2.Items.Clear();
            }
        }

        private void textBox7_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "OTHERS")
            {
                textBox8.Enabled = true;
            }
            else
            {
                textBox8.Enabled = false;
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
    }
}
