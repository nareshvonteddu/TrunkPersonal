using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace TestDataAccess
{
    class Program
    {
        private static SqlConnection con;
        public static void OpenConnection()
        {
            //string connectionString = @"Data Source=.\SQLExpress; Integrated Security=true; AttachDbFilename="" & Server.MapPath(""~\App_Data\NRIUturn.mdf"")";
            string connectionString = @"Data Source=.\SQLEXPRESS;AttachDbFilename= C:\Users\nvonteddu\Documents\Visual Studio 2010\Projects\NRIUturn\NRIUturn.WCFService\App_Data\NRIUturn.mdf;Integrated Security=True;User Instance=True";
            con = new SqlConnection(connectionString);

            // Open the connection
            con.Open();
        }

        public static void CloseConnection()
        {
            con.Close();
        }

        public static List<Dictionary<String, String>> GetData(String sql, string parameter)
        {
            OpenConnection();
            SqlCommand agentCommand = new SqlCommand(sql, con);
            agentCommand.CommandType = CommandType.StoredProcedure;
            agentCommand.CommandTimeout = 0;
            agentCommand.Parameters.Add(new SqlParameter("@moduleID", "1"));
            SqlDataReader reader = agentCommand.ExecuteReader();
            List<Dictionary<String, String>> resultSet = new List<Dictionary<string, string>>();
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

            return resultSet;
        }

        static void Main(string[] args)
        {
            GetData("GetPostsFor", "");
        }
    }
}
