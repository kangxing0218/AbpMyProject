using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.Suppliers
{    //供应商
    [Table("Suppliers")]
    public class Supplier: FullAuditedEntity
    {
        public const int Max32Length = 32;

        [StringLength(Max32Length)]
        public virtual string Code { get; set; }
        //供应商名称
        [MaxLength(100)]
        public string SupplierName { get; set; }
        //联系人
        //[MaxLength(50)]
        //public string ContactPwd { get; set; }
        //联系电话
        [Required]
        public string PersonTel { get; set; }
        //地址
        [MaxLength(200)]
        public string SupplierAddr { get; set; }
    }
}
