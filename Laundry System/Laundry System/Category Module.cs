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
    public partial class Category_Module : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Category_List cl;
        public Category_Module(Category_List c)
        {
            InitializeComponent();
            cl = c;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void clear()
        {
            button1.Enabled = true;
            button2.Enabled = false;
            textBox1.Clear();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("PLEASE ADD CATEGORY","Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            try
            {

                if (MessageBox.Show("Are You Sure To Add This Category", "Add New Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select Category from Category where Category='" + textBox1.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        MessageBox.Show("CATEGORY ALREADY EXISTS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into Category(Category) values ('" + textBox1.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("RECORD SAVED SUCCESSFULLY");
                        clear();
                        cl.load();
                    }
                }
            }catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void Category_Module_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            try
            {
                
                if (MessageBox.Show("ARE YOU SURE TO UPDATE THIS RECORD", "Update Category", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update Category set Category='" + textBox1.Text + "' where ID='" + label3.Text + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("RECORD UPDATED SUCCESSFULLY.","Record Updated",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    cl.load();
                }
            }catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
                
            }
        }
    }
}
