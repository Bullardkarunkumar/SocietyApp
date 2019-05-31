<%@ Page Title="Mudarorganic-AdminHome" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="AdminHome.aspx.cs" Inherits="AdminHome" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="row">
                <div class="col-lg-12 col-xs-12 col-sm-12 number-stats">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption caption-md">
                                <i class="icon-bar-chart font-dark hide"></i>
                                <span class="caption-subject font-green-steel bold uppercase">Orders Summary</span>
                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="row">
                                <div class="col-md-4 col-sm-4 col-xs-4">
                                    <div class="stat-right">
                                        <div class="stat-chart">
                                            <!-- do not line break "sparkline_bar" div. sparkline chart has an issue when the container div has line break -->
                                            <div id="sparkline_bar">
                                                <canvas width="113" height="55" style="display: inline-block; width: 113px; height: 55px; vertical-align: top;"></canvas>
                                            </div>
                                        </div>
                                        <div class="stat-number">
                                            <div class="title">New</div>
                                            <div class="number">
                                                <asp:Label ID="lblNew" CssClass="count" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-4">
                                    <div class="stat-right">
                                        <div class="stat-chart">
                                            <!-- do not line break "sparkline_bar" div. sparkline chart has an issue when the container div has line break -->
                                            <div id="sparkline_bar5">
                                                <canvas width="100" height="55" style="display: inline-block; width: 100px; height: 55px; vertical-align: top;"></canvas>
                                            </div>
                                        </div>
                                        <div class="stat-number">
                                            <div class="title">In Process </div>
                                            <div class="number">
                                                <asp:Label ID="lblOther" CssClass="count" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-md-4 col-sm-4 col-xs-4">
                                    <div class="stat-right">
                                        <div class="stat-chart">
                                            <!-- do not line break "sparkline_bar" div. sparkline chart has an issue when the container div has line break -->
                                            <div id="sparkline_bar2">
                                                <canvas width="107" height="55" style="display: inline-block; width: 107px; height: 55px; vertical-align: top;"></canvas>
                                            </div>
                                        </div>
                                        <div class="stat-number">
                                            <div class="title">Dispatch </div>
                                            <div class="number">
                                                <asp:Label ID="lblBranchOrderDispatch" CssClass="count" runat="server"></asp:Label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row margin-bottom-30">
                <div class="col-lg-6 col-xs-12 col-sm-12">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption caption-md">
                                <i class="icon-bar-chart font-dark hide"></i>
                                <span class="caption-subject font-green-steel bold uppercase">Recent Five Orders</span>

                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="table-scrollable table-scrollable-borderless">
                                <asp:Repeater ID="rptFiveOrders" runat="server" OnItemCommand="rptFiveOrders_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-hover table-light mudargrid" style="width: 100%">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%">Order ID</th>
                                                    <th style="width: 70%">Buyer Name</th>
                                                    <th style="width: 10%">Order Date</th>
                                                    <th style="width: 10%">Order Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="fit">
                                                <asp:LinkButton ID="lnkbtnOrderId" runat="server" Text='<%# Eval("OrderID") %>' CommandArgument='<%# Eval("OrderID") %>'></asp:LinkButton>
                                            </td>
                                            <td class="fit"><%# Eval("BuyerCompanyName") %>
                                            </td>
                                            <td class="fit"><%# Convert.ToDateTime(Eval("OrderDate")).ToString("dd/MM/yyyy") %>
                                            </td>
                                            <td class="fit"><%# Eval("OrderStatus") %>
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
                    </div>

                </div>

                <div class="col-lg-6 col-xs-12 col-sm-12">
                    <div class="portlet light">
                        <div class="portlet-title">
                            <div class="caption caption-md">
                                <i class="icon-bar-chart font-dark hide"></i>
                                <span class="caption-subject font-green-steel bold uppercase">Recent Five Lot Samples</span>

                            </div>
                        </div>
                        <div class="portlet-body">
                            <div class="table-scrollable table-scrollable-borderless">
                                <asp:Repeater ID="rptFiveLotSamples" runat="server" OnItemCommand="rptFiveLotSamples_ItemCommand">
                                    <HeaderTemplate>
                                        <table class="table table-hover table-light mudargrid">
                                            <thead>
                                                <tr>
                                                    <th style="width: 10%">Order ID</th>
                                                    <th style="width: 70%">Buyer Name</th>
                                                    <th style="width: 10%">Order Date</th>
                                                    <th style="width: 10%">Order Status</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr>
                                            <td class="fit">
                                                <asp:LinkButton ID="lnkbtnOrderId" runat="server" Text='<%# Eval("OrderID") %>' CommandArgument='<%# Eval("OrderID") %>'></asp:LinkButton>
                                            </td>
                                            <td class="fit"><%# Eval("BuyerCompanyName") %>
                                            </td>
                                            <td class="fit"><%# Convert.ToDateTime(Eval("OrderDate")).ToString("dd/MM/yyyy") %>
                                            </td>
                                            <td class="fit"><%# Eval("OrderStatus") %>
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
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
    <script type="text/javascript">        
        $(document).ready(function () {
            $('.count').each(function () {
                $(this).prop('Counter', 0).animate({
                    Counter: $(this).text()
                }, {
                        duration: 4000,
                        easing: 'swing',
                        step: function (now) {
                            $(this).text(Math.ceil(now));
                        }
                    });
            });
        })
    </script>
</asp:Content>


