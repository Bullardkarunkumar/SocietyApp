<%@ Page Title="Mudarorganic-FarmerApproval" Language="C#" MasterPageFile="~/MudarMasterNew.master"
    AutoEventWireup="true" CodeFile="FarmerApproval.aspx.cs" Inherits="Admin_FarmerApproval" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Farmer's Approval List 
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div id="btnAgent" align="center" runat="server" class="col-sm-12">
                                <div class="col-sm-12" style="margin-bottom: 10px">
                                    <asp:Button ID="btnApproveFarmer" runat="server" OnClick="btnApproveFarmer_Click"
                                        CssClass="btn btn-success"
                                        Text="Approve Farmer" />
                                </div>
                                <div class="col-sm-12">
                                    <asp:GridView ID="gvFarmer" runat="server" AutoGenerateColumns="False"
                                        DataKeyNames="FarmerID,FarmerCode" OnRowCommand="gvFarmer_RowCommand"
                                        CssClass="table table-bordered mudargrid" PageSize="30" AllowSorting="True"
                                        OnSorting="gvFarmer_Sorting">
                                        <Columns>
                                            <%--<asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />--%>
                                            <asp:ButtonField ButtonType="Link" DataTextField="FarmerCode" HeaderText="Farmer Code" CommandName="FarmerCode" SortExpression="FarmerCode" />
                                            <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" SortExpression="FirstName" />
                                            <asp:BoundField DataField="City_Village" HeaderText="Area" SortExpression="City_Village" />
                                            <asp:ButtonField ButtonType="Link" Text="EDIT" HeaderText="Enter Form" CommandName="Farmer" />
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="cbBApproval" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "PRESIDNT") %>' />
                                                </ItemTemplate>
                                                <HeaderTemplate>
                                                    Approval
                                                </HeaderTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="InspectionComments" HeaderText="Comments" />
                                            <asp:BoundField DataField="InspectorName" HeaderText="Inspector" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

