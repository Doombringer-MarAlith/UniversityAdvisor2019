using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App;

namespace AppUnitTests
{
    public class StringGenerator
    {
        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(System.Linq.Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomEmail()
        {
            return string.Format("qa{0:0000}@test.com", random.Next(10000));
        }
    }
}
