using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace EmployeeDirectory.EmpDirectory
{
    public partial class noallow : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            hdValue.Value = ASPxComboBox1.SelectedItem.Value.ToString();
            SqlDSSIUser.InsertParameters["payroll_number"].DefaultValue = ASPxComboBox1.SelectedItem.Value.ToString();
            SqlDSSIUser.Insert();
        }

        protected void SqlDSSIUser_Inserted(object sender, SqlDataSourceStatusEventArgs e)
        {
            SqlAudit.InsertParameters["value"].DefaultValue = hdValue.Value;
            SqlAudit.Insert();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            string startUpScript = string.Format("window.parent.HidePopupAndShowInfo5('Server','{0}');", "tata");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "ANY_KEY", startUpScript, true);
        }
    }
}