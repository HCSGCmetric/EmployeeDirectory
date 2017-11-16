using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class FormPermissionEntity
    {
        public int ID { get; set; }

        public bool IsView { get; set; }

        public bool IsAdd { get; set; }

        public bool IsEdit { get; set; }

        public bool IsDelete { get; set; }

        public bool IsDisplay { get; set; }

        public string FieldName { get; set; }

        public int FieldId { get; set; }

        public int UserGroupId { get; set; }

        public string FormControllerName { get; set; }

        public string FormApplicationName { get; set; }

        public string FormURL { get; set; }

        public string FormName { get; set; }

        public int UserType { get; set; }

        public string FormPermissionGridXML { get; set; }

        public List<FormPermissionEntity> lstFormPermissionEntity { get; set; }
    }

    public class FieldPermissionEntity
    {


        public bool IsDisplay { get; set; }

        public string FieldName { get; set; }

        public int FieldId { get; set; }

        public int UserGroupId { get; set; }


    }
}