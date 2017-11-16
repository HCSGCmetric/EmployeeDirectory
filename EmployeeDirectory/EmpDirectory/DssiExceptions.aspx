<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DssiExceptions.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.DssiExceptions" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
         <script>

             function validateAndConfirmAppDTCC() {
                 Page_ClientValidate("AppDTCCgroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmAppDTCCd() {
                 Page_ClientValidate("AppDTCCgroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmEndDTCC() {
                 Page_ClientValidate("EndDTCCgroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmEndDTCCd() {
                 Page_ClientValidate("EndDTCCgroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmEndDTAff() {
                 Page_ClientValidate("EndDTAffgroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmEndDTAffd() {
                 Page_ClientValidate("EndDTAffgroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmBackfeed() {
                 Page_ClientValidate("Backfeedgroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmBackfeedd() {
                 Page_ClientValidate("Backfeedgroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGBAccountCode() {
                 Page_ClientValidate("GBAccountCodegroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGBAccountCoded() {
                 Page_ClientValidate("GBAccountCodegroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGBAccountCodeHSKP() {
                 Page_ClientValidate("GBAccountCodeHSKPgroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGBAccountCodeHSKPd() {
                 Page_ClientValidate("GBAccountCodeHSKPgroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGBCustomerCode() {
                 Page_ClientValidate("GBCustomerCodegroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGBCustomerCoded() {
                 Page_ClientValidate("GBCustomerCodegroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }
             function validateAndConfirmFacilityCode() {
                 Page_ClientValidate("FacilityCodegroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmFacilityCoded() {
                 Page_ClientValidate("FacilityCodegroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGLAccountCodeGLChart() {
                 Page_ClientValidate("GLAccountCodeGLChartgroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGLAccountCodeGLChartd() {
                 Page_ClientValidate("GLAccountCodeGLChartgroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGLAC() {
                 Page_ClientValidate("GLACGroup");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

             function validateAndConfirmGLACd() {
                 Page_ClientValidate("GLACGroupd");
                 if (Page_IsValid) {
                     return confirm("Are you sure?");
                 }
                 return false;
             }

    </script>

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

     <asp:HiddenField ID="hiduser" runat="server" />
        <table style="width:100%"><tr><td style="width:80%"><h1>DSSI Exceptions Administration</h1></td><td style="width:20%"></td></tr></table>
        <table style="width:100%">
            <tr>
                <td colspan="3"><h2>Daily Invoices - Apply Date Rules <img src="img/info.png" data-toggle="tooltip" data-html="true" data-placement="right" title="<div style=text-align:left><B>Default Rules</B><ol><li>Period End Date will be used if CO2 does not equal FOOD.</li><li>Period End Date will be used with MGMT-4D and FD00.</li></ol>" /></h2></td>
            </tr>
            <tr>
                <td><h4>Customer Codes that will use 4-4-5 Date <img src="img/info.png" data-toggle="tooltip" data-html="true" data-placement="right" title="<div style=text-align:left><b>Rule 3</b><ol start=3><li> This is the list of Customers that will use the 445 Date from the Schedule Table (4-4-5)." /></h4></td>
                <td><h4>Management Codes that will use Period End Date <img src="img/info.png" data-toggle="tooltip" data-html="true" data-placement="right" title="<div style=text-align:left><b>Rule 4</b><ol start=4><li>This is the list of Management Codes that will use the Period End Date from the Current GL Period." /></h4></td>
                <td><h4>Customer Codes that will use Period End Date <img src="img/info.png" data-toggle="tooltip" data-html="true" data-placement="left" title="<div style=text-align:left><b>Rule 5</b><ol start=5><li>This is the list of Customers that will use the  Period End Date from the Current GL Period." /></h4></td>
                
            </tr>
            <tr>
                <td>
                    <asp:FormView ID="fvApplyDtCustCode" DataSourceID="SqlDataSource1" runat="server">
        <ItemTemplate>
            <table>
                <tr>
                    <td><asp:DropDownList ID="ddlApplyDtCustCode" class="dropdown" DataSourceID="SqlDataSource1" DataTextField="UseApplyDateCustCode" DataValueField="UseApplyDateCustCode" AppendDataBoundItems="true" runat="server"><asp:ListItem Text="Select a Customer Code" Value=""></asp:ListItem> </asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rqddlApplyDtCustCode" runat="server" ControlToValidate="ddlApplyDtCustCode" ErrorMessage="Select a Customer Code" ForeColor="Red" ValidationGroup="AppDTCCgroupd" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>            
                <tr>
                    <td><asp:Button ID="btnAddApplyDtCustCode" CssClass="btn btn-primary" OnClick="btnAddApplyDtCustCode_Click" runat="server" Text="Add" /> <asp:Button ID="btnDeleteApplyDtCustCode" CssClass="btn btn-danger" CausesValidation="true" ValidationGroup="AppDTCCgroupd" OnClientClick="return validateAndConfirmAppDTCCd();" OnClick="btnDeleteApplyDtCustCode_Click" runat="server" Text="Delete" /></td>
                </tr>
           </table>
            
        </ItemTemplate>
        <InsertItemTemplate>
            <table>
                <tr>
                    <td><asp:TextBox ID="txtApplyDtCustCode" CssClass="form-control" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rqApplyDtCustCode" runat="server" ErrorMessage="Enter a Customer Code" Display="Dynamic" ValidationGroup="AppDTCCgroup" ControlToValidate="txtApplyDtCustCode"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgapplygb" runat="server" ControlToValidate="txtApplyDtCustCode" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Correct Format<br>" ValidationGroup="AppDTCCgroup" ValidationExpression="[a-zA-Z]{5}\d{2}"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnCancelApplyDtCustCode" CssClass="btn btn-primary" OnClick="btnCancelApplyDtCustCode_Click" Text="Cancel" runat="server" /> <asp:Button ID="btnInsertApplyDtCustCode" CssClass="btn btn-primary" OnClick="btnInsertApplyDtCustCode_Click" OnClientClick="return validateAndConfirmAppDTCC();" ValidationGroup="AppDTCCgroup" CausesValidation="true" runat="server" Text="Insert" /></td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView>
                </td>
           
                 <td><asp:FormView ID="fvEndDtAff" DataSourceID="SqlDataSource3" runat="server">
        <ItemTemplate>
            <table>
                <tr>
                    <td><asp:DropDownList ID="ddlEndDtAff" class="dropdown" AppendDataBoundItems="true" DataSourceID="SqlDataSource3" DataTextField="UseEndDateAff" DataValueField="UseEndDateAff" runat="server"><asp:ListItem Value="" Text="Select a Management Code"></asp:ListItem> </asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rqddlEndDtAff" runat="server" ControlToValidate="ddlEndDtAff" ErrorMessage="Select a Management Code" ForeColor="Red" ValidationGroup="EndDTAffgroupd" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>            
                <tr>
                    <td><asp:Button ID="btnAddEndDtAff" CssClass="btn btn-primary" OnClick="btnAddEndDtAff_Click" runat="server" Text="Add" /> <asp:Button ID="btnDeleteEndDtAff" CssClass="btn btn-danger" CausesValidation="true" ValidationGroup="EndDTAffgroupd" OnClick="btnDeleteEndDtAff_Click" OnClientClick="return validateAndConfirmEndDTAffd();" runat="server" Text="Delete" /></td>
                </tr>
           
            </table>
        </ItemTemplate>
            <InsertItemTemplate>
            <table>
                <tr>
                    <td><asp:TextBox ID="txtEndDtAff" CssClass="form-control" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rqEndDtAff" runat="server" ErrorMessage="Enter a Management Code" Display="Dynamic" ValidationGroup="EndDTAffgroup" ControlToValidate="txtEndDtAff"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgenddtaff" runat="server" ControlToValidate="txtEndDtAff" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Correct Format<br>" ValidationGroup="EndDTAffgroup" ValidationExpression="^MGMT-.*"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnCancelEndDtAff" CssClass="btn btn-primary" OnClick="btnCancelEndDtAff_Click" Text="Cancel" runat="server" /> <asp:Button ID="btnInsertEndDtAff" CssClass="btn btn-primary" OnClick="btnInsertEndDtAff_Click" OnClientClick="return validateAndConfirmEndDTAff();" CausesValidation="true" ValidationGroup="EndDTAffgroup" runat="server" Text="Insert" /></td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView></td>
           
                <td><asp:FormView ID="fvEndDtCustCode" DataSourceID="SqlDataSource2" runat="server">
        <ItemTemplate>
            <table>
                <tr>
                    <td><asp:DropDownList ID="ddlEndDtCustCode" class="dropdown" DataSourceID="SqlDataSource2" DataTextField="UseEndDateCustCode" DataValueField="UseEndDateCustCode" AppendDataBoundItems="true" runat="server"><asp:ListItem Value="" Text="Select a Customer Code"></asp:ListItem> </asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rqddlEndDtCustCode" runat="server" ControlToValidate="ddlEndDtCustCode" ErrorMessage="Select a Customer Code" ForeColor="Red" ValidationGroup="EndDTCCgroupd" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>            
                <tr>
                    <td><asp:Button ID="btnAddEndDtCustCode" CssClass="btn btn-primary" OnClick="btnAddEndDtCustCode_Click" runat="server" Text="Add" /> <asp:Button ID="btnDeleteEndDtCustCode" CssClass="btn btn-danger" CausesValidation="true" ValidationGroup="EndDTCCgroupd" OnClick="btnDeleteEndDtCustCode_Click" OnClientClick="return validateAndConfirmEndDTCCd();" runat="server" Text="Delete" /></td>
                </tr>
           
            </table>
        </ItemTemplate>
            <InsertItemTemplate>
            <table>
                <tr>
                    <td><asp:TextBox ID="txtEndDtCustCode" CssClass="form-control" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rqEndDtCustCode" runat="server" ErrorMessage="Enter a Customer Code" Display="Dynamic" ValidationGroup="EndDTCCgroup" ControlToValidate="txtEndDtCustCode"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgCustenddt" runat="server" ControlToValidate="txtEndDtCustCode" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Correct Format<br>" ValidationGroup="EndDTCCgroup" ValidationExpression="[a-zA-Z]{5}\d{2}"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnCancelEndDtCustCode" CssClass="btn btn-primary" OnClick="btnCancelEndDtCustCode_Click" Text="Cancel" runat="server" /> <asp:Button ID="btnInsertEndDtCustCode" CssClass="btn btn-primary" OnClick="btnInsertEndDtCustCode_Click" OnClientClick="return validateAndConfirmEndDTCC();" ValidationGroup="EndDTCCgroup" CausesValidation="true" runat="server" Text="Insert" /></td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView></td>

               
            </tr>



        </table>
    
        <hr />
        

        <table>
            <tr>
                <td>
                    <h2>GL Account Codes</h2>
                </td>
            </tr>
            <tr>
                <td><h4>GL Account Codes that will be used in the following feeds:<br /><br />
                    <ul>
                        <li>Backfeed Invoices</li>
                        <li>Budgets (Food & Housekeeping)</li>
                        <li>GL Chart</li>
                    </ul></h4>
                </td>
            </tr>
            <tr>
                <td><asp:FormView ID="fvGLAC" runat="server" DataSourceID="SqlDataSource10">
                    <ItemTemplate>
                        <table>
                            <tr>
                                <td><asp:DropDownList ID="ddlGLAC" runat="server" DataTextField="seg_code" DataValueField="seg_code" DataSourceID="SqlDataSource10" AppendDataBoundItems="true">
                                    <asp:ListItem>Select an Account Code</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rqddlGlAC" runat="server" ControlToValidate="ddlGLAC" ErrorMessage="Select an Account Code" ForeColor="Red" ValidationGroup="GLACGroupd" Display="Dynamic"></asp:RequiredFieldValidator>
                                </td>
                            </tr>
                            <tr>
                                <td><asp:Button ID="btnAddGLAC" CssClass="btn btn-primary" OnClick="btnAddGLAC_Click" runat="server" Text="Add" /> <asp:Button ID="btnDeleteGLAC" CssClass="btn btn-danger" OnClientClick ="return validateandConfirmGLACd();" OnClick="btnDeleteGLAC_Click" CausesValidation="true" ValidationGroup="GLACGroupd" runat="server" Text="Delete" /></td>
                            </tr>
                        </table>
                    </ItemTemplate>
                    <InsertItemTemplate>
            <table>
                <tr>
                    <td><asp:TextBox ID="txtGLAC" CssClass="form-control" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rqGLExpAcct" ForeColor="Red" runat="server" ValidationGroup="GLACGroup" ErrorMessage="Enter an Account Code" Display="Dynamic" ControlToValidate="txtGLAC"></asp:RequiredFieldValidator>
                                                <asp:RegularExpressionValidator ID="rgCompany" runat="server" ControlToValidate="txtGLAC" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Correct Format<br>" ValidationGroup="GLACGroup" ValidationExpression="^\d{6}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnCancelGLAC" CssClass="btn btn-primary" OnClick="btnCancelGLAC_Click" Text="Cancel" runat="server" /> <asp:Button ID="btnInsertGLAC" CssClass="btn btn-primary" OnClick="btnInsertGLAC_Click" OnClientClick="return validateAndConfirmGLAC();" CausesValidation="true" ValidationGroup="GLACGroup" runat="server" Text="Insert" /></td>
                </tr>
            </table>
        </InsertItemTemplate>
                    </asp:FormView></td>
            </tr>
        </table>
        <hr />

       
     
        <table>
            <tr>
                <td><h2>Forum</h2></td>
            </tr>
            <tr>
                <td><h4>Forum Facilities <img src="img/info.png" data-toggle="tooltip" data-placement="right" title="This is the list of Forum/Sava Facilities.  They will be included in the HCG-Forum/Sava Facility/Hierarchy/Backfeed files feeds and excluded from HSG-Navigator files feeds." /></h4></td>
            </tr>
            <tr>
                <td> <asp:FormView ID="fvFacilityCode" DataSourceID="SqlDataSource8" runat="server">
        <ItemTemplate>
            <table>
                <tr>
                    <td><asp:DropDownList ID="ddlFacilityCode" class="dropdown" DataSourceID="SqlDataSource8" AppendDataBoundItems="true" DataTextField="FacilityCode" DataValueField="FacilityCode" runat="server">
                        <asp:ListItem Text="Select a Facility Code" Value=""></asp:ListItem>
                        </asp:DropDownList><br />
                        <asp:RequiredFieldValidator ID="rqddlFacilityCode" runat="server" ControlToValidate="ddlFacilityCode" ErrorMessage="Select a Facility Code" ForeColor="Red" ValidationGroup="FacilityCodegroupd" Display="Dynamic"></asp:RequiredFieldValidator>
                    </td>
                </tr>            
                <tr>
                    <td><asp:Button ID="btnAddFacilityCode" CssClass="btn btn-primary" OnClick="btnAddFacilityCode_Click" runat="server" Text="Add" /> <asp:Button ID="btnDeleteFacilityCode" CssClass="btn btn-danger" OnClientClick="return validateAndConfirmFacilityCoded();" OnClick="btnDeleteFacilityCode_Click" CausesValidation="true" ValidationGroup="FacilityCodegroupd" runat="server" Text="Delete" /></td>
                </tr>
           </table>
            
        </ItemTemplate>
        <InsertItemTemplate>
            <table>
                <tr>
                    <td><asp:TextBox ID="txtFacilityCode" CssClass="form-control" runat="server"></asp:TextBox><br />
                        <asp:RequiredFieldValidator ID="rqFacilityCode" ForeColor="Red" runat="server" ValidationGroup="FacilityCodegroup" ErrorMessage="Enter a Facility Code" Display="Dynamic" ControlToValidate="txtFacilityCode"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="rgfacility" runat="server" ControlToValidate="txtFacilityCode" Display="Dynamic" ForeColor="Red" ErrorMessage="Enter Correct Format<br>" ValidationGroup="FacilityCodegroup" ValidationExpression="0{2}-\w{3}$"></asp:RegularExpressionValidator>
                    </td>
                </tr>
                <tr>
                    <td><asp:Button ID="btnCancelFacilityCode" CssClass="btn btn-primary" OnClick="btnCancelFacilityCode_Click" Text="Cancel" runat="server" /> <asp:Button ID="btnInsertFacilityCode" CssClass="btn btn-primary" OnClick="btnInsertFacilityCode_Click" OnClientClick="return validateAndConfirmFacilityCode();" CausesValidation="true" ValidationGroup="FacilityCodegroup" runat="server" Text="Insert" /></td>
                </tr>
            </table>
        </InsertItemTemplate>
    </asp:FormView></td>
            </tr>
        </table>
        <hr />

     

       

        <asp:SqlDataSource ID="SqlDataSource1" runat="server" OnInserted="SqlDataSource1_Inserted" OnDeleted="SqlDataSource1_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT UseApplyDateCustCode FROM [HSG_DSSI_ExceptionDailyFeed] where UseApplyDateCustCode is not null order by UseApplyDateCustCode"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource2" runat="server" OnInserted="SqlDataSource2_Inserted" OnDeleted="SqlDataSource2_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT UseEndDateCustCode FROM [HSG_DSSI_ExceptionDailyFeed] where UseEndDateCustCode is not null order by UseEndDateCustCode"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource3" runat="server" OnInserted="SqlDataSource3_Inserted" OnDeleted="SqlDataSource3_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT UseEndDateAff FROM [HSG_DSSI_ExceptionDailyFeed] where UseEndDateAff is not null order by UseEndDateAff"></asp:SqlDataSource>
       <%-- <asp:SqlDataSource ID="SqlDataSource4" runat="server" OnInserted="SqlDataSource4_Inserted" OnDeleted="SqlDataSource4_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT [gl_exp_acct] FROM [HSG_DSSI_ExceptionBackfeed] order by gl_exp_acct"></asp:SqlDataSource>
         <asp:SqlDataSource ID="SqlDataSource5" runat="server" OnInserted="SqlDataSource5_Inserted" OnDeleted="SqlDataSource5_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT AccountCode FROM [HSG_DSSI_ExceptionBudget] where AccountCode is not null order by AccountCode"></asp:SqlDataSource>--%>
        <%-- <asp:SqlDataSource ID="SqlDataSource6" runat="server" OnInserted="SqlDataSource6_Inserted" OnDeleted="SqlDataSource6_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT AccountCodeHSKP FROM [HSG_DSSI_ExceptionBudget] where AccountCodeHSKP is not null order by AccountCodeHSKP"></asp:SqlDataSource>--%>
        <%--<asp:SqlDataSource ID="SqlDataSource7" runat="server" OnInserted="SqlDataSource7_Inserted" OnDeleted="SqlDataSource7_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT CustomerCode FROM [HSG_DSSI_ExceptionBudget] where CustomerCode is not null order by CustomerCode"></asp:SqlDataSource>--%>
         <asp:SqlDataSource ID="SqlDataSource8" runat="server" OnInserted="SqlDataSource8_Inserted" OnDeleted="SqlDataSource8_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT FacilityCode FROM [HSG_DSSI_ExceptionFacility] where FacilityCode is not null order by FacilityCode"></asp:SqlDataSource>
       <%-- <asp:SqlDataSource ID="SqlDataSource9" runat="server" OnInserted="SqlDataSource9_Inserted" OnDeleted="SqlDataSource9_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT seg_code FROM [HSG_DSSI_ExceptionGLChart] where seg_code is not null order by seg_code"></asp:SqlDataSource>--%>
         <asp:SqlDataSource ID="SqlDataSource10" runat="server" OnInserted="SqlDataSource10_Inserted" OnDeleted="SqlDataSource10_Deleted" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>" SelectCommand="SELECT seg_code FROM [HSG_DSSI_ExceptionGLCodes] where seg_code is not null order by seg_code"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlAudit" runat="server" ConnectionString="<%$ ConnectionStrings:HSGConnectionString %>"></asp:SqlDataSource>


    </div>

            </div>
       </div>
        
    </form>
        <script>
            $(document).ready(function () {
                $('[data-toggle="tooltip"]').tooltip();
            });
</script>
</body>
</html>
<script>

    $(document).ready(function () {

        var Liveurl = '<%= System.Configuration.ConfigurationManager.AppSettings["LivesubURL"] %>';
        Liveurl = "/" + Liveurl + "EmployeeDirectory/GetDSSIMenu";
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