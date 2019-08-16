
using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using YTMyprocte.StoreManagers.Dto;

namespace YTMyprocte.StoreManagers
{
    public interface IStoreManagerAppService: IApplicationService
    {
        //增  或改
        Task CreateOrUpdateStoreManagerAsync(CreateOrUpdateStoreManagerInput input);
        //获取人的相关信息
        Task<PagedResultDto<StoreManagerListDto>> GetPagedStoreManagerAsync(GetStoreManagerInput input);
        //根据Id获得用户信息
        //NullableIdDto 不允许为空
        Task<StoreManagerListDto> GetStoreManagerByIdAsync(NullableIdDto input);
        Task<StoreManagerEditDto> GetStoreManagerOrEditAsync(NullableIdDto input);

        //根据Id删
        Task DeleteStoreManager(EntityDto input);
    }
}
