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
using System.Web.Services;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace EmployeeDirectory
{
    public partial class mobileuserexceptions : System.Web.UI.Page
    {
        protected void gvProducts_Init(object sender, EventArgs e)
        {
            ASPxGridView gridView = sender as ASPxGridView;
            gridView.JSProperties["cpShowDeleteConfirmBox"] = false;

        }
        
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorizationAttribute obj = new AuthorizationAttribute(ScreenAlias.MobileUserexceptions, EmployeeDirectory.Helper.Rights.View, true);
            if (obj.AuthorizeCore())
            {

                var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.MobileUserexceptions));
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
                    //lblUser.Text = Session[AppConstant.NAME].ToString();
                    SqlDataSource1.UpdateParameters["UpdatedBy"].DefaultValue = Session[AppConstant.USERNAME].ToString();
                    SqlDataSource1.InsertParameters["UpdatedBy"].DefaultValue = Session[AppConstant.USERNAME].ToString();
                }

                if (permission != null)
                {
                    if (Convert.ToBoolean(ViewState["IsAdd"]) != true)
                    {
                        foreach (DevExpress.Web.GridViewColumn gvc in gvProducts.Columns)
                        {
                            if (gvc.VisibleIndex == 0)
                            {
                                gvc.VisibleIndex = -1;
                            }
                            if (gvc.VisibleIndex == 1)
                            {
                                gvc.VisibleIndex = -1;
                            }
                        }
                    }
                }
                else
                {
                    Response.Redirect("../Home/Dashboard");
                }

                SqlDataSource1.UpdateParameters["LASTUPDATED"].DefaultValue = System.DateTime.Now.ToShortDateString();
                SqlDataSource1.InsertParameters["LASTUPDATED"].DefaultValue = System.DateTime.Now.ToShortDateString();

            }
            else
            {
                Response.Redirect("../Home/Dashboard");
            }

        }

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

        [WebMethod]
        public static string GetUserProps(string empid)
        {
            string query = string.Empty;
            query += "select firstname+','+lastname+','+[Job Title]+','+arcust_div+','+arcust_reg+','+arcust_dist+','+arcust_subreg as employeeinfo FROM [HSGEmployeeDirectory].[dbo].[EmployeeMaster_VW] where [EE ID] = '" + empid + "'";
            string constr = ConfigurationManager.ConnectionStrings["HSGEmployeeDirectory"].ConnectionString;
            string employeeinfo = string.Empty;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {

                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            employeeinfo = sdr["employeeinfo"].ToString();
                        }
                    }
                    con.Close();
                    return employeeinfo;


                }
            }

        }
    }
}