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
    public partial class Void : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Cancel_Details f;
       
        public Void(Cancel_Details fm)
        {
            InitializeComponent();
            f = fm;
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Void_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            

}
        public void SaveCancelOrder( string user)
        {
            double disc = double.Parse(f.textdisc.Text) / double.Parse(f.textqty.Text);
            con.Open();
            cmd = new SqlCommand("insert into Cancelled_Orders(Transaction_Number,Cloth_ID,Price,Quantity,Service,Charges,Discount,S_Date,Void_By,Cancelled_By,Reason) " +
                                                      "Values('" + f.texttrans.Text + "','" + f.textcltID.Text + "','" +double.Parse(f.textprice.Text) + "','" +int.Parse(f.textcanqty.Text) + "','" + f.txtservice.Text + "','" + double.Parse(f.txtcharges.Text) + "','" + disc + "','" + DateTime.Now + "','" + user + "','"+f.textcancel.Text+"','" + f.textreason.Text + "')",con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void updateData(string sql)
        {
            con.Open();
            cmd = new SqlCommand(sql, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {

                if (metroTextBox2.Text != string.Empty)
                {
                    string user;
                    con.Open();
                    cmd = new SqlCommand("Select *from Users where Username='" + metroTextBox1.Text + "' and Password='" + metroTextBox2.Text + "'", con);
                    dr = cmd.ExecuteReader();
                    dr.Read();
                    if (dr.HasRows)
                    {

                        user = dr["Username"].ToString();
                        dr.Close();
                        con.Close();
                        SaveCancelOrder(user);
                        if (f.comboBox1.Text == "Yes")
                        {
                            updateData("update Products set Quantity=Quantity+" + int.Parse(f.textcanqty.Text) + " where Product_Code='" + f.textcltID.Text + "'");
                        }
                        updateData("update Cart set Quantity=Quantity-'" + int.Parse(f.textcanqty.Text) + "' where ID like '" + f.textID.Text + "'");

                        MessageBox.Show("ORDER CANCELLED SUCCESSFULLY!", "Cancel Order", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Dispose();
                        f.RefreshList();
                        
                        f.Dispose();
                    }
                    dr.Close();
                    con.Close();
                }
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");

            }
        }
    }
}
