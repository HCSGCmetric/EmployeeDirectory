<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="mobileuserexceptions.aspx.cs" Inherits="EmployeeDirectory.mobileuserexceptions" %>

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

           function OnTextChangedHandler(s) {
               //alert('TextChanged. Text = ' + s.GetText());


               $.ajax({
                   type: "POST",
                   url: "mobileuserexceptions.aspx/GetUserProps",

                   data: "{empid: '" + s.GetText() + "'}",

                   dataType: "json",
                   contentType: "application/json; charset=utf-8",
                   success: function (data) {
                       //alert("success");
                       //data = $.parseJSON(data);
                       var employee = data.d;
                       var theemp = employee.split(",")
                       FirstName.SetText(theemp[0]);
                       LastName.SetText(theemp[1]);
                       JobTitle.SetValue(theemp[2]);
                       Div.SetText(theemp[3]);
                       Reg.SetText(theemp[4]);
                       Dist.SetText(theemp[5]);
                       Sub.SetText(theemp[6]);
                       //aemail.SetText(employee);

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
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT * FROM [HSG_Users_exceptions]" InsertCommand="Insert into [HSG_Users_exceptions] (LASTNAME,FIRSTNAME,USERNAME,TITLE,DIVISION,SUBREGION,REGION,DISTRICT,LASTUPDATED,[APPROVE],[DENY],DISTRIBUTION_LIST,EEID,REPORT_ACCESS,UpdatedBy,EMAIL,affiliated_cust_code,updateinsert) values (@LASTNAME,@FIRSTNAME,@USERNAME,@TITLE,@DIVISION,@SUBREGION,@REGION,@DISTRICT,@LASTUPDATED,@APPROVE,@DENY,@DISTRIBUTION_LIST,@EEID,@REPORT_ACCESS,@UpdatedBy,@EMAIL,@affiliated_cust_code,@updateinsert)" UpdateCommand="update [HSG_Users_exceptions] set LASTNAME = @LASTNAME, FIRSTNAME = @FIRSTNAME, USERNAME = @USERNAME, TITLE = @TITLE, DIVISION = @DIVISION, SUBREGION = @SUBREGION, REGION = @REGION, DISTRICT = @DISTRICT, LASTUPDATED = @LASTUPDATED, [APPROVE] = @APPROVE, [DENY] = @DENY, DISTRIBUTION_LIST = @DISTRIBUTION_LIST, EEID = @EEID, REPORT_ACCESS = @REPORT_ACCESS, UpdatedBy = @UpdatedBy, EMAIL = @EMAIL, affiliated_cust_code = @affiliated_cust_code, updateinsert = @updateinsert where ID = @ID">
            <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:EmployeeDirectory.Helper.EncryptDecryptUtility.Decrypt(APPUSERSConnectionString) %>" SelectCommand="SELECT * FROM [HSG_Users_exceptions]" InsertCommand="Insert into [HSG_Users_exceptions] (LASTNAME,FIRSTNAME,USERNAME,TITLE,DIVISION,SUBREGION,REGION,DISTRICT,LASTUPDATED,[APPROVE],[DENY],DISTRIBUTION_LIST,EEID,REPORT_ACCESS,UpdatedBy,EMAIL,affiliated_cust_code,updateinsert) values (@LASTNAME,@FIRSTNAME,@USERNAME,@TITLE,@DIVISION,@SUBREGION,@REGION,@DISTRICT,@LASTUPDATED,@APPROVE,@DENY,@DISTRIBUTION_LIST,@EEID,@REPORT_ACCESS,@UpdatedBy,@EMAIL,@affiliated_cust_code,@updateinsert)" UpdateCommand="update [HSG_Users_exceptions] set LASTNAME = @LASTNAME, FIRSTNAME = @FIRSTNAME, USERNAME = @USERNAME, TITLE = @TITLE, DIVISION = @DIVISION, SUBREGION = @SUBREGION, REGION = @REGION, DISTRICT = @DISTRICT, LASTUPDATED = @LASTUPDATED, [APPROVE] = @APPROVE, [DENY] = @DENY, DISTRIBUTION_LIST = @DISTRIBUTION_LIST, EEID = @EEID, REPORT_ACCESS = @REPORT_ACCESS, UpdatedBy = @UpdatedBy, EMAIL = @EMAIL, affiliated_cust_code = @affiliated_cust_code, updateinsert = @updateinsert where ID = @ID">--%>
        
        <InsertParameters>
            <asp:Parameter Name="LASTNAME" />
            <asp:Parameter Name="FIRSTNAME" />
            <asp:Parameter Name="USERNAME" />
            <asp:Parameter Name="TITLE" />
            <asp:Parameter Name="DIVISION" />
            <asp:Parameter Name="SUBREGION" />
            <asp:Parameter Name="REGION" />
            <asp:Parameter Name="DISTRICT" />
            <asp:Parameter Name="LASTUPDATED" />
            <asp:Parameter Name="APPROVE" DefaultValue="0" />
            <asp:Parameter Name="DENY" DefaultValue="0" />
            <asp:Parameter Name="DISTRIBUTION_LIST" DefaultValue="0" />
            <asp:Parameter Name="EEID" />
            <asp:Parameter Name="REPORT_ACCESS" />
            <asp:Parameter Name="UpdatedBy" />
            <asp:Parameter Name="EMAIL" />
            <asp:Parameter Name="affiliated_cust_code" />
            <asp:Parameter Name="updateinsert" />
        </InsertParameters>
        <UpdateParameters>
            <asp:Parameter Name="LASTNAME" />
            <asp:Parameter Name="FIRSTNAME" />
            <asp:Parameter Name="USERNAME" />
            <asp:Parameter Name="TITLE" />
            <asp:Parameter Name="DIVISION" />
            <asp:Parameter Name="SUBREGION" />
            <asp:Parameter Name="REGION" />
            <asp:Parameter Name="DISTRICT" />
            <asp:Parameter Name="LASTUPDATED" />
            <asp:Parameter Name="APPROVE" />
            <asp:Parameter Name="DENY" />
            <asp:Parameter Name="DISTRIBUTION_LIST" />
            <asp:Parameter Name="EEID" />
            <asp:Parameter Name="REPORT_ACCESS" />
            <asp:Parameter Name="UpdatedBy" />
            <asp:Parameter Name="EMAIL" />
            <asp:Parameter Name="affiliated_cust_code" />
            <asp:Parameter Name="updateinsert" />
            <asp:Parameter Name="ID" />
        </UpdateParameters>
    </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT distinct (TITLE) as TITLE FROM [HSG_Users_exceptions] order by TITLE"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT distinct (UpdatedBy) as UpdatedBy FROM [HSG_Users_exceptions] order by UpdatedBy"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT distinct (affiliated_cust_code) as affiliated_cust_code FROM [HSG_Users_exceptions] order by affiliated_cust_code"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:APPUSERSConnectionString %>" SelectCommand="SELECT distinct (updateinsert) as updateinsert FROM [HSG_Users_exceptions] order by updateinsert"></asp:SqlDataSource>
        
             <h2 class="page-header">Mobile User Exceptions</h2>

        <dx:ASPxGridView ID="gvProducts" Width="100%" SettingsPager-PageSize="30" Styles-RowHotTrack-BackColor="#ffffcc" Styles-AlternatingRow-BackColor="#F3F2F3"  KeyFieldName="ID" Settings-ShowFilterRow="true" DataSourceID="SqlDataSource1" runat="server">
            <Columns>
                 <dx:GridViewCommandColumn ShowNewButtonInHeader="true" ShowEditButton="true" VisibleIndex="0" />
                 <dx:GridViewDataTextColumn FieldName="EEID" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ClientSideEvents-TextChanged="function(s, e) {
            OnTextChangedHandler(s);
        }"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn PropertiesTextEdit-ClientInstanceName="LastName" FieldName="LASTNAME"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FIRSTNAME" PropertiesTextEdit-ClientInstanceName="FirstName"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="USERNAME"></dx:GridViewDataTextColumn>
             <dx:GridViewDataComboBoxColumn FieldName="TITLE" PropertiesComboBox-ClientInstanceName="JobTitle" VisibleIndex="3">
                    <PropertiesComboBox DataSourceID="SqlDataSource2" ValueField="TITLE" TextField="TITLE" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                        <ClearButton DisplayMode="OnHover"></ClearButton>
                    </PropertiesComboBox>
                    
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataTextColumn PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ClientInstanceName="Div" FieldName="DIVISION"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ClientInstanceName="Sub" FieldName="SUBREGION"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ClientInstanceName="Reg" FieldName="REGION"></dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ClientInstanceName="Dist" FieldName="DISTRICT"></dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="APPROVE"><PropertiesCheckEdit ValueUnchecked="False"></PropertiesCheckEdit></dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="DENY"><PropertiesCheckEdit ValueUnchecked="False"></PropertiesCheckEdit></dx:GridViewDataCheckColumn>
                <dx:GridViewDataCheckColumn FieldName="DISTRIBUTION_LIST"><PropertiesCheckEdit ValueUnchecked="False"></PropertiesCheckEdit></dx:GridViewDataCheckColumn>
                <dx:GridViewDataTextColumn FieldName="EEID" PropertiesTextEdit-ValidationSettings-RequiredField-IsRequired="true" PropertiesTextEdit-ClientSideEvents-TextChanged="function(s, e) {
            OnTextChangedHandler(s);
        }"></dx:GridViewDataTextColumn>
                <dx:GridViewDataCheckColumn FieldName="REPORT_ACCESS"></dx:GridViewDataCheckColumn>
                 
                <dx:GridViewDataTextColumn FieldName="EMAIL"></dx:GridViewDataTextColumn>
                <dx:GridViewDataComboBoxColumn FieldName="affiliated_cust_code">
                    <PropertiesComboBox DataSourceID="SqlDataSource4" ValueField="affiliated_cust_code" TextField="affiliated_cust_code" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                        <ClearButton DisplayMode="OnHover"></ClearButton>
                    </PropertiesComboBox>
                    
                </dx:GridViewDataComboBoxColumn>
                <dx:GridViewDataComboBoxColumn FieldName="updateinsert">
                    <PropertiesComboBox DataSourceID="SqlDataSource5" ValidationSettings-RequiredField-IsRequired="true" ValidationSettings-ErrorText="Required" ValueField="updateinsert" TextField="updateinsert" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith">
                        <ClearButton DisplayMode="OnHover"></ClearButton>
                    </PropertiesComboBox>
                    
                </dx:GridViewDataComboBoxColumn>
                </Columns>
            <SettingsBehavior EnableRowHotTrack="true" />
            </dx:ASPxGridView>
    </div>
        </div>
    </form>

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

          .dxgvHeader_DefaultNew a.dxgvCommandColumnItem_DefaultNew
        {
            color:white;
        }

    </style>

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

