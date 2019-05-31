<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="FarmingInfo.aspx.cs" Inherits="Farmer_FarmingInfo" %>

<%--link master page and content page--%>
<%@ MasterType VirtualPath="~/MudarMaster.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" Runat="Server">

    <asp:MultiView ID="MultiView1" runat="server" ActiveViewIndex="0">
<%--        <asp:View ID="vFarmerList" runat="server"  >
            <div id="content_area_Home" align="center" style="height: auto">
                <div id="header_Text">
                    Farmer List Approved By Inspection
                </div>
                <asp:GridView ID="gvFarmer" runat="server" AutoGenerateColumns="False" DataKeyNames="FarmerID"
                    BackColor="White" BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px"
                    CellPadding="4" ForeColor="Black" GridLines="Vertical" OnRowCommand="gvFarmer_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="FarmerCode" HeaderText="Farmer Code" />
                        <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                        <asp:BoundField DataField="City_Village" HeaderText="Area" />
                        <asp:ButtonField ButtonType="Link" Text="View" HeaderText="Enter Form" CommandName="Farmer" />
                        <asp:BoundField DataField="InspectionComments" HeaderText="Comments" />
                        <asp:BoundField DataField="InspectorName" HeaderText="Inspector" />
                    </Columns>
                    <RowStyle BackColor="#F7F7DE" />
                    <FooterStyle BackColor="#CCCC99" />
                    <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                    <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="White" />
                </asp:GridView>
            </div>
        </asp:View>
--%>        
        <asp:View ID="vProduct" runat="server">
            <div id="content_area_Home" align="center">
                <div id="header_Text">
                    General Farming Information
                </div>
                <table width="100%" align="center" >
                    <tr><td colspan"2" align="center" style="font-size:larger"> Year</td><td colspan="2" align="center" style="font-size:larger">Season</td></tr>
                    <tr>
                        
                        <td colspan="2" align="center">
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlYear_SelectedIndexChanged" Width="160px">
                            </asp:DropDownList>
                        </td>
                        
                        <td colspan="2" align="center">
                            &nbsp;&nbsp;<asp:DropDownList ID="ddlSeason" runat="server" AutoPostBack="true" 
                                onselectedindexchanged="ddlSeason_SelectedIndexChanged" Width="160px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4"></td>
                    </tr>
                    <tr>
                        <td colspan="4" align="center">
                            <asp:GridView ID="gvProduct" runat="server" AutoGenerateColumns="False" 
                                DataKeyNames="ProductID,ProductName" BackColor="White" BorderColor="#DEDFDE" 
                                BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" 
                                GridLines="Vertical" onrowcommand="gvProduct_RowCommand">
                                <RowStyle BackColor="#F7F7DE" />
                                <Columns>
                                    <asp:ButtonField ButtonType="Link" CommandName="Product" 
                                        DataTextField="ProductName" HeaderText="Product List" />
                                </Columns>
                                <FooterStyle BackColor="#CCCC99" />
                                <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
                                <SelectedRowStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                                <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                                <AlternatingRowStyle BackColor="White" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </div>
        </asp:View>
        <asp:View ID="vFarming" runat="server">
            <div id="content_area_Home" style="height: auto;">
                <div id="header_Text">
                    Farming Information <b>
                        <asp:Label ID="lblProductName" runat="server" Text="" />
                    </b>
                </div>
                <div>
                    <table align="center">
                     <tr><td colspan="5">
                         <asp:HiddenField ID="hfPIID" runat="server" Value="0" />
                         </td></tr>
                        <tr>
                            <td colspan="5" align="center" style="color:White;background-color:#6B696B;font-weight:bold;">Planting Information
                                &nbsp;</td>
                        </tr>
                        <tr style="background-color:#F7F7DE;">
                            <td>
                                Source
                            </td>
                            <td>
                                Bill No & Dt. Of Purchase
                            </td>
                            <td>
                                Seed Variety
                            </td>
                            <td>
                                Seed Treatment?
                            </td>
                            <td>
                                Qty in KG / HC
                            </td>
                        </tr>
                        <tr style="background-color:#F7F7DE;">
                            <td>
                                <asp:TextBox ID="txtPIsource" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtPIBill" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtPISeedVariety" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtPISeedTreatment" runat="server" />
                            </td>
                            <td>
                                <asp:TextBox ID="txtPIQuantity" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" align="center">
                                <asp:Button ID="btnPlanting" runat="server" Text="Submit" CssClass="fb8" OnClick="btnPlanting_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />
                </div>
                <div align="center" >
                    <asp:DataList ID="dlInputInfo" runat="server" DataKeyField="S_InputMID" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                        GridLines="Vertical" OnItemCommand="dlInputInfo_ItemCommand"> 
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Input Information
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddInput" runat="server" CommandName="AddInput" Text="ADD" class="fb8_go"/>
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Input Material
                                    </td>
                                    <td>
                                        Source
                                    </td>
                                    <td>
                                        Bill No
                                    </td>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        Qty in MT
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtMaterial" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMMaterial")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMSource")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBill" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMBillNo")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMDate")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMQuantity")%>'/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddTInput" runat="server" CommandName="AddTInput" Text="ADD" class="fb8_go"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:DataList ID="dlTransaction" runat="server" DataKeyField="S_InputMID" BackColor="White"
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
                                                            <asp:TextBox ID="txtTQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IM_MT_HC")%>'/>
                                                            <asp:HiddenField ID="hfInputMTId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "InputMTId")%>' />
                                                        </td>
                                                        <td>
                                                            MT / HC
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMDays")%>'/>
                                                        </td>
                                                        <td>
                                                            Days
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPeriod" runat="server">
                                                                <asp:ListItem Text="BEFORE" Value="BEFORE" />
                                                                <asp:ListItem Text="AFTER" Value="AFTER" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPlanting" runat="server">
                                                                <asp:ListItem Text="Planting" Value="Planting" />
                                                                <asp:ListItem Text="1st Harvest" Value="1st Harvest" />
                                                                <asp:ListItem Text="2nd Harvest" Value="2nd Harvest" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Button ID="btnInputinfo" runat="server" Text="Submit" CssClass="fb8" OnClick="btnInputinfo_Click" />
                    <br />
                    <br />
                    <br />
                </div>
                <div align="center" class="scroll_div">
                    <asp:DataList ID="dlDisease" runat="server" DataKeyField="S_DiseaseMID" BackColor="White"
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black"
                        GridLines="Vertical" OnItemCommand="dlDisease_ItemCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Disease Management Info
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddDisease" runat="server" CommandName="AddDisease" Text="ADD" class="fb8_go" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Disease
                                    </td>
                                    <td>
                                        Expected
                                    </td>
                                    <td>
                                        Observed
                                    </td>
                                    <td>
                                        Prevention Material
                                    </td>
                                    <td>
                                        Source
                                    </td>
                                    <td>
                                        Bill No if Purchased
                                    </td>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        Qty Applied  in KG / HC
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtDisease" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DiseaseName")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtExpected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIExpected")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtObserved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIObserved")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPrevention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIPreventionMaterial")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMISource")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBill" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIBillNo")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIDate")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIQuantity")%>'/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddTDisease" runat="server" CommandName="AddTDisease" Text="ADD" class="fb8_go"/>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:DataList ID="dlDTransaction" runat="server" DataKeyField="S_DiseaseMID" BackColor="White"
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
                                                            <asp:TextBox ID="txtDTQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIT_HC")%>'/>
                                                            <asp:HiddenField ID="hfDMITId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "DMITId")%>' />
                                                        </td>
                                                        <td>
                                                            KG / HC
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtDTDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "DMIT_Days")%>'/>
                                                        </td>
                                                        <td>
                                                            Days
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDTPeriod" runat="server">
                                                                <asp:ListItem Text="BEFORE" Value="BEFORE" />
                                                                <asp:ListItem Text="AFTER" Value="AFTER" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlDTPlanting" runat="server">
                                                                <asp:ListItem Text="Planting" Value="Planting" />
                                                                <asp:ListItem Text="1st Harvest" Value="1st Harvest" />
                                                                <asp:ListItem Text="2nd Harvest" Value="2nd Harvest" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Button ID="btnDisease" runat="server" Text="Submit" CssClass="fb8" OnClick="btnDisease_Click" />
                    <br />
                    <br />
                    <br />
                </div>
                <div>&nbsp;</div>
				<div align="center" class="scroll_div">
                    <asp:DataList ID="dlInsect" runat="server" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        DataKeyField="S_InsectMIID" ForeColor="Black" GridLines="Vertical" 
                        onitemcommand="dlInsect_ItemCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Insect Management Info
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddInsect" runat="server" class="fb8_go" 
                                            CommandName="AddInsect" Text="ADD" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Insect
                                    </td>
                                    <td>
                                        Expected
                                    </td>
                                    <td>
                                        Observed
                                    </td>
                                    <td>
                                        Prevention Material
                                    </td>
                                    <td>
                                        Source
                                    </td>
                                    <td>
                                        Bill No if Purchased
                                    </td>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        Qty Applied  in KG / HC
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtInsect" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsectName")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIExpected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIExpected")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIObserved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIObserved")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIPrevention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIPreventionMaterial")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtISource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMISource")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIBill" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIBillNo")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIDate")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtIQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "IMIQuantity")%>'/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddTInsect" runat="server" CommandName="AddTInsect" Text="ADD"  class="fb8_go" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:DataList ID="dlITransaction" runat="server" DataKeyField="S_InsectMIID" BackColor="White"
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
                                                            <asp:TextBox ID="txtITQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsectM_MT_HC")%>'/>
                                                            <asp:HiddenField ID="hfInMITId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "InsectMTId")%>' />
                                                        </td>
                                                        <td>
                                                            KG / HC
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtITDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "InsectMDays")%>'/>
                                                        </td>
                                                        <td>
                                                            Days
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlITPeriod" runat="server">
                                                                <asp:ListItem Text="BEFORE" Value="BEFORE" />
                                                                <asp:ListItem Text="AFTER" Value="AFTER" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlITPlanting" runat="server">
                                                                <asp:ListItem Text="Planting" Value="Planting" />
                                                                <asp:ListItem Text="1st Harvest" Value="1st Harvest" />
                                                                <asp:ListItem Text="2nd Harvest" Value="2nd Harvest" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Button ID="btnInsect" runat="server" Text="Submit" CssClass="fb8" OnClick="btnInsect_Click" />
                    <br />
                    <br />
                    <br />
                </div>
                <div>&nbsp;
                </div>
				<div align="center" class="scroll_div">
                    <asp:DataList ID="dlPest" runat="server" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        DataKeyField="S_PestMIID" ForeColor="Black" GridLines="Vertical" 
                        onitemcommand="dlPest_ItemCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Pest Management Info
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddPest" runat="server" class="fb8_go" 
                                            CommandName="AddPest" Text="ADD" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Pest
                                    </td>
                                    <td>
                                        Expected
                                    </td>
                                    <td>
                                        Observed
                                    </td>
                                    <td>
                                        Prevention Material
                                    </td>
                                    <td>
                                        Source
                                    </td>
                                    <td>
                                        Bill No if Purchased
                                    </td>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        Qty Applied  in KG / HC
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtPest" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PestName")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPExpected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIExpected")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPObserved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIObserved")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPPrevention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIPreventionMaterial")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMISource")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPBill" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIBillNo")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIDate")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtPQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIQuantity")%>'/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddTPest" runat="server" CommandName="AddTPest" Text="ADD" class="fb8_go"  />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:DataList ID="dlPTransaction" runat="server" DataKeyField="S_PestMIID" BackColor="White"
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
                                                            <asp:TextBox ID="txtPTQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIT_HC")%>'/>
                                                            <asp:HiddenField ID="hfPMITId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "PMITId")%>' />
                                                        </td>
                                                        <td>
                                                            KG / HC
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtPTDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "PMIT_Days")%>'/>
                                                        </td>
                                                        <td>
                                                            Days
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPTPeriod" runat="server">
                                                                <asp:ListItem Text="BEFORE" Value="BEFORE" />
                                                                <asp:ListItem Text="AFTER" Value="AFTER" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlPTPlanting" runat="server">
                                                                <asp:ListItem Text="Planting" Value="Planting" />
                                                                <asp:ListItem Text="1st Harvest" Value="1st Harvest" />
                                                                <asp:ListItem Text="2nd Harvest" Value="2nd Harvest" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Button ID="btnPest" runat="server" Text="Submit" CssClass="fb8" 
                         onclick="btnPest_Click" />
                    <br />
                    <br />
                    <br />
                </div>
                <div>&nbsp;
                </div>
				<div align="center" class="scroll_div">
                    <asp:DataList ID="dlWeed" runat="server" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        DataKeyField="S_WeedMIID" ForeColor="Black" GridLines="Vertical" 
                        onitemcommand="dlWeed_ItemCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Weed Management Info
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddWeed" runat="server" class="fb8_go" 
                                            CommandName="AddWeed" Text="ADD" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Weed
                                    </td>
                                    <td>
                                        Expected
                                    </td>
                                    <td>
                                        Observed
                                    </td>
                                    <td>
                                        Prevention Material
                                    </td>
                                    <td>
                                        Source
                                    </td>
                                    <td>
                                        Bill No if Purchased
                                    </td>
                                    <td>
                                        Date
                                    </td>
                                    <td>
                                        Qty Applied  in KG / HC
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtWeed" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WeedName")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWExpected" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIExpected")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWObserved" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIObserved")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWPrevention" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIPreventionMaterial")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMISource")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWBill" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIBillNo")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIDate")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIQuantity")%>'/>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnAddTWeed" runat="server" CommandName="AddTWeed" Text="ADD" class="fb8_go" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:DataList ID="dlWTransaction" runat="server" DataKeyField="S_WeedMIID" BackColor="White"
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
                                                            <asp:TextBox ID="txtWTQuantity" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIT_HC")%>'/>
                                                            <asp:HiddenField ID="hfWMITId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "WMITId")%>' />
                                                        </td>
                                                        <td>
                                                            KG / HC
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWTDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WMIT_Days")%>'/>
                                                        </td>
                                                        <td>
                                                            Days
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlWTPeriod" runat="server">
                                                                <asp:ListItem Text="BEFORE" Value="BEFORE" />
                                                                <asp:ListItem Text="AFTER" Value="AFTER" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlWTPlanting" runat="server">
                                                                <asp:ListItem Text="Planting" Value="Planting" />
                                                                <asp:ListItem Text="1st Harvest" Value="1st Harvest" />
                                                                <asp:ListItem Text="2nd Harvest" Value="2nd Harvest" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Button ID="btnWeed" runat="server" Text="Submit" CssClass="fb8" 
                        onclick="btnWeed_Click" />
                    <br />
                    <br />
                    <br />
                </div>
                <div>&nbsp;
                </div>
				<div align="center">
                    <asp:DataList ID="dlWater" runat="server" BackColor="White" 
                        BorderColor="#DEDFDE" BorderStyle="None" BorderWidth="1px" CellPadding="4" 
                        DataKeyField="S_WaterMID" ForeColor="Black" GridLines="Vertical" 
                        onitemcommand="dlWater_ItemCommand">
                        <FooterStyle BackColor="#CCCC99" />
                        <AlternatingItemStyle BackColor="White" />
                        <ItemStyle BackColor="#F7F7DE" />
                        <SelectedItemStyle BackColor="#CE5D5A" Font-Bold="True" ForeColor="White" />
                        <HeaderTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Water Information
                                    </td>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAddWater" runat="server" class="fb8_go" 
                                            CommandName="AddWater" Text="ADD" />
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
                        <ItemTemplate>
                            <table>
                                <tr>
                                    <td>
                                        Source
                                    </td>
                                    <td>
                                        If not own,whether it is from Organic Farm
                                    </td>
                                    <td>
                                       If yes,Farmer Codes for the Source
                                    </td>
                                    <td>
                                        Farmer Codes of other farmers involved in the passage of waterflow
                                    </td>
                                   
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:TextBox ID="txtWISource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WISource")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWIOrganicF" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WIOrganicF")%>' />
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWIFCSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WIFCSource")%>'/>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtWIFCWaterFlow" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WIFCWaterFlow")%>'/>
                                    </td>
                                   
                                    <td>
                                        <asp:Button ID="btnAddTWater" runat="server" CommandName="AddTWater" Text="ADD" class="fb8_go" />
                                    </td>
                                    </tr>
                                <tr>
                                    <td colspan="5">
                                        <asp:DataList ID="dlWaTransaction" runat="server" DataKeyField="S_WaterMID" BackColor="White"
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
                                                            <asp:TextBox ID="txtIrrigation" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WITT_Irrigation")%>'/>
                                                            <asp:HiddenField ID="hfWaITTId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "WITTId")%>' />
                                                        </td>
                                                        <td>
                                                            MT / HC
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="txtWaDays" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "WITT_Days")%>'/>
                                                        </td>
                                                        <td>
                                                            Days
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlWaPeriod" runat="server">
                                                                <asp:ListItem Text="BEFORE" Value="BEFORE" />
                                                                <asp:ListItem Text="AFTER" Value="AFTER" />
                                                            </asp:DropDownList>
                                                        </td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlWaPlanting" runat="server">
                                                                <asp:ListItem Text="Planting" Value="Planting" />
                                                                <asp:ListItem Text="1st Harvest" Value="1st Harvest" />
                                                                <asp:ListItem Text="2nd Harvest" Value="2nd Harvest" />
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </ItemTemplate>
                                        </asp:DataList>
                                    </td>
                                </tr>
                            </table>
                            <br />
                            <br />
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:Button ID="btnWater" runat="server" Text="Submit" CssClass="fb8" onclick="btnWater_Click" 
                        />
                    <br />
                    <br />
                    <br />
                </div>
                <div align="right"><asp:Button id="btnBack" runat="server" Text="Back" 
                        CssClass="fb8" onclick="btnBack_Click" /></div>
            </div>
  
        </asp:View>
    </asp:MultiView>

</asp:Content>

