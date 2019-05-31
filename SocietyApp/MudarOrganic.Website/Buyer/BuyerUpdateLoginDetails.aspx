<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="BuyerUpdateLoginDetails.aspx.cs" Inherits="Buyer_BuyerUpdateLoginDetails" Title="Untitled Page" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
 <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
           Buyer Login Information
        </div>
     <div align="right">
         <asp:Label ID="lblUserID" runat="server" Visible="false"></asp:Label>
         <asp:LinkButton ID="lbtnBuyerlogo" runat="server" Text="Upload Logo" OnClick="lbtnBuyerlogo_Click" /></div>
     <div id="divUploadlogo" runat="server">
         <table align="center">
            <tr>
                <td colspan="4">
                
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Image ID="imgBuyerLogo" runat="server" Height="130px" Width="600px"  />
                </td>
            </tr>
            <tr>
                <td colspan="4">
                
                </td>
            </tr>
             <tr>
                 <td>
                     Buyer Logo :
                 </td>
                 <td>
                     <asp:FileUpload ID="fuBLogo" runat="server" />
                 </td>
                 <td>
                     <asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="fb8" 
                         onclick="btnUpload_Click" />
                        
                     <asp:Button ID="btnDisableBInvoice" runat="server" Text="Upload" CssClass="fb8_disable"
                         Visible="false" />
                 </td>
             </tr>
         </table>
     </div>
     <div id="divBueyrDetails" runat="server" align="center">
         <asp:GridView ID="gvBuyerDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="UserId"
             OnRowCommand="gvBuyerDetails_RowCommand">
             <Columns>
                 <asp:BoundField DataField="userid" HeaderText="User ID" Visible="false" />
                 <asp:BoundField DataField="BuyerCompanyName" HeaderText="Company Name" Visible="false" />
                 <asp:BoundField DataField="UserLoginID" HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" />
                 <asp:BoundField DataField="UserPassword" HeaderText="Password" HeaderStyle-HorizontalAlign="Center" />
                 <asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="View" CommandName="cmd_edit_Buyer"
                     ItemStyle-HorizontalAlign="Center" />
             </Columns>
             <HeaderStyle CssClass="gvheader" />
             <AlternatingRowStyle CssClass="gvalternate" />
             <RowStyle CssClass="gvnormal" />
         </asp:GridView>
     </div>
     <div>
     </div>
     <div id="divUserEdit" align="center" runat="server">
         <table>
             <tr>
                 <td align="center" colspan="2" style="background-color: #CE5D5A; color: White; font-size: medium;
                     font-weight: bolder;">
                     &nbsp;Update Login Details
                 </td>
             </tr>
             <tr>
                 <td colspan="2">
                 </td>
             </tr>
             <tr>
                 <td align="right">
                     User Name
                 </td>
                 <td align="center" style="margin-left: 40px">
                     &nbsp;&nbsp;<asp:TextBox ID="txtUsername" runat="server" Font-Size="Medium" Height="30px"
                         Style="margin-bottom: 1px" Width="340px" TabIndex="1"></asp:TextBox></td>
                 <td>
                     <asp:LinkButton ID="lbtnCheck" runat="server" Text="Check Availablity" OnClick="lbtnCheck_Click"></asp:LinkButton>&nbsp;</br><asp:Label
                         ID="lblCheckuser" runat="server" Visible="false" Text=" Is Not Available!!! Try it New One!!!"
                         ForeColor="Red" />
                 </td>
             </tr>
             <tr>
                 <td align="right">
                     Password
                 </td>
                 <td align="center" style="margin-left: 40px">
                     &nbsp;&nbsp;<asp:TextBox ID="txtPassword" ValidationGroup="Category" runat="server"
                         Font-Size="Medium" Height="30px" Style="margin-bottom: 1px" Width="340px" TabIndex="2"></asp:TextBox></td>
                 <td>
                 </td>
             </tr>
             <tr>
                 <td colspan="3">
                 </td>
             </tr>
             <tr>
                 <td>
                 </td>
                 <td align="left">
                     &nbsp;&nbsp;
                     <asp:Button ID="btnUserSubmit" runat="server" Text="Submit" CssClass="fb8" OnClick="btnUserSubmit_Click" />
                     &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                     <asp:Button ID="btnUserClear" runat="server" Text="Clear" CssClass="fb8" OnClick="btnUserClear_Click" />
                 </td>
             </tr>
         </table>
     </div>
 </div>
</asp:Content>

