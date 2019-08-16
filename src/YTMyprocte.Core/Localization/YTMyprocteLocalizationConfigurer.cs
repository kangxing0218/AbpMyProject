using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace YTMyprocte.Localization
{
    public static class YTMyprocteLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(YTMyprocteConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(YTMyprocteLocalizationConfigurer).GetAssembly(),
                        "YTMyprocte.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
