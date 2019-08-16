using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YTMyprocte.Configuration;

namespace YTMyprocte.Web.Startup
{
    [DependsOn(typeof(YTMyprocteWebCoreModule))]
    public class YTMyprocteWebMvcModule : AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public YTMyprocteWebMvcModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            Configuration.Navigation.Providers.Add<YTMyprocteNavigationProvider>();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YTMyprocteWebMvcModule).GetAssembly());
        }
    }
}
