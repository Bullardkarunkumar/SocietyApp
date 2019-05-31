using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using MudarOrganic.Components;


public partial class Farmer_FarmerPlantation : System.Web.UI.Page
{
    CategoryProduct_BL cp = new CategoryProduct_BL();
    Product_BL pObj = new Product_BL();
    FarmPlantation_BL fp = new FarmPlantation_BL();
    Farmer_BL far = new Farmer_BL();
    Farming_BL farmingObj = new Farming_BL();
    MudarApp objMudarApp = new MudarApp();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnFarmerPlantation();
			ListItemCollection items = MudarApp.BindYear();
            foreach (ListItem item in items)
            {
                ddlFarmerYear.Items.Add(item);
                ddlAllFarmerYear.Items.Add(item);
                ddlFPYear.Items.Add(item);
            }
            ddlUnitDetails.DataSource = fp.BindDropDownChild();
            ddlUnitDetails.DataTextField = "Name";
            ddlUnitDetails.DataValueField = "UnitId";
            ddlUnitDetails.DataBind();
            BindddlCultivation(1);
            gvFarmerList.DataSource = far.FamerDetails();
            gvFarmerList.DataBind();
        }
    }
    protected void gvFarmerList_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        string command = e.CommandName;
        switch (command)
        {
            case "Production":
                {
                    txtFarmerSearch.Text = gvFarmerList.DataKeys[index].Values[1].ToString();
                    MultiView1.ActiveViewIndex = 0;
                    //code = gvFarmer.DataKeys[index].Values[1].ToString();//gvFarmer.Rows[index].Cells[0].Text;
                    //Response.Redirect("~/Farmer/NewFarmer.aspx?NewFarmer=2&FarmerCode=" + code);
                }
                break;
            case "AddCrop":
                {
                    txtFPFarmerN.Text = gvFarmerList.DataKeys[index].Values[0].ToString();
                    MultiView1.ActiveViewIndex = 1;
                    //code = gvFarmer.DataKeys[index].Values[1].ToString(); //gvFarmer.Rows[index].Cells[0].Text;
                    //Response.Redirect("~/Farmer/FarmerView.aspx?FarmerCode=" + code + "&BackUrl=~/Farmer/Farmers.aspx");
                }
                break;
        }
    }
    protected void gvFarmerList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowIndex > -1)
        {
            if (e.Row.Cells[5].Text.ToUpper().Trim() == "FALSE")
            {
                e.Row.Cells[5].Text = "Org & FT";
            }
            else
                e.Row.Cells[5].Text = "Organic";
        }

    }
    private void BindddlCultivation(int Case)
    {
        if (Case == 1)
        {
            ddlCultivation.Items.Add("ALL");
            ddlCultivation.Items.Add("I Cut");
            ddlCultivation.Items.Add("II Cut");
            ddlCultivation.DataBind();
            ddlCultivation.Items[0].Selected = true;

        }
    }

    #region FarmerPlantation
    
    //
    //protected void ddlFPSeason_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if (ddlFPSeason.SelectedIndex > 0)
    //    {
    //        BindFPProductDetails(Convert.ToInt32(ddlFPSeason.SelectedValue));
    //    }
    //}
    //private void BindFPProductDetails(int SeasonID)
    //{
    //    ddlFPProduct.DataSource = pObj.GetProductDetailsbySeason(SeasonID);
    //    ddlFPProduct.DataTextField = "ProductName";
    //    ddlFPProduct.DataValueField = "ProductId";
    //    ddlFPProduct.DataBind();
    //    ddlFPProduct.Items.Insert(0, MudarApp.AddListItem());
    //}
    //protected void ddlFPProduct_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    BindgvFPDetails();
    //}
    //private void BindgvFPDetails()
    //{
    //    string farmercode = !string.IsNullOrEmpty(txtFPFarmerN.Text) ? (txtFPFarmerN.Text.Split('-'))[1].ToString().Trim() : string.Empty;
    //    int year = Convert.ToInt32(ddlFPYear.SelectedValue.ToString());
    //    int seasonid = Convert.ToInt32(ddlFPSeason.SelectedValue.ToString());
    //    int productid = Convert.ToInt32(ddlFPProduct.SelectedValue.ToString());
    //DataTable dtTemp = fp.BuildPlantation(year, seasonid, productid, farmercode);
    //    gvFP.DataSource = dtTemp.DefaultView;
    //    gvFP.DataBind();

    //}
    #endregion

    protected void ddlFarmerYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFarmerYear.SelectedIndex > 0)
        {
            BindSeasonDetailsList();
        }

    }
    private void BindSeasonDetailsList()
    {

        ddlFarmerSeason.DataSource = cp.GetSeasonDetails();
        ddlFarmerSeason.DataTextField = "SeasonName";
        ddlFarmerSeason.DataValueField = "SeasonID";
        ddlFarmerSeason.DataBind();

        ddlFarmerSeason.Items.Insert(0, MudarApp.AddListItem());
    }

    protected void ddlFarmerSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFarmerSeason.SelectedIndex > 0)
        {
            BindProductDetails(Convert.ToInt32(ddlFarmerSeason.SelectedValue));
        }
    }
    private void BindProductDetails(int SeasonID)
    {
        ddlFarmerProduct.DataSource = pObj.GetProductDetailsbySeason(SeasonID);
        ddlFarmerProduct.DataTextField = "ProductName";
        ddlFarmerProduct.DataValueField = "ProductId";
        ddlFarmerProduct.DataBind();
        ddlFarmerProduct.Items.Insert(0, MudarApp.AddListItem());
    }
    private void BindAllProductDetails(int SeasonID)
    {
        ddlAllFarmerProduct.DataSource = pObj.GetProductDetailsbySeason(SeasonID);
        ddlAllFarmerProduct.DataTextField = "ProductName";
        ddlAllFarmerProduct.DataValueField = "ProductId";
        ddlAllFarmerProduct.DataBind();
        ListItem item = new ListItem();
        item.Text = "ALL";
        item.Value = "0";
        ddlAllFarmerProduct.Items.Insert(0, item);
    }
    private void BindAllSeasonDetailsList()
    {

        ddlAllFarmerSeason.DataSource = cp.GetSeasonDetails();
        ddlAllFarmerSeason.DataTextField = "SeasonName";
        ddlAllFarmerSeason.DataValueField = "SeasonID";
        ddlAllFarmerSeason.DataBind();

        ddlAllFarmerSeason.Items.Insert(0, MudarApp.AddListItem());
    }
    protected void ddlAllFarmerYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAllFarmerYear.SelectedIndex > 0)
        {
            BindAllSeasonDetailsList();
        }
        else
            ddlAllFarmerSeason.Items.Clear();
    }
    protected void ddlAllFarmerSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlAllFarmerSeason.SelectedIndex > 0)
            BindAllProductDetails(Convert.ToInt32(ddlAllFarmerSeason.SelectedValue));
        else
            ddlAllFarmerProduct.Items.Clear();
    }
    protected void MainMenu_MenuItemClick(object sender, MenuEventArgs e)
    {
        //if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        //{
        MultiView1.ActiveViewIndex = Convert.ToInt32(MainMenu.SelectedValue);
        //}
        //else if (Session["RoleName_s"].ToString().Trim().ToLower() == LoginType.Branch.ToLower())
        //{
        //    MultiView1.ActiveViewIndex = 0;
        //}
        //if (Request.QueryString["Plantation"].ToString() == "0")

        //else if (Request.QueryString["Plantation"].ToString() == "1")

        //else
        //    MultiView1.ActiveViewIndex = 2;
        //MultiView1.ActiveViewIndex = Convert.ToInt32(MainMenu.SelectedValue);
    }


    protected void btnPlan_Click(object sender, EventArgs e)
    {
        //BindNEWgvAllfarmer();
        BindgvALLFarmer();
        if (gvAllFarmer.Rows.Count <= 0)
            BindNEWgvAllfarmer();
    }

    private void BindNEWgvAllfarmer()
    {
        DataTable dtPlantation = fp.BuildNEWPlantation(Convert.ToInt32(ddlAllFarmerYear.SelectedItem.Text), Convert.ToInt32(ddlAllFarmerSeason.SelectedValue), Convert.ToInt32(ddlAllFarmerProduct.SelectedValue));
        DataTable dtproduct = pObj.GetProductDetailsbySeason(Convert.ToInt32(ddlAllFarmerSeason.SelectedValue));
        List<string> datakey = new List<string>();

        if (ddlAllFarmerProduct.SelectedIndex > 0)
            BindColumngv(0, dtPlantation, ref dtproduct, ref datakey);
        else
            BindColumngv(dtproduct.Rows.Count, dtPlantation, ref dtproduct, ref datakey);
        gvAllFarmer.DataSource = dtPlantation;
        gvAllFarmer.DataKeyNames = datakey.ToArray<string>();
        gvAllFarmer.DataBind();
    }

    private void BindgvALLFarmer()
    {
        DataTable dtPlantation = fp.BuildPlantation(Convert.ToInt32(ddlAllFarmerYear.SelectedItem.Text), Convert.ToInt32(ddlAllFarmerSeason.SelectedValue), Convert.ToInt32(ddlAllFarmerProduct.SelectedValue));
        DataTable dtproduct = pObj.GetProductDetailsbySeason(Convert.ToInt32(ddlAllFarmerSeason.SelectedValue));
        List<string> datakey = new List<string>();

        if (ddlAllFarmerProduct.SelectedIndex > 0)
            BindColumngv(0, dtPlantation, ref dtproduct, ref datakey);
        else
            BindColumngv(dtproduct.Rows.Count, dtPlantation, ref dtproduct, ref datakey);
        gvAllFarmer.DataSource = dtPlantation;
        gvAllFarmer.DataKeyNames = datakey.ToArray<string>();
        gvAllFarmer.DataBind();
    }

    private void BindColumngv(int productcount, DataTable dt, ref DataTable dtproduct, ref List<string> datakey)
    {
        gvAllFarmer.Columns.Clear();

        BoundField bfFarmerId = new BoundField();
        bfFarmerId.DataField = dt.Columns["FarmerId"].ColumnName;
        bfFarmerId.HeaderText = "FarmerId";
        bfFarmerId.Visible = false;
        datakey.Add("FarmerId");
        gvAllFarmer.Columns.Add(bfFarmerId);

        BoundField bfFarmerCode = new BoundField();
        bfFarmerCode.DataField = dt.Columns["FarmerCode"].ColumnName;
        bfFarmerCode.HeaderText = "Farmer Code";
        gvAllFarmer.Columns.Add(bfFarmerCode);

        BoundField bfFirstName = new BoundField();
        bfFirstName.DataField = dt.Columns["FirstName"].ColumnName;
        bfFirstName.HeaderText = "Farmer Name";
        gvAllFarmer.Columns.Add(bfFirstName);

        BoundField bfFarmerRegNumber = new BoundField();
        bfFarmerRegNumber.DataField = dt.Columns["FarmerRegNumber"].ColumnName;
        bfFarmerRegNumber.HeaderText = "Farmer Reg Number";
        gvAllFarmer.Columns.Add(bfFarmerRegNumber);

        BoundField bfTotalAreaInHectares = new BoundField();
        bfTotalAreaInHectares.DataField = dt.Columns["TotalAreaInHectares"].ColumnName;
        bfTotalAreaInHectares.HeaderText = "Total Area";
        gvAllFarmer.Columns.Add(bfTotalAreaInHectares);

        BoundField bfFarmID = new BoundField();
        bfFarmID.DataField = dt.Columns["FarmID"].ColumnName;
        bfFarmID.HeaderText = "FarmID";
        bfFarmID.Visible = false;
        datakey.Add("FarmID");
        gvAllFarmer.Columns.Add(bfFarmID);

        BoundField bfAreaCode = new BoundField();
        bfAreaCode.DataField = dt.Columns["AreaCode"].ColumnName;
        bfAreaCode.HeaderText = "Area Code";
        gvAllFarmer.Columns.Add(bfAreaCode);

        BoundField bfPlotArea = new BoundField();
        bfPlotArea.DataField = dt.Columns["PlotArea"].ColumnName;
        bfPlotArea.HeaderText = "Plot Area";
        gvAllFarmer.Columns.Add(bfPlotArea);

        BoundField bfSeasonID = new BoundField();
        bfSeasonID.DataField = dt.Columns["SeasonID"].ColumnName;
        bfSeasonID.HeaderText = "SeasonID";
        bfSeasonID.Visible = false;
        datakey.Add("SeasonID");
        gvAllFarmer.Columns.Add(bfSeasonID);

        #region single Product
        if (productcount == 0)
        {
            BoundField bfProductId = new BoundField();
            bfProductId.DataField = dt.Columns["ProductId"].ColumnName;
            bfProductId.HeaderText = "ProductId";
            bfProductId.Visible = false;
            datakey.Add("ProductId");
            gvAllFarmer.Columns.Add(bfProductId);

            BoundField bfPlantationId = new BoundField();
            bfPlantationId.DataField = dt.Columns["PlantationId"].ColumnName;
            bfPlantationId.HeaderText = "PlantationId";
            bfPlantationId.Visible = false;
            datakey.Add("PlantationId");
            gvAllFarmer.Columns.Add(bfPlantationId);

            BoundField bfPlantationArea = new BoundField();
            bfPlantationArea.DataField = dt.Columns["PlantationArea"].ColumnName;
            bfPlantationArea.HeaderText = "Plantation Area";
            gvAllFarmer.Columns.Add(bfPlantationArea);

            BoundField bfPlantationDate = new BoundField();
            bfPlantationDate.DataField = dt.Columns["PlantationDate"].ColumnName;
            bfPlantationDate.HeaderText = "Plantation Date";
            gvAllFarmer.Columns.Add(bfPlantationDate);

            BoundField bfFirstHarvestDate = new BoundField();
            bfFirstHarvestDate.DataField = dt.Columns["FirstHarvestDate"].ColumnName;
            bfFirstHarvestDate.HeaderText = "I cut Date";
            gvAllFarmer.Columns.Add(bfFirstHarvestDate);

            BoundField bfFirstHerbaga = new BoundField();
            bfFirstHerbaga.DataField = dt.Columns["FirstHerbaga"].ColumnName;
            bfFirstHerbaga.HeaderText = "I Herbaga";
            gvAllFarmer.Columns.Add(bfFirstHerbaga);

            BoundField bfFirstDistillationDate = new BoundField();
            bfFirstDistillationDate.DataField = dt.Columns["FirstDistillationDate"].ColumnName;
            bfFirstDistillationDate.HeaderText = "Distillation Date";
            gvAllFarmer.Columns.Add(bfFirstDistillationDate);

            BoundField bfFirstDistillationUnitNO = new BoundField();
            bfFirstDistillationUnitNO.DataField = dt.Columns["FirstDistillationUnitNO"].ColumnName;
            bfFirstDistillationUnitNO.HeaderText = "Distillation Unit";
            gvAllFarmer.Columns.Add(bfFirstDistillationUnitNO);

            BoundField bfFirstProductQuantity = new BoundField();
            bfFirstProductQuantity.DataField = dt.Columns["FirstProductQuantity"].ColumnName;
            bfFirstProductQuantity.HeaderText = "Quantity";
            gvAllFarmer.Columns.Add(bfFirstProductQuantity);

            BoundField bfSecondHarvestDate = new BoundField();
            bfSecondHarvestDate.DataField = dt.Columns["SecondHarvestDate"].ColumnName;
            bfSecondHarvestDate.HeaderText = "II Cut Date";
            gvAllFarmer.Columns.Add(bfSecondHarvestDate);

            BoundField bfSecondHerbaga = new BoundField();
            bfSecondHerbaga.DataField = dt.Columns["SecondHerbaga"].ColumnName;
            bfSecondHerbaga.HeaderText = "II Herbaga";
            gvAllFarmer.Columns.Add(bfSecondHerbaga);

            BoundField bfSecondDistillationDate = new BoundField();
            bfSecondDistillationDate.DataField = dt.Columns["SecondDistillationDate"].ColumnName;
            bfSecondDistillationDate.HeaderText = "Distillation Date";
            gvAllFarmer.Columns.Add(bfSecondDistillationDate);

            BoundField bfSecondDistillationUnitNO = new BoundField();
            bfSecondDistillationUnitNO.DataField = dt.Columns["SecondDistillationUnitNO"].ColumnName;
            bfSecondDistillationUnitNO.HeaderText = "Distillation Unit";
            gvAllFarmer.Columns.Add(bfSecondDistillationUnitNO);

            BoundField bfSecondProductQuantity = new BoundField();
            bfSecondProductQuantity.DataField = dt.Columns["SecondProductQuantity"].ColumnName;
            bfSecondProductQuantity.HeaderText = "Quantity";
            gvAllFarmer.Columns.Add(bfSecondProductQuantity);

            BoundField bfTotalProductQuantity = new BoundField();
            bfTotalProductQuantity.DataField = dt.Columns["TotalProductQuantity"].ColumnName;
            bfTotalProductQuantity.HeaderText = "Total Quantity";
            gvAllFarmer.Columns.Add(bfTotalProductQuantity);
        }
        #endregion

        #region Multi Product
        if (productcount > 0)
        {
            foreach (DataRow dr in dtproduct.Rows)
            {
                BoundField bfProductId = new BoundField();
                bfProductId.DataField = dt.Columns["ProductId" + "_" + dr["ProductId"]].ColumnName;
                bfProductId.HeaderText = "ProductId";
                bfProductId.Visible = false;
                datakey.Add("ProductId" + "_" + dr["ProductId"]);
                gvAllFarmer.Columns.Add(bfProductId);

                BoundField bfPlantationId = new BoundField();
                bfPlantationId.DataField = dt.Columns["PlantationId" + "_" + dr["ProductId"]].ColumnName;
                bfPlantationId.HeaderText = "PlantationId";
                bfPlantationId.Visible = false;
                datakey.Add("PlantationId" + "_" + dr["ProductId"]);
                gvAllFarmer.Columns.Add(bfPlantationId);

                BoundField bfPlantationArea = new BoundField();
                bfPlantationArea.DataField = dt.Columns["PlantationArea" + "_" + dr["ProductId"]].ColumnName;
                bfPlantationArea.HeaderText = "Plantation Area";
                gvAllFarmer.Columns.Add(bfPlantationArea);

                BoundField bfPlantationDate = new BoundField();
                bfPlantationDate.DataField = dt.Columns["PlantationDate" + "_" + dr["ProductId"]].ColumnName;
                bfPlantationDate.HeaderText = "Plantation Date";
                gvAllFarmer.Columns.Add(bfPlantationDate);

                BoundField bfFirstHarvestDate = new BoundField();
                bfFirstHarvestDate.DataField = dt.Columns["FirstHarvestDate" + "_" + dr["ProductId"]].ColumnName;
                bfFirstHarvestDate.HeaderText = "I cut Date";
                gvAllFarmer.Columns.Add(bfFirstHarvestDate);

                BoundField bfFirstHerbaga = new BoundField();
                bfFirstHerbaga.DataField = dt.Columns["FirstHerbaga" + "_" + dr["ProductId"]].ColumnName;
                bfFirstHerbaga.HeaderText = "I Herbaga";
                gvAllFarmer.Columns.Add(bfFirstHerbaga);

                BoundField bfFirstDistillationDate = new BoundField();
                bfFirstDistillationDate.DataField = dt.Columns["FirstDistillationDate" + "_" + dr["ProductId"]].ColumnName;
                bfFirstDistillationDate.HeaderText = "Distillation Date";
                gvAllFarmer.Columns.Add(bfFirstDistillationDate);

                BoundField bfFirstDistillationUnitNO = new BoundField();
                bfFirstDistillationUnitNO.DataField = dt.Columns["FirstDistillationUnitNO" + "_" + dr["ProductId"]].ColumnName;
                bfFirstDistillationUnitNO.HeaderText = "Distillation Unit";
                gvAllFarmer.Columns.Add(bfFirstDistillationUnitNO);

                BoundField bfFirstProductQuantity = new BoundField();
                bfFirstProductQuantity.DataField = dt.Columns["FirstProductQuantity" + "_" + dr["ProductId"]].ColumnName;
                bfFirstProductQuantity.HeaderText = "Quantity";
                gvAllFarmer.Columns.Add(bfFirstProductQuantity);

                BoundField bfSecondHarvestDate = new BoundField();
                bfSecondHarvestDate.DataField = dt.Columns["SecondHarvestDate" + "_" + dr["ProductId"]].ColumnName;
                bfSecondHarvestDate.HeaderText = "II Cut Date";
                gvAllFarmer.Columns.Add(bfSecondHarvestDate);

                BoundField bfSecondHerbaga = new BoundField();
                bfSecondHerbaga.DataField = dt.Columns["SecondHerbaga" + "_" + dr["ProductId"]].ColumnName;
                bfSecondHerbaga.HeaderText = "II Herbaga";
                gvAllFarmer.Columns.Add(bfSecondHerbaga);

                BoundField bfSecondDistillationDate = new BoundField();
                bfSecondDistillationDate.DataField = dt.Columns["SecondDistillationDate" + "_" + dr["ProductId"]].ColumnName;
                bfSecondDistillationDate.HeaderText = "Distillation Date";
                gvAllFarmer.Columns.Add(bfSecondDistillationDate);

                BoundField bfSecondDistillationUnitNO = new BoundField();
                bfSecondDistillationUnitNO.DataField = dt.Columns["SecondDistillationUnitNO" + "_" + dr["ProductId"]].ColumnName;
                bfSecondDistillationUnitNO.HeaderText = "Distillation Unit";
                gvAllFarmer.Columns.Add(bfSecondDistillationUnitNO);

                BoundField bfSecondProductQuantity = new BoundField();
                bfSecondProductQuantity.DataField = dt.Columns["SecondProductQuantity" + "_" + dr["ProductId"]].ColumnName;
                bfSecondProductQuantity.HeaderText = "Quantity";
                gvAllFarmer.Columns.Add(bfSecondProductQuantity);

                BoundField bfTotalProductQuantity = new BoundField();
                bfTotalProductQuantity.DataField = dt.Columns["TotalProductQuantity" + "_" + dr["ProductId"]].ColumnName;
                bfTotalProductQuantity.HeaderText = "Total Quantity";
                gvAllFarmer.Columns.Add(bfTotalProductQuantity);

                BoundField bfSp = new BoundField();
                bfSp.DataField = "";
                bfSp.HeaderText = "  ";
                gvAllFarmer.Columns.Add(bfSp);
            }
        }
        #endregion
    }
    protected void btnsave_Click(object sender, EventArgs e)
    {
        //GridViewRow gvr = gvAllFarmer.Rows[1];
        //string str1 = string.Empty;
        //DataKey dkey = gvAllFarmer.DataKeys[1];
        //str1 += dkey.Values["FarmerId"].ToString() + " / " + dkey.Values["FarmID"].ToString() + " / " + dkey.Values["SeasonID"].ToString();// +" / " + dkey.Values["ProductId"].ToString() + " / " + dkey.Values["PlantationId"].ToString();
        ////foreach (TableCell str in gvr.Cells)
        ////{
        ////    str1 += str.Text;
        ////}
        //str1 += gvr.Cells[12].Text;
        DataTable dtproduct = pObj.GetProductDetailsbySeason(Convert.ToInt32(ddlAllFarmerSeason.SelectedValue));
        bool result = false;
        if (ddlAllFarmerProduct.SelectedIndex > 0)
        {
            foreach (GridViewRow gvr in gvAllFarmer.Rows)
            {
                int rowIndex = gvr.RowIndex;
                DataKey dk = gvAllFarmer.DataKeys[rowIndex];
                string farmerid = dk.Values["FarmerId"].ToString();
                int farmid = Convert.ToInt32(dk.Values["FarmID"].ToString());
                int seasonid = Convert.ToInt32(dk.Values["SeasonID"].ToString());
                string unitid = Guid.NewGuid().ToString();
                int cellcount = 12;

                int productid = Convert.ToInt32(dk.Values["ProductId"].ToString());
                int PlantationId = Convert.ToInt32(dk.Values["PlantationId"].ToString());
                result = fp.PlantationDetails_INSandUPDandDEL(farmerid, productid, unitid, farmid
                     , Convert.ToDateTime(gvr.Cells[cellcount].Text), Convert.ToDateTime(gvr.Cells[cellcount + 1].Text)
                     , Convert.ToDecimal(gvr.Cells[cellcount + 2].Text), Convert.ToDateTime(gvr.Cells[cellcount | +3].Text)
                     , Convert.ToInt32(gvr.Cells[cellcount + 4].Text), Convert.ToDecimal(gvr.Cells[cellcount + 5].Text)
                     , Convert.ToDateTime(gvr.Cells[cellcount + 6].Text), Convert.ToDecimal(gvr.Cells[cellcount + 7].Text)
                     , Convert.ToDateTime(gvr.Cells[cellcount + 8].Text), Convert.ToInt32(gvr.Cells[cellcount + 9].Text)
                     , Convert.ToDecimal(gvr.Cells[cellcount + 10].Text), Convert.ToDecimal(gvr.Cells[cellcount + 11].Text)
                     , false, false, "shaik Aslam", "shaik Aslam", seasonid, Convert.ToDecimal(gvr.Cells[cellcount - 1].Text)
                     , MudarApp.Insert, PlantationId, unitid, unitid, 0, 0, string.Empty, string.Empty);

            }
        }
        else
        {
            foreach (GridViewRow gvr in gvAllFarmer.Rows)
            {
                int rowIndex = gvr.RowIndex;
                DataKey dk = gvAllFarmer.DataKeys[rowIndex];
                string farmerid = dk.Values["FarmerId"].ToString();
                int farmid = Convert.ToInt32(dk.Values["FarmID"].ToString());
                int seasonid = Convert.ToInt32(dk.Values["SeasonID"].ToString());
                string unitid = Guid.NewGuid().ToString();
                int cellcount = 12;
                foreach (DataRow dr in dtproduct.Rows)
                {
                    if (!string.IsNullOrEmpty(dk.Values["ProductId_" + dr["ProductId"]].ToString()))
                    {
                        int productid = Convert.ToInt32(dk.Values["ProductId_" + dr["ProductId"]].ToString());
                        int PlantationId = Convert.ToInt32(dk.Values["PlantationId_" + dr["ProductId"]].ToString());
                        result = fp.PlantationDetails_INSandUPDandDEL(farmerid, productid, unitid, farmid
                             , Convert.ToDateTime(gvr.Cells[cellcount].Text), Convert.ToDateTime(gvr.Cells[cellcount + 1].Text)
                             , Convert.ToDecimal(gvr.Cells[cellcount + 2].Text), Convert.ToDateTime(gvr.Cells[cellcount | +3].Text)
                             , Convert.ToInt32(gvr.Cells[cellcount + 4].Text), Convert.ToDecimal(gvr.Cells[cellcount + 5].Text)
                             , Convert.ToDateTime(gvr.Cells[cellcount + 6].Text), Convert.ToDecimal(gvr.Cells[cellcount + 7].Text)
                             , Convert.ToDateTime(gvr.Cells[cellcount + 8].Text), Convert.ToInt32(gvr.Cells[cellcount + 9].Text)
                             , Convert.ToDecimal(gvr.Cells[cellcount + 10].Text), Convert.ToDecimal(gvr.Cells[cellcount + 11].Text)
                             , false, false, "shaik Aslam", "shaik Aslam", seasonid, Convert.ToDecimal(gvr.Cells[cellcount - 1].Text)
                             , MudarApp.Insert, PlantationId, unitid, unitid, 0, 0, string.Empty, string.Empty);
                    }
                    cellcount += 16;
                }
            }

        }
        BindgvALLFarmer();
    }
    protected void btnFarmerBack_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
        txtFarmerSearch.Text = string.Empty;
        ddlFarmerYear.ClearSelection();
        ddlFarmerSeason.ClearSelection();
        ddlFarmerProduct.ClearSelection();
        gvFarmer.DataBind();
    }
    private void Bindgvfarmer()
    {
        string farmercode = txtFarmerSearch.Text;//!string.IsNullOrEmpty(txtFarmerSearch.Text) ? (txtFarmerSearch.Text.Split('-'))[1].ToString().Trim() : string.Empty;
        int year = Convert.ToInt32(ddlFarmerYear.SelectedValue.ToString());
        int seasonid = Convert.ToInt32(ddlFarmerSeason.SelectedValue.ToString());
        int productid = Convert.ToInt32(ddlFarmerProduct.SelectedValue.ToString());
        DataTable dtTemp = fp.BuildPlantation(year, seasonid, productid, farmercode);
        gvFarmer.DataSource = dtTemp.DefaultView;
        gvFarmer.DataBind();
        foreach (GridViewRow gvr in gvFarmer.Rows)
        {
            if (!string.IsNullOrEmpty(dtTemp.Rows[gvr.RowIndex]["FirstUnitId"].ToString()))
            {
                DropDownList ddFirst = gvr.FindControl("ddlUnit") as DropDownList;
                ddFirst.ClearSelection();
                (gvr.FindControl("ddlUnit") as DropDownList).SelectedValue = dtTemp.Rows[gvr.RowIndex]["FirstUnitId"].ToString();
            }
            if (!string.IsNullOrEmpty(dtTemp.Rows[gvr.RowIndex]["SecondUnitId"].ToString()))
            {
                DropDownList ddSec = gvr.FindControl("ddlSecUnit") as DropDownList;
                ddSec.ClearSelection();
                (gvr.FindControl("ddlSecUnit") as DropDownList).SelectedValue = dtTemp.Rows[gvr.RowIndex]["SecondUnitId"].ToString();
            }
        }
        if (ddlCultivation.SelectedIndex == 1)
        {
            gvFirstCutVisible();
        }
        else if (ddlCultivation.SelectedIndex == 2)
        {
            gvSecCutVisible();
        }
        else if (ddlCultivation.SelectedIndex == 0)
        {
            gvAllCutVisible();
        }
        // gvFarmer.Columns[8].Visible = false;
    }

    private void gvFirstCutVisible()
    {
        for (int i = 13; i < 22; i++)
        {
            gvFarmer.Columns[i].Visible = true;
        }
        for (int i = 22; i < 31; i++)
        {
            gvFarmer.Columns[i].Visible = false;
        }
    }
    private void gvSecCutVisible()
    {
        for (int i = 13; i < 22; i++)
        {
            gvFarmer.Columns[i].Visible = false;
        }
        for (int i = 22; i < 31; i++)
        {
            gvFarmer.Columns[i].Visible = true;
        }
    }
    private void gvAllCutVisible()
    {
        for (int i = 11; i < 32; i++)
        {
            gvFarmer.Columns[i].Visible = true;
        }

    }
    protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void btnFarmerGo_Click(object sender, EventArgs e)
    {
        Bindgvfarmer();
    }
    protected void btnFarmerSave_Click(object sender, EventArgs e)
    {
        bool result = false;
        foreach (GridViewRow gvr in gvFarmer.Rows)
        {
            int plantationid = Convert.ToInt32(gvFarmer.DataKeys[gvr.RowIndex].Value);
            decimal PlantationArea = !string.IsNullOrEmpty((gvr.FindControl("txtPlantationArea") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtPlantationArea") as TextBox).Text) : 0;
            DateTime PlantationDate = !string.IsNullOrEmpty((gvr.FindControl("txtPlantationDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtPlantationDate") as TextBox).Text) : DateTime.Now;
            DateTime FirstHarvestDate = !string.IsNullOrEmpty((gvr.FindControl("txtFirstHarvestDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtFirstHarvestDate") as TextBox).Text) : DateTime.Now;
            decimal FirstHerbaga = !string.IsNullOrEmpty((gvr.FindControl("txtFirstHerbaga") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtFirstHerbaga") as TextBox).Text) : 0;
            DateTime FirstDistillationDate = !string.IsNullOrEmpty((gvr.FindControl("txtFirstDistillationDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtFirstDistillationDate") as TextBox).Text) : DateTime.Now;
            int FirstDistillationUnitNO = !string.IsNullOrEmpty((gvr.FindControl("txtFirstDistillationUnitNO") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtFirstDistillationUnitNO") as TextBox).Text) : 0;
            decimal FirstProductQuantity = !string.IsNullOrEmpty((gvr.FindControl("txtFirstProductQuantity") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtFirstProductQuantity") as TextBox).Text) : 0;
            bool FirstProductCompletion = (gvr.FindControl("txtFirstProductCompletion") as CheckBox).Checked;

            DateTime SecFirstHarvestDate = !string.IsNullOrEmpty((gvr.FindControl("txtSecondHarvestDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtSecondHarvestDate") as TextBox).Text) : DateTime.Now;
            decimal SecFirstHerbaga = !string.IsNullOrEmpty((gvr.FindControl("txtSecondHerbaga") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtSecondHerbaga") as TextBox).Text) : 0;
            DateTime SecFirstDistillationDate = !string.IsNullOrEmpty((gvr.FindControl("txtSecondDistillationDate") as TextBox).Text) ? Convert.ToDateTime((gvr.FindControl("txtSecondDistillationDate") as TextBox).Text) : DateTime.Now;
            int SecFirstDistillationUnitNO = !string.IsNullOrEmpty((gvr.FindControl("txtSecondDistillationUnitNO") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtSecondDistillationUnitNO") as TextBox).Text) : 0;
            decimal SecFirstProductQuantity = !string.IsNullOrEmpty((gvr.FindControl("txtSecondProductQuantity") as TextBox).Text) ? Convert.ToDecimal((gvr.FindControl("txtSecondProductQuantity") as TextBox).Text) : 0;
            bool SecFirstProductCompletion = (gvr.FindControl("txtFirstProductCompletion") as CheckBox).Checked;
            string FirstUnit = ((gvr.FindControl("ddlUnit") as DropDownList).SelectedItem.Value);
            string SecUnit = ((gvr.FindControl("ddlSecUnit") as DropDownList).SelectedItem.Value);
            int FirstNoLots = !string.IsNullOrEmpty((gvr.FindControl("txtNoOfLots") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtNoOfLots") as TextBox).Text) : 0;
            int SecNoLots = !string.IsNullOrEmpty((gvr.FindControl("txtSecNoOfLots") as TextBox).Text) ? Convert.ToInt32((gvr.FindControl("txtSecNoOfLots") as TextBox).Text) : 0;
            string FirstLotNo = (gvr.FindControl("hfLotNos") as HiddenField).Value;

            string SecLotNo = (gvr.FindControl("hfSecLotNos") as HiddenField).Value;

            string temp = Guid.NewGuid().ToString();
            result = fp.PlantationDetails_INSandUPDandDEL(temp, 0, temp, 0, PlantationDate, FirstHarvestDate, FirstHerbaga, FirstDistillationDate, FirstDistillationUnitNO,
                FirstProductQuantity, SecFirstHarvestDate, SecFirstHerbaga, SecFirstDistillationDate, SecFirstDistillationUnitNO, SecFirstProductQuantity,
                (FirstProductQuantity + SecFirstProductQuantity), false, false, "shaik Aslam", "shaik aslam", 0, PlantationArea, MudarApp.Update,
                plantationid, FirstUnit, SecUnit, FirstNoLots, SecNoLots, FirstLotNo, SecLotNo);
        }

        Bindgvfarmer();
    }
    protected void txtFirstProductCompletion_Checked(object sender, EventArgs e)
    {
        // Bindgvfarmer();
    }

    protected void OkButton_F_Click(object sender, EventArgs e)
    {
        CheckBox cb = (CheckBox)gvFarmer.Rows[3].FindControl("txtFirstProductCompletion");

        if (cb.Checked)
        {
            // purchaseProductList.Add(productId);
        }

        //int n=Convert.ToInt32(txtLots.Text);  

    }
    protected void Check_Clicked(Object sender, EventArgs e)
    {
        CheckBox ck1 = (CheckBox)sender;
        GridViewRow grow = (GridViewRow)ck1.NamingContainer;
        string str = ((CheckBox)grow.FindControl("txtFirstProductCompletion")).Text;
    }
    protected void DropDownList1_Load1(object sender, EventArgs e)
    {
        DataTable dt = fp.BindDropDownChild();
        if (((DropDownList)sender).Items.Count <= 0)
        {
            ((DropDownList)sender).DataSource = dt.DefaultView;
            ((DropDownList)sender).DataTextField = "Ucode";
            ((DropDownList)sender).DataValueField = "UnitId";
            ((DropDownList)sender).DataBind();
        }
    }
    protected void TextBox1_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlFarmerProduct_SelectedIndexChanged(object sender, EventArgs e)
    {

    }


    #region New Plantation Details    
    
    protected void ddlFPSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFPSeason.SelectedIndex > 0)
        {
            BindFPProductDetails(Convert.ToInt32(ddlFPSeason.SelectedValue));
        }
        else
            ddlFPProduct.Items.Clear();

    }
    protected void ddlFPYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlFPYear.SelectedIndex > 0)
        {
            BindFPSeasonDetailsList();
        }
        else
            ddlFPSeason.Items.Clear();
    }
    protected void ddlFPProduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void dlMainFP_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string command = e.CommandName;
        int index = e.Item.ItemIndex;
        switch (command)
        {
            case "addcrop":
                {
                    DataTable dtFarm = (DataTable)Session["dtFarm_s"];
                    DataRow drNew = dtFarm.NewRow();
                    drNew["FarmID"] = 0;
                    drNew["PlantationId"] = 0;
                    drNew["PlotArea"] = 0;
                    drNew["ParentFarmID"] = dlMainFP.DataKeys[index];
                    dtFarm.Rows.Add(drNew);
                    foreach (DataListItem dli in dlMainFP.Items)
                    {
                        string farmid = dlMainFP.DataKeys[dli.ItemIndex].ToString();
                        DataTable dt = new DataTable();
                        dt = dtFarm.Clone();
                        DataRow[] drs = dtFarm.Select("ParentFarmID = " + farmid);
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        GridView gvCrop = (GridView)dli.FindControl("gvCrops");
                        gvCrop.DataSource = dt;
                        gvCrop.DataBind();
                    }
                }
                break;
        }
    }
    protected void btnFPGo_Click(object sender, EventArgs e)
    {
        if (ddlFPProduct.SelectedIndex > 0)
            BindDlMainFP();
    }
    protected void btnFPBack_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 4;
        txtFPFarmerN.Text = string.Empty;
        dlMainFP.DataBind();
        ddlFPProduct.ClearSelection();
        ddlFPSeason.ClearSelection();
        ddlFPYear.ClearSelection();
    }
    protected void btnFPSave_Click(object sender, EventArgs e)
    {
        DataTable dtBasicFInfo = farmingObj.GetBasicFarmingInfo(Convert.ToInt32(ddlFPYear.SelectedValue), Convert.ToInt32(ddlFPProduct.SelectedValue), Convert.ToInt32(ddlFPSeason.SelectedValue));
        string[] strFarmer = txtFPFarmerN.Text.Split('-');
        string farmerID =string.Empty;
        //if (strFarmer.Length >= 2)
        //{
            DataTable dtFarmerDetails = far.FamerDetails(strFarmer[0].Trim(), strFarmer[1].Trim(), strFarmer[2].Trim());
            farmerID = txtFPFarmerN.Text;//dtFarmerDetails.Rows[0]["FarmerID"].ToString() ;
        //}
        if (dtBasicFInfo.Rows.Count > 0)
        {
            for (int count = 0; count < dlMainFP.Items.Count; count++)
            {
                int PFarmID = Convert.ToInt32(dlMainFP.DataKeys[count].ToString());
                GridView gv = (dlMainFP.Items[count].FindControl("gvCrops") as GridView);
                if (gv.Rows.Count > 0)
                {
                    decimal PTotalArea = Convert.ToDecimal((dlMainFP.Items[count].FindControl("lblPlotArea") as Label).Text);
                    decimal TotalArea = 0;
                    for (int gvCount = 0; gvCount < gv.Rows.Count; gvCount++)
                    {
                        DataKey dk = gv.DataKeys[gvCount];
                        int FarmId = !string.IsNullOrEmpty(dk.Values["FarmId"].ToString()) ? Convert.ToInt32(dk.Values["FarmId"].ToString()) : 0;
                        int PlantationId = !string.IsNullOrEmpty(dk.Values["PlantationId"].ToString()) ? Convert.ToInt32(dk.Values["PlantationId"].ToString()) : 0;
                        string PA = (gv.Rows[gvCount].FindControl("txtPlotArea") as TextBox).Text;
                        decimal PlotArea = !string.IsNullOrEmpty(PA) ? Convert.ToDecimal(PA) : 0;
                        TotalArea += PlotArea;
                        DateTime PlantationDate = objMudarApp.GenerateRandomDate(dtBasicFInfo.Rows[0]["PlantationFrom"].ToString(), dtBasicFInfo.Rows[0]["PlantationTo"].ToString());
                        int FHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["1stCutTo"].ToString()));
                        int SHCount = MudarApp.RandomNumber(Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutFrom"].ToString()), Convert.ToInt32(dtBasicFInfo.Rows[0]["2ndCutTo"].ToString()));
                        string unitid = Guid.NewGuid().ToString();

                        if(TotalArea<=PTotalArea)
                            if (FarmId == 0 && PlantationId == 0)
                            {
                                //IsInterCrop is constant false.
                                bool resutl = fp.Farm_PlantationDetails_INSandUPDandDEL(farmerID, FarmId, PlotArea, string.Empty, 0, 0, PFarmID, Convert.ToInt32(ddlFPProduct.SelectedValue),
                                    unitid, PlantationDate, PlantationDate.AddDays(FHCount), 0, PlantationDate.AddDays(FHCount + 2), 0, 0, PlantationDate.AddDays(FHCount + SHCount), 0,
                                    PlantationDate.AddDays(FHCount + SHCount + 2), 0, 0, 0, false, false, "Aslam", string.Empty,
                                    Convert.ToInt32(ddlFPSeason.SelectedValue), PlotArea, MudarApp.Insert, 0, unitid, unitid, 0, 0, string.Empty, string.Empty,
                                     Convert.ToInt32(ddlFPYear.SelectedValue), false,0,0,0,0);
                            }
                    }
                }
            }
            if (ddlFPProduct.SelectedIndex > 0)
                BindDlMainFP();
        }
        else
        {
            //no Basic Information is not schedule. ok
        }
    }
    private void BindFPSeasonDetailsList()
    {

        ddlFPSeason.DataSource = cp.GetSeasonDetails(ddlFPYear.SelectedValue);
        ddlFPSeason.DataTextField = "SeasonName";
        ddlFPSeason.DataValueField = "SeasonID";
        ddlFPSeason.DataBind();

        ddlFPSeason.Items.Insert(0, MudarApp.AddListItem());
    }
    private void BindFPProductDetails(int SeasonID)
    {
        ddlFPProduct.DataSource = pObj.GetProductDetailsbySeason(SeasonID);
        ddlFPProduct.DataTextField = "ProductName";
        ddlFPProduct.DataValueField = "ProductId";
        ddlFPProduct.DataBind();

        ddlFPProduct.Items.Insert(0, MudarApp.AddListItem());
    }
    private void BindDlMainFP()
    {
        string[] strFarmer = txtFPFarmerN.Text.Split('-');
        string farmerID =string.Empty;
        //if (strFarmer.Length >= 2)
        //{
            DataTable dtFarmerDetails = far.FamerDetails(strFarmer[0].Trim(), strFarmer[1].Trim(), strFarmer[2].Trim());
            farmerID = txtFPFarmerN.Text; //dtFarmerDetails.Rows[0]["FarmerID"].ToString() ;
            DataTable dtFarmDetails = far.FarmDetails(txtFPFarmerN.Text, true); //far.FarmDetails(dtFarmerDetails.Rows[0]["FarmerID"].ToString(), true);

            dlMainFP.DataSource = dtFarmDetails;
            dlMainFP.DataBind();

            DataTable dtFarm = fp.GetPlantation(Convert.ToInt32(ddlFPYear.SelectedValue), Convert.ToInt32(ddlFPSeason.SelectedValue), Convert.ToInt32(ddlFPProduct.SelectedValue), farmerID);
            Session["dtFarm_s"] = dtFarm;
            foreach (DataListItem dli in dlMainFP.Items)
            {
                string farmid = dlMainFP.DataKeys[dli.ItemIndex].ToString();
                DataTable dt = new DataTable();
                dt = dtFarm.Clone();
                DataRow[] drs = dtFarm.Select("ParentFarmID = " + farmid);
                foreach (DataRow dr in drs)
                    dt.ImportRow(dr);

                GridView gvCrop = (GridView)dli.FindControl("gvCrops");
                gvCrop.DataSource = dt;
                gvCrop.DataBind();
            }
        //}
        //else
        //{
        //    //dlMainFP.DataBind();
        //    //txtFPFarmerN is null plz retry
        //}
    }
    #endregion
}