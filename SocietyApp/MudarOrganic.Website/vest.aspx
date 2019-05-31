﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="vest.aspx.cs" Inherits="vest" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Untitled Page</title>

   <%-- <script type="text/javascript">
        function checkMyStatus() {
            var sum = 0.00;
            var grid = document.getElementById('<%=gvCollecingDetails.ClientID %>');
            if (grid.rows.length > 0) {
                for (row = 1; row < grid.rows.length; row++) {
                    if (grid.rows[row].cells[7].getElementsByTagName("input")[0].checked) {
                        var Qty = parseInt(grid.rows[row].cells[6].getElementsByTagName("input")[0].value);
                        if (Qty == 0) {
                            alert("!!! Collected Quantity Should not be Zero KG !!! ");
                        }
                        else {
                            sum = sum + Qty;
                            document.getElementById('lblpresentqty').innerText = sum;
                        }
                    }
                    else {
                        document.getElementById('lblpresentqty').innerText = sum;
                    }
                }
            }
        }
    </script>
--%>
    <style type="text/css">

	.adf77ca336-cfb8-4250-8382-cc6891fdd8ec-0 {border-color:#000000;border-left-width:0;border-right-width:0;border-top-width:0;border-bottom-width:0;}
	.fc939c4326-bd6d-40ea-8c27-9585b0dde001-0 {font-size:9pt;color:#000000;font-family:Arial;font-weight:normal;text-decoration:underline;}
	.fc939c4326-bd6d-40ea-8c27-9585b0dde001-1 {font-size:9pt;color:#000000;font-family:Arial;font-weight:bold;}
	.fc939c4326-bd6d-40ea-8c27-9585b0dde001-2 {font-size:9pt;color:#000000;font-family:Arial;font-weight:normal;}
	    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <div>
            <asp:FileUpload ID="FileUpload1" runat="server" />
            <asp:Button ID="btnUpload" runat="server" Text="Upload" OnClick="btnUpload_Click" />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Has Header ?" Visible="false"></asp:Label>
            <asp:RadioButtonList ID="rbHDR" runat="server">
                <asp:ListItem Text="Yes" Value="Yes" Selected="True"></asp:ListItem>
                <asp:ListItem Text="No" Value="No"></asp:ListItem>
            </asp:RadioButtonList>
        </div>
        <div id="divFarmerRegister" runat="server" visible="false">
            
        </div>
        <div>
            <asp:GridView ID="GridView1" runat="server" Visible="true" />
        </div>
         <div>
            <asp:GridView ID="GridView2" runat="server" Visible="true"/>
        </div>
    </div>
    <asp:Label ID="lblpresentqty" runat="server" Text="0" ForeColor="DarkGreen" Visible="false"></asp:Label>
    <div id="rr" runat="server" visible="false">
        Farmer code
        <asp:TextBox ID="txtFN" runat="server"></asp:TextBox>Farmer Name
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>village<asp:TextBox ID="TextBox2"
            runat="server"></asp:TextBox><asp:Button ID="btngo" runat="server" Text="Find" OnClick="btngo_Click" />
    </div>
    <div>
    </div>
    <div>
         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
    AutoDataBind="True" Height="50px" 
        ReuseParameterValuesOnRefresh="True" Width="350px" />
    </div>
    </form>
</body>
</html>
