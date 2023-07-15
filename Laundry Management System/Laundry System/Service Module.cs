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
    public partial class Service_Module : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Clothes_Details f;
        public Service_Module(Clothes_Details fm)
        {
            InitializeComponent();
            f = fm;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text != "" && textBox4.Text != "" && textBox3.Text != "")
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select Cloth_Type from Clothes_Details where Cloth_Type='" + textBox4.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        MessageBox.Show("SERVICE ALREADY EXISTS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into Services (Service_ID,Service,Charges) values ('" + textBox2.Text + "','" + textBox4.Text + "','" + textBox3.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        f.loadService();
                        MessageBox.Show("RECORD SAVED SUCCESSFULLY","Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                        
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        button6.Enabled = false;
                        button7.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void Service_Module_Load(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Update This Record", "Update Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textBox2.Enabled = false;
                double price = Convert.ToDouble(textBox3.Text);

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Services set Service_ID='" + textBox2.Text + "',Service='"+textBox4.Text+"',Charges='" + price + "' where Service_ID='" + textBox2.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                f.loadService();
                MessageBox.Show("Record Updated Successfully","Updated",MessageBoxButtons.OK,MessageBoxIcon.Information);
                
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
