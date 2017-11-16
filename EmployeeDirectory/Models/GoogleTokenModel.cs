using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Models
{
    public class GoogleTokenModel
    {
        public string Access_Token { get; set; }
        public string Refresh_Token { get; set; }
        public string Expires_In { get; set; }
        public string Token_Type { get; set; }
    }
}