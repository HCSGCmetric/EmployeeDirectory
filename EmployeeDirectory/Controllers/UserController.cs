using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;

namespace EmployeeDirectory.Controllers
{
    public class UserController : BaseController
    {
        
        /// <summary>
        /// Users the list.
        /// </summary>
        /// <returns></returns>
       [Authorization(ScreenAlias.User, Rights.View, true)]
        public ActionResult UserList()
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.User));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            }

            EmployeeDirectoryCls employeeDirectoryCls = new EmployeeDirectoryCls();
            EmployeeDetails employeeDetails = new EmployeeDetails();
            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<EmployeeDetailsList> list = employeeDirectoryCls.GetEmployeeDetailsList("", "", paggingArray[0], paggingArray[1], "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            employeeDetails.UserList = list;
            ViewBag.DivRegDistList = new List<DivRegDistEntity>();
            return View(employeeDetails);
        }

        /// <summary>
        /// Users the list paging sorting list.
        /// </summary>
        /// <param name="firstName">The first name.</param>
        /// <param name="lastName">The last name.</param>
        /// <param name="pagesize">The pagesize.</param>
        /// <param name="pageindex">The pageindex.</param>
        /// <param name="columnName">Name of the column.</param>
        /// <param name="sortorder">The sortorder.</param>
        /// <returns></returns>
        public ActionResult UserListPagingSortingList(string firstName, string lastName, string pagesize, string pageindex, string columnName, string sortorder)
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.User));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            }

            EmployeeDirectoryCls employeeDirectoryCls = new EmployeeDirectoryCls();
            if (string.IsNullOrEmpty(pageindex))
            {
                pageindex = "1";
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(Convert.ToInt32(pageindex), int.Parse(pagesize));
            List<EmployeeDetailsList> employeeDetailsList = employeeDirectoryCls.GetEmployeeDetailsList(firstName, lastName, paggingArray[0], paggingArray[1], columnName, sortorder);
            for (int i = 0; i < employeeDetailsList.Count; i++)
            {
                employeeDetailsList[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_UserList", employeeDetailsList);
        }

        /// <summary>
        /// Gets the user details by identifier.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns></returns>
        public JsonResult GetUserDetailsByID(int ID) 
        {
            EmployeeDirectoryCls employeeDirectoryCls = new EmployeeDirectoryCls();
            UserDetails userDetails = employeeDirectoryCls.GetUserDetailsByID(ID);
            return Json(userDetails, JsonRequestBehavior.AllowGet);
            //return PartialView("_UserList", userDetails);
        }

        /// <summary>
        /// Saves the record.
        /// </summary>
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
        public JsonResult SaveRecord(string firstName, string lastName, string username, string title, string div, string reg, string dist, string eeid, string emailId) 
        {
            try
            {
                UserDetails userDetails = new UserDetails();
                List<DivRegDistEntity> divRegDistEntityList;
                if (Session["DivRegDistUserDetails"] == null)
                {
                    divRegDistEntityList = new List<DivRegDistEntity>();
                    Session["DivRegDistUserDetails"] = divRegDistEntityList;
                }
                divRegDistEntityList = Session["DivRegDistUserDetails"] as List<DivRegDistEntity>;
                userDetails.FIRSTNAME = firstName;
                userDetails.LASTNAME = lastName;
                userDetails.USERNAME = username;
                //userDetails.NewJobTitle = title;
                userDetails.DIV = div;
                userDetails.REG = reg;
                userDetails.DIST = dist;
                userDetails.EEID = eeid;
                userDetails.UPDATEDBY = "";
                userDetails.EMAIL = emailId;
                userDetails.LASTUPDATED = DateTime.Now;
                userDetails.UPDATEDBY = Convert.ToString(Session[AppConstant.USERNAME]);
                userDetails.STATUS = "Active";
                EmployeeDirectoryCls employeeDirectoryCls = new EmployeeDirectoryCls();
                int result = employeeDirectoryCls.SaveUserRecord(userDetails, divRegDistEntityList);
                if (result > 0)
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString(), JsonRequestBehavior.AllowGet);
            }
            
        }

        public ActionResult EditDivRegDistDetails(string RowNumber)
        {
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistUserDetails"] != null)
            {
                divRegDistEntityList = Session["DivRegDistUserDetails"] as List<DivRegDistEntity>;
                var model = divRegDistEntityList.Where(p => p.RowNumber == int.Parse(RowNumber)).FirstOrDefault();
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            return null;
        }

        public ActionResult DeleteDivRegDistDetails(int rowNumber)
        {
            List<DivRegDistEntity> divRegDistEntityList = new List<DivRegDistEntity>();
            if (Session["DivRegDistUserDetails"] != null)
            {
                divRegDistEntityList = Session["DivRegDistUserDetails"] as List<DivRegDistEntity>;
                var model = divRegDistEntityList.Where(p => p.RowNumber == rowNumber).FirstOrDefault();
                divRegDistEntityList.Remove(model);
                Session["DivRegDistUserDetails"] = divRegDistEntityList;
            }

            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        [HttpPost]
        public ActionResult AddDivRegDistDetails(string title, string div, string reg, string dist)
        {
            List<DivRegDistEntity> divRegDistEntityList;
            if (Session["DivRegDistUserDetails"] == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
                Session["DivRegDistUserDetails"] = divRegDistEntityList;
            }

            divRegDistEntityList = Session["DivRegDistUserDetails"] as List<DivRegDistEntity>;
            DivRegDistEntity divRegDistEntity = new DivRegDistEntity();
            divRegDistEntity.EEID = "";
            divRegDistEntity.Title = title;
            divRegDistEntity.Div = div;
            divRegDistEntity.Reg = reg;
            divRegDistEntity.Dist = dist;
            if (divRegDistEntityList.Count > 0)
            {
                divRegDistEntity.RowNumber = divRegDistEntityList.Max(m => m.RowNumber) + 1;
            }
            else
            {
                divRegDistEntity.RowNumber = 1;
            }

            divRegDistEntityList.Add(divRegDistEntity);
            Session["DivRegDistUserDetails"] = divRegDistEntityList;
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        [HttpPost]
        public ActionResult UpdateDivRegDistDetails(string title, string div, string reg, string dist, string rowNumber)
        {
            List<DivRegDistEntity> divRegDistEntityList;
            int RowNumber = int.Parse(rowNumber);
            divRegDistEntityList = Session["DivRegDistUserDetails"] as List<DivRegDistEntity>;
            foreach (DivRegDistEntity divRegDistEntity in divRegDistEntityList.Where(m => m.RowNumber == RowNumber))
            {
                divRegDistEntity.Div = div;
                divRegDistEntity.Title = title;
                divRegDistEntity.Reg = reg;
                divRegDistEntity.Dist = dist;
            }
            Session["DivRegDistUserDetails"] = divRegDistEntityList;
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        public ActionResult ClearDivRegDistList()
        {
            Session["DivRegDistUserDetails"] = null;
            List<DivRegDistEntity> divRegDistEntityList = new List<DivRegDistEntity>();
            ViewBag.DisplaySidelist = new List<DivRegDistEntity>();
            //return Json(true, JsonRequestBehavior.AllowGet);
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

        public ActionResult GetDivRegDistList(int id)
        {
            EmployeeDirectoryCls employeeDirectoryCls = new EmployeeDirectoryCls();
            List<DivRegDistEntity> divRegDistEntityList = employeeDirectoryCls.GetDivRegDistByID(id);
            if (divRegDistEntityList == null)
            {
                divRegDistEntityList = new List<DivRegDistEntity>();
            }
            Session["DivRegDistUserDetails"] = divRegDistEntityList;
            return PartialView("_DivRegDistList", divRegDistEntityList);
        }

    }
}
