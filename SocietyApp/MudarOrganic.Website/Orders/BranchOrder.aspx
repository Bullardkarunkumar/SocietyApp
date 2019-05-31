<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="BranchOrder.aspx.cs" Inherits="Orders_BranchOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" style="height: auto">
                <div id="divBranch" runat="server">
                    <table align="center">
                        <tr>
                            <td colspan="2" bgcolor="#CCCC99" align="center">Order For Branch or Supplier
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2" bgcolor="#CCCC99" align="center">Select the Branch Or Supplier Details
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:RadioButtonList ID="rblBranchSupplier" runat="server" RepeatDirection="Horizontal"
                                    AutoPostBack="true" OnSelectedIndexChanged="rblBranchSupplier_SelectedIndexChanged"
                                    ForeColor="Red">
                                    <asp:ListItem Text="ICS Supplier" Value="1" Selected="True" />
                                    <asp:ListItem Text="Supplier" Value="2" />
                                </asp:RadioButtonList>
                            </td>
                            <td colspan="2"></td>
                            <td colspan="2"></td>
                            <td colspan="2"></td>
                            <td align="center" colspan="2">
                                <asp:DropDownList ID="ddlSelectBranch" Width="180px" runat="server" AutoPostBack="true"
                                    OnSelectedIndexChanged="ddlSelectBranch_SelectedIndexChanged" Height="30px">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" bgcolor="#CCCC99" align="center">Select&nbsp; the&nbsp; Mandy Tax
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2" bgcolor="#CCCC99" align="center">Select the Shipping Agent
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:DropDownList ID="ddlMandyTax" Width="180px" runat="server" AutoPostBack="true"
                                    Height="30px" OnSelectedIndexChanged="ddlMandyTax_SelectedIndexChanged">
                                    <asp:ListItem Value="0">Select---</asp:ListItem>
                                    <asp:ListItem Value="1">as applicable</asp:ListItem>
                                    <asp:ListItem Value="2">annually paid by admin</asp:ListItem>
                                </asp:DropDownList>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2">&nbsp;
                            </td>
                            <td colspan="2" align="center">
                                <asp:DropDownList ID="ddlCustomAgent" Width="180px" runat="server" AutoPostBack="true"
                                    Height="30px" OnSelectedIndexChanged="ddlCustomAgent_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divPurchseOrder" runat="server" visible="false">
                    <table align="center" style="font-family: Verdana; width: 885px">
                        <tr>
                            <td colspan="6">
                                <table align="center" style="font-family: Verdana; width: 885px">
                                    <tr>
                                        <td colspan="6" align="center" style="font-family: Verdana; height: 18px;">Purchase Order  &nbsp;<asp:Label ID="lblOrgType" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <table width="100%" border="1">
                                    <tr>
                                        <td width="50%" align="center" rowspan="4" colspan="2">
                                            <asp:Label ID="txtCompanyAddress" runat="server" Text="" />
                                        </td>
                                        <td width="50%" align="center" bgcolor="#CCCC99" colspan="2">Purchase Order INFO
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td bgcolor="#CCCC99">Branch PO
                                        </td>
                                        <td>
                                            <asp:Label ID="lblBPO" runat="server" Text="00000"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td bgcolor="#CCCC99">Date
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDate" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td bgcolor="#CCCC99">TIN
                                        </td>
                                        <td>
                                            <asp:Label ID="lblTIN" runat="server" Text="00000"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <tr>
                                <td colspan="6" align="center">Details of Overseas PO received
                                </td>
                            </tr>
                        <tr>
                            <td>
                                <table width="100%" border="1">
                                    <tr align="center">
                                        <td bgcolor="#CCCC99">Overseas PO/<br />
                                            Intent Number
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblPO" runat="server" Text="00000"></asp:Label>
                                        </td>
                                        <td align="center" bgcolor="#CCCC99">Date
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblPODate" runat="server" Text=""></asp:Label>
                                        </td>
                                        <td style="width: 90px" bgcolor="#CCCC99">Destination Country
                                        </td>
                                        <td>
                                            <asp:Label ID="lblDestCountry" runat="server" Text=""></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">Please arrange the shipment as per the Terms & Conditions mentioned
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="gvPurchaseOrder" runat="server" AutoGenerateColumns="False" BackColor="White"
                                    BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                    GridLines="Vertical" Width="880px">
                                    <RowStyle BackColor="#F7F7DE" />
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsnnumber" runat="server" Text=" <%#Container.DisplayIndex+1 %>" />
                                                <asp:Label ID="lblProductID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"ProductID") %>' Visible="false"></asp:Label>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <%--  <asp:TemplateField HeaderText="S.No">
                                        <ItemTemplate>
                                            <%#Container.DisplayIndex+1 %>
                                        </ItemTemplate>
                                    </asp:TemplateField>--%>
                                        <asp:BoundField DataField="ProductID" HeaderText="Product ID" ItemStyle-HorizontalAlign="Center" Visible="false" />
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Qty (KG)" HeaderStyle-Width="60px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Quantity") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packing (25KG)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPacking25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Packing25") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Packing (180KG)" HeaderStyle-Width="120px" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPacking180" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"Packing180") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Price / KG" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:Label ID="lblPOUPPrice" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"POUPPrice") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="TotalPrice" HeaderText="Price / KG" DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center"/>--%>
                                        <asp:TemplateField HeaderText="Total Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalPriceAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Total2","{0:0.00}")%>'
                                                    ItemStyle-HorizontalAlign="Center" />
                                            </ItemTemplate>
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
                        <tr>
                            <td colspan="6"></td>
                        </tr>
                        <tr>
                            <td>
                                <div id="divOrganicPremimu" runat="server" visible="false">
                                    <table width="100%" border="1">

                                        <tr>
                                            <td align="center">Organic Premium @ Rs. 10 / Kg will be transferred to the societies handling ICS
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table width="100%" border="1">
                                    <tr>
                                        <td align="center" width="35%" bgcolor="#CCCC99">Price Terms
                                        </td>
                                        <td align="center" width="35%">
                                            <asp:Label ID="lblpriceterm" runat="server" />
                                        </td>
                                        <td align="center" width="30%" bgcolor="#CCCC99">Address for Delivery
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td bgcolor="#CCCC99">Payment Terms</td>
                                        <td>
                                            <asp:Label ID="lblPayTer" runat="server" /></td>
                                        <td align="center" rowspan="6">
                                            <asp:Label ID="lblAddressDelivery" runat="server" Text="Label"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" bgcolor="#CCCC99">Additional Taxes & Duties
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblTax" runat="server" Text="Label" />
                                        </td>

                                    </tr>
                                    <tr>
                                        <td align="center" bgcolor="#CCCC99">Mandy Tax
                                        </td>
                                        <td align="center">
                                            <%-- + lblMandyTax.Text +--%>
                                            <asp:Label ID="lblMandyTax" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" bgcolor="#CCCC99">Mode of Transport
                                        </td>
                                        <td align="center">convenient mode</td>
                                    </tr>
                                    <tr>
                                        <td align="center" bgcolor="#CCCC99">Place of Delivery
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblplacedelivery" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" bgcolor="#CCCC99">Sales Tax Terms
                                        </td>
                                        <td align="center">
                                            <asp:Label ID="lblSalesTax" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center">E-mail the below mentioned documents IMMEDIATELY after dispatching the Shipment
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6">
                                <asp:GridView ID="gvReports" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mudargrid">
                                    <Columns>
                                        <asp:TemplateField HeaderText="S.No">
                                            <ItemTemplate>
                                                <asp:Label ID="lblsno" runat="server" Text=" <%#Container.DisplayIndex+1 %>" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="ReportID" HeaderText="ReportID" Visible="false" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="ReportName" HeaderText="Report Name" ItemStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Check">
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbReport" runat="server" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Date of Email">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtReportDate" runat="server" Text="" />
                                                <asp:CalendarExtender ID="ceReportDate" runat="server" TargetControlID="txtReportDate">
                                                </asp:CalendarExtender>
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:TemplateField>
                                    </Columns>
                                    <%--<FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />--%>
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divbutton" runat="server" align="center" visible="false">
                    <asp:Button ID="btnConfirm" CssClass="btn btn-success" runat="server" Text="Confirm" OnClick="btnConfirm_Click" />
                </div>
                <%-- ---old code start------%>
                <div id="divoldcode" runat="server" visible="false">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblOverSeasPOIntent" runat="server" Text="0"></asp:Label>
                                <asp:Label ID="lblMainAddress" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Label ID="lblDrum25" runat="server" />&nbsp;<asp:Label ID="lblDrum180" runat="server" />
                                &nbsp;<asp:Label ID="lblOrgPreimum" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 368px">Total # of Drums&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotalDrums"
                                runat="server" Text="0" />
                            </td>
                            <td style="width: 502px">Total Amount in Rs&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotalprice" runat="server"
                                Text="0" />
                            </td>
                        </tr>
                        <tr>
                            <td>Total Amt in Words
                            </td>
                            <td>
                                <asp:Label ID="lblAmount_word" runat="server" />
                            </td>
                        </tr>
                    </table>
                    <table style="width: 876px">
                        <tr>
                            <td colspan="2"></td>
                            <td align="center" style="width: 138px">Address for Delivery
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 130px">Mode of Transport
                            </td>
                            <td style="width: 204px">
                                <%--<asp:Label ID="lblModeofTransport" runat="server"></asp:Label>--%>
                                <asp:DropDownList ID="ddlModeofTransport" runat="server" OnSelectedIndexChanged="ddlModeofTransport_SelectedIndexChanged"
                                    Height="16px" Width="75px">
                                    <asp:ListItem Text="Air" Value="1" Selected="True"></asp:ListItem>
                                    <asp:ListItem Text="Sea" Value="2"></asp:ListItem>
                                    <asp:ListItem Text="Rail" Value="3"></asp:ListItem>
                                    <asp:ListItem Text="Road" Value="4"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                            <td rowspan="2" style="width: 138px" align="center"></td>
                        </tr>
                        <tr>
                            <td style="width: 130px">Place of Delivery
                            </td>
                            <td style="width: 114px">
                                <%--<asp:Label ID="lblPlaceofDelivery" runat="server"></asp:Label>--%>
                                <asp:DropDownList ID="ddlPlaceOfDelivery" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlPlaceOfDelivery_SelectedIndexChanged">
                                </asp:DropDownList>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
