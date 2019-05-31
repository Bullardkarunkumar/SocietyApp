<%@ Page Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="PreOrder.aspx.cs" Inherits="Admin_PreOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        th, td {
            text-align: center;
        }
    </style>
    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-cogs"></i>Pre Order
            </div>
        </div>
        <div class="portlet-body flip-scroll">
            <div style="text-align: center; margin-bottom: 15px">
                <asp:linkbutton id="btnCreate" runat="server" text="Create New Order" cssclass="btn btn-primary" onclick="btnCreate_Click" />
            </div>
            <asp:panel id="pnlLst" runat="server">
                 <asp:Repeater ID="dlOrderList" runat="server"
                    OnItemCommand="dlOrderList_ItemCommand"
                    OnItemDataBound="dlOrderList_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-bordered mudargrid">
                            <thead class="flip-content">
                                <tr>
                                    <th>Details
                                    </th>
                                    <th>Order Ref</th>
                                    <th>Buyer Ref</th>
                                    <th>Order Type
                                    </th>
                                    <th>Order Status
                                    </th>
                                    <th>Order Date
                                    </th>
                                    <th>Lot Sample
                                    </th>
                                    <th>Branch PO
                                    </th>
                                </tr>
                            </thead>
                            <tbody>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:Label ID="lblorderid" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem,"OrderID") %>'></asp:Label>
                                <asp:ImageButton ID="ibtnNoExpColap" CommandName="Exp_Col" runat="server" ImageUrl="~/images/expand.JPG" />
                            </td>
                            <td>
                                <asp:Label ID="lbtnOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchPOID")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BOrderType")%>' />
                            </td>

                            <td>
                                <asp:Label ID="lblBranchOrderStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>'></asp:Label>
                            </td>
                            <td>
                                <asp:Label ID="lblBranchOrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchOrderDate", "{0:dd MMM yy}")%>'></asp:Label>
                            </td>
                            <td align="center">
                                <asp:HiddenField ID="hfLSpdf" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BLSpath")%>' />
                                <asp:HyperLink ID="hlLSpdf" CssClass="btn btn-info btn-link btn-xs" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "BLSpath")%>' runat="server"> <i class="fa fa-2x fa-download"></i></asp:HyperLink>
                                <%--<asp:LinkButton ID="lbtnLSpdf" CssClass="btn btn-link btn-xs" Visible="false" runat="server" CommandName="Download" Text="Download" />--%>
                            </td>
                            <td align="center">
                                <asp:HiddenField ID="hfOrderPdf" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BranchOrderPath")%>' />
                                <asp:HyperLink ID="hlPDF" CssClass="btn btn-info btn-link btn-xs" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "BranchOrderPath")%>' runat="server"><i class="fa fa-2x fa-download"></i></asp:HyperLink>
                                <%--<asp:LinkButton ID="lbtnOrderpdfDownload" CssClass="btn btn-link btn-xs" Visible="false" runat="server" CommandName="Download" Text="Download" />--%>
                            </td>
                        </tr>
                        <tr id="trSubTable" runat="server" style="display: none">
                            <td colspan="8">
                                <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="false" CssClass="subgrid">
                                    <Columns>
                                        <asp:BoundField HeaderText="ProductID" DataField="ProductID" Visible="false" />
                                        <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                        <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Price/KG" DataField="BranchRateforKG" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Amt" DataField="Amount" DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Drums(25)" DataField="Packing25" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Drums(180)" DataField="Packing180" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <HeaderStyle Font-Size="Small" ForeColor="#BDBDBD" BackColor="#66CCFF"  />
                                    <RowStyle Font-Size="Small" ForeColor="#BDBDBD" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:Label ID="lblMSg" runat="server" Visible="false"></asp:Label>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        </tbody>
                    </table>
                    </FooterTemplate>
                </asp:Repeater>
            </asp:panel>
        </div>
    </div>
</asp:Content>
