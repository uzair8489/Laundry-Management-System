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
    public partial class Settle : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Cashier_Menu cm;
        public Settle(Cashier_Menu c)
        {
            InitializeComponent();
            cm = c;
            loadCustomer();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double sale = double.Parse(textBox1.Text);
                double cash = double.Parse(textBox2.Text);
                double change = cash - sale;
                textBox3.Text = change.ToString("#,##0.00");
            }
            catch(Exception ex)
            {
                textBox3.Text = "0.00";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadCustomer()
        {
            
            con.Open();
            cmd = new SqlCommand("select *from Customer_Details", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Customer_ID"].ToString());
               
            }
            dr.Close();
            con.Close();
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn9.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn6.Text;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn3.Text;
        }

        private void btnc_Click(object sender, EventArgs e)
        {
            textBox2.Clear();
            textBox2.Focus();
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn0.Text;
        }

        private void btn00_Click(object sender, EventArgs e)
        {
            textBox2.Text += btn00.Text;
        }

        private void btnenter_Click(object sender, EventArgs e)
        {
            try
            {
                if(radioButton1.Checked==false && radioButton2.Checked==false)
                {
                    return;
                }
                
                    if (double.Parse(textBox3.Text) < 0 || (textBox3.Text == string.Empty))
                    {
                        MessageBox.Show("INSUFFICIENT AMOUNT.PLEASE ENTER THE CORRECT AMOUNT!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                   
                    else
                    {

                        for (int i = 0; i < cm.dataGridView1.Rows.Count; i++)
                        {
                            //con.Open();
                            //SqlCommand cmd = con.CreateCommand();
                            //cmd.CommandType = CommandType.Text;
                            //cmd.CommandText = "update Cart set Product=Quantity-" + int.Parse(cm.dataGridView1.Rows[i].Cells[6].Value.ToString()) + " where ID='" + cm.dataGridView1.Rows[i].Cells[2].Value.ToString() + "'";
                            //cmd.ExecuteNonQuery();

                            //con.Close();

                            con.Open();
                            SqlCommand cmd1 = con.CreateCommand();
                            cmd1.CommandType = CommandType.Text;
                            cmd1.CommandText = "update Cart set Customer_Name='" + textBox5.Text + "',Customer_Contact='" + textBox4.Text + "',Return_Date='" + dateTimePicker1.Value + "',Status='Done' where ID='" + cm.dataGridView1.Rows[i].Cells[5].Value.ToString() + "'";
                            cmd1.ExecuteNonQuery();
                            con.Close();

                        }
                        Receipt rp = new Receipt(cm);
                        rp.LoadReport(textBox2.Text, textBox3.Text, textBox5.Text, textBox4.Text, dateTimePicker1.Value.ToShortDateString());
                        rp.ShowDialog();

                        MessageBox.Show("PAYMENT SAVED SUCCESSFULLY!", "Payment", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        cm.GetTransNo();
                        cm.loadcart();
                        this.Dispose();
                    }
                
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        private void Settle_Load(object sender, EventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            con.Open();
            cmd = new SqlCommand("select *from Customer_Details where Customer_ID like '" + comboBox1.Text + "'", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                textBox5.Text = dr["Name"].ToString();
                textBox4.Text = dr["Phone_Number"].ToString();
            }
            dr.Close();
            con.Close();
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton1.Checked)
            {
                radioButton2.Checked = false;
                comboBox1.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                dateTimePicker1.Enabled = true;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                radioButton1.Checked = false;
                comboBox1.Enabled = false;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                dateTimePicker1.Enabled = true;
            }
        }
    }
}
