<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/MudarMasterNew.master" CodeFile="StockOrder.aspx.cs" Inherits="Admin_StockOrder" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        th, td {
            text-align: center;
        }
    </style>
    <asp:GridView ID="gvOrder" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered mudargrid">
        <Columns>
            <asp:BoundField HeaderText="ProductID" DataField="ProductID" Visible="false" />
            <%--<asp:BoundField HeaderText="Drum No" DataField="DrumNo" />--%>
            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />            
            <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
            <asp:BoundField HeaderText="Parameter" DataField="Parameter" />
            <asp:BoundField HeaderText="Analysis" DataField="AnalysisValue" />
            <asp:BoundField HeaderText="Lotnumber" DataField="LotNumber" />
            <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
        </Columns>
    </asp:GridView>
</asp:Content>
