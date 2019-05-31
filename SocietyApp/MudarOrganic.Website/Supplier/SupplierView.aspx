<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="SupplierView.aspx.cs" Inherits="Supplier_SupplierView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
    <div ID="content_area_Home" style="height: auto">
        <h3 id="header_Text">
            Supplier Information</h3>
        <table>
            <tr>
                <td colspan="4">
                    &nbsp;<asp:Label ID="lblSupplierID" runat="server" Text="" Visible="false" />
                </td>
            </tr>
            <tr>
                <td rowspan="11" style="width: 20%;">
                </td>
                <td align="right">
                    Name of the Company
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyname" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rfvComapnyname"
                            runat="server" ControlToValidate="txtCompanyname" ValidationGroup="ComapnyName"
                            ErrorMessage="Enter the ComapnyName" Display="static" Text="*"></asp:RequiredFieldValidator>
                </td>
                <td rowspan="11" style="width: 20%;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    TIN
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTIN" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    VAT
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtVAT" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    CST
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCST" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Address Line 1
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress1" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Address Line 2
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress2" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Address Line 3
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress3" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    City
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Province/State
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    ZIP Code
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtZipCode" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Country
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCountry" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr align="center">
                <td colspan="4">
                    &nbsp;
                    <asp:ValidationSummary ID="vsCompanyName" runat="server" DisplayMode="SingleParagraph"
                        HeaderText="Error Messsage* : " ShowSummary="true" ValidationGroup="ComapnyName" />
                </td>
            </tr>
        </table>
        <h3 id="header_Text">
            Supplier Contact Information</h3>
        <table>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="8" style="width: 15%;">
                </td>
                <td align="right">
                    Contact Person
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContatperson" runat="server" Height="30px"
                        Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"> </asp:TextBox>
                </td>
                <td rowspan="8" style="width: 15%;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    Contact Phone #
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Height="30px"
                        Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Mobile # for Texting purpose
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMobile" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    e-mail
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    website
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWebsite" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
        </table>
        <h3 id="header_Text">
            Supplier Bank Information</h3>
        <table align="center">
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="8" style="width: 20%;">
                </td>
                <td align="right">
                    Name of the Bank
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBankname" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
                <td rowspan="8" style="width: 20%;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    Address Line 1
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress1" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Address Line 2
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress2" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style2">
                    Address Line 3
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress3" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style2">
                    City
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCity" runat="server" Font-Size="Medium" Height="30px"
                        Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Province/State
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBState" runat="server" Font-Size="Medium" Height="30px"
                        Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="style2">
                    ZIP Code
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBZipcode" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Country
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCountry" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                </td>
                <tr>
                    <td colspan="4">
                        &nbsp;
                    </td>
                </tr>
            </tr>
        </table>
        <h3 id="header_Text">
            Price Terms</h3>
        <table align="center">
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="18" style="width: 20%;">
                </td>
                <td align="right">
                    Ex-works
                </td>
                <td align="center">
                    &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnExworks" runat="server" GroupName="priceterms"
                        Font-Size="Larger" />
                </td>
                <td rowspan="18" style="width: 20%;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Ex-Suppliers Place
                </td>
                <td align="center">
                    &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnExSupplierPlace" runat="server" GroupName="priceterms"
                        Font-Size="Larger" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    FOR Destination
                </td>
                <td align="center">
                    &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnForDestination" runat="server" GroupName="priceterms"
                        Font-Size="Larger" />
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td align="left">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
        </table>
        <h3 id="header_Text">
            Payment Terms</h3>
        <table align="center">
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="12" style="width: 20%;">
                </td>
                <td align="right">
                    Payment Terms
                </td>
                <td align="center">
                    &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtPaymentTerms" runat="server" Height="30px"
                        Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox>
                </td>
                <td rowspan="12" style="width: 20%;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp;
                </td>
                <td align="center">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnHome" runat="server" CssClass="fb8" PostBackUrl="~/Supplier/Supplier.aspx"
                                Text="Back" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>

