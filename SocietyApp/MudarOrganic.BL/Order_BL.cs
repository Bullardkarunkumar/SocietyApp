using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MudarOrganic.DL;
using System.Data;

namespace MudarOrganic.BL
{
    public class Order_BL
    {
        public bool OrderInsert(int OrderID, string BuyerID, string BuyerAddress, string OrderStatus
               , bool Approval, decimal TotalAmount, DateTime OrderDate, int PriceTermsID, int TransportID, string OriginCountry
               , string LoadingPort, string PaymentTerms, string FreightTerms, string DestinationCountry, string DestinationPort
               , DateTime OrderDispatchDate, string BranchID, string PurchaseOrderPath, string PurchaseOrderID, string CreatedBy
               , string ModifiedBy, string LSpath, string Comments, string OrderType, string LotSampleID, int OrgType, string Admincomments, string Transport, int TypeOfOperation, ref int ReturnOrderID)
        {
            return Order_DL.OrderInsert(OrderID, BuyerID, BuyerAddress, OrderStatus
               , Approval, TotalAmount, OrderDate, PriceTermsID, TransportID, OriginCountry
               , LoadingPort, PaymentTerms, FreightTerms, DestinationCountry, DestinationPort
               , OrderDispatchDate, BranchID, PurchaseOrderPath, PurchaseOrderID, CreatedBy
               , ModifiedBy, LSpath, Comments, OrderType, LotSampleID, OrgType, Admincomments, Transport, TypeOfOperation, ref  ReturnOrderID);
        }
        public bool OrderInsert(int OrderID, string OrderStatus, decimal TotalAmount, DateTime OrderDate, string PaymentTerms, string FreightTerms, string DestinationPort,
               string PurchaseOrderPath, string PurchaseOrderID, string ModifiedBy, string Comments, string OrderType, string Admincomments, string Transport, int TypeOfOperation)
        {
            return Order_DL.OrderInsert(OrderID, OrderStatus,TotalAmount, OrderDate,PaymentTerms, FreightTerms, DestinationPort,
                PurchaseOrderPath, PurchaseOrderID, ModifiedBy, Comments, OrderType, Admincomments, Transport, TypeOfOperation);
        }
        public bool OrderProducts_INT_UPT_DEL(int OrderID, int ProductID, decimal RateperKG,decimal TotalPrice, string CreatedBy, string ModifiedBy, int Quantity, int Packing25, int Packing180, int TypeOfOperation)
        {
            return Order_DL.OrderProducts_INT_UPT_DEL(OrderID, ProductID, RateperKG, TotalPrice, CreatedBy, ModifiedBy, Quantity, Packing25, Packing180, TypeOfOperation);
        }
        public bool OrderDetails_UPD(int OrderID, string OrderStatus, string ModifiedBy, string Comments)
        {
            return Order_DL.OrderDetails_UPD(OrderID, OrderStatus, ModifiedBy, Comments);
        }
        public  bool OrderDetails_UPD(string OrderPdfPath, string ModifiedBy, string POID, int OrderID)
        {
            return Order_DL.OrderDetails_UPD(OrderPdfPath, ModifiedBy, POID, OrderID);
        }
        public bool OrderDetails_UPD(int OrderID, string OrderPdfPath, int type, string ModifiedBy)
        {
            return Order_DL.OrderDetails_UPD(OrderID, OrderPdfPath, type, ModifiedBy);
        }
        public bool OrderDetails_UPD(int OrderID)
        {
            return Order_DL.OrderDetails_UPD(OrderID);
        }
        public bool BranchOrderDetails_UPD(int BranchOrderID, string OrderStatus, string ModifiedBy, string Comments)
        {
            return Order_DL.BranchOrderDetails_UPD(BranchOrderID, OrderStatus, ModifiedBy, Comments);
        }
        public bool UpdateOrderBranch(string branchId, int OrderID)
        {
            return Order_DL.UpdateOrderBranch(branchId, OrderID);
        }
        public bool BranchOrderDetails_UPD(int BranchOrderID, string OrderStatus, string ModifiedBy,string Comments,DateTime BOdispatchDate, string BOinv)
        {
            return Order_DL.BranchOrderDetails_UPD(BranchOrderID, OrderStatus, ModifiedBy, Comments, BOdispatchDate, BOinv);
        }
        public bool BranchOrderDetails_UPD(int BranchOrderID, string BrnachOrderPath, int type, string ModifiedBy)
        {
            return Order_DL.BranchOrderDetails_UPD(BranchOrderID, BrnachOrderPath, type, ModifiedBy);
        }
        public DataTable ReturnPOList(string POvalue)
        {
            return Order_DL.ReturnPOList(POvalue);
        }
        public DataTable ReturnLotSampleList(string POvalue)
        {
            return Order_DL.ReturnLotSampleList(POvalue);
        }
        public DataTable ReturnBPOList(string BPovalue)
        {
            return Order_DL.ReturnBPOList(BPovalue);
        }
        public  DataTable OrderList(string OrderStatus,Guid branchId)
        {
            return Order_DL.OrderList(OrderStatus, branchId);
        }
        public DataTable OrderSortList(string OrderStatus, string Status)
        {
            return Order_DL.OrderSortList(OrderStatus, Status);
        }
        public DataTable OrderList(string OrderStatus,string mode)
        {
            return Order_DL.OrderList(OrderStatus,mode);
        }
        public DataTable OrderList(int Orderid)
        {
            return Order_DL.OrderList(Orderid);
        }
        public DataTable GetOrderDetails(int Orderid)
        {
            return Order_DL.GetOrderDetails(Orderid);
        }
        public DataTable OrderbyBuyer(string BuyerID)
        {
            return Order_DL.OrderbyBuyer(BuyerID);
        }
        public DataTable OrderbyBuyer(string BuyerID,string mode)
        {
            return Order_DL.OrderbyBuyer(BuyerID,mode);
        }
        public DataTable OrderDetailsBasedonBuyerIDandLotSample(string BuyerID, string LotsampleID)
        {
            return Order_DL.OrderDetailsBasedonBuyerIDandLotSample(BuyerID,LotsampleID);
        }
        public DataTable BindOrderProductList(int OrderID)
        {
            return Order_DL.BindOrderProductList(OrderID);
        }
        public DataTable BindBuyerOrderProductList(int OrderID)
        {
            return Order_DL.BindBuyerOrderProductList(OrderID);
        }
        public DataTable BindAdminOrderProductList(int OrderID)
        {
            return Order_DL.BindAdminOrderProductList(OrderID);
        }
        public DataTable BindBranchOrderProductList(int OrderID)
        {
            return Order_DL.BindBranchOrderProductList(OrderID);
        }
        public DataTable OrderProductList(int OrderID)
        {
            return Order_DL.OrderProductList(OrderID);
        }
        public DataTable OrderProductList(string POID)
        {
            return Order_DL.OrderProductList(POID);
        }
        public DataTable OrderProductList(int OrderID, int BranchOrderID)
        {
            return Order_DL.OrderProductList(OrderID, BranchOrderID);
        }
        public DataTable Ordercountry(string OrderID)
        {
            return Order_DL.Ordercountry(OrderID);
        }
        public DataTable GetSplitIDCount(string OrderID)
        {
            return Order_DL.GetSplitIDCount(OrderID);
        }
        public DataTable BranchOrderList(string OrderStatus)
        {
            return Order_DL.BranchOrderList(OrderStatus);
        }
        public DataTable BranchOrderList(string OrderStatus,string Status)
        {
            return Order_DL.BranchOrderList(OrderStatus,Status);
        }
        public DataTable BranchOrderList(string OrderStatus, int OrderTo, string OrderToID)
        {
            DataTable dt = BranchOrderList(OrderStatus);
            DataTable Result = dt.Clone();
            DataRow[] drs = dt.Select("OrderTo = " + OrderTo + " AND OrderToID = '" + OrderToID + "'");
            foreach (DataRow dr in drs)
                Result.ImportRow(dr);
            return Result;
        }
        public DataTable BranchOrderList(string OrderStatus, int OrderTo, string OrderToID, string Status)
        {
            DataTable dt = BranchOrderList(OrderStatus,Status);
            DataTable Result = dt.Clone();
            DataRow[] drs = dt.Select("OrderTo = " + OrderTo + " AND OrderToID = '" + OrderToID + "'");
            foreach (DataRow dr in drs)
                Result.ImportRow(dr);
            return Result;
        }
        public DataTable OrderTrack(string OrderID)
        {
            return Order_DL.OrderTrack(OrderID);
        }
        public DataTable GetPreOrderData()
        {
            return Order_DL.GetPreOrderData();
        }
        public DataTable OrderProduct_BPO(int OrderID)
        {
            return Order_DL.OrderProduct_BPO(OrderID);
        }
        public bool BranchOrderInsert(int OrderID, decimal TotalAmount, string OrderStatus, string BranchPOID, DateTime BranchOrderDate, string CreatedBy, string ModifiedBy, string Comments, int TypeOfOperation, ref int ReturnBranchOrderID, int OrderTo, string OrderToID)
        {
            return Order_DL.BranchOrderInsert(OrderID, TotalAmount, OrderStatus, BranchPOID, BranchOrderDate, CreatedBy, ModifiedBy, Comments, TypeOfOperation, ref ReturnBranchOrderID, OrderTo, OrderToID);
        }
        public bool BranchOrderProduct_INSandUPDandDEL(int BranchOrderID, int ProductID, decimal NetQuantity, decimal GrossQuantity, decimal Amount, decimal BranchRateforKG, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Order_DL.BranchOrderProduct_INSandUPDandDEL(BranchOrderID, ProductID, NetQuantity, GrossQuantity, Amount, BranchRateforKG, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public DataTable Order_Buyer_Branch_Details(int OrderID)
        {
            return Order_DL.Order_Buyer_Branch_Details(OrderID);
        }
        public DataTable CollectedProductDetails(int OrderID)
        {
            return Order_DL.CollectedProductDetails(OrderID);
        }
        public DataTable CollectedProductDetails_Product(int ProductID)
        {
            return Order_DL.CollectedProductDetails_Product(ProductID);
        }
        public DataTable GetBranchOrderDetails(string orderID, int ProductID)
        {
            return Order_DL.GetBranchOrderDetails(orderID, ProductID);
        }

        public DataTable GetBranchOrderDetails(int orderID)
        {
            return Order_DL.GetBranchOrderDetails(orderID);
        }
        public DataTable CollectedProductDetailsBasedonProduct(int ProductID, string ProductionYear, string name)
        {
            return Order_DL.CollectedProductDetailsBasedonProduct(ProductID, ProductionYear,name);
        }
        public DataTable CollectedProductDetailsBasedonProduct(int ProductID, string ProductionYear)
        {
            return Order_DL.CollectedProductDetailsBasedonProduct(ProductID,ProductionYear);
        }
        public DataTable CollectedProductDetailsBasedonProductandICs(int ProductID, string ProductionYear, string selectedICS)
        {
            return Order_DL.CollectedProductDetailsBasedonProductandICs(ProductID, ProductionYear, selectedICS);
        }
        public DataTable CollectedProductDetailsBasedonProductandICs(int ProductID, string ProductionYear, string selectedICS, DateTime SelDate)
        {
            return Order_DL.CollectedProductDetailsBasedonProductandICs(ProductID, ProductionYear, selectedICS,SelDate);
        }
        public DataTable CollectedProductDetailsBasedonProductandICsNew(int ProductID, string ProductionYear, string selectedICS)
        {
            return Order_DL.CollectedProductDetailsBasedonProductandICsNew(ProductID,ProductionYear,selectedICS);
        }
        public DataTable CollectedProductDetailsBasedonProductandICsNew(int ProductID, string selectedICS)
        {
            return Order_DL.CollectedProductDetailsBasedonProductandICsNew(ProductID, selectedICS);
        }
        public DataTable CollectedProductDetails(int OrderID, int ProductID, string BranchOrderID)
        {
            return Order_DL.CollectedProductDetails(OrderID, ProductID, BranchOrderID);
        }
        public DataTable CollectedProduct(int OrderID, int ProductID, int BranchOrderID)
        {
            return Order_DL.CollectedProduct(OrderID, ProductID, BranchOrderID);
        }
        public DataTable CollectedProduct_Product(int ProductID)
        {
            return Order_DL.CollectedProduct(ProductID);
        }
        public DataSet ReturnBatchList(string BatchID)
        {
            return Order_DL.ReturnBatchList(BatchID);
        }
        public DataTable ReturnFreezeBatch(string FBID)
        {
            return Order_DL.ReturnFreezeBatch(FBID);
        }
        public bool ProductsCollection_Insert(ref int CollectionID, int OrderID, int BranchOrderID, string CreatedBy, string ModifiedBy, int TypeOfOperation, bool IsPreOrder)
        {
            return Order_DL.ProductsCollection_Insert(ref CollectionID, OrderID, BranchOrderID, CreatedBy, ModifiedBy, TypeOfOperation, IsPreOrder);
        }
        public bool ProductsCollectionTran_Insert(string BatchID, int CollectionID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string CollectedQty, string FarmId, string LotNumber, string CreatedBy, DateTime InsertDate, string ModifiedBy, int TypeOfOperation, string PlantationID)
        {
            return Order_DL.ProductsCollectionTran_Insert(BatchID, CollectionID, ProductId, FarmerId, OtherFarmerNames, OtherFarmerArea, OtherFarmerQty, CollectedQty, FarmId, LotNumber, CreatedBy, InsertDate ,ModifiedBy, TypeOfOperation, PlantationID);
        }
        public bool ProductsBlending_Insert(ref int BlendingID, int CollectionID, string CreatedBy, string ModifiedBy, int TypeOfOperation)
        {
            return Order_DL.ProductsBlending_Insert(ref BlendingID, CollectionID, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public bool ProductsBlendingTran_Insert(int BlendingID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string BlendQty, string FarmId, string BatchNo, string LotNumber, decimal Bqty, string CreatedBy, DateTime InsertDate, string ModifiedBy, int TypeOfOperation, string PlantationID)
        {
            return Order_DL.ProductsBlendingTran_Insert(BlendingID, ProductId, FarmerId, OtherFarmerNames, OtherFarmerArea, OtherFarmerQty, BlendQty, FarmId, BatchNo, LotNumber, Bqty, CreatedBy, InsertDate, ModifiedBy, TypeOfOperation, PlantationID);
        }
        public bool ProductsPreorderCollectionTran_Insert(string BatchID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string CollectedQty, string FarmId, string LotNumber, string CreatedBy, string ModifiedBy, int TypeOfOperation, string PlantationID, int TotalQty, int AvaliableQty, int SoldQty, int PreOrderID)
        {
            return Order_DL.ProductsPreorderCollectionTran_Insert(BatchID, ProductId, FarmerId, OtherFarmerNames, OtherFarmerArea, OtherFarmerQty, CollectedQty, FarmId, LotNumber, CreatedBy, ModifiedBy, TypeOfOperation, PlantationID, TotalQty, AvaliableQty, SoldQty, PreOrderID); 
        }
        public bool ProductsPreorderCollectionTran_Insert_New(string BatchID, int ProductId, string FarmerId, string OtherFarmerNames, string OtherFarmerArea, string OtherFarmerQty, string CollectedQty, string FarmId, string LotNumber, string CreatedBy, string ModifiedBy, int TypeOfOperation, string PlantationID, decimal TotalQty, decimal AvaliableQty, int SoldQty, int PreOrderID)
        {
            return Order_DL.ProductsPreorderCollectionTran_Insert_New(BatchID, ProductId, FarmerId, OtherFarmerNames, OtherFarmerArea, OtherFarmerQty, CollectedQty, FarmId, LotNumber, CreatedBy, ModifiedBy, TypeOfOperation, PlantationID, TotalQty, AvaliableQty, SoldQty, PreOrderID); 
        }
        public DataTable PackingDetails(int OrderID, int BranchOrderID)
        {
            return Order_DL.PackingDetails(OrderID, BranchOrderID);
        }
        public bool PackingDetailsUpdate(int OrderID, int ProductID, int BranchOrderID, decimal GrossQty, int Packing25, int Packing180, string ModifiedBy, int TypeOfOperation)
        {
            return Order_DL.PackingDetailsUpdate(OrderID, ProductID, BranchOrderID, GrossQty, Packing25, Packing180, ModifiedBy, TypeOfOperation);
        }
        public bool OrderProductPackingInsert(int CollectionID, int ProductID, string LotNumber, int Packing25, int Packing180, int Drum25From, int Drum25To, int Drum180From, int Drum180To, DateTime InsertDate)
        {
            return Order_DL.OrderProductPackingInsert(CollectionID, ProductID, LotNumber, Packing25, Packing180, Drum25From, Drum25To, Drum180From, Drum180To, InsertDate);
        }
        public DataTable Convert_NumberToWord(decimal TotalValue)
        {
            return Order_DL.Convert_NumberToWord(TotalValue);
        }
        public DataTable AmtinWords(decimal TotalValue)
        {
            return Order_DL.AmtinWords(TotalValue);
        }
        public bool OrderSampleDetails(int SampleID,string BuyerID ,DateTime SampleDate, string Status, string CourierName, DateTime CourierDate, string ReceivedDate, string CourierAccountNumber,string CreatedBy, string ModifiedBy, ref int ReturnSampleID,int TypeOfOperation)
        {
            return Order_DL.OrderSampleDetails(SampleID, BuyerID, SampleDate, Status, CourierName, CourierDate, ReceivedDate, CourierAccountNumber, CreatedBy, ModifiedBy, ref ReturnSampleID, TypeOfOperation);
        }
        public bool OrderSampleProductDetails(int SampleProductID, int ProductID, int SampleID, int Quantity,string CreatedBy, string ModifiedBy,int TypeOfOperation)
        {
            return Order_DL.OrderSampleProductDetails(SampleProductID, ProductID, SampleID, Quantity, CreatedBy, ModifiedBy, TypeOfOperation);
        }
        public DataTable OrderSampleDetails(string BuyerID)
        {
            return Order_DL.OrderSampleDetails(BuyerID);
        }
        public DataTable OrderSampleDetails()
        {
            return Order_DL.OrderSampleDetails();
        }
        public DataTable OrderSampleProduct(int SampleID)
        {
            return Order_DL.OrderSampleProduct(SampleID);
        }
        public DataTable BranchOrderDetails(string BranchPOID)
        {
            return Order_DL.BranchOrderDetails(BranchPOID);
        }
        #region Blending
        public DataTable GetCollectionBlendDetails(string BlendID)
        {
            return Order_DL.GetCollectionBlendDetails(BlendID);
        }
        public DataTable GetBliendingDetails(string OrderID, string BOrderID,string ProductID)
        {
            return Order_DL.GetBliendingDetails(OrderID, BOrderID, ProductID);
        }
        public DataTable GetCollectedOrderProduct(int orderId, int productID)
        {
            return Order_DL.GetCollectedOrderProduct(orderId, productID);
        }
       
        public DataTable GetCollectionID(string OrderID)
        {
            return Order_DL.GetCollectionID(OrderID);
        }
        public  DataTable GetBliendingDetails(string CollectionID, string ProductID)
        {
            return Order_DL.GetBliendingDetails(CollectionID, ProductID);
        }
        public DataTable GetBlendFarmerDetails(string FarmerID, string FarmID)
        {
            return Order_DL.GetBlendFarmerDetails(FarmerID, FarmID);
        }
        public DataTable GetBlendDetailsBasedonBlendID(string BlendID, string CollectionID,string ProductID)
        {
            return Order_DL.GetBlendDetailsBasedonBlendID(BlendID,CollectionID,ProductID);
        }
        public DataTable ListOrderProducts(int OrderId)
        {
            return Order_DL.ListOrderProducts(OrderId);
        }
        public DataTable GetBlendDetailsBasedonBlendID(string CollectionID)
        {
            return Order_DL.GetBlendDetailsBasedonBlendID(CollectionID);
        }
        public DataTable ReturnBBatchList(string BBatchID)
        {
            return Order_DL.ReturnBBatchList(BBatchID);
        }
        
        public bool BlendingBatchNo(int CTID, string BBatchID, string ModifiedBy, int TypeOfOperation)
        {
            return Order_DL.BlendingBatchNo(CTID, BBatchID, ModifiedBy, TypeOfOperation);
        }

        public DataTable ListOrderPackingDetails(int CollectionID)
        {
            return Order_DL.ListOrderPackingDetails(CollectionID);
        }

        public bool UpdateOrderETA(int OrderID, string ETA)
        {
            return Order_DL.UpdateOrderETA(OrderID, ETA);
        }
        public DataTable GetPackingDetailsforCrystal(string BlendID, string ProductID)
        {
            return Order_DL.GetPackingDetailsforCrystal(BlendID, ProductID);
        }
        public DataTable GetPackingDetails(string BlendID, string ProductID)
        {
            return Order_DL.GetPackingDetails(BlendID, ProductID);
        }
        #endregion

        #region Freeze
        public DataTable GetFreezeBatchID(int OrderID, int ProductID)
        {
            return Order_DL.GetFreezeBatchID(OrderID, ProductID);
        }

        public DataTable GetFreezeDetails(string OrderID, string BOrderID)
        {
            return Order_DL.GetFreezeDetails(OrderID, BOrderID);
        }
        public DataTable GetFreezeDetails(string OrderID, string BOrderID, int BlendTransID)
        {
            return Order_DL.GetFreezeDetails(OrderID, BOrderID, BlendTransID);
        }
        public DataTable GetFreeDetails(string id)
        {
            return Order_DL.GetFreeDetails(id);
        }
        public DataTable ReturnFBatchList(string BBatchID)
        {
            return Order_DL.ReturnFBatchList(BBatchID);
        }
        public DataSet GetFreezeTran(string FID, string ProductID)
        {
            return Order_DL.GetFreezeTran(FID, ProductID);
        }
        public DataSet GetFreezeTran(string FID)
        {
            return Order_DL.GetFreezeTran(FID);
        }
        public bool Freeze_Insert(int OrderID, int BranchOrderID, string CreatedBy, string ModifiedBy, int Quntatiy, int ProductID, string FBatchID, int TypeOfOperation, DateTime StartDate, DateTime EndDate, int BlendTranID, ref int Fid)
        {
            return Order_DL.Freeze_Insert(OrderID, BranchOrderID, CreatedBy, ModifiedBy, Quntatiy, ProductID, FBatchID, TypeOfOperation, StartDate, EndDate, BlendTranID, ref Fid);
        }
        public bool FreezeTran_INSandUPDandDEL(int FreezeTransactionID, int FreezeID, int ProductID, string FreezeProductBatchID, string FreezeProductName, string FreezeQuantity, string CreatedBY, string ModifiedBy, int TypeOfOperation)
        {
            return Order_DL.FreezeTran_INSandUPDandDEL(FreezeTransactionID, FreezeID, ProductID, FreezeProductBatchID, FreezeProductName, FreezeQuantity, CreatedBY, ModifiedBy, TypeOfOperation);
        }
        public DataTable BindFreezeProduct()
        {
            return Order_DL.BindFreezeProduct();
        }
        public  DataTable BindBlendTranID(string OrderID)
        {
            return Order_DL.BindBlendTranID(OrderID);
        }
        #endregion

        #region Pre Order
        public bool PreorderSoldQtyUpdate(int PreOrderID, int SoldQty, string ModifiedBy)
        {
            return Order_DL.PreorderSoldQtyUpdate(PreOrderID, SoldQty, ModifiedBy);
        }
        public bool PreorderSoldQtyUpdateNew(int PreOrderID, decimal SoldQty, string ModifiedBy)
        {
            return Order_DL.PreorderSoldQtyUpdateNew(PreOrderID, SoldQty, ModifiedBy);
        }
        public DataTable PreOrderList()
        {
            return Order_DL.PreOrderList();
        }

        public DataTable PreOrderList(string icsCode)
        {
            return Order_DL.PreOrderList(icsCode);
        }
        public DataTable PreOrderList_Product(int ProductID, string icsCode)
        {
            DataTable DT = PreOrderList(icsCode);
            DataRow[] drs = DT.Select("ProductID = " + ProductID);

            DataTable ResultDT = DT.Clone();
            foreach (DataRow dr in drs)
                ResultDT.ImportRow(dr);
            return ResultDT;
        }
        public DataTable PreOrderList(int PreOrderID)
        {
            DataTable DT = PreOrderList();
            DataRow[] drs = DT.Select("CollectionTransactionID = " + PreOrderID);

            DataTable ResultDT = DT.Clone();
            foreach (DataRow dr in drs)
                ResultDT.ImportRow(dr);
            return ResultDT;
        }
        public DataTable GetPreorderDetails(string BlendBatchID)
        {
            return Order_DL.GetPreorderDetails(BlendBatchID);
        }
        
        #endregion

        #region Testing
        public DataTable GetTestFieldDataBasedonProduct(string ProdctID)
        {
            return Order_DL.GetTestFieldDataBasedonProduct(ProdctID);
        }
        public bool InsertTestingResults(int TestID, int CollectionID, int ProductID, string Parameter, string AnalysisValue, string Low, string High, string TestingMethod, string CreatedBy, string ModifiedBy,string LotNumber, int TypeOfOperation)
        {
            return Order_DL.InsertTestingResults(TestID, CollectionID, ProductID, Parameter, AnalysisValue, Low, High, TestingMethod, CreatedBy, ModifiedBy, LotNumber, TypeOfOperation);
        }

        public DataTable ListLotNumbers(int OrderId,int ProductId)
        {
            return Order_DL.ListLotNumbers(OrderId,ProductId);
        }
        public  DataTable ListLotNumbers(int OrderId, int ProductId, string BatchID)
        {
            return Order_DL.ListLotNumbers(OrderId, ProductId, BatchID);
        }
        #endregion

        #region Orders for Admin Checkit
        public bool NewOrderArrived()
        {
            return Order_DL.NewOrderArrived();
        }
        public void updateTrackPO()
        {
            Order_DL.UpdateTrackPO();
        }
        public bool NewLotSampleArrived()
        {
            return Order_DL.NewLotSampleArrived();
        }
        public void Updatelotsample()
        {
            Order_DL.UpdateLotSample();
        }
        #endregion

        #region Any Transactions for superadmin
        public DataTable GetTransactionsBuyerOrderDetails(string todaydate)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            try
            {
                dt = mdbh.ExecuteDataTable("SELECT od.OrderID,convert(char(11),od.OrderDate,106) as OrderDate,od.PurchaseOrderID,bd.BuyerCompanyName,od.OrderStatus,od.CreatedDate,CONVERT(varchar,od.CreatedDate , 121) as seachdate FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId WHERE od.[Delete]=0 and od.OrderDate= '" + todaydate + "' ORDER BY od.CreatedDate DESC");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetTransactionsBasedOnDate(string Date)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            try
            {
                dt = mdbh.ExecuteDataTable("SELECT od.OrderID,convert(char(11),od.OrderDate,106) as OrderDate,od.PurchaseOrderID,bd.BuyerCompanyName,od.OrderStatus,od.CreatedDate FROM tblOrderDetails od LEFT JOIN tblBuyerDetails bd ON od.BuyerID=bd.BuyerId WHERE od.[Delete]=0 and od.CreatedDate= '" + Date + "' ORDER BY od.CreatedDate DESC");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        public DataTable GetTransactionsBranchOrderDetails(string todayDate)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            try
            {
                dt = mdbh.ExecuteDataTable("SELECT CreatedDate as seachdate FROM tblBranchOrder WHERE BranchOrderDate='" + todayDate + "' AND [Delete]=0 ORDER BY BranchOrderDate DESC");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;

        }
        public DataTable GetTransactionsBranchOrdesBasedonDate(string Date)
        {
            MudarDBHelper mdbh = MudarDBHelper.Instance;
            DataTable dt = new DataTable();
            try
            {
                dt = mdbh.ExecuteDataTable("SELECT OrderId,BranchPOID,OrderStatus,BranchOrderDate,CreatedDate FROM tblBranchOrder WHERE CreatedDate='" + Date + "' AND [Delete]=0 ORDER BY BranchOrderDate DESC");
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return dt;
        }
        #endregion

        #region Orders for Branch checkit
        public bool NewBranchOrderArrived()
        {
            return Order_DL.NewBranchOrderArrived();
        }
        public void updateBranchPendingOrders()
        {
            Order_DL.updateBranchPendingOrders();
        }  
        #endregion

        // packing purpose for code
       
        public bool BuyerOrderStatusCheck(string BuyerID)
        {
            return Order_DL.BuyerOrderStatusCheck(BuyerID);
        }
        public void UpdateBuyerOrderStatusCheck(string BuyerID)
        {
            Order_DL.UpdateBuyerOrderStatusCheck(BuyerID);
        }
        public bool TestingResultsTrans_INT_UPT_DEL(int TestId, int ProductId, string LotNumber, string Parameter, string AnalysisValue, string Low, string High, string TestingMethod, string CreatedBy, DateTime InsertDate, string ModifiedBy, int TypeOfOperation)
        {
            return Order_DL.TestingResultsTrans_INT_UPT_DEL(TestId, ProductId, LotNumber, Parameter, AnalysisValue, Low, High, TestingMethod, CreatedBy, InsertDate, ModifiedBy, TypeOfOperation);
        }
        public bool InsertSampleQtyandMsgDetails(string SampQty, string SampDetails, int OrderID, int ProductID, string Lotnumber)
        {
            return Order_DL.InsertSampleQtyandMsgDetails( SampQty, SampDetails, OrderID, ProductID,  Lotnumber);
        }
        public bool TestingResults_INT_UPT_DEL(int CollectionID,  string CreatedBy, string ModifiedBy, int TypeOfOperation, ref int TestID)
        {
            return Order_DL.TestingResults_INT_UPT_DEL(CollectionID, CreatedBy, ModifiedBy, TypeOfOperation, ref TestID);
        }
        public DataTable ListTestResults(int CollectionID, int ProductId, string BatchID)
        {
            return Order_DL.ListTestResults(CollectionID, ProductId, BatchID);
        }
        public  bool  UpdatetheLotsampleReceivedDate(DateTime LSReceivedDate, string ID,string updstatus)
        {
            return Order_DL.UpdatetheLotsampleReceivedDate(LSReceivedDate, ID, updstatus);
        }
        public DataTable GetTestingResult(int CollectionID)
        {
            return Order_DL.GetTestingResult(CollectionID);
        }
        public DataTable CheckTestingCompleted(string BlendID, string ProductID)
        {
            return Order_DL.CheckTestingCompleted(BlendID, ProductID);
        }
        public DataTable CheckPackingCompleted(string BlendID, string ProductID)
        {
            return Order_DL.CheckPackingCompleted(BlendID, ProductID);
        }
        public DataSet GetTestingResultDS(int CollectionID)
        {
            return Order_DL.GetTestingResultDS(CollectionID);
        }
        public DataTable GetOrderProductDetails(string OrderID)
        {
            return Order_DL.GetOrderProductDetails(OrderID);
        }
        public DataTable GetCommentDetails(string OrderID)
        {
            return Order_DL.GetCommentDetails(OrderID);
        }
        public DataTable GetBranchPathDetails(string OrderID)
        {
            return Order_DL.GetBranchPathDetails(OrderID);
        }
        public DataTable GetAdminCommentDetails(string OrderID)
        {
            return Order_DL.GetAdminCommentDetails(OrderID);
        }
        public DataTable GetssampleQtyandMsgAdmin(string OrderID)
        {
            return Order_DL.GetssampleQtyandMsgAdmin(OrderID);
        }
        #region Updated Dates
        //Reports and update dates in Testing
        public DataTable GetCollectionTransDate(string OrderID, string ProductID)
        {
             return Order_DL.GetCollectionTransDate( OrderID,ProductID);
        }
        //Reports and update dates in Testing
        public DataTable GetBlendTransUpdatedDate(string OrderID, string ProductID, string BlendID)
        {
            return Order_DL.GetBlendTransUpdatedDate(OrderID,ProductID,BlendID);
        }
        public DataTable GetTestTransUpdateDate(string OrderID, string BlendID)
        {
            return Order_DL.GetTestTransUpdateDate(OrderID, BlendID);
        }
        #endregion
        public DataTable GetBlendID(string OrderID, string ProductID)
        {
            return Order_DL.GetBlendID(OrderID,ProductID);
        }
        public DataSet CheckBlendIDInsert(int BlendID)
        {
            return Order_DL.CheckBlendIDInsert(BlendID);
        }
        public DataSet CheckBlendIDInsert(string OrderID)
        {
            return Order_DL.CheckBlendIDInsert(OrderID);
        }
        public DataTable GetBlendID(string OrderID, string ProductID, string BlendTranID)
        {
            return Order_DL.GetBlendID(OrderID, ProductID, BlendTranID);
        }
        public DataTable GetBlendIDS(string OrderID, string ProductID)
        {
            return Order_DL.GetBlendIDS(OrderID, ProductID);
        }
        public DataTable GetSampleQtyandMsg(string BlendID, string ProductID)
        {
            return Order_DL.GetSampleQtyandMsg(BlendID, ProductID);
        }
        public DataTable GetInvandDispatchDetails(string OrderID)
        {
            return Order_DL.GetInvandDispatchDetails(OrderID);
        }
        public bool UpdateOrderStatus(string Status, string OrderID)
        {
            return Order_DL.UpdateOrderStatus(Status, OrderID);
        }
        public bool CancelOrders(string Status, string OrderID)
        {
            return Order_DL.CancelOrders(Status, OrderID);
        }
        public DataTable GetCollectionTransactions(string OrderID)
        {
            return Order_DL.GetCollectionTransactions(OrderID);
        }
        public bool UpdateSoldQtyforCancelOrder(decimal Qty,int PlantationId,int OrderID)
        {
            return Order_DL.UpdateSoldQtyforCancelOrder(Qty, PlantationId, OrderID);
        }
        public bool PreOrderUpdateSoldQtyforCancelOrder(decimal PreorderQty, string Blending_BatchID)
        {
            return Order_DL.PreOrderUpdateSoldQtyforCancelOrder(PreorderQty, Blending_BatchID);
        }
        #region Reports for Branch
        public DataTable GetallcollectionList(int productID, string code)
        {
            return Order_DL.GetallcollectionList(productID, code);
        }
        public DataTable GetcollectionRegisterBasedonproduct(int productID, string code)
        {
            return Order_DL.GetcollectionRegisterBasedonproduct(productID, code);
        } 
        #endregion
       
    }
}
