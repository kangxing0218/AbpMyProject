using Abp.Runtime.Validation;
using System;
using System.Collections.Generic;
using System.Text;
using YTMyprocte.Dto;

namespace YTMyprocte.Materiels.Dto
{
    public class GetMaterielInput : PagedAndSortedInputDto, IShouldNormalize
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
