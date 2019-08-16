using System;
using System.Collections.Generic;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YTMyprocte.PurchaseAndSale.Materiels;
using YTMyprocte.PurchaseAndSale.Purchases;
using YTMyprocte.PurchaseAndSale.StoreManagers;
using YTMyprocte.PurchaseAndSale.Suppliers;

using YTMyprocte.PurchaseOrders.Dto;
using YTMyprocte.Suppliers.Dto;
using YTMyprocte.PurchaseOrderDetails.Dto;
using Abp.UI;
using YTMyprocte.StoreManagers.Dto;

namespace YTMyprocte.PurchaseOrders
{
    public class PurchaseOrderAppService : YTMyprocteAppServiceBase, IPurchaseOrderAppService
    {
        
        private readonly IRepository<PurchaseOrder, int> _purchaseOrderRepository;
        private readonly IRepository<Supplier, int> _supplierReository;
       
        
        private readonly IRepository<PurchaseOrderDetail, int> _purchaseOrderDetailRepository;
        private readonly IRepository<Materiel, int> _materielrepository;
        private readonly IRepository<StoreManager, int> _storeManagerRepository;

        public PurchaseOrderAppService(IRepository<PurchaseOrder, int> purchaseOrderRepository
            , IRepository<Supplier, int> supplierrepository,IRepository<PurchaseOrderDetail, int> purchaseOrderDetailrepository, IRepository<Materiel, int> materielrepository, IRepository<StoreManager, int> storeManagerRepository)
        {
           
            _purchaseOrderRepository = purchaseOrderRepository;
            _supplierReository = supplierrepository;

            _purchaseOrderDetailRepository = purchaseOrderDetailrepository;
            _materielrepository = materielrepository;
            _storeManagerRepository = storeManagerRepository;
        }

        public async Task CreateOrUpdateOrderAsync(CreateOrUpdatePurchaseOrderInput input)
        {
            if (input.purchaseOrderEditDto.Id.HasValue)
            {
                await UpdatePurchaseOrderAsync(input);
            }
            else
            {
                await CreatePurchaseOrderAsync(input);
            }
        }

        private async Task UpdatePurchaseOrderAsync(CreateOrUpdatePurchaseOrderInput input)
        {
            List<PurchaseOrederDeListDto> orderList = input.OrderDetails;//订单列表
            var order = await _purchaseOrderRepository.SingleAsync(x => x.Id == input.purchaseOrderEditDto.Id.Value);//订单抬头
            double sum = 0;
            for (int i = 0; i < orderList.Count(); i++)
            {
                var etail = orderList[i];
                sum = sum + etail.UnitPrice;

            }
            order.CreationTime = DateTime.Now;
            order.SupplierId = input.purchaseOrderEditDto.SupplierId;
            order.Price = sum;
            await _purchaseOrderRepository.UpdateAsync(order);
            //先删除订单的详细表，在修改订单详细表
            var orderDetails = _purchaseOrderDetailRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ToList();
            for (int i = 0; i < orderDetails.Count(); i++)
            {
                var orderDetail = orderDetails[i];
                await _purchaseOrderDetailRepository.DeleteAsync(orderDetail);
            }

            for (int i = 0; i < orderList.Count(); i++)
            {
                var detail = orderList[i];

                detail.OrderCode = input.purchaseOrderEditDto.Code;
                var dd = detail.MapTo<PurchaseOrderDetail>();
                await _purchaseOrderDetailRepository.InsertAsync(dd);

            }
        }

        private async Task CreatePurchaseOrderAsync(CreateOrUpdatePurchaseOrderInput input)
        {
            //所有信息中的物料信息
            List<PurchaseOrederDeListDto> orderList = input.OrderDetails;

            var temp = false;
            for (int i = 0; i < orderList.Count(); i++)
            {
                if (orderList[i].Count == 0 || "".Equals(orderList[i].Count))
                {
                    temp = true;
                }
            }
            if (temp)
            {
                throw new UserFriendlyException($"物料采购数量不能为0！");
            }
            else
            {
                //保存订单
                double sum = 0;
                for (int i = 0; i < orderList.Count(); i++)
                {
                    var orderDetail = orderList[i];
                    sum = sum + orderDetail.TotalPrice;
                }
                var order = input.purchaseOrderEditDto.MapTo<PurchaseOrder>();
                order.Code = "DD-" + DateTime.Now.ToString("yyyyMMdd-hhmmss");
                order.Price = sum;
                order.IsInbound = false;
                order.CreationTime = DateTime.Now;
                var Code = order.Code;
                await _purchaseOrderRepository.InsertAsync(order);

                //保存订单详情
                for (int i = 0; i < orderList.Count(); i++)
                {
                    var detail = orderList[i];
                    detail.OrderCode = Code;

                    var dd = detail.MapTo<PurchaseOrderDetail>();

                    //保存订单详情
                    await _purchaseOrderDetailRepository.InsertAsync(dd);
                }
            }
        }
          public async Task UpdateInboundStatusUpdateOrder(EntityDto input) { 

            //保存到仓库
              var orders = await _purchaseOrderRepository.GetAsync(input.Id);
                orders.IsInbound = true;
            var orderDetails = await _purchaseOrderDetailRepository.GetAll().Where(x => x.OrderCode.Equals(orders.Code)).ProjectTo<PurchaseOrederDeListDto>().ToListAsync();
            for (int i = 0; i < orderDetails.Count(); i++)
            {
                var detail = orderDetails[i];
                var storeStorage = _storeManagerRepository.FirstOrDefault(x => x.MaterielId == detail.GoodsId);
                if (storeStorage != null)//有该物料，则库存增加
                {
                    //detail.UnitPrice;
                    storeStorage.CurrentAmount += detail.Count;
                    storeStorage.CountMoney += detail.TotalPrice;
                    
                    //库存总金额==数量*金额
                    await _storeManagerRepository.UpdateAsync(storeStorage);
                }
                else//库存表新增
                {
                    var stock = new StoreStorageCreateDto()
                    {   MaterielUnit= detail.GoodsUnit,
                        MaterielId = detail.GoodsId,
                        MaterielName = detail.GoodsName,
                        CurrentAmount = detail.Count, //当前数量
                        MaterielMoney = detail.UnitPrice,//单价
                        CountMoney = detail.TotalPrice, //总金额
                        
                    };
                    var stockNew = stock.MapTo<StoreManager>();
                    await _storeManagerRepository.InsertAsync(stockNew);
                }
            }
           }

        public async Task DeletePurchaseOrder(EntityDto input)
        {
            var order = await _purchaseOrderRepository.GetAsync(input.Id);
            var orderDetails = _purchaseOrderDetailRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ToList();
            for (int i = 0; i < orderDetails.Count(); i++)
            {
                var orderDetail = orderDetails[i];
                await _purchaseOrderDetailRepository.DeleteAsync(orderDetail);
            }
            await _purchaseOrderRepository.DeleteAsync(order);
        }
        //展示信息
        public async Task<OrderEditDto> GetOrderForEditAsync(NullableIdDto input)
        {
            //查询供应商，并展示供应商
            var suppliers = await _supplierReository.GetAll().ProjectTo<SupplierListDto>().ToListAsync();
          
            if (input.Id.HasValue)//编辑
            {
                var order = await _purchaseOrderRepository.GetAsync(input.Id.Value);
                var orderDetail = await _purchaseOrderDetailRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ProjectTo<PurchaseOrederDeListDto>().ToListAsync();
                var dto = new OrderEditDto
                {
                    Id = order.Id,
                    Code = order.Code,
                    PurOrderDet = orderDetail,
                    Suppliers = suppliers,
                    SupplierId = order.SupplierId

                };
                return dto;
            }
            else//添加
            {
                return new OrderEditDto()
                {
                    Suppliers = suppliers,
                   
                };
            }

        }
        //从物料中添加信息后，加入到详情表
        public async Task<PagedResultDto<PurchaseOrederDeListDto>> GetPagedPurchaseDetail(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var order = await _purchaseOrderRepository.GetAsync(input.Id.Value);
                var query = await _purchaseOrderDetailRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ProjectTo<PurchaseOrederDeListDto>().ToListAsync();
                var count = query.Count();

                return new PagedResultDto<PurchaseOrederDeListDto>(count, query);

            }
            else
            {
                return new PagedResultDto<PurchaseOrederDeListDto>();
            }

        }
        //进入index后的分页
        public async Task<PagedResultDto<PurchaseOrderListDto>> GetPagedOrdersAsync(GetPurchaseOrderInput input)
        {
            var query = _purchaseOrderRepository.GetAll();
            var count =await query.CountAsync();
            var list = await query
                 .OrderBy(input.Sorting)
                .PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            var dtos = list.MapTo<List<PurchaseOrderListDto>>();

            foreach (var item in dtos)
            {
                var supplier = await _supplierReository.GetAsync(item.SupplierId);
                item.SupplierName = supplier.SupplierName;  
            }

            return new PagedResultDto<PurchaseOrderListDto>(count, dtos);
        }

        public PurchaseOrderDetailListDto GetPurchaseOrderDet(string orderCode)
        {
            var orderCode1 =  _purchaseOrderRepository.FirstOrDefault(x => x.Code==orderCode);  //匹配抬头的编码
            var a = orderCode1.MapTo<PurchaseOrderDetailListDto>();//得到供应商的ID

            var suppplier = _supplierReository.Get(a.SupplierId);
           var qq = _purchaseOrderDetailRepository.GetAll().Where(x=>x.OrderCode.Equals(orderCode)).ProjectTo<PurchaseOrederDeListDto>().ToList(); //得到订单详情  编码和订单编码 匹配
            
            a.Supplier = suppplier.MapTo<SupplierListDto>();
            a.SupplierName = suppplier.SupplierName;
            a.PurchaseOrderDet = qq;
            return a;
        }
    }
}
