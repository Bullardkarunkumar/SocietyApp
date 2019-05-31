<%@ Page Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="HoildayList.aspx.cs" Inherits="Admin_HoildayList" Title="Mudarorganic-HoilDayList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-gift"></i>Holiday List
                        <asp:Label ID="lblHolidayID" runat="server" Text="" Visible="false" />
                    </div>
                </div>
                <div class="portlet-body">
                    <div class="row">
                        <div class="col-sm-12">
                            <div class="form-horizontal" style="margin-left: 150px">
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Year</label>
                                    <div class="col-md-9">
                                        <asp:DropDownList ID="ddlyear" runat="server" CssClass="form-control form-control-inline input-large"
                                            OnSelectedIndexChanged="ddlyear_SelectedIndexChanged">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="form-group">
                                    <label class="col-md-3 control-label">Holiday Date</label>
                                    <div class="col-md-9">
                                        <asp:TextBox ID="txtHolidayDate" runat="server" CssClass="form-control form-control-inline input-large">
                                        </asp:TextBox>
                                        <asp:CalendarExtender
                                            ID="dtpLevdate" runat="server" Format="MM/dd/yyyy"
                                            TargetControlID="txtHolidayDate">
                                        </asp:CalendarExtender>
                                    </div>
                                </div>

                            </div>
                            <div class="form-actions text-center">
                                <asp:Button ID="btnSubmit" runat="server" CssClass="btn btn-success"
                                    Text="Submit" OnClick="btnSubmit_Click" />
                            </div>
                        </div>
                        <div class="col-sm-12">
                            <div class="col-sm-12" style="margin-left: 250px">
                                Total Holidays :
                                <asp:Label ID="lblHCount" runat="server" Visible="false" ForeColor="OrangeRed" />
                            </div>
                            <div class="col-sm-12">
                                <asp:GridView ID="gvHolidayList" runat="server" AutoGenerateColumns="False" DataKeyNames="HolidayID"
                                    OnRowCommand="gvHolidayList_RowCommand" CssClass="table table-bordered mudargrid"
                                    OnRowDataBound="gvHolidayList_RowDataBound">
                                    <Columns>
                                        <asp:BoundField DataField="HolidayID" HeaderText="S.No" Visible="false"></asp:BoundField>
                                        <asp:BoundField DataField="HolidayYear" HeaderText="Year"></asp:BoundField>
                                        <asp:BoundField DataField="HolidayDate" HeaderText="Holiday Date" DataFormatString="{0:dd MMM yyyy}"></asp:BoundField>
                                        <asp:ButtonField ButtonType="Link" Text="Edit" HeaderText="View" CommandName="cmd_edit"></asp:ButtonField>
                                        <asp:TemplateField HeaderText="Delete" ItemStyle-HorizontalAlign="Center">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lkDelte" runat="server"
                                                    CommandName="cmd_delete" CommandArgument='<%# Eval("HolidayID") %>'><img src="../images/Delete.jpg" alt="Delete"/></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <script>
                function fnShowMessage(msg) {
                    bootbox.alert(msg);
                }
            </script>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>
