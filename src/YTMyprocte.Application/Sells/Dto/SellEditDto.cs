using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.PurchaseAndSale.Sells;

namespace YTMyprocte.Sells.Dto
{
    [AutoMap(typeof(Sell))]
    public class SellEditDto
    {
        public int? Id { get; set; }
        public string Code { get; set; }

        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public double Price { get; set; }

        public DateTime CreationTime { get; set; }
        public bool IsOutbound { get; set; }//是否出库
    }
}
