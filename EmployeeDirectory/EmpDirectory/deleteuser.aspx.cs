using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeDirectory.EmpDirectory
{
    public partial class deleteuser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnNo_Click(object sender, EventArgs e)
        {
            string startUpScript = string.Format("window.parent.HidePopupAndShowInfo3('Server','{0}');", "tata");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ANY_KEY", startUpScript, true);
        }

        protected void btnYes_Click(object sender, EventArgs e)
        {
            SqlDataSource1.Delete();
        }

        protected void SqlDataSource1_Deleted(object sender, SqlDataSourceStatusEventArgs e)
        {

            SqlAudit.Insert();

        }

        protected void SqlAudit_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            string startUpScript = string.Format("window.parent.HidePopupAndShowInfo3('Server','{0}');", "tata");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ANY_KEY", startUpScript, true);
        }
    }
}