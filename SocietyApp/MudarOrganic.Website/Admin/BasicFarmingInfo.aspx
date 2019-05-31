<%@ Page Title="Mudarorganic-BasicFarmingInfo" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="BasicFarmingInfo.aspx.cs" Inherits="Admin_BasicFarmingInfo" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
            Basic Farming Info
        </div>
    <div align="center">
        <asp:Button ID="btnAddBasic" runat="server" Text="Add BasicFarm" CssClass="fb8" OnClick="btnAddBasic_Click"
            Visible="true" />
    </div>
    <div id="divAllSeasonNames" runat="server">
        <table align="center">
            <tr>
                <td colspan="8" align="center">
                    Season Year<br/>
                    <asp:DropDownList ID="ddlSeasonYear" runat="server" AutoPostBack="true" Height="35px"
                        Width="125px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:Button ID="btnTZaid" runat="server" Text="Zaid" CssClass="btnFarmer" OnClick="btnTZaid_Click" />
                </td>
                <td colspan="2" align="center">
                    <asp:Button ID="btnTKharif" runat="server" Text="Kharif" CssClass="btnFarmer" OnClick="btnTKharif_Click" />
                </td>
                <td colspan="2" align="center">
                    <asp:Button ID="btnTRabi" runat="server" Text="Rabi" CssClass="btnFarmer" OnClick="btnTRabi_Click" />
                </td>
                <td colspan="2" align="left">
                    <asp:Button ID="btnTAnnual" runat="server" Text="Annual" CssClass="btnFarmer" OnClick="btnTAnnual_Click" Visible="false" />
                </td>
            </tr>
        </table>
    </div>
    <div id="divBasicFarmDetails" runat="server" align="center">
        <asp:GridView ID="gvBasicFarm" runat="server" AutoGenerateColumns="false" CssClass="grid-view"
            OnRowCommand="gvBasicFarm_RowCommand" DataKeyNames="FarmingInfoID">
            <Columns>
                <asp:BoundField DataField="FarmingInfoID" HeaderText="FarmingInfoID" Visible="false" />
                <asp:ButtonField ButtonType="Link" CommandName="cmd_Select" DataTextField="ProductName"
                    HeaderText="Product Name" />
                <asp:BoundField DataField="PlantFDate" HeaderText="Plantation Start" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="PlantTDate" HeaderText="Plantation End" DataFormatString="{0:dd MMM yyyy}" />
                <asp:BoundField DataField="Year" HeaderText="Season Year" />
            </Columns>
            <HeaderStyle CssClass="gvheader" />
            <AlternatingRowStyle CssClass="gvalternate" />
            <RowStyle CssClass="gvnormal" />
        </asp:GridView>
    </div>
    <div id="divBasicForm" runat="server" visible="false">
        <table>
            <tr>
                <td colspan="2" align="right">
                    <asp:HiddenField ID="hfBFID" runat="server" Value="0" />
                </td>
                <td colspan="2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    Year
                </td>
                <td colspan="2">
                    &nbsp;
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" Height="35px" Width="345px"
                        Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    Season
                </td>
                <td colspan="2">
                    &nbsp;
                    <asp:DropDownList ID="ddlSeason" runat="server" AutoPostBack="true" Height="35px"
                        Width="345px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    Product
                </td>
                <td colspan="2">
                    &nbsp;
                    <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" Height="35px"
                        Width="345px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                    </asp:DropDownList>
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td rowspan="8" style="width: 12%;"></td>
                    <td align="right">
                        Plantation Range
                    </td>
                    <td align="center">
                        &nbsp;&nbsp;<asp:TextBox ID="txtPlantationFDate" runat="server" Height="30px" Style="margin-bottom: 1px"
                            Width="180px" Font-Size="Medium"></asp:TextBox><asp:CalendarExtender ID="dtpLastDate"
                                runat="server" Format="MM/dd/yyyy" TargetControlID="txtPlantationFDate">
                            </asp:CalendarExtender>
                        &nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                        <asp:TextBox ID="txtPlantationTDate" runat="server" Height="30px" Style="margin-bottom: 1px"
                            Width="180px" Font-Size="Medium"></asp:TextBox>
                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="MM/dd/yyyy" TargetControlID="txtPlantationTDate">
                        </asp:CalendarExtender>
                    </td>
                    <td rowspan="8" style="width: 13%;">
                    </td>
            </tr>
            <tr>
                <td align="right">
                    No of Days of 1st Cut&nbsp;
                </td>
                <td align="center">
                    &nbsp;&nbsp;<asp:TextBox ID="txt1stCutF" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:TextBox ID="txt1stCutT" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Qty of Herbage in 1st Cut / HC&nbsp;
                </td>
                <td align="center">
                    &nbsp;&nbsp;<asp:TextBox ID="txt1stHCF" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:TextBox ID="txt1stHCT" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Oil Recovery / MT Herbage
                </td>
                <td align="center">
                    &nbsp;&nbsp;<asp:TextBox ID="txt1stOilF" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:TextBox ID="txt1stOilT" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    No of Days of 2nd Cut&nbsp;
                </td>
                <td align="center">
                    &nbsp;&nbsp;<asp:TextBox ID="txt2ndCutF" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:TextBox ID="txt2ndCutT" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Qty of Herbage in 2nd Cut / HC&nbsp;
                </td>
                <td align="center">
                    &nbsp;&nbsp;<asp:TextBox ID="txt2ndHCF" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:TextBox ID="txt2ndHCT" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Oil Recovery / MT Herbage
                </td>
                <td align="center">
                    &nbsp;&nbsp;<asp:TextBox ID="txt2ndOilF" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:TextBox ID="txt2ndOilT" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    Estimation vs Actual %
                </td>
                <td align="center">
                    &nbsp;&nbsp;<asp:TextBox ID="txtActualF" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>&nbsp;&nbsp;&nbsp; to&nbsp;&nbsp;
                    <asp:TextBox ID="txtActualTo" runat="server" Height="30px" Style="margin-bottom: 1px"
                        Width="180px" Font-Size="Medium"></asp:TextBox>
                </td>
            </tr>
        </table>
        <table width="885">
            <tr>
                <td>
                </td>
                <td>
                </td>
                <td align="center">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnFarmerInfosumbit" runat="server" Text="Submit" CssClass="fb8"
                        OnClick="btnFarmerInfosumbit_Click" />
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:Button ID="btnFarmerInfoClear"
                        CssClass="fb8" runat="server" Text="clear" OnClick="btnFarmerInfoClear_Click" />
                </td>
                <td>
                </td>
            </tr>
        </table>
    </div>
    </div>
</asp:Content>

