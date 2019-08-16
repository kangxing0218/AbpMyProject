using System.Threading.Tasks;
using Abp.Authorization;
using Abp.Runtime.Session;
using YTMyprocte.Configuration.Dto;

namespace YTMyprocte.Configuration
{
    [AbpAuthorize]
    public class ConfigurationAppService : YTMyprocteAppServiceBase, IConfigurationAppService
    {
        public async Task ChangeUiTheme(ChangeUiThemeInput input)
        {
            await SettingManager.ChangeSettingForUserAsync(AbpSession.ToUserIdentifier(), AppSettingNames.UiTheme, input.Theme);
        }
    }
}
