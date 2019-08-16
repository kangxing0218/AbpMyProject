using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTMyprocte.Suppliers.Dto;

namespace YTMyprocte.Web.Models.Suppliers
{
    [AutoMapFrom(typeof(SupplierEditDto))]
    public class CreateOrEditSupplierViewModel: SupplierEditDto
    {
        public bool IsEditMode
        {
            get { return Id.HasValue; }
        }

        public CreateOrEditSupplierViewModel(SupplierEditDto output)
        {
            output.MapTo(this);
        }
    }
}
