<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="Supplier.aspx.cs" Inherits="Buyer_Buyer" %>

<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <%-- <script type="text/javascript">
function SelectRadioButton(regexPattern, selectedRadioButton)
    {
        regex = new RegExp(regexPattern);
        for (i = 0; i < document.forms[0].elements.length; i++)
        {
            element = document.forms[0].elements[i];
            if (element.type == 'radio' && regex.test(element.name))
            {
                element.checked = false;
            }
        }
        selectedRadioButton.checked = true;
    }
</script>--%>
    <div id="content_area_Home" style="height: auto">
        <asp:MultiView ID="MainSupplierView" runat="server" ActiveViewIndex="0">
            <asp:View ID="CompanyInfo" runat="server">
                <h3 id="header_Text">Supplier Company Information</h3>
                <div>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnAddSupplier" runat="server" Text="Add Supplier" CssClass="fb8"
                                    OnClick="btnAddSupplier_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
                <div>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="gvSupplier" runat="server" AutoGenerateColumns="False" DataKeyNames="SupplierId"
                                    OnRowCommand="gvSupplier_RowCommand" CssClass="grid-view" EnableModelValidation="True">
                                    <Columns>
                                        <%--<asp:BoundField DataField="SupplierCompanyName" HeaderText="Supplier Name" ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:ButtonField ButtonType="Link" DataTextField="SupplierCompanyName" HeaderText="Supplier Name" CommandName="SupplierView"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                        <asp:BoundField DataField="CCity" HeaderText="Supplier City" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <%--<asp:BoundField DataField="CContactPerson" HeaderText="Contact Person" ItemStyle-HorizontalAlign="Center" />--%>
                                        <asp:BoundField DataField="CContactPhoneNo" HeaderText="Contact PhoneNo" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:BoundField DataField="email" HeaderText="E-mail" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:BoundField>
                                        <asp:ButtonField ButtonType="Link" Text="EDIT" HeaderText="Enter Form" CommandName="Supplier"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
                                    </Columns>
                                    <HeaderStyle CssClass="gvheader" />
                                    <AlternatingRowStyle CssClass="gvalternate" />
                                    <RowStyle CssClass="gvnormal" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divSupplierForm" runat="server">
                    <table>
                        <tr>
                            <td colspan="4">&nbsp;<asp:Label ID="lblSupplierID" runat="server" Text="" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center" valign="middle">&nbsp;<asp:CheckBoxList ID="chkICSList" CellPadding="4" CellSpacing="4" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"></asp:CheckBoxList>
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="11" style="width: 20%;"></td>
                            <td align="right">&nbsp;Supplier Name
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyname" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rfvComapnyname"
                                    runat="server" ControlToValidate="txtCompanyname" ValidationGroup="ComapnyName"
                                    ErrorMessage="Enter the Supplier Name" Display="static" Text="*"></asp:RequiredFieldValidator>
                            </td>
                            <td rowspan="11" style="width: 20%;"></td>
                        </tr>
                        <tr>
                            <td align="right">TIN
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTIN" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">VAT
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtVAT" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">CST
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCST" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 1
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress1" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 2
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress2" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 3
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress3" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">City
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Province/State
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">ZIP Code
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtZipCode" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Country
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCountry" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                            </td>
                        </tr>
                        <tr align="center">
                            <td colspan="4">&nbsp;
                            <asp:ValidationSummary ID="vsCompanyName" runat="server" DisplayMode="SingleParagraph"
                                HeaderText="Error Messsage* : " ShowSummary="true" ValidationGroup="ComapnyName" />
                            </td>
                        </tr>
                    </table>
                    <table width="885">
                        <tr>
                            <td></td>
                            <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCompanyInfoSubmit" runat="server" Text="Submit" CssClass="fb8"
                                OnClick="btnCompanyInfoSubmit_Click" Visible="False" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnCompanyInfoNext" CssClass="fb8" runat="server" Text="Next" ValidationGroup="ComapnyName" OnClick="btnCompanyInfoNext_Click" />
                            </td>
                        </tr>
                    </table>
                </div>

            </asp:View>
            <asp:View ID="ContactInfo" runat="server">
                <h3 id="header_Text">Supplier Contact Information</h3>
                <table>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="8" style="width: 15%;"></td>
                        <td align="right">Contact Person
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContatperson" runat="server" Height="30px"
                            Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"> </asp:TextBox>
                        </td>
                        <td rowspan="8" style="width: 15%;"></td>
                    </tr>
                    <tr>
                        <td align="right">Contact Phone #
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Height="30px"
                            Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Mobile # for Texting purpose
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMobile" runat="server" Height="30px" Style="margin-bottom: 1px"
                            Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">e-mail
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" Height="30px" Style="margin-bottom: 1px"
                            Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">website
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWebsite" runat="server" Height="30px" Style="margin-bottom: 1px"
                            Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnContactBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnContactBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnContactSubmit" runat="server" Text="Submit" CssClass="fb8" OnClick="btnContactSubmit_Click"
                                Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="Button2" CssClass="fb8" runat="server" Text="Next" OnClick="btnContactNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="BankInfo" runat="server">
                <h3 id="header_Text">Supplier Bank Information</h3>
                <table align="center">
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="8" style="width: 20%;"></td>
                        <td align="right">Name of the Bank
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBankname" runat="server" Font-Size="Medium"
                            Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                        <td rowspan="8" style="width: 20%;"></td>
                    </tr>
                    <tr>
                        <td align="right">Address Line 1
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress1" runat="server" Font-Size="Medium"
                            Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Address Line 2
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress2" runat="server" Font-Size="Medium"
                            Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style2">Address Line 3
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress3" runat="server" Font-Size="Medium"
                            Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style2">City
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCity" runat="server" Font-Size="Medium" Height="30px"
                            Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Province/State
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBState" runat="server" Font-Size="Medium" Height="30px"
                            Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right" class="style2">ZIP Code
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBZipcode" runat="server" Font-Size="Medium"
                            Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Country
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCountry" runat="server" Font-Size="Medium"
                            Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                        </td>
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnBankBack" runat="server" CssClass="fb8" OnClick="btnBankBack_Click"
                                Text="Back" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnBankSubmit" runat="server" CssClass="fb8" OnClick="btnBankSubmit_Click"
                                Text="Submit" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnBankNext" runat="server" CssClass="fb8" OnClick="btnBankNext_Click"
                                Text="Next" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="PriceTerms" runat="server">
                <h3 id="header_Text">Price Terms</h3>
                <table align="center">
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="18" style="width: 20%;"></td>
                        <td align="right">Ex-works
                        </td>
                        <td align="center">&nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnExworks" runat="server" GroupName="priceterms"
                            Font-Size="Larger" OnCheckedChanged="rbtnFOB_CheckedChanged" />
                        </td>
                        <td rowspan="18" style="width: 20%;"></td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;
                        </td>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                    <%--<tr>
                        <td align="right">
                            CNF by Sea
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCNFbySea" runat="server" 
                                 GroupName="priceterms" Font-Size="Larger" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            CNF by AIR ( Europe &amp; East USA)
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCNFAirEandEUSA" runat="server" GroupName="priceterms" 
                                 Font-Size="Larger" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            CNF by AIR (West USA)&nbsp;
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCNFAirWUSA" runat="server" 
                                 GroupName="priceterms" Font-Size="Larger" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>--%>
                    <tr>
                        <td align="right">Ex-Suppliers Place
                        </td>
                        <td align="center">&nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnExSupplierPlace" runat="server" GroupName="priceterms"
                            Font-Size="Larger" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;
                        </td>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="right">FOR Destination
                        </td>
                        <td align="center">&nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnForDestination" runat="server" GroupName="priceterms"
                            Font-Size="Larger" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;
                        </td>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                    <%-- <tr>
                        <td align="right">
                            FOR Destination
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnForDestination" runat="server" GroupName="priceterms" Font-Size="Larger" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>--%>
                    <%--<tr>
                        <td align="right">
                            CIF by AIR (West USA)
                        </td>
                        <td align="center">
                           &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCIFAirWUSA" runat="server" GroupName="priceterms" 
                                 Font-Size="X-Large" /></td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            FOR Destination
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnForDestination" runat="server" GroupName="priceterms" Font-Size="Larger" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td align="center">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td align="right">
                            Ex-Works
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnExworks" runat="server" GroupName="priceterms" Font-Size="Larger" />
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnPriceTermsBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnPriceTermsBack_Click" />
                        </td>
                        <td align="center">&nbsp;&nbsp;<asp:Button ID="btnPriceTermsSubmit" runat="server" Text="Submit" CssClass="fb8"
                            OnClick="btnPriceTermsSubmit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnPriceTermsNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnPriceTermsNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="PaymentTerms" runat="server">
                <h3 id="header_Text">Payment Terms</h3>
                <table align="center">
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="12" style="width: 20%;"></td>
                        <td align="right">Payment Terms
                        </td>
                        <td align="center">&nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtPaymentTerms" runat="server" Height="30px"
                            Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                        <td rowspan="12" style="width: 20%;"></td>
                    </tr>
                    <tr>
                        <td align="right">&nbsp;
                        </td>
                        <td align="center">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnPaymentTermsBack" GroupName="paymentterms" runat="server" Text="Back"
                                CssClass="fb8" OnClick="btnPaymentTermsBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnPaymentTermsSumit" runat="server" Text="Submit" CssClass="fb8"
                                OnClick="btnPaymentTermsSumit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnPaymentTermsNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnPaymentTermsNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="Conformation" runat="server">
                <h3 id="header_Text">Conformation</h3>
                <table width="100%" style="height: 330px; background-color: #F7F7DE">
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">Thank You<br>
                            We will contact you on your e-mail within next 24 hours with username and Password
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br />
                            <br />
                            <br />
                            <br />
                            <asp:Button ID="btnHome" runat="server" CssClass="fb8" PostBackUrl="~/Supplier/Supplier.aspx"
                                Text="Home" />
                            <br />
                            <br />
                            <br />
                            <br />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

