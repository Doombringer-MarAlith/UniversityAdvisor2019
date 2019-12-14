using System;
using System.Data.SqlClient;
using System.Globalization;

namespace Dbo
{
    public class DatabaseExecutor
    {
        internal static string ConnectionString = @"Server=(localdb)\madder;Database=DataAdapter;Trusted_Connection=True;";

        public void CreateAccount(Account account)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Account] VALUES ('{account.FirstName}','{account.LastName}','{account.Gender}', '{account.Password}', '{account.Email}', '{account.Age.ToString(CultureInfo.InvariantCulture)}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }
        }

        public Account ReturnAccount(string id)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
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
                            var account = new Account
                            {
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Age = DateTime.Parse(reader["Age"].ToString()),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString()
                            };
                            bdoConnection.Close();
                            return account;
                        }
                    }
                }
            }

            //Logger.Log($"Account return value is null", Level.Warning);
            return null;
        }

        public string ReturnAccountGuid(string email, string password)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Email='{email}' AND Password='{password}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var guid = reader["Guid"].ToString();
                            bdoConnection.Close();
                            return guid;
                        }
                    }
                }
            }
            //Logger.Log($"Account guid return value is null", Level.Warning);
            return null;
        }
    }
}