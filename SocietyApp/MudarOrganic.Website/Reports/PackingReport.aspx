<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="PackingReport.aspx.cs" Inherits="Reports_PackingReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
 <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
           Packing List
        </div>
        <div>
            <table border="1" align="center" style="font-family: Verdana; width: 885px">
                <tr>
                    <td colspan="6" align="center" style="font-family: Verdana;">
                        <h2>
                            Packing List</h2>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="font-family: Verdana;">
                        CUCERT - 025367
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#ffcc66" style="font-size: larger;">
                        EXPORT
                    </td>
                    <td colspan="4" align="center" bgcolor="#ffcc66" style="font-size: larger;">
                        EXPORTER&nbsp; INFO
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="5">
                        Mudar India Exports
                        <br />
                        <asp:Label ID="lblMudarAddress" runat="server"></asp:Label>
                    </td>
                    <td>
                        Contact &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblContactPerson" runat="server" />
                    </td>
                    <td>
                        Invoice No
                    </td>
                    <td>
                        <asp:Label ID="lblInvoice" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Phone/Fax
                    </td>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" />
                    </td>
                    <td>
                        Date
                    </td>
                    <td>
                        <asp:Label ID="lblInvoiceDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Mobile
                    </td>
                    <td>
                        <asp:Label ID="lblMobile" runat="server" />
                    </td>
                    <td>
                        IE Code
                    </td>
                    <td>
                        <asp:Label ID="lblIECode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        E-Mail
                    </td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" />
                    </td>
                    <td>
                        FDA
                    </td>
                    <td>
                        <asp:Label ID="lblFDA" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Website
                    </td>
                    <td>
                        <asp:Label ID="lblWebsite" runat="server" />
                    </td>
                    <td>
                        AP VAT
                    </td>
                    <td>
                        <asp:Label ID="lblAPVAT" runat="server" />
                    </td>
                </tr>
            </table>
            <table border="1" align="center" style="font-family: Verdana; width: 885px">
                <tr>
                    <td width="33%" align="center" bgcolor="#ffcc66" style="font-size: larger;">
                        Buyer
                    </td>
                    <td width="33%" align="center" bgcolor="#ffcc66" style="font-size: larger;">
                        Notify
                    </td>
                    <td width="33%" align="center" bgcolor="#ffcc66" style="font-size: larger;">
                        Consignee
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Label ID="lblBuyerAddress" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblNotifyAddress" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="lblConsigneeAddress" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="1" align="center" style="font-family: Verdana; width: 885px">
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        PO no
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblPO" runat="server" />
                    </td>
                    <td colspan="2" align="center">
                        Date
                    </td>
                    <td>
                        <asp:Label ID="lblPODate" runat="server" />
                    </td>
                </tr>
                <tr style="border-left-color: White;">
                    <td colspan="6" style="color: White;">
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center">
                        <asp:GridView ID="gvPurchaseOrder" DataKeyNames="ProductID" runat="server" AutoGenerateColumns="False"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundField DataField="BatchID" HeaderText="Batch No">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalDrums" HeaderText="Number Of Packing">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="Net Wt Qty(KG)" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="GrossQuantity" HeaderText="Gross Wt Qty(KG)" DataFormatString="{0:n0}"
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
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td width="56%" align="center">
                                    Totals without Pallets
                                </td>
                                <td width="22%">
                                    <asp:Label ID="lblNetQty" runat="server" />
                                </td>
                                <td width="22%">
                                    <asp:Label ID="lblGrossQty" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td>
                        Price Terms
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblPriceTerms" runat="server" />
                    </td>
                    <td colspan="2">
                        Payment Terms
                    </td>
                    <td>
                        <asp:Label ID="lblPayment" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Transport
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblTransport" runat="server" />
                    </td>
                    <td colspan="2">
                        Freight Terms
                    </td>
                    <td>
                        <asp:Label ID="lblFreight" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Origin Country
                    </td>
                    <td colspan="2">
                        India
                    </td>
                    <td colspan="2">
                        Destination Country
                    </td>
                    <td>
                        <asp:Label ID="lblDCountry" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Loading Port
                    </td>
                    <td colspan="2">
                        MUMBAI
                    </td>
                    <td colspan="2">
                        Destination Port
                    </td>
                    <td>
                        <asp:Label ID="lblDPort" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#ffcc66" style="font-size: larger;">
                        Bank Info
                    </td>
                    <td colspan="4" rowspan="6">
                        Declaration: We declare that this Invoice shows the actual price of the
                        <br />
                        goods described and that all particulars are true and correct.
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblBank" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        A/C. No:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblBankAccount" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        ADC Code:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblBankADC" runat="server" />
                    </td>
                </tr>
            </table>
    </div>
        <div align="center">
        <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="fb8" 
                onclick="btnConfirm_Click"/>
               
    </div>
    </div>
</asp:Content>

