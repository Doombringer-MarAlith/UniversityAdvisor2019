using System;
using System.Data.SqlClient;
using System.IO;

namespace Dbo
{
    internal class CreateDatabase
    {
        private static readonly string SqlConnectionString = DatabaseExecutor.connectionString;

        public static void Main(string[] args)
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnectionString);
            var fileEntries = Directory.GetFiles("Scripts");
            foreach (var fileEntry in fileEntries)
            {
                sqlConnection.Open();
                File.OpenRead(fileEntry);
                string script = File.ReadAllText(fileEntry);
                script = script.Replace("\r\n", " ");
                using (SqlCommand command = new SqlCommand(script, sqlConnection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("Database was updated successfully.");
                }

                sqlConnection.Close();
            }
            //AddJsonFiles.AddJsons(); // ADDED RANDOMLY. Needs better placement
            Console.WriteLine("Database is ready.");
        }
    }
}
