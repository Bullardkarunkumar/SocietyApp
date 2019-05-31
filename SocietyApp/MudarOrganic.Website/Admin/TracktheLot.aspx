<%@ Page Title="Mudarorganic-TracktheLot" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="TracktheLot.aspx.cs" Inherits="Admin_TracktheLot" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
 <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
           Track the Lot
        </div>
        <div class="scroll_div">
            <table>
                <tr>
                    <td align="right">
                        Search by 
                    </td>
                    <td >
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlSearchBy" runat="server" Width="144px">
                            <asp:ListItem Text="Select...." Value="0" />
                            <asp:ListItem Text="Invoice No." Value="1" />
                            <asp:ListItem Text="Lot No." Value="2" />
                            <asp:ListItem Text="Batch No." Value="3" />
                            <asp:ListItem Text="Order ID" Value="4" />
                            <asp:ListItem Text="PO" Value="5" />
                        </asp:DropDownList>
                    </td>
                    </tr>
                    <tr> 
                    <td align="right">Enter Text </td>
                    <td>
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtSearch" runat="server" Width="140px" />&nbsp;&nbsp;(if selected Date format: "MM/DD/YYYY like 12/24/2012")
                    </td></tr>
                    <tr>
                    <td></td>
                    <td align="left">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnSearch" runat="server"  CssClass="fb8_go" Text="GO" OnClick="btnSearch_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvTrack" runat="server" DataKeyNames="FarmerID" AutoGenerateColumns="False" 
                            onrowcommand="gvTrack_RowCommand" CssClass="grid-view">
                            <Columns>
                                <%--<asp:BoundField DataField = "ProductName" HeaderText="Product Name" />--%>
                                <asp:TemplateField  HeaderText="Product Name">
                                    <ItemTemplate>
                                        <asp:Label ID="lblProductName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>' />
                                        <asp:HiddenField ID="hfProductID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "ProductID")%>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:ButtonField DataTextField="FarmerCode" ButtonType="Link"  HeaderText="Farmer Code" CommandName="FarmerCode"  />
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                                <asp:BoundField DataField="Lotnumber" HeaderText="Lotnumber" />
                                <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                <asp:BoundField DataField ="PlantationDate" HeaderText="Plantation Date" DataFormatString="{0:MMM dd yyyy}" />
                                <asp:BoundField DataField="FirstHarvestDate" HeaderText="First Harvest Date" DataFormatString="{0:MMM dd yyyy}"/>
                                <asp:BoundField DataField="FirstDistillationDate" HeaderText="First Distillation Date" DataFormatString="{0:MMM dd yyyy}"/>
                                <asp:BoundField DataField="F_Ucode" HeaderText="Ucode" />
                                <asp:BoundField DataField="FirstHerbaga" HeaderText="First Herbaga" />
                                <asp:BoundField DataField="FirstProductQuantity" HeaderText="Distilation Quantity" />
                                
                                <asp:BoundField DataField="SecondHarvestDate" HeaderText="Second Harvest Date" DataFormatString="{0:MMM dd yyyy}"/>
                                <asp:BoundField DataField="SecondDistillationDate" HeaderText="Second Distillation Date" DataFormatString="{0:MMM dd yyyy}"/>
                                <asp:BoundField DataField="S_Ucode" HeaderText="Ucode" />
                                <asp:BoundField DataField="SecondHerbaga" HeaderText="Second Herbaga" />
                                <asp:BoundField DataField="SecondProductQuantity" HeaderText="Distilation Quantity" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                         <div id="header_Text">
                            Farmer Invoice List
                        </div>
                        <div><table align="center"><tr><td><asp:DataGrid ID="gvInvoiceList" runat="server" 
                                AutoGenerateColumns="False" BackColor="White" BorderColor="#DEDFDE" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                GridLines="Vertical">
                            <FooterStyle BackColor="#CCCC99" />
                            <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" 
                                Mode="NumericPages" />
                            <AlternatingItemStyle BackColor="White" />
                            <ItemStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundColumn DataField="OrderID" HeaderText="Order ID" />
                                <asp:BoundColumn DataField="InvoiceID" HeaderText="Invoice ID" />
                                <asp:BoundColumn DataField="Netweight" HeaderText="Quantity" />
                                <asp:BoundColumn DataField="OrderDate" HeaderText="OrderDate" />
                                <asp:BoundColumn DataField="Lotnumber" HeaderText="Lotnumber" />
                            </Columns>
                            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        </asp:DataGrid></td></tr></table></div>
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

