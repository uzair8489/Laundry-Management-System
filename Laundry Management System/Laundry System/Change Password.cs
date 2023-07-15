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
    public partial class Change_Password : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        DBConnection db = new DBConnection();
        Cashier_Menu f;
        public Change_Password(Cashier_Menu fm)
        {
            InitializeComponent();
            con = new SqlConnection(db.myconnection());
            f = fm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try 
            {
                string _oldPass = db.GetPassword(f.lbluser.Text);
                if(_oldPass !=txtold.Text)
                {
                    MessageBox.Show("OLD PASSWORD DID NOT MATCH!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if(txtnew.Text !=txtconfm.Text)
                {
                    MessageBox.Show("NEW PASSWORD DID NOT MATCH!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                else if(MessageBox.Show("CHANGE PASSWORD?","Confirm",MessageBoxButtons.YesNo,MessageBoxIcon.Question)==DialogResult.Yes)
                {
                    con.Open();
                    cmd = new SqlCommand("update Users set Password ='" + txtnew.Text + "' where Username='" + f.lbluser.Text + "'", con);
                    cmd.ExecuteNonQuery();
                    con.Close();

                    MessageBox.Show("PASSWORD CHANGED SUCCESSFULLY.", "Password Changed", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Dispose();
                }
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
