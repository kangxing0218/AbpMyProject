using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.Sells
{
    [Table("SellOrderDe")]
    public class SellOrderDe : FullAuditedEntity
    {
        public const int Max128Length = 128;
        public const int Max64Length = 64;
        public const int Max32Length = 32;

        [StringLength(Max32Length)]
        public virtual string OrderCode { get; set; }  //与Sell相连接
        public virtual int MaterielId { get; set; }

        [StringLength(Max32Length)]
        public virtual string MaterielCode { get; set; }
        [StringLength(Max32Length)]
        public virtual string MaterielName { get; set; }
        [StringLength(Max32Length)]
        public virtual string GoodsFomat { get; set; }//规格
        public virtual string GoodsUnit { get; set; }//单位
        public virtual double SellMoney { get; set; }//销售价
        public virtual double BuyMoney { get; set; }//采购价
        public virtual int OutPrice { get; set; } //出库价
        public virtual int Count { get; set; }  //数量
    }
}
