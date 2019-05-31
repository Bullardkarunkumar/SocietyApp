<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="NewSampleRequest.aspx.cs" Inherits="Buyer_NewSampleRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" language="javascript">
        function checkstatus(bEnable) {

            document.getElementById('<%= txtCourierName.ClientID %>').disabled = !bEnable
            document.getElementById('<%= txtCourierAcNo.ClientID %>').disabled = !bEnable

            document.getElementById('<%= txtCourierName.ClientID %>').value = ''
            document.getElementById('<%= txtCourierAcNo.ClientID %>').value = ''
        }
    </script>
    <asp:UpdatePanel ID="upd1" runat="server">
        <ContentTemplate>
            <div class="portlet box green">
                <div class="portlet-title">
                    <div class="caption">
                        <i class="fa fa-cogs"></i>New Sample Request
                    </div>
                </div>
                <div class="portlet-body flip-scroll">
                    <div class="row">
                        <div class="col-sm-12">
                            <span style="color: #FF0000">*Below Quantity Must be <= 100gms 
                            </span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <span style="color: #FF0000">*Samples doesnot belong to any patricular Lot
                            </span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" DataKeyNames="ProductID" CssClass="table table-bordered mudargrid">
                                <Columns>
                                    <asp:BoundField HeaderText="Product" DataField="ProductName" />
                                    <asp:BoundField HeaderText="Organic" DataField="ProductID" Visible="false" />
                                    <asp:BoundField HeaderText="Fair Trade" DataField="ProductID" Visible="false" />
                                    <asp:TemplateField HeaderText="Qty in gms">
                                        <ItemTemplate>
                                            <asp:TextBox ID="txtQuantity" runat="server" Text="0" />
                                            <asp:RangeValidator ID="RangeValidator1" runat="server" ControlToValidate="txtQuantity"
                                                MinimumValue="0" MaximumValue="100" Type="Integer" SetFocusOnError="true" ErrorMessage="*">
                                            </asp:RangeValidator>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Check">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbDoc" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12" style="text-align: center">
                            <asp:CheckBox ID="cbCourier" runat="server" onclick="checkstatus(this.checked);" Text="Use Our Courier Account" />
                        </div>
                    </div>
                    <div class="row">
                        <div class="panel panel-primary">
                            <div class="panel-heading">
                                <span>Courier Account Details</span>
                            </div>
                            <div class="panel-body">
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label>Courier Name</label>
                                        <asp:TextBox ID="txtCourierName" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12">
                                    <div class="form-group">
                                        <label>Courier Account No</label>
                                        <asp:TextBox ID="txtCourierAcNo" runat="server" CssClass="form-control"></asp:TextBox>
                                    </div>
                                </div>
                                <div class="col-sm-12" style="text-align: center">
                                    <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                        CssClass="btn btn-success" OnClick="btnSubmit_Click" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

