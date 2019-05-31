<%@ Page Title="Mudarorganic-InspectionPlan" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="Copy of InspectionHistory.aspx.cs" Inherits="Admin_InspectionHisoy" %>

<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Inspection History
        </div>
        <div id="divPlanDetails" runat="server">
            <table align="center">
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr id="trName" runat="server" visible="false">
                    <td align="right">Plan Name
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtPlanName" CssClass="textbox_Style" runat="server"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">Year
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlHolidayYear" runat="server" CssClass="dropdownlist_style"
                            OnSelectedIndexChanged="ddlHolidayYear_SelectedIndexChanged" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">Season
                    </td>
                    <td align="left">&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlSeason" runat="server" CssClass="dropdownlist_style" OnSelectedIndexChanged="ddlSeason_SelectedIndexChanged"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">&nbsp;ICS</td>
                    <td align="left">&nbsp;&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlICS" runat="server" CssClass="dropdownlist_style"
                            AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td colspan="2"></td>
                </tr>
                <tr>
                    <td colspan="2" align="center">
                        <asp:Button ID="btnPlanOk" runat="server" Text="PlanOk" CssClass="fb8" OnClick="btnPlanOk_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="divShowPlanDetails" runat="server" visible="false">
            <table align="center" border="1">
                <tr>
                    <td align="center" colspan="4" style="background-color: #CE5D5A; color: White; font-size: medium; font-weight: bolder;">
                        <asp:Label ID="lblSeas" runat="server" />&nbsp;Season&nbsp;Inspection&nbsp;Plan&nbsp;Details-<asp:Label ID="lblICStype" runat="server" /> 
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#CCCC99">Start Date
                    </td>
                    <td>&nbsp;
                        <asp:Label ID="lblStartDate" runat="server" Text="" ForeColor="OrangeRed" />
                    </td>
                    <td bgcolor="#CCCC99">End Date
                    </td>
                    <td>&nbsp;
                        <asp:Label ID="lblEndDate" runat="server" Text="" ForeColor="OrangeRed" />
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#CCCC99">No of Days
                    </td>
                    <td>&nbsp;
                        <asp:Label ID="lblDays" runat="server" ForeColor="OrangeRed"></asp:Label>
                    </td>
                    <td bgcolor="#CCCC99">No of Plan Days
                    </td>
                    <td>&nbsp;
                        <asp:Label ID="lblPlanDays" runat="server" ForeColor="OrangeRed"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#CCCC99">No of Holidays
                    </td>
                    <td>&nbsp;
                        <asp:Label ID="lblHoliday" runat="server" ForeColor="OrangeRed"></asp:Label>
                    </td>
                    <td bgcolor="#CCCC99">No of Sundays
                    </td>
                    <td>&nbsp;<asp:Label ID="lblSundays" runat="server" ForeColor="OrangeRed"></asp:Label></td>
                </tr>
                <tr>
                    <td bgcolor="#CCCC99">No of Farmers
                    </td>
                    <td>&nbsp;
                        <asp:Label ID="lblFarmers" runat="server" ForeColor="OrangeRed"></asp:Label>
                    </td>
                    <td bgcolor="#CCCC99">No of Inspectors
                    </td>
                    <td>&nbsp;<asp:Label ID="lblEmployees" runat="server" ForeColor="OrangeRed"></asp:Label></td>
                </tr>
                <%-- <tr>
        <td bgcolor="#CCCC99">&nbsp;</td>
            <td>&nbsp;
                </td>
            <td colspan="2">&nbsp;
            <asp:LinkButton ID="lbViewList" runat="server" onclick="lbViewList_Click" Visible="false">ViewList</asp:LinkButton>
            <asp:Label ID="lblvillage" runat="server" Visible="false" />
            <asp:Label ID="lblDaysPerFarmer" runat="server" ></asp:Label>
            </td>    
    </tr>--%>
            </table>
            <table align="center">
                <tr>
                    <td align="center">
                        <asp:HiddenField ID="HiddenHistoryID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblCount" runat="server" Visible="false" />
                        <asp:Label ID="lblvillage" runat="server" Visible="false" /><asp:Label ID="lblDaysPerFarmer"
                            runat="server" Visible="false" />
                        <asp:Label ID="lbltemp" runat="server" Visible="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:GridView ID="gvPlan" runat="server" DataMember="InspectionID" AutoGenerateColumns="False"
                            CssClass="grid-view" AllowSorting="true" DataKeyNames="FarmerID"
                            OnSorting="gvPlan_Sorting">
                            <Columns>
                                <asp:BoundField HeaderText="FarmerID" DataField="FarmerID" Visible="false" />
                                <asp:BoundField HeaderText="Farmer Name" DataField="FarmerName" SortExpression="FarmerName" />
                                <asp:BoundField HeaderText="Farmer Code" DataField="FarmerCode" SortExpression="FarmerCode" />
                                <asp:BoundField HeaderText="Village" DataField="FarmerArea" SortExpression="FarmerArea" />
                              <%--  <asp:BoundField HeaderText="InspectorID" DataField="InspectorID" Visible="false" />
                                <asp:BoundField HeaderText="Inspector Name" DataField="InspectorName" SortExpression="InspectorName" />
                                <asp:BoundField HeaderText="Plan Date" DataField="PlanDate" DataFormatString="{0:dd MMM yyyy}"
                                    SortExpression="PlanDate" />
                                <asp:BoundField HeaderText="Visited Date" DataField="VisitedDate" DataFormatString="{0:dd MMM yyyy}"
                                    SortExpression="VisitedDate" />
                                <asp:BoundField HeaderText="InspectionID" DataField="InspectionID" Visible="false" />--%>
                            </Columns>
                            <HeaderStyle CssClass="gvheader" />
                            <AlternatingRowStyle CssClass="gvalternate" />
                            <RowStyle CssClass="gvnormal" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btnSubmit" CssClass="fb8" runat="server" Text="Submit" OnClick="Submit_Click" />
                        <asp:Button ID="btnDisable" runat="server" Text="Submit" CssClass="fb8_disable"
                            Visible="false" ForeColor="Gray" />
                        <asp:Button ID="btnBack" CssClass="fb8" runat="server" Text="Back"
                            Visible="false" OnClick="btnBack_Click" />

                    </td>
                </tr>
            </table>
        </div>
    </div>
</asp:Content>
