<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="ProduceList.aspx.cs" Inherits="Admin_ProduceList" %>

<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
            Production Information 
        </div>
        <div></div>
        <div>
            <table align="center">
                <tr>
                    <td colspan="8" align="center">Year<br />
                        <asp:DropDownList ID="ddlyear" runat="server" Height="35px" Font-Size="Medium" />
                        <br />
                        <asp:CheckBoxList ID="chkICSList" AutoPostBack="true" CellPadding="4" CellSpacing="4" runat="server" RepeatDirection="Horizontal" RepeatColumns="4"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnPP" runat="server" Text="PP-1" CssClass="btnFarmer" OnClick="btnPP_Click" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnCM" runat="server" Text="CM-2" CssClass="btnFarmer" OnClick="btnCM_Click" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnSP" runat="server" Text="SP-3" CssClass="btnFarmer" OnClick="btnSP_Click" />
                    </td>
                    <td colspan="2" align="left">
                        <asp:Button ID="btnBOs" runat="server" Text="BO-4" CssClass="btnFarmer" OnClick="btnBOs_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divResult" runat="server" visible="false">
            <table>
                <tr>
                    <td align="center">&nbsp;Crop Area<br />
                        <asp:Label ID="lblCroparea" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center"> First Esti Herb<br />
                        <asp:Label ID="lblEstiHerb" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center"> First Actual Herb<br />
                        <asp:Label ID="lblHerbage" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center"> First Esti Oil<br />
                        <asp:Label ID="lblFEoil" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center"> First Actual Oil<br />
                        <asp:Label ID="lblFAOil" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                       <td align="center"> second Esti Herb<br />
                        <asp:Label ID="lblSEH" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center"> Second Actual Herb<br />
                        <asp:Label ID="lblSAH" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center"> Second Esti Oil<br />
                        <asp:Label ID="lblSEOil" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center"> Second Actual Oil<br />
                        <asp:Label ID="lblSAOil" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                    <td align="center">Toatl Oil<br />
                        <asp:Label ID="lblTOil" runat="server" Text="0" ForeColor="SeaGreen" /></td>
                </tr>

                <tr >
                    <td colspan="9" align="center">
                        <asp:GridView ID="gvPDetails" runat="server" AutoGenerateColumns="False"
                            Font-Size="11.5px"
                            CssClass="grid-view"
                            AllowSorting="True" OnSorting="gvPDetails_Sorting">
                            <Columns>
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />

                                <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Organic Area(Ha)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="PA" HeaderText="Plot Area(Ha)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="CA" HeaderText="Crop Area (Ha)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EstimationFHerbaga" HeaderText="F.Esti Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="FirstHerbaga" HeaderText="F.Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EstimationFProductQuantity" HeaderText="F.Esti Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="FirstProductQuantity" HeaderText="F.Actual Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EstimationSHerbaga" HeaderText="S.Esti Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SecondHerbaga" HeaderText="S.Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="EstimationSProductQuantity" HeaderText="S.Esti Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="SecondProductQuantity" HeaderText="S.Actual Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                                <asp:BoundField DataField="TotalProductQuantity" HeaderText="Total Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                                <%--  <asp:BoundField DataField="PlantationId" HeaderText="PlantationId" Visible="false" />
                                <asp:BoundField DataField="farmercode" HeaderText="Farmer Code" SortExpression="farmercode" />
                                <asp:BoundField DataField="firstname" HeaderText="Farmer Name" SortExpression="firstname" />
                                <asp:BoundField DataField="AreaCode" HeaderText="Area Code" SortExpression="AreaCode" />
                                <asp:BoundField DataField="PlantationDate" HeaderText="Plantation Date" SortExpression="PlantationDate" DataFormatString="{0:dd MMM yyyy}" />
                               <asp:BoundField DataField="DistillationDate" HeaderText="Distillation Date" SortExpression="DistillationDate" DataFormatString="{0:dd MMM yyyy}" />
                                <asp:BoundField DataField="PlantationArea" HeaderText="Plantation Area" SortExpression="PlantationArea" />
                                <asp:BoundField DataField="Herbage" HeaderText="Herbage" SortExpression="Herbage" />
                                <asp:BoundField DataField="Cut" HeaderText="Cut" SortExpression="Cut" />
                                <asp:BoundField DataField="Qty" HeaderText="Qty" SortExpression="Qty" />
                               <asp:BoundField DataField="SoldTotalQty" HeaderText="Sold Qty" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />
                                <asp:BoundField DataField="AvialQty" HeaderText="Available Qty" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:0.0}" />--%>
                            </Columns>
                            <HeaderStyle CssClass="gvheader" HorizontalAlign="Center" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>


        </div>
</asp:Content>
