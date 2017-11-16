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
    public class HsgUsersBusiness
    {
        public HsgUsersBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGReporting"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();
        }

        public List<HSGUserEntity> GetHSGUserList(string firstName, string lastName, string username, string title, string div, string reg, string dist, string eeid, string status, int rowStart, int rowEnd, string sortColumn, string sortDir)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[13];
                parameter[0] = DbHelper.CreateParameter("@FirstName", DbType.String, firstName);
                parameter[1] = DbHelper.CreateParameter("@LastName", DbType.String, lastName);
                parameter[2] = DbHelper.CreateParameter("@UserName", DbType.String, username);
                parameter[3] = DbHelper.CreateParameter("@Title", DbType.String, title);
                parameter[4] = DbHelper.CreateParameter("@Div", DbType.String, div);
                parameter[5] = DbHelper.CreateParameter("@Reg", DbType.String, reg);
                parameter[6] = DbHelper.CreateParameter("@Dist", DbType.String, dist);
                parameter[7] = DbHelper.CreateParameter("@EEID", DbType.String, eeid);
                parameter[8] = DbHelper.CreateParameter("@Status", DbType.String, status);
                parameter[9] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
                parameter[10] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
                parameter[11] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
                parameter[12] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetHsgUsersList", parameter);
                List<HSGUserEntity> listUserDetails = new List<HSGUserEntity>();
                if (dt.Rows.Count > 0)
                {
                    listUserDetails = dt.DataTableToList<HSGUserEntity>();
                }

                return listUserDetails;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }

        public HSGUserEntity GetHSGUserDetailsByID(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetHSGUserDetialById", parameter);

            List<HSGUserEntity> listUserDetails = new List<HSGUserEntity>();
            if (dt.Rows.Count > 0)
            {
                listUserDetails = dt.DataTableToList<HSGUserEntity>();
            }
            return listUserDetails.FirstOrDefault();
        }

        public List<DivRegDistEntity> GetDivRegDistByID(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetDivRegDistListId", parameter);

            List<DivRegDistEntity> listDivRegDistEntity = new List<DivRegDistEntity>();
            if (dt.Rows.Count > 0)
            {
                listDivRegDistEntity = dt.DataTableToList<DivRegDistEntity>();
            }
            return listDivRegDistEntity;
        }

        public int UpdateHSGUserRecord(HSGUserEntity hsgUserEntity, List<DivRegDistEntity> divRegDistEntityList)
        {
            DbConnection Con = DbHelper.BeginTransactionConn();
            try
            {

                int result = 0;
                //DbParameter[] parameterDelete = new DbParameter[1];
                //parameterDelete[0] = DbHelper.CreateParameter("@EEID", DbType.String, hsgUserEntity.EEID);
                //result = DbHelper.ExecuteNonQueryWithTransaction(Con, CommandType.StoredProcedure, "Delete_HSG_Users_ByEEID", parameterDelete);
                //if (result > 0)
                //{
                string DivList = "";
                string RegList = "";
                string DistList = "";
                string ApprovalList = "";
                string TitleList = "";
                foreach (DivRegDistEntity entity in divRegDistEntityList)
                {
                    DivList = DivList + entity.Div + ",";
                    RegList = RegList + entity.Reg + ",";
                    DistList = DistList + entity.Dist + ",";
                    ApprovalList = ApprovalList + entity.Approve + ",";
                    TitleList = TitleList + entity.Title + ",";
                }
                DbParameter[] parameterDelete = new DbParameter[6];
                parameterDelete[0] = DbHelper.CreateParameter("@UserName", DbType.String, hsgUserEntity.UserName);
                parameterDelete[1] = DbHelper.CreateParameter("@AllDiv", DbType.String, DivList);
                parameterDelete[2] = DbHelper.CreateParameter("@AllReg", DbType.String, RegList);
                parameterDelete[3] = DbHelper.CreateParameter("@AllDist", DbType.String, DistList);
                parameterDelete[4] = DbHelper.CreateParameter("@AllTitle", DbType.String, TitleList);
                parameterDelete[5] = DbHelper.CreateParameter("@AllApprove", DbType.String, ApprovalList);

                result = DbHelper.ExecuteNonQueryWithTransaction(Con, CommandType.StoredProcedure, "Delete_HSG_Users_IfDivRegDistNotExists", parameterDelete);
                foreach (DivRegDistEntity divRegDistEntity in divRegDistEntityList)
                {
                    DbParameter[] parameter = new DbParameter[12];
                    //parameter[0] = DbHelper.CreateParameter("@Id", DbType.String, hsgUserEntity.Id);
                    parameter[0] = DbHelper.CreateParameter("@LastName", DbType.String, hsgUserEntity.LastName);
                    parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, hsgUserEntity.FirstName);
                    parameter[2] = DbHelper.CreateParameter("@UserName", DbType.String, hsgUserEntity.UserName);
                    parameter[3] = DbHelper.CreateParameter("@Title", DbType.String, divRegDistEntity.Title);
                    //parameter[4] = DbHelper.CreateParameter("@Division", DbType.String, hsgUserEntity.Div);
                    //parameter[5] = DbHelper.CreateParameter("@Region", DbType.String, hsgUserEntity.Reg);
                    //parameter[6] = DbHelper.CreateParameter("@District", DbType.String, hsgUserEntity.Dist);

                    parameter[4] = DbHelper.CreateParameter("@Division", DbType.String, divRegDistEntity.Div);
                    parameter[5] = DbHelper.CreateParameter("@Region", DbType.String, divRegDistEntity.Reg);
                    parameter[6] = DbHelper.CreateParameter("@District", DbType.String, divRegDistEntity.Dist);
                    parameter[7] = DbHelper.CreateParameter("@Approve", DbType.Boolean, divRegDistEntity.Approve == "1" ? true : false);
                    parameter[8] = DbHelper.CreateParameter("@EEID", DbType.String, hsgUserEntity.EEID);
                    parameter[9] = DbHelper.CreateParameter("@UpdatedBy", DbType.String, hsgUserEntity.UpdatedBy);
                    parameter[10] = DbHelper.CreateParameter("@Email", DbType.String, hsgUserEntity.Email);
                    parameter[11] = DbHelper.CreateParameter("@Status", DbType.String, hsgUserEntity.Status);
                    result = DbHelper.ExecuteNonQueryWithTransaction(Con, CommandType.StoredProcedure, "Insert_HSG_Users", parameter);
                }
                //}
                DbHelper.CommintTransactionWithCloseConnection(Con);
                return result;
            }
            catch (Exception ex)
            {
                DbHelper.RollBackTransactionCloseConnection(Con);
                throw new Exception(ex.Message.ToString());
            }

        }

        public int SaveHSGUserRecord(HSGUserEntity hsgUserEntity, List<DivRegDistEntity> divRegDistEntityList)
        {
            int result = 0;
            foreach (DivRegDistEntity divRegDistEntity in divRegDistEntityList)
            {
                DbParameter[] parameter = new DbParameter[12];
                parameter[0] = DbHelper.CreateParameter("@LastName", DbType.String, hsgUserEntity.LastName);
                parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, hsgUserEntity.FirstName);
                parameter[2] = DbHelper.CreateParameter("@UserName", DbType.String, hsgUserEntity.UserName);
                parameter[3] = DbHelper.CreateParameter("@Title", DbType.String, divRegDistEntity.Title);
                parameter[4] = DbHelper.CreateParameter("@Division", DbType.String, divRegDistEntity.Div);
                parameter[5] = DbHelper.CreateParameter("@Region", DbType.String, divRegDistEntity.Reg);
                parameter[6] = DbHelper.CreateParameter("@District", DbType.String, divRegDistEntity.Dist);
                parameter[7] = DbHelper.CreateParameter("@Approve", DbType.Boolean, divRegDistEntity.Approve == "1" ? true : false);
                parameter[8] = DbHelper.CreateParameter("@EEID", DbType.String, hsgUserEntity.EEID);
                parameter[9] = DbHelper.CreateParameter("@UpdatedBy", DbType.String, hsgUserEntity.UpdatedBy);
                parameter[10] = DbHelper.CreateParameter("@Email", DbType.String, hsgUserEntity.Email);
                parameter[11] = DbHelper.CreateParameter("@Status", DbType.String, hsgUserEntity.Status);
                result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Insert_HSG_Users", parameter);
            }
            return result;
        }

        public int DeleteHSGUserRecord(int id)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@Id", DbType.String, id);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "Delete_HSG_Users", parameter);
            return result;
        }

        public HSGUserEntity GetHSGUserDetailsByID(string username)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@Username", DbType.String, username);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetHsgUserDetail", parameter);

                List<HSGUserEntity> listUserDetails = new List<HSGUserEntity>();
                if (dt.Rows.Count > 0)
                {
                    listUserDetails = dt.DataTableToList<HSGUserEntity>();
                }
                return listUserDetails.FirstOrDefault();
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

        public string VerifyHSGUsersApproval(string username, string allDiv, string allReg,string allDist, string allTitle, string allApprove)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[6];
                parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, username);
                parameter[1] = DbHelper.CreateParameter("@AllDiv", DbType.String, allDiv);
                parameter[2] = DbHelper.CreateParameter("@AllReg", DbType.String, allReg);
                parameter[3] = DbHelper.CreateParameter("@AllDist", DbType.String, allDist);
                parameter[4] = DbHelper.CreateParameter("@AllTitle", DbType.String, allTitle);
                parameter[5] = DbHelper.CreateParameter("@AllApprove", DbType.String, allApprove);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "VarifyHSG_UsersApproval", parameter);
                return result.ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int UpdateApprovalForHSGUsers(string username, string allDiv, string allReg, string allDist, string allTitle, string allApprove)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[6];
                parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, username);
                parameter[1] = DbHelper.CreateParameter("@AllDiv", DbType.String, allDiv);
                parameter[2] = DbHelper.CreateParameter("@AllReg", DbType.String, allReg);
                parameter[3] = DbHelper.CreateParameter("@AllDist", DbType.String, allDist);
                parameter[4] = DbHelper.CreateParameter("@AllTitle", DbType.String, allTitle);
                parameter[5] = DbHelper.CreateParameter("@AllApprove", DbType.String, allApprove);
                var result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "UpdateApprovalForHSGUsers", parameter);
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}