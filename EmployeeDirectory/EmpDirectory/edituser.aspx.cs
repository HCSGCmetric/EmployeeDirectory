using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeDirectory.EmpDirectory
{
    public partial class edituser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string startUpScript = string.Format("window.parent.HidePopupAndShowInfo('Server','{0}');", "tata");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ANY_KEY", startUpScript, true);
        }

        protected void rpDetails_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item ||
       e.Item.ItemType == ListItemType.AlternatingItem)
            {
                var thestates = e.Item.FindControl("ddlState") as DropDownList;
                var thefaccode = e.Item.FindControl("ddlFacCode") as DropDownList;
                var thejobtitle = e.Item.FindControl("ddlJob") as DropDownList;
                string states = (string)((DataRowView)e.Item.DataItem)["State"];
                string faccodes = (string)((DataRowView)e.Item.DataItem)["FacCode"];
                string jobtitles = (string)((DataRowView)e.Item.DataItem)["JobTitle"];

                if (thestates != null)
                {
                    thestates.DataSource = SqlDataSource2; //a datatable
                    thestates.DataTextField = "state";
                    thestates.DataValueField = "state";
                    thestates.DataBind();

                    if (thestates.Items.FindByValue(states) != null)
                        thestates.SelectedValue = states;
                }

                if (thefaccode != null)
                {
                    thefaccode.DataSource = SqlDataSource1; //a datatable
                    thefaccode.DataTextField = "customer_code";
                    thefaccode.DataValueField = "customer_code";
                    thefaccode.DataBind();

                    if (thefaccode.Items.FindByValue(faccodes) != null)
                        thefaccode.SelectedValue = faccodes;
                }

                if (thejobtitle != null)
                {
                    thejobtitle.DataSource = SqlDataSource3; //a datatable
                    thejobtitle.DataTextField = "JobTitle";
                    thejobtitle.DataValueField = "JobTitle";
                    thejobtitle.DataBind();

                    if (thejobtitle.Items.FindByValue(jobtitles) != null)
                        thejobtitle.SelectedValue = jobtitles;
                }

            }
        }

        protected void SqlDataSource5_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            SqlAudit.Insert();

        }

        protected void SqlAudit_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            string startUpScript = string.Format("window.parent.HidePopupAndShowInfo('Server','{0}');", "tata");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ANY_KEY", startUpScript, true);
        }

        protected void btnEditUser_Click(object sender, EventArgs e)
        {
            foreach (RepeaterItem repItem in rpDetails.Items)
            {

                TextBox lname = (TextBox)repItem.FindControl("txtLnamet");
                TextBox fname = (TextBox)repItem.FindControl("txtFnamet");
                TextBox uname = (TextBox)repItem.FindControl("txtUsernamet");
                TextBox email = (TextBox)repItem.FindControl("txtEmailt");
                TextBox add1 = (TextBox)repItem.FindControl("txtAdd1t");
                TextBox add2 = (TextBox)repItem.FindControl("txtAdd2t");
                TextBox city = (TextBox)repItem.FindControl("txtCityt");
                DropDownList state = (DropDownList)repItem.FindControl("ddlState");
                DropDownList faccode = (DropDownList)repItem.FindControl("ddlFacCode");
                DropDownList jobtitle = (DropDownList)repItem.FindControl("ddlJob");
                TextBox zip = (TextBox)repItem.FindControl("txtZipt");
                TextBox phone = (TextBox)repItem.FindControl("txtPhonet");
                TextBox sitegroup = (TextBox)repItem.FindControl("txtSiteGroupIDt");
                TextBox approvalgroup = (TextBox)repItem.FindControl("txtApprovalGroupt");
                TextBox staples = (TextBox)repItem.FindControl("txtStaplesApprovert");



                SqlDataSource5.UpdateParameters["lname"].DefaultValue = lname.Text;
                SqlDataSource5.UpdateParameters["fname"].DefaultValue = fname.Text;
                SqlDataSource5.UpdateParameters["uname"].DefaultValue = uname.Text;
                SqlDataSource5.UpdateParameters["email"].DefaultValue = email.Text;
                SqlDataSource5.UpdateParameters["add1"].DefaultValue = add1.Text;
                SqlDataSource5.UpdateParameters["add2"].DefaultValue = add2.Text;
                SqlDataSource5.UpdateParameters["city"].DefaultValue = city.Text;
                SqlDataSource5.UpdateParameters["state"].DefaultValue = state.SelectedValue;
                SqlDataSource5.UpdateParameters["faccode"].DefaultValue = faccode.SelectedValue;
                SqlDataSource5.UpdateParameters["title"].DefaultValue = jobtitle.SelectedValue;
                SqlDataSource5.UpdateParameters["zip"].DefaultValue = zip.Text;
                SqlDataSource5.UpdateParameters["phone"].DefaultValue = phone.Text;
                SqlDataSource5.UpdateParameters["sitegroup"].DefaultValue = sitegroup.Text;
                SqlDataSource5.UpdateParameters["approval"].DefaultValue = approvalgroup.Text;
                SqlDataSource5.UpdateParameters["staples"].DefaultValue = staples.Text;
                SqlDataSource5.Update();
            }



        }
    }
}