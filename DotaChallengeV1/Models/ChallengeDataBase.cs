using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DotaChallengeV1.Models
{
    public class ChallengeDataBase
    {
        private static SqlConnection sqlConn;

        public static SqlConnection ConnectDataBase()
        {
            string connString = "Server=tcp:dotachallenge.database.windows.net,1433;Initial Catalog=dotachallenge;Persist Security Info=False;User ID=ionofrei;Password=Igor7394;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=50;";
            try
            {
                sqlConn = new SqlConnection(connString);
            }
            catch (SqlException ex)
            {
                Console.WriteLine("------------\nData Base Connection error\n" + ex.Message + "-----------\n");
                return null;
            }
            return sqlConn;
        }
    }
}