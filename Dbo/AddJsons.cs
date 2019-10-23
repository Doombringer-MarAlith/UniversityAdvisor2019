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
        private static readonly string SqlConnectionString = DatabaseExecutor.ConnectionString;

        private static readonly DatabaseExecutor Database = new DatabaseExecutor();

        public static void AddJsons()
        {
            SqlConnection sqlConnection = new SqlConnection(SqlConnectionString);
            var fileEntries = Directory.GetFiles("Jsons");
            //string path = "json/";
            foreach (var fileEntry in fileEntries)
            {
                Console.WriteLine(fileEntry);
                switch (fileEntry)
                {
                    case "Jsons\\Accounts.json":
                        using (StreamReader r = new StreamReader(fileEntry))
                        {
                            string json = r.ReadToEnd();
                            JsonArray accounts = JsonConvert.DeserializeObject<JsonArray>(json);
                            foreach (var account in accounts.accounts)
                            {
                                Database.CreateAccount(account);
                            }
                        }
                        Console.WriteLine("PAVYkO");
                        break;
                    default:
                        Console.WriteLine("nePAVYkO");

                        break;
                }
            }
        }
    }

    [JsonObject]
    internal class JsonArray
    {
        [JsonProperty("accounts")]
        public List<Account> accounts;
    }

}
