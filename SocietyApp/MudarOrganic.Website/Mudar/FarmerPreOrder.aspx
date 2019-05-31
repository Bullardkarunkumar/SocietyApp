<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FarmerPreOrder.aspx.cs" Inherits="Farmer_FarmerPreOrder" %>

<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <style type="text/css">
        #ctl00_body_cph_chkICSList label {
            padding-left: 5px;
        }
        #ctl00_body_cph_chkICSListInner label {
            padding-left: 5px;
        }
        #ctl00_body_cph_chkVillageist label {
            padding-left: 5px;
        }
    </style>

    <script type="text/javascript">
        function calculateQuantity() {
            var chktest1 = 0;
            var X = 0;
            var str = '';
            var Grid_Table = document.getElementById('<%= gvCollecingDetails.ClientID %>');
            for (var row = 1; row < Grid_Table.rows.length; row++) {
                var col2 = Grid_Table.rows[row].cells[8];
                for (var j = 0; j < col2.childNodes.length; j++) {
                    if (col2.childNodes[j].type == "checkbox") {
                        if (col2.childNodes[j].checked) {
                            var col1 = Grid_Table.rows[row].cells[7];
                            for (var k = 0; k < col1.childNodes.length; k++) {
                                if (col1.childNodes[k].type == "text") {
                                    if (!isNaN(col1.childNodes[k].value) && col1.childNodes[k].value != "") {
                                        var curr = parseFloat(col1.childNodes[k].value);
                                        var availQty = parseFloat(Grid_Table.rows[row].cells[6].innerText);
                                        if (curr == 0) {
                                            alert('!!! Quantity must be atleast one KG !!! ');
                                            col2.childNodes[j].checked = false;
                                        }
                                        else if (/^\d{1,5}(\.\d{1,2})?$/.test(col1.childNodes[k].value) == false) {
                                            alert('!!! Please Enter valid Quantity !!! ');
                                            col2.childNodes[j].checked = false;
                                        }
                                        else if (curr > availQty) {
                                            alert('Plz Check the Available Quantity');
                                            col2.childNodes[j].checked = false;
                                        }
                                        else {
                                            X += parseFloat(col1.childNodes[k].value);
                                        }
                                    }
                                    else {
                                        col1.childNodes[k].value = '0';
                                        alert('!!! Please Enter the Quantity !!!');
                                    }
                                }
                            }
                        }
                    }
                }
            }
            document.getElementById('<%=lblpresentqty.ClientID%>').innerText = X;
            return false;
        }
    </script>
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Collecting Details
        </div>
        <div id="divCollcetionGrid" runat="server">
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:Button ID="btnNewPreOrder" runat="server" Text="New Pre-Order" CssClass="fb8" OnClick="btnNewPreOrder_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:CheckBoxList ID="chkICSList" AutoPostBack="true" CellPadding="4" CellSpacing="4" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" OnSelectedIndexChanged="chkICSList_SelectedIndexChanged"></asp:CheckBoxList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvPreOrder" runat="server"
                            DataKeyNames="CollectionTransactionID" AutoGenerateColumns="False"
                            CssClass="grid-view" OnRowCommand="gvPreOrder_RowCommand" EnableModelValidation="True">
                            <RowStyle HorizontalAlign="Center" />
                            <Columns>
                                <asp:ButtonField DataTextField="Blending_BatchID" ButtonType="Link" CommandName="PreOrder" HeaderText="Lot Number" />
                                <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                <asp:BoundField DataField="TotalQty" HeaderText="Qty" />
                                <asp:BoundField DataField="CreatedDate" HeaderText="Date" DataFormatString="{0:dd MMM yy}" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: center">
                        <asp:Button ID="btnNavPreorderFirst" Style="word-break: break-all; word-wrap: break-word" CssClass="fb8" runat="server" Text="Back to Pre-Order Collection" OnClick="btnNavPreorderFirst_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divCollectingDetails" runat="server" align="center">
            <table align="center">
                <tr>
                    <td colspan="4" align="center">
                            <asp:CheckBoxList ID="chkICSListInner" AutoPostBack="true" CellPadding="4" CellSpacing="4" runat="server" RepeatDirection="Horizontal" RepeatColumns="4" OnSelectedIndexChanged="chkICSListInner_SelectedIndexChanged"></asp:CheckBoxList>
                            <asp:CheckBoxList ID="chkVillageist" AutoPostBack="true" CellPadding="4" CellSpacing="4" runat="server" RepeatDirection="Horizontal" RepeatColumns="5" Visible="false" OnSelectedIndexChanged="chkVillageist_SelectedIndexChanged"></asp:CheckBoxList>
                        </td>
                </tr>
                <tr id="trProductName" runat="server" visible="false">
                    <td rowspan="4" style="width: 10%;"></td>
                    <td align="right">Product Name
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlSelectProduct" runat="server" AutoPostBack="true"
                        Font-Size="Medium" Height="35px" OnSelectedIndexChanged="ddlSelectProduct_SelectedIndexChanged"
                        TabIndex="9" Width="345px">
                    </asp:DropDownList>
                    </td>
                    <td rowspan="4" style="width: 0%;" align="left"></td>
                </tr>
                <div id="trCollect" runat="server" visible="false">
                    <tr>
                        <td align="right">Required Quantity
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOrderQuantity" runat="server" Height="30px"
                            Style="margin-bottom: 1px" Width="340px" Font-Size="Medium" TabIndex="5"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Other Farmer Name
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtOtherFarmers" runat="server" Height="30px"
                            Style="margin-bottom: 1px" Width="340px" Font-Size="Medium" TabIndex="5"></asp:TextBox>More
                            than One Farmer Added<b><span style="color: Red">&#39;;&#39;</span></b>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Other Farmer Quantity
                        </td>
                        <td align="left">&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtCollectQTY" runat="server" Height="30px" Style="margin-bottom: 1px"
                            Width="340px" Font-Size="Medium" TabIndex="5"></asp:TextBox>More than One Farmer
                            Qty Added<b><span style="color: Red">&#39;;&#39;</span></b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" align="Center">Collected Quantity<br />
                            <asp:Label ID="lblpresentqty" runat="server" ForeColor="Red" Text="0"></asp:Label>
                        </td>
                    </tr>
                </div>
            </table>
            <div id="divgvCollectDetails" runat="server" visible="false">
                <table align="center">
                    <tr>
                        <td colspan="6">
                            &nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="6">
                            <asp:GridView ID="gvCollecingDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="PlantationId,FarmerID,FarmID" EnableModelValidation="True" CssClass="grid-view">
                                <RowStyle HorizontalAlign="Center" />
                                <Columns>
                                    <%--<asp:BoundField DataField="FarmerId" HeaderText="Farmer ID" />
                            <asp:BoundField DataField="FarmID" HeaderText="Farm ID" />--%>
                                    <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="AreaCode" HeaderText="PlotCode" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Lot_No" HeaderText="Batch No" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="TotalProductQuantity" HeaderText="Actual Quanity" HeaderStyle-HorizontalAlign="Center">

                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundField>

                                    <asp:BoundField DataField="SoldTotalQty" HeaderText="Sold Quanity" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="Avaliable" HeaderText="Avaliable Quanity" ItemStyle-ForeColor="DarkGreen" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:BoundField>
                                    <asp:TemplateField HeaderText="Collect Quanity" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtCollectQty" runat="server" Text="0" Width="90px"></asp:TextBox>
                                            <itemstyle horizontalalign="Center"></itemstyle>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Collect" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbCollecting" runat="server" AutoPostBack="false" onclick="javascript:calculateQuantity()" OnCheckedChanged="cbCollecting_CheckedChanged1" />
                                            <itemstyle horizontalalign="Center"></itemstyle>
                                        </ItemTemplate>

                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    </asp:TemplateField>
                                </Columns>
                          <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">LotNumber :<asp:Label ID="lblBatchID" runat="server" ForeColor="#FF6600"></asp:Label>
                        </td>
                        <td colspan="3" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                            &nbsp;<asp:Button ID="btnGenerateBatchID" CssClass="fb8" runat="server" Text="Generate Lot"
                                OnClick="btnGenerateBatchID_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center">&nbsp;</td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
                            <asp:Button ID="btncollectSubmit" CssClass="fb8" runat="server" Text="Submit" OnClick="btncollectSubmit_Click" />

                        </td>
                        <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnBack" CssClass="fb8" runat="server" Text="Back" OnClick="btnBack_Click" /></td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:Button ID="btnNavPreorderLast" Style="word-break: break-all; word-wrap: break-word" CssClass="fb8" runat="server" Text="Back to Pre-Order Collection" OnClick="btnNavPreorderLast_Click" />
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
</asp:Content>

