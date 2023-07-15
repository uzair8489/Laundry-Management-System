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
    public partial class User_Account : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Admin_Portal f;
        public User_Account(Admin_Portal fm)
        {
            InitializeComponent();
            f = fm;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.Text = "";
            textBox1.Focus();
        }
        private void User_Account_Resize(object sender, EventArgs e)
        {
            metroTabControl1.Left = (this.Width - metroTabControl1.Width) / 2;
            metroTabControl1.Top = (this.Height - metroTabControl1.Height) / 2;
        }

        private void User_Account_Load(object sender, EventArgs e)
        {
            //if (textBox1.Text == " " || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || comboBox1.Text == "")
            //{
            //    button1.Enabled = false;
            //}
            //else
            //{
            //    button1.Enabled = true;
            //}
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty && textBox3.Text != string.Empty && textBox4.Text != string.Empty && comboBox1.Text != string.Empty)
            {
                try
                {
                    if (textBox2.Text != textBox3.Text)
                    {
                        MessageBox.Show("PASSWORD DID NOT MATCH!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (textBox3.Text.Length<4)
                    {
                        MessageBox.Show("PLEASE CHOOSE A STRONG PASSWORD", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {


                        con.Open();
                        cmd = new SqlCommand("insert into Users(Username,Password,Role,Name) values('" + textBox1.Text + "','" + textBox2.Text + "','" + comboBox1.Text + "','" + textBox4.Text + "')", con);
                        cmd.ExecuteNonQuery();
                        con.Close();
                        MessageBox.Show("ACCOUNT CREATED");
                        clear();
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("NO FIELD SHOULD BE EMPTY", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox5.Text != string.Empty && textBox6.Text != string.Empty && textBox7.Text != string.Empty && textBox8.Text != string.Empty)
            {
                try
                {

                    //string _oldPass = db.GetPassword(f.lbluser.Text);
                    if (textBox7.Text != f._pass)
                    {
                        MessageBox.Show("OLD PASSWORD DID NOT MATCH!", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (textBox6.Text != textBox5.Text)
                    {
                        MessageBox.Show("NEW PASSWORD DID NOT MATCH!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else if (MessageBox.Show("CHANGE PASSWORD?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        con.Open();
                        cmd = new SqlCommand("update Users set Password ='" + textBox6.Text + "' where Username='" + textBox8.Text + "'", con);
                        cmd.ExecuteNonQuery();
                        con.Close();

                        MessageBox.Show("PASSWORD CHANGED SUCCESSFULLY.", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //this.Dispose();
                    }
                    textBox5.Clear();
                    textBox6.Clear();
                    textBox7.Clear();

                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message, "Error");
                }
            }
            else
            {
                MessageBox.Show("NO FIELD SHOULD BE EMPTY", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {
            try
            {
                con.Open();
                cmd = new SqlCommand("Select *from Users where Username='" + textBox12.Text + "'", con);
                dr = cmd.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    checkBox1.Checked = bool.Parse(dr["Active_Status"].ToString());
                }
                else
                {
                    checkBox1.Checked = false;
                }
                dr.Close();
                con.Close();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            bool found = true;
            con.Open();
            cmd = new SqlCommand("Select *from Users where Username='" + textBox12.Text + "'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if (dr.HasRows)
            {
                found = true;
            }
            else
            {
                found = false;
            }
            dr.Close();
            con.Close();

            if (found == true)
            {
                try
                {
                    con.Open();
                    cmd = new SqlCommand("update Users set Active_Status='" + checkBox1.Checked.ToString() + "' where Username='" + textBox12.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();
                    MessageBox.Show("ACCOUNT STATUS UPDATED SUCCESSFULLY.", "Account Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    textBox12.Clear();
                    checkBox1.Checked = false;
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("ACCOUNT DOES NOT EXIST.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.TextLength >= 8)
            {
                label19.Visible = true;
                label19.Text = "Your Password Is Strong";
                label19.ForeColor = System.Drawing.Color.Green;
            }

            else if ((textBox2.TextLength < 6) && (textBox2.TextLength > 4))
            {
                label19.Visible = true;
                label19.Text = "Your Password Is Week";
                label19.ForeColor = System.Drawing.Color.YellowGreen;
            }
            else if ((textBox2.TextLength <= 4) && (textBox2.TextLength > 0))
            {
                label19.Visible = true;
                label19.Text = "Your Password Is Too Week";
                label19.ForeColor = System.Drawing.Color.Red;
            }
            else if (textBox2.Text == string.Empty)
            {
                label19.Visible = false;
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text != textBox3.Text)
            {
                label8.Visible = true;
                label8.Text = "Password Does not Match";
                label8.ForeColor = System.Drawing.Color.Red;
            }
           
            else if(textBox2.Text == textBox3.Text)
            {
                label8.Visible = false;
            }
            else if (textBox3.Text == string.Empty)
            {
                label8.Visible = false;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back);
        }

        private void textBox12_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }
    }
}
