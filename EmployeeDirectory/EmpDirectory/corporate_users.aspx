<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="corporate_users.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.corporate_users" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css"/>
    <link href="../css/bootstrap.min.css" rel="stylesheet"/>
    <link href="../css/sb-admin.css" rel="stylesheet"/>
    <link href="../css/main.css" rel="stylesheet"/>
    <link rel="stylesheet" href="../css/datepicker.css"/>
    <link href="../css/AdminLTE.min.css" rel="stylesheet" />
    <link href="../css/style.css" rel="stylesheet" />
  
    <%--<link href="css/sb-admin-rtl.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/sb-admin.css" rel="stylesheet" />--%>
   <script src="../Scripts/jquery-1.7.1.js"></script>
   <script src="../Scripts/jquery-ui-1.8.20.js"></script>
   <script src="../Scripts/_references.js"></script>
   <script src="../js/jquery.js"></script>
   <script src="../js/bootstrap.min.js"></script>

    <script type="text/javascript">
        function OnTextChangedHandler(s) {
            //alert('TextChanged. Text = ' + s.GetText());
            $.ajax({
                type: "POST",
                url: "corporate_users.aspx/GetEmployee",

                data: "{empid: '" + s.GetText() + "'}",

                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //alert("success");
                    //data = $.parseJSON(data);
                    var employee = data.d;
                    var theemp = employee.split(",")
                    Firstname.SetText(theemp[1]);
                    Lastname.SetText(theemp[2]);
                    JobTitle.SetText(theemp[0]);
                    VendorCode.SetText(theemp[3]);
                    CostCenter.SetText(theemp[4]);
                    //alert(data.d);
                    //allName = "";
                    //allVal = "";
                    //thecount = 0;
                    //$.map(data.d, function (full) {
                    //    allName += "," + full.Name.toUpperCase();
                    //    allVal += "," + full.Value;


                    //    thecount = thecount + 1;
                },
                error: function (data) {
                    alert("fail");
                }
            });


        }

        function OnValueChangedHandler(s) {
            //alert('TextChanged. Text = ' + s.GetText());
            $.ajax({
                type: "POST",
                url: "corporate_users.aspx/GetEmail",

                data: "{empid: '" + s.GetText() + "'}",

                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    //alert("success");
                    //data = $.parseJSON(data);
                    var employee = data.d;

                    aemail.SetText(employee);

                    //alert(data.d);
                    //allName = "";
                    //allVal = "";
                    //thecount = 0;
                    //$.map(data.d, function (full) {
                    //    allName += "," + full.Name.toUpperCase();
                    //    allVal += "," + full.Value;


                    //    thecount = thecount + 1;
                },
                error: function (data) {
                    alert("fail");
                }
            });


        }

        </script>
</head>
<body>
        <form id="form1" runat="server">
    <div id="wrapper">
   
           <nav class="navbar navbar-default navbar-fixed-top" role="navigation">
            <div class="container-fluid">
                 <div class="navbar-header">
                    <button aria-controls="navbar" aria-expanded="false" data-target="#navbar" data-toggle="collapse" class="navbar-toggle collapsed" type="button">
                        <span class="sr-only">Toggle navigation</span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                        <span class="icon-bar"></span>
                    </button>
                    <a href="../Home/DashBoard" class="navbar-brand"><img class="img-responsive" src="../img/HCSGLogo.png"></a>
                </div>
                <div class="navbar-collapse collapse" id="navbar">
                    <ul class="nav navbar-nav"></ul>
                    <ul class="nav navbar-nav navbar-right in">
                        <li>
                            <a aria-expanded="false" aria-haspopup="true" role="button" href="../Home/DashBoard">Home</a>
                        </li>





                            <li>
                                <a aria-expanded="false" aria-haspopup="true" role="button" data-toggle="dropdown" class="dropdown-toggle Userdropdown" href="#">
                                    <img class="img-responsive userimg" src="../img/User.png">
                                    <span class="user"><%= Session[EmployeeDirectory.Entity.AppConstant.NAME] %>  <span class="caret"></span></span>
                                </a>
                                <ul class="dropdown-menu">

                                    <li><a href="../Login/AdminSignOut" class="btn btn-default btn-flat">Log out</a></li>
                                </ul>
                            </li>
                       
                    </ul>
                </div><!--/.nav-collapse -->
            </div>

            
            <!-- Sidebar Menu Items - These collapse to the responsive navigation menu on small screens -->
          

        </nav>
       
        <div id="page-wrapper">
            



<div class="container-fluid">
    <h2>Corporate Users</h2>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" SelectCommand="SELECT * FROM [HSG_COUPA_Corporate_Users]" DeleteCommand="DELETE FROM [HSG_COUPA_Corporate_Users] WHERE [Employee_ID] = @Employee_ID" InsertCommand="if not exists (select * from [HSG_COUPA_Corporate_Users] cu where cu.Employee_ID = @Employee_ID)  INSERT INTO [HSG_COUPA_Corporate_Users] ([Employee_ID], [vendor_code], [USER], [Lastname], [Firstname], [Approver_Name], [Email], [Approver_1], [Cost_Center], [Job_Title], [UserRoles], [ContentGroups], [Inserted_Date]) VALUES (@Employee_ID, @vendor_code, @USER, @Lastname, @Firstname, @Approver_Name, @Email, @Approver_1, @Cost_Center, @Job_Title, @UserRoles, @ContentGroups, getDate())" UpdateCommand="UPDATE [HSG_COUPA_Corporate_Users] SET [vendor_code] = @vendor_code, [USER] = @USER, [Lastname] = @Lastname, [Firstname] = @Firstname, [Approver_Name] = @Approver_Name, [Email] = @Email, [Approver_1] = @Approver_1, [Cost_Center] = @Cost_Center, [Job_Title] = @Job_Title, [UserRoles] = @UserRoles, [ContentGroups] = @ContentGroups, [Updated_Date] = getdate() WHERE [Employee_ID] = @Employee_ID">
            <DeleteParameters>
                <asp:Parameter Name="Employee_ID" Type="String" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Employee_ID" Type="String" />
                <asp:Parameter Name="vendor_code" Type="String" />
                <asp:Parameter Name="USER" Type="String" />
                <asp:Parameter Name="Lastname" Type="String" />
                <asp:Parameter Name="Firstname" Type="String" />
                <asp:Parameter Name="Approver_Name" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Approver_1" Type="String" />
                <asp:Parameter Name="Cost_Center" Type="String" />
                <asp:Parameter Name="Job_Title" Type="String" />
                <asp:Parameter Name="UserRoles" Type="String" />
                <asp:Parameter Name="ContentGroups" Type="String" />
            </InsertParameters>
            <UpdateParameters>
                <asp:Parameter Name="vendor_code" Type="String" />
                <asp:Parameter Name="USER" Type="String" />
                <asp:Parameter Name="Lastname" Type="String" />
                <asp:Parameter Name="Firstname" Type="String" />
                <asp:Parameter Name="Approver_Name" Type="String" />
                <asp:Parameter Name="Email" Type="String" />
                <asp:Parameter Name="Approver_1" Type="String" />
                <asp:Parameter Name="Cost_Center" Type="String" />
                <asp:Parameter Name="Job_Title" Type="String" />
                <asp:Parameter Name="UserRoles" Type="String" />
                <asp:Parameter Name="ContentGroups" Type="String" />
                <asp:Parameter Name="Employee_ID" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" SelectCommand="SELECT Distinct USERROLES FROM [HSG_COUPA_Corporate_Users] where userroles is not null order by USERROLES"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" SelectCommand="SELECT DISTINCT ContentGroups FROM [HSG_COUPA_Corporate_Users] where contentgroups is not null order by ContentGroups"></asp:SqlDataSource>
    <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" SelectCommand="SELECT DISTINCT [USER] FROM [HSG_COUPA_Corporate_Users] where [USER] is not null order by [USER]"></asp:SqlDataSource>
        <dx:ASPxGridView ID="ASPxGridView1" Width="100%" SettingsBehavior-ConfirmDelete="true" OnCellEditorInitialize="ASPxGridView1_CellEditorInitialize" runat="server" KeyFieldName="Employee_ID" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Settings ShowFilterRow="True" VerticalScrollableHeight="680" />
            <Columns>
                
                <dx:GridViewCommandColumn ShowClearFilterButton="True" ShowDeleteButton="True" ShowEditButton="True" ShowNewButtonInHeader="True" VisibleIndex="0">
                </dx:GridViewCommandColumn>
                
                <dx:GridViewDataTextColumn PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" PropertiesTextEdit-ClientSideEvents-TextChanged="function(s, e) {
            OnTextChangedHandler(s);
        }" Caption="Employee ID" FieldName="Employee_ID" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Vendor Code" PropertiesTextEdit-ClientInstanceName="VendorCode" PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" FieldName="vendor_code" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="User Name" PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" FieldName="USER" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="First Name" PropertiesTextEdit-ClientInstanceName="Firstname" PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" FieldName="Firstname" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Last Name" PropertiesTextEdit-ClientInstanceName="Lastname" PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" FieldName="Lastname" VisibleIndex="5">
                </dx:GridViewDataTextColumn>
                
               
                <dx:GridViewDataTextColumn PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" FieldName="Email" VisibleIndex="6">
                </dx:GridViewDataTextColumn>

                <dx:GridViewDataComboBoxColumn PropertiesComboBox-ValueField="USER" PropertiesComboBox-ClientSideEvents-ValueChanged="function(s, e) {
            OnValueChangedHandler(s);
        }" FieldName="Approver_Name" VisibleIndex="7" Caption="Approver Name" PropertiesComboBox-TextField="USER" PropertiesComboBox-DataSourceID="SqlDataSource5">
                    
                </dx:GridViewDataComboBoxColumn>
             
                <dx:GridViewDataTextColumn FieldName="Approver_1" PropertiesTextEdit-ClientInstanceName="aemail" Caption="Approver Email" VisibleIndex="9">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" PropertiesTextEdit-ClientInstanceName="CostCenter" Caption="Cost Center" FieldName="Cost_Center" VisibleIndex="10">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn PropertiesTextEdit-ReadOnlyStyle-BackColor="LightGray" PropertiesTextEdit-ClientInstanceName="JobTitle" Caption="Job Title" FieldName="Job_Title" VisibleIndex="11">
                </dx:GridViewDataTextColumn>
              
                <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="SqlDataSource2" PropertiesComboBox-ValueField="UserRoles" PropertiesComboBox-TextField="UserRoles" FieldName="UserRoles" VisibleIndex="12"></dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn PropertiesComboBox-DataSourceID="SqlDataSource3" PropertiesComboBox-ValueField="ContentGroups" PropertiesComboBox-TextField="ContentGroups" FieldName="ContentGroups" VisibleIndex="13"></dx:GridViewDataComboBoxColumn>
            </Columns>
             <SettingsPager Mode="EndlessPaging" PageSize="100" />
        </dx:ASPxGridView>
        <br />
    </div>
            </div>
         
    </div>
    </form>

</body>
</html>

<script>
    $(document).ready(function () {

        var Liveurl = '<%= System.Configuration.ConfigurationManager.AppSettings["LivesubURL"] %>';
    Liveurl = "/" + Liveurl + "EmployeeDirectory/GetCoupaMenu";
    $("#modalWindow,#ui-loader").show();
    $.ajax({
        url: Liveurl,
        type: "GET",
        data: {},
        success: function (result) {
            $('.navbar-right').find("li:eq(0)").after(result);
            $("#modalWindow,#ui-loader").hide();

        }
    });

});

    </script>
