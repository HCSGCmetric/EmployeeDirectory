//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EmployeeDirectory.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CorporateContact
    {
        public int id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string BusinessPhone { get; set; }
        public string FaxNumber { get; set; }
        public Nullable<double> Extension { get; set; }
        public string EmailAddress { get; set; }
        public string Office { get; set; }
        public Nullable<System.DateTime> EnterDate { get; set; }
        public Nullable<System.DateTime> StartDate { get; set; }
        public string AccessType { get; set; }
        public string Supervisor { get; set; }
        public string employee_id { get; set; }
        public Nullable<System.DateTime> TermDate { get; set; }
    }
}