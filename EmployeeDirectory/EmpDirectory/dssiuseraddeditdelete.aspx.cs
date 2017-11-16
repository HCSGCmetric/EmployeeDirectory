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
    public partial class dssiuseraddeditdelete : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AuthorizationAttribute obj = new AuthorizationAttribute(ScreenAlias.DSSIUserAdmin, Rights.View, true);
            if (obj.AuthorizeCore())
            {
                var permission = CommonMethods.CheckPermission(StringEnum.GetStringValue(ScreenAlias.DSSIUserAdmin));
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
    }
}