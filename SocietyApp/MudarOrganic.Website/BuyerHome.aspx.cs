using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Configuration;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.IO;

public partial class BuyerHome : System.Web.UI.Page
{
    Buyer_BL BBL = new Buyer_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string reg_path = HttpContext.Current.Request.Url.AbsolutePath;
            if (reg_path == WebConfigurationManager.AppSettings["BuyerReg"].ToString())
            {

            }
            else if (((DataTable)Session["dtLoginDetails"]).Rows.Count > 0)
            {
                string path = HttpContext.Current.Request.Url.AbsolutePath;
                string[] s = path.Split('/');
                //if (s[2].ToString().ToLower() == "farmer")
                //{

                //}
            }
            else
            {
                Response.Redirect("~/login.aspx");
            }
        }
        catch (Exception ex)
        {
            Response.Redirect("~/login.aspx?error=" + ex.Message.ToString());
        }
       
        if (!Page.IsPostBack)
        {
            DataTable dtBuyer = new DataTable();
            dtBuyer= BBL.BuyerDetails(Session["BuyerId"].ToString());
            if (dtBuyer.Rows.Count > 0)
            {
                Session["BuyerCountry_S"] = dtBuyer.Rows[0]["CCountry"].ToString().ToUpper(); 
                
                if (dtBuyer.Rows[0]["CCountry"].ToString().ToUpper() == "INDIA")
                {                    
                    divEuropean.Visible = false;
                }
                else
                {
                    divIndian.Visible = false;
                }
            }
            DirectoryInfo di = new DirectoryInfo(Server.MapPath(WebConfigurationManager.AppSettings["BuyerLogo"].ToString()));
            FileInfo[] TXTFiles = di.GetFiles(Session["BuyerId"].ToString() + ".*");
            if (TXTFiles.Length > 0)
            {
                imgBuyerLogo.ImageUrl = WebConfigurationManager.AppSettings["BuyerLogo"].ToString() + TXTFiles[0].Name;
            }
        }
    }
    protected void btnPrices_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/BuyerPrice.aspx");
    }
    protected void btnTrackPO_Click(object sender, EventArgs e)
    {
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
    protected void btnComplaints_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Buyer/BuyerComplaint.aspx");
    }
   
}
