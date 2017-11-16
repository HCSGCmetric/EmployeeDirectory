using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeDirectory.EmpDirectory
{
    public partial class adduser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCancel_Click(object sender, EventArgs e)
        {
            string startUpScript = string.Format("window.parent.HidePopupAndShowInfo2('Server','{0}');", "tata");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ANY_KEY", startUpScript, true);
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            SqlDataSource4.InsertParameters["paynum"].DefaultValue = txtPayrollt.Text.ToString();
            SqlDataSource4.InsertParameters["lname"].DefaultValue = txtLnamet.Text;
            SqlDataSource4.InsertParameters["fname"].DefaultValue = txtFnamet.Text;
            SqlDataSource4.InsertParameters["uname"].DefaultValue = txtUsernamet.Text;
            SqlDataSource4.InsertParameters["email"].DefaultValue = txtEmailt.Text;
            SqlDataSource4.InsertParameters["sitegroup"].DefaultValue = txtSiteGroupIDt.Text;
            SqlDataSource4.InsertParameters["approval"].DefaultValue = txtApprovalGroupt.Text;
            SqlDataSource4.InsertParameters["add1"].DefaultValue = txtAdd1t.Text;
            SqlDataSource4.InsertParameters["add2"].DefaultValue = txtAdd2t.Text;
            SqlDataSource4.InsertParameters["city"].DefaultValue = txtCityt.Text;
            SqlDataSource4.InsertParameters["state"].DefaultValue = ddlState.SelectedValue;
            SqlDataSource4.InsertParameters["zip"].DefaultValue = txtZipt.Text;
            SqlDataSource4.InsertParameters["faccode"].DefaultValue = ddlFacCode.SelectedValue;
            SqlDataSource4.InsertParameters["staples"].DefaultValue = txtStaplesApprovert.Text;
            SqlDataSource4.InsertParameters["phone"].DefaultValue = txtPhonet.Text;
            SqlDataSource4.InsertParameters["title"].DefaultValue = ddlJob.SelectedValue;
            SqlDataSource4.Insert();

        }

        protected void SqlDataSource4_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            SqlAudit.InsertParameters["value"].DefaultValue = txtPayrollt.Text;
            SqlAudit.Insert();

        }

        protected void SqlAudit_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            string startUpScript = string.Format("window.parent.HidePopupAndShowInfo2('Server','{0}');", "tata");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ANY_KEY", startUpScript, true);
        }

        protected void txtPayrollt_TextChanged(object sender, EventArgs e)
        {
            SqlDataSource5.SelectParameters["EEID"].DefaultValue = txtPayrollt.Text;
            rpFill.DataBind();
        }

        protected void rpFill_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;

            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                TextBox fname = (TextBox)item.FindControl("lblFname");
                txtFnamet.Text = fname.Text;
                TextBox lname = (TextBox)item.FindControl("lblLname");
                txtLnamet.Text = lname.Text;
                TextBox uname = (TextBox)item.FindControl("lblUsername");
                txtUsernamet.Text = uname.Text;
                TextBox email = (TextBox)item.FindControl("lblEmail");
                txtEmailt.Text = email.Text;
                TextBox add1 = (TextBox)item.FindControl("lblAdd1");
                txtAdd1t.Text = add1.Text;
                TextBox add2 = (TextBox)item.FindControl("lblAdd2");
                txtAdd2t.Text = add2.Text;
                TextBox city = (TextBox)item.FindControl("lblCity");
                txtCityt.Text = city.Text;
                TextBox state = (TextBox)item.FindControl("lblState");
                ddlState.SelectedValue = state.Text;
                TextBox zip = (TextBox)item.FindControl("lblZip");
                txtZipt.Text = zip.Text;
                TextBox phone = (TextBox)item.FindControl("lblPhone");
                txtPhonet.Text = phone.Text;
                TextBox customercode = (TextBox)item.FindControl("lblCustomerCode");
                ddlFacCode.SelectedValue = customercode.Text;
                TextBox jobtitle = (TextBox)item.FindControl("lblJobTitle");
                ddlJob.SelectedValue = jobtitle.Text;
                TextBox div = (TextBox)item.FindControl("lbldiv");
                CascadingDropDown1.SelectedValue = div.Text;
                CascadingDropDown4.SelectedValue = div.Text;
                TextBox reg = (TextBox)item.FindControl("lblreg");
                CascadingDropDown2.SelectedValue = reg.Text;
                CascadingDropDown5.SelectedValue = reg.Text;
                TextBox dist = (TextBox)item.FindControl("lbldist");
                CascadingDropDown3.SelectedValue = dist.Text;
            }

        }
    }
}