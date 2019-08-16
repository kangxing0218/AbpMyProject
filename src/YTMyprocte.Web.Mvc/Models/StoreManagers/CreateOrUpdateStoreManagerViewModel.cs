using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTMyprocte.StoreManagers.Dto;

namespace YTMyprocte.Web.Models.StoreManagers
{
    [AutoMapFrom(typeof(StoreManagerEditDto))]
    public class CreateOrUpdateStoreManagerViewModel: StoreManagerEditDto
    { 
        public bool IsEditMode
        {
            get { return Id.HasValue; }
        }

        public CreateOrUpdateStoreManagerViewModel(StoreManagerEditDto output)
        {
            output.MapTo(this);
        }
    }
}
