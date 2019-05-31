using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;
using System.Data;
using System.IO;
using Ionic.Zip;
using System.Web.Configuration;

public partial class Orders_TrackOrder : System.Web.UI.Page
{
    Order_BL OrderTrackObj = new Order_BL();
    MudarUser mu = new MudarUser();
    Reports_BL reportObj = new Reports_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            divOrderTrack.Visible = false;
            divBuyerPO.Visible = false;
            divBuyerReports.Visible = false;
            if (MudarApp.OrderHistory > 0)
            {
                txtOrderID.Text = MudarApp.OrderHistory.ToString();
                BindTrackOrder();
                divOrderTrack.Visible = true;
                BindOrderReportPathdetails();
                MudarApp.OrderHistory = 0;
            }
            Visibility();
        }
    }
    private void BindTrackOrder()
    {
        if (!string.IsNullOrEmpty(txtOrderID.Text))
        {
            dlOrderTrack.DataSource = OrderTrackObj.OrderTrack(txtOrderID.Text);
            dlOrderTrack.DataBind();
            if (dlOrderTrack.Items.Count > 0)
            {
                string OrderID = (dlOrderTrack.Items[0].FindControl("lblOrderID") as Label).Text;
                gvProdcutList.DataSource = OrderTrackObj.BindOrderProductList(!string.IsNullOrEmpty(OrderID) ? Convert.ToInt32(OrderID) : 0);
                gvProdcutList.DataBind();
            }
            if ((dlOrderTrack.Items[0].FindControl("lblOrderStatus") as Label).Text == "HOLD")
                divBuyerPO.Visible = true;
        }
    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        divOrderTrack.Visible = true;
        BindTrackOrder();
        BindOrderReportPathdetails();
    }
    private void Visibility()
    {

        if (Session["RoleName_s"].ToString().ToLower() == LoginType.Buyer.ToLower())
        {
            divReports.Visible = false;
            divBuyerReports.Visible = true;
            
        }
    }
    private void BindOrderReportPathdetails()
    {
        string OrderID = txtOrderID.Text;

        DataTable dtORPD = reportObj.OrderReportsPathGetDetails(Convert.ToInt32(OrderID));
        if (dtORPD.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(dtORPD.Rows[0]["Invoice"].ToString()))
            {
                string[] path = dtORPD.Rows[0]["Invoice"].ToString().Split('/');
                string[] path2 = path[path.Length - 1].Split('.');
                string InvoiceID = path2[0].ToString();
                hlInvoice.Text = InvoiceID;
            }
            hlInvoice.NavigateUrl = dtORPD.Rows[0]["Invoice"].ToString();
            hlInvoice_B.NavigateUrl = dtORPD.Rows[0]["Invoice"].ToString();
            hlPacking.NavigateUrl = dtORPD.Rows[0]["Packing"].ToString();
            hlPacking_B.NavigateUrl = dtORPD.Rows[0]["Packing"].ToString();
            hlCoverLetter.NavigateUrl = dtORPD.Rows[0]["Cover_Letter"].ToString();
            hlFIRCover.NavigateUrl = dtORPD.Rows[0]["Fir_Cover_Letter"].ToString();
            hlHazSea.NavigateUrl = dtORPD.Rows[0]["Non_Haz_Sea"].ToString();
            hlHazAir.NavigateUrl = dtORPD.Rows[0]["Non_Haz_Air"].ToString();
            bindReports(dtORPD);
        }
    }
    private void bindReports(DataTable dtORPD)
    {
        int pCount = gvProdcutList.Rows.Count ;
        DataTable report = new DataTable();

        report.Columns.Add("ProductName");
        report.Columns.Add("PP");
        report.Columns.Add("AR");
        report.Columns.Add("SP");
        report.Columns.Add("CRY");
        report.Columns.Add("CRY_P");
        report.Columns.Add("BO");
        report.Columns.Add("LABEL");

        for (int count = 0; count < pCount; count++)
        {
            string[] PP = dtORPD.Rows[0]["PP"].ToString().Split('$');
            string[] LABEL = dtORPD.Rows[0]["LABEL"].ToString().Split('$');
            string[] AR = dtORPD.Rows[0]["AR"].ToString().Split('$');
            string[] SP = dtORPD.Rows[0]["SP"].ToString().Split('$');
            string[] CRY = dtORPD.Rows[0]["CRY"].ToString().Split('$');
            string[] CRY_P = dtORPD.Rows[0]["CRY_P"].ToString().Split('$');
            string[] BO = dtORPD.Rows[0]["BO"].ToString().Split('$');
            DataRow drNew = report.NewRow();
            drNew["ProductName"] = gvProdcutList.Rows[count].Cells[2].Text;
            drNew["PP"] = PP.Length > 1 ? PP[count].ToString() : string.Empty;
            drNew["AR"] = AR.Length > 1 ? AR[count].ToString() : string.Empty;
            drNew["SP"] = SP.Length > 1 ? SP[count].ToString() : string.Empty;
            drNew["CRY"] = CRY.Length > 1 ? CRY[count].ToString() : string.Empty;
            drNew["CRY_P"] = CRY_P.Length > 1 ? CRY_P[count].ToString() : string.Empty;
            drNew["BO"] = BO.Length > 1 ? BO[count].ToString() : string.Empty;
            drNew["LABEL"] = LABEL.Length > 1 ? LABEL[count].ToString() : string.Empty;

            report.Rows.Add(drNew);            
        }

        gvReports.DataSource = report;
        gvReports.DataBind();
    }
    protected void dlOrderTrack_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string command = e.CommandName;
        int Index = e.Item.ItemIndex;
        switch (command)
        {
            case "Download":
                {
                    string str = ((HyperLink)dlOrderTrack.Items[Index].FindControl("hlPDF")).NavigateUrl.ToString();
                    WebClient req = new WebClient();
                    HttpResponse response = HttpContext.Current.Response;
                    response.Clear();
                    response.ClearContent();
                    response.ClearHeaders();
                    response.Buffer = true;
                    response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(str) + "\"");
                    byte[] data = req.DownloadData(Server.MapPath(str));
                    response.BinaryWrite(data);
                    response.End();
                }
                break;
        }
    }
    protected void btnDownloadAll_Click(object sender, EventArgs e)
    {
        ZipFile multipleFilesAsZipFile = new ZipFile();
        Response.AddHeader("Content-Disposition", "attachment; filename=\""+  txtOrderID.Text + ".zip");
        Response.ContentType = "application/zip";
        if(!string.IsNullOrEmpty(hlInvoice.NavigateUrl))
        {
            multipleFilesAsZipFile.AddFile(Server.MapPath(hlInvoice.NavigateUrl),string.Empty);
        }
        if (!string.IsNullOrEmpty(hlPacking.NavigateUrl))
        {
            multipleFilesAsZipFile.AddFile(Server.MapPath(hlPacking.NavigateUrl), string.Empty);
        }
        if (!string.IsNullOrEmpty(hlCoverLetter.NavigateUrl))
        {
            multipleFilesAsZipFile.AddFile(Server.MapPath(hlCoverLetter.NavigateUrl), string.Empty);
        }
        if (!string.IsNullOrEmpty(hlFIRCover.NavigateUrl))
        {
            multipleFilesAsZipFile.AddFile(Server.MapPath(hlFIRCover.NavigateUrl), string.Empty);
        }
        if (!string.IsNullOrEmpty(hlHazSea.NavigateUrl))
        {
            multipleFilesAsZipFile.AddFile(Server.MapPath(hlHazSea.NavigateUrl), string.Empty);
        }
        if (!string.IsNullOrEmpty(hlHazAir.NavigateUrl))
        {
            multipleFilesAsZipFile.AddFile(Server.MapPath(hlHazAir.NavigateUrl), string.Empty);
        }
        multipleFilesAsZipFile.Save(Response.OutputStream);
    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        string orderid = txtOrderID.Text;
        if (FileUpload1.FileName.Length > 0)
            FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Attachments/") + FileUpload1.FileName);
        if (!string.IsNullOrEmpty(FileUpload1.FileName))
        {
            string newpath = mu.createfolder(orderid.ToString(), MudarUser.OrderPDF) ? WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/" + orderid.ToString() + "_" + FileUpload1.FileName : WebConfigurationManager.AppSettings["orderpdf"].ToString() + "/" + orderid.ToString() + "_" + FileUpload1.FileName;
            File.Move(Server.MapPath("~/Attachments/") + FileUpload1.FileName, Server.MapPath(newpath));
            OrderTrackObj.OrderDetails_UPD(newpath, "bhanu", txtPO.Text, Convert.ToInt32(orderid));
        }
    }
    protected void dlOrderTrack_ItemDataBound(object sender, DataListItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            string Status = drv.Row["OrderStatus"].ToString();
            if (Status == "NEW")
            {
                Label lbl = (Label)e.Item.FindControl("lblOrderStatus");
                lbl.ForeColor = System.Drawing.Color.Orange;
            }
            if (Status == "INPROCESS" || Status == "COLLECTING" || Status == "BLENDING")
            {
                Label lbl = (Label)e.Item.FindControl("lblOrderStatus");
                lbl.ForeColor = System.Drawing.Color.DarkGreen;
            }
            if (Status == "PACKING")
            {
                Label lbl = (Label)e.Item.FindControl("lblOrderStatus");
                lbl.ForeColor = System.Drawing.Color.OrangeRed;
            }
            if (Status == "SHIPPING")
            {
                Label lbl = (Label)e.Item.FindControl("lblOrderStatus");
                lbl.ForeColor = System.Drawing.Color.Orchid;
            }
            if (Status == "DISPATCH")
            {
                Label lbl = (Label)e.Item.FindControl("lblOrderStatus");
                lbl.ForeColor = System.Drawing.Color.DarkBlue;
            }
            if (Status == "CLOSE")
            {
                Label lbl = (Label)e.Item.FindControl("lblOrderStatus");
                lbl.ForeColor = System.Drawing.Color.Violet;
            }
        }
    }
}
