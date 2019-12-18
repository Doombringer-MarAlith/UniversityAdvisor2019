using System;
using System.Collections.Generic;
using System.Text;

namespace Dbo
{
    public class Account
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public DateTime Age { get; set; }
        public int Id { get; set; }
        public string Guid { get; set; }
    }
}