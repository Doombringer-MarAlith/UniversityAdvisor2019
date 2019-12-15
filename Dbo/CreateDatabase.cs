﻿using System;
using System.Data.SqlClient;
using System.IO;

namespace Dbo
{
    internal class CreateDatabase
    {
        private static readonly string SqlConnectionString = DatabaseExecutor.ConnectionString;

        public static void Main(string[] args)
        {
            SqlConnection connection = new SqlConnection(SqlConnectionString);
            var fileEntries = Directory.GetFiles("Scripts");

            foreach (var fileEntry in fileEntries)
            {
                connection.Open();
                File.OpenRead(fileEntry);
                string script = File.ReadAllText(fileEntry);
                script = script.Replace("\r\n", " ");

                using (SqlCommand command = new SqlCommand(script, connection))
                {
                    SqlDataReader reader = command.ExecuteReader();
                    Console.WriteLine("Database was updated successfully.");
                }
                connection.Close();
            }

            Console.WriteLine("Database is ready.");
        }
    }
}