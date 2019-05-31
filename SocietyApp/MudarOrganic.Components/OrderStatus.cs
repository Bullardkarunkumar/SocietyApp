using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MudarOrganic.Components
{
    public static class OrderStatus
    {
        public static string New = "NEW";
        public static string Inprocess = "INPROCESS";
        public static string Shipping = "SHIPPING";
        public static string Hold = "HOLD";
        public static string Pending = "PENDING";
        public static string Deliver = "DISPATCH";
        public static string Close = "CLOSE";
    }
    public static class OrderPriceTerms
    {
        public static List<string> PriceTerms = new List<string> { "100%advance", "50%adv+50%againstDocs", "100%againstDocs", "NoofDaysfromInvoice" };
        //public static List<string> PriceTerms = new List<string> { "100%advance", "50%advance+50%against_delivery", "50%advance+50%against_thedocumentsthroughbank", "100%against_thedocumentsthroughbank", "50%advance+50%against_bankLC", "100%against_delivery", "100%@30dayssightfromthe_dateofdelivery" };

    }
    public static class Transport
    {
        public static List<string> TransportMode = new List<string> { "Air", "Sea", "Rail", "Road" };
    }
    public static class BranchOrderStatus
    {
        public static string New = "NEW";
        public static string Collecting = "COLLECTING";
        public static string Testing = "TESTING";
        public static string Blending = "BLENDING";
        public static string Packing = "PACKING";
        public static string Documenting = "DOCUMENTING";
        public static string Dispatch = "DISPATCH";
    }
}
