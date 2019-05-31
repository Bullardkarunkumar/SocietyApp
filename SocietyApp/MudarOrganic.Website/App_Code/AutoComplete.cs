using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;

using MudarOrganic.BL;

[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class AutoComplete : WebService
{
    Farmer_BL farmer = new Farmer_BL();
    Product_BL product = new Product_BL();
    public AutoComplete()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    [WebMethod]
    public string[] GetCompletionList(string prefixText, int count)
    {
        List<string> famerlist = farmer.FarmerNameCodeArea(prefixText); //farmer.FamerNameCodeArea(prefixText, 1);
        return famerlist.ToArray();
    }

    [WebMethod]
    public string[] GetProductCompletionList(string prefixText, int count)
    {
        List<string> productlist = product.ProductName(prefixText);
        return productlist.ToArray();
    }
}
