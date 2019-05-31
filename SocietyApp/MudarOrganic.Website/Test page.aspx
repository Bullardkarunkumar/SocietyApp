<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Test page.aspx.cs" Inherits="Test_page" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
    
    <div>
        <asp:FileUpload ID="FileUpload1" runat="server" />
        <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Has Header ?"></asp:Label>
        <asp:RadioButtonList ID="rbHDR" runat="server">
            <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
            <asp:ListItem Text="No" Value="No"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:GridView ID="GridView1" runat="server" OnPageIndexChanging="PageIndexChanging" />
    </div>
    <div id="divFarmerRegister" runat="server" visible="false">
    </div>
    <div>
        <asp:GridView ID="gvFarmer" runat="server" Visible="false">
        </asp:GridView>
    </div>
    <div>
        <asp:GridView ID="gvFarmerDetails" runat="server" />
    </div>
    </form>
</body>
</html>
