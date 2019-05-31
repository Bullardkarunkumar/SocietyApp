using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;

namespace MudarOrganic.BL
{
    public class ProductPriceUpdate_BL
    {
        public bool ProductPriceDetailsINSandUPDandDEL(int PriceId, int ProductId, DateTime CreateDate, decimal PriceMB, decimal POPriceMB, decimal FOBPrice, decimal USA_Sea, decimal USA_Air, decimal Europe_Sea, decimal Europe_Air, decimal India_Price, decimal Non_organic_India, decimal Non_organic_USA, decimal USDollar, decimal OtherPrice, string createdBy, string modifiedBy, int TypeOfOperation)
        {
            return false; //ProductPriceUpdate_DL.ProductPriceDetailsINSandUPDandDEL(PriceId, ProductId, CreateDate, PriceMB, POPriceMB, FOBPrice, USA_Sea, USA_Air, Europe_Sea, Europe_Air, India_Price, Non_organic_India, Non_organic_USA, USDollar, OtherPrice, createdBy, modifiedBy, TypeOfOperation);
        }
    }
}
