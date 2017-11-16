using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
    public class EmployeeDirectoryController : BaseController
    {
        [Authorization(ScreenAlias.EmployeeDirectory, Rights.View, true)]
        public ActionResult EmployeeDirectoryList()
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.EmployeeDirectory));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }
            EmployeeDirectoryEntity objEmployeeDirectoryEntity = new EmployeeDirectoryEntity();
            List<IdNameEntity> lstIdNameEntity = new List<IdNameEntity>();
            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();
            objEmployeeDirectoryEntity.FutureDivRegDistEntityList = objEmployeeDirectoryCls.getFutureDivRegDistList();
            List<SelectListItem> idDivListTr = new List<SelectListItem>();
            List<SelectListItem> idRegListTr = new List<SelectListItem>();
            List<SelectListItem> idDistListTr = new List<SelectListItem>();


            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);

            List<FieldPermissionEntity> listFieldPermissionsEntity = CacheHelper.Get<List<FieldPermissionEntity>>(CacheKeys.FieldPermissions);
            List<SelectListItem> idJobTitleList = new List<SelectListItem>();
            List<JobTitleEntity> EmpJobTitleEntitylst = new List<JobTitleEntity>();
            List<IdNameEntity> ArcustStatelst = new List<IdNameEntity>();
            List<SelectListItem> idArcustStatelst = new List<SelectListItem>();

            objEmployeeDirectoryEntity.lstFieldPermissionEntity = listFieldPermissionsEntity;
            EmpJobTitleEntitylst = objEmployeeDirectoryCls.GetEmployeeJobTitleList("");
            foreach (JobTitleEntity objJobTitleEntity in EmpJobTitleEntitylst)
            {
                idJobTitleList.Add(new SelectListItem() { Text = objJobTitleEntity.Title, Value = objJobTitleEntity.Title });
            }
            //FormPermissionEntity result = new FormPermissionEntity();
            if (listFieldPermissionsEntity == null)
            {
                CacheHelper.RefreshCashItem(CacheKeys.FieldPermissions);
                listFieldPermissionsEntity = CacheHelper.Get<List<FieldPermissionEntity>>(CacheKeys.FieldPermissions);
            }

            objEmployeeDirectoryEntity.RowStart = paggingArray[0];
            objEmployeeDirectoryEntity.RowEnd = paggingArray[1];

            objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity = objEmployeeDirectoryCls.getEmployeeDirectoryList("", "", "", "", "","", "", "", "", "", "", "", 1, 50, "", "");
            for (int i = 0; i < objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity.Count; i++)
            {
                objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            ArcustStatelst = objEmployeeDirectoryCls.GetArcustStateList("");

            foreach (IdNameEntity objArcustStatelst in ArcustStatelst)
            {
                idArcustStatelst.Add(new SelectListItem() { Text = objArcustStatelst.Name, Value = objArcustStatelst.Name });
            }

            var FutureDivList = objEmployeeDirectoryCls.getFutureDivRegDistList().OrderBy(m => m.DIV).Select(m => m.DIV).Distinct().ToList();
            idDivListTr.Add(new SelectListItem() { Text = "select", Value = Convert.ToString("") });
            foreach (string objDiv in FutureDivList)
            {
                idDivListTr.Add(new SelectListItem() { Text = objDiv, Value = Convert.ToString(objDiv) });

            }

            idRegListTr.Add(new SelectListItem() { Text = "select", Value = Convert.ToString("") });

            var FutureRegList = objEmployeeDirectoryCls.getFutureDivRegDistList().OrderBy(m => m.REG).Select(m => m.REG).Distinct().ToList();

            foreach (string objReg in FutureRegList)
            {
                idRegListTr.Add(new SelectListItem() { Text = objReg, Value = Convert.ToString(objReg) });

            }

            idDistListTr.Add(new SelectListItem() { Text = "select", Value = Convert.ToString("") });

            var FutureDistList = objEmployeeDirectoryCls.getFutureDivRegDistList().OrderBy(m => m.DIST).Select(m => m.DIST).Distinct().ToList();


            foreach (string objDist in FutureDistList)
            {
                idDistListTr.Add(new SelectListItem() { Text = objDist, Value = Convert.ToString(objDist) });
            }

            ViewBag.DivListTr = idDivListTr;
            ViewBag.RegListTr = idRegListTr;
            ViewBag.DistListTr = idDistListTr;

            ViewBag.JobTitleList = idJobTitleList;
            ViewBag.ArcustStateList = idArcustStatelst;
            return View(objEmployeeDirectoryEntity);
        }

        [Authorization(ScreenAlias.EmployeeDirectory, Rights.View, true)]
        public ActionResult EmployeeDirectoryListByDeptId(string DepId)
        {

            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.EmployeeDirectory));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            EmployeeDirectoryEntity objEmployeeDirectoryEntity = new EmployeeDirectoryEntity();
            List<IdNameEntity> lstIdNameEntity = new List<IdNameEntity>();
            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();
            objEmployeeDirectoryEntity.FutureDivRegDistEntityList = objEmployeeDirectoryCls.getFutureDivRegDistList();
            List<SelectListItem> idDivListTr = new List<SelectListItem>();
            List<SelectListItem> idRegListTr = new List<SelectListItem>();
            List<SelectListItem> idDistListTr = new List<SelectListItem>();

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);

            List<FieldPermissionEntity> listFieldPermissionsEntity = CacheHelper.Get<List<FieldPermissionEntity>>(CacheKeys.FieldPermissions);
            List<SelectListItem> idJobTitleList = new List<SelectListItem>();
            List<JobTitleEntity> EmpJobTitleEntitylst = new List<JobTitleEntity>();
            List<IdNameEntity> ArcustStatelst = new List<IdNameEntity>();
            List<SelectListItem> idArcustStatelst = new List<SelectListItem>();

            objEmployeeDirectoryEntity.lstFieldPermissionEntity = listFieldPermissionsEntity;
            EmpJobTitleEntitylst = objEmployeeDirectoryCls.GetEmployeeJobTitleList("");
            foreach (JobTitleEntity objJobTitleEntity in EmpJobTitleEntitylst)
            {
                idJobTitleList.Add(new SelectListItem() { Text = objJobTitleEntity.Title, Value = objJobTitleEntity.Title });
            }
            //FormPermissionEntity result = new FormPermissionEntity();
            if (listFieldPermissionsEntity == null)
            {
                CacheHelper.RefreshCashItem(CacheKeys.FieldPermissions);
            }

            objEmployeeDirectoryEntity.RowStart = paggingArray[0];
            objEmployeeDirectoryEntity.RowEnd = paggingArray[1];

            objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity = objEmployeeDirectoryCls.getEmployeeDirectoryList("", "", "", DepId,"", "", "", "", "", "", "", "", 1, 50, "", "");
            for (int i = 0; i < objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity.Count; i++)
            {
                objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            ArcustStatelst = objEmployeeDirectoryCls.GetArcustStateList("");

            foreach (IdNameEntity objArcustStatelst in ArcustStatelst)
            {
                idArcustStatelst.Add(new SelectListItem() { Text = objArcustStatelst.Name, Value = objArcustStatelst.Name });
            }

            var FutureDivList = objEmployeeDirectoryCls.getFutureDivRegDistList().OrderBy(m => m.DIV).Select(m => m.DIV).Distinct().ToList();
            idDivListTr.Add(new SelectListItem() { Text = "select", Value = Convert.ToString("") });
            foreach (string objDiv in FutureDivList)
            {
                idDivListTr.Add(new SelectListItem() { Text = objDiv, Value = Convert.ToString(objDiv) });

            }

            idRegListTr.Add(new SelectListItem() { Text = "select", Value = Convert.ToString("") });

            var FutureRegList = objEmployeeDirectoryCls.getFutureDivRegDistList().OrderBy(m => m.REG).Select(m => m.REG).Distinct().ToList();

            foreach (string objReg in FutureRegList)
            {
                idRegListTr.Add(new SelectListItem() { Text = objReg, Value = Convert.ToString(objReg) });

            }

            idDistListTr.Add(new SelectListItem() { Text = "select", Value = Convert.ToString("") });

            var FutureDistList = objEmployeeDirectoryCls.getFutureDivRegDistList().OrderBy(m => m.DIST).Select(m => m.DIST).Distinct().ToList();


            foreach (string objDist in FutureDistList)
            {
                idDistListTr.Add(new SelectListItem() { Text = objDist, Value = Convert.ToString(objDist) });
            }

            ViewBag.DivListTr = idDivListTr;
            ViewBag.RegListTr = idRegListTr;
            ViewBag.DistListTr = idDistListTr;

            ViewBag.JobTitleList = idJobTitleList;
            ViewBag.ArcustStateList = idArcustStatelst;
            objEmployeeDirectoryEntity.DEPT = DepId;

            //if (objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity.Count() == 0) { 
            //objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity.Add(new EmployeeDirectoryEntity());


            //}

            return View("EmployeeDirectoryList", objEmployeeDirectoryEntity);
        }

        public ActionResult EmpDirectoryListPagingSortingList(string EEID, string firstName, string lastName, string Social, string DEPT, string FacilityCode, string JobTitle, string State, string DIV, string REG, string DIST, string Status, string pageindex, string pagesize, string columnName, string sortorder)
        {
            EmployeeDirectoryEntity objEmployeeDirectoryEntity = new EmployeeDirectoryEntity();
            EmployeeDirectoryCls employeeDirectoryCls = new EmployeeDirectoryCls();

            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();

            if (objEmployeeDirectoryEntity.Pageindex == 0)
            {
                objEmployeeDirectoryEntity.Pageindex = 1;
            }
            if (pageindex == "") {
                pageindex = "1";
            }
            objEmployeeDirectoryEntity.Pageindex = Convert.ToInt32(pageindex);
            objEmployeeDirectoryEntity.pagesize = Convert.ToInt32(pagesize);

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(objEmployeeDirectoryEntity.Pageindex, objEmployeeDirectoryEntity.pagesize);
            objEmployeeDirectoryEntity.RowStart = paggingArray[0];
            objEmployeeDirectoryEntity.RowEnd = paggingArray[1];
            List<EmployeeDirectoryEntity> employeeDetailsList = employeeDirectoryCls.getEmployeeDirectoryList(EEID, firstName, lastName, DEPT, Social, FacilityCode, JobTitle, State, DIV, REG, DIST, Status, paggingArray[0], paggingArray[1], columnName, sortorder);
            for (int i = 0; i < employeeDetailsList.Count; i++)
            {
                employeeDetailsList[i].Pageindex = Convert.ToInt32(pageindex);
            }

            //lst[0].lstFieldPermissionEntity = employeeDetailsList;
            List<FieldPermissionEntity> listFieldPermissionsEntity = CacheHelper.Get<List<FieldPermissionEntity>>(CacheKeys.FieldPermissions);
            if (employeeDetailsList.Count > 0)
            {
                employeeDetailsList[0].lstFieldPermissionEntity = listFieldPermissionsEntity;
            }
            else
            {
                employeeDetailsList.Add(new EmployeeDirectoryEntity());
                employeeDetailsList[0].lstFieldPermissionEntity = listFieldPermissionsEntity;
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_EmployeeDirectoryList", employeeDetailsList);
        }

        public ActionResult ViewEmployeeDirectoryDetail(string ID)
        {
            FieldMasterBusiness objFieldMasterBusiness = new FieldMasterBusiness();
            try
            {
                AllEmployeeDirectoryEntity objEmployeeDirectoryEntity = new AllEmployeeDirectoryEntity();
                EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();
                objEmployeeDirectoryEntity = objEmployeeDirectoryCls.getEmployeeDirectoryDetail(ID);
                List<FieldPermissionEntity> listFieldPermissionsEntity = CacheHelper.Get<List<FieldPermissionEntity>>(CacheKeys.FieldPermissions);
                objEmployeeDirectoryEntity.lstFieldPermissionEntity = listFieldPermissionsEntity;
                objEmployeeDirectoryEntity.lstEmployeeDirectoryEntity = objEmployeeDirectoryCls.getEmployeeListByDepartment(objEmployeeDirectoryEntity.DEPT);
                return View(objEmployeeDirectoryEntity);
            }
            catch (Exception ex)
            {
                objFieldMasterBusiness.StoreError(ex.Message.ToString());
                throw new Exception("FieldMasterController Exception :" + ex);
            }

        }

        public JsonResult getEmployeeDirectoryDetailByEEID(string EEID)
        {
            AllEmployeeDirectoryEntity objEmployeeDirectoryEntity = new AllEmployeeDirectoryEntity();
            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();
            objEmployeeDirectoryEntity = objEmployeeDirectoryCls.getEmployeeDirectoryDetail(EEID);
            return Json(objEmployeeDirectoryEntity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getEmployeeDirectoryDetailByEEIDForAsset(string EEID, int ItemId)
        {
            AllEmployeeDirectoryEntity objEmployeeDirectoryEntity = new AllEmployeeDirectoryEntity();
            EmployeeEntity objEmployeeEntity = new EmployeeEntity();
            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();
            objEmployeeDirectoryEntity = objEmployeeDirectoryCls.getEmployeeDirectoryDetailByEEIDForAsset(EEID, ItemId);

            objEmployeeEntity.EMPLOYEENAME = objEmployeeDirectoryEntity.EMPLOYEENAME;
            objEmployeeEntity.ADDRESSONE = objEmployeeDirectoryEntity.ADDRESSONE;
            objEmployeeEntity.ST = objEmployeeDirectoryEntity.ST;
            objEmployeeEntity.PrimaryPhone = objEmployeeDirectoryEntity.PrimaryPhone;
            objEmployeeEntity.EEID = objEmployeeDirectoryEntity.EEID;


            return Json(objEmployeeEntity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getFutureRegionList(string Div)
        {
            List<IdNameEntity> lstIdNameEntity = new List<IdNameEntity>();
            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();

            lstIdNameEntity = objEmployeeDirectoryCls.getFutureRegionList(Div);


            return Json(lstIdNameEntity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult getFutureDistictList(string Div, string Reg)
        {
            List<IdNameEntity> lstIdNameEntity = new List<IdNameEntity>();
            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();
            lstIdNameEntity = objEmployeeDirectoryCls.getFutureDistictList(Div, Reg);
            return Json(lstIdNameEntity, JsonRequestBehavior.AllowGet);
        }

        public void ExporttoExcelEmpDirectoryist(string EEID, string firstName, string lastName, string Social, string DEPT, string FacilityCode, string JobTitle, string State, string DIV, string REG, string DIST, string Status)
        {
            EmployeeDirectoryCls employeeDirectoryCls = new EmployeeDirectoryCls();
            EmployeeDirectoryFilterParameter employeeDirectoryFilterParameter = new EmployeeDirectoryFilterParameter();
            employeeDirectoryFilterParameter.EEID = EEID;
            employeeDirectoryFilterParameter.FirstName = firstName;
            employeeDirectoryFilterParameter.LastName = lastName;
            employeeDirectoryFilterParameter.Social = Social;
            employeeDirectoryFilterParameter.Department = DEPT;
            employeeDirectoryFilterParameter.FacilityCode = FacilityCode;
            employeeDirectoryFilterParameter.JobTitle = JobTitle;
            employeeDirectoryFilterParameter.State = State;
            employeeDirectoryFilterParameter.DIV = DIV;
            employeeDirectoryFilterParameter.REG = REG;
            employeeDirectoryFilterParameter.DIST = DIST;
            employeeDirectoryFilterParameter.Status = Status;
            employeeDirectoryFilterParameter.UserName = SessionManager.UserName;
            employeeDirectoryFilterParameter.ExportDatetime = DateTime.Now;
            int result = employeeDirectoryCls.SaveExportFilterAuditReport(employeeDirectoryFilterParameter);
            
            DataTable employeeDetailsList = employeeDirectoryCls.getEmployeeDirectoryDataTable(EEID, firstName, lastName, DEPT, Social, FacilityCode, JobTitle, State, DIV, REG, DIST, Status, 0, 0, "", "");
            List<FieldPermissionEntity> listFieldPermissionsEntity = CacheHelper.Get<List<FieldPermissionEntity>>(CacheKeys.FieldPermissions);
            if (listFieldPermissionsEntity == null)
            {
                CacheHelper.RefreshCashItem(CacheKeys.FieldPermissions);
                listFieldPermissionsEntity = CacheHelper.Get<List<FieldPermissionEntity>>(CacheKeys.FieldPermissions);
            }
            int columnindex = 0;
            List<DataColumn> dclist = new List<DataColumn>();
            for (int i = 0; i < employeeDetailsList.Columns.Count; i++)
            {
                if (listFieldPermissionsEntity.Where(j => j.FieldName == employeeDetailsList.Columns[i].ColumnName).Count() > 0)
                {
                    if (listFieldPermissionsEntity.Where(j => j.FieldName == employeeDetailsList.Columns[i].ColumnName).FirstOrDefault().IsDisplay == false)
                    {
                        dclist.Add(employeeDetailsList.Columns[columnindex]);
                        //employeeDetailsList.Columns.RemoveAt(columnindex);
                    }
                }
                columnindex = columnindex + 1;
            }
            foreach (DataColumn dc in dclist)
            {
                employeeDetailsList.Columns.Remove(dc);
            }
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("EmployeeDirectory");
                ws.Cells["A1"].LoadFromDataTable(employeeDetailsList, true);
                int columnCount = employeeDetailsList.Columns.Count;
                string cName = GetExcelColumnName(columnCount);
                //Format the header for column 1-3
                using (ExcelRange rng = ws.Cells["A1:" + cName + "1"])
                {
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 245, 245));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.Black);
                }

                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment;  filename=EmployeeDirectory_List.xlsx");
                Response.BinaryWrite(pck.GetAsByteArray());
            }
        }

        private string GetExcelColumnName(int columnNumber)
        {
            int dividend = columnNumber;
            string columnName = String.Empty;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                columnName = Convert.ToChar(65 + modulo).ToString() + columnName;
                dividend = (int)((dividend - modulo) / 26);
            }

            return columnName;
        }

        public ActionResult GetMobileMenu() {

            System.Web.Mvc.MvcHtmlString cc = HelperCls.CreateDynamicMenuItem(null, (string)Convert.ToString("MobileUsers"));

            return Content(cc.ToString());
        
        }

        public ActionResult GetCoupaMenu()
        {
            System.Web.Mvc.MvcHtmlString cc = HelperCls.CreateDynamicMenuItem(null, (string)Convert.ToString("CoupaAdmin"));
            return Content(cc.ToString());
        }

        public ActionResult GetDSSIMenu()
        {
            System.Web.Mvc.MvcHtmlString cc = HelperCls.CreateDynamicMenuItem(null, (string)Convert.ToString("DSSIUserAdmin"));
            return Content(cc.ToString());
        }


    }

}
