<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="handuplicates.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.handuplicates" %>
<%@ Register assembly="DevExpress.Web.v17.1, Version=17.1.3.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" namespace="DevExpress.Web" tagprefix="dx" %>
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
    <style>

        .btnhide {display:none}
    </style>
  
    <%--<link href="css/sb-admin-rtl.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/sb-admin.css" rel="stylesheet" />--%>
   <script src="../Scripts/jquery-1.7.1.js"></script>
    
    <script src="../Scripts/jquery-ui-1.8.20.js"></script>
    <script src="../Scripts/_references.js"></script>
     <script src="../js/jquery.js"></script>
    <script src="../js/bootstrap.min.js"></script>

     <script type="text/javascript">
         function gvDupes_EndCallback(s, e) {
             if (s.cpShowDeleteConfirmBox)
                 pcConfirm.Show();
         }

         function Yes_Click() {
             pcConfirm.Hide();
             gvDupes.DeleteRow(gvDupes.cpRowIndex);
         }

         function No_Click() {
             pcConfirm.Hide()
         }

         function showButton() {
             var b = document.getElementById("btnGoToRealign");
             b.style.display = "block";
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
       
        <div id="page-wrapper">
            


<div class="container-fluid">

    <h2 class="page-header">Please remove one of the duplicates for each of the duplicate records below</h2>

    <dx:ASPxGridView ID="gvDupes" OnInit="gvDupes_Init" ClientInstanceName="gvDupes" KeyFieldName="ID" runat="server" OnRowDeleted="gvDupes_RowDeleted" SettingsBehavior-AllowSort="false" SettingsPager-Mode="ShowAllRecords" OnCustomButtonCallback="gvDupes_CustomButtonCallback" DataSourceID="SqlDataSource3">
            <Columns>
                <dx:GridViewCommandColumn VisibleIndex="0" ShowClearFilterButton="True">
                    <CustomButtons>
                        <dx:GridViewCommandColumnCustomButton ID="btDelete" Text="Delete" Visibility="AllDataRows">
                        </dx:GridViewCommandColumnCustomButton>
                    </CustomButtons>
                </dx:GridViewCommandColumn>
                <dx:GridViewDataTextColumn FieldName="FacilityCode" VisibleIndex="1"></dx:GridViewDataTextColumn>
                 <dx:GridViewDataTextColumn FieldName="UserName" ReadOnly="true" VisibleIndex="2"></dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="ReflectedDate" ReadOnly="true" VisibleIndex="3"></dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="CurrentReg" ReadOnly="true" VisibleIndex="4">                    
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CurrentDist" ReadOnly="true" VisibleIndex="5">
                  
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FutureReg" ReadOnly="true" VisibleIndex="6">
                  
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FutureDist" ReadOnly="true" VisibleIndex="7">
                   
                </dx:GridViewDataTextColumn>
            </Columns>
            <ClientSideEvents EndCallback="gvDupes_EndCallback" />
        </dx:ASPxGridView>
    
                 <asp:Button ID="btnGoToRealign" runat="server" ClientIDMode="Static" Text="Go to Realignment Page" OnClick="btnGoToRealign_Click" />
           
      
        <dx:ASPxPopupControl ID="pcConfirm" runat="server" ClientInstanceName="pcConfirm"
            Modal="True" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter"
            HeaderText="Row Deleting">
            <ContentCollection>
                <dx:PopupControlContentControl runat="server" SupportsDisabledAttribute="True">
                    <table width="100%">
                        <tr>
                            <td colspan="2" align="center">
                                Delete Row?
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

     <asp:SqlDataSource ID="SqlDataSource3" OnSelected="SqlDataSource3_Selected"  runat="server" ConnectionString="<%$ ConnectionStrings:HSGRPTConnectionString %>" DeleteCommand="Delete FROM [FacilityReAlignment] where id=@id" SelectCommand="select y.id ID, y.facilitycode FacilityCode,y.facilityname,y.currentreg CurrentReg,y.CurrentDiv CurrentDiv, y.CurrentDist CurrentDist,y.FutureDist FutureDist,y.futurereg FutureReg,y.FutureDiv FutureDiv,y.FutureDist FutureDist,y.username UserName,y.reflecteddate ReflectedDate,y.requestdate from [ApolloDB].[dbo].[FacilityReAlignment] y INNER JOIN (SELECT [FacilityCode],[FacilityName],count(*) as countof FROM [ApolloDB].[dbo].[FacilityReAlignment] group by FacilityCode, FacilityName having count(*) > 1) dt on y.FacilityCode= dt.FacilityCode order by FacilityCode">
             <DeleteParameters>
                 <asp:Parameter Name="ID" />
             </DeleteParameters>
         </asp:SqlDataSource>
    
     </div>
     
        
        </div>            </div>         
    </form>
</body>
</html>
