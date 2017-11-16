using EmployeeDirectory.Business;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Web;
using System.Web.Mvc;

namespace EmployeeDirectory.Controllers
{
    public class AssetManagementController : BaseController
    {
        //
        // GET: /AssetManagement/

        public ActionResult AddItemDetail()
        {

            AssetEntity objAssetEntity = new AssetEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idItemCategoryList = new List<SelectListItem>();
            List<IdNameEntity> ItemCategoryList = new List<IdNameEntity>();
            idItemCategoryList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            ItemCategoryList = objAssetBusiness.GetItemCategoryList();

            foreach (IdNameEntity objIdNameEntity in ItemCategoryList)
            {
                idItemCategoryList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            ViewBag.ItemCategoryList = idItemCategoryList;
            return PartialView("_AddItemDetail", objAssetEntity);
        }

        [HttpPost]
        public ActionResult SaveAssetItem(AssetEntity objAssetEntity)
        {
            int ans = 0;
            try
            {
                objAssetEntity.UserId = Convert.ToInt32(Session[AppConstant.USERID]);
                AssetBusiness objAssetBusiness = new AssetBusiness();
                ans = objAssetBusiness.SaveAssetItem(objAssetEntity);

                return Content(ans.ToString());

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
                throw;
            }
            // return Content(ans.ToString());
        }

        [HttpPost]
        public ActionResult SaveAssetVendor(AssetEntity objAssetEntity)
        {
            int ans = 0;
            try
            {
                objAssetEntity.UserId = Convert.ToInt32(Session[AppConstant.USERID]);
                AssetBusiness objAssetBusiness = new AssetBusiness();
                ans = objAssetBusiness.SaveAssetVendor(objAssetEntity);
                if (ans > 0)
                {
                    return Content("1");

                }
                else
                {
                    return Content("-1");
                }

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
                throw;
            }
            // return Content(ans.ToString());
        }

        #region Purchase Order
        [Authorization(ScreenAlias.AssetPurchaseOrder, Rights.View, true)]
        public ActionResult AssetPurchageOrderList()
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.AssetPurchaseOrder));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }

            AssetEntity objAssetEntity = new AssetEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<IdNameEntity> VendorList = new List<IdNameEntity>();
            //objAssetEntity.lstAssetEntity = objAssetBusiness.GetPurchageOrderList();
            List<SelectListItem> idVendorList = new List<SelectListItem>();
            idVendorList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            VendorList = objAssetBusiness.GetPoVendorList();
            foreach (IdNameEntity objIdNameEntity in VendorList)
            {
                idVendorList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<AssetEntity> list = objAssetBusiness.GetAllPurchaseOrderInformation("", paggingArray[0], paggingArray[1], "", "", objAssetEntity, "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            objAssetEntity.lstAssetEntity = list;

            ViewBag.VendorDetailList = idVendorList;
            return View(objAssetEntity);
        }

        [Authorization(ScreenAlias.AssetPurchaseOrder, Rights.Add, true)]
        public ActionResult AddPurchaseOrder()
        {

            AssetEntity objAssetEntity = new AssetEntity();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idVendorList = new List<SelectListItem>();
            List<SelectListItem> idItemCategoryList = new List<SelectListItem>();
            List<IdNameEntity> VendorList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            List<IdNameEntity> ItemCategoryList = new List<IdNameEntity>();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            objAssetEntity.lstAssetEntity = new List<AssetEntity>();
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            idItemCategoryList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });

            idVendorList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });

            NameList = objAssetBusiness.getItemNameList();
            VendorList = objAssetBusiness.GetPoVendorList();
            ItemCategoryList = objAssetBusiness.GetItemCategoryList();

            objAssetEntity.PONumber = objAssetBusiness.getNextPONumber();

            foreach (IdNameEntity objIdNameEntity in VendorList)
            {
                idVendorList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            foreach (IdNameEntity objIdNameEntity in ItemCategoryList)
            {
                idItemCategoryList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            ViewBag.VendorDetailList = idVendorList;
            ViewBag.ItemDetailList = idNameList;
            ViewBag.ItemCategoryList = idItemCategoryList;
            return View(objAssetEntity);


        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveAssetPurchaseOrder(AssetEntity objAssetEntity)
        {
            int ans = 0;
            try
            {
                objAssetEntity.UserId = Convert.ToInt32(Session[AppConstant.USERID]);
                AssetBusiness objAssetBusiness = new AssetBusiness();
                ans = objAssetBusiness.SaveAssetPurchaseOrder(objAssetEntity);
                if (ans > 0)
                {
                    return Content("1");

                }
                else
                {
                    return Content("-1");
                }

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
                throw;
            }
            // return Content(ans.ToString());
        }

        [Authorization(ScreenAlias.AssetPurchaseOrder, Rights.Edit, true)]
        public ActionResult EditPurchageOrder(int Id)
        {

            AssetEntity objAssetEntity = new AssetEntity();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idVendorList = new List<SelectListItem>();
            List<IdNameEntity> VendorList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            List<IdNameEntity> ItemCategoryList = new List<IdNameEntity>();
            List<SelectListItem> idItemCategoryList = new List<SelectListItem>();

            AssetBusiness objAssetBusiness = new AssetBusiness();
            objAssetEntity.lstAssetEntity = new List<AssetEntity>();
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0" });
            idItemCategoryList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            idVendorList.Add(new SelectListItem() { Text = "select", Value = "" });

            ItemCategoryList = objAssetBusiness.GetItemCategoryList();
            VendorList = objAssetBusiness.GetPoVendorList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in VendorList)
            {
                idVendorList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            foreach (IdNameEntity objIdNameEntity in ItemCategoryList)
            {
                idItemCategoryList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            ViewBag.VendorDetailList = idVendorList;
            ViewBag.ItemDetailList = idNameList;
            ViewBag.ItemCategoryList = idItemCategoryList;

            objAssetEntity = objAssetBusiness.GetPurchageOrderDetailById(Id);
            objAssetEntity.lstAssetEntity = objAssetBusiness.GetPurchageOrderItemDetailByPO(objAssetEntity.PONumber);

            return View(objAssetEntity);
        }

        public ActionResult PurchaseOrderPagingSortingList(string PONumber, string PoFromDate, string PoToDate, int Vendorid, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetEntity objAssetEntity = new AssetEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.PONumber = PONumber;
            objAssetEntity.POVendorID = Vendorid;


            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetEntity> list = objAssetBusiness.GetAllPurchaseOrderInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, PoFromDate, PoToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetPurchageOrderList", list);
        }

        #endregion

        #region Assign Master


        [Authorization(ScreenAlias.AssetAssign, Rights.View, true)]
        public ActionResult AssetAssignList()
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            PONumberList = objAssetBusiness.getPONumberList();
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            //objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetAssetAssignList(objAssetAssignEntity);

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetAssignInformation("", paggingArray[0], paggingArray[1], "", "", objAssetAssignEntity, "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            objAssetAssignEntity.lstAssetAssignEntity = list;
            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;

            return View(objAssetAssignEntity);
        }

        [Authorization(ScreenAlias.AssetAssign, Rights.Add, true)]
        public ActionResult AddAssetAssign()
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            AssetTagList = objAssetBusiness.getAssetTagList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;

            return View(objAssetAssignEntity);
        }

        public ActionResult ViewAssetAssign(string PONumber, int ItemId, string ItemName, int POID, int AddWarrantyMonth, string TagNo)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idReAssignTagList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> ReAssignTagList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            AssetTagList = objAssetBusiness.getAssetTagList();

            ReAssignTagList = objAssetBusiness.GetReAssignAssetTagList(PONumber, ItemId);
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in ReAssignTagList)
            {
                idReAssignTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            objAssetAssignEntity.ItemId = ItemId;
            objAssetAssignEntity.ItemName = ItemName;
            objAssetAssignEntity.PONumber = PONumber;
            objAssetAssignEntity.POId = POID;
            objAssetAssignEntity.AssetTagNumber = TagNo;
            objAssetAssignEntity.AdditionalWarrantyMonth = AddWarrantyMonth;

            if (objAssetAssignEntity.AssetTagNumber != "")
            {
                objAssetAssignEntity.ReAssign = true;
            }
            var AvailAssignQty = objAssetBusiness.GetAvailableAssetIsNewAssignOrReAssign(objAssetAssignEntity);

            if (AvailAssignQty > 0)
            {
                ViewBag.AssetTagListNew = idAssetTagList;
            }
            else
            {
                objAssetAssignEntity.ReAssign = true;
                ViewBag.AvailAssignAssetQty = 0;
            }


            ViewBag.AvailAssignAssetQty = AvailAssignQty;
            ViewBag.AssetTagListReassign = idReAssignTagList;

            ViewBag.ItemDetailList = idNameList;

            return PartialView("_AddAssetAssign", objAssetAssignEntity);
        }

        public ActionResult GetPODetailByItemId(int ItemId)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetPODetailByItemId(ItemId);
            return PartialView("_PODetailView", objAssetAssignEntity);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveAssetAssignOrder(AssetAssignEntity objAssetAssignEntity)
        {
            int ans = 0;
            try
            {
                objAssetAssignEntity.UserId = Convert.ToInt32(Session[AppConstant.USERID]);
                AssetBusiness objAssetBusiness = new AssetBusiness();

                var AssetTagList = objAssetBusiness.getAssetTagList();

                var ReAssignTagList = objAssetBusiness.GetReAssignAssetTagList(objAssetAssignEntity.PONumber, objAssetAssignEntity.ItemId);



                ans = objAssetBusiness.SaveAssetAssignOrder(objAssetAssignEntity);

                return Content(ans.ToString());

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
                throw;
            }
            // return Content(ans.ToString());
        }


        [HttpPost]
        [ValidateInput(false)]
        public ActionResult SaveAssetReAssignOrder(AssetAssignEntity objAssetAssignEntity)
        {
            int ans = 0;
            try
            {
                objAssetAssignEntity.UserId = Convert.ToInt32(Session[AppConstant.USERID]);
                AssetBusiness objAssetBusiness = new AssetBusiness();

                var AssetTagList = objAssetBusiness.getAssetTagList();

                var ReAssignTagList = objAssetBusiness.GetReAssignAssetTagList(objAssetAssignEntity.PONumber, objAssetAssignEntity.ItemId);

                ans = objAssetBusiness.SaveAssetReAssignOrder(objAssetAssignEntity);

                return Content(ans.ToString());

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
                throw;
            }
            // return Content(ans.ToString());
        }

        public ActionResult AssetAssignPagingSortingList(string TagNo, int ItemId, string PONumber, string EEID, string FacilityCode, string AssetFromDate, string AssetToDate, string DIV, string REG, string DIST, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            if (string.IsNullOrEmpty(pageindex))
            {
                pageindex = "1";
            }
            if (string.IsNullOrEmpty(pagesize))
            {
                pagesize = "20";
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(Convert.ToInt32(pageindex), int.Parse(pagesize));
            objAssetEntity.AssetTagNumber = TagNo;
            objAssetEntity.ItemId = ItemId;
            objAssetEntity.PONumber = PONumber;
            objAssetEntity.EmployeeId = EEID;
            objAssetEntity.FacilityCode = FacilityCode;
            objAssetEntity.DIV = DIV;
            objAssetEntity.REG = REG;
            objAssetEntity.DIST = DIST;




            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetAssignInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, AssetFromDate, AssetToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetAssignList", list);
        }

        public ActionResult ViewAssetItemDetailByItemId(int ItemId)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            objAssetAssignEntity = objAssetBusiness.GetAssetItemDetailByItemId(ItemId);
            return PartialView("_ViewAssetItemDetail", objAssetAssignEntity);
        }

        [Authorization(ScreenAlias.AssetAssign, Rights.Delete, true)]
        public ActionResult DeleteAssetItem(int Id)
        {
            int ans = 0;
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            ans = objAssetBusiness.DeleteAssetItem(Id);
            return Content(ans.ToString());

        }

        #endregion

        #region Return Master

        [Authorization(ScreenAlias.AssetReturn, Rights.View, true)]
        public ActionResult AssetReturnList()
        {
            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.GroupMaster));
            if (permission != null)
            {
                ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            }
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            PONumberList = objAssetBusiness.getPONumberList();

            //objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetAssetReturnList(objAssetAssignEntity);
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetReturnInformation("", paggingArray[0], paggingArray[1], "", "", objAssetAssignEntity, "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }
            objAssetAssignEntity.lstAssetAssignEntity = list;

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;
            return View(objAssetAssignEntity);

        }

        [Authorization(ScreenAlias.AssetReturn, Rights.Add, true)]
        public ActionResult AddAssetReturn()
        {

            AssetAssignEntity objAssetEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            return View(objAssetEntity);

        }

        public ActionResult getAssetReturnDetailByTagNo(string TagNo)
        {

            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idStatusList = new List<SelectListItem>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            List<IdNameEntity> StatusList = new List<IdNameEntity>();
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            idStatusList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            NameList = objAssetBusiness.getItemNameList();
            StatusList = objAssetBusiness.getItemStatusList();
            foreach (IdNameEntity objIdNameEntity in NameList)
            {

                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });

            }
            foreach (IdNameEntity objIdNameEntity in StatusList)
            {
                if (objIdNameEntity.Name == "Request")
                {
                    idStatusList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
                }
            }

            ViewBag.ItemStatusList = idStatusList;
            ViewBag.ItemDetailList = idNameList;


            objAssetAssignEntity = objAssetBusiness.getItemDetailByTagNo(TagNo);
            //if (objAssetAssignEntity != null)
            //{ return Json(objAssetAssignEntity, JsonRequestBehavior.AllowGet); }
            //else
            //{
            //    return Content("");
            //}
            if (objAssetAssignEntity == null)
            {
                return Content("");
            }
            else if (objAssetAssignEntity.IsCurrentSystem == true)
            {
                return PartialView("_AddAssetReturnCurrentSystem", objAssetAssignEntity);
            }
            else
            {
                return PartialView("_AddAssetReturn", objAssetAssignEntity);
            }

        }

        [HttpPost]
        public ActionResult SaveAssetReturnOrder(AssetAssignEntity objAssetAssignEntity)
        {
            int ans = 0;
            try
            {
                objAssetAssignEntity.UserId = Convert.ToInt32(Session[AppConstant.USERID]);
                AssetBusiness objAssetBusiness = new AssetBusiness();

                if (objAssetAssignEntity.IsCurrentSystem == true)
                {
                    ans = objAssetBusiness.SaveAssetReturnOrderForCurrentSystem(objAssetAssignEntity);

                }
                else
                {
                    ans = objAssetBusiness.SaveAssetReturnOrder(objAssetAssignEntity);
                }

                return Content(ans.ToString());

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
                throw;
            }
            // return Content(ans.ToString());
        }

        public ActionResult AssetReturnPagingSortingList(string TagNo, int ItemId, string ReturnFromDate, string ReturnToDate, string PONumber, string EEID, string FacilityCode, string DIV, string REG, string DIST, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.AssetTagNumber = TagNo;
            objAssetEntity.ItemId = ItemId;

            objAssetEntity.PONumber = PONumber;
            objAssetEntity.EmployeeId = EEID;
            objAssetEntity.FacilityCode = FacilityCode;
            objAssetEntity.DIV = DIV;
            objAssetEntity.REG = REG;
            objAssetEntity.DIST = DIST;





            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetReturnInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, ReturnFromDate, ReturnToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetReturnList", list);
        }

        public ActionResult getReturnItemDetailByTagNo(string TagNo)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            objAssetAssignEntity = objAssetBusiness.getReturnItemDetailByTagNo(TagNo);
            if (objAssetAssignEntity != null)
            { return Json(objAssetAssignEntity, JsonRequestBehavior.AllowGet); }
            else
            {
                return Content("");
            }

        }

        public ActionResult GetItemHistoryByTagNo(string TagNo)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetItemHistoryByTagNo(TagNo);
            objAssetAssignEntity.lstReturnItem = objAssetBusiness.GetReturnItemByTagNo(TagNo);

            return PartialView("_ViewItemHistory", objAssetAssignEntity);
        }

        [Authorization(ScreenAlias.AssetReturn, Rights.Edit, true)]
        public ActionResult EditAssetReturn(int Id)
        {

            AssetAssignEntity objAssetEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idStatusList = new List<SelectListItem>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            List<IdNameEntity> StatusList = new List<IdNameEntity>();
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            idStatusList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            NameList = objAssetBusiness.getItemNameList();
            StatusList = objAssetBusiness.getItemStatusList();
            objAssetEntity = objAssetBusiness.GetAssetReturnDetailById(Id);
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            foreach (IdNameEntity objIdNameEntity in StatusList)
            {
                idStatusList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
                //if (objAssetEntity.ItemStatus == "Received" && objIdNameEntity.Name == "Resolved")
                //{
                //    idStatusList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
                //}
                //else if (objAssetEntity.ItemStatus == "Request" && objIdNameEntity.Name == "Received")
                //{
                //    idStatusList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
                //}
            }

            ViewBag.ItemStatusList = idStatusList;
            ViewBag.ItemDetailList = idNameList;
            return View(objAssetEntity);

        }

        #endregion

        #region Retire Master

        [Authorization(ScreenAlias.AssetRetire, Rights.View, true)]
        public ActionResult AssetRetireList()
        {

            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();

            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            PONumberList = objAssetBusiness.getPONumberList();
            //objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetAssetRetireList(objAssetAssignEntity);
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }

            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetRetireInformation("", paggingArray[0], paggingArray[1], "", "", objAssetAssignEntity, "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            objAssetAssignEntity.lstAssetAssignEntity = list;

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;
            return View(objAssetAssignEntity);

        }

        [Authorization(ScreenAlias.AssetRetire, Rights.Add, true)]
        public ActionResult AddAssetRetire()
        {

            AssetAssignEntity objAssetEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idStatusList = new List<SelectListItem>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            List<IdNameEntity> StatusList = new List<IdNameEntity>();
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            idStatusList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            NameList = objAssetBusiness.getItemNameList();
            StatusList = objAssetBusiness.getItemStatusList();
            foreach (IdNameEntity objIdNameEntity in NameList)
            {

                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });

            }
            foreach (IdNameEntity objIdNameEntity in StatusList)
            {
                if (objIdNameEntity.Name == "Request")
                {
                    idStatusList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
                }
            }

            ViewBag.ItemStatusList = idStatusList;
            ViewBag.ItemDetailList = idNameList;
            return View(objAssetEntity);

        }

        public ActionResult getItemDetailByTagNoForRetire(string TagNo)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            objAssetAssignEntity = objAssetBusiness.getItemDetailByTagNoForRetire(TagNo);
            if (objAssetAssignEntity != null)
            { return Json(objAssetAssignEntity, JsonRequestBehavior.AllowGet); }
            else
            {
                return Content("");
            }

        }

        [HttpPost]
        public ActionResult SaveAssetRetireOrder(AssetAssignEntity objAssetAssignEntity)
        {
            int ans = 0;
            try
            {
                objAssetAssignEntity.UserId = Convert.ToInt32(Session[AppConstant.USERID]);
                AssetBusiness objAssetBusiness = new AssetBusiness();
                ans = objAssetBusiness.SaveAssetRetireOrder(objAssetAssignEntity);

                return Content(ans.ToString());

            }
            catch (Exception ex)
            {
                TempData["Error"] = "An error occured while processing your request. Please try again later";
                throw;
            }
            // return Content(ans.ToString());
        }

        public ActionResult AssetRetirePagingSortingList(string TagNo, int ItemId, string RetireFromDate, string RetireToDate, string PONumber, string EEID, string FacilityCode, string DIV, string REG, string DIST, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.AssetTagNumber = TagNo;
            objAssetEntity.ItemId = ItemId;

            objAssetEntity.PONumber = PONumber;
            objAssetEntity.EmployeeId = EEID;
            objAssetEntity.FacilityCode = FacilityCode;
            objAssetEntity.DIV = DIV;
            objAssetEntity.REG = REG;
            objAssetEntity.DIST = DIST;


            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetRetireInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, RetireFromDate, RetireToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetRetireList", list);
        }

        public ActionResult AddAssetTagDetail(string TagNo)
        {
            int ans = 0;
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();

            ans = objAssetBusiness.AddAssetTagDetail(TagNo);

            return Content(ans.ToString());

        }

        #endregion

        #region Stock Master

        [Authorization(ScreenAlias.AssetManagement, Rights.View, true)]
        public ActionResult AssetStockMaster()
        {
            AssetEntity objAssetEntity = new AssetEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();

            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            PONumberList = objAssetBusiness.getPONumberList();
            objAssetEntity.lstAssetEntity = objAssetBusiness.GetAssetStockList();
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;
            return View(objAssetEntity);

        }

        public ActionResult AssetStockPagingSortingList(string ItemId, string PONumber, string StockFromDate, string StockToDate)
        {
            AssetEntity objAssetEntity = new AssetEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

            HsgUsersBusiness hsgUser = new HsgUsersBusiness();
            //if (string.IsNullOrEmpty(pageindex))
            //{
            //    pageindex = "1";
            //}
            //if (string.IsNullOrEmpty(pagesize))
            //{
            //    pagesize = "50";
            //}

            //int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(Convert.ToInt32(pageindex), int.Parse(pagesize));
            objAssetEntity.PONumber = PONumber;



            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetEntity> list = objAssetBusiness.GetAllAssetStockInformation(objAssetEntity, StockFromDate, StockToDate, ItemId);
            //for (int i = 0; i < list.Count; i++)
            //{
            //    list[i].Pageindex = Convert.ToInt32(pageindex);
            //}

            //ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetStockList", list);
        }

        public ActionResult AssetStockAssignListItemWise(int ItemId, string PONumber)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            PONumberList = objAssetBusiness.getPONumberList();
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            objAssetAssignEntity.PONumber = PONumber;
            objAssetAssignEntity.ItemId = ItemId;

            //objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetAssetAssignList(objAssetAssignEntity);
            int[] paggingArray = Helper.HelperCls.GetDisplayRecordCount(ConfigurationProvider.Pageindex, ConfigurationProvider.WegGridPageSize);
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetAssignInformation("", paggingArray[0], paggingArray[1], "", "", objAssetAssignEntity, "", "");
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(ConfigurationProvider.Pageindex);
            }

            objAssetAssignEntity.lstAssetAssignEntity = list;

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;


            return View(objAssetAssignEntity);
        }

        public ActionResult AssetStockAssignPagingSortingList(string TagNo, int ItemId, string PONumber, string EEID, string FacilityCode, string AssetFromDate, string AssetToDate, string DIV, string REG, string DIST, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.AssetTagNumber = TagNo;
            objAssetEntity.ItemId = ItemId;
            objAssetEntity.PONumber = PONumber;
            objAssetEntity.EmployeeId = EEID;
            objAssetEntity.FacilityCode = FacilityCode;
            objAssetEntity.DIV = DIV;
            objAssetEntity.REG = REG;
            objAssetEntity.DIST = DIST;




            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetAssignInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, AssetFromDate, AssetToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetAssignListItemWise", list);
        }

        public ActionResult AssetStockReturnListItemWise(int ItemId, string PONumber)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            PONumberList = objAssetBusiness.getPONumberList();
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }




            objAssetAssignEntity.PONumber = PONumber;
            objAssetAssignEntity.ItemId = ItemId;

            objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetAssetStockReturnList(objAssetAssignEntity);

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;


            return View(objAssetAssignEntity);
        }

        public ActionResult AssetStockReturnPagingSortingList(string TagNo, int ItemId, string PONumber, string EEID, string FacilityCode, string AssetFromDate, string AssetToDate, string DIV, string REG, string DIST, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.AssetTagNumber = TagNo;
            objAssetEntity.ItemId = ItemId;
            objAssetEntity.PONumber = PONumber;
            objAssetEntity.EmployeeId = EEID;
            objAssetEntity.FacilityCode = FacilityCode;
            objAssetEntity.DIV = DIV;
            objAssetEntity.REG = REG;
            objAssetEntity.DIST = DIST;




            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetStockReturnInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, AssetFromDate, AssetToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetReturnListItemWise", list);
        }

        public ActionResult AssetStockRetireListItemWise(int ItemId, string PONumber)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            PONumberList = objAssetBusiness.getPONumberList();
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }




            objAssetAssignEntity.PONumber = PONumber;
            objAssetAssignEntity.ItemId = ItemId;

            objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetAssetRetireList(objAssetAssignEntity);

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;


            return View(objAssetAssignEntity);
        }

        public ActionResult AssetStockRetirePagingSortingList(string TagNo, int ItemId, string PONumber, string EEID, string FacilityCode, string AssetFromDate, string AssetToDate, string DIV, string REG, string DIST, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.AssetTagNumber = TagNo;
            objAssetEntity.ItemId = ItemId;
            objAssetEntity.PONumber = PONumber;
            objAssetEntity.EmployeeId = EEID;
            objAssetEntity.FacilityCode = FacilityCode;
            objAssetEntity.DIV = DIV;
            objAssetEntity.REG = REG;
            objAssetEntity.DIST = DIST;




            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetRetireInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, AssetFromDate, AssetToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetRetireListItemWise", list);
        }

        public ActionResult AssetStockTermListItemWise(int ItemId, string PONumber)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            PONumberList = objAssetBusiness.getPONumberList();
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            objAssetAssignEntity.PONumber = PONumber;
            objAssetAssignEntity.ItemId = ItemId;

            objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.GetAssetTermedList(objAssetAssignEntity);

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;


            return View(objAssetAssignEntity);
        }

        public ActionResult AssetStockTermPagingSortingList(string TagNo, int ItemId, string PONumber, string EEID, string FacilityCode, string TermFromDate, string TermToDate, string DIV, string REG, string DIST, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.AssetTagNumber = TagNo;
            objAssetEntity.ItemId = ItemId;
            objAssetEntity.PONumber = PONumber;
            objAssetEntity.EmployeeId = EEID;
            objAssetEntity.FacilityCode = FacilityCode;
            objAssetEntity.DIV = DIV;
            objAssetEntity.REG = REG;
            objAssetEntity.DIST = DIST;




            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetTermedInformation(SessionManager.UserName, paggingArray[0], paggingArray[1], "", "", objAssetEntity, TermFromDate, TermToDate);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetTermListItemWise", list);
        }

        public ActionResult AssetStockAvailableListItemWise(int ItemId, string PONumber)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<SelectListItem> idNameList = new List<SelectListItem>();
            List<SelectListItem> idAssetTagList = new List<SelectListItem>();
            List<SelectListItem> idPODetailList = new List<SelectListItem>();
            List<IdNameEntity> AssetTagList = new List<IdNameEntity>();
            List<IdNameEntity> PONumberList = new List<IdNameEntity>();
            List<IdNameEntity> NameList = new List<IdNameEntity>();
            //idAssetTagList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idPODetailList.Add(new SelectListItem() { Text = "select", Value = "", Selected = true });
            idNameList.Add(new SelectListItem() { Text = "select", Value = "0", Selected = true });
            PONumberList = objAssetBusiness.getPONumberList();
            AssetTagList = objAssetBusiness.getAssetAllTagList();
            NameList = objAssetBusiness.getItemNameList();
            foreach (IdNameEntity objIdNameEntity in PONumberList)
            {
                idPODetailList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in AssetTagList)
            {
                idAssetTagList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Name) });
            }
            foreach (IdNameEntity objIdNameEntity in NameList)
            {
                idNameList.Add(new SelectListItem() { Text = objIdNameEntity.Name, Value = Convert.ToString(objIdNameEntity.Id) });
            }

            objAssetAssignEntity.PONumber = PONumber;
            objAssetAssignEntity.ItemId = ItemId;

            objAssetAssignEntity.lstAssetAssignEntity = objAssetBusiness.AssetStockAvailableListItemWise(objAssetAssignEntity);

            ViewBag.PODetailList = idPODetailList;
            ViewBag.AssetTagList = idAssetTagList;
            ViewBag.ItemDetailList = idNameList;


            return View(objAssetAssignEntity);
        }

        public ActionResult AssetStockAvailablePagingSortingList(int ItemId, string PONumber, string pagesize, string pageindex, string columnName, string sortorder)
        {
            AssetAssignEntity objAssetEntity = new AssetAssignEntity();

            //var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.Link));
            //if (permission != null)
            //{
            //    ViewBag.IsAdd = CommonMethods.GetFormPermission(permission).IsAdd;
            //    ViewBag.IsEdit = CommonMethods.GetFormPermission(permission).IsEdit;
            //    ViewBag.IsDelete = CommonMethods.GetFormPermission(permission).IsDelete;
            //}

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
            objAssetEntity.ItemId = ItemId;
            objAssetEntity.PONumber = PONumber;

            //List<HSGUserEntity> list = hsgUser.GetHSGUserList(firstName, lastName, username, title, div, reg, dist, eeid, status, paggingArray[0], paggingArray[1], columnName, sortorder);
            AssetBusiness objAssetBusiness = new AssetBusiness();
            List<AssetAssignEntity> list = objAssetBusiness.GetAllAssetAvailableInformation(paggingArray[0], paggingArray[1], "", "", objAssetEntity);
            for (int i = 0; i < list.Count; i++)
            {
                list[i].Pageindex = Convert.ToInt32(pageindex);
            }

            ViewBag.PageSize = int.Parse(pagesize);
            return PartialView("_AssetAvailableListItemWise", list);
        }

        public ActionResult AddAssetReassign(int Id)
        {
            AssetAssignEntity objAssetAssignEntity = new AssetAssignEntity();
            AssetBusiness objAssetBusiness = new AssetBusiness();
            var AssetDetail = objAssetBusiness.GetAssetItemDetailByItemId(Id);
            objAssetAssignEntity.AssetTagNumber = AssetDetail.AssetTagNumber;
            objAssetAssignEntity.ItemId = AssetDetail.ItemId;
            objAssetAssignEntity.ItemName = AssetDetail.ItemName;
            objAssetAssignEntity.PONumber = AssetDetail.PONumber;

            return PartialView("_ViewAssetReAssign", objAssetAssignEntity);
        }

        #endregion

        [HttpPost]
        [AllowAnonymous]
        public ActionResult ImportExcelAssetAssignList()
        {

            DataSet ds = new DataSet();
            DataTable dtExcel = new DataTable();
            int ans = 0;
            if (HttpContext.Request.Files.Count > 0 && HttpContext.Request.Files[0].ContentLength > 0)
            {
                var file = HttpContext.Request.Files[0];
                string fileExtension = Path.GetExtension(file.FileName);//System.IO.Path.GetExtension(Request.Files["file"].FileName);

                if (fileExtension == ".xls" || fileExtension == ".xlsx")
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        using (var pck = new OfficeOpenXml.ExcelPackage())
                        {
                            file.InputStream.CopyTo(memoryStream);
                            memoryStream.Position = 0;

                            using (var stream = memoryStream)
                            {
                                pck.Load(stream);
                            }
                            var ws = pck.Workbook.Worksheets.First();



                            //if (isProperExcel)
                            //{
                            foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                            {
                                dtExcel.Columns.Add(true ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                            }

                            List<string> ColumnNameList = new List<string>();
                            ColumnNameList.Add("AssetTagNumber");
                            ColumnNameList.Add("ItemName");
                            ColumnNameList.Add("PONumber");
                            ColumnNameList.Add("AssignDate");
                            ColumnNameList.Add("IsEmployee");
                            ColumnNameList.Add("EmployeeID");
                            ColumnNameList.Add("IsFacility");
                            ColumnNameList.Add("FacilityCode");
                            bool isProperExcel = false;
                            foreach (string str in ColumnNameList)
                            {
                                DataColumnCollection columns = dtExcel.Columns;
                                if (columns.Contains(str))
                                {
                                    isProperExcel = true;
                                }
                                else
                                {
                                    isProperExcel = false;
                                    break;
                                }
                            }
                            if (isProperExcel)
                            {
                                var startRow = true ? 2 : 1;
                                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                                {
                                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                                    DataRow row = dtExcel.Rows.Add();
                                    foreach (var cell in wsRow)
                                    {
                                        row[(cell.Start.Column) - 1] = cell.Text;
                                    }
                                }

                                List<ImportAssetAssignEntity> AssetAssignEntityList = new List<ImportAssetAssignEntity>();
                                if (dtExcel.Rows.Count > 0)
                                {
                                    AssetAssignEntityList = dtExcel.DataTableToList<ImportAssetAssignEntity>();
                                }
                                AssetBusiness objAssetBusiness = new AssetBusiness();
                                foreach (ImportAssetAssignEntity entity in AssetAssignEntityList)
                                {
                                    ans = objAssetBusiness.SaveImportedAssetAssign(entity, SessionManager.UserID);
                                }
                            }
                        }
                    }

                    //ClientInfoBusiness objClientInfoBusiness = new ClientInfoBusiness();
                    //bool result = objClientInfoBusiness.ImportClientInfo(dtExcel, "ClientInfo");
                    if (ans > 0)
                    {
                        return Content("1");
                    }
                    else
                    {
                        return Content("0");
                    }
                }
            }
            return View();
        }
    }
}
