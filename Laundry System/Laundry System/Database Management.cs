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
    public partial class Database_Management : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public Database_Management()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                metroTextBox1.Text = fbd.SelectedPath;
                button2.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            
            try
            {
                String database = con.Database.ToString();
                con.Open();
                if (metroTextBox1.Text == string.Empty)
                {
                    //  s.Speak("please enter the valid backup file location");
                    MessageBox.Show("PLEASE ENTER THE BACKUP LOCATION.");
                }
                else
                {

                    string q = "BACKUP DATABASE [" + database + "] TO DISK='" + metroTextBox1.Text + "\\" + "Database" + "-" + DateTime.Now.ToString("yyyy-MM-dd--HH-mm-ss") + ".bak'";

                    SqlCommand scmd = new SqlCommand(q, con);
                    scmd.ExecuteNonQuery();
                    // s.Speak("Backup taken successfully");
                    MessageBox.Show("DATABASE IS BACKED UP SUCCESSFULLY", "Backed Up ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    button2.Enabled = false;

                }
                con.Close();
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "SQL SERVER database backup files|*.bak";
            ofd.Title = "Database Restore";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                metroTextBox2.Text = ofd.FileName;
                button5.Enabled = true;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
           
            try
            {
                String database = con.Database.ToString();
                con.Open();
                string sql1 = string.Format("ALTER DATABASE [" + database + "] SET SINGLE_USER WITH ROLLBACK IMMEDIATE");
                SqlCommand cmd1 = new SqlCommand(sql1, con);
                cmd1.ExecuteNonQuery();

                string sql2 = string.Format("USE MASTER RESTORE DATABASE [" + database + "] FROM DISK='" + metroTextBox2.Text + "' WITH REPLACE;");
                SqlCommand cmd2 = new SqlCommand(sql2, con);
                cmd2.ExecuteNonQuery();

                string sql3 = string.Format("ALTER DATABASE [" + database + "] SET MULTI_USER");
                SqlCommand cmd3 = new SqlCommand(sql3, con);
                cmd3.ExecuteNonQuery();
                // s.Speak("Database Restored successfully");
                MessageBox.Show("DATABASE RESTORED SUCCESSFULLY.", "Restore Database successs", MessageBoxButtons.OK, MessageBoxIcon.Information);
                button2.Enabled = false;
                con.Close();


            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}

