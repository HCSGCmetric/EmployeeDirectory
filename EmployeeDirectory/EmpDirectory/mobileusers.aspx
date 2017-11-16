<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobileusers.aspx.cs" Inherits="EmployeeDirectory.mobileusers" %>

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
                      <%--   <li>
                            <a aria-expanded="false" aria-haspopup="true" role="button" href="../FormPermission/CreateFormPermission">Form Permission</a>
                        </li>
                        <li>
                            <a aria-expanded="false" aria-haspopup="true" role="button" href="../EmpDirectory/MobileUsers.aspx">Mobile Users</a>
                        </li>
                        <li>
                            <a aria-expanded="false" aria-haspopup="true" role="button" href="../EmpDirectory/MobileUserexceptions.aspx">Mobile User Exceptions</a>
                        </li>--%>

                        <li>
                            <a aria-expanded="false" aria-haspopup="true" role="button" data-toggle="dropdown" class="dropdown-toggle Userdropdown" href="#">
                                <img class="img-responsive userimg" src="../img/User.png"> <span class="user"><%= Session[EmployeeDirectory.Entity.AppConstant.NAME] %>  <span class="caret"></span></span>
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

        <div id="page-wrapper" style="width:1500px">

    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT * FROM [HSG_Users]"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT distinct (TITLE) as TITLE FROM [HSG_Users] order by TITLE"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT distinct (UpdatedBy) as UpdatedBy FROM [HSG_Users] order by UpdatedBy"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT distinct (affiliated_cust_code) as affiliated_cust_code FROM [HSG_Users] order by affiliated_cust_code"></asp:SqlDataSource>
        
             <h2 class="page-header">Mobile Users</h2>

        <dx:ASPxGridView ID="ASPxGridView1" Width="100%" Styles-RowHotTrack-BackColor="#ffffcc" Styles-AlternatingRow-BackColor="#F3F2F3" SettingsPager-PageSize="30" KeyFieldName="id" Settings-ShowFilterRow="true" DataSourceID="SqlDataSource1" runat="server">
            <Columns>
                <dx:GridViewDataTextColumn FieldName="LASTNAME"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FIRSTNAME"></dx:GridViewDataTextColumn>
             <dx:GridViewDataComboBoxColumn FieldName="TITLE" VisibleIndex="3">
                    <PropertiesComboBox DataSourceID="SqlDataSource2" ValueField="TITLE" TextField="TITLE" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                        <ClearButton DisplayMode="OnHover"></ClearButton>
                    </PropertiesComboBox>
                    
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn FieldName="DIVISION"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="SUBREGION"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="REGION"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DISTRICT"></dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="APPROVE"></dx:GridViewDataCheckColumn>
                 <dx:GridViewDataCheckColumn FieldName="DENY"></dx:GridViewDataCheckColumn>
                 <dx:GridViewDataCheckColumn FieldName="DISTRIBUTION_LIST"></dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="EEID"></dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="REPORT_ACCESS"></dx:GridViewDataCheckColumn>
                 <dx:GridViewDataComboBoxColumn FieldName="UpdatedBy">
                    <PropertiesComboBox DataSourceID="SqlDataSource3" ValueField="UpdatedBy" TextField="UpdatedBy" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                        <ClearButton DisplayMode="OnHover"></ClearButton>
                    </PropertiesComboBox>
                    
                </dx:GridViewDataComboBoxColumn>
                 <dx:GridViewDataTextColumn FieldName="EMAIL"></dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="affiliated_cust_code">
                    <PropertiesComboBox DataSourceID="SqlDataSource4" ValueField="affiliated_cust_code" TextField="affiliated_cust_code" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                        <ClearButton DisplayMode="OnHover"></ClearButton>
                    </PropertiesComboBox>
                    
                </dx:GridViewDataComboBoxColumn>
                </Columns>
            <SettingsBehavior EnableRowHotTrack="true" />
            </dx:ASPxGridView>
    </div>
        </div>
    </form>
</body>
</html>

<script>

    $(document).ready(function () {

        var Liveurl = '<%= System.Configuration.ConfigurationManager.AppSettings["LivesubURL"] %>';
        Liveurl = "/" + Liveurl + "EmployeeDirectory/GetMobileMenu";
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
