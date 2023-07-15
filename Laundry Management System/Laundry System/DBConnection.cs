using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Laundry_System
{
    public class DBConnection
    {
        SqlConnection con = new SqlConnection(@"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True");
        SqlCommand cmd;
        SqlDataAdapter da;
        SqlDataReader dr;
        //DataTable dt;
        private double dailyorders;
        private int productline;
        private int stockonhand;
        private int critical;
        //private string con;

        public string myconnection()
        {
            string cn = @"Data Source=DESKTOP-868HJSG;Initial Catalog=Laundry_Management;Integrated Security=True";
            return cn;

        }
        public double getDiscount()
        {
            double disc = 0;
            con.ConnectionString = myconnection();
            con.Open();
            cmd = new SqlCommand("select *from Set_Discount", con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                disc = Double.Parse(dr["Discount"].ToString());
            }
            dr.Close();
            con.Close();
            return disc;
        }
        public double getvat()
        {
            double vat=0;
            con.ConnectionString = myconnection();
            con.Open();
            cmd = new SqlCommand("select *from VAT", con);
            dr = cmd.ExecuteReader();
            while(dr.Read())
            { 
                vat = Double.Parse(dr["VAT"].ToString()); 
            }
            dr.Close();
            con.Close();
            return vat;
        }
        public double CriticalStock()
        {

            con.ConnectionString = myconnection();
            con.Open();
            cmd = new SqlCommand("select count(*) from vwCriticalItems ", con);
            critical = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return critical;
        }
        public double StockOnHand()
        {

            con.ConnectionString = myconnection();
            con.Open();
            cmd = new SqlCommand("select isnull(sum(Quantity),0) as Quantity from Products ", con);
            stockonhand = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return stockonhand;
        }
        public double ProductLine()
        {
            
            con.ConnectionString = myconnection();
            con.Open();
            cmd = new SqlCommand("select count(*) from Products ", con);
            productline = int.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return productline;
        }
        public double DailyOrders()
        {
            string sdate = DateTime.Now.ToShortDateString();
            con.ConnectionString = myconnection();
            con.Open();
            cmd = new SqlCommand("select isnull(sum(Total),0) as Total from Cart where S_Date between'" + sdate + "'and'" + sdate + "'and Status like 'Done'", con);
            dailyorders = double.Parse(cmd.ExecuteScalar().ToString());
            con.Close();
            return dailyorders;
        }
        public string GetPassword(string user)
        {
            string password="";
            con.ConnectionString = myconnection();
            con.Open();
            cmd = new SqlCommand("select *from Users where Username='"+ user +"'", con);
            dr = cmd.ExecuteReader();
            dr.Read();
            if(dr.HasRows)
            {
                password = dr["Password"].ToString();
            }
           
            dr.Close();
            con.Close();
            return password;
        }
    }
}

