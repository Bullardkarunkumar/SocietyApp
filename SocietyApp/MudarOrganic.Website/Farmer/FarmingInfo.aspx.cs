using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;
using System.Web.Configuration;

public partial class Farmer_FarmingInfo : System.Web.UI.Page
{
    Farmer_BL farmerObj = new Farmer_BL();
    CategoryProduct_BL cpObj = new CategoryProduct_BL();
    Product_BL productObj = new Product_BL();
    Farming_BL farmingObj = new Farming_BL();

    public static string code = string.Empty;
    public static int productID = 0;
    public static int seasonID = 0;
    public static string farmerID = System.Configuration.ConfigurationManager.AppSettings["FarmerID"].ToString() ;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnFarmsInfo();
            BindFarmerList();
            BindYears();
            ddlYear_SelectedIndexChanged(sender, e);
        }
    }
    protected void gvFarmer_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //int index = Convert.ToInt32(e.CommandArgument);
        //string command = e.CommandName;
        //switch (command)
        //{
        //    case "Farmer":
        //        {
        //            code = gvFarmer.Rows[index].Cells[0].Text;
        //            farmerID = gvFarmer.DataKeys[index].Value.ToString();
        //            MultiView1.ActiveViewIndex = 1;
        //        }
        //        break;
        //}
    }
    protected void ddlYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlYear.SelectedIndex > 0)
        {
            ddlSeason.DataSource = cpObj.GetSeasonDetails(ddlYear.SelectedValue);
            ddlSeason.DataTextField = "SeasonName";
            ddlSeason.DataValueField = "SeasonID";
            ddlSeason.DataBind();
            BindProductList();
        }
        else
        {
            ddlSeason.Items.Clear();
            gvProduct.DataBind();
        }
    }
    protected void ddlSeason_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindProductList();
    }

    protected void gvProduct_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int index = Convert.ToInt32(e.CommandArgument);
        switch (e.CommandName)
        {
            case "Product":
                {
                    DataKey dk = gvProduct.DataKeys[index];
                    productID = Convert.ToInt32(dk.Values["ProductID"].ToString());
                    lblProductName.Text = dk.Values["ProductName"].ToString();
                    
                    //BINDING ALL FARMING INFORMATION
                    BindPlantingInfo();
                    BindInputInfo();
                    BindDisease();
                    BindInsectInfo();
                    BindPestInfo();
                    BindWeedInfo();
                    BindWaterInfo();
                    MultiView1.ActiveViewIndex = 1;
                }
                break;
        }
    }
    protected void btnPlanting_Click(object sender, EventArgs e)
    {
        int platingID = Convert.ToInt32(hfPIID.Value);
        bool result = false;
        if (platingID == 0)
        {
            result = farmingObj.PlantingInfo_INT_UPT_DEL(ref platingID, farmerID, productID, seasonID, ddlYear.SelectedValue, txtPIsource.Text, txtPIBill.Text, txtPISeedVariety.Text, txtPISeedTreatment.Text, txtPIQuantity.Text, "Aslam", "Aslam", MudarApp.Insert, platingID);
            //hfPIID.Value = platingID.ToString();
        }
        else
        {
            result = farmingObj.PlantingInfo_INT_UPT_DEL(ref platingID, farmerID, productID, seasonID, ddlYear.SelectedValue, txtPIsource.Text, txtPIBill.Text, txtPISeedVariety.Text, txtPISeedTreatment.Text, txtPIQuantity.Text, "Aslam", "Aslam", MudarApp.Update, platingID);
            //hfPIID.Value = platingID.ToString();
        }
        BindPlantingInfo();
    }
    protected void dlInputInfo_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        switch (e.CommandName)
        {
            case "AddInput":
                {
                    DataSet ds = (DataSet)Session["InputInfo"];
                    index = ds.Tables[0].Rows.Count;
                    //ADD NEW ROWS
                    DataRow drp = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(drp);
                    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["S_InputMID"] = index + "$N";
                    dlInputInfo.DataSource = ds.Tables[0];
                    dlInputInfo.DataBind();


                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_InputMID"] = index + "$N";

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlInputInfo.Items.Count; count++)
                    {
                        DataList innerDL = (dlInputInfo.Items[count].FindControl("dlTransaction") as DataList);
                        string InputMID = dlInputInfo.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_InputMID like '" + InputMID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                    }
                    //ds = (DataSet)Session["InputInfo"];
                }
                break;
            case "AddTInput":
                {
                    string input = dlInputInfo.DataKeys[index].ToString();
                    DataSet ds = (DataSet)Session["InputInfo"];
                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_InputMID"] = input;

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlInputInfo.Items.Count; count++)
                    {
                        DataList innerDL = (dlInputInfo.Items[count].FindControl("dlTransaction") as DataList);
                        string InputMID = dlInputInfo.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_InputMID like '" + InputMID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                        for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                        {
                            string hfInputMTId = (innerDL.Items[Tcount].FindControl("hfInputMTId") as HiddenField).Value;
                            if (!string.IsNullOrEmpty(hfInputMTId))
                            {
                                DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlPeriod") as DropDownList);
                                (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["IMPeriod"].ToString())).Selected = true;
                                DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlPlanting") as DropDownList);
                                (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["IMPlanting"].ToString())).Selected = true;
                            }
                        }
                    }
                }
                break;
        }
    }
    protected void btnInputinfo_Click(object source, EventArgs e)
    {
        for (int index = 0; index < dlInputInfo.Items.Count; index++)
        {
            bool result = false;
            string InputMID = dlInputInfo.DataKeys[index].ToString();
            int rInputMID = 0;
            string txtIMMaterial = (dlInputInfo.Items[index].FindControl("txtMaterial") as TextBox).Text;
            string txtIMSource = (dlInputInfo.Items[index].FindControl("txtSource") as TextBox).Text;
            string txtIMBill = (dlInputInfo.Items[index].FindControl("txtBill") as TextBox).Text;
            string txtIMDate = (dlInputInfo.Items[index].FindControl("txtDate") as TextBox).Text;
            string txtIMQuantity = (dlInputInfo.Items[index].FindControl("txtQuantity") as TextBox).Text;
            if (InputMID.Contains('N'))
            {
                InputMID = InputMID.Replace("$N", "");
                result = farmingObj.InputInformation_INT_UPT_DEL(farmerID, productID, seasonID, ddlYear.SelectedValue, txtIMMaterial, txtIMSource, txtIMBill, !string.IsNullOrEmpty(txtIMDate) ? Convert.ToDateTime(txtIMDate) : DateTime.Now, txtIMQuantity, "Aslam", "", MudarApp.Insert, ref rInputMID, Convert.ToInt32(InputMID));
            }
            else
            {
                result = farmingObj.InputInformation_INT_UPT_DEL(farmerID, productID, seasonID, ddlYear.SelectedValue, txtIMMaterial, txtIMSource, txtIMBill, !string.IsNullOrEmpty(txtIMDate) ? Convert.ToDateTime(txtIMDate) : DateTime.Now, txtIMQuantity, "", "Aslam", MudarApp.Update, ref rInputMID, Convert.ToInt32(InputMID));
            }
            DataList dltran = (dlInputInfo.Items[index].FindControl("dlTransaction") as DataList);
            for (int indexT = 0; indexT < dltran.Items.Count; indexT++)
            {
                string hfInputMTId = (dltran.Items[indexT].FindControl("hfInputMTId") as HiddenField).Value;
                string txtTQuantity = (dltran.Items[indexT].FindControl("txtTQuantity") as TextBox).Text;
                string txtDays = (dltran.Items[indexT].FindControl("txtDays") as TextBox).Text;
                string ddlPeriod = (dltran.Items[indexT].FindControl("ddlPeriod") as DropDownList).Text;
                string ddlPlanting = (dltran.Items[indexT].FindControl("ddlPlanting") as DropDownList).Text;
                if (string.IsNullOrEmpty(hfInputMTId))
                {
                    result = farmingObj.InputTransaction_INT_UPT_DEL(rInputMID, txtTQuantity, txtDays, ddlPeriod, ddlPlanting, "Aslam", "", MudarApp.Insert, 0);
                }
                else
                {
                    result = farmingObj.InputTransaction_INT_UPT_DEL(rInputMID, txtTQuantity, txtDays, ddlPeriod, ddlPlanting, "", "Aslam", MudarApp.Update, Convert.ToInt32(hfInputMTId));
                }
            }
        }
        BindInputInfo();
    }
    
    protected void dlDisease_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        switch (e.CommandName)
        {
            case "AddDisease":
                {
                    DataSet ds = (DataSet)Session["Disease"];
                    index = ds.Tables[0].Rows.Count;
                    //ADD NEW ROWS
                    DataRow drp = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(drp);
                    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["S_DiseaseMID"] = index + "$N";
                    dlDisease.DataSource = ds.Tables[0];
                    dlDisease.DataBind();


                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_DiseaseMID"] = index + "$N";

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlDisease.Items.Count; count++)
                    {
                        DataList innerDL = (dlDisease.Items[count].FindControl("dlDTransaction") as DataList);
                        string InputMID = dlDisease.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_DiseaseMID like '" + InputMID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                    }
                    //ds = (DataSet)Session["InputInfo"];
                }
                break;
            case "AddTDisease":
                {
                    string input = dlDisease.DataKeys[index].ToString();
                    DataSet ds = (DataSet)Session["Disease"];
                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_DiseaseMID"] = input;

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlDisease.Items.Count; count++)
                    {
                        DataList innerDL = (dlDisease.Items[count].FindControl("dlDTransaction") as DataList);
                        string InputMID = dlDisease.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_DiseaseMID like '" + InputMID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                        for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                        {
                            string hfInputMTId = (innerDL.Items[Tcount].FindControl("hfDMITId") as HiddenField).Value;
                            if (!string.IsNullOrEmpty(hfInputMTId))
                            {
                                DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlDTPeriod") as DropDownList);
                                (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["DMIT_Period"].ToString())).Selected = true;
                                DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlDTPlanting") as DropDownList);
                                (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["DMIT_Planting"].ToString())).Selected = true;
                            }
                        }
                    }
                }
                break;
        }
    }
    protected void btnDisease_Click(object source, EventArgs e)
    {
        for (int index = 0; index < dlDisease.Items.Count; index++)
        {
            bool result = false;
            string DiseaseMID = dlDisease.DataKeys[index].ToString();
            int rDiseaseMID = 0;

            string txtDisease = (dlDisease.Items[index].FindControl("txtDisease") as TextBox).Text;
            string txtDExpected = (dlDisease.Items[index].FindControl("txtExpected") as TextBox).Text;
            string txtDObserved = (dlDisease.Items[index].FindControl("txtObserved") as TextBox).Text;
            string txtDPrevention = (dlDisease.Items[index].FindControl("txtPrevention") as TextBox).Text;
            string txtDSource = (dlDisease.Items[index].FindControl("txtSource") as TextBox).Text;
            string txtDBill = (dlDisease.Items[index].FindControl("txtBill") as TextBox).Text;
            string txtDDate = (dlDisease.Items[index].FindControl("txtDate") as TextBox).Text;
            string txtDQuantity = (dlDisease.Items[index].FindControl("txtQuantity") as TextBox).Text;
            if (DiseaseMID.Contains('N'))
            {
                DiseaseMID = DiseaseMID.Replace("$N", "");
                result = farmingObj.DiseaseInfo_INT_UPT_DEL(txtDisease, farmerID, productID, seasonID, ddlYear.SelectedValue, txtDExpected, txtDObserved, txtDPrevention, txtDSource, txtDBill, !string.IsNullOrEmpty(txtDDate) ? Convert.ToDateTime(txtDDate) : DateTime.Now, txtDQuantity, "Aslam", "", MudarApp.Insert, ref rDiseaseMID, Convert.ToInt32(DiseaseMID));
            }
            else
            {
                result = farmingObj.DiseaseInfo_INT_UPT_DEL(txtDisease, farmerID, productID, seasonID, ddlYear.SelectedValue, txtDExpected, txtDObserved, txtDPrevention, txtDSource, txtDBill, !string.IsNullOrEmpty(txtDDate) ? Convert.ToDateTime(txtDDate) : DateTime.Now, txtDQuantity, "", "Aslam", MudarApp.Update, ref rDiseaseMID, Convert.ToInt32(DiseaseMID));
            }
            DataList dltran = (dlDisease.Items[index].FindControl("dlDTransaction") as DataList);
            for (int indexT = 0; indexT < dltran.Items.Count; indexT++)
            {
                string hfInputMTId = (dltran.Items[indexT].FindControl("hfDMITId") as HiddenField).Value;
                string txtDTQuantity = (dltran.Items[indexT].FindControl("txtDTQuantity") as TextBox).Text;
                string txtDDays = (dltran.Items[indexT].FindControl("txtDTDays") as TextBox).Text;
                string ddlPeriod = (dltran.Items[indexT].FindControl("ddlDTPeriod") as DropDownList).Text;
                string ddlPlanting = (dltran.Items[indexT].FindControl("ddlDTPlanting") as DropDownList).Text;
                if (string.IsNullOrEmpty(hfInputMTId))
                {
                    result = farmingObj.DiseaseTransaction_INT_UP_DEL(rDiseaseMID, txtDTQuantity, txtDDays, ddlPeriod, ddlPlanting, "Aslam", "", MudarApp.Insert, 0);
                }
                else
                {
                    result = farmingObj.DiseaseTransaction_INT_UP_DEL(rDiseaseMID, txtDTQuantity, txtDDays, ddlPeriod, ddlPlanting, "", "Aslam", MudarApp.Update, Convert.ToInt32(hfInputMTId));
                }
            }
        }
        BindDisease();
    }
    protected void dlInsect_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        switch (e.CommandName)
        {
            case "AddInsect":
                {
                    DataSet ds = (DataSet)Session["Insect"];
                    index = ds.Tables[0].Rows.Count;
                    //ADD NEW ROWS
                    DataRow drp = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(drp);
                    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["S_InsectMIID"] = index + "$N";
                    dlInsect.DataSource = ds.Tables[0];
                    dlInsect.DataBind();


                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_InsectMIID"] = index + "$N";

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlInsect.Items.Count; count++)
                    {
                        DataList innerDL = (dlInsect.Items[count].FindControl("dlITransaction") as DataList);
                        string InsectMIID = dlInsect.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_InsectMIID like '" + InsectMIID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                    }
                    //ds = (DataSet)Session["InputInfo"];
                }
                break;
            case "AddTInsect":
                {
                    string input = dlInsect.DataKeys[index].ToString();
                    DataSet ds = (DataSet)Session["Insect"];
                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_InsectMIID"] = input;

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlInsect.Items.Count; count++)
                    {
                        DataList innerDL = (dlInsect.Items[count].FindControl("dlDTransaction") as DataList);
                        string InputMID = dlInsect.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_InsectMIID like '" + InputMID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                        for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                        {
                            string hfInMITId = (innerDL.Items[Tcount].FindControl("hfInMITId") as HiddenField).Value;
                            if (!string.IsNullOrEmpty(hfInMITId))
                            {
                                DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlITPeriod") as DropDownList);
                                (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["IMIT_Period"].ToString())).Selected = true;
                                DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlITPlanting") as DropDownList);
                                (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["IMIT_Planting"].ToString())).Selected = true;
                            }
                        }
                    }
                }
                break;
        }
    }
    protected void btnInsect_Click(object sender, EventArgs e)
    {
        for (int index = 0; index < dlInsect.Items.Count; index++)
        {
            bool result = false;
            string InsectMIID = dlInsect.DataKeys[index].ToString();
            int rInsectMIID = 0;
            string txtInsect = (dlInsect.Items[index].FindControl("txtInsect") as TextBox).Text;
            string txtIExpected = (dlInsect.Items[index].FindControl("txtIExpected") as TextBox).Text;
            string txtIObserved = (dlInsect.Items[index].FindControl("txtIObserved") as TextBox).Text;
            string txtIPrevention = (dlInsect.Items[index].FindControl("txtIPrevention") as TextBox).Text;
            string txtISource = (dlInsect.Items[index].FindControl("txtISource") as TextBox).Text;
            string txtIBill = (dlInsect.Items[index].FindControl("txtIBill") as TextBox).Text;
            string txtIDate = (dlInsect.Items[index].FindControl("txtIDate") as TextBox).Text;
            string txtIQuantity = (dlInsect.Items[index].FindControl("txtIQuantity") as TextBox).Text;
            if (InsectMIID.Contains('N'))
            {
                InsectMIID = InsectMIID.Replace("$N", "");
                result = farmingObj.InsectInfo_INT_UPT_DEL(txtInsect, farmerID, productID, seasonID, ddlYear.SelectedValue, txtIExpected, txtIObserved, txtIPrevention, txtISource, txtIBill, !string.IsNullOrEmpty(txtIDate) ? Convert.ToDateTime(txtIDate) : DateTime.Now, txtIQuantity, "Aslam", "", MudarApp.Insert, ref rInsectMIID, Convert.ToInt32(InsectMIID));
            }
            else
            {
                result = farmingObj.InsectInfo_INT_UPT_DEL(txtInsect, farmerID, productID, seasonID, ddlYear.SelectedValue, txtIExpected, txtIObserved, txtIPrevention, txtISource, txtIBill, !string.IsNullOrEmpty(txtIDate) ? Convert.ToDateTime(txtIDate) : DateTime.Now, txtIQuantity, "", "Aslam", MudarApp.Update, ref rInsectMIID, Convert.ToInt32(InsectMIID));
            }
            DataList dltran = (dlInsect.Items[index].FindControl("dlITransaction") as DataList);
            for (int indexT = 0; indexT < dltran.Items.Count; indexT++)
            {
                string hfInMITId = (dltran.Items[indexT].FindControl("hfInMITId") as HiddenField).Value;
                string txtITQuantity = (dltran.Items[indexT].FindControl("txtITQuantity") as TextBox).Text;
                string txtITDays = (dltran.Items[indexT].FindControl("txtITDays") as TextBox).Text;
                string ddlITPeriod = (dltran.Items[indexT].FindControl("ddlITPeriod") as DropDownList).Text;
                string ddlITPlanting = (dltran.Items[indexT].FindControl("ddlITPlanting") as DropDownList).Text;
                if (string.IsNullOrEmpty(hfInMITId))
                {
                    result = farmingObj.InsectTransaction_INT_UP_DEL(rInsectMIID, txtITQuantity, txtITDays, ddlITPeriod, ddlITPlanting, "Aslam", "", MudarApp.Insert, 0);
                }
                else
                {
                    result = farmingObj.InsectTransaction_INT_UP_DEL(rInsectMIID, txtITQuantity, txtITDays, ddlITPeriod, ddlITPlanting, "", "Aslam", MudarApp.Update, Convert.ToInt32(hfInMITId));
                }
            }
        }
        BindInsectInfo();
    }
    protected void dlPest_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        switch (e.CommandName)
        {
            case "AddPest":
                {
                    DataSet ds = (DataSet)Session["Pest"];
                    index = ds.Tables[0].Rows.Count;
                    //ADD NEW ROWS
                    DataRow drp = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(drp);
                    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["S_PestMIID"] = index + "$N";
                    dlPest.DataSource = ds.Tables[0];
                    dlPest.DataBind();


                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_PestMIID"] = index + "$N";

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlPest.Items.Count; count++)
                    {
                        DataList innerDL = (dlPest.Items[count].FindControl("dlPTransaction") as DataList);
                        string PestMIID = dlPest.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_PestMIID like '" + PestMIID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                    }
                    //ds = (DataSet)Session["InputInfo"];
                }
                break;
            case "AddTPest":
                {
                    string input = dlPest.DataKeys[index].ToString();
                    DataSet ds = (DataSet)Session["Pest"];
                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_PestMIID"] = input;

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlPest.Items.Count; count++)
                    {
                        DataList innerDL = (dlPest.Items[count].FindControl("dlPTransaction") as DataList);
                        string PestMIID = dlPest.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_PestMIID like '" + PestMIID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                        for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                        {
                            string hfPMITId = (innerDL.Items[Tcount].FindControl("hfPMITId") as HiddenField).Value;
                            if (!string.IsNullOrEmpty(hfPMITId))
                            {
                                DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlPTPeriod") as DropDownList);
                                (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["PMIT_Period"].ToString())).Selected = true;
                                DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlPTPlanting") as DropDownList);
                                (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["PMIT_Planting"].ToString())).Selected = true;
                            }
                        }
                    }
                }
                break;
        }
    }
    protected void btnPest_Click(object sender, EventArgs e)
    {
        for (int index = 0; index < dlPest.Items.Count; index++)
        {
            bool result = false;
            string PestMIID = dlPest.DataKeys[index].ToString();
            int rPestMIID = 0;

            string txtPest = (dlPest.Items[index].FindControl("txtPest") as TextBox).Text;
            string txtPExpected = (dlPest.Items[index].FindControl("txtPExpected") as TextBox).Text;
            string txtPObserved = (dlPest.Items[index].FindControl("txtPObserved") as TextBox).Text;
            string txtPPrevention = (dlPest.Items[index].FindControl("txtPPrevention") as TextBox).Text;
            string txtPSource = (dlPest.Items[index].FindControl("txtPSource") as TextBox).Text;
            string txtPBill = (dlPest.Items[index].FindControl("txtPBill") as TextBox).Text;
            string txtPDate = (dlPest.Items[index].FindControl("txtPDate") as TextBox).Text;
            string txtPQuantity = (dlPest.Items[index].FindControl("txtPQuantity") as TextBox).Text;
            if (PestMIID.Contains('N'))
            {
                PestMIID = PestMIID.Replace("$N", "");
                result = farmingObj.PestInfo_INT_UPT_DEL(txtPest, farmerID, productID, seasonID, ddlYear.SelectedValue, txtPExpected, txtPObserved, txtPPrevention, txtPSource, txtPBill, !string.IsNullOrEmpty(txtPDate) ? Convert.ToDateTime(txtPDate) : DateTime.Now, txtPQuantity, "Aslam", "", MudarApp.Insert, ref rPestMIID, Convert.ToInt32(PestMIID));
            }
            else
            {
                result = farmingObj.PestInfo_INT_UPT_DEL(txtPest, farmerID, productID, seasonID, ddlYear.SelectedValue, txtPExpected, txtPObserved, txtPPrevention, txtPSource, txtPBill, !string.IsNullOrEmpty(txtPDate) ? Convert.ToDateTime(txtPDate) : DateTime.Now, txtPQuantity, "", "Aslam", MudarApp.Update, ref rPestMIID, Convert.ToInt32(PestMIID));
            }
            DataList dltran = (dlPest.Items[index].FindControl("dlPTransaction") as DataList);
            for (int indexT = 0; indexT < dltran.Items.Count; indexT++)
            {
                string hfPMITId = (dltran.Items[indexT].FindControl("hfPMITId") as HiddenField).Value;
                string txtPTQuantity = (dltran.Items[indexT].FindControl("txtPTQuantity") as TextBox).Text;
                string txtPDays = (dltran.Items[indexT].FindControl("txtPTDays") as TextBox).Text;
                string ddlPeriod = (dltran.Items[indexT].FindControl("ddlPTPeriod") as DropDownList).Text;
                string ddlPlanting = (dltran.Items[indexT].FindControl("ddlPTPlanting") as DropDownList).Text;
                if (string.IsNullOrEmpty(hfPMITId))
                {
                    result = farmingObj.PestTransaction_INT_UPT_DEL(rPestMIID, txtPTQuantity, txtPDays, ddlPeriod, ddlPlanting, "Aslam", "", MudarApp.Insert, 0);
                }
                else
                {
                    result = farmingObj.PestTransaction_INT_UPT_DEL(rPestMIID, txtPTQuantity, txtPDays, ddlPeriod, ddlPlanting, "", "Aslam", MudarApp.Update, Convert.ToInt32(hfPMITId));
                }
            }
        }
    }
    protected void dlWeed_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        switch (e.CommandName)
        {
            case "AddWeed":
                {
                    DataSet ds = (DataSet)Session["Weed"];
                    index = ds.Tables[0].Rows.Count;
                    //ADD NEW ROWS
                    DataRow drp = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(drp);
                    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["S_WeedMIID"] = index + "$N";
                    dlWeed.DataSource = ds.Tables[0];
                    dlWeed.DataBind();


                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_WeedMIID"] = index + "$N";

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlWeed.Items.Count; count++)
                    {
                        DataList innerDL = (dlWeed.Items[count].FindControl("dlWTransaction") as DataList);
                        string WeedMIID = dlWeed.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_WeedMIID like '" + WeedMIID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                    }
                    //ds = (DataSet)Session["InputInfo"];
                }
                break;
            case "AddTWeed":
                {
                    string input = dlWeed.DataKeys[index].ToString();
                    DataSet ds = (DataSet)Session["Weed"];
                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_WeedMIID"] = input;

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlWeed.Items.Count; count++)
                    {
                        DataList innerDL = (dlWeed.Items[count].FindControl("dlWTransaction") as DataList);
                        string WeedMIID = dlWeed.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_WeedMIID like '" + WeedMIID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                        for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                        {
                            string hfWMITId = (innerDL.Items[Tcount].FindControl("hfWMITId") as HiddenField).Value;
                            if (!string.IsNullOrEmpty(hfWMITId))
                            {
                                DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlWTPeriod") as DropDownList);
                                (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["WMIT_Period"].ToString())).Selected = true;
                                DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlWTPlanting") as DropDownList);
                                (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["WMIT_Planting"].ToString())).Selected = true;
                            }
                        }
                    }
                }
                break;
        }

    }
    protected void btnWeed_Click(object sender, EventArgs e)
    {
        for (int index = 0; index < dlWeed.Items.Count; index++)
        {
            bool result = false;
            string WeedMIID = dlWeed.DataKeys[index].ToString();
            int rWeedMIID = 0;

            string txtWeed = (dlWeed.Items[index].FindControl("txtWeed") as TextBox).Text;
            string txtWExpected = (dlWeed.Items[index].FindControl("txtWExpected") as TextBox).Text;
            string txtWObserved = (dlWeed.Items[index].FindControl("txtWObserved") as TextBox).Text;
            string txtWPrevention = (dlWeed.Items[index].FindControl("txtWPrevention") as TextBox).Text;
            string txtWSource = (dlWeed.Items[index].FindControl("txtWSource") as TextBox).Text;
            string txtWBill = (dlWeed.Items[index].FindControl("txtWBill") as TextBox).Text;
            string txtWDate = (dlWeed.Items[index].FindControl("txtWDate") as TextBox).Text;
            string txtWQuantity = (dlWeed.Items[index].FindControl("txtWQuantity") as TextBox).Text;
            if (WeedMIID.Contains('N'))
            {
                WeedMIID = WeedMIID.Replace("$N", "");
                result = farmingObj.WeedInfo_INT_UPT_DEL(txtWeed, farmerID, productID, seasonID, ddlYear.SelectedValue, txtWExpected, txtWObserved, txtWPrevention, txtWSource, txtWBill, !string.IsNullOrEmpty(txtWDate) ? Convert.ToDateTime(txtWDate) : DateTime.Now, txtWQuantity, "Aslam", "", MudarApp.Insert, ref rWeedMIID, Convert.ToInt32(WeedMIID));
            }
            else
            {
                result = farmingObj.WeedInfo_INT_UPT_DEL(txtWeed, farmerID, productID, seasonID, ddlYear.SelectedValue, txtWExpected, txtWObserved, txtWPrevention, txtWSource, txtWBill, !string.IsNullOrEmpty(txtWDate) ? Convert.ToDateTime(txtWDate) : DateTime.Now, txtWQuantity, "", "Aslam", MudarApp.Update, ref rWeedMIID, Convert.ToInt32(WeedMIID));
            }
            DataList dltran = (dlWeed.Items[index].FindControl("dlWTransaction") as DataList);
            for (int indexT = 0; indexT < dltran.Items.Count; indexT++)
            {
                string hfWMITId = (dltran.Items[indexT].FindControl("hfWMITId") as HiddenField).Value;
                string txtWTQuantity = (dltran.Items[indexT].FindControl("txtWTQuantity") as TextBox).Text;
                string txtWTDays = (dltran.Items[indexT].FindControl("txtWTDays") as TextBox).Text;
                string ddlPeriod = (dltran.Items[indexT].FindControl("ddlWTPeriod") as DropDownList).Text;
                string ddlPlanting = (dltran.Items[indexT].FindControl("ddlWTPlanting") as DropDownList).Text;
                if (string.IsNullOrEmpty(hfWMITId))
                {
                    result = farmingObj.WeedTransaction_INT_UPT_DEL(rWeedMIID, txtWTQuantity, txtWTDays, ddlPeriod, ddlPlanting, "Aslam", "", MudarApp.Insert, 0);
                }
                else
                {
                    result = farmingObj.WeedTransaction_INT_UPT_DEL(rWeedMIID, txtWTQuantity, txtWTDays, ddlPeriod, ddlPlanting, "", "Aslam", MudarApp.Update, Convert.ToInt32(hfWMITId));
                }
            }
        }
    }
    #region Private Methods 
    private void BindFarmerList()
    {
        //gvFarmer.DataSource = farmerObj.ApprovedFamer_Inspection(true);
        //gvFarmer.DataBind();
    }
    private void BindYears()
    {
        //ListItemCollection items = MudarApp.BindYear();
        //foreach (ListItem item in items)
        //    ddlYear.Items.Add(item);
        //ddlYear.SelectedValue = DateTime.Now.Year.ToString();
        DataTable Seasond = cpObj.GetSeasonDetails();
        if (Seasond.Rows.Count > 0)
        {
            ddlYear.DataSource = Seasond.DefaultView.ToTable(true, "SeasonYear");
            ddlYear.DataTextField = "SeasonYear";
            ddlYear.DataValueField = "SeasonYear";
            ddlYear.DataBind();
            ddlYear.Items.Insert(0, MudarApp.AddListItem());
            ddlYear.SelectedValue = DateTime.Now.Year.ToString();
            
        }
    }
    private void BindProductList()
    {
        DataTable dtsProduct =farmingObj.GetProductDetailsByYear(ddlYear.SelectedValue);
        if (dtsProduct.Rows.Count > 0)
        {
            DataRow[] drseason = dtsProduct.Select("seasonName like '" + ddlSeason.SelectedItem.Text + "'");
            DataTable dtProduct = dtsProduct.Clone();
            foreach (DataRow dr in drseason)
                dtProduct.ImportRow(dr);
            gvProduct.DataSource = dtProduct;
            gvProduct.DataBind();
            seasonID = Convert.ToInt32(ddlSeason.SelectedValue.ToString());
        }
        else
        {
            Response.Write("<script>alert('!!!! No Data Found !!!!!');</script>");
            gvProduct.DataSource = null;
            gvProduct.DataBind();
        }
    }
    private void BindPlantingInfo()
    {
        DataTable dt = farmingObj.GetPlantingInfo(farmerID, productID.ToString(), seasonID.ToString());
        if (dt.Rows.Count > 0)
        {
            txtPIBill.Text = dt.Rows[0]["PlantingBill_Date"].ToString();
            txtPIsource.Text = dt.Rows[0]["PlantingSource"].ToString();
            txtPISeedVariety.Text = dt.Rows[0]["PlantingSeedVariety"].ToString();
            txtPISeedTreatment.Text = dt.Rows[0]["PlantingSeedTreatMent"].ToString();
            txtPIQuantity.Text = dt.Rows[0]["PlantingQuantity"].ToString();
            hfPIID.Value = dt.Rows[0]["PlantingID"].ToString();
        }
    }
    private void BindInputInfo()
    {
        DataSet ds = new DataSet();
        ds = farmingObj.GetInputInfo(farmerID, productID.ToString(), seasonID.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.Add("S_InputMID");
            dlInputInfo.DataSource = ds.Tables[0];
            dlInputInfo.DataBind();

            //Bind Inner grid
            if (dlInputInfo.Items.Count > 0)
            {
                //ds.Tables[1].Columns.Add("S_InputMID");

                for (int count = 0; count < dlInputInfo.Items.Count; count++)
                {
                    DataList innerDL = (dlInputInfo.Items[count].FindControl("dlTransaction") as DataList);
                    string InputMID = dlInputInfo.DataKeys[count].ToString();
                    DataTable dt = ds.Tables[1].Clone();
                    DataRow[] drs = ds.Tables[1].Select("S_InputMID like '" + InputMID + "'");

                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);
                    }
                    else
                    {
                        DataRow drc = dt.NewRow();
                        dt.Rows.Add(drc);
                        dt.Rows[0]["S_InputMID"] = InputMID;
                    }
                    innerDL.DataSource = dt;
                    innerDL.DataBind();
                    for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                    {
                        string hfInputMTId = (innerDL.Items[Tcount].FindControl("hfInputMTId") as HiddenField).Value;
                        if (!string.IsNullOrEmpty(hfInputMTId))
                        {
                            DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlPeriod") as DropDownList);
                            (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["IMPeriod"].ToString())).Selected = true;
                            DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlPlanting") as DropDownList);
                            (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["IMPlanting"].ToString())).Selected = true;
                        }
                    }
                }
            }
        }
        else
        {
            DataRow drp = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(drp);
            ds.Tables[0].Rows[0]["S_InputMID"] = "0$N";
            dlInputInfo.DataSource = ds.Tables[0];
            dlInputInfo.DataBind();


            DataRow drc = ds.Tables[1].NewRow();
            ds.Tables[1].Rows.Add(drc);
            ds.Tables[1].Rows[0]["S_InputMID"] = "0$N";

            DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
            innerDL.DataSource = ds.Tables[1];
            innerDL.DataBind();
        }
        Session["InputInfo"] = ds;
    }
    private void BindDisease()
    {
        DataSet ds = new DataSet();
        ds = farmingObj.GetDisease(farmerID, productID.ToString(), seasonID.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.Add("S_InputMID");
            dlDisease.DataSource = ds.Tables[0];
            dlDisease.DataBind();

            //Bind Inner grid
            if (dlDisease.Items.Count > 0)
            {
                //ds.Tables[1].Columns.Add("S_InputMID");

                for (int count = 0; count < dlDisease.Items.Count; count++)
                {
                    DataList innerDL = (dlDisease.Items[count].FindControl("dlDTransaction") as DataList);
                    string DiseaseMID = dlDisease.DataKeys[count].ToString();
                    DataTable dt = ds.Tables[1].Clone();
                    DataRow[] drs = ds.Tables[1].Select("S_DiseaseMID like '" + DiseaseMID + "'");

                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);
                    }
                    else
                    {
                        DataRow drc = dt.NewRow();
                        dt.Rows.Add(drc);
                        dt.Rows[0]["S_DiseaseMID"] = DiseaseMID;
                    }
                    innerDL.DataSource = dt;
                    innerDL.DataBind();
                    for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                    {
                        string hfInputMTId = (innerDL.Items[Tcount].FindControl("hfDMITId") as HiddenField).Value;
                        if (!string.IsNullOrEmpty(hfInputMTId))
                        {
                            DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlDTPeriod") as DropDownList);
                            (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["DMIT_Period"].ToString())).Selected = true;
                            DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlDTPlanting") as DropDownList);
                            (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["DMIT_Planting"].ToString())).Selected = true;
                        }
                    }
                }
            }
        }
        else
        {
            DataRow drp = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(drp);
            ds.Tables[0].Rows[0]["S_DiseaseMID"] = "0$N";
            dlDisease.DataSource = ds.Tables[0];
            dlDisease.DataBind();
            

            DataRow drc = ds.Tables[1].NewRow();
            ds.Tables[1].Rows.Add(drc);
            ds.Tables[1].Rows[0]["S_DiseaseMID"] = "0$N";

            DataList innerDL = (dlDisease.Items[0].FindControl("dlDTransaction") as DataList);
            innerDL.DataSource = ds.Tables[1];
            innerDL.DataBind();
        }
        Session["Disease"] = ds;
    }
    private void BindInsectInfo()
    {
        DataSet ds = new DataSet();
        ds = farmingObj.GetInsectInfo(farmerID, productID.ToString(), seasonID.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.Add("S_InputMID");
            dlInsect.DataSource = ds.Tables[0];
            dlInsect.DataBind();

            //Bind Inner grid
            if (dlInsect.Items.Count > 0)
            {
                //ds.Tables[1].Columns.Add("S_InputMID");

                for (int count = 0; count < dlInsect.Items.Count; count++)
                {
                    DataList innerDL = (dlInsect.Items[count].FindControl("dlITransaction") as DataList);
                    string InsectMIID = dlInsect.DataKeys[count].ToString();
                    DataTable dt = ds.Tables[1].Clone();
                    DataRow[] drs = ds.Tables[1].Select("S_InsectMIID like '" + InsectMIID + "'");

                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);
                    }
                    else
                    {
                        DataRow drc = dt.NewRow();
                        dt.Rows.Add(drc);
                        dt.Rows[0]["S_InsectMIID"] = InsectMIID;
                    }
                    innerDL.DataSource = dt;
                    innerDL.DataBind();
                    for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                    {
                        string hfInMITId = (innerDL.Items[Tcount].FindControl("hfInMITId") as HiddenField).Value;
                        if (!string.IsNullOrEmpty(hfInMITId))
                        {
                            DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlITPeriod") as DropDownList);
                            (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["InsectMPeriod"].ToString())).Selected = true;
                            DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlITPlanting") as DropDownList);
                            (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["InsectMPlanting"].ToString())).Selected = true;
                        }
                    }
                }
            }
        }
        else
        {
            DataRow drp = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(drp);
            ds.Tables[0].Rows[0]["S_InsectMIID"] = "0$N";
            dlInsect.DataSource = ds.Tables[0];
            dlInsect.DataBind();


            DataRow drc = ds.Tables[1].NewRow();
            ds.Tables[1].Rows.Add(drc);
            ds.Tables[1].Rows[0]["S_InsectMIID"] = "0$N";

            DataList innerDL = (dlInsect.Items[0].FindControl("dlITransaction") as DataList);
            innerDL.DataSource = ds.Tables[1];
            innerDL.DataBind();
        }
        Session["Insect"] = ds;
    }
    private void BindPestInfo()
    {
        DataSet ds = new DataSet();
        ds = farmingObj.GetPestInfo(farmerID, productID.ToString(), seasonID.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.Add("S_InputMID");
            dlPest.DataSource = ds.Tables[0];
            dlPest.DataBind();

            //Bind Inner grid
            if (dlPest.Items.Count > 0)
            {
                //ds.Tables[1].Columns.Add("S_InputMID");

                for (int count = 0; count < dlPest.Items.Count; count++)
                {
                    DataList innerDL = (dlPest.Items[count].FindControl("dlPTransaction") as DataList);
                    string PestMIID = dlPest.DataKeys[count].ToString();
                    DataTable dt = ds.Tables[1].Clone();
                    DataRow[] drs = ds.Tables[1].Select("S_PestMIID like '" + PestMIID + "'");

                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);
                    }
                    else
                    {
                        DataRow drc = dt.NewRow();
                        dt.Rows.Add(drc);
                        dt.Rows[0]["S_PestMIID"] = PestMIID;
                    }
                    innerDL.DataSource = dt;
                    innerDL.DataBind();
                    for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                    {
                        string hfPMITId = (innerDL.Items[Tcount].FindControl("hfPMITId") as HiddenField).Value;
                        if (!string.IsNullOrEmpty(hfPMITId))
                        {
                            DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlPTPeriod") as DropDownList);
                            (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["PMIT_Period"].ToString())).Selected = true;
                            DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlPTPlanting") as DropDownList);
                            (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["PMIT_Planting"].ToString())).Selected = true;
                        }
                    }
                }
            }
        }
        else
        {
            DataRow drp = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(drp);
            ds.Tables[0].Rows[0]["S_PestMIID"] = "0$N";
            dlPest.DataSource = ds.Tables[0];
            dlPest.DataBind();


            DataRow drc = ds.Tables[1].NewRow();
            ds.Tables[1].Rows.Add(drc);
            ds.Tables[1].Rows[0]["S_PestMIID"] = "0$N";

            DataList innerDL = (dlPest.Items[0].FindControl("dlPTransaction") as DataList);
            innerDL.DataSource = ds.Tables[1];
            innerDL.DataBind();
        }
        Session["Pest"] = ds;
    }
    private void BindWeedInfo()
    {
        DataSet ds = new DataSet();
        ds = farmingObj.GetWeedInfo(farmerID, productID.ToString(), seasonID.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.Add("S_InputMID");
            dlWeed.DataSource = ds.Tables[0];
            dlWeed.DataBind();

            //Bind Inner grid
            if (dlPest.Items.Count > 0)
            {
                //ds.Tables[1].Columns.Add("S_InputMID");

                for (int count = 0; count < dlWeed.Items.Count; count++)
                {
                    DataList innerDL = (dlWeed.Items[count].FindControl("dlWTransaction") as DataList);
                    string WeedMIID = dlWeed.DataKeys[count].ToString();
                    DataTable dt = ds.Tables[1].Clone();
                    DataRow[] drs = ds.Tables[1].Select("S_WeedMIID like '" + WeedMIID + "'");

                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);
                    }
                    else
                    {
                        DataRow drc = dt.NewRow();
                        dt.Rows.Add(drc);
                        dt.Rows[0]["S_WeedMIID"] = WeedMIID;
                    }
                    innerDL.DataSource = dt;
                    innerDL.DataBind();
                    for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                    {
                        string hfWMITId = (innerDL.Items[Tcount].FindControl("hfWMITId") as HiddenField).Value;
                        if (!string.IsNullOrEmpty(hfWMITId))
                        {
                            DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlWTPeriod") as DropDownList);
                            (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["WMIT_Period"].ToString())).Selected = true;
                            DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlWTPlanting") as DropDownList);
                            (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["WMIT_Planting"].ToString())).Selected = true;
                        }
                    }
                }
            }
        }
        else
        {
            DataRow drp = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(drp);
            ds.Tables[0].Rows[0]["S_WeedMIID"] = "0$N";
            dlWeed.DataSource = ds.Tables[0];
            dlWeed.DataBind();


            DataRow drc = ds.Tables[1].NewRow();
            ds.Tables[1].Rows.Add(drc);
            ds.Tables[1].Rows[0]["S_WeedMIID"] = "0$N";

            DataList innerDL = (dlWeed.Items[0].FindControl("dlWTransaction") as DataList);
            innerDL.DataSource = ds.Tables[1];
            innerDL.DataBind();
        }
        Session["Weed"] = ds;
    }
    private void BindWaterInfo()
    {

        DataSet ds = new DataSet();
        ds = farmingObj.GetWaterInfo(farmerID, productID.ToString(), seasonID.ToString());

        if (ds.Tables[0].Rows.Count > 0)
        {
            //ds.Tables[0].Columns.Add("S_InputMID");
            dlWater.DataSource = ds.Tables[0];
            dlWater.DataBind();

            //Bind Inner grid
            if (dlWater.Items.Count > 0)
            {
                //ds.Tables[1].Columns.Add("S_InputMID");

                for (int count = 0; count < dlWater.Items.Count; count++)
                {
                    DataList innerDL = (dlWater.Items[count].FindControl("dlWaTransaction") as DataList);
                    string WaterMID = dlWater.DataKeys[count].ToString();
                    DataTable dt = ds.Tables[1].Clone();
                    DataRow[] drs = ds.Tables[1].Select("S_WaterMID like '" + WaterMID + "'");

                    if (drs.Length > 0)
                    {
                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);
                    }
                    else
                    {
                        DataRow drc = dt.NewRow();
                        dt.Rows.Add(drc);
                        dt.Rows[0]["S_WaterMID"] = WaterMID;
                    }
                    innerDL.DataSource = dt;
                    innerDL.DataBind();
                    for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                    {
                        string hfWaITTId = (innerDL.Items[Tcount].FindControl("hfWaITTId") as HiddenField).Value;
                        if (!string.IsNullOrEmpty(hfWaITTId))
                        {
                            DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlWaPeriod") as DropDownList);
                            (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["WITT_Period"].ToString())).Selected = true;
                            DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlWaPlanting") as DropDownList);
                            (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["WITT_Planting"].ToString())).Selected = true;
                        }
                    }
                }
            }
        }
        else
        {
            DataRow drp = ds.Tables[0].NewRow();
            ds.Tables[0].Rows.Add(drp);
            ds.Tables[0].Rows[0]["S_WaterMID"] = "0$N";
            dlWater.DataSource = ds.Tables[0];
            dlWater.DataBind();


            DataRow drc = ds.Tables[1].NewRow();
            ds.Tables[1].Rows.Add(drc);
            ds.Tables[1].Rows[0]["S_WaterMID"] = "0$N";

            DataList innerDL = (dlWater.Items[0].FindControl("dlWaTransaction") as DataList);
            innerDL.DataSource = ds.Tables[1];
            innerDL.DataBind();
        }
        Session["WaterInfo"] = ds;
    }
    private void Clear()
    {
        //Planting Info
        txtPIBill.Text = string.Empty;
        txtPIsource.Text = string.Empty;
        txtPISeedVariety.Text = string.Empty;
        txtPISeedTreatment.Text = string.Empty;
        txtPIQuantity.Text = string.Empty;
        hfPIID.Value = "0";

        //
    }
    #endregion
    protected void btnWater_Click(object sender, EventArgs e)
    {
        for (int index = 0; index < dlWater.Items.Count; index++)
        {
            bool result = false;
            string WaterMID = dlWater.DataKeys[index].ToString();
            int rWaterMID = 0;
            string txtWISource = (dlWater.Items[index].FindControl("txtWISource") as TextBox).Text;
            string txtWIOrganicF = (dlWater.Items[index].FindControl("txtWIOrganicF") as TextBox).Text;
            string txtWIFCSource = (dlWater.Items[index].FindControl("txtWIFCSource") as TextBox).Text;
            string txtWIFCWaterFlow = (dlWater.Items[index].FindControl("txtWIFCWaterFlow") as TextBox).Text;

            if (WaterMID.Contains('N'))
            {
                WaterMID = WaterMID.Replace("$N", "");
                result = farmingObj.WaterInfo_INT_UPT_DEL(farmerID, productID, seasonID, ddlYear.SelectedValue, txtWISource, txtWIOrganicF, txtWIFCSource, txtWIFCWaterFlow, "Aslam", "", MudarApp.Insert, ref rWaterMID, Convert.ToInt32(WaterMID));
            }
            else
            {
                result = farmingObj.WaterInfo_INT_UPT_DEL(farmerID, productID, seasonID, ddlYear.SelectedValue, txtWISource, txtWIOrganicF, txtWIFCSource, txtWIFCWaterFlow, "", "Aslam", MudarApp.Update, ref rWaterMID, Convert.ToInt32(WaterMID));
            }
            DataList dltran = (dlWater.Items[index].FindControl("dlWaTransaction") as DataList);
            for (int indexT = 0; indexT < dltran.Items.Count; indexT++)
            {
                string hfWaITTId = (dltran.Items[indexT].FindControl("hfWaITTId") as HiddenField).Value;
                string txtIrrigation = (dltran.Items[indexT].FindControl("txtIrrigation") as TextBox).Text;
                string txtWaDays = (dltran.Items[indexT].FindControl("txtWaDays") as TextBox).Text;
                string ddlITPeriod = (dltran.Items[indexT].FindControl("ddlWaPeriod") as DropDownList).Text;
                string ddlITPlanting = (dltran.Items[indexT].FindControl("ddlWaPlanting") as DropDownList).Text;
                if (string.IsNullOrEmpty(hfWaITTId))
                {
                    result = farmingObj.WaterTransaction_INT_UPT_DEL(rWaterMID, txtIrrigation, txtWaDays, ddlITPeriod, ddlITPlanting, "Aslam", "", MudarApp.Insert, 0);
                }
                else
                {
                    result = farmingObj.WaterTransaction_INT_UPT_DEL(rWaterMID, txtIrrigation, txtWaDays, ddlITPeriod, ddlITPlanting, "", "Aslam", MudarApp.Update, Convert.ToInt32(hfWaITTId));
                }
            }
        }
        BindWaterInfo();
    }
    protected void dlWater_ItemCommand(object source, DataListCommandEventArgs e)
    {
        int index = e.Item.ItemIndex;
        switch (e.CommandName)
        {
            case "AddWater":
                {
                    DataSet ds = (DataSet)Session["WaterInfo"];
                    index = ds.Tables[0].Rows.Count;
                    //ADD NEW ROWS
                    DataRow drp = ds.Tables[0].NewRow();
                    ds.Tables[0].Rows.Add(drp);
                    ds.Tables[0].Rows[ds.Tables[0].Rows.Count - 1]["S_WaterMID"] = index + "$N";
                    dlWater.DataSource = ds.Tables[0];
                    dlWater.DataBind();


                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_WaterMID"] = index + "$N";

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlWater.Items.Count; count++)
                    {
                        DataList innerDL = (dlWater.Items[count].FindControl("dlWaTransaction") as DataList);
                        string WaterMID = dlWater.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_WaterMID like '" + WaterMID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                    }
                    //ds = (DataSet)Session["InputInfo"];
                }
                break;
            case "AddTWater":
                {
                    string input = dlWater.DataKeys[index].ToString();
                    DataSet ds = (DataSet)Session["WaterInfo"];
                    DataRow drc = ds.Tables[1].NewRow();
                    ds.Tables[1].Rows.Add(drc);
                    ds.Tables[1].Rows[ds.Tables[1].Rows.Count - 1]["S_WaterMID"] = input;

                    //DataList innerDL = (dlInputInfo.Items[0].FindControl("dlTransaction") as DataList);
                    //innerDL.DataSource = ds.Tables[1];
                    //innerDL.DataBind();
                    for (int count = 0; count < dlWater.Items.Count; count++)
                    {
                        DataList innerDL = (dlWater.Items[count].FindControl("dlWaTransaction") as DataList);
                        string WaterMID = dlWater.DataKeys[count].ToString();
                        DataTable dt = ds.Tables[1].Clone();
                        DataRow[] drs = ds.Tables[1].Select("S_WaterMID like '" + WaterMID + "'");

                        foreach (DataRow dr in drs)
                            dt.ImportRow(dr);

                        innerDL.DataSource = dt;
                        innerDL.DataBind();
                        for (int Tcount = 0; Tcount < innerDL.Items.Count; Tcount++)
                        {
                            string hfWaITTId = (innerDL.Items[Tcount].FindControl("hfWaITTId") as HiddenField).Value;
                            if (!string.IsNullOrEmpty(hfWaITTId))
                            {
                                DropDownList ddlPeriod = (innerDL.Items[Tcount].FindControl("ddlWaPeriod") as DropDownList);
                                (ddlPeriod.Items.FindByValue(dt.Rows[Tcount]["WITT_Period"].ToString())).Selected = true;
                                DropDownList ddlPlanting = (innerDL.Items[Tcount].FindControl("ddlWaPlanting") as DropDownList);
                                (ddlPlanting.Items.FindByValue(dt.Rows[Tcount]["WITT_Planting"].ToString())).Selected = true;
                            }
                        }
                    }
                }
                break;
        }

    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        MultiView1.ActiveViewIndex = 0;
    }
} 
    
