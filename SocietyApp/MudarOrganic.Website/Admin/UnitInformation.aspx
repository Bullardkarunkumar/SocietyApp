<%@ Page Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="UnitInformation.aspx.cs" Inherits="Admin_UnitInformation" Title="Mudarorganic-UnitInfo" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Unit Information
                    </div>
                </div>
                <div class="portlet-body">
                    <div id="divUnitList" runat="server" class="row">
                        <div class="col-sm-12">
                            <div id="divAddUnitButton" align="center" runat="server" class="row" style="margin-bottom: 10px">
                                <div class="col-sm-12">
                                    <asp:Button ID="btnAddUnit" runat="server" OnClick="btnAddUnit_Click" CssClass="btn btn-success"
                                        Text="Add Unit" />
                                    <asp:Label ID="lblUnitID" runat="server" Visible="false" />
                                </div>
                            </div>
                            <div id="divUnitBindDetails" runat="server" align="center">
                                <asp:GridView ID="gvUnitInfo" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="UnitId" CssClass="table table-bordered mudargrid" OnRowCommand="gvUnitInfo_RowCommand"
                                    AllowSorting="True" OnSorting="gvUnitInfo_Sorting">
                                    <Columns>
                                        <asp:BoundField DataField="UnitId" HeaderText="UnitID" Visible="false" />
                                        <asp:ButtonField ButtonType="Link" CommandName="cmd_select"
                                            DataTextField="Ucode" HeaderText="Unit Code" SortExpression="Ucode" />
                                        <asp:BoundField DataField="Name" HeaderText="Unit Name" SortExpression="Name" />
                                        <asp:BoundField DataField="Address" HeaderText="Unit Address" SortExpression="Address" />
                                        <asp:BoundField DataField="Unit_Village" HeaderText="Unit Villages" SortExpression="Unit_Village" />
                                        <asp:ButtonField ButtonType="Image" CommandName="cmd_delete"
                                            HeaderText="Delete" ImageUrl="~/images/Delete.jpg" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>

                    <div id="divUnitInfoForm" runat="server" visible="false">
                        <div class="portlet box green">
                            <div class="portlet-title">
                                <div class="caption">
                                    Create/Update Unit
                                </div>
                            </div>
                            <div class="portlet-body form">
                                <div class="form-horizontal">
                                    <div class="form-body" style="margin-left: 150px">
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Unit Code</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtUnitCode" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Unit Name</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtUnitName" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Unit Owner</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtUnitOwner" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div style="padding-bottom: 10px;margin-left:100px" class="form-group">
                                            <table style="text-align: center">
                                                <tr>
                                                    <td>Farmer Villages<br />
                                                        <asp:ListBox ID="lstAvailableVillages" runat="server" Height="130px" SelectionMode="Multiple"
                                                            CssClass="form-control form-control-inline input-large"></asp:ListBox>
                                                    </td>
                                                    <td style="padding: 10px">
                                                        <asp:Button ID="btnPush" runat="server" OnClick="btnPush_Click" Text="&gt;&gt;" />
                                                        <br />
                                                        <asp:Button ID="btnPull" runat="server" OnClick="btnPull_Click" Text="&lt;&lt;" />
                                                    </td>
                                                    <td>Assigned Villages<br />
                                                        <asp:ListBox ID="lstAssignedVillages" runat="server" Height="130px" SelectionMode="Multiple"
                                                            CssClass="form-control form-control-inline input-large"></asp:ListBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Unit Address</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtUAddress" runat="server" Height="50px" TextMode="MultiLine"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Raw Required</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtRaw" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Output State</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtOStaate" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Output Material</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtOMaterial" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Capacity Of Plant</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtCapacity" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Lots Of Produces</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtLotsof" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Permant Labour</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtPLabour" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Temporary Labour</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtTLabour" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>
                                        <div class="form-group">
                                            <label class="col-md-3 control-label">Child Labour</label>
                                            <div class="col-md-9">
                                                <asp:TextBox ID="txtCLabour" runat="server"
                                                    CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                            </div>
                                        </div>

                                        <div class="row form-actions noborder" style="margin-left: 275px">
                                            <asp:Button ID="btnSubmit" runat="server" OnClick="btnSubmit_Click" Text="Submit" CssClass="btn btn-success" />
                                            &nbsp;
                                    <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnClear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
