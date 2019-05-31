<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FarmerPDF.aspx.cs" Inherits="Farmer_FarmerPDF" Title="Untitled Page" EnableEventValidation="false" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
          Farmer Details
        </div>
       <div id="divgvFarmerdtforPDF" align="center" visible="false" runat="server">
        <asp:GridView ID="gvFarmerPDf" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerID,FarmerCode" CssClass="grid-view">
                <Columns>
                   <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />
                    <%--<asp:ButtonField ButtonType="Link" DataTextField="FarmerCode" HeaderText="Farmer Code"
                        CommandName="FarmerCode" ItemStyle-HorizontalAlign="Left" SortExpression="FarmerCode">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:ButtonField>--%>
                    <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                    <asp:BoundField DataField="City_Village" HeaderText="Village" />
                    <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Area in(HC)" 
                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                    <asp:BoundField DataField="Organic" HeaderText="Plot Status" ItemStyle-HorizontalAlign="Center">
                        </asp:BoundField>
                   <%-- <asp:TemplateField HeaderText="Farmer Status">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfFarmerCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PRESIDNT") %>' />
                            <asp:Label ID="lblApproval" runat="server" Text="Approved" Visible='<%# DataBinder.Eval(Container.DataItem, "PRESIDNT") %>' />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:TemplateField>--%>
                </Columns>
                <HeaderStyle CssClass="gvheader" />
                <AlternatingRowStyle CssClass="gvalternate" />
                <RowStyle CssClass="gvnormal" />
            </asp:GridView>
        </div>
    </div>
</asp:Content>

