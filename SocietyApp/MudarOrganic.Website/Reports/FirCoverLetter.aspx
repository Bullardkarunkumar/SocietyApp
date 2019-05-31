<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FirCoverLetter.aspx.cs" Inherits="FirCoverLetter" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
    <div id="content_area_Home" style="height:auto;">
            <div id="header_Text">
                Sample FIR Cover Letter
           </div>
        <div>
        <table width="100%" align="center" style="font-family:Verdana">
        <tr>
        <td width="50%"></td><td width="50%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Date&nbsp; : <asp:Label ID="lblTodayDate" runat="server"></asp:Label></td>
        </tr>
        <tr><td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Address&nbsp;:&nbsp;&nbsp;
            <asp:TextBox ID="txtAddress" runat="server" TextMode="MultiLine" Width="340px"></asp:TextBox>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td colspan="2" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Dear Sir&nbsp; :&nbsp;&nbsp;         
            <asp:TextBox 
                ID="txtDearsir" runat="server" Height="30px" Style="margin-bottom: 1px"
                                Width="340px" Font-Size="Medium"/></td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td  width="70%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Sub : 
            Submission of Export Documents
            </td>
            <td width="30%">&nbsp;</td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td width="70%" >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Ref&nbsp; : 
            Our Invoice No :&nbsp;<asp:Label ID="lblInviNo" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp; 
            Dt :<asp:Label ID="lblInvoiceDate" runat="server"></asp:Label>
            </td>
            <td width="30%">FIR no:&nbsp;<asp:TextBox ID="txtFIRno" runat="server" 
                    Height="25px" Width="178px" Style="margin-bottom: 1px" Font-Size="Medium" ></asp:TextBox></td>
        </tr>
        <tr><td colspan="2"></td></tr>
        <tr>
        <td colspan="2" align="center">Please find the below mentioned documents against the 
            above mentioned FIR</td>
        </tr>
        <tr>
        <td colspan="2" align="center">&nbsp;
        </td>
        </tr>
        <tr>
        <td colspan="2" align="center">
            <table border="1">
                    <tr>
                        <td bgcolor="#FFCC66">
                            <b>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Description&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>
                        </td>
                        <td bgcolor="#FFCC66" colspan="3">
                            <b>Document Reference</b>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFCC66">
                            Buyer
                        </td>
                        <td colspan="2">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="lblCompanyAddress" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td>
                            &nbsp;&nbsp;<asp:Label ID="lblDestCountry" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFCC66">
                            Inv No
                        </td>
                        <td>
                            <asp:Label ID="lblInvoice" runat="server" />
                        </td>
                        <td>
                            Dt
                        </td>
                        <td>
                            <asp:Label ID="lblInvoiceDt" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFCC66">
                            Packing List No
                        </td>
                        <td>
                            <asp:Label ID="lblInv" runat="server" />
                        </td>
                        <td>
                            Dt
                        </td>
                        <td>
                            <asp:Label ID="lblPackDt" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFCC66">
                            AWB / BoL
                        </td>
                        <td colspan="3">
                            <asp:Label ID="lblAWB" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFCC66">
                            SDF No
                        </td>
                        <td>
                            <asp:TextBox ID="txtSDFNo" runat="server" Height="25px" Width="191px" Style="margin-bottom: 1px"
                                Font-Size="Medium"></asp:TextBox>
                        </td>
                        <td>
                            Dt
                        </td>
                        <td>
                            <asp:TextBox ID="txtSDFNoDt" runat="server" Text="" Height="25px" 
                                Width="191px" />
                            <asp:CalendarExtender ID="ceReportDate" runat="server" TargetControlID="txtSDFNoDt">
                            </asp:CalendarExtender>
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#FFCC66">
                            EP Copy
                        </td>
                        <td>
                            <asp:TextBox ID="txtEPcopy" runat="server" Height="25px" Width="191px" Style="margin-bottom: 1px"
                                Font-Size="Medium"></asp:TextBox>
                        </td>
                        <td>
                            Dt
                        </td>
                        <td>
                            <asp:TextBox ID="txtEPcopyDt" runat="server" Text="" Height="25px" 
                                Width="191px" />
                            <asp:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtEPcopyDt">
                            </asp:CalendarExtender>
                        </td>
           
        </tr>
        </table>
        <tr>
        <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" align="center">Please find the details of FIR against the above 
            mentioned shipment</td>
        </tr>
        <tr>
        <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" align="center">
            <table border="1">
                <tr>
                    <td bgcolor="#FFCC66">
                        <b>Invoice 
                        <br />
                        Amount in USD</b></td>
                    <td bgcolor="#FFCC66">
                        <b>&nbsp;&nbsp;&nbsp;FIR NO&nbsp;&nbsp;&nbsp;</b></td>
                         <td bgcolor="#FFCC66">
                        <b>FIR Date</b></td>
                    <td bgcolor="#FFCC66">
                        <b>FIR Amount in USD</b></td>
                         <td bgcolor="#FFCC66">
                        <b>Amount 
                             Against 
                             <br />
                             the Invoice in USD</b></td>
                </tr>
                 <tr>
                    <td style="height: 34px">
                        <asp:Label ID="lblInvAmout" runat="server" /></td>
                    <td style="height: 34px" >
                        <asp:Label ID="lblFIRnum" runat="server" /></td>
                         <td style="height: 34px" >
                        <b>&nbsp;&nbsp;&nbsp;<asp:TextBox ID="txtFIRDate" runat="server" Height="25px" />
                            <asp:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFIRDate">
                            </asp:CalendarExtender>&nbsp;&nbsp;&nbsp;</b></td>
                    <td style="height: 34px" >
                        <asp:TextBox ID="txtFIRAmt" runat="server" Height="25px" /></td>
                         <td style="height: 34px" >
                        <asp:TextBox ID="txtAmtAgnistINV" runat="server" Height="25px" /></td>
                    
                </tr>
                 <tr>
                    <td colspan="2"><b>&nbsp;&nbsp;&nbsp;Enclosed Certificate of FIR Ref&nbsp;&nbsp;&nbsp;</b></td>
                    <td>                        <asp:TextBox ID="txtECFIR" runat="server" Height="25px" 
                            Width="136px" /></td>
                    <td>Dt</td>
                    <td><asp:TextBox ID="txtECFIRDt" runat="server" Height="25px" />
                            <asp:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtECFIRDt">
                            </asp:CalendarExtender></td>
                </tr>

            </table>
            </td>
        </tr>
        <tr>
        <td colspan="2" align="center">&nbsp;</td>
        </tr>
        <tr>
        <td colspan="2" align="center">Please acknowledge the receipt of all the documents in order</td>
        </tr>
        <tr><td colspan="2"></td></tr>
             <tr><td colspan="2" align="center"><asp:Button ID="btnFirCoverSubmit"  
                CssClass="fb8" runat="server" Text="Submit" 
                        onclick="btnFirCoverSubmit_Click" />&nbsp;</td></tr>
        </table>
        </div>
</asp:Content>

