<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="InternalInspectionReport.aspx.cs" Inherits="Farmer_InternalInspectionReport"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Internal Inspection
        </div>
        <div align="right">
            <asp:HyperLink ID="lbtnPdf" runat="server" Target="_blank" Visible="false">Print in PDF</asp:HyperLink></div>
        <div>
            <table align="center" style="font-family: Verdana; font-size: 10px; width: 820px"
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
                                    <asp:Label ID="lblFarmername" runat="server"></asp:Label>
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
                                    <asp:Label ID="lblVillage" runat="server"></asp:Label>
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
                <tr align="center">
                    <td colspan="2" bgcolor="#CCCC99">
                        Farm Details - Plot-wise
                    </td>
                    <td bgcolor="#CCCC99">
                        Seeds &amp; Sowing / Planting - Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvMainPlotDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="874px">
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
                                        <asp:TextBox ID="txtPlantationArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationDate")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Seed Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlantationArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationArea")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlantationArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationArea")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                 <asp:TemplateField HeaderText="Qty in KG/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPlantationArea" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PlantationArea")%>'
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
                <tr>
                    <td colspan="1">
                        <asp:GridView ID="gvFarmDeatils" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="360px">
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
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                    <td colspan="2">
                        <asp:GridView ID="gvSeedDetails" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="508px">
                            <Columns>
                                <asp:BoundField DataField="PlantationDate" HeaderText="Plantation Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlantingSource" HeaderText="Seed Source" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlantingBill_Date" HeaderText="Bill details if purchased"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlantingQuantity" HeaderText="Qty in KG /(Hc)" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
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
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr align="center">
                                <td width="25%">
                                    <%--<asp:Label ID="lblFTotalArea" runat="server" Visible"false"></asp:Label>--%>
                                    <asp:Label ID="lblTotalOrganic" runat="server"></asp:Label>
                                </td>
                                <td width="75%" bgcolor='#FFCC99'>
                                    Total Farm Area in Hc
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <table width="100%">
                            <tr align="center">
                                <td width="75%" bgcolor='#FFCC99'>
                                    other crops / Vacant Area in Hc
                                </td>
                                <td width="25%">
                                    <asp:Label ID="lblVacant" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2" bgcolor="#CCCC99">
                        Farm Details - Plot-wise
                    </td>
                    <td bgcolor="#CCCC99">
                        Input Material - Information
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="1">
                        <asp:GridView ID="gvFarmDeatils2" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="360px">
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
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                    <td colspan="2">
                        <asp:GridView ID="gvInput" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="508px">
                            <Columns>
                                <asp:BoundField DataField="IDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMMaterial" HeaderText="Input Item" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMQuantity" HeaderText="Qty in MT / Hc" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMSource" HeaderText="Source" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
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
                <tr align="center">
                    <td colspan="2" bgcolor="#CCCC99">
                        Farm Details - Plot-wise
                    </td>
                    <td bgcolor="#CCCC99">
                        Plant Protection - Disease Information
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <asp:GridView ID="gvFarmDeatils3" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="360px" Height="146px">
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
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                    <td colspan="2">
                        <asp:GridView ID="gvDiese" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="508px">
                            <Columns>
                                <asp:BoundField DataField="DMIDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DiseaseName" HeaderText="Disease Expected" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DMIPreventionMaterial" HeaderText="Protective / Preventive materia"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DMISource" HeaderText="source" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DMIBillNo" HeaderText="Bill details  if purchased" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="DMIQuantity" HeaderText="Applied in KG / Hc" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
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
                <tr>
                    <td colspan="2" bgcolor="#CCCC99" align="center">
                        Farm Details - Plot-wise
                    </td>
                    <td bgcolor="#CCCC99" align="center">
                        Plant Protection - Insect Information
                    </td>
                </tr>
                <tr>
                    <td colspan="1">
                        <asp:GridView ID="gvFarmDeatils5" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="360px" Height="146px">
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
                            </Columns>
                            <RowStyle BackColor="#F7F7DE" />
                            <FooterStyle BackColor="#CCCC99" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                            <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                            <AlternatingRowStyle BackColor="White" />
                        </asp:GridView>
                    </td>
                    <td colspan="2">
                        <asp:GridView ID="gvInsect" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="508px">
                            <Columns>
                                <asp:BoundField DataField="IMIDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="InsectName" HeaderText="Insect Expected" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMIPreventionMaterial" HeaderText="Protective / Preventive materia"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMISource" HeaderText="source" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMIBillNo" HeaderText="Bill details  if purchased" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMIQuantity" HeaderText="Applied in KG / Hc" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
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
                <tr align="center">
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td width="25%" align="center" bgcolor="#CCCC99">
                                    Farm Details - Plot-wise
                                </td>
                                <td width="75%" align="center" bgcolor="#CCCC99">
                                    Yields - Information
                                </td>
                            </tr>
                            <tr>
                                <td width="25%">
                                    <asp:GridView ID="gvFarmDeatils4" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="300px" Height="151px">
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
                                        </Columns>
                                        <RowStyle BackColor="#F7F7DE" />
                                        <FooterStyle BackColor="#CCCC99" />
                                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                        <AlternatingRowStyle BackColor="White" />
                                    </asp:GridView>
                                </td>
                                <td width="75%">
                                    <asp:GridView ID="gvYields" runat="server" AutoGenerateColumns="False" BackColor="White"
                                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                                        GridLines="Vertical" Width="480px">
                                        <Columns>
                                            <asp:BoundField DataField="FirstHarvestDate" HeaderText="Harvest Date" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="Estimation" HeaderText="Estimation Herbage(MT)" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FirstHerbaga" HeaderText="Actual Yield(MT)" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FirstDistillationDate" HeaderText="Distillation Date"
                                                ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FirstProductQuantity" HeaderText="Oil Yield(KG)" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="FarmerLotnumber" HeaderText="Batch No" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="soldMIE" HeaderText="Sold to MIE in KG" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
                                            <asp:BoundField DataField="SoldOut" HeaderText="Sold Out Side in KG" ItemStyle-HorizontalAlign="Center">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            </asp:BoundField>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Animal Husbandry
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvAnimal" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblAL" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtALRemarks" runat="server" TextMode="MultiLine" Width="365px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Farm Management
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvFarmMangement" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblFM" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFMRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Organic Compliance
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvOrannicComp" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblOC" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtOCRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Risk Management Compliance
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvRMC" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblRMC" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRMCRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Farmer Awareness
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvFA" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblFA" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtFARemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Risk Processing
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvRP" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblRP" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtRPRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Safety Compliance
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvSafetyComp" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblSafComp" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtSafCompRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Environment Compliance
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvEC" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblEc" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtECRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Statutory Compliance
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvStatComp" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblStatComp" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtStatCompRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6" bgcolor="#CCCC99" align="center">
                        Labor in the Farms
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%">
                            <tr>
                                <td>
                                    <asp:GridView ID="gvLF" runat="server" AutoGenerateColumns="False" DataKeyNames="QuestionId"
                                        Width="100%" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                        <RowStyle BackColor="#F7F7DE" />
                                        <Columns>
                                            <asp:BoundField DataField="QuestionID" Visible="false" HeaderText="QuestionId" />
                                            <asp:BoundField DataField="Question" HeaderText="Question" HeaderStyle-Width="250px"
                                                ItemStyle-HorizontalAlign="Center" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    <asp:RadioButtonList ID="rblLabour" runat="server" RepeatDirection="Horizontal">
                                                    </asp:RadioButtonList>
                                                </ItemTemplate>
                                                <ItemStyle HorizontalAlign="Center" />
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Remarks">
                                                <ItemTemplate>
                                                    <asp:TextBox ID="txtLabourRemarks" runat="server" TextMode="MultiLine" Width="250px"></asp:TextBox>
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
                        </table>
                    </td>
                </tr>
                <tr>
                    <td colspan="6">
                        <table width="100%" style="font-size: 12px;">
                            <tr>
                                <td width="50%">
                                    <table border="1">
                                        <tr align='center'>
                                            <td colspan='2' bgcolor='#CCCC99'>
                                                Inspector Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor='FFFF66'>
                                                Name of the Inspector
                                            </td>
                                            <td>
                                                <asp:Label ID="lblInspectname0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor='FFFF66'>
                                                Date of Inspection&nbsp;
                                            </td>
                                            <td>
                                                <asp:Label ID="lblPToDate0" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor='FFFF66'>
                                                Next Date
                                            </td>
                                            <td>
                                                <asp:Label ID="lblNDate" runat="server"></asp:Label>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                                <td width="50%">
                                    <table border="1" width="100%" style="font-size: 12px;">
                                        <tr align='center'>
                                            <td colspan='2' bgcolor='#CCCC99'>
                                                Project Manger Details
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor='FFFF66'>
                                                Approved by
                                            </td>
                                            <td>
                                                &nbsp; SukhDev singh
                                            </td>
                                        </tr>
                                        <tr>
                                            <td bgcolor='FFFF66'>
                                                Designation&nbsp;
                                            </td>
                                            <td>
                                                &nbsp;Project Manager
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                </tr>
        </div>
        <div>
            <table align="center">
                <tr>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td align="center">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSubmit" runat="server" CssClass="fb8" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    <td align="center">
                        <asp:Button ID="btnBack" runat="server" CssClass="fb8" Text="Back" OnClick="btnBack_Click" />
                    </td>
                    <td>
                    </td>
                    <td>
                    </td>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp
                    <td align="right">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnPDF" runat="server" CssClass="fb8" Text="PDF" OnClick="btnPDF_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
