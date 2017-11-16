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
    public class HomeController : BaseController
    {
        public ActionResult Dashboard()
        {
            List<FormPermissionEntity> listFormPermissionsEntity = CacheHelper.Get<List<FormPermissionEntity>>(CacheKeys.FormPermissions);

            if (listFormPermissionsEntity != null)
            {
                if (listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.CitrixUserApp)).Where(i => i.IsView == true).Count() > 0)
                {

                    if (listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.HsgUser.ToString()).Where(i => i.IsView == true).Count() > 0)
                    {
                        ViewBag.CitrixUserApp = listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.HsgUser.ToString()).FirstOrDefault().FormURL;
                    }
                    else
                    {
                        ViewBag.CitrixUserApp = listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.CitrixUserApp)).Where(i => i.IsView == true).FirstOrDefault().FormURL;
                    }
                }

                if (listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.EmployeeDirectory)).Where(i => i.IsView == true).Count() > 0)
                {

                    if (listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.EmployeeDirectory.ToString()).Where(i => i.IsView == true).Count() > 0)
                    {
                        ViewBag.EmployeeDirectoryApp = listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.EmployeeDirectory.ToString()).FirstOrDefault().FormURL;
                        
                    }
                    else
                    {
                        ViewBag.EmployeeDirectoryApp = listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.EmployeeDirectory)).Where(i => i.IsView == true).FirstOrDefault().FormURL;
                        
                    }
                }

                if (listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.MobileUser)).Where(i => i.IsView == true).Count() > 0)
                {

                    if (listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.MobileUsers.ToString()).Where(i => i.IsView == true).Count() > 0)
                    {
                        ViewBag.MobileuserApp = listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.MobileUsers.ToString()).FirstOrDefault().FormURL;
                    }
                    else
                    {
                        ViewBag.MobileuserApp = listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.MobileUser)).Where(i => i.IsView == true).FirstOrDefault().FormURL;
                    }
                }

                //if (listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.CorporateContact)).Where(i => i.IsView == true).Count() > 0)
                //{
                //    ViewBag.CorporateContact = listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.CorporateContact)).Where(i => i.IsView == true).FirstOrDefault().FormURL;
                //}

                //ViewBag.CorporateContact = "EmpDirectory/CorporateContacts.aspx";
                ViewBag.CorporateContact = "CorporateContact/CC";

                if (listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.FacilityRealignment)).Where(i => i.IsView == true).Count() > 0)
                {
                    ViewBag.FacilityRealignment = listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.FacilityRealignment)).Where(i => i.IsView == true).FirstOrDefault().FormURL;
                }
                

                if (listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.AssetManagement)).Where(i => i.IsView == true).Count() > 0)
                {

                    if (listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.AssetPurchaseOrder.ToString()).Where(i => i.IsView == true).Count() > 0)
                    {
                        ViewBag.AssetManagementAppURL = listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.AssetPurchaseOrder.ToString()).FirstOrDefault().FormURL;
                    }
                    else
                    {
                        ViewBag.AssetManagementAppURL = listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.AssetManagement)).Where(i => i.IsView == true).FirstOrDefault().FormURL;
                    }
                }

                ViewBag.CoupaAdmin = "EmpDirectory/coupa_users.aspx";

                if (listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.DssiAdmin)).Where(i => i.IsView == true).Count() > 0)
                {

                    if (listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.DSSIUserAdmin.ToString()).Where(i => i.IsView == true).Count() > 0)
                    {
                        ViewBag.DSSIAdmin = listFormPermissionsEntity.Where(i => i.FormControllerName == ScreenAlias.DSSIUserAdmin.ToString()).FirstOrDefault().FormURL;
                    }
                    else
                    {
                        ViewBag.DSSIAdmin = listFormPermissionsEntity.Where(i => i.FormApplicationName == StringEnum.GetStringValue(ApplicationNames.DssiAdmin)).Where(i => i.IsView == true).FirstOrDefault().FormURL;
                    }
                }

            }
            
            ViewBag.listFormPermissionsEntity = listFormPermissionsEntity;
            return View();
        }

        
    }
}
