using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using EmployeeDirectory.Entity;
using DevExpress.Export;
using DevExpress.XtraPrinting;
using EmployeeDirectory.Helper;
using DevExpress.Web;
using DevExpress.Web.Data;
using DevExpress.Utils;

namespace EmployeeDirectory
{
    public partial class corporatecontacts : System.Web.UI.Page
    {

        protected void gvProducts_Init(object sender, EventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            gridView.JSProperties["cpShowDeleteConfirmBox"] = false;



        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //AuthorizationAttribute obj = new AuthorizationAttribute(ScreenAlias.CorporateContact, EmployeeDirectory.Helper.Rights.View, true);
            //if (obj.AuthorizeCore())
            //{

            var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.CorporateContact));
            if (permission != null)
            {
                ViewState["IsAdd"] = CommonMethods.GetFormPermission(permission).IsAdd;
                ViewState["IsEdit"] = CommonMethods.GetFormPermission(permission).IsEdit;
                ViewState["IsDelete"] = CommonMethods.GetFormPermission(permission).IsDelete;
            }
            //    int userId = Convert.ToInt32(Session[AppConstant.USERID]);
            //    if (userId == 0)
            //    {

            //    }
            //    else
            //    {

            //    }
            //    if (permission != null)
            //    {
            
            if (Convert.ToBoolean(ViewState["IsAdd"]) != true)
            {
                (gvProducts.Columns[0] as GridViewCommandColumn).ShowNewButtonInHeader = false;
            }
            if (Convert.ToBoolean(ViewState["IsEdit"]) != true)
            {
                (gvProducts.Columns[1] as GridViewCommandColumn).ShowEditButton = false;

            }
            //if (Convert.ToBoolean(ViewState["IsDelete"]) == true)
            //{
            //    (gvProducts.Columns[1] as GridViewCommandColumn).ShowNewButtonInHeader = true;
            //}
            //}
            //else
            //{
            //    Response.Redirect("../Home/Dashboard");
            //}
            //}
        }
        //protected void ToolbarExport_ItemClick(object source, ExportItemClickEventArgs e)
        //{
        //    switch (e.ExportType)
        //    {
        //        case DemoExportFormat.Pdf:
        //            gridExport.WritePdfToResponse();
        //            break;
        //        case DemoExportFormat.Xls:
        //            gridExport.WriteXlsToResponse(new XlsExportOptionsEx { ExportType = ExportType.WYSIWYG });
        //            break;
        //        case DemoExportFormat.Xlsx:
        //            gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });
        //            break;
        //        case DemoExportFormat.Rtf:
        //            gridExport.WriteRtfToResponse();
        //            break;
        //        case DemoExportFormat.Csv:
        //            gridExport.WriteCsvToResponse(new CsvExportOptionsEx() { ExportType = ExportType.WYSIWYG });
        //            break;
        //    }
        //}


        protected void gvProducts_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;

            if (e.ButtonID == "btDelete")
            {
                //Server-side actions performed before showing popup here
                gridView.JSProperties["cpRowIndex"] = e.VisibleIndex;
                gridView.JSProperties["cpShowDeleteConfirmBox"] = true;
            }
        }

        protected void gvProducts_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
        {
            //if (e.ButtonID == "btDelete")
            //{

            //    if (Convert.ToString(ViewState["IsDelete"]) == "True")
            //    {
            //        e.Visible = DefaultBoolean.True;
            //    }
            //    else { e.Visible = DefaultBoolean.False; }


            //    //if (((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "Order_number").ToString() == "3")
            //    //    e.Visible = DefaultBoolean.False;
            //}

            if (e.CellType == GridViewTableCommandCellType.Filter)
                return;

            //if (e.VisibleIndex == 0 || e.CellType == GridViewTableCommandCellType.Filter)
            //    return;

            if (e.ButtonID == "btDelete")
            {

                if (Convert.ToString(ViewState["IsDelete"]) == "True")
                {
                    e.Visible = DefaultBoolean.True;
                }
                else { e.Visible = DefaultBoolean.False; }


                //if (((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "Order_number").ToString() == "3")
                //    e.Visible = DefaultBoolean.False;
            }
            //if (((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "0").ToString() == "0")
            //    e.Visible = DefaultBoolean.False;
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            gridExport.WriteXlsxToResponse(new XlsxExportOptionsEx { ExportType = ExportType.WYSIWYG });

        }
    }
}