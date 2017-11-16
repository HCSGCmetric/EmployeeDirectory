
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
    public class EmployeeDirectoryCls
    {
        public EmployeeDirectoryCls()
        {
           DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
           //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<EmployeeDetailsList> GetEmployeeDetailsList(string firstName, string lastName, int rowStart, int rowEnd, string sortColumn, string sortDir)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[6];
                parameter[0] = DbHelper.CreateParameter("@FirstName", DbType.String, firstName);
                parameter[1] = DbHelper.CreateParameter("@LastName", DbType.String, lastName);
                parameter[2] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
                parameter[3] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
                parameter[4] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
                parameter[5] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserList", parameter);
                List<EmployeeDetailsList> listEmployeeDetails = new List<EmployeeDetailsList>();
                if (dt.Rows.Count > 0)
                {
                    listEmployeeDetails = dt.DataTableToList<EmployeeDetailsList>();
                }

                return listEmployeeDetails;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }

        public UserDetails GetUserDetailsByID(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.Int32, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
            List<UserDetails> listUserDetails = new List<UserDetails>();
            if (dt.Rows.Count > 0)
            {
                listUserDetails = dt.DataTableToList<UserDetails>();
            }
            return listUserDetails.FirstOrDefault();
        }

        public int SaveUserRecord(UserDetails userDetails, List<DivRegDistEntity> divRegDistEntityList)
        {
            try
            {
                int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGReporting"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();
                foreach (DivRegDistEntity divRegDistEntity in divRegDistEntityList)
                {
                    DbParameter[] parameter = new DbParameter[11];
                    parameter[0] = DbHelper.CreateParameter("@LastName", DbType.String, userDetails.LASTNAME);
                    parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, userDetails.FIRSTNAME);
                    parameter[2] = DbHelper.CreateParameter("@UserName", DbType.String, userDetails.USERNAME);
                    parameter[3] = DbHelper.CreateParameter("@Title", DbType.String, divRegDistEntity.Title);
                    parameter[4] = DbHelper.CreateParameter("@Division", DbType.String, divRegDistEntity.Div);
                    parameter[5] = DbHelper.CreateParameter("@Region", DbType.String, divRegDistEntity.Reg);
                    parameter[6] = DbHelper.CreateParameter("@District", DbType.String, divRegDistEntity.Dist);
                    parameter[7] = DbHelper.CreateParameter("@EEID", DbType.String, userDetails.EEID);
                    parameter[8] = DbHelper.CreateParameter("@UpdatedBy", DbType.String, userDetails.UPDATEDBY);
                    parameter[9] = DbHelper.CreateParameter("@Email", DbType.String, userDetails.EMAIL);
                    parameter[10] = DbHelper.CreateParameter("@Status", DbType.String, userDetails.STATUS);
                    //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                    result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Insert_HSG_Users", parameter);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<DivRegDistEntity> GetDivRegDistByID(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDivRegDistListId", parameter);

            List<DivRegDistEntity> listDivRegDistEntity = new List<DivRegDistEntity>();
            if (dt.Rows.Count > 0)
            {
                listDivRegDistEntity = dt.DataTableToList<DivRegDistEntity>();
            }
            return listDivRegDistEntity;
        }

        public List<EmployeeDirectoryEntity> getEmployeeDirectoryList(string EEID, string firstName, string lastName, string DEPT, string Social, string FacilityCode, string JobTitle, string State, string DIV, string REG, string DIST, string Status, int rowStart, int rowEnd, string sortColumn, string sortDir)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[16];
                parameter[0] = DbHelper.CreateParameter("@EEID", DbType.String, EEID);
                parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, firstName);
                parameter[2] = DbHelper.CreateParameter("@LastName", DbType.String, lastName);
                parameter[3] = DbHelper.CreateParameter("@DepartmentCode", DbType.String, DEPT);
                parameter[4] = DbHelper.CreateParameter("@FacilityCode", DbType.String, FacilityCode);
                parameter[5] = DbHelper.CreateParameter("@JobTitle", DbType.String, JobTitle);
                parameter[6] = DbHelper.CreateParameter("@State", DbType.String, State);
                parameter[7] = DbHelper.CreateParameter("@Social", DbType.String, Social);
                parameter[8] = DbHelper.CreateParameter("@DIV", DbType.String, DIV);
                parameter[9] = DbHelper.CreateParameter("@REG", DbType.String, REG);
                parameter[10] = DbHelper.CreateParameter("@DIST", DbType.String, DIST);
                parameter[11] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
                parameter[12] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
                parameter[13] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
                parameter[14] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
                parameter[15] = DbHelper.CreateParameter("@Status", DbType.String, Status);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetEmployeeDirectoryList", parameter);
                List<EmployeeDirectoryEntity> listEmployeeDirectory = new List<EmployeeDirectoryEntity>();
                if (dt.Rows.Count > 0)
                {
                    listEmployeeDirectory = dt.DataTableToList<EmployeeDirectoryEntity>();
                }

                return listEmployeeDirectory;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }

        public DataTable getEmployeeDirectoryDataTable(string EEID, string firstName, string lastName, string DEPT, string Social, string FacilityCode, string JobTitle, string State, string DIV, string REG, string DIST, string Status, int rowStart, int rowEnd, string sortColumn, string sortDir)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[16];
                parameter[0] = DbHelper.CreateParameter("@EEID", DbType.String, EEID);
                parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, firstName);
                parameter[2] = DbHelper.CreateParameter("@LastName", DbType.String, lastName);
                parameter[3] = DbHelper.CreateParameter("@DepartmentCode", DbType.String, DEPT);
                parameter[4] = DbHelper.CreateParameter("@FacilityCode", DbType.String, FacilityCode);
                parameter[5] = DbHelper.CreateParameter("@JobTitle", DbType.String, JobTitle);
                parameter[6] = DbHelper.CreateParameter("@State", DbType.String, State);
                parameter[7] = DbHelper.CreateParameter("@Social", DbType.String, Social);
                parameter[8] = DbHelper.CreateParameter("@DIV", DbType.String, DIV);
                parameter[9] = DbHelper.CreateParameter("@REG", DbType.String, REG);
                parameter[10] = DbHelper.CreateParameter("@DIST", DbType.String, DIST);
                parameter[11] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
                parameter[12] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
                parameter[13] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
                parameter[14] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
                parameter[15] = DbHelper.CreateParameter("@Status", DbType.String, Status);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetEmployeeDirectoryList", parameter);
                return dt;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }

        public AllEmployeeDirectoryEntity getEmployeeDirectoryDetail(string ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getEmployeeDirectoryDetail", parameter);
            List<AllEmployeeDirectoryEntity> lstEmployeeDirectoryEntity = new List<AllEmployeeDirectoryEntity>();
            if (dt.Rows.Count > 0)
            {
                lstEmployeeDirectoryEntity = dt.DataTableToList<AllEmployeeDirectoryEntity>();
            }
            return lstEmployeeDirectoryEntity.FirstOrDefault();
        }


        public AllEmployeeDirectoryEntity getEmployeeDirectoryDetailByEEIDForAsset(string ID, int ItemId)
        {
            DbParameter[] parameter = new DbParameter[2];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.String, ID);
            parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, ItemId);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getEmployeeDirectoryDetailForAsset", parameter);
            List<AllEmployeeDirectoryEntity> lstEmployeeDirectoryEntity = new List<AllEmployeeDirectoryEntity>();
            if (dt.Rows.Count > 0)
            {
                lstEmployeeDirectoryEntity = dt.DataTableToList<AllEmployeeDirectoryEntity>();
            }
            return lstEmployeeDirectoryEntity.FirstOrDefault();
        }

        public List<EmployeeDirectoryEntity> getEmployeeListByDepartment(string DeptId)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@DeptId", DbType.String, DeptId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getEmployeeListByDepartment", parameter);

                List<EmployeeDirectoryEntity> listEmployeeDirectoryEntity = new List<EmployeeDirectoryEntity>();
                if (dt.Rows.Count > 0)
                {
                    listEmployeeDirectoryEntity = dt.DataTableToList<EmployeeDirectoryEntity>();
                }
                return listEmployeeDirectoryEntity;
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

        public List<JobTitleEntity> GetEmployeeJobTitleList(string UserName)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("UserName", DbType.String, UserName);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetEmployeeJobTitleList", parameter);

            List<JobTitleEntity> listJobTitleEntity = new List<JobTitleEntity>();
            if (dt.Rows.Count > 0)
            {
                listJobTitleEntity = dt.DataTableToList<JobTitleEntity>();
            }
            return listJobTitleEntity;
        }

        public List<IdNameEntity> GetArcustStateList(string UserName = "")
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("UserName", DbType.String, UserName);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetArcustStateList", parameter);

            List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
            if (dt.Rows.Count > 0)
            {
                listIdNameEntity = dt.DataTableToList<IdNameEntity>();
            }
            return listIdNameEntity;
        }

        public List<FutureEntity> getFutureDivRegDistList()
        {
            //Hashtable htParam = new Hashtable();
            //DataTable objDt = null;
            //objDt = GetDataSet("getFutureDivRegDistList", htParam).Tables[0];
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getFutureDivRegDistList");

            List<FutureEntity> listFutureEntity = new List<FutureEntity>();
            if (dt.Rows.Count > 0)
            {
                listFutureEntity = dt.DataTableToList<FutureEntity>();
            }
            return listFutureEntity;

        }

        public List<IdNameEntity> getFutureRegionList(string Div)
        {

            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("Div", DbType.String, Div);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getFutureRegionListByDivision", parameter);

            List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
            if (dt.Rows.Count > 0)
            {
                listIdNameEntity = dt.DataTableToList<IdNameEntity>();
            }
            return listIdNameEntity;

        }

        public List<IdNameEntity> getFutureDistictList(string Div, string Reg)
        {

            DbParameter[] parameter = new DbParameter[2];
            parameter[0] = DbHelper.CreateParameter("Div", DbType.String, Div);
            parameter[1] = DbHelper.CreateParameter("Reg", DbType.String, Reg);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getFutureDistictListByDivision", parameter);

            List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
            if (dt.Rows.Count > 0)
            {
                listIdNameEntity = dt.DataTableToList<IdNameEntity>();
            }
            return listIdNameEntity;

        }

        public int SaveExportFilterAuditReport(EmployeeDirectoryFilterParameter entity)
        {
            DbParameter[] parameter = new DbParameter[14];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, entity.UserName);
            parameter[1] = DbHelper.CreateParameter("@ExportDatetime", DbType.DateTime, entity.ExportDatetime);
            parameter[2] = DbHelper.CreateParameter("@EEID", DbType.String, entity.EEID);
            parameter[3] = DbHelper.CreateParameter("@FirstName", DbType.String, entity.FirstName);
            parameter[4] = DbHelper.CreateParameter("@LastName", DbType.String, entity.LastName);
            parameter[5] = DbHelper.CreateParameter("@Social", DbType.String, entity.Social);
            parameter[6] = DbHelper.CreateParameter("@Department", DbType.String, entity.Department);
            parameter[7] = DbHelper.CreateParameter("@JobTitle", DbType.String, entity.JobTitle);
            parameter[8] = DbHelper.CreateParameter("@DIV", DbType.String, entity.DIV);
            parameter[9] = DbHelper.CreateParameter("@REG", DbType.String, entity.REG);
            parameter[10] = DbHelper.CreateParameter("@DIST", DbType.String, entity.DIST);
            parameter[11] = DbHelper.CreateParameter("@State", DbType.String, entity.State);
            parameter[12] = DbHelper.CreateParameter("@FacilityCode", DbType.String, entity.FacilityCode);
            parameter[13] = DbHelper.CreateParameter("@Status", DbType.String, entity.Status);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "SaveEmployeeDirectoryExportAudit", parameter);
            return result;
        }
    }
}