<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="vidhyareports.aspx.cs" Inherits="Mudar_vidhyareports" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: 400px">
        <div id="header_Text">
            Reports List
        </div>
        <div id="mainhead" runat="server">
            <table>
                <tr>
                    <td>
                        <asp:Button ID="btncollReg" runat="server" Text="Reception Register" CssClass="btnFarmer"
                            Width="215px" OnClick="btncollReg_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnbleReg" runat="server" Text="Blending Register" CssClass="btnFarmer"
                            Width="215px" OnClick="btnbleReg_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btnpackReg" runat="server" Text="Packing Register" CssClass="btnFarmer"
                            Width="215px" OnClick="btnpackReg_Click" />
                    </td>
                    <td>
                        <asp:Button ID="btndisReg" runat="server" Text="Dispatch Register" CssClass="btnFarmer"
                            Width="215px" OnClick="btndisReg_Click" />
                    </td>
                </tr>
                <tr>
                      <td>
                        <asp:Button ID="btnFreezeReg" runat="server" Text="Freezing Register" CssClass="btnFarmer"
                            Width="215px" OnClick="btnFreezeReg_Click" /></td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

