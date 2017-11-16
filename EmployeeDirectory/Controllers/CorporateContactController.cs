using DevExpress.Web.Mvc;
using EmployeeDirectory.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
    public class CorporateContactController : Controller
    {
        public ActionResult CC()
        {
            return View();
        }


        EmployeeDirectory.Models.HSGEmployeeDirectoryEntities db = new EmployeeDirectory.Models.HSGEmployeeDirectoryEntities();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.CorporateContacts;

            var Department = db.CorporateContacts

                            .Select(x => new { Department = x.Department })
                            .OrderBy(x => x.Department)
                            .AsEnumerable()
                            .ToList();
            ViewBag.Department = Department.Distinct().ToList();

            var Office = db.CorporateContacts
                .Select(x => new { Office = x.Office })
                .OrderBy(x => x.Office)
                                .AsEnumerable()
                                .ToList();
            ViewBag.Office = Office.Distinct().ToList();

            return PartialView("_GridViewPartial", model.ToList());
        }

        public ActionResult ExportTo()
        {
            var model = db.CorporateContacts;


            return GridViewExtension.ExportToXlsx(GetGridSettings(), model.ToList());
        }

        // Returns the settings of the exported GridView. 
        private GridViewSettings GetGridSettings()
        {

            var permission = EmployeeDirectory.Helper.CommonMethods.CheckPermission(EmployeeDirectory.Helper.StringEnum.GetStringValue(EmployeeDirectory.Helper.ScreenAlias.CorporateContact));
            if (permission != null)
            {
                ViewBag.IsAdd = EmployeeDirectory.Helper.CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = EmployeeDirectory.Helper.CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = EmployeeDirectory.Helper.CommonMethods.GetFormPermission(permission).IsDelete;
            }

            var settings = new GridViewSettings();
            settings.Name = "GridView";
            settings.CallbackRouteValues = new { Controller = "CorporateContact", Action = "GridViewPartial" };

            // Export-specific settings  
            settings.SettingsExport.ExportSelectedRowsOnly = false;
            settings.SettingsExport.FileName = "Export.xlsx";
            //settings.SettingsExport.PaperKind = System.Drawing.Printing.PaperKind.A4;

            settings.KeyFieldName = "id";
            settings.Columns.Add("FirstName");
            settings.Columns.Add("LastName");
            settings.Columns.Add("Department");
            settings.Columns.Add("JobTitle");
            settings.Columns.Add("BusinessPhone");
            settings.Columns.Add("FaxNumber");
            settings.Columns.Add("Extension");
            settings.Columns.Add("EmailAddress");
            settings.Columns.Add("Office");
            if (Convert.ToBoolean(ViewBag.IsAdd) == true)
            {
                settings.Columns.Add("StartDate");
                settings.Columns.Add("AccessType");
                settings.Columns.Add("Supervisor");
                settings.Columns.Add("employee_id");
                settings.Columns.Add("TermDate");
            }
            return settings;
        }


        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew([ModelBinder(typeof(DevExpressEditorsBinder))] EmployeeDirectory.Models.CorporateContact item)
        {

            var fromAddress = new MailAddress("helpdesk@hcsgcorp.com", "New Hire Form");
            MailAddressCollection TO_addressList = new MailAddressCollection();
            string mailto = "helpdesk@hcsgcorp.com;bmilsop@hcsgcorp.com;MGriffin@hcsgcorp.com;DFrye@hcsgcorp.com";
            //3.Prepare the Destination email Addresses list
            MailAddress toAddress = null;
            foreach (var curr_address in mailto.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
            {
                toAddress = new MailAddress(curr_address, "Helpdesk");
                TO_addressList.Add(toAddress);
            }
            //var toAddress = new MailAddress("helpdesk@hcsgcorp.com;bmilsop@hcsgcorp.com;MGriffin@hcsgcorp.com;DFrye@hcsgcorp.com", "Helpdesk");

            var smtp = new SmtpClient
            {
                Host = "206.206.206.11",
                Port = 587,
                // EnableSsl = true,
                //   DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,

                //Credentials = new NetworkCredential(fromAddress.Address, fromPassword),
                Timeout = 20000

            };
            using (var message = new MailMessage(fromAddress, toAddress)
            {
                Subject = "Corporate New Hire " + Request.Form["FirstName"] + " " + Request.Form["LastName"],
                Body = "Name: " + Request.Form["FirstName"] + " " + Request.Form["LastName"] + "<br> Department: " + Request.Form["Department"] + "<br> Job Title: " + Request.Form["JobTitle"] + "<br>Supervisor: " + Request.Form["Supervisor"],
                IsBodyHtml = true
            })
            {
                //            ServicePointManager.ServerCertificateValidationCallback =
                //delegate (object s, X509Certificate certificate,
                //         X509Chain chain, SslPolicyErrors sslPolicyErrors)
                //{ return true; };

                //smtp.Send(message);
            }


            var model = db.CorporateContacts;
            if (ModelState.IsValid)
            {
                try
                {
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate([ModelBinder(typeof(DevExpressEditorsBinder))] EmployeeDirectory.Models.CorporateContact item)
        {
            var model = db.CorporateContacts;
            if (ModelState.IsValid)
            {
                try
                {
                    var modelItem = model.FirstOrDefault(it => it.id == item.id);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete([ModelBinder(typeof(DevExpressEditorsBinder))] Int32 id)
        {
            var model = db.CorporateContacts;
            if (id >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.id == id);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model.ToList());
        }

    }
}
