<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FreezeRegister.aspx.cs" Inherits="BranchReports_FreezeRegister" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <script language="javascript" type="text/javascript">
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
        <div id="header_Text">
            Freezing Register
        </div>
        <div>
            <table align="center">

                <tr align="center">
                    <td colspan="3">Year<br />
                        <asp:DropDownList ID="ddlSeasonYear" runat="server" AutoPostBack="true" Height="35px"
                            Width="210px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlSeasonYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>


            </table>
        </div>
        <div>
            <table align="center">
                <tr id="trId" visible="false" runat="server">
                    <td align="left">
                        <asp:Button ID="Button1" runat="server" Text="Print Page" CssClass="fb8" OnClientClick="javascript:CallPrint('bill');" />
                    </td>

                    <td align="center">
                        <%-- <asp:Button ID="btncollReg" runat="server" Text="Blend Details" CssClass="btnFarmer" OnClick="btncollReg_Click" />--%>
                         Total Lot Qty<br />
                        <asp:Label ID="lblLotQty" runat="server" ForeColor="Red" />

                    </td>

                    <td align="center">Total Crystal Qty<br />
                        <asp:Label ID="lblCrystal" runat="server" ForeColor="Red" />

                    </td>

                    <td align="center">Total DMO Qty<br />
                        <asp:Label ID="lblDMO" runat="server" ForeColor="Red" />

                    </td>
                    <td align="right">
                        <asp:Button ID="Button2" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnPF_Click" />
                    </td>
                </tr>

                <tr>
                    <td colspan="5">
                        <asp:GridView ID="gvBlendreg" runat="server" AutoGenerateColumns="False"
                            CssClass="grid-view" AllowSorting="True" Font-Size="12px">
                            <Columns>
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="EndDate" HeaderText="End Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="OrderID" HeaderText="OrderID" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                <asp:BoundField DataField="Qty" HeaderText="Lot Qty(KG)" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:0.00}"></asp:BoundField>
                                <asp:BoundField DataField="Blending_BatchID" HeaderText="Lotnumber" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="CrystalReceived" HeaderText="Crystal Received (KG)" DataFormatString="{0:0.00}"></asp:BoundField>
                                <asp:BoundField DataField="CrystalLotnumber" HeaderText="Crystal Lotno" />
                                <asp:BoundField DataField="FreezeQuantity" HeaderText="DMO Received (KG)" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:0.00}"></asp:BoundField>
                                <asp:BoundField DataField="freezeProductBatchID" HeaderText="DMO Lotno" />
                                <asp:BoundField DataField="Operator" HeaderText="Operator" />
                                <asp:BoundField DataField="Remarks" HeaderText="Remarks" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>

        </div>
        <div align="center" id="divBack" runat="server" visible="false">
            <asp:Button ID="btnGo" runat="server" Text="Back" CssClass="fb8" Visible="true" PostBackUrl="~/Mudar/vidhyareports.aspx" />
        </div>
    </div>
</asp:Content>

