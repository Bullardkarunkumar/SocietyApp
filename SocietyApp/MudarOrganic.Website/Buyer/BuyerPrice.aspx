<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="BuyerPrice.aspx.cs" Inherits="Buyer_BuyerPrice" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">

    <script type="text/javascript">
        function selectallproduct(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
       document.getElementById('<%= this.gvProductPricDetails.ClientID %>');
            var TargetChildControl = "cbitem";

            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            //Checked/Unchecked all the checkBoxes in side the GridView.
            for (var n = 0; n < Inputs.length; ++n)
                if (Inputs[n].type == 'checkbox' &&
                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
                Inputs[n].checked = CheckBox.checked;

            //Reset Counter
            //Counter = CheckBox.checked ? TotalChkBx : 0;
        }
    </script>

    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Place Order</div>
        <div align="center">
            <asp:MultiView ID="mvPrice" runat="server" ActiveViewIndex="0">
                <asp:View ID="vproduct" runat="server">
                    <table align="center">
                        <tr>
                            <td>
                                <asp:GridView ID="gvProductSpecification" AutoGenerateColumns="False" runat="server"
                                    DataKeyNames="ProductID" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                    BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                    <RowStyle BackColor="#F7F7DE" />
                                    <Columns>
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                        <asp:BoundField DataField="Specification" HeaderText="Specification" />
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
                            <asp:Label ID="lblmsg" runat="server" Text=" Please Register Products for place order <br /> Email Mudar Organic for more Information"
                                Visible="false" />
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnAccept" runat="server" Text="Accepted" OnClick="btnAccept_Click"
                                    CssClass="fb8" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vPriceTerms" runat="server">
                    <table align="center">
                        <tr>
                            <td colspan="5">
                                &nbsp;<asp:HiddenField ID="hfFreight" runat="server" />
                                &nbsp;</td>
                        </tr>
                        <tr>
                            <td rowspan="6" style="width: 20%;">
                            </td>
                            <td align="right">
                                <asp:Label ID="lblmoneytype" runat="server" Visible="false" />
                            </td>
                            <td align="center">
                                <asp:Label ID="lblPlacedelivey" runat="server" Visible="false" />
                            </td>
                            <td align="center"><asp:Label ID="lblFoBTrasport" runat="server" Visible="false" />&nbsp;
                                <asp:Label ID="lblFoBPort" runat="server" Visible="false" />
                            </td>
                            <td rowspan="6" style="width: 15%;">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                FOB India
                            </td>
                            <td align="left">
                                &nbsp;&nbsp<asp:RadioButton ID="rbFobIndia" runat="server" GroupName="PriceTerms" />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rbFob" runat="server" RepeatDirection="Horizontal" ForeColor="Red"
                                    Visible="false">
                                    <asp:ListItem Text="Air" Value="0" Selected="True"/>
                                    <asp:ListItem Text="Sea" Value="1"/>
                                    
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                CIF by SEA
                            </td>
                            <td align="left">
                                &nbsp;&nbsp<asp:RadioButton ID="rbCIFSea" runat="server" GroupName="PriceTerms" />
                            </td>
                            <td><asp:TextBox ID="txtSeaPort" runat="server" Visible="false" Width="214px" Height="25px" /></td>
                        </tr>
                        <tr>
                            <td align="right">
                                CIF by AIR
                            </td>
                            <td align="left">
                                &nbsp;&nbsp<asp:RadioButton ID="rbCIFAir" runat="server" GroupName="PriceTerms" />
                            </td>
                            <td><asp:TextBox
                                    ID="txtAirPort" runat="server" Visible="false" Width="214px" Height="25px" /></td>
                        </tr>
                        <tr>
                            <td align="right">
                                FOR Destination
                            </td>
                            <td align="left">
                                &nbsp;&nbsp;<asp:RadioButton ID="rbForDestination" runat="server" GroupName="PriceTerms" />
                            </td>
                            <td></td>
                        </tr>
                        
                    </table>
                    <table align="center">
                        <tr>
                            <td>
                                <asp:Button ID="btnPTBack" runat="server" CssClass="fb8" OnClick="btnPTBack_Click"
                                    Text="Back" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnPTNext" runat="server" CssClass="fb8" OnClick="btnPTNext_Click"
                                    Text="Next" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vFreignTerm" runat="server">
                    <table>
                        <tr>
                            <td>
                                <div id="divFreight" runat="server" align="center">
                                    Select Payment terms
                                    <asp:RadioButtonList ID="rbfreight" runat="server">
                                        <asp:ListItem Text="100% Advance" Value="1" />
                                        <asp:ListItem Text="50% Advance + 50% against delivery" Value="2" />
                                        <asp:ListItem Text="100% against delivery" Value="3" />
                                        <asp:ListItem Text="100% payment against number of SELECTED DAYS" Value="4" />
                                    </asp:RadioButtonList>
                                </div>
                            </td>
                        </tr>
                    </table>
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnFTBack" CssClass="fb8" runat="server" Text="Back" OnClick="btnFTBack_Click" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:Button ID="btnFTNext" CssClass="fb8" runat="server" Text="Next" OnClick="btnFTNext_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vPriceGrid" runat="server">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                Below
                                <asp:Label ID="lblprice" runat="server"></asp:Label>
                            </td>
                        </tr>
                        <tr id="CreditDays" runat="server" visible="false">
                            <td align="center">
                                <%-- <asp:Label ID="lblprice" runat="server"></asp:Label>--%>
                                (Select Credit Day's
                                <asp:DropDownList ID="ddlDays" runat="server" OnSelectedIndexChanged="ddlDays_SelectedIndexChanged"
                                    AutoPostBack="True">
                                    <asp:ListItem Text="15" Value="15" />
                                    <asp:ListItem Text="30" Value="30" />
                                    <asp:ListItem Text="45" Value="45" />
                                    <asp:ListItem Text="60" Value="60" />
                                </asp:DropDownList>
                                )
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:GridView ID="gvProductPricDetails" runat="server" AutoGenerateColumns="False"
                                    DataKeyNames="ProductId,PriceId,PriceHistoryId" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                    <RowStyle BackColor="#F7F7DE" />
                                    <Columns>
                                        <asp:BoundField DataField="ProductID" HeaderText="ProductId" />
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                        <asp:BoundField DataField="FOBPrice" HeaderText="100% Advance" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FOB_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FOB_100AD" HeaderText="100% against delivery" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="FOB_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Sea" HeaderText="100% Advance" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_SEA_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_SEA_100AD" HeaderText="100% against delivery" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Sea_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air" HeaderText="100% Advance" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air_100AD" HeaderText="100% against delivery" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air_West" HeaderText="100% Advance" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air_West_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air_West_100AD" HeaderText="100% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="USA_Air_West_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Sea" HeaderText="100% Advance" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Sea_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Sea_100AD" HeaderText="100% against delivery" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Sea_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air" HeaderText="100% Advance" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air_100AD" HeaderText="100% against delivery" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air_West" HeaderText="100% Advance" DataFormatString="{0:0.00}"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air_West_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air_West_100AD" HeaderText="100% against delivery"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Europe_Air_West_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="Quantity (KG)" ItemStyle-HorizontalAlign="Right">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantity" runat="server" Width="60" Text="25" />
                                            </ItemTemplate>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Drums #25">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtP25" runat="server" Width="60" Text="0" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Drums #180">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtP180" runat="server" Width="60" Text="0" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="cbitem" runat="server" />
                                            </ItemTemplate>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="cbheader" runat="server" onclick="javascript:selectallproduct(this);" />
                                            </HeaderTemplate>
                                        </asp:TemplateField>
                                        <%--<asp:BoundField DataField="Discount" HeaderText="Discount"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>
                                        <asp:BoundField DataField="Fair" HeaderText="Organic Fair"
                                            ItemStyle-HorizontalAlign="Center">
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        </asp:BoundField>--%>
                                        <asp:BoundField DataField="IndiaPrice" HeaderText="100% Advance" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="India_50A_50D" HeaderText="50% Advance + 50% against delivery"
                                            ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="India_100AD" HeaderText="100% against delivery" ItemStyle-HorizontalAlign="Center" />
                                        <asp:BoundField DataField="India_Day" HeaderText="100% payment against number of SELECTED DAYS"
                                            ItemStyle-HorizontalAlign="Center" />
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
                            <td align="center">
                                <asp:Button ID="btnorder" runat="server" Text="Place Order" CssClass="fb8" OnClick="btnorder_Click" />
                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="btnorderWithoutPO" runat="server" Text="Order With PO" CssClass="fb8"
                                    OnClick="btnorderWithoutPO_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vUploadPO" runat="server">
                    <table width="100%" align="center">
                        <tr>
                            <td rowspan="5" style="width: 25%;">
                            </td>
                            <td align="right">
                                PO Number
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPO" runat="server" Text="" CssClass="textbox_Style" />
                            </td>
                            <td rowspan="5" style="width: 20%;">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                PO Date
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtPODate" runat="server" CssClass="textbox_Style" />
                                <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtPODate">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Upload PO&nbsp;
                            </td>
                            <td align="left">
                                <asp:FileUpload ID="FileUpload1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Comment's
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtcomments" TextMode="MultiLine" runat="server" Height="50px" Style="margin-bottom: 1px"
                                    Width="340px"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnFinish" runat="server" Text="Finish" CssClass="fb8" OnClick="btnFinish_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vOrganic" runat="server">
                    <table width="100%" align="center">
                        <tr>
                            <td align="right">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOrganic" runat="server" Text="Organic"
                                    CssClass="fb8" OnClick="btnOrganic_Click" />
                            </td>
                            <td align="left">
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnOrganicFair" runat="server" Text="Organic & Fair"
                                    CssClass="fb8" OnClick="btnOrganicFair_Click" />
                            </td>
                        </tr>
                    </table>
                </asp:View>
            </asp:MultiView>
        </div>
    </div>
</asp:Content>
