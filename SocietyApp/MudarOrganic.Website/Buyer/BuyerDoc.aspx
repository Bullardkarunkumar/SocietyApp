<%@ Page Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="BuyerDoc.aspx.cs" Inherits="BuyerDoc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text" style="color: White; background-color: #6B696B; font-weight: bold; text-align: center">
            Buyer Register Information &nbsp;<asp:Label ID="lblBuyerID" runat="server" Visible="false"></asp:Label>
        </div>
        <div style="margin-top: 10px">
            <table align="center" width="100%" border="1">
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Company Information
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Name of the Company
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCompanyname" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" rowspan="7" bgcolor="#CCCC99">Buyer Address
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAddress1" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAddress2" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAddress3" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCity" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblState" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblZipCode" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCountry" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <div id="divIndBuyer" runat="server">
                    <tr>
                        <td align="center" bgcolor="#CCCC99">TIN
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTIN" runat="server" Style="margin-bottom: 1px"
                            Width="340px" Font-Size="Medium"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#CCCC99">VAT
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblVAT" runat="server" Style="margin-bottom: 1px"
                            Width="340px" Font-Size="Medium"></asp:Label></td>
                    </tr>
                    <tr>
                        <td align="center" bgcolor="#CCCC99">CST
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCST" runat="server" Width="340px"
                            Font-Size="Medium"></asp:Label></td>
                    </tr>
                </div>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Contact Person
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblContatperson" runat="server" Width="340px"
                        Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Phone
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblContactPhone" runat="server" Width="340px"
                        Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Phone for text message
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblMobile" runat="server" Width="340px"
                        Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">E-mail
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblEmail" runat="server" Width="340px"
                        Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Website
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblWebsite" runat="server" Width="340px"
                        Font-Size="Medium"></asp:Label><%--<asp:Label ID="lblProducts" runat="server"  Width="340px" Font-Size="Medium"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Products Information
                                      <%--<asp:Label ID="lblProducts" runat="server"  Width="340px" Font-Size="Medium"></asp:Label>--%>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:GridView ID="gvProductList" AutoGenerateColumns="False" runat="server"
                            DataKeyNames="ProductId" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundField DataField="ProductID" HeaderText="Product ID" Visible="false" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                            </Columns>
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Notify Information
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Notify Name
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNotifyName" runat="server" Width="340px"
                        Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" rowspan="7" bgcolor="#CCCC99">Notify Address
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNAddress1" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNAddress2" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNAddress3" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNCity" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNState" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNZipCode" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblNCountry" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Bank Information
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Name of the Bank
                    </td>
                    <td>&nbsp;&nbsp;&nbsp<asp:Label ID="lblBankname" runat="server" Width="340px"
                        Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" rowspan="7" bgcolor="#CCCC99">Bank Address
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBAddress1" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBAddress2" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBAddress3" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBCity" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBState" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBZipcode" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblBCountry" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Port Information
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Transport Mode
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTransportmode" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Nearest Air Port
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblAir" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Nearest Sea Port
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblSea" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Nearest Road Port
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRoad" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Nearest Rail Port
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblRail" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Price Information
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Price Terms
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPriceTerms" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Payment Information
                    </td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">Payment Terms
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblPaymentTerms" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td align="center" bgcolor="#CCCC99">If credit terms - Credit Days
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCreditDays" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr>
                    <td colspan="2" align="center" bgcolor="#cccccc" style="font-size: larger;">Log in Details
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td>UserName
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblusername" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>

                <tr align="center" bgcolor="#CCCC99">
                    <td>Password
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblpassword" runat="server" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium"></asp:Label></td>
                </tr>
                <tr style="margin-top: 15px">
                    <td align="center" colspan="2"></td>
                </tr>
            </table>
            <div style="margin-top: 15px; text-align:center">
                <asp:Button ID="btnSubmit" CssClass="btn btn-default" runat="server" Text="Back"
                    OnClick="btnSubmit_Click" />
            </div>
        </div>
    </div>
</asp:Content>

