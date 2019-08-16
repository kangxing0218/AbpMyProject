using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTMyprocte.Customers.Dto;

namespace YTMyprocte.Web.Models.Customers
{
    [AutoMapFrom(typeof(CustomerEditDto))]
    public class CreateOrUpdateCustomerViewModel:CustomerEditDto
    {
        public bool IsEditMode
        {
            get { return Id.HasValue; }
        }

        public CreateOrUpdateCustomerViewModel(CustomerEditDto output)
        {
            output.MapTo(this);
        }
    }
}
