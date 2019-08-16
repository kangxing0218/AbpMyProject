using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTMyprocte.Sells.Dto;

namespace YTMyprocte.Web.Models.SellOrders
{
    [AutoMapFrom(typeof(OutSellDeListDto))]
    public class OrderDetailListViewModel: OutSellDeListDto
    {
       

        public OrderDetailListViewModel(OutSellDeListDto output)
        {
            output.MapTo(this);
        }
    }
}
