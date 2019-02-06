using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace Bussiness
{
    class DAL
    {
        //static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=E:\Sharad\Project\BillingWorkstation\BillingWorkstation\BillingDB.mdf;Integrated Security=True");
        static SqlConnection con = new SqlConnection(@"Data Source=(LocalDB)\v11.0;AttachDbFilename=|DataDirectory|\BillingDB.mdf;Integrated Security=True");
        static SqlCommand cmd = null;
        static SqlDataAdapter da = null;
        static SqlDataReader dr = null;
        static DataTable dt = null;

        public static bool insert(string s)
        {
            bool b = false;
            con.Open();
            try
            {
                cmd = new SqlCommand(s,con);
                int c=cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    b = true;
                }
            }
            catch (Exception ee)
            {
                b = false;
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }
        
        public static bool update(string s)
        {
            bool b = false;
            con.Open();
            try
            {
                cmd = new SqlCommand(s,con);
                int c=cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    b = true;
                }
            }
            catch (Exception ee)
            {
                b = false;
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }
        
        public static bool delete(string s)
        {
            bool b = false;
            con.Open();
            try
            {
                cmd = new SqlCommand(s, con);
                int c=cmd.ExecuteNonQuery();
                if (c > 0)
                {
                    b = true;
                }
            }
            catch (Exception ee)
            {
                b = false;
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }

        public static string select(string s)
        {
            string b = "";
            con.Open();
            try
            {
                cmd = new SqlCommand(s, con);
                dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    b = dr[0].ToString();
                }
            }
            catch (Exception ee)
            {
                b = "";
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
            return b;
        }

        public static DataTable show(string s)
        {
            con.Open();
            try
            {
                da = new SqlDataAdapter(s,con);
                dt = new DataTable();
                da.Fill(dt);
                return dt;
            }
            catch (Exception ee)
            {
                throw new Exception(ee.ToString());
            }
            finally
            {
                con.Close();
            }
        }
        public static string ID(string query,string s)
        {
            string ReceivedId = string.Empty;
            string displayString = string.Empty;

            //query = "SELECT MAX(catid) FROM product_category";

            con.Open();

            cmd = new SqlCommand(query, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ReceivedId = dr[0].ToString();
            }

            con.Close();

            if (string.IsNullOrEmpty(ReceivedId))
            {
                ReceivedId = s;//"CUT0000"
            }
            int len = ReceivedId.Length;
            string splitNo = ReceivedId.Substring(3, len - 3); // substrine(startingindex,lengthofstring)
            int num = Convert.ToInt32(splitNo);
            num++;
            displayString = ReceivedId.Substring(0, 3) + num.ToString("0000");// substrine(startingindex,lengthofstring)
            return displayString;
        }

        public DataSet CustomerDetails(string Cusid)

        {
            SqlCommand com = new SqlCommand("Customer_details", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Customer_Id", Cusid);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }

        public DataSet PurchaseDetails(string Poid)
        {
            SqlCommand com = new SqlCommand("Purchase_details", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Poid", Poid);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }

        public DataSet SalesDetails(string Soid)
        {
            SqlCommand com = new SqlCommand("Sales_details", con);

            com.CommandType = CommandType.StoredProcedure;

            com.Parameters.AddWithValue("@Soid", Soid);

            SqlDataAdapter da = new SqlDataAdapter(com);

            DataSet ds = new DataSet();

            da.Fill(ds);

            return ds;

        }
    }
}
