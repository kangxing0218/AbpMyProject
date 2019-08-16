using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace YTMyprocte.PurchaseAndSale.Purchases
{
    [Table("PurchaseDetail")]
    public class PurchaseOrderDetail: FullAuditedEntity
    {
        public const int Max128Length = 128;
        public const int Max64Length = 64;
        public const int Max32Length = 32;
        /**
        入库详情
             */

        public virtual string OrderCode { get; set; }  //采购单号
        //商品ID
        public virtual int GoodsId { get; set; }
        public string Code { get; set; }
        public virtual string MaterielCode { get; set; }//编码
        //商品名称
        [StringLength(Max32Length)]
        public virtual string GoodsName { get; set; }
        [StringLength(Max32Length)]
        public virtual string GoodsUnit { get; set; } //商品单位  

        public virtual string WaresFomat { get; set; } //商品规格
        //数量
        public virtual int Count { get; set; }
        // 单价
        public virtual double UnitPrice { get; set; }
        //总价
        public virtual double TotalPrice { get; set; }


    }

}
