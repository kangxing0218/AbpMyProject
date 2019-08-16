using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.Materiels.Dto;
using YTMyprocte.PurchaseAndSale.Purchases;
using YTMyprocte.PurchaseOrderDetails.Dto;
using YTMyprocte.Suppliers.Dto;

namespace YTMyprocte.PurchaseOrders.Dto
{
    [AutoMap(typeof(PurchaseOrder))]
    public class PurchaseOrderEditDto
    {
        public const int Max128Length = 128;
        public const int Max64Length = 64;
        public const int Max32Length = 32;


        public int? Id { get; set; }
        public string Code { get; set; }
   
        //供货商ID       
        public virtual int SupplierId { get; set; }
        //供应商名称
       
        public virtual double Price { get; set; }
        public DateTime CreationTime { get; set; }
       
        
    }
}
