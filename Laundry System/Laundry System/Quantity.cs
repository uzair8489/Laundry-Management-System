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
    public partial class Quantity : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Cashier_Menu cm;
        
        private string Cltcode;
        private double price;
        private string transno;
        private int qty;
        public Quantity(Cashier_Menu cs)
        {
            InitializeComponent();
            cm = cs;
        }
        public void ClothDetails( string Cltcode, string transno)
        {
            this.Cltcode = Cltcode;
            //this.price = price;
            this.transno = transno;
            //this.qty = qty;

        }

        private void Quantity_Load(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
                
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
            if ((e.KeyChar==13)&&(textBox1.Text!=string.Empty))
            {
                string id="";
                bool found = false;
                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "select *from Cart where Transaction_Number='" + transno + "'and Cloth_ID='" + Cltcode + "'";
                SqlDataReader dr;
                dr = cmd1.ExecuteReader();
                dr.Read();
                if(dr.HasRows)
                {
                    found = true;
                    id = dr["ID"].ToString();
                }
                
                else
                {
                    found = false;
                }
                dr.Close();
                cmd1.ExecuteNonQuery();
                con.Close();

                if (found == true)
                {
                    
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update Cart set Quantity=(Quantity + "+ int.Parse(textBox1.Text)+ ") where ID='" + id + "'";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cm.metroTextBox1.Clear();
                    cm.metroTextBox1.Focus();


                    cm.loadcart();
                    this.Dispose();
                }
                else
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "insert into Cart(Transaction_Number,Cloth_ID,Price,Quantity,S_Date,Cashier)values('" + transno + "','" + Cltcode + "','" + price + "','" + int.Parse(textBox1.Text) + "','" + DateTime.Now + "','" + cm.lbluser.Text + "')";
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cm.metroTextBox1.Clear();
                    cm.metroTextBox1.Focus();


                    cm.loadcart();
                    this.Dispose();
                }
                
            }

        }
    }
}
