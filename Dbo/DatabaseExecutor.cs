using System;
using System.Data.SqlClient;
using System.Globalization;
using Models.Models;

namespace Dbo
{
    public class DatabaseExecutor
    {
        internal static string connetionString = @"Server=(localdb)\madder;Database=UniversityAdvisor;Trusted_Connection=True;";

        public void CreateAccount(Account acc)
        {
            var bdoConnection = new SqlConnection(connetionString);
            bdoConnection.Open();
            using (SqlCommand command = new SqlCommand(
                $"Insert Into [Account] values ('{acc.Name}' , '{acc.Password}' , '{acc.Email}' , '{acc.Guid}' ,'{acc.Age.ToString(CultureInfo.InvariantCulture)}')",
                bdoConnection))
            {
                SqlDataReader reader = command.ExecuteReader();
            }
            bdoConnection.Close();
        }

        public Account ReturnAccount(string id)
        {
            var bdoConnection = new SqlConnection(connetionString);
            bdoConnection.Open();
            using (SqlCommand command = new SqlCommand(
                $"Select * from [Account] where Guid='{id}'",
                bdoConnection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        return new Account
                        {
                            Name = reader["Name"].ToString(),
                            Age = DateTime.Parse(reader["Age"].ToString()),
                            Password = reader["Password"].ToString(),
                            Email = reader["Email"].ToString(),
                       //     Guid = reader["Guid"].ToString()
                        };
                    }
                }
            }
            bdoConnection.Close();
            return null;
        }

        public void DeleteAccount(string id)
        {
            var bdoConnection = new SqlConnection(connetionString);
            bdoConnection.Open();
            using (SqlCommand command = new SqlCommand(
                $"Delete from [Account] where Guid={id}",
                bdoConnection))
            {
            }
            bdoConnection.Close();
        }
    }
}