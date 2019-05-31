<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="AvailableQty.aspx.cs" Inherits="Admin_AvailableQty" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
            Check the Qty
        </div>
        <div>
            <table align="center" width="100%">
                <tr align="center">
                    <td width="50%">
                        Year
                    </td>
                    <td width="50%">
                        <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" Height="35px" Width="345px"
                            Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td width="50%">
                        Season
                    </td>
                    <td width="50%">
                        <asp:DropDownList ID="ddlSeason" runat="server" AutoPostBack="true" Height="35px"
                            Width="345px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td width="50%">
                        Product
                    </td>
                    <td width="50%">
                        <asp:DropDownList ID="ddlProduct" runat="server" AutoPostBack="true" Height="35px"
                            Width="345px" Font-Size="Medium" TabIndex="9">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr align="center">
                    <td width="50%">
                        Enter the Qty
                    </td>
                    <td width="50%">
                        <asp:TextBox ID="txtQty" runat="server" Font-Size="Medium" Height="30px" Style="margin-bottom: 1px"
                            TabIndex="4" Width="340px"></asp:TextBox>
                    </td>
                </tr>
                <tr align="center">
                    <td colspan="2">
                        <asp:Button ID="btnGo" runat="server" Text="Find" CssClass="fb8" Visible="true" 
                            onclick="btnGo_Click" />
                    </td>
                </tr>
                <tr id="trAviQty" runat="server" align="center" visible="false">
                    <td width="50%">
                        Available Date
                    </td>
                    <td width="50%">
                        <asp:TextBox ID="txtDate" runat="server" Font-Size="Medium" Height="30px" Style="margin-bottom: 1px"
                            TabIndex="4" Width="340px"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
