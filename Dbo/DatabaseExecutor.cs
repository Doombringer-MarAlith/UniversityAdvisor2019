using Models.Models;
using System;
using System.Data.SqlClient;
using System.Globalization;

namespace Dbo
{
    public class DatabaseExecutor
    {
        internal static string connetionString =
            @"Server=(localdb)\madder;Database=UniversityAdvisor;Trusted_Connection=True;";

        public void CreateAccount(Account account)
        {
            using (var bdoConnection = new SqlConnection(connetionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Account] VALUES ('{account.Name}', '{account.Password}', '{account.Email}', '{account.Guid}', '{account.Age.ToString(CultureInfo.InvariantCulture)}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                }
            }
        }

        public Account ReturnAccount(string id)
        {
            using (var bdoConnection = new SqlConnection(connetionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Guid='{id}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return new Account
                            {
                                Name = reader["Name"].ToString(),
                                Age = DateTime.Parse(reader["Age"].ToString()),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString(),
                                Guid = reader["Guid"].ToString()
                            };
                        }
                    }
                }
            }
            return null;
        }
        public string ReturnAccountGuid(string name, string password)
        {
            using (var bdoConnection = new SqlConnection(connetionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Name='{name}' AND Password='{password}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {

                            return reader["Guid"].ToString();
                        }
                    }
                }
            }
            return null;
        }

        public void DeleteAccount(string id)
        {
            using (var bdoConnection = new SqlConnection(connetionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"DELETE FROM [Account] WHERE Guid={id}",
                    bdoConnection
                ))
                {
                    command.ExecuteReader();
                }
            }
        }
    }
}
