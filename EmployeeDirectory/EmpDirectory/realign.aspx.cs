using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//using System.DirectoryServices;
using EmployeeDirectory.Entity;
using DevExpress.Web;
using EmployeeDirectory.Helper;

namespace EmployeeDirectory
{
    public partial class realign : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
               AuthorizationAttribute obj = new AuthorizationAttribute(ScreenAlias.FacilityRealignment, EmployeeDirectory.Helper.Rights.View, true);
               if (obj.AuthorizeCore())
               {
                   var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.MobileUserexceptions));
                   if (permission != null)
                   {
                       ViewState["IsAdd"] = CommonMethods.GetFormPermission(permission).IsAdd;
                       ViewState["IsEdit"] = CommonMethods.GetFormPermission(permission).IsEdit;
                       ViewState["IsDelete"] = CommonMethods.GetFormPermission(permission).IsDelete;
                   }
                   SqlDataSource1.UpdateParameters["RealignFacilityDate"].DefaultValue = DateTime.Today.ToShortDateString();
                   // lblUser.Text = GetFullName(User.Identity.Name);
                   if (Session.Count == 0)
                   {
                       Session.Abandon();
                       Response.Redirect("/Login/AdminSignOut");
                   }
               }
               else
               {
                   Response.Redirect("../Home/Dashboard");
               }
               //SqlDataSource3.DataBind();
           // lblUser.Text = Session[AppConstant.NAME].ToString();
        }
        protected void SqlDataSource1_Updated(object sender, SqlDataSourceStatusEventArgs e)
        {
            SqlDataSource2.Update();
        }




        protected void ASPxGridView1_CustomButtonInitialize(object sender, DevExpress.Web.ASPxGridViewCustomButtonEventArgs e)
        {

            if (e.VisibleIndex == 0 || e.CellType == GridViewTableCommandCellType.Filter)
                return;
            //if (((ASPxGridView)sender).GetRowValues(e.VisibleIndex, "0").ToString() == "0")
            //    e.Visible = DefaultBoolean.False;
        }


        protected void selectAll_Init(object sender, EventArgs e)
        {
            ASPxCheckBox chk = sender as ASPxCheckBox;
            ASPxGridView grid = (chk.NamingContainer as GridViewHeaderTemplateContainer).Grid;
            Boolean cbChecked = true;
            Int32 start = grid.VisibleStartIndex;
            Int32 end = grid.VisibleStartIndex + grid.SettingsPager.PageSize;
            end = (end > grid.VisibleRowCount ? grid.VisibleRowCount : end);
            for (int i = start; i < end; i++)
            {
                bool value = Convert.ToBoolean(grid.GetRowValues(i, "RealignFlag"));
                if (!value)
                {
                    cbChecked = false;
                    break;
                }
            }
            chk.Checked = cbChecked;
        }

        protected void SqlDataSource3_Selected(object sender, SqlDataSourceStatusEventArgs e)
        {
            if (e.AffectedRows != 0) //Here AffectedRows gives you the count of returned rows.
            {
                Response.Redirect("handuplicates.aspx");
            }
            else
            {

                // Response.Redirect("corporatecontacts.aspx");
                SqlDataSource1.SelectCommand = "SELECT [ID], [UserName], [FacilityCode], [ReflectedDate], [CurrentReg], [CurrentDist], [FutureReg], [FutureDist], case when RealignFlag is null then 0 else RealignFlag end as RealignFlag FROM [FacilityReAlignment] where realignflag = '0' or realignflag is NULL";
                ASPxGridView1.DataBind();
            }
        }


    }
}