<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="Sample Farmer.aspx.cs" Inherits="Farmer_Sample_Farmer" Title="Untitled Page" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<%--<%@ Register src="../UserControls/ucFarmerDetails.ascx" tagname="ucFarmerDetails" tagprefix="uc1" %>--%>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Farmers Information</div>
        <div id="SelectDiv" runat="server" align="center">
            <table>
                <tr>
                    <td colspan="8">
                        <asp:Button ID="btnAddFarmer" runat="server" Text="Add New Farmer" CssClass="fb8"
                            PostBackUrl="~/Farmer/NewFarmer.aspx?NewFarmer=0" OnClick="btnAddFarmer_Click" />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnPrint" runat="server" Text="Print PDF" CssClass="fb8" OnClick="btnPrint_Click" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Button ID="btnSearchFarmer" runat="server" Text="Search Farmer" CssClass="fb8"
                            OnClick="btnSearchFarmer_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divSearch" runat="server" align="center">
            <table>
                <tr>
                    <td>
                        &nbsp;Search Farmer By&nbsp;&nbsp;&nbsp; Name /&nbsp; Code&nbsp; /&nbsp; City<br />
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
        <div id="divgvFarmerDetails" runat="server" align="center">
            <table>
                <tr>
                    <td align="left">
                        <asp:Button ID="btnCondensed" runat="server" Text="Condensed" CssClass="fb8" OnClick="btnCondensed_Click" />
                    </td>
                    <td align="center">
                        <table>
                            <tr>
                                <td>
                                    Total Farmers
                                </td>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                                <td>
                                    Total Plots
                                </td>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                                <td>
                                    Total Area
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:Label ID="lblFarmers" runat="server" ForeColor="Orange" />
                                </td>
                                <td colspan="3">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblNoofPlots" runat="server" ForeColor="Orange" />
                                </td>
                                <td colspan="5">
                                    &nbsp;
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblTotalArea" runat="server" ForeColor="Orange" />
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnDetailed" runat="server" Text="Detailed" CssClass="fb8" OnClick="btnDetailed_Click" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:GridView ID="gvFarmer" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerID,FarmerCode"
                            OnRowCommand="gvFarmer_RowCommand" OnRowDataBound="gvFarmer_RowDataBound" EnableSortingAndPagingCallback="True"
                            OnPageIndexChanging="gvFarmer_PageIndexChanging" AllowSorting="True" PageSize="15"
                            OnSorting="gvFarmer_Sorting" CssClass="grid-view">
                            <Columns>
                                <%--<asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />--%>
                                <asp:ButtonField ButtonType="Link" DataTextField="FarmerCode" HeaderText="Farmer Code"
                                    CommandName="FarmerCode" ItemStyle-HorizontalAlign="Left" SortExpression="FarmerCode">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:ButtonField>
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" SortExpression="FirstName" />
                                <asp:BoundField DataField="City_Village" HeaderText="Village" SortExpression="City_Village" />
                                <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Area in(HC)" SortExpression="TotalAreaInHectares"
                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                <%--<asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="Enter Form" CommandName="Farmer" />--%>
                                <asp:BoundField DataField="Organic" HeaderText="Plot Status" ItemStyle-HorizontalAlign="Center"
                                    SortExpression="Organic">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:TemplateField HeaderText="Farmer Status">
                                    <ItemTemplate>
                                        <asp:HiddenField ID="hfFarmerCode" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PRESIDNT") %>' />
                                        <asp:Label ID="lblApproval" runat="server" Text="Approved" Visible='<%# DataBinder.Eval(Container.DataItem, "PRESIDNT") %>' />
                                    </ItemTemplate>
                                    <ItemStyle HorizontalAlign="Center" />
                                </asp:TemplateField>
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
            </table>
        </div>
        <div id="divPDF" runat="server" align="center" visible="false">
            <table align="center">
                <tr>
                    <td colspan="2" align="right">
                        <asp:HyperLink ID="lbtnPdf" runat="server" Target="_blank" Visible="false">Print in PDF</asp:HyperLink>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        Village<br />
                        <asp:DropDownList ID="ddlVillage" runat="server" AutoPostBack="true" Width="160px"
                            Height="30px" OnSelectedIndexChanged="ddlVillage_SelectedIndexChanged">
                        </asp:DropDownList>
                        &nbsp;&nbsp;&nbsp;
                    </td>
                </tr>
                <tr id="trFarmer" runat="server" visible="false">
                    <td align="center">
                        Total Farmers<br />
                        <asp:Label ID="lblTotFarmer" runat="server" ForeColor="Orange" />
                    </td>
                    <td align="center">
                        Total Area<br />
                        <asp:Label ID="lblTotArea" runat="server" ForeColor="Orange" /><br />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:LinkButton ID="lbtnCon" runat="server" Text="condensed" OnClick="lbtnCon_Click"
                            Visible="false" />
                    </td>
                    <td align="right">
                        <asp:LinkButton ID="lbtnDet" runat="server" Text="Detailed" OnClick="lbtnDet_Click"
                            Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:GridView ID="gvFarpdf" runat="server" AutoGenerateColumns="False" OnRowDataBound="gvFarpdf_RowDataBound"
                            CssClass="grid-view">
                            <Columns>
                                <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />
                                <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                                <asp:BoundField DataField="City_Village" HeaderText="Village" />
                                <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Area in(HC)" ItemStyle-HorizontalAlign="Center">
                                </asp:BoundField>
                                <asp:BoundField DataField="Organic" HeaderText="Plot Status" ItemStyle-HorizontalAlign="Center">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                </asp:BoundField>
                                <asp:BoundField DataField="PRESIDNT" HeaderText="Status" />
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Button ID="btnpd" runat="server" Text="Print" CssClass="fb8" OnClick="btnpd_Click" />
                    </td>
                    <td align="center">
                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" OnClick="btnBack_Click" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
