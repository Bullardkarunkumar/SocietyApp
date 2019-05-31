<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="SuperAdminOrders.aspx.cs" Inherits="Admin_SuperAdminOrders" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript">
        function fnShowMessage(msg) {
            bootbox.alert(msg);
        }
    </script>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" style="height: auto">
                <div class="page-content-inner">
                    <div class="note note-success" style="text-align: center">
                        <div class="form-inline">
                            <div class="form-group">
                                <label class="control-label">Order Status</label>
                                <asp:DropDownList ID="ddlOrderStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="ALL" Value="ALL" />
                                    <asp:ListItem Text="NEW" Value="NEW" />
                                    <asp:ListItem Text="INPROCESS" Value="INPROCESS" />
                                    <asp:ListItem Text="SHIPPING" Value="SHIPPING" />
                                    <asp:ListItem Text="HOLD" Value="HOLD" />
                                    <asp:ListItem Text="PENDING" Value="PENDING" />
                                    <asp:ListItem Text="DISPATCH" Value="DISPATCH" />
                                    <asp:ListItem Text="CANCEL" Value="CANCEL" />
                                    <asp:ListItem Text="CLOSE" Value="CLOSE" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="portlet box green">
                    <div class="portlet-title">
                        <div class="caption">
                            <i class="fa fa-cogs"></i>Orders List
                        </div>
                    </div>
                    <div class="portlet-body flip-scroll">
                        <asp:Repeater ID="dlOrderList" runat="server"
                            OnItemCommand="dlOrderList_ItemCommand"
                            OnItemDataBound="dlOrderList_ItemDataBound">
                            <HeaderTemplate>
                                <table class="table table-bordered mudargrid">
                                    <thead class="flip-content">
                                        <tr>
                                            <th>Details
                                            </th>
                                            <th nowrap="nowrap">Order ID</th>
                                            <th nowrap="nowrap">Order Type
                                            </th>
                                            <th>Buyer Name
                                            </th>
                                             <th>Branch
                                            </th>
                                            <th>Status
                                            </th>
                                            <th>Order Date
                                            </th>
                                            <th>Buyer PO
                                            </th>
                                            <th></th>
                                        </tr>
                                    </thead>
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr>
                                    <td>
                                        <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                            runat="server" />
                                    </td>
                                    <td>
                                        <asp:Label Visible="false" ID="lblorderid" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:Label>
                                        <asp:Label Visible="false" ID="lblOrderAssign" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderAssign")%>'></asp:Label>                                        
                                        <asp:LinkButton ID="lbtnOrderID" CommandName="BranchOrder" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'
                                            Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:LinkButton>
                                    </td>
                                    <td>
                                        <asp:Label ID="lblType" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderType")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBuyerName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerCompanyName")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblBranch" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "BranchCode")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' />
                                    </td>
                                    <td>
                                        <asp:Label ID="lblDateOfOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:dd MMM yyyy}")%>' />
                                    </td>
                                    <td>
                                        <asp:HyperLink ID="hlPDF" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>'
                                            runat="server">Open</asp:HyperLink>
                                        <asp:HiddenField ID="hfOrderPdf" Visible="false" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>' />
                                        <asp:LinkButton ID="lbtnOrderpdfDownload" Visible="false" runat="server" CommandName="Download" Text="Download" />
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAssign" runat="server" CssClass="btn btn-success" Text="Assign" CommandName="assign" Visible="false" />
                                        <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-success" Text="Cancel" CommandName="cancel" />
                                    </td>
                                </tr>
                                <tr id="trSubTable" runat="server" style="display: none">
                                    <td colspan="9">
                                        <asp:GridView ID="gvNewOrder" runat="server" AutoGenerateColumns="false" CssClass="subgrid">
                                            <Columns>
                                                <asp:BoundField HeaderText="ProductID" DataField="ProductID" Visible="false" />
                                                <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                                <asp:BoundField HeaderText="Qty" DataField="Quantity" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Price/KG" DataField="RateforKG" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Amt" DataField="TotalPrice" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:0.00}" />
                                                <asp:BoundField HeaderText="Drums(25)" DataField="Packing25" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="Drums(180)" DataField="Packing180" ItemStyle-HorizontalAlign="Center" />
                                                <asp:BoundField HeaderText="" DataField="Country" ItemStyle-HorizontalAlign="Center" />
                                            </Columns>
                                        </asp:GridView>
                                        <asp:Label ID="lblMSg" runat="server" Visible="false"></asp:Label>
                                        <asp:Label ID="lblCourier" runat="server" Visible="false"></asp:Label>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </table>
                            </FooterTemplate>
                        </asp:Repeater>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hdnBranchesJson" runat="server" Value="" />
        </ContentTemplate>
    </asp:UpdatePanel>
    <script>
        function fnShowBranches(orderId) {
            //debugger;
            var data = JSON.parse($('#<%= hdnBranchesJson.ClientID%>').val());
            var datatoshow = [{ text: 'Select Branch', value: '' }];
            for (var idx = 0; idx < data.length; idx++) {
                var obj = new Object()
                obj.text = data[idx].Bname;
                obj.value = data[idx].BranchId;
                datatoshow.push(obj);
            }
            //alert(datatoshow.length);
            bootbox.prompt({
                title: "<b>Select Branch</b>",
                inputType: 'select',
                inputOptions: datatoshow,
                callback: function (result) {
                    //alert(result);
                    //console.log(result);
                    $.ajax({
                        method: "POST",
                        url: "/Mudar/MudarAjaxHandler.aspx?type=assignbranch&bid=" + result + "&oid=" + orderId,
                        success: function (response) {
                            if (response == 1) {
                                window.location.href = window.location.href;
                            }
                            //alert(response);
                        },
                        error: function (response) { }
                    });
                }
            });
            return false;
        }
    </script>
</asp:Content>
