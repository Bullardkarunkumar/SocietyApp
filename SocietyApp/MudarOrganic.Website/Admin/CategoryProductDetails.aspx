<%@ Page Title="Mudarorganic-Season or Category or Product" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true"
    CodeFile="CategoryProductDetails.aspx.cs" Inherits="Admin_ProductDetails" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <asp:HiddenField ID="htnSelectedTab" runat="server" Value="1" />
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Category or Products or Season List
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="tabbable-line nav-justified">
                        <ul class="nav nav-tabs nav-justified">
                            <li id="liCategory" runat="server">
                                <asp:LinkButton ID="lnkbtnCategory" runat="server" Text="Category" OnClick="lnkbtnCategory_Click"></asp:LinkButton>
                            </li>
                            <li id="liProducts" runat="server">
                                <asp:LinkButton ID="lnkbtnProducts" runat="server" Text="Products" OnClick="lnkbtnProducts_Click"></asp:LinkButton>
                            </li>
                            <li id="liSeason" runat="server">
                                <asp:LinkButton ID="lnkbtnSeason" runat="server" Text="Season List" OnClick="lnkbtnSeason_Click"></asp:LinkButton>
                            </li>
                        </ul>
                        <div class="tab-content">
                            <div class="tab-pane" id="tabpaneSeaon" runat="server">
                                <div id="divSeasonButton" align="center" runat="server" class="row">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnSeason" runat="server" OnClick="btnSeason_Click" CssClass="btn btn-success"
                                            Text="Add Season" />
                                    </div>
                                </div>
                                <div id="divSeasonDetails" runat="server" class="row">
                                    <div class="col-sm-12">
                                        <div class="col-sm-2">
                                        </div>
                                        <div class="col-sm-6">
                                            <div class="portlet-body form">
                                                <div class="form-horizontal form-body" style="padding-bottom: 5px">
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Season Year</label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList ID="ddlSelSeasonYear" runat="server" AutoPostBack="True" CssClass="form-control form-control-inline input-large"
                                                                OnSelectedIndexChanged="ddlSelSeasonYear_SelectedIndexChanged">
                                                            </asp:DropDownList>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-sm-4">
                                        </div>
                                    </div>
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvSeason" runat="server" AutoGenerateColumns="False"
                                            CssClass="table table-bordered mudargrid" DataKeyNames="SeasonID"
                                            OnRowCommand="gvSeason_RowCommand" OnRowDataBound="gvSeason_RowDataBound">
                                            <Columns>
                                                <asp:BoundField DataField="SeasonID" HeaderText="Season ID" Visible="false" />
                                                <asp:ButtonField ButtonType="Link" CommandName="cmd_select"
                                                    DataTextField="SeasonName" HeaderText="Season Name" />
                                                <asp:BoundField DataField="StartDate" DataFormatString="{0:dd MMM yyyy}"
                                                    HeaderText="Start Date" />
                                                <asp:BoundField DataField="EndDate" DataFormatString="{0:dd MMM yyyy}"
                                                    HeaderText="End Date" />
                                                <asp:ButtonField ButtonType="Image" CommandName="cmd_delete"
                                                    HeaderText="Delete" ImageUrl="~/images/Delete.jpg"
                                                    ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                            </Columns>
                                            <HeaderStyle CssClass="flip-content" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="divAddSeason" runat="server" class="row">
                                    <div class="portlet-body form">
                                        <div class="form-horizontal">
                                            <div class="form-body" style="margin-left: 30px">
                                                <div class="form-group" style="display: none">
                                                    <label class="col-md-3 control-label">Season ID</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtSeasonID" runat="server" CssClass="form-control form-control-inline input-large" Enabled="False"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Season Name</label>
                                                    <div class="col-md-9">
                                                        <asp:TextBox ID="txtSeasonNmae" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Season Year</label>
                                                    <div class="col-md-9">
                                                        <asp:DropDownList ID="ddlSeasonYear" CssClass="form-control form-control-inline input-large" runat="server">
                                                        </asp:DropDownList>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">Start Date</label>
                                                    <div class="col-md-9">
                                                        <div class="input-group date form_datetime form_datetime bs-datetime input-large">
                                                            <asp:TextBox ID="txtStartDate" runat="server" CssClass="form-control form-control-inline"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <button class="btn default date-set" type="button">
                                                                    <i class="fa fa-calendar"></i>
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>

                                                <div class="form-group">
                                                    <label class="col-md-3 control-label">End Date</label>
                                                    <div class="col-md-9">
                                                        <div class="input-group date form_datetime form_datetime bs-datetime input-large">
                                                            <asp:TextBox ID="txtEndDate" runat="server" CssClass="form-control form-control-inline"></asp:TextBox>
                                                            <span class="input-group-addon">
                                                                <button class="btn default date-set" type="button">
                                                                    <i class="fa fa-calendar"></i>
                                                                </button>
                                                            </span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <table align="center">
                                        <tr>
                                            <td colspan="2" align="center">
                                                <asp:GridView ID="grdSeasonProduct" runat="server" AutoGenerateColumns="False"
                                                    CssClass="table table-bordered mudargrid" DataKeyNames="ProductID">
                                                    <Columns>
                                                        <asp:BoundField DataField="ProductID" HeaderText="ProductID" Visible="false" />
                                                        <asp:BoundField DataField="ProductName"
                                                            HeaderText="Product Name" />
                                                        <asp:TemplateField>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="chkItemCheck" runat="server" Checked='<%# Eval("Selected") %>' />
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
                                                    </Columns>
                                                    <HeaderStyle CssClass="flip-content" />
                                                </asp:GridView>
                                            </td>
                                        </tr>
                                    </table>
                                    <div class="row form-actions noborder text-center" style="align-content: center">
                                        <asp:Button ID="btnSeasonSubmit" runat="server" Text="Submit" CssClass="btn btn-success" OnClick="btnSeasonSubmit_Click" />
                                        &nbsp;
                                    <asp:Button ID="btnSeasonClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnSeasonClear_Click" />
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="tabpaneCategory" runat="server">
                                <div id="divCategoryButton" align="center" runat="server" class="row margin-bottom-10">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnAddCategory" runat="server" OnClick="btnAddCategory_Click" CssClass="btn btn-success"
                                            Text="Add Category" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvCategory" runat="server" AutoGenerateColumns="False" DataKeyNames="CategoryID"
                                            Font-Size="Medium" OnRowDataBound="gvCategory_RowDataBound" OnRowCommand="gvCategoryList_RowCommand"
                                            CssClass="table table-bordered mudargrid">
                                            <Columns>
                                                <asp:BoundField DataField="CategoryID" HeaderText="Category ID" Visible="false" />
                                                <asp:BoundField DataField="CategoryName" HeaderText=" Category Name " />
                                                <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                                    <ItemTemplate>
                                                        <asp:LinkButton ID="lkDelte" runat="server" CommandName="cmd_delete" CommandArgument='<%# Eval("CategoryID") %>'><img src="../images/Delete.jpg" alt="Delete"/></asp:LinkButton>
                                                    </ItemTemplate>
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:TemplateField>
                                            </Columns>
                                            <EmptyDataTemplate>
                                                <label>No Categories found.</label>
                                            </EmptyDataTemplate>
                                            <HeaderStyle CssClass="flip-content" />
                                        </asp:GridView>
                                    </div>
                                </div>
                                <div id="divAddCategory" runat="server" class="row">
                                    <div class="col-sm-12" style="margin-left: 20px">
                                        <div class="portlet-body form" style="align-content: center">
                                            <div class="form-horizontal">
                                                <div class="form-body" style="margin-left: 30px">
                                                    <div class="form-group" style="display: none">
                                                        <label class="col-md-3 control-label">Category ID</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtCategoryID" CssClass="form-control form-control-inline input-large"
                                                                runat="server" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Category Name</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtCategoryName" CssClass="form-control form-control-inline input-large"
                                                                runat="server"></asp:TextBox>
                                                            <asp:RequiredFieldValidator
                                                                ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCategoryName"
                                                                ValidationGroup="Category" ErrorMessage="Enter the CategoryName" Display="Dynamic"
                                                                Text="Enter the CategoryName"></asp:RequiredFieldValidator>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="row form-actions noborder text-center" style="align-content: center">
                                            <asp:Button ID="btnCatSubmit" runat="server" ValidationGroup="Category" Text="Submit" CssClass="btn btn-success" OnClick="btnCatSubmit_Click" />
                                            &nbsp;
                                    <asp:Button ID="btnCatClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnCatClear_Click" />
                                        </div>
                                    </div>
                                </div>
                            </div>

                            <div class="tab-pane" id="tabpaneProducts" runat="server">

                                <div id="divProductAdd" align="center" runat="server" class="row margin-bottom-10">
                                    <div class="col-sm-12">
                                        <asp:Button ID="btnAddProduct" runat="server" OnClick="btnAddProduct_Click" CssClass="btn btn-success"
                                            Text="Add Product" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-sm-12">
                                        <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID"
                                            OnRowCommand="gvProduct_RowCommand" Font-Size="Medium" CssClass="table table-bordered mudargrid">
                                            <Columns>
                                                <asp:BoundField DataField="ProductID" HeaderText="ProductID" Visible="false" />
                                                <asp:ButtonField ButtonType="Link" CommandName="cmd_Select" DataTextField="ProductName"
                                                    HeaderText="ProductName" />
                                                <asp:BoundField DataField="ItcHsCode" HeaderText="ItcHsCode" />
                                                <asp:BoundField DataField="CategoryName" HeaderText="ICS Product" />
                                                <asp:BoundField DataField="Specification" HeaderText="Specification" />
                                                <asp:ButtonField ButtonType="Image" CommandName="cmd_delete" HeaderText="Delete"
                                                    ImageUrl="~/images/Delete.jpg" ItemStyle-HorizontalAlign="Center">
                                                    <ItemStyle HorizontalAlign="Center" />
                                                </asp:ButtonField>
                                            </Columns>
                                            <HeaderStyle CssClass="flip-content" />
                                        </asp:GridView>
                                    </div>
                                </div>

                                <div id="divAddProduct" runat="server" class="row">
                                    <div class="col-sm-12" style="margin-left: 20px">
                                        <div class="portlet-body form" style="align-content: center">
                                            <div class="form-horizontal">
                                                <div class="form-body" style="margin-left: 30px">

                                                    <div class="form-group" style="display: none">
                                                        <label class="col-md-3 control-label">Product ID</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtProductID" runat="server" CssClass="form-control form-control-inline input-large" Enabled="False"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Product Code</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtProductcode" CssClass="form-control form-control-inline input-large" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Product Name</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtProductName" CssClass="form-control form-control-inline input-large" runat="server"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Category</label>
                                                        <div class="col-md-9">
                                                            <asp:DropDownList ID="ddlSelectCategory" runat="server" CssClass="form-control form-control-inline input-large"></asp:DropDownList>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Description</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtDescription" runat="server" CssClass="form-control form-control-inline input-large" TextMode="MultiLine"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">ITCHS Code</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtITCHSCode" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                        </div>
                                                    </div>

                                                    <div class="form-group">
                                                        <label class="col-md-3 control-label">Specification</label>
                                                        <div class="col-md-9">
                                                            <asp:TextBox ID="txtSpecification" runat="server" CssClass="form-control form-control-inline input-large"></asp:TextBox>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="row form-actions noborder text-center" style="align-content: center">
                                        <asp:Button ID="btnProductSubmit" runat="server" ValidationGroup="Category" Text="Submit" CssClass="btn btn-success" OnClick="btnProductSubmit_Click" />
                                        &nbsp;
                                    <asp:Button ID="btnProductClear" runat="server" Text="Clear" CssClass="btn btn-default" OnClick="btnProductClear_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script type="text/javascript">
                $(function () {
                    fnBindPlugins();
                });

                function fnBindPlugins() {
                    $('#<%= txtStartDate.ClientID %>').datetimepicker({
                        minView: 2,
                        pickTime: false,
                        format: 'dd/mm/yyyy',
                        autoclose: true
                    });
                    $('#<%= txtEndDate.ClientID %>').datetimepicker({
                        minView: 2,
                        pickTime: false,
                        format: 'dd/mm/yyyy',
                        autoclose: true
                    });
                }
                function fnShowMessage(msg) {
                    bootbox.alert(msg);
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

