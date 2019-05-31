<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="FarmerProductionData.aspx.cs" Inherits="Admin_FarmerProductionData"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
            Farmer Production Data</div>
        <div id="divGetDetails" runat="server">
            <table align="center">
                <tr>
                    <td rowspan="4" style="width: 25%;">
                    </td>
                    <td align="right">
                        Year
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true"
                            OnSelectedIndexChanged="ddlYear_SelectedIndexChanged" Font-Size="Medium" Height="35px"
                            TabIndex="9" Width="345px">
                        </asp:DropDownList>
                    </td>
                    <td rowspan="4" style="width: 25%;">
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        Season
                    </td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlSeason" runat="server" AutoPostBack="true"
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
                        &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true"
                            Font-Size="Medium" Height="35px" TabIndex="9" Width="345px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:Button ID="btnFarmerGo" CssClass="fb8" runat="server" Text="Find" OnClick="btnFarmerGo_Click"
                            Visible="false" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divNewFarmerPlantation" runat="server" class="scroll_div">
            <asp:GridView ID="gvNewFarmerPlantation" runat="server" AutoGenerateColumns="false"
                DataKeyNames="PlantationID" CssClass="grid-view" Font-Size="Small">
                <Columns>
                    <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />
                    <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                    <asp:BoundField DataField="farmerAPEDAcode" HeaderText="Farmer Register number" />
                    <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Area (Hc)" />
                    <asp:BoundField DataField="PlantationArea" HeaderText="Plantation Area (Hc)" />
                    <asp:BoundField DataField="PlantationDate" HeaderText="Plantation Date" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="FirstHarvestDate" HeaderText=" I Cut Date" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="EstimationFHerbaga" HeaderText=" Estimation Herbage (MT)" />
                    <asp:BoundField DataField="FirstHerbaga" HeaderText="Actual Herbage (MT)" />
                    <asp:BoundField DataField="FirstDistillationDate" HeaderText="Distillation Date"
                        DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="FirstUnitId" HeaderText="Distillation Unit no" />
                    <asp:BoundField DataField="FirstProductQuantity" HeaderText="Qty of Oil (KG)" />
                    <asp:BoundField DataField="SecondHarvestDate" HeaderText="II Cut Date" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="EstimationFHerbaga" HeaderText=" Estimation Herbage (MT)" />
                    <asp:BoundField DataField="FirstHerbaga" HeaderText="Actual Herbage (MT)" />
                    <asp:BoundField DataField="SecondDistillationDate" HeaderText="II Distillation Date"
                        DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="SecondUnitId" HeaderText="Distillation Unit no" />
                    <asp:BoundField DataField="SecondProductQuantity" HeaderText="Qty of Oil (KG)" />
                    <asp:BoundField DataField="TotalProductOutput" HeaderText="Total Yield (KG)" />
                </Columns>
                <HeaderStyle CssClass="gvheader" />
                <AlternatingRowStyle CssClass="gvalternate" />
                <RowStyle CssClass="gvnormal" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>
