<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="NewFarmer.aspx.cs" Inherits="Farmer_Farmer" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<%@ Register src="../UserControls/CucFarmerDetails.ascx" tagname="CucFarmerDetails" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">
    <div id="content_area_Home" style="height:auto">
        <div id="header_Text">
            Farmer Information
        </div>
        <div><asp:Label ID="lblinfo" runat="server" Visible="False" /></div>
        <div>
        <uc3:CucFarmerDetails ID="CucFarmerDetails" runat="server" />
        </div>
  </div>
</asp:Content>


