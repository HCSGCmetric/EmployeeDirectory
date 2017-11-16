using EmployeeDirectory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Helper
{
    public static class SessionManager
    {
        public static int UserID
        {
            get
            {
                if (HttpContext.Current.Session["UserID"] != null)
                {

                    return int.Parse(Convert.ToString(HttpContext.Current.Session["UserID"]));
                }
                else
                {
                    return 0;
                }
            }

            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }
        
        public static string UserName
        {
            get
            {
                if (HttpContext.Current.Session["UserName"] != null)
                {

                    return Convert.ToString(HttpContext.Current.Session["UserName"]);
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                HttpContext.Current.Session["UserName"] = value;
            }
        }

        public static string UserEmail
        {
            get
            {
                if (HttpContext.Current.Session["UserEmail"] != null)
                {

                    return Convert.ToString(HttpContext.Current.Session["UserEmail"]);
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                HttpContext.Current.Session["UserEmail"] = value;
            }
        }

        public static bool IsFieldUser
        {
            get
            {
                if (HttpContext.Current.Session[AppConstant.FIELDUSER] != null)
                {

                    return (Convert.ToBoolean(HttpContext.Current.Session[AppConstant.FIELDUSER]));
                }
                else
                {
                    return false;
                }
            }
            set
            {
                HttpContext.Current.Session[AppConstant.FIELDUSER] = value;
            }
        }

        public static int GroupId
        {
            get
            {
                if (HttpContext.Current.Session[AppConstant.GROUPID] != null)
                {

                    return int.Parse(Convert.ToString(HttpContext.Current.Session[AppConstant.GROUPID]));
                }
                else
                {
                    return 0;
                }
            }
            set
            {
                HttpContext.Current.Session[AppConstant.GROUPID] = value;
            }
        }

        public static string UserType
        {
            get
            {
                if (HttpContext.Current.Session[AppConstant.USERTYPE] != null)
                {

                    return Convert.ToString(HttpContext.Current.Session[AppConstant.USERTYPE]);
                }
                else
                {
                    return string.Empty;
                }
            }

            set
            {
                HttpContext.Current.Session[AppConstant.USERTYPE] = value;
            }
        }

        public static List<FieldPermissionEntity> FieldPermission
        {
            get
            {
                if (HttpContext.Current.Session[AppConstant.FIELDPERMISSION] != null)
                {

                    return (List<FieldPermissionEntity>)(HttpContext.Current.Session[AppConstant.FIELDPERMISSION]);
                }
                else
                {
                    return new List<FieldPermissionEntity>();
                }
            }

            set
            {
                HttpContext.Current.Session[AppConstant.FIELDPERMISSION] = value;
            }
        }

        public static List<FormPermissionEntity> FormPermission
        {
            get
            {
                if (HttpContext.Current.Session[AppConstant.FORMPERMISSION] != null)
                {

                    return (List<FormPermissionEntity>)(HttpContext.Current.Session[AppConstant.FORMPERMISSION]);
                }
                else
                {
                    return new List<FormPermissionEntity>();
                }
            }

            set
            {
                HttpContext.Current.Session[AppConstant.FORMPERMISSION] = value;
            }
        }

        public static bool IsGoogleAuthenticate {
            get
            {
                if (HttpContext.Current.Session["IsGoogleAuthenticate"] != null)
                {

                    return (Convert.ToBoolean(HttpContext.Current.Session["IsGoogleAuthenticate"]));
                }
                else
                {
                    return false;
                }
            }
            set
            {
                HttpContext.Current.Session["IsGoogleAuthenticate"] = value;
            }
        }
    }
}