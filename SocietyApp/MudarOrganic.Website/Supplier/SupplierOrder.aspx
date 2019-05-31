<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="SupplierOrder.aspx.cs" Inherits="Supplier_SupplierOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height:auto">
        <div id="header_Text">
           Supplier Order History
        </div>
        <div align="center">
            Select Order Status : <asp:DropDownList ID="ddlOrderStatus" runat="server"  AutoPostBack="true"
                        onselectedindexchanged="ddlOrderStatus_SelectedIndexChanged">
                        <asp:ListItem Text="ALL" Value="ALL" />
                        <asp:ListItem Text="NEW" Value="NEW" />
                        <asp:ListItem Text="COLLECTING" Value="COLLECTING" />
                        <asp:ListItem Text="TESTING" Value="TESTING" />
                        <asp:ListItem Text="FREEZE" Value="FREEZE" />
                        <asp:ListItem Text="BLENDING" Value="BLENDING" />
                        <asp:ListItem Text="PACKING" Value="PACKING" />
                        <asp:ListItem Text="DOCUMENTING" Value="DOCUMENTING" />
                        <asp:ListItem Text="DISPATCH" Value="DISPATCH" />
                    </asp:DropDownList>
            <br />
            <br />
            <br />
            <asp:DataList ID="dlOrderList" DataKeyField="OrderID" runat="server" 
            OnItemCommand="dlOrderList_ItemCommand" BackColor="White" 
            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
            ForeColor="Black" GridLines="Vertical" >
            <FooterStyle BackColor="#CCCC99" />
            <AlternatingItemStyle BackColor="White" />
            <ItemStyle BackColor="#F7F7DE" />
            <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
          <HeaderTemplate>
            <table>
                    <tr>
                        <td style="width: 50px;" align="center">
                        Item Details
                        </td>
                        <td style="width: 80px;" align="center">Order Ref
                        </td>
                        <td style="width: 150px;" align="center">Purchase Order Ref
                        </td>
                        <td style="width: 150px;" align="center">Order Status
                        </td>
                        <td style="width: 150px;" align="center">Order Date
                        </td>
                        <td style="width: 80px;" align="center">OP Document
                        </td>
                    </tr>
            </table>
          </HeaderTemplate>
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
          <ItemTemplate>
                <table>
                    <tr>
                        <td style="width: 50px;" align="center">
                            <asp:ImageButton ID="ibtnNoExpColap" CommandName="Exp_Col" runat="server" ImageUrl="~/images/expand.JPG" />
                        </td>
                        <td style="width: 80px;" align="center">
                            <asp:LinkButton ID="lbtnOrderID" CommandName="BranchOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:LinkButton>
                            <%--<asp:Label ID="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderId")%>'></asp:Label>--%>
                        </td>
                        <td style="width: 150px;" align="center">
                            <asp:Label ID="lblPOID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchPOID")%>'></asp:Label>
                        </td>
                        <td style="width: 150px;" align="center">
                            <asp:Label ID="lblBranchOrderStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>'></asp:Label>
                        </td>
                        <td style="width: 150px;" align="center">
                            <asp:Label ID="lblBranchOrderDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchOrderDate", "{0:dd MMM yy}")%>'></asp:Label>
                        </td>
                        <td style="width: 80px;" align="center">
                            <%--<asp:Label ID="lblPDF" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchOrderPath")%>'></asp:Label>--%>
                            <asp:HyperLink ID="hlPDF" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "BranchOrderPath")%>' runat="server">Open</asp:HyperLink>
                            <asp:HiddenField ID="hfOrderPdf" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "BranchOrderPath")%>' />
                            <asp:LinkButton ID="lbtnOrderpdfDownload" runat="server" CommandName="Download" Text="Download" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:GridView ID="gvOrder" runat="server" Visible="false" AutoGenerateColumns="false">
                                <Columns>
                                    <asp:BoundField HeaderText="OrderProductID" DataField="OrderProductID" />
                                    <asp:BoundField HeaderText="ProductID" DataField="ProductID" />
                                    <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                    <asp:BoundField HeaderText="Total Price" DataField="TotalPrice" />
                                    <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                    <asp:BoundField HeaderText="Packing(25)" DataField="Packing25" />
                                    <asp:BoundField HeaderText="Packing(180)" DataField="Packing180" />
                                </Columns>
                                <RowStyle BackColor="#F7F7DE" />
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
          </ItemTemplate>
        </asp:DataList>
        </div>
 </div>
</asp:Content>

