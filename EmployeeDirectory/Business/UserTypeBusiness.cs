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
    public class UserTypeBusiness
    {
        public UserTypeBusiness()
        {
           DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<UserTypeEntity> GetHSGUserTypeList(string Name, string Description, int rowStart, int rowEnd, string sortColumn, string sortDir)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[6];
                parameter[0] = DbHelper.CreateParameter("@Name", DbType.String, Name);
                parameter[1] = DbHelper.CreateParameter("@Description", DbType.String, Description);
                parameter[2] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
                parameter[3] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
                parameter[4] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
                parameter[5] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllUserTypeInformation", parameter);
                List<UserTypeEntity> lstUserTypeEntity = new List<UserTypeEntity>();
                if (dt.Rows.Count > 0)
                {
                    lstUserTypeEntity = dt.DataTableToList<UserTypeEntity>();
                }

                return lstUserTypeEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }

        public int SaveUserType(UserTypeEntity entity)
        {
            DbParameter[] parameter = new DbParameter[4];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, entity.ID);
            parameter[1] = DbHelper.CreateParameter("@Name", DbType.String, entity.Name);
            parameter[2] = DbHelper.CreateParameter("@Description", DbType.String, entity.Description);
            parameter[3] = DbHelper.CreateParameter("@IsActive", DbType.String, entity.IsActive);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Insert_UserType", parameter);
            return result;
        }

        public UserTypeEntity GetUserTypeDetailById(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserTypeDetailById", parameter);
            List<UserTypeEntity> objUserTypeEntity = new List<UserTypeEntity>();
            if (dt.Rows.Count > 0)
            {
                objUserTypeEntity = dt.DataTableToList<UserTypeEntity>();
            }

            return objUserTypeEntity.FirstOrDefault();
        }


        public int DeleteUserType(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, ID);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteUserType", parameter);
            return result;
        }

        public List<UserTypeEntity> GetAllUserTypeList()
        {

            try
            {
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllUserTypeList");
                List<UserTypeEntity> lstUserTypeEntity = new List<UserTypeEntity>();
                if (dt.Rows.Count > 0)
                {
                    lstUserTypeEntity = dt.DataTableToList<UserTypeEntity>();
                }

                return lstUserTypeEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }



    }
}