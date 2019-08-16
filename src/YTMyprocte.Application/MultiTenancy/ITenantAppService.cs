using Abp.Application.Services;
using Abp.Application.Services.Dto;
using YTMyprocte.MultiTenancy.Dto;

namespace YTMyprocte.MultiTenancy
{
    public interface ITenantAppService : IAsyncCrudAppService<TenantDto, int, PagedTenantResultRequestDto, CreateTenantDto, TenantDto>
    {
    }
}

