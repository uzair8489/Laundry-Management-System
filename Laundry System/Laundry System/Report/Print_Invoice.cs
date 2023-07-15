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

namespace Laundry_System.Report
{
    public partial class Print_Invoice : Form
    {
        string invoice_Id;
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public Print_Invoice(string _id)
        {
            invoice_Id = _id;
            InitializeComponent();
        }

        private void Print_Invoice_Load(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select *from view_All_Bill where Invoice_ID='" + invoice_Id + "'", con);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "DataTable1_Invoice");
            ReportDataSource dataSource = new ReportDataSource("DataSet_Report", ds.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();

            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("select *from view_All_Bill where Invoice_ID='" + textBox1.Text + "'",con);
            DataSet1 ds = new DataSet1();
            da.Fill(ds, "DataTable1_Invoice");
            ReportDataSource dataSource = new ReportDataSource("DataSet_Report",ds.Tables[0]);
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.DataSources.Add(dataSource);
            this.reportViewer1.RefreshReport();

        }
    }
}
