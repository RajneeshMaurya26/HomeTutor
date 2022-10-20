using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace demoproject.Models
{
    public class DBManager
    {
        public string MyCommandText;
        SqlConnection con;
        public DBManager()
        {
            con = new SqlConnection(ConfigurationManager.ConnectionStrings["Mydb"].ToString());
        }
        public bool ExecuteInsertDeleteOrUpdate()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(MyCommandText, con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                int n = cmd.ExecuteNonQuery();
                con.Close();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public object ReadSingleRecord()
        {
            try
            {
                SqlCommand cmd = new SqlCommand(MyCommandText, con);
                if (con.State == ConnectionState.Closed)
                    con.Open();
                object x = cmd.ExecuteScalar();
                return x;
            }
            catch
            {
                return null;
            }
        }
        public DataTable ReadBulkRecord()
        {
            DataTable dt = new DataTable();
            try
            {
                SqlDataAdapter da = new SqlDataAdapter(MyCommandText, con);
                da.Fill(dt);
                return dt;
            }
            catch
            {
                return dt;
            }
        }
    }
}