<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="SampleRequest.aspx.cs" Inherits="Buyer_SampleRequest" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="portlet box green">
        <div class="portlet-title">
            <div class="caption">
                <i class="fa fa-cogs"></i>Sample Request
            </div>
        </div>
        <div class="portlet-body flip-scroll">
            <div class="row">
                <div class="col-sm-12 pull-right">
                    <asp:Button ID="btnNewSampleRequest" runat="server"
                        Text="New Sample" CssClass="btn btn-success" PostBackUrl="~/Buyer/NewSampleRequest.aspx" />
                </div>
            </div>
            <div class="row" style="text-align: center; margin-bottom: 10px">
                <asp:DropDownList ID="ddlStatus" AutoPostBack="true" CssClass="input input-sm" runat="server" OnSelectedIndexChanged="ddlStatus_SelectedIndexChanged">
                    <asp:ListItem Text="NEW" Value="NEW" />
                    <asp:ListItem Text="DISPATCH" Value="DISPATCH" />
                </asp:DropDownList>
            </div>
            <div class="row">
                <asp:Repeater ID="dlSampleRequestDetails" runat="server"
                    OnItemCommand="dlSampleRequestDetails_ItemCommand" OnItemDataBound="dlSampleRequestDetails_ItemDataBound">
                    <HeaderTemplate>
                        <table class="table table-bordered mudargrid">
                            <tr>
                                <th>Details
                                </th>
                                <th>Sample Request Ref
                                </th>
                                <th>Date
                                </th>
                                <th>Status
                                </th>
                                <th>Courier Name
                                </th>
                                <th>Courier Ref
                                </th>
                                <th>Courier Date
                                </th>
                                <th>Received
                                </th>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <asp:ImageButton ID="ibtnNOExpColap" CommandName="Exp_Col" ImageUrl="~/images/expand.JPG"
                                    runat="server" />
                            </td>
                            <td>
                                <asp:Label ID="lblSampleID" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SampleID")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblSampleDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SampleDate","{0:dd MMM yyyy}")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCourierName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourierName")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCourierRef" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourierAccountNumber")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblCourierDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CourierDate", "{0:dd MMM yyyy}")%>' />
                            </td>
                            <td>
                                <asp:Label ID="lblReceiveDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "RDate", "{0:dd MMM yyyy}")%>' />
                                <asp:Panel ID="pReceive" runat="server">
                                    <div class="col-sm-12">
                                        <div class="col-sm-6">
                                            <asp:TextBox ID="txtReceivedDate" runat="server" Width="95%" CssClass="form-control" />
                                            <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtReceivedDate" />
                                        </div>
                                        <div>
                                            <asp:Button ID="btnUpdate" runat="server" Text="Update" CommandName="RUpdate" CssClass="btn btn-success btn-sm" />
                                        </div>
                                    </div>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="8" style="padding-left: 35px">
                                <asp:GridView ID="gvOrderSampleProduct" runat="server" Visible="false" AutoGenerateColumns="false" CssClass="subgrid">
                                    <Columns>
                                        <asp:BoundField HeaderText="ProductID" DataField="ProductID" />
                                        <asp:BoundField HeaderText="Product Name" DataField="ProductName" />
                                        <asp:BoundField HeaderText="Quantity" DataField="Quantity" />
                                    </Columns>
                                </asp:GridView>
                            </td>
                        </tr>
                        </table>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </div>
</asp:Content>

