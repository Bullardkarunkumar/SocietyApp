<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="FarmerProductionInfo.aspx.cs" Inherits="Farmer_FarmerProductionInfo"
    Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">

    <script type="text/javascript">
        function validate(key) {
            //getting key code of pressed key
            var keycode = (key.which) ? key.which : key.keyCode;
            var phn = document.getElementById('PlotArea');
            //comparing pressed keycodes
            if (!(keycode == 8 || keycode == 46) && (keycode < 48 || keycode > 57)) {
                return false;
            }
        }

        function clearLabelText() {
            document.getElementById('<%=this.lblImportErrorMsg.ClientID%>').innerHTML = '';
            return true;
        }
    </script>

    <script type="text/javascript">
        function txtlotNo(objRef, HfLotText, txtNoOfLots, lblLotText, cutplan) {
            var txtLotNoId = objRef.id;
            //            var lblLotText = 'lblLotNos';
            var lblLotId = txtLotNoId.replace(txtNoOfLots, lblLotText);
            var hfLotId = txtLotNoId.replace(txtNoOfLots, HfLotText);
            var lblPlantationId = txtLotNoId.replace(txtNoOfLots, "hfPlantationId");
            var PlantnId = document.getElementById(lblPlantationId).value;
            var date = new Date().format('MMddyyyy');
            var lableText = '';
            //alert(date +'-'+ objRef.value);
            for (var i = 1; i <= objRef.value; i++) {
                lableText = lableText + date + '-' + cutplan + i + '/' + PlantnId;
                if (i < objRef.value) {
                    lableText = lableText + ';\n';
                }
            }
            ////            //document.getElementById(lblLotId).innerText = lableText;
            if (typeof document.getElementById(lblLotId).textContent != 'undefined')
                document.getElementById(lblLotId).textContent = lableText;
            else
                document.getElementById(lblLotId).innerText = lableText;
            //            document.getElementById(lblLotId).innerText = lableText;
            document.getElementById(hfLotId).value = lableText;

        }
        function GenerateLot() {
        }
    </script>
    <asp:ScriptManagerProxy ID="ScriptManagerProxy1" runat="server"></asp:ScriptManagerProxy>
    <asp:Panel ID="pnlImportModel" runat="server" CssClass="modalPopup" style="display: none" Width="600">
        <table cellpadding="5" cellspacing="5" style="width:100%">
            <tr>
                <td colspan="3" align="right">
                    <asp:LinkButton ID="lnkImportPopClose" style="text-decoration:none" runat="server" Text="Close"></asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td align="right">Choose Excel file : </td>
                <td><asp:FileUpload ID="fileUpload1"  runat="server" />&nbsp;&nbsp;</td>
                <td style="width:20px"></td>
            </tr>
            <tr>
                <td>

                </td>
                <td colspan="2">
                    <asp:Label ID="lblImportErrorMsg" runat="server" ForeColor="Red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td></td>
                <td><asp:Button ID="btnImportSubmit" OnClick="btnImportSubmit_Click" Text="Submit" runat="server" CssClass="fb8" />&nbsp;&nbsp;&nbsp;<asp:Button ID="btnImportClear" OnClick="btnImportClear_Click" Text="Clear" runat="server" CssClass="fb8" /></td>
                <td></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ModalPopupExtender ID="popupExtender" runat="server" DropShadow="false" TargetControlID="lnkImportFarmerProdInfo" PopupControlID="pnlImportModel" PopupDragHandleControlID="pnlImportModel"
        CancelControlID="lnkImportPopClose" BackgroundCssClass="modalBackground"></asp:ModalPopupExtender>
    <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
            Farmer Production Info
        </div>
        <div>
            <asp:HiddenField ID="hfFarmerID" runat="server" />
            <asp:HiddenField ID="hfFarmerCode" runat="server" />
        </div>
        <%-- <div id="divSelect" runat="server"><table align="center"><tr><td><asp:Button ID="btnCrops" 
                runat="server" Text="Crops" CssClass="btnFarmer" onclick="btnCrops_Click"/></td>
        <td><asp:Button ID="btnProduction" runat="server" Text="Production" 
                CssClass="btnFarmer" onclick="btnProduction_Click"/></td></tr></table></div>--%>
        <div id="divGetDetails" runat="server">
            <table>
                <tr>
                    <td rowspan="4" style="width: 35%;"></td>
                    <td align="right">Year
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true"
                        OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Font-Size="Medium" Height="35px"
                        TabIndex="9" Width="345px">
                    </asp:DropDownList>
                    </td>
                    <td rowspan="4" style="width: 5%;"></td>
                </tr>
                <tr>
                    <td align="right">Season
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlSeason" runat="server" AutoPostBack="true"
                        Font-Size="Medium" Height="35px" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged"
                        TabIndex="9" Width="345px">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Product
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true"
                        Font-Size="Medium" Height="35px" TabIndex="9" Width="345px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Village
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true"
                        Font-Size="Medium" Height="35px" TabIndex="9" Width="345px" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged"
                        Enabled="false">
                    </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <%--BindFarmerPlots Gobuttons--%>
                        &nbsp;&nbsp;<asp:Button ID="btnFarmerGo" CssClass="fb8" runat="server" Text="GO"
                            OnClick="btnFarmerGo_Click" Visible="false"/>
                        <%--BindFarmerproduction Gobuttons--%>
                        <asp:Button ID="btnFPGo" CssClass="fb8" runat="server" Text="Go" OnClick="btnFPGo_Click"
                            Visible="false" />
                        <%--BindFarmerPlots Back buttons--%>
                        &nbsp;&nbsp;
                        <%--BindFarmerproduction Backbuttons--%>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divSeasonDetails" runat="server" align="center" visible="false">
            <table align="center">
                <tr>
                    <td align="center">Season Year<br />
                        <asp:Label ID="lblSyear" runat="server" ForeColor="#FF6600" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="center">Season Name<br />
                        <asp:Label ID="lblSname" runat="server" ForeColor="#FF6600" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="center">Product Name<br />
                        <asp:Label ID="lblProduct" runat="server" ForeColor="#FF6600" />
                    </td>
                    
                </tr>
                <tr id="trTotalplots" runat="server" visible="false">
                    <td align="center">Total Plot Area<br />
                        <asp:Label ID="lblTplotArea" runat="server" ForeColor="#FF6600" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="center">Total Crop Area<br />
                        <asp:Label ID="lblTcropArea" runat="server" ForeColor="#FF6600" />
                    </td>
                    <td>&nbsp;
                    </td>
                    <td align="center">Total Available Area<br />
                        <asp:Label ID="lblTAvaiArea" runat="server" ForeColor="#FF6600" />
                    </td>
                </tr>
                <tr>
                    <div id="divFindDetails" runat="server" visible="false">
                        <td align="center">Farmer Code<br />
                            <asp:Label ID="lblFarmerCode" runat="server" ForeColor="#FF6600" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td align="center">Farmer Name<br />
                            <asp:Label ID="lblFarmername" runat="server" ForeColor="#FF6600" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td align="center">Farmer Village<br />
                            <asp:Label ID="lblVillage" runat="server" ForeColor="#FF6600" />
                        </td>
                    </div>
                </tr>
                <tr>
                    <td colspan="5" align="center">
                        <asp:LinkButton ID="lnkImportFarmerProdInfo" runat="server" Text="Import Farmer Production Info" OnClientClick="return clearLabelText()"></asp:LinkButton>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divfarmerlist" runat="server" visible="false">
            <table align="center">
                <tr>
                    <td>
                        <asp:GridView ID="gvFarmerList" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerID,FarmerCode"
                            OnRowCommand="gvFarmerList_RowCommand" OnRowDataBound="gvFarmerList_RowDataBound"
                            PageSize="15" CssClass="grid-view" AllowSorting="True" OnSorting="gvFarmerList_Sorting"
                            EnableSortingAndPagingCallback="True">
                            <Columns>
                                <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" SortExpression="FarmerCode" />
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" SortExpression="FirstName" />
                                <asp:BoundField DataField="PlotCode" HeaderText="Plot Code" SortExpression="PlotCode" />
                                <asp:BoundField DataField="plotarea" HeaderText="Plot Area" SortExpression="plotarea"
                                    DataFormatString="{0:0.000}" />
                                <asp:BoundField DataField="croparea" HeaderText="Crop Area" SortExpression="croparea"
                                    DataFormatString="{0:0.000}" />
                                <asp:BoundField DataField="Availablearea" HeaderText="Available Area" SortExpression="Availablearea"
                                    DataFormatString="{0:0.000}" />
                                <asp:BoundField DataField="City_Village" HeaderText="Village" SortExpression="City_Village" />
                                <asp:BoundField DataField="Organic" HeaderText="Plot Status" SortExpression="Organic" />
                                <asp:ButtonField ButtonType="Link" Text="Add Crop" HeaderText="AddCrop" CommandName="AddCrop" />
                                <asp:ButtonField ButtonType="Link" Text="Production" HeaderText="Production" CommandName="Production" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divFarmerPlotDetails" visible="false" runat="server">
            <table>
                <tr>
                    <td colspan="2">
                        <asp:DataList ID="dlMainFP" runat="server" BackColor="White" BorderColor="#DEDFDE"
                            BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyField="FarmID" ForeColor="Black"
                            GridLines="Vertical" OnItemCommand="dlMainFP_ItemCommand" Width="100%" OnItemDataBound="dlMainFP_ItemDataBound">
                            <FooterStyle BackColor="#CCCC99" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#F7F7DE" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td align="center" style="width: 150px;">Farmer Code
                                        </td>
                                        <td align="center" style="width: 120px;">Plot Code
                                        </td>
                                        <td align="center" style="width: 120px;">Latitude
                                        </td>
                                        <td align="center" style="width: 120px;">Longitude
                                        </td>
                                        <td align="center" style="width: 120px;">Area in HC
                                        </td>
                                        <td align="center" style="width: 100px;">Avaliable Area in HC
                                        </td>
                                        <td align="center" style="width: 140px;">Add Crop
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td align="center" style="width: 150px;">
                                            <asp:Label ID="lblFarmerCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FarmerCode")%>' />
                                        </td>
                                        <td align="center" style="width: 120px;">
                                            <asp:Label ID="lblPlotCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AreaCode")%>' />
                                        </td>
                                        <td align="center" style="width: 120px;">
                                            <asp:Label ID="lbllatitude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Latitude")%>' />
                                        </td>
                                        <td align="center" style="width: 120px;">
                                            <asp:Label ID="lbllongitude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Longitude")%>' />
                                        </td>
                                        <td align="center" style="width: 120px;">
                                            <asp:Label ID="lblPlotArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlotArea")%>' />
                                        </td>
                                        <td align="center" style="width: 100px;">
                                            <asp:Label ID="lblAvaliableArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AvaliablePlotArea")%>' />
                                        </td>
                                        <td align="center" style="width: 140px;">
                                            <asp:Button ID="btnAddFarm" runat="server" CommandName="addcrop" CssClass="fb9_addplot"
                                                Text="Add" />
                                            <asp:Button ID="btnDisable" runat="server" CssClass="fb9_addplotdisable" Visible="false"
                                                Text="Add" />
                                            <asp:Button ID="btnEdit" runat="server" CommandName="Edit" CssClass="fb9_addplot"
                                                Text="Edit" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:GridView ID="gvCrops" runat="server" AutoGenerateColumns="false" BackColor="White"
                                                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" DataKeyNames="FarmId,PlantationId"
                                                ForeColor="Black" GridLines="Vertical">
                                                <RowStyle BackColor="#F7F7DE" />
                                                <FooterStyle BackColor="#CCCC99" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField DataField="AreaCode" HeaderText="Plot Code" />
                                                    <asp:TemplateField HeaderText="Area in HC">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPlotArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlotArea")%>'
                                                                onkeypress="return validate(event)" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Is Inter Crop">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbIsInterCrop" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "IsInterCrop")%>' />
                                                        </ItemTemplate>
                                                        <ItemStyle HorizontalAlign="Center" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
                <tr>
                    <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnFarmerSave" CssClass="fb8" runat="server" Text="Save" OnClick="btnFarmerSave_Click"
                            Visible="false" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                            ID="btnFarmerBack" CssClass="fb8" runat="server" Text="Back" OnClick="btnFarmerBack_Click"
                            Visible="false" />
                    </td>
                    <td></td>
                </tr>
            </table>
        </div>
        <div id="divFarmerPlantationDetails" runat="server" class="scroll_div" visible="false">
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvFarmer" runat="server" DataKeyNames="PlantationId" AutoGenerateColumns="False"
                            CssClass="grid-view">
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPlantationId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationId")%>'
                                            Visible="false" Width="60" />
                                        <asp:HiddenField ID="hfPlantationId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PlantationId")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FarmerId" HeaderText="FarmerId" Visible="false" />
                                <asp:BoundField DataField="ProductId" HeaderText="ProductId" Visible="false" />
                                <asp:BoundField DataField="FarmId" HeaderText="FarmId" Visible="false" />
                                <asp:BoundField DataField="SeasonId" HeaderText="SeasonID" Visible="false" />
                                <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                                <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Area(HC)" />
                                <asp:BoundField DataField="FarmerRegNumber" HeaderText="Farmer Reg Number" />
                                <asp:BoundField DataField="AreaCode" HeaderText="Area Code" />
                                <asp:BoundField DataField="TotalPloatArea" HeaderText="Plot Area(HC)" />
                                <asp:TemplateField HeaderText="Plantation Area(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlantationArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationArea")%>'
                                            Width="50" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Plantation Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlantationDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationDate")%>'
                                            Width="60" />
                                        <asp:CalendarExtender ID="calPlantationDate" runat="server" TargetControlID="txtPlantationDate">
                                        </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="I Cut Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFirstHarvestDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstHarvestDate")%>'
                                            Width="60" />
                                        <asp:CalendarExtender ID="calFirstHarvestDate" runat="server" TargetControlID="txtFirstHarvestDate">
                                        </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="I Herbage(MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFirstHerbaga" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstHerbaga")%>'
                                            Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="I Distillation Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFirstDistillationDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstDistillationDate")%>'
                                            Width="60" />
                                        <asp:CalendarExtender ID="calFirstDistillationDate" runat="server" TargetControlID="txtFirstDistillationDate">
                                        </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="I Distillation Unit NO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFirstDistillationUnitNO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstDistillationUnitNO")%>'
                                            Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFirstProductQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstProductQuantity")%>'
                                            Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlUnit" runat="server" OnLoad="DropDownList1_Load1">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NoOfLots" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtNoOfLots" runat="server" onChange="txtlotNo(this,'hfLotNos','txtNoOfLots','lblLotNos','C1')"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "FirstNoOfLots")%>' Width="60" />
                                        <%--ontextchanged="TextBox1_TextChanged"--%>
                                        <asp:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" runat="server" FilterType="Numbers"
                                            TargetControlID="txtNoOfLots" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LotNos">
                                    <ItemTemplate>
                                        <asp:Label ID="lblLotNos" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FirstLotNos")%>'></asp:Label>
                                        <asp:HiddenField ID="hfLotNos" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "FirstLotNos")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Completion">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txtFirstProductCompletion" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "FirstProductCompletion")%>'
                                            Width="60" />
                                        <%--<asp:ModalPopupExtender ID="ModalPopupExtender1" runat="server" TargetControlID="txtFirstProductCompletion"
                            PopupControlID="pAddChild_F" BackgroundCssClass="modalBackground"
                              PopupDragHandleControlID="pAddChild_F" />--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="II Cut Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSecondHarvestDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondHarvestDate")%>'
                                            Width="60" />
                                        <asp:CalendarExtender ID="calSecondHarvestDate" runat="server" TargetControlID="txtSecondHarvestDate">
                                        </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="II Herbage(MT)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSecondHerbaga" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondHerbaga")%>'
                                            Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="II Distillation Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSecondDistillationDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondDistillationDate")%>'
                                            Width="60" />
                                        <asp:CalendarExtender ID="calSecondDistillationDate" runat="server" TargetControlID="txtSecondDistillationDate">
                                        </asp:CalendarExtender>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="II Distillation Unit NO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSecondDistillationUnitNO" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondDistillationUnitNO")%>'
                                            Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSecondProductQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondProductQuantity")%>'
                                            Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Unit">
                                    <ItemTemplate>
                                        <asp:DropDownList ID="ddlSecUnit" runat="server" OnLoad="DropDownList1_Load1">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="NoOfLots" Visible="false">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSecNoOfLots" runat="server" onChange="txtlotNo(this,'hfSecLotNos','txtSecNoOfLots','lblSecLotNos','C2')"
                                            Text='<%# DataBinder.Eval(Container.DataItem, "SecondNoOfLots")%>' Width="60" />
                                        <%--ontextchanged="TextBox1_TextChanged"--%>
                                        <asp:FilteredTextBoxExtender ID="SecFilteredTextBoxExtender" runat="server" FilterType="Numbers"
                                            TargetControlID="txtSecNoOfLots" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="LotNos">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSecLotNos" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondLotNos")%>'></asp:Label>
                                        <asp:HiddenField ID="hfSecLotNos" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "SecondLotNos")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Product Completion">
                                    <ItemTemplate>
                                        <asp:CheckBox ID="txtSecondProductCompletion" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "SecondProductCompletion")%>'
                                            Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Total Product Quantity (KG)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotalProductQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalProductQuantity")%>'
                                            Enabled="false" Width="60" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divNewFarmerPlantation" runat="server" class="scroll_div">
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvNewFarmerPlantation" runat="server" AutoGenerateColumns="false"
                            DataKeyNames="PlantationID" CssClass="grid-view">
                            <Columns>
                                <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                                <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Farm area (HC)" />
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" />
                                <asp:BoundField DataField="PlotArea" HeaderText="Plot Area (HC)" />
                                <asp:BoundField DataField="PlantationArea" HeaderText="Plantation Area (HC)" />
                                <asp:BoundField DataField="PlantationDate" HeaderText="Plantation Date" DataFormatString="{0:dd MMM yyyy}" />
                                <asp:BoundField DataField="FirstHarvestDate" HeaderText="I Cutting Date" DataFormatString="{0:dd MMM yyyy}" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td colspan="2">I Herbage (MT)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="50%">Estimated
                                                </td>
                                                <td align="center" width="50%">Actual
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td align="center" width="50%">
                                                    <%# DataBinder.Eval(Container.DataItem, "EstimationFHerbaga")%>
                                                </td>
                                                <td align="center" width="50%">&nbsp;<%# DataBinder.Eval(Container.DataItem, "FirstHerbaga")%></td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FirstDistillationDate" HeaderText="I Distillation Date"
                                    DataFormatString="{0:dd MMM yyyy}" />
                                <asp:BoundField DataField="Ucode" HeaderText="Unit" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td colspan="2">I Yield (KG)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="50%">Estimated
                                                </td>
                                                <td align="center" width="50%">Actual
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="1">
                                            <tr align="center">
                                                <td align="center" width="50%">
                                                    <%# DataBinder.Eval(Container.DataItem, "EstimationFProductQuantity")%>
                                                </td>
                                                <td align="center" width="50%">
                                                    <%# DataBinder.Eval(Container.DataItem, "FirstProductQuantity")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="FirstLotNos" HeaderText="First Lot Number" Visible="false"/>
                                <asp:BoundField DataField="SecondHarvestDate" HeaderText="II Cutting Date" DataFormatString="{0:dd MMM yyyy}" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td colspan="2">II Herbage (MT)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="50%">Estimated
                                                </td>
                                                <td align="center" width="50%">Actual
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="1">
                                            <tr align="center">
                                                <td align="center" width="50%">
                                                    <%# DataBinder.Eval(Container.DataItem, "EstimationSHerbaga")%>
                                                </td>
                                                <td align="center" width="50%">
                                                    <%# DataBinder.Eval(Container.DataItem, "SecondHerbaga")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SecondDistillationDate" HeaderText="II Distillation Date"
                                    DataFormatString="{0:dd MMM yyyy}" />
                                <asp:BoundField DataField="Ucode" HeaderText="Unit" />
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td colspan="2">II Yield (KG)
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" width="50%">Estimated
                                                </td>
                                                <td align="center" width="50%">Actual
                                                </td>
                                            </tr>
                                        </table>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <table width="100%" border="1">
                                            <tr align="center">
                                                <td align="center" width="50%">
                                                    <%# DataBinder.Eval(Container.DataItem, "EstimationSProductQuantity")%>
                                                </td>
                                                <td align="center" width="50%">
                                                    <%# DataBinder.Eval(Container.DataItem, "SecondProductQuantity")%>
                                                </td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="SecondLotNos" HeaderText="Second Lot Number"  Visible="false" />
                                <asp:BoundField DataField="FarmerLotnumber" HeaderText="FarmerLotNumber" />
                                <asp:BoundField DataField="TotalProductOutput" HeaderText="Total Yield (KG)" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnFPSave" CssClass="fb8" runat="server" Text="Save" Visible="false"
                OnClick="btnFPSave_Click" />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button
                ID="btnFPBack" CssClass="fb8" runat="server" Text="Back" OnClick="btnFPBack_Click"
                Visible="false" />
        </div>
        <div>
            <asp:Panel ID="pAddChild_F" runat="server" Style="display: none" CssClass="modalPopup">
                <asp:Panel ID="Panel3" runat="server" Style="cursor: move; background-color: #DDDDDD; border: solid 1px Gray; color: Black">
                    <div>
                        <p>
                            Enter the Unit Details:
                        </p>
                    </div>
                </asp:Panel>
                <table>
                    <tr>
                        <td>Select Unit
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlUnitDetails" runat="server">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>No of Lots
                        </td>
                        <td>
                            <asp:TextBox ID="txtLots" runat="server"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <p style="text-align: center;">
                    <%--<asp:Button ID="OkButton_F" runat="server" Text="OK" OnClick="OkButton_F_Click" />
                <asp:Button ID="CancelButton_F" runat="server" Text="Cancel" />--%>
                </p>
            </asp:Panel>
        </div>
    </div>
</asp:Content>
