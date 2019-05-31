<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="Buyer.aspx.cs" Inherits="Buyer_Buyer" %>
<%@ MasterType VirtualPath="~/MudarMaster.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
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
                <h3 id="header_Text">
                    Company Information</h3>
                <table>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblBuyerID" runat="server" Visible="false" Text="" />&nbsp;
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
                                Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rfvComapnyname"  runat="server" ControlToValidate="txtCompanyname" ValidationGroup="ComapnyName" ErrorMessage="Enter the ComapnyName" display="static" Text="*"  ></asp:RequiredFieldValidator></td>
                        <td rowspan="11" style="width: 20%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 1
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress1" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 2
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress2" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 3
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress3" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            City
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Province/State
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            ZIP Code
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtZipCode" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Country
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCountry" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" ontextchanged="txtCountry_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                    </tr>
                    <div id="divIndBuyer" runat="server">
                        <tr>
                            <td align="right">
                                TIN
                            </td>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTIN" runat="server" Height="30px" Style="margin-bottom: 1px"
                                    Width="340px" Font-Size="Medium"></asp:TextBox></td>
                   
                    </tr><tr>
                        <td align="right">
                            VAT
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtVAT" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                     <tr>
                        <td align="right">
                            CST
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCST" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr> </div>
                    <tr><td colspan="4"></td></tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:ValidationSummary runat="server" ID="vsCompanyName" 
                                 ValidationGroup="ComapnyName" ShowSummary="true" 
                                DisplayMode="SingleParagraph" HeaderText="Error Messsage* : "/>
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td>
                        </td>
                        <td align="right">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCompanyInfoSubmit"
                                runat="server" Text="Submit" CssClass="fb8" 
                                OnClick="btnCompanyInfoSubmit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnCompanyInfoNext" CssClass="fb8" runat="server" Text="Next" ValidationGroup="ComapnyName" OnClick="btnCompanyInfoNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ContactInfo" runat="server">
                <h3 id="header_Text">
                    Contact Information</h3>
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
                                Style="margin-bottom: 1px" Width="340px" Font-Size="Medium">
                            </asp:TextBox></td>
                        <td rowspan="8" style="width: 15%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Contact Phone #
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtContactPhone" runat="server" Height="30px"
                                Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Mobile # for Texting purpose
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMobile" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            e-mail
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEmail" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            website
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtWebsite" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
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
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnContactBack" runat="server" Text="Back" CssClass="fb8" 
                                OnClick="btnContactBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnContactSubmit" runat="server" Text="Submit" CssClass="fb8" 
                                OnClick="btnContactSubmit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnContactNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnContactNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="NotifyInfo" runat="server">
                <h3 id="header_Text">
                    Notify Information</h3>
                <table>
                     <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="8" style="width: 20%;">
                        </td>
                        <td align="right">
                            Notify (Custom Broker)
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNotifyName" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        <td rowspan="8" style="width: 20%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 1
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNAddress1" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 2
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNAddress2" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 3
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNAddress3" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            City
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNCity" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Province/State
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNState" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            ZIP Code
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNZipCode" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Country
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtNCountry" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                        &nbsp;
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnNotifyBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnNotifyBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnNotifySubmit" runat="server" Text="Submit" CssClass="fb8" 
                                OnClick="btnNotifySubmit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnNotifyNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnNotifyNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="BankInfo" runat="server">
                <h3 id="header_Text">
                    Buyer Bank Information</h3>
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
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                        <td rowspan="8" style="width: 20%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 1
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress1" runat="server" Font-Size="Medium" 
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address Line 2
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress2" runat="server" Font-Size="Medium" 
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" class="style2">
                            Address Line 3
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBAddress3" runat="server" Font-Size="Medium" 
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" class="style2">
                            City
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCity" runat="server" Font-Size="Medium" Height="30px" 
                                Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Province/State
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBState" runat="server" Font-Size="Medium" 
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right" class="style2">
                            ZIP Code
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBZipcode" runat="server" Font-Size="Medium" 
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Country
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBCountry" runat="server" Font-Size="Medium" 
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                        <tr>
                            <td colspan="4">
                                &nbsp;
                            </td>
                        </tr>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnBankBack" runat="server" CssClass="fb8" 
                                OnClick="btnBankBack_Click" Text="Back" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnBankSubmit" runat="server" CssClass="fb8" 
                                OnClick="btnBankSubmit_Click" Text="Submit" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnBankNext" runat="server" CssClass="fb8" 
                                OnClick="btnBankNext_Click" Text="Next" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="PortInfo" runat="server">
                <h3 id="header_Text">
                    Port Information</h3>
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
                            Transport Mode
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp; &nbsp;<asp:DropDownList ID="ddlTransportMode" runat="server" 
                                Font-Size="Medium" Height="35px" TabIndex="9" Width="340px" ValidationGroup="TransportMode">
                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                            <asp:ListItem Value="0">Air</asp:ListItem>
                            <asp:ListItem Value="1">Sea</asp:ListItem>
                            <asp:ListItem Value="2">Rail</asp:ListItem>
                            <asp:ListItem Value="3">Road</asp:ListItem>
                            </asp:DropDownList>
                            
                            <asp:RequiredFieldValidator ID="rfvTransportMode"  runat="server" ControlToValidate="ddlTransportMode" ValidationGroup="TransportMode" ErrorMessage="select the Transport Mode" display="static" Text="*"  >
                            
                            </asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="8" style="width: 20%;">
                       
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Nearest Air port
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAir" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
                    </tr>
                    <tr>
                       
 <td align="right">
                            Nearest Sea Port
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSea" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Nearest Road Port
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRoad" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
                    </tr>
                    <tr>
                         <td align="right">
                            Nearest Rail Port
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtRail" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"> &nbsp;&nbsp;</asp:TextBox></td>
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
                        <td colspan="4" align="center">
                            <asp:ValidationSummary runat="server" ID="TransportModeNext" 
                                 ValidationGroup="TransportMode" ShowSummary="true" 
                                DisplayMode="SingleParagraph" HeaderText="Error Messsage* : "/>
                        </td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnPortBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnPortBack_Click" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnPortSubmit" runat="server" Text="Submit" CssClass="fb8" 
                                OnClick="btnPortSubmit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnPortNext" CssClass="fb8" ValidationGroup="TransportMode" runat="server" Text="Next" OnClick="btnPortNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="PriceTerms" runat="server">
                <h3 id="header_Text">
                    Price Terms</h3>
                <table align="center">
                    <tr>
                        <td colspan="4">
                        &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="18" style="width:25%;">
                        </td>
                        <td align="right">
                            FOB India
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnFOB" runat="server"  
                                 GroupName="priceterms" Font-Size="Larger" 
                                   oncheckedchanged="rbtnFOB_CheckedChanged"   />
                        </td>
                        <td rowspan="18" style="width:30%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            &nbsp;</td>
                        <td align="left">
                            &nbsp;</td>
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
                        <td align="right">
                            CIF&nbsp; by Sea
                        </td>
                        <td align="center">
                             &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCIFbySea" runat="server" 
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
                            CIF By AIR&nbsp;&nbsp;
                        </td>
                        <td align="center">
                            &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnCIFAirEandEUSA" runat="server" GroupName="priceterms" 
                                 Font-Size="Larger" /></td>
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
                        <td align="left">
                            &nbsp;</td>
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
                    <tr><td colspan="2">&nbsp; </td></tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnPriceTermsBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnPriceTermsBack_Click" />
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;<asp:Button ID="btnPriceTermsSubmit" runat="server" Text="Submit" CssClass="fb8"
                                OnClick="btnPriceTermsSubmit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnPriceTermsNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnPriceTermsNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="PaymentTerms" runat="server">
                <h3 id="header_Text">
                    Payment Terms</h3>
                <table align="center">
                    <tr><td colspan="4">&nbsp;</td></tr>
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="12" style="width: 10%;">
                        </td>
                        <td align="right">
                            100% Advance
                        </td>
                        <td align="left">
                            &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnHundperAdvance" GroupName="paymentterms" runat="server"
                                Font-Size="Larger"/>
                        </td>
                        <td rowspan="12" style="width: 30%;">
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
                            50% adv + 50% against Docs
                        </td>
                        <td align="left">
                            &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnFiftyfityAgnistDocs" GroupName="paymentterms" runat="server"
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
                            100% against Docs
                        </td>
                        <td align="left">
                            &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnHundAgnistDocs" runat="server" Font-Size="Larger" 
                                GroupName="paymentterms" />
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
                            No of Days from Inv date
                        </td>
                        <td align="left">
                            &nbsp; &nbsp; &nbsp;<asp:RadioButton ID="rbtnNoofInvDate" runat="server" 
                                Font-Size="Larger" GroupName="paymentterms" />&nbsp;&nbsp;
                                <asp:DropDownList ID="ddlInvNoofDays" runat="server" Height="20px" 
                                Width="60px" onselectedindexchanged="ddlInvNoofDays_SelectedIndexChanged">
                                  <asp:ListItem Value="15">15</asp:ListItem>
                                  <asp:ListItem Value="30">30</asp:ListItem>
                                  <asp:ListItem Value="45">45</asp:ListItem>
                                  <asp:ListItem Value="60">60</asp:ListItem>
                                </asp:DropDownList>
                        </td>
                    </tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr><td colspan="2">&nbsp;</td></tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr><td colspan="3">&nbsp;</td></tr>
                    <tr>
                        <td colspan="3">
                            &nbsp;</td>
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
                <h3 id="header_Text">
                    Conformation</h3>
                <table width="100%"  style="height:330px; background-color:#F7F7DE" >
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                     <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            Thank You<br>
                            We will contact you on your e-mail within next 24 hours with username and Password
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <br></br>
                            <asp:Button ID="Button1" CssClass="fb8" runat="server" Text="Home" OnClick="btnPaymentTermsNext_Click"
                                PostBackUrl="~/Login.aspx" />
                        </td>
                    </tr>
                </table>
            </asp:View>
            <asp:View ID="ProductInfo" runat="server">
                <h3 id="header_Text">
                    Products Registration</h3>
                <table width="100%" align="center">
                   <tr align="center">
                   <td> <asp:GridView ID="gvProductList" AutoGenerateColumns="False" runat="server"
                                    DataKeyNames="ProductId" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
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
                                </asp:GridView></td>
                   </tr>
                </table>
                <table width="885">
                    <tr>
                        <td align="left">
                            <asp:Button ID="btnProductBack" runat="server" Text="Back" CssClass="fb8" onclick="btnProductBack_Click" 
                                 />
                        </td>
                        <td align="center">
                            <asp:Button ID="Button3" runat="server" Text="Submit" CssClass="fb8" 
                                Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnProductNext" CssClass="fb8" runat="server" Text="Next" 
                                onclick="btnProductNext_Click" />
                        </td>
                    </tr>
                </table>
            </asp:View>
        </asp:MultiView>
    </div>
</asp:Content>

