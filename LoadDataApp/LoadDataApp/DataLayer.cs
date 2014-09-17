using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace LoadDataApp
{
    public static class DataLayer
    {
        static string _connString = ConfigurationManager.AppSettings["ConnectionString"].ToString();

        public static void SaveData(DataTable dataTable)
        {
            SqlConnection conn = null;
            try
            {
                foreach (DataRow dataRow in dataTable.Rows)
                {
                    try
                    {
                        conn = new SqlConnection(_connString);
                        conn.Open();
                        SqlCommand stmt = new SqlCommand("dbo.AddBusiness", conn);
                        stmt.CommandType = CommandType.StoredProcedure;
                        SqlParameter paramBusinessName = stmt.Parameters.Add("@BusinessName", SqlDbType.VarChar,150);
                        paramBusinessName.Value = dataRow["BusinessName"];
                        SqlParameter paramGroupName = stmt.Parameters.Add("@GroupName", SqlDbType.VarChar, 50);
                        paramGroupName.Value = dataRow["GroupName"];
                        SqlParameter paramPhoneNumber = stmt.Parameters.Add("@PhoneNumber", SqlDbType.NChar, 11);
                        paramPhoneNumber.Value = dataRow["PhoneNumber"];
                        SqlParameter paramCurrentDateTime = stmt.Parameters.Add("@CurrentDateTime", SqlDbType.DateTime);
                        paramCurrentDateTime.Value = DateTime.Now;
                        var result = stmt.ExecuteScalar();
                    }
                    catch (Exception e)
                    {
                        Logger.WriteToLog(e.ToString());
                    }
                    finally
                    {
                        if (conn != null) conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.WriteToLog(ex.ToString());
            }
        }
    }
}
