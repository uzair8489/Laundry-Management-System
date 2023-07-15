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
    public partial class Customer_Module : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Customer_Details f;
        public Customer_Module(Customer_Details fm)
        {
            InitializeComponent();
            f = fm;
            GetTransNo();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void get(string cstid)
        {
            textcid.Text = cstid;
        }
        private void Customer_Module_Load(object sender, EventArgs e)
        {

        }
        public void clear()
        {
            textaddress.Clear();
            textcname.Clear();
            textcphone.Clear();
            textemail.Clear();
            
        }
        public void GetTransNo()
        {
           
            try
            {

                int count;
                string cust;
                string cstid = "CST";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 1 Customer_ID from Customer_Details where Customer_ID like '" + cstid + "%' order by Customer_ID desc";
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    cust = dr[0].ToString();
                    count = int.Parse(cust.Substring(3, 3));
                    textcid.Text = cstid + (count + 1);
                    

                }
                else
                {
                    cust = cstid + "101";
                    textcid.Text= cust;
                    
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

        private void button7_Click(object sender, EventArgs e)
        {
            
            //if (textBox1.Text == "")
            //{
            //    MessageBox.Show("PLEASE ADD CATEGORY", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            try
            {

                if (MessageBox.Show("Are You Sure To Add This Customer", "Add New Customer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into Customer_Details(Customer_ID,Name,Phone_Number,Email,Address) values ('" + textcid.Text + "','" + textcname.Text + "','" + textcphone.Text + "','" + textemail.Text + "','" + textaddress.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("RECORD SAVED SUCCESSFULLY");
                        GetTransNo();
                        f.load(); 
                        clear();
                        //cl.load();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Update This Record", "Update", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Customer_Details set Name='" + textcname.Text + "',Phone_Number='"+textcphone.Text+ "',Email='"+textemail.Text+"',Address='"+textaddress.Text+"' where Customer_ID='" + textcid.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Record Updated Successfully.", "Record Updated");

                f.load();
            }
        }

        private void textcphone_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textcname_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void textemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }
    }
}
