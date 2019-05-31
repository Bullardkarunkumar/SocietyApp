<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="TermDepositApplication.aspx.cs" Inherits="Masters_TermDepositApplication" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" style="height: auto;">
                <div class="panel panel-default">
                    <div id="header_Text" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                        TermDeposit Application Details
                    </div>
                    <div class="panel-body">
                        <div id="divMemberform" runat="server">
                            <div class="form form-horizontal">
                                <div class="form-body">
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Admission Number</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtAdmissionNumber" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Deposit Number</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMemberName" class="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="TextBox2" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Deposit Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtDepositDate" runat="server" Enabled="false" CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:CalendarExtender
                                                            ID="dtpLevdate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDepositDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Product</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="DropDownList3" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Term Deposit</asp:ListItem>
                                                            <asp:ListItem Value="2">Recurring Deposit</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Deposit Type</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                          <asp:DropDownList ID="DropDownList5" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Fixed Deposit</asp:ListItem>
                                                            <asp:ListItem Value="2">Cumulative Deposit</asp:ListItem>
                                                        </asp:DropDownList>
                                                        
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Deposit Amount</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtDepositAmount" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Duration</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="DropDownList4" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">1</asp:ListItem>
                                                            <asp:ListItem Value="2">2</asp:ListItem>
                                                            <asp:ListItem Value="2">3</asp:ListItem>
                                                            <asp:ListItem Value="2">4</asp:ListItem>
                                                            <asp:ListItem Value="2">5</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Maturity Interest</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMaturityInterest" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Maturity Amount</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                       <asp:TextBox ID="txtMaturityAmount" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Maturity Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMaturityDate" runat="server" Enabled="false" CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:CalendarExtender
                                                            ID="CalendarExtender1" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtMaturityDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Frequency Type</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="DropDownList2" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Monthly</asp:ListItem>
                                                            <asp:ListItem Value="2">closing time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Nominee Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="TextBox1" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Nominee Relation</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="DropDownList1" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Father</asp:ListItem>
                                                            <asp:ListItem Value="2">Mother</asp:ListItem>
                                                            <asp:ListItem Value="3">Husband</asp:ListItem>
                                                            <asp:ListItem Value="4">Wife</asp:ListItem>
                                                            <asp:ListItem Value="5">Son</asp:ListItem>
                                                            <asp:ListItem Value="6">Daughter</asp:ListItem>
                                                            <asp:ListItem Value="7">Brother</asp:ListItem>
                                                            <asp:ListItem Value="8">Sister</asp:ListItem>
                                                            <asp:ListItem Value="8">Others</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="divDetailButton" align="center" runat="server">
                            <table>
                                <tr align="center">
                                    <td>
                                        <asp:Button ID="btnDetailed" runat="server" Text="submit" CssClass="btn btn-success" />
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnClear" runat="server" Text="Close" CssClass="btn btn-default" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>