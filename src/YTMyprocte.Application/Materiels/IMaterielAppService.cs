using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YTMyprocte.Materiels.Dto;

namespace YTMyprocte.Materiels
{
   public interface IMaterielAppService:IApplicationService
    {
        //增  或改
        Task CreateOrUpdateMaterielAsync(CreateOrUpdateMaterielInput input);
        //获取人的相关信息
        Task<PagedResultDto<MaterielListDto>> GetPagedMaterielAsync(GetMaterielInput input);
        //根据Id获得用户信息
        //NullableIdDto 不允许为空
        Task<MaterielListDto> GetMaterielByIdAsync(NullableIdDto input);
        Task<MaterielEditDto> GetMaterielOrEditAsync(NullableIdDto input);

        //根据Id删
        Task DeleteMateriel(EntityDto input);
    }
}
