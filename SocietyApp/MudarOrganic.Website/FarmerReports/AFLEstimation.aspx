<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="AFLEstimation.aspx.cs" Inherits="FarmerReports_AFLEstimation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            AFL Estimation
        </div>
    <div>
    </div>
         <div align="center">
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnsubmit" runat="server" Text="Print Page" CssClass="fb8" OnClientClick="javascript:CallPrint('bill');" />
                    </td>
                        <td align="center">
                          Year<br/>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" Height="35px"
                        Width="150px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPF" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnPF_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="bill" runat="server" visible="false">
            <div align="center" style="font-size: 16px; background-color: #CCCC99">
                Mudar India Exports
                <br />
                AFL Report - 2<br />
            </div>
            <div>
                <asp:GridView ID="gvFarmerList" runat="server" AutoGenerateColumns="False"
                    Font-Size="8px" OnRowCreated="gvFarmerList_RowCreated" OnRowDataBound="gvFarmerList_RowDataBound"
                    OnDataBound="gvFarmerList_DataBound" GridLines="Both">
                    <Columns>
                        <asp:BoundField DataField="FarmerCode" HeaderText="Famer Code" />
                        <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                        <asp:BoundField DataField="FarmerAPEDACode" HeaderText="Farmer Tracenet No" />
                        <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Organic Area(Ha)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PC" HeaderText="Organic Area code" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="PA" HeaderText="Organic Plot Area(Ha)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="CA" HeaderText="Crop Area (Ha)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FEH1" HeaderText="FEH" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SEH1" HeaderText="SEH" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Esti" HeaderText="ETH(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FEO" HeaderText="FEO" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SEO" HeaderText="SEO" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ETO" HeaderText="ETO" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CA1" HeaderText="Crop Area (Ha)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="FEH2" HeaderText="FEH1" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SEH2" HeaderText="SEH1" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Esti1" HeaderText="ETH2 (MT)" ItemStyle-HorizontalAlign="Center" />
                         <asp:BoundField DataField="FEO1" HeaderText="FEO1" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SEO1" HeaderText="SEO1" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ETO1" HeaderText="ETO1" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="CA2" HeaderText="Crop Area (Ha)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FEH3" HeaderText="FEH2" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SEH3" HeaderText="SEH2" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="Esti2" HeaderText="ETH2(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FEO2" HeaderText="FEO2" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SEO2" HeaderText="SEO2" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="ETO2" HeaderText="ETO2" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="VC" HeaderText="Vacant Land" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <AlternatingRowStyle BackColor="#e6e3e3" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#66CCFF" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#CCCC99" />
                </asp:GridView>
            </div>
        </div>
         <div align="center">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" Visible="false" PostBackUrl="~/FarmerReports/FarmersReports.aspx" />
        </div>
    </div>
</asp:Content>

