using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeDirectory.EmpDirectory
{
    public partial class DssiExceptions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorizationAttribute obj = new AuthorizationAttribute(ScreenAlias.DSSIExceptions, Rights.View, true);
            if (obj.AuthorizeCore())
            {
                var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.DSSIExceptions));
                if (permission != null)
                {
                    ViewState["IsAdd"] = CommonMethods.GetFormPermission(permission).IsAdd;
                    ViewState["IsEdit"] = CommonMethods.GetFormPermission(permission).IsEdit;
                    ViewState["IsDelete"] = CommonMethods.GetFormPermission(permission).IsDelete;
                }

                int userId = Convert.ToInt32(Session[AppConstant.USERID]);
                if (userId == 0)
                {

                }
                else
                {
                    //  lblUser.Text = Session[AppConstant.NAME].ToString();

                }
            }
            else
            {
                Response.Redirect("../Home/Dashboard");
            }

        }

        protected void btnAddApplyDtCustCode_Click(object sender, EventArgs e)
        {
            fvApplyDtCustCode.ChangeMode(FormViewMode.Insert);
        }

        protected void btnDeleteApplyDtCustCode_Click(object sender, EventArgs e)
        {
            DropDownList ddlApplyDtCustCode = (DropDownList)fvApplyDtCustCode.FindControl("ddlApplyDtCustCode");
            SqlDataSource1.DeleteCommand = "delete from [HSG_DSSI_ExceptionDailyFeed] where UseApplyDateCustCode = '" + ddlApplyDtCustCode.SelectedValue + "'";
            SqlDataSource1.Delete();
        }

        protected void btnCancelApplyDtCustCode_Click(object sender, EventArgs e)
        {
            fvApplyDtCustCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void btnInsertApplyDtCustCode_Click(object sender, EventArgs e)
        {
            TextBox txtApplyDtCustCode = (TextBox)fvApplyDtCustCode.FindControl("txtApplyDtCustCode");
            SqlDataSource1.InsertCommand = "insert into [HSG_DSSI_ExceptionDailyFeed] (UseApplyDateCustCode) values ('" + txtApplyDtCustCode.Text + "')";
            SqlDataSource1.Insert();
        }

        protected void btnAddEndDtAff_Click(object sender, EventArgs e)
        {
            fvEndDtAff.ChangeMode(FormViewMode.Insert);
        }

        protected void btnDeleteEndDtAff_Click(object sender, EventArgs e)
        {
            DropDownList ddlEndDtAff = (DropDownList)fvEndDtAff.FindControl("ddlEndDtAff");
            SqlDataSource3.DeleteCommand = "delete from [HSG_DSSI_ExceptionDailyFeed] where UseEndDateAff = '" + ddlEndDtAff.SelectedValue + "'";
            SqlDataSource3.Delete();
        }

        protected void btnCancelEndDtAff_Click(object sender, EventArgs e)
        {
            fvEndDtAff.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void btnInsertEndDtAff_Click(object sender, EventArgs e)
        {
            TextBox txtEndDtAff = (TextBox)fvEndDtAff.FindControl("txtEndDtAff");
            SqlDataSource3.InsertCommand = "insert into [HSG_DSSI_ExceptionDailyFeed] (UseEndDateAff) values ('" + txtEndDtAff.Text + "')";
            SqlDataSource3.Insert();
        }

        protected void btnAddEndDtCustCode_Click(object sender, EventArgs e)
        {
            fvEndDtCustCode.ChangeMode(FormViewMode.Insert);
        }

        protected void btnDeleteEndDtCustCode_Click(object sender, EventArgs e)
        {
            DropDownList ddlEndDtCustCode = (DropDownList)fvEndDtCustCode.FindControl("ddlEndDtCustCode");
            SqlDataSource2.DeleteCommand = "delete from [HSG_DSSI_ExceptionDailyFeed] where UseEndDateCustCode = '" + ddlEndDtCustCode.SelectedValue + "'";
            SqlDataSource2.Delete();
        }

        protected void btnCancelEndDtCustCode_Click(object sender, EventArgs e)
        {
            fvEndDtCustCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void btnInsertEndDtCustCode_Click(object sender, EventArgs e)
        {
            TextBox txtEndDtCustCode = (TextBox)fvEndDtCustCode.FindControl("txtEndDtCustCode");
            SqlDataSource2.InsertCommand = "insert into [HSG_DSSI_ExceptionDailyFeed] (UseEndDateCustCode) values ('" + txtEndDtCustCode.Text + "')";
            SqlDataSource2.Insert();
        }

        protected void btnCancelGLAC_Click(object sender, EventArgs e)
        {
            fvGLAC.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void btnInsertGLAC_Click(object sender, EventArgs e)
        {
            TextBox txtGLAC = (TextBox)fvGLAC.FindControl("txtGLAC");
            SqlDataSource10.InsertCommand = "insert into [HSG_DSSI_ExceptionGLCodes] (seg_code) values ('" + txtGLAC.Text + "')";
            SqlDataSource10.Insert();
        }

        protected void btnAddGLAC_Click(object sender, EventArgs e)
        {
            fvGLAC.ChangeMode(FormViewMode.Insert);
        }

        protected void btnDeleteGLAC_Click(object sender, EventArgs e)
        {
            DropDownList ddlGLAC = (DropDownList)fvGLAC.FindControl("ddlGLAC");
            SqlDataSource10.DeleteCommand = "delete from [HSG_DSSI_ExceptionGLCodes]where seg_code = '" + ddlGLAC.SelectedValue + "'";
            SqlDataSource10.Delete();
        }

        protected void btnAddFacilityCode_Click(object sender, EventArgs e)
        {
            fvFacilityCode.ChangeMode(FormViewMode.Insert);
        }

        protected void btnDeleteFacilityCode_Click(object sender, EventArgs e)
        {
            DropDownList ddlFacilityCode = (DropDownList)fvFacilityCode.FindControl("ddlFacilityCode");
            SqlDataSource8.DeleteCommand = "delete from [HSG_DSSI_ExceptionFacility] where FacilityCode = '" + ddlFacilityCode.SelectedValue + "'";
            SqlDataSource8.Delete();
        }

        protected void btnCancelFacilityCode_Click(object sender, EventArgs e)
        {
            fvFacilityCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void btnInsertFacilityCode_Click(object sender, EventArgs e)
        {
            TextBox txtFacilityCode = (TextBox)fvFacilityCode.FindControl("txtFacilityCode");
            SqlDataSource8.InsertCommand = "insert into [HSG_DSSI_ExceptionFacility] (FacilityCode) values ('" + txtFacilityCode.Text + "')";
            SqlDataSource8.Insert();
        }

        //protected void btnLogout_Click(object sender, EventArgs e)
        //{
        //    Session.Abandon();
        //    Response.Redirect("default.aspx");
        //}

        protected void SqlDataSource1_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            TextBox txtApplyDtCustCode = (TextBox)fvApplyDtCustCode.FindControl("txtApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('add','" + txtApplyDtCustCode.Text + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvApplyDtCustCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource1_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            DropDownList ddlApplyDtCustCode = (DropDownList)fvApplyDtCustCode.FindControl("ddlApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('delete','" + ddlApplyDtCustCode.SelectedValue + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvApplyDtCustCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource2_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            TextBox txtEndDtCustCode = (TextBox)fvEndDtCustCode.FindControl("txtApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('add','" + txtEndDtCustCode.Text + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvEndDtCustCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource2_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            DropDownList ddlEndDtCustCode = (DropDownList)fvEndDtCustCode.FindControl("ddlApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('delete','" + ddlEndDtCustCode.SelectedValue + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvEndDtCustCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource3_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            TextBox txtEndDtAff = (TextBox)fvEndDtAff.FindControl("txtApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('add','" + txtEndDtAff.Text + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvEndDtAff.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource3_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            DropDownList ddlEndDtAff = (DropDownList)fvEndDtAff.FindControl("ddlApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('delete','" + ddlEndDtAff.SelectedValue + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvEndDtAff.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource8_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            TextBox txtFacilityCode = (TextBox)fvFacilityCode.FindControl("txtApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('add','" + txtFacilityCode.Text + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvFacilityCode.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource8_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            DropDownList ddlFacilityCode = (DropDownList)fvFacilityCode.FindControl("ddlApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('delete','" + ddlFacilityCode.SelectedValue + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvFacilityCode.ChangeMode(FormViewMode.ReadOnly);

        }

        protected void SqlDataSource10_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            TextBox txtGLAC = (TextBox)fvGLAC.FindControl("txtApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('add','" + txtGLAC.Text + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvGLAC.ChangeMode(FormViewMode.ReadOnly);
        }

        protected void SqlDataSource10_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {
            DropDownList ddlGLAC = (DropDownList)fvGLAC.FindControl("ddlApplyDtCustCode");
            SqlAudit.InsertCommand = "Insert into HSG_DSSI_Exceptionsaudit (action,value,type,theuser) values ('delete','" + ddlGLAC.SelectedValue + "','UseApplyDateCustCode','" + hiduser.Value + "')";
            SqlAudit.Insert();
            fvGLAC.ChangeMode(FormViewMode.ReadOnly);

        }
    }
}