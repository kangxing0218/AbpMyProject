using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.PurchaseAndSale.Purchases;
using YTMyprocte.PurchaseOrderDetails.Dto;
using YTMyprocte.Suppliers.Dto;

namespace YTMyprocte.PurchaseOrders.Dto
{
    [AutoMapFrom(typeof(PurchaseOrder))]
    public class OrderEditDto
    {
        public const int Max128Length = 128;
        public const int Max64Length = 64;
        public const int Max32Length = 32;


        public int? Id { get; set; }
        public string Code { get; set; }

        public List<SupplierListDto> Suppliers { get; set; }

        //供货商ID       
        public virtual int SupplierId { get; set; }
        //供应商名称
        [StringLength(Max32Length)]
        public virtual string SupplierName { get; set; }
        public virtual double Price { get; set; }
        public DateTime CreationTime { get; set; }
        public IList<PurchaseOrederDeListDto> PurOrderDet { get; set; }
    }
}
