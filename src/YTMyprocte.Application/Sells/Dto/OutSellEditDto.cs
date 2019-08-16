using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.Customers.Dto;
using YTMyprocte.PurchaseAndSale.Sells;
using YTMyprocte.SellorderDes.Dto;

namespace YTMyprocte.Sells.Dto
{
    [AutoMapFrom(typeof(Sell))]
    public class OutSellEditDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public double Price { get; set; }
        public CustomerListDto Customer { get; set; }
        public List<CustomerListDto> Customers { get; set; }
        public DateTime CreationTime { get; set; }
        public IList<SellOrderDeListDto> SellOrderDe { get; set; }
    }
}
