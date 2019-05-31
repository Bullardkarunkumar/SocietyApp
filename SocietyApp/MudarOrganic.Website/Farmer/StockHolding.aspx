<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="StockHolding.aspx.cs" Inherits="Farmer_StockHolding" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height:auto">
         <div id="header_Text">
            Stock Holding
        </div>
        <div>
            <table align="center" style="font-family: Verdana; font-size:10px;width:880px"
                border="1">
                <tr>
                    <td colspan="3" style="font-size: 16px;" align="center">
                        Mudar India Exports<br />
                        Internal Inspection Report
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%" border="1">
                            <tr align="center">
                                <td>
                                    Name of the Inspector
                                </td>
                                <td>
                                    <asp:Label ID="lblInspectname" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Date of Inspection
                                </td>
                                <td>
                                    <asp:Label ID="lblIDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%" border="1">
                            <tr align="center">
                                <td colspan="4" bgcolor="#CCCC99">
                                    PERIOD
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    From
                                </td>
                                <td>
                                    <asp:Label ID="lblPFromDate" runat="server"></asp:Label>
                                </td>
                                <td>
                                    To&nbsp;&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblPToDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%" border="1">
                            <tr align="center" bgcolor="#CCCC99">
                                <td width="60%" colspan="4">
                                    Farmer Information
                                </td>
                                <td width="40%" colspan="2">
                                    Farm Details (all plots including non-organic plots)
                                </td>
                            </tr>
                            <tr align="center">
                                <td rowspan="2">
                                    Farmer Name
                                </td>
                                <td rowspan="2">
                                    <asp:Label ID="Label1" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Farmer (mie) Code
                                </td>
                                <td>
                                    <asp:Label ID="lblMIE" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Totar Area of the Farmer in Hc
                                </td>
                                <td>
                                    <asp:Label ID="lblTotalArea" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    Farmer (tracenet) Ciode
                                </td>
                                <td>
                                    <asp:Label ID="lblTrans" runat="server"></asp:Label>
                                </td>
                                <td>
                                    Total Organic Area in Hc&nbsp;
                                </td>
                                <td>
                                    <asp:Label ID="lblTotalArea0" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td>
                                    Village
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server"></asp:Label>
                                </td>
                                <td>
                                    accompanied by
                                </td>
                                <td>
                                    <asp:TextBox ID="txtAccompanied" runat="server" />
                                    <%--<asp:Label ID="lblAccompanied" runat="server"></asp:Label>--%>
                                </td>
                                <td>
                                    Number of Organic Plots
                                </td>
                                <td>
                                    <asp:Label ID="lblNoofOrganicplots" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                    </td>
                </tr>
                <tr align="center">
                    <td>
                        <asp:Label ID="lblTotalOrganic" runat="server"></asp:Label>
                    </td>
                    <td colspan="2">
                        Total Farm Area in Hc
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        other crops / Vacant Area in Hc
                    </td>
                    <td>
                        <asp:Label ID="lblVacant" runat="server" />
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Seeds &amp; Sowing / Planting - Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvMainPlotDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot (mie) code" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlotArea" HeaderText="Area in HC" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Maincrop" HeaderText="Main Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsInterCrop" HeaderText="Inter Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Plantation Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationDate","{0:dd MMM yyyy}")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Seed Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantingSource")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPBillDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantingBill_Date")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in KG/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantingQuantity")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Input Material - Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvInput" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot (mie) code" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlotArea" HeaderText="Area in HC" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Maincrop" HeaderText="Main Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsInterCrop" HeaderText="Inter Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIdate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IDate","{0:dd MMM yyyy}")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Input Item">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMMaterial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMMaterial")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IM_MT_HC")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in KG/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMSource")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Plant Protection - Disese Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvDiseInfo" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="508px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot (mie) code" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlotArea" HeaderText="Area in HC" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Maincrop" HeaderText="Main Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsInterCrop" HeaderText="Inter Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDMIDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIDate","{0:dd MMM yyyy}")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disease Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDiseaseName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiseaseName")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Protective / Preventive materia">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDMIPreventionMaterial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIPreventionMaterial")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDMISource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMISource")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDMIBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIBillNo")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in KG/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtDMIQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIT_HC")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Plant Protection - Insect Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvInsect" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot (mie) code" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlotArea" HeaderText="Area in HC" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Maincrop" HeaderText="Main Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsInterCrop" HeaderText="Inter Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIDate","{0:dd MMM yyyy}")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disease Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInsectName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsectName")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Protective / Preventive materia">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIPreventionMaterial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIPreventionMaterial")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMISource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMISource")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIBillNo")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in KG/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsectM_MT_HC")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Yields - Information
                    </td>
                </tr>
                <tr><td><asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot (mie) code" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlantationArea" HeaderText="Area in HC" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="Main Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Date">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIDate","{0:dd MMM yyyy}")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Disease Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtInsectName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsectName")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Protective / Preventive materia">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIPreventionMaterial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIPreventionMaterial")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMISource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMISource")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIBillNo")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in KG/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMIQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsectM_MT_HC")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView></td></tr>
                <tr>
                    <td colspan="3">
                        &nbsp;</td>
                </tr>
            </table></div>
        <table align="center">
            
            <tr>
                <td colspan="3">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblProId" runat="server" Text="ProductId/Name"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtProduct" runat="server" CssClass="textbox_Style"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtAutoFarmerName" runat="server" TargetControlID="txtProduct"
                        CompletionInterval="978" EnableCaching="true" CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        ShowOnlyCurrentWordInCompletionListItem="true" BehaviorID="AutoCompleteEx" ServicePath="~/AutoComplete.asmx"
                        ServiceMethod="GetProductCompletionList" MinimumPrefixLength="1">
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
                <td>
                    <asp:Button ID="btnGo" runat="server"  CssClass="fb8_go" Text="Go" 
                        onclick="btnGo_Click" />
                </td>
            </tr>
            <tr>
                <td colspan="3">
                    &nbsp
                </td>
            </tr>
            <tr>
                <td colspan="3" align="center">
                    <asp:DataList ID="dtProductDetails" runat="server" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        ForeColor="Black" GridLines="Vertical">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        ProductID
                                    </td>
                                    <td>
                                        ProductName
                                    </td>
                                    <td>
                                        Stock Holding
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <th>
                                        <%#DataBinder.Eval(Container.DataItem,"ProductId") %>
                                    </th>
                                    <th>
                                        <%#DataBinder.Eval(Container.DataItem,"ProductName") %>
                                    </th>
                                    <th>
                                        <%# DataBinder.Eval(Container.DataItem,"Holding") %>
                                    </th>
                                </tr>
                                <tr>
                                <asp:GridView ID="gvProFarmer" runat="server">
                                
                                </asp:GridView>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    
                 
                    
                    <asp:GridView ID="grdStockHolding" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#DEDFDE" 
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                        GridLines="Vertical">
                        
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                            <asp:TemplateField HeaderText="Farmer Code">
                                <ItemTemplate>
                                    <%# Eval("SNO")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Farmer Registor Number">
                                <ItemTemplate>
                                    <%# Eval("Name")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Farmer Name">
                                <ItemTemplate>
                                    <%# Eval("UserName")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Quantity">
                                <ItemTemplate>
                                    <%# Eval("Password")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Area">
                                <ItemTemplate>
                                    <%# Eval("Category")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Contact">
                                <ItemTemplate>
                                    <%# Eval("activity")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="MoreInfo">
                                <ItemTemplate>
                                    <%# Eval("activity")%>
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" Wrap="true" />
                            </asp:TemplateField>
                        </Columns>

<AlternatingRowStyle BackColor="White"></AlternatingRowStyle>
                    </asp:GridView>
                </td>
            </tr>
        </table>
        <div></div>
    </div>



</asp:Content>

