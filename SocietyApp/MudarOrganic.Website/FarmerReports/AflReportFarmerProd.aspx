<%@ Page Title="" Language="C#" MasterPageFile="~/MudarMaster.master" AutoEventWireup="true" CodeFile="AflReportFarmerProd.aspx.cs" Inherits="FarmerReports_AflReportFarmerProd" %>

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
                    <td align="right">
                        <asp:Button ID="btnPF" runat="server" Text="Print In Excel" CssClass="fb8" OnClick="btnPF_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <div id="bill">
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
                        <asp:BoundField DataField="PlantationDate" HeaderText="PlantationDate" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="FirstHarvestDate" HeaderText="I Cut Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationFHerbaga" HeaderText="Herbaga in MT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstHerbaga" HeaderText="Actual herb" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstDistillationDate" HeaderText="date of distillation" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstUnitId" HeaderText="distillation code" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationFProductQuantity" HeaderText="Qty of Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstProductQuantity" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SecondHarvestDate" HeaderText="II Cut Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationSHerbaga" HeaderText="Herbaga in MT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondHerbaga" HeaderText="Actual herb" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondDistillationDate" HeaderText="date of distillation" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondUnitId" HeaderText="distillation code" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationSProductQuantity" HeaderText="Qty of Oil" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondProductQuantity" HeaderText="Actual Oil" ItemStyle-HorizontalAlign="Center" />

                         <asp:BoundField DataField="PlantationDate1" HeaderText="PlantationDate" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="FirstHarvestDate1" HeaderText="I Cut Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationFHerbaga1" HeaderText="Herbaga in MT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstHerbaga1" HeaderText="Actual herb" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstDistillationDate1" HeaderText="date of distillation" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstUnitId1" HeaderText="distillation code" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationFProductQuantity1" HeaderText="Qty of Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstProductQuantity1" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SecondHarvestDate1" HeaderText="II Cut Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationSHerbaga1" HeaderText="Herbaga in MT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondHerbaga1" HeaderText="Actual herb" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondDistillationDate1" HeaderText="date of distillation" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondUnitId1" HeaderText="distillation code" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationSProductQuantity1" HeaderText="Qty of Oil" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondProductQuantity1" HeaderText="Actual Oil" ItemStyle-HorizontalAlign="Center" />

                        <asp:BoundField DataField="PlantationDate2" HeaderText="PlantationDate" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="FirstHarvestDate2" HeaderText="I Cut Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationFHerbaga2" HeaderText="Herbaga in MT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstHerbaga2" HeaderText="Actual herb" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstDistillationDate2" HeaderText="date of distillation" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstUnitId2" HeaderText="distillation code" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationFProductQuantity2" HeaderText="Qty of Oil(KG)" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="FirstProductQuantity2" HeaderText="Actual Oil(KG)" ItemStyle-HorizontalAlign="Center"/>
                        <asp:BoundField DataField="SecondHarvestDate2" HeaderText="II Cut Date" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationSHerbaga2" HeaderText="Herbaga in MT" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondHerbaga2" HeaderText="Actual herb" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondDistillationDate2" HeaderText="date of distillation" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondUnitId2" HeaderText="distillation code" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="EstimationSProductQuantity2" HeaderText="Qty of Oil" ItemStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="SecondProductQuantity2" HeaderText="Actual Oil" ItemStyle-HorizontalAlign="Center" />
                      
                        <asp:BoundField DataField="VC" HeaderText="Est.Qty (MT)" ItemStyle-HorizontalAlign="Center" />
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
            <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="fb8" PostBackUrl="~/FarmerReports/FarmersReports.aspx" />
        </div>
    </div>
</asp:Content>

