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
    public class ArcustViewBusiness
    {
        public ArcustViewBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public List<ArcustViewEntity> GetArcustViewList(ArcustViewSearchEntity arcustViewSearchEntity)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[25];
                parameter[0] = DbHelper.CreateParameter("@FacilityCode", DbType.String, arcustViewSearchEntity.FacilityCode);
                parameter[1] = DbHelper.CreateParameter("@FacilityName", DbType.String, arcustViewSearchEntity.FacilityName);
                parameter[2] = DbHelper.CreateParameter("@ManagementCode", DbType.String, arcustViewSearchEntity.ManagementCode);
                parameter[3] = DbHelper.CreateParameter("@ManagementName", DbType.String, arcustViewSearchEntity.ManagementName);
                parameter[4] = DbHelper.CreateParameter("@TerritoryCode", DbType.String, arcustViewSearchEntity.TerritoryCode);
                parameter[5] = DbHelper.CreateParameter("@CollectorCode", DbType.String, arcustViewSearchEntity.CollectorCode);
                parameter[6] = DbHelper.CreateParameter("@PayrollCode", DbType.String, arcustViewSearchEntity.PayrollCode);
                parameter[7] = DbHelper.CreateParameter("@DepartmentCode", DbType.String, arcustViewSearchEntity.DepartmentCode);
                parameter[8] = DbHelper.CreateParameter("@City", DbType.String, arcustViewSearchEntity.City);
                parameter[9] = DbHelper.CreateParameter("@State", DbType.String, arcustViewSearchEntity.State);
                parameter[10] = DbHelper.CreateParameter("@StartDate", DbType.DateTime, arcustViewSearchEntity.StartDate);
                parameter[11] = DbHelper.CreateParameter("@EndDate", DbType.DateTime, arcustViewSearchEntity.EndDate);
                parameter[12] = DbHelper.CreateParameter("@TermStartDate", DbType.DateTime, arcustViewSearchEntity.TermStartDate);
                parameter[13] = DbHelper.CreateParameter("@TermEndDate", DbType.DateTime, arcustViewSearchEntity.TermEndDate);
                parameter[14] = DbHelper.CreateParameter("@Address", DbType.String, arcustViewSearchEntity.Address);
                parameter[15] = DbHelper.CreateParameter("@ZipCode", DbType.String, arcustViewSearchEntity.ZipCode);
                parameter[16] = DbHelper.CreateParameter("@Div", DbType.String, arcustViewSearchEntity.Div);
                parameter[17] = DbHelper.CreateParameter("@Reg", DbType.String, arcustViewSearchEntity.Reg);
                parameter[18] = DbHelper.CreateParameter("@Dist", DbType.String, arcustViewSearchEntity.Dist);
                parameter[19] = DbHelper.CreateParameter("@Status", DbType.String, arcustViewSearchEntity.Status);
                parameter[20] = DbHelper.CreateParameter("@RowStart", DbType.Int32, arcustViewSearchEntity.RowStart);
                parameter[21] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, arcustViewSearchEntity.RowEnd);
                parameter[22] = DbHelper.CreateParameter("@Sort", DbType.String, arcustViewSearchEntity.Sort);
                parameter[23] = DbHelper.CreateParameter("@SortDirection", DbType.String, arcustViewSearchEntity.SortDirection);

                if (arcustViewSearchEntity.isFieldUser)
                {
                    parameter[24] = DbHelper.CreateParameter("@UserName", DbType.String, null);
                }
                else
                {
                    parameter[24] = DbHelper.CreateParameter("@UserName", DbType.String, arcustViewSearchEntity.UserName);
                }
               
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetHSG_Arcust_VwList", parameter);
                List<ArcustViewEntity> listArcustViewEntity = new List<ArcustViewEntity>();
                if (dt.Rows.Count > 0)
                {
                    listArcustViewEntity = dt.DataTableToList<ArcustViewEntity>();
                }

                return listArcustViewEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }

        public ArcustViewEntity GetArcustDetailByFacilityCode(string facilityCode)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@FacilityCode", DbType.String, facilityCode);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetArcustVwDetailsByFacilityCode", parameter);
            List<ArcustViewEntity> listArcustViewEntity = new List<ArcustViewEntity>();
            if (dt.Rows.Count > 0)
            {
                listArcustViewEntity = dt.DataTableToList<ArcustViewEntity>();
            }

            return listArcustViewEntity.FirstOrDefault();
        }


        public ArcustViewEntity GetDetailsByfacilityCodeForAsset(string facilityCode,int ItemId)
        {
            DbParameter[] parameter = new DbParameter[2];
            parameter[0] = DbHelper.CreateParameter("@FacilityCode", DbType.String, facilityCode);
            parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, ItemId);
            
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetArcustVwDetailsByFacilityCodeForAsset", parameter);
            List<ArcustViewEntity> listArcustViewEntity = new List<ArcustViewEntity>();
            if (dt.Rows.Count > 0)
            {
                listArcustViewEntity = dt.DataTableToList<ArcustViewEntity>();
            }

            return listArcustViewEntity.FirstOrDefault();
        }


        public List<ArcustViewSearchEntity> getBuildingListByDepartment(string Department)
        {
            try
            {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@DeptId", DbType.String, Department);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetHSG_BuildingListByDepartment", parameter);
                List<ArcustViewSearchEntity> listArcustViewEntity = new List<ArcustViewSearchEntity>();
                if (dt.Rows.Count > 0)
                {
                    listArcustViewEntity = dt.DataTableToList<ArcustViewSearchEntity>();
                }

                return listArcustViewEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        }

        public List<EmployeeDirectoryEntity> getEmployeeListByDepartment(string eeid, string firstname, string lastname, string jobtitle, string status, string Department, int RowStart, int RowEnd, string Sort, string SortDirection)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[10];


                parameter[0] = DbHelper.CreateParameter("@EEID", DbType.String, eeid);
                parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, firstname);
                parameter[2] = DbHelper.CreateParameter("@LastName", DbType.String, lastname);
                parameter[3] = DbHelper.CreateParameter("@JobTitle", DbType.String, jobtitle);
                parameter[4] = DbHelper.CreateParameter("@Status", DbType.String, status);


                parameter[5] = DbHelper.CreateParameter("@DeptId", DbType.String, Department);
                parameter[6] = DbHelper.CreateParameter("@RowStart", DbType.Int32, RowStart);
                parameter[7] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, RowEnd);
                parameter[8] = DbHelper.CreateParameter("@Sort", DbType.String, Sort);
                parameter[9] = DbHelper.CreateParameter("@SortDirection", DbType.String, SortDirection);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getArcustEmployeeListByDepartment", parameter);
                List<EmployeeDirectoryEntity> lstEmployeeDirectoryEntity = new List<EmployeeDirectoryEntity>();
                if (dt.Rows.Count > 0)
                {
                    lstEmployeeDirectoryEntity = dt.DataTableToList<EmployeeDirectoryEntity>();
                }

                return lstEmployeeDirectoryEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        
        
        
        }


        public DataTable getEmployeeDatatableByDepartment(string eeid, string firstname, string lastname, string jobtitle, string status, string Department)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[6];
                parameter[0] = DbHelper.CreateParameter("@EEID", DbType.String, eeid);
                parameter[1] = DbHelper.CreateParameter("@FirstName", DbType.String, firstname);
                parameter[2] = DbHelper.CreateParameter("@LastName", DbType.String, lastname);
                parameter[3] = DbHelper.CreateParameter("@JobTitle", DbType.String, jobtitle);
                parameter[4] = DbHelper.CreateParameter("@Status", DbType.String, status);
                parameter[5] = DbHelper.CreateParameter("@DeptId", DbType.String, Department);
                DataTable dt = null;
                dt = DbHelper.ExecuteTable(CommandType.StoredProcedure,"getArcustEmployeeDatatableListByDepartment",parameter);
                
                return dt;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }
        
        }



    }
}