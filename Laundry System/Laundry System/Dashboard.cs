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
using System.Windows.Forms.DataVisualization.Charting;

namespace Laundry_System
{
    public partial class Dashboard : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        public Dashboard()
        {
            InitializeComponent();
            LoadChart();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }
        public void LoadChart()
        {
            SqlDataAdapter da = new SqlDataAdapter("select Datename(Month,S_Date) as Month,datename(Year,S_Date) as Year, isnull(sum(Total),0.0) as Total from Cart where Status like 'Done' group by Datename(Month,s_Date),Datename(year,S_Date) order by year,month asc  ", con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Orders");
            chart1.DataSource = ds.Tables["Orders"];
            Series series1 = chart1.Series["Series1"];
            series1.ChartType = SeriesChartType.Doughnut;
            series1.Name = "ORDERS";
            var chart = chart1;
            chart.Series[series1.Name].XValueMember = "Month";
            //chart.Series[series1.Name].XValueMember = "Year";
            //double dd = double.Parse(chart.Series[series1.Name].YValueMembers);
            chart.Series[series1.Name].YValueMembers = "Total";
            chart.Series[0].LabelFormat = "{#,##0.00}";

            chart.Series[0].IsValueShownAsLabel = true;
            //chart.Series[0].LegendText

            con.Close();
        }

        private void Dashboard_Resize(object sender, EventArgs e)
        {
            //panel1.Left = (this.Width = panel1.Width) / 2;

            //int intx = Screen.PrimaryScreen.Bounds.Width;
            //int inty = Screen.PrimaryScreen.Bounds.Height;
            //this.Width = intx;
            //this.Height = inty - 40;
            //this.Top = 0;
            //this.Left = 0;
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel8_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel6_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
