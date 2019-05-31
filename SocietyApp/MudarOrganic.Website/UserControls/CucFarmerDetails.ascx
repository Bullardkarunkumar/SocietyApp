<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CucFarmerDetails.ascx.cs"
    Inherits="UserControls_UcFarmerDetails" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>


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
<script type="text/javascript">
//    $(document).ready(function() {

//        $("#tabs").tabs();

//    });
</script>
<div>
    <table>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnTFarmerInfo" runat="server" Text="Farmer Details" CssClass="btnFarmer"
                    OnClick="btnTFarmerInfo_Click" />
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnTBankInfo" runat="server" Text="Bank Info" CssClass="btnFarmer"
                    OnClick="btnTBankInfo_Click" />
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnTaddPlots" runat="server" Text="Add Plots" CssClass="btnFarmer" 
                    onclick="btnTaddPlots_Click" />
            </td>
            <td colspan="2" align="left">
                <asp:Button ID="btnTSeasonInfo" runat="server" Text="Standard Season Info" 
                    CssClass="btnFarmer" onclick="btnTSeasonInfo_Click" />
            </td>
        </tr>
        <tr>
            <td colspan="2" align="center">
                <asp:Button ID="btnTStandardRisk" runat="server" Text="Standard Risk Field" 
                    CssClass="btnFarmer" onclick="btnTStandardRisk_Click" 
                     />
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnTFamily" runat="server" Text="Family Info" CssClass="btnFarmer" onclick="btnTFamily_Click" 
                     />
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnTCattle" runat="server" Text="Cattle Info" CssClass="btnFarmer" onclick="btnTCattle_Click" 
                     />
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnTOffice" runat="server" Text="Office Use" CssClass="btnFarmer" onclick="btnTOffice_Click" 
                     />
            </td>
        </tr>
    </table>
</div>
    <%--<div id="tabs">
    <%--<ul>
        <li><a href="#divFarmerDetails">FarmerDetails</a></li>
        <li><a href="#divBankDetails">BankDetails</a></li>
        <li><a href="#divFarmDetails">FarmDetails</a></li>
        <li><a href="#divSeasonDetails">SeasonDetails</a></li>
        <li><a href="#divFamilyDetails">FamilyDetails</a></li>
        <li><a href="#divCattleDetails">CattleDetails</a></li>
        <li><a href="#divForOfficeUse">ForOfficeUse</a></li>
        <li><a href="#divFieldRisk">FieldRisk</a></li>
        <li><a href="#divComplted">Complted</a></li>
        <li><a href="#divNewFarmDetails">NewFarmDetails</a></li>
    </ul>--%>
    <%--   <div id="tabs-1">
        <p>
            tab1 content</p>
    </div>--%>
    <div id="divFarmerDetails" runat="server" visible="true">
        <%--<div>
            <h3 id="h1">
                Farmer Information</h3>
        </div>--%>
        <asp:HiddenField ID="HiddenField1" runat="server" />
        <asp:HiddenField ID="hfUserPhotoPath" runat="server" />
        <asp:Label ID="lblfarmerUid" runat="server" Visible="false" Text="" />
        <table align="center">
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="10" style="width: 12%;">
                </td>
                <td align="right">
                    Farmer Name
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFarmerName" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="1"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFarmername"
                            runat="server" ControlToValidate="txtFarmername" ValidationGroup="FarmerDetails"
                            ErrorMessage="Farmer Name," Display="static" Text="*"></asp:RequiredFieldValidator></td>
                <td rowspan="10" style="width: 20%;" align="left">
                    <asp:UpdatePanel ID="upFarmerPhoto" runat="server" ChildrenAsTriggers="true" UpdateMode="Conditional">
                        <ContentTemplate>
                            <asp:Image ID="imgFarmerP" runat="server" Height="140" Width="140" />
                            <br />
                            <asp:AsyncFileUpload ID="afuFarmerPhoto" runat="server" CompleteBackColor="Lime"
                                ErrorBackColor="Red" OnClientUploadComplete="UploadComplete" OnClientUploadError="uploadError"
                                OnClientUploadStarted="StartUpload" OnUploadedComplete="afuFarmerPhoto_UploadedComplete"
                                ThrobberID="Throbber" UploaderStyle="Modern" UploadingBackColor="#66CCFF"/>
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
                <td align="right">
                    Father Name
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFatherName" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="2"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Address
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="3"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Village
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCity" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="4"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvCity"
                            runat="server" ControlToValidate="txtCity" ValidationGroup="FarmerDetails"
                            ErrorMessage="Village," Display="static" Text="*"></asp:RequiredFieldValidator>
                        </td>
            </tr>
            <tr>
                <td align="right">
                    Taluk
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTaluk" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="5"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    District
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDistrict" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="6"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    State
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="7"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvState"
                            runat="server" ControlToValidate="txtState" ValidationGroup="FarmerDetails"
                            ErrorMessage="State" Display="static" Text="*"></asp:RequiredFieldValidator>
                        </td>
            </tr>
            <tr>
                <td align="right">
                    Country
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCountry" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="8"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Phone
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhone" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="9"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    MPhone
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMPhone" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" TabIndex="10"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:ValidationSummary runat="server" ID="vsFarmerName" ValidationGroup="FarmerDetails"
                        ShowSummary="true" DisplayMode="SingleParagraph" HeaderText="Error Messsage* :Enter the " />
                       
                </td>
            </tr>
        </table>
        <table width="885">
            <tr>
                <td>
                </td>
                <td align="right">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnFarmerDetailsSubmit" runat="server" Text="Submit" CssClass="fb8"
                        OnClick="btnFarmerDetailsSubmit_Click" Visible="False" />
                </td>
                <td align="right">
                    <asp:Button ID="btnFarmerNext" CssClass="fb8" runat="server" ValidationGroup="FarmerDetails"
                        Text="Submit" OnClick="btnFarmerNext_Click" TabIndex="11"/>
                </td>
            </tr>
        </table>
    </div>
    <div id="divBankDetails" runat="server" visible="false">
        <%--<div>
            <h3 id="h2">
                Farmer Bank Information</h3>
        </div>--%>
        <table align="center">
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="10" style="width: 20%;">
                </td>
                <td align="right">
                    Farmer Code:
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblFarmerCode" runat="server" Text=""></asp:Label></td>
                <td rowspan="10" style="width: 20%;" align="left">
                </td>
            </tr>
            <tr>
                <td align="right">
                    Farmer Reg Number
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFarmerRegNo" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Bank Account
                </td>
                <td align="left">
                    &nbsp;&nbsp;
                    <asp:CheckBox ID="cbBank" runat="server" onclick="checkstatus(this.checked);" />
                    &nbsp;&nbsp;&nbsp;Yes or No
                </td>
            </tr>
            <tr>
                <td align="right">
                    Bank Account No
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAccountNo" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Account Holder Name
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAcccountHolder" runat="server" Height="30px"
                        Style="margin-bottom: 1px" Width="340px" Font-Size="Medium"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Bank Name
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBankName" runat="server" Height="30px" Style="margin-bottom: 1px"
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
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
        </table>
        <table width="885">
            <tr>
                <td align="left">
                    <asp:Button ID="btnBankPrev" CssClass="fb8" runat="server" Text="PREV" OnClick="btnBankPrev_Click" Visible="false" />
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBankSubmit"
                        runat="server" Text="Submit" CssClass="fb8" OnClick="btnBankSubmit_Click" Visible="False" />
                </td>
                <td align="right">
                    <asp:Button ID="btnBankNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnBankNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divNewFarmDetails" runat="server" visible="false">
        <%--<div>
            <h3 id="h10">
                Farm List</h3>
        </div>--%>
        <div align="center">
            <table>
                <tr>
                    <td align="right">
                        Last Date Of Chemical Application
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtLastDate" runat="server" Height="30px" Style="margin-bottom: 1px"
                                   Width="180px" Font-Size="Medium"></asp:TextBox>
                        <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtLastDate">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Organic
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="rbOrganic" runat="server" GroupName="organic" Checked="true" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Organic & Fair Trade
                    </td>
                    <td align="left">
                        <asp:RadioButton ID="rbOrganicFairTrad" runat="server" GroupName="organic" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Total Area :
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotalArea" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Total Plots :
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblTotalPlots" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Click ADD button to Add new Plot :
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
                                <asp:ButtonField ButtonType="Button" Text="Remove" CommandName="Remove" ControlStyle-CssClass="fb8"/>
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
            <%--onchange="CalAge(this.id)" OnTextChanged="txtDOB_TextChanged"--%>
            <table width="885px">
                <tr>
                    <td align="left">
                       
                    </td>
                    <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnNFarmNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnNFarmNextt_Click" Visible="false" />
                     <asp:Button ID="btnDisableSubmit" runat="server" Text="Submit" CssClass="fb8_disable" Visible="false" Enabled="false"/>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnNFarmBack" CssClass="fb8" runat="server" Text="Next" OnClick="btnNFarmNextBack_Click" Visible="false" />
                        <asp:Button ID="btnDisable" runat="server" Text="Next" CssClass="fb8_disable" Visible="false" Enabled="false"/>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div id="divSeasonDetails" runat="server" visible="false">
        <%--<div>
            <h3 id="h4">
                Farmer Season Information</h3>
        </div>--%>
        <div align="center">
            <div>
                Season Year :
                <asp:DropDownList ID="ddlSeasonYear" runat="server" Height="35px" Width="345px" Font-Size="Medium"/>
                
            </div>

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
                <AlternatingRowStyle BackColor="White" HorizontalAlign="Center" VerticalAlign="Middle"  />
            </asp:GridView>

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
        </div>
        <table width="885px">
            <tr>
                <td align="left">
                    <asp:Button ID="btnPrev" CssClass="fb8" runat="server" Text="PREV" OnClick="btnPrev_Click" Visible="false" />
                </td>
                <td>
                </td>
                <td align="right">
                    <asp:Button ID="btnNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFamilyDetails" runat="server" visible="false">
        <%--<div>
            <h3 id="h5">
                Farmer Family Information</h3>
        </div>--%>
        <table>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td rowspan="5" style="width: 15%;">
                </td>
                <td align="right">
                    Earning Members
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtEarningMember" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px" Text="0"></asp:TextBox></td>
                <td rowspan="5" style="width: 15%;">
                </td>
            </tr>
            <tr>
                <td align="right">
                    Dependent Elders
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDependentElder" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px" Text="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    &nbsp; Dependent Childs :
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:Label ID="lblChildCount" runat="server" Text="0"></asp:Label></td>
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
                    <asp:GridView ID="gvFamilyDet" runat="server" AutoGenerateColumns="False" DataKeyNames="FFamilyDetailsId"
                        OnRowCommand="gvFamilyDet_RowCommand" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="ChildName" />
                            <asp:BoundField DataField="Gender" HeaderText="Gender" ItemStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="DOB" HeaderText="DateofBirth" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}"/>
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
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
            <tr>
                <td colspan="4">
                </td>
            </tr>
        </table>
        <asp:Panel ID="pAddChild" runat="server" Style="display: none" CssClass="modalPopup">
            <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD;
                border: solid 1px Gray; color: Black">
                <div>
                    <p>
                        Enter the Child Details:</p>
                </div>
            </asp:Panel>
            <table>
                <tr>
                    <td>
                        Name of Child
                    </td>
                    <td>
                        <asp:TextBox ID="txtNameChild" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Gender
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
                    <td>
                        Date of Birth
                    </td>
                    <td>
                        <asp:TextBox ID="txtDOB" runat="server"></asp:TextBox><%--onchange="CalAge(this.id)" OnTextChanged="txtDOB_TextChanged"--%>
                        <asp:CalendarExtender ID="dtpDOB" runat="server" TargetControlID="txtDOB" OnClientDateSelectionChanged="DataSelectChanged">
                        </asp:CalendarExtender>
                    </td>
                </tr>
                <tr>
                    <td>
                        Age :
                    </td>
                    <td>
                        <asp:Label ID="lblAge" runat="server" Text="0"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        School Going
                    </td>
                    <td>
                        <asp:CheckBox ID="cbSchool" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Working
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
                <td align="left">
                    <asp:Button ID="btnFamilyPrev" CssClass="fb8" runat="server" Text="PREV" OnClick="btnFamilyPrev_Click" Visible="false" />
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnFamilySubmit"
                        runat="server" Text="Submit" CssClass="fb8" OnClick="btnFamilySubmit_Click" Visible="False" />
                </td>
                <td align="right">
                    <asp:Button ID="btnFamilyNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnFamilyNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divCattleDetails" runat="server" visible="false">
        <%--<div>
            <h3 id="h6">
                Farmer Cattle Information</h3>
        </div>--%>
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
                    Buffalo&nbsp;
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBuffalo" runat="server" Font-Size="Medium"
                        Height="30px" Style="margin-bottom: 1px" Width="340px" Text="0"></asp:TextBox></td>
                <td rowspan="8" style="width: 20%;" align="left">
                </td>
            </tr>
            <tr>
                <td align="right">
                    Cows&nbsp;
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCows" runat="server" Font-Size="Medium" Height="30px"
                        Style="margin-bottom: 1px" Width="340px" Text="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Ox&nbsp;
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOx" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" Text="0"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Sheep&nbsp;
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSheep" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="340px" Font-Size="Medium" Text="0"></asp:TextBox></td>
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
        <table width="885px">
            <tr>
                <td align="left">
                    <asp:Button ID="btnCattlePrev" CssClass="fb8" runat="server" Text="PREV" OnClick="btnCattlePrev_Click" Visible="false" />
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btbCattleSubmit"
                        runat="server" Text="Submit" CssClass="fb8" OnClick="btbCattleSubmit_Click" Visible="False" />
                </td>
                <td align="right">
                    <asp:Button ID="btbCattleNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btbCattleNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divForOfficeUse" runat="server" visible="false">
        <%--<div>
            <h3 id="h7">
                For Office Use Only
            </h3>
        </div>--%>
        <table align="center">
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td align="right">
                    Comments by Internal Inspector&nbsp;
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCommentInternalInspector" runat="server" Font-Size="Medium"
                        Height="50px" Style="margin-bottom: 1px" Width="340px" TextMode="MultiLine" TabIndex="1"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right">
                    Uploaded by Internal Inspector
                </td>
                <td align="left">
                    &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtuploadedInternalInspector" runat="server" Height="30px"
                        Style="margin-bottom: 1px" Width="340px" Font-Size="Medium" TabIndex="2"></asp:TextBox></td>
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
        <table width="885px">
            <tr>
                <td align="left">
                    <asp:Button ID="btnOfficePrev" CssClass="fb8" runat="server" Text="PREV" OnClick="btnOfficePrev_Click" Visible="false" />
                </td>
                <td align="left">
                </td>
                <td align="right">
                    <asp:Button ID="officesubmit" runat="server" Text="Submit" CssClass="fb8" Visible="true"
                        OnClick="officesubmit_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divFieldRisk" runat="server" visible="false">
        <%--<div>
            <h3 id="h8">
                Field Risk Assessment</h3>
        </div>--%>
        <div align="center">
            <asp:DataList ID="dlFieldRisk" runat="server" BackColor="White" BorderColor="#DEDFDE"
                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
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
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            </asp:DataList>
        </div>
        <table width="885px">
            <tr>
                <td align="left">
                    <asp:Button ID="btnFieldRiskBack" CssClass="fb8" runat="server" Text="PREV" OnClick="btnFieldRiskBack_Click" Visible="false" />
                </td>
                <td>
                </td>
                <td align="right">
                    <asp:Button ID="btnFieldRiskNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnFieldRiskNext_Click" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divComplted" runat="server" visible="false">
        <div>
            <h3 id="h9">
                Farmer Registration Complete</h3>
        </div>
        <div align="center">
            FARMER REGISTRATION FORWARDED
            <br />
            FOR APPROVAL SUCCESSFULLY.
            <table width="885px" align="center">
                <tr>
                    <td>
                        <asp:Button ID="btncompleted" CssClass="fb8" runat="server" Text="Farmer List" PostBackUrl="~/Farmer/Farmers.aspx" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <asp:MultiView ID="MainFarmerView" runat="server" ActiveViewIndex="1" Visible="false">
        <asp:View ID="FarmerDetails" runat="server">
            <%--<div id="divFarmerDetails">
                <div>
                    <h3 id="header_Text">
                        Farmer Information</h3>
                </div>
                <asp:HiddenField ID="HiddenField1" runat="server" />
                <asp:HiddenField ID="hfUserPhotoPath" runat="server" />
                <asp:Label ID="lblfarmerUid" runat="server" Visible="false" Text="" />
                <table align="center">
                    <tr>
                        <td colspan="4">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td rowspan="10" style="width: 12%;">
                        </td>
                        <td align="right">
                            Farmer Name
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFarmerName" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox><asp:RequiredFieldValidator ID="rfvFarmername"
                                    runat="server" ControlToValidate="txtFarmername" ValidationGroup="FarmerName"
                                    ErrorMessage="Enter the Farmer Name" Display="static" Text="*"></asp:RequiredFieldValidator>
                        </td>
                        <td rowspan="10" style="width: 20%;" align="left">
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
                        <td align="right">
                            Father Name
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFatherName" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Address
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAddress" runat="server" Height="30px" Style="margin-bottom: 1px"
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
                            District
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtDistrict" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Taluk
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtTaluk" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            State
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtState" runat="server" Height="30px" Style="margin-bottom: 1px"
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
                    <tr>
                        <td align="right">
                            Phone
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtPhone" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            MPhone
                        </td>
                        <td align="left">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtMPhone" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            &nbsp;
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
                        <td>
                        </td>
                        <td align="right">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnFarmerDetailsSubmit" runat="server" Text="Submit" CssClass="fb8"
                                OnClick="btnFarmerDetailsSubmit_Click" Visible="False" />
                        </td>
                        <td align="right">
                            <asp:Button ID="btnFarmerNext" CssClass="fb8" runat="server" ValidationGroup="FarmerName"
                                Text="Next" OnClick="btnFarmerNext_Click" />
                        </td>
                    </tr>
                </table>
            </div>--%>
        </asp:View>
        <%--        <asp:View ID="BankDetails" runat="server">
           
        </asp:View>
--%>
        <asp:View ID="FarmDetails" runat="server">
            <%--<div>
                <h3 id="header_Text">
                    Farmer Farm Information</h3>
            </div>--%>
            <table width="100%">
                <tr>
                    <td>
                    </td>
                    <caption>
                        &nbsp;</caption>
                </tr>
                <tr>
                    <td>
                        <asp:DataList ID="dlFarmdetails" runat="server" GridLines="Vertical" UseAccessibleHeader="True"
                            OnItemCommand="dlFarmdetails_ItemCommand" DataKeyField="FarmID" OnItemDataBound="dlFarmdetails_ItemDataBound"
                            OnSelectedIndexChanged="dlFarmdetails_SelectedIndexChanged" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            Width="885px">
                            <FooterStyle BackColor="#CCCC99" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#F7F7DE" />
                            <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderTemplate>
                                <table width="100%" align="center">
                                    <tr>
                                        <%--<td>
                                    FarmerID
                                </td>
                                <td>
                                    FarmID
                                </td>--%>
                                        <td style="width: 70px;" align="center" visible="false">
                                            &nbsp;
                                        </td>
                                        <td style="width: 100px;" align="left">
                                            Plot Name
                                        </td>
                                        <td style="width: 120px;" align="left">
                                            &nbsp;&nbsp;Plot Size
                                        </td>
                                        <td style="width: 120px;" align="left">
                                            &nbsp;&nbsp;Latitude
                                        </td>
                                        <td style="width: 120px;" align="left">
                                            &nbsp;&nbsp;Longitude
                                        </td>
                                        <td style="width: 140px;" align="left">
                                            &nbsp;&nbsp;Farmer Reg No
                                        </td>
                                        <%-- <td style="width: 90px;" align="left" visible="false">
                                    MIE Code
                                </td>
                                <td style="width: 90px;" align="left" visible="false">
                                    Tracenet Code
                                </td>--%>
                                        <td>
                                            <asp:Button ID="btnAddFarm" runat="server" CssClass="fb9_addplot" CommandName="addfarm"
                                                Text="Add Plot" />
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFarmerUid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FarmerID")%>'
                                                Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblFarmId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FarmID")%>'
                                                Visible="false" />
                                        </td>
                                        <td style="width: 70px;" align="left">
                                            &nbsp;<asp:ImageButton ID="imgbtnExpand_Collapse" runat="server" CommandName="Exp_Col"
                                                ImageUrl='<%# DataBinder.Eval(Container.DataItem, "Exp_Col")%>' />
                                        </td>
                                        <td style="width: 100px;" align="left">
                                            <asp:Label ID="lblPlot" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Plot")%>' />
                                        </td>
                                        <td style="width: 120px;" align="left">
                                            <asp:TextBox ID="txtPlotArea" runat="server" EnableViewState="true" Text='<%# DataBinder.Eval(Container.DataItem, "PlotArea")%>'
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td style="width: 120px;" align="left">
                                            <asp:TextBox ID="txtLatitude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Latitude")%>'
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td style="width: 110px;" align="left">
                                            <asp:TextBox ID="txtLongitude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Longitude")%>'
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td style="width: 140px;" align="left">
                                            &nbsp;<asp:Label ID="lblAreaCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AreaCode")%>' />
                                        </td>
                                        <td align="right">
                                            <asp:TextBox ID="txtIMECode" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "IMECode")%>'
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td align="center">
                                            <asp:TextBox ID="txtTrancenet" runat="server" Visible="false" Text='<%# DataBinder.Eval(Container.DataItem, "TracenetCode")%>'
                                                Width="80px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblC_P" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "C_P")%>'
                                                Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Label ID="lblChildCount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ChildCount")%>'
                                                Visible="false" />
                                        </td>
                                        <td>
                                            <asp:Button ID="btnRemoveFarm" runat="server" CssClass="fb9_addplot" CommandName="delete"
                                                Text="Remove" />
                                        </td>
                                        <td>
                                            &nbsp;
                                            <asp:Button ID="btnAddChild" runat="server" CssClass="fb9_addplot" CommandName="addchildfarm"
                                                Text="Add Crop" Visible="false" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
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
            </table>
            <table width="885px">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnFarmPrev" CssClass="fb8" runat="server" Text="PREV" OnClick="btnFarmPrev_Click" Visible="false" />
                    </td>
                    <td align="center">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnFarmSubmit"
                            runat="server" Text="Submit" CssClass="fb8" OnClick="btnFarmSubmit_Click" Visible="False" />
                    </td>
                    <td align="right">
                        <asp:Button ID="btnFarmNext" CssClass="fb8" runat="server" Text="Submit" OnClick="btnFarmNext_Click" />
                    </td>
                </tr>
            </table>
        </asp:View>
        <%--      <asp:View ID="SeasonDetails" runat="server">
        </asp:View>
        <asp:View ID="FamilyDetails" runat="server">
        </asp:View>
        <asp:View ID="CattleDetails" runat="server">
        </asp:View>
        <asp:View ID="ForOfficeUse" runat="server">
        
        </asp:View>
        <asp:View ID="FieldRisk" runat="server">
        </asp:View>
        <asp:View ID="Completed" runat="server">
        </asp:View>--%>
        <asp:View ID="NewFarmDetails" runat="server">
        </asp:View>
    </asp:MultiView>

