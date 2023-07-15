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
    public partial class Vendors : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Vendors_List f;
        public Vendors(Vendors_List fm)
        {
            InitializeComponent();
            f = fm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            textBox6.Clear();
            button1.Enabled = true;
            button2.Enabled = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
               if(MessageBox.Show("SAVE THIS RECORD?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("insert into Vendors(Vendor,Address,Person_Contact,Telephone,Email,Fax) Values('" + textBox1.Text + "','" + textBox2.Text + "','" + textBox3.Text + "','" + textBox6.Text + "','" + textBox5.Text + "','" + textBox4.Text + "')", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("RECORD SAVED SUCCESSFULLY", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    f.LoadRecord();
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if (MessageBox.Show("SAVE THIS RECORD?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("update Vendors set Vendor='" + textBox1.Text + "',Address='" + textBox2.Text + "',Person_Contact='" + textBox3.Text + "',Telephone='" + textBox6.Text + "',Email='" + textBox5.Text + "',Fax='" + textBox4.Text + "' where ID='"+label8.Text+"'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("RECORD UPDATED SUCCESSFULLY", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    clear();
                    f.LoadRecord();
                    this.Dispose();
                }
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
