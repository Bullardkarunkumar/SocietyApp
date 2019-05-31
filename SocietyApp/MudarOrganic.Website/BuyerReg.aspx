<%@ Page Language="C#" MasterPageFile="~/RegBuyer.master" AutoEventWireup="true" CodeFile="BuyerReg.aspx.cs" Inherits="BuyerReg" Title="Mudarorganic-BuyerRegister" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function selectallproduct(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= this.gvProductList.ClientID %>');
            var TargetChildControl = "cbitem";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                    Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                    Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            //Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function fnShowMessage(msg) {
            bootbox.alert(msg, function () {
                window.location.href = "login.aspx";
            });
        }
    </script>

    <div id="content_area_Home" style="height: auto; display: none">
        <div>
            <h3 id="header_Text">Buyer Company Information</h3>
            <div id="divTBuyerDetails" runat="server">
                <table>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnTBuyerInfo" runat="server" Text="Company Details"
                                CssClass="btnFarmer" OnClick="btnTBuyerInfo_Click" />
                        </td>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnTContactInfo" runat="server" Text="Contact Details"
                                CssClass="btnFarmer" OnClick="btnTContactInfo_Click" />
                        </td>
                        <td colspan="2" align="left">
                            <asp:Button ID="btnTProductInfo" runat="server" Text="Product Details"
                                CssClass="btnFarmer" OnClick="btnTProductInfo_Click" />
                        </td>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnTNotifyInfo" runat="server" Text="Notify Details"
                                CssClass="btnFarmer" OnClick="btnTNotifyInfo_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnTBankInfo" runat="server" Text="Bank Details"
                                CssClass="btnFarmer" OnClick="btnTBankInfo_Click" />
                        </td>
                        <td colspan="2" align="left">
                            <asp:Button ID="btnTPortInfo" runat="server" Text="Port Details"
                                CssClass="btnFarmer" OnClick="btnTPortInfo_Click" />
                        </td>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnTPriceTerms" runat="server" Text="Price Terms"
                                CssClass="btnFarmer" OnClick="btnTPriceTerms_Click" />
                        </td>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnTPaymentTerms" runat="server" Text="Payment Terms"
                                CssClass="btnFarmer" OnClick="btnTPaymentTerms_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
        <div>
            <asp:MultiView ID="MainBuyerView" runat="server" ActiveViewIndex="0">
                <asp:View ID="CompanyInfo" runat="server">
                    <%--    <h3 id="header_Text">
                    Company Information</h3>--%>
                    <table>
                        <tr>
                            <td colspan="4">
                                <asp:Label ID="lblBuyerID" runat="server" Visible="false" Text="" />&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="11" style="width: 23%;"></td>
                            <td align="right">Company Name 
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtCompanyname1" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                    Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rfvComapnyname1" runat="server" ControlToValidate="txtCompanyname"
                                        ValidationGroup="ComapnyName" ErrorMessage="Enter the ComapnyName" Display="static"
                                        Text="*"></asp:RequiredFieldValidator></td>
                            <td rowspan="11" style="width: 20%;"></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 1
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress11" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rqftxtAddress1" runat="server" ControlToValidate="txtAddress1"
                                    ValidationGroup="ComapnyName" ErrorMessage="Enter the ComapnyName" Display="static"
                                    Text="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 2
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress21" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 3
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress31" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">City
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity1" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCity1" runat="server" ControlToValidate="txtCity"
                                    ValidationGroup="ComapnyName" ErrorMessage="Enter the City" Display="static"
                                    Text="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">Province/State
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState1" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtState"
                                    ValidationGroup="ComapnyName" ErrorMessage="Enter the State" Display="static"
                                    Text="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">ZIP Code
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtZipCode1" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtZipCode"
                                    ValidationGroup="ComapnyName" ErrorMessage="Enter the ZipCode" Display="static"
                                    Text="*"></asp:RequiredFieldValidator></td>
                        </tr>
                        <tr>
                            <td align="right">Country
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCountry1" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvTxtcountry1" runat="server" ControlToValidate="txtCountry"
                                    ValidationGroup="ComapnyName" ErrorMessage="Enter the Country" Display="static"
                                    Text="*"></asp:RequiredFieldValidator>
                            </td>
                        </tr>
                        <div id="divIndBuyer" runat="server" visible="false">
                            <tr>
                                <td align="right">TIN
                                </td>
                                <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTIN" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                    Width="340px" Font-Size="Medium"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">VAT
                                </td>
                                <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtVAT" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                    Width="340px" Font-Size="Medium"></asp:TextBox></td>
                            </tr>
                            <tr>
                                <td align="right">CST
                                </td>
                                <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCST" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                    Width="340px" Font-Size="Medium"></asp:TextBox></td>
                            </tr>
                        </div>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:ValidationSummary runat="server" ID="vsCompanyName1" ValidationGroup="ComapnyName"
                                    ShowSummary="true" DisplayMode="SingleParagraph" HeaderText="Error Messsage* : " Visible="false" />
                            </td>
                        </tr>
                    </table>
                    <table width="885">
                        <tr>
                            <td></td>
                            <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCompanyInfoSubmit1" runat="server" Text="Submit" CssClass="fb8"
                                OnClick="btnCompanyInfoSubmit_Click" Visible="False" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnCompanyInfoNext1" CssClass="fb8" runat="server" Text="Next" ValidationGroup="ComapnyName"
                                    OnClick="btnCompanyInfoNext_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="ContactInfo" runat="server">
                    <%-- <h3 id="header_Text">
                    Contact Information</h3>--%>
                </asp:View>
                <asp:View ID="NotifyInfo" runat="server">
                    <%-- <h3 id="header_Text">
                    Notify Information</h3>--%>
                    <table>
                        <tr>
                            <td colspan="4" align="center" style="color: #FF0000">&nbsp;* Notify same as buyer
                            <asp:CheckBox ID="cdNotify" runat="server" AutoPostBack="true" OnCheckedChanged="cdNotify_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="8" style="width: 20%;"></td>
                            <td align="right">Notify<asp:Label ID="lblCustomBroker" runat="server" Visible="false" Text="(Custom Broker)" />
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNotifyName" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                            <td rowspan="8" style="width: 20%;"></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 1
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNAddress1" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 2
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNAddress2" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 3
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNAddress3" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">City
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNCity" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Province/State
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNState" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">ZIP Code
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNZipCode" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Country
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNCountry" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="885">
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnNotifyBack" runat="server" Text="Back" CssClass="fb8"
                                    OnClick="btnNotifyBack_Click" Visible="False" />
                            </td>
                            <td align="center">
                                <asp:Button ID="btnNotifySubmit" runat="server" Text="Submit" CssClass="fb8" OnClick="btnNotifySubmit_Click"
                                    Visible="False" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnNotifyNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnNotifyNext_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="BankInfo" runat="server">
                    <%--  <h3 id="header_Text">
                    Buyer Bank Information</h3>--%>
                    <table>
                        <tr>
                            <td colspan="4" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If Documnet routing is through Bank
                                <asp:RadioButton ID="rbBank" runat="server"
                                    Font-Size="Larger" OnCheckedChanged="rbBank_CheckedChanged" GroupName="check" AutoPostBack="true" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Consignee
                            <asp:RadioButton ID="rbConsignee" runat="server" Font-Size="Larger" OnCheckedChanged="rbConsignee_CheckedChanged" GroupName="check" AutoPostBack="true" />
                            </td>
                        </tr>
                        <tr id="trbank" runat="server" visible="false">
                            <td colspan="4" align="center" style="color: #FF0000">
                                <asp:CheckBox ID="cbBank" runat="server" Visible="false" />
                            </td>
                        </tr>
                        <tr id="trConsignee" runat="server" visible="false">
                            <td colspan="4" align="center" style="color: #FF0000">Consignee same as Buyer.
                            <asp:CheckBox ID="cbconsignee" runat="server" AutoPostBack="true" OnCheckedChanged="cbconsignee_CheckedChanged" />
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="8" style="width: 20%;"></td>
                            <td align="right">
                                <asp:Label ID="lblTex" runat="server" />&nbsp;Name
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBankname" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                            <td rowspan="8" style="width: 20%;"></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 1
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress1" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Address Line 2
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress2" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" class="style2">Address Line 3
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress3" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" class="style2">City
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCity" runat="server" Font-Size="Medium" Height="30px"
                                Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Province/State
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBState" runat="server" Font-Size="Medium" Height="30px"
                                Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right" class="style2">ZIP Code
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBZipcode" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Country
                            </td>
                            <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCountry" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
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
                                    Text="Back" Visible="False" />
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
                <asp:View ID="PortInfo" runat="server">
                    <%-- <h3 id="header_Text">
                    Port Information</h3>--%>
                </asp:View>
                <asp:View ID="PriceTerms" runat="server">
                    <%--<h3 id="header_Text">
                    Price Terms</h3>--%>
                    <table align="center">
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="18" style="width: 25%;"></td>
                            <td align="right">FOB India
                            </td>
                            <td align="center">&nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnFOB1" runat="server" GroupName="priceterms"
                                Font-Size="Larger" OnCheckedChanged="rbtnFOB_CheckedChanged" />
                            </td>
                            <td rowspan="18" style="width: 30%;"></td>
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
                            <td align="right">CIF&nbsp; by Sea
                            </td>
                            <td align="center">&nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCIFbySea1" runat="server" GroupName="priceterms"
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
                            <td align="right">CIF By AIR&nbsp;&nbsp;
                            </td>
                            <td align="center">&nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCIFAirEandEUSA1" runat="server" GroupName="priceterms"
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
                            <td align="center">&nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnForDestination1" runat="server" GroupName="priceterms"
                                Font-Size="Larger" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp;
                            </td>
                            <td align="left">&nbsp;
                            </td>
                        </tr>
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
                                <asp:Button ID="btnPriceTermsBack" runat="server" Text="Back" CssClass="fb8"
                                    OnClick="btnPriceTermsBack_Click" Visible="False" />
                            </td>
                            <td align="center">&nbsp;&nbsp;<asp:Button ID="btnPriceTermsSubmit" runat="server" Text="Submit" CssClass="fb8"
                                OnClick="btnPriceTermsSubmit_Click" Visible="False" />
                            </td>
                            <td align="right">
                                <asp:Button ID="btnPriceTermsNext1" CssClass="fb8" runat="server" Text="Next" OnClick="btnPriceTermsNext_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="PaymentTerms" runat="server">
                    <%-- <h3 id="header_Text">
                    Payment Terms</h3>--%>
                </asp:View>
                <asp:View ID="Conformation" runat="server">
                    <%--<h3 id="header_Text">
                    Conformation</h3>--%>
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
                            <td align="center">&nbsp; Thank You for Registered.<br>We will contact you on your E-mail within 24 hours with username and Password.<br></br>
                            </br>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
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
                                <asp:Button ID="Button1" CssClass="fb8" runat="server" Text="Home" OnClick="btnPaymentTermsNext_Click"
                                    PostBackUrl="~/Login.aspx" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="ProductInfo" runat="server">
                    <%--  <h3 id="header_Text">
                    Products Registration</h3>--%>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="portlet light " id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        <i class=" icon-layers font-red"></i>
                        <span class="caption-subject font-red bold uppercase">Buyer Registration -
                    <span class="step-title">Step <span id="spanCount" runat="server">1</span> of 6 </span>
                        </span>
                    </div>
                </div>
                <div class="portlet-body form ">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <ul class="nav nav-pills nav-justified steps">
                                    <li class="active" runat="server" id="licompanydetails">
                                        <a href="#" class="step" aria-expanded="true">
                                            <span class="number">1 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Company Details </span>
                                        </a>
                                    </li>
                                    <li runat="server" id="licontactdetails">
                                        <a href="#" class="step">
                                            <span class="number">2 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Contact Details </span>
                                        </a>
                                    </li>
                                    <li runat="server" id="liproductdetails">
                                        <a href="#" class="step">
                                            <span class="number">3 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Product Details </span>
                                        </a>
                                    </li>
                                    <li runat="server" id="liportdetails">
                                        <a href="#" class="step">
                                            <span class="number">4 </span>
                                            <br />
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Port Details</span>
                                        </a>
                                    </li>
                                    <li runat="server" id="liFrightTerms">
                                        <a href="#" class="step">
                                            <span class="number">5 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Fright Details</span>
                                        </a>
                                    </li>
                                    <li runat="server" id="lipaymentterms">
                                        <a href="#" class="step">
                                            <span class="number">6 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Payment Terms</span>
                                        </a>
                                    </li>
                                </ul>
                                <div id="bar" class="progress progress-striped" role="progressbar">
                                    <div id="divProgress" runat="server" class="progress-bar progress-bar-success" style="width: 16.67%;"></div>
                                </div>
                                <div class="tab-content">

                                    <div class="tab-pane active" id="tabCompanyDetails" runat="server">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Company Name</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtCompanyname" runat="server" CssClass="form-control" placeholder="Enter Company Name"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvComapnyname" runat="server" ControlToValidate="txtCompanyname"
                                                        ValidationGroup="ComapnyName" ErrorMessage="Enter the Comapny Name" CssClass="help-block" Display="Dynamic"
                                                        Text="Enter the Comapny Name"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Address Line 1 </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtAddress1" runat="server" CssClass="form-control" placeholder="Enter Address Line 1"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rqftxtAddress" runat="server" ControlToValidate="txtAddress1"
                                                        ValidationGroup="ComapnyName" ErrorMessage="Enter the Comapny Name" CssClass="help-block" Display="Dynamic"
                                                        Text="Enter the Comapny Name"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Address Line 2</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtAddress2" runat="server" CssClass="form-control" placeholder="Enter Address Line 2"></asp:TextBox>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Address Line 3 </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtAddress3" runat="server" CssClass="form-control" placeholder="Enter Address Line 3"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">City </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtCity" runat="server" CssClass="form-control" placeholder="Enter City"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvtxtCity" runat="server" ControlToValidate="txtCity"
                                                        ValidationGroup="ComapnyName" ErrorMessage="Enter the City" Display="Dynamic"
                                                        Text="Enter the City"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Province/State</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtState" runat="server" CssClass="form-control" placeholder="Enter State"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtState"
                                                        ValidationGroup="ComapnyName" ErrorMessage="Enter the State" Display="Dynamic"
                                                        Text="Enter the State"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">ZIP Code </span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtZipCode" runat="server" CssClass="form-control" placeholder="Enter Zip Code"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtZipCode"
                                                        ValidationGroup="ComapnyName" ErrorMessage="Enter the ZipCode" Display="Dynamic"
                                                        Text="Enter the ZipCode"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Country</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtCountry" runat="server" CssClass="form-control" OnTextChanged="txtCountry_TextChanged" AutoPostBack="true" placeholder="Enter Country"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ID="rfvTxtcountry" runat="server" ControlToValidate="txtCountry"
                                                        ValidationGroup="ComapnyName" ErrorMessage="Enter the Country" Display="Dynamic"
                                                        Text="Enter the Country"></asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <asp:ValidationSummary runat="server" ID="vsCompanyName" ValidationGroup="ComapnyName"
                                            ShowSummary="true" DisplayMode="SingleParagraph" HeaderText="Error Messsage* : " Visible="false" />
                                        <div class="form-actions">
                                            <div class="col-md-1"></div>
                                            <div class="col-md-11" style="align-content: center; text-align: center">
                                                <asp:Button ID="btnCompanyInfoSubmit" runat="server" CssClass="btn green button-submit"
                                                    Text="Submit" OnClick="btnCompanyInfoSubmit_Click" Visible="False" />
                                                <asp:Button ID="btnCompanyInfoNext" CssClass="btn btn-success green button-next" runat="server"
                                                    Text="Next" ValidationGroup="ComapnyName"
                                                    OnClick="btnCompanyInfoNext_Click" />
                                            </div>
                                        </div>

                                    </div>

                                    <div class="tab-pane" id="tabContactDetails" runat="server">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Contact Person</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtContatperson" runat="server" CssClass="form-control" placeholder="Enter Contact Person"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Contact Phone #</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtContactPhone" runat="server" CssClass="form-control" placeholder="Enter Contact Phone #"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Mobile # for Texting purpose</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtMobile" runat="server" CssClass="form-control" placeholder="Enter Mobile # for Texting purpose"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">E-mail</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control" placeholder="Enter E-mail"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Website</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtWebsite" runat="server" CssClass="form-control" placeholder="Enter Website"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="col-md-12" style="align-content: center; text-align: center">
                                                <asp:Button ID="btnContactBack" runat="server" Text="Back" CssClass="btn btn-default"
                                                    OnClick="btnContactBack_Click" Visible="False" />
                                                <asp:Button ID="btnContactSubmit" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="btnContactSubmit_Click"
                                                    Visible="False" />
                                                <asp:Button ID="btnContactNext" CssClass="btn btn-success" runat="server" Text="Next" OnClick="btnContactNext_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tabProductDetails" runat="server">
                                        <asp:GridView ID="gvProductList" AutoGenerateColumns="False" runat="server" DataKeyNames="ProductId"
                                            CssClass="table table-bordered mudargrid">
                                            <Columns>
                                                <asp:BoundField DataField="ProductID" HeaderText="Product ID" />
                                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="cbitem" runat="server" />
                                                    </ItemTemplate>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="cbheader" runat="server" onclick="javascript:selectallproduct(this);" />
                                                    </HeaderTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                        <div class="form-actions">
                                            <div class="col-md-12" style="align-content: center; text-align: center">
                                                <asp:Button ID="btnProductBack" runat="server" Text="Back" CssClass="btn btn-default"
                                                    OnClick="btnProductBack_Click" Visible="False" />
                                                <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="btn btn-info" Visible="False" />
                                                <asp:Button ID="btnProductNext" CssClass="btn btn-success" runat="server" Text="Next" OnClick="btnProductNext_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tabPortDetails" runat="server">
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Transport Mode</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:DropDownList ID="ddlTransportMode" runat="server" CssClass="form-control" ValidationGroup="TransportMode">
                                                        <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                        <asp:ListItem Value="0">Air</asp:ListItem>
                                                        <asp:ListItem Value="1">Sea</asp:ListItem>
                                                        <asp:ListItem Value="2">Rail</asp:ListItem>
                                                        <asp:ListItem Value="3">Road</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ID="rfvTransportMode" runat="server" ControlToValidate="ddlTransportMode"
                                                        ValidationGroup="TransportMode" ErrorMessage="select the Transport Mode" Display="dynamic"
                                                        Text="Please select transport mode">
                                                    </asp:RequiredFieldValidator>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Nearest Air port</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtAir" runat="server" CssClass="form-control" placeholder="Enter Nearest Air port"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Nearest Sea port</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtSea" runat="server" CssClass="form-control" placeholder="Enter Nearest Sea port"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Nearest Road port</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtRoad" runat="server" CssClass="form-control" placeholder="Enter Nearest Road port"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <div class="form-group">
                                                <label class="control-label col-md-3">
                                                    <span class="pull-right">Nearest Rail port</span>
                                                </label>
                                                <div class="col-md-4">
                                                    <asp:TextBox ID="txtRail" runat="server" CssClass="form-control" placeholder="Enter Nearest Rail port"></asp:TextBox>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-12">
                                            <asp:ValidationSummary runat="server" ID="TransportModeNext" ValidationGroup="TransportMode"
                                                ShowSummary="true" DisplayMode="SingleParagraph" HeaderText="Error Messsage* : " />
                                        </div>

                                        <div class="form-actions">
                                            <div class="col-md-12" style="align-content: center; text-align: center">
                                                <asp:Button ID="btnPortBack" runat="server" Text="Back" CssClass="btn btn-default"
                                                    OnClick="btnPortBack_Click" Visible="False" />
                                                <asp:Button ID="btnPortSubmit" runat="server" Text="Submit" CssClass="btn btn-info" OnClick="btnPortSubmit_Click"
                                                    Visible="False" />
                                                <asp:Button ID="btnPortNext" CssClass="btn btn-success" ValidationGroup="TransportMode" runat="server"
                                                    Text="Next" OnClick="btnPortNext_Click" />
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tabFrightTerms" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3" style="padding-top: 0px !important">
                                                        <span class="pull-right">FOB India</span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnFOB" runat="server" GroupName="priceterms" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3" style="padding-top: 0px !important">
                                                        <span class="pull-right">By Sea </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnCIFbySea" GroupName="priceterms" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3" style="padding-top: 0px !important">
                                                        <span class="pull-right">CIF By AIR </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnCIFAirEandEUSA" GroupName="priceterms" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3" style="padding-top: 0px !important">
                                                        <span class="pull-right">FOR Destination</span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnForDestination" GroupName="priceterms" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-actions">
                                                <div class="col-md-12" style="align-content: center; text-align: center">
                                                    <asp:Button ID="btnPriceTermsNext" CssClass="btn btn-success" runat="server" Text="Next" OnClick="btnPriceTermsNext_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="tab-pane" id="tabPaymentTerms" runat="server">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <span class="pull-right">100% with PO  </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnHundperAdvance" GroupName="paymentterms" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <span class="pull-right">50% with PO + 50% against Delivery </span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnFiftyfityAgnistDocs" GroupName="paymentterms" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <span class="pull-right">100% against Delivery</span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnHundAgnistDocs" GroupName="paymentterms" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-12">
                                                <div class="form-group">
                                                    <label class="control-label col-md-3">
                                                        <span class="pull-right">No of Days from Inv date</span>
                                                    </label>
                                                    <div class="col-md-4">
                                                        <asp:RadioButton ID="rbtnNoofInvDate" GroupName="paymentterms" runat="server" AutoPostBack="true"
                                                            OnCheckedChanged="rbtnNoofInvDate_CheckedChanged" />
                                                        <asp:DropDownList ID="ddlInvNoofDays" Visible="false" runat="server" OnSelectedIndexChanged="ddlInvNoofDays_SelectedIndexChanged">
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="60">60</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="form-actions">
                                            <div class="col-md-12" style="align-content: center; text-align: center">
                                                <asp:Button ID="btnPaymentTermsBack" runat="server" Text="Back" CssClass="btn btn-default"
                                                    OnClick="btnPaymentTermsBack_Click" Visible="False" />
                                                <asp:Button ID="btnPaymentTermsSumit" runat="server" Text="Submit" CssClass="btn btn-info" Visible="False" OnClick="btnPaymentTermsSumit_Click" />
                                                <asp:Button ID="btnPaymentTermsNext" CssClass="btn btn-success" runat="server" Text="Next" OnClick="btnPaymentTermsNext_Click" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

