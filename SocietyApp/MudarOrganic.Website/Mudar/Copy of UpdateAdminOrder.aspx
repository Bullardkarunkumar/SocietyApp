<%@ Page Language="C#" Title="Mudar Order Process" MasterPageFile="~/MudarMaster.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="Copy of UpdateAdminOrder.aspx.cs" Inherits="Admin_UpdateOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <style type="text/css">
        #accordion {
            width: 98%;
            margin: 0px auto;
            padding-bottom: 1%;
            padding-top: 1%;
        }
        .ui-accordion .ui-state-default {
            border: 1px solid #2F4F4F !important;
            color: white !important;
            background: #FDB700;
            font-family: Arial, Sans-Serif !important;
            font-size: 18px !important;
            font-weight: bold !important;
            margin: 2px 0 0 0;
            padding: .5em .5em .5em .7em;
            cursor: pointer !important;
            text-align: center !important;
        }

        .ui-accordion-header-active {
            border: 1px solid #2F4F4F !important;
            color: white !important;
            background: #ff9900 !important;
            font-family: Arial, Sans-Serif !important;
            font-size: 18px !important;
            font-weight: bold !important;
            margin: 2px 0 0 0;
            padding: .5em .5em .5em .7em;
            cursor: pointer !important;
            text-align: center !important;
        }

        ui-accordion-content {
            font-weight: normal;
            font-size: 14px;
        }

        .ui-datepicker-trigger {
            height: 26px;
        }
    </style>
    <link rel="stylesheet" href="../Style/jquery-ui.css">
    <%--<script src="../Style/jquery-1.6.4.js"></script>--%>
    <script src="http://code.jquery.com/jquery-1.6.4.js"></script>
    <script src="../Style/jquery-ui.js" type="text/javascript"></script>
    <script type="text/javascript">

        //(function ($, undefined) {

        //    // Modify the accordion, making the disabled option
        //    // accept section indexes.
        //    $.widget("ab.accordion", $.ui.accordion, {

        //        _create: function () {

        //            this._super();

        //            // Kick-start setting the disabled option,
        //            // using our custom code below.  jQuery widgets
        //            // don't call _setOption() when first created.
        //            this._setOption(
        //                "disabled",
        //                this.options.disabled
        //            );

        //        },

        //        // Sets the disabled object, but only if the option key
        //        // is "disabled" and the option value is a number.
        //        _setOption: function (key, value) {

        //            var isDisabled = (key === "disabled"),
        //                isNumber = (typeof value === "number"),
        //                isArray = (value instanceof Array),
        //            $panel;

        //            // Check if we're disabling a specific accordion
        //            // section by index.
        //            if (isDisabled && isNumber) {

        //                // Get the accordion header and panel, and 
        //                // disable them.  The base jQuery UI widget
        //                // knows not to handle events on elements that
        //                // have the ui-state-disabled class applied.
        //                // Adding the class to the panel header and 
        //                // content elements is enough to completely
        //                // disable the section.
        //                $panel = this._findActive(value);
        //                $panel.addClass("ui-state-disabled")
        //                      .next()
        //                      .filter(".ui-accordion-content-active")
        //                      .addClass("ui-state-disabled");

        //            }
        //            else if (isDisabled && isArray) {
        //                for (var i = 0; i < value.length; i++) {
        //                    $panel = null;
        //                    $panel = this._findActive(value[i]);
        //                    $panel.addClass("ui-state-disabled")
        //                          .next()
        //                          .filter(".ui-accordion-content-active")
        //                          .addClass("ui-state-disabled");
        //                }
        //            }
        //            else {

        //                this._super(key, value);

        //            }

        //        },

        //    });

        //})(jQuery);



        var localData = [{ "Id": "1", "Name": "DOCUMENTING" }, { "Id": "2", "Name": "UNDER<BR/>CUSTOM<BR/>CLEARANCE" },
        { "Id": "3", "Name": "INTRANSIT" }, { "Id": "4", "Name": "CLOSE" }];


        $(document).ready(function () {


            //$("[id$=txtIntransitDate]").datepicker({
            //    showOn: 'both',
            //    defaultDate: new Date(),
            //    minDate: 0,
            //    buttonImageOnly: false,
            //    buttonImage: 'http://jqueryui.com/resources/demos/datepicker/images/calendar.gif'
            //});

            //var myDate = new Date();
            //var prettyDate = (myDate.getMonth() + 1) + '/' + myDate.getDate() + '/' +
            //        myDate.getFullYear();
            //$("[id$=txtIntransitDate]").val(prettyDate);
        });
        function ManageTabs(name, status) {
            var active = 0;
            var disableArray = [];
            var disableStartIndex = 0;
            //alert(name);
            if (name !== '') {
                $.each(localData, function (k, v) {
                    if (v.Name == name) {
                        active = Number(v.Id) - 1;
                    }

                    if (v.Name == status) {
                        disableStartIndex = Number(v.Id);
                    }
                });

                if (disableStartIndex == 0) {
                    disableStartIndex = 1;
                }
            }
            $("#accordion").accordion({
                collapsible: true,
                heightStyle: "content",
                active: active
                //disabled: disableArray
            });
            $('#accordion > h3:gt(' + disableStartIndex + ')').addClass('ui-state-disabled');
            return false;
        }

        function ManageStartupTabs(name, status) {
            var active = 0;
            var disableArray = [];
            var disableStartIndex = 0;
            //alert(name);
            if (name !== '') {
                $.each(localData, function (k, v) {
                    if (v.Name == name) {
                        active = Number(v.Id);
                    }

                    if (v.Name == status) {
                        disableStartIndex = Number(v.Id);
                    }
                });

                if (active >= 4) {
                    active = 3;
                }
            }
            $("#accordion").accordion({
                collapsible: true,
                heightStyle: "content",
                active: active
                //disabled: disableArray
            });
            $('#accordion > h3:gt(' + disableStartIndex + ')').addClass('ui-state-disabled');
            return false;
        }

        function refreshAccordion() {
            $("#accordion").accordion({
                collapsible: true,
                heightStyle: "content",
                active: 0
            });
        }
    </script>
    <style type="text/css">
        .gvheader th {
            color: blue;
            text-align: center;
            font-weight: bold;
            text-decoration: underline;
        }

        #tdGrid div {
            text-align: center;
            display: inline-block;
        }
    </style>
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Admin Order Details
        </div>
        <asp:Label ID="lblLotsample" runat="server" Text="" ForeColor="#FF6600" Visible="false" />
        <asp:Label ID="lblPOID" runat="server" Text="" ForeColor="#FF6600" Visible="false" />
        <div style="background: #E9E1CC">
            <%--<table cellpadding="5" cellspacing="5" border="0" style="width: 100%">
                <tr>
                    <td colspan="7" align="center" style="text-align: center; color: white; font-weight: bold; background-color: #FDB700; font-size: 20px; height: 25px">Order Details
                    </td>
                </tr>
                <tr>
                    <td></td>
                    <td style="text-align: right">&nbsp; 
                    </td>
                    <td style="text-align: center">Order ID<br />
                        &nbsp;</td>
                    <td style="width: 50px;">Lot sample <br />
                    </td>
                    <td style="text-align: center">Purchase Order&nbsp; ID<br />
                    </td>
                    <td style="width: 50px;">&nbsp;</td>
                    <td style="text-align: center">&nbsp;</td>
                </tr>
                <tr>
                    <td style="width: 50px;"></td>
                    <td style="text-align: right">
                        <br />
                        &nbsp;</td>
                    <td style="text-align: center">Order Type<br />
                    </td>
                    <td style="width: 50px;">Lotsample<br />
                        <asp:HyperLink ID="hfLS" runat="server" Text="Open"
                            Target="_blank" />
                    </td>
                    <td style="text-align: center">Purchase Order
                        <br />
                        &nbsp;<asp:HyperLink ID="hfPdf" runat="server" Text="Open"
                            Target="_blank" />
                    </td>
                    <td style="text-align: left">&nbsp;</td>
                    <td style="width: 50px;"></td>
                </tr>
                <tr>
                    <td align="right" colspan="2"></td>
                    <td colspan="5">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp</td>
                </tr>
            </table>--%>
            <table width="90%" align="center">
                <tr>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td width="30%" align="center">Order ID</td>
                    <td width="30%" align="center">Buyer PO</td>
                    <td width="30%" align="center">Branch PO</td>
                </tr>
                <tr>
                    <td width="30%" align="center">
                        <asp:Label ID="lblOrderId" runat="server" Text="" ForeColor="#FF6600" />
                    </td>
                    <td width="30%" align="center">

                        <asp:HyperLink ID="hfPdf" runat="server" Text="Open"
                            Target="_blank" />

                    </td>
                    <td width="30%" align="center">&nbsp;</td>
                </tr>
                <tr>
                    <td width="30%" align="center">Order Type</td>
                    <td width="30%" align="center">Buyer Lot Sample</td>
                    <td width="30%" align="center">Branch Lot Sample</td>
                </tr>
                <tr>
                    <td width="30%" align="center">
                        <asp:Label ID="lblOtype" runat="server" ForeColor="#FF6600" />
                    </td>
                    <td width="30%" align="center">
                        <asp:HyperLink ID="hfLS" runat="server" Text="Open"
                            Target="_blank" /></td>
                    <td width="30%" align="center">
                        <asp:HyperLink ID="hfLS0" runat="server" Text="Open"
                            Target="_blank" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3"></td>
                </tr>
                <tr>
                    <td colspan="3" align="center">
                        <asp:Button ID="btnPlaceBrnchOrder" runat="server" Text="Place Order" CommandName="BranchOrder"
                            OnClick="btnPlaceBrnchOrder_Click" CssClass="fb8" />
                        <asp:Button ID="btnDisable" runat="server" Text="Place Order" CssClass="fb8_disable"
                            Visible="false" ForeColor="Gray" /></td>
                </tr>
            </table>
            <div id="accordion" style="background: none !important; border: none !important;">
                <h3>Generate Documents</h3>
                <div style="background: #E9E1CC">
                    <table align="center">
                        <tr align="right">
                            <td>Invoice :
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="hlInvoice" runat="server" NavigateUrl="" Text="" Target="_blank" />
                            </td>
                            <td>
                                <asp:Button ID="btnInvoice" runat="server" CssClass="fb8" Text="Create Invoice" OnClick="btnInvoice_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableInv" runat="server" Text="Create Invoice" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnPP" runat="server" CssClass="fb8" Text="Create COA- PP" OnClick="btnPP_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisablePP" runat="server" Text="Create COA- PP" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td>Packing :
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="hlPacking" runat="server" Text="Open" Target="_blank" />
                            </td>
                            <td>
                                <asp:Button ID="btnPacking" runat="server" CssClass="fb8" Text="Create Packing" OnClick="btnPacking_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisablePack" runat="server" Text="Create Packing" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnAR" runat="server" CssClass="fb8" Text="Create COA-AR" OnClick="btnAR_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableAR" runat="server" Text="Create COA-AR" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td>Non-Haz Sea :
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="hlHazSea" runat="server" Text="Open" Target="_blank" />
                            </td>
                            <td>
                                <asp:Button ID="btnHazsea" runat="server" CssClass="fb8" Text="Create HAZ-SEA" OnClick="btnHazsea_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableHazSea" runat="server" Text="Create HAZ-SEA" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnSP" runat="server" CssClass="fb8" Text="Create COA-SP" OnClick="btnSP_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableSP" runat="server" Text="Create COA-SP" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td>Non-Haz Air :
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="hlHazAir" runat="server" Text="Open" Target="_blank" />
                            </td>
                            <td>
                                <asp:Button ID="btnHazAir" runat="server" CssClass="fb8" Text="Create HAZ-AIR" OnClick="btnHazAir_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableHazAir" runat="server" Text="Create HAZ-AIR" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnbo" runat="server" CssClass="fb8" Text="Create COA-BO" OnClick="btnbo_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisablebo" runat="server" Text="Create COA-BO" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td>Cover Letter :
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="hlCoverLetter" runat="server" Text="Open" Target="_blank" />
                            </td>
                            <td>
                                <asp:Button ID="btnCoverLetter" runat="server" CssClass="fb8" Text="Create Cover Letter"
                                    OnClick="btnLetter_Click" Visible="false" />
                                <asp:Button ID="btnDisableCL" runat="server" Text="Create Cover Letter" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnCRY" runat="server" CssClass="fb8" Text="Create COA-CRY" OnClick="btnCRY_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableCRY" runat="server" Text="Create COA-CRY" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td>Fir Cover Letter :
                            </td>
                            <td align="center">
                                <asp:HyperLink ID="hlFIRCover" runat="server" Text="Open" Target="_blank" />
                            </td>
                            <td>
                                <asp:Button ID="btnFLetter" runat="server" CssClass="fb8" Text="Create Fir Letter"
                                    OnClick="btnFLetter_Click" Visible="false" />
                                <asp:Button ID="btnDisableFL" runat="server" Text="Create Fir Letter" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                            <td>
                                <asp:Button ID="btnCRYp" runat="server" CssClass="fb8" Text="Create COA-CRY-P" OnClick="btnCRYp_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableCRYp" runat="server" Text="Create COA-CRY-P" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                        <tr align="right">
                            <td colspan="2" align="center">Label's :&nbsp;&nbsp;&nbsp;
                            </td>
                            <td colspan="1" align="center">
                                <asp:Button ID="btnLable" runat="server" CssClass="fb8" Text="Create Label" OnClick="btnLable_Click"
                                    Visible="false" />
                                <asp:Button ID="btnDisableLabel" runat="server" Text="Create Label" CssClass="fb8_disable"
                                    ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td colspan="2">
                                <asp:Button ID="btnAdminSkip" runat="server" Text="Skip" CssClass="fb8" OnClick="btnAdminSkip_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center">
                                <asp:GridView ID="gvReports" runat="server" BackColor="White" BorderColor="#DEDFDE"
                                    BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                                    AutoGenerateColumns="false">
                                    <Columns>
                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                        <asp:HyperLinkField DataNavigateUrlFields="PP" HeaderText="PP" Text="Open" Target="_blank" />
                                        <asp:HyperLinkField DataNavigateUrlFields="AR" HeaderText="AR" Text="Open" Target="_blank" />
                                        <asp:HyperLinkField DataNavigateUrlFields="SP" HeaderText="SP" Text="Open" Target="_blank" />
                                        <asp:HyperLinkField DataNavigateUrlFields="BO" HeaderText="BO" Text="Open" Target="_blank" />
                                        <asp:HyperLinkField DataNavigateUrlFields="CRY" HeaderText="CRY" Text="Open" Target="_blank" />
                                        <asp:HyperLinkField DataNavigateUrlFields="CRY_P" HeaderText="CRY_P" Text="Open"
                                            Target="_blank" />
                                        <asp:HyperLinkField DataNavigateUrlFields="LABEL" HeaderText="LABEL" Text="Open"
                                            Target="_blank" />
                                    </Columns>
                                    <RowStyle BackColor="#F7F7DE" />
                                    <FooterStyle BackColor="#CCCC99" />
                                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                    <AlternatingRowStyle BackColor="White" />
                                </asp:GridView>
                            </td>
                        </tr>
                    </table>
                </div>

                <h3>Under Custom Clearance</h3>
                <div style="background: #E9E1CC; text-align: center">
                    <br />
                    <asp:Button ID="btnClearance" runat="server" CssClass="fb8" Text="Submit" OnClick="btnClearance_Click" />
                    <asp:Button ID="btnDisableClearance" runat="server" Text="Submit" CssClass="fb8_disable"
                        ForeColor="Gray" Enabled="false" Visible="false" />
                    <br />
                </div>
                <h3>Intransit</h3>
                <div style="background: #E9E1CC; text-align: center" align="center">
                    <asp:Label ID="lblETAText" runat="server" Text="" Visible="false"></asp:Label>
                    <br />
                    <table align="center" cellpadding="5" cellspacing="5" runat="server" id="tblETA">
                        <tr>
                            <td>ETA Date<br />
                                <asp:TextBox ID="txtIntransitDate" runat="server"></asp:TextBox>
                                <asp:CalendarExtender
                                    ID="dtpLevdate" runat="server" Format="MM/dd/yyyy"
                                    TargetControlID="txtIntransitDate">
                                </asp:CalendarExtender>

                                <%--<asp:TextBox ID="txtIntransitDate" CssClass="inputDatePicker" runat="server" ReadOnly = "true"></asp:TextBox>--%> </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnIntransitSubmit" runat="server" Text="Submit" CssClass="fb8" OnClick="btnIntransitSubmit_Click" Visible="false" /></td>
                        </tr>
                    </table>
                </div>
                <h3>Close</h3>
                <div style="background: #E9E1CC; text-align: center">
                    <br />
                    <asp:Button ID="btnCloseOrder" runat="server" Text="Close Order" CssClass="fb8" OnClick="btnCloseOrder_Click" />
                    <asp:Button ID="btnDisableCloseOrder" runat="server" Text="Close Order" CssClass="fb8_disable"
                        ForeColor="Gray" Enabled="false" Visible="false" />
                    <br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
