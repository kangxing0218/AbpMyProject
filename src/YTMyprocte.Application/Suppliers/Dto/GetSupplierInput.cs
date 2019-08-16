using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.Dto;

namespace YTMyprocte.Suppliers.Dto
{
    public class GetSupplierInput : PagedAndSortedInputDto, IShouldNormalize
    {
        public void Normalize()
        {
            if (string.IsNullOrEmpty(Sorting))
            {
                Sorting = "id";
            }

        }
    }
}
