using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;

namespace MudarOrganic.BL
{
    public class Invoice_BL
    {
        public bool InvoiceInsert(int OrderId, int BranchOrderID, string PriceTerms, string Transport, string OriginCountry, string LoadingPort, string PaymentTerms, string FreightTerms, string DestinationCountry, string DestinationPort, DateTime OrderDate, string CreatedBy, string ModifiedBy, int TypeOfOperation, string InvoiceID)
        {
            return Invoice_DL.InvoiceInsert(OrderId, BranchOrderID, PriceTerms, Transport, OriginCountry, LoadingPort, PaymentTerms, FreightTerms, DestinationCountry, DestinationPort, OrderDate, CreatedBy, ModifiedBy, TypeOfOperation, InvoiceID);
        }
        public bool InvoiceProductDetailsINSandUPDandDEL(string InvoiceId,int ProductId,decimal Netweight,decimal Grossweight,decimal PriceforKG,int TotalDrums,decimal TotalAmount,string CreatedBy, string ModifiedBy,int TypeOfOperation)
        {
            return Invoice_DL.InvoiceProductDetailsINSandUPDandDEL(InvoiceId, ProductId, Netweight, Grossweight, PriceforKG, TotalDrums, TotalAmount, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public DataTable ReturnInvoiceList(string InvoiceID)
        {
            return Invoice_DL.ReturnInvoiceList(InvoiceID);
        }
        public DataTable InvoiceDetails(int OrderID)
        {
            return Invoice_DL.InvoiceDetails(OrderID);
        }
        public DataTable GetProductsBasedonInvoice(string InvoiceID)
        {
            return Invoice_DL.GetProductsBasedonInvoice(InvoiceID);
        }
    }
}
