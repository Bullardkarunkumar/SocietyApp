<%@ Page Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="Default1.aspx.cs" Inherits="NewFolder1_Default" Title="Untitled Page" %>

<%@ Register assembly="CrystalDecisions.Web, Version=10.5.3700.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" namespace="CrystalDecisions.Web" tagprefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            Farmer Inspection
        </div>
        <div>
         <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" 
    AutoDataBind="True" Height="50px" ReportSourceID="CrystalReportSource1" 
        ReuseParameterValuesOnRefresh="True" Width="350px" />
    <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
        <Report FileName="NewFolder1\rptGetGeneralInspection.rpt">
        </Report>
    </CR:CrystalReportSource>
   </div>
</div>
</asp:Content>

