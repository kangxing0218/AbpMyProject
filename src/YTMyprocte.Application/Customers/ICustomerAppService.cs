using Abp.Application.Services;
using Abp.Application.Services.Dto;
using System.Threading.Tasks;
using YTMyprocte.Customers.Dto;

namespace YTMyprocte.Customers
{
    public interface ICustomerAppService: IApplicationService
    { 
        //增  或改
        Task CreateOrUpdateCustomerAsync(CreateOrUpdateCustomerInput input);
        //获取人的相关信息
        Task<PagedResultDto<CustomerListDto>> GetPagedCustomersAsync(GetCustomerInput input);
        //根据Id获得用户信息
        //NullableIdDto 不允许为空
        Task<CustomerListDto> GetCustomerByIdAsync(NullableIdDto input);
        Task<CustomerEditDto> GetCustomerOrEditAsync(NullableIdDto input);
       
        //根据Id删
        Task DeleteCustomer(EntityDto input);








    }


}