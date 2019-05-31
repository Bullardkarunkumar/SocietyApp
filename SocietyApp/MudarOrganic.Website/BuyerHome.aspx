<%@ Page Title="Mudarorganic-BuyerHome" Language="C#" MasterPageFile="~/MudarMasterNew.master" AutoEventWireup="true" CodeFile="BuyerHome.aspx.cs" Inherits="BuyerHome" %>


<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="header_Text" class="page-title">
        <h1>Welcome to Buyer Page</h1>
    </div>
    <div class="row" style="text-align: center;margin-bottom:50px">
        <asp:Image ID="imgBuyerLogo" runat="server" Height="135px" Width="200px" ImageUrl="~/images/MUDAR LOGO.jpg" />
    </div>
    <div class="col-sm-12" style="margin-bottom:40px">
        <div class="col-sm-2"></div>
        <div class="col-sm-8">
            <div id="myCarousel" class="carousel slide" data-ride="carousel">
                <!-- Indicators -->
                <ol class="carousel-indicators">
                    <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
                    <li data-target="#myCarousel" data-slide-to="1"></li>
                    <li data-target="#myCarousel" data-slide-to="2"></li>
                </ol>

                <!-- Wrapper for slides -->
                <div class="carousel-inner">
                    <div class="item active" id="divIndian" runat="server" style="margin-left: 40%">
                        <a href="Attachments/Mudar India Exports NPOP processing Scope certificate.pdf" target="_blank">
                            <asp:Image ID="imgIndian" runat="server" ImageUrl="~/images/IndianOrganic.jpg" Height="135px" Width="200px" /></a>
                    </div>
                    <div class="item active" id="divEuropean" runat="server" style="margin-left: 40%">
                        <a href="Attachments/EU Scope certificate 2015.pdf" target="_blank">
                            <asp:Image ID="imgEuropean" runat="server" ImageUrl="~/images/eu-organic-logo.jpg" Height="135px" Width="200px" /></a>
                    </div>

                    <div class="item" style="margin-left: 40%">
                        <a href="Attachments/Mudar India Exports NOP processing Scope certificate.pdf" target="_blank">
                            <asp:Image ID="imgUSA" runat="server" ImageUrl="~/images/USDAOrganic.jpg" Height="135px" Width="200px" />
                        </a>
                    </div>

                    <div class="item" style="margin-left: 40%">
                        <a href="Attachments/Fair for Life certificate.pdf" target="_blank">
                            <asp:Image ID="imgFair" runat="server" ImageUrl="~/images/FairOrganic.jpg" Height="135px" Width="200px" /></a>
                    </div>
                </div>

                <!-- Left and right controls -->
                <a class="left carousel-control" href="#myCarousel" data-slide="prev">
                    <span class="glyphicon glyphicon-chevron-left"></span>
                    <span class="sr-only">Previous</span>
                </a>
                <a class="right carousel-control" href="#myCarousel" data-slide="next">
                    <span class="glyphicon glyphicon-chevron-right"></span>
                    <span class="sr-only">Next</span>
                </a>
            </div>
        </div>
        <div class="col-sm-2"></div>
    </div>

</asp:Content>

