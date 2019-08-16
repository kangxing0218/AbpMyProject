using System.Threading.Tasks;
using Abp.Application.Services;
using YTMyprocte.Sessions.Dto;

namespace YTMyprocte.Sessions
{
    public interface ISessionAppService : IApplicationService
    {
        Task<GetCurrentLoginInformationsOutput> GetCurrentLoginInformations();
    }
}
