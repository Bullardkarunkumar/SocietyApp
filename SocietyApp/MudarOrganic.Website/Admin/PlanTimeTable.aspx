<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="PlanTimeTable.aspx.cs" Inherits="Admin_PlanTimeTable" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
    <div id="content_area_Home" style="height: auto;">
       <div id="Div1" style="height:auto">
        <div id="header_Text">
            Admin Orders List
        </div>
        <div align="center">
            <br />
            Order Status  <br />
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
            <br /><br />
        <asp:DataList ID="dlOrderList" DataKeyField="OrderID" runat="server"
                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                onitemcommand="dlOrderList_ItemCommand"> 
                <%--onitemdatabound="dlOrderList_ItemDataBound"--%>
                <FooterStyle BackColor="#CCCC99" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#F7F7DE" />
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderTemplate>
                    <table>
                        <tr>
                            <td align="center" style="width:100px;">
                                Details
                            </td>
                            <td  align="center" style="width:120px;">
                                Order ID
                            </td>
                            <td  align="center" style="width:120px;"><%--style="width:100px;"--%>
                                Order Type
                            </td>
                            <td  align="center" style="width:130px;"><%--style="width: 160px;"--%>
                                Buyer Name
                            </td>
                            <td  align="center" style="width:120px;"><%--style="width: 110px;"--%>
                                Status
                            </td>
                            <td  align="center" style="width:120px;"><%--style="width: 120px;"--%>
                                 Order Date
                            </td>
                            <td align="center" style="width:100px;"><%--style="width: 80px;"--%>
                                Buyer PO
                            </td>
                        </tr>
                    </table>
                </HeaderTemplate>
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <ItemTemplate >
                    <table>
                        <tr>
                            <td align="center" style="width:100px;">
                                <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                    runat="server" />
                            </td>
                            <td align="center" style="width:120px;">
                                <asp:LinkButton ID="lbtnOrderID" CommandName="BranchOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:LinkButton>
                            </td>
                            <td align="center" style="width:120px;">
                                <asp:Label ID="lblType"  runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderType")%>' />
                            </td>
                            <td align="center" style="width:130px;">
                                <asp:Label ID="lblBuyerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerCompanyName")%>' />
                            </td>
                            <td align="center" style="width:120px;">
                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' />
                            </td>
                            <td align="center" style="width:120px;"><%--style="width: 110px;"--%>
                                <asp:Label ID="lblDateOfOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:dd MMM yyyy}")%>' />
                            </td>
                            <td style="width:100px;" align="center">
                                <%--<asp:Label ID="lblPDF" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderPath")%>' />--%>
                                <asp:HyperLink ID="hlPDF" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>'
                                    runat="server">Open</asp:HyperLink>
                                <asp:HiddenField ID="hfOrderPdf" Visible="false" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>' />
                                <%--<asp:LinkButton ID="lbtnOrderpdf" runat="server" CommandName="Display" Text="Display" PostBackUrl='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderPath")%>' />--%>
                                <asp:LinkButton ID="lbtnOrderpdfDownload" Visible="false" runat="server" CommandName="Download" Text="Download" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" align="center">
                                <asp:GridView ID="gvNewOrder" runat="server" AutoGenerateColumns="false">
                                    <Columns>
                                        <%--<asp:BoundField HeaderText="OrderProductID" DataField="OrderProductID" Visible="false" />--%>
                                        <asp:BoundField HeaderText="ProductID" DataField="ProductID" Visible="false"/>
                                        <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                        <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Price/KG" DataField="RateforKG" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Amt" DataField="TotalPrice" ItemStyle-HorizontalAlign="Center"
                                            DataFormatString="{0:0.00}" />
                                        <asp:BoundField HeaderText="Drums(25)" DataField="Packing25" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="Drums(180)" DataField="Packing180" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField HeaderText="" DataField="Country" ItemStyle-HorizontalAlign="Center" />
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

