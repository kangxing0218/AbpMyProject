using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.SellorderDes.Dto;

namespace YTMyprocte.Sells.Dto
{
    public class CreateOrUpdateOutOrderInput
    {
        [Required]
        public SellEditDto OutSell { get; set; }


        [Required]
        public List<SellOrderDeListDto> OrderDetails { get; set; }
    }
}
