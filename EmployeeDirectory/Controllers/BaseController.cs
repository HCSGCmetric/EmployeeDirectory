using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using Microsoft.Owin.Security;
using OneLogin.Saml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EmployeeDirectory.Controllers
{
    public class BaseController : Controller
    {
        //
        // GET: /Base/

        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Called before the action method is invoked.
        /// </summary>
        /// <param name="filterContext">Information about the current request and action.</param>
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var ActionName = filterContext.ActionDescriptor.ActionName;
            var controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;
            //int userId = Convert.ToInt32(filterContext.HttpContext.Session[AppConstant.USERID]);
            string username = Convert.ToString(filterContext.HttpContext.Session[AppConstant.USERNAME]);
            if (username == "" || username == null)
            {
                OneLoginAuthenticate();
                //filterContext.Result = new RedirectToRouteResult(
                //    new RouteValueDictionary 
                //    { 
                //        { "controller", "Login" }, 
                //        { "action", "AdminLogin" }, 
                //        { "returnUrl", Request.RawUrl.ToString() }
                //    });
            }

            ViewBag.CurrentApplicationName = controllerName;

            //if ((ActionName == "EmployeeDirectoryList" || ActionName == "ArcustViewList") && Convert.ToString(Session[EmployeeDirectory.Entity.AppConstant.USERTYPE]) == Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser))
            //{
            //    return;
            //}
            //else{
            //    filterContext.Result = new RedirectToRouteResult(
            //        new RouteValueDictionary 
            //    { 
            //        { "controller", "ArcustView" }, 
            //        { "action", "ArcustViewList" } 
            //    });
            //}

            if ((controllerName == "FieldMaster" || controllerName == "GroupMaster" || controllerName == "User" || ActionName == "AdminUserList" || controllerName == "HsgUser") && Convert.ToString(Session[EmployeeDirectory.Entity.AppConstant.USERTYPE]) == Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary 
                { 
                    { "controller", "ArcustView" }, 
                    { "action", "ArcustViewList" } 
                });
            }
            else if ((controllerName == "FieldMaster" || controllerName == "GroupMaster" || controllerName == "User" || ActionName == "AdminUserList" || ActionName == "CreateAdminUser" || ActionName == "EditAdminUser" || controllerName == "HsgUser") && Convert.ToString(Session[EmployeeDirectory.Entity.AppConstant.USERTYPE]) == Convert.ToString(EmployeeDirectory.Entity.UserType.EmployeeDirectory))
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary 
                { 
                    { "controller", "ArcustView" }, 
                    { "action", "ArcustViewList" } 
                });
            }
            else if ((controllerName == "EmployeeDirectory" || ActionName == "EmployeeDirectoryAdminUserList" || ActionName == "CreateEmpAdminUser" || ActionName == "EditEmpAdminUser") && Convert.ToString(Session[EmployeeDirectory.Entity.AppConstant.USERTYPE]) == Convert.ToString(EmployeeDirectory.Entity.UserType.User))
            {
                filterContext.Result = new RedirectToRouteResult(
                       new RouteValueDictionary 
                { 
                    { "controller", "ArcustView" }, 
                    { "action", "ArcustViewList" } 
                });

            }
            //else if (controllerName == "Calendar")
            //{
            //    if (!SessionManager.IsGoogleAuthenticate)
            //    {
            //        GoogleAuthentication();
            //        //new ChallengeResult("Google", Url.Action("GoogleLoginCallback", "Base", new { ReturnUrl = "" }));
            //    }
            //    else
            //    {
            //        filterContext.Result = new RedirectToRouteResult(
            //           new RouteValueDictionary 
            //        { 
            //            { "controller", "ArcustView" }, 
            //            { "action", "ArcustViewList" } 
            //        });
            //    }
            //}

        }

        private void OneLoginAuthenticate()
        {
            //Session[AppConstant.USERNAME] = "cfox";
            //HttpCookie cookieUser = new HttpCookie("loginuser", "cfox");
            //RedirectToAction("Dashboard", "Home");

            AccountSettings accountSettings = new AccountSettings();
            if (Request.Form["SAMLResponse"] == null)
            {
                AppSettings appsettings = new AppSettings();
                appsettings.assertionConsumerServiceUrl = Request.Url.OriginalString;
                OneLogin.Saml.AuthRequest req = new AuthRequest(appsettings, accountSettings);

                //Response.Redirect(accountSettings.idp_sso_target_url + "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64)));
                string reqstr = accountSettings.idp_sso_target_url + "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64));
                //return View();
                HttpContext.Response.Redirect(reqstr);
                //return RedirectToAction("Index", "Home");
            }
            else
            {


                //this means that user signed in thru onelogin
                //now need to check if onelogin response is valid
                OneLogin.Saml.Response samlResponse = new Response(accountSettings);
                samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]);

                if (samlResponse.IsValid())
                {
                    //Response.Write("OK!");
                    //Response.Write(samlResponse.GetNameID());
                    string loginUser = samlResponse.GetNameID();
                    if (loginUser.IndexOf('@') >= 0)
                    {
                        loginUser = loginUser.Substring(0, loginUser.IndexOf('@'));
                    }
                    Session[AppConstant.USERNAME] = loginUser;
                    //Session["loginuser"] = loginUser;
                    //create a cookie next
                    HttpCookie cookieUser = new HttpCookie("loginuser", loginUser);
                    //HttpContext.Current.Response.SetCookie(cookieUser);
                    RedirectToAction("Dashboard", "Home");

                }
                else
                {
                    Response.Redirect("error401.aspx");
                }

            }
        }

        protected override void OnException(ExceptionContext filterContext)
        {
            var controllerName = filterContext.RouteData.Values["controller"];
            var actionName = filterContext.RouteData.Values["action"];
            Exception ex = filterContext.Exception;
            ex.Data.Add("Controller", controllerName);
            ex.Data.Add("Action", actionName);

            HttpException httpException = ex as HttpException;
            // ExceptionHandler removed by Rujuta Xavier on 27/06/2016
            //Response.Redirect(Url.Action("PageNotFound", "Error"));
        }

        
    }
}
