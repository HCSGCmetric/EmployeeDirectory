using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class EmployeeDirectoryEntity
    {

        public int ID { get; set; }
        public string EEID { get; set; }
        public string COID { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string LastName { get; set; }
        public string Social { get; set; }
        public string ShortSocial { get; set; }
        public string DEPT { get; set; }
        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string Address { get; set; }
        public string JobTitle { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string Zip { get; set; }
        public string phone { get; set; }
        public string DistinctManagerName { get; set; }
        public string RegionManagerName { get; set; }
        public string AccountManagerName { get; set; }


        public string SA { get; set; }
        public string DIV { get; set; }
        public string REG { get; set; }
        public string DIST { get; set; }
        public int TotalRecord { get; set; }
        public string ST { get; set; }
        public int Pageindex { get; set; }
        public int pagesize { get; set; }

        public int RowStart { get; set; }
        public int RowEnd { get; set; }
        public int RowNumber { get; set; }

        public List<EmployeeDirectoryEntity> lstEmployeeDirectoryEntity { get; set; }
        public List<FieldPermissionEntity> lstFieldPermissionEntity { get; set; }

        public List<FutureEntity> FutureDivRegDistEntityList { get; set; }

        public FormPermissionEntity _formPermission = null;
        public FormPermissionEntity FormPermission
        {
            get { return _formPermission; }
            set
            {
                this._formPermission = value;
                if (value != null)
                {
                    this.isAdd = value.IsAdd;
                    this.isEdit = value.IsEdit;
                    this.isDelete = value.IsDelete;
                    this.isView = value.IsView;
                }

            }
        }

        public bool _isFormpermissionNull = false;
        public bool isFormpermissionNull
        {
            get
            {
                return _isFormpermissionNull;
            }
            set
            {
                if (_formPermission == null)
                {
                    //FormPermission = new FormPermissionEntity();
                    _isFormpermissionNull = true;
                }
                else
                {
                    _isFormpermissionNull = false;
                }
                //this._isFormpermissionNull = value;
            }
        }

        public bool _isAdd = false;
        public bool isAdd
        {
            get
            {
                return _isAdd;
            }
            set
            {
                if (_formPermission == null)
                {
                    _isAdd = false;
                }
                else
                {
                    _isAdd = _formPermission.IsAdd;
                }

            }
        }

        public bool _isView = false;
        public bool isView
        {
            get
            {
                return _isView;
            }
            set
            {
                if (_formPermission == null)
                {
                    _isView = false;
                }
                else
                {
                    _isView = _formPermission.IsView;
                }

            }
        }

        public bool _isEdit = false;
        public bool isEdit
        {
            get
            {
                return _isEdit;
            }
            set
            {
                if (_formPermission == null)
                {
                    _isEdit = false;
                }
                else
                {
                    _isEdit = _formPermission.IsEdit;
                }

            }
        }

        public bool _isDelete = false;
        public bool isDelete
        {
            get
            {
                return _isDelete;
            }
            set
            {
                if (_formPermission == null)
                {
                    _isDelete = false;
                }
                else
                {
                    _isDelete = _formPermission.IsDelete;
                }

            }
        }

    }

    public class FutureEntity
    {


        public string DIV { get; set; }
        public string REG { get; set; }
        public string DIST { get; set; }

    }

    public class AllEmployeeDirectoryEntity
    {

        public string COID { get; set; }
        public string LOC { get; set; }
        public string SUBLOC { get; set; }
        public string EEID { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string SA { get; set; }
        public string TYP { get; set; }
        public string SH { get; set; }
        public string PP { get; set; }
        public string HRS { get; set; }
        public string PG { get; set; }
        public string ADDRESSONE { get; set; }
        public string ADDRESSTWO { get; set; }
        public string CITY { get; set; }
        public string ST { get; set; }
        public string ZIP { get; set; }
        public string BIRTHDATE { get; set; }
        public string HIREDATE { get; set; }
        public string TERMDATE { get; set; }
        public string TERMREASON { get; set; }
        public string EMAIL { get; set; }
        public string JOBTITLE { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string MiddleName { get; set; }
        public string Social { get; set; }
        public string PrimaryPhone { get; set; }
        public string DEPT { get; set; }
        public string WageRate { get; set; }
        public string Sex { get; set; }
        public string Ethnicity { get; set; }
        public string FilingStatus { get; set; }
        public string FederalExemptions { get; set; }
        public string LASTHIREDATE { get; set; }
        public string SeniorityDate { get; set; }
        public string customer_code { get; set; }
        public string state { get; set; }
        public string DIV { get; set; }
        public string REG { get; set; }
        public string DIST { get; set; }
        public List<FieldPermissionEntity> lstFieldPermissionEntity { get; set; }
        public List<EmployeeDirectoryEntity> lstEmployeeDirectoryEntity { get; set; }

    }

    public class JobTitleEntity
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string AppreviationInHRP { get; set; }
        public string WCCodeForHRP { get; set; }
        public string EEOJobCode { get; set; }
    }

    public class EmployeeDirectoryFilterParameter
    {
        public string UserName { get; set; }
        public DateTime ExportDatetime { get; set; }
        public string EEID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Social { get; set; }
        public string Department { get; set; }
        public string JobTitle { get; set; }
        public string DIV { get; set; }
        public string REG { get; set; }
        public string DIST { get; set; }
        public string State { get; set; }
        public string FacilityCode { get; set; }
        public string Status { get; set; }
    }

    
}