<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FarmerHome.aspx.cs" Inherits="FarmerHome" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Button ID="Button1" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="Button2" runat="server" Text="Button" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Button ID="btnNewFarmer" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="btnPlantation" runat="server" Text="Button" />
                </td>
            </tr><tr>
                <td>
                    <asp:Button ID="btnUpdateFarmer" runat="server" Text="Button" />
                </td>
                <td>
                    <asp:Button ID="btnInspection" runat="server" Text="Button" />
                </td>
            </tr>
            
        </table>
    </div>
    </form>
</body>
</html>
