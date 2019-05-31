<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="OrderForm.aspx.cs" Inherits="Orders_OrderForm" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
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
    <div id="content_area_Home" style="height:auto">
    <div id="header_Text">
                Place Order
            </div>
        <table>
            <tr>
                <td colspan="2">
                    Type Product name or Type "All" for all Product.
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtSproductName" autocomplete="off" runat="server" Width="250px"></asp:TextBox>
                    <asp:AutoCompleteExtender ID="txtAutoFarmerName" runat="server" TargetControlID="txtSproductName"
                        CompletionInterval="1000" EnableCaching="true" CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                        CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                        ShowOnlyCurrentWordInCompletionListItem="true" BehaviorID="AutoCompleteEx" ServicePath="~/AutoComplete.asmx"
                        ServiceMethod="GetProductCompletionList" MinimumPrefixLength="1">
                        <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                           <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                        </Animations>
                    </asp:AutoCompleteExtender>
                </td>
                <td>
                    <asp:Button ID="btnSearch" runat="server" CssClass="fb8_go" Text="GO" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </table>
        <div>
            <table>
                <tr>
                    <td>
                        <asp:GridView ID="gvProductPricDetails" runat="server" AutoGenerateColumns="False"
                            DataKeyNames="ProductId,PriceId,PriceHistoryId" BackColor="White" BorderColor="#DEDFDE"
                            BorderStyle="Solid" BorderWidth="2px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                            <RowStyle BackColor="#F7F7DE" />
                            <Columns>
                                <asp:BoundField DataField="ProductID" HeaderText="ProductId" Visible="false" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                <asp:BoundField DataField="itchscode" HeaderText="ITC HS Code" />
                                <asp:BoundField DataField="PriceID" HeaderText="PriceId" Visible="false" />
                                <asp:BoundField DataField="FOBPrice" HeaderText="FOB India ($)" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USA_Sea" HeaderText="USA SEA ($)" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="USA_Air" HeaderText="USA AIR ($)" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Europe_Sea" HeaderText="Europe SEA ($)" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="Europe_Air" HeaderText="Europe AIR ($)" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="India_Price" HeaderText="INDIA (RS)" DataFormatString="{0:0}"
                                    ItemStyle-HorizontalAlign="Right">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Quantity (KG)" ItemStyle-HorizontalAlign="Right">
                                    <ItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" Width="60" Text="25" />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="cbitem" runat="server" />
                                    </ItemTemplate>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="cbheader" runat="server" onclick="javascript:selectallproduct(this);" />
                                    </HeaderTemplate>
                                </asp:TemplateField>
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
                        <asp:Button ID="btnOrder" CssClass="fb8" runat="server" Text="Place Order" 
                            OnClick="btnOrder_Click" Font-Size="Medium" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>

