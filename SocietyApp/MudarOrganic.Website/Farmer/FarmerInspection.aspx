<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="FarmerInspection.aspx.cs" Inherits="Farmer_FarmerInspection" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Farmer Inspection
        </div>
        <div>
            &nbsp;<asp:Label ID="lblTodayDate" runat="server" Visible="false" /></div>
        <div align="center">
            <asp:Button ID="btnSInspection" runat="server" Text="Search By Inspector" CssClass="btnFarmer"
                OnClick="btnSInspection_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnSFarmer" runat="server" Text="Search By Farmer" CssClass="btnFarmer"
                OnClick="btnSFarmer_Click" Visible="false" />
        </div>
        <div align="center">
            <table>
                <tr>
                    <td align="center">
                        <div id="divInspection" runat="server" visible="false">
                            <table width="100%">
                                <tr>
                                    <td>
                                        &nbsp;Inspector Name<br />
                                        <asp:DropDownList ID="ddlEmployeeList" runat="server" Width="160px" />
                                    </td>
                                    <td>
                                        &nbsp;From<br />
                                        <asp:TextBox ID="txtFromDate" runat="server" Width="160px" />
                                        <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtFromDate">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        &nbsp;To<br />
                                        <asp:TextBox ID="txtToDate" runat="server" Width="160px" />
                                        <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtToDate">
                                        </asp:CalendarExtender>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btnGo" runat="server" Text="GO" CssClass="fb8_go" OnClick="btnGo_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <div id="divFarmer" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td>
                                        &nbsp; Enter the Farmer &nbsp;&nbsp; Name /&nbsp; Code&nbsp; /&nbsp; City<br />
                                        <asp:TextBox ID="txtSFarmerName" autocomplete="off" runat="server" CssClass="textbox_Style"></asp:TextBox>
                                        <asp:AutoCompleteExtender ID="txtAutoFarmerName" runat="server" TargetControlID="txtSFarmerName"
                                            CompletionInterval="1000" EnableCaching="true" CompletionSetCount="10" CompletionListCssClass="autocomplete_completionListElement"
                                            CompletionListItemCssClass="autocomplete_listItem" CompletionListHighlightedItemCssClass="autocomplete_highlightedListItem"
                                            ShowOnlyCurrentWordInCompletionListItem="true" BehaviorID="AutoCompleteEx" ServicePath="~/AutoComplete.asmx"
                                            ServiceMethod="GetCompletionList" MinimumPrefixLength="1">
                                            <Animations>
                    <OnShow>
                        <Sequence>
                            <%-- Make the completion list transparent and then show it --%>
                            <OpacityAction Opacity="0" />
                            <HideAction Visible="true" />
                            
                            <%--Cache the original size of the completion list the first time
                                the animation is played and then set it to zero --%>
                            <ScriptAction Script="
                                // Cache the size and setup the initial size
                                var behavior = $find('AutoCompleteEx');
                                if (!behavior._height) {
                                    var target = behavior.get_completionList();
                                    behavior._height = target.offsetHeight - 2;
                                    target.style.height = '0px';
                                }" />
                            
                            <%-- Expand from 0px to the appropriate size while fading in --%>
                            <Parallel Duration=".4">
                                <FadeIn />
                                <Length PropertyKey="height" StartValue="0" EndValueScript="$find('AutoCompleteEx')._height" />
                            </Parallel>
                        </Sequence>
                    </OnShow>
                    <OnHide>
                        <%-- Collapse down to 0px and fade out --%>
                        <Parallel Duration=".4">
                            <FadeOut />
                            <Length PropertyKey="height" StartValueScript="$find('AutoCompleteEx')._height" EndValue="0" />
                        </Parallel>
                    </OnHide>
                                            </Animations>
                                        </asp:AutoCompleteExtender>
                                    </td>
                                    <td>
                                        <br />
                                        <asp:Button ID="btnSearch" runat="server" CssClass="fb8_go" Text="GO" OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>
                        <div>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </div>
                        <div id="divResult" runat="server" visible="false">
                            <table>
                                <tr>
                                    <td align="left">
                                        <asp:LinkButton ID="lbtnPrev" runat="server" Text="Previous Date" OnClick="lbtnPrev_Click" />
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td align="right">
                                        <asp:LinkButton ID="lbtnNext" runat="server" Text="Next Date" OnClick="lbtnNext_Click" />
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td colspan="2">
                                        <asp:Label ID="lblHoliday" runat="server" Visible="false" ForeColor="Green"></asp:Label>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4" align="center">
                                        <asp:GridView ID="gvFarmerList" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerId,FarmerCode,PlanDate,InspectionID,InspectorName"
                                            OnRowCommand="gvFarmerList_RowCommand" CssClass="grid-view" AllowSorting="True"
                                            OnSorting="gvFarmerList_Sorting" OnRowDataBound="gvFarmerList_RowDataBound" >
                                            <Columns>
                                                <asp:BoundField DataField="PlanDate" HeaderText="Scheduled Date " DataFormatString="{0: dd MMM yyyy}"
                                                    SortExpression="PlanDate" />
                                                <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" SortExpression="FarmerCode" />
                                                <%--<asp:ButtonField DataTextField="FarmerCode" ButtonType="Link" HeaderText="Farmer Code"
                                                   SortExpression="FarmerCode" />--%>
                                                <asp:BoundField DataField="FarmerName" HeaderText="Farmer Name" SortExpression="FarmerName" />
                                                <asp:BoundField DataField="FarmerVillage" HeaderText="Village" SortExpression="FarmerVillage" />
                                                <asp:BoundField DataField="Total_Area" HeaderText="Area in (HC)" SortExpression="Total_Area" />
                                                <asp:BoundField DataField="VisitedDate" HeaderText="Inspection Date" DataFormatString="{0: dd MMM yyyy}"
                                                    SortExpression="VisitedDate" />
                                                <asp:BoundField DataField="InspectorName" HeaderText="Inspector Name" SortExpression="InspectorName" />
                                                <asp:BoundField DataField="InspectionID" HeaderText="InspectionID" Visible="false" />
                                                <asp:ButtonField ButtonType="Link" Text="Report" HeaderText="" CommandName="Report" />
                                                <asp:BoundField DataField="Result" HeaderText="Report Status"/>
                                                <asp:ButtonField ButtonType="Link" Text="Diary" HeaderText="" CommandName="Diary" />
                                            </Columns>
                                            <HeaderStyle CssClass="gvheader" />
                                            <AlternatingRowStyle CssClass="gvalternate" />
                                            <RowStyle CssClass="gvnormal" />
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr id="trgvFarmerList" runat="server">
                                    <td colspan="4">
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divBack" runat="server" align="center">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" Visible="false"
                OnClick="btnBack_Click" /></div>
    </div>
</asp:Content>
