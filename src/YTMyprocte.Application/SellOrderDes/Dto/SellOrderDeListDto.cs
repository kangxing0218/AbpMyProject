using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.PurchaseAndSale.Sells;

namespace YTMyprocte.SellorderDes.Dto
{
    [AutoMap(typeof(SellOrderDe))]
    public class SellOrderDeListDto
    {
        public int Id { get; set; }
        public virtual string OrderCode { get; set; }  //与Sell相连接
        public virtual int MaterielId { get; set; }

        
        public virtual string MaterielCode { get; set; }
       
        public virtual string MaterielName { get; set; }
      
        public virtual string GoodsFomat { get; set; }//规格
        public virtual string GoodsUnit { get; set; }//单位
        public virtual double SellMoney { get; set; }//销售价
        //public virtual double BuyMoney { get; set; }//采购价
        public virtual int OutPrice { get; set; } //出库价
        public virtual int Count { get; set; }  //数量
        public DateTime CreationTime { get; set; }

    }
}
