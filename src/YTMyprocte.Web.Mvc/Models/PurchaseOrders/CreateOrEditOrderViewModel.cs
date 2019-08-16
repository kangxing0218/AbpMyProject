using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTMyprocte.PurchaseOrders.Dto;

namespace YTMyprocte.Web.Models.PurchaseOrders
{
    [AutoMapFrom(typeof(OrderEditDto))]
    public class CreateOrEditOrderViewModel : OrderEditDto
    {
        public bool IsEditMode
        {
            get { return Id.HasValue; }
        }

        public CreateOrEditOrderViewModel(OrderEditDto output)
        {
            output.MapTo(this);
        }
    }
}
