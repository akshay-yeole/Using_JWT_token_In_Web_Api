using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Implement_JWT_token_In_Web_Api.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
