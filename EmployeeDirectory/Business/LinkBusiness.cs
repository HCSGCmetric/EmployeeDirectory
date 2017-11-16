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
    public class LinkBusiness
    {
        public LinkBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<LinkEntity> GetLinkList(bool IsFieldUser, string UserName, string SearchValue)
        {
            
            DbParameter[] parameter = new DbParameter[3];
            parameter[0] = DbHelper.CreateParameter("@IsFieldUser", DbType.String, IsFieldUser);
            parameter[1] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[2] = DbHelper.CreateParameter("@SearchValue", DbType.String, SearchValue);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetLinkList", parameter);
            List<LinkEntity> listLinkEntity = new List<LinkEntity>();
            if (dt.Rows.Count > 0)
            {
                listLinkEntity = dt.DataTableToList<LinkEntity>();
            }

            return listLinkEntity;

        }

        public List<LinkEntity> GetAllLinkInformation(string UserName, int rowStart, int rowEnd, string sortColumn, string sortDir, LinkEntity objLinkEntity)
        {
            DbParameter[] parameter = new DbParameter[6];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[1] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[2] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[3] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[4] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[5] = DbHelper.CreateParameter("@Name", DbType.String, objLinkEntity.Name);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAllLinkInformation", parameter);
            List<LinkEntity> listLinkEntity = new List<LinkEntity>();
            if (dt.Rows.Count > 0)
            {
                listLinkEntity = dt.DataTableToList<LinkEntity>();
            }

            return listLinkEntity;
        }

        public int SaveLink(LinkEntity entity)
        {
                DbParameter[] parameter = new DbParameter[10];
                parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, entity.ID);
                parameter[1] = DbHelper.CreateParameter("@Name", DbType.String, entity.Name);
                parameter[2] = DbHelper.CreateParameter("@Link", DbType.String, entity.Link);
                parameter[3] = DbHelper.CreateParameter("@LinkDisplayIDs", DbType.String, entity.LinkDisplayIDs);
                parameter[4] = DbHelper.CreateParameter("@Description", DbType.String, entity.Description);
                parameter[5] = DbHelper.CreateParameter("@UserTypes", DbType.String, entity.UserTypes);
                parameter[6] = DbHelper.CreateParameter("@IsCommonForAll", DbType.String, entity.IsCommonForAll);
                parameter[7] = DbHelper.CreateParameter("@DepartmentId", DbType.String, entity.DepartmentId);
                parameter[8] = DbHelper.CreateParameter("@CreatedBy", DbType.String, entity.CreatedBy);
                parameter[9] = DbHelper.CreateParameter("@IsActive", DbType.String, entity.IsActive);
                int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "SaveLinkDetail", parameter);
                return result;
            
        }

        public LinkEntity getLinkDetailById(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.Int32, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getLinkDetailById", parameter);
            List<LinkEntity> listUserDetails = new List<LinkEntity>();
            if (dt.Rows.Count > 0)
            {
                listUserDetails = dt.DataTableToList<LinkEntity>();
            }
            return listUserDetails.FirstOrDefault();
        }

        public int UpdateLink(LinkEntity entity)
        {
                DbParameter[] parameter = new DbParameter[10];
                parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, entity.ID);
                parameter[1] = DbHelper.CreateParameter("@Name", DbType.String, entity.Name);
                parameter[2] = DbHelper.CreateParameter("@Link", DbType.String, entity.Link);
                parameter[3] = DbHelper.CreateParameter("@LinkDisplayIDs", DbType.String, entity.LinkDisplayIDs);
                parameter[4] = DbHelper.CreateParameter("@Description", DbType.String, entity.Description);
                parameter[5] = DbHelper.CreateParameter("@UserTypes", DbType.String, entity.UserTypes);
                parameter[6] = DbHelper.CreateParameter("@IsCommonForAll", DbType.String, entity.IsCommonForAll);
                parameter[7] = DbHelper.CreateParameter("@DepartmentId", DbType.String, entity.DepartmentId);
                parameter[8] = DbHelper.CreateParameter("@UpdatedBy", DbType.String, entity.UpdatedBy);
                parameter[9] = DbHelper.CreateParameter("@IsActive", DbType.String, entity.IsActive);
                int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateLinkDetail", parameter);
                return result;
          
        }

        public bool DeleteLink(int Id)
        {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@ID", DbType.Int32, Id);
                int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteLinkByid", parameter);
                return Convert.ToBoolean(result);
            
        }

        public int DeleteMultipleLink(string IDS)
        {

            try
            {

                
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@IDS", DbType.String, IDS);

                int isSuccess = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteMultipleLink", parameter);
                if (Convert.ToBoolean(isSuccess)) { return 1; } else { return 0; }

            }
            catch (Exception ex)
            {
                return 0;
            }
        }


    }
}