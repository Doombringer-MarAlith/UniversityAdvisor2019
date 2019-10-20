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

        enum GuidEnum
        {
            UniGuid,
            FacultyGuid,
            LecturerGuid,
            UniProgramGuid
        }

        // Account START

        public void CreateAccount(Account account)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Account] VALUES ('{account.Name}', '{account.Password}', '{account.Email}', '{account.Guid}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            Logger.Log
                    (
                        $"DatabaseExecutor.CreateAccount: Account is created with values ({account.Name} , {account.Password} , {account.Email} , {account.Guid} )"
                    );
        }

        public string CheckAccountEmail(string email)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Email='{email}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdoConnection.Close();
                            return reader["Guid"].ToString();
                        }
                    }
                }
            }
            Logger.Log($"DatabaseExecutor.CheckAccountEmail({email}): CheckAccountEmail return value is null", Level.Warning);
            return null;
        }

        public string CheckAccountUsername(string username)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Name='{username}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            bdoConnection.Close();
                            return reader["Guid"].ToString();
                        }
                    }
                }
            }
            Logger.Log($"DatabaseExecutor.CheckAccountUsername({username}): CheckAccountUsername return value is null", Level.Warning);
            return null;
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
                        string guid = null;
                        while (reader.Read())
                        {
                            guid = reader["Guid"].ToString();
                        }
                        bdoConnection.Close();
                        if (guid != null)
                        {
                            return guid;
                        }
                    }
                }
            }

            Logger.Log($"DatabaseExecutor.ReturnAccountGuid({email}  ,  {password}): Account guid return value is null", Level.Warning);
            return null;
        }

        public void DeleteAccount(string id)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
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

        // Account END

        public object ReturnReviews(string Guid, int guidType)
        {
            string GuidType = ((GuidEnum)guidType).ToString();
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Reviews] WHERE {GuidType} = '{Guid}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<Review> reviews = new List<Review>();
                        while (reader.Read())
                        {
                            var review = new Review
                            {
                                UserId = reader["UserId"].ToString(),
                                UniGuid = reader["UniGuid"].ToString(),
                                ReviewGuid = reader["ReviewGuid"].ToString(),
                                Text = reader["Text"].ToString(),
                                Value = reader["Value"].ToString()

                            };
                            reviews.Add(review);
                        }
                        bdoConnection.Close();
                        if (reviews != null)
                        {
                            return reviews;
                        }
                    }
                }
            }

            Logger.Log($"DatabaseExecutor.ReturnReviews({Guid}): Reviews return value is null", Level.Warning);
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
                        List<University> unis = new List<University>();
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

        public void CreateFaculty(Faculty faculty)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Faculties] VALUES ('{faculty.UniGuid}','{faculty.Name}', '{faculty.FacultyGuid}')",

                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            Logger.Log
                    (
                        $"DatabaseExecutor.CreateFaculty: faculty is created with values ({faculty.UniGuid}, {faculty.Name}, {faculty.FacultyGuid})"
                    );
        }

        public List<Faculty> ReturnFaculties(string guid)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Faculties] WHERE UniGuid = '{guid}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<Faculty> facs = new List<Faculty>();
                        while (reader.Read())
                        {
                            var fac = new Faculty
                            {
                                Name = reader["Name"].ToString(), // add more fields
                                UniGuid = reader["UniGuid"].ToString(),
                                FacultyGuid = reader["FacultyGuid"].ToString()
                            };
                            facs.Add(fac);
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


        public object ReturnReview(string Guid)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Reviews] WHERE ReviewGuid = '{Guid}'",
                    bdoConnection
                ))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        List<Review> reviews = new List<Review>();
                        while (reader.Read())
                        {
                            var review = new Review
                            {
                                UserId = reader["UserId"].ToString(),
                                UniGuid = reader["UniGuid"].ToString(),
                                ReviewGuid = reader["ReviewGuid"].ToString(),
                                Text = reader["Text"].ToString(),
                                Value = reader["Value"].ToString()

                            };
                            reviews.Add(review);
                        }
                        bdoConnection.Close();
                        if (reviews != null)
                        {
                            return reviews;
                        }
                    }
                }
            }

            Logger.Log($"DatabaseExecutor.ReturnReview({Guid}): Review return value is null", Level.Warning);
            return null;
        }

        //might not work, didn't check
        public void CreateReview(Review review)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Reviews](UniGuid, Text, Value, UserId, ReviewGuid, FacultyGuid, LecturerGuid, CourseGuid) VALUES ('{review.UniGuid}','{review.Text}', '{review.Value}', '{review.UserId}', '{review.ReviewGuid}', '{review.FacultyGuid}', '{review.LecturerGuid}', '{review.CourseGuid}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            Logger.Log
            (
                $"DatabaseExecutor.CreateReview: review is created with values ({review.UniGuid}, {review.Text}, {review.Value}, {review.UserId}, {review.ReviewGuid} )"
            );
        }
    }
}