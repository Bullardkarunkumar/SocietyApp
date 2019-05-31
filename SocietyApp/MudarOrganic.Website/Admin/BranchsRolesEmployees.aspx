<%@ Page EnableEventValidation="false" Title="Mudarorganic-Branch or Role or Employee" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="BranchsRolesEmployees.aspx.cs" Inherits="Mudar_BranchsRolesEmployees" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="htnSelectedTab" runat="server" Value="1" />
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Branch or Roles or Employee List
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="tabbable-line nav-justified">
                        <ul class="nav nav-tabs nav-justified">
                            <li id="liICS" runat="server">
                                <asp:LinkButton ID="lnkbtnICS" runat="server" Text="ICS" OnClick="lnkbtnICS_Click"></asp:LinkButton>
                            </li>
                            <li id="liRoles" runat="server">
                                <asp:LinkButton ID="lnkbtnRoles" runat="server" Text="Roles" OnClick="lnkbtnRoles_Click"></asp:LinkButton>
                            </li>
                            <li id="liEmployee" runat="server">
                                <asp:LinkButton ID="lnkbtnEmployee" runat="server" Text="Employee" OnClick="lnkbtnEmployee_Click"></asp:LinkButton>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane" id="tabpaneICS" runat="server">
                                <div id="divICSDetails" runat="server" class="row">
                                    <div class="col-sm-12">
                                        <div id="divBranchList" runat="server" class="col-sm-12">
                                            <div id="divICSButton" align="center" runat="server" class="row" style="margin-bottom: 10px">
                                                <div class="col-sm-12">
                                                    <asp:Button ID="btnICS" runat="server" OnClick="btnAddBranch_Click" CssClass="btn btn-success"
                                                        Text="Add ICS" />
                                                </div>
                                            </div>
                                            <asp:GridView ID="gvBranchList" runat="server" AutoGenerateColumns="False" DataKeyNames="BranchId"
                                                OnRowCommand="gvBranchList_RowCommand"
                                                CssClass="table table-bordered mudargrid">
                                                <Columns>
                                                    <asp:BoundField DataField="BranchId" HeaderText="ICS ID" Visible="false" />
                                                    <%--<asp:BoundField DataField="BranchCode" HeaderText="Code" />--%>
                                                    <asp:ButtonField ButtonType="Link" CommandName="cmd_Select" DataTextField="BranchCode"
                                                        HeaderText="ICS Code" />
                                                    <asp:BoundField DataField="Bname" HeaderText="ICS Name" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="State" HeaderText="ICS State" />
                                                    <asp:ButtonField ButtonType="Image" CommandName="cmd_delete" HeaderText="Delete"
                                                        ImageUrl="~/images/Delete.jpg" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>

                                        <div id="divAddBranch" runat="server" class="col-sm-12" visible="false">
                                            <div class="portlet box green">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        Create/Update ICS
                                                    </div>
                                                </div>
                                                <div class="portlet-body form">
                                                    <div class="form-horizontal">
                                                        <div class="form-body" style="margin-left: 125px">
                                                            <div class="form-group" style="display: none">
                                                                <label class="col-md-3 control-label">ICS ID</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtBranchID" runat="server" CssClass="form-control form-control-inline input-large" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">ICS Code</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtBranchCode" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">ICS Name</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtBranchName" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Contact Person</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtContactPerson" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Phone / Fax</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtPhoneorFax" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Mobile</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">E-Mail</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Website</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Bank Name</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtBankName" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Bank Accouont Number</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtBankACno" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Bank Account Code</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtBankADCCode" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">IECode</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtIECode" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">FDA</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtFDA" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">AP VAT</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtAPVAT" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Address</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="340px" Height="50px"
                                                                        CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">City</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">District</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtDistrict" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Taluk</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtTaluk" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">State</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Country</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">TIN Number</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtTin" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">OrganicPremium</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtOrganicPremium" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group" id="trSale" runat="server" visible="false">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6">
                                                                        <label class="col-md-3 control-label">Sale</label>
                                                                        <div class="col-md-9">
                                                                            <asp:CheckBox ID="cbSales" runat="server" TabIndex="10" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-md-3 control-label">Exports</label>
                                                                        <div class="col-md-9">
                                                                            <asp:CheckBox
                                                                                ID="cbExports" runat="server" TabIndex="11" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>

                                                            <div class="form-group" id="trWareHouse" runat="server" visible="false">
                                                                <div class="col-sm-12">
                                                                    <div class="col-sm-6">
                                                                        <label class="col-md-3 control-label">WareHouse</label>
                                                                        <div class="col-md-9">
                                                                            <asp:CheckBox ID="cbWareHouse" runat="server" TabIndex="12" />
                                                                        </div>
                                                                    </div>
                                                                    <div class="col-sm-6">
                                                                        <label class="col-md-3 control-label">Other</label>
                                                                        <div class="col-md-9">
                                                                            <asp:CheckBox ID="cbOther" runat="server" TabIndex="13" />
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row form-actions noborder text-center" style="align-content: center">
                                                            <asp:Button ID="btnBranSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnBranSubmit_Click" />
                                                            &nbsp;
                                    <asp:Button ID="btnBranClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnBranClear_Click" />
                                                        </div>
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tabpaneRoles" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div id="divRoles" runat="server">
                                            <div id="divRolesButton" align="center" runat="server" class="row" style="margin-bottom: 10px">
                                                <div class="col-sm-12">
                                                    <asp:Button ID="btnRole" runat="server" OnClick="btnRole_Click" CssClass="btn btn-success"
                                                        Text="Add Role" />
                                                </div>
                                            </div>
                                            <asp:GridView ID="gvRoles" runat="server" AutoGenerateColumns="False" DataKeyNames="RoleId"
                                                OnRowCommand="gvRoles_RowCommand" CssClass="table table-bordered mudargrid">
                                                <Columns>
                                                    <asp:BoundField DataField="RoleId" HeaderText="Role ID" Visible="false" />
                                                    <asp:BoundField DataField="RoleName" HeaderText="Role Name" />
                                                    <asp:ButtonField ButtonType="Image" CommandName="cmd_delete" HeaderText="Delete"
                                                        ImageUrl="~/images/Delete.jpg" ItemStyle-HorizontalAlign="Center">
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div id="divAddRoles" runat="server" class="col-sm-12" visible="false">
                                            <div class="portlet box green">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        Create Role
                                                    </div>
                                                </div>
                                                <div class="portlet-body form">
                                                    <div class="form-horizontal">
                                                        <div class="form-body" style="margin-left: 125px">
                                                            <div class="form-group" style="display: none">
                                                                <label class="col-md-3 control-label">`Role ID</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtRoleId" runat="server" CssClass="form-control form-control-inline input-large" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">`Role Name</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtRoleName" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row form-actions noborder text-center" style="align-content: center">
                                                            <asp:Button ID="btnRoleSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnRoleSubmit_Click" />
                                                            &nbsp;
                                    <asp:Button ID="btnRoleClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnRoleClear_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tabpaneEmployee" runat="server">
                                <div class="row">
                                    <div class="col-sm-12">
                                        <div id="divEmployeeList" runat="server">
                                            <div id="divEmployeeButton" align="center" runat="server" class="row" style="margin-bottom: 10px">
                                                <div class="col-sm-12">
                                                    <asp:Button ID="btnEmployee" runat="server" OnClick="btnEmployee_Click" CssClass="btn btn-success"
                                                        Text="Add Employee" />
                                                </div>
                                            </div>
                                            <asp:GridView ID="gvEmployee" runat="server" DataKeyNames="EmployeeID" AutoGenerateColumns="False"
                                                OnRowCommand="gvEmployee_RowCommand" CssClass="table table-bordered mudargrid">
                                                <Columns>
                                                    <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" Visible="false" />
                                                    <asp:ButtonField ButtonType="Link" DataTextField="EmployeeFristName" HeaderText="Employee Name"
                                                        CommandName="cmd_Selected" />
                                                    <asp:BoundField DataField="BranchName" HeaderText="Branch Name" />
                                                    <asp:BoundField DataField="City" HeaderText="City" />
                                                    <asp:BoundField DataField="MPhone" HeaderText="Moblie Phone" />
                                                    <asp:ButtonField ButtonType="Image" ImageUrl="~/images/Delete.jpg" HeaderText="Delete"
                                                        CommandName="Cmd_delete" ItemStyle-HorizontalAlign="Center"></asp:ButtonField>
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                        <div id="divAddEmployee" runat="server" class="col-sm-12" visible="false">
                                            <div class="portlet box green">
                                                <div class="portlet-title">
                                                    <div class="caption">
                                                        Create/Update Employee
                                                    </div>
                                                </div>
                                                <div class="portlet-body form">
                                                    <div class="form-horizontal">
                                                        <div class="form-body" style="margin-left: 125px">
                                                            <div class="form-group" style="display: none">
                                                                <label class="col-md-3 control-label">Employee ID</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmployeeID" runat="server" CssClass="form-control form-control-inline input-large" Enabled="False"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Employee Name</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmployeeName" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Branch Code</label>
                                                                <div class="col-md-9">
                                                                    <asp:DropDownList ID="ddlBranchID" runat="server" CssClass="form-control form-control-inline input-large">
                                                                    </asp:DropDownList>
                                                                </div>
                                                            </div>
                                                            <div style="margin-left: 80px; margin-bottom: 15px">
                                                                <table>
                                                                    <tr>
                                                                        <td>
                                                                            <label class="control-label">Available Roles</label>
                                                                            <asp:ListBox ID="lstAvailableRoles" runat="server" Height="130px" SelectionMode="Multiple"
                                                                                CssClass="form-control form-control-inline input-large"></asp:ListBox>
                                                                        </td>
                                                                        <td style="padding: 10px">
                                                                            <asp:Button ID="btnPush" runat="server" OnClick="btnPush_Click" Text="&gt;&gt;" />
                                                                            <br />
                                                                            <asp:Button ID="btnPull" runat="server" OnClick="btnPull_Click" Text="&lt;&lt;" />
                                                                        </td>
                                                                        <td>
                                                                            <label class="control-label">Assigned Roles</label>
                                                                            <asp:ListBox ID="lstAssignedRoles" runat="server" Height="130px" SelectionMode="Multiple"
                                                                                CssClass="form-control form-control-inline input-large"></asp:ListBox>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Phone Number</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtPhoneNumber" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Mobile Number</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtMobileNumber" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Address</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmpAddress" runat="server" TextMode="MultiLine" Height="50px"
                                                                        CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">City</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmpCity" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">District</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmpDistrict" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Taluk</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmpTaluk" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">State</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmpState" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                            <div class="form-group">
                                                                <label class="col-md-3 control-label">Country</label>
                                                                <div class="col-md-9">
                                                                    <asp:TextBox ID="txtEmpCountry" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="row form-actions noborder text-center" style="align-content: center">
                                                            <asp:Button ID="btnEmpSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnEmpSubmit_Click" />
                                                            &nbsp;
                                    <asp:Button ID="btnEmpClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnEmpClear_Click" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                function fnShowMessage(msg) {
                    bootbox.alert(msg);
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

