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
    public partial class Employee_Data_Updation : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Employees_Details f;
        public Employee_Data_Updation(Employees_Details fm)
        {
            InitializeComponent();
            f = fm;
        }

        private void Employee_Data_Updation_Load(object sender, EventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
        public void clear()
        {
            txteid.Clear();
            txtfirst.Clear();
            txtlast.Clear();
            txtphon.Clear();
            txtemail.Clear();
            txtage.Clear();
            cmbgend.Text = "";
            txtaddress.Clear();
            txtcnic.Clear();
            txtcity.Text = "";
            txtdepart.Clear();
            txtdesg.Clear();
            txtsal.Clear();
            //dtjoin.Text = "";
            dtdutystart.Text = "";
            dtdutyend.Text = "";
            dtvac.Text = "";
            comboBox1.Text="";

            

        }

        private void button6_Click(object sender, EventArgs e)
        {
            
            try
            {
                //Employees_Details s = new Employees_Details();
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Employee_Personal_Details set First_Name='" + txtfirst.Text + "',Last_Name='" + txtlast.Text + "',Phone_Number='" + txtphon.Text + "',CNIC='" + txtcnic.Text + "',Email='" + txtemail.Text + "',Age='" + txtage.Text + "',Gender='" + cmbgend.Text + "',Address='" + txtaddress.Text + "',City='" + txtcity.Text + "' where Employee_ID='" + txteid.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();


                con.Open();
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "update Employee_Duty_Details set Department='" + txtdepart.Text + "',Designation='" + txtdesg.Text + "',Salary='" + txtsal.Text + "',Duty_Start_Time='" + dtdutystart.Text + "',Duty_End_Time='" + dtdutyend.Text + "',Allowed_Vacations='" + dtvac.Text + "',Status='" + comboBox1.Text + "'where Employee_ID='" + txteid.Text + "'";
                cmd2.ExecuteNonQuery();
                con.Close();


                MessageBox.Show("RECORD UPDATED SUCCESSFULLY", "Record Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                f.loadRecord();
                clear();
                
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
    }
}
