<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Invoice.aspx.cs" Inherits="Invoice" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table border="1" width="700">
            <tr>
                <td colspan="6" align="center">
                Invoice</td>
            </tr>
            <tr>
                <td>
                    Exporter
                </td>
                <td colspan="5">
                    Exporter Info
                </td>
            </tr>
            <tr>
                <td rowspan="5" style="width: 94px; height: auto">
                    <asp:Label ID="lblExporterAddress" runat="server"></asp:Label>
                </td>
                <td>
                    Contact
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td style="width: 94px; height: auto">
                    Inv No
                </td>
                <td>
                    A
                </td>
            </tr>
            <tr>
                <td>
                    Phone/Fax
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td style="width: 94px; height: auto">
                    Date
                </td>
                <td>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Mobile
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td style="width: 94px; height: auto">
                    IE Code
                </td>
                <td>
                    0902008340
                </td>
            </tr>
            <tr>
                <td class="style2">
                    e-mail
                </td>
                <td colspan="2" class="style2">
                </td>
                <td style="width: 94px; height: auto">
                    FDA
                </td>
                <td class="style2">
                    16102479324
                </td>
            </tr>
            <tr>
                <td>
                    web
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td style="width: 94px; height: auto">
                    AP VAT
                </td>
                <td>
                    28770277330
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    BUYER
                </td>
                <td colspan="2">
                    NOTIFY
                </td>
                <td colspan="2">
                    CONSIGNEE
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="lblBuyerAddress" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblNotifyAddress" runat="server"></asp:Label>
                </td>
                <td colspan="2">
                    <asp:Label ID="lblConsigneeAddress" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td>
                    PO no
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    Date
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">
                    <asp:GridView ID="gvInvoiceOrder" runat="server" AutoGenerateColumns="false" Width="100%">
                        <Columns>
                            <asp:BoundField DataField="batchid" HeaderText="Batch No" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="Quantity" HeaderText="Net Wt Qty(KG))" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:n0}" />
                            <asp:BoundField DataField="GrossQuantity" HeaderText="Gross Wt Qty(KG)" DataFormatString="{0:n0}"
                                ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="TotalPrice" HeaderText="Total Amount" DataFormatString="{0:n0}"
                                ItemStyle-HorizontalAlign="Center" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="3">Total No of Drums:
                <asp:Label ID="lblTotalDrums" runat="server" />
                </td>
                <td colspan="3">TotalAmount:
                <asp:Label ID="lblTotalAmount" runat="server" />
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
                    &nbsp;
                </td>
                <td>
                    Payment Terms
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Transport
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    Freight Terms
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Origin Country
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    Destination Country
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td>
                    Loading Port
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
                <td>
                    Destination Port
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="6">
                </td>
            </tr>
            <tr>
                <td colspan="2" rowspan="6">
                    BANK INFO</br>
                    <asp:Label ID="lblBankInfo" runat="server" Text="Label"></asp:Label>
                </td>
                <td colspan="2" rowspan="6">
                    Declaration: We declare that this Invoice shows the actual price of the goods described
                    and that all particulars are true and correct.
                </td>
                <td colspan="2" rowspan="6">
                    Authorized Signature</br> Mudar India Exports
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
