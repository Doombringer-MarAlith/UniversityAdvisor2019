using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ServerCallFromApp
{
    public interface IDataManipulations
    {
        Task<string> GetDataFromServer(string url);
        Task PostDataToServer(string url, string data);
    }
}
