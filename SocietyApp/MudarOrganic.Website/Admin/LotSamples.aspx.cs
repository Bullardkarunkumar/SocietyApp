using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Net;
using MudarOrganic.Components;
using MudarOrganic.BL;

public partial class Admin_LotSamples : System.Web.UI.Page
{
    Order_BL orderobj = new Order_BL();

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            //Master.MasterControlbtnLotSample();
            (Master.FindControl("btnLotsSample") as Button).BackColor = System.Drawing.ColorTranslator.FromHtml("#FFFFB3");
            (Master.FindControl("btnLotsSample") as Button).ForeColor = System.Drawing.ColorTranslator.FromHtml("#000000");
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
                    Session["sOrderID"] = Encrypt_Decrypt.Encrypt(((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text, true);
                    Response.Redirect("~/mudar/UpdateOrder.aspx");
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
    private void BinddlOrderList()
    {
        dlOrderList.DataSource = orderobj.OrderbyBuyer(Session["BuyerId"].ToString(),"LotSample");
        dlOrderList.DataBind();
        foreach (DataListItem dli in dlOrderList.Items)
        {
            int OrderID = Convert.ToInt32(dlOrderList.DataKeys[dli.ItemIndex]);
            //string Status = dlOrderList.DataKeys[(dli.FindControl("lblStatus") as Label).Text];
            ////if((dli.FindControl("lblStatus") as Label).Text  == "New")
            //    (dli.FindControl("lblStatus") as Label).ForeColor = System.Drawing.ColorTranslator.FromHtml("Orange");
            (dli.FindControl("gvNewOrder") as GridView).DataSource = orderobj.OrderProductList(OrderID);
            (dli.FindControl("gvNewOrder") as GridView).DataBind();
        }
    }
    protected void dlOrderList_ItemDataBound(object sender, DataListItemEventArgs e)
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
        }
    }
}