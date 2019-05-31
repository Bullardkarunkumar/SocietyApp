<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="BlendingRegister.aspx.cs" Inherits="BranchReports_BlendingRegister"  EnableEventValidation="false" %>

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
            Blending Register
        </div>
        <div>
            <table align="center">

                <tr align="center">
                    <td>Year<br />
                        <asp:DropDownList ID="ddlSeasonYear" runat="server" AutoPostBack="true" Height="35px"
                            Width="210px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlSeasonYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                    <td>&nbsp;&nbsp;&nbsp;</td>
                    <td>Product name<br />
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true"
                            Font-Size="Medium" Height="35px" TabIndex="9" Width="210px" OnSelectedIndexChanged="ddlProduct_SelectedIndexChanged">
                        </asp:DropDownList>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
                <tr >
                    <td>
                        <%-- <asp:Button ID="btncollReg" runat="server" Text="Blend Details" CssClass="btnFarmer" OnClick="btncollReg_Click" />--%>
                    </td>
                    <td>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr id="trtotal" runat="server" visible="false" align="center">
                    <td>Total Production Qty</br><asp:Label ID="lblTotalProd" runat="server" ForeColor="Red" /></td>
                    <td>Blened Qty<br />
                    </td>
                    <td>Avail Qty<br />
                        <asp:Label ID="lblAvailQty" runat="server" ForeColor="Red" />
                    </td>
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
                   Blened Qty<br />
                        <asp:Label ID="lblColleted" runat="server" ForeColor="Red" />
                        
                    </td>
                    <td align="right">
                        <asp:Button ID="Button2" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnPF_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvBlendreg" runat="server" AutoGenerateColumns="False"
                            CssClass="grid-view" AllowSorting="True" Font-Size="12px"  OnDataBound="gvBlendreg_DataBound">
                            <Columns>
                                <asp:BoundField DataField="CreatedDate" HeaderText="Blend Date" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                <asp:BoundField DataField="OrderID" HeaderText="OrderID" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                <asp:BoundField DataField="Lotnumber" HeaderText="Batchno" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="BlendQty" HeaderText="Qty(KG)" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:0.00}"></asp:BoundField>
                                <asp:BoundField DataField="BQty" HeaderText="Total Batch Qty (KG)" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:0.00}"></asp:BoundField>
                                <asp:BoundField DataField="Blending_BatchID" HeaderText="Lotnumber" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <asp:BoundField DataField="BQty1" HeaderText="Lot Qty(KG)" ItemStyle-HorizontalAlign="Center"
                                    DataFormatString="{0:0.00}"></asp:BoundField>
                                <asp:BoundField DataField="BlendedBy" HeaderText="Blended By" />
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

