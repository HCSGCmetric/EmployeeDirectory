using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class UserDetails
    {
        public int ID { get; set; }

        public string FIRSTNAME { get; set; }

        public string LASTNAME { get; set; }

        public string USERNAME { get; set; }

        public string NewJobTitle { get; set; }

        public string DIV { get; set; }

        public string REG { get; set; }

        public string DIST { get; set; }

        public string EEID { get; set; }

        public DateTime LASTUPDATED { get; set; }

        public string UPDATEDBY { get; set; }

        public string EMAIL { get; set; }

        public string STATUS { get; set; }

    }
}