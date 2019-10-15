using System;
using System.Data.SqlClient;
using System.Globalization;

namespace Debugger
{
    public static class Logger
    {
        internal static string connectionString = @"Server=(localdb)\madder;Database=UniversityAdvisor;Trusted_Connection=True;";

        public static void Log(string message, Level level = Level.Info, Exception exception = null)
        {
            using (var bdoConnection = new SqlConnection(connectionString))
            {
                bdoConnection.Open();
                using (var command = new SqlCommand
                (
                    $"INSERT INTO [Debug] VALUES ('{level.ToString()}','{message}','{DateTime.Now.ToString(CultureInfo.InvariantCulture)}', '{exception}')",
                    bdoConnection
                ))
                {
                    var reader = command.ExecuteReader();
                    bdoConnection.Close();
                }
            }
        }
    }

    public enum Level
    {
        Info,
        Warning,
        Error,
    }
}