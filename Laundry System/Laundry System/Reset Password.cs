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
    public partial class Reset_Password : Form
    {
        Recover_Password f;
        public Reset_Password(Recover_Password fs)
        {
            InitializeComponent();
            f = fs;
        }

        private void Reset_Password_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox2.Text == textBox3.Text)
            {
                SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                SqlCommand cmd = new SqlCommand("update Users set password='" + textBox3.Text + "'where Username='" + f.textBox1 + "'", con);
                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("YOUR PASSWORD HAS BEEN RESET SUCCESSFULLY", "Password Reset", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Dispose();
                Security s = new Security();
                s.Show();
            }
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength >= 8)
            {
                label5.Visible = true;
                label5.Text = "Your Password Is Strong";
                label5.ForeColor = System.Drawing.Color.Green;
            }

            else if ((textBox2.TextLength < 6) && (textBox2.TextLength > 4))
            {
                label5.Visible = true;
                label5.Text = "Your Password Is Week";
                label5.ForeColor = System.Drawing.Color.YellowGreen;
            }
            else if ((textBox2.TextLength <= 4) && (textBox2.TextLength > 0))
            {
                label5.Visible = true;
                label5.Text = "Your Password Is Too Week";
                label5.ForeColor = System.Drawing.Color.Red;
            }
            else if (textBox2.Text == string.Empty)
            {
                label5.Visible = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                label6.Visible = true;
                label6.Text = "Password Does not Match";
                label6.ForeColor = System.Drawing.Color.Red;
            }

            else if (textBox2.Text == textBox3.Text)
            {
                label6.Visible = false;
            }
            else if (textBox3.Text == string.Empty)
            {
                label6.Visible = false;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
            Security s = new Security();
            s.Show();
        }
    }
}
