<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="COA-AR.aspx.cs" Inherits="Reports_COA_AR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
 <div id="content_area_Home" style="height:auto;">
  <div id="header_Text">
                &nbsp;COA-AR Report
           </div>
           <div align="center" style="font-family: Verdana;">
        <table align="center" style="font-family: Verdana; width:869px">
            <%--<tr bgcolor='#ffcc66'>
                <td colspan="4">
                    <table align='center' style='font-family: Verdana;'>
                        <tr>
                            <td  align='center' style='font-size: 12px;'>
                                Mudar India Exports
                            </td>
                        </tr>
                        <tr>
                            <td  align='center' style='font-size: 10px;'>
                                6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>--%>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td>
                    <table width="50%" align="center" border="1">
                        <tr>
                            <td>
                                COA No
                            </td>
                            <td>
                                025367
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                    <table width="50%" align="center" border="1">
                        <tr>
                            <td>
                                Date
                            </td>
                            <td align="center">
                                <asp:Label ID="lblTodayDate" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    Certificate of Analysis
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table width="100%" border="1">
                        <tr>
                            <td rowspan="2">
                                <asp:Label ID="lblConsigneeAddress" runat="server" />
                              
                            </td>
                            <td>
                                Buyers PO #
                                
                            </td>
                            <td >
                                <asp:Label ID="lblPO" runat="server" />
                            </td>
                            <td >
                                &nbsp;Date&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblPODate" runat="server" />
                            </td>
                        </tr>
                       
                        
                        <tr><td>
                                Inv No<br />
                            </td>
                            <td>
                                <asp:Label ID="lblInvoice" runat="server" />
                            </td>
                            <td>
                                &nbsp;Date&nbsp;
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceDate" runat="server" />
                            </td></tr>
                        <tr>
                        <td></td><td></td><td></td><td></td><td></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
            </tr>
             <tr>
                <td colspan="2">
                    <asp:DataList ID="dlCOAdetails" runat="server" GridLines="Vertical" UseAccessibleHeader="True"
                        DataKeyField="ProductID" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                        BorderWidth="1px" CellPadding="4" ForeColor="Black" Width="885px">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table width="100%">
                                <tr>
                                    <td>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td colspan="2">
                                                    Name of the Product
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblProductName" runat="server"
                                                     Text='<%# DataBinder.Eval(Container.DataItem, "ProductName")%>'  />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Lot Qty in KG
                                                </td>
                                                <td>
                                                   <asp:Label ID="lblQty" runat="server"
                                                   Text='<%# DataBinder.Eval(Container.DataItem, "Quantity")%>'  />
                                                </td>
                                                <td>
                                                    Year of Production
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtYearProduction" runat="server" Text="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Lot No
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblLotNo" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "Blending_BatchID")%>'/>
                                                </td>
                                                <td>
                                                    Drum Ref
                                                </td>
                                                <td>
                                                    <asp:Label ID="lblDrumRef" runat="server" 
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "TotalDrums")%>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <table width="100%" border="1">
                                            <tr>
                                                <td rowspan="2">
                                                    Parameter
                                                </td>
                                                <td rowspan="2">
                                                    Analysis Value
                                                </td>
                                                <td colspan="2">
                                                    Standard Specification
                                                </td>
                                                <td rowspan="2">
                                                    Testing Method
                                                    <br />
                                                    Adopted
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    Low
                                                </td>
                                                <td>
                                                    High
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                     <asp:TextBox ID="txtApperance" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApperanceAnalysis" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApperanceLow" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtApperanceHigh" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="txtApperanceTMA" runat="server" Text="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:TextBox ID="txtOdor" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOdorAnalysis" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="txtOdorLow" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtOdorHigh" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtOdorTMA" runat="server" Text="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                     <asp:TextBox ID="txtLMenthol" runat="server" Text="0" />
                                                </td>
                                                 <td>
                                                    <asp:TextBox ID="txtLMentholAnalysis" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                   <asp:TextBox ID="txtLMentholLow" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                  <asp:TextBox ID="txtLMentholHigh" runat="server" Text="0" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtLMentholTMA" runat="server" Text="0" />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
               
            </tr>
            <tr>
             <td colspan="2" align="center">
                <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="fb8" 
                     onclick="btnConfirm_Click" />
                </td>
            </tr>
        </table>
    </div>
  </div>
</asp:Content>

