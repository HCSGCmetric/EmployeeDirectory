<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="realign.aspx.cs" Inherits="EmployeeDirectory.realign" %>

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
  
    <%--<link href="css/sb-admin-rtl.css" rel="stylesheet" />
    <link href="font-awesome/css/font-awesome.min.css" rel="stylesheet" />
    <link href="css/sb-admin.css" rel="stylesheet" />--%>
   <script src="../Scripts/jquery-1.7.1.js"></script>
    
    <script src="../Scripts/jquery-ui-1.8.20.js"></script>
    <script src="../Scripts/_references.js"></script>
     <script src="../js/jquery.js"></script>
    <script src="../js/bootstrap.min.js"></script>
   <script>
       function OnEndCallback(s, e) {
           $("#ASPxGridView1_DXCBtn0 span:contains('Save changes')").html("Re-Align Facilities")
       };
   </script>

    
<script>
    $('.datepicker').datepicker({
        format: 'mmddyyyy',
        startDate: '-3d'
    })

</script>

      <script type="text/javascript">
          var visibleIndex;
          var DeletedValue;
          function OnInitHeader(s, e) {
              setTimeout(function () { CheckSelectedCellsOnPage("usualCheck"); }, 0);
          }
          function OnHeaderCheckBoxCheckedChanged(s, e) {
              var visibleIndices = Grid.batchEditApi.GetRowVisibleIndices();
              var totalRowsCountOnPage = visibleIndices.length;
              for (var i = 0; i < totalRowsCountOnPage ; i++) {
                  Grid.batchEditApi.SetCellValue(visibleIndices[i], "RealignFlag", s.GetChecked())
              }
          }
          function OnCellCheckedChanged(s, e) {
              Grid.batchEditApi.SetCellValue(visibleIndex, "RealignFlag", s.GetValue());
              CheckSelectedCellsOnPage("usualCheck");
          }
          function OnBatchEditRowDeleting(s, e) {
              DeletedValue = Grid.batchEditApi.GetCellValue(e.visibleIndex, "RealignFlag");
              CheckSelectedCellsOnPage("deleteCheck");
          }
          function OnBatchEditRowInserting(s, e) {
              CheckSelectedCellsOnPage("insertCheck");
          }

          function OnBatchEditStartEditing(s, e) {
              visibleIndex = e.visibleIndex;
          }

          function CheckSelectedCellsOnPage(checkType) {
              var currentlySelectedRowsCount = 0;
              var visibleIndices = Grid.batchEditApi.GetRowVisibleIndices();
              var totalRowsCountOnPage = visibleIndices.length;
              for (var i = 0; i < totalRowsCountOnPage ; i++) {
                  if (Grid.batchEditApi.GetCellValue(visibleIndices[i], "RealignFlag"))
                      currentlySelectedRowsCount++;
              }
              if (checkType == "insertCheck")
                  totalRowsCountOnPage++;
              else if (checkType == "deleteCheck") {
                  totalRowsCountOnPage--;
                  if (DeletedValue)
                      currentlySelectedRowsCount--;
              }
              if (currentlySelectedRowsCount <= 0)
                  HeaderCheckBox.SetCheckState("Unchecked");
              else if (currentlySelectedRowsCount >= totalRowsCountOnPage)
                  HeaderCheckBox.SetCheckState("Checked");
              else
                  HeaderCheckBox.SetCheckState("Indeterminate");
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
    <h2 class="page-header">Realign Facility</h2>

    <div>
   
        <dx:ASPxGridView ID="ASPxGridView1" ClientInstanceName="Grid"  DataSourceID="SqlDataSource1" Settings-ShowFilterRow="true" SettingsPager-Mode="ShowAllRecords" Width="100%" SettingsText-ConfirmOnLosingBatchChanges="true" SettingsEditing-BatchEditSettings-StartEditAction="Click" SettingsEditing-Mode="Batch" KeyFieldName="FacilityCode" runat="server" AutoGenerateColumns="False">
            <ClientSideEvents EndCallback="OnEndCallback" />
            <SettingsEditing Mode="Batch">

            </SettingsEditing>
<SettingsCommandButton>
<ShowAdaptiveDetailButton ButtonType="Image"></ShowAdaptiveDetailButton>

<HideAdaptiveDetailButton ButtonType="Image"></HideAdaptiveDetailButton>
</SettingsCommandButton>
            <Columns>
        
                <dx:GridViewDataTextColumn FieldName="UserName" ReadOnly="true" VisibleIndex="1">
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FacilityCode" ReadOnly="true" VisibleIndex="2">
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataDateColumn FieldName="ReflectedDate" ReadOnly="true" VisibleIndex="3">
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataDateColumn>
                <dx:GridViewDataTextColumn FieldName="CurrentReg" ReadOnly="true" VisibleIndex="4">
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="CurrentDist" ReadOnly="true" VisibleIndex="5">
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FutureReg" ReadOnly="true" VisibleIndex="6">
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FutureDist" ReadOnly="true" VisibleIndex="7">
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataTextColumn>
                
             <%--   <dx:GridViewDataCheckColumn FieldName="RealignFlag" Settings-AllowAutoFilter="False" Settings-AllowSort="False" Caption="Re-Align Facility" VisibleIndex="7">
                    
                    <SettingsHeaderFilter>
                        <DateRangePickerSettings EditFormatString="" />
                    </SettingsHeaderFilter>
                </dx:GridViewDataCheckColumn>--%>

                  <dx:GridViewDataCheckColumn FieldName="RealignFlag" HeaderStyle-HorizontalAlign="Center" PropertiesCheckEdit-ValueType="System.Int32" Caption="Re-Align Facility" VisibleIndex="7">
                    
                     <PropertiesCheckEdit ValueChecked="1" ValueUnchecked="0">
                            <ClientSideEvents CheckedChanged="OnCellCheckedChanged" />
                        </PropertiesCheckEdit>
                        <HeaderTemplate>
                            <dx:ASPxCheckBox ID="HeaderCheckBox" runat="server" ClientInstanceName="HeaderCheckBox" AllowGrayed="true" AllowGrayedByClick="false">
                                <ClientSideEvents CheckedChanged="OnHeaderCheckBoxCheckedChanged" Init="OnInitHeader" />
                            </dx:ASPxCheckBox>
                        </HeaderTemplate>
                        <Settings AllowSort="False" />
                    </dx:GridViewDataCheckColumn>
               
            </Columns>
        </dx:ASPxGridView>
    
        
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HSGRPTConnectionString %>" OnUpdated="SqlDataSource1_Updated" UpdateCommand="update [FacilityReAlignment] set RealignFacilityDate = @RealignFacilityDate, RealignFlag = @RealignFlag where FacilityCode = @FacilityCode" SelectCommand="SELECT [ID], [UserName], [FacilityCode], [ReflectedDate], [CurrentReg], [CurrentDist], [FutureReg], [FutureDist], case when RealignFlag is null then 0 else RealignFlag end as RealignFlag FROM [FacilityReAlignment] where realignflag = '0' or realignflag is NULL">
           
            <UpdateParameters>
                <asp:Parameter Name="RealignFacilityDate" />
                <asp:Parameter Name="RealignFlag" />
                <asp:Parameter Name="FacilityCode" />
            </UpdateParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" UpdateCommand="sp_HSG_ARRealignmentUpdate" ConnectionString="<%$ ConnectionStrings:HSGRPTConnectionString %>" UpdateCommandType="StoredProcedure"></asp:SqlDataSource>
    </div>
     
        
        </div>            </div>          </div>
    </form>
  
    <script>

        $("#ASPxGridView1_DXCBtn0 span:contains('Save changes')").html("Re-Align Facilities")

    </script>
</body>
</html>
