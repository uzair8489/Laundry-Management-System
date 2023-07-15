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
    public partial class Product_Module : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Product_List pl;
        public Product_Module(Product_List p)
        {
            InitializeComponent();
            pl = p;
            GetTransNo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadcategory()
        {
            comboBox2.Items.Clear();
            con.Open();
            string query = "SELECT Category FROM Category";
            cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["Category"].ToString());
            }
            dr.Close();
            con.Close();

        }
        public void loadbrand()
        {
            comboBox1.Items.Clear();
            con.Open();
            string query = "SELECT Brand FROM Brands";
            cmd = new SqlCommand(query, con);
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Brand"].ToString());
            }
            dr.Close();
            con.Close();

        }
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            comboBox1.Text  = "";
            comboBox2.Text = "";
        }
        private void Product_Module_Load(object sender, EventArgs e)
        {
            
        }
        public void GetTransNo()
        {

            try
            {

                int count;
                string prcode;
                string pcode = "PDT";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 1 Product_Code from Products where Product_Code like '" + pcode + "%' order by Product_Code desc";
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    prcode = dr[0].ToString();
                    count = int.Parse(prcode.Substring(3, 3));
                    textBox1.Text = pcode + (count + 1);


                }
                else
                {
                    prcode = pcode + "01";
                    textBox1.Text = prcode;

                }
                dr.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
       

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure To Add This Category", "Add New Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = ""; 
                    string cid="";
                    con.Open();
                    
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = ("select ID from Brands where Brand like'"+comboBox1.Text+"'");
                    dr=cmd1.ExecuteReader();
                    dr.Read();
                    if(dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();

                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = ("select ID from Category where Category like'" + comboBox2.Text + "'");
                    dr = cmd2.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Products(Product_Code,Barcode,Product_Description,Brand_ID,Category_ID,Price,Reorder) values ('" + textBox1.Text + "','"+ textBox4.Text +"','"+textBox2.Text+"','"+bid+"','"+cid+"','"+double.Parse(textBox3.Text)+"','"+int.Parse(textBox5.Text)+"')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    pl.load();
                    MessageBox.Show("Record Saved Successfully.");
                    clear();
                    GetTransNo();
                    
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("Are You Sure To Update This Record", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string bid = "";
                    string cid = "";
                    con.Open();

                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = ("select ID from Brands where Brand like'" + comboBox1.Text + "'");
                    dr = cmd1.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        bid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();

                    con.Open();

                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = ("select ID from Category where Category like'" + comboBox2.Text + "'");
                    dr = cmd2.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        cid = dr[0].ToString();
                    }
                    dr.Close();
                    con.Close();


                    //string brndid = comboBox1.Text.ToString();
                    //string ctgid = comboBox2.Text.ToString();
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "Update Products set Barcode='"+textBox4.Text+"', Product_Description='" + textBox2.Text + "',Brand_ID='" + bid + "',Category_ID='" + cid + "',Price='" + double.Parse(textBox3.Text) + "',Reorder='" + int.Parse(textBox5.Text) + "' where Product_Code like '"+textBox1.Text+"'"; 
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Saved Successfully.");
                    
                    pl.load();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        

        private void button3_Click_2(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
