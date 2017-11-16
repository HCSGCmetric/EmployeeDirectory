using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public enum UserType
    {
        [StringValue("SuperUser")]
        SuperUser,
        [StringValue("User")]
        User,
        [StringValue("EmployeeDirectory")]
        EmployeeDirectory,
        [StringValue("HSGUser")]
        HSGUser
    }

    public enum ApplicationName
    {
        [StringValue("Citrix User App")]
        CitrixUserApp,
        [StringValue("Employee Directory")]
        EmployeeDirectory
    }

    public enum CorporateContacts
    {
        [StringValue("SuperUser")]
        SuperUser,
        [StringValue("User")]
        User
    }
}