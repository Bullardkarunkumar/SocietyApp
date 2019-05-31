using MudarOrganic.BL;
using MudarOrganic.Components;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Admin_PreOrder : System.Web.UI.Page
{
    Order_BL orderObj = new Order_BL();

    Product_BL prodBL = new Product_BL();
    Supplier_BL supplierObj = new Supplier_BL();
    Buyer_BL buyerObj = new Buyer_BL();
    DataTable dtbuyer = new DataTable();
    Order_BL BranchOrderObj = new Order_BL();
    string defaultBuyerId = MudarOrderConstants.DefaultBuyerId;
    string statusId = "0E47F5F1-2A54-4DEB-9CB6-3B860E479945";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            var dt = orderObj.PreOrderList(string.Empty);
            BinddlOrderList();
        }
    }
    private void BinddlOrderList()
    {
        DataTable dtLoginDetails = (DataTable)Session["dtLoginDetails"];
        DataTable dtOrderList = BranchOrderObj.BranchOrderList("ALL", 1, statusId);
        DataView dv = dtOrderList.DefaultView;
        dv.Sort = "OrderId desc";
        DataTable sortedDT = dv.ToTable();
        dlOrderList.DataSource = sortedDT; //BranchOrderObj.BranchOrderList(ddlOrderStatus.Text);
        dlOrderList.DataBind();

    }

    protected void btnCreate_Click(object sender, EventArgs e)
    {
        //BindProductPricDetails();
        //pnlLst.Visible = false;
        //pnlNewOrder.Visible = true;
        Response.Redirect("~/Admin/PreOrderProducts.aspx");
    }
    private void BindSubGrid(RepeaterItem itm)
    {
        var grd = itm.FindControl("gvOrder") as GridView;
        var trSubTable = itm.FindControl("trSubTable") as HtmlTableRow;
        int OrderID = Convert.ToInt32((itm.FindControl("lblorderid") as Label).Text);
        ((ImageButton)itm.FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
        DataTable dtBOPlist = BranchOrderObj.BindBranchOrderProductList(OrderID);
        string strLS = ((HiddenField)itm.FindControl("hfLSpdf")).Value.ToString();
        string str = ((HiddenField)itm.FindControl("hfOrderPdf")).Value.ToString();
        if (dtBOPlist.Rows.Count > 0)
        {
            if (!string.IsNullOrEmpty(strLS))
            {
                (itm.FindControl("hlLSpdf") as HyperLink).Visible = true;
                //(itm.FindControl("lbtnLSpdf") as LinkButton).Visible = true;
            }
            else
            {
                (itm.FindControl("hlLSpdf") as HyperLink).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            }
            if (!string.IsNullOrEmpty(str))
            {
                (itm.FindControl("hlPDF") as HyperLink).Visible = true;
                //(itm.FindControl("lbtnOrderpdfDownload") as LinkButton).Visible = true;
            }
            else
            {
                (itm.FindControl("hlPDF") as HyperLink).ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            }
            grd.DataSource = dtBOPlist;
            grd.DataBind();
            trSubTable.Style.Add(HtmlTextWriterStyle.Display, "''");
        }
        else
            trSubTable.Style.Add(HtmlTextWriterStyle.Display, "none");
    }
    protected void dlOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            var lblorderid = e.Item.FindControl("lblorderid") as Label;
            if (Convert.ToInt32(lblorderid.Text) < 100000)
            {
                e.Item.Visible = false;
            }
            BindSubGrid(e.Item);
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            string Status = drv.Row["OrderStatus"].ToString();
            if (Status == "NEW")
            {
                Label lbl = (Label)e.Item.FindControl("lblBranchOrderStatus");
                lbl.ForeColor = System.Drawing.Color.Orange;
            }
            if (Status == "COLLECTING")
            {
                Label lbl = (Label)e.Item.FindControl("lblBranchOrderStatus");
                lbl.ForeColor = System.Drawing.Color.BlueViolet;
            }
            if (Status == "BLENDING")
            {
                Label lbl = (Label)e.Item.FindControl("lblBranchOrderStatus");
                lbl.ForeColor = System.Drawing.Color.DarkGreen;
            }
            if (Status == "PACKING")
            {
                Label lbl = (Label)e.Item.FindControl("lblBranchOrderStatus");
                lbl.ForeColor = System.Drawing.Color.OrangeRed;
            }
            if (Status == "DISPATCH")
            {
                Label lbl1 = (Label)e.Item.FindControl("lblBranchOrderStatus");
                lbl1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblT = (Label)e.Item.FindControl("lblType");
                lblT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblD = (Label)e.Item.FindControl("lblBranchOrderDate");
                lblD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");

                Label lbl = (Label)e.Item.FindControl("lblMSg");
                Label lbtnO = (Label)e.Item.FindControl("lbtnOrderID");
                dt1 = orderObj.GetInvandDispatchDetails(lbtnO.Text);
                if (dt1.Rows.Count > 0)
                {
                    lbl.Visible = true;
                    lbl.Text = " Inv :" + dt1.Rows[0]["BOinv"].ToString() + " / " + dt1.Rows[0]["DispatchDate"].ToString();
                    lbl.ForeColor = System.Drawing.Color.Red;
                }
                (e.Item.FindControl("gvOrder") as GridView).Visible = false;
            }
            if (Status == "CANCEL")
            {
                Label lbl1 = (Label)e.Item.FindControl("lblBranchOrderStatus");
                lbl1.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblT = (Label)e.Item.FindControl("lblType");
                lblT.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
                Label lblD = (Label)e.Item.FindControl("lblBranchOrderDate");
                lblD.ForeColor = System.Drawing.ColorTranslator.FromHtml("#d3d3d3");
            }
            if (Status == "SAMPLE DISPATCH")
            {
                Label lbl1 = (Label)e.Item.FindControl("lblBranchOrderStatus");
                lbl1.ForeColor = System.Drawing.Color.Gray;
                Label lblT = (Label)e.Item.FindControl("lblType");
                lblT.ForeColor = System.Drawing.Color.Gray;
                Label lblD = (Label)e.Item.FindControl("lblBranchOrderDate");
                lblD.ForeColor = System.Drawing.Color.Gray;
                Label lbl = (Label)e.Item.FindControl("lblMSg");
                string text = string.Empty;
                Label lbtnO = (Label)e.Item.FindControl("lbtnOrderID");
                Label lblcourier = (Label)e.Item.FindControl("lblCourier");
                dt1 = orderObj.GetAdminCommentDetails(lbtnO.Text);
                string Msg1 = dt1.Rows[0]["Comments"].ToString();
                if (dt1.Rows[0]["OrderType"].ToString() == "LotSample")
                {
                    if (Msg1.Contains("#"))
                    {
                        lbl.Visible = false;
                    }
                    else
                    {
                        dt = orderObj.GetssampleQtyandMsgAdmin(lbtnO.Text);
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
            }
        }
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
                        ((GridView)dlOrderList.Items[Index].FindControl("gvOrder")).Visible = true;

                    }
                    else if (img.ImageUrl == "~/images/collapse.JPG")
                    {
                        ((ImageButton)dlOrderList.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/expand.JPG";
                        ((GridView)dlOrderList.Items[Index].FindControl("gvOrder")).Visible = false;

                    }
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
                break;
        }
    }
}