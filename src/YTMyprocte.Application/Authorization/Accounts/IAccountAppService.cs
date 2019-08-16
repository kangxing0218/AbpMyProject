using System.Threading.Tasks;
using Abp.Application.Services;
using YTMyprocte.Authorization.Accounts.Dto;

namespace YTMyprocte.Authorization.Accounts
{
    public interface IAccountAppService : IApplicationService
    {
        Task<IsTenantAvailableOutput> IsTenantAvailable(IsTenantAvailableInput input);

        Task<RegisterOutput> Register(RegisterInput input);
    }
}
