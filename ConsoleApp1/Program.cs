using System;
using Models.Models;
using Newtonsoft.Json;
using ServerCallFromApp;

namespace TestProject
{
    class Program
    {
        static void Main(string[] args)
        {
            string guid = Guid.NewGuid().ToString();
            var account = new Account()
            {
                Name = "Tomas",
                Password = "Bpp",
                Guid = guid
            };
            DataManipulations.PostDataToServer("account/create", JsonConvert.SerializeObject(account));
            var ReturnGuid = DataManipulations.GetDataFromServer($"account/login/Tomas/Bpp");
            var acc = JsonConvert.DeserializeObject<Account>(DataManipulations.GetDataFromServer($"account/{"3e390d7a-0be2-4ce8-aa30-9a986fd529be"}"));
            
            Console.WriteLine(acc.Guid);
        }
    }
}
