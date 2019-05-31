<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FarmerPlantation.aspx.cs" Inherits="Farmer_FarmerPlantation" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">   

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
    

    <div id="content_area_Home" style="height:auto;">
        <div>
            <div id="header_Text">
                Farmer Plantation
            </div>
            <asp:Menu ID="MainMenu" runat="server" Orientation="Horizontal" OnMenuItemClick="MainMenu_MenuItemClick"
                BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana" Font-Size="Small"
                ForeColor="#990000" Height="25px" StaticSubMenuIndent="10px">
                <StaticSelectedStyle BackColor="#FFCC66" />
                <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                <DynamicMenuStyle BackColor="#FFFBD6" />
                <DynamicSelectedStyle BackColor="#FFCC66" />
                <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                <Items>
                    <asp:MenuItem Text="Production Info" Value="0"></asp:MenuItem>
                    <asp:MenuItem Text="Add Crops" Value="1"></asp:MenuItem>
                </Items>
            </asp:Menu>
            <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="4">
                <asp:View ID="Farmer" runat="server">
                     <div>
                        <table align="center">
                            <tr>
                                <td rowspan="5" style="width: 20%;">
                                </td>
                                <td align="right">
                                    <%--Farmer--%>
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtFarmerSearch" runat="server" Visible="false"></asp:TextBox>
                                    <asp:AutoCompleteExtender ID="txtAutoFarmerName" runat="server" TargetControlID="txtFarmerSearch"
                                        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        ShowOnlyCurrentWordInCompletionListItem="true" BehaviorID="AutoCompleteEx" ServicePath="~/AutoComplete.asmx"
                                        ServiceMethod="GetCompletionList" MinimumPrefixLength="1">
                                        <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                                        </Animations>
                                    </asp:AutoCompleteExtender>
                                </td>
                                <td rowspan="5" style="width: 20%;" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Year
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFarmerYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFarmerYear_SelectedIndexChanged"
                                        Font-Size="Medium" Height="35px" 
                                        TabIndex="9" Width="345px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Season
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFarmerSeason" runat="server" AutoPostBack="true"
                                        Font-Size="Medium" Height="35px" OnSelectedIndexChanged="ddlFarmerSeason_SelectedIndexChanged"
                                        TabIndex="9" Width="345px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Product
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFarmerProduct" runat="server" AutoPostBack="true"
                                        Font-Size="Medium" Height="35px" OnSelectedIndexChanged="ddlFarmerProduct_SelectedIndexChanged"
                                        TabIndex="9" Width="345px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Cultivation
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlCultivation" runat="server" AutoPostBack="true"
                                        Font-Size="Medium" Height="35px"
                                        TabIndex="9" Width="345px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnFarmerGo" CssClass="fb8" runat="server" Text="Go" OnClick="btnFarmerGo_Click" />
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnFarmerSave" CssClass="fb8" runat="server" Text="Save" OnClick="btnFarmerSave_Click" />
                                    <asp:Button ID="btnFarmerBack" CssClass="fb8" runat="server" Text="Back" OnClick="btnFarmerBack_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                     <div class="scroll_div">
                            <asp:GridView ID="gvFarmer" runat="server" DataKeyNames="PlantationId" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <RowStyle BackColor="#F7F7DE" />
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
                                    <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Area In Hectares" />
                                    <asp:BoundField DataField="FarmerRegNumber" HeaderText="Farmer Reg Number" />
                                    <asp:BoundField DataField="AreaCode" HeaderText="Area Code" />
                                    <asp:BoundField DataField="PlotArea" HeaderText="Plot Area" />
                                    <asp:TemplateField HeaderText="Plantation Area">
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
                                    <asp:TemplateField HeaderText="I Herbaga">
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
                                    <asp:TemplateField HeaderText="NoOfLots">
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
                                    <asp:TemplateField HeaderText="II Herbaga">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtSecondHerbaga" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SecondHerbaga")%>'
                                                Width="60" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="I Distillation Date">
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
                                    <asp:TemplateField HeaderText="NoOfLots">
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
                                    <asp:TemplateField HeaderText="Total Product Quantity">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtTotalProductQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalProductQuantity")%>'
                                                Width="60" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                </asp:View>
                <asp:View ID="FarmerPlantation" runat="server">
                    <div>
                        <table align="center">
                            <tr>
                                <td colspan="4">
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td rowspan="4" style="width: 20%;">
                                </td>
                                <td align="right">
                                    <%--Farmer--%>
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;
                                    <asp:TextBox ID="txtFPFarmerN" runat="server" Font-Size="Medium" Height="30px"
                                        Style="margin-bottom: 1px" Width="340px"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtFPFarmerN"
                                        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                        ShowOnlyCurrentWordInCompletionListItem="true" BehaviorID="AutoCompleteEx" ServicePath="~/AutoComplete.asmx"
                                        ServiceMethod="GetCompletionList" MinimumPrefixLength="1">
                                        <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                                        </Animations>
                                    </asp:AutoCompleteExtender>
                                    &nbsp;
                                </td>
                                <td rowspan="4" style="width: 20%;" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Year
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFPYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlFPYear_SelectedIndexChanged"
                                        Font-Size="Medium" Height="35px" 
                                        TabIndex="9" Width="345px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Season
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFPSeason" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlFPSeason_SelectedIndexChanged"
                                        Font-Size="Medium" Height="35px" 
                                        TabIndex="9" Width="345px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    Product
                                </td>
                                <td align="left">
                                    &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlFPProduct" runat="server" OnSelectedIndexChanged="ddlFPProduct_SelectedIndexChanged"
                                        Font-Size="Medium" Height="35px"
                                        TabIndex="9" Width="345px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                 <td align="center" colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnFPGo" CssClass="fb8" runat="server" Text="GO" OnClick="btnFPGo_Click" />
                                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnFPSave" CssClass="fb8" runat="server" Text="Save" OnClick="btnFPSave_Click" />
                                    <asp:Button ID="btnFPBack" CssClass="fb8" runat="server" Text="Back" OnClick="btnFPBack_Click" />
                                </td>
                            </tr>
                        </table>
                        <div class="scroll_div">
                        <asp:DataList ID="dlMainFP" Width="100%" runat="server" DataKeyField="FarmID" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" OnItemCommand="dlMainFP_ItemCommand" >
                            <FooterStyle BackColor="#CCCC99" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#F7F7DE" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <HeaderTemplate>
                                <table>
                                    <tr>
                                        <td style="width:150px;" align="center">
                                            Farmer Code
                                        </td>
                                        <td style="width:140px;" align="center">
                                            Plot Code
                                        </td>
                                        <td style="width:150px;" align="center" >
                                            Latitude
                                        </td>
                                        <td style="width:150px;" align="center" >
                                            Longitude
                                        </td>
                                        <td style="width:150px;" align="center" >
                                            Area in HC
                                        </td>
                                        <td style="width:130px;" align="center" >
                                            Add Crop
                                        </td>
                                    </tr>
                                </table>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <table>
                                    <tr>
                                        <td style="width:150px;" align="center">
                                            <asp:Label ID="lblFarmerCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FarmerCode")%>' />
                                        </td>
                                        <td style="width:140px;" align="center">
                                            <asp:Label ID="lblPlotCode" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AreaCode")%>' />
                                        </td>
                                        <td style="width:150px;" align="center" >
                                            <asp:Label ID="lbllatitude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Latitude")%>' />
                                        </td>
                                        <td style="width:150px;" align="center" >
                                            <asp:Label ID="lbllongitude" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Longitude")%>' />
                                        </td>
                                        <td style="width:150px;" align="center" >
                                            <asp:Label ID="lblPlotArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlotArea")%>' />
                                        </td>
                                        <td style="width:130px;" align="center" >
                                            <asp:Button ID="btnAddFarm" runat="server" CssClass="fb9_addplot" CommandName="addcrop"
                                                Text="Add" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6">
                                            <asp:GridView ID="gvCrops" runat="server" AutoGenerateColumns="false" DataKeyNames="FarmId,PlantationId"
                                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                                CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                                <RowStyle BackColor="#F7F7DE" />
                                                <FooterStyle BackColor="#CCCC99" />
                                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                                <AlternatingRowStyle BackColor="White" />
                                                <Columns>
                                                    <asp:BoundField HeaderText="Plot Code" DataField="AreaCode" />
                                                    <asp:TemplateField HeaderText="Area in HC">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPlotArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlotArea")%>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList></div>
                    </div>
                </asp:View>
                <asp:View ID="AllFarmer" runat="server">
                    <div>
                        <table>
                            <tr>
                                <td>
                                    Year
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAllFarmerYear" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAllFarmerYear_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Season
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAllFarmerSeason" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlAllFarmerSeason_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </td>
                                <td>
                                    Product
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlAllFarmerProduct" runat="server">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnPlan" CssClass="fb8" runat="server" Text="Plan" OnClick="btnPlan_Click" />
                                </td>
                                <td colspan="2">
                                    <asp:Button ID="btnsave" CssClass="fb8" runat="server" Text="Save" OnClick="btnsave_Click" />
                                </td>
                            </tr>
                        </table>
                        <div class="scroll_div">
                            <asp:GridView ID="gvAllFarmer" runat="server" DataKeyNames="FarmerId" AutoGenerateColumns="False"
                                BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <RowStyle BackColor="#F7F7DE" />
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </div>
                    </div>
                </asp:View>
                <asp:View ID="authorization" runat="server">
                    Your Not Authorization to vew this page
                </asp:View>
                <asp:View ID="FarmerList"  runat="server">
                    <asp:GridView ID="gvFarmerList" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerID,FarmerCode"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowCommand="gvFarmerList_RowCommand" onrowdatabound="gvFarmerList_RowDataBound">
                        <Columns>
                            <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />
                            <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                            <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Area (HC)" />
                            <asp:BoundField DataField="City_Village" HeaderText="Village" />
                            <%--<asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="Enter Form" CommandName="Farmer" />--%>
                            <asp:BoundField DataField="Organic" HeaderText="Plot Status" />
                            <asp:ButtonField ButtonType="Link" Text="Production" HeaderText="Production" CommandName="Production" />
                            <asp:ButtonField ButtonType="Link" Text="Add Crop" HeaderText="AddCrop" CommandName="AddCrop" />
                        </Columns>
                        <RowStyle BackColor="#F7F7DE" />
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </asp:View>
            </asp:MultiView>
        </div>
        <asp:Panel ID="pAddChild_F" runat="server" Style="display: none" CssClass="modalPopup">
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
                        Select Unit
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlUnitDetails" runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        No of Lots
                    </td>
                    <td>
                        <asp:TextBox ID="txtLots" runat="server"></asp:TextBox>
                    </td>
                </tr>
            </table>
            <p style="text-align: center;">
                <asp:Button ID="OkButton_F" runat="server" Text="OK" OnClick="OkButton_F_Click" />
                <asp:Button ID="CancelButton_F" runat="server" Text="Cancel" />
            </p>
        </asp:Panel>
    </div>
</asp:Content>

