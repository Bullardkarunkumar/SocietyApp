<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="AFLFarmerTotalProd.aspx.cs" Inherits="FarmerReports_AFLFarmerTotalProd" %>

<asp:Content ID="Content1" ContentPlaceHolderID="body_cph" runat="Server">

    <script language="javascript" type="text/javascript">
        function CallPrint(strid) {
            var prtContent = document.getElementById(strid);
            var WinPrint = window.open('', '', 'letf=0,top=0,width=800,height=100,toolbar=0,scrollbars=0,status=0,dir=ltr');
            WinPrint.document.write(prtContent.innerHTML);
            WinPrint.document.close();
            WinPrint.focus();
            WinPrint.print();
            WinPrint.close();
            prtContent.innerHTML = strOldOne;
        }
    </script>

    <div id="content_area_Home" style="height: auto">
        <div id="header_Text">
            AFL Documnet</div>
        <div align="center">
            <table width="100%">
                <tr>
                    <td align="left">
                        <asp:Button ID="btnsubmit" runat="server" Text="Print Page" CssClass="fb8" OnClientClick="javascript:CallPrint('bill');" />
                    </td>
                    <td align="center">
                          Year<br/>
                    <asp:DropDownList ID="ddlYear" runat="server" AutoPostBack="true" Height="35px"
                        Width="150px" Font-Size="Medium" TabIndex="9" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged">
                    </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Button ID="btnPF" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnPF_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="bill" runat="server" visible="false">
            <div align="center" style="font-size: 16px; background-color: #CCCC99">
                Mudar India Exports
                <br />
                AFL Report - 2<br />
            </div>
            <div>
                <asp:GridView ID="gvFarmerList" runat="server" AutoGenerateColumns="False"
                    Font-Size="8px" OnRowCreated="gvFarmerList_RowCreated" OnRowDataBound="gvFarmerList_RowDataBound"
                    OnDataBound="gvFarmerList_DataBound" GridLines="Both">
                    <Columns>
                        <asp:BoundField DataField="FirstName" HeaderText="Farmer Name" />
                        <asp:BoundField DataField="City_Village" HeaderText="Village" />
                        <asp:BoundField DataField="FarmerAPEDACode" HeaderText="Farmer Tracenet No" />
                        <asp:BoundField DataField="TotalAreaInHectares" HeaderText="Total Organic Area(Ha)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PA" HeaderText="Organic Plot Area(Ha)" ItemStyle-HorizontalAlign="Center"/>
                        
                        <asp:BoundField DataField="CA" HeaderText="Crop Area (Ha)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PlantationDate" HeaderText="Plantation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="FirstHarvestDate" HeaderText="I Cut Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}"  />
                        <asp:BoundField DataField="EstimationFHerbaga" HeaderText="Herbaga(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstHerbaga" HeaderText="Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstDistillationDate" HeaderText="Distillation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}"  />
                        <asp:BoundField DataField="code1" HeaderText="Distillation Unit" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="EstimationFProductQuantity" HeaderText="Qty Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstProductQuantity" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SecondHarvestDate" HeaderText="II Cut Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="EstimationSHerbaga" HeaderText="Herbaga(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondHerbaga" HeaderText="Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondDistillationDate" HeaderText="Distillation Date" ItemStyle-HorizontalAlign="Center"  DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="code1" HeaderText="Distillation Unit" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="EstimationSProductQuantity" HeaderText="Qty Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondProductQuantity" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="TotalProductQuantity" HeaderText="Total Oil" ItemStyle-HorizontalAlign="Center"/>
                        
                        <asp:BoundField DataField="CA1" HeaderText="Crop Area (Ha)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="PD" HeaderText="Plantation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="FHD" HeaderText="I Cut Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="EFH" HeaderText="Herbaga(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FH" HeaderText="Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FDD" HeaderText="Distillation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="code1" HeaderText="Distillation Unit" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="EFPQ" HeaderText="Qty Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FPQ" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SHD" HeaderText="II Cut Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="ESH" HeaderText="Herbaga(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SH" HeaderText="Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SDD" HeaderText="Distillation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="code1" HeaderText="Distillation Unit" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="ESPQ" HeaderText="Qty Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SPQ" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="TPQ" HeaderText="Total Oil" ItemStyle-HorizontalAlign="Center"/>
                        
                        <asp:BoundField DataField="CA2" HeaderText="Crop Area (Ha)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="PD2" HeaderText="Plantation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="FHD2" HeaderText="I Cut Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="EFH2" HeaderText="Herbaga(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FH2" HeaderText="Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FDD2" HeaderText="Distillation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="code2" HeaderText="Distillation Unit" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="EFPQ2" HeaderText="Qty Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FPQ2" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SHD2" HeaderText="II Cut Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="ESH2" HeaderText="Herbaga(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SH2" HeaderText="Actual Herb(MT)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SDD2" HeaderText="Distillation Date" ItemStyle-HorizontalAlign="Center" DataFormatString="{0:dd MMM yyyy}" />
                        <asp:BoundField DataField="code2" HeaderText="Distillation Unit" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="ESPQ2" HeaderText="Qty Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SPQ2" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="TPQ2" HeaderText="Total Oil" ItemStyle-HorizontalAlign="Center"/>

                        <asp:BoundField DataField="VC" HeaderText="Vacant land/Fallow" ItemStyle-HorizontalAlign="Center" />
                    </Columns>
                    <AlternatingRowStyle BackColor="#e6e3e3" />
                    <EditRowStyle BackColor="#7C6F57" />
                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="#66CCFF" Font-Bold="True" ForeColor="Black" />
                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                    <RowStyle BackColor="#CCCC99" />
                </asp:GridView>
            </div>
        </div>
         <div align="center">
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" Visible="false" PostBackUrl="~/FarmerReports/FarmersReports.aspx" />
        </div>
    </div>
</asp:Content>

