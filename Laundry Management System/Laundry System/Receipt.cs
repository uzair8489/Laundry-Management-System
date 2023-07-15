using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;

namespace Laundry_System
{
    public partial class Receipt : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        Cashier_Menu cm;
        string store = "STAR LAUNDRY";
        string address = "G.T Road Sahiwal";
        public Receipt(Cashier_Menu c)
        {
            InitializeComponent();
            cm = c;
        }

        private void Receipt_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
            
        }
        public void LoadReport(string ocash,string ochange,string oCName,string oCnum,string oRet)
        {
            ReportDataSource rptDatasource;
            try
            {
                this.reportViewer1.LocalReport.ReportPath = Application.StartupPath + @"\Reports\Report2.rdlc";
                this.reportViewer1.LocalReport.DataSources.Clear();
                //DataSet ds = new DataSet();
                Model1 ds = new Model1();
                SqlDataAdapter da = new SqlDataAdapter();
                con.Open();
                da.SelectCommand = new SqlCommand("select c.ID,c.Transaction_Number,c.Cloth_ID,c.Price,c.Quantity,c.Service,c.Charges,c.Discount,c.Total,c.S_Date,c.Status,clt.Cloth_Type from cart as c inner join Clothes_Details as clt on clt.Cloth_ID=c.Cloth_ID where Transaction_Number like '"+cm.label2.Text+"'", con);
                da.Fill(ds.Tables["dtdone"]);
                con.Close();

                ReportParameter oVatable = new ReportParameter("oVatable",cm.label12.Text);
                ReportParameter oVat = new ReportParameter("oVat", cm.label10.Text);
                ReportParameter oDiscount = new ReportParameter("oDiscount", cm.label8.Text);
                ReportParameter ototal = new ReportParameter("ototal", cm.label15.Text);
                ReportParameter oCash = new ReportParameter("oCash", ocash);
                ReportParameter oChange = new ReportParameter("oChange", ochange);
                ReportParameter oStore = new ReportParameter("oStore", store);
                ReportParameter oAddress = new ReportParameter("oAddress", address);
                ReportParameter oTransaction = new ReportParameter("oTransaction","Invoice #: " + cm.label2.Text);
                ReportParameter oCashier = new ReportParameter("oCashier", cm.lbluser.Text);
                ReportParameter oReturn = new ReportParameter("oReturn", oRet);
                ReportParameter oCustName = new ReportParameter("oCustName", oCName);
                ReportParameter oCustNum = new ReportParameter("oCustNum", oCnum);



                reportViewer1.LocalReport.SetParameters(oVatable);
                reportViewer1.LocalReport.SetParameters(oVat);
                reportViewer1.LocalReport.SetParameters(oDiscount);
                reportViewer1.LocalReport.SetParameters(ototal);
                reportViewer1.LocalReport.SetParameters(oCash);
                reportViewer1.LocalReport.SetParameters(oChange);
                reportViewer1.LocalReport.SetParameters(oStore);
                reportViewer1.LocalReport.SetParameters(oAddress);
                reportViewer1.LocalReport.SetParameters(oTransaction);
                reportViewer1.LocalReport.SetParameters(oCashier);
                reportViewer1.LocalReport.SetParameters(oReturn);
                reportViewer1.LocalReport.SetParameters(oCustName);
                reportViewer1.LocalReport.SetParameters(oCustNum);


                rptDatasource = new ReportDataSource("DataSet1",ds.Tables["dtdone"]);
                reportViewer1.LocalReport.DataSources.Add(rptDatasource);
                //reportViewer1.SetDisplayMode(Microsoft.Reporting.WinForms.DisplayMode.PrintLayout);
                //reportViewer1.ZoomMode = ZoomMode.Percent;
                //reportViewer1.ZoomPercent = 100;

            }
            catch(Exception ex)
            {
                con.Close();
                MessageBox.Show(ex.Message);
            }
        }

        private void reportViewer1_Load(object sender, EventArgs e)
        {

        }
    }
}
