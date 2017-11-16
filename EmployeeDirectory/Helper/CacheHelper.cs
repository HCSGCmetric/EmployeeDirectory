using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Helper
{
    public static class CacheHelper
    {
        /// <summary>
        /// Insert value into the cache using appropriate name/value pairs
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="o">Item to be cached</param>
        /// <param name="key">Name of item</param>
        public static void Add<T>(T obj, string key) where T : class
        {
            if (key == CacheKeys.FieldPermissions) 
            {
                SessionManager.FieldPermission = obj as List<FieldPermissionEntity>; 
            }
            if (key == CacheKeys.FormPermissions) 
            {
                SessionManager.FormPermission = obj as List<FormPermissionEntity>; 
            }
            
            //HttpContext.Current.Cache.Insert(key, obj, null, DateTime.Now.AddHours(5), System.Web.Caching.Cache.NoSlidingExpiration);
        }

        /// <summary>
        /// Remove item from cache 
        /// </summary>
        /// <param name="key">Name of cached item</param>
        public static void Clear(string key)
        {
            HttpContext.Current.Cache.Remove(key);
        }

        /// <summary>
        /// Check for item in cache
        /// </summary>
        /// <param name="key">Name of cached item</param>
        /// <returns></returns>
        public static bool Exists(string key)
        {
            return HttpContext.Current.Cache[key] != null;
        }

        /// <summary>
        /// Retrieve cached item
        /// </summary>
        /// <typeparam name="T">Type of cached item</typeparam>
        /// <param name="key">Name of cached item</param>
        /// <returns>Cached item as type</returns>
        public static T Get<T>(string key) where T : class
        {
            try
            {
                if (key == CacheKeys.FieldPermissions)
                {
                    return (T)Convert.ChangeType(SessionManager.FieldPermission, typeof(T));
                    //return (T)SessionManager.FieldPermission;
                }
                else if (key == CacheKeys.FormPermissions)
                {
                    return (T)Convert.ChangeType(SessionManager.FormPermission, typeof(T));
                    //SessionManager.FormPermission = obj as List<FormPermissionEntity>;
                }
                else { return default(T); }
                //return (T)HttpContext.Current.Cache[key];
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Refreshes the cash item.
        /// </summary>
        /// <param name="key">The key.</param>
        public static void RefreshCashItem(string key)
        {
            if (key == CacheKeys.FieldPermissions.ToString())
            {
                CacheHelper.Clear(key);
                List<FieldPermissionEntity> listFieldPermissionsEntity = new PermissionBusiness().GetFieldPermissionInformations(SessionManager.GroupId.ToString());
                CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
            }
            else if (key == CacheKeys.FormPermissions.ToString())
            {
                CacheHelper.Clear(key);
                List<FormPermissionEntity> listFormPermissionsEntity = new PermissionBusiness().GetFormPermissionInformations(SessionManager.UserType, null, null);
                CacheHelper.Add(listFormPermissionsEntity, CacheKeys.FormPermissions);
            }
            //else if (key == CacheKeys.FormPermissions.ToString())
            //{
            //    CacheHelper.Clear(key);
            //    var formPermissions = ServiceHelper.RoleSvcClient.GetFormPermissionByRoleID(SessionProvider.GetCurrentRoleId());
            //    CacheHelper.Add(formPermissions, CacheKeys.FormPermissions);
            //}
            //else if (key == CacheKeys.FormFieldPermissions.ToString())
            //{
            //    CacheHelper.Clear(key);
            //    var formFieldPermissions = ServiceHelper.RoleSvcClient.GetFormFieldPermissionByRoleID(SessionProvider.GetCurrentRoleId());
            //    CacheHelper.Add(formFieldPermissions, CacheKeys.FormFieldPermissions);
            //}
            //else if (key == CacheKeys.SystemConfiguration.ToString())
            //{
            //    CacheHelper.Clear(key);
            //    var systemConfigModel = ServiceHelper.ConfigurationSvcClient.GetSystemConfiguration();
            //    var emailConfigModel = ServiceHelper.ConfigurationSvcClient.GetEmailConfiguration();

            //    SytemConfigurationMasterModel sytemConfigurationMasterModel = new SytemConfigurationMasterModel();
            //    sytemConfigurationMasterModel.SystemConfig = new SystemConfigurationModel();
            //    sytemConfigurationMasterModel.EmailConfig = new EmailConfigurationModel();
            //    if (systemConfigModel != null)
            //    {
            //        sytemConfigurationMasterModel.SystemConfig.Id = systemConfigModel.Id;
            //        sytemConfigurationMasterModel.SystemConfig.MaxPrecisionSize = systemConfigModel.MaxPrecisionSize;
            //        sytemConfigurationMasterModel.SystemConfig.PageSize = systemConfigModel.PageSize;
            //    }

            //    if (emailConfigModel != null)
            //    {
            //        sytemConfigurationMasterModel.EmailConfig.Id = emailConfigModel.Id;
            //        sytemConfigurationMasterModel.EmailConfig.SMTPDomainName = emailConfigModel.SMTPDomainName;
            //        sytemConfigurationMasterModel.EmailConfig.SMTPEmail = emailConfigModel.SMTPEmail;
            //        sytemConfigurationMasterModel.EmailConfig.Port = emailConfigModel.Port;
            //        sytemConfigurationMasterModel.EmailConfig.MailHeader = emailConfigModel.MailHeader;
            //        sytemConfigurationMasterModel.EmailConfig.Password = emailConfigModel.Password;
            //    }

            //    CacheHelper.Add(sytemConfigurationMasterModel, CacheKeys.SystemConfiguration);
            //}
        }
    }

    public static class CacheKeys
    {
        /// <summary>
        /// The menu items
        /// </summary>
        public static readonly string MenuItems = "MenuItems";

        /// <summary>
        /// The roles
        /// </summary>
        public static readonly string Roles = "Roles";

        /// <summary>
        /// The form permissions
        /// </summary>
        public static readonly string FormPermissions = "FormPermissions";

        /// <summary>
        /// The field permissions
        /// </summary>
        public static readonly string FieldPermissions = "FieldPermissions";

        /// <summary>
        /// The item selectors
        /// </summary>
        public static readonly string ItemSelectors = "ItemSelectors";

        /// <summary>
        /// The system configuration
        /// </summary>
        public static readonly string SystemConfiguration = "SystemConfiguration";
    }
}