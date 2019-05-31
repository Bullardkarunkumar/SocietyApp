<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="BranchComplaints.aspx.cs" Inherits="Mudar_BranchComplaints" %>

<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height: auto">
 <div id="header_Text">
            Branch Complaint List
        </div>
        <div>&nbsp;</div>
        <div id="divgvCompliant" runat="server" align="center">
        <asp:Button ID="btnComplaint" CssClass="fb8" runat="server" Text="New Complaint" onclick="btnComplaint_Click"/>
        <asp:GridView ID="gvBranchComplaint" DataKeyNames="ComplaintID" AutoGenerateColumns="False"
                runat="server" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                CellPadding="4" ForeColor="Black" GridLines="Vertical" 
                OnRowCommand="gvBranchComplaint_RowCommand" >
                <RowStyle BackColor="#F7F7DE" />
                <Columns>
                    
                    <asp:BoundField DataField="CreatedDate" HeaderText="Date of Complaint" DataFormatString="{0:dd MMM yyyy}" />
                    <asp:BoundField DataField="Complaint" HeaderText="Complaint" />
                    <asp:BoundField DataField="InvoiceId" HeaderText="Inv No" />
                    <asp:BoundField DataField="InvoiceDate" HeaderText="Invoice Date" />
                    <asp:BoundField DataField="InvoiceProductID" HeaderText="Product" />
                    <asp:BoundField DataField="ComplaintBy" HeaderText="Complained By" />
                    <asp:BoundField DataField="Action" HeaderText="Action Taken" />
                    <asp:ButtonField ButtonType="Link" HeaderText="View" Text="view" CommandName="cmd_Select"/>
                </Columns>
                <FooterStyle BackColor="#CCCC99" />
                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                <AlternatingRowStyle BackColor="White" />
            </asp:GridView>
        </div>
        <div>&nbsp;</div>
        <div id="divComplaintForm" runat="server"><table>
                    <tr><td colspan="4"><asp:Label ID="lblBuyerID" runat="server" Visible="false" Text="" /></td></tr>
                    <tr>
                        <td rowspan="9" style="width: 15%;">
                        </td>
                        <td align="right">
                            Complained By
                        </td>
                     <td align="center">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCompBy" runat="server" Height="30px" Width="340px" 
                                    Font-Size="Medium" Style="margin-bottom: 1px"></asp:TextBox></td>
                        <td rowspan="9" style="width: 15%;">
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            Po No
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtInvno" runat="server" Height="30px"
                                Style="margin-bottom: 1px" Width="340px" Font-Size="Medium" 
                                ontextchanged="txtInvno_TextChanged" AutoPostBack="true"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Po Date
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtInvDate" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Products
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlProduct" runat="server" Height="35px"
                                                        Width="345px" Font-Size="Medium" TabIndex="4">
                                                    </asp:DropDownList></td>
                    </tr>
                     <tr>
                         <td align="right">
                            Batch No if Applicable
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtBatch" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Qty of the product in KG
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtQty" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"></asp:TextBox></td>
                    </tr>
                   
                    <tr>
                         <td align="right">
                            Complaint 
                        </td>
                        <td align="center"> 
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtComplaintDesc" runat="server" 
                                Height="52px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr>
                        <td align="right">
                            Action Taken
                        </td>
                        <td align="center">
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtAction" runat="server" Height="52px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium" TextMode="MultiLine"></asp:TextBox></td>
                    </tr>
                    <tr> 
                    <td align="right">Any Proof</td>
                    <td  align="center">
                    &nbsp;&nbsp;&nbsp;<asp:FileUpload ID="fuBGLCInfo" runat="server" />
                         &nbsp;&nbsp;<asp:Button ID ="btnBGLCInfo" runat="server" Text ="Upload" 
                            CssClass="fb8" Visible="False"  />
                            
                           </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                </table>
                <table width="885">
                    <tr>
                        <td></td>
                        <td align="center">
                            <asp:Button ID="btnSubmit" CssClass="fb8" runat="server" Text="Submit" onclick="btnSubmit_Click" 
                                />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnBack" CssClass="fb8" runat="server" Text="Back" onclick="btnBack_Click"/>
                        </td>
                        <td align="center">
                            
                        </td>
                    </tr>
                </table>
                </div>
                 
</div>
</asp:Content>

