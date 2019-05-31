using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using System.Data;


/// <summary>
/// Summary description for MudarApp
/// </summary>
public class MudarApp : System.Web.UI.MasterPage
{
    public const int Insert = 1;
    public const int Update = 2;
    public const int Delete = 3;
    public static int OrderHistory = 0;
    Farmer_BL farmerobj = new Farmer_BL();
    Order_BL orderObj = new Order_BL();
    Invoice_BL invoiceObj = new Invoice_BL();
    
    public MudarApp()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static ListItem AddListItem(string itemText, string itemValue)
    {
        ListItem selectitem = new ListItem();
        selectitem.Text = itemText;
        selectitem.Value = itemValue;
        selectitem.Selected = true;
        return selectitem;
    }
    /// <summary>
    /// Create By  : Shaik Aslam
    /// Date       : 7/3/2012
    /// Discribtion: Add this to all Drop down @ index 0
    /// </summary>
    /// <returns></returns>
    public static ListItem AddListItem()
    {
        ListItem selectitem = new ListItem();
        selectitem.Text = string.Empty;
        selectitem.Value = string.Empty;
        selectitem.Selected = true;
        return selectitem;
    }
    public static ListItem AddListItemWithDefaultValue()
    {
        ListItem selectitem = new ListItem();
        selectitem.Text = "Select";
        selectitem.Value = string.Empty;
        selectitem.Selected = true;
        return selectitem;
    }
    public static ListItem AddListItemWithDefaultValueNotSelected()
    {
        ListItem selectitem = new ListItem();
        selectitem.Text = "Select";
        selectitem.Value = string.Empty;
        selectitem.Selected = false;
        return selectitem;
    }
    public string farmercode(string city, string state)
    {
        string farmercode = string.Empty;
        if (!string.IsNullOrEmpty(city) && !string.IsNullOrEmpty(state))
        {
            farmercode = state.Substring(0, 1).ToUpper() + city.Substring(0, 1).ToUpper();
            farmercode += (farmerobj.FarmerCount(farmercode) + 1);
        }
        return farmercode;
    }
    public static ListItemCollection BindYear()
    {
        ListItemCollection items = new ListItemCollection();
        int startYear = Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["MudarStartYear"]);
        int currentYear = DateTime.Now.Year;

        //for (int count = 0; count < Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["SeasonYearCount"].ToString()); count++)
        //{
        //    ListItem item = new ListItem();
        //    item.Text = (Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["SeasonStartYear"].ToString()) + count).ToString();
        //    item.Value = (Convert.ToInt32(System.Web.Configuration.WebConfigurationManager.AppSettings["SeasonStartYear"].ToString()) + count).ToString();
        //    items.Add(item);
        //}

        while (startYear <= currentYear)
        {
            ListItem item = new ListItem();
            item.Text = startYear.ToString();
            item.Value = startYear.ToString();
            items.Add(item);
            startYear++;
        }
        items.Insert(0, AddListItemWithDefaultValueNotSelected());
        return items;
    }
    public string GeneratePOno()
    {
        Random ran = new Random();
        int count = 0;
        DataTable dt = orderObj.ReturnPOList("PO" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString());
        count = dt.Rows.Count + 1;
        return "PO" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + count.ToString();
    }
    #region New code for Buper-PO,Branch-PO,Lotnumber(after Blending)
    //Buyer PO Number Generate
    public string SystemGenerateBuyerPO()
    {
        int count = 0;
        DataTable dt = orderObj.ReturnPOList("B.");
        count = dt.Rows.Count + 1;
        return "B." + count.ToString();
    }
    public string SystemGenerateBuyerLotSample()
    {
        int count = 0;
        DataTable dt = orderObj.ReturnLotSampleList("LS.");
        count = dt.Rows.Count + 1;
        return "LS." + count.ToString();
    }
    //Branch PO Number Generate
    public string GenerateBPOno()
    {
        Random ran = new Random();
        int count = 0;
        DataTable dt = orderObj.ReturnBPOList("Pg.");
        count = dt.Rows.Count + 1;
        return "Pg." + count.ToString();
    }
    // lot sample(pre order screen) Generate
    public string GenerateLotNumber(string ProductCode, string finalcialyear)
    {
        int count = 0;
        DataSet ds = orderObj.ReturnBatchList(ProductCode + finalcialyear + "0");
        foreach (DataTable dt in ds.Tables)
            count += dt.Rows.Count;
        count += 1;
        return ProductCode + finalcialyear + "0" + count.ToString();
    }
    public string GenerateFreezeLotnumber(string ProductCode, string finalcialyear)
    {
        int count = 0;
        DataTable ds = orderObj.ReturnFreezeBatch(ProductCode + finalcialyear + "0");
        count += (ds.Rows.Count + 1);
        return ProductCode + finalcialyear + "0" + count.ToString();
    }
    #endregion
    public string GenerateBatchID(int ProductID)
    {
        Random ran = new Random();
        int count = 0;
        DataSet ds = orderObj.ReturnBatchList("B" + ProductID + DateTime.Now.Year.ToString());
        foreach (DataTable dt in ds.Tables)
            count += dt.Rows.Count;
        count += 1;
        return "B" + ProductID + DateTime.Now.Year.ToString() + count.ToString();
    }
    public string GenerateBBatchID(int ProductID)
    {
        Random ran = new Random();
        int count = 0;
        DataTable dt = orderObj.ReturnBBatchList("BB" + ProductID + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString());
        count = dt.Rows.Count + 1;
        return "BB" + ProductID + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + count.ToString();
    }
    public string GenerateFBatchID(int ProductID)
    {
        Random ran = new Random();
        int count = 0;
        DataTable dt = orderObj.ReturnFBatchList("FB" + ProductID + DateTime.Now.Year.ToString());
        count = dt.Rows.Count + 1;
        return "FB" + ProductID + DateTime.Now.Year.ToString() + count.ToString();
    }
    public string GenerateInvoiceID()
    {
        int count = 0;
        DataTable dt = invoiceObj.ReturnInvoiceList("IN-" + "1516" + count.ToString());
        //return ProductCode + finalcialyear + "0" + count.ToString();
        //count = dt.Rows.Count + 1;
        return "IN-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + count.ToString();
    }
    public string GenerateInvoiceID(string finalcialyear)
    {
        int count = 0;
        DataTable dt = invoiceObj.ReturnInvoiceList("IN-" + "1516" + count.ToString());
        count = dt.Rows.Count + 1;
        return "IN-" + DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + count.ToString();
    }
    public DateTime GenerateRandomDate(string Sdate, string Edate)
    {
        Random ran = new Random();
        int range = ((TimeSpan)(Convert.ToDateTime(Edate) - Convert.ToDateTime(Sdate))).Days;
        return Convert.ToDateTime(Sdate).AddDays(ran.Next(range));
    }
    public static int RandomNumber(int min, int max)
    {
        Random random = new Random();
        return random.Next(min, max);
    }
    public static decimal RandomNumber(decimal min, decimal max)
    {
        Random random = new Random();
        return Convert.ToDecimal(random.NextDouble() * Convert.ToDouble(max - min) + Convert.ToDouble(min));
    }
}
