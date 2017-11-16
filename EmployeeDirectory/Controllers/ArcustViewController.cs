using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace EmployeeDirectory.Controllers
{
    public class ArcustViewController : BaseController
    {
        //
        // GET: /ArcustView/

        public ActionResult ArcustViewList()
        {
            //List<ArcustViewEntity> arcustViewEntity = new List<ArcustViewEntity>();
            //return View(arcustViewEntity);
            ArcustViewEntity objArcustViewEntity = new ArcustViewEntity();
            EmployeeDirectoryCls objEmployeeDirectoryCls = new EmployeeDirectoryCls();

            List<SelectListItem> idDivListTr = new List<SelectListItem>();
            List<SelectListItem> idRegListTr = new List<SelectListItem>();
            List<SelectListItem> idDistListTr = new List<SelectListItem>();

            ArcustViewBusiness arcustView = new ArcustViewBusiness();
            List<IdNameEntity> ArcustStatelst = new List<IdNameEntity>();
            List<SelectListItem> idArcustStatelst = new List<SelectListItem>();
            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            ArcustViewSearchEntity arcustViewSearchEntity = new ArcustViewSearchEntity();
            arcustViewSearchEntity.RowStart = paggingArray[0];
            arcustViewSearchEntity.RowEnd = paggingArray[1];
            List<ArcustViewEntity> list = arcustView.GetArcustViewList(arcustViewSearchEntity);

            ArcustStatelst = objEmployeeDirectoryCls.GetArcustStateList("");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }
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

            ViewBag.ArcustStateList = idArcustStatelst;
            ViewBag.DivListTr = idDivListTr;
            ViewBag.RegListTr = idRegListTr;
            ViewBag.DistListTr = idDistListTr;

            return View(list);
        }

        public ActionResult ArcustViewListPagingSortingList(ArcustViewSearchEntity arcustViewSearchEntity)
        {
            ArcustViewBusiness arcustView = new ArcustViewBusiness();
            if (arcustViewSearchEntity.Pageindex == 0)
            {
                arcustViewSearchEntity.Pageindex = 1;
            }


            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(arcustViewSearchEntity.Pageindex, arcustViewSearchEntity.PageSize);
            arcustViewSearchEntity.RowStart = paggingArray[0];
            arcustViewSearchEntity.RowEnd = paggingArray[1];
            arcustViewSearchEntity.UserName = Convert.ToString(Session[AppConstant.USERNAME]);
            arcustViewSearchEntity.isFieldUser = Convert.ToBoolean(Session[AppConstant.FIELDUSER]);
            List<ArcustViewEntity> list = arcustView.GetArcustViewList(arcustViewSearchEntity);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = arcustViewSearchEntity.Pageindex;
            }

            ViewBag.PageSize = arcustViewSearchEntity.PageSize;
            return PartialView("_ArcustViewList", list);
        }

        public FileResult ExporttoExcelArcustViewList(string facilityCode, string facilityName, string managementCode, string managementName, string territoryCode, string collectorCode, string payrollCode, string departmentCode, string city, string state, string address, string zipCode, string startDate, string endDate, string termStartDate, string termEndDate, string div, string reg, string dist, string status)
        {
            ArcustViewBusiness arcustView = new ArcustViewBusiness();
            ArcustViewSearchEntity arcustViewSearchEntity = new ArcustViewSearchEntity();
            arcustViewSearchEntity.RowStart = 0;
            arcustViewSearchEntity.RowEnd = 0;
            arcustViewSearchEntity.FacilityCode = facilityCode;
            arcustViewSearchEntity.FacilityName = facilityName;
            arcustViewSearchEntity.ManagementCode = managementCode;
            arcustViewSearchEntity.ManagementName = managementName;
            arcustViewSearchEntity.TerritoryCode = territoryCode;
            arcustViewSearchEntity.CollectorCode = collectorCode;
            arcustViewSearchEntity.PayrollCode = payrollCode;
            arcustViewSearchEntity.DepartmentCode = departmentCode;
            arcustViewSearchEntity.City = city;
            arcustViewSearchEntity.State = state;
            arcustViewSearchEntity.Address = address;
            arcustViewSearchEntity.ZipCode = zipCode;
            arcustViewSearchEntity.Div = div;
            arcustViewSearchEntity.Reg = reg;
            arcustViewSearchEntity.Dist = dist;
            arcustViewSearchEntity.Status = status;
            if (!string.IsNullOrEmpty(startDate))
            {
                arcustViewSearchEntity.StartDate = Convert.ToDateTime(startDate);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                arcustViewSearchEntity.EndDate = Convert.ToDateTime(endDate);
            }
            if (!string.IsNullOrEmpty(termStartDate))
            {
                arcustViewSearchEntity.TermStartDate = Convert.ToDateTime(termStartDate);
            }
            if (!string.IsNullOrEmpty(termEndDate))
            {
                arcustViewSearchEntity.TermEndDate = Convert.ToDateTime(termEndDate);
            }

            List<ArcustViewEntity> list = arcustView.GetArcustViewList(arcustViewSearchEntity);
            DataTable arcustViewDT = HelperCls.ToDataTable(list);


            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("arcustViewList");
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(arcustViewDT, true);
                
                int columnCount = arcustViewDT.Columns.Count;
                string cName = GetExcelColumnName(columnCount);
                //Format the header for column 1-3
                using (ExcelRange rng = ws.Cells["A1:" + cName + "1"])
                {

                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 245, 245));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.Black);
                }
                
                using (ExcelRange rng = ws.Cells["A:XFD"])
                {
                    rng.Style.Numberformat.Format = "@";
                }


               
                //formatRange = xlWorkSheet.get_Range("a1", "b1");
                //formatRange.NumberFormat = "@";


                var fcr = new FileContentResult(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                return fcr;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "0", "alert('File Uploaded Succesfully..!!');", true);

            }

            // tempararoy comment 
            //var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(arcustViewDT.ToCSV());
            //return File(bytes.ToArray(), "text/csv", "arcustViewList.csv");
        }

        public JsonResult GetDetailsByfacilityCode(string facilityCode)
        {
            ArcustViewBusiness arcustView = new ArcustViewBusiness();
            ArcustViewEntity arcustViewEntity = arcustView.GetArcustDetailByFacilityCode(facilityCode);
            return Json(arcustViewEntity, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetDetailsByfacilityCodeForAsset(string facilityCode,int ItemId)
        {
            ArcustViewBusiness arcustView = new ArcustViewBusiness();
            FacilityEntity objFacilityEntity = new FacilityEntity();
            ArcustViewEntity arcustViewEntity = arcustView.GetDetailsByfacilityCodeForAsset(facilityCode, ItemId);

          

            if (arcustViewEntity == null)
            {
                return Json("", JsonRequestBehavior.AllowGet);
            }
            else
            {
                objFacilityEntity.customer_code = arcustViewEntity.customer_code;
                objFacilityEntity.customer_name = arcustViewEntity.customer_name;
                objFacilityEntity.state = arcustViewEntity.state;
                objFacilityEntity.addr1 = arcustViewEntity.addr1;

                return Json(objFacilityEntity, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetArcustViewDetailsByfacilityCode(string facilityCode)
        {
            ArcustViewBusiness objarcustViewBusiness = new ArcustViewBusiness();
            ArcustViewEntity objArcustViewEntity = new ArcustViewEntity();
            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, 20);
            objArcustViewEntity = objarcustViewBusiness.GetArcustDetailByFacilityCode(facilityCode);
            objArcustViewEntity.lstArcustViewSearchEntity = objarcustViewBusiness.getBuildingListByDepartment(objArcustViewEntity.Department);
            objArcustViewEntity.lstEmployeeEntity = objarcustViewBusiness.getEmployeeListByDepartment("","","","","",objArcustViewEntity.Department, paggingArray[0], paggingArray[1], "", "");
            for (int i = 0; i < objArcustViewEntity.lstEmployeeEntity.Count; i++)
            {
                objArcustViewEntity.lstEmployeeEntity[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }
            ViewBag.Department = objArcustViewEntity.Department;
            return PartialView("_ArcustViewDetail", objArcustViewEntity);
        }

        public ActionResult GetArcustEmployeeDetail(string lastname, string firstname, string jobtitle, string eeid, string status, string Dept,string pagesize, string pageindex, string columnName, string sortorder)
        {
        
            ArcustViewEntity objArcustViewEntity = new ArcustViewEntity();
            ArcustViewBusiness objarcustViewBusiness = new ArcustViewBusiness();
            if (string.IsNullOrEmpty(pageindex))
            {
                pageindex = "1";
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(Convert.ToInt32(pageindex), int.Parse(pagesize));
            objArcustViewEntity.lstEmployeeEntity = objarcustViewBusiness.getEmployeeListByDepartment(eeid, firstname, lastname, jobtitle, status, Dept, paggingArray[0], paggingArray[1], columnName, sortorder);
            for (int i = 0; i < objArcustViewEntity.lstEmployeeEntity.Count; i++)
            {
                objArcustViewEntity.lstEmployeeEntity[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.Department = Dept;
            return PartialView("_ArcustEmployeeDetail", objArcustViewEntity.lstEmployeeEntity);
        
        }

        //public void ExportToExcelArcustEmployeeList(string DepId)
        //{
        //    ArcustViewEntity entity = new ArcustViewEntity();

        //    if (DepId == "null") { entity.Department = ""; }
        //    else
        //    {
        //        entity.Department = DepId;
        //    }

        //    ArcustViewBusiness objarcustViewBusiness = new ArcustViewBusiness();
        //    DataTable submittedFormSummaryEntityList = objarcustViewBusiness.getEmployeeDatatableByDepartment(DepId);

        //    //if (submittedFormSummaryEntityList.Rows.Count > 0)
        //    //{
        //    using (ExcelPackage pck = new ExcelPackage())
        //    {
        //        ExcelWorksheet ws = pck.Workbook.Worksheets.Add("StartUp");
        //        //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
        //        ws.Cells["A1"].LoadFromDataTable(submittedFormSummaryEntityList, true);
        //        int columnCount = submittedFormSummaryEntityList.Columns.Count;
        //        string cName = GetExcelColumnName(columnCount);
        //        //Format the header for column 1-3
        //        using (ExcelRange rng = ws.Cells["A1:" + cName + "1"])
        //        {
        //            rng.Style.Font.Bold = true;
        //            rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
        //            rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 245, 245));  //Set color to dark blue
        //            rng.Style.Font.Color.SetColor(Color.Black);

        //        }

        //        //Write it back to the client
        //        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        //        Response.AddHeader("content-disposition", "attachment;  filename=ArcustEmployee_List.xlsx");
        //        Response.BinaryWrite(pck.GetAsByteArray());
                
        //    }
        
        
        //}

        public FileContentResult ExportToExcelArcustEmployeeList(string lastname, string firstname, string jobtitle, string eeid, string status, string DepId)
        {
            ArcustViewEntity entity = new ArcustViewEntity();

            if (DepId == "null") { entity.Department = ""; }
            else
            {
                entity.Department = DepId;
            }

            ArcustViewBusiness objarcustViewBusiness = new ArcustViewBusiness();
            DataTable submittedFormSummaryEntityList = objarcustViewBusiness.getEmployeeDatatableByDepartment(eeid, firstname, lastname, jobtitle, status, DepId);

            //if (submittedFormSummaryEntityList.Rows.Count > 0)
            //{
            using (ExcelPackage pck = new ExcelPackage())
            {
                ExcelWorksheet ws = pck.Workbook.Worksheets.Add("StartUp");
                //Load the datatable into the sheet, starting from cell A1. Print the column names on row 1
                ws.Cells["A1"].LoadFromDataTable(submittedFormSummaryEntityList, true);
                int columnCount = submittedFormSummaryEntityList.Columns.Count;
                string cName = GetExcelColumnName(columnCount);
                //Format the header for column 1-3
                using (ExcelRange rng = ws.Cells["A1:" + cName + "1"])
                {
                    
                    rng.Style.Font.Bold = true;
                    rng.Style.Fill.PatternType = ExcelFillStyle.Solid;                      //Set Pattern for the background to Solid
                    rng.Style.Fill.BackgroundColor.SetColor(Color.FromArgb(245, 245, 245));  //Set color to dark blue
                    rng.Style.Font.Color.SetColor(Color.Black);
                }

                var fcr = new FileContentResult(pck.GetAsByteArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                return fcr;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "0", "alert('File Uploaded Succesfully..!!');", true);
                
            }
         
        }

        //public bool sdsd()
        //{

        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "Alert", "javascript:alert('Please Enter UserName.');", true);

        //    return false;

        //}

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

    }
}
