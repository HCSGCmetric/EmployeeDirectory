using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
    /// <summary>
    /// HSG User functionality class
    /// </summary>
    public class HsgUserController : BaseController
    {

        /// <summary>
        /// HSGs the user list.
        /// </summary>
        /// <returns></returns>
        [Authorization(ScreenAlias.HsgUser, Rights.View, true)]
        public ActionResult HsgUserList()
        {

            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.HsgUser));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<HSGUserEntity> list = hsgUser.GetHSGUserList("", "", "", "", "", "", "", "", "", paggingArray[0], paggingArray[1], "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }
            ViewBag.DisplaySidelist = new List<DivRegDistEntity>();
            return View(list);
        }

        /// <summary>
        /// HSGs the user list paging sorting list.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="username">The username.</param>
        /// <param name="title">The title.</param>
        /// <param name="div">The div.</param>
        /// <param name="reg">The reg.</param>
        /// <param name="dist">The dist.</param>
        /// <param name="eeid">The eeid.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="sortorder">The sortorder.</param>
        /// <returns></returns>
        public ActionResult HsgUserListPagingSortingList(string firstName, string lastName, string username, string title, string div, string reg, string dist, string eeid, string status, string pagesize, string pageindex, string columnName, string sortorder)
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.HsgUser));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            if (string.IsNullOrEmpty(pageindex))
            {
                pageindex = "1";
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(Convert.ToInt32(pageindex), int.Parse(pagesize));
            List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_HsgUserList", list);
        }

        /// <summary>
        /// Gets the HSG user details by identifier.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns></returns>
        public JsonResult GetHSGUserDetailsByID(int ID)
        {
            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            HSGUserEntity HSGuserDetails = hsgUser.GetHSGUserDetailsByID(ID);
            return Json(HSGuserDetails, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Updates the record.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="username">The username.</param>
        /// <param name="title">The title.</param>
        /// <param name="div">The div.</param>
        /// <param name="reg">The reg.</param>
        /// <param name="dist">The dist.</param>
        /// <param name="eeid">The eeid.</param>
        /// <param name="emailId">The email identifier.</param>
        /// <returns></returns>
        public JsonResult UpdateRecord(int id, string firstName, string lastName, string username, string title, string div, string reg, string dist, string eeid, string emailId, string status, string Approve)
        {
            HSGUserEntity hsgUserDetails = new HSGUserEntity();
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistDetails"] == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
                Session["DivRegDistDetails"] = divRegDistEntityList;
            }
            divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;
            hsgUserDetails.Id = id;
            hsgUserDetails.FirstName = firstName;
            hsgUserDetails.LastName = lastName;
            hsgUserDetails.UserName = username;
            hsgUserDetails.Title = title;
            //hsgUserDetails.Div = div;
            //hsgUserDetails.Reg = reg;
            //hsgUserDetails.Dist = dist;
            hsgUserDetails.EEID = eeid;
            hsgUserDetails.UpdatedBy = "";
            hsgUserDetails.Email = emailId;
            hsgUserDetails.Status = status;
            //hsgUserDetails.Approve = Approve;
            hsgUserDetails.LastUpdated = DateTime.Now;
            hsgUserDetails.UpdatedBy = Convert.ToString(Session[AppConstant.USERNAME]);
            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            int result = hsgUser.UpdateHSGUserRecord(hsgUserDetails, divRegDistEntityList);
            if (result > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult SaveRecord(string firstName, string lastName, string username, string title, string div, string reg, string dist, string eeid, string emailId, string status, string Approve)
        {
            HSGUserEntity hsgUserDetails = new HSGUserEntity();
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistDetails"] == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
                Session["DivRegDistDetails"] = divRegDistEntityList;
            }
            divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;
            hsgUserDetails.FirstName = firstName;
            hsgUserDetails.LastName = lastName;
            hsgUserDetails.UserName = username;
            hsgUserDetails.Title = title;
            //hsgUserDetails.Div = div;
            //hsgUserDetails.Reg = reg;
            //hsgUserDetails.Dist = dist;
            hsgUserDetails.EEID = eeid;
            hsgUserDetails.UpdatedBy = "";
            hsgUserDetails.Email = emailId;
            hsgUserDetails.Status = status;
            //hsgUserDetails.Approve = Approve;
            hsgUserDetails.LastUpdated = DateTime.Now;
            hsgUserDetails.UpdatedBy = Convert.ToString(Session[AppConstant.USERNAME]);
            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            int result = hsgUser.SaveHSGUserRecord(hsgUserDetails, divRegDistEntityList);
            if (result > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        [Authorization(ScreenAlias.HsgUser, Rights.Delete, true)]
        public JsonResult DeleteRecord(int id)
        {
            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            int result = hsgUser.DeleteHSGUserRecord(id);
            if (result > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult EditDivRegDistDetails(string RowNumber)
        {
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistDetails"] != null)
            {
                divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;
                var model = divRegDistEntityList.Where(p => p.RowNumber == int.Parse(RowNumber)).FirstOrDefault();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult DeleteDivRegDistDetails(int rowNumber)
        {
            List<DivRegDistEntity> divRegDistEntityList = new List<DivRegDistEntity>();
            if (Session["DivRegDistDetails"] != null)
            {
                divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;
                var model = divRegDistEntityList.Where(p => p.RowNumber == rowNumber).FirstOrDefault();
                divRegDistEntityList.Remove(model);
                Session["DivRegDistDetails"] = divRegDistEntityList;
            }

            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        [HttpPost]
        public ActionResult AddDivRegDistDetails(string title, string div, string reg, string dist, string Approve)
        {
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistDetails"] == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
                Session["DivRegDistDetails"] = divRegDistEntityList;
            }

            divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;
            DivRegDistEntity divRegDistEntity = new DivRegDistEntity();
            divRegDistEntity.EEID = "";
            divRegDistEntity.Title = title;
            divRegDistEntity.Div = div;
            divRegDistEntity.Reg = reg;
            divRegDistEntity.Dist = dist;
            divRegDistEntity.Approve = Approve;
            if (divRegDistEntityList.Count > 0)
            {
                divRegDistEntity.RowNumber = divRegDistEntityList.Max(m => m.RowNumber) + 1;
            }
            else
            {
                divRegDistEntity.RowNumber = 1;
            }

            divRegDistEntityList.Add(divRegDistEntity);
            Session["DivRegDistDetails"] = divRegDistEntityList;
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        [HttpPost]
        public ActionResult UpdateDivRegDistDetails(string title, string div, string reg, string dist, string Approve, string rowNumber)
        {
            List<DivRegDistEntity> divRegDistEntityList;
            int RowNumber = int.Parse(rowNumber);
            divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;
            foreach (DivRegDistEntity divRegDistEntity in divRegDistEntityList.Where(m => m.RowNumber == RowNumber))
            {
                divRegDistEntity.Title = title;
                divRegDistEntity.Div = div;
                divRegDistEntity.Reg = reg;
                divRegDistEntity.Dist = dist;
                divRegDistEntity.Approve = Approve;
            }
            Session["DivRegDistDetails"] = divRegDistEntityList;
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        public ActionResult ClearDivRegDistList()
        {
            Session["DivRegDistDetails"] = null;
            List<DivRegDistEntity> divRegDistEntityList = new List<DivRegDistEntity>();
            ViewBag.DisplaySidelist = new List<DivRegDistEntity>();
            //return Json(true, JsonRequestBehavior.AllowGet);
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        public ActionResult GetDivRegDistList(int id)
        {
            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            List<DivRegDistEntity> divRegDistEntityList = hsgUser.GetDivRegDistByID(id);
            if (divRegDistEntityList == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
            }
            Session["DivRegDistDetails"] = divRegDistEntityList;
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        public JsonResult VerifyHSGUsersApproval(string username)
        {
            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistDetails"] == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
                Session["DivRegDistDetails"] = divRegDistEntityList;
            }
            divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;

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

            string result = hsgUser.VerifyHSGUsersApproval(username, DivList, RegList, DistList, TitleList, ApprovalList);
            return Json(result, JsonRequestBehavior.AllowGet);
            
        }

        public JsonResult UpdateHSGUsersApproval(string username, bool isUpdate)
        {
            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistDetails"] == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
                Session["DivRegDistDetails"] = divRegDistEntityList;
            }
            divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;

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

            int result = hsgUser.UpdateApprovalForHSGUsers(username, DivList, RegList, DistList, TitleList, ApprovalList);
            if (result > 0)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult ValidateDivRegDistIfAlreadyExists(string title, string div, string reg, string dist, string rownumber)
        {
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistDetails"] == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
                Session["DivRegDistDetails"] = divRegDistEntityList;
            }
            divRegDistEntityList = Session["DivRegDistDetails"] as List<DivRegDistEntity>;
            bool result = false;
            foreach (DivRegDistEntity entity in divRegDistEntityList.Where(i => i.RowNumber != int.Parse(rownumber)))
            {
                if (entity.Div == div && entity.Reg == reg && entity.Dist == dist && entity.Title == title)
                {
                    result = true;
                    break;
                }
            }

            return Json(result, JsonRequestBehavior.AllowGet);

        }
    }
}
