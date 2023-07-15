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
    public partial class Security : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        public string _pass, _username = "";
        public bool _isactive;
        public Security()
        {
            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string _role="", _name = "";
            try
            {
                bool found = false;
                    
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select *from Users where Username='"+metroTextBox1.Text+"' and Password='"+metroTextBox2.Text+"'";
                dr = cmd.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    found = true;
                    _username = dr["Username"].ToString();
                    _role = dr["Role"].ToString();
                    _name = dr["Name"].ToString();
                    _pass = dr["Password"].ToString();
                    _isactive = bool.Parse(dr["Active_Status"].ToString());
                }
                else
                {
                    found = false;
                }
                dr.Close();
                con.Close();
                if(found==true)
                {
                    if(_isactive==false)
                    {
                        MessageBox.Show("ACCOUNT IS INACTIVE. YOU CANNOT LOGIN", "Inactive Account", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (_role == "Cashier")
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        metroTextBox2.Clear();
                        metroTextBox1.Clear();
                        this.Hide();
                        Cashier_Menu c = new Cashier_Menu(this);
                        c.lbluser.Text = _username;
                        c.lblname.Text = _name + " | " +_role;
                        c.ShowDialog();
                    }
                    else
                    {
                        MessageBox.Show("Welcome " + _name + "!", "ACCESS GRANTED", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        metroTextBox2.Clear();
                        metroTextBox1.Clear();
                        this.Hide();
                        Admin_Portal a = new Admin_Portal();
                        a.label4.Text = _name;
                        a.label5.Text = _role;
                        a.lbluser.Text = _username;
                        a._user = _username;
                        a._pass = _pass;
                        a.mdashboard();
                        a.ShowDialog();
                    }
                }
                else
                {
                    MessageBox.Show("Invalid Username or Password!" , "ACCESS DENIED", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //this.Dispose();
            Recover_Password rp = new Recover_Password();
           
            rp.ShowDialog();
            
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Recover_Password rp = new Recover_Password();
            rp.Show();
            this.Dispose();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            
        }

        private void Security_Load(object sender, EventArgs e)
        {

        }
    }
}
