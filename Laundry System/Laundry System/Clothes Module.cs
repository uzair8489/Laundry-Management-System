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
    public partial class Edit_Cloth_Details : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Clothes_Details cl;

        public Edit_Cloth_Details(Clothes_Details c)
        {
            InitializeComponent();
            cl = c;
            GetTransNo();
        }
       
        
        private void Edit_Cloth_Details_Load(object sender, EventArgs e)
        {
            
            

            
        }
        public void GetTransNo()
        {

            try
            {

                int count;
                string clt;
                string cltid = "CLT";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 1 Cloth_ID from Clothes_Details where Cloth_ID like '" + cltid + "%' order by Cloth_ID desc";
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    clt = dr[0].ToString();
                    count = int.Parse(clt.Substring(3, 3));
                    textBox2.Text = cltid + (count + 1);


                }
                else
                {
                    clt = cltid + "101";
                    textBox2.Text = clt;

                }
                dr.Close();
                con.Close();

            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadRecord()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select *from Clothes_Details";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            cl.dataGridView1.DataSource = dt;
            con.Close();
        }

        public void clear()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            button6.Enabled = false;
            button7.Enabled = false;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are You Sure To Update This Record", "Update Details", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                textBox2.Enabled = false;
                int price = Convert.ToInt32(textBox3.Text);

                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "update Clothes_Details set Cloth_Type='" + textBox1.Text + "',Price='" + price + "' where Cloth_ID='" + textBox2.Text + "'";
                cmd.ExecuteNonQuery();
                con.Close();
                LoadRecord();
                MessageBox.Show("Record Updated Successfully");
                //clear();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            textBox2.Enabled = true;
            //int price = Convert.ToInt32(textBox3.Text);
            if (textBox1.Text != "" && textBox2.Text != "" && textBox3.Text != "" && textBox4.Text != "")
            {
                try
                {
                    SqlDataAdapter da = new SqlDataAdapter("Select Cloth_Type from Clothes_Details where Cloth_Type='" + textBox1.Text + "'", con);
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count >= 1)
                    {
                        MessageBox.Show("CLOTH TYPE ALREADY EXISTS", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    else
                    {
                        con.Open();
                        SqlCommand cmd = con.CreateCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "insert into Clothes_Details (Cloth_ID,Barcode,Cloth_Type,Price) values ('" + textBox2.Text + "','" + textBox4.Text + "','" + textBox1.Text + "','" + textBox3.Text + "')";
                        cmd.ExecuteNonQuery();
                        con.Close();
                        LoadRecord();
                        MessageBox.Show("Record Saved Successfully.");
                        textBox1.Clear();
                        textBox2.Clear();
                        textBox3.Clear();
                        textBox4.Clear();
                        button6.Enabled = false;
                        button7.Enabled = true;
                        clear();
                        GetTransNo();
                    }
                }
                catch (Exception ex)
                {
                    con.Close();
                    MessageBox.Show(ex.Message);
                }
            }

        }

        private void textBox3_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            clear();
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void textBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (textBox2.Text.Length == 0 && e.KeyChar == ' ')
            {
                e.Handled = true;
            }
            else
            {
                e.Handled = false;
            }
            
                
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
