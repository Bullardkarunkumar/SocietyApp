using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using MudarOrganic.BL;
using MudarOrganic.Components;

public partial class MudarMaster : System.Web.UI.MasterPage
{
    Buyer_BL BBL = new Buyer_BL();
    Login_BL lbl = new Login_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        
        //if (!Page.IsPostBack)
        //    Session["dtLoginDetails"] = new DataTable();
        string str = string.Empty;
        try
        {
            string reg_path = HttpContext.Current.Request.Url.AbsolutePath;
            if (reg_path == WebConfigurationManager.AppSettings["BuyerReg"].ToString())
            {
                DivMasterManage();

            }
            else if (((DataTable)Session["dtLoginDetails"]).Rows.Count > 0)
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                str = "Path : " + path;
                string[] s = path.Split('/');
                str += "    Path Count : " + s.Length.ToString();

                //if (s[2].ToString().ToLower() == "farmer")
                //{

                //}
                DivMasterManage();
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/login.aspx?error=" + ex.Message + " &new error= " + str);
        }

    }
    public void DivMasterManage()
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            Timer1.Enabled = true;
            //Timer2.Enabled = true;
            divAdmin.Visible = true;
            divBranch.Visible = false;
            divSuperAdmin.Visible = false;
            divBuyer.Visible = false;
            divSupplier.Visible = false;
        }
        else if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Branch.ToLower())
        {
            DataTable dt = Session["dtLoginDetails"] as DataTable;
            lblICS.Text = dt.Rows[0]["UserLoginID"].ToString();
            BranchOrderTrans.Enabled = true;
            divBranch.Visible = true;
            divAdmin.Visible = false;
            divSuperAdmin.Visible = false;
            divBuyer.Visible = false;
            divSupplier.Visible = false;
        }
        else if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Buyer.ToLower())
        {
            //Timer23.Enabled = true;
            DataTable dtBuyer = new DataTable();
            dtBuyer = BBL.BuyerDetails(Session["BuyerId"].ToString());
            if (dtBuyer.Rows.Count > 0)
            {
                lblBuyerName.Text = dtBuyer.Rows[0]["BuyerCompanyName"].ToString().ToUpper();
                divBranch.Visible = false;
                divAdmin.Visible = false;
                divBuyer.Visible = true;
                divSuperAdmin.Visible = false;
                divSupplier.Visible = false;
            }
           
        }
        else if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            Transaction.Enabled = true;
            divBranch.Visible = false;
            divAdmin.Visible = false;
            divBuyer.Visible = false;
            divSuperAdmin.Visible = true;
            divSupplier.Visible = false;
        }
        else if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Supplier.ToLower())
        {
            BranchOrderTrans.Enabled = true;
            DataTable dt = Session["dtLoginDetails"] as DataTable;
            if (dt.Rows.Count > 0)
            {
                DataTable dtIS = lbl.GetSupplierName(dt.Rows[0]["UserId"].ToString());
                if (dtIS.Rows.Count > 0)
                    lblIS.Text = dtIS.Rows[0]["SupplierCompanyName"].ToString();
            }
            divBranch.Visible = false;
            divAdmin.Visible = false;
            divBuyer.Visible = false;
            divSuperAdmin.Visible = false;
            divSupplier.Visible = true;
        }
        else
        {
            divBranch.Visible = false;
            divAdmin.Visible = false;
            divBuyer.Visible = false;
            divSuperAdmin.Visible = false;
            divSupplier.Visible = false;
        }
    }

    #region Branch
    protected void btnFarmerInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/Sample Farmer.aspx");
    }
    protected void btnAddPrices_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/ProductPriceUpdate.aspx");
    }
    protected void btnInternalInspection_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/FarmerInspection.aspx");
    }
    protected void btnPendingOrders_Click(object sender, EventArgs e)
    {
        Order_BL orderBL = new Order_BL();
        orderBL.updateBranchPendingOrders();
        Response.Redirect("~/Mudar/BranchOrder.aspx");
    }
    protected void btnFarmerPlantation_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/FarmerProductionInfo.aspx");
    }
    protected void btnReports_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/FarmerReports/AFL Report.aspx");
        Response.Redirect("~/FarmerReports/FarmersReports.aspx");
    }
    protected void btnTracktheLot_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/TracktheLot.aspx");
    }
    protected void btnPreorder_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/FarmerPreOrder.aspx");
    }
    protected void btnFarmsInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Farmer/FarmingInfo.aspx");
    }
    protected void btnComplaints_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/BranchComplaints.aspx");
    }
    //expose Branch controls on master page as public properties
    public void MasterControlbtnFarmerInfo()
    {
        btnFarmerInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnFarmerInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnAddPrices()
    {
        btnAddPrices.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnAddPrices.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnInternalInspection()
    {
        btnInternalInspection.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnInternalInspection.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnPendingOrders()
    {
        btnPendingOrders.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnPendingOrders.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnFarmerPlantation()
    {
        btnFarmerPlantation.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnFarmerPlantation.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnReports()
    {
        btnReports.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnReports.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnTracktheLot()
    {
        btnTracktheLot.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTracktheLot.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnPreorder()
    {
        btnPreorder.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnPreorder.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnFarmsInfo()
    {
        btnFarmsInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnFarmsInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnComplaints()
    {
        btnComplaints.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnComplaints.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    #endregion

    #region Admin
    protected void btnCatProSea_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/CategoryProductDetails.aspx");
    }
    protected void btnempbran_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/BranchsRolesEmployees.aspx");
    }
    protected void btnUnitInformation_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/UnitInformation.aspx");
    }
    protected void btnStandInfo_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Admin/PlanTimeTable.aspx");
        Response.Redirect("~/Admin/StandPage.aspx");
    }
    protected void btnTrackPO_Click(object sender, EventArgs e)
    {
        //Order_BL orderBL = new Order_BL();
        //orderBL.updateTrackPO();
        Response.Redirect("~/Admin/OrderAdmin.aspx");
    }
    protected void btnLotsSample_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/LotSample.aspx");
    }
    protected void btnAddSupplier_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Supplier/Supplier.aspx");
    }
    protected void btnCustomAgent_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/CustomAgent.aspx");
    }
    protected void btnFarmingInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/BasicFarmingInfo.aspx");
    }
    protected void btnInspectionSchedules_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/InspectionHistory.aspx");
        //Response.Redirect("~/Admin/Copy of InspectionHistory.aspx");
    }
    protected void btnFarmerApproval_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/FarmerApproval.aspx");
    }
    protected void btnTrackLot_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/TracktheLot.aspx");
    }
    protected void btnHolidaysList_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/HoildayList.aspx");
    }
    protected void btnAddBuyer_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/Buyer.aspx");
    }
	
    //expose Admin controls on master page as public properties
    public void MasterControlbtnCatProSea()
    {
        btnCatProSea.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnCatProSea.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnempbran()
    {
        btnempbran.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnempbran.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnTrackPO()
    {
        btnTrackPO.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTrackPO.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnAddSupplier()
    {
        btnAddSupplier.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnAddSupplier.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnCustomAgent()
    {
        btnCustomAgent.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnCustomAgent.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnInspectionSchedules()
    {
        btnInspectionSchedules.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnInspectionSchedules.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnFarmerApproval()
    {
        btnFarmerApproval.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnFarmerApproval.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnHolidaysList()
    {
        btnHolidaysList.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnHolidaysList.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnAddBuyer()
    {
        btnAddBuyer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnAddBuyer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnUnitInfo()
    {
        btnUnitInformation.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnUnitInformation.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnTrackLot()
    {
        btnTrackLot.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTrackLot.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnStandInfo()
    {
        btnStandInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnStandInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
   
    #endregion

    #region Buyer
    protected void btnPrices_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/BuyNewPrice.aspx");
    }
    protected void btnTrackPOBuyer_Click(object sender, EventArgs e)
    {
        Order_BL orderBL = new Order_BL();
        orderBL.UpdateBuyerOrderStatusCheck(Session["BuyerId"].ToString());
        Response.Redirect("~/Buyer/OrderHistory.aspx");
    }
    protected void btnSampleRequest_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/SampleRequest.aspx");
    }
    protected void btnProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/BuyerUpdate.aspx");
    }
    protected void btnComplaintsBuyer_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/BuyerComplaint.aspx");
    }
    protected void btnUpateLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/BuyerUpdateLoginDetails.aspx");
    }
    //expose Buyer controls on master page as public properties
    public void MasterControlbtnPrices()
    {
        btnPrices.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnPrices.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnTrackPOBuyer()
    {
        btnTrackPOBuyer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnTrackPOBuyer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnSampleRequest()
    {
        btnSampleRequest.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnSampleRequest.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnProfile()
    {
        btnProfile.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnProfile.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnComplaintsBuyer()
    {
        btnComplaintsBuyer.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnComplaintsBuyer.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnUpdateBuyerLoginDetails()
    {
        btnUpateLogin.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnUpateLogin.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    #endregion

    #region SuperAdmin
    protected void btnBuyerApproval_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/BuyerApproalDetails.aspx");
    }
    protected void btnPriceUpdate_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/ProductPriceUpdate.aspx");
    }
    protected void btnUserEdit_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/UserDetails.aspx");
    }
    protected void btnBuyerInfo_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/BuyerInfo.aspx");
    }
    protected void btnTransactions_Click(object sender, EventArgs e)
    {
        Order_BL orderBL = new Order_BL();
        orderBL.updateTrackPO();
        Response.Redirect("~/Admin/TrancationsPage.aspx");
    }
    //expose Super Admin controls on master page as public properties
    public void MasterControlbtnBuyerApproval()
    {
        btnBuyerApproval.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnBuyerApproval.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnFarmingInfo()
    {
        btnFarmingInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnFarmingInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnUserEdit()
    {
        btnUserEdit.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnUserEdit.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnPriceUpdate()
    {
        btnPriceUpdate.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnPriceUpdate.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnBuyerInfo()
    {
        btnBuyerInfo.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnBuyerInfo.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterControlbtnAvailableQuantity()
    {
        btnCheckData.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnCheckData.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    public void MasterSuperAdminOrders()
    {
        btnSuperOrders.BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
        btnSuperOrders.ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
    }
    #endregion

    #region comment code
    //protected void btnActualYields_Click(object sender, EventArgs e)
    //{

    //}
    //protected void btnPlantationPlan_Click(object sender, EventArgs e)
    //{

    //}
    //protected void ibtnProductPriceDetails_Click(object sender, ImageClickEventArgs e)
    //{

    //    //if (ibtnProductPriceDetails.ImageUrl == "~/images/collapse.JPG")
    //    //{
    //    //    ibtnProductPriceDetails.ImageUrl = "~/images/expand.JPG";
    //    //    divProductPriceDet.Visible = false;
    //    //}
    //    //else if (ibtnProductPriceDetails.ImageUrl == "~/images/expand.JPG")
    //    //{
    //    //    ibtnProductPriceDetails.ImageUrl = "~/images/collapse.JPG";
    //    //    divProductPriceDet.Visible = true;
    //    //}

    //}
    //protected void btnAddNewFarmer_Click(object sender, EventArgs e)
    //{

    //} 
    #endregion

    #region Supplier
    protected void btnSupplierTrackPO_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Supplier/SupplierOrder.aspx");
    }
    protected void btnSupplierProfile_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Supplier/SupplierView.aspx?sid={" + Session["SupplierID"].ToString() + "}");
    }
    #endregion

    /* Any Orders for Admin */
    protected void Timer1_Tick(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            Order_BL order = new Order_BL();
            bool result = order.NewOrderArrived();
            if (result)
            {
                btnTrackPO.Style.Add("background-color", "Blue");
            }
        }
    }
   /* Any Lotsample for Admin */
    protected void Timer2_Tick(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        {
            Order_BL order = new Order_BL();
            bool result = order.NewLotSampleArrived();
            if (result)
            {
                btnLotsSample.Style.Add("background-color", "Blue");
            }
        }
    }
   /* Any Transactions for Superadmin */
    protected void Transaction_Tick(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.SuperAdmin.ToLower())
        {
            Order_BL order = new Order_BL();
            bool result = order.NewOrderArrived();
            if (result)
            {
                btnTransactions.Style.Add("background-color", "Blue");
            }
            bool check = order.NewBranchOrderArrived();
            if (check)
            {
                btnTransactions.Style.Add("background-color", "Blue");
            }
        }
    }
    /*  Any Orders for Branch */
    protected void BranchOrderTrans_Tick(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Supplier.ToLower())
        {
            Order_BL order = new Order_BL();
            bool result = order.NewBranchOrderArrived();
            if (result)
            {
                btnPendingOrders.Style.Add("background-color", "Blue");
            }
        }
    }
    protected void Timer23_Tick(object sender, EventArgs e)
    {
        if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Buyer.ToLower())
        {
            Order_BL order = new Order_BL();
            bool result = order.BuyerOrderStatusCheck(Session["BuyerId"].ToString());
            if (result)
            {
                btnTrackPOBuyer.Style.Add("background-color", "Blue");
            }
        }
    }
    protected void btnCheckData_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/AvailableQty.aspx");
    }
    protected void btnSampleReq_Click(object sender, EventArgs e)
    {
        //Response.Redirect("~/Admin/Copy of InspectionHistory.aspx");
        Response.Redirect("~/Admin/SampleReq.aspx");
    }
    protected void btnSoldQty_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/ProduceList.aspx");
    }
    protected void btnSuperOrders_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Admin/SuperAdminOrders.aspx");
    }
    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Mudar/vidhyareports.aspx");
    }
}
