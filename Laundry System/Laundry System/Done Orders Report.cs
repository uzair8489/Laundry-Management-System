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
    public partial class Done_Orders_Report : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        Orders_Done f;
        public Done_Orders_Report(Orders_Done fm)
        {
            InitializeComponent();
            f = fm;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }


        private void Done_Orders_Report_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
           
        }


        public void LoadReport()
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Done Orders.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                if (f.metroTextBox1.Text == string.Empty)
                {
                    if (f.comboBox1.Text == "All Cashiers")
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Picked' and S_Date between '" + f.dateTimePicker1.Value + "'and '" + f.dateTimePicker2.Value + "'", con);
                    }
                    else
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Picked' and S_Date between '" + f.dateTimePicker1.Value + "'and '" + f.dateTimePicker2.Value + "'and Cashier Like '" + f.comboBox1.Text + "'", con);
                    }
                }
                if(f.metroTextBox1.Text != string.Empty)
                {
                    if (f.comboBox1.Text == "All Cashiers")
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Picked' and S_Date between '" + f.dateTimePicker1.Value + "'and '" + f.dateTimePicker2.Value + "'and Transaction_Number like '"+f.metroTextBox1.Text+"%'", con);
                    }
                    else
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Picked' and S_Date between '" + f.dateTimePicker1.Value + "'and '" + f.dateTimePicker2.Value + "'and Cashier Like '" + f.comboBox1.Text + "' and Transaction_Number like '" + f.metroTextBox1.Text + "%'", con);
                    }
                }
                
                da.Fill(ds.Tables["dtOrdersDoneReport"]);
                con.Close();
                ReportParameter oDate = new ReportParameter("oDate", "Date From: " + f.dateTimePicker1.Value.ToShortDateString() + " To: " + f.dateTimePicker2.Value.ToShortDateString());
                ReportParameter oCashier = new ReportParameter("oCashier", "Cashier: " + f.comboBox1.Text);
                ReportParameter oHeader = new ReportParameter("oHeader", "ORDERS REPORT");

                reportViewer1.LocalReport.SetParameters(oDate);
                reportViewer1.LocalReport.SetParameters(oCashier);
                reportViewer1.LocalReport.SetParameters(oHeader);

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtOrdersDoneReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
        public void LoadNotPicked()
        {
            try
            {
                ReportDataSource rptds;

                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\OrderNotPickedReport.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                if (f.metroTextBox2.Text == string.Empty)
                {
                    if (f.comboBox2.Text == "All Cashiers")
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Not Picked' and S_Date between '" + f.dateTimePicker4.Value + "'and '" + f.dateTimePicker3.Value + "'", con);
                    }
                    else
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Not Picked' and S_Date between '" + f.dateTimePicker4.Value + "'and '" + f.dateTimePicker3.Value + "'and Cashier Like '" + f.comboBox2.Text + "'", con);
                    }
                }
                if (f.metroTextBox2.Text != string.Empty)
                {
                    if (f.comboBox2.Text == "All Cashiers")
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Not Picked' and S_Date between '" + f.dateTimePicker4.Value + "'and '" + f.dateTimePicker3.Value + "'and Transaction_Number like '" + f.metroTextBox2.Text + "%'", con);
                    }
                    else
                    {
                        da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Customer_Name,c.Customer_Contact,cl.Cloth_Type,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Return_Date,c.Picked from Cart as c inner join Clothes_Details as cl on c.Cloth_ID=cl.Cloth_ID where status like 'Done' and Picked like 'Not Picked' and S_Date between '" + f.dateTimePicker4.Value + "'and '" + f.dateTimePicker3.Value + "'and Cashier Like '" + f.comboBox2.Text + "' and Transaction_Number like '" + f.metroTextBox1.Text + "%'", con);
                    }
                }

                da.Fill(ds.Tables["dtOrdersDoneNotPickedReport"]);
                con.Close();
                ReportParameter oDate = new ReportParameter("oDate", "Date From: " + f.dateTimePicker4.Value.ToShortDateString() + " To: " + f.dateTimePicker3.Value.ToShortDateString());
                ReportParameter oCashier = new ReportParameter("oCashier", "Cashier: " + f.comboBox1.Text);
                ReportParameter oHeader = new ReportParameter("oHeader", "ORDERS NOT PICKED REPORT");

                reportViewer1.LocalReport.SetParameters(oDate);
                reportViewer1.LocalReport.SetParameters(oCashier);
                reportViewer1.LocalReport.SetParameters(oHeader);

                rptds = new ReportDataSource("DataSet1", ds.Tables["dtOrdersDoneNotPickedReport"]);
                reportViewer1.LocalReport.DataSources.Add(rptds);
                reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                reportViewer1.ZoomMode = ZoomMode.Percent;
                reportViewer1.ZoomPercent = 50;
            }
            catch (Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }
    }
}
