<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="FarmerDiary.aspx.cs" Inherits="FarmerReports_FarmerDiary" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Farmer Diary</div>
        <div>
            <table align="center" width="100%">
                <tr align="center" style="font-family: Verdana; font-size: 18px;">
                    <td>
                        VGPal Trust
                    </td>
                </tr>
                <tr align="center" style="font-family: Verdana; font-size: 15px;">
                    <td>
                        Mudar India Exports
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:HyperLink ID="lbtnPdf" runat="server" Target="_blank" Visible="false" ForeColor="Red">Print in PDF</asp:HyperLink>
                    </td>
                </tr>
                <tr align="center" style="font-size:18px" font-family:Verdana;bgcolor="#CCCC99">
                    <td>
                        Farmer Diary
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <table border="1" align="center" width="100%">
                <tr align="center">
                    <td bgcolor="#CCFFCC">
                        Farmer Code
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblMIE" runat="server" />
                    </td>
                </tr>
                <tr align="center">
                    <td bgcolor="#CCFFCC">
                        Name of the Farmer
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblFarmername" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td bgcolor="#CCFFCC">
                        Tracenet Code
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblTrans" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td bgcolor="#CCFFCC">
                        Village
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblVillage" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td bgcolor="#CCFFCC">
                        Organic Area in Hc
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblTotalArea" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr align="center">
                    <td bgcolor="#CCFFCC">
                        No of Plots
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblNoofOrganicplots" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table width="100%" border="1" style="font-family: Verdana; font-size: 10px; width: 870px">
                <tr><td colspan="4" style="font-size:18px" bgcolor="#CCCC99" align="center">Farmer Plots 
                    wise Information</td></tr>
                <tr>
                    <td bgcolor="#CCFFCC">
                        Farmer Code
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblMIE0" runat="server" />
                    </td>
                    <td bgcolor="#CCFFCC">
                        Farmer Name
                    </td>
                    <td bgcolor="#FFFFCC">
                        <asp:Label ID="lblFarmername0" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#CCFFCC">
                        Period From
                    </td>
                    <td bgcolor='#FFFFCC'>
                        <asp:Label ID="lblPeriodFrom" runat="server" Text=""/></td>
                    <td bgcolor="#CCFFCC">Period To
                    </td>
                    <td bgcolor="#FFFFCC">
                        <asp:Label ID="lblPeriodTo" runat="server" Text=""/></td>
                </tr>
                <tr>
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        Seeds &amp; Sowing / Planting - Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvMainPlotDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" ItemStyle-HorizontalAlign="Center">
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
                                <asp:BoundField DataField="PlantingQuantity" HeaderText="Qty in KG /(MT)" ItemStyle-HorizontalAlign="Center">
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
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        Input Material Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvInput" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" ItemStyle-HorizontalAlign="Center">
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
                                <asp:BoundField DataField="IDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IMMaterial" HeaderText="Input Item" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IM_MT_HC" HeaderText="Qty in KG / MT" ItemStyle-HorizontalAlign="Center">
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
                <tr>
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        Plant Protection - Disese Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvDiseInfo" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" ItemStyle-HorizontalAlign="Center">
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
                                <asp:BoundField DataField="DMIT_HC" HeaderText="Applied in KG / MT" ItemStyle-HorizontalAlign="Center">
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
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        Plant Protection - Insect Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvInsect" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" ItemStyle-HorizontalAlign="Center">
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
                                <asp:BoundField DataField="InsectM_MT_HC" HeaderText="Applied in KG / MT" ItemStyle-HorizontalAlign="Center">
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
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        Plant Protection - Pest Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvPest" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" ItemStyle-HorizontalAlign="Center">
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
                                <asp:BoundField DataField="PMIDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PestName" HeaderText="Disease Expected" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PMIPreventionMaterial" HeaderText="Protective / Preventive materia"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PMISource" HeaderText="source" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PMIBillNo" HeaderText="Bill details  if purchased" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PMIT_HC" HeaderText="Applied in KG / MT" ItemStyle-HorizontalAlign="Center">
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
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        &nbsp; Plant Protection - Weed Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:GridView ID="gvWeed" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
                            BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                            CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" ItemStyle-HorizontalAlign="Center">
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
                                <asp:BoundField DataField="WMIDate" HeaderText="Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="WeedName" HeaderText="Disease Expected" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="WMIPreventionMaterial" HeaderText="Protective / Preventive materia"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="WMISource" HeaderText="source" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="WMIBillNo" HeaderText="Bill details  if purchased" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="WMIT_HC" HeaderText="Applied in KG / MT" ItemStyle-HorizontalAlign="Center">
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
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        Yields - Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        F:First,S:Second,HD:HarvestDate,E:Estimated,H:Herbage,AH:ActualHerbage,O:Oil,A:Actual,DD:DistillationDate,DU:DistillationUnit,BN:BatchNo
                    </td>
                </tr>
                <tr style="font-size:7.5px;">
                    <td colspan="4" align="center">
                        <asp:GridView ID="gvYields" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="875px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="code"  ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlotArea" HeaderText="Area"  ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Maincrop" HeaderText="Main Crop" 
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsInterCrop" HeaderText="Inter Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FHD11" HeaderText="FHD" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EFH" HeaderText="EFH" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FH" HeaderText="AFH" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FDD" HeaderText="FDD" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FDU" HeaderText="FDU" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="EFPQ" HeaderText="EFO" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FPQ" HeaderText="AFO" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SHD" HeaderText="SHD" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ESH" HeaderText="ESH" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SH" HeaderText="ASH" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SDD" HeaderText="SDD" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SDU" HeaderText="SDU" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ESPQ" HeaderText="ESO" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SPQ" HeaderText="ASO" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="FarmerLotnumber" HeaderText="Batch No" ItemStyle-HorizontalAlign="Center">
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
                    <td colspan="4" align="center" bgcolor="#CCCC99">
                        Total Produce Quantiy Information
                    </td>
                </tr>
                <tr>
                    <td colspan="4" align="center">
                        <asp:GridView ID="gvTotal" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="875px">
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
                                  <asp:BoundField DataField="FarmerLotnumber" HeaderText="Batch No" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="TotalProductOutput" HeaderText="Produce Quantity" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="SoldTotalQty" HeaderText="Sold to MIE" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="AvilQty" HeaderText="Available Qty" ItemStyle-HorizontalAlign="Center">
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
                    <td colspan="2" align="right">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="fb8" Text="Submit" OnClick="btnSubmit_Click" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnBack" runat="server" CssClass="fb8" Text="Back" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
