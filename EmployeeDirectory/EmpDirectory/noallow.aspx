<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="noallow.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.noallow" %>
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
    <asp:SqlDataSource ID="SqlDSSIUser" runat="server" OnInserted="SqlDSSIUser_Inserted" SelectCommand="SELECT distinct [EE ID] as payroll_number,[Firstname] + ' ' + [Lastname] as name FROM [HSGEmployeeDirectory].[dbo].EmployeeMaster_VW where [TERM DATE] is null and [JOB TITLE] in ('District Manager', 'District Dietician', 'District Dietitian','Regional Manager', 'Regional Director', 'Manager in Training', 'Regional Dietician' , 'Regional Dietitian','Divisional Manager', 'Divisional Director', 'Clerical Admin Support') and COID = '722' and SA = 'A' and [EE ID] not in (select payroll_number from [HSGOPS].[dbo].[hsg_DssiUser_ExceptionDelete]) or [EE ID] in (select payroll_number from [HSGOPS].[dbo].[hsg_DssiUser_ExceptionUpdate])" InsertCommand="insert into hsg_DssiUser_ExceptionDelete (payroll_number) values (@payroll_number)" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>">
            <InsertParameters>
                <asp:Parameter Name="payroll_number" />
            </InsertParameters>
        </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlAudit" runat="server" InsertCommand="Insert into HSG_DSSIUser_Exceptionsaudit (action,value,type,theuser) values ('delete',@value,'paynum',@user)" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>">
             <InsertParameters>
                 <asp:Parameter Name="value" />
                 <asp:SessionParameter Name="user" SessionField="user" />
             </InsertParameters>
         </asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource1" runat="server" SelectCommand="SELECT distinct [EE ID] as payroll_number,[Firstname] + ' ' + [Lastname] as name FROM [HSGEmployeeDirectory].[dbo].EmployeeMaster_VW where [EE ID] in (select payroll_number from [HSGOPS].[dbo].[hsg_DssiUser_ExceptionDelete])" DeleteCommand="delete from [HSGOPS].[dbo].[hsg_DssiUser_ExceptionDelete] where payroll_number = @payroll_number" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>">
            <DeleteParameters>
                <asp:Parameter Name="payroll_number" />
            </DeleteParameters>
        </asp:SqlDataSource>
        <br /><blockquote>
        <table><tr><td><dx:ASPxComboBox ID="ASPxComboBox1" runat="server" Width="300" ValueField="payroll_number" TextFormatString="{1}" IncrementalFilteringMode="Contains" DataSourceID="SqlDSSIUser">
            <Columns>
                <dx:ListBoxColumn FieldName="payroll_number" Caption="Payroll Number"></dx:ListBoxColumn>
                <dx:ListBoxColumn FieldName="name" Caption="Name"></dx:ListBoxColumn>
               
            </Columns>
        </dx:ASPxComboBox></td><td>&nbsp;&nbsp;&nbsp;</td><td><asp:Button runat="server" CssClass="btneffect" ID="btnRemove" OnClick="btnRemove_Click" Text="Exclude User"></asp:Button></td></tr></table>
        
            <h2>Current Excluded Users</h2>
            <dx:ASPxGridView ID="ASPxGridView1" SettingsBehavior-ConfirmDelete="true" Width ="70%" KeyFieldName="payroll_number" DataSourceID="SqlDataSource1" runat="server">
                <Settings ShowFilterRow="True" VerticalScrollableHeight="200" />
                <SettingsPager Mode="EndlessPaging" PageSize="100" />
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
        <asp:HiddenField ID="hdValue" runat="server" />
        

    </div>
    </form>
</body>
</html>
