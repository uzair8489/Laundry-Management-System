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
    public partial class Discount : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Cashier_Menu cm;
        public Discount(Cashier_Menu c)
        {
            InitializeComponent();
            cm = c;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double discount = Double.Parse(textBox1.Text) * (Double.Parse(textBox2.Text)/100);
                textBox3.Text = discount.ToString("#,##0.00");
            }
            catch (Exception ex)
            {
                textBox3.Text = "0.00";
            }
        }

        private void Discount_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            double txt1 = double.Parse(textBox1.Text.ToString());
            double txt3= double.Parse(textBox3.Text.ToString());
            try
            {
                if(txt3 >txt1 )
                {
                    MessageBox.Show("PLEASE ENTER CORRECT PERCENTAGE.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (MessageBox.Show("ADD DISCOUNT?CLICK YES TO CONFIRM.", "Add Discount", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
                    con.Open();
                    SqlCommand cmd = new SqlCommand("update Cart set Discount='" + Double.Parse(textBox3.Text) + "',Discount_Percentage='"+Double.Parse(textBox2.Text)+"'where ID='" + int.Parse(label5.Text) + "'", con);
                    cmd.ExecuteNonQuery();

                    con.Close();
                    cm.loadcart();
                    this.Dispose();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}