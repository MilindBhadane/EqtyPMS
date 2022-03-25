using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.OleDb;

namespace EqtyPMS.DAL
{
    public class DBConnector
    {
        string connectionString = ConfigurationManager.AppSettings["PMSConnection"];
        string dbFilePath = ConfigurationManager.AppSettings["DBFilePath"];

        private OleDbConnection GetOleDbConnection()
        {
            OleDbConnection conn;
            var DBPath = dbFilePath;
            conn = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + DBPath);
            return conn;
        }

        public DataSet GetAccessDBData(string query, string tableName)
        {
            OleDbConnection conn = GetOleDbConnection();
            DataSet dataset = new DataSet();
            DataTable dt = new DataTable(tableName);
            dt.Columns.Add("TradeID");
            dt.Columns.Add("SecurityCode");
            dt.Columns.Add("Side");

            try
            {
                OleDbDataReader reader = null;
                OleDbCommand cmd = conn.CreateCommand();
                conn.Open();
                cmd.CommandText = query;
                cmd.Connection = conn;
                reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    DataRow dr = dt.NewRow();
                    dr["TradeID"] = reader[0];
                    dr["SecurityCode"] = reader[1];
                    dr["Side"] = reader[2];

                    dt.Rows.Add(dr);
                }

                dt.AcceptChanges();
                dataset.Tables.Add(dt);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (conn != null && conn.State != ConnectionState.Closed)
                {
                    conn.Close();
                }
            }
            return dataset;

        }

        public DataSet GetSQLData(string query)
        {
            DataSet dataset = new DataSet();
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(query, connection);
                adapter.Fill(dataset);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return dataset;
        }

        public bool ExecuteNonQuery(string cmdText)
        {
            bool retValue = false;
            SqlConnection connection = new SqlConnection(connectionString);
            try
            {
                connection.Open();

                SqlCommand command = new SqlCommand(cmdText, connection);
                command.ExecuteNonQuery();
                retValue = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (connection != null && connection.State != ConnectionState.Closed)
                {
                    connection.Close();
                }
            }
            return retValue;
        }
    }
}
