<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="MembershipDetails.aspx.cs" Inherits="Masters_MembershipDetails" %>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" class="panel panel-success">
                <div id="header_Text" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                    Members List
                </div>
                <div class="panel-body">
                <%--    <div class="row" style="text-align: center">
                       <asp:LinkButton ID="lbtnUnapproved" runat="server" Text="UN Approved" OnClick="lbtnUnapproved_Click"
                            Visible="false" />
                        <asp:LinkButton ID="lbtnmember" runat="server" Text="Approved" OnClick="lbtnmember_Click"
                            />
                    </div>
                    <div class="row" style="text-align: center">
                        <asp:Button ID="btnUnapproved" runat="server" Text="Unapproved List" CssClass="btn btn-default"
                            OnClick="btnUnapproved_Click" />
                        <asp:Button ID="btnmember" runat="server" Text="Approved List" CssClass="btn btn-success"
                            OnClick="btnmember_Click" />
                    </div>--%>

                 
                    <div id="Approved" runat="server" class="row" style="margin-top: 15px">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvMembers" DataKeyNames="Buyerid" AutoGenerateColumns="False"
                                runat="server" OnRowCommand="gvMembers_RowCommand" CssClass="table table-bordered mudargrid"
                                AllowSorting="True" OnSorting="gvMembers_Sorting" OnRowEditing="gvMembers_RowEditing">
                                <Columns>
                                    <asp:BoundField DataField="BuyerCompanyName" HeaderText="Company Name" SortExpression="BuyerCompanyName" />
                                    <%--  <asp:ButtonField ButtonType="Link" CommandName="Select" DataTextField="BuyerCompanyName"
                                    HeaderText="Company Name" ItemStyle-HorizontalAlign="left" SortExpression="BuyerCompanyName">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                </asp:ButtonField>--%>
                                    <asp:BoundField DataField="email" HeaderText="EMail" Visible="false" />
                                    <asp:BoundField DataField="MobileforTextingpurpose" HeaderText="Mobile" Visible="false" />
                                    <asp:BoundField DataField="CState" HeaderText="State" Visible="false" />
                                    <asp:ButtonField ButtonType="Link" CommandName="View" Text="View" HeaderText="View" />
                                    <asp:BoundField DataField="BuyerCode" HeaderText="Buyer Code" />
                                    <asp:BoundField DataField="Discount" HeaderText="Discount" />
                                    <asp:BoundField DataField="FairTrade" HeaderText="FairTrade" />
                                    <asp:BoundField DataField="FairTradPremium" HeaderText="FairTrade Premium" />
                                    <asp:TemplateField HeaderText="CIF Air Freight Terms">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPrice" runat="server">
                                                <asp:ListItem Text="Europen & East USA" Value="1" />
                                                <asp:ListItem Text="West USA" Value="2" />
                                            </asp:DropDownList>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbLotsample" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Lotsample") %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Lotsample
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbBApproval" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "Apporval") %>' />
                                            <asp:HiddenField ID="hfLoginID" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserLoginID") %>' />
                                            <asp:HiddenField ID="hfPassword" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "UserPassword") %>' />
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            Approval
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="Buyerid" HeaderText="Company Name" Visible="false" />
                                    <asp:ButtonField ButtonType="Link" Text="Edit" CommandName="Edit" />
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server">No approved list</asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-12" style="text-align: center">
                            <asp:Button ID="btnMembersSubmit" runat="server" Text="Unapproved" CssClass="btn btn-success" Visible="false"
                                OnClick="btnMembersSubmit_Click" />
                        </div>
                    </div>
                    <div id="divbuyerApprovaledit" runat="server" align="center" visible="false" class="row">
                        <div class="form form-horizontal">
                            <div class="form-body">
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Buyer Name</label>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblBuyerName" runat="server" ForeColor="Orange" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Country</label>
                                            <div class="col-md-4">
                                                <asp:Label ID="lblCountry" runat="server" ForeColor="Orange" />
                                            </div>
                                        </div>
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Buyer Code</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtBcode" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Discount</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtDis" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Fair Trade</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtFT" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Fair Trade Premium</label>
                                            <div class="col-md-4">
                                                <asp:TextBox ID="txtFTP" runat="server" CssClass="form-control" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">CIF Air Freight Terms</label>
                                            <div class="col-md-4">
                                                <asp:DropDownList ID="ddlPrice" runat="server" Enabled="false" CssClass="form-control">
                                                    <asp:ListItem Text="Europen & East USA" Value="1" />
                                                    <asp:ListItem Text="West USA" Value="2" />
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="col-sm-6">
                                        <div class="form-group">
                                            <label class="col-md-4 control-label">Lot sample</label>
                                            <div class="col-md-4" style="text-align:left;margin-top: 8px;">
                                                <asp:CheckBox ID="cbLotsample" runat="server" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12" style="text-align: center">
                                       <%-- <asp:Button ID="btnEdit" runat="server" Text="Submit" CssClass="btn  btn-success" OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default" OnClick="btnBack_Click" />--%>
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

