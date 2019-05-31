<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master"  AutoEventWireup="true" CodeFile="PurchaseOrder.aspx.cs" Inherits="Reports_PurchaseOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
    <div id="content_area_Home" style="height: auto">
  <div id="header_Text">
         Place Order
        </div>
        <div>
        <table border="green" align="center" style="width: 886px">
            <tr>
                <td rowspan="3" align="left" style="width: 394px">
                    Mudar India Exports<br />
                    <asp:Label ID="txtCompanyAddress" runat="server" Text="" />
                    <%--<asp:TextBox ID="txtCompanyAddress" runat="server" TextMode="MultiLine" Text="" />--%>
                </td>
                <td >
                    PO#
                </td>
                <td>
                    <asp:Label ID="lblPO" runat="server"></asp:Label><asp:Label ID="lblBPO" runat="server" />
                </td>
            </tr>
            <tr>
                <td >
                    Date
                </td>
                <td>
                    <asp:Label ID="lblDate" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    TIN#
                </td>
                <td>
                    <asp:Label ID="lblTAN" runat="server" Text="00000"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Details of Overseas PO received
        <table>
            <tr>
                <td style="height: 26px">
                    Overseas PO/Intent Number
                </td>
                <td align="center" style="width: 128px; height: 26px;" colspan="4">
                    <asp:Label ID="lblOverSeasPOIntent" runat="server" Text="0"></asp:Label>
                </td>
               
                
                <td style="height: 26px">
                    Date
                </td>
                <td align="center" style="width: 148px; height: 26px;" colspan="2">
                    <asp:Label ID="lblOverSeaDate" runat="server" Text=""></asp:Label>
                </td>
             
              
                <td style="height: 26px">
                    Destination Country
                </td>
                <td align="center" style="width: 138px; height: 26px;">
                    <asp:Label ID="lblDestCountry" runat="server" Text=""></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        Please arrange the shipment as per the Terms & Conditions mentioned
        <br />
        All prices below are in <asp:Label ID="lblmoney" runat="server" />
        <table>
            <tr>
                <td colspan="2">
                    <asp:GridView ID="gvPurchaseOrder" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                        
                         
                             <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lblsnnumber" runat="server" Text=" <%#Container.DisplayIndex+1 %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            
                           <%-- <asp:TemplateField HeaderText="S.No">
                                <ItemTemplate>
                                    <%#Container.DisplayIndex+1 %>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                            <asp:BoundField DataField="ProductID" HeaderText="Product ID">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                   </asp:BoundField>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                   </asp:BoundField>
                            <asp:BoundField DataField="Quantity"  HeaderText="Quantity (KG)" DataFormatString="{0:0.00}">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                   </asp:BoundField>
                            <asp:BoundField DataField="PackingDetails25" DataFormatString="{0:n0}" HeaderText="Packing Details (25 KG)">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                   </asp:BoundField>
                            <asp:BoundField DataField="PackingDetails180" DataFormatString="{0:n0}" HeaderText="Packing Details (180 KG)">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                   </asp:BoundField>
                            <asp:BoundField DataField="Price" DataFormatString="{0:n0}" HeaderText="Price / KG">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                   </asp:BoundField>
                            <asp:BoundField DataField="TotalPrices" DataFormatString="{0:n0}" HeaderText="Total Amount">
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                   </asp:BoundField>
                        </Columns>
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
                    Total No of Drums:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTotalDrums" runat="server" Text="0" />
                </td>
                <td>
                    Total Amount:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Label ID="lblTotalprice" runat="server" Text="0" />
                </td>
            </tr>
            <tr><td width="30%">Amount in Words </td><td><asp:Label ID="lblAmountword" runat="server"/></td></tr>
        </table>
    </div>
    <div>
        <table>
        <tr><td colspan="2"><asp:Label ID="lblplaceofdelivery" runat="server" /><asp:Label ID="lblComments" runat="server" /><asp:Label ID="lblOrganicType" runat="server" /><asp:Label ID="lblSplitID" runat="server" /></td></tr>
        <tr><td colspan="2"><asp:Label ID="lblBuyerCopmany" runat="server" /></td></tr>
        <tr><td colspan="2"><asp:Label ID="lblDrum25" runat="server" />&nbsp;<asp:Label ID="lblDrum180" runat="server" /></td></tr>
        <tr><td colspan="2"><asp:Label ID="lblBuyerAddress" runat="server" />&nbsp;<asp:Label ID="lblBankAddress" runat="server" />&nbsp;<asp:Label ID="lblNotifyAddress" runat="server" /></td></tr>
        <tr><td colspan="2"><asp:Label ID="lblTransportmode" runat="server" /></td></tr>
        <tr><td colspan="2"><asp:Label ID="lblPaymentTerms" runat="server" />&nbsp;<asp:Label ID="lblCreditDays" runat="server" /><asp:Label ID="lblPriceTerm" runat="server" />
            </td></tr>
            <tr>
                <td style="width: 207px">
                    Price Terms
                <td style="width: 321px">
                    <%--<asp:Label ID="lblPriceTerms" runat="server"></asp:Label>--%>
                    <asp:DropDownList ID="ddlPriceTerms" runat="server">
                    </asp:DropDownList>
                </td>
                <td style="width: 343px">
                    Address for Delivery
                </td>
            </tr>
            <tr>
                <td style="width: 207px">
                    Taxes & Duties
                </td>
                <td style="width: 321px">
                    <asp:Label ID="lblTaxesDuties" runat="server"></asp:Label>
                </td>
                <td rowspan="5" style="width: 343px" valign="top">
                    <asp:Label ID="lblAddress" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 207px">
                    Mandy Tax
                </td>
                <td style="width: 321px">
                    <asp:Label ID="lblMandyTax" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td style="width: 207px">
                    Mode of Transport
                </td>
                <td style="width: 321px">
                    <%--<asp:Label ID="lblModeofTransport" runat="server"></asp:Label>--%>
                    <asp:DropDownList ID="ddlModeofTransport" runat="server">
                        <asp:ListItem Text="Air" Value="1" Selected="True"></asp:ListItem>
                        <asp:ListItem Text="Sea" Value="2"></asp:ListItem>
                        <asp:ListItem Text="Rail" Value="3"></asp:ListItem>
                        <asp:ListItem Text="Road" Value="4"></asp:ListItem>
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 207px">
                    Place of Delivery
                </td>
                <td style="width: 321px">
                    <%--<asp:Label ID="lblPlaceofDelivery" runat="server"></asp:Label>--%>
                    <asp:DropDownList ID="ddlPlaceOfDelivery" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlaceOfDelivery_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td style="width: 207px">
                    Sales Tax Terms
                </td>
                <td style="width: 321px">
                    <asp:Label ID="lblSalesTaxTerms" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div>
        &nbsp;&nbsp;
        E-mail the below mentioned documents IMMEDIATELY after dispatching the Shipment
        <table>
            <tr>
                <td>
                    <asp:GridView ID="gvReports" runat="server" AutoGenerateColumns="False" Width="878px"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="80px">
                                <ItemTemplate>
                                    <asp:Label ID="lblsno" runat="server" Text=" <%#Container.DisplayIndex+1 %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ReportID" HeaderText="ReportID" HeaderStyle-HorizontalAlign="Center" Visible="false" />
                            <asp:BoundField DataField="ReportName" HeaderText="Report Name" HeaderStyle-HorizontalAlign="Center">
                               <ItemStyle HorizontalAlign="Center" />
                            </asp:BoundField>
                            <asp:TemplateField HeaderText="Check" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbReport" runat="server" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Date of Email" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtReportDate" runat="server" Text="" />
                                    <asp:CalendarExtender ID="ceReportDate" runat="server" TargetControlID="txtReportDate">
                                    </asp:CalendarExtender>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                            </asp:TemplateField>
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr><td></td></tr>
            </table>
    </div>
    <div align="center">
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="fb8" OnClick="btnConfirm_Click" />
    </div>
 </div>
</asp:Content>
