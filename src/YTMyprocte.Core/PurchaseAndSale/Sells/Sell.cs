using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.Sells
{
    [Table("Sell")]
    public class Sell : FullAuditedEntity
    {
        public const int Max128Length = 128;
        public const int Max64Length = 64;
        public const int Max32Length = 32;

        [StringLength(Max32Length)]
        public virtual string Code { get; set; }

        public virtual int CustomerId { get; set; }

        public virtual double Price { get; set; }
        public bool IsOutbound { get; set; }//是否出库
    }
}
