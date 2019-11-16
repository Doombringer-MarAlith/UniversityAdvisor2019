﻿using System.Collections.Generic;
using System.Data.SqlClient;
using Castle.Core.Internal;
using Debugger;
using Models.Models;

namespace DboExecutor
{
    public class DatabaseExecutor : IDatabaseExecutor
    {
        public static string ConnectionString =
            @"Server=(localdb)\madder;Database=UniversityAdvisor;Trusted_Connection=True;";

        private readonly ILogger _logger;

        enum GuidEnum
        {
            UniGuid,
            FacultyGuid,
            LecturerGuid,
            UniProgramGuid
        }

        public DatabaseExecutor(ILogger logger)
        {
            _logger = logger;
        }

        // Account START

        public void CreateAccount(Account account)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
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

            _logger.Log
            (
                $"DatabaseExecutor.CreateAccount: Account is created with values ({account.Name}, {account.Password}, {account.Email}, {account.Guid})"
            );
        }

        public string CheckAccountEmail(string email)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Email = '{email}'",
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

            _logger.Log($"DatabaseExecutor.CheckAccountEmail({email}): CheckAccountEmail return value is null", Level.Warning);
            return null;
        }

        public string CheckAccountUsername(string username)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Name = '{username}'",
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

            _logger.Log($"DatabaseExecutor.CheckAccountUsername({username}): CheckAccountUsername return value is null", Level.Warning);
            return null;
        }

        public Account ReturnAccount(string id)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Guid = '{id}'",
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

            _logger.Log($"DatabaseExecutor.ReturnAccount({id}): Account return value is null", Level.Warning);
            return null;
        }

        public string ReturnAccountGuid(string email, string password)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"SELECT * FROM [Account] WHERE Email = '{email}' AND Password = '{password}'",
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

            _logger.Log($"DatabaseExecutor.ReturnAccountGuid({email}, {password}): Account guid return value is null", Level.Warning);
            return null;
        }

        public void DeleteAccount(string id)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"DELETE FROM [Account] WHERE Guid = {id}",
                    bdoConnection
                ))
                {
                    command.ExecuteReader();
                }
            }
        }

        // Account END

        public void CreateUniversity(University university)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [University] VALUES ('{university.Guid}', '{university.Name}', '{university.Description}', '{university.FoundingDate.Year.ToString()}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            _logger.Log
            (
                $"DatabaseExecutor.CreateUniversity: university is created with values ({university.Guid}, {university.Name})"
            );
        }

        public List<University> ReturnUniversities(string name)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
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
                        List<University> universities = new List<University>();
                        while (reader.Read())
                        {
                            var university = new University
                            {
                                Name = reader["Name"].ToString(), // add more fields
                                Guid = reader["Guid"].ToString()
                            };

                            universities.Add(university);
                        }

                        bdoConnection.Close();

                        if (!universities.IsNullOrEmpty())
                        {
                            return universities;
                        }
                    }
                }
            }

            _logger.Log($"DatabaseExecutor.ReturnUniversityGuid({name}): University guid return value is null", Level.Warning);
            return null;
        }

        public void CreateFaculty(Faculty faculty)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Faculties] VALUES ('{faculty.UniGuid}', '{faculty.Name}', '{faculty.FacultyGuid}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            _logger.Log
            (
                $"DatabaseExecutor.CreateFaculty: faculty is created with values ({faculty.UniGuid}, {faculty.Name}, {faculty.FacultyGuid})"
            );
        }

        public List<Faculty> ReturnFaculties(string guid)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
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
                        List<Faculty> faculties = new List<Faculty>();
                        while (reader.Read())
                        {
                            var faculty = new Faculty
                            {
                                Name = reader["Name"].ToString(), // add more fields
                                UniGuid = reader["UniGuid"].ToString(),
                                FacultyGuid = reader["FacultyGuid"].ToString()
                            };

                            faculties.Add(faculty);
                        }

                        bdoConnection.Close();

                        if (!faculties.IsNullOrEmpty())
                        {
                            return faculties;
                        }
                    }
                }
            }

            _logger.Log($"DatabaseExecutor.ReturnFaculties({guid}): Faculties return value is null", Level.Warning);
            return null;
        }

        public List<Review> ReturnReviews(string Guid, int guidType)
        {
            string GuidType = ((GuidEnum)guidType).ToString();
            using (var bdoConnection = new SqlConnection(ConnectionString))
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

                        if (reviews.Count > 0)
                        {
                            return reviews;
                        }
                    }
                }
            }

            _logger.Log($"DatabaseExecutor.ReturnReviews({Guid}): Reviews return value is null", Level.Warning);
            return null;
        }

        public Review ReturnReview(string Guid)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
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
                        while (reader.Read())
                        {
                            return new Review
                            {
                                UserId = reader["UserId"].ToString(),
                                UniGuid = reader["UniGuid"].ToString(),
                                ReviewGuid = reader["ReviewGuid"].ToString(),
                                Text = reader["Text"].ToString(),
                                Value = reader["Value"].ToString()
                            };
                        }

                        bdoConnection.Close();
                    }
                }
            }

            _logger.Log($"DatabaseExecutor.ReturnReview({Guid}): Review return value is null", Level.Warning);
            return null;
        }

        public void CreateReview(Review review)
        {
            using (var bdoConnection = new SqlConnection(ConnectionString))
            {
                bdoConnection.Open();

                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Reviews](UniGuid, Text, Value, UserId, ReviewGuid, FacultyGuid, LecturerGuid, CourseGuid) VALUES ('{review.UniGuid}', '{review.Text}', '{review.Value}', '{review.UserId}', '{review.ReviewGuid}', '{review.FacultyGuid}', '{review.LecturerGuid}', '{review.CourseGuid}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }

            _logger.Log
            (
                $"DatabaseExecutor.CreateReview: review is created with values ({review.UniGuid}, {review.Text}, {review.Value}, {review.UserId}, {review.ReviewGuid})"
            );
        }
    }
}