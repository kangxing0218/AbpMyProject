using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.Materiels
{
    [Table("Materiels")]
    public class Materiel : FullAuditedEntity
    {

        public const int Max32Length = 32;
        //商品名称
        [MaxLength(100)]
        public virtual string WaresName { get; set; }
        //型号
        [StringLength(Max32Length)]
        public virtual string Code { get; set; }
        [MaxLength(20)]
        public virtual string WaresType { get; set; }
        //规格
        [Required]
        public virtual string WaresFomat { get; set; }
        //单位
        [MaxLength(20)]
        public virtual string WaresUnit { get; set; }
        [MaxLength(100)]
        public virtual string Manufacturer { get; set; }//生产厂家
        [MaxLength(100)]
        public virtual string Brands { get; set; }//品牌
        public virtual string SellMoney { get; set; }//销售价
        public virtual string BuyMoney { get; set; }//采购价

    }
}
