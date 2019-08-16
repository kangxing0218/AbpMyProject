using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using YTMyprocte.Suppliers.Dto;

namespace YTMyprocte.Suppliers
{
    public interface ISupplierAppService : IApplicationService
    {
        //增  或改
        Task CreateOrUpdateSupplierAsync(CreateOrUpdateSupplierInput input);
        //获取人的相关信息
        Task<PagedResultDto<SupplierListDto>> GetPagedSuppliersAsync(GetSupplierInput input);
        //根据Id获得用户信息
        //NullableIdDto 不允许为空
        Task<SupplierListDto> GetSupplierByIdAsync(NullableIdDto input);
        Task<SupplierEditDto> GetSupplierOrEditAsync(NullableIdDto input);

        //根据Id删
        Task DeleteSupplier(EntityDto input);
    }
}
