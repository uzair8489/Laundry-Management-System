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
    public partial class Cancel_Details : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Orders_Done f;
        public Cancel_Details(Orders_Done fm)
        {
            InitializeComponent();
            f = fm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void Cancel_Details_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        public void RefreshList()
        {
            f.LoadNotPickedRecord();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if(/*(comboBox1.Text!=string.Empty)&&*/ (textqty.Text!=string.Empty) && (textreason.Text!=string.Empty))
                {
                    if(int.Parse(textqty.Text) >= int.Parse(textcanqty.Text))
                    {
                        Void v = new Void(this);
                        v.ShowDialog();
                    }
                   
                }
            }
            catch(Exception ex)
            {
                
                MessageBox.Show(ex.Message);
            }
        }

        private void textcanqty_TextChanged(object sender, EventArgs e)
        {

        }

        private void textcanqty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }
    }
}
