<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="Sharecertificate.aspx.cs" Inherits="Masters_Sharecertificate" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" style="height: auto;">
                <div class="panel panel-default">
                    <div id="header_Text" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                        Share Certificate
                    </div>
                </div>
                <div class="panel-body">
                    <div>
                  
                    
                        <CR:CrystalReportViewer ID="CrystalReportViewer1" runat="server" AutoDataBind="True" 
                             Height="400px" Width="600px" BestFitPage="False" ToolPanelView="None" DisplayPage="False" DisplayToolbar="False" EnableDatabaseLogonPrompt="False" EnableParameterPrompt="False" GroupTreeImagesFolderUrl="" OnInit="CrystalReportViewer1_Init" ReportSourceID="CrystalReportSource1" ToolbarImagesFolderUrl="" ToolPanelWidth="200px"/>
                  
                    
                        <CR:CrystalReportSource ID="CrystalReportSource1" runat="server">
                            <Report FileName="CrystalReports\ShareCertificate.rpt">
                            </Report>
                        </CR:CrystalReportSource>
                  
                    
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
