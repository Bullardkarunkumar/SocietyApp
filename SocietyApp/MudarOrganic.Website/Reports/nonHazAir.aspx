<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="nonHazAir.aspx.cs" Inherits="Reports_nonHazAir" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
  <div id="content_area_Home" style="height:auto;">
  <div id="header_Text">
                Non Haz Air Report
           </div>
      <div>
          <table align="center" style="font-family: Verdana; width: 885px">
              <tr><td></td></tr>
              <%--<tr bgcolor="#ffcc66">
                  <td style="width: 885px">
                      <table align="center" style="font-family: Verdana; width: 885px">
                          <tr>
                              <td colspan="6" align="center" style="font-size: 12px;">
                                  <h1>
                                      Mudar India Exports</h1>
                              </td>
                          </tr>
                          <tr>
                              <td colspan="6" align="center" style="font-size: 10px">
                                  6-1-744, Kovur Nagar,ANANTAPUR - 515 004,Andhra Pradesh,India
                              </td>
                          </tr>
                      </table>
                  </td>
              </tr>--%>
              <tr>
                  <td align="right">
                      Date:<asp:Label ID="lblDate" runat="server" />
                  </td>
              </tr>
              <tr>
                  <td align="center">
                      Shipper’s Certificate for Non-Hazardous Cargo
                  </td>
              </tr>
              <tr>
                  <td>
                      <table width="100%" border="1">
                          <tr align="center">
                              <td width="24%">
                                  AWB no
                              </td>
                              <td width="38%">
                                  Airport of Departure
                              </td>
                              <td width="38%">
                                  Airport of Destination
                              </td>
                          </tr>
                          <tr align="center">
                              <td width="24%">
                                  <asp:TextBox ID="txtAWB" runat="server" />
                              </td>
                              <td width="38%">
                                  <asp:Label ID="lblDeparture" runat="server" />
                              </td>
                              <td width="38%">
                                  <asp:Label ID="lblDestination" runat="server" />
                              </td>
                          </tr>
                      </table>
                  </td>
              </tr>
              <tr>
                  <td>
                      This to certify that the articles / substances of this shipment are properly described
                      by name, that they are not listed in the current edition of IATA. Dangerous Goods
                      Regulation (DGR), Alphabetical list of Dangerous Goods nor do they correspond to
                      any hazard classes appearing in DGR. Section 3, classification of Dangerous Goods
                      and they are known to be not Dangerous i.e. Non restricted. Furthermore the shipper
                      confirms that the goods are in proper Condition for Transportation on Passenger
                      Carrying aircraft (DGR, para 8.1.23)
                  </td>
              </tr>
              <tr>
                  <td>
                      <asp:GridView ID="gvPurchaseOrder" DataKeyNames="ProductID" runat="server" AutoGenerateColumns="False"
                          BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                          CellPadding="4" ForeColor="Black" GridLines="Vertical" Width="875px">
                          <RowStyle BackColor="#F7F7DE" />
                          <Columns>
                                 <asp:BoundField DataField="TotalDrums" HeaderText="Number Of Packing">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="Net Wt Qty(KG)" DataFormatString="{0:n0}">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                          </Columns>
                          <FooterStyle BackColor="#CCCC99" />
                          <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                          <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                          <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                          <AlternatingRowStyle BackColor="White" />
                      </asp:GridView>
                  </td>
              </tr>
              <tr>
                  <td>
                      <table width="100%" border="1">
                          <tr align="center">
                              <td width="50%">
                                  <asp:Label ID="lblShipperAddress" runat="server" />
                              </td>
                              <td width="50%">
                                  <asp:Label ID="lblBranchAddress" runat="server" />
                              </td>
                          </tr>
                          </table>
                  </td>
              </tr>
              <tr>
                  <td>
                      <div align="center">
                          <asp:Button ID="btnConfirm" runat="server" Text="Confirm" CssClass="fb8" OnClick="btnConfirm_Click" />
                      </div>
                  </td>
              </tr>
          </table>
      </div>
 </div>
</asp:Content>

