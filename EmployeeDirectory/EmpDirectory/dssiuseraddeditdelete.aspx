<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dssiuseraddeditdelete.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.dssiuseraddeditdelete" %>
<%@ Register Assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    
    
    <script type="text/javascript">

        function ShowEditPopup(payroll_number) {
            popup.SetWidth(1400);
            //popup.SetWidth(250);
            popup.SetHeight(800);

            popup.SetContentUrl('edituser.aspx?keys=' + payroll_number);

            popup
            popup.Show();

        }

        function ShowAddPopup() {
            popup2.SetWidth(1400);
            //popup.SetWidth(250);
            popup2.SetHeight(800);
            popup2.SetContentUrl('adduser.aspx');
            popup2
            popup2.Show();

        }

        function ShowManageExceptionsPopup() {
            popup6.SetWidth(1400);
            //popup.SetWidth(250);
            popup6.SetHeight(850);
            popup6.SetContentUrl('manageexceptions.aspx');
            popup6
            popup6.Show();

        }

        function ShowDeletePopup(payroll_number) {
            popup3.SetWidth(475);
            //popup.SetWidth(250);
            popup3.SetHeight(200);

            popup3.SetContentUrl('deleteuser.aspx?keys=' + payroll_number);

            popup3
            popup3.Show();

        }

        function ShowRemoveUpdatePopup(payroll_number) {
            popup4.SetWidth(475);
            //popup.SetWidth(250);
            popup4.SetHeight(200);

            popup4.SetContentUrl('removeupdate.aspx?keys=' + payroll_number);

            popup4
            popup4.Show();

        }

        function ShowNoAllowPopup(payroll_number) {
            popup5.SetWidth(800);
            //popup.SetWidth(250);
            popup5.SetHeight(600);

            popup5.SetContentUrl('noallow.aspx');

            popup5
            popup5.Show();

        }


        function OnPopupShown() {
            var d = document.getElementById('Container');
            d.style.overflow = '';
            d.style.width = '';
            d.style.height = '';
        }


        function HidePopupAndShowInfo(closedBy, returnValue) {
            popup.Hide();
            grid1.Refresh();

            //alert('Closed By: ' + closedBy + '\nReturn Value: ' + returnValue);
        }



        function HidePopupAndShowInfo2(closedBy, returnValue) {
            popup2.Hide();
            grid1.Refresh();

            //alert('Closed By: ' + closedBy + '\nReturn Value: ' + returnValue);
        }

        function HidePopupAndShowInfo3(closedBy, returnValue) {
            popup3.Hide();
            grid1.Refresh();

            //alert('Closed By: ' + closedBy + '\nReturn Value: ' + returnValue);
        }

        function HidePopupAndShowInfo4(closedBy, returnValue) {
            popup4.Hide();
            grid1.Refresh();

            //alert('Closed By: ' + closedBy + '\nReturn Value: ' + returnValue);
        }

        function HidePopupAndShowInfo5(closedBy, returnValue) {
            popup5.Hide();
            grid1.Refresh();

            //alert('Closed By: ' + closedBy + '\nReturn Value: ' + returnValue);
        }

        function HidePopupAndShowInfo6(closedBy, returnValue) {
            popup6.Hide();
            grid1.Refresh();

            //alert('Closed By: ' + closedBy + '\nReturn Value: ' + returnValue);
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

    <table style="width:100%"><tr><td style="width:80%"><h1>DSSI User Administration</h1></td><td style="width:20%"></td></tr></table>
        <button type="button" class="btneffect" onclick="ShowAddPopup()">Add New User</button><button type="button" class="btneffect" onclick="ShowNoAllowPopup()">Manage Excluded Users</button><button type="button" class="btneffect" onclick="ShowManageExceptionsPopup()">Manage Exceptions</button>
        <dx:ASPxGridView ID="ASPxGridView1" runat="server" Width="100%" ClientInstanceName="grid1" AutoGenerateColumns="False" DataSourceID="SqlDSSIUser" KeyFieldName="payroll_number" >
            <Columns>
               <%-- <dx:GridViewDataHyperLinkColumn FieldName="payroll_number" Caption=" " Settings-AllowAutoFilter="False" AdaptivePriority="1" VisibleIndex="0">
              <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:ShowDeletePopup('{0}')" Text="Delete"></PropertiesHyperLinkEdit>
                  </dx:GridViewDataHyperLinkColumn>--%>

                  <dx:GridViewDataHyperLinkColumn FieldName="payroll_number" Caption=" " Settings-AllowAutoFilter="False" AdaptivePriority="1" VisibleIndex="1">
              <PropertiesHyperLinkEdit NavigateUrlFormatString="javascript:ShowEditPopup('{0}')" Text="Edit"></PropertiesHyperLinkEdit>
                  </dx:GridViewDataHyperLinkColumn>

              <%--  <dx:GridViewDataTextColumn FieldName="updated" Caption=" " Settings-AllowAutoFilter="False" AdaptivePriority="1" VisibleIndex="1">
                    <DataItemTemplate>
                        <asp:HyperLink ID="hp" runat="server" Visible='<%# Convert.ToBoolean(Eval("updated")) %>'  NavigateUrl="javascript:ShowRemoveUpdatePopup('payroll')" Text="Remove Update"></asp:HyperLink>
                    </DataItemTemplate>
                 </dx:GridViewDataTextColumn>--%>
            
                <dx:GridViewDataTextColumn FieldName="lastname" Caption="Last Name" VisibleIndex="1">
                <EditFormSettings VisibleIndex="0" />
            </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="firstname" Caption="First Name" VisibleIndex="2">
                <EditFormSettings VisibleIndex="1" />
            </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="username" Caption="User Name" VisibleIndex="3">
                <EditFormSettings VisibleIndex="2" />
            </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="email" Caption="Email" VisibleIndex="4">
                <EditFormSettings VisibleIndex="3" />
            </dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="roleid" Caption="Role ID" VisibleIndex="5">
                <EditFormSettings VisibleIndex="4" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="sitegroupid" Caption="SiteGroupID" VisibleIndex="6">
                <EditFormSettings VisibleIndex="5" />
            </dx:GridViewDataTextColumn>
                
         
                <dx:GridViewDataTextColumn FieldName="ApprovalGroup" VisibleIndex="8">
                <EditFormSettings VisibleIndex="7" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AddressOne" VisibleIndex="9">
                <EditFormSettings VisibleIndex="8" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="AddressTwo" VisibleIndex="10">
                <EditFormSettings VisibleIndex="9" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="City" VisibleIndex="11">
                <EditFormSettings VisibleIndex="10" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="State" VisibleIndex="12">
                <EditFormSettings VisibleIndex="11" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Zip" VisibleIndex="13">
                <EditFormSettings VisibleIndex="12" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FacCode" VisibleIndex="14">
                <EditFormSettings VisibleIndex="13" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="StaplesApprover" VisibleIndex="15">
                <EditFormSettings VisibleIndex="14" />
            </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="PrimaryPhone" VisibleIndex="16">
                <EditFormSettings VisibleIndex="15" />
            </dx:GridViewDataTextColumn>
                
                <dx:GridViewDataTextColumn FieldName="JobTitle" VisibleIndex="17">
                <EditFormSettings VisibleIndex="16" />
            </dx:GridViewDataTextColumn>
                </Columns>
            <Settings ShowFilterRow="True" VerticalScrollableHeight="500" />
            <SettingsPager Mode="EndlessPaging" PageSize="40" />
        </dx:ASPxGridView>


         <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" Modal="true" CloseAction="CloseButton" HeaderText="Edit User" ShowCloseButton="false" ClientInstanceName="popup">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="function(s){ OnPopupShown(); }" />
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="ASPxPopupControl2" Width="1100" Height="800" runat="server" Modal="true" CloseAction="CloseButton" HeaderText="Add User" ShowCloseButton="false" ClientInstanceName="popup2">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="function(s){ OnPopupShown(); }" />
        </dx:ASPxPopupControl>
        <dx:ASPxPopupControl ID="ASPxPopupControl3" runat="server" Modal="true" CloseAction="CloseButton" HeaderText="Delete User" ShowCloseButton="false" ClientInstanceName="popup3">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="function(s){ OnPopupShown(); }" />
        </dx:ASPxPopupControl> 
        <dx:ASPxPopupControl ID="ASPxPopupControl4" runat="server" Modal="true" CloseAction="CloseButton" HeaderText="Delete User" ShowCloseButton="false" ClientInstanceName="popup4">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="function(s){ OnPopupShown(); }" />
        </dx:ASPxPopupControl> 
         <dx:ASPxPopupControl ID="ASPxPopupControl5" runat="server" Modal="true" CloseAction="CloseButton" HeaderText="Do Not Include User" ShowCloseButton="false" ClientInstanceName="popup5">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="function(s){ OnPopupShown(); }" />
        </dx:ASPxPopupControl> 
     <dx:ASPxPopupControl ID="ASPxPopupControl6" runat="server" Modal="true" CloseAction="CloseButton" HeaderText="Manage Exceptions" ShowCloseButton="false" ClientInstanceName="popup6">
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl6" runat="server">
                </dx:PopupControlContentControl>
            </ContentCollection>
            <ClientSideEvents Shown="function(s){ OnPopupShown(); }" />
        </dx:ASPxPopupControl> 
        <asp:SqlDataSource ID="SqlDSSIUser" runat="server" SelectCommandType="StoredProcedure" SelectCommand="hsg_Get_DssiUserSP" DeleteCommand="delete fromm hsgDssiUser where payroll_number = @payroll_number" UpdateCommand="sp_HSG_UpdateDssiUser" InsertCommand="sp_HSG_InsertDssiUser" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>">
            <DeleteParameters>
                <asp:Parameter Name="payroll_number" />
            </DeleteParameters>
        </asp:SqlDataSource>
    </div></div></div>
    </form>
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