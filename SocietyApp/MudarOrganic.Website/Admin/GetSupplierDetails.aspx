<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="GetSupplierDetails.aspx.cs" Inherits="Admin_GetSupplierDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height:auto">
        <div id="header_Text">
            Supplier Details</div>
            <div>  
            <table>
                     <tr><td colspan="4"></td></tr>
                     <tr>
                        <td rowspan="9" style="width: 20%;">
                        </td>
                        <td align="right">
                            Price Terms
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPriceTerms" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        <td rowspan="9" style="width: 20%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                           Taxes & Duties
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTaxesandDuties" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                           Mandy Tax
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMandyTax" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Mode of Transport
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtModeofTransport" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                           Place of Delivery
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPlaceofDelivery" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                           Delivery Address
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDeliveryAddress" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Docs Required
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDocsRequired" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Apart from
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtApartfrom" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td align="right">
                            Regular Docs
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRegularDocs" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:TextBox ID="txtPlantationFDate" runat="server" Height="30px" Style="margin-bottom: 1px"
                            Width="180px" Font-Size="Medium"></asp:TextBox><asp:CalendarExtender ID="dtpLastDate"
                                runat="server" Format="MM/dd/yyyy" TargetControlID="txtPlantationFDate">
                            </asp:CalendarExtender>
                            
                            <asp:Button ID="btnSupplierPlaceorder0"
                                runat="server" Text="Submit" CssClass="fb8" 
                                OnClick="btnSupplierPlaceorder_Click" />
                                <asp:Label ID="lblID" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                </table>
             <table width="885">
                    <tr>
                        <td>
                        </td>
                        <td></td>
                        <td align="center">
                            
                            <asp:Button ID="btnSupplierPlaceorder"
                                runat="server" Text="Submit" CssClass="fb8" 
                                OnClick="btnSupplierPlaceorder_Click" />
                        </td>
                        <td></td>
                    </tr>
                </table>
            </div>
        </div>
</asp:Content>

