using Abp.Application.Services.Dto;
using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.PurchaseAndSale.Customers;

namespace YTMyprocte.Customers.Dto
{
    [AutoMapFrom(typeof(Customer))]
    public class CustomerListDto
    {
        public int? Id { get; set; }
        //客户名称
        public virtual string CustomerName { get; set; }
        public virtual string Code { get; set; }
        //密码
        // public virtual string CustomerPwd { get; set; }

        //电话
        public virtual string CustomerTel { get; set; }
        //地址
        public virtual string CustomerAddr { get; set; }
    }
}
