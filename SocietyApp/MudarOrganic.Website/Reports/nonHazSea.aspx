<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="nonHazSea.aspx.cs" Inherits="Reports_nonHazSea" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height:auto;">
  <div id="header_Text">
                Non Haz Sea Report
           </div>
      <div>
<div>
        <table align="center" style="font-family: Verdana; width: 885px">
           <tr><td colspan="4"></td></tr>
           <%-- <tr bgcolor="#ffcc66">
                <td style="width: 885px" colspan="4">
                    <table align="center" style="font-family: Verdana; width: 885px">
                        <tr>
                            <td colspan="6" align="center" style='font-size: 12px;'>
                                <h1>
                                    Mudar India Exports</h1>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6" align="center" style='font-size: 10px;'>
                                6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td colspan="4" align="right">
                    Date:<asp:Label ID="lblDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center" class="style1">
                    To Whomever It may Concern
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    We declare that the below mentioned goods
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="4" align="center">
                    <asp:GridView ID="gvProductList" runat="server" AutoGenerateColumns="False" 
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" 
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <Columns>
                            <asp:TemplateField HeaderText="S.No.">
                                <ItemTemplate>
                                    <asp:Label ID="lblsnnumber" runat="server" Text=" <%#Container.DisplayIndex+1 %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                            <asp:BoundField DataField="TotalDrums" HeaderText="Total Drums" />
                            <asp:BoundField DataField="Quantity" HeaderText="Total Qty in KG" />
                        </Columns>
                        <RowStyle BackColor="#F7F7DE" />
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td width="25%" align="center">
                    shipped to
                </td>
                <td colspan="2" width="45%" align="center">
                    <asp:Label ID="lblCAddress" runat="server" />
                </td>
                <td width="30%" align="center">
                    &nbsp;<asp:Label ID="lblDCountry" runat="server" />
                </td>
            </tr>
            <tr>
                <td width="50%" colspan="2" align="center">
                    through our Invoice No&nbsp;:&nbsp;   <asp:Label ID="lblInvoice" runat="server" />
                </td>
                <td width="25%">
                    &nbsp;Date&nbsp; :&nbsp;
                    <asp:Label ID="lblInvoiceDate" runat="server" />
                </td>
                <td width="25%">
                    &nbsp;&nbsp; is 100% Non-Hazardous
                </td>
            </tr>
        </table>
    </div>
 </div>
</asp:Content>

