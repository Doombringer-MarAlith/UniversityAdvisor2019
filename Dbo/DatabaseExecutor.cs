using Debugger;
using Models.Models;
using System;
using System.Collections.Generic;
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

        public void CreateUniversity(University university)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [University] VALUES ('{university.Guid}','{university.Name}')",

                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            Logger.Log
                    (
                        $"DatabaseExecutor.CreateUniversity: university is created with values ({university.Guid}, {university.Name})"
                    );
        }

        public List<University> ReturnUniversities(string name)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [University] WHERE LOWER(Name) LIKE LOWER('%{name}%')",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List <University> unis = new List<University>();
                        while (reader.Read())
                        {
                            var uni = new University
                            {
                                Name = reader["Name"].ToString(), // add more fields
                                Guid = reader["Guid"].ToString()
                            };
                            unis.Add(uni);
                        }
                        bdoConnection.Close();
                        if (unis != null)
                        {
                            return unis;
                        }
                    }
                }
            }

            Logger.Log($"DatabaseExecutor.ReturnUniversityGuid({name}): University guid return value is null", Level.Warning);
            return null;
        }

        public List<Faculty> ReturnFaculties(string guid)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Faculty] WHERE UniversityGuid = '{guid}')",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<Faculty> facs = new List<Faculty>();
                        while (reader.Read())
                        {
                            var uni = new Faculty
                            {
                                Name = reader["Name"].ToString(), // add more fields
                                Guid = reader["Guid"].ToString()
                            };
                            facs.Add(uni);
                        }
                        bdoConnection.Close();
                        if (facs != null)
                        {
                            return facs;
                        }
                    }
                }
            }

            Logger.Log($"DatabaseExecutor.ReturnFaculties({guid}): Faculties return value is null", Level.Warning);
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