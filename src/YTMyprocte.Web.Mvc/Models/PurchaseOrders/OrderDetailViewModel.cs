using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using YTMyprocte.PurchaseOrders.Dto;

namespace YTMyprocte.Web.Models.PurchaseOrders
{
    [AutoMapFrom(typeof(PurchaseOrderDetailListDto))]
    public class OrderDetailViewModel: PurchaseOrderDetailListDto
    {
        
       

        public OrderDetailViewModel(PurchaseOrderDetailListDto output)
        {
            output.MapTo(this);
        }
    }
}
