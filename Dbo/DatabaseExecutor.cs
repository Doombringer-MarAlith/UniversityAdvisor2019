using System;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;

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
                    $"INSERT INTO [Account] VALUES ('{account.FirstName}','{account.LastName}','{account.Gender}', '{account.Password}', '{account.Email}','{account.Guid}', '{account.Age.ToString(CultureInfo.InvariantCulture)}')",
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
                    SqlDataAdapter dataAdapter = new SqlDataAdapter { SelectCommand = command };
                    DataTable data = new DataTable();
                    dataAdapter.Fill(data);

                    foreach (DataRow row in data.Rows)
                    {
                        var account = new Account
                        {
                            FirstName = row["FirstName"].ToString(),
                            LastName = row["LastName"].ToString(),
                            Gender = row["Gender"].ToString(),
                            Age = DateTime.Parse(row["Age"].ToString()),
                            Password = row["Password"].ToString(),
                            Guid = row["Guid"].ToString(),
                            Email = row["Email"].ToString(),
                            Id = int.Parse(row["Id"].ToString())
                        };
                        bdoConnection.Close();
                        return account;
                    }

                    bdoConnection.Close();
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
                    $"SELECT * FROM [Account]",
                    bdoConnection
                ))
                {
                    SqlDataAdapter dataAdapter = new SqlDataAdapter { SelectCommand = command };
                    DataTable data = new DataTable();
                    dataAdapter.Fill(data);
                    var guid = data.Select($"Email='{email}' and Password='{password}'").FirstOrDefault()?["Guid"].ToString();
                    bdoConnection.Close();
                    return guid;
                }
            }
        }
    }
}