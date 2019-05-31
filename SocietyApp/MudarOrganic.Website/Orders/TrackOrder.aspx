<%@Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="TrackOrder.aspx.cs" Inherits="Orders_TrackOrder" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto;">
        <table width="100%">
            <tr>
                <td align="center">
                    <div id="header_Text">
                        &nbsp;PO Details</div>
                    <div>
                        <table>
                            <tr>
                                <%-- <td>
                        OrderID/PoID
                    </td>--%>
                                <td>
                                    <asp:TextBox ID="txtOrderID" runat="server" Visible="false"></asp:TextBox>
                                </td>
                                <td>
                                    <asp:Button ID="btnGo" Visible="false" runat="server" CssClass="fb8_go" Text="GO"
                                        OnClick="btnGo_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divOrderTrack" runat="server" align="center">
                        <asp:DataList ID="dlOrderTrack" runat="server" DataKeyField="OrderID" OnItemCommand="dlOrderTrack_ItemCommand" OnItemDataBound="dlOrderTrack_ItemDataBound">
                            <HeaderTemplate>
                                <table>
                                    <tr style="background-color: #6B696B; font-weight: bold; color: White;">
                                        <td style="width:90px;" align="center">
                                            Order NO
                                        </td>
                                        <td style="width:120px;" align="center">
                                            PO NO
                                        </td>
                                        <td style="width:120px;" align="center">
                                            PO Date
                                        </td>
                                        <td style="width:120px;" align="center">
                                            Status
                                        </td>
                                        <td style="width:140px;" align="center">
                                            Company Name
                                        </td>
                                        <td style="width:80px;" align="center" Visible="false">
                                            PDF
                                        </td>
                                    </tr>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="background-color: #F7F7DE;">
                                    <td style="width:90px;" align="center">
                                        <asp:Label ID="lblOrderID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                                    </td>
                                    <td style="width:120px;" align="center">
                                        <asp:Label ID="lblPOID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"PurchaseOrderID")%>'></asp:Label>
                                    </td>
                                    <td style="width:120px;" align="center">
                                        <asp:Label ID="lblOrderDate" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OrderDate", "{0:dd MMM yyyy}") %>'></asp:Label>
                                    </td>
                                    <td style="width:120px;" align="center">
                                        <asp:Label ID="lblOrderStatus" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"OrderStatus") %>'></asp:Label>
                                    </td>
                                    <td style="width:140px;" align="center">
                                        <asp:Label ID="lblBuyCompany" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"BuyerCompanyName") %>'></asp:Label>
                                    </td>
                                    <td style="width:80px;" align="center" Visible="false">
                                        <asp:HyperLink ID="hlPDF" runat="server"  Target="_blank" NavigateUrl='<%#DataBinder.Eval(Container.DataItem,"PurchaseOrderPath") %>'>Open</asp:HyperLink>
                                        &nbsp;
                                        <asp:ImageButton ID="btnDownload" runat="server" CommandName="Download" ImageUrl="~/images/download.jpg" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr style="background-color: #6B696B; color: White;">
                                    <td colspan="6" align="center">
                                        Comments
                                    </td>
                                </tr>
                                <tr style="background-color: #F7F7DE;">
                                    <td colspan="6" align="center">
                                        <asp:Label ID="lblComments" runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"Comments") %>'></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="6">
                                        &nbsp;
                                    </td>
                                </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                        <asp:GridView ID="gvProdcutList" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical">
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundField HeaderText="OrderProductID" DataField="OrderProductID" Visible="false" />
                                <asp:BoundField HeaderText="ProductID" DataField="ProductID" ItemStyle-HorizontalAlign="Center"/>
                                <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                <asp:BoundField HeaderText="Total Price" DataField="TotalPrice" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Quantity" DataFormatString="{0:n0}" DataField="Quantity"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Packing(25)" DataFormatString="{0:n0}" DataField="Packing25"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField HeaderText="Packing(180)" DataFormatString="{0:n0}" DataField="Packing180"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </div>
                    <div id="divReports" runat="server">
                        <table>
                            <tr>
                                <td align="right">
                                    Invoice :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlInvoice" runat="server" NavigateUrl="" Text="" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Packing :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlPacking" runat="server" Text="PDF" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Non-Haz Sea :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlHazSea" runat="server" Text="PDF" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Non-Haz Air :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlHazAir" runat="server" Text="PDF" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Cover Letter :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlCoverLetter" runat="server" Text="PDF" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Fir Cover Letter :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlFIRCover" runat="server" Text="PDF" Target="_blank" />
                                </td>
                            </tr>
                        </table>
                        <table>
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:GridView ID="gvReports" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                        AutoGenerateColumns="false">
                                        <Columns>
                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                            <asp:HyperLinkField DataNavigateUrlFields="PP" HeaderText="PP" Text="PDF" Target="_blank" />
                                            <asp:HyperLinkField DataNavigateUrlFields="AR" HeaderText="AR" Text="PDF" Target="_blank" />
                                            <asp:HyperLinkField DataNavigateUrlFields="SP" HeaderText="SP" Text="PDF" Target="_blank" />
                                            <asp:HyperLinkField DataNavigateUrlFields="BO" HeaderText="BO" Text="PDF" Target="_blank" />
                                            <asp:HyperLinkField DataNavigateUrlFields="CRY" HeaderText="CRY" Text="PDF" Target="_blank" />
                                            <asp:HyperLinkField DataNavigateUrlFields="CRY_P" HeaderText="CRY_P" Text="PDF" Target="_blank" />
                                            <asp:HyperLinkField DataNavigateUrlFields="LABEL" HeaderText="LABEL" Text="PDF" Target="_blank" />
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
                            <tr>
                                <td>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="btnDownloadAll" runat="server" Text="Download All" 
                                        CssClass="fb8" onclick="btnDownloadAll_Click" />
                                </td>
                               
                            </tr>
                             <tr>
                                <td>
                                    <asp:TextBox ID="txtEmail" runat="server"  CssClass ="textbox_Style"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnEmail" runat="server" Text="Email" CssClass="fb8" />
                                    <br /><span style="color:Red">Multi Email's use ',' or ';'</span> 
                                </td>
                               
                            </tr>
                        </table>
                    </div>
                    <div id="divBuyerReports" runat="server">
                        <table>
                            <tr>
                                <td align="right">
                                    Invoice :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlInvoice_B" runat="server" NavigateUrl="" Text="" Target="_blank" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Packing :
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlPacking_B" runat="server" Text="PDF" Target="_blank" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divBuyerPO" runat="server">
                    <br />
                    <br />
                    <div id="header_Text">Upload PO</div>
                        <table width="100%" align="center">
                        <tr>
                            <td rowspan="5" style="width: 20%;"></td>
                            <td align="right">
                                PO Number
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPO" runat="server" Text="" CssClass="textbox_Style" />
                            </td>
                            <td rowspan="5" style="width: 20%;"></td>
                        </tr>
                        <%--<tr>
                            <td align="right">
                                PO Date
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPODate" runat="server" CssClass="textbox_Style" />
                                <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtPODate"></asp:CalendarExtender>                                
                            </td>
                        </tr>--%>
                        <tr>
                            <td align="right">
                                Upload PO (.PDF)
                            </td>
                            <td align="left">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                        </tr>
                        <%--<tr>
                            <td align="right">
                                Comment's
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtcomments" TextMode="MultiLine" runat="server"></asp:TextBox>
                            </td>
                        </tr>--%>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="fb8" OnClick="btnUpload_Click" />
                            </td>
                        </tr>
                    </table>
                    </div>
                    
                </td>
            </tr>
        </table>
    </div>
</asp:Content>


