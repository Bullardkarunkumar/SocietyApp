<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="Farmerview.aspx.cs" Inherits="Farmer_Farmerview" Title="Untitled Page" %>

<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">

    <script type="text/javascript" language="javascript">
        var gridID = '<%=lblStatus.ClientID%>'
        function uploadError(sender, args) {
            document.getElementById('<%=lblStatus.ClientID%>').innerText = args.get_fileName(), "<span style='color:red;'>" + args.get_errorMessage() + "</span>";
        }

        function StartUpload(sender, args) {
            document.getElementById('<%=lblStatus.ClientID%>').innerText = 'Uploading Started.';
        }

        function UploadComplete(sender, args) {
            var pathstr = "../Attachments/Farmer/";
            var filename = args.get_fileName();
            var contentType = args.get_contentType();
            var text = "Size of " + filename + " is " + args.get_length() + " bytes";
            if (contentType.length > 0) {
                text += " and content type is '" + contentType + "'.";
            }
            pathstr = pathstr + document.getElementById('<%= HiddenField1.ClientID %>').value + "/" + filename;
        document.getElementById('<%= hfUserPhotoPath.ClientID %>').value = pathstr;
        document.getElementById('<%=imgFarmerP.ClientID%>').src = pathstr;
        document.getElementById('<%=lblStatus.ClientID%>').innerText = text;
        }

        function DataSelectChanged(sender, args) {
            var str = sender.get_selectedDate();
            document.getElementById('<%= lblAge.ClientID %>').innerText = CalcYear(new Date(str), new Date());


        }
        function CalcYear(d1, d2) {
            return d2.getFullYear() - d1.getFullYear();
        }
        function checkstatus(bEnable) {

            document.getElementById('<%= txtAccountNo.ClientID %>').disabled = !bEnable
        document.getElementById('<%= txtBankName.ClientID %>').disabled = !bEnable
        document.getElementById('<%= txtAcccountHolder.ClientID %>').disabled = !bEnable
        }
    </script>
    <div id="content_area_Home" style="height: auto">

        <div id="header_Text">
            Farmer Registration Information
        </div>

        <div id="divMain">
            <!----------------------Farmer information start------------------------------------------------------------------------------------------------->
            <div id="divfarmerInfoView" runat="server">
                <table align="center" width="100%" border="1">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Farmer Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:LinkButton
                            ID="lbtnFarmerEdit" runat="server" Text="Edit"
                            OnClick="lbtnFarmerEdit_Click"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Farmer name</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblFarmerName" runat="server" />&nbsp;<asp:HiddenField ID="hdfarmerUid" runat="server" Value="" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Total Area in Hc<br />
                            <asp:Label ID="lblTotalArea" runat="server" /></td>
                        <td align="center">Total number of Plots<br />
                            <asp:Label ID="lblPlots" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Father name</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblFatherName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Address</td>
                        <td width="50%">
                            <asp:Label ID="lblAddress" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Fixed Phone</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblPhone" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Mobile Phone</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblMobile" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Farmer Code</td>
                        <td width="50%">&nbsp;<asp:Label
                            ID="lblFCode" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Farmer Registration no</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblFarmerRegistration" runat="server" /></td>
                    </tr>
                </table>
            </div>
            <!-- Edit Farmer info button code-->
            <div id="divFarmerDetails" runat="server" visible="false">
                <div>
                    <table width="100%">
                        <tr style="border: 1px solid #ffffff; font-size: 18px; color: #ffffff; background-color: #FDB700;">
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                Farmer Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnFarmerClose" runat="server" Text="Close"
                                OnClick="lbtnFarmerClose_Click" />
                            </td>
                        </tr>
                    </table>
                    <asp:HiddenField ID="HiddenField1" runat="server" />
                    <asp:HiddenField ID="hfUserPhotoPath" runat="server" />
                    <asp:Label ID="lblfarmerUid" runat="server" Visible="false" Text="" />
                    <table align="center">
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="10"></td>
                            <td align="right">Farmer Name
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFarmerName" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="1"></asp:TextBox><asp:RequiredFieldValidator
                                    ID="rfvFarmername" runat="server" ControlToValidate="txtFarmername" ValidationGroup="FarmerName"
                                    ErrorMessage="Enter the Farmer Name" Display="static" Text="*"></asp:RequiredFieldValidator></td>
                            <td rowspan="10">
                                <asp:UpdatePanel ID="upFarmerPhoto" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <asp:Image ID="imgFarmerP" runat="server" Height="140" Width="140" />
                                        <br />
                                        <asp:AsyncFileUpload ID="afuFarmerPhoto" runat="server" CompleteBackColor="Lime"
                                            ErrorBackColor="Red" OnClientUploadComplete="UploadComplete" OnClientUploadError="uploadError"
                                            OnClientUploadStarted="StartUpload" OnUploadedComplete="afuFarmerPhoto_UploadedComplete"
                                            ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF" />
                                        <br />
                                        <asp:Label ID="Throbber" runat="server" Style="display: none">
                            <img align="middle" alt="loading" src="../images/indicator.gif" />
                                        </asp:Label>
                                        <br />
                                        <br />
                                        <asp:Label ID="lblStatus" runat="server" Style="font-family: Arial; font-size: small;">
                                        </asp:Label>
                                    </ContentTemplate>
                                    <Triggers>
                                        <asp:AsyncPostBackTrigger ControlID="afuFarmerPhoto" EventName="" />
                                    </Triggers>
                                </asp:UpdatePanel>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Father Name
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFatherName" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="2"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Address
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="3"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">City
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="4"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">District
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDistrict" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="5"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Taluk
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTaluk" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="6"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">State
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="7"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Country
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCountry" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="8"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Phone
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhone" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="9"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">MPhone
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMPhone" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TabIndex="10"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:ValidationSummary runat="server" ID="vsFarmerName" ValidationGroup="FarmerName"
                                    ShowSummary="true" DisplayMode="SingleParagraph" HeaderText="Error Messsage* : " />
                            </td>
                        </tr>
                    </table>
                    <table width="885">
                        <tr>
                            <td></td>
                            <td align="right">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            </td>
                            <td align="right">
                                <asp:Button ID="btnFarmerNext" CssClass="fb8" runat="server" ValidationGroup="FarmerName"
                                    Text="Submit" OnClick="btnFarmerNext_Click" TabIndex="11" />
                            </td>
                        </tr>
                    </table>
                </div>

            </div>
            <!----------------------------------------------Farmer information End------------------------------------------------------------------------------>


            <!---------------------------------------------Farmer Bank information start------------------------------------------------------------------------->
            <div id="divbankinfoView" runat="server">
                <table border="1" width="100%">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;Bank Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton
                ID="lbtnBank" runat="server" Text="Edit"
                OnClick="lbtnBankEdit_Click"></asp:LinkButton></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Name of the Bank</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblBankName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">A/C holder name</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblHolderName" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">A/C Number</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblAcctNo" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Last date of chemical application</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblChemicalDate" runat="server" /></td>
                    </tr>
                    <tr>

                        <td align="center" width="50%">Organic<%--link master page and content page--%>
                        </td>
                        <td width="50%">
                            <asp:Label ID="lblOrg" runat="server" /><%--link master page and content page--%>
                        </td>
                    </tr>
                    <tr>
                        <td align="center">Org & FairTrade</td>
                        <td>
                            <asp:Label ID="lblNonOrganic" runat="server" /></td>
                    </tr>
                </table>
            </div>
            <!-- Edit Bank infobutton code-->
            <div id="divBankDetails" runat="server" visible="false">
                <div>
                    <table width="100%">
                        <tr style="border: 1px solid #ffffff; font-size: 18px; color: #ffffff; background-color: #FDB700;">
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                Bank Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lblBankClose" runat="server" Text="Close"
                                OnClick="lblBankClose_Click" />

                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="10" style="width: 20%;"></td>
                            <td align="right">Farmer Code:
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblFarmerCode" runat="server" Text=""></asp:Label></td>
                            <td rowspan="10" style="width: 20%;" align="left"></td>
                        </tr>
                        <tr>
                            <td align="right">Farmer Reg Number
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFarmerRegNo" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Bank Account
                            </td>
                            <td align="left">&nbsp;&nbsp;
                    <asp:CheckBox ID="cbBank" runat="server" onclick="checkstatus(this.checked);" />
                                &nbsp;&nbsp;&nbsp;Yes or No
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Bank Account No
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAccountNo" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Account Holder Name
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAcccountHolder" runat="server" Height="30px"
                                Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Bank Name
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBankName" runat="server" Height="30px" Style="margin-bottom: 1px"
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
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                    </table>
                    <table width="885">
                        <tr>
                            <td align="left">&nbsp;</td>
                            <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td align="right">
                                <asp:Button ID="btnBankNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnBankNext_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!---------------------------------------------Farmer Bank information End--------------------------------------------------------------------------->

            <!------------------------------------------Farmer plot information start---------------------------------------------------------------------------->
            <div id="divplotinfoview" runat="server">
                <table align="center" width="100%">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
            &nbsp;&nbsp;&nbsp;&nbsp;Plot Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:LinkButton
                ID="lbtnPlot" runat="server" Text="Edit" OnClick="lbtnPlotEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:GridView ID="gvPlot" runat="server"
                                DataKeyNames="FarmID" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical" BorderStyle="None">
                                <RowStyle BackColor="#F7F7DE" />
                                <Columns>
                                    <asp:BoundField HeaderText="Plot Code" DataField="AreaCode" />
                                    <asp:BoundField HeaderText="Area in Hc" DataField="PlotArea" />
                                    <asp:BoundField HeaderText="Latitude" DataField="Latitude" />
                                    <asp:BoundField HeaderText="Longitude" DataField="Longitude" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black"
                                    HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
            <!--edit plot info-->
            <div id="divNewFarmDetails" runat="server" visible="false">
                <div align="center">
                    <table width="100%">
                        <tr style="border: 1px solid #ffffff; font-size: 18px; color: #ffffff; background-color: #FDB700;">
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                Plot Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnPlotClose" runat="server" Text="Close"
                                OnClick="lbtnPlotClose_Click" />
                            </td>
                        </tr>
                    </table>

                    <table>
                        <tr>
                            <td align="right">Last Date Of Chemical Application
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtLastDate" runat="server" CssClass="textbox_Style"></asp:TextBox>
                                <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtLastDate">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Organic
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbOrganic" runat="server" GroupName="organic"
                                    Checked="true" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Organic & Fair Trade
                            </td>
                            <td align="left">
                                <asp:RadioButton ID="rbOrganicFairTrad" runat="server" GroupName="organic" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Total Area :
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotArea" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Total Plots :
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotalPlots" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">Click ADD button to Add new Plot :
                        <asp:Button ID="btnAddPlot" CssClass="fb8" runat="server" Text="ADD" OnClick="btnAddPlot_Click" />
                                <asp:GridView ID="gvfarmdetails" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                    CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowCommand="gvfarmdetails_RowCommand">
                                    <RowStyle BackColor="#F7F7DE" />
                                    <Columns>
                                        <asp:BoundField DataField="AreaCode" HeaderText="Plot Code" />
                                        <asp:TemplateField HeaderText="Area in (HC)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPSize" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlotArea")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Latitude">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLatitude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Latitude")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Longtiude">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLongtiude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Longitude")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Button" Text="Remove" CommandName="Remove" ControlStyle-CssClass="fb8" />
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
                    <%--<asp:RadioButton ID="rbOrganic" Text="Organic" 
                GroupName="organic" runat="server" 
                oncheckedchanged="rbOrganic_CheckedChanged" Visible="false" />--%>
                    <table width="885px">
                        <tr>
                            <td align="left">&nbsp;</td>
                            <td></td>
                            <td align="right">
                                <asp:Button ID="btnNFarmNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnNFarmNextt_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!------------------------------------------Farmer plot information End------------------------------------------------------------------------------>

            <!------------------------------------------Farmer Season information start---------------------------------------------------------------------------->
            <div id="divseasoninfoview" runat="server">
                <table align="center" width="100%">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            Season Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:LinkButton ID="lbtnSeasonEdit" runat="server" Text="Edit" OnClick="lbtnSeasonEdit_Click" />
                            <asp:LinkButton ID="lbtnSeasocClose" runat="server" Text="Close"
                                OnClick="lbtnSeasocClose_Click" Visible="false" />

                        </td>
                    </tr>
                </table>
                <table align="center" width="100%">
                    <tr>
                        <td colspan="2" align="center">Select Season Year &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlSeasonYear" runat="server"
                            AutoPostBack="true"
                            OnSelectedIndexChanged="ddlSeasonYear_SelectedIndexChanged" />
                        </td>
                    </tr>
                </table>
            </div>
            <div align="center">
                <%--<asp:DataList ID="dlFarmerSeasonDetails" DataKeyField="SeasonID" runat="server" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                        GridLines="Vertical">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblseasonName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SeasonName")%>' />
                                        <asp:HiddenField ID="hfStartDate" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                        <asp:HiddenField ID="hfEndDate" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "EndDate")%>' />
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="cblProduct" runat="server" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>--%>

                <asp:GridView ID="gvFarmerSeasonDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="Product Id"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" ForeColor="Black" GridLines="Both" OnRowDataBound="gvFarmerSeasonDetails_RowDataBound">
                    <Columns>
                    </Columns>
                    <RowStyle BackColor="#F7F7DE" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                    <AlternatingRowStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle" />
                </asp:GridView>

                <table width="885px">
                    <tr>
                        <td align="left">&nbsp;</td>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnSeasonSubmit" CssClass="fb8" runat="server" Text="Submit"
                                OnClick="btnSeasonSubmit_Click" Visible="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <div id="divSeasonSubmit" runat="server">
                <%--<asp:RadioButton ID="rbNonOrganic" Text="Org & Fair Trade"
                    GroupName="organic" runat="server" Visible="false" />--%>
            </div>

            <!--edit season info-->

            <!------------------------------------------Farmer season information end---------------------------------------------------------------------------->


            <!------------------------------------------Farmer Risk information start---------------------------------------------------------------------------->
            <div id="divRiskinfoview" runat="server">
                <table align="center" width="100%">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        Risk Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                       <asp:LinkButton ID="lbtnRiskEdit" runat="server" Text="Edit"
                                                           OnClick="lbtnRiskEdit_Click" />
                            <asp:LinkButton ID="lbtnRiskClose" runat="server" Text="Close"
                                OnClick="lbtnRiskClose_Click" Visible="false" />

                        </td>
                    </tr>
                </table>
                <table align="center">
                    <tr>
                        <td colspan="2" align="center">
                            <asp:DataList ID="dlFieldRisk" runat="server" BackColor="White"
                                BorderColor="#DEDFDE" BorderWidth="1px" CellPadding="4"
                                ForeColor="Black" BorderStyle="None" GridLines="Vertical">
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                <asp:Label ID="lblFieldRisk" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AreaCode")%>' />
                                                : 
                                <asp:HiddenField ID="hfFieldRisk" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "FarmID")%>' />
                                            </td>
                                            <td>
                                                <asp:RadioButtonList ID="rblFieldRisk" runat="server" RepeatDirection="Horizontal" />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                                <FooterStyle BackColor="#CCCC99" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#F7F7DE" />
                                <SelectedItemStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            </asp:DataList></td>
                    </tr>
                </table>
            </div>
            <div id="divRiskSubmit" runat="server">
                <table width="885px">
                    <tr>
                        <td align="left">&nbsp;
                        </td>
                        <td></td>
                        <td align="right">
                            <asp:Button ID="btnFieldRiskNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnFieldRiskNext_Click" Visible="false" />

                        </td>
                    </tr>
                </table>
            </div>
            <!--edit Risk info-->

            <!------------------------------------------Farmer Risk information start---------------------------------------------------------------------------->

            <!------------------------------------------Farmer Family information start---------------------------------------------------------------------------->
            <div id="divFamilyinfoview" runat="server">
                <table align="center" width="100%" border="1">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Family Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;<asp:LinkButton ID="lbtnFamilyEdit" runat="server" Text="Edit"
                        OnClick="lbtnFamilyEdit_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td align="center" width="50%">Earning members</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblEarningMember" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Dependent Elders</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblDElder" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Dependent Children</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblDChildren" runat="server" /></td>
                    </tr>
                    <tr>

                        <td colspan="2" align="center">
                            <asp:GridView ID="gvFamilyDet" runat="server"
                                AutoGenerateColumns="False" DataKeyNames="FFamilyDetailsId"
                                BackColor="White" BorderColor="#DEDFDE"
                                BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                BorderStyle="None">

                                <RowStyle BackColor="#F7F7DE" />

                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="ChildName" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                    <asp:BoundField DataField="DOB" HeaderText="DateofBirth" DataFormatString="{0:dd MMM yyyy}" />
                                    <asp:BoundField DataField="AGE" HeaderText="Age" />
                                    <asp:BoundField DataField="SchoolGoing" HeaderText="School Going" />
                                    <asp:BoundField DataField="Working" HeaderText="Work Going" />
                                    <%--<asp:ButtonField ButtonType="Image" CommandName="cmd_delete" HeaderText="Delete"
                                           ImageUrl="~/images/Delete.jpg" ItemStyle-HorizontalAlign="Center">
                                           <ItemStyle HorizontalAlign="Center"/>
                                       </asp:ButtonField>--%>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black"
                                    HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

            </div>
            <!--edit Famil info-->
            <div id="divFamilyDetails" runat="server" visible="false">
                <div align="center">
                    <table width="100%">
                        <tr style="border: 1px solid #ffffff; font-size: 18px; color: #ffffff; background-color: #FDB700;">
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                Family Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnFamilyClose" runat="server" Text="Close"
                                OnClick="lbtnFamilyClose_Click" />
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td rowspan="5" style="width: 15%;"></td>
                            <td align="right">Earning Members
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEarningMember" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px" Width="340px">0</asp:TextBox></td>
                            <td rowspan="5" style="width: 15%;"></td>
                        </tr>
                        <tr>
                            <td align="right">Dependent Elders
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDependentElder" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px" Width="340px">0</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">&nbsp; Dependent Childs :
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:Label ID="lblChildCount" runat="server" Text=""></asp:Label></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:LinkButton ID="lbtnAddChild" runat="server">Add Child</asp:LinkButton><asp:ModalPopupExtender
                                    ID="ModalPopupExtender1" runat="server" TargetControlID="lbtnAddChild" PopupControlID="pAddChild"
                                    BackgroundCssClass="modalBackground" CancelControlID="CancelButton" DropShadow="true"
                                    PopupDragHandleControlID="pAddChild" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:GridView ID="gvFarmerChildDetails" runat="server"
                                    AutoGenerateColumns="False" DataKeyNames="FFamilyDetailsId"
                                    BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                    GridLines="Vertical" OnRowCommand="gvFarmerChildDetails_RowCommand"
                                    OnRowDataBound="gvFarmerChildDetails_RowDataBound">
                                    <RowStyle BackColor="#F7F7DE" />
                                    <Columns>
                                        <asp:BoundField DataField="Name" HeaderText="ChildName" />
                                        <asp:BoundField DataField="Gender" HeaderText="Gender" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="DOB" HeaderText="DateofBirth" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                                        <asp:BoundField DataField="AGE" HeaderText="Age" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="SchoolGoing" HeaderText="School Going" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="Working" HeaderText="Working" ItemStyle-HorizontalAlign="Center" />
                                        <asp:ButtonField ButtonType="Image" CommandName="cmd_delete" HeaderText="Delete"
                                            ImageUrl="~/images/Delete.jpg" ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center" />
                                        </asp:ButtonField>
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
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                    </table>
                    <asp:Panel ID="pAddChild" runat="server" Style="display: none" CssClass="modalPopup">
                        <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD; border: solid 1px Gray; color: Black">
                            <div>
                                <p>
                                    Enter the Child Details:
                                </p>
                            </div>
                        </asp:Panel>
                        <table>
                            <tr>
                                <td>Name of Child
                                </td>
                                <td>
                                    <asp:TextBox ID="txtNameChild" runat="server"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>Gender
                                </td>
                                <td>
                                    <asp:RadioButtonList ID="rbtnGender" runat="server">
                                        <Items>
                                            <asp:ListItem Text="Male" Value="M"></asp:ListItem>
                                            <asp:ListItem Text="Female" Value="F"></asp:ListItem>
                                        </Items>
                                    </asp:RadioButtonList>
                                </td>
                            </tr>
                            <tr>
                                <td>Date of Birth
                                </td>
                                <td>
                                    <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox><%--onchange="CalAge(this.id)" OnTextChanged="txtDOB_TextChanged"--%>
                                    <asp:CalendarExtender ID="dtpDOB" runat="server" TargetControlID="txtDOB" OnClientDateSelectionChanged="DataSelectChanged">
                                    </asp:CalendarExtender>
                                </td>
                            </tr>
                            <tr>
                                <td>Age :
                                </td>
                                <td>
                                    <asp:Label ID="lblAge" runat="server" Text="0"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>School Going
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbSchool" runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td>Working
                                </td>
                                <td>
                                    <asp:CheckBox ID="cbWorking" runat="server" />
                                </td>
                            </tr>
                        </table>
                        <p style="text-align: center;">
                            <asp:Button ID="OkButton" runat="server" Text="OK" OnClick="OkButton_Click" />
                            <%--"--%>
                            <asp:Button ID="CancelButton" runat="server" Text="Cancel" />
                        </p>
                    </asp:Panel>
                    <table width="885px">
                        <tr>
                            <td align="right">
                                <asp:Button ID="btnFamilyNext" CssClass="fb8" runat="server" Text="Submit"
                                    OnClick="btnFamilyNext_Click" />
                            </td>
                        </tr>
                    </table>

                </div>
            </div>
            <!------------------------------------------Farmer Family information end---------------------------------------------------------------------------->

            <!------------------------------------------Farmer Animal information start---------------------------------------------------------------------------->
            <div id="divAnimalinfoview" runat="server">
                <table align="center" width="100%" border="1">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Animal Husbandry&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:LinkButton
                        ID="lbtnAnimalEdit" runat="server" Text="Edit" OnClick="lbtnAnimalEdit_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Buffalos</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblBuffalos" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Cows</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblCows" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Ox</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblOx" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Sheep</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblSheep" runat="server" /></td>
                    </tr>
                </table>
            </div>
            <!--edit Animal info-->
            <div id="divCattleDetails" runat="server" visible="false">
                <div align="center">
                    <table width="100%">
                        <tr style="border: 1px solid #ffffff; font-size: 18px; color: #ffffff; background-color: #FDB700;">
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                Animal Husbandry&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnAnimalClose" runat="server" Text="Close"
                                OnClick="lbtnAnimalClose_Click" />
                            </td>
                        </tr>
                    </table>

                    <%--onchange="CalAge(this.id)" OnTextChanged="txtDOB_TextChanged"--%>
                    <table align="center">
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td rowspan="8" style="width: 20%;"></td>
                            <td align="right">Buffalo&nbsp;
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBuffalo" runat="server" Font-Size="Medium"
                                Height="30px" Style="margin-bottom: 1px" Width="340px">0</asp:TextBox></td>
                            <td rowspan="8" style="width: 20%;" align="left"></td>
                        </tr>
                        <tr>
                            <td align="right">Cows&nbsp;
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCows" runat="server" Font-Size="Medium" Height="30px"
                                Style="margin-bottom: 1px" Width="340px">0</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Ox&nbsp;
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOx" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium">0</asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Sheep&nbsp;
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSheep" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium">0</asp:TextBox></td>
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
                    <table width="885px">
                        <tr>
                            <td align="left">&nbsp;</td>
                            <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                            <td align="right">
                                <asp:Button ID="btbCattleNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btbCattleNext_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!------------------------------------------Farmer Animal information end---------------------------------------------------------------------------->

            <!------------------------------------------Farmer Office information start---------------------------------------------------------------------------->
            <div id="divOfficeinfoview" runat="server">
                <table align="center" width="100%" border="1">
                    <tr>
                        <td colspan="2" bgcolor="#ffcc66">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Office Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    &nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnOfficeEdit" runat="server" Text="Edit"
                                OnClick="lbtnOfficeEdit_Click" />

                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Comments from Branch</td>
                        <td width="50%">&nbsp;<asp:Label ID="lblComments" runat="server" /></td>
                    </tr>
                    <tr>
                        <td align="center" width="50%">Uploaded by </td>
                        <td width="50%">&nbsp;<asp:Label ID="lblUploadedBy" runat="server" /></td>
                    </tr>
                </table>
            </div>
            <!--edit office info-->
            <div id="divForOfficeUse" runat="server" visible="false">
                <div align="center">
                    <table width="100%">
                        <tr style="border: 1px solid #ffffff; font-size: 18px; color: #ffffff; background-color: #FDB700;">
                            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
                                Office Information&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:LinkButton ID="lbtnOfficeclose" runat="server" Text="Close"
                                OnClick="lbtnOfficeclose_Click" />
                            </td>
                        </tr>
                    </table>
                    <table align="center">
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td align="right">Comments by Internal Inspector&nbsp;
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCommentInternalInspector" runat="server" Font-Size="Medium"
                                Height="50px" Style="margin-bottom: 1px" Width="340px" TextMode="MultiLine" TabIndex="3"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="right">Uploaded by Internal Inspector
                            </td>
                            <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtuploadedInternalInspector" runat="server" Height="30px"
                                Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox></td>
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
                    <table width="885px">
                        <tr>

                            <td align="left"></td>
                            <td align="right">
                                <asp:Button ID="officesubmit" runat="server" Text="Submit" CssClass="fb8" Visible="true"
                                    OnClick="officesubmit_Click" />
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <!------------------------------------------Farmer Office information end---------------------------------------------------------------------------->

        </div>

        <div align="center">
            <asp:Button ID="btnPdf" runat="server" CssClass="fb8" Text="PDF"
                OnClick="btnPdf_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
         <asp:Button ID="btnBack" runat="server" CssClass="fb8" Text="Back" PostBackUrl="~/Farmer/Sample Farmer.aspx"
             OnClick="btnBack_Click" />
        </div>
        <div>
        </div>
        <div>
        </div>
    </div>


</asp:Content>

