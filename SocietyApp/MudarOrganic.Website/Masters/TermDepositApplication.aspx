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
                                                        <asp:TextBox ID="txtAdmissionNumber" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtAdmissionNumber_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Deposit Number</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtDepositNumber" class="form-control" Enabled="false" runat="server"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtName" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtName_TextChanged"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Bond Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtBondName" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Deposit Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtDepositDate" runat="server" Enabled="true" CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:CalendarExtender
                                                            ID="dtpDepositDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDepositDate">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">By Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtByDate" runat="server" Enabled="true" CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:CalendarExtender
                                                            ID="dtpByDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtByDate">
                                                        </asp:CalendarExtender>
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
                                                <label class="col-md-4 control-label">Deposit Type</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="ddlDepositType" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Fixed Deposit</asp:ListItem>
                                                            <asp:ListItem Value="2">Cumulative Deposit</asp:ListItem>
                                                            <asp:ListItem Value="2">Recurring Deposit</asp:ListItem>
                                                        </asp:DropDownList>

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Tenure</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <%--<asp:DropDownList ID="ddlTenure" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">31Days - 180 Days</asp:ListItem>
                                                            <asp:ListItem Value="2">1Y - 2Y</asp:ListItem>
                                                            <asp:ListItem Value="2">2Y - 5Y</asp:ListItem>
                                                        </asp:DropDownList>--%>
                                                        <asp:DropDownList ID="ddlTenure" CssClass="form-control" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlTenure_SelectedIndexChanged">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Rate Of Intrest</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtRateOfIntrest" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Frequency Type</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="ddlFrequencyType" Enabled="false" CssClass="form-control" runat="server">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="1">Monthly</asp:ListItem>
                                                            <asp:ListItem Value="2">closing time</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Maturity Interest</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMaturityInterest" class="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Maturity Amount</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMaturityAmount" class="form-control" Enabled="false" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Maturity Date</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMaturityDate" runat="server" Enabled="false" CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:CalendarExtender
                                                            ID="dtpMaturityDate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtMaturityDate">
                                                        </asp:CalendarExtender>
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
                                                        <asp:TextBox ID="txtNomineeNameTDA" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Nominee Relation</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:DropDownList ID="ddlNomineeRelationTDA" CssClass="form-control" runat="server">
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
