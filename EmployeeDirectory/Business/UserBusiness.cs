using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Business
{
    /// <summary>
    /// 
    /// </summary>
    public class UserBusiness
    {
        public UserBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        /// <summary>
        /// Gets the admin user list.
        /// </summary>
        /// <returns></returns>
        public List<AdminUserEntity> GetAdminUserList(string firstName, string lastName, string username, string UserType, int rowStart, int rowEnd, string sortColumn, string sortDir)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[8];
                parameter[0] = DbHelper.CreateParameter("@FirstName", DbType.String, firstName);
                parameter[1] = DbHelper.CreateParameter("@LastName", DbType.String, lastName);
                parameter[2] = DbHelper.CreateParameter("@UserName", DbType.String, username);
                parameter[3] = DbHelper.CreateParameter("@UserType", DbType.String, UserType);
                parameter[4] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
                parameter[5] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
                parameter[6] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
                parameter[7] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAdminUserList", parameter);
                List<AdminUserEntity> listAdminUser = new List<AdminUserEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAdminUser = dt.DataTableToList<AdminUserEntity>();
                }

                return listAdminUser;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }


        public List<AdminUserEntity> GetEmployeeDirectoryUserList() 
        {
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetEmployeeDirectoryUserList");
            List<AdminUserEntity> adminUserEntity = new List<AdminUserEntity>();
            if (dt.Rows.Count > 0)
            {
                adminUserEntity = dt.DataTableToList<AdminUserEntity>();
            }
            return adminUserEntity;
        }

        /// <summary>
        /// Saves the admin user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int SaveAdminUser(AdminUserEntity entity) 
        {
            DbParameter[] parameter = new DbParameter[9];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, entity.UserName);
            parameter[1] = DbHelper.CreateParameter("@Password", DbType.String, entity.Password);
            parameter[2] = DbHelper.CreateParameter("@FirstName", DbType.String, entity.FirstName);
            parameter[3] = DbHelper.CreateParameter("@LastName", DbType.String, entity.LastName);
            parameter[4] = DbHelper.CreateParameter("@Email", DbType.String, entity.Email);
            parameter[5] = DbHelper.CreateParameter("@UserType", DbType.String, entity.UserType);
            parameter[6] = DbHelper.CreateParameter("@GroupId", DbType.String, entity.GroupId);
            parameter[7] = DbHelper.CreateParameter("@InvalidAttemptCounter", DbType.Int32, entity.InvalidAttemptCounter);
            parameter[8] = DbHelper.CreateParameter("@IsLocked", DbType.Boolean, entity.IsLocked);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Insert_AdminUserDetails", parameter);
            return result;
        }


        /// <summary>
        /// Updates the admin user.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns></returns>
        public int UpdateAdminUser(AdminUserEntity entity)
        {
            DbParameter[] parameter = new DbParameter[8];
            parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, entity.ID);
            parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, entity.FirstName);
            parameter[2] = DbHelper.CreateParameter("@LastName", DbType.String, entity.LastName);
            parameter[3] = DbHelper.CreateParameter("@Email", DbType.String, entity.Email);
            parameter[4] = DbHelper.CreateParameter("@UserType", DbType.String, entity.UserType);
            parameter[5] = DbHelper.CreateParameter("@GroupId", DbType.String, entity.GroupId);
            parameter[6] = DbHelper.CreateParameter("@IsLocked", DbType.Boolean, entity.IsLocked);
            parameter[7] = DbHelper.CreateParameter("@UpdatedBy", DbType.Int32, entity.UpdatedBy);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Update_AdminUserDetails", parameter);
            return result;
        }


        /// <summary>
        /// Deletes the admin user.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public int DeleteAdminUser(int Id)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, Id);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Delete_AdminUser", parameter);
            return result;
        }


        /// <summary>
        /// Gets the admin user detail by identifier.
        /// </summary>
        /// <param name="Id">The identifier.</param>
        /// <returns></returns>
        public AdminUserEntity GetAdminUserDetailById(int Id)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@Id", DbType.String, Id);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAdminDetailsByID", parameter);
            List<AdminUserEntity> adminUserEntity = new List<AdminUserEntity>();
            if (dt.Rows.Count > 0)
            {
                adminUserEntity = dt.DataTableToList<AdminUserEntity>();
            }

            return adminUserEntity.FirstOrDefault();
        }

        public bool CheckUserAlreadyExists(string username)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, username);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "CheckUserAlreadyExist", parameter);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else 
            { 
                return false; 
            }
            
        }

        public bool CheckEmpUserAlreadyExists(string username)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, username);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "CheckEmpUserAlreadyExists", parameter);
            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        /// <summary>
        /// Admins the user login.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="password">The password.</param>
        /// <returns></returns>
        public AdminUserEntity AdminUserLogin(string userName, string password = "")
        {
            try
            {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, userName);
                //parameter[1] = DbHelper.CreateParameter("@Password", DbType.String, password);
                
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "CheckAdminUserLogin", parameter);
                
                List<AdminUserEntity> adminUserEntity = new List<AdminUserEntity>();
                if (dt.Rows.Count > 0)
                {
                    adminUserEntity = dt.DataTableToList<AdminUserEntity>();
                }
                
                return adminUserEntity.FirstOrDefault();
                
            }
            catch (Exception ex)
            {
                StoreError(ex.Message.ToString());
                StoreError(ex.InnerException.Message.ToString());
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

        /// <summary>
        /// Checks the admin user invalid attampt update.
        /// </summary>
        /// <param name="userName">Name of the user.</param>
        /// <param name="invalidCounter">The invalid counter.</param>
        /// <returns></returns>
        public string CheckAdminUserInvalidAttamptUpdate(string userName, int invalidCounter)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, userName);
            parameter[1] = DbHelper.CreateParameter("@InvalidCounter", DbType.Int32, invalidCounter);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "CheckAdminUserInvalidAttamptUpdate", parameter);
            List<AdminUserEntity> adminUserEntity = new List<AdminUserEntity>();
            if (dt.Rows.Count > 0)
            {
                adminUserEntity = dt.DataTableToList<AdminUserEntity>();
            }

            if (adminUserEntity.FirstOrDefault().IsLocked == true)
            {
                return "Login attempts reached a maximum of 5 tries. Contact support at the number below or try again later.";
            }
            else 
            { 
                return ""; 
            }
        }

        public List<UserTypeEntity> GetAllUserTypeList()
        {
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllUserTypeList");
            List<UserTypeEntity> lstUserTypeEntity = new List<UserTypeEntity>();
            if (dt.Rows.Count > 0)
            {
                lstUserTypeEntity = dt.DataTableToList<UserTypeEntity>();
            }
            return lstUserTypeEntity;
        }

        public List<IdNameEntity> GetAllHeadTypeList()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGReporting"].ToString();
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllHeadTypeList");
            List<IdNameEntity> lstIdNameEntity = new List<IdNameEntity>();
            if (dt.Rows.Count > 0)
            {
                lstIdNameEntity = dt.DataTableToList<IdNameEntity>();
            }
            return lstIdNameEntity;
        }
        

    }
}