<%@ Page Language="C#" Title="Mudar Order Process" MasterPageFile="~/MudarMasterNew.master" EnableEventValidation="false" AutoEventWireup="true" CodeFile="UpdateOrderNew.aspx.cs" Inherits="Admin_UpdateOrder" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%-- <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>--%>
    <style type="text/css">
        .textbox {
            padding-left: 3px;
        }

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

        .FixedHeader {
            position: absolute;
            font-weight: bold;
        }

        .divauto {
            height: auto;
            max-height: 400px;
            overflow-y: auto;
        }

        #ctl00_body_cph_chkICSList label {
            padding-left: 5px;
        }

        #ctl00_body_cph_chkVillageist label {
            padding-left: 5px;
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

        .inputDatePicker {
            background-color: #CCC;
        }

        td.tdFreezeDL > div > table > tbody > tr > td:nth-child(1) {
            width: 95px;
        }

        td.tdFreezeDL > div > table > tbody > tr > td:nth-child(2) {
            width: 95px;
        }
    </style>
    <link rel="stylesheet" href="../Style/jquery-ui.css">
    <%--<script src="../Style/jquery-1.6.4.js"></script>--%>
    <%--<script src="http://code.jquery.com/jquery-1.6.4.js"></script>--%>
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



        var localData = [{ "Id": "1", "Name": "COLLECTING" }, { "Id": "2", "Name": "BLENDING" },
        { "Id": "3", "Name": "FREEZING" }, { "Id": "4", "Name": "TESTING" }, { "Id": "5", "Name": "PACKING" }
            , { "Id": "6", "Name": "DOCUMENTING" }, { "Id": "7", "Name": "DISPATCH" }];


        $(document).ready(function () {
            //$("#accordion").accordion({
            //    collapsible: true,
            //    heightStyle: "content",
            //    disabled: [2, 3]
            //});

            $('#ankToggle').click(function () {
                if ($('#<%= trTotalQtyRow.ClientID %>').is(":visible")) {
                    $('#<%= trTotalQtyRow.ClientID %>').hide();
                } else {
                    $('#<%= trTotalQtyRow.ClientID %>').show();
                }

                if ($('#<%= trCollectGridRow.ClientID %>').is(":visible")) {
                    $('#<%= trCollectGridRow.ClientID %>').hide();
                } else {
                    $('#<%= trCollectGridRow.ClientID %>').show();
                }
                return false;
            });

        });
        function ManageTabs(name, status, isFreeze, orderType) {
            var activeValue = 0;
            var statusValue = 0;
            var active = 0;
            var disableArray = [];
            var disableStartIndex = 0;
            if (name !== '') {
                $.each(localData, function (k, v) {
                    if (v.Name == name) {
                        active = Number(v.Id) - 1;
                        activeValue = Number(v.Id);
                    }

                    if (v.Name == status) {
                        disableStartIndex = Number(v.Id);
                        statusValue = Number(v.Id);
                    }
                });
            }
            if (isFreeze == 'False' && status == 'BLENDING') {
                disableStartIndex = 3;
            }
            //if (activeValue < statusValue) {
            //    disableStartIndex = disableStartIndex - 1;
            //}

            if (orderType == 'LotSample' && active >= 3) {
                active = 3;
                disableStartIndex = 3;
            }

            $("#accordion").accordion({
                collapsible: true,
                heightStyle: "content",
                active: active
                //disabled: disableArray
            });

            $('#accordion > h3:gt(' + disableStartIndex + ')').addClass('ui-state-disabled');
            if (isFreeze == 'False') {
                $('#accordion > h3:eq(2)').addClass('ui-state-disabled');
            }
            return true;
        }
        function ManageStartupTabs(name, status, isFreeze, orderType) {
            var active = 0;
            var disableArray = [];
            var disableStartIndex = 0;
            if (name !== '') {
                $.each(localData, function (k, v) {
                    if (v.Name == name) {
                        active = Number(v.Id);
                    }

                    if (v.Name == status) {
                        disableStartIndex = Number(v.Id);
                    }
                });
            }
            if (isFreeze == 'False' && status == 'BLENDING') {
                active = 3;
                disableStartIndex = 3;
            }
            if (orderType == 'LotSample' && active >= 3) {
                active = 3;
                disableStartIndex = 3;
            }
            else if (active > 6) {
                active = 6;
            }


            $("#accordion").accordion({
                collapsible: true,
                heightStyle: "content",
                active: active
                //disabled: disableArray
            });
            $('#accordion > h3:gt(' + disableStartIndex + ')').addClass('ui-state-disabled');
            if (isFreeze == 'False') {
                $('#accordion > h3:eq(2)').addClass('ui-state-disabled');
            }
            return false;
        }
    </script>
    <style type="text/css">
        .gvheader th {
            color: blue;
            text-align: center;
            font-weight: bold;
            text-decoration: underline;
        }

        .ui-datepicker-trigger {
            height: 26px;
        }

        #tdGrid div {
            text-align: center;
            display: inline-block;
        }
    </style>
    <script type="text/javascript">
        $(function () {
            $('#divCollectingdetails').children("div").first().attr("class", "divauto");
            $.expr[":"].containsNoCase = function (el, i, m) {
                var search = m[3];
                if (!search) return false;
                return eval("/" + search + "/i").test($(el).text());
            };
            $('#txtSearch').keyup(function () {
                if (event.keyCode == 27) {
                    resetSearchValue();
                    return;
                }
                if ($('#txtSearch').val().length > 1) {
                    //var tbl=$('#divCollectingdetails').children("table").first();
                    $('#<%= gvCollecingDetails.ClientID%> tr').hide();
                    $('#<%= gvCollecingDetails.ClientID%> tr:first').show();
                    $('#<%= gvCollecingDetails.ClientID%> tr td:containsNoCase(\'' + $('#txtSearch').val() + '\')').parent().show();
                }
                else if ($('#txtSearch').val().length == 0) {
                    resetSearchValue();
                }
            });
        });

        function resetSearchValue() {
            $('#txtSearch').val('');
            $('#<%= gvCollecingDetails.ClientID%> tr').show();
            $('.norecords').remove();
            $('#txtSearch').focus();
        }
        function fnShowMessage(msg) {
            bootbox.alert(msg);
        }
        function validateGridQuantity() {

            var chktest1 = 0;
            var X = parseFloat(document.getElementById('<%=hdnCollectionQuantity.ClientID%>').value);
            var str = '';
            var Grid_Table = document.getElementById('<%= gvCollecingDetails.ClientID %>');
            document.getElementById('<%=btnFarmerCollectSearchapply.ClientID%>').style.display = 'block';
            document.getElementById('<%=btnFarmerCollectSearchapplyDisabled.ClientID%>').style.display = 'none';
            for (var row = 1; row < Grid_Table.rows.length; row++) {
                var col2 = Grid_Table.rows[row].cells[7];
                for (var j = 0; j < col2.childNodes.length; j++) {
                    if (col2.childNodes[j].type == "checkbox") {
                        if (col2.childNodes[j].checked) {
                            //document.getElementById('<%=btnFarmercollt.ClientID%>').disabled = false;
                            var col1 = Grid_Table.rows[row].cells[6];
                            for (var k = 0; k < col1.childNodes.length; k++) {
                                if (col1.childNodes[k].type == "text") {
                                    if (!isNaN(col1.childNodes[k].value) && col1.childNodes[k].value != "") {
                                        var curr = parseFloat(col1.childNodes[k].value);
                                        var availCell = Grid_Table.rows[row].cells[5];
                                        var availQty = 0;
                                        if ((availCell.textContent) && (typeof (availCell.textContent) != "undefined")) {
                                            availQty = parseFloat(availCell.textContent);
                                        } else {
                                            availQty = parseFloat(availCell.innerText);
                                        }
                                        if (curr == 0) {
                                            alert('!!! Quantity must be atleast one KG !!! ');
                                            col2.childNodes[j].checked = false;
                                            col1.childNodes[k].value = '0';
                                            return;
                                        }
                                        else if (/^\d{1,5}(\.\d{1,2})?$/.test(col1.childNodes[k].value) == false) {
                                            alert('!!! Please Enter valid Quantity !!! ');
                                            col2.childNodes[j].checked = false;
                                            col1.childNodes[k].value = '0';
                                            return;
                                        }
                                        else if (curr > availQty) {
                                            alert('Plz Check the Available Quantity');
                                            col2.childNodes[j].checked = false;
                                            col1.childNodes[k].value = '0';
                                            return;
                                        }
                                        else {
                                            X += Number(parseFloat(col1.childNodes[k].value).toFixed(2));

                                        }
                                    }
                                    else {
                                        col1.childNodes[k].value = '0';
                                        alert('!!! Please Enter the Quantity !!!');
                                        col2.childNodes[j].checked = false;
                                        return;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            var runningTotal = document.getElementById('<%=lblRunningTotal.ClientID%>');
            if ((runningTotal.textContent) && (typeof (runningTotal.textContent) != "undefined")) {
                runningTotal.textContent = X;
            } else {
                runningTotal.innerText = X;;
            }
            return false;
        }
        function calculateQuantity() {
            document.getElementById('<%= btnFarmercollt.ClientID %>').style.display = 'block';
            document.getElementById('<%= btnDisablecollectSave.ClientID %>').style.display = 'none';
            var chktest1 = 0;
            var X = parseFloat(document.getElementById('<%=hdnCollectionQuantity.ClientID%>').value);
            var str = '';
            var Grid_Table = document.getElementById('<%= gvSelectedCollectionDetails.ClientID %>');

            var Grid_Table1 = document.getElementById('<%= gvPreorderCollection.ClientID %>');
            for (var row = 1; row < Grid_Table1.rows.length; row++) {
                var col2 = Grid_Table1.rows[row].cells[5];
                for (var j = 0; j < col2.childNodes.length; j++) {
                    if (col2.childNodes[j].type == "checkbox") {
                        if (col2.childNodes[j].checked) {
                            var col1 = Grid_Table1.rows[row].cells[4];
                            for (var k = 0; k < col1.childNodes.length; k++) {
                                if (col1.childNodes[k].type == "text") {
                                    X = X + parseFloat(col1.childNodes[k].value);
                                }
                            }
                        }
                    }
                }
            }
            for (var row = 1; row < Grid_Table.rows.length; row++) {
                var col2 = Grid_Table.rows[row].cells[7];
                for (var j = 0; j < col2.childNodes.length; j++) {
                    if (col2.childNodes[j].type == "checkbox") {
                        if (col2.childNodes[j].checked) {
                            document.getElementById('<%=btnFarmercollt.ClientID%>').disabled = false;
                            var col1 = Grid_Table.rows[row].cells[6];
                            for (var k = 0; k < col1.childNodes.length; k++) {
                                if (col1.childNodes[k].type == "text") {
                                    if (!isNaN(col1.childNodes[k].value) && col1.childNodes[k].value != "") {
                                        var curr = parseFloat(col1.childNodes[k].value);
                                        var availQty = parseFloat(Grid_Table.rows[row].cells[5].innerText);
                                        if (curr == 0) {
                                            alert('!!! Quantity must be atleast one KG !!! ');
                                            col2.childNodes[j].checked = false;
                                            col1.childNodes[k].value = '0';
                                        }
                                        else if (/^\d{1,5}(\.\d{1,2})?$/.test(col1.childNodes[k].value) == false) {
                                            alert('!!! Please Enter valid Quantity !!! ');
                                            col2.childNodes[j].checked = false;
                                            col1.childNodes[k].value = '0';
                                        }
                                        else if (curr > availQty) {
                                            alert('Plz Check the Available Quantity');
                                            col2.childNodes[j].checked = false;
                                            col1.childNodes[k].value = '0';
                                        }
                                        else {
                                            X += Number(parseFloat(col1.childNodes[k].value).toFixed(2));
                                        }
                                    }
                                    else {
                                        col1.childNodes[k].value = '0';
                                        alert('!!! Please Enter the Quantity !!!');
                                        col2.childNodes[j].checked = false;
                                    }
                                }
                            }
                        }

                    }
                }
            }
            document.getElementById('<%=lblpresentqty.ClientID%>').innerText = X;
            var Y = parseFloat(document.getElementById('<%=lblOrderQuantity.ClientID%>').innerText);
            document.getElementById('<%=lblAlrcollQty.ClientID%>').innerText = (Y - X);
            return false;
        }
        function calculatePreorderQuantity() {
            var Y = 0;
            var orderQuantity = document.getElementById('<%=lblOrderQuantity.ClientID%>');
            if ((orderQuantity.textContent) && (typeof (orderQuantity.textContent) != "undefined")) {
                Y = orderQuantity.textContent;
            } else {
                Y = runningTotal1.innerText;
            }
            var chktest1 = 0;
            var X = parseFloat(document.getElementById('<%=hdnCollectionQuantity.ClientID%>').value);
            var str = '';
            var Grid_Table = document.getElementById('<%= gvPreorderCollection.ClientID %>');

            var Grid_Table1 = document.getElementById('<%= gvCollecingDetails.ClientID %>');
            for (var row = 1; row < Grid_Table1.rows.length; row++) {
                var col2 = Grid_Table1.rows[row].cells[7];
                for (var j = 0; j < col2.childNodes.length; j++) {
                    if (col2.childNodes[j].type == "checkbox") {
                        if (col2.childNodes[j].checked) {
                            var col1 = Grid_Table1.rows[row].cells[6];
                            for (var k = 0; k < col1.childNodes.length; k++) {
                                if (col1.childNodes[k].type == "text") {
                                    X = X + parseFloat(col1.childNodes[k].value);
                                }
                            }
                        }
                    }
                }
            }
            for (var row = 1; row < Grid_Table.rows.length; row++) {
                var col2 = Grid_Table.rows[row].cells[5];
                for (var j = 0; j < col2.childNodes.length; j++) {
                    if (col2.childNodes[j].type == "checkbox") {
                        if (col2.childNodes[j].checked) {
                            document.getElementById('<%=btnPrecollt.ClientID%>').disabled = false;
                            var col1 = Grid_Table.rows[row].cells[4];
                            for (var k = 0; k < col1.childNodes.length; k++) {
                                if (col1.childNodes[k].type == "text") {
                                    if (!isNaN(col1.childNodes[k].value) && col1.childNodes[k].value != "") {
                                        var curr = parseFloat(col1.childNodes[k].value);
                                        //var availQty = parseFloat(Grid_Table.rows[row].cells[3].innerText);

                                        var availCell = Grid_Table.rows[row].cells[3];
                                        var availQty = 0;
                                        if ((availCell.textContent) && (typeof (availCell.textContent) != "undefined")) {
                                            availQty = parseFloat(availCell.textContent);
                                        } else {
                                            availQty = parseFloat(availCell.innerText);
                                        }

                                        if (curr < 1) {
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
                                            X += Number(parseFloat(col1.childNodes[k].value).toFixed(2));
                                            if (X > Y) {
                                                alert('Collected Quantity should not exceed Order Quantity');
                                                col2.childNodes[j].checked = false;
                                                col1.childNodes[k].value = '0';
                                                return;
                                            }
                                            document.getElementById('<%=btnPrecollt.ClientID%>').style.display = 'block';
                                            document.getElementById('<%=btnDisablepreorderSave.ClientID%>').style.display = 'none';
                                        }
                                    }
                                    else {
                                        col1.childNodes[k].value = '0';
                                        alert('!!! Please Enter the Quantity !!!');
                                    }
                                }
                            }
                        }
                        //else {
                        //    var colText = Grid_Table.rows[row].cells[4];
                        //    for (var k = 0; k < colText.childNodes.length; k++) {
                        //        if (colText.childNodes[k].type == "text") {
                        //            colText.childNodes[k].value = '0';
                        //        }
                        //    }
                        //}
                    }
                }
            }
            var runningTotal = document.getElementById('<%=lblpresentqty.ClientID%>');
            if ((runningTotal.textContent) && (typeof (runningTotal.textContent) != "undefined")) {
                runningTotal.textContent = X;
            } else {
                runningTotal.innerText = X;;
            }


            var runningTotal1 = document.getElementById('<%=lblAlrcollQty.ClientID%>');
            if ((runningTotal1.textContent) && (typeof (runningTotal1.textContent) != "undefined")) {
                runningTotal1.textContent = Y - X;
            } else {
                runningTotal1.innerText = Y - X;
            }
            return false;
        }
        function checkAll(objRef) {
            var GridView = objRef.parentNode.parentNode.parentNode;
            var inputList = GridView.getElementsByTagName("input");
            var orderQuantity = document.getElementById('<%=lblNetQty.ClientID%>');
            for (var i = 0; i < inputList.length; i++) {
                var row = inputList[i].parentNode.parentNode;
                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {
                    if (objRef.checked) {
                        inputList[i].checked = true;
                        document.getElementById('<%=lblAlrBlendQty.ClientID%>').textContent = document.getElementById('<%=lblNetQty.ClientID%>').textContent;
                        document.getElementById('<%=lblBlendQty.ClientID%>').textContent = '0.00';
                    }
                    else {
                        inputList[i].checked = false;
                        document.getElementById('<%=lblAlrBlendQty.ClientID%>').textContent = '0.00';
                        document.getElementById('<%=lblBlendQty.ClientID%>').textContent = document.getElementById('<%=lblNetQty.ClientID%>').textContent;
                    }

                }
            }
        }
        function calculateBlendingQuantity() {
            document.getElementById('<%= lblBlendQty.ClientID %>').style.display = 'block';
            var chktest1 = 0;
            var X = parseFloat(document.getElementById('<%=hdnBlendingQuantity.ClientID%>').value);
            var str = '';



            var Grid_Table = document.getElementById('<%= gvBlending.ClientID %>');
            if (Grid_Table != null) {
                for (var row = 1; row < Grid_Table.rows.length; row++) {
                    var col2 = Grid_Table.rows[row].cells[5];
                    for (var j = 0; j < col2.childNodes.length; j++) {
                        if (col2.childNodes[j].type == 'checkbox') {
                            if (col2.childNodes[j].checked) {
                                //X = X + Number(parseFloat(Grid_Table.rows[row].cells[3].innerText).toFixed(2));;
                                var col1 = Grid_Table.rows[row].cells[4];
                                for (var k = 0; k < col1.childNodes.length; k++) {
                                    if (col1.childNodes[k].type == "text") {
                                        if (!isNaN(col1.childNodes[k].value) && col1.childNodes[k].value != "") {
                                            var curr = parseFloat(col1.childNodes[k].value);
                                            var availCell = Grid_Table.rows[row].cells[3];
                                            var availQty = 0;
                                            if ((availCell.textContent) && (typeof (availCell.textContent) != "undefined")) {
                                                availQty = parseFloat(availCell.textContent);
                                            } else {
                                                availQty = parseFloat(availCell.innerText);
                                            }
                                            if (curr == 0) {
                                                alert('!!! Quantity must be atleast one KG !!! ');
                                                col2.childNodes[j].checked = false;
                                                col1.childNodes[k].value = '0';
                                            }
                                            else if (/^\d{1,5}(\.\d{1,2})?$/.test(col1.childNodes[k].value) == false) {
                                                alert('!!! Please Enter valid Quantity !!! ');
                                                col2.childNodes[j].checked = false;
                                                col1.childNodes[k].value = '0';
                                            }
                                            else if (curr > availQty) {
                                                alert('Plz Check the Available Quantity');
                                                col2.childNodes[j].checked = false;
                                                col1.childNodes[k].value = '0';
                                            }
                                            else {
                                                X += Number(parseFloat(col1.childNodes[k].value).toFixed(2));
                                            }
                                        }
                                        else {
                                            col1.childNodes[k].value = '0';
                                            alert('!!! Please Enter the Quantity !!!');
                                            col2.childNodes[j].checked = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            Grid_Table = document.getElementById('<%= gvBlendPreorder.ClientID %>');
            if (Grid_Table != null) {
                for (var row = 1; row < Grid_Table.rows.length; row++) {
                    var col2 = Grid_Table.rows[row].cells[3];
                    for (var j = 0; j < col2.childNodes.length; j++) {
                        if (col2.childNodes[j].type == "checkbox") {
                            if (col2.childNodes[j].checked) {
                                //X = X + Number(parseFloat(Grid_Table.rows[row].cells[1].innerText).toFixed(2));;
                                var col1 = Grid_Table.rows[row].cells[2];
                                for (var k = 0; k < col1.childNodes.length; k++) {
                                    if (col1.childNodes[k].type == "text") {
                                        if (!isNaN(col1.childNodes[k].value) && col1.childNodes[k].value != "") {
                                            var curr = parseFloat(col1.childNodes[k].value);
                                            var availCell = Grid_Table.rows[row].cells[1];
                                            var availQty = 0;
                                            if ((availCell.textContent) && (typeof (availCell.textContent) != "undefined")) {
                                                availQty = parseFloat(availCell.textContent);
                                            } else {
                                                availQty = parseFloat(availCell.innerText);
                                            }
                                            if (curr == 0) {
                                                alert('!!! Quantity must be atleast one KG !!! ');
                                                col2.childNodes[j].checked = false;
                                                col1.childNodes[k].value = '0';
                                            }
                                            else if (/^\d{1,5}(\.\d{1,2})?$/.test(col1.childNodes[k].value) == false) {
                                                alert('!!! Please Enter valid Quantity !!! ');
                                                col2.childNodes[j].checked = false;
                                                col1.childNodes[k].value = '0';
                                            }
                                            else if (curr > availQty) {
                                                alert('Plz Check the Available Quantity');
                                                col2.childNodes[j].checked = false;
                                                col1.childNodes[k].value = '0';
                                            }
                                            else {
                                                X += Number(parseFloat(col1.childNodes[k].value).toFixed(2));
                                            }
                                        }
                                        else {
                                            col1.childNodes[k].value = '0';
                                            alert('!!! Please Enter the Quantity !!!');
                                            col2.childNodes[j].checked = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //document.getElementById('<%=lblBlendQty.ClientID%>').innerText = X;
            var Y = 0;
            var runningTotal1 = document.getElementById('<%=lblNetQty.ClientID%>');
            if ((runningTotal1.textContent) && (typeof (runningTotal1.textContent) != "undefined")) {
                Y = runningTotal1.textContent;
            } else {
                Y = runningTotal1.innerText;
            }
            //X = X + parseFloat(document.getElementById('<%=lblAlrBlendQty.ClientID%>').innerText);
            runningTotal1 = document.getElementById('<%=lblBlendQty.ClientID%>');
            if ((runningTotal1.textContent) && (typeof (runningTotal1.textContent) != "undefined")) {
                runningTotal1.textContent = (Y - X);
            } else {
                runningTotal1.innerText = (Y - X);
            }
            runningTotal1 = document.getElementById('<%=lblAlrBlendQty.ClientID%>');
            if ((runningTotal1.textContent) && (typeof (runningTotal1.textContent) != "undefined")) {
                runningTotal1.textContent = X;
            } else {
                runningTotal1.innerText = X;
            }
            return false;
        }


        function checkItemsForBlend() {
            var isChecked = false;

            var Grid_Table = document.getElementById('<%= gvBlending.ClientID %>');
            if (Grid_Table != null) {
                for (var row = 1; row < Grid_Table.rows.length; row++) {
                    var col2 = Grid_Table.rows[row].cells[5];
                    for (var j = 0; j < col2.childNodes.length; j++) {
                        if (col2.childNodes[j].type == 'checkbox') {
                            if (col2.childNodes[j].checked) {
                                isChecked = true;
                                break;
                            }
                        }
                    }
                }
            }

            Grid_Table = document.getElementById('<%= gvBlendPreorder.ClientID %>');
            if (Grid_Table != null) {
                for (var row = 1; row < Grid_Table.rows.length; row++) {
                    var col2 = Grid_Table.rows[row].cells[3];
                    for (var j = 0; j < col2.childNodes.length; j++) {
                        if (col2.childNodes[j].type == "checkbox") {
                            if (col2.childNodes[j].checked) {
                                isChecked = true;
                                break;
                            }
                        }
                    }
                }
            }

            if (isChecked == false) {
                alert('!!! Please select atleast one product for blending !!!');
                return false;
            }
            else {
                return true;
            }
        }

        function calculateBlendingPOQuantity() {
            document.getElementById('<%= lblBlendQty.ClientID %>').style.display = 'block';
            var chktest1 = 0;
            var X = 0;
            var str = '';
            var Grid_Table = document.getElementById('<%= gvBlendPreorder.ClientID %>');

            for (var row = 1; row < Grid_Table.rows.length; row++) {
                var col2 = Grid_Table.rows[row].cells[2];
                for (var j = 0; j < col2.childNodes.length; j++) {
                    if (col2.childNodes[j].type == "checkbox") {
                        if (col2.childNodes[j].checked) {
                            X = X + Number(parseFloat(Grid_Table.rows[row].cells[1].innerText).toFixed(2));;
                        }
                    }
                }
            }
            document.getElementById('<%=lblBlendQty.ClientID%>').innerText = X;
            if (X + parseInt(document.getElementById('<%=lblAlrBlendQty.ClientID%>').innerText) == parseInt(document.getElementById('<%=lblAlrBlendQty.ClientID%>').innerText))
                document.getElementById('<%=lblBlendQty.ClientID%>').innerText = X + parseInt(document.getElementById('<%=lblAlrBlendQty.ClientID%>').innerText);

            return false;
        }

    </script>
    <script>
        $(function () {
            $(".updateqnty").click(function () {

                calculateTotalQuantities();

            });
        });
        function calculateTotalQuantities() {
            var totalQuantity = 0;
            var totalActiveQuantities = $(".updateqnty input[type='checkbox']");
            $.each(totalActiveQuantities, function (index, value) {
                if ($(this).prop("checked")) {
                    var currentQuantity = $(this).parent().parent().prev().children().eq(0).val();
                    totalQuantity += parseFloat(currentQuantity);
                }
            });
            $("#ctl00_body_cph_lblRunningTotal").text(totalQuantity.toFixed(1));
        }
        calculateTotalQuantities();
    </script>
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Branch Order Details
        </div>
        <%-- <table cellpadding="5" cellspacing="5" border="0" style="width: 100%">
            <tr>
                <td colspan="7" align="center" style="text-align: center; color: white; font-weight: bold; background-color: #FDB700; font-size: 20px; height: 25px">Branch Order Details
                </td>
            </tr>
            <tr>
                <td style="width: 50px;"></td>
                <td>&nbsp;</td>
                <td style="text-align: center">Branch Order ID<br />

                    &nbsp;&nbsp;</td>
                <td style="width: 50px;">&nbsp;</td>
                <td style="text-align: center">Branch Purchase Order ID
                    <br />
                    &nbsp; 
                </td>
                <td style="text-align: left">Lot Sample ID<br />
                    &nbsp;&nbsp;</td>
                <td style="width: 50px;"></td>
            </tr>
            <tr>
                <td style="width: 50px;"></td>
                <td>&nbsp;</td>
                <td style="text-align: center"><br />
                    &nbsp;&nbsp;</td>
                <td style="width: 50px;"></td>
                <td style="text-align: center">Branch Purchase Order
                    <br />
                    &nbsp;&nbsp;</td>
                <td style="text-align: left">Lotsample<br />
                    &nbsp;&nbsp; &nbsp;&nbsp;</td>
                <td style="width: 50px;"></td>
            </tr>

            <%--<asp:BoundField DataField="AreaCode" HeaderText="PlotCode" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="AreaCode"></asp:BoundField>
        </table>--%>

        <table width="90%" align="center">
            <tr id="tr" runat="server" visible="false">
                <td colspan="2" align="center">Order ID  :&nbsp;
                    <asp:Label ID="lblBranchOrderID" runat="server" Text="" ForeColor="#FF6600" Visible="false" />
                    <asp:Label ID="lblBPOID" runat="server" Text="" ForeColor="#FF6600" Visible="false" />
                    <asp:Label ID="lblBLs" runat="server" ForeColor="#FF6600" Visible="false" /><asp:HyperLink ID="hlBranchPDF" runat="server" Text="Open" Target="_blank" Visible="false" />
                    <asp:HyperLink ID="hlLSpdf" runat="server" Text="Open" Target="_blank" Visible="false" />

                </td>
            </tr>

            <tr>

                <td width="50%" align="center">Branch Order ID</td>
                <td width="50%" align="center">Branch Order Type</td>
            </tr>
            <tr>

                <td width="50%" align="center">
                    <asp:Label ID="lblorderID" runat="server" Text="" ForeColor="#FF6600" />

                </td>
                <td width="50%" align="center">
                    <asp:Label ID="lblBOtype" runat="server" Text="" ForeColor="#FF6600" />
                </td>
            </tr>


        </table>

        <div id="accordion" style="background: none !important; border: none !important;">
            <h3>Collecting Details</h3>
            <div style="background: #E9E1CC">
                <table style="width: 100%">
                    <tr>
                        <td colspan="4" align="center">
                            <table>
                                <tr>
                                    <td>Product Name<br />
                                        <asp:DropDownList ID="ddlSelectProduct" runat="server" OnSelectedIndexChanged="ddlSelectProduct_SelectedIndexChanged"
                                            AutoPostBack="true" CssClass="form-control">
                                        </asp:DropDownList>
                                        <asp:HiddenField ID="hdnCollectionQuantity" runat="server" Value="0" />
                                    </td>
                                    <td style="padding-left: 10px">Selected Date<br />
                                        <asp:TextBox ID="txtSelDate" runat="server"
                                            Height="30px" Width="200px" Style="margin-bottom: 1px" Font-Size="Medium" autocomplete="false"></asp:TextBox>
                                        <asp:CalendarExtender ID="CalendarExtender4" runat="server" Format="dd/MM/yyyy"
                                            TargetControlID="txtSelDate">
                                        </asp:CalendarExtender>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                    </tr>
                    <tr id="trShowCollectedInfo" runat="server" visible="false">
                        <td colspan="4" align="center">
                            <table cellpadding="5" cellspacing="5">
                                <tr runat="server" id="trSubFromFarmer">
                                    <td align="left">Collected from Farmer :
                                        <asp:Label ID="ltlFarmerTotal" runat="server" Text="0" ForeColor="SeaGreen"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvCollectedFromFarmer" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerCode,FarmerID"
                                            CssClass="table table-bordered mudargrid">
                                            <Columns>
                                                <asp:BoundField DataField="FarmerID" HeaderText="Farmer ID" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="FarmID" HeaderText="Farmer ID" ItemStyle-HorizontalAlign="Center"
                                                    Visible="false"></asp:BoundField>
                                                <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                <asp:BoundField DataField="FarmerName" HeaderText="Farmer Name" />
                                                <asp:BoundField DataField="PlotCode" HeaderText="Plot Code" Visible="false" />
                                                <asp:BoundField DataField="Lotnumber" HeaderText="Batch No" />
                                                <asp:BoundField DataField="CollectionQty" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:0.00}"></asp:BoundField>
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="gvheader" />
                                            <AlternatingRowStyle CssClass="gvalternate" />
                                            <RowStyle CssClass="gvnormal" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr runat="server" id="trSubFromPreorder">
                                    <td align="left">Collected from Pre-Order List :
                                        <asp:Label ID="ltlPreorderTotal" runat="server" Text="0" ForeColor="SeaGreen"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:GridView ID="gvCollectedFromPreorder" runat="server" AutoGenerateColumns="False">
                                            <Columns>
                                                <asp:BoundField DataField="Blending_BatchID" HeaderText="Batch ID" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                                <asp:BoundField DataField="CollectionQty" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.00}"></asp:BoundField>
                                                <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-HorizontalAlign="Center"
                                                    DataFormatString="{0:dd MMM yyyy}" HtmlEncode="false"></asp:BoundField>
                                            </Columns>
                                            <HeaderStyle CssClass="gvheader" />
                                            <AlternatingRowStyle CssClass="gvalternate" />
                                            <RowStyle CssClass="gvnormal" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr id="trShowICSList" runat="server" visible="false">
                        <td colspan="4" align="center">
                            <asp:CheckBoxList ID="chkICSList" AutoPostBack="true" CellPadding="4" CellSpacing="2" runat="server" RepeatDirection="Horizontal" OnSelectedIndexChanged="chkICSList_SelectedIndexChanged"></asp:CheckBoxList>
                            <asp:CheckBoxList ID="chkVillageist" AutoPostBack="true" CellPadding="4" CellSpacing="2" runat="server" RepeatDirection="Horizontal" Visible="false" OnSelectedIndexChanged="chkVillageist_SelectedIndexChanged"></asp:CheckBoxList>
                            <asp:Label ID="lbAqty" runat="server" ForeColor="Tan" />
                        </td>
                    </tr>
                    <tr id="trButtonDetails" runat="server" visible="false">
                        <td align="center">
                            <asp:Button ID="btnFarmer" runat="server" Text="Collection from Farmer" CssClass="btnFarmer"
                                Width="250px" OnClick="btnFarmer_Click" Enabled="false" />
                        </td>
                        <td colspan="2" align="center">
                            <asp:Label ID="lblMCText" runat="server" Text="Actual Order Qty" Visible="false" /><br />
                            <asp:Label ID="lblMCQty" runat="server" Text="0" Visible="false" ForeColor="Tomato" />
                        </td>
                        <td align="center">
                            <asp:Button ID="btnPreorder" runat="server" Text="PreOrder List" CssClass="btnFarmer"
                                Width="250px" OnClick="btnPreorder_Click" Enabled="false" />
                        </td>

                    </tr>
                    <tr id="trShowDetails" runat="server" visible="false">
                        <td align="center">Order Quantity<br />
                            <asp:Label ID="lblOrderQuantity" runat="server" Text="0" ForeColor="DarkOrange"></asp:Label>
                        </td>
                        <td colspan="2" align="center">Collected Quantity<br />
                            <asp:Label ID="lblpresentqty" runat="server" Text="0" ForeColor="DarkGreen"></asp:Label>
                        </td>
                        <td align="center">Remaining Quantity<br />
                            <asp:Label ID="lblAlrcollQty" runat="server" Text="0" ForeColor="SeaGreen"
                                Visible="true"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr id="trOtherFarmer" runat="server" visible="false">
                        <td align="center">Other Farmer Name<br />
                            <asp:TextBox ID="txtOtherFarmers" runat="server" Text="0" Width="140px"></asp:TextBox>
                        </td>
                        <td colspan="2" align="center">Other Farmer Qty<br />
                            <asp:TextBox ID="txtCollectQTY" runat="server" Text="0" Width="140px"></asp:TextBox>
                        </td>
                        <td align="center">
                            <asp:Button ID="btnGet" runat="server" CssClass="fb8" Text="Save" OnClick="btnGet_Click" />
                        </td>
                    </tr>

                    <tr>
                        <td colspan="4">
                            <div id="divFarmerCollecting" runat="server" align="center" visible="false">
                                <table>
                                    <tr>
                                        <td>
                                            <table align="center" style="padding-left: 5px; font-weight: bold; padding: 5px; width: 680px;">
                                                <tbody>
                                                    <tr>
                                                        <td align="center" style="text-align: center">&nbsp;</td>
                                                    </tr>
                                                </tbody>
                                            </table>


                                        </td>
                                    </tr>
                                    <tr id="trcollecteditems" runat="server" visible="false">
                                        <td align="center">
                                            <%--Selected Items--%>
                                            <a href="javascript:void(0);" id="ankToggle">Show/Hide Farmer Collection Details</a>
                                            <asp:GridView align="center" HorizontalAlign="Center" ID="gvSelectedCollectionDetails" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="PlantationId,FarmerID,FarmID" OnSorting="gvSelectedCollectionDetails_Sorting"
                                                AllowSorting="true" CssClass="table table-bordered mudargrid" OnRowEditing="gvSelectedCollectionDetails_RowEditing" OnRowDeleting="gvSelectedCollectionDetails_RowDeleting"
                                                OnRowCancelingEdit="gvSelectedCollectionDetails_RowCancelingEdit" OnRowUpdating="gvSelectedCollectionDetails_RowUpdating"
                                                OnRowDataBound="gvSelectedCollectionDetails_RowDataBound" HeaderStyle-Wrap="true">
                                                <Columns>
                                                    <%--<asp:BoundField DataField="FarmerId" HeaderText="Farmer ID" />
                            <asp:BoundField DataField="FarmID" HeaderText="Farm ID" />--%>
                                                    <asp:BoundField DataField="FarmerCode" ReadOnly="true" HeaderText="Farmer Code" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center" SortExpression="FarmerCode"></asp:BoundField>
                                                    <asp:BoundField DataField="FirstName" ReadOnly="true" HeaderText="Farmer Name" HeaderStyle-HorizontalAlign="Center"
                                                        SortExpression="FirstName" HeaderStyle-Width="150"></asp:BoundField>
                                                    <%--<asp:BoundField DataField="AreaCode" HeaderText="PlotCode" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="AreaCode"></asp:BoundField>--%>
                                                    <%-- <asp:BoundField DataField="Lot_No" ReadOnly="true" HeaderText="Batch No" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="120"
                                                    SortExpression="Lot_No"></asp:BoundField>
                                                <asp:BoundField DataField="TotalProductQuantity" ReadOnly="true" HeaderText="Actual Quanity" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.00}" SortExpression="TotalProductQuantity" HeaderStyle-Width="100"></asp:BoundField>
                                                <asp:BoundField DataField="SoldTotalQty" ReadOnly="true" HeaderText="Sold Quanity" HeaderStyle-HorizontalAlign="Center"
                                                    ItemStyle-HorizontalAlign="Center" SortExpression="SoldTotalQty" HeaderStyle-Width="90"></asp:BoundField>--%>
                                                    <asp:BoundField DataField="Avaliable" ReadOnly="true" HeaderText="Avaliable Quanity" ItemStyle-Font-Bold="true"
                                                        HeaderStyle-HorizontalAlign="Center" ItemStyle-ForeColor="DarkGreen" ItemStyle-HorizontalAlign="Center"
                                                        SortExpression="Avaliable"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Collect Quanity" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblCollectQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQuantity")%>'></asp:Label>

                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtCollectQty" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQuantity")%>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderStyle-Width="170" HeaderText="  ">
                                                        <EditItemTemplate>
                                                            <asp:Button ID="ButtonUpdate" runat="server" Style="padding: 5px" CommandName="Update" Text="Save" />
                                                            <asp:Button ID="ButtonCancel" runat="server" Style="padding: 5px" CommandName="Cancel" Text="Cancel" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Button ID="ButtonEdit" runat="server" Style="padding: 5px" CommandName="Edit" CssClass="fb9_addplot" Text="Edit" />
                                                            <asp:Button ID="ButtonDelete" runat="server" Style="padding: 5px" CommandName="Delete" CssClass="fb9_addplot" Text="Del" />
                                                        </ItemTemplate>

                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gvheader" />
                                                <AlternatingRowStyle CssClass="gvalternate" />
                                                <RowStyle CssClass="gvnormal" />
                                                <EmptyDataTemplate>
                                                    <label>No Item(s) collected from farmer yet.</label>
                                                </EmptyDataTemplate>
                                                <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <%-- <tr>
                                        <td align="center" style="text-align: center" id="tdGrid"></td>
                                    </tr>--%>

                                    <tr id="trser" runat="server" visible="false">
                                        <td align="center" style="text-align: center">
                                            <table cellpadding="5" cellspacing="5" align="center">
                                                <tr>
                                                    <td style="font-weight: bold">Farmer Name / Code : </td>
                                                    <td>
                                                        <asp:TextBox ID="txtFarmerSearch" runat="server" placeholder="Farmer Name / Code" Style="height: 40px; width: 300px; line-height: 20px; font-size: 20px; vertical-align: middle; padding-left: 5px;"></asp:TextBox></td>
                                                    <td>
                                                        <asp:Button ID="btnFarmerSearch" CssClass="fb8" runat="server" Text="Search" OnClick="btnFarmerSearch_Click" /></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">&nbsp;</td>
                                    </tr>
                                    <tr id="trTotalQtyRow" runat="server" style="display: block">
                                        <td align="center">Total Quantity :
                                            <asp:Label ID="lblRunningTotal" runat="server" Text="0" ForeColor="SeaGreen"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr id="trCollectGridRow" runat="server" style="display: block">
                                        <td class="tdCollectGrid">
                                            <div class="col-sm-12">
                                                <div class="search-container pull-right">
                                                    <input id="txtSearch" type="text" placeholder="Search.." name="search" />
                                                </div>
                                            </div>
                                            <div class="col-sm-12" id="divCollectingdetails">
                                                <asp:GridView ID="gvCollecingDetails" runat="server" AutoGenerateColumns="False"
                                                    DataKeyNames="PlantationId,FarmerID,FarmID" OnSorting="gvCollecingDetails_Sorting"
                                                    AllowSorting="true" CssClass="table table-bordered mudargrid" OnRowDataBound="gvCollecingDetails_RowDataBound">
                                                    <Columns>
                                                        <%--<asp:BoundField DataField="FarmerId" HeaderText="Farmer ID" />
                            <asp:BoundField DataField="FarmID" HeaderText="Farm ID" />--%>
                                                        <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" SortExpression="FarmerCode"></asp:BoundField>
                                                        <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" HeaderStyle-HorizontalAlign="Center"
                                                            SortExpression="FirstName"></asp:BoundField>
                                                        <%--<asp:BoundField DataField="AreaCode" HeaderText="PlotCode" HeaderStyle-HorizontalAlign="Center"
                                                    SortExpression="AreaCode"></asp:BoundField>--%>
                                                        <asp:BoundField DataField="Lot_No" HeaderText="Batch No" HeaderStyle-HorizontalAlign="Center"
                                                            SortExpression="Lot_No"></asp:BoundField>
                                                        <asp:BoundField DataField="TotalProductQuantity" HeaderText="Actual Quanity" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" DataFormatString="{0:0.00}" SortExpression="TotalProductQuantity"></asp:BoundField>
                                                        <asp:BoundField DataField="SoldTotalQty" HeaderText="Sold Quanity" HeaderStyle-HorizontalAlign="Center"
                                                            ItemStyle-HorizontalAlign="Center" SortExpression="SoldTotalQty"></asp:BoundField>
                                                        <asp:BoundField DataField="Avaliable" HeaderText="Avaliable Quanity" ItemStyle-Font-Bold="true"
                                                            HeaderStyle-HorizontalAlign="Center" ItemStyle-ForeColor="DarkGreen" ItemStyle-HorizontalAlign="Center"
                                                            SortExpression="Avaliable"></asp:BoundField>
                                                        <asp:TemplateField HeaderText="Collect Quanity" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <asp:TextBox ID="txtCollectQty" runat="server" Class="searchboxx colquantity" Text='<%# DataBinder.Eval(Container.DataItem, "AvailableQuantity")%>' Width="80px"></asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="Center">
                                                            <ItemTemplate>
                                                                <%--<asp:CheckBox ID="cbCollecting" runat="server" />--%>
                                                                <asp:CheckBox ID="cbCollecting" Checked='<%# DataBinder.Eval(Container.DataItem, "Collect")%>' runat="server" onclick="javascript:validateGridQuantity()" AutoPostBack="false" />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <EmptyDataTemplate>
                                                        <label>No Farmer(s) available for selected product in selected ICS(s).</label>
                                                    </EmptyDataTemplate>
                                                    <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                                </asp:GridView>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr id="trSearchButtons" runat="server" visible="true">
                                        <td align="center">
                                            <table cellpadding="5" cellspacing="5" align="center">
                                                <tr>
                                                    <td>
                                                        <asp:Button ID="btnFarmerCollectSearchapply" runat="server" Text="Apply" CssClass="fb8" OnClick="btnFarmerCollectSearchapply_Click" Style="display: block" />
                                                        <asp:Button ID="btnFarmerCollectSearchapplyDisabled" runat="server" Text="Apply" CssClass="fb8_disable" ForeColor="Gray" Enabled="false" Style="display: none" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="btnFarmercollt" CssClass="fb8" runat="server" Text="Save" OnClick="btnFarmercollt_Click" />
                                                        <asp:Button ID="btnDisablecollectSave" runat="server" Text="Save" CssClass="fb8_disable" ForeColor="Gray" Enabled="false" />
                                                        <asp:Button ID="btnFarmerSearchCancel" Visible="false" runat="server" Text="Cancel" CssClass="fb8" OnClick="btnFarmerSearchCancel_Click" />
                                                    </td>
                                                    <td></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                            <div id="divPerOrderCollecting" runat="server" align="center" visible="false">
                                <table>
                                    <tr>
                                        <td colspan="4">
                                            <asp:GridView ID="gvPreorderCollection" runat="server" AutoGenerateColumns="False"
                                                DataKeyNames="PlantationId,FarmerID,FarmID,CollectionTransactionID" CssClass="table table-bordered mudargrid">
                                                <Columns>
                                                    <asp:BoundField DataField="Blending_BatchID" HeaderText="Batch ID" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="LotNumber" HeaderText="Batch Number" HeaderStyle-HorizontalAlign="Center"
                                                        Visible="false"></asp:BoundField>
                                                    <asp:BoundField DataField="TotalQty" HeaderText="Actual Qty" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="SoldQty" HeaderText="SoldQty" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="AvaliableQty" HeaderText="Avaliable Qty" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-Font-Bold="true" ItemStyle-ForeColor="DarkGreen" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Collect Quanity" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtCollectQty" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "CollectedQuantity")%>'></asp:TextBox>
                                                            <itemstyle horizontalalign="Center"></itemstyle>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Collect" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbCollectingPreorder" Checked='<%# DataBinder.Eval(Container.DataItem, "Collect")%>' runat="server" AutoPostBack="false" onclick="javascript:calculatePreorderQuantity()" OnCheckedChanged="cbCollectingPreorder_CheckedChanged1" />
                                                            <itemstyle horizontalalign="Center"></itemstyle>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gvheader" />
                                                <AlternatingRowStyle CssClass="gvalternate" />
                                                <RowStyle CssClass="gvnormal" />
                                                <EmptyDataTemplate>
                                                    <table cellspacing="2" cellpadding="3" rules="all" class="grid-view">
                                                        <tr class="gvheader">
                                                            <th scope="col">Batch ID
                                                            </th>
                                                            <th scope="col">Actual Qty
                                                            </th>
                                                            <th scope="col">Sold Qty
                                                            </th>
                                                            <th scope="col">Avaliable Qty
                                                            </th>
                                                            <th scope="col">Collect Quanity
                                                            </th>
                                                            <th scope="col"></th>
                                                        </tr>
                                                        <tr class="gvnormal">
                                                            <td colspan="8" style="font-weight: bold; color: red; text-align: center">No Pre-Order collection available for selected product in selected ICS(s).
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </EmptyDataTemplate>
                                                <EmptyDataRowStyle ForeColor="Red" Font-Bold="true" HorizontalAlign="Center" VerticalAlign="Middle" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr id="GenerateBID" runat="server" visible="false">
                                        <td colspan="2" align="right">LotNumber :
                                        <asp:Label ID="lblBatchID" runat="server" ForeColor="#FF6600"></asp:Label>
                                        </td>
                                        <td colspan="4" align="center">
                                            <asp:Button ID="btnGenerateBatchID" CssClass="fb8" runat="server" Text="Click Here"
                                                OnClick="btnGenerateBatchID_Click" />
                                        </td>
                                    </tr>
                                    <tr align="center">
                                        <td colspan="6">
                                            <asp:Button ID="btnPrecollt" CssClass="fb8" runat="server" Text="Save" OnClick="btnPrecollt_Click" />
                                            &nbsp;<asp:Button ID="btnDisablepreorderSave" runat="server" Text="Save" CssClass="fb8_disable" ForeColor="Gray" Enabled="false" />
                                            &nbsp;&nbsp;<asp:Button ID="btnRedirectPreorder" runat="server" Text="Go to Pre-Order" CssClass="fb8" OnClick="btnRedirectPreorder_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </td>
                    </tr>

                    <tr id="trDate" runat="server" visible="false">
                        <td colspan="2" align="center">Name<br />
                            <asp:TextBox ID="txtName" runat="server"
                                Height="30px" Width="200px" Style="margin-bottom: 1px" Font-Size="Medium"></asp:TextBox></td>
                        <td colspan="2" align="center">Collection Date<br />
                            <asp:TextBox ID="txtModifyDate" runat="server"
                                Height="30px" Width="200px" Style="margin-bottom: 1px" Font-Size="Medium" Enabled="false"></asp:TextBox>
                            <asp:CalendarExtender ID="dtpDate" runat="server" Format="dd/MM/yyyy"
                                TargetControlID="txtModifyDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr id="trbtnCollectSubmit" runat="server" visible="false">
                        <td colspan="4" align="center">
                            <asp:Button ID="btncollectSubmit" CssClass="fb8" runat="server" Text="Submit" OnClick="btncollectSubmit_Click" />&nbsp;
                        <asp:Button ID="btnDisablecollectSubmit" runat="server" Text="Submit" CssClass="fb8_disable"
                            Visible="false" ForeColor="Gray" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <h3>Blending Details</h3>
            <div style="background: #E9E1CC">
                <table align="center">
                    <tr>
                        <td colspan="3">
                            <asp:HiddenField ID="hfProductID" runat="server" />
                            <asp:HiddenField ID="hf" runat="server" />
                            <asp:Label ID="lblCollectionID" runat="server" ForeColor="Orange" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="center">Product Name<br />
                            <asp:DropDownList ID="ddlBlendProduct" runat="server" AutoPostBack="true" Style="width: 220px;"
                                OnSelectedIndexChanged="ddlBlendProduct_SelectedIndexChanged">
                            </asp:DropDownList>
                            <asp:HiddenField ID="hdnBlendingQuantity" runat="server" Value="0" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <div id="divCompletedBlendDetails" align="center" runat="server" visible="false">
                                <table align="center">

                                    <tr id="trBlendListQty" runat="server" visible="false">
                                        <td>Blending List :
                                        <asp:Label ID="lblBlendFarmer" runat="server" Text="0" ForeColor="SeaGreen"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <asp:GridView ID="gvBlendCompleted" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mudargrid">
                                                <Columns>
                                                    <asp:BoundField DataField="Blending_BatchID" HeaderText="Lot Number" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="BlendQty" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="Date" HeaderText="Created Date" ItemStyle-HorizontalAlign="Center"
                                                        DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                                </Columns>
                                                <HeaderStyle CssClass="gvheader" />
                                                <AlternatingRowStyle CssClass="gvalternate" />
                                                <RowStyle CssClass="gvnormal" />
                                            </asp:GridView>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                            <div id="divBlendDetails" runat="server" visible="false">
                                <table>
                                    <tr align="center">
                                        <td>Net Quanity<br />
                                            <asp:Label ID="lblNetQty" runat="server" ForeColor="Orange" Visible="false" />
                                        </td>
                                        <td>Blend Quanity<br />
                                            <asp:Label ID="lblAlrBlendQty" runat="server" ForeColor="Orange" Text="0" Visible="false" />
                                        </td>
                                        <td>Remaining Quanity<br />
                                            <asp:Label ID="lblBlendQty" runat="server" ForeColor="Red" Style="display: none" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:GridView ID="gvBlending" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerCode,FarmerID,FarmID"
                                                OnRowCommand="gvBlending_RowCommand1" CssClass="table table-bordered mudargrid">
                                                <Columns>
                                                    <asp:BoundField DataField="FarmerID" HeaderText="Farmer ID" ItemStyle-HorizontalAlign="Center"
                                                        Visible="false"></asp:BoundField>
                                                    <asp:BoundField DataField="FarmID" HeaderText="Farm ID" ItemStyle-HorizontalAlign="Center"
                                                        Visible="false"></asp:BoundField>
                                                    <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="FarmerName" HeaderText="Farmer Name" />
                                                    <asp:BoundField DataField="PlotCode" HeaderText="Plot Code" Visible="false" />
                                                    <asp:BoundField DataField="Lotnumber" HeaderText="Batch No" />
                                                    <asp:BoundField DataField="CollectionQty" HeaderText="Quantity" ItemStyle-HorizontalAlign="Center"
                                                        DataFormatString="{0:0.00}"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Blend Quanity" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtBlendingQty" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "CollectionQty")%>'></asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Blend" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbBlending" runat="server" AutoPostBack="false" onclick="javascript:calculateBlendingQuantity()" OnCheckedChanged="cbBlending_CheckedChanged1" />
                                                        </ItemTemplate>
                                                        <HeaderTemplate>
                                                            <asp:CheckBox ID="cbheader" runat="server" onclick="javascript:checkAll(this);" />
                                                        </HeaderTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"></td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" align="center">
                                            <asp:GridView ID="gvBlendPreorder" runat="server" AutoGenerateColumns="False">
                                                <Columns>
                                                    <asp:BoundField DataField="Blending_BatchID" HeaderText="Batch ID" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField DataField="CollectionQty" HeaderText="Quantity" HeaderStyle-HorizontalAlign="Center"
                                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:TemplateField HeaderText="Blend Quanity" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="txtPreBlendingQty" runat="server" Width="80px" Text='<%# DataBinder.Eval(Container.DataItem, "CollectionQty")%>'></asp:TextBox>
                                                            <itemstyle horizontalalign="Center"></itemstyle>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Blend" HeaderStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="cbBlendingPreorder" runat="server" AutoPostBack="false" onclick="javascript:calculateBlendingQuantity()" OnCheckedChanged="cbBlendingPreorder_CheckedChanged1" />
                                                            <itemstyle horizontalalign="Center"></itemstyle>
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                </Columns>
                                                <HeaderStyle CssClass="gvheader" />
                                                <AlternatingRowStyle CssClass="gvalternate" />
                                                <RowStyle CssClass="gvnormal" />
                                            </asp:GridView>
                                        </td>
                                    </tr>
                                    <tr align="right">
                                        <td>LotNumber :
                                        </td>
                                        <td align="left">
                                            <asp:Label ID="lblLotNum" runat="server" ForeColor="#FF6600"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:Button ID="btnGenLot" CssClass="fb8" runat="server" Text="Generate Lot" OnClick="btnGenLot_Click" OnClientClick="return checkItemsForBlend()" />
                                            <asp:Button ID="btnDisableGenLot" runat="server" Text="Generate Lot" CssClass="fb8_disable"
                                                Visible="false" Enabled="false" ForeColor="Gray" />

                                        </td>
                                    </tr>
                                    <tr id="trBlendDate" runat="server" visible="false">
                                        <td colspan="2" align="center">Name<br />
                                            <asp:TextBox ID="txtBname" runat="server" Font-Size="Medium" Height="30px" Style="margin-bottom: 1px" Width="200px"></asp:TextBox>
                                        </td>
                                        <td align="center">&nbsp;Blend Date<br />
                                            <asp:TextBox ID="txtBlendDate" runat="server"
                                                Height="30px" Width="200px" Style="margin-bottom: 1px" Font-Size="Medium"></asp:TextBox>
                                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                TargetControlID="txtBlendDate">
                                            </asp:CalendarExtender>
                                        </td>
                                    </tr>
                                    <tr id="trblendSubmit" runat="server" visible="false">
                                        <td colspan="3" align="center">
                                            <asp:Button ID="btnBlendSubmit" runat="server" Text="Submit" CssClass="fb8" OnClick="btnBlendSubmit_Click" Visible="false" />&nbsp;
                            <asp:Button ID="btnDisableBlendSubmit" runat="server" Text="Submit" CssClass="fb8_disable"
                                Visible="false" Enabled="false" ForeColor="Gray" />
                                        </td>
                                    </tr>
                                </table>
                            </div>

                        </td>
                    </tr>
                </table>
            </div>

            <h3>Freeze Details</h3>
            <div style="background: #E9E1CC">
                <table align="center">
                    <tr align="center" id="trBlend" runat="server">
                        <td>Blend ID<br />
                            <asp:DropDownList ID="ddlBlendID" runat="server" AutoPostBack="true"
                                OnSelectedIndexChanged="ddlBlendID_SelectedIndexChanged">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                    </tr>
                    <tr align="center">
                        <td>
                            <asp:GridView ID="gvFreeze" DataKeyNames="FreezeID,ProductID" CssClass="table table-bordered mudargrid"
                                runat="server" AutoGenerateColumns="False" OnRowCommand="gvFreeze_RowCommand" EnableModelValidation="True">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" />
                                    <asp:BoundField DataField="Quntatiy" HeaderText="Qty" />
                                    <asp:BoundField DataField="Blending_BatchID" HeaderText="Lot Number" />
                                    <asp:TemplateField HeaderText="Start Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtStartDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                            <asp:CalendarExtender ID="calStartDate" runat="server" TargetControlID="txtStartDate" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="End Date">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtEndDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EndDate")%>' />
                                            <asp:CalendarExtender ID="calEndDate" runat="server" TargetControlID="txtEndDate" Format="dd/MM/yyyy">
                                            </asp:CalendarExtender>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="FBatchID" HeaderText="Batch No"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:Button ID="btnFreez" runat="server" Style="padding: 5px;" CommandName="Freeze" Text="Freeze"
                                                CommandArgument="<%# ((GridViewRow) Container).RowIndex %>"
                                                CssClass="fb9_addplot" />

                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--     <asp:ButtonField ButtonType="Button" Text="Freeze" ControlStyle-CssClass="fb9_addplot" CommandName="Freeze" />--%>
                                </Columns>
                                <HeaderStyle CssClass="gvheader" />
                                <AlternatingRowStyle CssClass="gvalternate" />
                                <RowStyle CssClass="gvnormal" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:DataList ID="dlFreeze" runat="server" BackColor="White" BorderColor="#DEDFDE"
                        BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical"
                        OnItemCommand="dlFreeze_ItemCommand">
                        <ItemTemplate>
                            <table width="100%">
                                <tr align="center">
                                    <td style="width: 130px; color: Red;">Date
                                    </td>
                                    <td style="width: 130px; color: Red;">F Batch No
                                    </td>
                                    <td style="width: 100px; color: Red;">Qty(KG)
                                    </td>
                                    <td style="width: 160px; color: Red;">Product Name
                                    </td>
                                    <td style="width: 180px; color: Red;">Qty(KG)
                                    </td>
                                    <td style="width: 100px; color: Red;">Batch No
                                    </td>
                                    <td style="width: 150px;">
                                        <asp:Button ID="btnAddProduct" runat="server" Text="ADD" CssClass="fb8_go" CommandName="AddProduct" />
                                        <asp:Button ID="btnAddRemove" runat="server" Text="Del" Visible="false" CssClass="fb8_go" CommandName="Del" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" style="width: 130px;">
                                        <%# DataBinder.Eval(Container.DataItem, "EndDate" , "{0:dd MMM yyyy}")%>
                                    </td>
                                    <td style="width: 130px;">
                                        <%# DataBinder.Eval(Container.DataItem, "FBatchID")%>
                                    </td>
                                    <td style="width: 100px;" align="center">
                                        <%# DataBinder.Eval(Container.DataItem, "Quntatiy")%>
                                    </td>
                                    <td colspan="4" class="tdFreezeDL">
                                        <asp:GridView ID="gvFreezeTran" DataKeyNames="FreezeTransactionID,ProductId" runat="server"
                                            AutoGenerateColumns="false" BackColor="White" BorderColor="#DEDFDE" BorderStyle="None"
                                            BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical" ShowHeader="false">
                                            <Columns>
                                                <%--<asp:BoundField DataField="FreezeProductName" />
                                            <asp:BoundField DataField="FreezeQuantity" />--%>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFProductName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FreezeProductName")%>'
                                                            Width="160px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:TextBox ID="txtFQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "FreezeQuantity")%>'
                                                            Width="60px" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="FreezeProductBatchID" />
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
                        </ItemTemplate>
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    </asp:DataList>
                    <br />
                    <br />
                    <br />
                    <div id="divFreezeSubmit" runat="server" align="center" visible="false">
                        <asp:Button ID="btnFreezeSubmit" runat="server" Text="Submit" CssClass="fb8" OnClick="btnFreezeSubmit_Click" />&nbsp;
                <asp:Button ID="btnDisableFrzee" runat="server" Text="Submit" CssClass="fb8_disable"
                    Visible="false" ForeColor="Gray" Enabled="false" />
                    </div>
                </div>
            </div>
            <h3>Testing Details</h3>
            <div style="background: #E9E1CC">
                <div align="center">
                    <table width="100%">
                        <tr>
                            <td align="center">Product Name<br />
                                <asp:DropDownList ID="ddlTestProduct" runat="server" AutoPostBack="true" Style="width: 220px;"
                                    OnSelectedIndexChanged="ddlTestProduct_SelectedIndexChanged" /></td>
                            <td align="center">Blend ID<br />
                                <asp:DropDownList ID="ddlTBlendID" runat="server" AutoPostBack="true" Style="width: 220px;" OnSelectedIndexChanged="ddlTBlendID_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                    </table>
                </div>
                <br />
                <div id="divTestingCompleted" runat="server" visible="false" style="text-align: center">
                    <asp:GridView runat="server" ID="gvTestingCompleted" AutoGenerateColumns="false" CssClass="table table-bordered mudargrid">
                        <Columns>
                            <asp:BoundField HeaderText="Product Name" DataField="ProductName" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Lot Number" DataField="LotNumber" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Parameter" DataField="Parameter" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Analysis Value" DataField="AnalysisValue" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Low" DataField="Low" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="High" DataField="High" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField HeaderText="Tesing Method" DataField="TestingMethod" HeaderStyle-HorizontalAlign="Center" />
                            <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-HorizontalAlign="Center"
                                DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>

                        </Columns>
                        <HeaderStyle CssClass="gvheader" />
                        <AlternatingRowStyle CssClass="gvalternate" />
                        <RowStyle CssClass="gvnormal" />
                    </asp:GridView>
                </div>

                <div id="divTestResults" runat="server" visible="false">
                    <table>
                        <tr>
                            <td colspan="2">
                                <asp:GridView ID="gvTesting" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered mudargrid"
                                    OnRowCommand="gvTesting_RowCommand" OnRowDataBound="gvTesting_RowDataBound">
                                    <Columns>
                                        <asp:BoundField HeaderText="Lot Number" DataField="Blending_BatchId" HeaderStyle-HorizontalAlign="Center" />
                                        <asp:TemplateField HeaderText="Parameter">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPara" runat="server" Height="25px" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem, "Parameter")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Analysis Value">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtAValue" runat="server" Height="25px" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem, "AnalysisValue")%>' />
                                                <asp:Label ID="lblMinAnalysisValue" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MinAnalysisValue") %>'></asp:Label>
                                                <asp:Label ID="lblMaxAnalysisValue" Visible="false" runat="server" Text='<%# DataBinder.Eval(Container.DataItem,"MaxAnalysisValue") %>'></asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Low">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtLow" runat="server" Height="25px" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem, "Low")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="High">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtHigh" runat="server" Height="25px" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem, "High")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Testing Method">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtTmethod" runat="server" Height="25px" Width="120px" Text='<%# DataBinder.Eval(Container.DataItem, "TestingMethod")%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:ButtonField ButtonType="Button" Text="Add" CommandName="Add" ControlStyle-CssClass="fb8_go" />
                                        <asp:ButtonField ButtonType="Button" Text="Del" CommandName="Remove" ControlStyle-CssClass="fb8_go" />
                                    </Columns>
                                    <HeaderStyle CssClass="gvheader" />
                                    <AlternatingRowStyle CssClass="gvalternate" />
                                    <RowStyle CssClass="gvnormal" />
                                </asp:GridView>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">Name<br />
                                <asp:TextBox ID="txtTestname" runat="server"
                                    Height="30px" Width="200px" Style="margin-bottom: 1px" Font-Size="Medium"></asp:TextBox></td>
                            <td align="center">Testing Date<br />
                                <asp:TextBox ID="txtTestDate" runat="server" Font-Size="Medium" Height="30px" Style="margin-bottom: 1px" Width="200px"></asp:TextBox>
                                <asp:CalendarExtender ID="dtpDate0" runat="server" Format="dd/MM/yyyy" TargetControlID="txtTestDate">
                                </asp:CalendarExtender>
                            </td>
                        </tr>
                    </table>
                </div>
                <div id="divTestingButtons" runat="server" align="center" visible="false">
                    <asp:Button ID="btnTestingSubmit" runat="server" Text="Submit" CssClass="fb8" OnClick="btnTestingSubmit_Click" />&nbsp;
                <asp:Button ID="btnTestinsDisable" runat="server" Text="Submit" CssClass="fb8_disable"
                    Visible="false" ForeColor="Gray" Enabled="false" />
                </div>
                <div>&nbsp;</div>
                <div id="divLSMsg" align="center" runat="server" visible="false">
                    <table>
                        <tr style="background-color: #CE5D5A; color: White; font-size: medium; font-weight: bolder;">
                            <td colspan="2" align="center">Lot Sample Sent Details</td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td align="right">Sample Qty</td>
                            <td>
                                <asp:TextBox ID="txtQty" runat="server" Height="30px" Width="400px" CssClass="textbox" /></td>
                        </tr>
                        <tr>
                            <td align="right">Sample Details</td>
                            <td>
                                <asp:TextBox ID="txtLSmsg" runat="server" Height="75px" Width="400px" CssClass="textbox" TextMode="MultiLine" /></td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                        <tr>
                            <td colspan="2" align="center">
                                <asp:Button ID="btnSend" runat="server" Text="Submit" CssClass="fb8" OnClick="btnSend_Click" />
                                <asp:Button ID="btnDisableSend" runat="server" Text="Submit" CssClass="fb8_disable"
                                    Visible="false" ForeColor="Gray" Enabled="false" />
                            </td>
                        </tr>
                    </table>
                </div>

            </div>

            <h3>Packing Details</h3>
            <div style="background: #E9E1CC">
                <div align="center">
                    <table width="100%">
                        <tr>
                            <td align="center">Product Name<br />
                                <asp:DropDownList ID="ddlPackingProduct" runat="server" AutoPostBack="true" Style="width: 220px;"
                                    OnSelectedIndexChanged="ddlPackingProduct_SelectedIndexChanged" /></td>
                            <td align="center">Blend ID<br />
                                <asp:DropDownList ID="ddlPBlendID" runat="server" AutoPostBack="true" Style="width: 220px;" OnSelectedIndexChanged="ddlPBlendID_SelectedIndexChanged">
                                </asp:DropDownList></td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                    </table>
                </div>
                <table align="center">
                    <%--<tr>
                        <td align="center">Drum Ref Numbers<br />
                            <asp:TextBox ID="txtDrumFr" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="180px" Font-Size="Medium" Text="1"></asp:TextBox>
                            &nbsp; &nbsp; &nbsp;<asp:TextBox ID="txtDrumT" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="180px" Font-Size="Medium"></asp:TextBox>
                        </td>
                    </tr>--%>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:GridView ID="gvPackingCompleted" runat="server" AutoGenerateColumns="false" CssClass="table table-bordered mudargrid">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:BoundField DataField="LotNumber" HeaderText="Lot Number" HeaderStyle-HorizontalAlign="Center" />
                                    <asp:TemplateField HeaderText="Packing 25KG" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td><%# DataBinder.Eval(Container.DataItem, "Packing25")%> (<span><%# DataBinder.Eval(Container.DataItem, "25Dfrom")%></span>&nbsp;-&nbsp;<span><%# DataBinder.Eval(Container.DataItem, "25Dto")%></span>)</td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Packing 180KG" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <table>
                                                <tr>
                                                    <td><%# DataBinder.Eval(Container.DataItem, "Packing180")%> (<span><%# DataBinder.Eval(Container.DataItem, "180Dfrom")%></span>&nbsp;-&nbsp;<span><%# DataBinder.Eval(Container.DataItem, "180Dto")%></span>)</td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CreatedDate" HeaderText="Created Date" ItemStyle-HorizontalAlign="Center"
                                        DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                </Columns>
                                <HeaderStyle CssClass="gvheader" />
                                <AlternatingRowStyle CssClass="gvalternate" />
                                <RowStyle CssClass="gvnormal" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:GridView ID="gvPackingDetails" runat="server" AutoGenerateColumns="False" DataKeyNames="OrderId"
                                CssClass="table table-bordered mudargrid">
                                <Columns>
                                    <asp:BoundField DataField="ProductId" HeaderText="Product ID" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product Name" HeaderStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <asp:BoundField DataField="LotNumber" HeaderText="Lot Number" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                    <%--<asp:BoundField DataField="Lotnumber" HeaderText="Batch No" HeaderStyle-HorizontalAlign="Center"
                                        Visible="false"></asp:BoundField>--%>
                                    <asp:BoundField DataField="ActualQuantity" HeaderText="Actual Quantity" ItemStyle-HorizontalAlign="Center"
                                        DataFormatString="{0:0.00}"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Gross Quantity" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtGrossQty" runat="server" Style="width: 80px;" Text='<%# DataBinder.Eval(Container.DataItem, "GrossQuantity" )%>'></asp:TextBox>
                                            <itemstyle horizontalalign="Center"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>

                                    <asp:TemplateField HeaderText="Packing 25KG" HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <table cellpadding="3" cellspacing="3">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtPacking25" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing25KG")%>'
                                                            Style="width: 80px;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDrum25Start" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing25KG_Drum_Start")%>'
                                                            Style="width: 40px;"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDrum25End" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing25KG_Drum_End")%>'
                                                            Style="width: 40px;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>


                                            <itemstyle horizontalalign="Center"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Packing 180KG " HeaderStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <table cellpadding="3" cellspacing="3">
                                                <tr>
                                                    <td colspan="2">
                                                        <asp:TextBox ID="txtPacking180" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing180KG")%>'
                                                            Style="width: 80px;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TextBox ID="txtDrum180Start" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing180KG_Drum_Start")%>'
                                                            Style="width: 40px;"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="txtDrum180End" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Packing180KG_Drum_End")%>'
                                                            Style="width: 40px;"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>

                                            <itemstyle horizontalalign="Center"></itemstyle>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="CollectionID" Visible="false" />
                                </Columns>
                                <HeaderStyle CssClass="gvheader" />
                                <AlternatingRowStyle CssClass="gvalternate" />
                                <RowStyle CssClass="gvnormal" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr id="trPackDate" runat="server" visible="false">
                        <td colspan="3" align="center">Name<br />
                            <asp:TextBox ID="txtPName" runat="server"
                                Height="30px" Width="200px" Style="margin-bottom: 1px" Font-Size="Medium"></asp:TextBox></td>
                        <td colspan="3" align="center">Packing Date<br />
                            <asp:TextBox ID="txtPackingDate" runat="server" Font-Size="Medium" Height="30px" Style="margin-bottom: 1px" Width="200px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" Format="dd/MM/yyyy" TargetControlID="txtPackingDate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="6" align="center">
                            <asp:Button ID="btnPackingDetailsSubmit" CssClass="fb8" runat="server" Text="Submit"
                                OnClick="btnPackingDetailsSubmit_Click" Visible="false" />&nbsp;
                        <asp:Button ID="btnDisablePacking" runat="server" Text="Submit" CssClass="fb8_disable"
                            Visible="false" ForeColor="Gray" Enabled="false" />
                        </td>
                    </tr>
                </table>
            </div>
            <h3>Admin Reports</h3>
            <div style="background: #E9E1CC">
                <table align="center">
                    <tr>
                        <td colspan="4" align="center">
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" /></td>
                    </tr>
                    <tr>
                        <td>Invoice :
                        </td>
                        <td>
                            <asp:HyperLink ID="hlBInvoice" runat="server" Text="Open" Target="_blank" />
                        </td>
                        <td>
                            <asp:FileUpload ID="fuBInvoice" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnBInvoice" runat="server" Text="Upload" CssClass="fb8" OnClick="btnBInvoice_Click" />
                            <asp:Button ID="btnDisableBInvoice" runat="server" Text="Upload" CssClass="fb8_disable"
                                ForeColor="Gray" Visible="false" Enabled="false" />
                            <asp:Button ID="btnInvcancel" runat="server" Text="Cancel" CssClass="fb8" Visible="false" OnClick="btnInvcancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>Scanned GLC:
                        </td>
                        <td>
                            <asp:HyperLink ID="hlBGLCInfo" runat="server" Text="Open" Target="_blank" />
                        </td>
                        <td>
                            <asp:FileUpload ID="fuBGLCInfo" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnBGLCInfo" runat="server" Text="Upload" CssClass="fb8" OnClick="btnBGLCInfo_Click" />
                            <asp:Button ID="btnDisableBGLCInfo" runat="server" Text="Upload" CssClass="fb8_disable"
                                Visible="false" ForeColor="Gray" Enabled="false" />
                            <asp:Button ID="btnGLCcancel" runat="server" Text="Cancel" CssClass="fb8" Visible="false" OnClick="btnGLCcancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>Truck Bill :
                        </td>
                        <td>
                            <asp:HyperLink ID="hlBTruckBill" runat="server" Text="Open" Target="_blank" />
                        </td>
                        <td>
                            <asp:FileUpload ID="fuBTruckBill" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnBTruckBill" runat="server" Text="Upload" CssClass="fb8" OnClick="btnBTruckBill_Click" />
                            <asp:Button ID="btnDisableBTruckBill" runat="server" Text="Upload" CssClass="fb8_disable"
                                Visible="false" ForeColor="Gray" Enabled="false" />
                            <asp:Button ID="btnTBcancel" runat="server" Text="Cancel" CssClass="fb8" Visible="false" OnClick="btnTBcancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>L R :
                        </td>
                        <td>
                            <asp:HyperLink ID="hlBLR" runat="server" Text="Open" Target="_blank" />
                        </td>
                        <td>
                            <asp:FileUpload ID="fuBLR" runat="server" />
                        </td>
                        <td>
                            <asp:Button ID="btnBLR" runat="server" Text="Upload" CssClass="fb8" OnClick="btnBLR_Click" />
                            <asp:Button ID="btnDisableBLR" runat="server" Text="Upload" CssClass="fb8_disable"
                                Visible="false" ForeColor="Gray" Enabled="false" />
                            <asp:Button ID="btnLRcancel" runat="server" Text="Cancel" CssClass="fb8" Visible="false" OnClick="btnLRcancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td>Other : </td>
                        <td>
                            <asp:HyperLink ID="hlBOther" runat="server" Text="Open" Target="_blank" />
                        </td>
                        <td>
                            <asp:FileUpload runat="server" ID="fuBOther" />
                        </td>
                        <td>
                            <asp:Button ID="btnBOther" runat="server" Text="Upload" CssClass="fb8" OnClick="btnBOther_Click" />
                            <asp:Button ID="btnBOtherDisabled" runat="server" Text="Upload" CssClass="fb8_disable"
                                Visible="false" ForeColor="Gray" Enabled="false" />
                            <asp:Button ID="btnOthcancel" runat="server" Text="Cancel" CssClass="fb8" Visible="false" OnClick="btnOthcancel_Click" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="4">
                            <asp:Button ID="btnReportSubmit" runat="server" Text="Submit" visiable="false" CssClass="fb8" OnClick="btnReportSubmit_Click" />&nbsp;&nbsp;                        
                            <asp:Button ID="btnSkip" runat="server" Text="Skip" CssClass="fb8" OnClick="btnSkip_Click" />
                        </td>
                    </tr>
                </table>
            </div>
            <h3>Dispatch Details</h3>
            <div style="background: #E9E1CC">
                <table align="center">
                    <tr id="trInv" runat="server">
                        <td align="center">Invoice Number<br />
                            <asp:TextBox ID="txtInvno" runat="server"
                                Height="30px" Width="200px" Style="margin-bottom: 1px" Font-Size="Medium"></asp:TextBox></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td></td>
                        <td align="center">Dispatch Date<br />
                            <asp:TextBox ID="txtDispatchdate" runat="server" Font-Size="Medium" Height="30px" Style="margin-bottom: 1px" Width="200px"></asp:TextBox>
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" Format="dd/MM/yyyy" TargetControlID="txtDispatchdate">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="12"></td>
                    </tr>
                </table>
                <p style="text-align: center">
                    <asp:Button ID="btnDispatchNext" runat="server" Text="Dispatch" CssClass="fb8" OnClick="btnDispatchNext_Click" />
                    <asp:Button ID="btnDisableDispatchNext" runat="server" Text="Dispatch" CssClass="fb8_disable"
                        Visible="false" ForeColor="Gray" Enabled="false" />

                </p>

            </div>
        </div>
    </div>
    <%-- </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
