using System.Threading.Tasks;
using YTMyprocte.Configuration.Dto;

namespace YTMyprocte.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
