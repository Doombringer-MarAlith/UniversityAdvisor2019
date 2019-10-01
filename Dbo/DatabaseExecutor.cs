using Debugger;
using Models.Models;
using System;
using System.Data.SqlClient;
using System.Globalization;

namespace Dbo
{
    public class DatabaseExecutor
    {
        internal static string connectionString =
            @"Server=(localdb)\madder;Database=UniversityAdvisor;Trusted_Connection=True;";

        public void CreateAccount(Account account)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Account] VALUES ('{account.Name}', '{account.Password}', '{account.Email}', '{account.Guid}', '{account.Age.ToString(CultureInfo.InvariantCulture)}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            Logger.Log
                    (
                        $"DatabaseExecutor.CreateAccount: Account is created with values ({account.Name} , {account.Password} , {account.Email} , {account.Guid} , {account.Age.ToString(CultureInfo.InvariantCulture)} )"
                    );
        }

        public Account ReturnAccount(string id)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
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
                                Name = reader["Name"].ToString(),
                                Age = DateTime.Parse(reader["Age"].ToString()),
                                Password = reader["Password"].ToString(),
                                Email = reader["Email"].ToString(),
                                Guid = reader["Guid"].ToString()
                            };

                            bdoConnection.Close();
                            return account;
                        }
                    }
                }
            }

            Logger.Log($"DatabaseExecutor.ReturnAccount({id}): Account return value is null", Level.Warning);
            return null;
        }

        public string ReturnAccountGuid(string email, string password)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
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

            Logger.Log($"DatabaseExecutor.ReturnAccountGuid({email}  ,  {password}): Account guid return value is null", Level.Warning);
            return null;
        }

        //public void DeleteAccount(string id)
        //{
        //    using (var bdoConnection = new SqlConnection(connectionString))
        //    {
        //        bdoConnection.Open();
        //        using (var command = new SqlCommand
        //        (
        //            $"DELETE FROM [Account] WHERE Guid={id}",
        //            bdoConnection
        //        ))
        //        {
        //            command.ExecuteReader();
        //        }
        //    }
        //}
    }
}