<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="CoverLetter.aspx.cs" Inherits="CoverLetter" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
 <div id="content_area_Home" style="height:auto;">
            <div id="header_Text">
                Sample Cover Letter
           </div>
        <div>
        <table width="100%" align="center" style="font-family:Verdana">
        <tr>
        <td width="50%"></td><td width="50%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date&nbsp; : <asp:Label ID="lblTodayDate" runat="server"></asp:Label></td>
        </tr>
        <tr>
        <td colspan="2" align="left"><asp:Label ID="lblCompanyAddress" runat="server"></asp:Label>
           
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td colspan="2" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Attn&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;&nbsp;
        &nbsp;<asp:TextBox 
                ID="txtAttn" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"/></td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td colspan="2" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dear Sir&nbsp; :&nbsp;&nbsp;         
            <asp:TextBox 
                ID="txtDearsir" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"/></td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td  width="70%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sub : Original Documents against our Inv No :<asp:Label 
                ID="lblInvoiceNo" runat="server"></asp:Label>
            </td>
            <td width="30%">Date&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;<asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td width="70%" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ref&nbsp; : Your PO Number :&nbsp;<asp:Label ID="lblPO" runat="server"></asp:Label></td>
            <td width="30%">PO Date&nbsp;&nbsp;&nbsp;&nbsp; :&nbsp;<asp:Label ID="lblPODate" runat="server"></asp:Label></td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td colspan="2" align="center">We have dispatched the below mentioned items as per the PO and Inv referances given above:</td>
        </tr>
        <tr>
        <td colspan="2" align="center">&nbsp;
        <asp:GridView ID="gvPurchaseOrder" DataKeyNames="ProductID" runat="server" AutoGenerateColumns="False" BackColor="White"
                            BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                            GridLines="Vertical" Width="875px">
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                 <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <asp:Label ID="lblsnnumber" runat="server" Text=" <%#Container.DisplayIndex+1 %>" />
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" >
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Quantity" HeaderText="Qty in KG" 
                                    DataFormatString="{0:n0}">
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
        <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" align="center">We are herewith enclosing the below mentioned 
            original documents:</td>
        </tr>
        <tr>
        <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" align="center">
        <asp:GridView ID="gvReports" runat="server" AutoGenerateColumns="False" Width="878px"
                        BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                        CellPadding="4" ForeColor="Black" GridLines="Vertical">
                        <RowStyle BackColor="#F7F7DE" />
                        <Columns>
                            <asp:TemplateField HeaderText="S.No" HeaderStyle-HorizontalAlign="center" HeaderStyle-Width="80px" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:Label ID="lblsno" runat="server" Text=" <%#Container.DisplayIndex+1 %>" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="Center" Width="80px"></HeaderStyle>
                            </asp:TemplateField>
                            
                            <asp:BoundField DataField="ReportName" HeaderText="Documnet" HeaderStyle-HorizontalAlign="Center">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:BoundField>
                             <asp:TemplateField HeaderText="Check" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbDoc" runat="server" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="No Of Copies" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:TextBox ID="txtNoOfCopies" runat="server" Text="" width="40%" />
                                </ItemTemplate>
                                <HeaderStyle HorizontalAlign="center"></HeaderStyle>
                            </asp:TemplateField>
                           
                        </Columns>
                        <FooterStyle BackColor="#CCCC99" />
                        <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                        <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <AlternatingRowStyle BackColor="White" />
                    </asp:GridView></td>
        </tr>
        <tr>
        <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" align="center">Please acknowledge the receipt of the documents.</td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <%--<tr>
        <td colspan="2" align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Thanks & Regards<br />
            <br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            Sudheer - President<br />
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            MudarIndia Exports&nbsp; 			
                                        </td>
        </tr>--%>
        <tr><td colspan="2" align="center"><asp:Button ID="btnCoverSubmit"  CssClass="fb8" runat="server" Text="Submit" 
                        onclick="btnCoverSubmit_Click" />&nbsp;</td></tr>
        </table>
        </div>
</asp:Content>

