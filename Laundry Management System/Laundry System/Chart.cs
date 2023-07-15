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

    public partial class Chart : Form
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-K7JS32A;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        DataTable dt;
        SqlDataReader dr;
        public Chart()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        public void LoadChartOrdered(string sql)
        {
            SqlDataAdapter da;
            con.Open();
            da = new SqlDataAdapter(sql, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "ORDERED");
            chart1.DataSource = ds.Tables["ORDERED"];
            Series series = chart1.Series[0];
            series.ChartType = SeriesChartType.Pie;

            series.Name = "ORDERED CLOTHES";
            chart1.Series[0].XValueMember = "Cloth_Type";
            chart1.Series[0].YValueMembers = "Total";
            chart1.Series[0].LabelFormat = "{#,##0.00}";
            chart1.Series[0].IsValueShownAsLabel = true;
            con.Close();
        }

    }
}
