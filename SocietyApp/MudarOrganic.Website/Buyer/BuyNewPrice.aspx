<%@ Page Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="BuyNewPrice.aspx.cs" Inherits="Buyer_BuyNewPrice" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <script type="text/javascript">
        function validateGrid() {
            var checked = false;
            var GridView = document.getElementById('<%= this.grdProdQuantity.ClientID %>');
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (inputList[i].checked) {
                        checked = true;
                        break;
                    }
                }
            }
            if (checked == false) {
                fnShowMessage('!!! Please select atleast one product !!!');
                return false;
            }
            else {
                if (confirm("Are you sure you really wish to go with selected products?")) {
                    return true;
                }
                else {
                    return false;
                }
            }
        }
        function Confirm() {
            var confirm_value = document.createElement("INPUT");
            confirm_value.type = "hidden";
            confirm_value.name = "confirm_value";
            if (confirm("Do you want to Place the order?")) {
                confirm_value.value = "Yes";
            } else {
                confirm_value.value = "No";
            }
            document.forms[0].appendChild(confirm_value);
        }
        function Check_Click(objRef) {
            var row = objRef.parentNode.parentNode;
            var GridView = row.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var headerCheckBox = inputList[0];
                var checked = true;
                if (inputList[i].type == "checkbox" && inputList[i] != headerCheckBox) {
                    if (!inputList[i].checked) {
                        checked = false;
                        break;
                    }
                }
            }
            headerCheckBox.checked = checked;
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                    }
                    else {
                        inputList[i].checked = false;
                    }
                }
            }
        }
        function checkDrumvalue() {
            var Grid_Table = document.getElementById('<%= gvProductPricDetails.ClientID %>');
            for (var row = 1; row < Grid_Table.rows.length; row++) {
                var drum25 = Grid_Table.rows[row].cells[5].children[0];
                var drum180 = Grid_Table.rows[row].cells[6].children[0];
                var prodCell = Grid_Table.rows[row].cells[1];
                var prodName = 0;
                if ((prodCell.textContent) && (typeof (prodCell.textContent) != "undefined")) {
                    prodName = prodCell.textContent;
                } else {
                    prodName = prodCell.innerText;
                }

                var prodIdCell = Grid_Table.rows[row].cells[0];
                var prodId = 0;
                if ((prodIdCell.textContent) && (typeof (prodIdCell.textContent) != "undefined")) {
                    prodId = prodIdCell.textContent;
                } else {
                    prodId = prodIdCell.innerText;
                }
                if (prodId == "4" && drum25.value == "0") {
                    fnShowMessage('!!! Please enter the values in either Drum# 25 for the product ' + prodName + ' !!!');
                    return false;
                }

                else if (drum25.value == "0" && drum180.value == "0") {

                    fnShowMessage('!!! Please enter the values in either Drum# 25 or Drum# 180 for the product ' + prodName + ' !!!');
                    return false;
                }
                else {
                    var availCell = Grid_Table.rows[row].cells[2];
                    var availQty = 0;
                    if ((availCell.textContent) && (typeof (availCell.textContent) != "undefined")) {
                        availQty = parseFloat(availCell.textContent);
                    } else {
                        availQty = parseFloat(availCell.innerText);
                    }
                    var drum25Value = parseFloat(drum25.value);
                    var drum180Value = parseFloat(drum180.value);
                    if (availQty != ((drum25Value * 25) + (drum180Value * 180))) {
                        fnShowMessage('!!Packing Quantity should match with actual quantity!!');
                        return false;
                    }
                }
            }
            return true;
        }
        function fnShowMessage(msg) {
            bootbox.alert(msg);
        }
    </script>
    <style type="text/css">
        .btnFarmer1 {
            width: 215px;
            padding: 10px 25px 10px 25px;
            font-family: Arial;
            font-size: medium;
            text-decoration: none;
            color: #ffffff;
            background-color: #9B336F;
        }
            /*#98ba3f*/
            .btnFarmer1:hover {
                background-color: #FFFFB3;
                padding: 10px 25px 10px 25px;
                font-family: Arial;
                font-size: medium;
                text-decoration: blink;
                color: Black !important;
            }

        .bactive {
            border: 3px solid dodgerblue;
            font-weight: bold;
        }


        /* The container */
        .rbtncontainer {
            display: block;
            position: relative;
            padding-left: 35px;
            margin-bottom: 12px;
            cursor: pointer;
            font-size: 18px;
            -webkit-user-select: none;
            -moz-user-select: none;
            -ms-user-select: none;
            user-select: none;
        }

            /* Hide the browser's default radio button */
            .rbtncontainer input {
                position: absolute;
                opacity: 0;
                cursor: pointer;
            }

        /* Create a custom radio button */
        .checkmark {
            position: absolute;
            top: 0;
            left: 0;
            height: 25px;
            width: 25px;
            background-color: #eee;
            border-radius: 50%;
        }

        /* On mouse-over, add a grey background color */
        .rbtncontainer:hover input ~ .checkmark {
            background-color: #ccc;
        }

        /* When the radio button is checked, add a blue background */
        .rbtncontainer input:checked ~ .checkmark {
            background-color: #36c6d3;
        }

        /* Create the indicator (the dot/circle - hidden when not checked) */
        .checkmark:after {
            content: "";
            position: absolute;
            display: none;
        }

        /* Show the indicator (dot/circle) when checked */
        .container input:checked ~ .checkmark:after {
            display: block;
        }

        /* Style the indicator (dot/circle) */
        .rbtncontainer .checkmark:after {
            top: 9px;
            left: 9px;
            width: 8px;
            height: 8px;
            border-radius: 50%;
            background: white;
        }
    </style>

    <div id="content_area_Home" style="height: auto; display: none">
        <div id="header_Text">
            Place Order
        </div>
        <div>
            <table>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnTProducts" runat="server" Text="Products List" CssClass="btnFarmer1" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnTQunatity" runat="server" Text="Quantity" CssClass="btnFarmer1" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnTOrganic" runat="server" Text="Organic" CssClass="btnFarmer1" />
                    </td>
                    <td colspan="2" align="left">
                        <asp:Button ID="btnFT" runat="server" Text="Freight Terms" CssClass="btnFarmer1" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnTPT" runat="server" Text="Payment Terms" CssClass="btnFarmer1" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnTOrderDetails" runat="server" Text="Order Details" CssClass="btnFarmer1" />
                    </td>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnTPlaceorder" runat="server" Text="Place Order" CssClass="btnFarmer1" />
                    </td>
                    <td colspan="2" align="center">&nbsp;
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:MultiView ID="mvPrice" runat="server" ActiveViewIndex="0">
                <asp:View ID="vproduct" runat="server">
                    <div>
                        <table align="center">
                            <tr>
                                <td></td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:GridView ID="gvProductSpecification" AutoGenerateColumns="False" runat="server"
                                        DataKeyNames="ProductId" BackColor="White" BorderColor="#DEDFDE" BorderStyle="Solid"
                                        BorderWidth="2px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
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
                                <td>
                                    <asp:Label ID="lblmsg" runat="server" Text=" Please Register Products for place order <br /> Email Mudar Organic for more Information"
                                        Visible="false" />
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Button ID="btnAccept" runat="server" Text="Accepted" OnClick="btnAccept_Click"
                                        CssClass="fb8" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:View>
                <asp:View ID="vquantity" runat="server">
                    <table align="center">
                        <tr>
                            <td style="color: #FF0000"></td>
                        </tr>
                        <tr>
                            <td></td>
                        </tr>
                        <tr>
                            <td align="center">

                                <%--<asp:HiddenField ID="hdnCheckConfirm" runat="server" Value="false" />--%>
                            </td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vOrganic" runat="server">
                    <table width="100%" align="center">
                        <tr>
                            <td colspan="4">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" align="center"></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                        <tr>
                            <td colspan="4"></td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vFrightTerms" runat="server">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always">
                        <ContentTemplate>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                    <table align="center">
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td></td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td colspan="6"></td>
                        </tr>
                    </table>
                </asp:View>
                <asp:View ID="vPaymentTerms" runat="server">
                </asp:View>
                <asp:View ID="vPriceGrid" runat="server">
                </asp:View>
                <asp:View ID="vUploadPO" runat="server">
                </asp:View>
                <asp:View ID="vLotSamples" runat="server">
                </asp:View>
            </asp:MultiView>
        </div>
    </div>


    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="portlet light " id="form_wizard_1">
                <div class="portlet-title">
                    <div class="caption">
                        <i class=" icon-layers font-red"></i>
                        <span class="caption-subject font-red bold uppercase">Place Order -
                    <span class="step-title">Step <span id="spanCount" runat="server">1</span> of 6 </span>
                        </span>
                    </div>
                </div>
                <div class="portlet-body form ">
                    <div class="form-horizontal">
                        <div class="form-wizard">
                            <div class="form-body">
                                <ul class="nav nav-pills nav-justified steps">
                                    <li class="active" runat="server" id="liproductquantity">
                                        <a href="#" class="step" aria-expanded="true">
                                            <span class="number">1 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Product Quanity</span>
                                        </a>
                                    </li>
                                    <li runat="server" id="liorganic">
                                        <a href="#" class="step">
                                            <span class="number">2 </span>
                                            <br />
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Organic</span>
                                        </a>
                                    </li>
                                    <li runat="server" id="lifreightterms">
                                        <a href="#" class="step">
                                            <span class="number">3 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Freight Terms</span>
                                        </a>
                                    </li>
                                    <li runat="server" id="lipaymentterms">
                                        <a href="#" class="step">
                                            <span class="number">4 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Payment Terms</span>
                                        </a>
                                    </li>
                                    <li runat="server" id="liorderdetails">
                                        <a href="#" class="step">
                                            <span class="number">5 </span>
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Order Details</span>
                                        </a>
                                    </li>
                                    <li runat="server" id="liplaceorder">
                                        <a href="#" class="step">
                                            <span class="number">6 </span>
                                            <br />
                                            <span class="desc">
                                                <i class="fa fa-check"></i>Place order</span>
                                        </a>
                                    </li>
                                </ul>
                                <div id="bar" class="progress progress-striped" role="progressbar">
                                    <div id="divProgress" runat="server" class="progress-bar progress-bar-success" style="width: 16.67%;"></div>
                                </div>
                                <div class="tab-content">
                                    <div class="tab-pane active" id="tabProductQuantity" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center">
                                                <span style="color: red">* multiples of 25 and 180 kg only</span>
                                            </div>
                                            <div class="col-sm-12">
                                                <asp:GridView ID="grdProdQuantity" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductId" CssClass="table table-bordered mudargrid">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProductId" HeaderText="ProductID" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="10%" />
                                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" ItemStyle-Width="25%" />
                                                        <asp:BoundField DataField="Specification" HeaderText="Specification" ItemStyle-Width="25%" />
                                                        <asp:TemplateField HeaderText="Quantity (KG)" ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtQuantity" runat="server" CssClass="form-control" Text="25" />
                                                                <%-- <asp:DropDownList ID="ddlQuantity" runat="server" CssClass="form-control dropdown">
                                                                    <asp:ListItem Value="k25" Text="25"></asp:ListItem>
                                                                    <asp:ListItem Value="180" Text="180"></asp:ListItem>
                                                                </asp:DropDownList>--%>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField ItemStyle-Width="10%">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbitem" runat="server" onclick="javascript:Check_Click(this)" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbheader" runat="server" onclick="javascript:checkAll(this);" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="row" style="text-align: center">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClientClick="return validateGrid();" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tabOrganic" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center">
                                                <asp:Button ID="btnOrganic" runat="server" Text="Organic"
                                                    CssClass="btn btn-success" OnClick="btnOrganic_Click" />
                                                <asp:Button ID="btnOrganicFair" runat="server" Text="Organic & Fair"
                                                    CssClass="btn btn-default" Visible="false" OnClick="btnOrganicFair_Click" />
                                            </div>
                                        </div>
                                        <asp:Label ID="lblOrgType" runat="server" Visible="false" />
                                        <asp:Label ID="lblOrgFair" runat="server" Visible="false" />
                                        <asp:Label ID="lblOrgFTP" runat="server" Visible="false" />
                                    </div>
                                    <div class="tab-pane" id="tabFreightTerms" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblmoneytype" runat="server" Visible="false" />
                                                </div>
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblFoBPort" runat="server" Visible="false" />
                                                </div>
                                                <div class="col-sm-8" style="text-align: center">
                                                    <asp:Label ID="lblPlacedelivey" runat="server" Visible="false" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                    <asp:Label ID="lblFTValue" runat="server" Visible="false" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <div class="col-sm-4">
                                                        <%--<asp:RadioButton ID="rbFobIndia" runat="server" AutoPostBack="true"
                                                            OnCheckedChanged="rbFobIndia_CheckedChanged" />--%>
                                                        <label class="rbtncontainer">
                                                            FOB India
                                                            <asp:RadioButton ID="rbFobIndia" runat="server" AutoPostBack="true"
                                                                OnCheckedChanged="rbFobIndia_CheckedChanged" />
                                                            <span class="checkmark"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-5" style="text-align: left">
                                                        <asp:RadioButtonList ID="rbFob" runat="server" ForeColor="Red" RepeatDirection="Horizontal"
                                                            Visible="false" AutoPostBack="true"
                                                            OnSelectedIndexChanged="rbFob_SelectedIndexChanged">
                                                            <asp:ListItem Selected="True" Text="Air" Value="0" />
                                                            <asp:ListItem Text="Sea" Value="1" />
                                                        </asp:RadioButtonList>
                                                        <div id="divFob" runat="server" visible="false" class="row">
                                                            <div class="col-sm-3">
                                                                <label class="rbtncontainer">
                                                                    Air
                                                            <asp:RadioButton ID="rbtnFobAir" runat="server" GroupName="fob" AutoPostBack="true" OnCheckedChanged="rbFob_SelectedIndexChanged" />
                                                                    <span class="checkmark"></span>
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-3">
                                                                <label class="rbtncontainer">
                                                                    Sea
                                                            <asp:RadioButton ID="rbtnFobSea" runat="server" GroupName="fob" AutoPostBack="true" OnCheckedChanged="rbFob_SelectedIndexChanged" />
                                                                    <span class="checkmark"></span>
                                                                </label>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">

                                                    <div class="col-sm-4">
                                                        <label class="rbtncontainer">
                                                            CIF by SEA
                                                            <asp:RadioButton ID="rbCIFSea" runat="server" AutoPostBack="true"
                                                                OnCheckedChanged="rbCIFSea_CheckedChanged" />
                                                            <span class="checkmark"></span>
                                                        </label>


                                                    </div>
                                                    <div class="col-sm-5" style="text-align: left">
                                                        <asp:TextBox ID="txtSeaPort" runat="server" Style="padding: 1px; padding-left: 5px;" Height="25px" Visible="false"
                                                            Width="184px" BorderColor="SpringGreen" ForeColor="Teal" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <div class="col-sm-4">
                                                        <label class="rbtncontainer">
                                                            CIF by AIR
                                                           <asp:RadioButton ID="rbCIFAir" runat="server" AutoPostBack="true"
                                                               OnCheckedChanged="rbCIFAir_CheckedChanged" />
                                                            <span class="checkmark"></span>
                                                        </label>
                                                    </div>
                                                    <div class="col-sm-5" style="text-align: left">
                                                        <asp:TextBox ID="txtAirPort" runat="server" Height="25px" Style="padding: 1px; padding-left: 5px;" Visible="false" Width="184px"
                                                            BorderColor="SpringGreen" ForeColor="Teal" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <div class="col-sm-5">
                                                        <label class="rbtncontainer">
                                                            FOR Destination
                                                           <asp:RadioButton ID="rbForDestination" runat="server" OnCheckedChanged="rbForDestination_CheckedChanged" />
                                                            <span class="checkmark"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center; margin-top: 10px">
                                                <asp:Button ID="btnFrightTermssubmit" runat="server" CssClass="btn btn-success" Text="Submit"
                                                    OnClick="btnFrightTermssubmit_Click" />
                                            </div>
                                        </div>

                                    </div>
                                    <div class="tab-pane" id="tabPaymentTerms" runat="server">
                                        <asp:Label ID="lblPTValue" runat="server" Visible="false" />
                                        <asp:Label ID="lblDays" runat="server" Visible="false" />
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <div class="col-sm-8">
                                                        <label class="rbtncontainer">
                                                            100% with PO
                                                            <asp:RadioButton ID="rb100Adv" runat="server" AutoPostBack="true" OnCheckedChanged="rb100Adv_CheckedChanged" />
                                                            <span class="checkmark"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <div class="col-sm-8">
                                                        <label class="rbtncontainer">
                                                            50% with PO + 50% Against Delivery
                                                            <asp:RadioButton ID="rb50Adv50AgnDevi" runat="server" AutoPostBack="true" OnCheckedChanged="rb50Adv50AgnDevi_CheckedChanged" />
                                                            <span class="checkmark"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <div class="col-sm-8">
                                                        <label class="rbtncontainer">
                                                            100% against Delivery
                                                            <asp:RadioButton ID="rb100AgnDelivery" runat="server" AutoPostBack="True" OnCheckedChanged="rb100AgnDelivery_CheckedChanged" />
                                                            <span class="checkmark"></span>
                                                        </label>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-sm-12">
                                                <div class="col-sm-5"></div>
                                                <div class="col-sm-7">
                                                    <div class="col-sm-8">
                                                        <div class="row" style="margin: 0px !important">
                                                            <div class="col-sm-3" style="margin-left: -14px">
                                                                <label class="rbtncontainer">
                                                                    100%-
                                                                    <asp:RadioButton ID="rb100PaySelectDays" runat="server" AutoPostBack="true" OnCheckedChanged="rb100PaySelectDays_CheckedChanged" />
                                                                    <span class="checkmark"></span>
                                                                </label>
                                                            </div>
                                                            <div class="col-sm-9" style="margin-left: -12px">
                                                                <asp:DropDownList ID="ddlDays" runat="server" AutoPostBack="true"
                                                                    OnSelectedIndexChanged="ddlDays_SelectedIndexChanged" Visible="false">
                                                                    <asp:ListItem Text="15" Value="15" />
                                                                    <asp:ListItem Text="30" Value="30" />
                                                                    <asp:ListItem Text="45" Value="45" />
                                                                    <asp:ListItem Text="60" Value="60" />
                                                                </asp:DropDownList>
                                                                <span style="font-size: 15px">Days from the date of Invoice
                                                                </span>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12" style="text-align: center; margin-top: 25px">
                                                <asp:Button ID="btnPaymentTermsSubmit" runat="server" CssClass="btn btn-success" Text="Submit"
                                                    OnClick="btnPaymentTermsSubmit_Click" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tabOrderDetails" runat="server">
                                        <div class="row">
                                            <div class="col-sm-12">
                                                <div class="col-sm-4">
                                                    <span>Price Terms</span><br />
                                                    <asp:Label ID="lblprice" runat="server" ForeColor="OrangeRed"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <span>Payment Terms</span><br />
                                                    <asp:Label ID="lblPayment" runat="server" ForeColor="OrangeRed"></asp:Label>
                                                </div>
                                                <div class="col-sm-4">
                                                    <span>Currency in</span><br />
                                                    <asp:Label ID="lblCur" runat="server" ForeColor="OrangeRed"></asp:Label>
                                                </div>
                                            </div>
                                            <div class="colsm-12" style="margin-top: 50px">
                                                <asp:GridView ID="gvProductPricDetails" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="ProductId" OnRowDataBound="gvProductPricDetails_RowDataBound" CssClass="table table-bordered mudargrid">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProductId" HeaderText="ProductID" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                                        <asp:BoundField DataField="Quantity" HeaderText="Qty (KG)" ItemStyle-HorizontalAlign="Center" />
                                                        <asp:BoundField DataField="100% Advance" HeaderText="Rate/KG" ItemStyle-HorizontalAlign="Center"
                                                            Visible="false">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="50%Advance" HeaderText="Rate/KG"
                                                            ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="100% against delivery" HeaderText="Rate/KG"
                                                            DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="15_Days" HeaderText="Rate/KG"
                                                            DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="30_Days" HeaderText="Rate/KG"
                                                            DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="45_Days" HeaderText="Rate/KG"
                                                            DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="60_Days" HeaderText="Rate/KG"
                                                            DataFormatString="{0:0.00}" ItemStyle-HorizontalAlign="Center" Visible="false">
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        </asp:BoundField>
                                                        <asp:BoundField DataField="TotalPrice" HeaderText="Total Amt" ItemStyle-HorizontalAlign="Center" />
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
                                                        <asp:TemplateField Visible="false">
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="cbitem" runat="server" />
                                                            </ItemTemplate>
                                                            <HeaderTemplate>
                                                                <asp:CheckBox ID="cbheader" runat="server" onclick="javascript:selectallproduct(this);" />
                                                            </HeaderTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate><span>No Product(s) found.</span></EmptyDataTemplate>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-sm-12" style="margin-top: 25px; text-align: center">
                                                <asp:Button ID="btnorder" runat="server" Text="Place Order" CssClass="btn btn-success" OnClientClick="return checkDrumvalue();" OnClick="btnorder_Click" />
                                                &nbsp;
                                                <asp:Button ID="btnLotSample" runat="server" Text="Lot Samples" CssClass="btn btn-info" OnClientClick="return checkDrumvalue();" OnClick="btnLotSample_Click" Visible="false" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="tab-pane" id="tabPlaceorder" runat="server">
                                        <div id="divplaceorder" runat="server" visible="false" class="row">
                                            <div class="col-sm-12">
                                                <div class="form-group" style="margin: 10px">
                                                    <span>Comments</span>
                                                    <asp:TextBox ID="txtcomments" TextMode="MultiLine" runat="server" Height="70px" Style="margin-bottom: 1px"
                                                        CssClass="form-control"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-sm-12" style="text-align: center">
                                                <asp:Button ID="btnFinish" runat="server" Text="Finish" CssClass="btn btn-success" OnClick="btnFinish_Click" OnClientClick="Confirm()" />
                                            </div>
                                            <table style="display: none">
                                                <tr id="trpodate" runat="server" visible="false">
                                                    <td align="right">PO Date
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPODate" runat="server" CssClass="textbox_Style" />
                                                        <asp:CalendarExtender ID="dtpLastDate" runat="server" TargetControlID="txtPODate"></asp:CalendarExtender>
                                                    </td>
                                                </tr>
                                                <tr id="trupload" runat="server" visible="false">
                                                    <td align="right">Upload PO&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:FileUpload ID="FileUpload1" runat="server" />
                                                    </td>
                                                </tr>
                                                <tr id="trpo" runat="server" visible="false">
                                                    <td align="right">PO Number
                                                    </td>
                                                    <td align="left">
                                                        <asp:TextBox ID="txtPO" runat="server" Text="" CssClass="textbox_Style" />
                                                    </td>
                                                </tr>

                                            </table>
                                        </div>
                                        <div id="divLotsample" runat="server" visiable="false">
                                            <table align="center" visible="false">
                                                <tr>
                                                    <td align="right">No of Days&nbsp;&nbsp;
                                                    </td>
                                                    <td style="width: 70%; text-align: left">
                                                        <asp:TextBox ID="txtTotalDays" runat="server" Style="width: 30%; height: 30px;" Width="285px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" align="center">
                                                        <asp:RequiredFieldValidator ID="rvTotalDays" ControlToValidate="txtTotalDays" runat="server"
                                                            ErrorMessage="!!!Please enter No. of days!!!" ForeColor="Red" Display="Dynamic"
                                                            ValidationGroup="days"></asp:RequiredFieldValidator>
                                                        <asp:CompareValidator ID="cvTotalDays" ControlToValidate="txtTotalDays" runat="server"
                                                            ErrorMessage="!!!Please enter valid No. of days!!!" ForeColor="Red" Display="Dynamic"
                                                            ValidationGroup="days" Type="Integer" Operator="DataTypeCheck"></asp:CompareValidator>
                                                        <asp:RangeValidator ID="rngTotalDays" runat="server" ControlToValidate="txtTotalDays"
                                                            ErrorMessage="!!!Please enter No. of days between 1 to 365 only!!!" ForeColor="Red"
                                                            Display="Dynamic" ValidationGroup="days" MinimumValue="1" MaximumValue="365"
                                                            Type="Integer"></asp:RangeValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="2" style="text-align: center">
                                                        <asp:Button ID="btnLotSamples" runat="server" Text="Submit" CssClass="fb8" OnClick="btnLotSamples_Click"
                                                            ValidationGroup="days" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    <asp:Button ID="btnClose" runat="server" Text="Close" CssClass="fb8" Visible="false"
                                        OnClick="btnClose_Click" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
