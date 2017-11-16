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
    public class AdminUserController : BaseController
    {
        /// <summary>
        /// Admins the user list.
        /// </summary>
        /// <returns></returns>
        [Authorization(ScreenAlias.AdminUser, Rights.View, true)]
        public ActionResult AdminUserList()
        {
            List<AdminUserEntity> adminUserList = new List<AdminUserEntity>();
            List<SelectListItem> idUserTypeList = new List<SelectListItem>();
            List<UserTypeEntity> lstUserTypeEntity = new List<UserTypeEntity>();
            UserBusiness objUserBusiness = new UserBusiness();

            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.AdminUser));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }
            lstUserTypeEntity = objUserBusiness.GetAllUserTypeList();
            foreach (UserTypeEntity UserTypeEntity in lstUserTypeEntity)
            {
                idUserTypeList.Add(new SelectListItem() { Text = UserTypeEntity.Name, Value = Convert.ToString(UserTypeEntity.ID) });
            }
            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            adminUserList = new UserBusiness().GetAdminUserList("", "", "", "", paggingArray[0], paggingArray[1], "", "");
            for (int i = 0; i < adminUserList.Count; i++)
            {
                adminUserList[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }
            ViewBag.UserTypeList = idUserTypeList;

            return View(adminUserList);
        }

        public ActionResult AdminUserListPagingSortingList(string firstName, string lastName, string username, string UserType, string pagesize, string pageindex, string columnName, string sortorder)
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.AdminUser));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            UserBusiness objUserBusiness = new UserBusiness();
            if (string.IsNullOrEmpty(pageindex))
            {
                pageindex = "1";
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(Convert.ToInt32(pageindex), int.Parse(pagesize));
            List<AdminUserEntity> list = objUserBusiness.GetAdminUserList(firstName, lastName, username, UserType, paggingArray[0], paggingArray[1], columnName, sortorder);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AdminUserWebGrid", list);
        }

        /// <summary>
        /// Creates the admin user.
        /// </summary>
        /// <returns></returns>
        [Authorization(ScreenAlias.AdminUser, Rights.Add, true)]
        public ActionResult CreateAdminUser()
        {
            List<GroupMasterEntity> lstGroupMasterEntity = new List<GroupMasterEntity>();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            List<SelectListItem> idGroupMaster = new List<SelectListItem>();
            AdminUserModel model = new AdminUserModel();
            List<UserTypeEntity> lstUserTypeEntity = new List<UserTypeEntity>();
            UserBusiness objUserBusiness = new UserBusiness();
            List<SelectListItem> idUserTypeList = new List<SelectListItem>();

            idGroupMaster.Add(new SelectListItem() { Text = "select", Value = "0" });
            lstGroupMasterEntity = objGroupMasterBusiness.GetAllGroupMasterList();

            foreach (GroupMasterEntity objGroupMasterEntity in lstGroupMasterEntity)
            {
                idGroupMaster.Add(new SelectListItem() { Text = objGroupMasterEntity.Name, Value = Convert.ToString(objGroupMasterEntity.ID) });
            }

            lstUserTypeEntity = objUserBusiness.GetAllUserTypeList();
            foreach (UserTypeEntity UserTypeEntity in lstUserTypeEntity)
            {
                idUserTypeList.Add(new SelectListItem() { Text = UserTypeEntity.Name, Value = Convert.ToString(UserTypeEntity.ID) });
            }

            ViewBag.UserTypeList = idUserTypeList;
            ViewBag.GroupMasterList = idGroupMaster;
            return View();
        }

        /// <summary>
        /// Creates the admin user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult CreateAdminUser(AdminUserModel model)
        {
            //Mapper.CreateMap<AdminUserModel, AdminUserEntity>();
            //AdminUserEntity entity = Mapper.Map<AdminUserModel, AdminUserEntity>(model);
            try
            {
                if (!new UserBusiness().CheckUserAlreadyExists(model.UserName.Trim()))
                {
                    AdminUserEntity entity = new AdminUserEntity();
                    entity.UserName = model.UserName.Trim();
                    entity.FirstName = model.FirstName.Trim();
                    entity.LastName = model.LastName.Trim();
                    entity.Email = model.Email.Trim();
                    if (model.UserTypeIds != null && model.UserTypeIds.Count() > 0)
                    {
                        entity.UserType = String.Join(",", model.UserTypeIds.ToArray());
                    }
                    entity.GroupId = model.GroupId;
                    entity.IsLocked = model.IsLocked;
                    //entity.Password = MD5Helper.Utf8MD5Hex(model.Password);
                    int result = new UserBusiness().SaveAdminUser(entity);
                    if (result > 0)
                    {
                        return RedirectToAction("AdminUserList");
                    }

                    ViewBag.ErrorMessage = "An Error occured while processing your request.";
                }
                else
                {
                    List<GroupMasterEntity> lstGroupMasterEntity = new List<GroupMasterEntity>();
                    GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
                    List<SelectListItem> idGroupMaster = new List<SelectListItem>();

                    var UserTypeList = new List<SelectListItem>() {new SelectListItem() { Text = "Select", Value = "" },
                                                               new SelectListItem() { Text = "Super User", Value = UserType.SuperUser.ToString() },
                                                               new SelectListItem() { Text = "User", Value = UserType.User.ToString() },
                                                               new SelectListItem() { Text = "Employee Directory", Value = UserType.EmployeeDirectory.ToString() } 
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
            }
            catch (Exception ex)
            {
                new FieldMasterBusiness().StoreError(ex.Message.ToString());
                throw new Exception("CreateAdminUser Exception :" + ex);
            }


            return View();
        }

        /// <summary>
        /// Edits the admin user.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns></returns>
        [Authorization(ScreenAlias.AdminUser, Rights.Edit, true)]
        public ActionResult EditAdminUser(int ID)
        {
            List<GroupMasterEntity> lstGroupMasterEntity = new List<GroupMasterEntity>();
            GroupMasterBusiness objGroupMasterBusiness = new GroupMasterBusiness();
            AdminUserEntity entity = new UserBusiness().GetAdminUserDetailById(ID);
            List<SelectListItem> idGroupMaster = new List<SelectListItem>();
            idGroupMaster.Add(new SelectListItem() { Text = "select", Value = "0" });
            AdminUserModel model = new AdminUserModel();
            List<UserTypeEntity> lstUserTypeEntity = new List<UserTypeEntity>();
            UserBusiness objUserBusiness = new UserBusiness();
            List<SelectListItem> idUserTypeList = new List<SelectListItem>();

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

            lstUserTypeEntity = objUserBusiness.GetAllUserTypeList();
            foreach (UserTypeEntity UserTypeEntity in lstUserTypeEntity)
            {
                idUserTypeList.Add(new SelectListItem() { Text = UserTypeEntity.Name, Value = Convert.ToString(UserTypeEntity.ID) });
            }

            ViewBag.UserTypeList = idUserTypeList;
            ViewBag.GroupMasterList = idGroupMaster;
            return View(model);
        }

        /// <summary>
        /// Edits the admin user.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult EditAdminUser(AdminUserModel model)
        {
            try
            {
                AdminUserEntity entity = new AdminUserEntity();
                entity.ID = model.ID;
                entity.UserName = model.UserName;
                entity.FirstName = model.FirstName;
                entity.LastName = model.LastName;
                entity.Email = model.Email;

                if (model.UserTypeIds != null && model.UserTypeIds.Count() > 0 )
                {
                    entity.UserType = String.Join(",", model.UserTypeIds.ToArray());
                }


                entity.GroupId = model.GroupId;
                entity.UpdatedBy = Convert.ToInt32(Session[AppConstant.USERID]);
                entity.IsLocked = model.IsLocked;
                entity.UpdatedDate = DateTime.Now;



                int result = new UserBusiness().UpdateAdminUser(entity);
                if (result > 0)
                {
                    return RedirectToAction("AdminUserList");
                }
                ViewBag.status = "An Error occured while processing your request.";
            }
            catch (Exception ex)
            {
                new FieldMasterBusiness().StoreError(ex.Message.ToString());
                throw new Exception("EditAdminUser Exception :" + ex);
            }
            
            return View();
        }

        /// <summary>
        /// Deletes the admin user.
        /// </summary>
        /// <param name="ID">The identifier.</param>
        /// <returns></returns>
        [Authorization(ScreenAlias.AdminUser, Rights.Delete, true)]
        public ActionResult DeleteAdminUser(int ID)
        {
            int result = new UserBusiness().DeleteAdminUser(ID);
            if (result > 0)
            {
                return RedirectToAction("AdminUserList");
            }
            ViewBag.status = "An Error occured while processing your request.";
            return View();
        }


    }
}
