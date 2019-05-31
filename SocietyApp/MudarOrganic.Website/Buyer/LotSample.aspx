<%@ Page Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="LotSample.aspx.cs" Inherits="Buyer_LotSample" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        th {
            text-align: center;
        }
    </style>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="divMainDetails" align="center" runat="server">
                <div>
                    <div class="page-content-inner">
                        <div class="note note-success">
                            <div class="form-inline" style="text-align: center">
                                <div class="form-group">
                                    <label class="control-label">Order Status</label>
                                    <asp:DropDownList ID="ddlOrderStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                                        <asp:ListItem Text="ALL" Value="ALL" />
                                        <asp:ListItem Text="NEW" Value="NEW" />
                                        <asp:ListItem Text="INPROCESS" Value="INPROCESS" />
                                        <asp:ListItem Text="SHIPPING" Value="SHIPPING" />
                                        <asp:ListItem Text="HOLD" Value="HOLD" />
                                        <asp:ListItem Text="PENDING" Value="PENDING" />
                                        <asp:ListItem Text="DISPATCH" Value="DISPATCH" />
                                        <asp:ListItem Text="CLOSE" Value="CLOSE" />
                                    </asp:DropDownList>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <div class="portlet box green">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Lot Samples Details
                        </div>
                    </div>
                    <div class="portlet-body flip-scroll">
                        <asp:Repeater ID="dlOrderList" runat="server" OnItemCommand="dlOrderList_ItemCommand"
                            OnItemDataBound="dlOrderList_ItemDataBound">
                            <HeaderTemplate>
                                <table class="table table-bordered mudargrid">
                                    <thead class="flip-content">
                                        <tr>
                                            <th>Details
                                            </th>
                                            <th>LS ID
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>LS Date
                                            </th>
                                            <th>Received Date
                                            </th>
                                            <th></th>
                                            <th>Lotsample
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label ID="lbtnOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>' />
                                    </td>

                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDateOfOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:dd MMM yyyy}")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblReceiveDate" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "LSReceivedDate", "{0:dd MMM yyyy}")%>' />
                                        <asp:Panel ID="pReceive" runat="server" Visible="false">
                                            <asp:TextBox ID="txtReceivedDate" runat="server" Width="95%" />
                                            <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtReceivedDate" />
                                            <asp:Button ID="btnUpdate" runat="server" Text="Received" CssClass="btn btn-success" CommandName="RUpdate" />
                                        </asp:Panel>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnOrderPlace" CssClass="btn btn-success" runat="server" Text="Order"
                                            CommandName="orderplace" Visible="false" />
                                        <asp:Button ID="btnDisableOrderPlace" CssClass="btn btn-default" runat="server" Text="Order" Enabled="false" />&nbsp;
                                               <asp:Button ID="btnSplitOrder" CssClass="btn btn-success" runat="server" Text="Split" CommandName="SplitOrder" Visible="false" />
                                        <asp:Button ID="btnDisableSplitOrder" CssClass="btn btn-default" runat="server" Text="Split" Enabled="false" />&nbsp;
                                            <asp:Button ID="btnCancel" CssClass="btn btn-danger" runat="server" Text="Cancel" CommandName="ordercancel" Visible="false" />
                                        <asp:Button ID="btnDisableCancel" CssClass="btn btn-default" runat="server" Text="Cancel" Enabled="false" />

                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlLS" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LSpath")%>' CssClass="btn btn-success"
                                            runat="server">Open</asp:HyperLink>
                                        <asp:HiddenField ID="hfLS" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LSpath")%>' />
                                        <asp:LinkButton ID="lbtnLS" runat="server" Visible="false" CommandName="LSDownload" Text="Download" CssClass="btn btn-success" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" style="padding-left: 35px;">
                                        <asp:GridView ID="gvOrderHistory" runat="server" Visible="true" AutoGenerateColumns="false" CssClass="subgrid">
                                            <Columns>
                                                <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                                <asp:BoundField HeaderText="Qty" DataField="Quantity" />
                                                <asp:BoundField HeaderText="Price/KG" DataField="RateforKG" />
                                                <asp:BoundField HeaderText="Total Price" DataField="TotalPrice" DataFormatString="{0:0.00}" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="8" style="padding-left: 35px;">
                                        <asp:Label ID="lblMSg" runat="server" Visible="false"></asp:Label></td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>

                    <div id="divOrderDetails" runat="server" visible="false" align="center" class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvOrderDetais" runat="server" AutoGenerateColumns="False" DataKeyNames="LotSampleID" CssClass="table table-bordered mudargrid"
                                OnRowDataBound="gvOrderDetais_RowDataBound"
                                OnRowCommand="gvOrderDetais_RowCommand">
                                <Columns>
                                    <asp:BoundField DataField="LotSampleID" HeaderText="LotSampleID" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" DataFormatString="{0:0.00}"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="FreightTerms" HeaderText="Freight Terms" />
                                    <asp:BoundField DataField="PaymentTerms" HeaderText="Payment Terms" />
                                    <asp:BoundField DataField="TotalAmount" HeaderText="Total Price" DataFormatString="{0:0.00}" Visible="false"
                                        ItemStyle-HorizontalAlign="Center" />
                                    <%--<asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="lkDelte" runat="server" CommandName="cmd_edit" CommandArgument='<%# Eval("LotSampleID") %>'
                                            Text="Place the Order"></asp:LinkButton>
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>--%>
                                </Columns>

                            </asp:GridView>
                        </div>
                        <div class="col-sm-12">
                            <asp:Button ID="btnPlaceOrder" runat="server" Text="Place the Order" CssClass="btn btn-success" OnClick="btnPlaceOrder_Click" />
                            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default" OnClick="btnBack_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
