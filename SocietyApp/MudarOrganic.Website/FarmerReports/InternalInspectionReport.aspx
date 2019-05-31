<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="InternalInspectionReport.aspx.cs" Inherits="FarmerReports_InternalInspectionReport"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Internal Inspection
        </div>
        <div>
            <table align="right" style="font-family: Verdana; font-size: 9px; width: 870px; background-color: #FFFFCC;"
                border="1">
                <tr id="trreport" runat="server" visible="false">
                    <td  style="font-size: 16px;">
                        Report Submit Date:
                        <asp:Label ID="lblSubmitDate" runat="server" ForeColor="Red" ></asp:Label>
                    </td>
                    <td colspan="2" align="right" style="font-size: 16px;">
                        <asp:HyperLink ID="lbtnPdf" runat="server" Target="_blank" ForeColor="Red">Print in PDF</asp:HyperLink>
                    </td>
                </tr>
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
                                <td bgcolor="#CCFFCC">
                                    Name of the Inspector
                                </td>
                                <td bgcolor="#FFFFCC">
                                    <asp:Label ID="lblInspectname" runat="server"></asp:Label>
                                </td>
                                <td bgcolor="#CCFFCC">
                                    Date of Inspection
                                </td>
                                <td bgcolor="#FFFFCC">
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
                                <td bgcolor="#CCFFCC">
                                    From
                                </td>
                                <td bgcolor="#FFFFCC">
                                    <asp:Label ID="lblPFromDate" runat="server"></asp:Label>
                                </td>
                                <td bgcolor="#CCFFCC">
                                    To&nbsp;&nbsp;
                                </td>
                                <td bgcolor="#FFFFCC">
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
                                <td rowspan="2" bgcolor="#CCFFCC">
                                    Farmer Name
                                </td>
                                <td rowspan="2" bgcolor="#FFFFCC">
                                    <asp:Label ID="lblFarmername" runat="server"></asp:Label>
                                </td>
                                <td bgcolor="#CCFFCC">
                                    Farmer (mie) Code
                                </td>
                                <td bgcolor="#FFFFCC">
                                    <asp:Label ID="lblMIE" runat="server"></asp:Label>
                                </td>
                                <td bgcolor="#CCFFCC">
                                    Totar Area of the Farmer in Hc
                                </td>
                                <td bgcolor='#FFFFCC'>
                                    <asp:Label ID="lblTotalArea" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td bgcolor="#CCFFCC">
                                    Farmer (tracenet) Ciode
                                </td>
                                <td bgcolor="#FFFFCC">
                                    <asp:Label ID="lblTrans" runat="server"></asp:Label>
                                </td>
                                <td bgcolor="#CCFFCC">
                                    Total Organic Area in Hc&nbsp;</td>
                                <td bgcolor="#FFFFCC">
                                    <asp:Label ID="lblTotalArea0" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr align="center">
                                <td bgcolor="#CCFFCC">
                                    Village
                                </td>
                                <td>
                                    <asp:Label ID="lblVillage" runat="server"></asp:Label>
                                </td>
                                <td bgcolor="#CCFFCC">
                                    accompanied by
                                </td>
                                <td bgcolor="#FFFFCC">
                                    <asp:TextBox ID="txtAccompanied" runat="server" />
                                    <%--<asp:Label ID="lblAccompanied" runat="server"></asp:Label>--%>
                                </td>
                                <td bgcolor="#CCFFCC">
                                    Number of Organic Plots
                                </td>
                                <td bgcolor="#FFFFCC">
                                    <asp:Label ID="lblNoofOrganicplots" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
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
                                <asp:TemplateField HeaderText="Qty in MT/(HC)">
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
                <tr align="center" id="trTotFarm" runat="server" visible="false">
                    <td>
                        <asp:Label ID="lblTotalOrganic" runat="server"></asp:Label>
                    </td>
                    <td colspan="2" bgcolor="#CCFFCC">
                        Total Farm Area in Hc
                    </td>
                </tr>
                <tr align="center" id="trOthers" runat="server" visible="false">
                    <td colspan="2" bgcolor="#CCFFCC">
                        other crops / Vacant Area in Hc
                    </td>
                    <td>
                        <asp:Label ID="lblVacant" runat="server" />
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
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMSource")%>'
                                            Width="120px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in MT/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtIMQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IM_MT_HC")%>'
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
                                        <asp:TextBox ID="txtDMIDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIDate","{0:dd MMM yyyy}")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Diesease Nme">
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
                                <asp:TemplateField HeaderText="Qty in MT/(HC)">
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
                                <asp:TemplateField HeaderText="Insect Name">
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
                                <asp:TemplateField HeaderText="Qty in MT/(HC)">
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
                        Plant Protection - Pest Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvPest" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
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
                                        <asp:TextBox ID="txtPMIDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIDate","{0:dd MMM yyyy}")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Pest Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPestName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PestName")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Protective / Preventive materia">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMIPreventionMaterial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIPreventionMaterial")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMISource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMISource")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMIBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIBillNo")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in MT/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtPMIT_HC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIT_HC")%>'
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
                        Plant Protection - Weed Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvWeed" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmID"
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
                                        <asp:TextBox ID="txtWMIDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIDate","{0:dd MMM yyyy}")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Weed Name">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWeedName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WeedName")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Protective / Preventive materia">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWMIPreventionMaterial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIPreventionMaterial")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Source">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWMISource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMISource")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Bill details If Purchased">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWMIBillNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIBillNo")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Qty in MT/(HC)">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtWMIT_HC" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIT_HC")%>'
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
                <tr bgcolor="#CCCC99">
                    <td colspan="3">
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        F:First,S:Second,HD:HarvestDate,E:Estimated,H:Herbage,AH:ActualHerbage,O:Oil,A:Actual,DD:DistillationDate,DU:DistillationUnit,BN:BatchNo</td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvYields" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="874px">
                            <Columns>
                                <asp:BoundField DataField="AreaCode" HeaderText="code" HeaderStyle-Width="10px" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PlotArea" HeaderText="Area" HeaderStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Maincrop" HeaderText="Main Crop" HeaderStyle-Width="20px"
                                    ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="IsInterCrop" HeaderText="Inter Crop" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="FHD">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFHD" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"FHD11","{0:dd MMM yyyy}")%>'
                                            Width="40px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EFH">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEFH" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFH")%>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AFH">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFH" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FH")%>'
                                            Width="20px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FDD">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFDD" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDD")%>'
                                            Width="40px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FDU">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFDU" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FDU")%>'
                                            Width="26px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="EFO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtEFPQ" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EFPQ")%>'
                                            Width="26px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="AFO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtFPQ" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FPQ")%>'
                                            Width="26px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SHD">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSHD" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SHD")%>'
                                            Width="50px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ESH">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtESH" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ESH")%>'
                                            Width="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ASH">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSH" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SH")%>'
                                            Width="25px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SDD">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSDD" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SDD")%>'
                                            Width="40px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="SDU">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSDU" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SDU")%>'
                                            Width="26px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ESO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtESPQ" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ESPQ")%>'
                                            Width="26px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="ASO">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtSPQ" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SPQ")%>'
                                            Width="26px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="BN">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtBN" Font-Size="X-Small" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FarmerLotnumber")%>'
                                            Width="26px" />
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
                        &nbsp;Total Produce Quantiy&nbsp; Information
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
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
                                <asp:TemplateField HeaderText="Produce Quantity">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtTotalProductOutput" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalProductOutput")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Sold to MIE">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtsoldtoMIE" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SoldTotalQty")%>'
                                            Width="90px" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Available Qty">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtAvilQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AvilQty")%>'
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
                        Animal Husbandry
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Farm Management
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Organic Compliance
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Risk Management Compliance
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Farmer Awareness
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Risk Processing
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Safety Compliance
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Environment Compliance
                    </td>
                </tr>
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Statutory Compliance
                    </td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
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
                <tr align="center" bgcolor="#CCCC99">
                    <td colspan="3">
                        Labor in the Farms
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
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
                <tr style="background-color: #FFFFCC;">
                    <td colspan="2" align="right">
                        <asp:Button ID="btnSubmit" runat="server" CssClass="fb8" Text="Submit" OnClick="btnPdf_Click" />
                    </td>
                    <td colspan="1" align="center">
                        <asp:Button ID="btnBack" runat="server" CssClass="fb8" Text="Back" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
