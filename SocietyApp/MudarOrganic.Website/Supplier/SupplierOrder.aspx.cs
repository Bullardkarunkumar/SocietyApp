using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using MudarOrganic.BL;
using MudarOrganic.Components;
using System.Net;

public partial class Supplier_SupplierOrder : System.Web.UI.Page
{
    Order_BL BranchOrderObj = new Order_BL();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BinddlOrderList();
            //ddlOrderStatus_SelectedIndexChanged(sender, e);
        }
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
                        ((GridView)dlOrderList.Items[Index].FindControl("gvOrder")).Visible = true;

                    }
                    else if (img.ImageUrl == "~/images/collapse.JPG")
                    {
                        ((ImageButton)dlOrderList.Items[Index].FindControl("ibtnNOExpColap")).ImageUrl = "~/images/expand.JPG";
                        ((GridView)dlOrderList.Items[Index].FindControl("gvOrder")).Visible = false;

                    }
                }
                break;
            case "BranchOrder":
                {
                    Session["sOrderID"] = Encrypt_Decrypt.Encrypt(((LinkButton)dlOrderList.Items[Index].FindControl("lbtnOrderID")).Text, true);
                    //Response.Redirect("~/mudar/UpdateOrder.aspx");
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

    protected void ddlOrderStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BinddlOrderList();
    }
    private void BinddlOrderList()
    {
        dlOrderList.DataSource = BranchOrderObj.BranchOrderList(ddlOrderStatus.Text, 2, Session["SupplierID"].ToString());//BranchOrderObj.BranchOrderList(ddlOrderStatus.Text);
        dlOrderList.DataBind();

        foreach (DataListItem dli in dlOrderList.Items)
        {
            int OrderID = Convert.ToInt32(dlOrderList.DataKeys[dli.ItemIndex]);
            (dli.FindControl("gvOrder") as GridView).DataSource = BranchOrderObj.OrderProductList(OrderID);
            (dli.FindControl("gvOrder") as GridView).DataBind();
        }
    }
}