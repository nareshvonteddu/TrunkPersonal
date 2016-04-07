using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.ServiceModel.Activation;

namespace DataLayerService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    //[AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Required)]
    public class DataLayerService : IDataLayerService
    {
        private SqlConnection con = new SqlConnection();
        public void OpenConnection()
        {
            string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename= C:\NRIUturn\NRIUturn.WCFService\App_Data\NRIUturn.mdf;Integrated Security=True;User Instance=True";
            //string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename= C:\Users\nvonteddu\Documents\Visual Studio 2010\Projects\NRIUturn\NRIUturn.WCFService\App_Data\NRIUturn.mdf;Integrated Security=True;User Instance=True";
            if(con == null || con.State != ConnectionState.Open)
            {
                con = new SqlConnection(connectionString);
                con.Open();
            }
        }

        public void CloseConnection()
        {
            if (con.State == ConnectionState.Open)
            {
                con.Close();
            }
        }

        public List<Dictionary<String, String>> CallProcedure(ref string sql, Dictionary<string, string> parameters, out bool isSuccess)
        {
            isSuccess = false;
            List<Dictionary<String, String>> resultSet = new List<Dictionary<string, string>>();
            try
            {
                OpenConnection();
                SqlCommand agentCommand = new SqlCommand(sql, con);
                agentCommand.CommandType = CommandType.StoredProcedure;
                agentCommand.CommandTimeout = 0;
                foreach (var param in parameters)
                {
                    agentCommand.Parameters.Add(new SqlParameter(param.Key, param.Value));
                }
                SqlDataReader reader = agentCommand.ExecuteReader();
                Dictionary<String, String> rrow;
                int columns = 0;
                while (reader.Read())
                {
                    rrow = new Dictionary<String, String>();
                    columns = reader.FieldCount;
                    for (int i = 0; i < columns; i++)
                    {
                        rrow[reader.GetName(i)] = reader.GetValue(i).ToString();
                    }
                    resultSet.Add(rrow);
                }
                reader.Close();
                CloseConnection();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                isSuccess = false;
            }
            return resultSet;
        }

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
    }
}
