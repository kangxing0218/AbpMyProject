using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.StoreManagers
{   //仓库管理
    [Table("StoreManagers")]
    public class StoreManager: FullAuditedEntity
    {
        public const int Max32Length = 32;

        [StringLength(Max32Length)]
        public virtual string Code { get; set; }
        public virtual int MaterielId { get; set; }//物料id
        [MaxLength(100)]
        public virtual string MaterielName { get; set; }//物料名称
        [MaxLength(20)]
        public virtual string MaterielUnit { get; set; }//物料单位
        [Required]
        public virtual double MaterielMoney { get; set; }//物料价格
        
        public int? CurrentAmount { get; set; }//当前库存
        [Required]
        public double CountMoney { get; set; }//总金额

        public int? MinAmount { get; set; }
    }
}
