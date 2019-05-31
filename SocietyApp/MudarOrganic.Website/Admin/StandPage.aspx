<%@ Page Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="StandPage.aspx.cs" Inherits="Admin_StandPage" Title="Standard Data" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="htnSelectedTab" runat="server" Value="1" />
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Settings Page
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="tabbable-line nav-justified">
                        <ul class="nav nav-tabs nav-justified">
                            <li id="liCategory" runat="server">
                                <asp:LinkButton ID="btnTProductionYear" runat="server" Text="Production Year" OnClick="btnTProductionYear_Click" />
                            </li>
                            <li id="liProducts" runat="server">
                                <asp:LinkButton ID="btnTMenthol" runat="server" Text="Process Percentage" OnClick="btnTMenthol_Click" />
                            </li>
                        </ul>

                        <div class="tab-content">
                            <div class="tab-pane" id="tabpanproductionyear" runat="server">
                                <div id="divDetails" runat="server">
                                    <div style="margin-bottom: 10px; text-align: center">
                                        <asp:Button ID="btnAddSDate" runat="server" Text="Add Date" CssClass="btn btn-success"
                                            OnClick="btnAddSDate_Click" />
                                    </div>
                                    <div>
                                        <asp:GridView ID="gvStandDetails" runat="server"
                                            AutoGenerateColumns="False" DataKeyNames="StandID"
                                            CssClass="table table-bordered mudargrid" OnRowCommand="gvStandDetails_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="StandID" HeaderText="S.No" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="ProductCode" HeaderText="Code"></asp:BoundField>
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name"></asp:BoundField>
                                                <asp:BoundField DataField="Date" HeaderText=" Date" DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                                <asp:BoundField DataField="Year" HeaderText="Year"></asp:BoundField>
                                                <asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="View" CommandName="cmd_edit"></asp:ButtonField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="divForm" runat="server" visible="false">
                                    <div class="portlet-body form">
                                        <div class="form-horizontal">
                                            <div class="form-body" style="margin-left: 80px">
                                                <div class="form-group" style="display: none">
                                                    <label class="col-md-3 control-label"></label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblStandID" runat="server" Visible="false" />
                                                        <asp:Label ID="lblID" runat="server" Visible="false" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Year</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ID="ddlSeasonYear" runat="server" AutoPostBack="false"
                                                            CssClass="form-control form-control-inline input-large">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Product</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="false"
                                                            CssClass="form-control form-control-inline input-large">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Date</label>
                                                    <div class="col-md-9">
                                                        <div class="input-group date form_datetime form_datetime bs-datetime input-large">
                                                            <asp:TextBox ID="txtPlantationFDate" autocomplete="false" AutoCompleteType="None" runat="server"
                                                                CssClass="form-control form-control-inline">
                                                            </asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <button class="btn default date-set" type="button">
                                                                    <i class="fa fa-calendar"></i>
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-actions noborder text-center" style="align-content: center">
                                                <asp:Button ID="btnSupplierPlaceorder" runat="server" Text="Submit"
                                                    CssClass="btn btn-success" OnClick="btnSupplierPlaceorder_Click" />
                                                &nbsp;
                                    <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default"
                                        OnClick="btnBack_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tab-pane" id="tabpaneTMenthol" runat="server">
                                <div id="divMDetails" runat="server">
                                    <div style="text-align: center; margin-bottom: 10px">
                                        <asp:Button ID="AddPercen" runat="server" Text="Add Percentage" CssClass="btn btn-success" OnClick="AddPercen_Click" />
                                    </div>
                                    <div>
                                        <asp:GridView ID="gvMperdetails" runat="server" AutoGenerateColumns="False" DataKeyNames="PerID"
                                            CssClass="table table-bordered mudargrid" OnRowCommand="gvMperdetails_RowCommand">
                                            <Columns>
                                                <asp:BoundField DataField="PerID" HeaderText="S.No" Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name"></asp:BoundField>
                                                <asp:BoundField DataField="percentage" HeaderText="Percentage"></asp:BoundField>
                                                <asp:BoundField DataField="SeasonYear" HeaderText="Year"></asp:BoundField>
                                                <asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="View" CommandName="cmd_edit"></asp:ButtonField>
                                            </Columns>
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="divMForm" runat="server" visible="false">
                                    <div class="portlet-body form">
                                        <div class="form-horizontal">
                                            <div class="form-body" style="margin-left: 80px">
                                                <div class="form-group" style="display: none">
                                                    <label class="col-md-3 control-label"></label>
                                                    <div class="col-md-9">
                                                        <asp:Label ID="lblPerID" runat="server" Visible="false" />
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Year</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ID="ddlMyer" runat="server" AutoPostBack="false"
                                                            CssClass="form-control form-control-inline input-large">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Product</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ID="ddlMproduct" runat="server" AutoPostBack="false"
                                                            CssClass="form-control form-control-inline input-large">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Percentage</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPer" runat="server" AutoPostBack="false"
                                                            CssClass="form-control form-control-inline input-large">
                                                        </asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row form-actions noborder text-center" style="align-content: center">
                                                <asp:Button ID="btnMsubmit" runat="server" Text="Submit"
                                                    CssClass="btn btn-success" OnClick="btnMsubmit_Click" />
                                                &nbsp;
                                    <asp:Button ID="btnMback" runat="server" Text="Back" CssClass="btn btn-default"
                                        OnClick="btnMback_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                //$(function () {
                //    fnBindPlugins();
                //});

                function fnBindPlugins() {
                    $('#<%= txtPlantationFDate.ClientID %>').datetimepicker({
                        minView: 2,
                        pickTime: false,
                        format: 'dd/mm/yyyy',
                        autoclose: true
                    });
                }
                function fnShowMessage(msg) {
                    bootbox.alert(msg);
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="content_area_Home" style="height: auto; display: none">
        <div id="divProductionDetails" runat="server" visible="false">
        </div>
        <div id="divMenthol" runat="server" visible="false">
        </div>
    </div>
</asp:Content>
