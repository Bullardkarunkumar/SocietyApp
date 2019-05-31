<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="UnitInfo.aspx.cs" Inherits="FarmerReports_UnitInfo" %>


<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <script lang="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=100,toolbar=0,scrollbars=0,status=0,dir=ltr');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>
    <div id="content_area_Home" style="height: auto">
        <style type="text/css">
            #ctl00_body_cph_chkICSList label {
                padding-left: 5px;
            }
        </style>
        <div id="header_Text">
            Unit Information
        </div>
        <div>
            <table align="center">
                <tr id="btnVisible" runat="server">

                    <td colspan="8" align="center">
                        <asp:Button ID="btnUnitwise" runat="server" Text="All Unitwise" CssClass="fb8" OnClick="btnUnitwise_Click" /></td>
                    <td colspan="4">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;</td>
                    <td colspan="8" align="center">
                        <asp:Button ID="btnProductwise" runat="server" Text="All Produtwise" CssClass="fb8" OnClick="btnProductwise_Click" /></td>
                </tr>
                <tr id="trYear" runat="server" visible="false">
                    <td colspan="20" align="center">Year<br />
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" Height="35px"
                            Width="150px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList></td>
                </tr>
                <tr id="trIcslist" runat="server" visible="false">
                    <td colspan="20" align="center">
                        <asp:CheckBoxList ID="chkICSList" AutoPostBack="true" CellPadding="4" CellSpacing="4" runat="server" RepeatDirection="Horizontal" RepeatColumns="10" OnSelectedIndexChanged="chkICSList_SelectedIndexChanged"></asp:CheckBoxList></td>
                </tr>
            </table>
        </div>
        <div id="divUnitDetails" runat="server" visible="false">
            <table align="center">
                <tr>
                    <td colspan="2" align="left">
                        <asp:Button ID="btnsubmit" runat="server" Text="Print Page" CssClass="fb8" OnClientClick="javascript:CallPrint('bill');" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 
                    </td>
                    <td colspan="2" align="center" id="tdProdwise" runat="server">Total Herbaga(MT)</br><asp:Label ID="lblHerb" runat="server" ForeColor="Red" /></td>
                    <td colspan="2"></td>
                    &nbsp;&nbsp;&nbsp;&nbsp;
                    <td colspan="2" align="center" id="tdProdwise1" runat="server">Total Oil Qty(KG)</br><asp:Label ID="lblOil" runat="server" ForeColor="Red" /></td>
                    <td colspan="2" align="right">
                        <asp:Button ID="btnPF" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnPF_Click" /></td>
                </tr>
                <tr>
                    <td colspan="10">
                        <asp:GridView ID="gvUnitInfo" runat="server" AutoGenerateColumns="false"
                            CssClass="grid-view" AllowSorting="True" Font-Size="12px">
                            <Columns>
                                <asp:BoundField DataField="Ucode" HeaderText="Unit Name" />
                                <asp:BoundField DataField="Date" HeaderText="Date" DataFormatString="{0:dd MMM yyyy}" />
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                                <asp:BoundField DataField="AreaCode" HeaderText="Plot code" HeaderStyle-Width="25px" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-Width="25px" />
                                <asp:BoundField DataField="Cut" HeaderText="Cut" />
                                <asp:BoundField DataField="FirstHerbaga" HeaderText="Herbage(MT)" HeaderStyle-Width="25px" />
                                <asp:BoundField DataField="FirstProductQuantity" HeaderText="Oil Yield(KG)" HeaderStyle-Width="25px" />
                                <asp:BoundField DataField="FarmerLotnumber" HeaderText="Batch" HeaderStyle-Width="25px" />
                                <asp:BoundField DataField="whetherdrumsealed" HeaderText="whether drumsealed?" HeaderStyle-Width="25px" />
                                <asp:BoundField DataField="inchargeperson" HeaderText="inchargeperson" HeaderStyle-Width="25px" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" Font-Size="12px" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>

                    </td>
                </tr>
                <tr align="center">
                    <td colspan="10">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnBack_Click" /></td>
                </tr>
            </table>
        </div>
        <div id="tdProdYear" runat="server" visible="false" align="center">
            Year<br />
            <asp:DropDownList ID="ddlProdYear" runat="server" AutoPostBack="true" Height="35px"
                Width="150px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlProdYear_SelectedIndexChanged">
            </asp:DropDownList>
        </div>
        <div id="divProdwiseDetails" runat="server" visible="false">
            <table>
                <tr>
                    <td align="left">
                        <asp:Button ID="btnsubmit0" runat="server" Text="Print Page" CssClass="fb8" OnClientClick="javascript:CallPrint('bill');" />
                    </td>
                    <td align="center">&nbsp;</td>
                    <td align="right">
                        <asp:Button ID="btnProdEx" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnProdEx_Click" /></td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvProduct" runat="server" CssClass="grid-view" Font-Size="Medium"
                            AutoGenerateColumns="false" GridLines="Both">
                            <Columns>
                                <asp:BoundField DataField="Ucode" HeaderText="Unit Name" />
                                <asp:BoundField DataField="PPH" HeaderText="PP in Herb(MT)" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="CMH" HeaderText="CM in Herb(MT)" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="SPH" HeaderText="SP in Herb(MT)" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="BOH" HeaderText="BO in Herb(MT)" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="PPO" HeaderText="PP in Oil(KG)" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="CMO" HeaderText="CM in Oil(KG)" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="SPO" HeaderText="SP in Oil(KG)" DataFormatString="{0:0.00}" />
                                <asp:BoundField DataField="BOO" HeaderText="BO in Oil(KG)" DataFormatString="{0:0.00}" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="3">
                        <asp:Button ID="btnBackprod" Visible="false" runat="server" Text="Back" CssClass="fb8" OnClick="btnBackprod_Click" /></td>
                </tr>
            </table>

        </div>
</asp:Content>

