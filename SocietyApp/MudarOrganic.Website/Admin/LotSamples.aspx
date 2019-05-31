<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MudarMaster.master" CodeFile="LotSamples.aspx.cs" Inherits="Admin_LotSamples" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height:auto">
        <div id="header_Text">
             Lot Samples
        </div>
        <div align="center">
            <br />
            Select Order Status <br />
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
            <br />
            <br />
            <asp:DataList ID="dlOrderList" DataKeyField="OrderID" runat="server"
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                onitemcommand="dlOrderList_ItemCommand" 
                onitemdatabound="dlOrderList_ItemDataBound">
                <FooterStyle BackColor="#CCCC99" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#F7F7DE" />
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td style="width: 80px;" align="center">
                                Details
                            </td>
                            <td style="width: 110px;" align="center">
                                Order ID
                            </td>
                            <td style="width: 120px;" align="center">
                                LotSample ID
                            </td>
                          
                            <td style="width: 90px;" align="center">
                                Status
                            </td>
                            <td style="width: 120px;" align="center">
                                Date Of Order
                            </td>
                           <%--<td style="width: 80px;" align="center">
                                PDF
                            </td>--%>
                        </tr>
                    </table>
                </HeaderTemplate>
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <ItemTemplate>
                    <table>
                        <tr>
                            <td style="width: 80px;" align="center">
                                <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                    runat="server" />
                            </td>
                            <td style="width: 120px;" align="center">
                                <asp:LinkButton ID="lbtnOrderID" CommandName="BranchOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:LinkButton>
                            </td>
                            <td style="width: 120px;" align="center">
                                <asp:Label ID="lblPOID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderID")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblBuyerID" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerID")%>' />
                            </td>
                          
                            <td style="width: 100px;" align="center">
                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' />
                            </td>
                            <td style="width: 120px;" align="center">
                                <asp:Label ID="lblDateOfOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:dd MMM yyyy}")%>' />
                            </td>
                          <%--<td style="width: 50px;" align="center">
                               
                                <asp:HyperLink ID="hlPDF" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderPath")%>'
                                    runat="server">Open</asp:HyperLink>
                                <asp:HiddenField ID="hfOrderPdf" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderPath")%>' />
                                
                                <asp:LinkButton ID="lbtnOrderpdfDownload" runat="server" CommandName="Download" Text="Download" />
                            </td>--%>
                        </tr>
                        <tr>
                            <td colspan="8">
                                <asp:GridView ID="gvNewOrder" runat="server" Visible="false" AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField HeaderText="OrderProductID" DataField="OrderProductID" Visible="false" />
                                        <asp:BoundField HeaderText="ProductID" DataField="ProductID" />
                                        <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                        <asp:BoundField HeaderText="Total Price" DataField="TotalPrice" ItemStyle-HorizontalAlign="Center"
                                            DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Quantity" DataField="Quantity" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Packing(25)" DataField="Packing25" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Packing(180)" DataField="Packing180" ItemStyle-HorizontalAlign="Center" />
                                    </Columns>
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
            </asp:DataList>
        </div>
    </div>
    
</asp:Content>
