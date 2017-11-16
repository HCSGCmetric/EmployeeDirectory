using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using EmployeeDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
    public class FormPermissionController : BaseController
    {


        [Authorization(ScreenAlias.FormPermission, Rights.View, true)]
        public ActionResult CreateFormPermission()
        {

            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.FormPermission));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            FormPermissionEntity objFormPermissionEntity = new FormPermissionEntity();
            FormPermissionBusiness objFormPermissionBusiness = new FormPermissionBusiness();
            UserBusiness objUserBusiness = new UserBusiness();
            List<SelectListItem> idUserTypeList = new List<SelectListItem>();
            idUserTypeList.Add(new SelectListItem() { Text = "select", Value = "" });
            List<UserTypeEntity> lstUserTypeEntity = new List<UserTypeEntity>();


            lstUserTypeEntity = objUserBusiness.GetAllUserTypeList();
            foreach (UserTypeEntity UserTypeEntity in lstUserTypeEntity)
            {
                idUserTypeList.Add(new SelectListItem() { Text = UserTypeEntity.Name, Value = Convert.ToString(UserTypeEntity.ID) });
            }

            ViewBag.UserTypeList = idUserTypeList;
            objFormPermissionEntity.lstFormPermissionEntity = new List<FormPermissionEntity>();

            return View(objFormPermissionEntity);
        }

        public ActionResult GetFormPermissionListById(int UserTypeId)
        {

            List<FormPermissionEntity> objFormPermissionEntitylst = new List<FormPermissionEntity>();
            FormPermissionBusiness objFormPermissionBusiness = new FormPermissionBusiness();
            objFormPermissionEntitylst = objFormPermissionBusiness.GetFormPermissionList(UserTypeId);
            return PartialView("_FormPermissionGrid", objFormPermissionEntitylst);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreateFormPermission(FormPermissionEntity objFormPermissionEntity)
        {
            int ans = 0;
            try
            {
                //objFormPermissionEntity.UserId = SessionManager.UserID;
                //objFormPermissionEntity.UserName = SessionManager.UserName;
                FormPermissionBusiness objFormPermissionBusiness = new FormPermissionBusiness();
                ans = objFormPermissionBusiness.CreateFormPermission(objFormPermissionEntity);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }

            return Content(ans.ToString());

        }
    }
}
