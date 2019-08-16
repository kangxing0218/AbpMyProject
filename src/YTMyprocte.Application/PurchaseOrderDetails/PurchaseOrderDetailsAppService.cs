using Abp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.Authorization.Users;
using YTMyprocte.PurchaseAndSale.Materiels;
using YTMyprocte.PurchaseAndSale.Purchases;
using YTMyprocte.PurchaseAndSale.StoreManagers;
using YTMyprocte.PurchaseAndSale.Suppliers;

namespace YTMyprocte.PurchaseOrderDetails
{
    public class PurchaseOrderDetailsAppService: YTMyprocteAppServiceBase, IPurchaseOrderDetailsAppService
    {
        private readonly IRepository<PurchaseOrder, int> _purchaseOrderRepository;
        private readonly IRepository<Supplier, int> _supplierReository;
        private readonly User _userManager;

        private readonly IRepository<PurchaseOrderDetail, int> _purchaseOrderDetailRepository;
        private readonly IRepository<Materiel, int> _materielrepository;
        private readonly IRepository<StoreManager, int> _storeManagerRepository;

        public PurchaseOrderDetailsAppService(User userManager, IRepository<PurchaseOrder, int> purchaseOrderRepository
            , IRepository<Supplier, int> supplierrepository, IRepository<PurchaseOrderDetail, int> purchaseOrderDetailrepository, IRepository<Materiel, int> materielrepository, IRepository<StoreManager, int> storeManagerRepository)
        {
            _userManager = userManager;
            _purchaseOrderRepository = purchaseOrderRepository;
            _supplierReository = supplierrepository;

            _purchaseOrderDetailRepository = purchaseOrderDetailrepository;
            _materielrepository = materielrepository;
            _storeManagerRepository = storeManagerRepository;
        }
    }
}
