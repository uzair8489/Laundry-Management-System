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
    public partial class Employees_Details : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        string imageUrl = null;
        public Employees_Details()
        {
            InitializeComponent();
            GetTransNo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            //tabPage2.
            //metroTabControl1.e
        }
        public void clear()
        {
            //txteid.Clear();
            txtfirst.Clear();
            txtlast.Clear();
            txtphon.Clear();
            txtemail.Clear();
            txtage.Clear();
            txtcnic.Clear();
            txtcity.Clear();
            cmbgend.Text = "";
            txtaddress.Clear();
            //pictureBox1.Image = null;
            

            //txteid.Clear();
            txtskills.Clear();
            txtcertif.Clear();
            txtexperienc.Clear();
           
            txtdepart.Clear();
            txtdesg.Clear();
            txtsal.Clear();
            dtjoin.Text = "";
            dtdutystart.Text = "";
            dtdutyend.Text = "";
            dtvac.Text="";
            //txtstatus.Clear();
            
            txtprevorg.Clear();
            txtprevdes.Clear();
            txtprevsal.Clear();
            dtprevjoin.Text = "";
            dtprevleav.Text = "";

        }

        private void button3_Click(object sender, EventArgs e)
        {
            
            //txtstatus.Enabled = true;
        }

        private void Employees_Details_Load(object sender, EventArgs e)
        {
            loadRecord();
            //button8.Enabled = false;
           // Admin_Portal s = new Admin_Portal();
            
           

        }
        public void GetTransNo()
        {

            try
            {

                int count;
                string emp;
                string eid = "EMP";
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select top 1 Employee_ID from Employee_Personal_Details where Employee_ID like '" + eid + "%' order by Employee_ID desc";
                dr = cmd.ExecuteReader();
                dr.Read();
                if (dr.HasRows)
                {
                    emp = dr[0].ToString();
                    count = int.Parse(emp.Substring(3, 3));
                    txteid.Text = eid + (count + 1);


                }
                else
                {
                    emp = eid + "101";
                    txteid.Text = emp;

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

        private void button7_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog of = new OpenFileDialog())
            {
                if(of.ShowDialog()==DialogResult.OK)
                {
                    imageUrl = of.FileName;
                    pictureBox1.Image = Image.FromFile(of.FileName);

                }
            }
        }
        
        public void loadRecord()
        {
            con.Open();
            SqlCommand cmd = con.CreateCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select e.Employee_ID,e.First_Name,e.Last_Name,e.Phone_Number,e.CNIC,e.Email,e.Age,e.Gender,e.Address,e.City,em.Joining_Date,em.Department,em.Designation,em.Salary,em.Duty_Start_Time,em.Duty_End_Time,em.Allowed_Vacations,em.Status from Employee_Personal_Details as e inner join Employee_Duty_Details as em on e.Employee_ID=em.Employee_ID ";
            cmd.ExecuteNonQuery();
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt;
            con.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Image img = pictureBox1.Image;
            byte[] arr;
            ImageConverter converter = new ImageConverter();
            arr = (byte[])converter.ConvertTo(img, typeof(byte[]));
            
            try
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into Employee_Personal_Details(Employee_ID,First_Name,Last_Name,Phone_Number,CNIC,Email,Age,Gender,Address,City,Photo,PhotoURL) values ('" + txteid.Text + "','" + txtfirst.Text + "','" + txtlast.Text + "','" + txtphon.Text + "','" + txtcnic.Text + "','" + txtemail.Text + "','" + txtage.Text + "','" + cmbgend.Text + "','" + txtaddress.Text + "','" + txtcity.Text + "','" + arr + "','" + imageUrl + "')";
                cmd.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand cmd1 = con.CreateCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "insert into Employee_Professional_Details(Employee_ID,Skills,Certificates,Experience) values('" + txteid.Text + "','" + txtskills.Text + "','" + txtcertif.Text + "','" + txtexperienc.Text + "')";
                cmd1.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand cmd2 = con.CreateCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.CommandText = "insert into Employee_Duty_Details(Employee_ID,Department,Designation,Salary,Joining_Date,Duty_Start_Time,Duty_End_Time,Allowed_Vacations) values('" + txteid.Text + "','" + txtdepart.Text + "','" + txtdesg.Text + "','" + txtsal.Text + "','" + dtjoin.Value + "','" + dtdutystart.Text + "','" + dtdutyend.Text + "','" + dtvac.Text + "')";
                cmd2.ExecuteNonQuery();
                con.Close();

                con.Open();
                SqlCommand cmd3 = con.CreateCommand();
                cmd3.CommandType = CommandType.Text;
                cmd3.CommandText = "insert into Employee_Prevoious_Experience(Employee_ID,Employee_Organization,Employee_Designation,Employee_Salary,Employee_Joining_Date,Employee_Leaving_Date) values('" + txteid.Text + "','" + txtprevorg.Text + "','" + txtprevdes.Text + "','" + txtprevsal.Text + "','" + dtprevjoin.Value + "','" + dtprevleav.Value + "')";
                cmd3.ExecuteNonQuery();
                con.Close();

                MessageBox.Show("RECORD SAVED SUCCESSFULLY","Record Saved",MessageBoxButtons.OK,MessageBoxIcon.Information);
                clear();
                GetTransNo();
                loadRecord();
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
           if(txteid.Text != string.Empty && txtfirst.Text != string.Empty && txtlast.Text != string.Empty && txtphon.Text != string.Empty && txtemail.Text != string.Empty && cmbgend.Text != string.Empty && txtaddress.Text!=string.Empty && txtcity.Text!=string.Empty)
            {
                metroTabControl3.SelectTab(tabPage6);
                txtskills.Enabled = true;
                txtcertif.Enabled = true;
                txtexperienc.Enabled = true;
                button4.Enabled = true;
            }
            
        }

        private void txteid_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (txtskills.Text != string.Empty && txtcertif.Text != string.Empty && txtexperienc.Text != string.Empty)
            {
                metroTabControl3.SelectTab(tabPage7);
                txtdepart.Enabled = true;
                txtdesg.Enabled = true;
                txtsal.Enabled = true;
                dtjoin.Enabled = true;
                dtdutystart.Enabled = true;
                dtdutyend.Enabled = true;
                dtvac.Enabled = true;
                button5.Enabled = true;
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (txtdepart.Text != string.Empty && txtdesg.Text != string.Empty && txtsal.Text != string.Empty && dtjoin.Text != string.Empty && dtdutystart.Text != string.Empty && dtdutyend.Text != string.Empty && dtvac.Text != string.Empty )
            {
                metroTabControl3.SelectTab(tabPage8);
                txtprevdes.Enabled = true;
                txtprevorg.Enabled = true;
                txtprevsal.Enabled = true;
                dtprevjoin.Enabled = true;
                dtprevleav.Enabled = true;
                button6.Enabled = true;
            }
        }

        private void txteid_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtphon_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtemail_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtemail_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = (e.KeyChar == (char)Keys.Space);
        }

        private void txtage_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void cmbgend_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void txtsal_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtsal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtvac_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtvac_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void txtprevsal_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtfirst_TextChanged(object sender, EventArgs e)
        {
           
        }

        private void txtfirst_KeyPress(object sender, KeyPressEventArgs e)
        {

            //e.Handled = (e.KeyChar == (char)Keys.Space);
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void metroTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void metroTextBox1_TextChanged(object sender, EventArgs e)
        {
            if (metroComboBox1.Text == "SEARCH BY EMPLOYEE ID")
            {
                
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select e.Employee_ID,e.First_Name,e.Last_Name,e.Phone_Number,e.CNIC,e.Email,e.Age,e.Gender,e.Address,e.City,em.Joining_Date,em.Department,em.Designation,em.Salary,em.Duty_Start_Time,em.Duty_End_Time,em.Allowed_Vacations,em.Status from Employee_Personal_Details as e inner join Employee_Duty_Details as em on e.Employee_ID=em.Employee_ID where e.Employee_ID like'" + metroTextBox1.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            
            if (metroComboBox1.Text == "SEARCH BY NAME")
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select e.Employee_ID,e.First_Name,e.Last_Name,e.Phone_Number,e.CNIC,e.Email,e.Age,e.Gender,e.Address,e.City,em.Joining_Date,em.Department,em.Designation,em.Salary,em.Duty_Start_Time,em.Duty_End_Time,em.Allowed_Vacations,em.Status from Employee_Personal_Details as e inner join Employee_Duty_Details as em on e.Employee_ID=em.Employee_ID where e.First_Name like'" + metroTextBox1.Text + "%' or e.Last_Name like'" + metroTextBox1.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            if (metroComboBox1.Text == string.Empty)
            {
                con.Open();
                SqlCommand cmd = con.CreateCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "select e.Employee_ID,e.First_Name,e.Last_Name,e.Phone_Number,e.CNIC,e.Email,e.Age,e.Gender,e.Address,e.City,em.Joining_Date,em.Department,em.Designation,em.Salary,em.Duty_Start_Time,em.Duty_End_Time,em.Allowed_Vacations,em.Status from Employee_Personal_Details as e inner join Employee_Duty_Details as em on e.Employee_ID=em.Employee_ID where e.Employee_ID like'" + metroTextBox1.Text + "%'";
                cmd.ExecuteNonQuery();
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dataGridView1.DataSource = dt;
                con.Close();
            }
            
            
           
        }

        private void metroComboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            string p, l="", m="", n="";
            if (metroComboBox1.Text == string.Empty)
            {
                Inventory_Report f = new Inventory_Report();
                f.LoadEmployeesReport("select e.Employee_ID,e.First_Name,e.Last_Name,e.Phone_Number,e.CNIC,e.Email,e.Age,e.Gender,e.Address,e.City,em.Joining_Date,em.Department,em.Designation,em.Salary,em.Duty_Start_Time,em.Duty_End_Time,em.Allowed_Vacations,em.Status from Employee_Personal_Details as e inner join Employee_Duty_Details as em on e.Employee_ID=em.Employee_ID where e.Employee_ID like'" + metroTextBox1.Text + "%'");
                f.ShowDialog();
            }

            if (metroComboBox1.Text == "SEARCH BY EMPLOYEE ID")
            {
                Inventory_Report f = new Inventory_Report();
                f.LoadEmployeesReport("select e.Employee_ID,e.First_Name,e.Last_Name,e.Phone_Number,e.CNIC,e.Email,e.Age,e.Gender,e.Address,e.City,em.Joining_Date,em.Department,em.Designation,em.Salary,em.Duty_Start_Time,em.Duty_End_Time,em.Allowed_Vacations,em.Status from Employee_Personal_Details as e inner join Employee_Duty_Details as em on e.Employee_ID=em.Employee_ID where e.Employee_ID like'" + metroTextBox1.Text + "%'");
                f.ShowDialog();
            }

            if (metroComboBox1.Text == "SEARCH BY NAME")
            {
                Inventory_Report f = new Inventory_Report();
                f.LoadEmployeesReport("select e.Employee_ID,e.First_Name,e.Last_Name,e.Phone_Number,e.CNIC,e.Email,e.Age,e.Gender,e.Address,e.City,em.Joining_Date,em.Department,em.Designation,em.Salary,em.Duty_Start_Time,em.Duty_End_Time,em.Allowed_Vacations,em.Status from Employee_Personal_Details as e inner join Employee_Duty_Details as em on e.Employee_ID=em.Employee_ID where e.First_Name like'" + metroTextBox1.Text + "%'or e.Last_Name like'" + metroTextBox1.Text + "%'");
                f.ShowDialog();
            }
            
        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtlast_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !(char.IsLetter(e.KeyChar) || e.KeyChar == (char)Keys.Back || e.KeyChar == (char)Keys.Space);
        }

        private void txtcnic_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void dtdutystart_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dtdutyend_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.CurrentCell.ColumnIndex.ToString());
            string colname = dataGridView1.Columns[e.ColumnIndex].Name;
            if (colname == "Delete")
            {
                if (MessageBox.Show("Are You Sure To Delete The Selected Data?", "Delete", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {

                    con.Open();
                    SqlCommand cmd1 = con.CreateCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "delete from Employee_Personal_Details where Employee_ID like '" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'";
                    cmd1.ExecuteNonQuery();
                    con.Close();
                    con.Open();

                    SqlCommand cmd2 = con.CreateCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "delete from Employee_Professional_Details where Employee_ID like '" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'";
                    cmd2.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd3 = con.CreateCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.CommandText = "delete from Employee_Duty_Details where Employee_ID like '" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'";
                    cmd3.ExecuteNonQuery();
                    con.Close();

                    con.Open();
                    SqlCommand cmd4 = con.CreateCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.CommandText = "delete from Employee_Prevoious_Experience where Employee_ID like '" + dataGridView1.Rows[e.RowIndex].Cells[3].Value + "'";
                    cmd4.ExecuteNonQuery();
                    con.Close();

                    loadRecord();
                    MessageBox.Show("RECORD DELETED SUCCESSFULLY.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
                if (colname == "Edit")
                {
                    //MessageBox.Show("Hey");

                    Employee_Data_Updation s = new Employee_Data_Updation(this);
                s.txteid.Text = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                s.txtfirst.Text = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                s.txtlast.Text = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();
                s.txtphon.Text = dataGridView1.Rows[e.RowIndex].Cells[6].Value.ToString();
                s.txtcnic.Text = dataGridView1.Rows[e.RowIndex].Cells[7].Value.ToString();
                s.txtemail.Text = dataGridView1.Rows[e.RowIndex].Cells[8].Value.ToString();
                s.txtage.Text = dataGridView1.Rows[e.RowIndex].Cells[9].Value.ToString();
                s.cmbgend.Text = dataGridView1.Rows[e.RowIndex].Cells[10].Value.ToString();
                s.txtaddress.Text = dataGridView1.Rows[e.RowIndex].Cells[11].Value.ToString();
                s.txtcity.Text = dataGridView1.Rows[e.RowIndex].Cells[12].Value.ToString();

                s.txtdepart.Text = dataGridView1.Rows[e.RowIndex].Cells[14].Value.ToString();
                s.txtdesg.Text= dataGridView1.Rows[e.RowIndex].Cells[15].Value.ToString();
                s.txtsal.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                //s.dtjoin.Text = dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString();
                s.dtdutystart.Text = dataGridView1.Rows[e.RowIndex].Cells[17].Value.ToString();
                s.dtdutyend.Text = dataGridView1.Rows[e.RowIndex].Cells[18].Value.ToString();
                s.dtvac.Text= dataGridView1.Rows[e.RowIndex].Cells[19].Value.ToString();
                s.comboBox1.Text= dataGridView1.Rows[e.RowIndex].Cells[20].Value.ToString();

                s.Show();

            }

        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            this.dataGridView1.Rows[e.RowIndex].Cells[0].Value = (e.RowIndex + 1).ToString();
        }

        private void panel16_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
