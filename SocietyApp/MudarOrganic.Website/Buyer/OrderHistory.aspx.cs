using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;
using System.IO;
using System.Web.Configuration;

public partial class Buyer_OrderHistory : System.Web.UI.Page
{
    Order_BL orderObj = new Order_BL();
    Buyer_BL buyerobj = new Buyer_BL();
    MudarUser mu = new MudarUser();
    string Pdf_path = string.Empty;
    string orderid = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnTrackPOBuyer();
            Session["OrderID"] = new object();
            BindGrid();
            txtPODate.Text = DateTime.Now.ToShortDateString();
        }
    }
    protected void dlOrderHistory_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string command = e.CommandName;
        int Index = e.Item.ItemIndex;
        var item = e.Item;
        switch (command)
        {
            case "Exp_Col":
                {
                    //Label test = item.FindControl("lblC_P") as Label;
                    ImageButton img = item.FindControl("ibtnNOExpColap") as ImageButton;
                    if (img.ImageUrl == "~/images/expand.JPG")
                    {
                        ((ImageButton)item.FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
                        ((GridView)item.FindControl("gvOrderHistory")).Visible = true;

                    }
                    else if (img.ImageUrl == "~/images/collapse.JPG")
                    {
                        ((ImageButton)item.FindControl("ibtnNOExpColap")).ImageUrl = "~/images/expand.JPG";
                        ((GridView)item.FindControl("gvOrderHistory")).Visible = false;

                    }
                }
                break;
            case "BranchOrder":
                {
                    //Session["sOrderID"] = Encrypt_Decrypt.Encrypt(((LinkButton)dlOrderHistory.Items[Index].FindControl("lbtnOrderID")).Text, true);
                    MudarApp.OrderHistory = Convert.ToInt32(((LinkButton)item.FindControl("lbtnOrderID")).Text);
                    Response.Redirect("~/Orders/TrackOrder.aspx");
                }
                break;
            case "LSDisplay":
                {
                    string str = ((HiddenField)item.FindControl("hlLS")).Value.ToString();
                    //ifOrderPdf.Attributes.Add("src", str);
                    //ifOrderPdf.Attributes.Add("src", "../Attachments/OrderPDF/1015/1015_PO201296.pdf");
                }
                break;
            case "LSDownload":
                {
                    string str = ((HiddenField)item.FindControl("hfLS")).Value.ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
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
                }
                break;
            case "Display":
                {
                    string str = ((HiddenField)item.FindControl("hfOrderPdf")).Value.ToString();

                    //ifOrderPdf.Attributes.Add("src", str);
                    //ifOrderPdf.Attributes.Add("src", "../Attachments/OrderPDF/1015/1015_PO201296.pdf");
                }
                break;
            case "Download":
                {
                    string str = ((HiddenField)item.FindControl("hfOrderPdf")).Value.ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
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
                }
                break;
            case "uploadpo":
                {
                    Session["OrderID"] = Convert.ToInt32(((LinkButton)item.FindControl("lbtnOrderID")).CommandArgument);
                    Button btnUpdate = (Button)e.Item.FindControl("btnUpdate");
                    //btnUpdate.ForeColor = System.Drawing.Color.Orange;
                    divplaceorder.Visible = true;
                }
                break;
        }
    }
    private void BindGrid()
    {
        if (!string.IsNullOrEmpty(Session["BuyerId"].ToString()))
        {
            DataTable dtOrder = orderObj.OrderbyBuyer(Session["BuyerId"].ToString());
            Session["OrderDt_s"] = dtOrder;
            if (dtOrder.Rows.Count > 0)
            {
                dlOrderHistory.DataSource = dtOrder;
                dlOrderHistory.DataBind();

                foreach (RepeaterItem dli in dlOrderHistory.Items)
                {
                    int OrderID = Convert.ToInt32(((LinkButton)dli.FindControl("lbtnOrderID")).CommandArgument);
                    ((ImageButton)dlOrderHistory.Items[dli.ItemIndex].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
                    string str = ((HiddenField)dlOrderHistory.Items[dli.ItemIndex].FindControl("hfBuyerPO")).Value.ToString();
                    string Lsstr = ((HiddenField)dlOrderHistory.Items[dli.ItemIndex].FindControl("hfLS")).Value.ToString();
                    if (!string.IsNullOrEmpty(Lsstr))
                    {
                        (dli.FindControl("hlLS") as HyperLink).Visible = true;
                        //(dli.FindControl("lbtnLS") as LinkButton).Visible = true;
                    }
                    // string Status = (dlOrderHistory.FindControl("hfBuyerPO") as HiddenField).Value.ToString();
                    if (!string.IsNullOrEmpty(str))
                    {
                        (dli.FindControl("hlBuyerPO") as HyperLink).Visible = true;
                        (dli.FindControl("btnUpdate") as Button).Visible = false;
                    }
                    (dli.FindControl("gvOrderHistory") as GridView).DataSource = orderObj.BindBuyerOrderProductList(OrderID);
                    (dli.FindControl("gvOrderHistory") as GridView).DataBind();
                }
            }
        }
    }
    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        //DataRow[] drs = ((DataTable)Session["OrderDt_s"]).Select(" OrderStatus = '" + ddlOrderStatus.SelectedValue + "'");

        DataView dv = new DataView(((DataTable)Session["OrderDt_s"]));
        dv.RowFilter = " OrderStatus = '" + ddlOrderStatus.SelectedValue + "'";
        if (ddlOrderStatus.SelectedValue.ToString().ToLower().Trim() == "all")
            dlOrderHistory.DataSource = (DataTable)Session["OrderDt_s"];
        else
            dlOrderHistory.DataSource = dv;
        dlOrderHistory.DataBind();
        foreach (RepeaterItem dli in dlOrderHistory.Items)
        {
            int OrderID = Convert.ToInt32(((LinkButton)dli.FindControl("lbtnOrderID")).CommandArgument);
            (dli.FindControl("gvOrderHistory") as GridView).DataSource = orderObj.OrderProductList(OrderID);
            (dli.FindControl("gvOrderHistory") as GridView).DataBind();
        }
    }
    protected void dlOrderHistory_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            string Status = drv.Row["OrderStatus"].ToString();
            if (Status == "NEW")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.Orange;
            }
            if (Status == "INPROCESS" || Status == "COLLECTING" || Status == "BLENDING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.DarkGreen;
            }
            if (Status == "PACKING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.OrangeRed;
            }
            if (Status == "SHIPPING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.Brown;
            }
            if (Status == "DISPATCH")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.DarkBlue;
            }
            if (Status == "INTRANSIT")
            {
                Label lblM = (Label)e.Item.FindControl("lblMSg");
                LinkButton lbtnO = (LinkButton)e.Item.FindControl("lbtnOrderID");
                dt1 = orderObj.GetAdminCommentDetails(lbtnO.Text);
                string Msg1 = dt1.Rows[0]["Comments"].ToString();
                if (Msg1.Contains("$"))
                {
                    lblM.Visible = true;
                    string[] po = Msg1.Split('$');
                    lblM.Text = po[1].ToString();
                    lblM.ForeColor = System.Drawing.Color.Red;
                }
            }
            if (Status == "CLOSE")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.Violet;
            }
        }
    }

    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    if (FileUpload1.HasFile)
    //    {
    //        orderid = Session["OrderID"].ToString();
    //        Pdf_path = WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/" + FileUpload1.FileName.ToString();
    //        FileUpload1.PostedFile.SaveAs(Server.MapPath(Pdf_path));
    //        FileUpload1.Attributes.Clear();
    //        FileUpload1.Dispose();
    //        hlBuyerPO.Visible = true;
    //        btncancel.Visible = true;
    //        btnFinish.Visible = true;
    //        btnDFinish.Visible = false;
    //        hlBuyerPO.NavigateUrl = Pdf_path;
    //        lblMsg.Text = string.Empty;
    //        btnUpload.Visible = false;
    //        lblUploadText.Text = Pdf_path;
    //    }
    //    else
    //    {
    //        lblMsg.Text = " *Please select a file to upload ";
    //        hlBuyerPO.Visible = false;
    //        btncancel.Visible = false;
    //    }
    //}
    protected void btnFinish_Click(object sender, EventArgs e)
    {
        bool result;
        var pdfpath = Session["Pdf_path"] as string;
        orderid = Session["OrderID"].ToString();
        if (!string.IsNullOrEmpty(pdfpath))
        {
            result = buyerobj.UpdateBuyerPOandPathDetails(Convert.ToInt32(orderid), txtPO.Text, pdfpath, txtcomments.Text);
            if (result)
            {
                //Session["OrderID"] = new object();
                //BindGrid();
                //divplaceorder.Visible = false;
                Response.Redirect("~/Buyer/OrderHistory.aspx");
            }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ////hlBuyerPODwn.Visible = false;
        //btncancel.Visible = false;
        ////btnUpload.Visible = true;
        ////btnDFinish.Visible = true;
        //btnFinish.Visible = false;
        //lblUploadText.Text = string.Empty;
        Response.Redirect("~/Buyer/OrderHistory.aspx");
    }

    protected void AsyncFileUpload1_UploadedComplete(object sender, AjaxControlToolkit.AsyncFileUploadEventArgs e)
    {
        if (AsyncFileUpload1.HasFile)
        {
            var virtualfilepath = WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/" + AsyncFileUpload1.FileName;
            orderid = Session["OrderID"].ToString();
            var folderpath = Server.MapPath(WebConfigurationManager.AppSettings["orderpdf"].ToString() + orderid.ToString() + "/");
            if (!Directory.Exists(folderpath))
                Directory.CreateDirectory(folderpath);
            Pdf_path = Server.MapPath(virtualfilepath);
            AsyncFileUpload1.SaveAs(Pdf_path);
            //FileUpload1.Attributes.Clear();
            //FileUpload1.Dispose();
            //hlBuyerPODwn.Visible = true;
            btncancel.Visible = true;
            btnFinish.Visible = true;
            //btnDFinish.Visible = false;
            //hlBuyerPODwn.NavigateUrl = Pdf_path;
            lblMsg.Text = string.Empty;
            //btnUpload.Visible = false;
            lblUploadText.Text = Pdf_path;
            Session["Download_Pdf_path"] = Pdf_path;
            Session["Pdf_path"] = virtualfilepath;
            //upd1.Update();
        }
        else
        {
            lblMsg.Text = " *Please select a file to upload ";
            //hlBuyerPODwn.Visible = false;
            //btncancel.Visible = false;
            Session["Download_Pdf_path"] = null;
            Session["Pdf_path"] = null;
        }
    }

    protected void lnkdownloadpo_Click(object sender, EventArgs e)
    {
        var filepath = Session["Download_Pdf_path"];
        if (filepath != null)
        {
            var filedata = File.ReadAllBytes(filepath.ToString());
            FileInfo dlgFile = new FileInfo(filepath.ToString());
            Response.ClearContent();
            Response.AddHeader("Content-Disposition", "attachment; filename=" + dlgFile.Name);
            Response.AddHeader("Content-Length", dlgFile.Length.ToString());
            Response.ContentType = GetExtension(dlgFile.Extension.ToLower());
            Response.TransmitFile(dlgFile.FullName);
            Response.End();
        }
    }

    private string GetExtension(string Extension)
    {
        switch (Extension)
        {
            case ".doc":
                return "application/ms-word";
            case ".xls":
                return "application/vnd.ms-excel";
            case ".ppt":
                return "application/mspowerpoint";
            case "jpeg":
                return "image/jpeg";
            case ".bmp":
                return "image/bmp";
            case ".zip":
                return "application/zip";
            case ".log":
                return "text/HTML";
            case ".txt":
                return "text/plain";
            case ".tiff":
            case ".tif":
                return "image/tiff";
            case ".asf":
                return "video/x-ms-asf";
            case ".avi":
                return "video/avi";
            case ".gif":
                return "image/gif";
            case ".jpg":
            case ".wav":
                return "audio/wav";
            case ".pdf":
                return "application/pdf";
            case ".fdf":
                return "application/vnd.fdf";
            case ".dwg":
                return "image/vnd.dwg";
            case ".msg":
                return "application/msoutlook";
            case ".xml":
                return "application/xml";
            default:
                return "application/octet-stream";
        }
    }
}
