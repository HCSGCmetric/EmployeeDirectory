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
    public class GroupMasterBusiness
    {
        public GroupMasterBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<GroupMasterEntity> GetGroupMasterDetailList(string Name, string Description, int rowStart, int rowEnd, string sortColumn, string sortDir)
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
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetGroupMasterDetailList", parameter);
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


        public int SaveGroupMaster(GroupMasterEntity entity) 
        {
            DbParameter[] parameter = new DbParameter[4];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, entity.ID);
            parameter[1] = DbHelper.CreateParameter("@Name", DbType.String, entity.Name);
            parameter[2] = DbHelper.CreateParameter("@Description", DbType.String, entity.Description);
            parameter[3] = DbHelper.CreateParameter("@IsActive", DbType.String, entity.IsActive);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Insert_GroupMaster", parameter);
            return result;
        }


        public GroupMasterEntity GetGroupDetailById(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetGroupDetailById", parameter);
            List<GroupMasterEntity> objGroupMasterEntity = new List<GroupMasterEntity>();
            if (dt.Rows.Count > 0)
            {
                objGroupMasterEntity = dt.DataTableToList<GroupMasterEntity>();
            }

            return objGroupMasterEntity.FirstOrDefault();
        }


        public int DeleteGroupMaster(int ID) 
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, ID);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Delete_GroupMaster", parameter);
            return result;
        }


        public List<GroupMasterEntity> GetAllGroupMasterList()
        {

            try
            {
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllGroupMasterList");
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

    }
}