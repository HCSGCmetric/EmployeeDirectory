using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Entity
{
    public class AssetEntity
    {

        public int Id { get; set; }
        public int POID { get; set; }
        public int ItemId { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public string PONumber { get; set; }
        public string ItemDescription { get; set; }
        public int ItemCategoryID { get; set; }
        [Required]
        public string PODate { get; set; }
        public decimal Total { get; set; }
        public decimal POTotal { get; set; }
        public decimal POTax { get; set; }
        public decimal POShipping { get; set; }
        [Required]
        public int POVendorID {get;set;}
        public string POVendorName { get; set; }
        public string CompanyName { get; set; }
        public string InvoiceNumber { get; set; }
        public string InvoiceDate { get; set; }
        public int Quantity { get; set; }
        public decimal Cost { get; set; }
        public decimal WarrantyCost { get; set; }
        public string StockDate { get; set; }
        public int AdditionalWarrantyMonth { get; set; }
        public int TotalRows { get; set; }
        public int Pageindex { get; set; }
        public int pagesize { get; set; }
        public int TotalRecord { get; set; }
        public int AssignQty { get; set; }
        public int UsedQty { get; set; }
        public int ReturnQty { get; set; }
        public int TermQty { get; set; }
        public int RetireQty { get; set; }
        public int TotalAvailableQty { get; set; }
        public int CreatedBy { get; set; }
        public int UpdatedBy { get; set; }

        public string CreatedDate { get; set; }
        public bool IsActive { get; set; }
        public int UserId { get; set; }

        public string PurchaseOrderXML { get; set; }


        public List<AssetEntity> lstAssetEntity { get; set; }

    }

    public class AssetAssignEntity {


        //public DateTime? _assignDate = null;
        //[Required]
        //public DateTime? AssignDate
        //{
        //    get
        //    {
        //        return _assignDate;
        //    }
        //    set
        //    {
        //        if (value.Value.ToString() == "01/01/1900 12:00:00 AM")
        //        {
        //            //FormPermission = new FormPermissionEntity();
        //            _assignDate = null;
        //        }
        //        else
        //        {
        //            _assignDate = value;
        //        }
        //        //this._isFormpermissionNull = value;
        //    }
        //}


        public int ID { get; set; }
        public int POId { get; set; }
        public int AssetTagId { get; set; }
        [Required]
        public string AssetTagNumber { get; set; }
        [Required]
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public bool IsReturned { get; set; }
        [Required]
        public string PONumber { get; set; }
        public int AdditionalWarrantyMonth { get; set; }
        [Required]
        public string AssignDate { get; set; }
        [Required]
        public string ReturnDate { get; set; }
        [Required]
        public string PODate { get; set; }
        public string VendorSKU { get; set; }
        public bool IsCurrentSystem { get; set; }
        public string InvoiceNumber { get; set; }
        
        [Required]
        public string RetireDate { get; set; }
        public string ReceivedDate { get; set; }
        public string ResolvedDate { get; set; }
        public string RetireReason { get; set; }
        [Required]
        public string ReturnReason { get; set; }
        public bool IsEmployee { get; set; }
        public bool IsTermed { get; set; }
        public string TermedDate { get; set; }
        [Required]
        public string EmployeeId { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public string POVendorName { get; set; }
        [Required]
        public bool IsFacility { get; set; }
        [Required]
        public string FacilityCode { get; set; }
        public string DIV { get; set; }
        public string REG { get; set; }
        public string DIST { get; set; }
        [Required]
        public string FacilityName { get; set; }

        public string ReceivedAdditionalNote { get; set; }
        public string ResolvedAdditionalNote { get; set; }
        
        public int TotalAvailableQty { get; set; }
        public int UserId { get; set; }

        public string EmpAddress { get; set; }
        public string EmpState { get; set; }
        public string EmpPhoneNo { get; set; }
        public string FacTrackingNo { get; set; }
        public string FacHelpDeskTicketNo { get; set; }
        public string FacAddress { get; set; }
        public string FacState { get; set; }
        public bool ReAssign { get; set; }
        public int AssignMasterId { get; set; }
        [Required]
        public string HelpDeskNo { get; set; }
        [Required]
        public string ItemStatus { get; set; }
        public int TotalRows { get; set; }
        public int Pageindex { get; set; }
        public int pagesize { get; set; }
        public int TotalRecord { get; set; }

        public List<AssetAssignEntity> lstAssetAssignEntity { get; set; }
        public List<AssetAssignEntity> lstReturnItem { get; set; }
    }

    public class ImportAssetAssignEntity 
    {
        public int AssetTagNumber { get; set; }
        public string ItemName { get; set; }
        public string PONumber { get; set; }
        public string AssignDate { get; set; }
        public Boolean IsEmployee { get; set; }
        public string EmployeeID { get; set; }
        public Boolean IsFacility { get; set; }
        public string FacilityCode { get; set; }
        public int UserID { get; set; }
    }

    public class EmployeeEntity
    {
        public string EEID { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string ADDRESSONE { get; set; }
        public string ST { get; set; }
        public string PrimaryPhone { get; set; }

    }

    public class FacilityEntity
    {

        public string customer_code { get; set; }
        public string customer_name { get; set; }
        public string state { get; set; }
        public string addr1 { get; set; }

    }
}