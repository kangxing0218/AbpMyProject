using Abp.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using YTMyprocte.Materiels.Dto;

namespace YTMyprocte.Web.Models.Materiels
{
    [AutoMapFrom(typeof(MaterielEditDto))]
    public class CreateOrEditMaterielViewModel: MaterielEditDto
    {
        public bool IsEditMode
        {
            get { return Id.HasValue; }
        }

        public CreateOrEditMaterielViewModel(MaterielEditDto output)
        {
            output.MapTo(this);
        }
    }

}
