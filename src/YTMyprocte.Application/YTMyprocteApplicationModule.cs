using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using YTMyprocte.Authorization;

namespace YTMyprocte
{
    [DependsOn(
        typeof(YTMyprocteCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class YTMyprocteApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<YTMyprocteAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(YTMyprocteApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
