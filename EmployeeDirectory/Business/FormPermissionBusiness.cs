
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeDirectory.Business
{
    public class FormPermissionBusiness
    {
        public FormPermissionBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<FormPermissionEntity> GetFormPermissionList(int UserTypeId)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("UserTypeId", DbType.String, UserTypeId);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetFormPermissionList", parameter);

            List<FormPermissionEntity> lstFormPermissionEntity = new List<FormPermissionEntity>();
            if (dt.Rows.Count > 0)
            {
                lstFormPermissionEntity = dt.DataTableToList<FormPermissionEntity>();
            }
            return lstFormPermissionEntity;


        }

        public int CreateFormPermission(FormPermissionEntity entity)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@UserType", DbType.Int32, entity.UserType);
                parameter[1] = DbHelper.CreateParameter("@FormPermissionGridXML", DbType.String, entity.FormPermissionGridXML);
                int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "CreateFormPermission", parameter);
                return result;
            }
            catch (Exception ex)
            {
                new FieldMasterBusiness().StoreError(ex.Message.ToString());
                throw new Exception("CreateFormPermission Exception :" + ex);
            }
            


        }


    }
}