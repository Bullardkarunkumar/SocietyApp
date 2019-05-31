using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;

public partial class Admin_PlanTimeTable : System.Web.UI.Page
{
    Order_BL orderobj = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["status"] = new object();
        if (!Page.IsPostBack)
        {
            Master.MasterControlbtnTrackPO();
            ddlOrderStatus_SelectedIndexChanged(sender, e);
        }
    }
    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinddlOrderList();
    }
    protected void dlOrderList_ItemCommand(object source, DataListCommandEventArgs e)
    {
        string command = e.CommandName;
        int Index = e.Item.ItemIndex;
        switch (command)
        {
            case "Exp_Col":
                {

                    DataListItem item = dlOrderList.Items[Index];
                    //Label test = item.FindControl("lblC_P") as Label;
                    ImageButton img = item.FindControl("ibtnNOExpColap") as ImageButton;
                    if (img.ImageUrl == "~/images/expand.JPG")
                    {
                        ((ImageButton)dlOrderList.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
                        ((GridView)dlOrderList.Items[Index].FindControl("gvNewOrder")).Visible = true;

                    }
                    else if (img.ImageUrl == "~/images/collapse.JPG")
                    {
                        ((ImageButton)dlOrderList.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/expand.JPG";
                        ((GridView)dlOrderList.Items[Index].FindControl("gvNewOrder")).Visible = false;

                    }
                }
                break;
            case "BranchOrder":
                {
                    DataListItem item = dlOrderList.Items[Index];
                    Session["sOrderID"] = Encrypt_Decrypt.Encrypt(((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text, true);
                    Response.Redirect("~/mudar/UpdateAdminOrder.aspx");
                }
                break;
            case "Display":
                {
                    string str = ((HiddenField)dlOrderList.Items[Index].FindControl("hfOrderPdf")).Value.ToString();
                    //ifOrderPdf.Attributes.Add("src", str);
                    //ifOrderPdf.Attributes.Add("src", "../Attachments/OrderPDF/1015/1015_PO201296.pdf");
                }
                break;
            case "Download":
                {
                    try
                    {
                        string str = ((HiddenField)dlOrderList.Items[Index].FindControl("hfOrderPdf")).Value.ToString();
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
                    catch (Exception ex)
                    {
                        Session["ErrorMsg"] = ex.Message;
                        Response.Redirect("~/NoAccess.aspx", false);
                    }
                }
                break;
        }
    }
    private void BinddlOrderList()
    {
        dlOrderList.DataSource = orderobj.OrderList(ddlOrderStatus.Text,MudarLogin.GetBranchId());
        dlOrderList.DataBind();
        foreach (DataListItem dli in dlOrderList.Items)
        {
            int OrderID = Convert.ToInt32(dlOrderList.DataKeys[dli.ItemIndex]);
            ((ImageButton)dlOrderList.Items[dli.ItemIndex].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
            DataTable dtAOPlist = orderobj.BindAdminOrderProductList(OrderID);
            string str = ((HiddenField)dlOrderList.Items[dli.ItemIndex].FindControl("hfOrderPdf")).Value.ToString();
            if (dtAOPlist.Rows.Count > 0)
            {
                for (int i = 0; i < dtAOPlist.Rows.Count; i++)
                {
                    if (dtAOPlist.Rows[i]["Country"].ToString() == "INDIA")
                        dtAOPlist.Rows[i]["Country"] = "INR";
                    else
                        dtAOPlist.Rows[i]["Country"] = "USD";
                }
                if (!string.IsNullOrEmpty(str))
                {
                    (dli.FindControl("hlPDF") as HyperLink).Visible = true;
                    (dli.FindControl("lbtnOrderpdfDownload") as LinkButton).Visible = true;
                }
                else
                {
                    (dli.FindControl("hlPDF") as HyperLink).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                }
                (dli.FindControl("gvNewOrder") as GridView).DataSource = dtAOPlist;
                (dli.FindControl("gvNewOrder") as GridView).DataBind();
            }
        }
    }
    //protected void dlOrderList_ItemDataBound(object sender, DataListItemEventArgs e)
    //{
    //    //if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
    //    //{
    //    //    DataRowView drv = (DataRowView)(e.Item.DataItem);
    //    //    string Status = drv.Row["OrderStatus"].ToString();
    //    //    if (Status == "NEW")
    //    //    {
    //    //        Label lbl = (Label)e.Item.FindControl("lblStatus");
    //    //        lbl.ForeColor = System.Drawing.Color.Orange;
    //    //    }
    //    //    if (Status == "INPROCESS")
    //    //    {
    //    //        Label lbl = (Label)e.Item.FindControl("lblStatus");
    //    //        lbl.ForeColor = System.Drawing.Color.DarkSalmon;
    //    //    }
    //    //    if (Status == "COLLECTING")
    //    //    {
    //    //        Label lbl = (Label)e.Item.FindControl("lblStatus");
    //    //        lbl.ForeColor = System.Drawing.Color.BlueViolet;
    //    //    }
    //    //    if (Status == "BLENDING")
    //    //    {
    //    //        Label lbl = (Label)e.Item.FindControl("lblStatus");
    //    //        lbl.ForeColor = System.Drawing.Color.DarkGreen;
    //    //    }
    //    //    if (Status == "PACKING")
    //    //    {
    //    //        Label lbl = (Label)e.Item.FindControl("lblStatus");
    //    //        lbl.ForeColor = System.Drawing.Color.OrangeRed;
    //    //    }
    //    //    if (Status == "CLOSE")
    //    //    {
    //    //        LinkButton lbtnO = (LinkButton)e.Item.FindControl("lbtnOrderID");
    //    //        lbtnO.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        Label lblT = (Label)e.Item.FindControl("lblType");
    //    //        lblT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        Label lblP = (Label)e.Item.FindControl("lblPOID");
    //    //        lblP.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        Label lblB = (Label)e.Item.FindControl("lblBuyerName");
    //    //        lblB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        Label lblD = (Label)e.Item.FindControl("lblDateOfOrder");
    //    //        lblD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        Label lblS = (Label)e.Item.FindControl("lblStatus");
    //    //        lblS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        ImageButton ibtn = (ImageButton)e.Item.FindControl("ibtnNOExpColap");
    //    //        ibtn.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        HyperLink hl = (HyperLink)e.Item.FindControl("hlPDF");
    //    //        hl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //        LinkButton lbtnD = (LinkButton)e.Item.FindControl("lbtnOrderpdfDownload");
    //    //        lbtnD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
    //    //    }
    //    //}
    //}
}