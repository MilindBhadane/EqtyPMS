using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using EqtyPMS.DAL;
using System.Data;

namespace EqtyPMS.Service
{
    public class Service : IService
    {
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public DataSet GetAccessDBData(string query, string tableName)
        {
            DataSet dataset = new DataSet();
            try
            {
                DBConnector dbConnector = new DBConnector();
                dataset = dbConnector.GetAccessDBData(query, tableName);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {

            }
            return dataset;
        }

        public DataSet GetSQLData(string query)
        {
            DataSet dataset = new DataSet();
            try
            {
                DBConnector dbConnector = new DBConnector();
                dataset = dbConnector.GetSQLData(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
            return dataset;
        }

        public bool ExecuteSQLData(string query)
        {
            bool retval = false;
            try
            {
                DBConnector dbConnector = new DBConnector();
                retval = dbConnector.ExecuteNonQuery(query);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
            return retval;
        }


    }
}
