using DevExpress.Web;
using EmployeeDirectory.Entity;
using EmployeeDirectory.Helper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeDirectory.EmpDirectory
{
    public partial class dietitian_users : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorizationAttribute obj = new AuthorizationAttribute(ScreenAlias.DietitianUsers, Helper.Rights.View, true);
            if (obj.AuthorizeCore())
            {
                var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.DietitianUsers));
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

            if (Convert.ToBoolean(ViewState["IsAdd"]) != true)
            {
                (ASPxGridView1.Columns[0] as GridViewCommandColumn).ShowNewButtonInHeader = false;
            }
            if (Convert.ToBoolean(ViewState["IsEdit"]) != true)
            {
                (ASPxGridView1.Columns[0] as GridViewCommandColumn).ShowEditButton = false;

            }
            if (Convert.ToBoolean(ViewState["IsDelete"]) != true)
            {
                (ASPxGridView1.Columns[0] as GridViewCommandColumn).ShowDeleteButton = false;

            }

        }

        protected void ASPxGridView1_CellEditorInitialize(object sender, DevExpress.Web.ASPxGridViewEditorEventArgs e)
        {
            var abc = sender as DevExpress.Web.ASPxGridView;
            if (e.Column.FieldName == "vendor_code" || e.Column.FieldName == "Employee ID" || e.Column.FieldName == "USER" || e.Column.FieldName == "Firstname" || e.Column.FieldName == "Lastname" || e.Column.FieldName == "Email" || e.Column.FieldName == "Cost Center" || e.Column.FieldName == "Job Title")
                e.Editor.ReadOnly = !abc.IsNewRowEditing;
        }

        //protected void btnLogout_Click(object sender, EventArgs e)
        //{
        //    Session.Abandon();
        //    Response.Redirect("default.aspx");
        //}

        [WebMethod]
        public static string GetOther(string empid)
        {
            string query = string.Empty;
            query += "select ([Employee_ID]+','+Email) as employeeinfo FROM [HSG_COUPA_Dietitian_Users] where [USER] = '" + empid + "'";
            string constr = ConfigurationManager.ConnectionStrings["HSGOPSConnectionString"].ConnectionString;
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