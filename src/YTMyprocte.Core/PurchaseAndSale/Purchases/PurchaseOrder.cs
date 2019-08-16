using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.Purchases
{
    [Table("Purchase")] 
    public class PurchaseOrder : FullAuditedEntity
    {
        public const int Max128Length = 128;
        public const int Max64Length = 64;
        public const int Max32Length = 32;

        // combgrade
        public virtual string Code { get; set; }
        //操作员
        public virtual int EmployeeId { get; set; }
       
        //供货商ID       
        public virtual int SupplierId { get; set; }
        //供应商名称
        [StringLength(Max32Length)]
        public virtual string SupplierName { get; set; }
        public virtual double  Price { get; set; }
        public bool IsInbound { get; set; }//是否入库




    }
}
