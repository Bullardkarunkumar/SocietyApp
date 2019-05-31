using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using MudarOrganic.Components;
using MudarOrganic.BL;

public partial class Buyer_LotSample : System.Web.UI.Page
{
    Order_BL orderobj = new Order_BL();
    DataTable dt = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnLotSample();
            //(Master.FindControl("btnLotsSample") as Button).BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            //(Master.FindControl("btnLotsSample") as Button).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
            BinddlOrderList();
            //ddlOrderStatus_SelectedIndexChanged(sender, e);
        }
    }
    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinddlOrderList();
    }
    private void BinddlOrderList()
    {
        dlOrderList.DataSource = orderobj.OrderbyBuyer(Session["BuyerId"].ToString(), "LotSample");
        dlOrderList.DataBind();
        foreach (RepeaterItem dli in dlOrderList.Items)
        {
            int OrderID = Convert.ToInt32(((Label)dli.FindControl("lbtnOrderID")).Text);
            ((ImageButton)dlOrderList.Items[dli.ItemIndex].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/collapse.JPG";
            string OrderStatus = (dli.FindControl("lblStatus") as Label).Text;
            string Status = (dli.FindControl("lblReceiveDate") as Label).Text;
            if (!string.IsNullOrEmpty(Status))
            {
                (dli.FindControl("lblReceiveDate") as Label).Visible = true;
                (dli.FindControl("pReceive") as Panel).Visible = false;
                (dli.FindControl("btnOrderPlace") as Button).Visible = true;
                (dli.FindControl("btnCancel") as Button).Visible = true;
                (dli.FindControl("btnSplitOrder") as Button).Visible = true;
                (dli.FindControl("btnDisableOrderPlace")).Visible = false;
                (dli.FindControl("btnDisableCancel")).Visible = false;
                (dli.FindControl("btnDisableSplitOrder")).Visible = false;

            }
            else
            {
                if (OrderStatus == "SAMPLE DISPATCH")
                    (dli.FindControl("pReceive") as Panel).Visible = true;
            }
            string Lsstr = (dli.FindControl("hfLS") as HiddenField).Value.ToString();
            if (!string.IsNullOrEmpty(Lsstr))
            {
                (dli.FindControl("hlLS") as HyperLink).Visible = true;
                (dli.FindControl("lbtnLS") as LinkButton).Visible = true;
            }
            (dli.FindControl("gvOrderHistory") as GridView).DataSource = orderobj.GetOrderProductDetails(OrderID.ToString());
            (dli.FindControl("gvOrderHistory") as GridView).DataBind();
        }
    }
    protected void dlOrderList_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
        {
            DataRowView drv = (DataRowView)(e.Item.DataItem);
            string Status = drv.Row["OrderStatus"].ToString();
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
            if (Status == "TESTING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.Blue;
            }
            if (Status == "PACKING")
            {
                Label lbl = (Label)e.Item.FindControl("lblStatus");
                lbl.ForeColor = System.Drawing.Color.OrangeRed;
            }
            if (Status == "SAMPLE DISPATCH")
            {
                Label lbl = (Label)e.Item.FindControl("lblMSg");
                lbl.ForeColor = System.Drawing.Color.Red;
                Label lblOrderID = (Label)e.Item.FindControl("lbtnOrderID");
                dt = orderobj.GetAdminCommentDetails(lblOrderID.Text);
                if (dt.Rows.Count > 0)
                {
                    string Msg = dt.Rows[0]["Comments"].ToString();
                    if (Msg.Contains("#"))
                    {
                        string[] po = dt.Rows[0]["Comments"].ToString().Split('#');
                        if (po[1].ToString() != string.Empty)
                        {
                            lbl.Visible = true;
                            lbl.Text = po[1].ToString();
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
        RepeaterItem item = e.Item;
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
            case "orderplace":
                {
                    Label ID = item.FindControl("lbtnOrderID") as Label;
                    dt = orderobj.OrderDetailsBasedonBuyerIDandLotSample(Session["BuyerId"].ToString(), ID.Text);
                    Session["dtUpdateOrder"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        Response.Redirect("~/Buyer/BuyNewPrice.aspx?lotid=" + ID.Text);
                    }
                }
                break;
            case "SplitOrder":
                {
                    Label ID = item.FindControl("lbtnOrderID") as Label;
                    dt = orderobj.OrderDetailsBasedonBuyerIDandLotSample(Session["BuyerId"].ToString(), ID.Text);
                    Session["dtUpdateOrder"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        Session["Split"] = "3";
                        Response.Redirect("~/Buyer/BuyNewPrice.aspx?lotid=" + ID.Text);

                    }
                }
                break;
            case "ordercancel":
                {


                }
                break;
            case "RUpdate":
                {
                    //DataListItem item = dlOrderList.Items[Index];
                    //int sampleID = Convert.ToInt32(dlOrderList.DataKeys[Index].ToString());
                    Label ID = item.FindControl("lbtnOrderID") as Label;
                    string Date = (item.FindControl("txtReceivedDate") as TextBox).Text;
                    if (!string.IsNullOrEmpty(Date))
                    {
                        DateTime dat = Convert.ToDateTime(Date.ToString());
                        bool status = orderobj.UpdatetheLotsampleReceivedDate(Convert.ToDateTime(Date), ID.Text, "SAMP RECV" + " <br/>" + string.Format("{0:dd MMM yyyy}", dat));
                        if (status == true)
                        {
                            ((Label)item.FindControl("lblReceiveDate")).Visible = true;
                            Label RDate = item.FindControl("lblReceiveDate") as Label;
                            RDate.Text = string.Format("{0:dd MMM yyyy}", dat);
                            ((TextBox)item.FindControl("txtReceivedDate")).Visible = false;
                            ((Button)item.FindControl("btnOrderPlace")).Visible = true;
                            ((Button)item.FindControl("btnCancel")).Visible = true;
                            ((Button)item.FindControl("btnDisableOrderPlace")).Visible = false;
                            ((Button)item.FindControl("btnDisableCancel")).Visible = false;
                            ((Button)item.FindControl("btnUpdate")).Visible = false;
                        }
                        Response.Redirect("~/Buyer/LotSample.aspx");
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(Page), "alert", "<script language=JavaScript>alert('!!! Please Enter the Received Date !!!');</script>");
                    }
                }
                break;
        }
    }
    protected void btnBack_Click(object sender, EventArgs e)
    {
        divMainDetails.Visible = true;
        divOrderDetails.Visible = false;
    }
    protected void gvOrderDetais_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[6].Attributes.Add("onClick", "return confirm('In this Lotsample.If want any changes !!!! TotalPrice Should be Changed !!!! ');");
        }
    }
    protected void gvOrderDetais_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "cmd_edit")
        {
            Session["dtUpdateOrder"] = new object();
            string index = e.CommandArgument.ToString();
            dt = orderobj.OrderDetailsBasedonBuyerIDandLotSample(Session["BuyerId"].ToString(), index);
            if (dt.Rows.Count > 0)
            {
                Session["dtUpdateOrder"] = dt;
                string lotid = string.Empty;
                if (dt.Rows.Count > 0)
                {
                    lotid = Convert.ToString(dt.Rows[0]["LotSampleID"]);
                }
                Response.Redirect("~/Buyer/BuyNewPrice.aspx?lotid=" + lotid);
            }
        }
    }
    protected void btnPlaceOrder_Click(object sender, System.EventArgs e)
    {
        dt = orderobj.OrderDetailsBasedonBuyerIDandLotSample(Session["BuyerId"].ToString(), Session["LSID"].ToString());
        if (dt.Rows.Count > 0)
        {
            gvOrderDetais.DataSource = dt;
            gvOrderDetais.DataBind();
            divOrderDetails.Visible = true;
            divMainDetails.Visible = false;
        }
    }
}
