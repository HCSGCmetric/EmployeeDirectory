using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class AppSettings
    {
        public string assertionConsumerServiceUrl = "https://web.hcsgcorp.com/EDprod/Home/Dashboard";
        //public string assertionConsumerServiceUrl = "http://localhost:31595/Home/Dashboard";
        public string assertionConsumerLogoutServiceUrl = "http://localhost:65475/Home/AfterLogoutIndex";
        public string issuer = string.Empty;
    }
}