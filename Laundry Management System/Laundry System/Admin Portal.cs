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
using Tulpep.NotificationWindow;
using Guna.UI2.WinForms;

namespace Laundry_System
{
    public partial class Admin_Portal : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        DBConnection db = new DBConnection();
        public string _pass,_user;
        
        public Admin_Portal()
        {
            InitializeComponent();
            NotifyCriticalItems();
            mdashboard();
            
            
        }
       

        private void button5_Click(object sender, EventArgs e)
        {
            
        }
        public void mdashboard()
        {
            panel1.Controls.Clear();
            Dashboard s = new Dashboard();
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.label1.Text = db.DailyOrders().ToString("#,##0.00");
            s.label6.Text = db.ProductLine().ToString("#,##0");
            s.label9.Text = db.StockOnHand().ToString("#,##0");
            s.label12.Text = db.CriticalStock().ToString("#,##0");
            s.Show();
        }

        private void Admin_Portal_Load(object sender, EventArgs e)
        {
            mdashboard();
            

            

        }
        public void NotifyCriticalItems()
        {
            string critical = "";
            int i = 0;
            con.Open();
            cmd = new SqlCommand("Select count(*) from vwCriticalItems", con);
            string count = cmd.ExecuteScalar().ToString();
            con.Close();
            
            con.Open();
            cmd = new SqlCommand("Select * from vwCriticalItems", con);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                i++;
                critical += i+". " + dr["Product_Description"].ToString() + Environment.NewLine;

            }
            dr.Close();
            con.Close();

            PopupNotifier popup = new PopupNotifier();
            popup.Image = Properties.Resources.icons8_delete_48;
            popup.TitleText = count+ " SHORT STOCK(S)";
            popup.ContentText = critical;
            popup.Popup();
        }

        private void CltDetails_Click(object sender, EventArgs e)
        {
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void StockIn_Click(object sender, EventArgs e)
        {
           
        }

        private void button6_Click(object sender, EventArgs e)
        {
           
        }

        private void button8_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
           
        }

        private void button9_Click(object sender, EventArgs e)
        {
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
        {
            mdashboard();
            
        }

        private void button10_Click(object sender, EventArgs e)
        {
            
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

       

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            Database_Management s = new Database_Management();
            s.TopLevel = false;
            panel1.Controls.Add(s);

            s.Show();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("LOGOUT FROM APPLICATION?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                this.Hide();
                Security f = new Security();
                f.ShowDialog();
            }
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Product_List s = new Product_List();
            s.TopLevel = false;
            panel1.Controls.Add(s);

            s.Show();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            //mdashboard();
            panel1.Controls.Clear();
            Dashboard s = new Dashboard();
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.label1.Text = db.DailyOrders().ToString("#,##0.00");
            s.label6.Text = db.ProductLine().ToString("#,##0");
            s.label9.Text = db.StockOnHand().ToString("#,##0");
            s.label12.Text = db.CriticalStock().ToString("#,##0");
            s.Show();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Clothes_Details l = new Clothes_Details();
            l.TopLevel = false;
            panel1.Controls.Add(l);
            l.BringToFront();
            l.Show();
        }

        private void guna2Button3_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();

            Vendors_List s = new Vendors_List();
            s.LoadRecord();
            s.TopLevel = false;
            panel1.Controls.Add(s);

            s.Show();
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Stock_In s = new Stock_In(this);
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            s.Dock = DockStyle.Fill;
            s.Show();
        }

        private void guna2Button7_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Category_List l = new Category_List();
            l.TopLevel = false;
            panel1.Controls.Add(l);
            //l.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            //l.Dock = DockStyle.Fill;
            l.BringToFront();
            l.Show();
        }

        private void guna2Button8_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Brand_List s = new Brand_List();
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            s.Dock = DockStyle.Fill;
            s.Show();
        }

        private void guna2Button9_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Records l = new Records();
            l.TopLevel = false;
            l.LoadRecord();
            l.TotalOrders();
            l.loadCriticalItems();
            l.LoadInventry();
            l.CancelledOrders();
            l.loadStockInHistory();
            panel1.Controls.Add(l);
            l.BringToFront();
            l.Show();
        }

        private void guna2Button10_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Orders_Done s = new Orders_Done();
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.suser = lbluser.Text;
            s.Show();
        }

        private void guna2Button11_Click(object sender, EventArgs e)
        {

            Store s = new Store();
            s.LoadRecord();
            s.Show();
        }

        private void guna2Button12_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            User_Account s = new User_Account(this);
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.textBox8.Text = _user;
            s.Show();
        }

        private void Admin_Portal_Resize(object sender, EventArgs e)
        {
            int intx = Screen.PrimaryScreen.Bounds.Width;
            int inty = Screen.PrimaryScreen.Bounds.Height;
            this.Width = intx;
            this.Height = inty-40;
            this.Top = 0;
            this.Left = 0;

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            //panel3.HorizontalScroll.Enabled = false;
        }

        private void guna2Button13_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Employees_Details s = new Employees_Details();
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.Show();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void guna2Button14_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            Customer_Details s = new Customer_Details();
            s.TopLevel = false;
            panel1.Controls.Add(s);
            s.Show();
        }

        private void guna2Button6_Click(object sender, EventArgs e)
        {
            Stock_Adjustment f = new Stock_Adjustment();
            f.load();
            f.textBox6.Text = lbluser.Text;
            f.ReferenceNo();
            f.ShowDialog();
        }
    }

}
