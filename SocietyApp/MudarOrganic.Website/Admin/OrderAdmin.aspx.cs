using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;

public partial class Admin_OrderAdmin : System.Web.UI.Page
{
    Order_BL orderobj = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnTrackPO();
            BinddlOrderList();
        }
    }
    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinddlOrderList(ddlSortBy.SelectedValue.ToString());
    }
    protected void dlOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string command = e.CommandName;
        int Index = e.Item.ItemIndex;
        switch (command)
        {
            case "Exp_Col":
                {

                    RepeaterItem item = dlOrderList.Items[Index];
                    //Label test = item.FindControl("lblC_P") as Label;
                    ImageButton img = item.FindControl("ibtnNOExpColap") as ImageButton;
                    if (img.ImageUrl == "~/images/expand.JPG")
                    {
                        ((ImageButton)dlOrderList.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
                        ((GridView)dlOrderList.Items[Index].FindControl("gvNewOrder")).Visible = true;
                        ((HtmlTableRow)dlOrderList.Items[Index].FindControl("trSubTable")).Style.Add(HtmlTextWriterStyle.Display, "''");
                    }
                    else if (img.ImageUrl == "~/images/collapse.JPG")
                    {
                        ((ImageButton)dlOrderList.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/expand.JPG";
                        ((GridView)dlOrderList.Items[Index].FindControl("gvNewOrder")).Visible = false;
                        ((HtmlTableRow)dlOrderList.Items[Index].FindControl("trSubTable")).Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                }
                break;
            case "BranchOrder":
                {
                    RepeaterItem item = dlOrderList.Items[Index];
                    Session["sOrderID"] = Encrypt_Decrypt.Encrypt(((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text, true);
                    Response.Redirect("~/Mudar/UpdateAdminOrder.aspx");
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
        var dtOrdersList = orderobj.OrderList(ddlOrderStatus.Text, MudarLogin.GetBranchId());
     
        //if (dtlogin.Rows[0]["RoleName"].ToString().Trim().ToLower() == LoginType.Admin.ToLower())
        //{
        //    var drows = dtOrdersList.Rows.Cast<DataRow>().OrderByDescending(itm => itm["OrderDate"]).Where(itm => itm["orderassign"].ToString() != string.Empty);
        //    if (drows.Count() > 0)
        //    {
        //        var result = drows.CopyToDataTable();
        //        dlOrderList.DataSource = result;
        //        dlOrderList.DataBind();
        //    }
        //}
        dlOrderList.DataSource = dtOrdersList;
        dlOrderList.DataBind();
        //foreach (DataListItem dli in dlOrderList.Items)
        //{
        //    int OrderID = Convert.ToInt32(dlOrderList.DataKeys[dli.ItemIndex]);
        //    ((ImageButton)dlOrderList.Items[dli.ItemIndex].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
        //    DataTable dtAOPlist = orderobj.BindAdminOrderProductList(OrderID);
        //    string str = ((HiddenField)dlOrderList.Items[dli.ItemIndex].FindControl("hfOrderPdf")).Value.ToString();
        //    if (dtAOPlist.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dtAOPlist.Rows.Count; i++)
        //        {
        //            if (dtAOPlist.Rows[i]["Country"].ToString() == "INDIA")
        //                dtAOPlist.Rows[i]["Country"] = "INR";
        //            else
        //                dtAOPlist.Rows[i]["Country"] = "USD";
        //        }
        //        if (!string.IsNullOrEmpty(str))
        //        {
        //            (dli.FindControl("hlPDF") as HyperLink).Visible = true;
        //            (dli.FindControl("lbtnOrderpdfDownload") as LinkButton).Visible = true;
        //        }
        //        else
        //        {
        //            (dli.FindControl("hlPDF") as HyperLink).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        //        }
        //        (dli.FindControl("gvNewOrder") as GridView).DataSource = dtAOPlist;
        //        (dli.FindControl("gvNewOrder") as GridView).DataBind();
        //    }
        //}
    }
    private void BinddlOrderList(string SortStatus)
    {
        dlOrderList.DataSource = orderobj.OrderList(ddlOrderStatus.Text, SortStatus);
        dlOrderList.DataBind();
        //foreach (DataListItem dli in dlOrderList.Items)
        //{
        //    int OrderID = Convert.ToInt32(dlOrderList.DataKeys[dli.ItemIndex]);
        //    ((ImageButton)dlOrderList.Items[dli.ItemIndex].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
        //    DataTable dtAOPlist = orderobj.BindAdminOrderProductList(OrderID);
        //    string str = ((HiddenField)dlOrderList.Items[dli.ItemIndex].FindControl("hfOrderPdf")).Value.ToString();
        //    if (dtAOPlist.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dtAOPlist.Rows.Count; i++)
        //        {
        //            if (dtAOPlist.Rows[i]["Country"].ToString() == "INDIA")
        //                dtAOPlist.Rows[i]["Country"] = "INR";
        //            else
        //                dtAOPlist.Rows[i]["Country"] = "USD";
        //        }
        //        if (!string.IsNullOrEmpty(str))
        //        {
        //            (dli.FindControl("hlPDF") as HyperLink).Visible = true;
        //            (dli.FindControl("lbtnOrderpdfDownload") as LinkButton).Visible = true;
        //        }
        //        else
        //        {
        //            (dli.FindControl("hlPDF") as HyperLink).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
        //        }
        //        (dli.FindControl("gvNewOrder") as GridView).DataSource = dtAOPlist;
        //        (dli.FindControl("gvNewOrder") as GridView).DataBind();
        //    }
        //}
    }
    protected void dlOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            BindSubGrid(e.Item);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            string Status = drv.Row["OrderStatus"].ToString();
            string Otype = drv.Row["OrderType"].ToString();
            if (Otype == "order")
            {
                Label lblT = (Label)e.Item.FindControl("lblType");
                lblT.ForeColor = System.Drawing.Color.Green;
            }
            if (Otype == "LotSample")
            {
                Label lblT = (Label)e.Item.FindControl("lblType");
                lblT.ForeColor = System.Drawing.Color.Orange;
            }
            if (Status == "NEW")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.Orange;
            }
            if (Status == "INPROCESS")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.DarkSalmon;
            }
            if (Status == "COLLECTING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.BlueViolet;
            }
            if (Status == "BLENDING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.DarkGreen;
            }
            if (Status == "PACKING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.OrangeRed;
            }
            if (Status == "SAMPLE DISPATCH")
            {
                Label lbl = (Label)e.Item.FindControl("lblMSg");
                string text = string.Empty;
                LinkButton lbtnO = (LinkButton)e.Item.FindControl("lbtnOrderID");
                Label lblcourier = (Label)e.Item.FindControl("lblCourier");
                dt1 = orderobj.GetAdminCommentDetails(lbtnO.Text);
                string Msg1 = dt1.Rows[0]["Comments"].ToString();
                if (Msg1.Contains("#"))
                {
                    string[] po = Msg1.Split('#');
                    lblcourier.Text = po[1].ToString();
                    lbl.Visible = false;
                    lblcourier.Visible = true;
                    lblcourier.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    dt = orderobj.GetssampleQtyandMsgAdmin(lbtnO.Text);
                    if (dt.Rows.Count > 0)
                    {
                        lbl.Visible = true;
                        foreach (DataRow item in dt.Rows)
                        {
                            lbl.Text = lbl.Text + item["SampQty"].ToString() + "  " + item["SampDetails"].ToString() + "<br/>";
                        }
                        lbl.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            if (Status.Contains("ETA"))
            {
                string Date = string.Empty;
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.Brown;
                if (Status.Contains("ETA"))
                {
                    //string[] po = Status.Split(new string[] { "ETA<br/>" }, StringSplitOptions.None);
                    //Date = po[1].ToString();
                    //DateTime d = Convert.ToDateTime(Date);
                    //if (d.AddDays(10) < DateTime.Now)
                    //{
                    //    LinkButton lbtnO = (LinkButton)e.Item.FindControl("lbtnOrderID");
                    //    orderobj.UpdateOrderStatus("CLOSE", lbtnO.Text);
                    //}
                }
            }
            if (Status.Contains("SAMP RECV"))
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.BurlyWood;
            }
            if (Status == "CLOSE")
            {
                Label lblT = (Label)e.Item.FindControl("lblType");
                lblT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblB = (Label)e.Item.FindControl("lblBuyerName");
                lblB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblD = (Label)e.Item.FindControl("lblDateOfOrder");
                lblD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblS = (Label)e.Item.FindControl("lblStatus");
                lblS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                HyperLink hl = (HyperLink)e.Item.FindControl("hlPDF");
                hl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                (e.Item.FindControl("gvNewOrder") as GridView).Visible = false;
                //LinkButton lbtnD = (LinkButton)e.Item.FindControl("lbtnOrderpdfDownload");
                //lbtnD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            }
            if (Status == "CANCEL")
            {
                Label lblT = (Label)e.Item.FindControl("lblType");
                lblT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblB = (Label)e.Item.FindControl("lblBuyerName");
                lblB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblD = (Label)e.Item.FindControl("lblDateOfOrder");
                lblD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblS = (Label)e.Item.FindControl("lblStatus");
                lblS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                HyperLink hl = (HyperLink)e.Item.FindControl("hlPDF");
                hl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                (e.Item.FindControl("gvNewOrder") as GridView).Visible = false;
                //LinkButton lbtnD = (LinkButton)e.Item.FindControl("lbtnOrderpdfDownload");
                //lbtnD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            }
        }
    }
    protected void ddlSortBy_SelectedIndexChanged(object sender, EventArgs e)
    {
        //BinddlOrderList(ddlSortBy.SelectedValue.ToString());
    }
    protected void RefreshRepeater_Click(object sender, EventArgs e)
    {
        BinddlOrderList(ddlSortBy.SelectedValue.ToString());
    }

    private void BindSubGrid(RepeaterItem itm)
    {
        var grd = itm.FindControl("gvNewOrder") as GridView;
        var trSubTable = itm.FindControl("trSubTable") as HtmlTableRow;
        int OrderID = Convert.ToInt32((itm.FindControl("lblorderid") as Label).Text);
        ((ImageButton)itm.FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
        DataTable dtAOPlist = orderobj.BindAdminOrderProductList(OrderID);
        string str = ((HiddenField)itm.FindControl("hfOrderPdf")).Value.ToString();
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
                (itm.FindControl("hlPDF") as HyperLink).Visible = true;
                (itm.FindControl("lbtnOrderpdfDownload") as LinkButton).Visible = true;
            }
            else
            {
                (itm.FindControl("hlPDF") as HyperLink).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            }
            grd.DataSource = dtAOPlist;
            grd.DataBind();
            trSubTable.Style.Add(HtmlTextWriterStyle.Display, "''");
        }
        else
            trSubTable.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
}
