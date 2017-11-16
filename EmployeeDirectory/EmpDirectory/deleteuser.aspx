<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="deleteuser.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.deleteuser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/new.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    
    <link href="../css/style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <h2>Are you sure want to delete user?</h2>
        <table><tr><td><asp:Button ID="btnYes" OnClick="btnYes_Click" runat="server" CssClass="btneffect" Text="Yes" /></td><td><asp:Button ID="btnNo" OnClick="btnNo_Click" runat="server" CssClass="btneffect" Text="No" /></td></tr></table>
    
    <asp:SqlDataSource ID="SqlDataSource1" OnDeleted="SqlDataSource1_Deleted" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" DeleteCommandType="StoredProcedure" DeleteCommand="sp_hsg_dssi_delete_users">
        <DeleteParameters>
            <asp:QueryStringParameter Name="paynum" QueryStringField="keys" />
        </DeleteParameters>
    </asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlAudit" runat="server" OnInserted="SqlAudit_Inserted" InsertCommand="Insert into HSG_DSSIUser_Exceptionsaudit (action,value,type,theuser) values ('delete',@value,'paynum',@user)" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>">
             <InsertParameters>
                 <asp:QueryStringParameter Name="value" QueryStringField="keys" />
                 <asp:SessionParameter Name="user" SessionField="user" />
             </InsertParameters>
         </asp:SqlDataSource>
    </div>
    </form>
</body>
</html>
