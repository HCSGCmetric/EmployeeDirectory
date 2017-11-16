using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using EmployeeDirectory.Entity;

namespace EmployeeDirectory.Helper
{
    public class CommonMethods
    {
        public static List<FormPermissionEntity> CheckPermission(string controllername)
        {
            List<FormPermissionEntity> listFormPermissionsEntity = CacheHelper.Get<List<FormPermissionEntity>>(CacheKeys.FormPermissions);
            //FormPermissionEntity result = new FormPermissionEntity();
            if (listFormPermissionsEntity == null && SessionManager.IsFieldUser == false)
            {
                CacheHelper.RefreshCashItem(CacheKeys.FormPermissions);
            }
            if (SessionManager.IsFieldUser)
            {
                return null;
            }
            else
            {
               var result = (from data in listFormPermissionsEntity
                              where data.FormControllerName == controllername
                              select data).ToList();
                return result;
            }
        }

        public static FormPermissionEntity GetFormPermission(List<FormPermissionEntity> PermissionEntityList) 
        {
            FormPermissionEntity permissionentity = new FormPermissionEntity();
            permissionentity.IsAdd = false;
            permissionentity.IsEdit = false;
            permissionentity.IsDelete = false;
            permissionentity.IsView = false;

            foreach (FormPermissionEntity entity in PermissionEntityList)
            {
                if (entity.IsAdd) { permissionentity.IsAdd = true; }
                if (entity.IsEdit) { permissionentity.IsEdit = true; }
                if (entity.IsDelete) { permissionentity.IsDelete = true; }
                if (entity.IsView) { permissionentity.IsView = true; }
            }
            return permissionentity;
        }
    }
}