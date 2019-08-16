using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.PurchaseOrderDetails.Dto;

namespace YTMyprocte.PurchaseOrders.Dto
{
    public class CreateOrUpdatePurchaseOrderInput
    {
        public PurchaseOrderEditDto purchaseOrderEditDto { get; set; }

        public List<PurchaseOrederDeListDto> OrderDetails { get; set; }


    }
}
