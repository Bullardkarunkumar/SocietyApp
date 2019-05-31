<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="ProductPriceUpdate.aspx.cs" Inherits="Admin_ProductPriceUpdate" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function DataSelectChanged(sender, args) {
            var str = sender.get_selectedDate().format('MM/dd/yyyy');
            //document.getElementById('<%= gvProductPrice.ClientID %>').innerText = CalcYear(new Date(str), new Date());

            var gridview1 = document.getElementById('<%=gvProductPrice.ClientID %>');
            for (i = 1; i < gridview1.rows.length; i++) {
                gridview1.rows[i].cells[5].innerText = str;
            }
        }
        function selectallproduct(CheckBox) {
            //Get target base & child control.
            var TargetBaseControl =
                document.getElementById('<%= gvProductPrice.ClientID %>');
            var TargetChildControl = "txtNewPrice";
            //Get all the control of the type INPUT in the base control.
            var Inputs = TargetBaseControl.getElementsByTagName("input");

            for (i = 1; i < TargetBaseControl.rows.length; i++) {
                var input_txt = TargetBaseControl.rows[i].cells[4].getElementsByTagName("input")[0];
                //                var pricevalue = 0;
                //                pricevalue = TargetBaseControl.rows[i].cells[1].innerText;
                //alert(TargetBaseControl.rows[i].cells[1].textContent + "  " + TargetBaseControl.rows[i].cells[1].innerText);
                if (typeof TargetBaseControl.rows[i].cells[3].textContent != 'undefined') {
                    input_txt.value = TargetBaseControl.rows[i].cells[2].textContent;
                    input_txt.value = input_txt.value.replace(/^\s+|\s+$/g, '');
                }
                else
                    input_txt.value = TargetBaseControl.rows[i].cells[2].innerText.trim();

                //alert(input_txt.value);
            }
            //Checked/Unchecked all the checkBoxes in side the GridView.
            //            for (var n = 0; n < Inputs.length; ++n)
            //                if (Inputs[n].type == 'textbox' &&
            //                Inputs[n].id.indexOf(TargetChildControl, 0) >= 0)
            //                Inputs[n].value = '100';

            //Reset Counter
            //Counter = CheckBox.checked ? TotalChkBx : 0;
        }
        function fnShowMessage(msg) {
            bootbox.alert(msg);
        }
    </script>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" style="height: auto;">
                <div class="panel panel-default">
                    <div id="header_Text" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                        Price Update Details
                    </div>
                    <div class="panel-body">
                        <div id="divDetailButton" align="center" runat="server" visible="false">
                            <table>
                                <tr align="center">
                                    <td>
                                        <asp:Button ID="btnDetailed" runat="server" Text="Detailed Price List" CssClass="btn btn-success"
                                            OnClick="btnDetailed_Click" />
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                        <div id="divDetailsList" runat="server" visible="false">
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Quantity</label>
                                                <div class="col-md-4">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="ddlQty" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">1 to 24</asp:ListItem>
                                                            <asp:ListItem Value="2">25 to 100</asp:ListItem>
                                                            <asp:ListItem Value="3">101 to 539</asp:ListItem>
                                                            <asp:ListItem Value="4">Above 540</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Search Date</label>
                                                <div class="col-md-4">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtFindDate" runat="server" CssClass="form-control"></asp:TextBox>
                                                        <asp:CalendarExtender
                                                            ID="dtpLevdate" runat="server" Format="MM/dd/yyyy"
                                                            TargetControlID="txtFindDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="row" style="text-align: center">
                                        <asp:Button ID="btnFOB" runat="server" Text="FOB India" CssClass="btn btn-primary" OnClick="btnFOB_Click" />
                                        <asp:Button ID="btnCIFsea" runat="server" Text="CIF by SEA"
                                            OnClick="btnCIFsea_Click" CssClass="btn btn-success" />
                                        <asp:Button ID="btnCIFair" runat="server" Text="CIF by AIR-East" CssClass="btn btn-info"
                                            OnClick="btnCIFair_Click" />
                                        <asp:Button ID="btnCIFWestair" runat="server" Text="CIF by AIR-West" CssClass="btn btn-warning" OnClick="btnCIFWestair_Click" />
                                        <asp:Button ID="btnFOR" runat="server" Text="FOR Destination" CssClass="btn btn-danger"
                                            OnClick="FOR_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divAdminprice" runat="server" visible="false">
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Us Dollor Rate</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtUsDolar" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Packing & Freight</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtTransportation" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Add-on UP Price</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtAdonupPrice" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Add on Price</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtAddonProfit" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Mandy Tax</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMandyTax" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">B. Mar</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtBMar" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Insurance</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtInsurance" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Local Tax</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtLocalTax" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtProductDate" runat="server" Enabled="false" class="form-control"></asp:TextBox>
                                                        <asp:CalendarExtender ID="dtpProductDate" runat="server" TargetControlID="txtProductDate"
                                                            OnClientDateSelectionChanged="DataSelectChanged" Format="dd/MM/yyyy">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <%-- <table align="center">
                                <tr>
                                    <td colspan="4" align="center"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblUsRate" runat="server" Text="Us Dollor Rate"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtUsDolar" runat="server">0</asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label1" runat="server" Text="Packing & Freight"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtTransportation" runat="server">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblDate" runat="server" Text="Date"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtProductDate" runat="server" Enabled="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="dtpProductDate" runat="server" TargetControlID="txtProductDate"
                                            OnClientDateSelectionChanged="DataSelectChanged" Format="dd/MM/yyyy">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <asp:Label ID="Label2" runat="server" Text="Add on Price"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddonProfit" runat="server">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtMandyTax" runat="server" Text="0" />
                                    </td>
                                    <td>B. Mar
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBMar" runat="server" Text="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>Insurance
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtInsurance" runat="server" Text="0" />
                                    </td>
                                    <td>&nbsp;Local Tax
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtLocalTax" runat="server" Text="0" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>--%>
                        </div>
                        <div class="row" style="margin-top: 15px; margin-bottom: 15px" runat="server" id="divHeaderText" visible="false">
                            <div id="divbuttondataHeader" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                                <asp:Label ID="lblDataHeader" Text="" runat="server"></asp:Label>
                            </div>
                        </div>
                        <div id="divPrices" runat="server" align="center" visible="false">
                            <table align="center">
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvAllPrices" runat="server" CssClass="table table-bordered mudargrid" AutoGenerateColumns="false">
                                            <HeaderStyle CssClass="gvheader" />
                                            <AlternatingRowStyle CssClass="gvalternate" />
                                            <RowStyle CssClass="gvnormal" />
                                            <Columns>
                                                <asp:BoundField HeaderText="ProductID" DataField="ProductId" Visible="false" />
                                                <asp:BoundField HeaderText="ProductName" DataField="ProductName" />
                                                <asp:BoundField HeaderText="UP Price" DataField="UP Price" />
                                                <asp:BoundField HeaderText="AP Price" DataField="MainBranch Price" />
                                                <asp:BoundField HeaderText="100% Advance" DataField="100% Advance" />
                                                <asp:BoundField HeaderText="50% Advance + 50% against delivery" DataField="50% Advance + 50% against delivery" />
                                                <asp:BoundField HeaderText="100% against delivery" DataField="100% against delivery" />
                                                <asp:BoundField HeaderText="15_Days" DataField="15_Days" />
                                                <asp:BoundField HeaderText="30_Days" DataField="30_Days" />
                                                <asp:BoundField HeaderText="45_Days" DataField="45_Days" />
                                                <asp:BoundField HeaderText="60_Days" DataField="60_Days" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td align="center">
                                        <asp:Button ID="btnBack" runat="server" Text="Back" Visible="false" CssClass="btn btn-success"
                                            OnClick="btnBack_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divBranch" runat="server" class="col-sm-12">
                            <div class="row" style="text-align: center;">
                                <label class="mt-checkbox mt-checkbox-outline">
                                    <asp:CheckBox ID="cbPriceD" runat="server" onclick="javascript:selectallproduct(this);" />
                                    <b>OLD Price = NEW Price</b><span style="border-color: #32c5d2;"></span>
                                </label>
                            </div>
                            <div class="row">
                                <asp:GridView ID="gvProductPrice" runat="server" CssClass="table table-bordered mudargrid" AutoGenerateColumns="False" DataKeyNames="ProductId">
                                    <Columns>
                                        <asp:BoundField HeaderText="ProductID" DataField="ProductId" />
                                        <asp:BoundField HeaderText="ProductName" DataField="ProductName" />
                                        <%-- <asp:BoundField HeaderText="Old Price" />--%>
                                        <asp:TemplateField HeaderText="Old Price">
                                            <ItemTemplate>
                                                <asp:Label ID="lblOldPrice" runat="server" Text="0"></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Old Date" DataField="CreatedDate" DataFormatString="{0:dd MMM yyyy}" />
                                        <asp:TemplateField HeaderText="New Price">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtNewPrice" runat="server" CssClass="form-control input-medium" Text="0"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Date" DataField="Date" DataFormatString="{0:dd MMM yyyy}" />
                                        <%--<asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="row" style="text-align: center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success" OnClick="btnSubmit_Click"
                                    Text="Submit" />
                            </div>
                        </div>
                        <%--<asp:TemplateField HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="lblDate" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <div id="divUpdatePrice" runat="server" visible="false">
                            <asp:DataList ID="dtUpdatedPrice" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                <FooterStyle BackColor="#CCCC99" />
                                <AlternatingItemStyle BackColor="White" />
                                <ItemStyle BackColor="#F7F7DE" />
                                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderTemplate>
                                    <table>
                                        <tr>
                                            <td rowspan="2" width="20%">ProductId
                                            </td>
                                            <td rowspan="2" width="55%">Product Name
                                            </td>
                                            <td rowspan="2" width="45%">MainBranch
                                            </td>
                                            <td rowspan="2" width="45%">SubBranch
                                            </td>
                                            <td rowspan="2" width="60%">FOB India
                                            </td>
                                            <td colspan="2" width="60%">Price(US)
                                            </td>
                                            <td colspan="2" visible="false" width="60%">Price(UK)
                                            </td>
                                            <td rowspan="2" width="60%">Indian Price
                                            </td>
                                        </tr>
                                        <tr>
                                            <th width="30%">Sea
                                            </th>
                                            <th width="30%">Air
                                            </th>
                                            <th visible="false">Sea
                                            </th>
                                            <th visible="false">Air
                                            </th>
                                        </tr>
                                    </table>
                                </HeaderTemplate>
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <ItemTemplate>
                                    <table>
                                        <tr>
                                            <th align="center" width="1500">
                                                <%#DataBinder.Eval(Container.DataItem,"ProductId") %>
                                            </th>
                                            <th width="1800" align="left">
                                                <%#DataBinder.Eval(Container.DataItem,"ProductName") %>
                                            </th>
                                            <th width="1000" align="left">
                                                <%#DataBinder.Eval(Container.DataItem,"PriceMB","{0:#}") %>
                                            </th>
                                            <th width="1000" align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "POPriceMB", "{0:#}")%>
                                            </th>
                                            <th width="250" align="right">
                                                <%#DataBinder.Eval(Container.DataItem, "FOBPrice", "{0:#}")%>
                                            </th>
                                            <th width="250" align="center">
                                                <%#DataBinder.Eval(Container.DataItem, "USA_Sea", "{0:#}")%>
                                            </th>
                                            <th width="250" align="center">
                                                <%#DataBinder.Eval(Container.DataItem, "USA_Air", "{0:#}")%>
                                            </th>
                                            <th width="450" align="center" visible="false">
                                                <%#DataBinder.Eval(Container.DataItem, "Europe_Sea", "{0:#}")%>
                                            </th>
                                            <th width="450" align="center" visible="false">
                                                <%#DataBinder.Eval(Container.DataItem, "Europe_Air", "{0:#}")%>
                                            </th>
                                            <th width="4000" align="left">
                                                <%#DataBinder.Eval(Container.DataItem, "India_Price", "{0:#}")%>
                                            </th>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:DataList>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>