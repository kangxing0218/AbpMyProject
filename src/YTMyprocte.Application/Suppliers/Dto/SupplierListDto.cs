using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.PurchaseAndSale.Suppliers;

namespace YTMyprocte.Suppliers.Dto
{
    [AutoMapFrom(typeof(Supplier))]
    public class SupplierListDto
    {
        public int? Id { get; set; }
        //供应商名称
        [MaxLength(100)]
        public string SupplierName { get; set; }
        public virtual string Code { get; set; }
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
