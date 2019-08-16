using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.PurchaseAndSale.StoreManagers;

namespace YTMyprocte.StoreManagers.Dto
{
    [AutoMapFrom(typeof(StoreManager)),AutoMapTo(typeof(StoreManager))]
    public class StoreStorageCreateDto
    {
        public int? Id { get; set; }
        [MaxLength(50)]
        public virtual int MaterielId { get; set; }//物料id
        [MaxLength(100)]

        public virtual string MaterielName { get; set; }//物料名称
        public virtual string MaterielUnit { get; set; }//物料名称
        public virtual double MaterielMoney { get; set; }//物料价格
        public int CurrentAmount { get; set; }//当前库存
        public double CountMoney { get; set; }//总金额
    }
}
