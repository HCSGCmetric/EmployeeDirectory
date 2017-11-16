using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
    public class LinkController : BaseController
    {

         [Authorization(ScreenAlias.Link, Rights.View, true)]
        public ActionResult LinkList()
        {
            if (!SessionManager.IsFieldUser)
            {
                LinkEntity objLinkEntity = new LinkEntity();
                LinkBusiness objLinkBusiness = new LinkBusiness();
                var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
                if (permission != null)
                {
                    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
                }

                int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
                List<LinkEntity> list = objLinkBusiness.GetAllLinkInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objLinkEntity);
                
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
                }

                return View(list);
            }
            else
            {
                return RedirectToAction("ViewLinkList");
            }
        }

         public ActionResult LinkListPagingSortingList(string LinkName, string pagesize, string pageindex, string columnName, string sortorder)
         {
             LinkEntity objLinkEntity = new LinkEntity();
             
             var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
             if (permission != null)
             {
                 ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                 ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                 ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
             }

             HsgUsersBusiness hsgUser = new HsgUsersBusiness();
             if (string.IsNullOrEmpty(pageindex))
             {
                 pageindex = "1";
             }
             if (string.IsNullOrEmpty(pagesize))
             {
                 pagesize = "50";
             }

             int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(Convert.ToInt32(pageindex), int.Parse(pagesize));
             objLinkEntity.Name = LinkName;
            
             //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
             LinkBusiness objLinkBusiness = new LinkBusiness();
             List<LinkEntity> list = objLinkBusiness.GetAllLinkInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objLinkEntity);
             for (int i = 0; i < list.Count; i++)
             {
                 list[i].Pageindex = Convert.ToInt32(pageindex);
             }

             ViewBag.PageSize = int.Parse(pagesize);
             return PartialView("_LinkList", list);
         }

        public ActionResult AddLink()
        {
            if (!SessionManager.IsFieldUser)
            {
                LinkEntity objLinkEntity = new LinkEntity();
                List<SelectListItem> idHeadTypeList = new List<SelectListItem>();
                List<SelectListItem> idLinkDisplayList = new List<SelectListItem>();
                List<SelectListItem> idDepartmentList = new List<SelectListItem>();

                List<IdNameEntity> lstHeadtype = new List<IdNameEntity>();
                List<DocumentDisplayEntity> lstDocumentDisplay = new List<DocumentDisplayEntity>();
                

                UserBusiness objUserBusiness = new UserBusiness();
                lstHeadtype = objUserBusiness.GetAllHeadTypeList();
                DocumentDisplayBusiness objDocumentDisplayBusiness = new DocumentDisplayBusiness();
                lstDocumentDisplay = objDocumentDisplayBusiness.GetAllDocumentDisplayList("IsDisplayLink");
                
                idLinkDisplayList.Add(new SelectListItem() { Text = "select", Value = "0" });
                idDepartmentList.Add(new SelectListItem() { Text = "select", Value = "0" });

                foreach (IdNameEntity itemIdNameEntity in lstHeadtype)
                {
                    idHeadTypeList.Add(new SelectListItem() { Text = itemIdNameEntity.Name, Value = itemIdNameEntity.Name });
                }
                foreach (DocumentDisplayEntity itemLinkDisplayEntity in lstDocumentDisplay)
                {
                    idLinkDisplayList.Add(new SelectListItem() { Text = itemLinkDisplayEntity.Name, Value = Convert.ToString(itemLinkDisplayEntity.ID) });
                }

                ViewBag.HeadTypesList = idHeadTypeList;
                ViewBag.LinkDisplayList = idLinkDisplayList;
                ViewBag.DepartMentList = idDepartmentList;
                return View(objLinkEntity);
            }
            else
            {
                return RedirectToAction("ViewLinkList");
            }
        }

        [HttpPost]
        public ActionResult AddLink(LinkEntity model)
        {
            model.CreatedBy = SessionManager.UserID;
            model.CreatedDate = DateTime.Now;
            model.IsActive = true;
            model.IsCommonForAll = true;
            int result = new LinkBusiness().SaveLink(model);

            return RedirectToAction("LinkList");
            //ViewBag.status = "An Error occured while processing your request.";
        }

        public ActionResult EditLink(int Id)
        {
            if (!SessionManager.IsFieldUser)
            {
                LinkEntity objLinkEntity = new LinkEntity();
                List<SelectListItem> idHeadTypeList = new List<SelectListItem>();
                List<SelectListItem> idLinkDisplayList = new List<SelectListItem>();
                List<SelectListItem> idDepartmentList = new List<SelectListItem>();

                List<IdNameEntity> lstHeadtype = new List<IdNameEntity>();
                List<DocumentDisplayEntity> lstDocumentDisplay = new List<DocumentDisplayEntity>();


                UserBusiness objUserBusiness = new UserBusiness();
                lstHeadtype = objUserBusiness.GetAllHeadTypeList();
                DocumentDisplayBusiness objDocumentDisplayBusiness = new DocumentDisplayBusiness();
                LinkBusiness objLinkBusiness = new LinkBusiness();

                
                lstDocumentDisplay = objDocumentDisplayBusiness.GetAllDocumentDisplayList("IsDisplayLink");

                idLinkDisplayList.Add(new SelectListItem() { Text = "select", Value = "0" });
                idDepartmentList.Add(new SelectListItem() { Text = "select", Value = "0" });

                foreach (IdNameEntity itemIdNameEntity in lstHeadtype)
                {
                    idHeadTypeList.Add(new SelectListItem() { Text = itemIdNameEntity.Name, Value = itemIdNameEntity.Name });
                }
                foreach (DocumentDisplayEntity itemLinkDisplayEntity in lstDocumentDisplay)
                {
                    idLinkDisplayList.Add(new SelectListItem() { Text = Convert.ToString(itemLinkDisplayEntity.Name), Value = Convert.ToString(itemLinkDisplayEntity.ID) });
                }
               

                objLinkEntity = objLinkBusiness.getLinkDetailById(Id);

                ViewBag.HeadTypesList = idHeadTypeList;
                ViewBag.LinkDisplayList = idLinkDisplayList;
                ViewBag.DepartMentList = idDepartmentList;


                return View(objLinkEntity);
            }
            else
            {
                return RedirectToAction("ViewLinkList");
            }
        }

        [HttpPost]
        public ActionResult EditLink(LinkEntity model)
        {
            model.UpdatedBy = SessionManager.UserID;
            model.IsCommonForAll = true;
            int result = new LinkBusiness().UpdateLink(model);

            return RedirectToAction("LinkList");
            //ViewBag.status = "An Error occured while processing your request.";
        }

        public ActionResult DeleteLink(int Id)
        {
            if (!SessionManager.IsFieldUser)
            {
                try
                {
                    LinkBusiness objLinkBusiness = new LinkBusiness();
                    bool result = objLinkBusiness.DeleteLink(Id);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occured while processing your request. Please try again later";
                }
                return RedirectToAction("LinkList");
            }
            else
            {
                return RedirectToAction("ViewLinkList");
            }
        }

        public ActionResult ViewLinkList(string SearchValue = "")
        {

            LinkEntity objLinkEntity = new LinkEntity();
            LinkBusiness objLinkBusiness = new LinkBusiness();
            ViewBag.SearchValue = SearchValue;
            objLinkEntity.lstLinkEntity = objLinkBusiness.GetLinkList(SessionManager.IsFieldUser, SessionManager.UserName, SearchValue);
            return View(objLinkEntity);

        }

        public ActionResult DeleteMultipleLink(string IDS)
        {
            int result = 0;
            if (!SessionManager.IsFieldUser)
            {
                try
                {
                    LinkBusiness objLinkBusiness = new LinkBusiness();
                    result = objLinkBusiness.DeleteMultipleLink(IDS);
                }
                catch (Exception ex)
                {
                    TempData["Error"] = "An error occured while processing your request. Please try again later";
                }
                return Content(result.ToString());
            }
            else
            {
                return Content(result.ToString());
            }
        }

    }
}
