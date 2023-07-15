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
    public partial class Brand_Module : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Brand_List bl;
        public Brand_Module(Brand_List b)
        {
            InitializeComponent();
            bl = b;
        }

        private void Brand_Module_Load(object sender, EventArgs e)
        {

        }
        private void clear()
        {
            button1.Enabled = true;
            button2.Enabled = false;
            textBox1.Clear();
            textBox1.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(textBox1.Text=="")
            {
                MessageBox.Show("Please Add Brand Name.");
                return;
            }
            else if (MessageBox.Show("Are You Sure To Add This Brand", "Add New Brand", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                SqlDataAdapter da = new SqlDataAdapter("Select Brand from Brands where Brand='" + textBox1.Text + "'", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count >= 1)
                {
                    MessageBox.Show("BRAND ALREADY EXISTS", "", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Brands(Brand) values ('" + textBox1.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("Record Saved Successfully.");
                    clear();
                    bl.load();
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (MessageBox.Show("Are You Sure To Update This Record", "Update Brand", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Brands set Brand='" + textBox1.Text + "' where ID='" + label3.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Updated Successfully.", "Record Updated");

                bl.load();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }
    }
}
