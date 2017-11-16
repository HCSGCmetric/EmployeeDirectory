using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Business
{
    public class FieldMasterBusiness
    {

        public FieldMasterBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<GroupMasterEntity> GetGroupMasterEntitylst()
        {

            try
            {
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllGroupMasterDetailList");
                List<GroupMasterEntity> lstGroupMasterEntity = new List<GroupMasterEntity>();
                if (dt.Rows.Count > 0)
                {
                    lstGroupMasterEntity = dt.DataTableToList<GroupMasterEntity>();
                }

                return lstGroupMasterEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }


        public List<FieldMasterEntity> GetFieldMasterEntitylst(int ID)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, ID);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllFieldMasterEntitylst", parameter);
                List<FieldMasterEntity> lstFieldMasterEntity = new List<FieldMasterEntity>();
                if (dt.Rows.Count > 0)
                {
                    lstFieldMasterEntity = dt.DataTableToList<FieldMasterEntity>();
                }

                return lstFieldMasterEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public int AssignFieldPremission(string FieldPermissionGridXML, int GroupId)
        {
            try
            {
             
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@FieldPermissionGridXML", DbType.String, FieldPermissionGridXML);
                parameter[1] = DbHelper.CreateParameter("@GroupId", DbType.String, GroupId);
                int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "AssignFieldPremission", parameter);
             
                StoreError(result.ToString()+"kd3");

                return result;
            }
            catch (Exception ex)
            {
                StoreError(ex.Message.ToString());
                throw new Exception("DAL Exception " + ex);
            }
        }

        public int StoreError(string errorMSG)
        {

            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ErrorMSg", DbType.String, errorMSG);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "InsertError", parameter);
            return result;

        }
    }
}