<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="TrancationsPage.aspx.cs" Inherits="Admin_TrancationsPage" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
            Transactions Page
        </div>
        <div id="divResult" runat="server" visible="true">
            <table align="center">
                <tr>
                    <td colspan="2">
                       
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvTransactions" runat="server" AutoGenerateColumns="False" 
                            CssClass="grid-view" onrowcommand="gvTransactions_RowCommand" DataKeyNames="seachdate">
                            <Columns>
                                <asp:TemplateField HeaderText="Transaction">
                                    <ItemTemplate>
                                        <%#Container.DisplayIndex+1 %>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="seachdate" HeaderText="Date and Time" DataFormatString="{0:dd/MM/yyyy hh:mm:ss tt}" />
                                <asp:ButtonField ButtonType="Link" Text="Click" HeaderText="Details" CommandName="cmd_view">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:ButtonField>
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr><td colspan="2"></td></tr>
            </table>
        </div>
        <div id="divDetailsView" runat="server" align="center" visible="false">
            <asp:GridView ID="gvDetaildList" runat="server" CssClass="grid-view">
                <Columns>
                </Columns>
                <HeaderStyle CssClass="gvheader" />
                <AlternatingRowStyle CssClass="gvalternate" />
                <RowStyle CssClass="gvnormal" />
            </asp:GridView>
        </div>
        <div></div>
    </div>
</asp:Content>
