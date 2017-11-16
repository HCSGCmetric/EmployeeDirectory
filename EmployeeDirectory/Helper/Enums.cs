using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDirectory.Helper
{
    public class StringValueAttribute : System.Attribute
    {

        private string _value;

        /// <summary>
        /// Constructor for StringValueAttribute class.
        /// </summary>
        /// <param name="value">string value of the enum.</param>
        public StringValueAttribute(string value)
        {
            _value = value;
        }

        public string Value
        {
            get { return _value; }
        }

    }


    public enum ScreenAlias
    {
        [StringValue("AdminUser")]
        AdminUser,

        [StringValue("EmployeeAdminUser")]
        EmployeeAdminUser,

        [StringValue("EmployeeDirectory")]
        EmployeeDirectory,

        [StringValue("FieldMaster")]
        FieldMaster,

        [StringValue("GroupMaster")]
        GroupMaster,

        [StringValue("HsgUser")]
        HsgUser,

        [StringValue("User")]
        User,

        [StringValue("UserType")]
        UserType,

        [StringValue("CorporateContact")]
        CorporateContact,

        [StringValue("FacilityRealignment")]
        FacilityRealignment,

        [StringValue("MobileUsers")]
        MobileUsers,

        [StringValue("MobileUserexceptions")]
        MobileUserexceptions,

        [StringValue("FormPermission")]
        FormPermission,

        [StringValue("Link")]
        Link,

        [StringValue("AssetManagement")]
        AssetManagement,

        [StringValue("AssetPurchaseOrder")]
        AssetPurchaseOrder,

        [StringValue("AssetAssign")]
        AssetAssign,
        [StringValue("AssetReturn")]
        AssetReturn,
        [StringValue("AssetRetire")]
        AssetRetire,
        [StringValue("AssetMaster")]
        AssetMaster,
        [StringValue("CoupaAdmin")]
        CoupaAdmin,

        [StringValue("CoupaApprovers")]
        CoupaApprovers,

        [StringValue("CorporateUsers")]
        CorporateUsers,

        [StringValue("DietitianUsers")]
        DietitianUsers,

        [StringValue("DSSIUserAdmin")]
        DSSIUserAdmin,

        [StringValue("DSSIExceptions")]
        DSSIExceptions


    }

    public enum PaymentStatus
    {
        InvalidRequest = 0,
    }

    /// <summary>
    /// Types of rights
    /// </summary>
    public enum Rights
    {
        /// <summary>
        /// The add
        /// </summary>
        [StringValue("Add")]
        Add,

        /// <summary>
        /// The delete
        /// </summary>
        [StringValue("Delete")]
        Delete,

        /// <summary>
        /// The edit
        /// </summary>
        [StringValue("Edit")]
        Edit,

        /// <summary>
        /// The view
        /// </summary>
        [StringValue("View")]
        View,

        /// <summary>
        /// The Approve
        /// </summary>
        [StringValue("Approve")]
        Approve
    }

    public enum AllowAccess
    {
        /// <summary>
        /// The in active user
        /// </summary>
        InActiveUser = 1,

        /// <summary>
        /// The true
        /// </summary>
        True,

        /// <summary>
        /// The false
        /// </summary>
        False
    }

    public enum ApplicationNames
    {
        /// <summary>
        /// The add
        /// </summary>
        [StringValue("Citrix User App")]
        CitrixUserApp,

        /// <summary>
        /// The delete
        /// </summary>
        [StringValue("Employee Directory")]
        EmployeeDirectory,

        [StringValue("Mobile User")]
        MobileUser,

        [StringValue("Corporate Contact")]
        CorporateContact,

        [StringValue("Facility Realignment")]
        FacilityRealignment,

        [StringValue("Asset Management")]
        AssetManagement,
        
        [StringValue("Coupa Admin")]
        CoupaAdmin,

        [StringValue("DSSI Admin")]
        DssiAdmin
    }


}