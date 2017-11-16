<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="edituser.aspx.cs" Inherits="EmployeeDirectory.EmpDirectory.edituser" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
       <script src="../js/jquery-1.10.2.min.js"></script>
    <script src="../js/bootstrap.min.js"></script>
    <link href="../css/new.css" rel="stylesheet" />
    <link href="../css/bootstrap.min.css" rel="stylesheet" />
    <link href="../css/main.css" rel="stylesheet" />
    
    <link href="css/style.css" rel="stylesheet" />
    <script>
        function addSiteGroup() {
            var thedivision = $('#ddlDivision').val();
            var theregion = $('#ddlRegion').val();
            var thedistrict = $('#ddlDistrict').val();
            var theSiteGroup = $('#txtSiteGroupIDt').val();

            var fullvalue = thedivision + "" + theregion + "" + thedistrict; ShowEditPopup

            if ($("#chkAll").is(':checked')) {
                fullvalue = thedivision + "ALL";
            }
            if (theSiteGroup != "") {
                $('#txtSiteGroupIDt').val(theSiteGroup + "#" + fullvalue);
            }
            else {
                $('#txtSiteGroupIDt').val(fullvalue);
            }
        }

        function addApprovalGroup() {
            var thedivision = $('#ddlDivision').val();
            var theregion = $('#ddlRegion').val();
            var thedistrict = $('#ddlDistrict').val();
            var theAppGroup = $('#txtApprovalGroupt').val();

            var fullvalue = thedivision + "" + theregion + "" + thedistrict;
            if ($("#chkAll").is(':checked')) {
                fullvalue = thedivision + "ALL";
            }
            if (theAppGroup != "") {
                $('#txtApprovalGroupt').val(theAppGroup + "#" + fullvalue);
            }
            else {
                $('#txtApprovalGroupt').val(fullvalue);
            }
        }

        function addStaples() {
            var thedivision = $('#ddlDiv2').val();
            var theregion = $('#ddlReg2').val();
            var theSiteGroup = $('#txtStaplesApprovert').val();

            var fullvalue = thedivision + "" + theregion;
            if ($("#chkAll2").is(':checked')) {
                fullvalue = thedivision + "ALL";
            }
            if ($("#chkCorp").is(':checked')) {
                fullvalue = "CORP";
            }

            $('#txtStaplesApprovert').val(fullvalue);

        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="sm" runat="server"></asp:ScriptManager>


          <asp:Repeater ID="rpDetails" runat="server" OnItemDataBound="rpDetails_ItemDataBound" DataSourceID="SqlDataSource4"><ItemTemplate>

        <div class="container-fluid">
    <div class="row">
        <div class="col-xs-12">
            <div class="box">

                

                
                    <div class="col-md-12 box-body">
                        <div class="row">
                            <div class="clearfix" style="clear: both"></div>
                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtPayrollt" class="col-sm-5 control-label" style="padding-top:10px">Payroll Number</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtPayrollt" Text='<%# Eval("payroll_number").ToString() %>' ReadOnly="true" CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtUsernamet" class="col-sm-5 control-label" style="padding-top:10px">Username</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtUsernamet" Text='<%# Eval("username").ToString() %>'  CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtFnamet" class="col-sm-5 control-label" style="padding-top:10px">First Name</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtFnamet" CssClass="form-control" Text='<%# Eval("firstname").ToString() %>' Width="170px" runat="server"></asp:TextBox>
                                    </div> 
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtLnamet" class="col-sm-5 control-label" style="padding-top:10px">Last Name</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         <asp:TextBox ID="txtLnamet" Text='<%# Eval("lastname").ToString() %>' CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                             <div class="col-md-12 box-body">
                        <div class="row">
                            <div class="clearfix" style="clear: both"></div>
                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtEmailt" class="col-sm-5 control-label" style="padding-top:10px">Email</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtEmailt" Text='<%# Eval("email").ToString() %>' CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtAdd1t" class="col-sm-5 control-label" style="padding-top:10px">Address 1</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtAdd1t" Text='<%# Eval("addressone").ToString() %>' CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtAdd2t" class="col-sm-5 control-label" style="padding-top:10px">Address 2</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtAdd2t" Text='<%# Eval("addresstwo").ToString() %>' CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtCityt" class="col-sm-5 control-label" style="padding-top:10px">City</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         <asp:TextBox ID="txtCityt" Text='<%# Eval("city").ToString() %>' CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>                            <div class="col-md-12 box-body">
                        <div class="row">
                            <div class="clearfix" style="clear: both"></div>
                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlState" class="col-sm-5 control-label" style="padding-top:10px">State</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:DropDownList ID="ddlState" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtZipt" class="col-sm-5 control-label" style="padding-top:10px">Zip</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtZipt" Text='<%# Eval("zip").ToString() %>' CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="txtPhonet" class="col-sm-5 control-label" style="padding-top:10px">Phone</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:TextBox ID="txtPhonet" Text='<%# Eval("primaryphone").ToString() %>' CssClass="form-control" Width="170px" runat="server"></asp:TextBox>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlFacCode" class="col-sm-5 control-label" style="padding-top:10px">Facility Code</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         <asp:DropDownList runat="server" CssClass="form-control" ID="ddlFacCode"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>                            <div class="col-md-12 box-body">                                 <div class="row">                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlJob" class="col-sm-5 control-label" style="padding-top:10px">Job Title</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         <asp:DropDownList ID="ddlJob" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlJob" class="col-sm-5 control-label" style="padding-top:10px"></label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlJob" class="col-sm-5 control-label" style="padding-top:10px"></label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlJob" class="col-sm-5 control-label" style="padding-top:10px"></label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         
                                    </div>
                                </div>
                            </div>                           </div>                                </div>                            </div>                                </div>                                                             <div style="padding-left:30px;font-size:larger;font-weight:bold">For Site Group ID and Approval Group, select Division, Region, District to build ID.  For "All" just select Division and click "All" checkbox</div>                                <div class="col-md-12 box-body">
                        <div class="row">
                            <div class="clearfix" style="clear: both"></div>
                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlDivision" class="col-sm-5 control-label" style="padding-top:10px">Division</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:DropDownList ID="ddlDivision" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList><ajaxToolkit:CascadingDropDown ID="CascadingDropDown1" Category="Division" TargetControlID="ddlDivision" ServiceMethod="FetchDivision" ServicePath="AjaxCascadeDropDown.asmx" PromptText="Select Division" LoadingText="[Loading Divisions]" runat="server" />
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlRegion" class="col-sm-5 control-label" style="padding-top:10px">Region</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown2" Category="Region" runat="server" ParentControlID="ddlDivision" TargetControlID="ddlRegion" PromptText="Select Region" LoadingText="[Loading Regions]" ServiceMethod="FetchRegion" ServicePath="AjaxCascadeDropDown.asmx" /><asp:DropDownList ID="ddlRegion" CssClass="form-control" ClientIDMode="Static" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlDistrict" class="col-sm-5 control-label" style="padding-top:10px">District</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown3" runat="server" ClientIDMode="Static" Category="District" ParentControlID="ddlRegion" PromptText="Select District" LoadingText="[Loading Districts]" ServiceMethod="FetchDistrict" ServicePath="AjaxCascadeDropDown.asmx" TargetControlID="ddlDistrict" /><asp:DropDownList ClientIDMode="Static" CssClass="form-control" ID="ddlDistrict" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="chkAll" class="col-sm-5 control-label" style="padding-top:10px">All</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         <asp:CheckBox ID="chkAll" ClientIDMode="Static" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>                            <div style="padding-left:28px"><table><tr><td width="128px">Site Group ID</td><td width="180px"><asp:TextBox ID="txtSiteGroupIDt" Text='<%# Eval("sitegroupid").ToString() %>' ClientIDMode="Static" Width="170px" CssClass="form-control" runat="server"></asp:TextBox></td><td><button type="button" class="btneffect" onclick="addSiteGroup()" id="AddSiteGroup">Add Site Group</button></td></tr></table>
                                    </div>
                            <div style="padding-left:28px; padding-top:20px;"><table><tr><td width="128px">Approval Group</td><td width="180px"><asp:TextBox ID="txtApprovalGroupt" Text='<%# Eval("approvalgroup").ToString() %>' ClientIDMode="Static" Width="170px" CssClass="form-control" runat="server"></asp:TextBox></td><td><button type="button" class="btneffect" onclick="addApprovalGroup()" id="AddApprovalGroup">Add Approval Group</button></td></tr></table>
                                    </div>
                                </div>
                            </div>                            <div style="padding-left:30px; padding-top:170px; font-size:larger;font-weight:bold">For Staples Approver, select Division, Region or check the "CORP" checkbox for choose "CORP". For "All" just select Division and click "All" checkbox</div>                           <div class="col-md-12 box-body">
                        <div class="row">
                            <div class="clearfix" style="clear: both"></div>
                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlDiv2" class="col-sm-5 control-label" style="padding-top:10px">Division</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:DropDownList ID="ddlDiv2" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList><ajaxToolkit:CascadingDropDown ID="CascadingDropDown4" Category="Division" TargetControlID="ddlDiv2" ServiceMethod="FetchDivision" ServicePath="AjaxCascadeDropDown.asmx" LoadingText="[Loading Divisions]" PromptText="Select Division" runat="server" />
                                    </div>
                                </div>
                            </div>                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="ddlReg2" class="col-sm-5 control-label" style="padding-top:10px">Region</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <ajaxToolkit:CascadingDropDown ID="CascadingDropDown5" Category="Region" runat="server" ParentControlID="ddlDiv2" TargetControlID="ddlReg2" PromptText="Select Region" LoadingText="[Loading Regions]" ServiceMethod="FetchRegion" ServicePath="AjaxCascadeDropDown.asmx" /><asp:DropDownList ID="ddlReg2" ClientIDMode="Static" CssClass="form-control" runat="server"></asp:DropDownList>
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="chkAll2" class="col-sm-5 control-label" style="padding-top:10px">All</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                        <asp:CheckBox CssClass="form-control" ClientIDMode="Static" ID="chkAll2" runat="server" />
                                    </div>
                                </div>
                            </div>

                            <div class="col-xs-12 col-sm-3 col-md-3 col-lg-3">
                                <div class="form-group">
                                    <label for="chkCorp" class="col-sm-5 control-label" style="padding-top:10px">Corp</label>
                                    <div class="col-sm-7" style="padding-top:5px">
                                         <asp:CheckBox ID="chkCorp" ClientIDMode="Static" CssClass="form-control" runat="server" />
                                    </div>
                                </div>
                            </div>                            <div style="padding-left:28px; padding-top:20px"><table><tr><td width="128px">Staples Approver</td><td width="180px"><asp:TextBox ClientIDMode="Static" ID="txtStaplesApprovert" Text='<%# Eval("staplesapprover").ToString() %>' Width="170px" CssClass="form-control" runat="server"></asp:TextBox></td><td><button type="button" class="btneffect" onclick="addStaples()" id="AddStaplesApprover">Add Approval Group</button></td></tr></table>                            </div>                             <div style="padding-left:28px; padding-top:20px; padding-bottom:30px"><asp:Button class="btneffect" ID="btnEditUser" OnClick="btnEditUser_Click" runat="server" Text="Add Changes to User" /><asp:Button class="btneffect" OnClick="btnCancel_Click" ID="btnCancel" runat="server" Text="Cancel" /></div>                        </div>                </div>
      </div></div></div></div></div></div></div></div>
        
</ItemTemplate></asp:Repeater>
        



        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:HSGEmployeeDirectory %>" SelectCommand="SELECT distinct [customer_code] FROM [HSG_ARCUST_VW] order by customer_code"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:HSGEmployeeDirectory %>" SelectCommand="SELECT distinct ltrim(state) as state FROM [HSG_ARCUST_VW] where state is not null and state <> '' and term_dt is null order by ltrim(state)"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" SelectCommand="SELECT distinct [JobTitle] FROM [hsg_DssiUser] order by JobTitle"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" SelectCommand="SELECT [payroll_number],[lastname],[firstname],[username],[password],[email],[roleid],[sitegroupid],[IsActive],[ApprovalGroup],[AddressOne],[AddressTwo],[City],[State],[Zip],[FacCode],[StaplesApprover],[PrimaryPhone],[JobTitle] FROM [HSGOPS].[dbo].[hsg_DssiUser] where payroll_number = @payroll_number">

            <SelectParameters>
                <asp:QueryStringParameter Name="payroll_number" QueryStringField="keys" />
                
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSource5" runat="server" OnUpdated="SqlDataSource5_Updated" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>" UpdateCommandType="StoredProcedure" UpdateCommand="sp_hsg_dssi_update_users">
            <UpdateParameters>
                <asp:QueryStringParameter Name="paynum" QueryStringField="keys" />
                <asp:Parameter Name="lname" />
                <asp:Parameter Name="fname" />
                <asp:Parameter Name="uname" />
                <asp:Parameter Name="pass" DefaultValue=" " />
                <asp:Parameter Name="email" />
                <asp:Parameter Name="role" DefaultValue="WM" />
                <asp:Parameter Name="sitegroup" />
                <asp:Parameter Name="isactive" DefaultValue="Y" />
                <asp:Parameter Name="approval" />
                <asp:Parameter Name="add1" />
                <asp:Parameter Name="add2" />
                <asp:Parameter Name="city" />
                <asp:Parameter Name="state" />
                <asp:Parameter Name="Zip" />
                <asp:Parameter Name="faccode" />
                <asp:Parameter Name="staples" />
                <asp:Parameter Name="phone" />
                <asp:Parameter Name="title" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlAudit" runat="server" OnInserted="SqlAudit_Inserted" InsertCommand="Insert into HSG_DSSIUser_Exceptionsaudit (action,value,type,theuser) values ('addtoupdate',@value,'paynum',@user)" ConnectionString="<%$ ConnectionStrings:HSGOPSConnectionString %>">
             <InsertParameters>
                 <asp:QueryStringParameter Name="value" QueryStringField="keys" />
                 <asp:SessionParameter Name="user" SessionField="user" />
             </InsertParameters>
         </asp:SqlDataSource>

    
    </form>
</body>
</html>
