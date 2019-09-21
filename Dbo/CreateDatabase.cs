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
            SqlConnection sqlConnection = new SqlConnection(SqlConnectionString);
            var fileEntries = Directory.GetFiles("Scripts");
            sqlConnection.Open();

            foreach (var fileEntry in fileEntries)
            {
                File.OpenRead(fileEntry);
                string script = File.ReadAllText(fileEntry);
                script = script.Replace("\r\n", " ");
                using (SqlCommand command = new SqlCommand(script,sqlConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("DataBase was updated successfully");
                }
            }
            
            sqlConnection.Close();
            Console.WriteLine("DataBase is ready");
        }
    }
}
