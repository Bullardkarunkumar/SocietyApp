<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FarmersReports.aspx.cs" Inherits="FarmerReports_FarmersReports" %>

<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
<div id="content_area_Home" style="height: 400px">
        <div id="header_Text">
            Farmer Reports
        </div>
    <div>&nbsp;</div>
   <div align="center">
       <table>
           <tr>
               <td><asp:Button ID="btnAflInsp" runat="server" Text="AFL - Inspection " CssClass="fb8"  PostBackUrl="~/FarmerReports/AFL Report.aspx" /></td>
               <td>&nbsp;&nbsp;&nbsp;</td>
               <td><asp:Button ID="btnAflProd" runat="server" Text="AFL - Production" CssClass="fb8"  PostBackUrl="~/FarmerReports/AflReportFarmerProd.aspx" OnClick="btnAflProd_Click" /></td>
               <td>&nbsp;&nbsp;&nbsp;</td>
               <td><asp:Button ID="btnAFLEsti" runat="server" Text="AFL - EStimation" CssClass="fb8"  PostBackUrl="~/FarmerReports/AFLEstimation.aspx" OnClick="btnAFLEsti_Click" /></td>
               <td>&nbsp;&nbsp;&nbsp;</td>
               <td><asp:Button ID="btnUnitData" runat="server" Text="Unit Info" CssClass="fb8"  PostBackUrl="~/FarmerReports/UnitInfo.aspx"/></td>
               <td>&nbsp;&nbsp;&nbsp;</td>
               <td><asp:Button ID="btnTotalProduction" runat="server" Text="Total Production" CssClass="fb8"  PostBackUrl="~/FarmerReports/AFLFarmerTotalProd.aspx"/></td>
           </tr>
          
       </table>
        </div>
     <div>&nbsp;</div>
</div>
</asp:Content>

