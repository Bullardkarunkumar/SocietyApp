using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MudarOrganic.Components;
using System.Data.Common;

namespace MudarOrganic.DL
{
    public static class Order_DL
    {
        public static bool OrderInsert(int OrderID, string BuyerID, string BuyerAddress, string OrderStatus
               , bool Approval, decimal TotalAmount, DateTime OrderDate, int PriceTermsID, int TransportID, string OriginCountry
               , string LoadingPort, string PaymentTerms, string FreightTerms, string DestinationCountry, string DestinationPort
               , DateTime OrderDispatchDate, string BranchID, string PurchaseOrderPath, string PurchaseOrderID, string CreatedBy
               , string ModifiedBy, string LSpath, string Comments, string OrderType, string LotSampleID, int OrgType, string Admincomments, string Transport, int TypeOfOperation, ref int ReturnOrderID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("BuyerAddress", SqlDbType.NVarChar, BuyerAddress.Replace("<br/>", "  ")));
            Params.Add(mdbh.AddParameter("OrderStatus", SqlDbType.NVarChar, OrderStatus));
            Params.Add(mdbh.AddParameter("Approval", SqlDbType.Bit, Approval));
            Params.Add(mdbh.AddParameter("TotalAmount", SqlDbType.Money, TotalAmount));
            Params.Add(mdbh.AddParameter("OrderDate", SqlDbType.DateTime, OrderDate));
            Params.Add(mdbh.AddParameter("PriceTermsID", SqlDbType.Int, PriceTermsID));
            Params.Add(mdbh.AddParameter("TransportID", SqlDbType.Int, TransportID));
            Params.Add(mdbh.AddParameter("OriginCountry", SqlDbType.NVarChar, OriginCountry));
            Params.Add(mdbh.AddParameter("LoadingPort", SqlDbType.NVarChar, LoadingPort));
            Params.Add(mdbh.AddParameter("PaymentTerms", SqlDbType.NVarChar, PaymentTerms));
            Params.Add(mdbh.AddParameter("FreightTerms", SqlDbType.NVarChar, FreightTerms));
            Params.Add(mdbh.AddParameter("DestinationCountry", SqlDbType.NVarChar, DestinationCountry));
            Params.Add(mdbh.AddParameter("DestinationPort", SqlDbType.NVarChar, DestinationPort));
            Params.Add(mdbh.AddParameter("OrderDispatchDate", SqlDbType.DateTime, OrderDispatchDate));
            Params.Add(mdbh.AddParameter("BranchID", SqlDbType.UniqueIdentifier, new Guid(BranchID)));
            Params.Add(mdbh.AddParameter("PurchaseOrderPath", SqlDbType.NVarChar, PurchaseOrderPath));
            Params.Add(mdbh.AddParameter("PurchaseOrderID", SqlDbType.NVarChar, PurchaseOrderID));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("LSpath", SqlDbType.NVarChar, LSpath));
            Params.Add(mdbh.AddParameter("Comments", SqlDbType.NVarChar, Comments));
            Params.Add(mdbh.AddParameter("OrderType", SqlDbType.VarChar, OrderType));
            Params.Add(mdbh.AddParameter("LotSampleID", SqlDbType.NVarChar, LotSampleID));
            Params.Add(mdbh.AddParameter("OrgType", SqlDbType.Int, OrgType));
            Params.Add(mdbh.AddParameter("Admincomments", SqlDbType.VarChar, Admincomments));
            Params.Add(mdbh.AddParameter("Transport", SqlDbType.VarChar, Transport));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("ReturnOrderID", SqlDbType.Int, ReturnOrderID, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_OrderDetails_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    ReturnOrderID = Convert.ToInt32(output[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool OrderInsert(int OrderID, string OrderStatus, decimal TotalAmount, DateTime OrderDate, string PaymentTerms, string FreightTerms, string DestinationPort,
               string PurchaseOrderPath, string PurchaseOrderID, string ModifiedBy, string Comments, string OrderType, string Admincomments, string Transport, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("OrderStatus", SqlDbType.NVarChar, OrderStatus));
            Params.Add(mdbh.AddParameter("TotalAmount", SqlDbType.Money, TotalAmount));
            Params.Add(mdbh.AddParameter("OrderDate", SqlDbType.DateTime, OrderDate));
            Params.Add(mdbh.AddParameter("PaymentTerms", SqlDbType.NVarChar, PaymentTerms));
            Params.Add(mdbh.AddParameter("FreightTerms", SqlDbType.NVarChar, FreightTerms));
            Params.Add(mdbh.AddParameter("DestinationPort", SqlDbType.NVarChar, DestinationPort));
            Params.Add(mdbh.AddParameter("PurchaseOrderPath", SqlDbType.NVarChar, PurchaseOrderPath));
            Params.Add(mdbh.AddParameter("PurchaseOrderID", SqlDbType.NVarChar, PurchaseOrderID));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("Comments", SqlDbType.NVarChar, Comments));
            Params.Add(mdbh.AddParameter("OrderType", SqlDbType.VarChar, OrderType));
            Params.Add(mdbh.AddParameter("Admincomments", SqlDbType.VarChar, Admincomments));
            Params.Add(mdbh.AddParameter("Transport", SqlDbType.VarChar, Transport));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_LotSample_OrderDetails_UPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool OrderProducts_INT_UPT_DEL(int OrderID, int ProductID, decimal RateperKG, decimal TotalPrice, string CreatedBy, string ModifiedBy, int Quantity, int Packing25, int Packing180, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("RateperKG", SqlDbType.Decimal, RateperKG));
            Params.Add(mdbh.AddParameter("TotalPrice", SqlDbType.Money, TotalPrice));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("Quantity", SqlDbType.Int, Quantity));
            Params.Add(mdbh.AddParameter("Packing25", SqlDbType.Int, Packing25));
            Params.Add(mdbh.AddParameter("Packing180", SqlDbType.Int, Packing180));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));

            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_OrderProducts_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool OrderDetails_UPD(int OrderID, string OrderStatus, string ModifiedBy, string Comments)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("OrderStatus", SqlDbType.NVarChar, OrderStatus));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("Comments", SqlDbType.NVarChar, Comments));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_OrderDetails_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool OrderDetails_UPD(int OrderID, string OrderPdfPath, int type, string ModifiedBy)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("PurchaseOrderPath", SqlDbType.NVarChar, OrderPdfPath));
            Params.Add(mdbh.AddParameter("type", SqlDbType.Int, type));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_Order_Pdfpath_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool OrderDetails_UPD(string OrderPdfPath, string ModifiedBy, string POID, int OrderID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("PurchaseOrderPath", SqlDbType.NVarChar, OrderPdfPath));
            Params.Add(mdbh.AddParameter("PurchaseOrderID", SqlDbType.NVarChar, POID));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_Order_PO_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool OrderDetails_UPD(int OrderID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_UpdateOrderproductsforLotsample, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static bool UpdateOrderBranch(string branchId, int OrderID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BranchId", SqlDbType.VarChar, branchId));
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            try
            {
                mdbh.ExecuteNonQuery("sp_UpdateOrderBranch", Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static bool BranchOrderDetails_UPD(int BranchOrderID, string OrderStatus, string ModifiedBy, string Comments)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("OrderStatus", SqlDbType.NVarChar, OrderStatus));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("Comments", SqlDbType.NVarChar, Comments));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_BranchOrderDetails_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool BranchOrderDetails_UPD(int BranchOrderID, string OrderStatus, string ModifiedBy, string Comments, DateTime BOdispatchDate, string BOinv)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("OrderStatus", SqlDbType.NVarChar, OrderStatus));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("Comments", SqlDbType.NVarChar, Comments));
            Params.Add(mdbh.AddParameter("BOdispatchDate", SqlDbType.DateTime, BOdispatchDate));
            Params.Add(mdbh.AddParameter("BOinv", SqlDbType.NVarChar, BOinv));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_BranchOrderDispatchDetails_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool BranchOrderDetails_UPD(int BranchOrderID, string BrnachOrderPath, int type, string ModifiedBy)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("BrnachOrderPath", SqlDbType.NVarChar, BrnachOrderPath));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("type", SqlDbType.Int, type));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_BranchOrder_Pdfpath_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable ReturnPOList(string POvalue)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblOrderDetails WHERE PurchaseOrderID LIKE '%" + POvalue + "%' AND [Delete] = 0");
        }
        public static DataTable ReturnLotSampleList(string POvalue)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblOrderDetails WHERE LotSampleID LIKE '%" + POvalue + "%' AND [Delete] = 0");
        }
        public static DataTable ReturnBPOList(string BPovalue)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblBranchOrder WHERE BranchPOID LIKE '%" + BPovalue + "%' AND [Delete] = 0");
        }
        public static DataTable Ordercountry(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM dbo.tblOrderDetails WHERE OrderID ='" + OrderID + "'");
        }
        public static DataTable GetSplitIDCount(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ISNULL(MAX(rowcountid), 0 ) from tblSplitOrderDetails where OrderID ='" + OrderID + "'");
        }
        public static DataTable OrderList(string OrderStatus, Guid branchId)
        {
            string sql = string.Empty;
            if (branchId == Guid.Empty)
            {
                if (OrderStatus == "ALL")
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId  WHERE od.[Delete]=0 AND od.OrderStatus<>'CLOSE' and od.OrderStatus<>'CANCEL' and od.BuyerId<>'" + MudarOrderConstants.DefaultBuyerId + "' ORDER BY od.CreatedDate DESC";
                else
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId WHERE od.[Delete]=0 AND od.OrderStatus='" + OrderStatus + "' and od.BuyerId<>'" + MudarOrderConstants.DefaultBuyerId + "' ORDER BY od.CreatedDate DESC";
            }
            else
            {
                if (OrderStatus == "ALL")
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId WHERE od.BranchId='" + branchId.ToString() + "' AND od.[Delete]=0 AND od.OrderStatus<>'CLOSE' and od.OrderStatus<>'CANCEL' and od.BuyerId<>'" + MudarOrderConstants.DefaultBuyerId + "'  ORDER BY od.CreatedDate DESC";
                else
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId WHERE od.BranchId='" + branchId.ToString() + "' AND od.[Delete]=0 AND od.OrderStatus='" + OrderStatus + "' and od.BuyerId<>'" + MudarOrderConstants.DefaultBuyerId + "' ORDER BY od.CreatedDate DESC";
            }

            //if(OrderStatus.ToLower()=="new")
            //        sql+=" AND od.OrderStatus='NEW'";
            //if (OrderStatus.ToLower() == "close")
            //    sql += " AND od.OrderStatus='close' AND od.Approval=1";
            //if (OrderStatus.ToLower() != "new" && OrderStatus.ToLower() != "close")
            //    sql += " AND od.Approval = 1 AND od.OrderStatus <>'NEW' AND od.OrderStatus <>'close' ";

            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }

        public static DataTable GetPreOrderList(string OrderStatus, Guid branchId)
        {
            string sql = string.Empty;
            if (branchId == Guid.Empty)
            {
                if (OrderStatus == "ALL")
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId  WHERE od.[Delete]=0 AND od.OrderStatus<>'CLOSE' and od.OrderStatus<>'CANCEL' and od.BuyerId='" + MudarOrderConstants.DefaultBuyerId + "' ORDER BY od.CreatedDate DESC";
                else
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId WHERE od.[Delete]=0 AND od.OrderStatus='" + OrderStatus + "' and od.BuyerId='" + MudarOrderConstants.DefaultBuyerId + "' ORDER BY od.CreatedDate DESC";
            }
            else
            {
                if (OrderStatus == "ALL")
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId WHERE od.BranchId='" + branchId.ToString() + "' AND od.[Delete]=0 AND od.OrderStatus<>'CLOSE' and od.OrderStatus<>'CANCEL' and od.BuyerId='" + MudarOrderConstants.DefaultBuyerId + "'  ORDER BY od.CreatedDate DESC";
                else
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,od.OrderAssign,tbd.BranchCode,tbd.Bname,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId LEFT JOIN tblBranchDetails tbd on tbd.BranchId=od.BranchId WHERE od.BranchId='" + branchId.ToString() + "' AND od.[Delete]=0 AND od.OrderStatus='" + OrderStatus + "' and od.BuyerId='" + MudarOrderConstants.DefaultBuyerId + "' ORDER BY od.CreatedDate DESC";
            }

            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable OrderSortList(string OrderStatus, string Status)
        {
            string sql = string.Empty;
            if (OrderStatus == "ALL")
                sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.OrderStatus<>'CLOSE' and od.OrderStatus<>'CANCEL'  ORDER BY " + Status + " ASC";
            else
                sql = "SELECT od.OrderID,PurchaseOrderID,od.AdminOrderStatus as OrderStatus,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,od.OrderType,od.Buyer_PO_No,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.OrderStatus='" + OrderStatus + "' ORDER BY " + Status + " ASC";

            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable OrderList(string OrderStatus, string mode)
        {
            string sql = string.Empty;
            if (OrderStatus == "ALL")
            {
                if (mode == "order")
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.OrderType,od.OrderStatus,od.Buyer_PO_No,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.OrderType='order' ORDER BY od.CreatedDate DESC";
                else
                    sql = "SELECT od.OrderID,PurchaseOrderID,od.OrderType,od.OrderStatus,od.Buyer_PO_No,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.OrderType!='order' ORDER BY od.CreatedDate DESC";
            }
            else
                if (mode == "order")
                sql = "SELECT od.OrderID,PurchaseOrderID,od.OrderType,od.OrderStatus,od.Buyer_PO_No,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.OrderStatus='" + OrderStatus + "' AND od.OrderType='order' ORDER BY od.CreatedDate DESC";
            else
                sql = "SELECT od.OrderID,PurchaseOrderID,od.OrderType,od.OrderStatus,od.Buyer_PO_No,OrderDate,PurchaseOrderPath,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.OrderStatus='" + OrderStatus + "' AND od.OrderType!='order' ORDER BY od.CreatedDate DESC";
            //if(OrderStatus.ToLower()=="new")
            //        sql+=" AND od.OrderStatus='NEW'";
            //if (OrderStatus.ToLower() == "close")
            //    sql += " AND od.OrderStatus='close' AND od.Approval=1";
            //if (OrderStatus.ToLower() != "new" && OrderStatus.ToLower() != "close")
            //    sql += " AND od.Approval = 1 AND od.OrderStatus <>'NEW' AND od.OrderStatus <>'close' ";

            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable OrderList(int Orderid)
        {
            string sql = "SELECT od.OrderID,PurchaseOrderID,od.orderstatus,od.AdminOrderStatus,od.comments,OrderDate,PurchaseOrderPath,OrderType,LotSampleID,od.Admincomments AS 'O_Comments',od.Transport,BuyerCompanyName,od.BuyerID,od.ETA,bo.BranchPOID,bo.BranchOrderId,bo.BOrderType ,bo.OrderStatus AS 'bOrderStatus' , bo.Comments AS 'B_Comments',bo.BLSpath,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath,b.Bname,b.BranchCode ,bd.CAddress,bd.CCity,bd.CState,bd.CCountry,bd.CPincode,bd.CContactPerson,bd.CContactPhoneNo,bd.NAddress,bd.NCity,bd.NState,bd.NCountry,bd.NPincode,bd.NContactPerson,bd.NContactPhoneNo,bd.TINNumber, od.PriceTermsID, bpd.FOB_India, bpd.CNF_Sea_By, bpd.CNF_AIR_By_EuropeandEastUSA,bpd.CNF_AIR_By_WEST_USA, bpd.CIF_Sea_By,bpd.CIF_Air_By_EuropeandEastUSA,bpd.CIF_AIR_By_WEST_USA,FORDestination,Exworks,btd.*, od.PaymentTerms, od.FreightTerms, od.DestinationCountry, od.DestinationPort,od.OrgType  FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId and bo.[Delete]=0 LEFT JOIN tblBranchDetails b ON od.BranchID=b.BranchId LEFT JOIN tblBuyerPriceTermsDetails bpd ON od.PriceTermsID = bpd.BuyerPriceID LEFT JOIN tblBuyerTransportDetails btd ON od.TransportID = btd.BuyerTransportID  WHERE od.[Delete] = 0 AND od.OrderID = " + Orderid;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }

        public static DataTable GetOrderDetails(int Orderid)
        {
            string sql = "SELECT * from tblOrderDetails WHERE [Delete] = 0 AND OrderID = " + Orderid;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }

        public static DataTable OrderbyBuyer(string BuyerID)
        {
            //string sql = "SELECT * FROM tblOrderDetails WHERE BuyerID = '" + BuyerID + "' ORDER BY CreatedDate DESC";
            string sql = "SELECT od.OrderID,PurchaseOrderID,od.OrderType,od.OrderStatus,OrderDate,PurchaseOrderPath,Buyer_PO_No,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId and bo.[Delete]=0 WHERE od.[Delete]=0 AND od.OrderType < > 'LotSample' AND od.BuyerID = '" + BuyerID + "' ORDER BY od.OrderID DESC";
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable OrderbyBuyer(string BuyerID, string mode)
        {
            //string sql = "SELECT * FROM tblOrderDetails WHERE BuyerID = '" + BuyerID + "' ORDER BY CreatedDate DESC";
            string sql = string.Empty;
            if (mode == "order")
                sql = "SELECT od.OrderID,PurchaseOrderID,od.OrderType,od.OrderStatus,od.LotSampleID,OrderDate,PurchaseOrderPath,Buyer_PO_No,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath,od.LSReceivedDate FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.BuyerID = '" + BuyerID + "' AND od.OrderType='order' ORDER BY od.OrderID DESC ";
            else
                sql = "SELECT od.OrderID,PurchaseOrderID,od.OrderType,od.OrderStatus,od.LotSampleID,OrderDate,PurchaseOrderPath,Buyer_PO_No,BuyerCompanyName,od.BuyerID,bo.BranchPOID,bo.BranchOrderId ,bo.OrderStatus AS 'bOrderStatus' ,od.modifieddate AS 'ClosedDate',od.LSpath,bo.BranchOrderDate,bo.BranchOrderPath,od.LSReceivedDate FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId LEFT JOIN tblBranchOrder bo ON od.OrderID=bo.OrderId WHERE od.[Delete]=0 AND od.BuyerID = '" + BuyerID + "' AND od.OrderType!='order'  ORDER BY od.OrderID DESC ";
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable OrderDetailsBasedonBuyerIDandLotSample(string BuyerID, string LotsampleID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblOrderDetails od,tblOrderProducts op,tblProductDetails pd WHERE od.BuyerID='" + BuyerID + "' AND od.OrderType='LotSample' AND od.OrderType='LotSample' AND od.OrderID='" + LotsampleID + "' and op.OrderID='" + LotsampleID + "' and op.ProductID = pd.ProductId");
        }
        public static DataTable BindOrderProductList(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT OrderProductID,op.ProductID,RateforKG,TotalPrice,Quantity,Packing25,Packing180,ProductName, bop.GrossQuantity  FROM tblOrderProducts op LEFT JOIN tblProductDetails pd ON op.ProductID=pd.ProductId LEFT JOIN tblBranchOrder bo ON op.OrderID = bo.OrderID LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID WHERE op.OrderID=" + OrderID + " AND op.[Delete]=0 AND bop.[delete]=0 GROUP BY OrderProductID,op.ProductID,RateforKG,TotalPrice,Quantity,Packing25,Packing180,ProductName,bop.GrossQuantity");
        }
        public static DataTable BindBuyerOrderProductList(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT OrderProductID,op.ProductID,RateforKG,TotalPrice,Quantity,Packing25,Packing180,ProductName FROM tblOrderProducts op LEFT JOIN tblProductDetails pd ON op.ProductID=pd.ProductId WHERE op.OrderID='" + OrderID + "' AND op.[Delete]=0 GROUP BY OrderProductID,op.ProductID,RateforKG,TotalPrice,Quantity,Packing25,Packing180,ProductName");
        }
        public static DataTable BindAdminOrderProductList(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select op.ProductID,RateforKG,Quantity,Packing25,Packing180,TotalPrice,pd.ProductName,(select CCountry from tblbuyerdetails where buyerid=(select BuyerID from tblOrderDetails where [delete]=0 and OrderID=" + OrderID + ")) as Country from tblProductDetails pd,tblOrderProducts op where op.OrderID=" + OrderID + "  and op.[Delete]=0 and op.ProductID=pd.ProductId GROUP BY op.ProductID,RateforKG,Quantity,Packing25,Packing180,TotalPrice,pd.ProductName");
        }
        public static DataTable BindBranchOrderProductList(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bop.ProductID,Amount,BranchRateforKG,op.Quantity,Packing25,Packing180,pd.ProductName from tblBranchOrderProduct bop,tblBranchOrder bo,tblProductDetails pd,tblOrderProducts op where bo.BranchOrderID=bop.BranchOrderID and op.OrderID='" + OrderID + "' and bo.orderid='" + OrderID + "' and bop.[Delete]=0 and op.[Delete]=0 and pd.ProductId=bop.ProductID and op.ProductID=pd.ProductId GROUP BY bop.ProductID,Amount,BranchRateforKG,op.Quantity,Packing25,Packing180,pd.ProductName");
        }
        public static DataTable OrderProductList(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT OrderProductID,op.ProductID,TotalPrice,Quantity,Packing25,Packing180,ProductName, ct.BatchID, bop.GrossQuantity  FROM tblOrderProducts op LEFT JOIN tblProductDetails pd ON op.ProductID=pd.ProductId LEFT JOIN tblBranchOrder bo ON op.OrderID = bo.OrderID LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID  LEFT JOIN tblCollection c ON op.OrderID = c.OrderID AND bo.BranchOrderID = c.BranchOrderID LEFT JOIN tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID  WHERE op.OrderID=" + OrderID + " AND op.[Delete]=0");
            return mdbh.ExecuteDataTable("SELECT OrderProductID,op.ProductID,RateforKG,TotalPrice,Quantity,op.Packing25,op.Packing180,ProductName,bt.Blending_BatchID, bop.GrossQuantity,(op.Packing25+op.Packing180) AS 'TotalDrums',isnull([25dfrom],0) as [25dfrom],isnull([25dfrom],0) as [25dto],isnull([180dfrom],0) as [180dfrom],isnull([180dto],0) as [180dto],'0' as [DrumF],'0' as[DrumT]  FROM tblOrderProducts op LEFT JOIN tblProductDetails pd ON op.ProductID=pd.ProductId LEFT JOIN tblBranchOrder bo ON op.OrderID = bo.OrderID and bo.[Delete] =0 LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID AND op.ProductID=bop.ProductID  LEFT JOIN tblCollection c ON op.OrderID = c.OrderID AND bo.BranchOrderID = c.BranchOrderID LEFT JOIN tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID  LEFT JOIN tblBlending b ON b.CollectionID = c.CollectionID  LEFT JOIN tblBlendingTransaction bt ON bt.BlendingID = b.BlendingID AND op.ProductID = ct.ProductID LEFT JOIN tblOrderPackingDetails opd ON opd.CollectionID = c.CollectionID WHERE op.OrderID=" + OrderID + " AND op.[Delete]=0 GROUP BY OrderProductID,op.ProductID,RateforKG,TotalPrice,Quantity,op.Packing25,op.Packing180,ProductName,bt.Blending_BatchID,bop.GrossQuantity,[25dfrom],[25dto],[180dfrom],[180dto]");
        }
        public static DataTable OrderProductList(string POID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT OrderProductID,op.ProductID,TotalPrice,Quantity,Packing25,Packing180,ProductName, ct.BatchID, bop.GrossQuantity,(Packing25+Packing180) AS 'TotalDrums'  , od.OrderID, od.PurchaseOrderID, od.OrderDate , bd.BuyerCompanyName, (op.TotalPrice / op.Quantity ) AS 'Price'  FROM tblOrderDetails od LEFT JOIN tblOrderProducts op ON od.OrderID = op.OrderID LEFT JOIN tblProductDetails pd ON op.ProductID=pd.ProductId LEFT JOIN tblBranchOrder bo ON op.OrderID = bo.OrderID LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID AND op.ProductID=bop.ProductID  LEFT JOIN tblCollection c ON op.OrderID = c.OrderID AND bo.BranchOrderID = c.BranchOrderID LEFT JOIN tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID  AND op.ProductID = ct.ProductID LEFT JOIN tblBuyerDetails bd ON od.BuyerID = bd.BuyerId WHERE od.PurchaseOrderID = '" + POID + "' AND op.[Delete]=0  GROUP BY OrderProductID,op.ProductID,TotalPrice,Quantity,Packing25,Packing180,ProductName, ct.BatchID, bop.GrossQuantity  , od.OrderID, od.PurchaseOrderID, od.OrderDate , bd.BuyerCompanyName");
        }
        public static DataTable OrderProductList(int OrderID, int BranchOrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT OrderProductID,op.ProductID,TotalPrice,Quantity,Packing25,Packing180,ProductName, ct.BatchID, bop.GrossQuantity  FROM tblOrderProducts op LEFT JOIN tblProductDetails pd ON op.ProductID=pd.ProductId LEFT JOIN tblBranchOrder bo ON op.OrderID = bo.OrderID LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID AND pd.ProductId = bop.ProductID LEFT JOIN tblCollection c ON op.OrderID = c.OrderID AND bo.BranchOrderID = c.BranchOrderID LEFT JOIN tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID AND op.ProductID = ct.ProductID  WHERE op.OrderID= " + OrderID + " AND op.[Delete]=0 AND bo.BranchOrderID = " + BranchOrderID + " GROUP BY OrderProductID,op.ProductID,TotalPrice,Quantity,Packing25,Packing180,ProductName, ct.BatchID, bop.GrossQuantity");
        }
        public static DataTable BranchOrderList(string OrderStatus)
        {
            string sql = string.Empty;
            if (OrderStatus.Trim().ToUpper() == "ALL")
                sql = "SELECT OrderId,BranchPOID,OrderStatus,BranchOrderDate,BranchOrderPath,BLSpath,BranchOrderID,OrderTo,OrderToID,BOrderType FROM tblBranchOrder WHERE [Delete]=0 AND OrderStatus <> 'DISPATCH' and OrderStatus <> 'CANCEL' ORDER BY BranchOrderDate DESC";
            else
                sql = "SELECT OrderId,BranchPOID,OrderStatus,BranchOrderDate,BranchOrderPath,BLSpath,BranchOrderID,OrderTo,OrderToID,BOrderType FROM tblBranchOrder WHERE [Delete]=0 AND OrderStatus='" + OrderStatus + "' ORDER BY BranchOrderDate DESC";
            //if (OrderStatus.ToLower() == "new")
            //    sql += " AND OrderStatus='NEW'";
            //if (OrderStatus.ToLower() != "new" && OrderStatus.ToLower() != "close")
            //    sql += " AND OrderStatus <> 'NEW' AND OrderStatus <> 'CLOSE'";
            //if (OrderStatus.ToLower() == "close")
            //    sql += " AND OrderStatus='CLOSE'";
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable BranchOrderList(string OrderStatus, string Status)
        {
            string sql = string.Empty;
            if (OrderStatus.Trim().ToUpper() == "ALL")
                sql = "SELECT OrderId,BranchPOID,OrderStatus,BranchOrderDate,BranchOrderPath,BLSpath,BranchOrderID,OrderTo,OrderToID,BOrderType FROM tblBranchOrder WHERE [Delete]=0 ORDER BY " + Status + " ASC";
            else
                sql = "SELECT OrderId,BranchPOID,OrderStatus,BranchOrderDate,BranchOrderPath,BLSpath,BranchOrderID,OrderTo,OrderToID,BOrderType FROM tblBranchOrder WHERE [Delete]=0 AND OrderStatus<>'" + OrderStatus + "' ORDER BY " + Status + " ASC";
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable(sql);
        }

        public static DataTable OrderTrack(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select OrderID,PurchaseOrderID,OrderStatus, REPLACE(SUBSTRING(Comments,1,LEN(Comments)-1),';','') as Comments, OrderDate, bd.BuyerCompanyName, PurchaseOrderPath from tblOrderDetails od, tblBuyerDetails bd where od.BuyerID = bd.BuyerId AND (cast(OrderID as nvarchar) = '" + OrderID + "' OR PurchaseOrderID = '" + OrderID + "')");
        }

        public static DataTable GetPreOrderData()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable result = new DataTable();
            try
            {
                result = mdbh.ExecuteDataTable("GetPreOrderData", "PreOrderData");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable OrderProduct_BPO(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //testing purpose
            return mdbh.ExecuteDataTable("SELECT op.*,pd.ProductName, pp.PriceMB, pp.POPriceMB, CAST(op.totalprice as decimal(15,2)) AS 'Total2',(pp.AddUPPrice + pp.PriceMB) [POUPPrice] FROM dbo.tblOrderProducts op LEFT JOIN tblOrderDetails od ON op.OrderID = od.OrderID LEFT JOIN tblProductDetails pd ON op.ProductID = pd.ProductId LEFT JOIN tblProductPrice pp ON op.ProductID = pp.ProductId and op.[delete]=0 LEFT JOIN tblPriceHistory ph ON pp.PriceHistoryId = ph.PriceHistoryId WHERE od.OrderID = " + OrderID + " 	AND ph.PriceHistoryId = (SELECT top 1 tph.PriceHistoryId FROM tblPriceHistory tph WHERE tph.CreateDate <= od.CreatedDate ORDER BY tph.CreateDate DESC)");
            //return mdbh.ExecuteDataTable("SELECT op.*,pd.ProductName, pp.PriceMB, pp.POPriceMB, CAST(op.totalprice as decimal(15,2)) AS 'Total2' FROM dbo.tblOrderProducts op LEFT JOIN tblOrderDetails od ON op.OrderID = od.OrderID LEFT JOIN tblProductDetails pd ON op.ProductID = pd.ProductId LEFT JOIN tblProductPrice pp ON op.ProductID = pp.ProductId LEFT JOIN tblPriceHistory ph ON pp.PriceHistoryId = ph.PriceHistoryId WHERE od.OrderID = " + OrderID + " 	AND ph.PriceHistoryId = 3");
        }
        public static bool BranchOrderInsert(int OrderID, decimal TotalAmount, string OrderStatus, string BranchPOID, DateTime BranchOrderDate, string CreatedBy, string ModifiedBy, string Comments, int TypeOfOperation, ref int ReturnBranchOrderID, int OrderTo, string OrderToID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("TotalAmount", SqlDbType.Decimal, TotalAmount));
            Params.Add(mdbh.AddParameter("OrderStatus", SqlDbType.NVarChar, OrderStatus));
            Params.Add(mdbh.AddParameter("BranchPOID", SqlDbType.NVarChar, BranchPOID));
            Params.Add(mdbh.AddParameter("BranchOrderDate", SqlDbType.DateTime, BranchOrderDate));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("Comments", SqlDbType.NVarChar, Comments));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("OrderTo", SqlDbType.Int, OrderTo));
            Params.Add(mdbh.AddParameter("OrderToID", SqlDbType.UniqueIdentifier, new Guid(OrderToID)));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("ReturnBranchOrderID", SqlDbType.Int, ReturnBranchOrderID, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();

                mdbh.ExecuteNonQuery(sp.sp_BranchOrderDetails_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    ReturnBranchOrderID = Convert.ToInt32(output[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool BranchOrderProduct_INSandUPDandDEL(int BranchOrderID, int ProductID, decimal NetQuantity, decimal GrossQuantity, decimal Amount, decimal BranchRateforKG, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("NetQuantity", SqlDbType.Decimal, NetQuantity));
            Params.Add(mdbh.AddParameter("Amount", SqlDbType.Money, Amount));
            Params.Add(mdbh.AddParameter("BranchRateforKG", SqlDbType.Money, BranchRateforKG));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));

            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_BranchOrderProduct_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable Order_Buyer_Branch_Details(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT op.*,bud.*, bpt.*,bpd.*,pd.ProductName,bo.*,bop.* FROM tblOrderDetails od LEFT JOIN tblOrderProducts op ON od.OrderID = op.OrderID LEFT JOIN tblProductDetails pd ON pd.ProductId=op.ProductID LEFT JOIN tblBuyerDetails bud ON bud.BuyerId = od.BuyerID LEFT JOIN tblBuyerPriceTermsDetails bpt ON bpt.BuyerId = od.BuyerID LEFT JOIN tblBuyerTransportDetails bpd ON bpd.BuyerId = od.BuyerID LEFT JOIN tblBranchOrder bo ON od.OrderID = bo.OrderID  LEFT JOIN tblBranchOrderProduct bop ON bop.BranchOrderID=bo.BranchOrderID and bop.ProductID=op.ProductID WHERE od.OrderID='" + OrderID + "' AND od.[Delete] = 0");
        }
        public static DataTable BranchOrderDetails(string BranchPOID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblBranchOrder bo LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID LEFT JOIN tblProductDetails pd ON bop.ProductID = pd.ProductId WHERE bo.BranchPOID LIKE '" + BranchPOID + "'");
        }
        public static bool OrderSampleDetails(int SampleID, string BuyerID, DateTime SampleDate, string Status, string CourierName, DateTime CourierDate, string ReceivedDate, string CourierAccountNumber, string CreatedBy, string ModifiedBy, ref int ReturnSampleID, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("SampleID", SqlDbType.Int, SampleID));
            Params.Add(mdbh.AddParameter("BuyerID", SqlDbType.UniqueIdentifier, new Guid(BuyerID)));
            Params.Add(mdbh.AddParameter("SampleDate", SqlDbType.DateTime, SampleDate));
            Params.Add(mdbh.AddParameter("Status", SqlDbType.NVarChar, Status));
            Params.Add(mdbh.AddParameter("CourierName", SqlDbType.NVarChar, CourierName));
            Params.Add(mdbh.AddParameter("CourierDate", SqlDbType.DateTime, CourierDate));
            Params.Add(mdbh.AddParameter("ReceivedDate", SqlDbType.NVarChar, ReceivedDate));
            Params.Add(mdbh.AddParameter("CourierAccountNumber", SqlDbType.NVarChar, CourierAccountNumber));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("Returnvalue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("ReturnSampleID", SqlDbType.Int, ReturnSampleID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            try
            {
                List<string> output = new List<string>();

                mdbh.ExecuteNonQuery(sp.sp_OrderSampleDetails_INSandUPD, Params, ref output);
                if (output.Count >= 2)
                {
                    result = Convert.ToBoolean(output[0]);
                    ReturnSampleID = Convert.ToInt32(output[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool OrderSampleProductDetails(int SampleProductID, int ProductID, int SampleID, int Quantity, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("SampleProductID", SqlDbType.Int, SampleProductID));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("SampleID", SqlDbType.NVarChar, SampleID));
            Params.Add(mdbh.AddParameter("Quantity", SqlDbType.NVarChar, Quantity));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_OrderSampleProductDetails_INSandUPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable OrderSampleDetails(string BuyerID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT *, CASE WHEN  ReceivedDate='1900-01-01 00:00:00.000' Then '' ELSE CONVERT(varchar(10), ReceivedDate,101) END AS 'RDate' From tblOrderSample where BuyerId = '" + BuyerID + "' ");
        }
        public static DataTable OrderSampleDetails()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT os.*, CASE WHEN  ReceivedDate='1900-01-01 00:00:00.000' Then '' ELSE CONVERT(varchar(10), ReceivedDate,101) END AS 'RDate',bd.BuyerCompanyName From tblOrderSample os,tblBuyerDetails bd where os.[delete]=0 and os.BuyerID=bd.BuyerID");
        }
        public static DataTable OrderSampleProduct(int SampleID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT osp.*,pd.ProductName FROM tblOrderSampleProduct osp LEFT JOIN tblProductDetails pd ON pd.ProductId = osp.ProductID WHERE SampleID='" + SampleID + "'");
        }
        #region Collecting
        public static DataTable CollectedProductDetails(int OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT bop.*, fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId, (pld.FirstLotNos +';'+ pld.SecondLotNos) AS 'Lot_No', pld.TotalProductQuantity FROM tblBranchOrder bo LEFT JOIN tblOrderDetails od ON bo.OrderID = od.OrderID LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID LEFT JOIN tblPlantationDetails pld ON bop.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId WHERE od.OrderID='" + OrderID + "'  AND pld.TotalProductQuantity>0");
        }
        public static DataTable CollectedProductDetails_Product(int Product)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,(pld.FirstLotNos +';'+ pld.SecondLotNos) AS 'Lot_No',pld.TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId WHERE  pd.ProductID = '" + Product + "' AND pld.FirstDistillationDate <= (GETDATE()-1)  AND pld.SecondDistillationDate > (GETDATE()-1) UNION SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,(pld.FirstLotNos +';'+ pld.SecondLotNos) AS 'Lot_No',pld.TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId WHERE  pd.ProductID = '" + Product + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0");
        }
        public static DataTable CollectedProductDetailsBasedonProduct(int ProductID, string ProductionYear, string name)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
            + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
            + " WHERE pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1) AND ffd.FYear = 2013 AND fd.FarmerCode LIKE '%" + name + "%'"
            + " UNION"
            + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
            + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
            + " WHERE pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = 2013  AND fd.FarmerCode LIKE '%" + name + "%'");
        }
        public static DataTable CollectedProductDetailsBasedonProduct(int ProductID, string ProductionYear)
        {
            if (ProductID == 4)
                ProductID = 2;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
            + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
            + " WHERE pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1) AND ffd.FYear = '" + ProductionYear + "'"
            + " UNION"
            + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
            + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
            + " WHERE  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = '" + ProductionYear + "'");
        }
        public static DataTable CollectedProductDetailsBasedonProductandICs(int ProductID, string ProductionYear, string selectedICS)
        {
            if (ProductID == 4 || ProductID == 10 || ProductID == 11)
            {
                if (ProductID == 4)
                    ProductID = 2;
                if (ProductID == 10)
                    ProductID = 3;
                if (ProductID == 11)
                    ProductID = 8;
            }
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (!string.IsNullOrEmpty(selectedICS))
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.City_Village in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1) AND ffd.FYear = '" + ProductionYear + "'"
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.City_Village in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = '" + ProductionYear + "'");
            }
            else
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1) AND ffd.FYear = '" + ProductionYear + "'"
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = '" + ProductionYear + "'");
            }
        }

        public static DataTable CollectedProductDetailsBasedonProductandICs(int ProductID, string ProductionYear, string selectedICS, DateTime SelDate)
        {
            if (ProductID == 4 || ProductID == 10 || ProductID == 11)
            {
                if (ProductID == 4)
                    ProductID = 2;
                if (ProductID == 10)
                    ProductID = 3;
                if (ProductID == 11)
                    ProductID = 8;
            }
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (!string.IsNullOrEmpty(selectedICS))
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.City_Village in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= '" + SelDate + "' AND pld.SecondDistillationDate > '" + SelDate + "' AND ffd.FYear = '" + ProductionYear + "'"
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.City_Village in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= '" + SelDate + "' AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = '" + ProductionYear + "'");
            }
            else
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= '" + SelDate + "' AND pld.SecondDistillationDate > '" + SelDate + "' AND ffd.FYear = '" + ProductionYear + "'"
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= '" + SelDate + "' AND (pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = '" + ProductionYear + "'");
            }
        }
        public static DataTable CollectedProductDetailsBasedonProductandICsNew(int ProductID, string ProductionYear, string selectedICS)
        {
            if (ProductID == 4)
                ProductID = 2;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (!string.IsNullOrEmpty(selectedICS))
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.ICSType in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1) AND ffd.FYear = '" + ProductionYear + "'"
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.ICSType in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = '" + ProductionYear + "'");
            }
            else
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1) AND ffd.FYear = '" + ProductionYear + "'"
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0  AND ffd.FYear = '" + ProductionYear + "'");
            }
        }
        public static DataTable CollectedProductDetailsBasedonProductandICsNew(int ProductID, string selectedICS)
        {
            if (ProductID == 4)
                ProductID = 2;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (!string.IsNullOrEmpty(selectedICS))
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.ICSType in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1)"
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE fd.ICSType in (" + selectedICS + ")  and  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0");
            }
            else
            {
                return mdbh.ExecuteDataTable("SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.FirstProductQuantity+pld.SecondProductQuantity as TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.FirstProductQuantity+pld.SecondProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE pd.ProductID = '" + ProductID + "' AND pld.FirstDistillationDate <= (GETDATE()-1) AND pld.SecondDistillationDate > (GETDATE()-1) "
                + " UNION"
                + " SELECT fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,pld.PlantationId,pld.FarmerLotnumber AS 'Lot_No',pld.TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',(pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblProductDetails pd"
                + " LEFT JOIN tblPlantationDetails pld ON pd.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId"
                + " WHERE  pd.ProductID = '" + ProductID + "' AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0 ");
            }
        }
        public static DataTable GetBranchOrderDetails(string orderID, int ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bo.*,bop.* from tblBranchOrder bo,tblBranchOrderProduct bop where bo.OrderID='" + orderID + "' and bo.BranchOrderID = bop.BranchOrderID and bop.ProductID='" + ProductID + "'");
        }

        public static DataTable GetBranchOrderDetails(int orderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bo.* from tblBranchOrder bo where bo.OrderID='" + orderID + "'");
        }
        public static DataTable CollectedProductDetails(int OrderID, int ProductID, string BranchOrderID)
        {
            if (ProductID == 4)
                ProductID = 2;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT pld.FirstDistillationDate, pld.SecondDistillationDate, bop.*, fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,	pld.PlantationId, (pld.FarmerLotnumber) AS 'Lot_No', "
    + " (pld.TotalProductQuantity-ISNULL(pld.SecondProductQuantity, 0)) AS 'TotalProductQuantity' , ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',	(pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable'"
    + " FROM tblBranchOrder bo LEFT JOIN tblOrderDetails od ON bo.OrderID = od.OrderID "
    + " LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID "
    + " LEFT JOIN tblPlantationDetails pld ON bop.ProductID = pld.ProductId "
    + " LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID "
    + " LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId "
    + " LEFT JOIN tblProductDetails pd ON pd.ProductId = bop.ProductID "
    + " WHERE od.OrderID='" + OrderID + "' AND bop.ProductID ='" + ProductID + "' AND pld.TotalProductQuantity>0 AND bop.BranchOrderID='" + BranchOrderID + "' "
    + " AND pld.FirstDistillationDate <= (GETDATE()-1)  AND pld.SecondDistillationDate > (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0 "
+ " UNION "
+ " SELECT pld.FirstDistillationDate, pld.SecondDistillationDate, bop.*, fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,	pld.PlantationId, (pld.FarmerLotnumber) AS 'Lot_No', "
    + " pld.TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',	(pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' "
    + " FROM tblBranchOrder bo LEFT JOIN tblOrderDetails od ON bo.OrderID = od.OrderID "
    + " LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID "
    + " LEFT JOIN tblPlantationDetails pld ON bop.ProductID = pld.ProductId "
    + " LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID "
    + " LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId "
    + " LEFT JOIN tblProductDetails pd ON pd.ProductId = bop.ProductID "
    + " WHERE od.OrderID='" + OrderID + "' AND bop.ProductID ='" + ProductID + "' AND pld.TotalProductQuantity>0 AND bop.BranchOrderID='" + BranchOrderID + "' "
    + " AND pld.SecondDistillationDate <= (GETDATE()-1) AND (pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0))>0 ");
            //return mdbh.ExecuteDataTable("SELECT bop.*, fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.FarmID,ffd.AreaCode,	pld.PlantationId, (pld.FirstLotNos +';'+ pld.SecondLotNos) AS 'Lot_No', 	pld.TotalProductQuantity, ISNULL(pld.SoldTotalQty, 0) AS 'SoldTotalQty',	(pld.TotalProductQuantity-ISNULL(pld.SoldTotalQty, 0)) AS 'Avaliable' FROM tblBranchOrder bo LEFT JOIN tblOrderDetails od ON bo.OrderID = od.OrderID LEFT JOIN tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID LEFT JOIN tblPlantationDetails pld ON bop.ProductID = pld.ProductId LEFT JOIN tblFarmerFarmDetails ffd ON pld.FarmID = ffd.FarmID LEFT JOIN tblFarmerDetails fd ON pld.FarmerId = fd.FarmerId LEFT JOIN tblProductDetails pd ON pd.ProductId = bop.ProductID WHERE od.OrderID='" + OrderID + "' AND bop.ProductID ='" + ProductID + "' AND pld.TotalProductQuantity>0 AND bop.BranchOrderID='" + BranchOrderID + "'");
        }
        public static DataTable CollectedProduct(int OrderID, int ProductID, int BranchOrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT c.*,ct.* FROM tblCollection c LEFT JOIN tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID WHERE c.OrderID = " + OrderID + " AND c.BranchOrderID = " + BranchOrderID + " AND ct.ProductID = " + ProductID);
        }
        public static DataTable CollectedProduct(int ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT c.*,ct.* FROM tblCollection c LEFT JOIN tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID WHERE ct.ProductID = " + ProductID);
        }
        public static DataSet ReturnBatchList(string BatchID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataSet("SELECT * FROM tblBlendingTransaction WHERE Blending_BatchID LIKE '" + BatchID + "%';SELECT * FROM [tblPreOrderCollectionTransaction] WHERE Blending_BatchID LIKE '" + BatchID + "%';");
        }
        public static DataTable ReturnFreezeBatch(string FBID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblFreezeTransaction WHERE FreezeProductBatchID LIKE '" + FBID + "%'");
        }
        public static bool ProductsCollection_Insert(ref int CollectionID, int OrderID, int BranchOrderID, string CreatedBy, string ModifiedBy, int TypeOfOperation, bool IsPreOrder)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("CollectionID", SqlDbType.Int, CollectionID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("IsPreOrder", SqlDbType.Bit, IsPreOrder));
            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblCollection_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[1]);
                    CollectionID = Convert.ToInt32(output[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool ProductsCollectionTran_Insert(string BatchID, int CollectionID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string CollectedQty, string FarmId, string LotNumber, string CreatedBy, DateTime InsertDate, string ModifiedBy, int TypeOfOperation, string PlantationID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BatchID", SqlDbType.NVarChar, BatchID));
            Params.Add(mdbh.AddParameter("CollectionID", SqlDbType.Int, CollectionID));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("FarmerId ", SqlDbType.NVarChar, FarmerId));
            Params.Add(mdbh.AddParameter("OtherFarmerNames", SqlDbType.NVarChar, OtherFarmerNames));
            Params.Add(mdbh.AddParameter("OtherFarmerArea", SqlDbType.NVarChar, OtherFarmerArea));
            Params.Add(mdbh.AddParameter("OtherFarmerQty", SqlDbType.NVarChar, OtherFarmerQty));
            Params.Add(mdbh.AddParameter("CollectionQty ", SqlDbType.NVarChar, CollectedQty));
            Params.Add(mdbh.AddParameter("PlantationID ", SqlDbType.NVarChar, PlantationID));
            Params.Add(mdbh.AddParameter("FarmId", SqlDbType.NVarChar, FarmId));
            Params.Add(mdbh.AddParameter("LotNumber", SqlDbType.NVarChar, LotNumber));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("InsertDate", SqlDbType.DateTime, InsertDate));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblCollectionTransaction_INSandUPDandDEL, Params);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #endregion
        #region Blending
        public static bool ProductsBlending_Insert(ref int BlendingID, int CollectionID, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BlendingID", SqlDbType.Int, BlendingID, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("CollectionID", SqlDbType.Int, CollectionID));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();

                mdbh.ExecuteNonQuery(sp.sp_tblBlending_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[1]);
                    BlendingID = Convert.ToInt32(output[0]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool ProductsBlendingTran_Insert(int BlendingID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string BlendQty, string FarmId, string BatchNo, string LotNumber, decimal Bqty, string CreatedBy, DateTime InsertDate, string ModifiedBy, int TypeOfOperation, string PlantationID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BlendingID", SqlDbType.Int, BlendingID));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("FarmerId ", SqlDbType.NVarChar, FarmerId));
            Params.Add(mdbh.AddParameter("OtherFarmerNames", SqlDbType.NVarChar, OtherFarmerNames));
            Params.Add(mdbh.AddParameter("OtherFarmerArea", SqlDbType.NVarChar, OtherFarmerArea));
            Params.Add(mdbh.AddParameter("OtherFarmerQty", SqlDbType.NVarChar, OtherFarmerQty));
            Params.Add(mdbh.AddParameter("BlendQty ", SqlDbType.NVarChar, BlendQty));
            Params.Add(mdbh.AddParameter("PlantationID ", SqlDbType.NVarChar, PlantationID));
            Params.Add(mdbh.AddParameter("FarmId", SqlDbType.NVarChar, FarmId));
            Params.Add(mdbh.AddParameter("BatchNo", SqlDbType.NVarChar, BatchNo));
            Params.Add(mdbh.AddParameter("LotNumber", SqlDbType.NVarChar, LotNumber));
            Params.Add(mdbh.AddParameter("Bqty", SqlDbType.Decimal, Bqty));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("InsertDate", SqlDbType.DateTime, InsertDate));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblBlendingTransaction_INSandUPDandDEL, Params);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable GetCollectionBlendDetails(string BlendID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("select * from tblCollectionTransaction where Blending_BatchID='" + BlendID + "'");
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetBliendingDetails(string OrderID, string BOrderID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("SELECT od.OrderID, od.OrderDate, bo.BranchOrderID, bo.BranchOrderDate, c.CollectionID , prod.ProductName , (SELECT fd.FirstName +',' FROM tblFarmerDetails fd LEFT JOIN tblPlantationDetails pds ON fd.FarmerId = pds.FarmerId  WHERE CAST( pds.PlantationId AS NVARCHAR) IN (SELECT value FROM SplitDelimited(ct.PlantationID,';'))  ORDER BY fd.FirstName FOR XML PATH('') ) AS FarmerName , (SELECT fds.FarmerCode +',' FROM tblFarmerDetails fds LEFT JOIN tblPlantationDetails pds ON fds.FarmerId = pds.FarmerId  WHERE CAST( pds.PlantationId AS NVARCHAR) IN (SELECT value FROM SplitDelimited(ct.PlantationID,';'))  ORDER BY fds.FarmerCode FOR XML PATH('') ) AS FarmerCode , (SELECT ffd.AreaCode +',' FROM tblFarmerFarmDetails ffd WHERE CAST( ffd.FarmID AS NVARCHAR) IN (SELECT value FROM SplitDelimited(ct.FarmID,';'))  ORDER BY ffd.AreaCode FOR XML PATH('') ) AS PlotCode , ct.FarmID,ct.FarmerID,ct.Lotnumber, ct.CollectionQty, ct.BatchID, ct.Blending_BatchID , ct.OtherFarmersName, ct.OtherFarmerQty, ct.CollectionTransactionID , bop.NetQuantity, ct.ProductID   FROM tblOrderDetails od LEFT JOIN tblBranchOrder bo ON bo.OrderID = od.OrderID  LEFT JOIN tblCollection c ON c.OrderID = od.OrderID AND c.BranchOrderID = bo.BranchOrderID  LEFT JOIN tblCollectionTransaction ct ON ct.CollectionID = c.CollectionID LEFT JOIN tblProductDetails prod ON ct.ProductID = prod.ProductId LEFT JOIN tblBranchOrderProduct bop ON bop.BranchOrderID = bo.BranchOrderID AND bop.ProductID = prod.ProductId  WHERE od.OrderID = " + OrderID + " AND bo.BranchOrderID = " + BOrderID + " AND  prod.ProductId= " + ProductID + " AND od.Approval=1");
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetCollectionID(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("select * from tblCollection where OrderID='" + OrderID + "' and[Delete] = 0");
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetBliendingDetails(string CollectionID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("select c.CollectionID,ct.FarmID,ct.FarmerID,ct.ProductID,ct.CollectionQty,ct.Lotnumber,ct.Blending_BatchID from tblCollection c,tblCollectionTransaction ct where c.CollectionID='" + CollectionID + "' and ct.CollectionID ='" + CollectionID + "' and ct.ProductID = '" + ProductID + "' and c.[Delete] = 0 and ct.[Delete] = 0");
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetBlendFarmerDetails(string FarmerID, string FarmID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("select fd.FarmerId,fd.FarmerCode,fd.FirstName,ffd.AreaCode from tblFarmerDetails fd,tblFarmerFarmDetails ffd where fd.FarmerId='" + FarmerID + "' and  ffd.FarmerID='" + FarmerID + "' and  FarmID='" + FarmID + "'");
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetBlendDetailsBasedonBlendID(string BlendID, string CollectionID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("select bl.*,blt.*,blt.CreatedDate as [Date] from tblBlending bl, tblBlendingTransaction blt where blt.BlendingID='" + BlendID + "' and blt.ProductID=" + ProductID + " and blt.[Delete]= 0 and bl.CollectionID = '" + CollectionID + "'");
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetBlendDetailsBasedonBlendID(string CollectionID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("select * from tblBlending where CollectionID = '" + CollectionID + "' AND [Delete]= 0");
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable ReturnBBatchList(string BBatchID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM tblCollectionTransaction WHERE Blending_BatchID LIKE '" + BBatchID + "%'");
        }
        public static bool BlendingBatchNo(int CTID, string BBatchID, string ModifiedBy, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("CollectionTransactionID", SqlDbType.Int, CTID));
            Params.Add(mdbh.AddParameter("Blending_BatchID", SqlDbType.NVarChar, BBatchID));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            try
            {
                mdbh.ExecuteNonQuery(sp.sp_tblCollectionTransaction_UPD, Params);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        #endregion
        #region Freeze
        public static DataTable GetFreezeBatchID(int OrderID, int ProductID)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("orderid", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("productid", SqlDbType.Int, ProductID));
            return mdbh.ExecuteDataTable(sp.sp_GetFreezeBatchID, Params, "FreezeBatchDetails");
        }
        public static DataTable GetFreezeDetails(string OrderID, string BOrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT od.OrderID, OrderDate, op.OrderProductID, op.ProductID, op.Quantity , pd.ProductName, f.FreezeID, f.FBatchID, bo.BranchOrderID,f.StartDate,f.EndDate  FROM tblOrderDetails od LEFT JOIN tblOrderProducts op ON op.OrderID = od.OrderID  LEFT JOIN tblBranchOrder bo ON bo.OrderID = od.OrderID  LEFT JOIN tblProductDetails pd ON pd.ProductId = op.ProductID  LEFT JOIN tblFreeze f ON f.OrderID = od.OrderID  AND f.ProductId = op.ProductID   WHERE od.OrderID = " + OrderID + " AND bo.BranchOrderID =" + BOrderID + "AND op.ProductID=" + 4;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetFreezeDetails(string OrderID, string BOrderID, int BlendTransID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT od.OrderID, OrderDate, op.OrderProductID, op.ProductID, op.Quantity , pd.ProductName, f.FreezeID, f.FBatchID, bo.BranchOrderID,f.StartDate,f.EndDate  FROM tblOrderDetails od LEFT JOIN tblOrderProducts op ON op.OrderID = od.OrderID  LEFT JOIN tblBranchOrder bo ON bo.OrderID = od.OrderID  LEFT JOIN tblProductDetails pd ON pd.ProductId = op.ProductID  LEFT JOIN tblFreeze f ON f.OrderID = od.OrderID  AND f.ProductId = op.ProductID AND f.BlendingTransID=" + BlendTransID + "  WHERE od.OrderID = " + OrderID + " AND bo.BranchOrderID =" + BOrderID + "AND op.ProductID=" + 4;
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataTable GetFreeDetails(string id)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "select Parameter,AnalysisValue from tblTestingFiledsTrans where TestResID= " + id + "  and [delete]=0";
            return mdbh.ExecuteDataTable(sql);
        }
        public static DataSet GetFreezeTran(string FID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT * FROM tblFreeze WHERE FreezeID = " + FID + " SELECT * FROM tblFreezeTransaction WHERE FreezeID = " + FID + " AND ProductID = " + ProductID;
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataSet GetFreezeTran(string FID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = "SELECT * FROM tblFreeze WHERE FreezeID = " + FID + " SELECT * FROM tblFreezeTransaction WHERE FreezeID = " + FID + " AND [delete]=" + 0;
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataTable ReturnFBatchList(string FBatchID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT * FROM  tblFreeze WHERE FBatchID LIKE '" + FBatchID + "%'");
        }
        public static bool Freeze_Insert(int OrderID, int BranchOrderID, string CreatedBy, string ModifiedBy, int Quntatiy, int ProductID, string FBatchID, int TypeOfOperation, DateTime StartDate, DateTime EndDate, int BlendTranID, ref int Fid)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("FBatchID", SqlDbType.NVarChar, FBatchID));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("Quntatiy", SqlDbType.Int, Quntatiy));
            Params.Add(mdbh.AddParameter("StartDate", SqlDbType.DateTime, StartDate));
            Params.Add(mdbh.AddParameter("EndDate", SqlDbType.DateTime, EndDate));
            Params.Add(mdbh.AddParameter("BlendTranID", SqlDbType.Int, BlendTranID));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("Fid", SqlDbType.Int, Fid, Param_Directions.Param_Out));
            try
            {
                //Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblFreeze_INSandUPDandDEL, Params);
                List<string> output = new List<string>();

                mdbh.ExecuteNonQuery(sp.sp_tblFreeze_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    Fid = Convert.ToInt32(output[1]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool FreezeTran_INSandUPDandDEL(int FreezeTransactionID, int FreezeID, int ProductID, string FreezeProductBatchID, string FreezeProductName, string FreezeQuantity, string CreatedBY, string ModifiedBy, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("FreezeTransactionID", SqlDbType.Int, FreezeTransactionID));
            Params.Add(mdbh.AddParameter("FreezeID", SqlDbType.Int, FreezeID));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("FreezeProductBatchID", SqlDbType.NVarChar, FreezeProductBatchID));
            Params.Add(mdbh.AddParameter("FreezeProductName", SqlDbType.NVarChar, FreezeProductName));
            Params.Add(mdbh.AddParameter("FreezeQuantity", SqlDbType.NVarChar, FreezeQuantity));
            Params.Add(mdbh.AddParameter("CreatedBY", SqlDbType.NVarChar, CreatedBY));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblFreezeTransaction_INSandUPDandDEL, Params);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable BindFreezeProduct()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select ProductId,ProductName from tblProductDetails where ProductId in ('4','6')");
        }
        public static DataTable BindBlendTranID(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bt.BlendingTransactionID as BlendingTransID,bt.Blending_BatchID,bt.BQty as Quntatiy,bt.ProductID from tblBlending b ,tblBlendingTransaction bt where CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "') and b.BlendingID = bt.BlendingID");
        }
        #endregion
        #region Packing
        public static DataTable PackingDetails(int OrderID, int BranchOrderID)
        {
            #region Comment code
            // old code
            //return mdbh.ExecuteDataTable("SELECT od.OrderID, odp.ProductID, odp.Packing25, odp.Packing180, bop.NetQuantity, bop.GrossQuantity, ct.BatchID, ct.Lotnumber, pd.ProductName, bo.BranchOrderID, ct.CollectionID FROM dbo.tblOrderDetails od  LEFT JOIN dbo.tblOrderProducts odp ON od.OrderID = odp.OrderID LEFT JOIN dbo.tblBranchOrder bo ON od.OrderID = bo.OrderID LEFT JOIN dbo.tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID AND odp.ProductID = bop.ProductID LEFT JOIN dbo.tblCollection c ON od.OrderID = c.OrderID LEFT JOIN dbo.tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID  LEFT JOIN dbo.tblProductDetails pd ON odp.ProductID = pd.ProductId  WHERE od.OrderID = '" + OrderID + "' AND bo.BranchOrderID = '" + BranchOrderID + "' GROUP BY od.OrderID, odp.ProductID, odp.Packing25, odp.Packing180, bop.NetQuantity, bop.GrossQuantity, ct.BatchID, ct.Lotnumber, pd.ProductName, bo.BranchOrderID, ct.CollectionID ");
            //return mdbh.ExecuteDataTable("SELECT od.OrderID, odp.ProductID, odp.Packing25, odp.Packing180, bop.NetQuantity, CASE WHEN bop.GrossQuantity IS NULL OR bop.GrossQuantity=0 THEN  bop.GrossQuantity+(odp.Packing25*3)+(odp.Packing180*21) +bop.NetQuantity ELSE bop.GrossQuantity END as GrossQuantity, c.CollectionID,bt.Blending_BatchID,ct.Lotnumber, pd.ProductName, bo.BranchOrderID FROM dbo.tblOrderDetails od  LEFT JOIN dbo.tblOrderProducts odp ON od.OrderID = odp.OrderID LEFT JOIN dbo.tblBranchOrder bo ON od.OrderID = bo.OrderID LEFT JOIN dbo.tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID AND odp.ProductID = bop.ProductID LEFT JOIN dbo.tblCollection c ON od.OrderID = c.OrderID AND bo.BranchOrderID = c.BranchOrderID LEFT JOIN dbo.tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID  AND odp.ProductID = ct.ProductID LEFT JOIN dbo.tblProductDetails pd ON odp.ProductID = pd.ProductId LEFT JOIN dbo.tblBlending b ON  b.CollectionID = c.CollectionID LEFT JOIN dbo.tblBlendingTransaction bt ON bt.BlendingID = b.BlendingID AND odp.ProductID = bt.ProductID  WHERE od.OrderID = '" + OrderID + "' AND bo.BranchOrderID = '" + BranchOrderID + "' GROUP BY od.OrderID, odp.ProductID, odp.Packing25, odp.Packing180, bop.NetQuantity, bop.GrossQuantity, bt.Blending_BatchID, ct.Lotnumber, pd.ProductName, bo.BranchOrderID, c.CollectionID "); 
            #endregion
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            return mdbh.ExecuteDataTable(sp.sp_Get_PackingDetails, Params, "PackingDetails");
        }
        public static bool PackingDetailsUpdate(int OrderID, int ProductID, int BranchOrderID, decimal GrossQty, int Packing25, int Packing180, string ModifiedBy, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("BranchOrderID", SqlDbType.Int, BranchOrderID));
            Params.Add(mdbh.AddParameter("GrossQty", SqlDbType.Decimal, GrossQty));
            Params.Add(mdbh.AddParameter("Packing25", SqlDbType.Int, Packing25));
            Params.Add(mdbh.AddParameter("Packing180", SqlDbType.Int, Packing180));
            //Params.Add(mdbh.AddParameter("Drum25From", SqlDbType.Int, Drum25From));
            //Params.Add(mdbh.AddParameter("Drum25To", SqlDbType.Int, Drum25To));
            //Params.Add(mdbh.AddParameter("Drum180From", SqlDbType.Int, Drum180From));
            //Params.Add(mdbh.AddParameter("Drum180To", SqlDbType.Int, Drum180To));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_packingDetails_UPD, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static bool OrderProductPackingInsert(int CollectionID, int ProductID, string LotNumber, int Packing25, int Packing180, int Drum25From, int Drum25To, int Drum180From, int Drum180To, DateTime InsertDate)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("collectionid", SqlDbType.Int, CollectionID));
            Params.Add(mdbh.AddParameter("productid", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("lotnumber", SqlDbType.NVarChar, LotNumber));
            Params.Add(mdbh.AddParameter("packing25", SqlDbType.Int, Packing25));
            Params.Add(mdbh.AddParameter("packing180", SqlDbType.Int, Packing180));
            Params.Add(mdbh.AddParameter("drum25from", SqlDbType.Int, Drum25From));
            Params.Add(mdbh.AddParameter("drum25to", SqlDbType.Int, Drum25To));
            Params.Add(mdbh.AddParameter("drum180from", SqlDbType.Int, Drum180From));
            Params.Add(mdbh.AddParameter("drum180to", SqlDbType.Int, Drum180To));
            Params.Add(mdbh.AddParameter("InsertDate", SqlDbType.DateTime, InsertDate));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_Insert_OrderPackingDetails, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool UpdateOrderETA(int OrderID, string ETA)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("orderid", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("eta", SqlDbType.NVarChar, ETA));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_OrderDetails_ETA_Update, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static DataTable ListOrderPackingDetails(int CollectionID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select trp.ProductID,tpd.ProductName,trp.LotNumber,trp.Packing25,Packing180,[25Dfrom],[25Dto],[180Dfrom],[180Dto],trp.[CreatedDate] from tblOrderPackingDetails trp join tblProductDetails tpd on trp.ProductID=tpd.ProductId where trp.CollectionID=" + CollectionID + " and tpd.[Delete]=0");
        }
        // productID 4 or 6
        public static DataTable GetPackingDetailsforCrystal(string BlendID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select FreezeQuantity,FreezeProductBatchID from tblFreezeTransaction where FreezeProductBatchID ='" + BlendID + "' and ProductID='" + ProductID + "'");
        }
        public static DataTable GetPackingDetails(string BlendID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select bl.*,blt.*,blt.CreatedDate as [Date] from tblBlending bl, tblBlendingTransaction blt where bl.BlendingID=blt.BlendingID and blt.Blending_BatchID='" + BlendID + "' and blt.ProductID='" + ProductID + "'");
        }

        #endregion
        #region Pre Order
        public static bool ProductsPreorderCollectionTran_Insert(string BatchID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string CollectedQty, string FarmId, string LotNumber, string CreatedBy, string ModifiedBy, int TypeOfOperation, string PlantationID, int TotalQty, int AvaliableQty, int SoldQty, int PreOrderID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BatchID", SqlDbType.NVarChar, BatchID));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("FarmerId ", SqlDbType.NVarChar, FarmerId));
            Params.Add(mdbh.AddParameter("OtherFarmerNames", SqlDbType.NVarChar, OtherFarmerNames));
            Params.Add(mdbh.AddParameter("OtherFarmerArea", SqlDbType.NVarChar, OtherFarmerArea));
            Params.Add(mdbh.AddParameter("OtherFarmerQty", SqlDbType.NVarChar, OtherFarmerQty));
            Params.Add(mdbh.AddParameter("CollectionQty ", SqlDbType.NVarChar, CollectedQty));
            Params.Add(mdbh.AddParameter("PlantationID ", SqlDbType.NVarChar, PlantationID));
            Params.Add(mdbh.AddParameter("FarmId", SqlDbType.NVarChar, FarmId));
            Params.Add(mdbh.AddParameter("LotNumber", SqlDbType.NVarChar, LotNumber));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("TotalQty", SqlDbType.Int, TotalQty));
            Params.Add(mdbh.AddParameter("AvaliableQty", SqlDbType.Int, AvaliableQty));
            Params.Add(mdbh.AddParameter("SoldQty", SqlDbType.Int, SoldQty));
            Params.Add(mdbh.AddParameter("CollectionTransactionID", SqlDbType.Int, PreOrderID));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblPreOrderCollectionTransaction_INSandUPDandDEL, Params);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static bool ProductsPreorderCollectionTran_Insert_New(string BatchID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string CollectedQty, string FarmId, string LotNumber, string CreatedBy, string ModifiedBy, int TypeOfOperation, string PlantationID, decimal TotalQty, decimal AvaliableQty, int SoldQty, int PreOrderID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("BatchID", SqlDbType.NVarChar, BatchID));
            Params.Add(mdbh.AddParameter("ProductId", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("FarmerId ", SqlDbType.NVarChar, FarmerId));
            Params.Add(mdbh.AddParameter("OtherFarmerNames", SqlDbType.NVarChar, OtherFarmerNames));
            Params.Add(mdbh.AddParameter("OtherFarmerArea", SqlDbType.NVarChar, OtherFarmerArea));
            Params.Add(mdbh.AddParameter("OtherFarmerQty", SqlDbType.NVarChar, OtherFarmerQty));
            Params.Add(mdbh.AddParameter("CollectionQty ", SqlDbType.NVarChar, CollectedQty));
            Params.Add(mdbh.AddParameter("PlantationID ", SqlDbType.NVarChar, PlantationID));
            Params.Add(mdbh.AddParameter("FarmId", SqlDbType.NVarChar, FarmId));
            Params.Add(mdbh.AddParameter("LotNumber", SqlDbType.NVarChar, LotNumber));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("TotalQty", SqlDbType.Decimal, TotalQty));
            Params.Add(mdbh.AddParameter("AvaliableQty", SqlDbType.Decimal, AvaliableQty));
            Params.Add(mdbh.AddParameter("SoldQty", SqlDbType.Int, SoldQty));
            Params.Add(mdbh.AddParameter("CollectionTransactionID", SqlDbType.Int, PreOrderID));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblPreOrderCollectionTransaction_INSandUPDandDEL_New, Params);

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static DataTable PreOrderList()
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT poct.*,pd.Productname FROM [tblPreOrderCollectionTransaction] poct  Left Join tblProductDetails pd ON pd.ProductId = poct.ProductID");

        }



        public static DataTable PreOrderList(string icscode)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            if (string.IsNullOrEmpty(icscode))
                return mdbh.ExecuteDataTable("SELECT poct.*,pd.Productname FROM [tblPreOrderCollectionTransaction] poct  Left Join tblProductDetails pd ON pd.ProductId = poct.ProductID");
            else
                return mdbh.ExecuteDataTable("SELECT poct.*,pd.Productname FROM [tblPreOrderCollectionTransaction] poct  Left Join tblProductDetails pd ON pd.ProductId = poct.ProductID join tblFarmerDetails tfd on poct.FarmerID=tfd.FarmerId where tfd.ICSType in (" + icscode + ")");

        }

        public static bool PreorderSoldQtyUpdate(int PreOrderID, int SoldQty, string ModifiedBy)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("SoldQty", SqlDbType.Int, SoldQty));
            Params.Add(mdbh.AddParameter("CollectionTransactionID", SqlDbType.Int, PreOrderID));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblPreOrderCollectionTransaction_Update, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static bool PreorderSoldQtyUpdateNew(int PreOrderID, decimal SoldQty, string ModifiedBy)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("SoldQty", SqlDbType.Decimal, SoldQty));
            Params.Add(mdbh.AddParameter("CollectionTransactionID", SqlDbType.Int, PreOrderID));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblPreOrderCollectionTransaction_Update_New, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }

        public static DataTable GetPreorderDetails(string BlendBatchID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("select * from tblPreOrderCollectionTransaction where Blending_BatchID='" + BlendBatchID + "' AND [Delete] = 0");
        }
        #endregion
        #region Orders for Admin checkit
        public static bool NewOrderArrived()
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            dt = mdbh.ExecuteDataTable("SELECT * FROM tblOrderIndicator where Status='true'");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string value = Convert.ToString(dt.Rows[0][1]);
                    result = value == "true" ? true : false;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool NewLotSampleArrived()
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            dt = mdbh.ExecuteDataTable("SELECT * FROM tblLotSampleIndicator where Status='true'");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string value = Convert.ToString(dt.Rows[0][1]);
                    result = value == "true" ? true : false;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static void UpdateTrackPO()
        {
            try
            {
                string sqlQuery = "update tblOrderIndicator set Status='false'";
                MudarDBHelper mdbh = MudarDBHelper.Instance;
                mdbh.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void UpdateLotSample()
        {
            try
            {
                string sqlQuery = "update tblLotSampleIndicator set Status='false'";
                MudarDBHelper mdbh = MudarDBHelper.Instance;
                mdbh.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Orders for Branch Checkit
        public static bool NewBranchOrderArrived()
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            dt = mdbh.ExecuteDataTable("SELECT * FROM tblBranchOrderIndicator where Status='true'");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string value = Convert.ToString(dt.Rows[0][1]);
                    result = value == "true" ? true : false;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static void updateBranchPendingOrders()
        {
            try
            {
                string sqlQuery = "update tblBranchOrderIndicator set Status='false'";
                MudarDBHelper mdbh = MudarDBHelper.Instance;
                mdbh.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        // buyer orderstatus checked
        #region Order status for buyer checking
        public static bool BuyerOrderStatusCheck(string BuyerID)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            dt = mdbh.ExecuteDataTable("SELECT * FROM tblBuyerOrderStatusIndicator where Status='true' and BuyerID='" + BuyerID + "'");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string value = Convert.ToString(dt.Rows[0][1]);
                    result = value == "true" ? true : false;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static void UpdateBuyerOrderStatusCheck(string BuyerID)
        {
            try
            {
                string sqlQuery = "update tblBuyerOrderStatusIndicator set Status='false' where BuyerID='" + BuyerID + "'";
                MudarDBHelper mdbh = MudarDBHelper.Instance;
                mdbh.ExecuteDataTable(sqlQuery);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #region Rate/KG in words
        public static DataTable Convert_NumberToWord(decimal TotalValue)
        {
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Number", SqlDbType.Decimal, TotalValue));
            return mdbh.ExecuteDataTable(sp.SP_udf_Num_ToWords, Params, "Totalvalues");
        }
        public static DataTable AmtinWords(decimal TotalValue)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            //return mdbh.ExecuteDataTable("SELECT od.OrderID, odp.ProductID, odp.Packing25, odp.Packing180, bop.NetQuantity, bop.GrossQuantity, ct.BatchID, ct.Lotnumber, pd.ProductName, bo.BranchOrderID, ct.CollectionID FROM dbo.tblOrderDetails od  LEFT JOIN dbo.tblOrderProducts odp ON od.OrderID = odp.OrderID LEFT JOIN dbo.tblBranchOrder bo ON od.OrderID = bo.OrderID LEFT JOIN dbo.tblBranchOrderProduct bop ON bo.BranchOrderID = bop.BranchOrderID AND odp.ProductID = bop.ProductID LEFT JOIN dbo.tblCollection c ON od.OrderID = c.OrderID LEFT JOIN dbo.tblCollectionTransaction ct ON c.CollectionID = ct.CollectionID  LEFT JOIN dbo.tblProductDetails pd ON odp.ProductID = pd.ProductId  WHERE od.OrderID = '" + OrderID + "' AND bo.BranchOrderID = '" + BranchOrderID + "' GROUP BY od.OrderID, odp.ProductID, odp.Packing25, odp.Packing180, bop.NetQuantity, bop.GrossQuantity, ct.BatchID, ct.Lotnumber, pd.ProductName, bo.BranchOrderID, ct.CollectionID ");
            return mdbh.ExecuteDataTable("select value = dbo.fnMoneyToEnglish ('" + TotalValue + "')");
        }
        #endregion
        public static DataTable GetTestFieldDataBasedonProduct(string ProdctID)
        {
            if (ProdctID == "10")
                ProdctID = "3";
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            return mdbh.ExecuteDataTable("SELECT tf.*,tft.* FROM tblTestingFields tf,tblTestingFiledsTrans tft WHERE tf.TestResID = tft.TestResID AND tf.ProductID = '" + ProdctID + "' AND tft.[delete] = 0");
        }
        public static bool InsertTestingResults(int TestID, int CollectionID, int ProductID, string Parameter, string AnalysisValue, string Low, string High, string TestingMethod, string CreatedBy, string ModifiedBy, string LotNumber, int TypeOfOperation)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("TestID", SqlDbType.Int, TestID));
            Params.Add(mdbh.AddParameter("CollectionID", SqlDbType.NVarChar, CollectionID));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.NVarChar, ProductID));
            Params.Add(mdbh.AddParameter("Parameter ", SqlDbType.NVarChar, Parameter));
            Params.Add(mdbh.AddParameter("AnalysisValue ", SqlDbType.NVarChar, AnalysisValue));
            Params.Add(mdbh.AddParameter("Low", SqlDbType.NVarChar, Low));
            Params.Add(mdbh.AddParameter("High", SqlDbType.NVarChar, High));
            Params.Add(mdbh.AddParameter("TestingMethod", SqlDbType.NVarChar, TestingMethod));
            Params.Add(mdbh.AddParameter("CreatedBy", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("ModifiedBy", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("LotNumber", SqlDbType.VarChar, LotNumber));
            Params.Add(mdbh.AddParameter("TypeOfOperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, result, Param_Directions.Param_Out));
            try
            {
                result = (bool)mdbh.ExecuteNonQuery(sp.sp_TestingResults_INS_UPD_DEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static DataTable GetCollectedOrderProduct(int orderId, int productID)
        {
            DataTable resultTable = new DataTable();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select bo.BranchOrderID from tblBranchOrder bo join tblBranchOrderProduct bop on bo.BranchOrderID=bop.BranchOrderID where bo.OrderID=" + orderId + " and bop.ProductID=" + productID + " and bo.[Delete]=0 and bop.[Delete]=0");
            if (dt.Rows.Count > 0)
            {
                int branchOrderId = Convert.ToInt32(dt.Rows[0]["BranchOrderID"]);
                dt = mdbh.ExecuteDataTable("select CollectionID from tblCollection where OrderID=" + orderId + " and BranchOrderID=" + branchOrderId + " and [Delete]=0");
                if (dt.Rows.Count > 0)
                {
                    int collectionId = Convert.ToInt32(dt.Rows[0]["CollectionID"]);
                    dt = mdbh.ExecuteDataTable("select * from  tblCollectionTransaction where CollectionID=" + collectionId + " and ProductID=" + productID + " and [Delete]=0");
                    return dt;
                }
                else
                {
                    return resultTable;
                }
            }
            else
            {
                return resultTable;
            }
        }

        public static DataTable ListLotNumbers(int OrderId, int ProductId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("orderid", SqlDbType.Int, OrderId));
            Params.Add(mdbh.AddParameter("productid", SqlDbType.Int, ProductId));
            return mdbh.ExecuteDataTable(sp.sp_ListBlendingLotNumbers, Params, "ListBlendingLotNumbers");
        }
        public static DataTable ListLotNumbers(int OrderId, int ProductId, string BatchID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("orderid", SqlDbType.Int, OrderId));
            Params.Add(mdbh.AddParameter("productid", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("BatchID", SqlDbType.NVarChar, BatchID));
            return mdbh.ExecuteDataTable(sp.sp_GetBlendingLotNumbers, Params, "GetBlendingLotNumbers");
        }
        public static DataTable ListOrderProducts(int OrderId)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select op.ProductID,Quantity,Packing25,Packing180,od.ProductName from tblOrderProducts op join tblProductDetails od on op.ProductID=od.ProductID where op.OrderID=" + OrderId + " and op.[Delete]=0 and od.[Delete]=0");
            return dt;
        }
        public static DataTable ListTestResults(int CollectionID, int ProductId, string BatchID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("collectionid", SqlDbType.Int, CollectionID));
            Params.Add(mdbh.AddParameter("productid", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("BatchID", SqlDbType.NVarChar, BatchID));
            return mdbh.ExecuteDataTable(sp.sp_Get_TestingResults, Params, "ListTestResults");
        }
        public static DataTable GetTestingResult(int CollectionID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select TestID,CreatedDate from tblTestingResults where CollectionID=" + CollectionID + " and [Delete]=0");
            return dt;
        }
        public static DataTable CheckTestingCompleted(string BlendID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select * from tblTestingResultsTrans where LotNumber='" + BlendID + "' and ProductID='" + ProductID + "'");
            return dt;
        }
        public static DataTable CheckPackingCompleted(string BlendID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select opd.*,pd.ProductName from tblOrderPackingDetails opd,tblProductDetails pd where opd.LotNumber='" + BlendID + "' and opd.ProductID='" + ProductID + "' and pd.ProductID='" + ProductID + "'");
            return dt;
        }
        public static DataSet GetTestingResultDS(int CollectionID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("select TestID,CreatedDate from tblTestingResults where CollectionID=" + CollectionID + " and [Delete]=0 select * from tblTestingResultsTrans where TestID=(select TestID from tblTestingResults where CollectionID=" + CollectionID + ")");
            return mdbh.ExecuteDataSet(sql);
        }
        public static bool TestingResults_INT_UPT_DEL(int CollectionID, string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int TestID)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();

            Params.Add(mdbh.AddParameter("collectionid", SqlDbType.Int, CollectionID));

            Params.Add(mdbh.AddParameter("createdby", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("modifiedby", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("typeofoperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            Params.Add(mdbh.AddParameter("newtesid", SqlDbType.Int, TestID, Param_Directions.Param_Out));

            try
            {
                List<string> output = new List<string>();
                mdbh.ExecuteNonQuery(sp.sp_tblTestResults_INSandUPDandDEL, Params, ref output);
                if (output.Count >= 2)
                {
                    Result = Convert.ToBoolean(output[0]);
                    TestID = Convert.ToInt32(!string.IsNullOrEmpty(output[1]) ? output[1] : "0");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;

        }
        public static bool TestingResultsTrans_INT_UPT_DEL(int TestId, int ProductId, string LotNumber, string Parameter, string AnalysisValue, string Low, string High, string TestingMethod, string CreatedBy, DateTime InsertDate, string ModifiedBy, int TypeOfOperation)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("testid", SqlDbType.Int, TestId));
            Params.Add(mdbh.AddParameter("productid", SqlDbType.Int, ProductId));
            Params.Add(mdbh.AddParameter("lotnumber", SqlDbType.NVarChar, LotNumber));
            Params.Add(mdbh.AddParameter("parameter", SqlDbType.NVarChar, Parameter));
            Params.Add(mdbh.AddParameter("avalue", SqlDbType.NVarChar, AnalysisValue));
            Params.Add(mdbh.AddParameter("low", SqlDbType.NVarChar, Low));
            Params.Add(mdbh.AddParameter("high", SqlDbType.NVarChar, High));
            Params.Add(mdbh.AddParameter("testingmethod", SqlDbType.NVarChar, TestingMethod));
            Params.Add(mdbh.AddParameter("createdby", SqlDbType.NVarChar, CreatedBy));
            Params.Add(mdbh.AddParameter("InsertDate", SqlDbType.NVarChar, InsertDate));
            Params.Add(mdbh.AddParameter("modifiedby", SqlDbType.NVarChar, ModifiedBy));
            Params.Add(mdbh.AddParameter("typeofoperation", SqlDbType.Int, TypeOfOperation));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                List<string> output = new List<string>();
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_tblTestingResultsTrans_INSandUPDandDEL, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool InsertSampleQtyandMsgDetails(string SampQty, string SampDetails, int OrderID, int ProductID, string Lotnumber)
        {
            bool Result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("SampQty", SqlDbType.NVarChar, SampQty));
            Params.Add(mdbh.AddParameter("SampDetails", SqlDbType.NVarChar, SampDetails));
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("ProductID", SqlDbType.Int, ProductID));
            Params.Add(mdbh.AddParameter("Lotnumber ", SqlDbType.NVarChar, Lotnumber));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_SampleQty_Msg_Purpose, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool UpdatetheLotsampleReceivedDate(DateTime LSReceivedDate, string ID, string status)
        {
            bool Result = false;
            MudarSPName sp = new MudarSPName();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("LotSampleID", SqlDbType.NVarChar, ID));
            Params.Add(mdbh.AddParameter("LSReceivedDate", SqlDbType.DateTime, LSReceivedDate));
            Params.Add(mdbh.AddParameter("Status", SqlDbType.NVarChar, status));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_Lotsample_ReceivedDate_Update, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable GetOrderProductDetails(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select pd.ProductName,op.Quantity,RateforKG,TotalPrice from tblOrderDetails od,tblOrderProducts op,tblProductDetails pd where od.OrderID='" + OrderID + "' and op.OrderID='" + OrderID + "' and op.ProductID= pd.ProductId");
            return dt;
        }
        // branch purpose
        public static DataTable GetCommentDetails(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select Admincomments from tblOrderDetails where OrderID='" + OrderID + "'");
            return dt;
        }
        // admin purpose
        public static DataTable GetAdminCommentDetails(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select OrderType,Comments from tblOrderDetails where OrderID='" + OrderID + "'");
            return dt;
        }
        // get sample Qty and Msg details show in the Admin screen
        public static DataTable GetssampleQtyandMsgAdmin(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select * from tblSampleQty where OrderID='" + OrderID + "'");
            return dt;
        }

        public static DataTable GetBranchPathDetails(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select BranchOrderPath,BLSpath from tblBranchOrder where OrderID='" + OrderID + "'");
            return dt;
        }
        // update the Blend date
        public static DataTable GetCollectionTransDate(string OrderID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select ct.[CreatedDate] as CreatedDate  from tblCollectionTransaction ct where CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "') and ProductID='" + ProductID + "' order by ct.CreatedDate desc");
            return dt;
        }
        // update the testing date
        public static DataTable GetBlendTransUpdatedDate(string OrderID, string ProductID, string BlendID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            if (ProductID == "4")
                dt = mdbh.ExecuteDataTable("select CreatedDate from tblFreezeTransaction where ProductID='" + ProductID + "' and FreezeProductBatchID='" + BlendID + "'");
            else
                //dt = mdbh.ExecuteDataTable("select bt.CreatedDate as CreatedDate from tblBlendingTransaction bt where BlendingID=(select BlendingID from tblBlending where CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "')) and ProductID='" + ProductID + "' order by bt.CreatedDate desc");
                dt = mdbh.ExecuteDataTable("select bt.CreatedDate as CreatedDate from tblBlendingTransaction bt where ProductID='" + ProductID + "' AND Blending_BatchID='" + BlendID + "'");
            return dt;
        }
        // update the Packing date
        public static DataTable GetTestTransUpdateDate(string OrderID, string BlendID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select trt.CreatedDate from tblTestingResultsTrans trt where trt.TestID=(select TestID from tblTestingResults where CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "')) and trt.Lotnumber='" + BlendID + "'");
            return dt;
        }
        // Bind the BlendID for Freezing purpose
        public static DataTable GetBlendID(string OrderID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select bt.BlendingTransactionID,bt.Blending_BatchID,bt.bqty from tblBlending b,tblBlendingTransaction bt where bt.BlendingID= b.BlendingID and bt.ProductID='" + ProductID + "' and b.CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "')");
            return dt;
        }
        // for freezing purpose
        public static DataSet CheckBlendIDInsert(int BlendID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("SELECT * FROM tblFreeze WHERE [BlendingTransID] = '" + BlendID + "' SELECT * FROM tblFreezeTransaction WHERE FreezeID = (SELECT FreezeID FROM tblFreeze WHERE [BlendingTransID] = '" + BlendID + "') AND [delete]=0 SELECT Blending_BatchID FROM tblBlendingTransaction WHERE BlendingTransactionID = '" + BlendID + "'");
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataSet CheckBlendIDInsert(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            string sql = ("SELECT * FROM tblFreeze WHERE [BlendingTransID] = (select BlendingTransID from tblFreeze where OrderID='" + OrderID + "') SELECT * FROM tblFreezeTransaction WHERE FreezeID = (SELECT FreezeID FROM tblFreeze WHERE [BlendingTransID] = (select BlendingTransID from tblFreeze where OrderID='" + OrderID + "')) AND [delete]=0");
            return mdbh.ExecuteDataSet(sql);
        }
        public static DataTable GetBlendID(string OrderID, string ProductID, string BlendTranID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select bt.BlendingTransactionID,bt.Blending_BatchID,bt.bqty from tblBlending b,tblBlendingTransaction bt where bt.BlendingID= b.BlendingID and bt.BlendingTransactionID='" + BlendTranID + "' and bt.ProductID='" + ProductID + "' and b.CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "')");
            return dt;
        }
        // Testing check

        // In Testing bind the BlendIDs
        public static DataTable GetBlendIDS(string OrderID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            if (ProductID == "4" || ProductID == "6")
                dt = mdbh.ExecuteDataTable("select FreezeProductBatchID AS Blending_BatchID from tblFreezeTransaction where BlendingID=(select BlendingID from tblBlending where CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "')) and ProductID='" + ProductID + "' ");
            else
                dt = mdbh.ExecuteDataTable("SELECT Blending_BatchID from tblBlendingTransaction where BlendingID=(select BlendingID from tblBlending where CollectionID=(select CollectionID from tblCollection where OrderID='" + OrderID + "')) and ProductID='" + ProductID + "' ");
            return dt;
        }

        //Get the SampleQty and the Msg Details
        public static DataTable GetSampleQtyandMsg(string BlendID, string ProductID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select * from tblSampleQty where Lotnumber ='" + BlendID + "' and ProductID ='" + ProductID + "'");
            return dt;
        }
        public static DataTable GetInvandDispatchDetails(string OrderID)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("SELECT convert(char(11),BOdispatchDate,106) as DispatchDate,BOinv FROM tblbranchorder WHERE OrderID ='" + OrderID + "'");
            return dt;
        }
        public static bool UpdateOrderStatus(string Status, string OrderID)
        {
            bool result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            dt = mdbh.ExecuteDataTable("UPDATE tblOrderDetails SET OrderStatus='" + Status + "',AdminOrderStatus='" + Status + "' from tblorderdetails WHERE OrderID='" + OrderID + "'");
            try
            {
                if (dt.Rows.Count > 0)
                {
                    string value = Convert.ToString(dt.Rows[0][1]);
                    result = value == "true" ? true : false;
                }
                else
                    result = false;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public static bool CancelOrders(string Status, string OrderID)
        {
            bool Result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Status", SqlDbType.NVarChar, Status));
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.NVarChar, OrderID));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_CanceltheOrder, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static DataTable GetCollectionTransactions(string OrderID)
        {
            DataTable resultTable = new DataTable();
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select CollectionID from tblCollection where OrderID='" + OrderID + "'");
            if (dt.Rows.Count > 0)
            {
                int collectionId = Convert.ToInt32(dt.Rows[0]["CollectionID"]);
                dt = mdbh.ExecuteDataTable("select * from  tblCollectionTransaction where CollectionID=" + collectionId + " and [Delete]=0");
                return dt;
            }
            return resultTable;
        }
        public static bool UpdateSoldQtyforCancelOrder(decimal Qty, int PlantationId, int OrderID)
        {
            bool Result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("Qty", SqlDbType.Decimal, Qty));
            Params.Add(mdbh.AddParameter("OrderID", SqlDbType.Int, OrderID));
            Params.Add(mdbh.AddParameter("PlantationId", SqlDbType.Int, PlantationId));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_UpdateSoldQtyforCancelOrder, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        public static bool PreOrderUpdateSoldQtyforCancelOrder(decimal PreorderQty, string Blending_BatchID)
        {
            bool Result = false;
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            MudarSPName sp = new MudarSPName();
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(mdbh.AddParameter("PreorderQty", SqlDbType.Decimal, PreorderQty));
            Params.Add(mdbh.AddParameter("Blending_BatchID ", SqlDbType.NVarChar, Blending_BatchID));
            Params.Add(mdbh.AddParameter("ReturnValue", SqlDbType.Bit, Result, Param_Directions.Param_Out));
            try
            {
                Result = (bool)mdbh.ExecuteNonQuery(sp.sp_PreorderUpdateSoldQtyforCancelOrder, Params);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Result;
        }
        #region Reports for Branch

        public static DataTable GetallcollectionList(int productID, string code)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dtp = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductID='" + productID + "'");
            string cc = dtp.Rows[0]["ProductCode"].ToString() + code;
            if (productID == 2)
            {
                DataTable dt = mdbh.ExecuteDataTable("select ct.*,'Organic Cornmint Oil ' as ProductName from  tblCollectionTransaction ct,tblProductDetails pd where pd.ProductId in ('2','4') and ct.ProductID in ('2','4') and ct.[Delete]=0 and Lotnumber like '%" + cc + "%'");
                return dt;
            }
            else
            {
                DataTable dt = mdbh.ExecuteDataTable("select ct.*,ProductName from  tblCollectionTransaction ct,tblProductDetails pd where pd.ProductId='" + productID + "' and ct.ProductID='" + productID + "' and ct.[Delete]=0 and Lotnumber like '%" + cc + "%'");
                return dt;
            }
        }

        public static DataTable GetcollectionRegisterBasedonproduct(int productID, string code)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = mdbh.ExecuteDataTable("select ProductCode from tblProductDetails where ProductID='" + productID + "'");
            string cc = dt.Rows[0]["ProductCode"].ToString() + code;
            string sql = string.Empty;
            sql = "select ct.*,ProductName,isnull(bt.BQty,0)as BQty from  tblCollectionTransaction ct";
            sql += " left join tblProductDetails pd on pd.ProductId='" + productID + "' and ct.ProductID='" + productID + "'";
            sql += " left join tblBlending b on ct.collectionid=b.CollectionID";
            sql += " left join tblBlendingTransaction bt on b.BlendingID=bt.BlendingID and bt.ProductID='" + productID + "'";
            sql += " where ct.Lotnumber like '%" + cc + "%'";
            return mdbh.ExecuteDataTable(sql);
        }
        #endregion
    }
}
