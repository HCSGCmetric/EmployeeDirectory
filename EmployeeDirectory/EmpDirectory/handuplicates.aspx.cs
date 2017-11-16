using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using DevExpress.Web.Data;

namespace EmployeeDirectory.EmpDirectory
{
    public partial class handuplicates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }


        protected void gvDupes_Init(object sender, EventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            gridView.JSProperties["cpShowDeleteConfirmBox"] = false;



        }



        protected void gvDupes_CustomButtonCallback(object sender, ASPxGridViewCustomButtonCallbackEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;

            if (e.ButtonID == "btDelete")
            {
                //Server-side actions performed before showing popup here
                gridView.JSProperties["cpRowIndex"] = e.VisibleIndex;
                gridView.JSProperties["cpShowDeleteConfirmBox"] = true;
            }
        }

        protected void gvDupes_RowDeleted(object sender, ASPxDataDeletedEventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            gridView.Visible = false;
        }

        protected void btnGoToRealign_Click(object sender, EventArgs e)
        {
            Response.Redirect("realign.aspx");
        }

        protected void SqlDataSource3_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.AffectedRows != 0) //Here AffectedRows gives you the count of returned rows.
            {
                // Response.Redirect("handuplicates.aspx");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "text", "showButton()", true);

                // btnGoToRealign.Visible = true;
            }
        }



    }
}