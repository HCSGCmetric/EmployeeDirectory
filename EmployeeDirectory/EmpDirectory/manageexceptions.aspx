<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="manageexceptions.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.manageexceptions" %>
<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
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
        <blockquote>
    <h2>Delete Exceptions</h2>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" DeleteCommand="delete from [hsg_DssiUser_ExceptionUpdate] where payroll_number = @payroll_number" SelectCommand="select payroll_number,[Firstname] + ' ' + [Lastname] as name from [hsg_DssiUser_ExceptionUpdate]">
            <DeleteParameters>
                <asp:Parameter Name="payroll_number" />
            </DeleteParameters>
        </asp:SqlDataSource>
    
     <dx:ASPxGridView ID="ASPxGridView1" SettingsBehavior-ConfirmDelete="true" Width ="70%" KeyFieldName="payroll_number" DataSourceID="SqlDataSource1" runat="server">
                <Settings ShowFilterRow="True" VerticalScrollableHeight="600" />
                <SettingsPager Mode="EndlessPaging" PageSize="550" />
            <Columns>
                
                <dx:GridViewCommandColumn ShowClearFilterButton="True" Caption=" " ShowDeleteButton="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                
                <dx:GridViewDataTextColumn PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" Caption="ID" FieldName="payroll_number" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Name" PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" FieldName="name" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                </Columns>
            </dx:ASPxGridView>
        <br />
         <asp:Button ID="btnClose" runat="server" OnClick="btnClose_Click" Text="Close" CssClass="btneffect" />
            </blockquote>
    </div>
    </form>
</body>
</html>
