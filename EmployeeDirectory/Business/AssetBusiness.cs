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
    public class AssetBusiness
    {
        public AssetBusiness()
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGEmployeeDirectory").ToString();
        }

        public int SaveAssetPurchaseOrder(AssetEntity objAssetEntity)
        {
            try
            {
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[11];
                parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, objAssetEntity.Id);
                parameter[1] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
                parameter[2] = DbHelper.CreateParameter("@PODate", DbType.Date, objAssetEntity.PODate);
                parameter[3] = DbHelper.CreateParameter("@POVendorID", DbType.Int32, objAssetEntity.POVendorID);
                parameter[4] = DbHelper.CreateParameter("@POTax", DbType.Decimal, objAssetEntity.POTax);
                parameter[5] = DbHelper.CreateParameter("@POShipping", DbType.Decimal, objAssetEntity.POShipping);
                parameter[6] = DbHelper.CreateParameter("@POTotal", DbType.Decimal, objAssetEntity.POTotal);
                parameter[7] = DbHelper.CreateParameter("@PurchaseOrderXML", DbType.String, objAssetEntity.PurchaseOrderXML);
                parameter[8] = DbHelper.CreateParameter("@InvoiceNumber", DbType.String, objAssetEntity.InvoiceNumber);
                parameter[9] = DbHelper.CreateParameter("@InvoiceDate", DbType.Date, objAssetEntity.InvoiceDate);
                parameter[10] = DbHelper.CreateParameter("@UserId", DbType.Int32, objAssetEntity.UserId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetPurchaseOrder", parameter);
                return int.Parse(result.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveAssetItem(AssetEntity objAssetEntity)
        {
            try
            {
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[5];
                parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, objAssetEntity.Id);
                parameter[1] = DbHelper.CreateParameter("@ItemName", DbType.String, objAssetEntity.ItemName);
                parameter[2] = DbHelper.CreateParameter("@ItemDescription", DbType.String, objAssetEntity.ItemDescription);
                parameter[3] = DbHelper.CreateParameter("@ItemCategoryID", DbType.Int32, objAssetEntity.ItemCategoryID);
                parameter[4] = DbHelper.CreateParameter("@UserId", DbType.Int32, objAssetEntity.UserId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetItem", parameter);
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int SaveAssetVendor(AssetEntity objAssetEntity)
        {
            try
            {
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[4];
                parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, objAssetEntity.Id);
                parameter[1] = DbHelper.CreateParameter("@POVendorName", DbType.String, objAssetEntity.POVendorName);
                parameter[2] = DbHelper.CreateParameter("@CompanyName", DbType.String, objAssetEntity.CompanyName);
                parameter[3] = DbHelper.CreateParameter("@UserId", DbType.Int32, objAssetEntity.UserId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
               var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetVendor", parameter);
                return int.Parse(result.ToString());

                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int GetAvailableAssetIsNewAssignOrReAssign(AssetAssignEntity objAssetAssignEntity)
        {
            try
            {
                //int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[3];
                parameter[0] = DbHelper.CreateParameter("@ItemID", DbType.Int32, objAssetAssignEntity.ItemId);
                parameter[1] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetAssignEntity.PONumber);
                parameter[2] = DbHelper.CreateParameter("@POID", DbType.Int32, objAssetAssignEntity.POId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                //result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "GetAvailableAssetIsNewAssignOrReAssign", parameter);
                //return result;
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "GetAvailableAssetIsNewAssignOrReAssign", parameter);
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssetEntity> GetPurchageOrderList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetPurchageOrderList");
                List<AssetEntity> listAssetEntity = new List<AssetEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public string getNextPONumber() {

            try
            {
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();

                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "getNextPONumber");
                return result.ToString();


            }
            catch (Exception ex)
            {
                throw ex;
            }
        
        }

        public List<IdNameEntity> GetPoVendorList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetPoVendorList");
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<IdNameEntity> getItemNameList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getItemNameList");
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<IdNameEntity> GetItemCategoryList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetItemCategoryList");
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public AssetEntity GetPurchageOrderDetailById(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.Int32, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetPurchageOrderDetailById", parameter);
            List<AssetEntity> listAssetEntity = new List<AssetEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetEntity>();
            }
            return listAssetEntity.FirstOrDefault();
        }

        public List<AssetEntity> GetPurchageOrderItemDetailByPO(string PONumber)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, PONumber);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetPurchageOrderItemDetailByPONo", parameter);
                List<AssetEntity> listAssetEntity = new List<AssetEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<AssetEntity> GetAllPurchaseOrderInformation(string UserName, int rowStart, int rowEnd, string sortColumn, string sortDir, AssetEntity objAssetEntity, string PoFromDate, string PoToDate)
        {
            DbParameter[] parameter = new DbParameter[9];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[1] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[2] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[3] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[4] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[5] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
            parameter[6] = DbHelper.CreateParameter("@POVendorID", DbType.String, objAssetEntity.POVendorID);
            parameter[7] = DbHelper.CreateParameter("@PoFromDate", DbType.String, PoFromDate);
            parameter[8] = DbHelper.CreateParameter("@PoToDate", DbType.String, PoToDate);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetPurchageOrderList", parameter);
            List<AssetEntity> listAssetEntity = new List<AssetEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetEntity>();
            }

            return listAssetEntity;
        }

        public List<IdNameEntity> getAssetTagList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getAssetTagList");
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }


        }


        public List<IdNameEntity> GetReAssignAssetTagList(string PONumber,int ItemId)
        {

            try
            {
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, PONumber);
                parameter[1] = DbHelper.CreateParameter("@ItemID", DbType.Int32, ItemId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetReAssignAssetTagList", parameter);
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }


        }

        public List<IdNameEntity> getAssetAllTagList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getAssetAllTagList");
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }


        }

        public List<IdNameEntity> getPONumberList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getPONumberList");
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }


        }

        public List<AssetAssignEntity> GetPODetailByItemId(int ItemId)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ItemId", DbType.Int32, ItemId);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetPODetailByItemId", parameter);
            List<AssetAssignEntity> listAssetAssignEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetAssignEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetAssignEntity;
        }

        public int SaveAssetAssignOrder(AssetAssignEntity objAssetEntity)
        {
            try
            {
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[14];
                parameter[0] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
                parameter[1] = DbHelper.CreateParameter("@ItemID", DbType.Int32, objAssetEntity.ItemId);
                parameter[2] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
                parameter[3] = DbHelper.CreateParameter("@AdditionalWarrantyMonth", DbType.Int32, objAssetEntity.AdditionalWarrantyMonth);
                parameter[4] = DbHelper.CreateParameter("@AssignDate", DbType.String, objAssetEntity.AssignDate);
                parameter[5] = DbHelper.CreateParameter("@IsEmployee", DbType.Boolean, objAssetEntity.IsEmployee);
                parameter[6] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
                parameter[7] = DbHelper.CreateParameter("@EmployeeName", DbType.String, objAssetEntity.EmployeeName);
                parameter[8] = DbHelper.CreateParameter("@IsFacility", DbType.Boolean, objAssetEntity.IsFacility);
                parameter[9] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
                parameter[10] = DbHelper.CreateParameter("@FacilityName", DbType.String, objAssetEntity.FacilityName);
                parameter[11] = DbHelper.CreateParameter("@POId", DbType.Int32, objAssetEntity.POId);
                parameter[12] = DbHelper.CreateParameter("@ReAssign", DbType.Boolean, objAssetEntity.ReAssign);
                parameter[13] = DbHelper.CreateParameter("@UserId", DbType.Int32, objAssetEntity.UserId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetAssignOrder", parameter);

                return int.Parse(result.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int SaveAssetReAssignOrder(AssetAssignEntity objAssetEntity)
        {
            try
            {
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[14];
                parameter[0] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
                parameter[1] = DbHelper.CreateParameter("@ItemID", DbType.Int32, objAssetEntity.ItemId);
                parameter[2] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
                parameter[3] = DbHelper.CreateParameter("@AdditionalWarrantyMonth", DbType.Int32, objAssetEntity.AdditionalWarrantyMonth);
                parameter[4] = DbHelper.CreateParameter("@AssignDate", DbType.String, objAssetEntity.AssignDate);
                parameter[5] = DbHelper.CreateParameter("@IsEmployee", DbType.Boolean, objAssetEntity.IsEmployee);
                parameter[6] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
                parameter[7] = DbHelper.CreateParameter("@EmployeeName", DbType.String, objAssetEntity.EmployeeName);
                parameter[8] = DbHelper.CreateParameter("@IsFacility", DbType.Boolean, objAssetEntity.IsFacility);
                parameter[9] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
                parameter[10] = DbHelper.CreateParameter("@FacilityName", DbType.String, objAssetEntity.FacilityName);
                parameter[11] = DbHelper.CreateParameter("@POId", DbType.Int32, objAssetEntity.POId);
                parameter[12] = DbHelper.CreateParameter("@ReAssign", DbType.Boolean, objAssetEntity.ReAssign);
                parameter[13] = DbHelper.CreateParameter("@UserId", DbType.Int32, objAssetEntity.UserId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetReAssignOrder", parameter);

                return int.Parse(result.ToString());

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssetAssignEntity> GetAssetAssignList(AssetAssignEntity objAssetAssignEntity)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetAssignEntity.PONumber);
                parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetAssignEntity.ItemId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetAssignList", parameter);
                List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<AssetAssignEntity> GetAllAssetAssignInformation(string UserName, int rowStart, int rowEnd, string sortColumn, string sortDir, AssetAssignEntity objAssetEntity, string AssetFromDate, string AssetToDate)
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();

            DbParameter[] parameter = new DbParameter[15];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[1] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[2] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[3] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[4] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[5] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
            parameter[6] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetEntity.ItemId);
            parameter[7] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
            parameter[8] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
            parameter[9] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
            parameter[10] = DbHelper.CreateParameter("@AssetFromDate", DbType.String, AssetFromDate);
            parameter[11] = DbHelper.CreateParameter("@AssetToDate", DbType.String, AssetToDate);
            parameter[12] = DbHelper.CreateParameter("@DIV", DbType.String, objAssetEntity.DIV);
            parameter[13] = DbHelper.CreateParameter("@REG", DbType.String, objAssetEntity.REG);
            parameter[14] = DbHelper.CreateParameter("@DIST", DbType.String, objAssetEntity.DIST);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetAssignList", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }

            return listAssetEntity;
        }

        public AssetAssignEntity GetAssetItemDetailByItemId(int ItemId)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ItemId", DbType.Int32, ItemId);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetItemDetailByItemId", parameter);
            List<AssetAssignEntity> listAssetAssignEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetAssignEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetAssignEntity.FirstOrDefault(); ;
        }

        public int DeleteAssetItem(int Id)
        {
            try
            {
                int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@Id", DbType.Int32, Id);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "DeleteAssetItem", parameter);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public AssetAssignEntity getItemDetailByTagNo(string TagNo)
        {

            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@TagNo", DbType.String, TagNo);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getItemDetailByTagNo", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetEntity.FirstOrDefault();
        
        }

        public int SaveAssetReturnOrder(AssetAssignEntity objAssetEntity)
        {
            try
            {
                //int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[9];
                parameter[0] = DbHelper.CreateParameter("@ID", DbType.Int32, objAssetEntity.ID);
                parameter[1] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
                parameter[2] = DbHelper.CreateParameter("@ReturnDate", DbType.String, objAssetEntity.ReturnDate);
                parameter[3] = DbHelper.CreateParameter("@ReturnReason", DbType.String, objAssetEntity.ReturnReason);
                parameter[4] = DbHelper.CreateParameter("@HelpDeskNo", DbType.String, objAssetEntity.HelpDeskNo);
                parameter[5] = DbHelper.CreateParameter("@ItemStatus", DbType.String, objAssetEntity.ItemStatus);
                parameter[6] = DbHelper.CreateParameter("@ReceivedAdditionalNote", DbType.String, objAssetEntity.ReceivedAdditionalNote);
                parameter[7] = DbHelper.CreateParameter("@ResolvedAdditionalNote", DbType.String, objAssetEntity.ResolvedAdditionalNote);
                parameter[8] = DbHelper.CreateParameter("@UserId", DbType.Int32, objAssetEntity.UserId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
               var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetReturnOrder", parameter);

                //return result;
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public int SaveAssetReturnOrderForCurrentSystem(AssetAssignEntity objAssetEntity)
        {
            try
            {
                //int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[16];
                parameter[0] = DbHelper.CreateParameter("@ID", DbType.Int32, objAssetEntity.ID);
                parameter[1] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
                parameter[2] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetEntity.ItemId);
                parameter[3] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
                parameter[4] = DbHelper.CreateParameter("@VendorSKU", DbType.String, objAssetEntity.VendorSKU);
                parameter[5] = DbHelper.CreateParameter("@ReturnDate", DbType.String, objAssetEntity.ReturnDate);
                parameter[6] = DbHelper.CreateParameter("@ReturnReason", DbType.String, objAssetEntity.ReturnReason);
                parameter[7] = DbHelper.CreateParameter("@HelpDeskNo", DbType.String, objAssetEntity.HelpDeskNo);
                parameter[8] = DbHelper.CreateParameter("@ItemStatus", DbType.String, objAssetEntity.ItemStatus);
                parameter[9] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
                parameter[10] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
                parameter[11] = DbHelper.CreateParameter("@PODate", DbType.String, objAssetEntity.PODate);
                parameter[12] = DbHelper.CreateParameter("@InvoiceNumber", DbType.String, objAssetEntity.InvoiceNumber);
                parameter[13] = DbHelper.CreateParameter("@EmployeeName", DbType.String, objAssetEntity.EmployeeName);
                parameter[14] = DbHelper.CreateParameter("@FacilityName", DbType.String, objAssetEntity.FacilityName);
                parameter[15] = DbHelper.CreateParameter("@UserId", DbType.Int32, objAssetEntity.UserId);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetReturnOrderForCurrentSystem", parameter);

                //return result;
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<IdNameEntity> getItemStatusList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getItemStatusList");
                List<IdNameEntity> listIdNameEntity = new List<IdNameEntity>();
                if (dt.Rows.Count > 0)
                {
                    listIdNameEntity = dt.DataTableToList<IdNameEntity>();
                }

                return listIdNameEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public AssetAssignEntity getReturnItemDetailByTagNo(string TagNo)
        {

            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@TagNo", DbType.String, TagNo);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getReturnItemDetailByTagNo", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetEntity.FirstOrDefault();
        
        }

        public List<AssetAssignEntity> GetItemHistoryByTagNo(string TagNo)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@TagNo", DbType.String, TagNo);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetItemHistoryByTagNo", parameter);
            List<AssetAssignEntity> listAssetAssignEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetAssignEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetAssignEntity;
        }

        public List<AssetAssignEntity> GetReturnItemByTagNo(string TagNo)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@TagNo", DbType.String, TagNo);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetReturnItemByTagNo", parameter);
            List<AssetAssignEntity> listAssetAssignEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetAssignEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetAssignEntity;
        }

        public List<AssetAssignEntity> GetAssetReturnList(AssetAssignEntity objAssetAssignEntity)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetAssignEntity.PONumber);
                parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetAssignEntity.ItemId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetReturnList",parameter);
                List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<AssetAssignEntity> GetAssetStockReturnList(AssetAssignEntity objAssetAssignEntity)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetAssignEntity.PONumber);
                parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetAssignEntity.ItemId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetStockReturnList", parameter);
                List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<AssetAssignEntity> GetAllAssetStockReturnInformation(string UserName, int rowStart, int rowEnd, string sortColumn, string sortDir, AssetAssignEntity objAssetEntity, string ReturnFromDate, string ReturnToDate)
        {
            DbParameter[] parameter = new DbParameter[15];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[1] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[2] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[3] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[4] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[5] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
            parameter[6] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetEntity.ItemId);
            parameter[7] = DbHelper.CreateParameter("@ReturnFromDate", DbType.String, ReturnFromDate);
            parameter[8] = DbHelper.CreateParameter("@ReturnToDate", DbType.String, ReturnToDate);
            parameter[9] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
            parameter[10] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
            parameter[11] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
            parameter[12] = DbHelper.CreateParameter("@DIV", DbType.String, objAssetEntity.DIV);
            parameter[13] = DbHelper.CreateParameter("@REG", DbType.String, objAssetEntity.REG);
            parameter[14] = DbHelper.CreateParameter("@DIST", DbType.String, objAssetEntity.DIST);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetStockReturnList", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }

            return listAssetEntity;
        }

        public AssetAssignEntity GetAssetReturnDetailById(int ID)
        {
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ID", DbType.Int32, ID);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetReturnDetailById", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetEntity.FirstOrDefault();
        }

        public List<AssetAssignEntity> GetAllAssetReturnInformation(string UserName, int rowStart, int rowEnd, string sortColumn, string sortDir, AssetAssignEntity objAssetEntity, string ReturnFromDate, string ReturnToDate)
        {
            DbParameter[] parameter = new DbParameter[15];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[1] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[2] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[3] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[4] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[5] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
            parameter[6] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetEntity.ItemId);
            parameter[7] = DbHelper.CreateParameter("@ReturnFromDate", DbType.String, ReturnFromDate);
            parameter[8] = DbHelper.CreateParameter("@ReturnToDate", DbType.String, ReturnToDate);
            parameter[9] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
            parameter[10] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
            parameter[11] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
            parameter[12] = DbHelper.CreateParameter("@DIV", DbType.String, objAssetEntity.DIV);
            parameter[13] = DbHelper.CreateParameter("@REG", DbType.String, objAssetEntity.REG);
            parameter[14] = DbHelper.CreateParameter("@DIST", DbType.String, objAssetEntity.DIST);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetReturnList", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }

            return listAssetEntity;
        }

        public List<AssetAssignEntity> GetAssetRetireList(AssetAssignEntity objAssetAssignEntity)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetAssignEntity.PONumber);
                parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetAssignEntity.ItemId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetRetireList", parameter);
                List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public AssetAssignEntity getItemDetailByTagNoForRetire(string TagNo)
        {

            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@TagNo", DbType.String, TagNo);
            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "getItemDetailByTagNoForRetire", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }
            return listAssetEntity.FirstOrDefault();

        }

        public int SaveAssetRetireOrder(AssetAssignEntity objAssetEntity)
        {
            try
            {
                //int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[10];
                parameter[0] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
                parameter[1] = DbHelper.CreateParameter("@PurchaseDate", DbType.String, objAssetEntity.PODate);
                parameter[2] = DbHelper.CreateParameter("@ItemID", DbType.Int32, objAssetEntity.ItemId);
                parameter[3] = DbHelper.CreateParameter("@AdditionalWarranty", DbType.Int32, objAssetEntity.AdditionalWarrantyMonth);
                parameter[4] = DbHelper.CreateParameter("@RetireDate", DbType.String, objAssetEntity.RetireDate);
                parameter[5] = DbHelper.CreateParameter("@RetireReason", DbType.String, objAssetEntity.RetireReason);
                parameter[6] = DbHelper.CreateParameter("@IsReturned", DbType.Boolean, objAssetEntity.IsReturned);
                parameter[7] = DbHelper.CreateParameter("@AssignMasterId", DbType.Int32, objAssetEntity.AssignMasterId);
                parameter[8] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
                parameter[9] = DbHelper.CreateParameter("@POID", DbType.Int32, objAssetEntity.POId);
                
                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveAssetRetireOrder", parameter);

                //return result;
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssetAssignEntity> GetAllAssetRetireInformation(string UserName, int rowStart, int rowEnd, string sortColumn, string sortDir, AssetAssignEntity objAssetEntity, string RetireFromDate, string RetireToDate)
        {
            DbParameter[] parameter = new DbParameter[15];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[1] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[2] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[3] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[4] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[5] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
            parameter[6] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetEntity.ItemId);
            parameter[7] = DbHelper.CreateParameter("@RetireFromDate", DbType.String, RetireFromDate);
            parameter[8] = DbHelper.CreateParameter("@RetireToDate", DbType.String, RetireToDate);
            parameter[9] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
            parameter[10] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
            parameter[11] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
            parameter[12] = DbHelper.CreateParameter("@DIV", DbType.String, objAssetEntity.DIV);
            parameter[13] = DbHelper.CreateParameter("@REG", DbType.String, objAssetEntity.REG);
            parameter[14] = DbHelper.CreateParameter("@DIST", DbType.String, objAssetEntity.DIST);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetRetireList", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }

            return listAssetEntity;
        }

        public int AddAssetTagDetail(string TagNo)
        {
            try
            {
                //int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[1];
                parameter[0] = DbHelper.CreateParameter("@TagNo", DbType.String, TagNo);

                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "AddAssetTagDetail", parameter);

                //return result;
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<AssetEntity> GetAssetStockList()
        {

            try
            {

                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetStockDetails");
                List<AssetEntity> listAssetEntity = new List<AssetEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<AssetEntity> GetAllAssetStockInformation(AssetEntity objAssetEntity, string StockFromDate, string StockToDate, string ItemId)
        {
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
            DbParameter[] parameter = new DbParameter[4];
            parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
            parameter[1] = DbHelper.CreateParameter("@ItemID", DbType.String, ItemId);
            parameter[2] = DbHelper.CreateParameter("@PurchaseStartDate", DbType.String, StockFromDate);
            parameter[3] = DbHelper.CreateParameter("@PurchaseEndDate", DbType.String, StockToDate);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetAssetStockDetails", parameter);
            List<AssetEntity> listAssetEntity = new List<AssetEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetEntity>();
            }

            return listAssetEntity;
        }

        public List<AssetAssignEntity> GetAssetTermedList(AssetAssignEntity objAssetAssignEntity)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetAssignEntity.PONumber);
                parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetAssignEntity.ItemId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetTermAssetList", parameter);
                List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<AssetAssignEntity> GetAllAssetTermedInformation(string UserName, int rowStart, int rowEnd, string sortColumn, string sortDir, AssetAssignEntity objAssetEntity, string TermFromDate, string TermToDate)
        {
            DbParameter[] parameter = new DbParameter[15];
            parameter[0] = DbHelper.CreateParameter("@UserName", DbType.String, UserName);
            parameter[1] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[2] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[3] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[4] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[5] = DbHelper.CreateParameter("@AssetTagNumber", DbType.String, objAssetEntity.AssetTagNumber);
            parameter[6] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetEntity.ItemId);
            parameter[7] = DbHelper.CreateParameter("@TermFromDate", DbType.String, TermFromDate);
            parameter[8] = DbHelper.CreateParameter("@TermToDate", DbType.String, TermToDate);
            parameter[9] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);
            parameter[10] = DbHelper.CreateParameter("@EmployeeId", DbType.String, objAssetEntity.EmployeeId);
            parameter[11] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objAssetEntity.FacilityCode);
            parameter[12] = DbHelper.CreateParameter("@DIV", DbType.String, objAssetEntity.DIV);
            parameter[13] = DbHelper.CreateParameter("@REG", DbType.String, objAssetEntity.REG);
            parameter[14] = DbHelper.CreateParameter("@DIST", DbType.String, objAssetEntity.DIST);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetTermAssetList", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }

            return listAssetEntity;
        }

        public List<AssetAssignEntity> AssetStockAvailableListItemWise(AssetAssignEntity objAssetAssignEntity)
        {

            try
            {
                DbParameter[] parameter = new DbParameter[2];
                parameter[0] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetAssignEntity.PONumber);
                parameter[1] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetAssignEntity.ItemId);
                DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "AssetStockAvailableListItemWise", parameter);
                List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
                if (dt.Rows.Count > 0)
                {
                    listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
                }

                return listAssetEntity;
            }
            catch (Exception ex)
            {
                ExceptionHandling.ExceptionHandling.ManageErrorLog(ex);
                throw new Exception("DAL Exception " + ex);
            }

        }

        public List<AssetAssignEntity> GetAllAssetAvailableInformation(int rowStart, int rowEnd, string sortColumn, string sortDir, AssetAssignEntity objAssetEntity)
        {
            DbParameter[] parameter = new DbParameter[6];
            parameter[0] = DbHelper.CreateParameter("@RowStart", DbType.Int32, rowStart);
            parameter[1] = DbHelper.CreateParameter("@RowEnd", DbType.Int32, rowEnd);
            parameter[2] = DbHelper.CreateParameter("@Sort", DbType.String, sortColumn);
            parameter[3] = DbHelper.CreateParameter("@SortDirection", DbType.String, sortDir);
            parameter[4] = DbHelper.CreateParameter("@ItemId", DbType.Int32, objAssetEntity.ItemId);
            parameter[5] = DbHelper.CreateParameter("@PONumber", DbType.String, objAssetEntity.PONumber);

            DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "AssetStockAvailableListItemWise", parameter);
            List<AssetAssignEntity> listAssetEntity = new List<AssetAssignEntity>();
            if (dt.Rows.Count > 0)
            {
                listAssetEntity = dt.DataTableToList<AssetAssignEntity>();
            }

            return listAssetEntity;
        }

        public int SaveImportedAssetAssign(ImportAssetAssignEntity objImportAssetAssignEntity, int UserID)
        {
            try
            {
                //int result = 0;
                DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ToString();
                //DbHelper.ConnectionString = GeneralSettings.GetConnectionString("HSGReporting").ToString();

                DbParameter[] parameter = new DbParameter[9];
                parameter[0] = DbHelper.CreateParameter("@AssetTagNumber", DbType.Int32, objImportAssetAssignEntity.AssetTagNumber);
                parameter[1] = DbHelper.CreateParameter("@ItemName", DbType.String, objImportAssetAssignEntity.ItemName);
                parameter[2] = DbHelper.CreateParameter("@PONumber", DbType.String, objImportAssetAssignEntity.PONumber);
                parameter[3] = DbHelper.CreateParameter("@AssignDate", DbType.String, objImportAssetAssignEntity.AssignDate);
                parameter[4] = DbHelper.CreateParameter("@IsEmployee", DbType.Boolean, objImportAssetAssignEntity.IsEmployee);
                parameter[5] = DbHelper.CreateParameter("@EmployeeID", DbType.String, objImportAssetAssignEntity.EmployeeID);
                parameter[6] = DbHelper.CreateParameter("@IsFacility", DbType.Boolean, objImportAssetAssignEntity.IsFacility);
                parameter[7] = DbHelper.CreateParameter("@FacilityCode", DbType.String, objImportAssetAssignEntity.FacilityCode);
                parameter[8] = DbHelper.CreateParameter("@UserID", DbType.Int32, UserID);
                
                //DataTable dt = DbHelper.ExecuteTable(CommandType.StoredProcedure, "GetUserDetailsByID", parameter);
                var result = DbHelper.ExecuteScalar(CommandType.StoredProcedure, "SaveImportAssetAssign", parameter);

                //return result;
                return int.Parse(result.ToString());
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}