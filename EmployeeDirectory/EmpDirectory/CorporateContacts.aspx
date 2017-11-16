<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="corporatecontacts.aspx.cs" Inherits="EmployeeDirectory.corporatecontacts" %>

<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" type="text/css" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/sb-admin.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    <link rel="stylesheet" href="../css/datepicker.css" />
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

    <script>
    $(document).ready(function () {

        $(".dxbButtonSys:link").css({ "font-size": "10pt" });
        $(".dxbButtonSys:link").css({ "color": "#3C8DBC" });
        $(".dxbButtonSys:link").css({ "font-weight": "normal" });
        $(".dxbButtonSys:visited").css({ "font-size": "10pt" });
        $(".dxbButtonSys:visited").css({ "color": "#3C8DBC" });
        $(".dxbButtonSys:visited").css({ "font-weight": "normal" });
        $(".dxbButton_DefaultNew.dxgvCommandColumnItem_DefaultNew.dxbButtonSys").css({ "text-decoration": "none" });
        $("#ASPxGridView1_DXCBtn0").css({ "color": "White" });
        $("#ASPxGridView1_DXCBtn0").css({ "font-size": "10pt" });
        $("#ASPxGridView1_DXCBtn0:visited").css({ "color": "White" });
        $("#ASPxGridView1_DXCBtn0:visited").css({ "font-size": "10pt" });
        $("#ASPxGridView1_DXCBtn0:active").css({ "color": "White" });
        $("#ASPxGridView1_DXCBtn0:active").css({ "font-size": "10pt" });
        $("#btnExport:link").css({ "font-size": "10pt" });
        $("#btnExport:link").css({ "color": "White" });
        $("#btnExport:link").css({ "font-weight": "Bold" });
        $("#btnExport:hover").css({ "background-color": "#ED6C44" });
        $("#btnExport:visited").css({ "font-size": "10pt" });
        $("#btnExport:visited").css({ "color": "White" });
        $("#btnExport:visited").css({ "font-weight": "Bold" });

    });

    </script>
    <script type="text/javascript">
    function gvProducts_EndCallback(s, e) {
        if (s.cpShowDeleteConfirmBox)
            pcConfirm.Show();
    }

    function Yes_Click() {
        pcConfirm.Hide();
        gvProducts.DeleteRow(gvProducts.cpRowIndex);
    }

    function No_Click() {
        pcConfirm.Hide()
    }
    </script>

    <style>
        .btneffect {
            background: rgb(237, 108, 68) !important;
            color: #fff !important;
            font-weight: bold !important;
            font-size: 15px !important;
            border: 1px solid !important;
            box-shadow: inset 0 0 20px rgba(255, 255, 255, 0) !important;
            outline: 1px solid !important;
            outline-color: rgba(237, 108, 68, .5) !important;
            outline-offset: 0px !important;
            text-shadow: none !important;
            transition: all 1250ms cubic-bezier(0.19, 1, 0.22, 1) !important;
            padding: 6px 30px !important;
            font-family: 'Raleway', sans-serif !important;
            text-align: center !important;
            margin-top: 0px !important;
        }
    </style>

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
                        <a href="../Home/DashBoard" class="navbar-brand">
                            <img class="img-responsive" src="../img/HCSGLogo.png"></a>
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
                    </div>
                    <!--/.nav-collapse -->
                </div>


                <!-- Sidebar Menu Items - These collapse to the responsive navigation menu on small screens -->


            </nav>
            <div id="page-wrapper">

                <div>
                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HSGEmployeeDirectory %>" InsertCommand="Insert into [CorporateContacts] (FirstName,LastName,Department,JobTitle,BusinessPhone,FaxNumber,Extension,EmailAddress,Office) values (@FirstName,@LastName,@Department,@JobTitle,@BusinessPhone,@FaxNumber,@Extension,@EmailAddress,@Office)" UpdateCommand="update [CorporateContacts] set FirstName = @FirstName, LastName = @LastName, Department = @Department, JobTitle = @JobTitle, BusinessPhone = @BusinessPhone, FaxNumber = @FaxNumber, Extension = @Extension, EmailAddress= @EmailAddress, Office = @Office  where id=@id" DeleteCommand="Delete FROM [CorporateContacts] where id=@id" SelectCommand="SELECT * FROM [CorporateContacts]">
                        <InsertParameters>
                            <asp:Parameter Name="FirstName" />
                            <asp:Parameter Name="LastName" />
                            <asp:Parameter Name="Department" />
                            <asp:Parameter Name="JobTitle" />
                            <asp:Parameter Name="BusinessPhone" />
                            <asp:Parameter Name="FaxNumber" />
                            <asp:Parameter Name="Extension" />
                            <asp:Parameter Name="EmailAddress" />
                            <asp:Parameter Name="Office" />
                        </InsertParameters>
                        <UpdateParameters>
                            <asp:Parameter Name="FirstName" />
                            <asp:Parameter Name="LastName" />
                            <asp:Parameter Name="Department" />
                            <asp:Parameter Name="JobTitle" />
                            <asp:Parameter Name="BusinessPhone" />
                            <asp:Parameter Name="FaxNumber" />
                            <asp:Parameter Name="Extension" />
                            <asp:Parameter Name="EmailAddress" />
                            <asp:Parameter Name="Office" />
                            <asp:Parameter Name="id" />
                        </UpdateParameters>
                        <DeleteParameters>
                            <asp:Parameter Name="id" />
                        </DeleteParameters>
                    </asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HSGEmployeeDirectory %>" SelectCommand="SELECT distinct Department FROM [CorporateContacts] order by Department"></asp:SqlDataSource>
                    <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HSGEmployeeDirectory %>" SelectCommand="SELECT distinct Office FROM [CorporateContacts] order by Office"></asp:SqlDataSource>
                </div>
                <asp:Button ID="btnExportb" CssClass="btneffect" Text="Export to Excel" runat="server" OnClick="btnExport_Click" />


                <dx:ASPxGridView ID="gvProducts" OnInit="gvProducts_Init" OnCustomButtonCallback="gvProducts_CustomButtonCallback"  ClientInstanceName="gvProducts" Width="100%" Styles-RowHotTrack-BackColor="#ffffcc" SettingsPager-PageSize="50" Styles-AlternatingRow-BackColor="#F3F2F3" KeyFieldName="id" Settings-ShowFilterRow="true" DataSourceID="SqlDataSource1" runat="server" OnCustomButtonInitialize="gvProducts_CustomButtonInitialize">
                    <SettingsCommandButton>
                        <ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

                        <HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
                    </SettingsCommandButton>
                    <Columns>

                        <dx:GridViewCommandColumn ShowNewButtonInHeader="true" VisibleIndex="0">

                            <CustomButtons>

                                <dx:GridViewCommandColumnCustomButton ID="btDelete" Text="Delete" Visibility="AllDataRows"></dx:GridViewCommandColumnCustomButton>
                            </CustomButtons>

                        </dx:GridViewCommandColumn>
                        <dx:GridViewCommandColumn ShowEditButton="true" VisibleIndex="0" />
                        <dx:GridViewDataTextColumn FieldName="FirstName" VisibleIndex="1">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="LastName" VisibleIndex="2">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="Department" VisibleIndex="3">
                            <PropertiesComboBox DataSourceID="SqlDataSource2" ValueField="Department" TextField="Department" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                                <ClearButton DisplayMode="OnHover"></ClearButton>
                            </PropertiesComboBox>

                        </dx:GridViewDataComboBoxColumn>
                        <dx:GridViewDataTextColumn FieldName="JobTitle" VisibleIndex="4">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="BusinessPhone" VisibleIndex="5">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="FaxNumber" VisibleIndex="6">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="Extension" VisibleIndex="7">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn FieldName="EmailAddress" VisibleIndex="8">
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataComboBoxColumn FieldName="Office" VisibleIndex="9">
                            <PropertiesComboBox DataSourceID="SqlDataSource3" ValueField="Office" TextField="Office" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                                <ClearButton DisplayMode="OnHover"></ClearButton>
                            </PropertiesComboBox>
                        </dx:GridViewDataComboBoxColumn>
                    </Columns>
                    <ClientSideEvents EndCallback="gvProducts_EndCallback" />
                    <SettingsBehavior EnableRowHotTrack="true" />
                </dx:ASPxGridView>

                <%--<dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="ASPxGridView1"></dx:ASPxGridViewExporter>--%>
                <dx:ASPxGridViewExporter ID="gridExport" runat="server" GridViewID="gvProducts"></dx:ASPxGridViewExporter>
            </div>
            <dx:ASPxPopupControl ID="pcConfirm" runat="server" ClientInstanceName="pcConfirm"
                Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
                HeaderText="Row Deleting">
                <ContentCollection>
                    <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                        <table width="100%">
                            <tr>
                                <td colspan="2" align="center">Delete Row?
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <a href="javascript:Yes_Click()">Yes</a>
                                </td>
                                <td align="center">
                                    <a href="javascript:No_Click()">No</a>
                                </td>
                            </tr>
                        </table>
                    </dx:PopupControlContentControl>
                </ContentCollection>
            </dx:ASPxPopupControl>
        </div>
    </form>

</body>
</html>


<style>
    /*.dxp-lead dxp-summary {
            color:black;
        }
        .dxp-num {
            color:black;
        }

        .dxbButtonSys{
            font-weight:normal;
            font-size:10pt;
            color: #3C8DBC;
        }

        .dxbButton_DefaultNew.dxgvCommandColumnItem_DefaultNew.dxbButtonSys{
            text-decoration:none;
        }

        #ASPxGridView1_DXCBtn0
        {
            font-size:10pt;
            color:white;
        }*/
    .dxgvHeader_DefaultNew a.dxgvCommandColumnItem_DefaultNew {
        color: white;
    }
</style>

<script>
    $('.datepicker').datepicker({
        
    format: 'mmddyyyy',
        
        startDate: '-3d'
        
    })

</script>
