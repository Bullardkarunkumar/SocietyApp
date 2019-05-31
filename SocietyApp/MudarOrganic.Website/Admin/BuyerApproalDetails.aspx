<%@ Page Title="Mudarorganic-BuyerApproval" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="BuyerApproalDetails.aspx.cs" Inherits="Admin_BuyerDetails" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div id="content_area_Home" class="panel panel-success">
                <div id="header_Text" class="panel-heading" style="background-color: #32c5d2 !important; text-align: center; color: #fff; font-weight: bold">
                    Buyer Approval List
                </div>
                <div class="panel-body">
                    <div class="row" style="text-align: center">
                        <asp:LinkButton ID="lbtnUnapproved" runat="server" Text="UN Approved" OnClick="lbtnUnapproved_Click"
                            Visible="false" />
                        <asp:LinkButton ID="lbtnapproved" runat="server" Text="Approved" OnClick="lbtnapproved_Click"
                            Visible="false" />
                    </div>
                    <div class="row" style="text-align: center">
                        <asp:Button ID="btnUnapproved" runat="server" Text="Unapproved List" CssClass="btn btn-default"
                            OnClick="btnUnapproved_Click" />
                        <asp:Button ID="btnapproved" runat="server" Text="Approved List" CssClass="btn btn-success"
                            OnClick="btnapproved_Click" />
                    </div>

                    <div id="UnApproved" runat="server" class="row" style="margin-top: 15px">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvBuyerDetails" DataKeyNames="Buyerid" AutoGenerateColumns="False"
                                runat="server" OnRowCommand="gvBuyerDetails_RowCommand" CssClass="table table-bordered mudargrid"
                                AllowSorting="True" OnSorting="gvBuyerDetails_Sorting" OnRowDataBound="gvBuyerDetails_RowDataBound">
                                <Columns>
                                    <asp:BoundField DataField="BuyerCompanyName" HeaderText="Company Name" SortExpression="BuyerCompanyName" />
                                    <asp:BoundField DataField="email" HeaderText="EMail" ItemStyle-Wrap="true" Visible="false">
                                        <ItemStyle Wrap="True"></ItemStyle>
                                    </asp:BoundField>
                                    <asp:BoundField DataField="MobileforTextingpurpose" HeaderText="Mobile" Visible="false" />
                                    <asp:BoundField DataField="CState" HeaderText="State" Visible="false" />
                                    <asp:ButtonField ButtonType="Link" Text="View" HeaderText="View" CommandName="View" />
                                    <asp:TemplateField HeaderText="Buyer Code">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtBCode" Width="50" ToolTip="BuyerCode" Text='<%# DataBinder.Eval(Container.DataItem, "BuyerCode") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Discount">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtDiscount" Width="50" ToolTip="Discount" Text='<%# DataBinder.Eval(Container.DataItem, "Discount") %>'
                                                runat="server" />
                                            <asp:HiddenField ID="hfEmail" Value='<%# DataBinder.Eval(Container.DataItem, "email") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="FairTrade">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFairT" Width="50" ToolTip="FairTrade" Text='<%# DataBinder.Eval(Container.DataItem, "FairTrade") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Fair Trad Premium">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtFairTP" Width="50" ToolTip="Fair Trad Premium" Text='<%# DataBinder.Eval(Container.DataItem, "FairTradPremium") %>'
                                                runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CIF Air Freight Terms">
                                        <ItemTemplate>
                                            <asp:DropDownList ID="ddlPrice" runat="server" ToolTip="CIF Air Freight Terms">
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
                                    <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="lkDelte" runat="server" CommandName="cmd_delete" CommandArgument='<%# Eval("Buyerid") %>'><img src="../images/Delete.jpg" alt="Delete"/></asp:LinkButton>
                                        </ItemTemplate>
                                        <ItemStyle HorizontalAlign="Center" />
                                    </asp:TemplateField>
                                </Columns>
                                <EmptyDataTemplate>
                                    <asp:Label ID="lblEmpty" runat="server">No Un-approved list</asp:Label>
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </div>
                        <div class="col-sm-12" style="text-align: center">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSubmit_Click"
                                Visible="false" />
                        </div>
                    </div>
                    <div id="Approved" runat="server" class="row" style="margin-top: 15px">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvApprovaledBuyer" DataKeyNames="Buyerid" AutoGenerateColumns="False"
                                runat="server" OnRowCommand="gvApprovaledBuyer_RowCommand" CssClass="table table-bordered mudargrid"
                                AllowSorting="True" OnSorting="gvApprovaledBuyer_Sorting" OnRowEditing="gvApprovaledBuyer_RowEditing">
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
                            <asp:Button ID="btnApprovalSubmit" runat="server" Text="Unapproved" CssClass="btn btn-success" Visible="false"
                                OnClick="btnApprovalSubmit_Click" />
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
                                        <asp:Button ID="btnEdit" runat="server" Text="Submit" CssClass="btn  btn-success" OnClick="btnEdit_Click" />
                                        <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btn btn-default" OnClick="btnBack_Click" />
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
