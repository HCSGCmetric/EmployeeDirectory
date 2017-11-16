using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using EmployeeDirectory.Models;
using OneLogin.Saml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml;

namespace EmployeeDirectory.Controllers
{
    public class LoginController : BaseController
    {
        //
        // GET: /Login/

        public ActionResult AdminLogin()
        {
            try
            {

                AdminLoginModel model = new AdminLoginModel();
                if (Convert.ToString(Session[AppConstant.USERNAME]) != "" && Session[AppConstant.USERNAME] != null)
                {
                    //string[] uname = model.UserName.Split('@');
                    string first = Session[AppConstant.USERNAME].ToString();
                    AdminUserEntity adminUserEntity = new UserBusiness().AdminUserLogin(first);

                    if (adminUserEntity != null && adminUserEntity.ID > 0)
                    {
                        if (adminUserEntity.IsLocked == true)
                        {
                            TempData["LoginError"] = "This user is locked.";
                            return RedirectToAction("AdminLogin", "Login");
                        }
                        List<FieldPermissionEntity> listFieldPermissionsEntity = new PermissionBusiness().GetFieldPermissionInformations(adminUserEntity.GroupId);
                        CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
                        List<FormPermissionEntity> listFormPermissionsEntity = new PermissionBusiness().GetFormPermissionInformations(adminUserEntity.UserType, null, null);

                        if (listFormPermissionsEntity.Count == 0)
                        {
                            Session[AppConstant.FIELDUSER] = true;
                        }


                        CacheHelper.Add(listFormPermissionsEntity, CacheKeys.FormPermissions);
                        Session[AppConstant.listFormPermissionsEntity] = listFormPermissionsEntity;

                        Session[AppConstant.GROUPID] = adminUserEntity.GroupId;
                        Session[AppConstant.USERID] = adminUserEntity.ID;
                        Session[AppConstant.USERNAME] = adminUserEntity.UserName;
                        Session[AppConstant.ADMINUSEREMAIL] = adminUserEntity.Email;
                        Session[AppConstant.NAME] = adminUserEntity.FirstName + " " + adminUserEntity.LastName;
                        Session[AppConstant.USERTYPE] = adminUserEntity.UserType;
                        Session[AppConstant.CORPORATECONTACTS] = adminUserEntity.CorporateContacts;
                        return RedirectToAction("Dashboard", "Home");
                    }
                    else
                    {

                        HSGUserEntity hsgUserEntity = new HsgUsersBusiness().GetHSGUserDetailsByID(first);
                        List<FieldPermissionEntity> listFieldPermissionsEntity = new PermissionBusiness().GetFieldPermissionInformations("0");
                        CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
                        List<FormPermissionEntity> listFormPermissionsEntity = new List<FormPermissionEntity>();
                        CacheHelper.Add(listFormPermissionsEntity, CacheKeys.FormPermissions);
                        if (hsgUserEntity == null)
                        {

                            Session[AppConstant.USERNAME] = first;
                            Session[AppConstant.NAME] = first;
                            Session[AppConstant.USERTYPE] = Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser);
                            Session[AppConstant.FIELDUSER] = true;
                            return RedirectToAction("Dashboard", "Home");
                        }
                        else if (hsgUserEntity.Id > 0)
                        {

                            Session[AppConstant.USERID] = hsgUserEntity.Id;
                            Session[AppConstant.USERNAME] = first;
                            Session[AppConstant.NAME] = hsgUserEntity.FirstName + " " + hsgUserEntity.LastName;
                            Session[AppConstant.USERTYPE] = Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser);
                            Session[AppConstant.FIELDUSER] = true;
                            return RedirectToAction("Dashboard", "Home");
                        }
                        else
                        {

                            return View();
                        }
                    }
                }
                return View();
            }
            catch (Exception ex)
            {
                StoreError("SAML Error :" + ex.Message.ToString());
                throw new Exception(ex.Message.ToString());
            }


            //AccountSettings accountSettings = new AccountSettings();
            //if (Request.Form["SAMLResponse"] == null)
            //{
            //    AppSettings appsettings = new AppSettings();
            //    appsettings.assertionConsumerServiceUrl = Request.Url.OriginalString;
            //    OneLogin.Saml.AuthRequest req = new AuthRequest(appsettings, accountSettings);

            //    //Response.Redirect(accountSettings.idp_sso_target_url + "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64)));
            //    string reqstr = accountSettings.idp_sso_target_url + "?SAMLRequest=" + Server.UrlEncode(req.GetRequest(AuthRequest.AuthRequestFormat.Base64));
            //    //return View();
            //    HttpContext.Response.Redirect(reqstr);
            //    //return RedirectToAction("Index", "Home");
            //}
            //else
            //{


            //    //this means that user signed in thru onelogin
            //    //now need to check if onelogin response is valid
            //    OneLogin.Saml.Response samlResponse = new Response(accountSettings);
            //    samlResponse.LoadXmlFromBase64(Request.Form["SAMLResponse"]);

            //    if (samlResponse.IsValid())
            //    {
            //        //Response.Write("OK!");
            //        //Response.Write(samlResponse.GetNameID());
            //        string loginUser = samlResponse.GetNameID();
            //        if (loginUser.IndexOf('@') >= 0)
            //        {
            //            loginUser = loginUser.Substring(0, loginUser.IndexOf('@'));
            //        }
            //        Session[AppConstant.USERNAME] = loginUser;
            //        //create a cookie next
            //        HttpCookie cookieUser = new HttpCookie("loginuser", loginUser);

            //        //string[] uname = model.UserName.Split('@');
            //        string first = loginUser;
            //        AdminUserEntity adminUserEntity = new UserBusiness().AdminUserLogin(first);

            //        if (adminUserEntity != null && adminUserEntity.ID > 0)
            //        {
            //            if (adminUserEntity.IsLocked == true)
            //            {
            //                TempData["LoginError"] = "This user is locked.";
            //                return RedirectToAction("AdminLogin", "Login");
            //            }
            //            List<FieldPermissionEntity> listFieldPermissionsEntity = new PermissionBusiness().GetFieldPermissionInformations(adminUserEntity.GroupId);
            //            CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
            //            List<FormPermissionEntity> listFormPermissionsEntity = new PermissionBusiness().GetFormPermissionInformations(adminUserEntity.UserType, null, null);

            //            if (listFormPermissionsEntity.Count == 0)
            //            {
            //                Session[AppConstant.FIELDUSER] = true;
            //            }


            //            CacheHelper.Add(listFormPermissionsEntity, CacheKeys.FormPermissions);
            //            Session[AppConstant.listFormPermissionsEntity] = listFormPermissionsEntity;

            //            Session[AppConstant.GROUPID] = adminUserEntity.GroupId;
            //            Session[AppConstant.USERID] = adminUserEntity.ID;
            //            Session[AppConstant.USERNAME] = adminUserEntity.UserName;
            //            Session[AppConstant.ADMINUSEREMAIL] = adminUserEntity.Email;
            //            Session[AppConstant.NAME] = adminUserEntity.FirstName + " " + adminUserEntity.LastName;
            //            Session[AppConstant.USERTYPE] = adminUserEntity.UserType;
            //            Session[AppConstant.CORPORATECONTACTS] = adminUserEntity.CorporateContacts;
            //            return RedirectToAction("Dashboard", "Home");
            //        }
            //        else
            //        {

            //            HSGUserEntity hsgUserEntity = new HsgUsersBusiness().GetHSGUserDetailsByID(first);
            //            List<FieldPermissionEntity> listFieldPermissionsEntity = new PermissionBusiness().GetFieldPermissionInformations("0");
            //            CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
            //            List<FormPermissionEntity> listFormPermissionsEntity = new List<FormPermissionEntity>();
            //            CacheHelper.Add(listFormPermissionsEntity, CacheKeys.FormPermissions);
            //            if (hsgUserEntity == null)
            //            {

            //                Session[AppConstant.USERNAME] = first;
            //                Session[AppConstant.NAME] = first;
            //                Session[AppConstant.USERTYPE] = Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser);
            //                Session[AppConstant.FIELDUSER] = true;
            //                return RedirectToAction("Dashboard", "Home");
            //            }
            //            else if (hsgUserEntity.Id > 0)
            //            {

            //                Session[AppConstant.USERID] = hsgUserEntity.Id;
            //                Session[AppConstant.USERNAME] = first;
            //                Session[AppConstant.NAME] = hsgUserEntity.FirstName + " " + hsgUserEntity.LastName;
            //                Session[AppConstant.USERTYPE] = Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser);
            //                Session[AppConstant.FIELDUSER] = true;
            //                return RedirectToAction("Dashboard", "Home");
            //            }
            //            else
            //            {

            //                return View();
            //            }
            //        }


            //        //HttpContext.Current.Response.SetCookie(cookieUser);
            //        //return RedirectToAction("Dashboard", "Home");
            //    }
            //    else
            //    {
            //        Response.Redirect("error401.aspx");
            //    }

            //}
            //return View();
            
            //return RedirectToAction("Dashboard", "Home");
           
        }

        //[HttpPost]
        //public ActionResult AdminLogin(AdminLoginModel model)
        //{
        //    try
        //    {

        //        string authenticated = "";
        //        string message = "";
        //        string pass = MD5Helper.Utf8MD5Hex("M0bilehcsg!");
        //        var request = (HttpWebRequest)WebRequest.Create("https://api.onelogin.com/api/v1/delegated_auth?api_key=6cf13ab5f0584c06e692b8f4d02b9498a21485d0&email=" + model.UserName + "&password=" + model.Password + "");
        //        request.Method = "GET";
        //        var responseonelogin = (HttpWebResponse)request.GetResponse();
        //        var responseString = new StreamReader(responseonelogin.GetResponseStream()).ReadToEnd();
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(responseString);
        //        authenticated = doc.SelectNodes("/response/authenticated")[0].InnerText;
        //        message = doc.SelectNodes("/response/message")[0].InnerText;
        //        //authenticated = "true";
        //        if (authenticated == "true")
        //        {
                    
        //            string[] uname = model.UserName.Split('@');
        //            string first = uname[0];
        //            //AdminUserEntity adminUserEntity = new UserBusiness().AdminUserLogin(model.UserName, MD5Helper.Utf8MD5Hex(model.Password));
                    
        //            AdminUserEntity adminUserEntity = new UserBusiness().AdminUserLogin(first);
        //            //if (adminUserEntity != null)
        //            //{
        //            //    List<FieldPermissionEntity> listFieldPermissionsEntity = new FieldPermissionBusiness().GetFieldPermissionInformations(adminUserEntity.GroupId);
        //            //    CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
        //            //}
                    
        //            if (adminUserEntity != null && adminUserEntity.ID > 0)
        //            {
                    
        //                if (adminUserEntity.IsLocked == true)
        //                {
        //                    TempData["LoginError"] = "This user is locked.";
        //                    return RedirectToAction("AdminLogin", "Login");
        //                }
        //                List<FieldPermissionEntity> listFieldPermissionsEntity = new PermissionBusiness().GetFieldPermissionInformations(adminUserEntity.GroupId);
        //                CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
        //                List<FormPermissionEntity> listFormPermissionsEntity = new PermissionBusiness().GetFormPermissionInformations(adminUserEntity.UserType, null, null);

        //                if (listFormPermissionsEntity.Count == 0) {
        //                    Session[AppConstant.FIELDUSER] = true;
        //                }


        //                CacheHelper.Add(listFormPermissionsEntity, CacheKeys.FormPermissions);
        //                Session[AppConstant.listFormPermissionsEntity] = listFormPermissionsEntity;

        //                Session[AppConstant.GROUPID] = adminUserEntity.GroupId;
        //                Session[AppConstant.USERID] = adminUserEntity.ID;
        //                Session[AppConstant.USERNAME] = adminUserEntity.UserName;
        //                Session[AppConstant.ADMINUSEREMAIL] = adminUserEntity.Email;
        //                Session[AppConstant.NAME] = adminUserEntity.FirstName + " " + adminUserEntity.LastName;
        //                Session[AppConstant.USERTYPE] = adminUserEntity.UserType;
        //                Session[AppConstant.CORPORATECONTACTS] = adminUserEntity.CorporateContacts;
        //                return RedirectToAction("Dashboard", "Home");
        //            }
        //            else
        //            {
                        
        //                HSGUserEntity hsgUserEntity = new HsgUsersBusiness().GetHSGUserDetailsByID(first);
        //                List<FieldPermissionEntity> listFieldPermissionsEntity = new PermissionBusiness().GetFieldPermissionInformations("0");
                        
        //                CacheHelper.Add(listFieldPermissionsEntity, CacheKeys.FieldPermissions);
        //                List<FormPermissionEntity> listFormPermissionsEntity = new List<FormPermissionEntity>();
        //                CacheHelper.Add(listFormPermissionsEntity, CacheKeys.FormPermissions);
        //                if (hsgUserEntity == null)
        //                {
                            
        //                    Session[AppConstant.USERNAME] = first;
        //                    Session[AppConstant.NAME] = first;
        //                    Session[AppConstant.USERTYPE] = Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser);
        //                    Session[AppConstant.FIELDUSER] = true;
        //                    return RedirectToAction("Dashboard", "Home");
        //                    //TempData["LoginError"] = "User or Password is incorrect.";
        //                    //return RedirectToAction("AdminLogin", "Login"); 
        //                }
        //                else if (hsgUserEntity.Id > 0)
        //                {
                            
        //                    Session[AppConstant.USERID] = hsgUserEntity.Id;
        //                    Session[AppConstant.USERNAME] = first;
        //                    Session[AppConstant.NAME] = hsgUserEntity.FirstName + " " + hsgUserEntity.LastName;
        //                    //string result = new UserBusiness().CheckAdminUserInvalidAttamptUpdate(model.UserName, ConfigurationProvider.InvalidAttamptCounter);
        //                    //ViewBag.ErrorMessage = "Invalid Login!!! \nEmployee ID is located on your Paystub \nHaving trouble? Please contact 1-800-363-4274";
        //                    //ViewBag.LockedErrorMessage = result;
        //                    Session[AppConstant.USERTYPE] = Convert.ToString(EmployeeDirectory.Entity.UserType.HSGUser);
        //                    Session[AppConstant.FIELDUSER] = true;
        //                    return RedirectToAction("Dashboard", "Home");
        //                }
        //                else
        //                {
                            
        //                    return View();
        //                }
        //                //return View(model);
        //                //return RedirectToAction("AdminLogin", "Login");
        //            }
        //        }
        //        else
        //        {
        //            //ViewBag.ErrorMessage = doc.SelectNodes("/response/message")[0].InnerText;
        //            return View();
        //        }
        //        //AdminUserEntity adminUserEntity = new UserBusiness().AdminUserLogin(model.UserName, MD5Helper.Utf8MD5Hex(model.Password));

        //    }
        //    catch (Exception ex)
        //    {
        //        StoreError(ex.Message.ToString());
        //        throw new Exception("DAL Exception " + ex);
        //    }
          
        //}

        public int StoreError(string errorMSG)
        {
            
            //DbHelper.ConnectionString = "Data Source=HSG-RPT;Initial Catalog=HSGEmployeeDirectory;User ID=eduser;Password=Rep0rting!";
            DbHelper.ConnectionString = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ConnectionString;
            DbParameter[] parameter = new DbParameter[1];
            parameter[0] = DbHelper.CreateParameter("@ErrorMSg", DbType.String, errorMSG);
            int result = DbHelper.ExecuteNonQuery(CommandType.StoredProcedure, "InsertError", parameter);
            return result;

        }

        public ActionResult AdminSignOut()
        {
            HttpRuntime.Cache.Remove(Convert.ToString(Session[AppConstant.USERID]));
            Session.Clear();
            Session.Abandon();
            //return RedirectToAction("AdminLogin");
            return Redirect("https://hcsgcorp.onelogin.com");
        }

    }
}
