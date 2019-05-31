<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="OrderHistory.aspx.cs" Inherits="Buyer_OrderHistory" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <style>
        th {
            text-align: center;
        }
    </style>
    <asp:UpdatePanel ID="upd1" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <div class="portlet box green" id="divplaceorder" runat="server" visible="false">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-upload"></i>Upload PO
                    </div>
                </div>
                <div class="portlet-body flip-scroll">
                    <asp:Label ID="lblMsg" runat="server" ForeColor="Red" />
                    <div class="row">
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-8">
                            <div class="form-group">
                                <asp:TextBox placeholder="Enter PO Number" ID="txtPO" runat="server" Text="" CssClass="form-control form-control-solid placeholder-no-fix" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-8">
                            <div class="form-group">
                                <asp:TextBox placeholder="Enter PO Date" ID="txtPODate" runat="server" CssClass="form-control form-control-solid placeholder-no-fix" />
                                <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtPODate"></asp:CalendarExtender>
                            </div>
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-8">
                            <div class="form-group">
                                <%--<asp:FileUpload ID="FileUpload1" runat="server" />--%>
                                <asp:AsyncFileUpload OnClientUploadError="uploadError"
                                    OnUploadedComplete="AsyncFileUpload1_UploadedComplete"
                                    OnClientUploadComplete="uploadComplete" runat="server"
                                    ID="AsyncFileUpload1" UploaderStyle="Traditional"
                                    UploadingBackColor="#CCFFFF" />
                                <%--<asp:Button ID="btnUpload" runat="server" Text="Upload" CssClass="btn btn-primary" OnClick="btnUpload_Click" />--%>
                                <div style="display: none" id="divdown">
                                    <asp:Label ID="lblUploadText" runat="server" />
                                    <%--<asp:HyperLink ID="hlBuyerPODwn" runat="server" Text="Open" Target="_blank" />--%>
                                    <asp:LinkButton ID="lnkdownloadpo" runat="server" OnClick="lnkdownloadpo_Click" Text="Download"></asp:LinkButton>
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-8">
                            <div class="form-group">
                                <asp:TextBox ID="txtcomments" TextMode="MultiLine" placeholder="Enter Comment's" CssClass="form-control form-control-solid placeholder-no-fix" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-2">
                        </div>
                        <div class="col-sm-8">
                            <div class="form-group" style="text-align: center">
                                <asp:Button ID="btncancel" runat="server" Text="Cancel" CssClass="btn btn-default" Visible="true" OnClick="btncancel_Click" />
                                <asp:Button ID="btnFinish" runat="server" Text="Finish" CssClass="btn btn-success" OnClick="btnFinish_Click" Enabled="false" />
                            </div>
                        </div>
                        <div class="col-sm-2">
                        </div>
                    </div>
                </div>
            </div>
            <div>
                <div class="page-content-inner">
                    <div class="note note-success">
                        <div class="form-inline" style="text-align: center">
                            <div class="form-group">
                                <label class="control-label">Select Order Status</label>
                                <asp:DropDownList ID="ddlOrderStatus" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlOrderStatus_SelectedIndexChanged">
                                    <asp:ListItem Text="ALL" Value="ALL" />
                                    <asp:ListItem Text="NEW" Value="NEW" />
                                    <asp:ListItem Text="INPROCESS" Value="INPROCESS" />
                                    <asp:ListItem Text="SHIPPING" Value="SHIPPING" />
                                    <asp:ListItem Text="HOLD" Value="HOLD" />
                                    <asp:ListItem Text="PENDING" Value="PENDING" />
                                    <asp:ListItem Text="DISPATCH" Value="DISPATCH" />
                                    <asp:ListItem Text="CLOSE" Value="CLOSE" />
                                </asp:DropDownList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>Buyer Order History
                    </div>
                </div>
                <div class="portlet-body flip-scroll">
                    <asp:Repeater ID="dlOrderHistory" runat="server"
                        OnItemCommand="dlOrderHistory_ItemCommand" OnItemDataBound="dlOrderHistory_ItemDataBound">
                        <HeaderTemplate>
                            <table class="table table-bordered mudargrid">
                                <thead>
                                    <tr>
                                        <th>Details
                                        </th>
                                        <th>Order ID
                                        </th>
                                        <th>Order Type
                                        </th>
                                        <th>PO No
                                        </th>
                                        <th>PO Date
                                        </th>
                                        <th>Status
                                        </th>
                                        <th>LotSample
                                        </th>
                                        <th>System PO
                                        </th>
                                        <th>Buyer PO
                                        </th>
                                    </tr>
                                </thead>
                                <tbody>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td>
                                    <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                        runat="server" />
                                </td>
                                <td>
                                    <asp:LinkButton ID="lbtnOrderID" CommandName="BranchOrder" runat="server" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>' Text='<%# DataBinder.Eval(Container.DataItem, "OrderID")%>'></asp:LinkButton>
                                </td>
                                <td>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderType")%>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblPOID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderID")%>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblDateOfOrder" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderDate", "{0:dd MMM yyyy}")%>' />
                                </td>
                                <td>
                                    <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "OrderStatus")%>' />
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlLS" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "LSpath")%>' CssClass="btn btn-success"
                                        runat="server" Visible="false">Open</asp:HyperLink>
                                    <asp:HiddenField ID="hfLS" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "LSpath")%>' />
                                    <asp:LinkButton ID="lbtnLS" runat="server" Visible="false" CommandName="LSDownload" Text="Download" CssClass="btn btn-success" />
                                </td>
                                <td>
                                    <asp:HyperLink ID="hlPDF" Target="_blank" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderPath")%>' CssClass="btn btn-success"
                                        runat="server">Open</asp:HyperLink>
                                    <asp:HiddenField ID="hfOrderPdf" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PurchaseOrderPath")%>' />
                                    <asp:LinkButton ID="lbtnOrderpdfDownload" runat="server" CommandName="Download" Text="Download" CssClass="btn btn-success" />
                                </td>
                                <td>
                                    <asp:Button ID="btnUpdate" runat="server" Text="upload PO" CssClass="btn btn-success" CommandName="uploadpo" />
                                    <asp:HyperLink ID="hlBuyerPO" Target="_blank" Visible="false" CssClass="btn btn-success" NavigateUrl='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>'
                                        runat="server">Open</asp:HyperLink><br />
                                    <asp:HiddenField ID="hfBuyerPO" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Buyer_PO_No")%>' />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="9" style="padding-left: 50px;">
                                    <asp:GridView ID="gvOrderHistory" runat="server" AutoGenerateColumns="false" CssClass="subgrid">
                                        <Columns>
                                            <asp:BoundField HeaderText="OrderProductID" DataField="OrderProductID" Visible="false" />
                                            <asp:BoundField HeaderText="ProductID" DataField="ProductID" Visible="false" />
                                            <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                            <asp:BoundField HeaderText="Qty(KG)" DataField="Quantity" />
                                            <asp:BoundField HeaderText="Price/KG" DataField="RateforKG" />
                                            <asp:BoundField HeaderText="Total Amt" DataField="TotalPrice"
                                                DataFormatString="{0:0.00}" />
                                            <asp:BoundField HeaderText="Drums(25)" DataField="Packing25" />
                                            <asp:BoundField HeaderText="Drums(180)" DataField="Packing180" />
                                        </Columns>
                                    </asp:GridView>
                                    <asp:Label ID="lblMSg" runat="server" Visible="false"></asp:Label>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </tbody>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lnkdownloadpo" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        function uploadComplete(sender) {
            //alert("File Uploaded Successfully");
            $('#divdown').show();
            $('#<%=btnFinish.ClientID%>').prop('disabled', false);
            clearContents();
        }

        function uploadError(sender, args) {
            alert("File upload failed.");
            clearContents()
        }

        function clearContents() {
            var span = $get("<%=AsyncFileUpload1.ClientID%>");
            var txts = span.getElementsByTagName("input");
            for (var i = 0; i < txts.length; i++) {
                if (txts[i].type == "text") {
                    txts[i].value = "";
                }
            }
        }
    </script>
</asp:Content>

