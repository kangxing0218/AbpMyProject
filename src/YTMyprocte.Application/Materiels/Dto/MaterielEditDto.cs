using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.PurchaseAndSale.Materiels;

namespace YTMyprocte.Materiels.Dto
{
    [AutoMap(typeof(Materiel))]
    public class MaterielEditDto
    {
        public int? Id { get; set; }
        //商品名称
        [MaxLength(100)]
        public virtual string WaresName { get; set; }
        public virtual string Code { get; set; }
        //型号
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
