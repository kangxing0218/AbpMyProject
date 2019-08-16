using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using YTMyprocte.PurchaseAndSale.Customers;

namespace YTMyprocte.Customers.Dto
{
    [AutoMap(typeof(Customer))]
    public class CustomerEditDto
    {
        public int? Id { get; set; }
        //客户名称
        [MaxLength(100)]
        [Remote("CheckName", "Customers", ErrorMessage = "名字重复")]
        public virtual string CustomerName { get; set; }
        public virtual string Code { get; set; }
        //电话
        [Required]
        [Phone]
        public virtual string CustomerTel { get; set; }
        //地址
        [MaxLength(100)]
        public virtual string CustomerAddr { get; set; }
    }
}
