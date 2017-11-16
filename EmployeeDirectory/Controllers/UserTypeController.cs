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
    public class UserTypeController : BaseController
    {
        [Authorization(ScreenAlias.UserType, Rights.View, true)]
        public ActionResult UserTypeList()
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.UserType));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            UserTypeEntity objUserTypeEntity = new UserTypeEntity();
            List<UserTypeEntity> List = new List<UserTypeEntity>();
            UserTypeBusiness objUserTypeBusiness = new UserTypeBusiness();

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<UserTypeEntity> list = objUserTypeBusiness.GetHSGUserTypeList("", "", paggingArray[0], paggingArray[1], "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            objUserTypeEntity.UserTypeEntityList = list;

            return View(objUserTypeEntity);
        }

        [HttpPost]
        public ActionResult SaveUserType(UserTypeEntity model)
        {

            UserTypeBusiness objUserTypeBusiness = new UserTypeBusiness();
            int ans = 0;
            try
            {
                //model.UserId = SessionManager.UserID;
                //model.UserName = SessionManager.UserName;
                ans = objUserTypeBusiness.SaveUserType(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }
            return RedirectToAction("UserTypeList");
        }

        [Authorization(ScreenAlias.UserType, Rights.Edit, true)]
        public ActionResult EditUserType(int ID)
        {

            UserTypeEntity objUserTypeEntity = new UserTypeEntity();
            UserTypeBusiness objUserTypeBusiness = new UserTypeBusiness();
            try
            {

                objUserTypeEntity = objUserTypeBusiness.GetUserTypeDetailById(ID);

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }

            return View(objUserTypeEntity);
        }

        [Authorization(ScreenAlias.UserType, Rights.Delete, true)]
        public ActionResult DeleteUserType(int ID)
        {

            UserTypeEntity objUserTypeEntity = new UserTypeEntity();
            UserTypeBusiness objUserTypeBusiness = new UserTypeBusiness();
            try
            {

                int ans = objUserTypeBusiness.DeleteUserType(ID);

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }

            return RedirectToAction("UserTypeList");
        }

        public ActionResult GetAllUserTypeList()
        {

            UserTypeEntity objUserTypeEntity = new UserTypeEntity();
            UserTypeBusiness objUserTypeBusiness = new UserTypeBusiness();
            try
            {
                objUserTypeEntity.UserTypeEntityList = objUserTypeBusiness.GetAllUserTypeList();

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }

            return Json(objUserTypeEntity.UserTypeEntityList, JsonRequestBehavior.AllowGet);
        }

    }
}
