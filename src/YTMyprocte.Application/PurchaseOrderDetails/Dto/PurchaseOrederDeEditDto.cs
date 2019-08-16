using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.Materiels.Dto;
using YTMyprocte.PurchaseAndSale.Purchases;
using YTMyprocte.Suppliers.Dto;

namespace YTMyprocte.PurchaseOrderDetails.Dto
{
    [AutoMapFrom(typeof(PurchaseOrderDetail))]
    public class PurchaseOrederDeEditDto
    {
        public const int Max128Length = 128;
        public const int Max64Length = 64;
        public const int Max32Length = 32;
        /**
        入库详情
             */
        public int? Id { get; set; }
        public virtual string Code { get; set; }

        //商品ID
        public virtual int GoodsId { get; set; }
        public virtual int SupplierId { get; set; }
        public List<MaterielListDto> Materiels { get; set; }
        public List<SupplierListDto> Suppliers { get; set; }

     
         
        //数量
        public virtual int Count { get; set; }
        // 单价
        public virtual double UnitPrice { get; set; }
        //总价
        public virtual double TotalPrice { get; set; }

    }
}
