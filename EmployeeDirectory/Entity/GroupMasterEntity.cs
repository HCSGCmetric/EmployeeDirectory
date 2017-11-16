using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class GroupMasterEntity
    {

        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public int Pageindex { get; set; }
        public int pagesize { get; set; }
        public int TotalRecord { get; set; }

        public List<GroupMasterEntity> GroupMasterEntityList { get; set; }


    }
}