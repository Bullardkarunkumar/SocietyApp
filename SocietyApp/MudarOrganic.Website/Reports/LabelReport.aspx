<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="LabelReport.aspx.cs" Inherits="LabelSamplePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height:auto;">
            <div id="header_Text">
                Sample Cover Letter
           </div>
        <div>
        <table width="100%" border="1" align="center" style="font-family:Verdana">
        <tr>
        <td  colspan="4" align="center" bgcolor="#ffcc66"> X</td></tr>
       <tr>
        <td colspan="4" style='font-size: 9px' align="center"> ( Product Produced &amp; Processed in accordance with requirements of 
            India’s National Program for Organic Production (NPOP) which is considered 
            equivalent to Council Regulation (EC) 834/2007 &amp; also as per USDA-NOP)</td></tr>
        <tr>
        <td  colspan="4" style='font-size: 15px' align="center"> Licensee Producer</td></tr>
     <tr>
        <td colspan="4" align="center"> <b>Mudar India Exports</b></td></tr>
        
     <tr>
        <td colspan="4" style='font-size: 12px' align="center"> 6-1-744, Kovur Nagar, ANANTAPUR - 515004 Andhra Pradesh, India</td></tr>
        
     <tr>
        <td colspan="4" style='font-size: 12px' align="center"> <b>Certified Organic by CU-025367</b></td></tr>
        
     <tr>
        <td colspan="2" width="50%" align="center"> Buyer</td>
        <td colspan="2">&nbsp;&nbsp;&nbsp; <b><asp:Label ID="lblCompanyAddress" runat="server"></asp:Label></b></td>
        </tr>
        <tr>
         <td  width="25%" align="center"> Country of Origin</td>
         <td  width="25%" align="center"> &nbsp;&nbsp;India</td>
          <td  width="25%" align="center"> Country of Destination</td>
         <td  width="25%" align="center"><asp:Label ID="lblDCountry" runat="server" /></td>
        </tr>
        <tr>
        <td  width="25%" align="center"> Gross Weight (KG)</td>
         <td  width="25%" align="center"> <asp:Label ID="lblGrossWt" runat="server" /></td>
          <td  width="25%" align="center" colspan="2" style="width: 50%"> <b>Do Not Fumigate</b></td>
        </tr>
         <tr>
        <td  width="25%" align="center"> Tare Weight (KG)</td>
         <td  width="25%" align="center"> <asp:Label ID="lblTareWt" runat="server" /></td>
          <td  width="25%" align="center"> Lot Number</td>
         <td  width="25%" align="center"><asp:Label ID="lblLotNo" runat="server" /></td>
        </tr>
         <tr>
        <td  width="25%" align="center"> Net Weight(KG)</td>
         <td  width="25%" align="center"> <asp:Label ID="lblNetWt" runat="server" /></td>
          <td  width="25%" align="center"> Drum Number</td>
         <td  width="25%" align="center"><asp:Label ID="lblDrumNo" runat="server" /></td>
        </tr>
        </table>
        </div>
</asp:Content>

