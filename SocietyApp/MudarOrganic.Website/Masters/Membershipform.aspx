<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="Membershipform.aspx.cs" Inherits="Masters_Membershipform" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" style="height: auto;">
                <div class="panel panel-default">
                    <div id="header_Text" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                        Membership Details
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
                                                        <asp:TextBox ID="txtAdmissionNumber" class="form-control" runat="server" Enabled="false"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Member Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMemberName" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Father Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtFatherName" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Spouse Name</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtSpouseName" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Aadhaar Number</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtAadhaarNumber" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">PAN Number</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtPanNumber" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Date of Birth</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtDoB" runat="server" CssClass="form-control">
                                                        </asp:TextBox>
                                                        <asp:CalendarExtender
                                                            ID="dtpLevdate" runat="server" Format="dd/MM/yyyy"
                                                            TargetControlID="txtDoB">
                                                        </asp:CalendarExtender>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Mobile Number</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtMobileNumber" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Address</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtAddress" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Area</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtArea" class="form-control" runat="server"></asp:TextBox>
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
                                                        <asp:TextBox ID="txtNomineeName" class="form-control" runat="server"></asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Nominee Relation</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                       <%-- <asp:DropDownList ID="ddlNomineeDetails" CssClass="form-control" runat="server">
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
                                                        </asp:DropDownList>--%>
                                                           <asp:DropDownList ID="ddlNomineeDetails" CssClass="form-control form-control-inline input-large" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">No of Shares</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtNoofShares" class="form-control" runat="server" AutoPostBack="true" OnTextChanged="txtNoofShares_TextChanged">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Share Price</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtShareValue" Enabled="false" class="form-control" runat="server">100</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row">
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Total Share Amount</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtTotalShareAmt" Enabled="false" class="form-control" runat="server">0</asp:TextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="form-group">
                                                <label class="col-md-4 control-label">Entrance Fee</label>
                                                <div class="col-md-8">
                                                    <div class="input-icon right">
                                                        <asp:TextBox ID="txtEntranceFee" class="form-control" runat="server">10</asp:TextBox>
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
                                        <asp:Button ID="btnDetailed" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnDetailed_Click" />
                                    </td>
                                    <td>&nbsp;&nbsp;&nbsp;</td>
                                    <td>
                                        <asp:Button ID="btnClear" runat="server" Text="Close" CssClass="btn btn-default" OnClick="btnClear_Click" /></td>
                                    <td>&nbsp;</td>
                                </tr>
                            </table>
                        </div>
                    </div>

                </div>
            </div>
            <script type="text/javascript">
                function fnShowMessage(msg) {
                    bootbox.alert(msg);
                }
            </script>
           
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>