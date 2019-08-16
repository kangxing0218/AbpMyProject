using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YTMyprocte.Configuration;

namespace YTMyprocte.Web.Host.Startup
{
    [DependsOn(
       typeof(YTMyprocteWebCoreModule))]
    public class YTMyprocteWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public YTMyprocteWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(YTMyprocteWebHostModule).GetAssembly());
        }
    }
}
