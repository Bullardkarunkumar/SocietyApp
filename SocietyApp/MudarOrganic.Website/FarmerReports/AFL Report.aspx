<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true"
    CodeFile="AFL Report.aspx.cs" Inherits="FarmerReports_AFL_Report" Title="Untitled Page" %>

<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=100,toolbar=0,scrollbars=0,status=0,dir=ltr');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>

    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            AFL Documnet
        </div>
        <%--  <div align="center">
            <asp:Button ID="btnAflInsp" runat="server" Text="AFL - Inspection " CssClass="fb8" OnClick="btnAflInsp_Click" />&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnAflProd" runat="server" Text="AFL - Production" CssClass="fb8" OnClick="btnAflProd_Click" PostBackUrl="~/FarmerReports/AflReportFarmerProd.aspx" />
        </div>--%>
        <div align="center">
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnsubmit" runat="server" Text="Print Page" CssClass="fb8" OnClientClick="javascript:CallPrint('bill');" />
                    </td>
                     <td align="center">
                          Year<br/>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" Height="35px"
                        Width="150px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPF" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnPF_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="bill" runat="server" visible="false">
            <div align="center" style="font-size: 16px; background-color: #CCCC99">
                Mudar India Exports
                <br />
                AFL Report&nbsp; - 1<br />
            </div>
            <div>
                <table align="center" style="font-size: 8px;">
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>CUC Licensee Number
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="txtCUC" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>Inspection Year
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="lblYear" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>Name of Project
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="txtProject" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>Tracenet Registration No
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="txtTRN" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>Name of the ICS
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="txtICS" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>Tracenet Registration No
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="txtTICS" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor="#CCFFCC">Number of Farmer
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="lblFarmers" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>Total Organic Area (Ha)
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="lblTotalArea" runat="server" Width="160px" />
                        </td>
                    </tr>
                    <tr align="center">
                        <td bgcolor='#CCFFCC'>No. of farmers having 4 Ha and more than 4 Ha.
                        </td>
                        <td bgcolor="#FFFFCC">
                            <asp:TextBox ID="txtCount" runat="server" Width="160px" />
                        </td>
                    </tr>
                </table>
            </div>
            <div>
                <asp:GridView ID="gvFarmerList" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerId"
                    Font-Size="8px" OnRowCreated="gvFarmerList_RowCreated" OnRowDataBound="gvFarmerList_RowDataBound"
                    OnDataBound="gvFarmerList_DataBound" GridLines="Both">
                    <Columns>
                        <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                        <asp:BoundField DataField="FatherName" HeaderText="Father Name" />
                        <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Reg. No" />
                        <asp:BoundField DataField="FarmerAPEDACode" HeaderText="Farmer Tracenet No" />
                        <asp:BoundField DataField="City_Village" HeaderText="Village" />
                        <asp:BoundField DataField="Taluk" HeaderText="Taluk" />
                        <asp:BoundField DataField="District" HeaderText="District" />
                        <asp:BoundField DataField="State" HeaderText="State" />
                        <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Organic Area(Ha)" />
                        <asp:BoundField DataField="PlotArea" HeaderText="Organic Plot Area(Ha)" />
                        <asp:BoundField DataField="Organic" HeaderText="Organic Status" />
                        <asp:BoundField DataField="Surveyno" HeaderText="Survey No." />
                        <asp:BoundField DataField="Latitude" HeaderText="Latitude" />
                        <asp:BoundField DataField="Longitude" HeaderText="Longitude" />
                        <asp:BoundField DataField="ChemicalAppDate" HeaderText="Last date of prohibited substances"
                            DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="first" HeaderText="First" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="second" HeaderText="Second" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="third" HeaderText="third" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="firsttname" HeaderText="First" />
                        <asp:BoundField DataField="secondname" HeaderText="Second" />
                        <asp:BoundField DataField="thirdname" HeaderText="Third" />
                        <asp:BoundField DataField="firstRes" HeaderText="First" />
                        <asp:BoundField DataField="secondRes" HeaderText="Second" />
                        <asp:BoundField DataField="thirddRes" HeaderText="Third" />
                    </Columns>
                    <AlternatingRowStyle BackColor="#e6e3e3" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#66CCFF" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#CCCC99" />
                </asp:GridView>
            </div>
        </div>
        <div align="center">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" Visible="false" PostBackUrl="~/FarmerReports/FarmersReports.aspx" />
        </div>
    </div>
</asp:Content>
