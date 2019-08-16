using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.PurchaseAndSale.Sells;

namespace YTMyprocte.Sells.Dto
{
    [AutoMapFrom(typeof(Sell))]
    public class SellListDto
    {
        public int Id { get; set; }
        public string Code { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public double Price { get; set; }
        public bool IsOutbound { get; set; }//是否出库
        public DateTime CreationTime { get; set; }
    }
}
