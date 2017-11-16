using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class UserTypeEntity
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public int Pageindex { get; set; }

        public List<UserTypeEntity> UserTypeEntityList { get; set; }

    }
}