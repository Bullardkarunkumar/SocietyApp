<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="BuyerUpdate.aspx.cs" Inherits="Buyer_BuyerUpdate" %>

<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
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

    </script>
    <div id="content_area_Home" style="height: auto">
        <asp:MultiView ID="MainBuyerView" runat="server" ActiveViewIndex="0">
            <asp:View ID="CompanyInfo" runat="server">
                <h3 id="header_Text">Company Information</h3>
                <table>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblMudarEmail" runat="server" Visible="false" Text="" />&nbsp;
                            <asp:Label ID="lblBuyerID" runat="server" Visible="false" Text="" />&nbsp;
                            <asp:Button ID="btnFullview" runat="server" CssClass="fb8" Text="FullView" OnClick="btnFullview_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="11" style="width: 23%;"></td>
                        <td align="right">Company Name 
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompanyname" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium" padding-left="5px" Enabled="false"></asp:TextBox><asp:RequiredFieldValidator ID="rfvComapnyname" runat="server" ControlToValidate="txtCompanyname"
                                ValidationGroup="ComapnyName" ErrorMessage="Enter the ComapnyName" Display="static"
                                Text="*"></asp:RequiredFieldValidator></td>
                        <td rowspan="11" style="width: 20%;"></td>
                    </tr>
                    <tr>
                        <td align="right">Address Line 1
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress1" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rqftxtAddress" runat="server" ControlToValidate="txtAddress1"
                                ValidationGroup="ComapnyName" ErrorMessage="Enter the ComapnyName" Display="static"
                                Text="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right">Address Line 2
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress2" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">Address Line 3
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress3" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">City
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rfvtxtCity" runat="server" ControlToValidate="txtCity"
                                ValidationGroup="ComapnyName" ErrorMessage="Enter the City" Display="static"
                                Text="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right">Province/State
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtState"
                                ValidationGroup="ComapnyName" ErrorMessage="Enter the State" Display="static"
                                Text="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right">ZIP Code
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtZipCode" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtZipCode"
                                ValidationGroup="ComapnyName" ErrorMessage="Enter the ZipCode" Display="static"
                                Text="*"></asp:RequiredFieldValidator></td>
                    </tr>
                    <tr>
                        <td align="right">Country
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCountry" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium" OnTextChanged="txtCountry_TextChanged"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvTxtcountry" runat="server" ControlToValidate="txtCountry"
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
                        <td align="center" colspan="4">
                            <asp:ValidationSummary ID="vsCompanyName" runat="server" DisplayMode="SingleParagraph" HeaderText="Error Messsage* : " ShowSummary="true" ValidationGroup="ComapnyName" Visible="false" />
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td align="right">
                            <asp:Button ID="btnCompanyInfo" runat="server" CssClass="fb8"
                                Text="Edit" ValidationGroup="ComapnyName" OnClick="btnCompanyInfo_Click" />
                            <asp:Button ID="btnUpdateCI" runat="server" CssClass="fb8" Text="update" ValidationGroup="ComapnyName" Visible="false" OnClick="btnUpdateCI_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnCompanyInfoNext" runat="server" CssClass="fb8"
                                OnClick="btnCompanyInfoNext_Click" Text="Next"
                                ValidationGroup="ComapnyName" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ContactInfo" runat="server">
                <h3 id="header_Text">Contact Information</h3>
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
                            Style="margin-bottom: 1px; padding-left: 5px;" Width="340px" Font-Size="Medium">
                        </asp:TextBox></td>
                        <td rowspan="8" style="width: 15%;"></td>
                    </tr>
                    <tr>
                        <td align="right">Contact Phone #
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Height="30px"
                            Style="margin-bottom: 1px; padding-left: 5px;" Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">Mobile # for Texting purpose
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMobile" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">e-mail
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">website
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWebsite" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox></td>
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
                            <asp:Button ID="btnContactinfoBank" runat="server" Text="Back" CssClass="fb8" OnClick="btnContactinfoBank_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnContactemail" runat="server" Text="Edit" CssClass="fb8" OnClick="btnContactemail_Click" />
                            <asp:Button ID="btnUpdateContact" runat="server" CssClass="fb8" Text="update" ValidationGroup="ComapnyName" Visible="false" OnClick="btnUpdateContact_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnContactinfo" CssClass="fb8" runat="server" Text="Next" OnClick="btnContactinfo_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="NotifyInfo" runat="server">
                <h3 id="header_Text">Notify Information</h3>
                <table>
                    <tr>
                        <td colspan="4" align="center" style="color: #FF0000">&nbsp;* Notify same as buyer
                            <asp:CheckBox ID="cdNotify" runat="server" AutoPostBack="true" OnCheckedChanged="cdNotify_CheckedChanged" />
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="8" style="width: 20%;"></td>
                        <td align="right">
                            <asp:Label ID="lblCustom" Visible="false" runat="server" Text="CustomBroker/" />Notify
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
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNZipCode" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">Country
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNCountry" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
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
                            <asp:Button ID="btnNotifyBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnNotifyBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnNotify" CssClass="fb8" runat="server" Text="Edit" OnClick="btnNotify_Click" />
                            <asp:Button ID="btnUpdNotify" runat="server" CssClass="fb8" Text="update" ValidationGroup="ComapnyName" Visible="false" OnClick="btnUpdNotify_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnNotifyNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnNotifyNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="BankInfo" runat="server">
                <h3 id="header_Text">Buyer Bank Information</h3>
                <table align="center">
                    <tr>
                        <td colspan="4" align="center" style="color: #FF0000">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;If Documnet routing is through Bank
                                <asp:RadioButton ID="rbBank" runat="server"
                                    Font-Size="Larger" OnCheckedChanged="rbBank_CheckedChanged" GroupName="check" AutoPostBack="true" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Consignee
                            <asp:RadioButton ID="rbConsignee" runat="server" Font-Size="Larger" OnCheckedChanged="rbConsignee_CheckedChanged" GroupName="check" AutoPostBack="true" /></td>
                    </tr>
                    <tr id="trCong" runat="server" visible="false">
                            <td colspan="4" align="center" style="color: #FF0000">
                                Consignee same as Buyer.
                            <asp:CheckBox ID="cbconsignee" runat="server" OnCheckedChanged="cbconsignee_CheckedChanged" AutoPostBack="true" />
                            </td>
                        </tr>
                    <div id="divForm" runat="server" visible="false">
                    <tr>
                        <td rowspan="8" style="width: 20%;"></td>
                        <td align="right"><asp:Label ID="lblTex" runat="server" />&nbsp;Name
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBankname" runat="server" Font-Size="Medium"
                            Height="30px" Style="margin-bottom: 1px; padding-left: 5px;" Width="340px"></asp:TextBox></td>
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
                            Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
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
                    </tr>
                        </div>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnBankBack" runat="server" CssClass="fb8" OnClick="btnBankBack_Click"
                                Text="Back" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnBank" runat="server" CssClass="fb8" OnClick="btnBank_Click" Text="Edit" />
                            <asp:Button ID="btnUpdBank" runat="server" CssClass="fb8" Text="update" Visible="false" OnClick="btnUpdBank_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnBankNext" runat="server" CssClass="fb8" OnClick="btnBankNext_Click"
                                Text="Next" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="PortInfo" runat="server">
                <h3 id="header_Text">Port Information</h3>
                <table align="center">
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="8" style="width: 20%;"></td>
                        <td align="right">Transport Mode
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlTransportMode" runat="server" Font-Size="Medium"
                            Height="35px" TabIndex="9" Width="340px" ValidationGroup="TransportMode">
                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                            <asp:ListItem Value="0">Air</asp:ListItem>
                            <asp:ListItem Value="1">Sea</asp:ListItem>
                            <asp:ListItem Value="2">Rail</asp:ListItem>
                            <asp:ListItem Value="3">Road</asp:ListItem>
                        </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="rfvTransportMode" runat="server" ControlToValidate="ddlTransportMode"
                                ValidationGroup="TransportMode" ErrorMessage="select the Transport Mode" Display="static"
                                Text="*">
                            
                            </asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="8" style="width: 20%;"></td>
                    </tr>
                    <tr>
                        <td align="right">Nearest Air port
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAir" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">Nearest Sea Port
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSea" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">Nearest Road Port
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRoad" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">Nearest Rail Port
                        </td>
                        <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRail" runat="server" Height="30px" Style="margin-bottom: 1px; padding-left: 5px;"
                            Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
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
                        <td colspan="4" align="center">
                            <asp:ValidationSummary runat="server" ID="TransportModeNext" ValidationGroup="TransportMode"
                                ShowSummary="true" DisplayMode="SingleParagraph" HeaderText="Error Messsage* : " />
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnPortBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnPortBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnPortNext" CssClass="fb8" ValidationGroup="TransportMode" runat="server"
                                Text="Edit" OnClick="btnPortNext_Click" />
                            <asp:Button ID="btnUpdPort" runat="server" CssClass="fb8" Text="update" Visible="false" OnClick="btnUpdPort_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnHome" CssClass="fb8" Text="Home" runat="server"
                                OnClick="btnHome_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ProductInfo" runat="server">
                <h3 id="header_Text">Products Registration</h3>
                <table width="100%" align="center">
                    <tr align="center">
                        <td>
                            <asp:GridView ID="gvProductList" AutoGenerateColumns="False" runat="server"
                                DataKeyNames="ProductId" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid"
                                BorderWidth="2px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <RowStyle BackColor="#F7F7DE" />
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
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnProductBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnProductBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnProduct" runat="server" Text="Edit" OnClick="btnProduct_Click" CssClass="fb8" />
                            <asp:Button ID="btnupdProduct" runat="server" CssClass="fb8"  Text="update" Visible="false" OnClick="btnupdProduct_Click" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnProductNext" CssClass="fb8" runat="server" Text="Next"
                                OnClick="btnProductNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

