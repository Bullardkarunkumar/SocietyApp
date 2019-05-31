<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MudarMenus.ascx.cs" Inherits="MudarMenus" %>
<asp:Panel ID="pnlAdminMenu" runat="server" Visible="false">
    <ul class="nav navbar-nav">
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="/AdminHome.aspx"><i class="fa fa-home"></i>Home
            </a>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Masters
                                            <span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hypLnkCateProdDetails" runat="server" NavigateUrl="~/Masters/Membershipform.aspx"
                        CssClass="nav-link" Text="Membership"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hypLnkBranchsRolesEmployees" runat="server" NavigateUrl="~/Admin/BranchsRolesEmployees.aspx"
                        CssClass="nav-link" Text="EMP/ROL/BRN"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hypLnkCustomAgent" runat="server" NavigateUrl="~/Admin/CustomAgent.aspx" CssClass="nav-link" Text="Custom Agent"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hylnkSupplier" runat="server" NavigateUrl="~/Supplier/Supplier.aspx" CssClass="nav-link" Text="Supplier Info"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hyplnkBuyer" runat="server" NavigateUrl="~/Buyer/Buyer.aspx" CssClass="nav-link" Text="Add Buyer"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown" id="liPreorder" runat="server" visible="false">
            <asp:HyperLink ID="HyperLink211" runat="server" NavigateUrl="~/Admin/PreOrder.aspx"
                CssClass="nav-link" Text="Pre Order"></asp:HyperLink>
        </li>
         <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown" id="liStockOrder" runat="server" visible="false">
            <asp:HyperLink ID="HyperLink21" runat="server" NavigateUrl="~/Admin/StockOrder.aspx"
                CssClass="nav-link" Text="Stock Order"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Orders
                                            <span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/Admin/OrderAdmin.aspx"
                        CssClass="nav-link" Text="Track PO O/W"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/Mudar/TracktheLot.aspx"
                        CssClass="nav-link" Text="Track the Lot"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Admin/SampleReq.aspx"
                        CssClass="nav-link" Text="Sample Request"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Admin/AvailableQty.aspx"
                        CssClass="nav-link" Text="Check Avail Qty"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Inspection
                                            <span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Admin/HoildayList.aspx"
                        CssClass="nav-link" Text="Holidays List"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Admin/InspectionHistory.aspx"
                        CssClass="nav-link" Text="Inspection Plan"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink7" runat="server" NavigateUrl="~/Admin/UnitInformation.aspx"
                        CssClass="nav-link" Text="Unit Information"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Farmers
                                            <span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Admin/FarmerApproval.aspx"
                        CssClass="nav-link" Text="Farmer Approval"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink9" runat="server" NavigateUrl="~/Mudar/ProduceList.aspx"
                        CssClass="nav-link" Text="Production List"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Admin/StandPage.aspx"
                        CssClass="nav-link" Text="Standard Data"></asp:HyperLink>
                </li>
            </ul>
        </li>
    </ul>
</asp:Panel>

<asp:Panel ID="pnlBranch" runat="server" Visible="false">
    <ul class="nav navbar-nav">
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="/SupplierHome.aspx"><i class="fa fa-home"></i>Home
            </a>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="hyplnkaddprices" runat="server" NavigateUrl="~/Mudar/ProductPriceUpdate.aspx"
                CssClass="nav-link" Text="Add Prices"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="hyplnkPreOrder" runat="server" NavigateUrl="~/Mudar/BranchOrder.aspx?preorder=true"
                CssClass="nav-link" Text="Pre Order"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="hyplnkPendingOrders" runat="server" NavigateUrl="~/Mudar/BranchOrder.aspx"
                CssClass="nav-link" Text="Pending Orders"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Reports <span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hyplnk" runat="server" NavigateUrl="~/FarmerReports/Reception%20Register.aspx"
                        CssClass="nav-link" Text="Reception Register"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hyplnkblending" runat="server" NavigateUrl="~/BranchReports/BlendingRegister.aspx"
                        CssClass="nav-link" Text="Blending Register"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hyplnkPackaging" runat="server" NavigateUrl="~/BranchReports/PackingRegister.aspx"
                        CssClass="nav-link" Text="Packaging Register"></asp:HyperLink>
                </li>

                <li aria-haspopup="true">
                    <asp:HyperLink ID="hyplnkDispatch" runat="server" NavigateUrl="~/BranchReports/DispatchRegister.aspx"
                        CssClass="nav-link" Text="Dispatch Register"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hyplnkFreezing" runat="server" NavigateUrl="~/BranchReports/FreezeRegister.aspx"
                        CssClass="nav-link" Text="Freezing Register"></asp:HyperLink>
                </li>

            </ul>
        </li>
    </ul>
</asp:Panel>

<asp:Panel ID="pnlSuperAdmin" runat="server" Visible="false">
    <ul class="nav navbar-nav">
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="/SuperAdminHome.aspx"><i class="fa fa-home"></i>Home
            </a>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink11" runat="server" NavigateUrl="~/Admin/SuperAdminOrders.aspx"
                CssClass="nav-link" Text="Orders List"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink12" runat="server" NavigateUrl="~/Mudar/ProductPriceUpdate.aspx"
                CssClass="nav-link" Text="Price List"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/Admin/UserDetails.aspx"
                CssClass="nav-link" Text="User Info"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Buyer<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="lnkBuyerApproval" runat="server" NavigateUrl="~/Admin/BuyerApproalDetails.aspx"
                        CssClass="nav-link" Text="Buyer Approval"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="lnkBuyerInfo" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Buyer Info"></asp:HyperLink>
                </li>

            </ul>
        </li>
    </ul>
</asp:Panel>

<asp:Panel ID="pnlBuyer" runat="server" Visible="false">
    <ul class="nav navbar-nav">
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="/BuyerHome.aspx"><i class="fa fa-home"></i>Home
            </a>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/Buyer/BuyNewPrice.aspx"
                CssClass="nav-link" Text="Prices"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink15" runat="server" NavigateUrl="~/Buyer/OrderHistory.aspx"
                CssClass="nav-link" Text="Track PO"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink16" runat="server" NavigateUrl="~/Buyer/SampleRequest.aspx"
                CssClass="nav-link" Text="Sample Request"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink17" runat="server" NavigateUrl="~/Buyer/LotSample.aspx"
                CssClass="nav-link" Text="Track Lot Sample"></asp:HyperLink>
        </li>

        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink18" runat="server" NavigateUrl="~/Buyer/BuyerComplaint.aspx"
                CssClass="nav-link" Text="Compliants"></asp:HyperLink>
        </li>

        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink19" runat="server" NavigateUrl="~/Buyer/BuyerUpdateLoginDetails.aspx"
                CssClass="nav-link" Text="Update Login Details"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink20" runat="server" NavigateUrl="~/Buyer/BuyerUpdate.aspx"
                CssClass="nav-link" Text="Edit Profile"></asp:HyperLink>
        </li>

    </ul>
</asp:Panel>

<asp:Panel ID="pnlICS" runat="server" Visible="false">
    <ul class="nav navbar-nav">
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="/BranchHome.aspx"><i class="fa fa-home"></i>Home
            </a>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink22" runat="server" NavigateUrl="~/Farmer/Sample Farmer.aspx"
                CssClass="nav-link" Text="Farmer Info"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink23" runat="server" NavigateUrl="~/Farmer/FarmerProductionInfo.aspx"
                CssClass="nav-link" Text="Production Info"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink24" runat="server" NavigateUrl="~/Farmer/FarmerInspection.aspx"
                CssClass="nav-link" Text="Inspection List"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink25" runat="server" NavigateUrl="~/Farmer/FarmingInfo.aspx"
                CssClass="nav-link" Text="Farming Info"></asp:HyperLink>
        </li>

        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink26" runat="server" NavigateUrl="~/Mudar/TracktheLot.aspx"
                CssClass="nav-link" Text="Track the Lot"></asp:HyperLink>
        </li>

        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink27" runat="server" NavigateUrl="~/FarmerReports/FarmersReports.aspx"
                CssClass="nav-link" Text="Reports"></asp:HyperLink>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <asp:HyperLink ID="HyperLink28" runat="server" NavigateUrl="~/Mudar/BranchComplaints.aspx"
                CssClass="nav-link" Text="Complaints"></asp:HyperLink>
        </li>

    </ul>
</asp:Panel>

<asp:Panel ID="PnlSociety" runat="server" Visible="false">
    <ul class="nav navbar-nav">
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="/SocietyHome.aspx"><i class="fa fa-home"></i>Home
            </a>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Masters<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="hypLnkMembership" runat="server" NavigateUrl="~/Masters/Membershipform.aspx"
                        CssClass="nav-link" Text="Membership"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink33" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Accounts"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Loans<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink29" runat="server" NavigateUrl="~/Admin/BuyerApproalDetails.aspx"
                        CssClass="nav-link" Text="Buyer Approval"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink30" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Buyer Info"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Share & Thrift<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink31" runat="server" NavigateUrl="~/Admin/BuyerApproalDetails.aspx"
                        CssClass="nav-link" Text="Buyer Approval"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink34" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Buyer Info"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Saving Deposits<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink35" runat="server" NavigateUrl="~/Admin/BuyerApproalDetails.aspx"
                        CssClass="nav-link" Text="SB Application"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink36" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="SB Deposit"></asp:HyperLink>
                </li>
                 <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink43" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="SB Withdrwal"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Term Deposits<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink37" runat="server" NavigateUrl="~/Masters/TermDepositApplication.aspx"
                        CssClass="nav-link" Text="TermDeposit Application"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink38" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Interest Payments"></asp:HyperLink>
                </li>
                 <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink32" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Recurring Installment"></asp:HyperLink>
                </li>
                 <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink44" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="TD ForeClosure"></asp:HyperLink>
                </li>
                 <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink45" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="TD Closure"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Investments<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink39" runat="server" NavigateUrl="~/Admin/BuyerApproalDetails.aspx"
                        CssClass="nav-link" Text="Buyer Approval"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink40" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Buyer Info"></asp:HyperLink>
                </li>
            </ul>
        </li>
        <li aria-haspopup="true" class="menu-dropdown classic-menu-dropdown">
            <a href="javascript:;">Reports<span class="arrow"></span>
            </a>
            <ul class="dropdown-menu pull-left">
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink41" runat="server" NavigateUrl="~/Admin/BuyerApproalDetails.aspx"
                        CssClass="nav-link" Text="Buyer Approval"></asp:HyperLink>
                </li>
                <li aria-haspopup="true">
                    <asp:HyperLink ID="HyperLink42" runat="server" NavigateUrl="~/Buyer/BuyerInfo.aspx"
                        CssClass="nav-link" Text="Buyer Info"></asp:HyperLink>
                </li>
            </ul>
        </li>
    </ul>
</asp:Panel>