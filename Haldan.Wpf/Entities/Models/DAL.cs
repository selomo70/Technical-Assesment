using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class DAL
    {
        public static int executeQuery(string query)
        {
            // Initialization.  
            int rowCount = 0;
            string strConn = "Data Source=CONFIDENCES-L\\SQLSERVER2019;Database=AutoEDITest;Integrated Security=true;MultipleActiveResultSets=true;";
            SqlConnection sqlConnection = new SqlConnection(strConn);
            SqlCommand sqlCommand = new SqlCommand();

            try
            {
                // Settings.  
                sqlCommand.CommandText = query;
                sqlCommand.CommandType = CommandType.Text;
                sqlCommand.Connection = sqlConnection;
                sqlCommand.CommandTimeout = 12 * 3600; //// Setting timeeout for longer queries to 12 hours.  

                // Open.  
                sqlConnection.Open();

                // Result.  
                rowCount = sqlCommand.ExecuteNonQuery();

                // Close.  
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                // Close.  
                sqlConnection.Close();

                throw ex;
            }

            return rowCount;
        }
    }
}
