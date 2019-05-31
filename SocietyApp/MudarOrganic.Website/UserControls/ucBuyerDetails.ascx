<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ucBuyerDetails.ascx.cs" Inherits="UserControls_UCBuyerDetails" %>
<style type="text/css">
    .style1
    {
        width: 100%;
    }
    .style2
    {
        width: 120px;
    }
</style>
    <div id="content_area">
        
        <table class="style1">
            <tr>
                <td class="style2" rowspan="7">
                    <asp:ListBox ID="lbOrdersList" runat="server" Font-Bold="False" 
                        Font-Names="Verdana" Height="184px" 
                        onselectedindexchanged="ListBox1_SelectedIndexChanged" Width="129px">
                        <asp:ListItem>MT</asp:ListItem>
                    </asp:ListBox>
                </td>
                <td>
                    Inovice</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Packing List</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Non-Haz Sea</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Non-Haz Air</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Cover Leter</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    FIR Cover Leter</td>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td>
                    Label&#39;s</td>
                <td>
                    &nbsp;</td>
            </tr>
            
             <tr colspan="3">
                <td colspan="3"> &nbsp;</td>   
            </tr>
            <tr colspan="3">
                <td colspan="3" align="center"> Certificate of Analysis</td>   
            </tr>
             <tr colspan="3">
                <td colspan="3" align="center"> 
                    <asp:GridView ID="GridView1" runat="server">
                    </asp:GridView>
                </td>   
            </tr>
        </table>
        
    </div>
    
    
    
