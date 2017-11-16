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
    public class EmployeeAdminUserController : BaseController
    {

        [Authorization(ScreenAlias.EmployeeAdminUser, Rights.View, true)]
        public ActionResult EmployeeDirectoryAdminUserList()
        {
            List<AdminUserEntity> adminUserList = new List<AdminUserEntity>();

            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.EmployeeAdminUser));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            adminUserList = new UserBusiness().GetEmployeeDirectoryUserList();
            return View(adminUserList);
        }


        [Authorization(ScreenAlias.EmployeeAdminUser, Rights.Add, true)]
        public ActionResult CreateEmpAdminUser()
        {
            List<GroupMasterEntity> lstGroupMasterEntity = new List<GroupMasterEntity>();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            List<SelectListItem> idGroupMaster = new List<SelectListItem>();
            AdminUserModel model = new AdminUserModel();

            idGroupMaster.Add(new SelectListItem() { Text = "select", Value = "0" });
            lstGroupMasterEntity = objGroupMasterBusiness.GetAllGroupMasterList();

            foreach (GroupMasterEntity objGroupMasterEntity in lstGroupMasterEntity)
            {
                idGroupMaster.Add(new SelectListItem() { Text = objGroupMasterEntity.Name, Value = Convert.ToString(objGroupMasterEntity.ID) });
            }

            var UserTypeList = new List<SelectListItem>() 
            { new SelectListItem() { Text = "Select", Value = "" },
              new SelectListItem() { Text = "Employee Directory", Value = "3" } 
            };
            ViewBag.UserTypeList = UserTypeList;
            ViewBag.GroupMasterList = idGroupMaster;
            return View();
        }


        [HttpPost]
        public ActionResult CreateEmpAdminUser(AdminUserModel model)
        {
            //Mapper.CreateMap<AdminUserModel, AdminUserEntity>();
            //AdminUserEntity entity = Mapper.Map<AdminUserModel, AdminUserEntity>(model);
            if (!new UserBusiness().CheckEmpUserAlreadyExists(model.UserName.Trim()))
            {
                AdminUserEntity entity = new AdminUserEntity();
                entity.UserName = model.UserName.Trim();
                entity.FirstName = model.FirstName.Trim();
                entity.LastName = model.LastName.Trim();
                entity.Email = model.Email.Trim();
                entity.UserType = model.UserType;
                entity.GroupId = model.GroupId;
                //entity.Password = MD5Helper.Utf8MD5Hex(model.Password);
                int result = new UserBusiness().SaveAdminUser(entity);
                if (result > 0)
                {
                    return RedirectToAction("EmployeeDirectoryAdminUserList");
                }
                ViewBag.ErrorMessage = "An Error occured while processing your request.";
            }
            else
            {
                List<GroupMasterEntity> lstGroupMasterEntity = new List<GroupMasterEntity>();
                GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
                List<SelectListItem> idGroupMaster = new List<SelectListItem>();

                var UserTypeList = new List<SelectListItem>() {new SelectListItem() { Text = "Employee Directory", Value = UserType.EmployeeDirectory.ToString() } 
                                                      };
                lstGroupMasterEntity = objGroupMasterBusiness.GetAllGroupMasterList();

                foreach (GroupMasterEntity objGroupMasterEntity in lstGroupMasterEntity)
                {
                    idGroupMaster.Add(new SelectListItem() { Text = objGroupMasterEntity.Name, Value = Convert.ToString(objGroupMasterEntity.ID) });
                }
                ViewBag.GroupMasterList = idGroupMaster;
                ViewBag.UserTypeList = UserTypeList;
                ViewBag.ErrorMessage = "User Already Exists";

            }

            return View();
        }


        [Authorization(ScreenAlias.EmployeeAdminUser, Rights.Edit, true)]
        public ActionResult EditEmpAdminUser(int ID)
        {
            List<GroupMasterEntity> lstGroupMasterEntity = new List<GroupMasterEntity>();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            AdminUserEntity entity = new UserBusiness().GetAdminUserDetailById(ID);
            List<SelectListItem> idGroupMaster = new List<SelectListItem>();
            idGroupMaster.Add(new SelectListItem() { Text = "select", Value = "0" });
            AdminUserModel model = new AdminUserModel();

            lstGroupMasterEntity = objGroupMasterBusiness.GetAllGroupMasterList();

            foreach (GroupMasterEntity objGroupMasterEntity in lstGroupMasterEntity)
            {
                idGroupMaster.Add(new SelectListItem() { Text = objGroupMasterEntity.Name, Value = Convert.ToString(objGroupMasterEntity.ID) });
            }

            model.UserName = entity.UserName;
            model.FirstName = entity.FirstName;
            model.LastName = entity.LastName;
            model.Email = entity.Email;
            model.UserType = entity.UserType;
            model.IsLocked = entity.IsLocked;
            model.GroupId = entity.GroupId;
            var UserTypeList = new List<SelectListItem>() { 
                                                               new SelectListItem() { Text = "Employee Directory", Value = "3" } 
                                                      };
            ViewBag.UserTypeList = UserTypeList;
            ViewBag.GroupMasterList = idGroupMaster;
            return View(model);
        }


        [HttpPost]
        public ActionResult EditEmpAdminUser(AdminUserModel model)
        {
            AdminUserEntity entity = new AdminUserEntity();
            entity.ID = model.ID;
            entity.UserName = model.UserName;
            entity.FirstName = model.FirstName;
            entity.LastName = model.LastName;
            entity.Email = model.Email;
            entity.UserType = model.UserType;
            entity.GroupId = model.GroupId;
            entity.UpdatedBy = Convert.ToInt32(Session[AppConstant.USERID]);
            entity.IsLocked = model.IsLocked;
            entity.UpdatedDate = DateTime.Now;



            int result = new UserBusiness().UpdateAdminUser(entity);
            if (result > 0)
            {
                return RedirectToAction("EmployeeDirectoryAdminUserList");
            }
            ViewBag.status = "An Error occured while processing your request.";
            return View();
        }

        [Authorization(ScreenAlias.EmployeeAdminUser, Rights.Delete, true)]
        public ActionResult DeleteEmpAdminUser(int ID)
        {
            int result = new UserBusiness().DeleteAdminUser(ID);
            if (result > 0)
            {
                return RedirectToAction("EmployeeDirectoryAdminUserList");
            }
            ViewBag.status = "An Error occured while processing your request.";
            return View();
        }




    }
}
