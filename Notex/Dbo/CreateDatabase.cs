using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;

namespace Dbo
{
    class CreateDatabase
    {
        private static readonly string SqlConnectionString = DatabaseExecutor.connetionString;

        private static void Main(string[] args)
        {
            SqlConnection conn = new SqlConnection(SqlConnectionString);
            var fileEntries = Directory.GetFiles("Scripts");
            conn.Open();

            foreach (var fileEntry in fileEntries)
            {
                string script = File.ReadAllText(@"Scripts");
                using (SqlCommand command = new SqlCommand(script))
                {
                    SqlDataReader reader = command.ExecuteReader();
                }
            }

            conn.Close();
        }
    }
}
