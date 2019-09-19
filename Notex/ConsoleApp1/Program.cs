using NotexServerCallFromApp;
using System;
using System.Threading;

using Models.Models;
using Newtonsoft.Json;

namespace ConsoleApp1
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            var acc = JsonConvert.DeserializeObject<Account>(DataManipulations.GetDataFromServer($"account/{"3e390d7a-0be2-4ce8-aa30-9a986fd529be"}"));
            var account = new Account()
            {
                Name = "Tomas",
                Guid = Guid.NewGuid().ToString()
            };
            DataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            Console.WriteLine(acc.Guid);
        }
    }
}