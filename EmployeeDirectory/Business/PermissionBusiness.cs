using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Business
{

    

    public class PermissionBusiness
    {
        public PermissionBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<FieldPermissionEntity> GetFieldPermissionInformations(string GroupId) {

            try
            {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@GroupId", DbType.String, GroupId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetFieldPermission",parameter);
                List<FieldPermissionEntity> lstFieldPermissionEntity = new List<FieldPermissionEntity>();
                if (dt.Rows.Count > 0)
                {
                    lstFieldPermissionEntity = dt.DataTableToList<FieldPermissionEntity>();
                }

                return lstFieldPermissionEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        
        
        }

        public List<FormPermissionEntity> GetFormPermissionInformations(string usertype, int? roleId, int? formID)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[3];
                parameter[0] = DbHelper.CreateParameter("@UserTypeId", DbType.String, usertype);
                parameter[1] = DbHelper.CreateParameter("@RoleId", DbType.String, roleId);
                parameter[2] = DbHelper.CreateParameter("@FormId", DbType.String, formID);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetFormPermissionInformation", parameter);
                List<FormPermissionEntity> lstFormPermissionEntity = new List<FormPermissionEntity>();
                if (dt.Rows.Count > 0)
                {
                    lstFormPermissionEntity = dt.DataTableToList<FormPermissionEntity>();
                }

                return lstFormPermissionEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }


    }
}