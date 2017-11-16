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
    public class GroupMasterController : BaseController
    {
        [Authorization(ScreenAlias.GroupMaster, Rights.View, true)]
        public ActionResult GroupMasterList()
        {
            
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.GroupMaster));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }


            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            GroupMasterEntity objGroupMasterEntity = new GroupMasterEntity();
            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<GroupMasterEntity> list = objGroupMasterBusiness.GetGroupMasterDetailList("", "", paggingArray[0], paggingArray[1], "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            objGroupMasterEntity.GroupMasterEntityList = list;
            ViewBag.DivRegDistList = new List<DivRegDistEntity>();
            return View(objGroupMasterEntity);
        }

        public ActionResult GroupMasterListPagingWise(string pageindex, string pagesize, string columnName, string sortorder)
        {

            GroupMasterEntity objGroupMasterEntity = new GroupMasterEntity();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();

            objGroupMasterEntity.Pageindex = Convert.ToInt32(pageindex);
            objGroupMasterEntity.pagesize = Convert.ToInt32(pagesize);

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<GroupMasterEntity> list = objGroupMasterBusiness.GetGroupMasterDetailList("", "", paggingArray[0], paggingArray[1], "", "");

            //for (int i = 0; i < list.Count; i++)
            //{
            //    list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            //}
            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_GroupMasterList", list);
        }


        [HttpPost]
        public ActionResult SaveGroupMaster(GroupMasterEntity model) {

            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            int ans = 0;
            try
            {
                //model.UserId = SessionManager.UserID;
                //model.UserName = SessionManager.UserName;
                ans = objGroupMasterBusiness.SaveGroupMaster(model);
            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }
            return RedirectToAction("GroupMasterList");
        }

        [Authorization(ScreenAlias.GroupMaster, Rights.Edit, true)]
        public ActionResult EditGroupMaster(int ID) {

            GroupMasterEntity objGroupMasterEntity = new GroupMasterEntity();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            try
            {

                objGroupMasterEntity = objGroupMasterBusiness.GetGroupDetailById(ID);

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }

            return View(objGroupMasterEntity);
        }

        [Authorization(ScreenAlias.GroupMaster, Rights.Delete, true)]
        public ActionResult DeleteGroupMaster(int ID)
        {

            GroupMasterEntity objGroupMasterEntity = new GroupMasterEntity();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            try
            {

                int ans = objGroupMasterBusiness.DeleteGroupMaster(ID);

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }

            return RedirectToAction("GroupMasterList");
        }

        public ActionResult GetAllGroupMasterList()
        {

            GroupMasterEntity objGroupMasterEntity = new GroupMasterEntity();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            try
            {
                objGroupMasterEntity.GroupMasterEntityList = objGroupMasterBusiness.GetAllGroupMasterList();

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
            }

            return Json(objGroupMasterEntity.GroupMasterEntityList, JsonRequestBehavior.AllowGet);
        }



    }
}
