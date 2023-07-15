using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Mail;
using System.Data.SqlClient;

namespace Laundry_System
{
    public partial class Recover_Password : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        string randomCode;
        
        public static string to;
        public Recover_Password()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != string.Empty && textBox2.Text != string.Empty)
            {
                try
                {
                    bool found = false;

                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select *from Users where Username='" + textBox1.Text + "'";
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
                        string from, pass, mail;
                        Random rnd = new Random();
                        randomCode = (rnd.Next(999999)).ToString();
                        MailMessage message = new MailMessage();
                        to = (textBox2.Text).ToString();
                        from = "starlaundry00@gmail.com";
                        pass = "Starlaundry1@";
                        mail = "Your Password Reset Code is " + randomCode + ". Please do not share this code with others for security purposes.";
                        message.To.Add(to);
                        message.From = new MailAddress(from);
                        message.Body = mail;
                        message.Subject = "Password Reseting Code";
                        SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                        smtp.EnableSsl = true;
                        smtp.Port = 587;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        smtp.Credentials = new NetworkCredential(from, pass);
                        smtp.Send(message);
                        MessageBox.Show("THE PASSWORD RESET CODE HAS BEEN SENT TO YOUR EMAIL", "Email Sent", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("USERNAME NOT FOUND.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("FILL UP THE EMPTY FIELDS FIRST.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
                
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (randomCode == (textBox3.Text).ToString())
            {
                to = textBox2.Text;
                Reset_Password rs = new Reset_Password(this);
                this.Dispose();
                Security s = new Security();
                s.Dispose();
                rs.Show();
            }
            else
            {
                MessageBox.Show("OTP IS WRONG. PLEASE RE-ENTER","Wrong OTP",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

       

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
            Security s = new Security();
            s.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                bool found = false;
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select *from Users where Username='" + textBox1.Text + "'";
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

                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "select *from Users where Username='" + textBox1.Text + "'";
                    dr = cmd1.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {
                        textBox2.Text = dr["Email"].ToString();
                        //textBox2.Text = dr["Email"].ToString();
                    }
                    dr.Close();
                    con.Close();
                }
                else
                {

                    found = false;
                    MessageBox.Show("USERNAME NOT FOUND.", "Invalid", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Wrong OTP", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            
        }

        private void Recover_Password_Load(object sender, EventArgs e)
        {

        }
    }
}
