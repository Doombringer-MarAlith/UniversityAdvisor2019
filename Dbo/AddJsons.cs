using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using Models.Models;
using Newtonsoft.Json;

namespace Dbo
{
    public class AddJsonFiles
    {
        private static readonly string SqlConnectionString = DatabaseExecutor.connectionString;

        private static readonly DatabaseExecutor db = new DatabaseExecutor();

        public static void AddJsons()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnectionString);
            var fileEntries = Directory.GetFiles("Jsons");
            foreach (var fileEntry in fileEntries)
            {
                switch (fileEntry)
                {
                    case "Accounts.json":
                        using (StreamReader r = new StreamReader(fileEntry))
                        {
                            string json = r.ReadToEnd();
                            List<Account> accounts = JsonConvert.DeserializeObject<List<Account>>(json);
                            foreach (var account in accounts)
                            {
                                db.CreateAccount(account);
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
