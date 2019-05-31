<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="InvoiceReport.aspx.cs" Inherits="Admin_Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Invoice Report
        </div>
        <div>
            <table border="1" align="center" style="font-family: Verdana; width: 885px">
                <tr>
                    <td colspan="6" align="center" style="font-family: Verdana;vertical-align:middle;height:50px;">
                        <h2>Invoice</h2>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" align="center" style="font-family: Verdana;">CUCERT - 025367
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#ffcc66" style="font-size: larger;">EXPORT
                    </td>
                    <td colspan="4" align="center" bgcolor="#ffcc66" style="font-size: larger;">EXPORTER&nbsp; INFO
                    </td>
                </tr>
                <tr>
                    <td colspan="2" rowspan="5">
                        <br />
                        <asp:Label ID="lblMudarAddress" runat="server"></asp:Label>
                    </td>
                    <td>Contact &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblContactPerson" runat="server" />
                    </td>
                    <td>Invoice No
                    </td>
                    <td>
                        <asp:Label ID="lblInvoice" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Phone/Fax
                    </td>
                    <td>
                        <asp:Label ID="lblPhone" runat="server" />
                    </td>
                    <td>Date
                    </td>
                    <td>
                        <asp:Label ID="lblInvoiceDate" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Mobile
                    </td>
                    <td>
                        <asp:Label ID="lblMobile" runat="server" />
                    </td>
                    <td>IE Code
                    </td>
                    <td>
                        <asp:Label ID="lblIECode" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>E-Mail
                    </td>
                    <td>
                        <asp:Label ID="lblEmail" runat="server" />
                    </td>
                    <td>FDA
                    </td>
                    <td>
                        <asp:Label ID="lblFDA" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Website
                    </td>
                    <td>
                        <asp:Label ID="lblWebsite" runat="server" />
                    </td>
                    <td>AP VAT
                    </td>
                    <td>
                        <asp:Label ID="lblAPVAT" runat="server" />
                    </td>
                </tr>
            </table>
            <table border="1" align="center" style="font-family: Verdana; width: 885px">
                <tr>
                    <td width="33%" align="center" bgcolor="#ffcc66" style="font-size: larger;">Buyer
                    </td>
                    <td width="33%" align="center" bgcolor="#ffcc66" style="font-size: larger;">Notify
                    </td>
                    <td width="33%" align="center" bgcolor="#ffcc66" style="font-size: larger;">Consignee
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
            <table border="1" align="center" style="font-family: Verdana;width:885px;vertical-align:">
                <tr>
                    <td colspan="6"></td>
                </tr>
                <tr>
                    <td align="center">PO no
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblPO" runat="server"/>
                    </td>
                    <td colspan="2" align="center">Date
                    </td>
                    <td>
                        <asp:Label ID="lblPODate" runat="server" />
                    </td>
                </tr>
                <tr style="border-left-color: White;">
                    <td colspan="6" style="color: White;">
                        <asp:Label ID="lblbranchorderID" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <asp:GridView ID="gvPurchaseOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" DataKeyNames="ProductID" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="875px">
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%">
                                            <tr>
                                                <td colspan="2">Marking
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td colspan="2" align="center">Lot :
                                                    <asp:Label ID="lblBlendID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Blending_BatchID")%>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="50%">
                                                    <asp:Label ID="lblDrumF" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DrumF")%> '></asp:Label>
                                                    
                                                </td>
                                                <td align="center" width="50%">&nbsp;
                                                    <asp:Label ID="lblDrumT" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DrumT")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%-- <asp:BoundField DataField="BatchID" HeaderText="Lotnumber">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>--%>
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td colspan="2" align="center">Qty(KG)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="50%">Net Wt
                                                </td>
                                                <td align="center" width="50%">Gross Wt
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td align="center" width="50%">
                                                    <asp:Label ID="lblQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Quantity","{0:0.00}")%>'></asp:Label>
                                                </td>
                                                <td align="center" width="50%">
                                                    <asp:Label ID="lblGQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "GrossQuantity","{0:0.00}")%>'></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="RateforKG" HeaderText="Rate(KG)" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="TotalPrice" HeaderText="Total Amount" DataFormatString="{0:0.00}" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center" />
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
                    <td align="center">
                    Total No of Drums
                                    <tr>
                                        <td align="center">Total No of Drums
                                        </td>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="lblTotalDrum" runat="server" />
                                        </td>
                                        <td align="center">Total Amount
                                        </td>
                                        <td colspan="2" align="center">
                                            <asp:Label ID="lblTotalAmout" runat="server" />
                                        </td>
                                    </tr>
                <tr>
                    <td colspan="2" align="center">Amount in Words
                    </td>
                    <td colspan="4">
                        <asp:Label ID="lblAmount_word" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6"><asp:Label ID="lblmoney" runat="server" /></td>
                </tr>
                <tr>
                    <td>Price Terms
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblPriceTerms" runat="server" />
                    </td>
                    <td colspan="2">Payment Terms
                    </td>
                    <td>
                        <asp:Label ID="lblPayment" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Transport
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblTransport" runat="server" />
                    </td>
                    <td colspan="2">Freight Terms
                    </td>
                    <td>
                        <asp:Label ID="lblFreight" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Origin Country
                    </td>
                    <td colspan="2">INDIA</td>
                    <td colspan="2">Destination Country
                    </td>
                    <td>
                        <asp:Label ID="lblDCountry" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>Loading Port
                    </td>
                    <td colspan="2">
                        <asp:Label ID="lblLport" runat="server" />
                    </td>
                    <td colspan="2">Destination Port
                    </td>
                    <td>
                        <asp:Label ID="lblDPort" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="6"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#ffcc66" style="font-size: larger;">Bank Info
                    </td>
                    <td colspan="4" rowspan="6">Declaration: We declare that this Invoice shows the actual price of the
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
                    <td colspan="2">A/C. No:
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblBankAccount" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">ADC Code:
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
            <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="fb8" OnClick="btnConfirm_Click" />
        </div>
    </div>
</asp:Content>

