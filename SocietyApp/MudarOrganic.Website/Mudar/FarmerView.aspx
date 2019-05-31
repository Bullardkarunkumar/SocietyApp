<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FarmerView.aspx.cs" Inherits="Farmer_FarmerView" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">
<div id="content_area_Home" style="height:auto">
    <div>
        <div>
            <h3 id="header_Text">
                Farmer Full View</h3>
        </div>
        <table width="100%" border="1" align="center" style="border-color:"#C0C0C0">
            <tr>
                <td align="center">
                    Farmer name
                </td>
                <td>
                    <asp:Label ID="lblFarmerName" runat="server" />&nbsp;<asp:HiddenField ID="hdfarmerUid" runat="server" Value="" />
                </td>
            </tr>
            <tr>
                <td  colspan="2">
                    <table width="100%">
                        <tr>
                            <td align="center">
                                Total Area in Hc
                            </td>
                           <td>&nbsp;</td>
                            <td align="center">
                                Total number of Plots
                            </td>
                        </tr>
                        <tr>
                         <td align="center">
                                <asp:Label ID="lblTotalArea" runat="server" />
                            </td>
                            <td>&nbsp;</td>
                            <td align="center">
                                <asp:Label ID="lblPlots" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    Father name
                </td>
                <td align="center">
                    <asp:Label ID="lblFatherName" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    Address
                </td>
                <td align="center">
                    <asp:Label ID="lblAddress" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    Fixed Phone
                </td>
                <td align="center">
                    <asp:Label ID="lblPhone" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    Mobile Phone
                </td>
                <td align="center">
                    <asp:Label ID="lblMobile" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    Farmer Code
                </td>
                <td align="center">
                    <asp:Label ID="lblFarmerCode" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    Farmer Registration no
                </td>
                <td align="center">
                    <asp:Label ID="lblFarmerRegistration" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h3 id="header_Text">
                Bank Details</h3>
                </td>
            </tr>
            <tr>
                <td align="center">
                    Name of the Bank
                </td>
                <td align="center">
                    <asp:Label ID="lblBankName" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    A/C holder name
                </td>
                <td align="center">
                    <asp:Label ID="lblHolderName" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    A/C Number
                </td>
                <td align="center">
                    <asp:Label ID="lblAcctNo" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    Last date of chemical application
                </td>
                <td align="center">
                    <asp:Label ID="lblChemicalDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:RadioButton ID="rbOrganic" Text="Organic" GroupName="organic" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:RadioButton ID="rbNonOrganic" Text="Org & Fair Trade" GroupName="organic" runat="server" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     <h3 id="header_Text">
                Plot Details</h3>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <asp:GridView ID="gvPlot" runat="server" DataKeyNames="FarmID" AutoGenerateColumns="false">
                        <Columns>
                            <asp:BoundField HeaderText="Plot Code" DataField="AreaCode" />
                            <asp:BoundField HeaderText="Area in Hc" DataField="PlotArea" />
                            <asp:BoundField HeaderText="Latitude" DataField="Latitude" />
                            <asp:BoundField HeaderText="Longitude" DataField="Longitude" />
                        </Columns>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     <h3 id="header_Text">
                Season Information</h3>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    Select Season Year <asp:DropDownList ID="ddlSeasonYear" runat="server" 
                        AutoPostBack="true" 
                        onselectedindexchanged="ddlSeasonYear_SelectedIndexChanged" />
                    <asp:DataList ID="dlFarmerSeasonDetails" DataKeyField="SeasonID" runat="server" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                        GridLines="Vertical">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="lblseasonName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "SeasonName")%>' />
                                        <asp:HiddenField ID="hfStartDate" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "StartDate")%>' />
                                        <asp:HiddenField ID="hfEndDate" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "EndDate")%>' />
                                    </td>
                                    <td>
                                        <asp:CheckBoxList ID="cblProduct" runat="server" RepeatDirection="Horizontal">
                                        </asp:CheckBoxList>
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     <h3 id="header_Text">
                Field Risk Assessment</h3>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div align="center">
            <asp:DataList ID="dlFieldRisk" runat="server" BackColor="White" 
                BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                ForeColor="Black" GridLines="Vertical">
                <ItemTemplate>
                    <table>
                        <tr>
                            <td >
                                <asp:Label ID="lblFieldRisk" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "AreaCode")%>' /> : 
                                <asp:HiddenField ID="hfFieldRisk" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "FarmID")%>' />
                            </td>
                            <td>
                                <asp:RadioButtonList ID="rblFieldRisk" runat="server" RepeatDirection="Horizontal" />
                            </td>
                        </tr>
                    </table>
                </ItemTemplate>
                <FooterStyle BackColor="#CCCC99" />
                <AlternatingItemStyle BackColor="White" />
                <ItemStyle BackColor="#F7F7DE" />
                <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            </asp:DataList>
        </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                     <h3 id="header_Text">
                Family Information</h3>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table>
                        <tr>
                            <td>
                                Earning members
                            </td>
                           <td></td>
                            <td>
                                Dependent Elders
                            </td>
                           <td></td>
                            <td>
                                Dependent Children
                            </td>
                           
                        </tr>
                        <tr> 
                        <td align="center">
                                <asp:Label ID="lblEarningMember" runat="server" />
                            </td>
                            <td></td>
                             <td align="center">
                                <asp:Label ID="lblDElder" runat="server" />
                            </td>
                            <td></td>
                             <td align="center">
                                <asp:Label ID="lblDChildren" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h3 id="header_Text">
                Children Information</h3>
                </td>
            </tr>
            <tr>
                <td  colspan="2" align="center">
                   <asp:GridView ID="gvFamilyDet" runat="server" AutoGenerateColumns="False" DataKeyNames="FFamilyDetailsId"
                                   BackColor="White" BorderColor="#DEDFDE"
                                   BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Vertical">
                                   <RowStyle BackColor="#F7F7DE" />
                                   <Columns>
                                       <asp:BoundField DataField="Name" HeaderText="ChildName" />
                                       <asp:BoundField DataField="Gender" HeaderText="Gender" />
                                       <asp:BoundField DataField="DOB" HeaderText="DateofBirth" />
                                       <asp:BoundField DataField="AGE" HeaderText="Age" />
                                       <asp:BoundField DataField="SchoolGoing" HeaderText="School Going" />
                                       <asp:BoundField DataField="Working" HeaderText="Work Going" />
                                       <asp:ButtonField ButtonType="Image" CommandName="cmd_delete" HeaderText="Delete"
                                           ImageUrl="~/images/Delete.jpg" ItemStyle-HorizontalAlign="Center">
                                           <ItemStyle HorizontalAlign="Center" />
                                       </asp:ButtonField>
                                   </Columns>
                                   <FooterStyle BackColor="#CCCC99" />
                                   <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                   <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                   <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                   <AlternatingRowStyle BackColor="White" />
                               </asp:GridView>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <h3 id="header_Text">
                Animal Husbandry</h3>
                </td>
            </tr>
            <tr>
                <td colspan="2" align="center">
                    <table>
                        <tr>
                            <td>
                                Buffalos
                            </td>
                            <td colspan="2"></td>
                            <td>
                                Cows
                            </td>
                             <td colspan="2"></td>
                            <td>
                                Ox
                            </td>
                             <td colspan="2"></td>
                            <td>
                                Sheep
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Label ID="lblBuffalos" runat="server" />
                            </td>
                            <td colspan="2"></td>
                            <td  align="center">
                                <asp:Label ID="lblCows" runat="server" />
                            </td>
                             <td colspan="2"></td>
                            <td  align="center">
                                <asp:Label ID="lblOx" runat="server" />
                            </td>
                             <td colspan="2"></td>
                            <td  align="center">
                                <asp:Label ID="lblSheep" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td align="center">
                    Comments from Branch
                </td>
                <td align="center">
                    <asp:Label ID="lblComments" runat="server" />
                </td>
            </tr>
            <tr>
                <td align="center">
                    Uploaded by 
                </td>
                <td align="center">
                    <asp:Label ID="lblUploadedBy" runat="server" />
                </td>
            </tr>
        </table>
    </div>
    <div align="center">
        <asp:Button id="btnBack" runat="server" CssClass="fb8" Text="Back" PostBackUrl="~/Farmer/Farmers.aspx" />
    </div>
</div>
</asp:Content>

