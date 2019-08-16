using Abp.Domain.Entities.Auditing;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.Customers
{     //客户
    [Table("Customers")]
    public class Customer: FullAuditedEntity
    {
        public const int Max64Length = 64;
        public const int Max32Length = 32;

        [StringLength(Max32Length)]
        public virtual string Code { get; set; }
        //客户名称
        [StringLength(Max32Length)]
        
        public virtual string CustomerName { get; set; }
        //密码
        //[MaxLength(20)]
        //public virtual string CustomerPwd { get; set; }
        //电话
        [StringLength(Max32Length)]
        [Phone]
        public virtual string CustomerTel { get; set; }
        //地址
        [StringLength(Max64Length)]
        public virtual string CustomerAddr { get; set; }
    }
}
