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
using System.Web.UI.HtmlControls;
using System.IO;
using Newtonsoft.Json;

public partial class Admin_SuperAdminOrders : System.Web.UI.Page
{
    Order_BL orderobj = new Order_BL();
    Supplier_BL supplierObj = new Supplier_BL();
    DataTable dtOrdersList = new DataTable();
    bool result, result1, result2;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Session["status"] = new object();
        if (!Page.IsPostBack)
        {
            ddlOrderStatus_SelectedIndexChanged(sender, e);
            hdnBranchesJson.Value = supplierObj.GetBranches();
        }
    }
    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinddlOrderList();
    }
    protected void dlOrderList_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        string command = e.CommandName;
        int Index = e.Item.ItemIndex;
        switch (command)
        {
            case "Exp_Col":
                {
                    ImageButton img = e.Item.FindControl("ibtnNOExpColap") as ImageButton;
                    if (img.ImageUrl == "~/images/expand.JPG")
                    {
                        ((ImageButton)e.Item.FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
                        ((GridView)e.Item.FindControl("gvNewOrder")).Visible = true;
                        ((HtmlTableRow)dlOrderList.Items[Index].FindControl("trSubTable")).Style.Add(HtmlTextWriterStyle.Display, "''");
                    }
                    else if (img.ImageUrl == "~/images/collapse.JPG")
                    {
                        ((ImageButton)e.Item.FindControl("ibtnNOExpColap")).ImageUrl = "~/images/expand.JPG";
                        ((GridView)e.Item.FindControl("gvNewOrder")).Visible = false;
                        ((HtmlTableRow)dlOrderList.Items[Index].FindControl("trSubTable")).Style.Add(HtmlTextWriterStyle.Display, "none");
                    }
                }
                break;
            case "BranchOrder":
                {
                    Session["sOrderID"] = Encrypt_Decrypt.Encrypt(e.CommandArgument.ToString(), true);
                    Response.Redirect("~/mudar/UpdateAdminOrder.aspx");
                }
                break;
            case "Display":
                {
                    string str = ((HiddenField)e.Item.FindControl("hfOrderPdf")).Value.ToString();
                    //ifOrderPdf.Attributes.Add("src", str);
                    //ifOrderPdf.Attributes.Add("src", "../Attachments/OrderPDF/1015/1015_PO201296.pdf");
                }
                break;
            case "Download":
                {
                    try
                    {
                        string str = ((HiddenField)e.Item.FindControl("hfOrderPdf")).Value.ToString();
                        WebClient req = new WebClient();
                        HttpResponse response = HttpContext.Current.Response;
                        response.Clear();
                        response.ClearContent();
                        response.ClearHeaders();
                        response.Buffer = true;
                        if (!File.Exists(Server.MapPath(str)))
                        {
                            throw new FileNotFoundException();
                        }
                        response.AddHeader("Content-Disposition", "attachment;filename=\"" + Server.MapPath(str) + "\"");
                        byte[] data = req.DownloadData(Server.MapPath(str));
                        response.BinaryWrite(data);
                        response.End();
                    }
                    catch (FileNotFoundException ex)
                    {
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "downloaderror", "fnShowMessage('PO does not exists')", true);
                    }
                    catch (Exception ex)
                    {
                        Session["ErrorMsg"] = ex.Message;
                        //Response.Redirect("~/NoAccess.aspx", false);
                        ScriptManager.RegisterStartupScript(this, typeof(Page), "downloaderror", "fnShowMessage('Error while downloading PO')", true);
                    }
                }
                break;
            case "cancel":
                {
                    #region old code
                    //    DataListItem item = dlOrderList.Items[Index];
                    //    DataTable dt = orderobj.GetCollectionTransactions(((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text);
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        foreach (DataRow items in dt.Rows)
                    //        {
                    //            string[] BlendingBID = items["Blending_BatchID"].ToString().Split('@');
                    //            string[] PlantationID = items["PlantationID"].ToString().Split('@');
                    //            string[] CollectionQty = items["CollectionQty"].ToString().Split('@');
                    //            string blendingBatchId = items["Blending_BatchID"].ToString();
                    //            if (!string.IsNullOrEmpty(blendingBatchId) && blendingBatchId.Contains("@"))
                    //            {
                    //                if (BlendingBID[0].ToString().Trim() == string.Empty)
                    //                {
                    //                    string[] PlantationID1 = PlantationID[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    //                    string[] CollectionQty1 = CollectionQty[0].ToString().Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    //                    for (int i = 0; i < PlantationID1.Length; i++)
                    //                    {
                    //                        result1 = orderobj.UpdateSoldQtyforCancelOrder(Convert.ToDecimal(CollectionQty1[i].ToString()), Convert.ToInt32(PlantationID1[i].ToString()), Convert.ToInt32(((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text));
                    //                    }
                    //                }
                    //                if (BlendingBID[1].ToString() != string.Empty)
                    //                {
                    //                    string[] BlendingBID2 = BlendingBID[1].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    //                    string[] CollectionQty2 = CollectionQty[1].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    //                    for (int j = 0; j < BlendingBID2.Length; j++)
                    //                    {
                    //                        result2 = orderobj.PreOrderUpdateSoldQtyforCancelOrder(Convert.ToDecimal(CollectionQty2[j].ToString()), BlendingBID2[j].ToString());
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                if (BlendingBID[0].ToString() != string.Empty)
                    //                {
                    //                    string[] BlendingBID2 = BlendingBID[0].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    //                    string[] CollectionQty2 = CollectionQty[1].ToString().Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);
                    //                    for (int j = 0; j < BlendingBID2.Length; j++)
                    //                    {
                    //                        result2 = orderobj.PreOrderUpdateSoldQtyforCancelOrder(Convert.ToDecimal(CollectionQty2[j].ToString()), BlendingBID2[j].ToString());
                    //                    }
                    //                }
                    //            }
                    //        }
                    //        if (result1 == true || result2 == true)
                    //        {
                    //            result = orderobj.CancelOrders("CANCEL", ((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text);
                    //            if (result)
                    //            {
                    //                BinddlOrderList();
                    //                ((Button)dlOrderList.Items[Index].FindControl("btnUpdate")).Visible = false;
                    //                ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Order Canceled !!!');</script>");
                    //            }
                    //        }
                    //    }
                    //    else
                    //    {
                    //        result = orderobj.CancelOrders("CANCEL", ((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text);
                    //        if (result)
                    //        {
                    //            BinddlOrderList();
                    //            ((Button)dlOrderList.Items[Index].FindControl("btnUpdate")).Visible = false;
                    //            ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Order Canceled !!!');</script>");
                    //        }
                    //    } 
                    #endregion
                }
                break;
        }
    }
    private void BinddlOrderList()
    {
        dlOrderList.DataSource = orderobj.OrderList(ddlOrderStatus.Text, Guid.Empty);
        dlOrderList.DataBind();
        //foreach (RepeaterItem dli in dlOrderList.Items)
        //{
        //    int OrderID = Convert.ToInt32(((Label)dlOrderList.Items[dli.ItemIndex].FindControl("lblorderid")).Text);
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
            Label lbl = (Label)e.Item.FindControl("lblStatus");
            switch (Status)
            {
                case "NEW":
                    lbl.ForeColor = System.Drawing.Color.Orange;
                    Button btn = (Button)e.Item.FindControl("btnAssign");
                    Label lblOrderAssign = e.Item.FindControl("lblOrderAssign") as Label;
                    if (string.IsNullOrEmpty(lblOrderAssign.Text) || lblOrderAssign.Text.Equals("0"))
                    {
                        btn.Visible = true;
                        var lblBranch = e.Item.FindControl("lblBranch") as Label;
                        lblBranch.Visible = false;
                        var lblorderid = e.Item.FindControl("lblorderid") as Label;
                        btn.OnClientClick = "return fnShowBranches('" + lblorderid.Text + "')";
                    }
                    break;
                case "INPROCESS":
                    lbl.ForeColor = System.Drawing.Color.DarkSalmon;
                    break;
                case "COLLECTING":
                    lbl.ForeColor = System.Drawing.Color.BlueViolet;
                    break;
                case "BLENDING":
                    lbl.ForeColor = System.Drawing.Color.DarkGreen;
                    break;
                case "PACKING":
                    lbl.ForeColor = System.Drawing.Color.OrangeRed;
                    break;
                case "SAMPLE DISPATCH":
                    Label lblMSg = (Label)e.Item.FindControl("lblMSg");
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
                            lblMSg.Visible = true;
                            foreach (DataRow item in dt.Rows)
                            {
                                lblMSg.Text = lbl.Text + item["SampQty"].ToString() + "  " + item["SampDetails"].ToString() + "<br/>";
                            }
                            lblMSg.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    break;
                case "CLOSE":
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
                    break;
                case "CANCEL":
                    Label lblT1 = (Label)e.Item.FindControl("lblType");
                    lblT1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    Label lblB1 = (Label)e.Item.FindControl("lblBuyerName");
                    lblB1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    Label lblD1 = (Label)e.Item.FindControl("lblDateOfOrder");
                    lblD1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    Label lblS1 = (Label)e.Item.FindControl("lblStatus");
                    lblS1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    HyperLink hl1 = (HyperLink)e.Item.FindControl("hlPDF");
                    hl1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                    (e.Item.FindControl("gvNewOrder") as GridView).Visible = false;
                    break;
            }
            //if (Status == "NEW")
            //{
            //    Label lbl = (Label)e.Item.FindControl("lblStatus");
            //    lbl.ForeColor = System.Drawing.Color.Orange;
            //    Button btn = (Button)e.Item.FindControl("btnAssign");
            //    btn.Visible = true;
            //}
            //if (Status == "INPROCESS")
            //{
            //    Label lbl = (Label)e.Item.FindControl("lblStatus");
            //    lbl.ForeColor = System.Drawing.Color.DarkSalmon;
            //}
            //if (Status == "COLLECTING")
            //{
            //    Label lbl = (Label)e.Item.FindControl("lblStatus");
            //    lbl.ForeColor = System.Drawing.Color.BlueViolet;
            //}
            //if (Status == "BLENDING")
            //{
            //    Label lbl = (Label)e.Item.FindControl("lblStatus");
            //    lbl.ForeColor = System.Drawing.Color.DarkGreen;
            //}
            //if (Status == "PACKING")
            //{
            //    Label lbl = (Label)e.Item.FindControl("lblStatus");
            //    lbl.ForeColor = System.Drawing.Color.OrangeRed;
            //}
            //if (Status == "SAMPLE DISPATCH")
            //{
            //    Label lbl = (Label)e.Item.FindControl("lblMSg");
            //    string text = string.Empty;
            //    LinkButton lbtnO = (LinkButton)e.Item.FindControl("lbtnOrderID");
            //    Label lblcourier = (Label)e.Item.FindControl("lblCourier");
            //    dt1 = orderobj.GetAdminCommentDetails(lbtnO.Text);
            //    string Msg1 = dt1.Rows[0]["Comments"].ToString();
            //    if (Msg1.Contains("#"))
            //    {
            //        string[] po = Msg1.Split('#');
            //        lblcourier.Text = po[1].ToString();
            //        lbl.Visible = false;
            //        lblcourier.Visible = true;
            //        lblcourier.ForeColor = System.Drawing.Color.Red;
            //    }
            //    else
            //    {
            //        dt = orderobj.GetssampleQtyandMsgAdmin(lbtnO.Text);
            //        if (dt.Rows.Count > 0)
            //        {
            //            lbl.Visible = true;
            //            foreach (DataRow item in dt.Rows)
            //            {
            //                lbl.Text = lbl.Text + item["SampQty"].ToString() + "  " + item["SampDetails"].ToString() + "<br/>";
            //            }
            //            lbl.ForeColor = System.Drawing.Color.Red;
            //        }
            //    }
            //}
            if (Status.Contains("ETA"))
            {
                string Date = string.Empty;
                Label lblETAStatus = (Label)e.Item.FindControl("lblStatus");
                lblETAStatus.ForeColor = System.Drawing.Color.Brown;
                if (Status.Contains("ETA"))
                {
                    string[] po = Status.Split(new string[] { "ETA<br/>" }, StringSplitOptions.None);
                    Date = po[1].ToString();
                    DateTime d = Convert.ToDateTime(Date);
                    if (d.AddDays(10) < DateTime.Now)
                    {
                        LinkButton lbtnO = (LinkButton)e.Item.FindControl("lbtnOrderID");
                        orderobj.UpdateOrderStatus("CLOSE", lbtnO.Text);
                    }
                }
            }
            if (Status.Contains("SAMP RECV"))
            {
                Label lblStatus = (Label)e.Item.FindControl("lblStatus");
                lblStatus.ForeColor = System.Drawing.Color.BurlyWood;
            }
            //if (Status == "CLOSE")
            //{
            //    Label lblT = (Label)e.Item.FindControl("lblType");
            //    lblT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    Label lblB = (Label)e.Item.FindControl("lblBuyerName");
            //    lblB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    Label lblD = (Label)e.Item.FindControl("lblDateOfOrder");
            //    lblD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    Label lblS = (Label)e.Item.FindControl("lblStatus");
            //    lblS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    HyperLink hl = (HyperLink)e.Item.FindControl("hlPDF");
            //    hl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    (e.Item.FindControl("gvNewOrder") as GridView).Visible = false;
            //    //LinkButton lbtnD = (LinkButton)e.Item.FindControl("lbtnOrderpdfDownload");
            //    //lbtnD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //}
            //if (Status == "CANCEL")
            //{
            //    Label lblT = (Label)e.Item.FindControl("lblType");
            //    lblT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    Label lblB = (Label)e.Item.FindControl("lblBuyerName");
            //    lblB.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    Label lblD = (Label)e.Item.FindControl("lblDateOfOrder");
            //    lblD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    Label lblS = (Label)e.Item.FindControl("lblStatus");
            //    lblS.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    HyperLink hl = (HyperLink)e.Item.FindControl("hlPDF");
            //    hl.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //    (e.Item.FindControl("gvNewOrder") as GridView).Visible = false;
            //    //LinkButton lbtnD = (LinkButton)e.Item.FindControl("lbtnOrderpdfDownload");
            //    //lbtnD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            //}
        }
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