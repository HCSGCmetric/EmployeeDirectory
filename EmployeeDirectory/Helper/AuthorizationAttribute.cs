using EmployeeDirectory.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeDirectory.Helper
{
    public class AuthorizationAttribute : AuthorizeAttribute
    {
        #region private variables

        /// <summary>
        /// The _screenalias
        /// </summary>
        private readonly string _screenalias;

        /// <summary>
        /// The _new r
        /// </summary>
        private readonly Rights _rights;

        private readonly bool _isAllowFieldUser;
        #endregion

        #region Constructors
        public AuthorizationAttribute(ScreenAlias screenalias, Rights rights, bool IsAllowFieldUser)
        {
            this._screenalias = StringEnum.GetStringValue(screenalias);
            this._rights = rights;
            this._isAllowFieldUser = IsAllowFieldUser;
        }
        #endregion

        #region Protected Methods
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (SessionManager.UserName == "" || SessionManager.UserName == null)
            {
                return false;
            }

            //AllowAccess authorize = AllowAccess.False;
            Rights permissiontype = _rights;
            var permission = CommonMethods.CheckPermission(this._screenalias);
            if (permission != null && permission.Count > 0)
            {
                foreach (FormPermissionEntity entity in permission)
                {
                    switch (permissiontype)
                    {
                        case Rights.Add:
                            if (entity.IsAdd) 
                            { 
                                return true; 
                            }
                            break;
                        case Rights.Edit:
                            if (entity.IsEdit) { return true; }
                            break;
                        case Rights.Delete:
                            if (entity.IsDelete) { return true; }
                            break;
                        case Rights.View:
                            if (entity.IsView) { return true; }
                            break;
                    }

                }
                return false;

            }
            else
            {
                if (SessionManager.IsFieldUser)
                {
                    switch (permissiontype)
                    {
                        case Rights.Add:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                        case Rights.Edit:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                        case Rights.Delete:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                        case Rights.View:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                    }
                }
            }

            return false;
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            if (SessionManager.UserID == 0)
            {
                if (filterContext.HttpContext.Request.IsAjaxRequest())
                {
                    filterContext.Result = new JsonResult()
                    {
                        Data = AppConstants.LOGOUT,
                        JsonRequestBehavior = JsonRequestBehavior.AllowGet
                    };
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary { { "action", "DashBoard" }, { "controller", "Home" } });
                }
            }
            else
            {
                //filterContext.Result = new HttpUnauthorizedResult();
                //filterContext.Controller.TempData["Message"] = "AccessDenied";
                //base.HandleUnauthorizedRequest(filterContext);
                filterContext.Result = new RedirectToRouteResult(new
                  RouteValueDictionary(new { controller = "Home", action = "DashBoard" }));
            }
        }
        #endregion

        public bool AuthorizeCore()
        {
            if (SessionManager.UserName == "" || SessionManager.UserName == null)
            {
                return false;
            }

            //AllowAccess authorize = AllowAccess.False;
            Rights permissiontype = _rights;
            var permission = CommonMethods.CheckPermission(this._screenalias);
            if (permission != null)
            {
                foreach (FormPermissionEntity entity in permission)
                {
                    switch (permissiontype)
                    {
                        case Rights.Add:
                            if (entity.IsAdd)
                            {
                                return true;
                            }
                            break;
                        case Rights.Edit:
                            if (entity.IsEdit) { return true; }
                            break;
                        case Rights.Delete:
                            if (entity.IsDelete) { return true; }
                            break;
                        case Rights.View:
                            if (entity.IsView) { return true; }
                            break;
                    }

                }
                return false;

            }
            else
            {
                if (SessionManager.IsFieldUser)
                {
                    switch (permissiontype)
                    {
                        case Rights.Add:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                        case Rights.Edit:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                        case Rights.Delete:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                        case Rights.View:
                            if (_isAllowFieldUser) { return true; }
                            return false;
                    }
                }
            }

            return false;
        }
    }
}