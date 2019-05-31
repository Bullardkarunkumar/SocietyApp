<%@ Page Title="Mudarorganic-OrdersforAdmin" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="OrderAdmin.aspx.cs" Inherits="Admin_OrderAdmin" %>

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
                        <div class="form-inline col-lg-offset-2">
                            <div class="form-group">
                                <label class="control-label">Order Status</label>
                                <asp:DropDownList ID="ddlOrderStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="ALL" Value="ALL" />
                                    <asp:ListItem Text="NEW" Value="NEW" />
                                    <asp:ListItem Text="INPROCESS" Value="INPROCESS" />
                                    <asp:ListItem Text="BLENDING" Value="BLENDING" />
                                    <asp:ListItem Text="PACKING" Value="PACKING" />
                                    <asp:ListItem Text="SAMPLE DISPATCH" Value="SAMPLE DISPATCH" />
                                    <asp:ListItem Text="COLLECTING" Value="COLLECTING" />
                                    <asp:ListItem Text="DISPATCH" Value="DISPATCH" />
                                    <asp:ListItem Text="CANCEL" Value="CANCEL" />
                                    <asp:ListItem Text="CLOSE" Value="CLOSE" />
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label class="control-label">Sorting By</label>
                                <asp:DropDownList ID="ddlSortBy" runat="server" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                                    <asp:ListItem Value="OrderID">Order ID</asp:ListItem>
                                    <asp:ListItem Value="OrderType">Order Type</asp:ListItem>
                                    <asp:ListItem Value="OrderStatus">Order Status</asp:ListItem>
                                    <asp:ListItem Value="OrderDate">Order Date</asp:ListItem>
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label class="control-label">Except By</label>
                                <asp:DropDownList ID="ddlExcept" runat="server" OnSelectedIndexChanged="ddlSortBy_SelectedIndexChanged">
                                    <asp:ListItem Text="Except" Value="NEW" />
                                </asp:DropDownList>
                            </div>
                            <asp:Button runat="server" ID="RefreshRepeater" class="btn btn-circle btn-primary" Text="Get" OnClick="RefreshRepeater_Click" />
                        </div>
                    </div>
                </div>
                <div class="portlet box green">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Admin Orders List
                        </div>
                    </div>
                    <div class="portlet-body flip-scroll">
                        <asp:Repeater ID="dlOrderList" runat="server"
                            OnItemCommand="dlOrderList_ItemCommand"
                            OnItemDataBound="dlOrderList_ItemDataBound">
                            <HeaderTemplate>
                                <table class="table table-bordered mudargrid">
                                    <thead class="flip-content">
                                        <tr>
                                            <th>Details
                                            </th>
                                            <th nowrap="nowrap">Order ID</th>
                                            <th nowrap="nowrap">Order Type
                                            </th>
                                            <th>Buyer Name
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>Order Date
                                            </th>
                                            <th>Buyer PO
                                            </th>
                                        </tr>
                                    </thead>
                                    <tbody>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblorderid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"OrderID") %>'></asp:Label>
                                        <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:LinkButton ID="lbtnOrderID" CommandName="BranchOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderType")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBuyerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerCompanyName")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDateOfOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:dd MMM yyyy}")%>' />
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:HyperLink ID="hlPDF" CssClass="btn btn-link btn-sm" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>'
                                            runat="server">Open</asp:HyperLink>
                                        <asp:HiddenField ID="hfOrderPdf" Visible="false" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>' />
                                        <asp:LinkButton ID="lbtnOrderpdfDownload" CssClass="btn btn-link btn-sm" Visible="false" runat="server" CommandName="Download" Text="Download" />
                                    </td>

                                </tr>
                                <tr id="trSubTable" runat="server" style="display: none">
                                    <td colspan="8">
                                        <asp:GridView ID="gvNewOrder" runat="server" AutoGenerateColumns="false" CssClass="subgrid">
                                            <Columns>
                                                <asp:BoundField HeaderText="ProductID" DataField="ProductID" Visible="false" />
                                                <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                                <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Price/KG" DataField="RateforKG" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Amt" DataField="TotalPrice" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="Drums(25)" DataField="Packing25" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Drums(180)" DataField="Packing180" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="" DataField="Country" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                            <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                            <HeaderStyle Font-Size="Small" ForeColor="#BDBDBD" />
                                            <RowStyle Font-Size="Small" ForeColor="#BDBDBD" HorizontalAlign="Center" VerticalAlign="Middle" />
                                        </asp:GridView>
                                        <asp:Label ID="lblMSg" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblCourier" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </tbody>
                    </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

