using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace EmployeeDirectory.Controllers
{
    public class FieldMasterController : BaseController
    {
        [Authorization(ScreenAlias.FieldMaster, Rights.View, true)]
        public ActionResult FieldMasterList()
        {

            FieldMasterBusiness objFieldMasterBusiness = new FieldMasterBusiness();
            List<GroupMasterEntity> GroupMasterEntitylst = new List<GroupMasterEntity>();
            List<SelectListItem> idGroupMasterEntityList = new List<SelectListItem>();

            idGroupMasterEntityList.Add(new SelectListItem() { Text = "select", Value = "0" });

            GroupMasterEntitylst = objFieldMasterBusiness.GetGroupMasterEntitylst();


            foreach (GroupMasterEntity objGroupMasterEntity in GroupMasterEntitylst)
            {
                idGroupMasterEntityList.Add(new SelectListItem() { Text = objGroupMasterEntity.Name, Value = objGroupMasterEntity.ID.ToString() });
            }



            ViewBag.GroupMasterList = idGroupMasterEntityList;


            return View();
        }

         [Authorization(ScreenAlias.FieldMaster, Rights.View, true)]
        public ActionResult GetFieldDetailByGroupId(int ID)
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.FieldMaster));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }
            FieldMasterBusiness objFieldMasterBusiness = new FieldMasterBusiness();
            List<FieldMasterEntity> FieldMasterEntitylst = new List<FieldMasterEntity>();
            List<SelectListItem> idFieldMasterEntityList = new List<SelectListItem>();
            FieldMasterEntitylst = objFieldMasterBusiness.GetFieldMasterEntitylst(ID);
            return PartialView("_FieldMasterlist", FieldMasterEntitylst);

        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AssignFieldPremission(string FieldPermissionGridXML, int GroupId)
        {
            FieldMasterBusiness objFieldMasterBusiness = new FieldMasterBusiness();
            try
            {
             
                int ans = objFieldMasterBusiness.AssignFieldPremission(FieldPermissionGridXML, GroupId);
                return Content(ans.ToString());
            }
            catch (Exception ex)
            {
                objFieldMasterBusiness.StoreError(ex.Message.ToString());
                throw new Exception("FieldMasterController Exception :" + ex);
            }


        }

        public int StoreError(string errorMSG)
        {

            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ErrorMSg", DbType.String, errorMSG);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "InsertError", parameter);
            return result;

        }

    }
}
