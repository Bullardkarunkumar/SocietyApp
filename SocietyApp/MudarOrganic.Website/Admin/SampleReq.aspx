<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="SampleReq.aspx.cs" Inherits="Admin_SampleReq" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        th {
            text-align: center;
        }
    </style>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div>
                <div class="page-content-inner">
                    <div class="note note-success">
                        <div class="form-inline col-lg-offset-5">
                            <div class="form-group">
                                <label class="control-label">Select Status</label>
                                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" runat="server"
                                    OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="NEW" Value="NEW" />
                                    <asp:ListItem Text="DISPATCH" Value="DISPATCH" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="portlet box green">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Sample Request
                        </div>
                    </div>
                    <div class="portlet-body flip-scroll">
                        <asp:Repeater ID="dlSampleRequestDetails" runat="server" OnItemCommand="dlSampleRequestDetails_ItemCommand">
                            <HeaderTemplate>
                                <table class="table table-bordered mudargrid">
                                    <thead class="flip-content">
                                        <tr>
                                            <th>Details
                                            </th>
                                            <th>SR Ref
                                            </th>
                                            <th>Date
                                            </th>
                                            <th>Buyer
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>Courier Name
                                            </th>
                                            <th>Courier Ref
                                            </th>
                                            <th>Courier Date
                                            </th>
                                            <th>Dispatch
                                            </th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblSampleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SampleID")%>' />
                                    </td>

                                    <td>
                                        <asp:Label ID="lblSampleDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SampleDate","{0:dd MMM yyyy}")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerCompanyName")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourierName")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourierRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourierAccountNumber")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblCourierDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourierDate", "{0:dd MMM yyyy}")%>' />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnUpdate" runat="server" Text="Dispatch" CssClass="btn btn-success btn-sm" CommandName="Disaptch" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8">
                                        <asp:GridView ID="gvOrderSampleProduct" runat="server" CssClass="subgrid" Visible="false" AutoGenerateColumns="false">
                                            <Columns>
                                                <asp:BoundField HeaderText="ProductID" DataField="ProductID" />
                                                <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                                <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
        </div>
        <div id="divSampleRequestDetails" runat="server" align="center">
        </div>
        <div id="divplaceorder" runat="server" visible="false">
            <table width="100%" align="center">
                <tr>
                    <td colspan="4" align="center">
                        <asp:Label ID="lblMsg" runat="server" ForeColor="Red" /></td>
                </tr>
                <tr>
                    <td rowspan="3" style="width: 10%;"></td>
                    <td align="right">Courier Name
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtPO" runat="server" Text="" CssClass="textbox_Style" />
                    </td>
                    <td rowspan="3" style="width: 10%;"></td>
                </tr>
                <tr>
                    <td align="right">Courier Number</td>
                    <td>
                        <asp:TextBox ID="txtCourierAcNo" runat="server" CssClass="textbox_Style"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnFinish" runat="server" Text="Finish" CssClass="fb8" Visible="false" />
                        <asp:Button ID="btnDFinish" runat="server" Text="Finish" CssClass="fb8_disable" ForeColor="Gray" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">&nbsp;</td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
