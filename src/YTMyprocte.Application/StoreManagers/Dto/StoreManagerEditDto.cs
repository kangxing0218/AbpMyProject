﻿using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.Materiels.Dto;
using YTMyprocte.PurchaseAndSale.StoreManagers;

namespace YTMyprocte.StoreManagers.Dto
{
    [AutoMap(typeof(StoreManager))]
    public class StoreManagerEditDto
    {
        public int? Id { get; set; }
      
        public virtual int MaterielId { get; set; }//物料id
        public virtual string Code { get; set; }
        [MaxLength(100)]
        public virtual string MaterielName { get; set; }//物料名称
        [MaxLength(20)]
        public virtual string MaterielUnit { get; set; }//物料单位
        [Required]
        public virtual string MaterielMoney { get; set; }//物料价格
   
        public int? CurrentAmount { get; set; }//当前库存
        [Required]
        public double CountMoney { get; set; }//总金额

        public int? MinAmount { get; set; }

       //// public IList<MaterielListDto> Materiels { get; set; }
        
    }
}