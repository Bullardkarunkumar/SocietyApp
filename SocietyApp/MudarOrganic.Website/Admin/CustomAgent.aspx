<%@ Page Title="Mudarorganic-CustomAgent" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="CustomAgent.aspx.cs" Inherits="Admin_CustomBroker" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Custom Agent
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div id="btnAgent" align="center" runat="server" class="col-sm-12">
                                <div class="col-sm-12" style="margin-bottom: 10px">
                                    <asp:Button ID="btnAddAgent" runat="server" OnClick="btnAddAgent_Click" CssClass="btn btn-success"
                                        Text="Add Custom Agent" />
                                </div>
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvAgent" runat="server" AutoGenerateColumns="False" DataKeyNames="CustomAgentId"
                                        OnRowCommand="gvAgent_RowCommand" OnRowDataBound="gvAgent_RowDataBound"
                                        CssClass="table table-bordered mudargrid"
                                        AllowSorting="True" OnSorting="gvAgent_Sorting">
                                        <Columns>
                                            <asp:BoundField DataField="AgentCode" HeaderText="Agent Code" SortExpression="AgentCode"></asp:BoundField>
                                            <asp:BoundField DataField="AgentName" HeaderText="Agent Name" SortExpression="AgentName"></asp:BoundField>
                                            <asp:BoundField DataField="Place" HeaderText="Place" SortExpression="Place"></asp:BoundField>
                                            <asp:BoundField DataField="ModeofTransport" HeaderText="Transport Mode" SortExpression="ModeofTransport"></asp:BoundField>
                                            <asp:BoundField DataField="AddressforDelivery" HeaderText="Delivery Address"></asp:BoundField>
                                            <asp:ButtonField ButtonType="Link" Text="View" HeaderText="Enter Form" CommandName="Agent"></asp:ButtonField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                            <div id="AgentForm" runat="server" class="col-sm-12" visible="false">
                                <div class="portlet box green">
                                    <div class="portlet-title">
                                        <div class="caption">
                                            Create/Update Custom Agent
                                        </div>
                                    </div>
                                    <div class="portlet-body form">
                                        <div class="form-horizontal">
                                            <div class="form-body" style="margin-left: 80px">
                                                <div class="form-group" style="display: none">
                                                    <asp:Label ID="lblAgentID" runat="server" Text="" Visible="false" />
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Custom Agent</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAgentname" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Agent Code</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAgentcode" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Agent Name</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtcontactperson" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Place</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPlace" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Mode of Transport</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ID="ddlTransportMode" runat="server" CssClass="form-control form-control-inline input-large"
                                                            ValidationGroup="TransportMode">
                                                            <asp:ListItem Selected="True">--select--</asp:ListItem>
                                                            <asp:ListItem Value="0">Air</asp:ListItem>
                                                            <asp:ListItem Value="1">Sea</asp:ListItem>
                                                            <asp:ListItem Value="2">Rail</asp:ListItem>
                                                            <asp:ListItem Value="3">Road</asp:ListItem>
                                                        </asp:DropDownList>
                                                        <asp:RequiredFieldValidator ID="rfvTransportMode" runat="server" ControlToValidate="ddlTransportMode"
                                                            ValidationGroup="TransportMode" ErrorMessage="select the Transport Mode" Display="Dynamic"
                                                            Text="*">                            
                                                        </asp:RequiredFieldValidator>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Address of Delivery</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtDeliveyAddress" runat="server"
                                                            CssClass="form-control form-control-inline input-large" Height="64px" TextMode="MultiLine"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Address Line 1</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAddress1" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Address Line 2</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAddress2" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Address Line 3</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtAddress3" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">City</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtCity" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Province/State</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtState" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">ZIP Code</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtZipCode" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Country</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtCountry" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Phone No</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtPhoneNo" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Mobile No</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtMphone" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">E-Mail</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtEmail" runat="server"
                                                            CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:ValidationSummary runat="server" ID="TransportModeNext" ValidationGroup="TransportMode"
                                                        ShowSummary="true" DisplayMode="SingleParagraph" HeaderText="Error Messsage* : " />
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row form-actions noborder text-center">
                                            <asp:Button ID="btnAgentSubmit" runat="server" ValidationGroup="ComapnyName"
                                                OnClick="btnAgentSubmit_Click" Text="Submit" CssClass="btn btn-success" />
                                            &nbsp;
                                    <asp:Button ID="btnDeliveryClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnDeliveryClear_Click" />
                                        </div>
                                    </div>


                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

