<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="BuyerInfo.aspx.cs" Inherits="Buyer_BuyerInfo" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        .mudargrid > tbody > tr > th > a {
            color: #fff;
        }
    </style>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" style="height: auto" class="panel panel-success">
                <div id="header_Text" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                    <b>Buyer Approval List</b>
                </div>
                <div class="panel-body">
                    <asp:GridView ID="gvBuyerDetails" runat="server" DataKeyNames="Buyerid" CssClass="table table-bordered mudargrid" OnRowCommand="gvBuyerDetails_RowCommand"
                        AutoGenerateColumns="false" AllowSorting="True" OnSorting="gvBuyerDetails_Sorting">
                        <Columns>
                            <asp:ButtonField ButtonType="Link" DataTextField="BuyerCompanyName" CommandName="buyer" HeaderText="CompanyName" SortExpression="BuyerCompanyName" />
                            <asp:BoundField DataField="MobileforTextingpurpose" HeaderText="Mobile" SortExpression="MobileforTextingpurpose" />
                            <asp:BoundField DataField="CState" HeaderText="State" SortExpression="CState" />
                            <asp:BoundField DataField="CCountry" HeaderText="Country" SortExpression="CCountry" />
                            <asp:BoundField DataField="email" HeaderText="EMail" SortExpression="email" ItemStyle-Wrap="true" />
                            <asp:ButtonField ButtonType="Link" Text="EDIT" CommandName="Edit" ControlStyle-CssClass="btn btn-success" />
                        </Columns>
                    </asp:GridView>
                </div>
                <div></div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
