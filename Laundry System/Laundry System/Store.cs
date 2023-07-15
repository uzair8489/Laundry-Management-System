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
    public partial class Store : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;


        public Store()
        {
            InitializeComponent();
            
           
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadRecord()
        {
            con.Open();
            
            cmd =new SqlCommand("select *from Store_Details",con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                textBox1.Text = dr["Store_Name"].ToString();
                textBox2.Text = dr["Address"].ToString();
                textBox3.Text = dr["Contact"].ToString();
            }
            else
            {
                textBox1.Clear();
                textBox2.Clear();
                textBox3.Clear();
            }
            dr.Close();
            con.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(MessageBox.Show("SAVE STORE DETAILS","CONFIRM",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    int count;
                    con.Open();
                    cmd = new SqlCommand("Select count(*) from Store_Details", con);
                    count = int.Parse(cmd.ExecuteScalar().ToString());
                    con.Close();
                    if(count>0)
                    {
                        con.Open();
                        cmd = new SqlCommand("update Store_Details set Store_Name='" + textBox1.Text + "',Address='" + textBox2.Text + "',Contact='" + textBox3.Text + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        
                    }
                    else
                    {
                        con.Open();
                        cmd = new SqlCommand("insert into Store_Details(Store_Name,Address,Contact) Values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        

                    }
                    MessageBox.Show("STORE DETAILS SAVED SUCCESSFULLY", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Store_Load(object sender, EventArgs e)
        {
            LoadRecord();
            //string name = textBox1.Text.ToString();
            //string ad = textBox2.Text.ToString();
            //string cn = textBox3.Text.ToString();
            //Inventory_Report r = new Inventory_Report();
            //Records r = new Records(thi);
        }
    }
}
