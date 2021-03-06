﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace AccountLess.Models
{
    public class GeneralDataAccess
    {


        public DataSet GetDataSet(string SQLQuery)
        {
            AppSettings ap = new AppSettings();
            string connectionString = ap.ConnectionString;
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
            AppSettings ap = new AppSettings();
            string connectionString = ap.ConnectionString;
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

        public string callAPIWithWebReq(string url, List<KeyValuePair<string, string>> headers)
        {
            WebRequest req = WebRequest.Create(url);
            req.ContentType = "application/json";

            foreach (KeyValuePair<string, string> kvp in headers)
            {
                req.Headers[kvp.Key] = kvp.Value;

            }
            WebResponse wr = null;
            try
            {
                wr = req.GetResponse();
            }
            catch (WebException ex)
            {
                if (ex.Message.Contains("Too Many"))
                {
                    return "Too Many Requests";
                }
            }
            Stream stream = wr.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.UTF8);
            string content = reader.ReadToEnd();
            reader.Close();
            var json = $"[{content}]"; 
            var objects = JArray.Parse(json); 
            string finalJson = "";
            foreach (JObject o in objects.Children<JObject>())
            {
                foreach (JProperty p in o.Properties())
                {
                    string name = p.Name;
                    string value = p.Value.ToString();
                    finalJson += name + ": " + value;
                }
            }
            return finalJson;
        }

        public int runStoredProc(string storedProcName, List<SqlParameter> sqlParams)
        {
            AppSettings ap = new AppSettings();
            string connectionString = ap.ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcName;

            foreach (SqlParameter param in sqlParams)
            {
                cmd.Parameters.Add(param);
            }
            conn.Open();
            int rowsAffected = cmd.ExecuteNonQuery();
            conn.Close();
            return rowsAffected;
        }

        public string runStoredProcOutput(string storedProcName, List<SqlParameter> sqlParams, string outputParamName)
        {
            AppSettings ap = new AppSettings();
            string connectionString = ap.ConnectionString;
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = storedProcName;

            foreach (SqlParameter param in sqlParams)
            {
                cmd.Parameters.Add(param);
            }
            conn.Open();
            cmd.ExecuteNonQuery();
            string output = cmd.Parameters[outputParamName].Value.ToString();
            conn.Close();
            return output;
        }
    }
}
