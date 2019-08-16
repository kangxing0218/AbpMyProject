using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTMyprocte.Sells.Dto;

namespace YTMyprocte.Web.Models.SellOrders
{
    [AutoMapFrom(typeof(OutSellEditDto))]
    public class CreateOrEditSellOrderViewModel: OutSellEditDto
    {
       
        public CreateOrEditSellOrderViewModel(OutSellEditDto output)
        {
            output.MapTo(this);
        }

        public bool IsEditMode
        {
            get { return Id.HasValue; }
        }
    }
}
