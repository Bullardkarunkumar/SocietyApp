<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="UserDetails.aspx.cs" Inherits="Admin_UserDetails" Title="Mudarorganic-UserDetails" %>
<%@ MasterType VirtualPath="~/MudarMaster.master"%>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
    <div id="content_area_Home" style="height: auto;">
        <div id="header_Text">
           User Information
        </div>
    <div>
    <table align="center">
        <tr>
              <td colspan="2" align="center">
                <asp:Button ID="btnTAdmin" runat="server" Text="Admin" CssClass="btnFarmer" 
                      onclick="btnTAdmin_Click"/> 
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnTBranch" runat="server" Text="Branch" CssClass="btnFarmer" 
                    onclick="btnTBranch_Click" />
            </td>
            <td colspan="2" align="center">
                <asp:Button ID="btnTBuyer" runat="server" Text="Buyer" CssClass="btnFarmer" 
                    onclick="btnTBuyer_Click"/>
            </td>
            <td colspan="2" align="left">
                <asp:Button ID="btnTOthers" runat="server" Text="Others" CssClass="btnFarmer" 
                    onclick="btnTOthers_Click"/> 
            </td>
        </tr>
    </table>
    </div>
    <div id="divgvUserDetails" runat="server" align="center">
        <table align="center"><tr><td><asp:Label ID="lblUserID" runat="server" Visible="false"></asp:Label>&nbsp;<asp:Label ID="lblRoleID" runat="server" Visible="false"></asp:Label></td></tr>
        <tr><td><asp:GridView ID="gvUserDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="UserId"
                onrowcommand="gvUserDetails_RowCommand">
        <Columns>
         <asp:BoundField DataField="userid" HeaderText="User ID" Visible="false" />
         <asp:BoundField DataField="BranchCode" HeaderText="Branch Code" HeaderStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="Bname" HeaderText="Branch Name" HeaderStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="EmployeeFristName" HeaderText="Employee Name" HeaderStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="UserLoginID" HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" />
         <asp:BoundField DataField="UserPassword" HeaderText="Password" HeaderStyle-HorizontalAlign="Center"/>
         <asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="View" CommandName="cmd_edit"
                                 ItemStyle-HorizontalAlign="Center" />
        </Columns>
          <HeaderStyle CssClass="gvheader" />
          <AlternatingRowStyle CssClass="gvalternate" />
          <RowStyle CssClass="gvnormal" />
        </asp:GridView></td></tr>
        </table></div>
        
        <div id="divBueyrDetails" runat="server" align="center">
            <asp:GridView ID="gvBuyerDetails" runat="server" AutoGenerateColumns="false" DataKeyNames="UserId"
                OnRowCommand="gvBuyerDetails_RowCommand">
                <Columns>
                    <asp:BoundField DataField="userid" HeaderText="User ID" Visible="false" />
                    <asp:BoundField DataField="BuyerCompanyName" HeaderText="Company Name" />
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
        
        <div id="divICSsupplierdetails" runat="server" align="center">
            <asp:GridView ID="gvICSsupplier" runat="server" AutoGenerateColumns="false" DataKeyNames="SupplierId" OnRowCommand="gvICSsupplier_RowCommand">
                <Columns>
                    <asp:BoundField DataField="SupplierId" HeaderText="User ID" Visible="false" />
                    <asp:BoundField DataField="SupplierCompanyName" HeaderText="SupplierCompanyName" />
                    <asp:BoundField DataField="UserLoginID" HeaderText="User Name" HeaderStyle-HorizontalAlign="Center" />
                    <asp:BoundField DataField="UserPassword" HeaderText="Password" HeaderStyle-HorizontalAlign="Center" />
                    <asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="View" CommandName="cmd_edit_Supplier"
                        ItemStyle-HorizontalAlign="Center" />
                </Columns>
                <HeaderStyle CssClass="gvheader" />
                <AlternatingRowStyle CssClass="gvalternate" />
                <RowStyle CssClass="gvnormal" />
            </asp:GridView>
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
                </tr>
                <tr>
                    <td align="right">
                        Password
                    </td>
                    <td align="center" style="margin-left: 40px">
                        &nbsp;&nbsp;<asp:TextBox ID="txtPassword" ValidationGroup="Category" runat="server"
                            Font-Size="Medium" Height="30px" Style="margin-bottom: 1px" Width="340px" TabIndex="2"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
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

