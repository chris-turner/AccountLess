using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace AccountLess.Models
{
    public class GeneralDataAccess
    {
        private string connectionString = 

            public DataSet GetDataSet(string SQLQuery)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = SQLQuery;
            da.SelectCommand = cmd;
            DataSet ds = new DataSet();   

            conn.Open();
            da.Fill(ds);
            conn.Close();

            return ds;
        }

        public int runSQLQuery(string SQLQuery)
        {
            SqlConnection conn = new SqlConnection(connectionString);
            
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandText = SQLQuery;
            

            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }

        public string getJSONFromURL(string url)
        {
            string json = "";
            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString(url);
            }
            return json;
        }


    }
}
