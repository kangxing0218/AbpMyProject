using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using Abp.Linq.Extensions;
using Abp.UI;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using YTMyprocte.Customers.Dto;
using YTMyprocte.PurchaseAndSale.Customers;
using YTMyprocte.PurchaseAndSale.Materiels;
using YTMyprocte.PurchaseAndSale.Sells;
using YTMyprocte.PurchaseAndSale.StoreManagers;
using YTMyprocte.SellorderDes.Dto;
using YTMyprocte.Sells.Dto;
using YTMyprocte.StoreManagers.Dto;

namespace YTMyprocte.Sells
{
    public class SellAppService : YTMyprocteAppServiceBase, ISellAppService
    {
        private readonly IRepository<Materiel, int> _materielrepository;
        private readonly IRepository<StoreManager, int> _storeManagerRepository;
        private readonly IRepository<Sell, int> _sellRepository;
        private readonly IRepository<SellOrderDe, int> _sellOrderDeRepository;
        private readonly IRepository<Customer, int> _customerRepository;

        public SellAppService(IRepository<Sell, int> sellRepository
            , IRepository<Customer, int> cutomerrepository, IRepository<SellOrderDe, int> sellOrderDerepository, IRepository<Materiel, int> materielrepository, IRepository<StoreManager, int> storeManagerRepository)
        {

            _sellRepository = sellRepository;
            _customerRepository = cutomerrepository;

            _sellOrderDeRepository = sellOrderDerepository;
            _materielrepository = materielrepository;
            _storeManagerRepository = storeManagerRepository;
        }

        public async Task CreateOrUpdateOrder(CreateOrUpdateOutOrderInput input)
        {
            if (input.OutSell.Id.HasValue)
            {
                await UpdateSellAsync(input);
            }
            else
            {
                await CreateSellAsync(input);
            }
        }

        private async Task CreateSellAsync(CreateOrUpdateOutOrderInput input)
        {
            List<SellOrderDeListDto> list = input.OrderDetails;//表中要出库的物料信息,多个物料
            double sum = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                var detail = list[i];
                if (detail.Count == 0 || "".Equals(detail.Count))//表中数量
                {
                    throw new UserFriendlyException($"物料[{detail.MaterielName}]销售数量不能为0！");
                    //temp = true;
                }
                else//表中有数量
                {
                    var storeStorage = await _storeManagerRepository.GetAll().Where(x => x.MaterielId == detail.MaterielId).ProjectTo<StoreManagerListDto>().ToListAsync();
                    if (storeStorage.Count == 0)//当前物料库存
                    {
                        throw new UserFriendlyException($"物料[{detail.MaterielName}]库存为0,请采购！");
                    }
                    else
                    { //出库
                        var amount = 0;
                        for (int k = 0; k < storeStorage.Count; k++)//20
                        {
                            amount = amount + (int)storeStorage[k].CurrentAmount;//当前库存
                        }
                        if (amount < detail.Count)
                        {
                            throw new UserFriendlyException($"[{detail.MaterielName}]库存量为[{amount}]，低于销售量！");
                        }
                        else
                        {
                            sum = sum + detail.OutPrice;
                        }
                    }
                }
            }

            var order = input.OutSell.MapTo<Sell>();
            order.Code = "XS-" + DateTime.Now.ToString("yyyyMMdd-hhmmss");
            order.Price = sum;
            order.IsOutbound = false;
            order.CustomerId = input.OutSell.CustomerId;
            var Code = order.Code;
            await _sellRepository.InsertAsync(order);
            //保存订单详情
            for (int i = 0; i < list.Count(); i++)
            {
                var detail = list[i];
                detail.OrderCode = Code;
                var dd = detail.MapTo<SellOrderDe>();
                //保存订单详情
                await _sellOrderDeRepository.InsertAsync(dd);
            }

        }
        public async Task updateOutboundStatus(EntityDto input) { 
            //保存到仓库
            var order1 = await _sellRepository.GetAsync(input.Id);
            order1.IsOutbound = true;
            //抬头和物料连接
            var orderDetails = await _sellOrderDeRepository.GetAll().Where(x => x.OrderCode.Equals(order1.Code)).ProjectTo<SellOrderDeListDto>().ToListAsync();
            for (int i = 0; i < orderDetails.Count(); i++)
            {
                var detail = orderDetails[i];
                var storeStorage = await _storeManagerRepository.GetAll().Where(x => x.MaterielId == detail.MaterielId).ProjectTo<StoreManagerListDto>().ToListAsync();
                if (storeStorage.Count == 0)
                {
                    throw new UserFriendlyException($"物料[{detail.MaterielName}]库存为0,不可出库！");
                }
                else
                {
                    var amount = 0;
                    for (int k = 0; k < storeStorage.Count; k++)
                    {
                        amount = amount + (int)storeStorage[k].CurrentAmount;
                    }
                    if (amount < detail.Count)
                    {
                        throw new UserFriendlyException($"[{detail.MaterielName}]库存量为[{amount}]，低于销售量！");
                    }
                    else
                    {

                    }
                }
            }

            await _sellRepository.UpdateAsync(order1);
            for (int i = 0; i < orderDetails.Count(); i++)
            {
                var detail = orderDetails[i];
                var storeStorage = await _storeManagerRepository.GetAll().Where(x => x.MaterielId == detail.MaterielId).ProjectTo<StoreManagerListDto>().ToListAsync();
                var sellAmount = detail.Count;
                var sellMoney = detail.OutPrice;//总金额
                for (int j = 0; j < storeStorage.Count; j++)
                {
                    var temp = storeStorage[j].MapTo<StoreManager>();
                    var x = temp.CurrentAmount - sellAmount;//剩余数量
                    temp.CurrentAmount = x;
                    temp.CountMoney = temp.MaterielMoney * Convert.ToDouble((temp.CurrentAmount - sellAmount));  
                    await _storeManagerRepository.UpdateAsync(temp);
                    break;
                    
                }
            }

        }

        private async Task UpdateSellAsync(CreateOrUpdateOutOrderInput input)
        {
            List<SellOrderDeListDto> list = input.OrderDetails;//表中要出库的物料信息,多个物料
            
            var order = await _sellRepository.SingleAsync(x => x.Id == input.OutSell.Id.Value);
            double sum = 0;
            for (int i = 0; i < list.Count(); i++)
            {
                var etail = list[i];
                sum = sum + etail.OutPrice;

            }
            order.CreationTime = input.OutSell.CreationTime;
            order.CustomerId = input.OutSell.CustomerId;
            order.Price = sum;
            await _sellRepository.UpdateAsync(order);
            //先删除订单的详细表，在修改订单详细表
            var orderDetails = _sellOrderDeRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ToList();
            for (int i = 0; i < orderDetails.Count(); i++)
            {
                var orderDetail = orderDetails[i];
                await _sellOrderDeRepository.DeleteAsync(orderDetail);
            }

            for (int i = 0; i < list.Count(); i++)
            {
                var detail = list[i];

                detail.OrderCode = input.OutSell.Code;
                var dd = detail.MapTo<SellOrderDe>();
                await _sellOrderDeRepository.InsertAsync(dd);

            }
        }

        public async Task DeleteOrder(EntityDto input)
        {
            var order = await _sellRepository.GetAsync(input.Id);
            var orderDetails = _sellOrderDeRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ToList();
            for (int i = 0; i < orderDetails.Count(); i++)
            {
                var orderDetail = orderDetails[i];
                await _sellOrderDeRepository.DeleteAsync(orderDetail);
            }
            await _sellRepository.DeleteAsync(order);
        }

        public OutSellDeListDto GetOrderDetails(string orderCode)
        {
            var query = _sellRepository.FirstOrDefault(x => x.Code == orderCode);
            var a = query.MapTo<OutSellDeListDto>();
            var customer = _customerRepository.Get(a.CustomerId);
            var orderDetails = _sellOrderDeRepository.GetAll().Where(x => x.OrderCode == orderCode).ProjectTo<SellOrderDeListDto>().ToList();

            a.Customer = customer.MapTo<CustomerListDto>();
            a.CustomerName = customer.CustomerName;
            a.Outbounds = orderDetails;
            return a;
        }

        public async Task<OutSellEditDto> GetOrderForEdit(NullableIdDto input)
        {
           
            var customers = await _customerRepository.GetAll().ProjectTo<CustomerListDto>().ToListAsync();

            if (input.Id.HasValue)//编辑
            {
                var order = await _sellRepository.GetAsync(input.Id.Value);
                var orderDetail = await _sellOrderDeRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ProjectTo<SellOrderDeListDto>().ToListAsync();
                var dto = new OutSellEditDto
                {
                    Id = order.Id,
                    Code = order.Code,
                    Price = order.Price,
                    SellOrderDe = orderDetail,
                    CustomerId = order.CustomerId,
                    Customers = customers
                };
                return dto;
            }
            else//添加
            {
                return new OutSellEditDto()
                {
                    Customers = customers

                };

            }
        }

        public async Task<PagedResultDto<SellListDto>> GetOrdersAsync(GetSellInput input)
        {
            var query = _sellRepository.GetAll();
            var count = await query.CountAsync();
            var list = await query.OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount).ToListAsync();
            var dots = list.MapTo<List<SellListDto>>();
            foreach (var item in dots)
            {
                var customer = await _customerRepository.GetAsync(item.CustomerId);
                item.CustomerName = customer.CustomerName;
            }
            return new PagedResultDto<SellListDto>(count, dots);
        }

        public async Task<PagedResultDto<SellOrderDeListDto>> GetSellOrder(NullableIdDto input)
        {
            if (input.Id.HasValue)
            {
                var order = await _sellRepository.GetAsync(input.Id.Value);
                var query = await _sellOrderDeRepository.GetAll().Where(x => x.OrderCode.Equals(order.Code)).ProjectTo<SellOrderDeListDto>().ToListAsync();
                var count = query.Count();
                return new PagedResultDto<SellOrderDeListDto>(count, query);
            }
            else
            {
                return new PagedResultDto<SellOrderDeListDto>();
            }
        }
    }
}
