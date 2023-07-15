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
using Microsoft.Reporting.WinForms;

namespace Laundry_System
{
    public partial class Inventory_Report : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        string _name = "Star Laundry";
        string _address = "G.T Road Sahiwal";
        string _contact = "049400004";
        public Inventory_Report()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void loadCustomer(string sql,string param)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\CustomersReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtCustomers"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oHeader);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtCustomers"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        public void loadDamanged(string sql,string param)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\DamagedReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtDamaged"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oHeader);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtDamaged"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        public void loadExpired(string sql,string param)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\ExpiredReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtExpired"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oHeader);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtExpired"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        public void loadCloth(string sql,string param)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\ClothReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtCloth"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oHeader);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtCloth"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        public void loadService(string sql,string param)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\ServiceReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtService"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oHeader);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtService"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        private void Inventory_Report_Load(object sender, EventArgs e)
        {
            //con.Open();

            //cmd = new SqlCommand("select *from Store_Details", con);
            //dr = cmd.ExecuteReader();
            //dr.Read();
            //if (dr.HasRows)
            //{
            //    label2.Text = dr["Store_Name"].ToString();
            //    label3.Text = dr["Address"].ToString();
            //    label4.Text = dr["Contact"].ToString();
            //}
            //dr.Close();
            //con.Close();
            this.reportViewer1.RefreshReport();
            //textBox1.Text = label2.Text.ToString();
            //textBox2.Text = label3.Text.ToString();
            //textBox3.Text = label4.Text.ToString();
            

            //dr.Close();
            //con.Close();
        }
        public void loadProduct(string sql)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\ProductsReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtProductsReport"]);
                con.Close();

                //ReportParameter oDate = new ReportParameter("oDate", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                //reportViewer1.LocalReport.SetParameters(oDate);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtProductsreport"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        public void TotalOrders(string sql, string param)
        {
            Admin_Portal a = new Admin_Portal();
            
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Total Orders.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtTotalOrders"]);
                con.Close();

                ReportParameter oDate = new ReportParameter("oDate", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oDate);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtTotalOrders"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }

        }
        public void loadCategory(string sql, string param)
        {
            
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Category Report.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtCategory"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oHeader);

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtCategory"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }

        public void loadBrands(string sql, string param)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Brands Report.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtBrands"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);
                //ReportParameter oName = new ReportParameter("oName",label1.Text);

                reportViewer1.LocalReport.SetParameters(oHeader);

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtBrands"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        public void LoadVendors(string sql, string param)
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Vendors Report.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtVendors"]);
                con.Close();

                ReportParameter oHeader = new ReportParameter("oHeader", param);

                reportViewer1.LocalReport.SetParameters(oHeader);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtVendors"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
            
        public void MostOrdered(string sql, string param,string header)
        {
           // Admin_Portal a = new Admin_Portal();
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Most Ordered.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtMostOrdered"]);
                con.Close();

                ReportParameter oDate = new ReportParameter("oDate", param);
                ReportParameter oHeader = new ReportParameter("oHeader", header);
                ReportParameter oName = new ReportParameter("oName",_name );
                ReportParameter oAddress = new ReportParameter("oAddress",_address);
                ReportParameter oContact = new ReportParameter("oContact",_contact);
                
                reportViewer1.LocalReport.SetParameters(oDate);
                reportViewer1.LocalReport.SetParameters(oHeader);
                reportViewer1.LocalReport.SetParameters(oName);
                reportViewer1.LocalReport.SetParameters(oAddress);
                reportViewer1.LocalReport.SetParameters(oContact);
                rptds = new ReportDataSource("DataSet1", ds.Tables["dtMostOrdered"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

            }
        }
        public void LoadReport(string sql)
        {
            Records r = new Records();
            ReportDataSource rptds;
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report3.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();

                con.Open();
                da.SelectCommand = new SqlCommand(sql, con);
                da.Fill(ds.Tables["dtInventory"]);  
                con.Close();

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtInventory"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void LoadStockInReport(string oSql,string param)
        {
            ReportDataSource rptds;
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Stock In Report.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();

                ReportParameter oDate = new ReportParameter("oDate", param);
               
                reportViewer1.LocalReport.SetParameters(oDate);
               

                con.Open();
                da.SelectCommand = new SqlCommand(oSql, con);
                da.Fill(ds.Tables["dtStockIn"]);
                con.Close();

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtStockIn"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void LoadCancelledReport(string oSql, string param)
        {
            ReportDataSource rptds;
            try
            {
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Cancelled Report.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();

                ReportParameter oDate = new ReportParameter("oDate", param);

                reportViewer1.LocalReport.SetParameters(oDate);


                con.Open();
                da.SelectCommand = new SqlCommand(oSql, con);
                da.Fill(ds.Tables["dtCancelledOrder"]);
                con.Close();

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtCancelledOrder"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }
        public void LoadEmployeesReport(string oSql)
        {
            ReportDataSource rptds;
            try
            {
                Store s = new Store();
                reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\EmployeeDetails.rdlc";
                reportViewer1.LocalReport.DataSources.Clear();

                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();

                ReportParameter oAddress = new ReportParameter("oAddress",label1.Text);

                reportViewer1.LocalReport.SetParameters(oAddress);


                con.Open();
                da.SelectCommand = new SqlCommand(oSql, con);
                da.Fill(ds.Tables["dtEmployeesDetails"]);
                con.Close();

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtEmployeesDetails"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 100;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message, "Error");
            }
        }
    }
}
