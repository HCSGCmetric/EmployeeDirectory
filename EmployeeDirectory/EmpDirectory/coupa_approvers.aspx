<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="coupa_approvers.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.coupa_approvers" %>
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
        <h2>Coupa Approvers</h2>
     <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" SelectCommand="SELECT * FROM [HSG_COUPA_APPROVERS]"></asp:SqlDataSource>
    <dx:ASPxGridView ID="ASPxGridView1" Width="100%" runat="server" KeyFieldName="id" AutoGenerateColumns="False" DataSourceID="SqlDataSource1">
            <Settings ShowFilterRow="True" VerticalScrollableHeight="700" />
        <SettingsPager Mode="EndlessPaging" PageSize="100" />
            <SettingsDataSecurity AllowDelete="False" AllowEdit="False" AllowInsert="False" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="DIVISION" VisibleIndex="0">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="REGION" VisibleIndex="1">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="DISTRICT" VisibleIndex="2">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="USERNAME" VisibleIndex="3">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="TITLE" VisibleIndex="4">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="LASTUPDATED" VisibleIndex="5">
                </dx:GridViewDataDateColumn>
            </Columns>
        </dx:ASPxGridView>
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