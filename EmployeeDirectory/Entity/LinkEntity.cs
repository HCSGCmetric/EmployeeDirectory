using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class LinkEntity
    {

        public int ID { get; set; }
        public string Name { get; set; }
     
        public string Link { get; set; }
        public string Description { get; set; }
        public string LinkDisplayIDs { get; set; }
        public string LinkDisplayName { get; set; }
        public string DepartmentName { get; set; }
        public string UserTypes { get; set; }
        public bool IsCommonForAll { get; set; }
        public string UserTypeId { get; set; }
        public int DepartmentId { get; set; }
        public int TotalRows { get; set; }
        public int Pageindex { get; set; }
        public int pagesize { get; set; }
        public int TotalRecord { get; set; }

        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }
        
        public DateTime CreatedDate { get; set; }
        public bool IsActive { get; set; }

        public List<LinkEntity> lstLinkEntity { get; set; }
    }

    public class DocumentDisplayEntity 
    {

        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsDocument { get; set; }
        public bool IsDisplayLink { get; set; }

    }
}